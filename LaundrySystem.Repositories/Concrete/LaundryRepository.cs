using LaundrySystem.Entities.Enums;
using LaundrySystem.Entities.Models;
using LaundrySystem.Repositories.Abstract;
using LaundrySystem.Repositories.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace LaundrySystem.Repositories.Concrete
{
    public class LaundryRepository : RepositoryBase<Laundry>, ILaundryRepository
    {
        public LaundryRepository(IConfiguration configuration) : base(configuration)
        {
        }



        public override async Task<IEnumerable<Laundry>> GetAllAsync()
        {
            throw new NotImplementedException("sp_GetAllLaundries stored procedure is not defined.");
        }



        public async Task<IEnumerable<Laundry>> GetLaundriesByDormitoryAsync(int dormitoryId)
        {
            var laundries = new List<Laundry>();
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand("sp_GetLaundriesByDormitory", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@DormitoryId", dormitoryId);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                laundries.Add(LaundryMapper.MapToLaundry(reader));
            }

            return laundries;
        }



        public override async Task<Laundry?> GetByIdAsync(int id)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand("sp_GetLaundryById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@LaundryId", id);

            await using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return LaundryMapper.MapToLaundry(reader);
            }

            return null;
        }



        public override async Task CreateAsync(Laundry entity)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand("sp_CreateLaundry", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            LaundryMapper.AddLaundryParameters(command, entity);

            await command.ExecuteNonQueryAsync();
        }



        public override async Task UpdateAsync(Laundry entity)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand("sp_UpdateLaundry", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@LaundryId", entity.LaundryId);
            LaundryMapper.AddLaundryParameters(command, entity);

            await command.ExecuteNonQueryAsync();
        }



        public override async Task DeleteAsync(int id)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand("sp_DeleteLaundry", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@LaundryId", id);

            await command.ExecuteNonQueryAsync();
        }
    }
}