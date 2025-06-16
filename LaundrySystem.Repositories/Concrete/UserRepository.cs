using LaundrySystem.Entities.Models;
using LaundrySystem.Repositories.Abstract;
using LaundrySystem.Repositories.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace LaundrySystem.Repositories.Concrete
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
        }



        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = new List<User>();
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand("sp_GetAllUsers", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                users.Add(UserMapper.MapToUserWithJoins(reader));
            }

            return users;
        }



        public override async Task<User?> GetByIdAsync(int id)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            await using var command = new SqlCommand("sp_GetUserById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@UserId", id);
            await using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return UserMapper.MapToUser(reader);
            }
            return null;
        }



        public async Task<User?> GetUserByEmailAsync(string email)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            await using var command = new SqlCommand("sp_GetUserByEmail", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@Email", email);
            await using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return UserMapper.MapToUser(reader);
            }
            return null;
        }



        public override async Task CreateAsync(User entity)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            await using var command = new SqlCommand("sp_CreateUser", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            UserMapper.AddUserParameters(command, entity);

            var newId = await command.ExecuteScalarAsync();
        }



        public override async Task UpdateAsync(User entity)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            await using var command = new SqlCommand("sp_UpdateUser", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@UserId", entity.UserId);
            command.Parameters.AddWithValue("@FirstName", entity.FirstName);
            command.Parameters.AddWithValue("@LastName", entity.LastName);
            command.Parameters.AddWithValue("@Email", entity.Email);
            command.Parameters.AddWithValue("@PhoneNumber", (object)entity.PhoneNumber ?? DBNull.Value);
            command.Parameters.AddWithValue("@DormitoryId", (object)entity.DormitoryId ?? DBNull.Value);
            command.Parameters.AddWithValue("@RoleId", entity.RoleId);

            await command.ExecuteNonQueryAsync();
        }



        public async Task UpdatePasswordAsync(int userId, string passwordHash)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            await using var command = new SqlCommand("sp_UpdateUserPassword", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@NewPasswordHash", passwordHash);

            await command.ExecuteNonQueryAsync();
        }



        public override async Task DeleteAsync(int id)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            await using var command = new SqlCommand("sp_DeleteUser", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@UserId", id);
            await command.ExecuteNonQueryAsync();
        }
    }
}