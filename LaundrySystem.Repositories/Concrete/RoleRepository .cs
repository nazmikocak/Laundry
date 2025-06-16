using LaundrySystem.Entities.Models;
using LaundrySystem.Repositories.Abstract;
using LaundrySystem.Repositories.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace LaundrySystem.Repositories.Concrete
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(IConfiguration configuration) : base(configuration)
        {
        }



        public override async Task<IEnumerable<Role>> GetAllAsync()
        {
            var roles = new List<Role>();

            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand("sp_GetAllRoles", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                roles.Add(RoleMapper.MapToRole(reader));
            }

            return roles;
        }



        public override async Task<Role?> GetByIdAsync(int id)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand("sp_GetRoleById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@RoleId", id);

            await using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return RoleMapper.MapToRole(reader);
            }

            return null;
        }



        public override async Task CreateAsync(Role entity)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand("sp_CreateRole", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@RoleName", entity.RoleName);

            await command.ExecuteNonQueryAsync();
        }



        public override async Task UpdateAsync(Role entity)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand("sp_UpdateRole", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@RoleId", entity.RoleId);
            command.Parameters.AddWithValue("@RoleName", entity.RoleName);

            await command.ExecuteNonQueryAsync();
        }



        public override async Task DeleteAsync(int id)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand("sp_DeleteRole", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@RoleId", id);

            await command.ExecuteNonQueryAsync();
        }
    }
}