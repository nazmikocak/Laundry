using LaundrySystem.Entities.Models;
using LaundrySystem.Repositories.Abstract;
using LaundrySystem.Repositories.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace LaundrySystem.Repositories.Concrete
{
    public class DormitoryRepository : RepositoryBase<Dormitory>, IDormitoryRepository
    {
        public DormitoryRepository(IConfiguration configuration) : base(configuration)
        {
        }



        public override async Task<IEnumerable<Dormitory>> GetAllAsync()
        {
            var dormitories = new List<Dormitory>();

            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand("sp_GetAllDormitories", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                dormitories.Add(DormitoryMapper.MapToDormitory(reader));
            }

            return dormitories;
        }



        public override async Task<Dormitory?> GetByIdAsync(int id)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand("sp_GetDormitoryById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@DormitoryId", id);

            await using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return DormitoryMapper.MapToDormitory(reader);
            }

            return null;
        }



        public override async Task CreateAsync(Dormitory entity)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand("sp_CreateDormitory", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@DormitoryName", entity.DormitoryName);
            command.Parameters.AddWithValue("@City", entity.City);
            command.Parameters.AddWithValue("@Address", (object)entity.Address ?? DBNull.Value);

            await command.ExecuteNonQueryAsync();
        }



        public override async Task UpdateAsync(Dormitory entity)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand("sp_UpdateDormitory", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@DormitoryId", entity.DormitoryId);
            command.Parameters.AddWithValue("@DormitoryName", entity.DormitoryName);
            command.Parameters.AddWithValue("@City", entity.City);
            command.Parameters.AddWithValue("@Address", (object)entity.Address ?? DBNull.Value);

            await command.ExecuteNonQueryAsync();
        }



        public override async Task DeleteAsync(int id)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand("sp_DeleteDormitory", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@DormitoryId", id);

            await command.ExecuteNonQueryAsync();
        }
    }
}