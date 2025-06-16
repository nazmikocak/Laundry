using LaundrySystem.Entities.Models;
using Microsoft.Data.SqlClient;

namespace LaundrySystem.Repositories.Utilities
{
    public static class UserMapper
    {
        public static User MapToUser(SqlDataReader reader)
        {
            return new User
            {
                UserId = Convert.ToInt32(reader["UserId"]),
                FirstName = reader["FirstName"].ToString()!,
                LastName = reader["LastName"].ToString()!,
                Email = reader["Email"].ToString()!,
                PasswordHash = reader["PasswordHash"].ToString()!,
                PhoneNumber = reader["PhoneNumber"] != DBNull.Value ? reader["PhoneNumber"].ToString() : null,
                NationalNumber = reader["NationalNumber"].ToString()!,
                DormitoryId = reader["DormitoryId"] != DBNull.Value ? Convert.ToInt32(reader["DormitoryId"]) : null,
                RoleId = Convert.ToInt32(reader["RoleId"]),
                Role = new Role { RoleId = Convert.ToInt32(reader["RoleId"]), RoleName = reader["RoleName"].ToString()! },
                Dormitory = reader["DormitoryId"] != DBNull.Value ? new Dormitory { DormitoryId = Convert.ToInt32(reader["DormitoryId"]), DormitoryName = reader["DormitoryName"].ToString()! } : null
            };
        }

        public static User MapToUserWithJoins(SqlDataReader reader)
        {
            return new User
            {
                UserId = Convert.ToInt32(reader["UserId"]),
                FirstName = reader["FirstName"].ToString()!,
                LastName = reader["LastName"].ToString()!,
                Email = reader["Email"].ToString()!,
                PhoneNumber = reader["PhoneNumber"] != DBNull.Value ? reader["PhoneNumber"].ToString() : null,
                NationalNumber = reader["NationalNumber"].ToString()!,
                Role = new Role { RoleName = reader["RoleName"].ToString()! },
                Dormitory = reader["DormitoryName"] != DBNull.Value ? new Dormitory { DormitoryName = reader["DormitoryName"].ToString()! } : null
            };
        }

        public static void AddUserParameters(SqlCommand command, User entity)
        {
            command.Parameters.AddWithValue("@FirstName", entity.FirstName);
            command.Parameters.AddWithValue("@LastName", entity.LastName);
            command.Parameters.AddWithValue("@Email", entity.Email);
            command.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash);
            command.Parameters.AddWithValue("@PhoneNumber", (object)entity.PhoneNumber ?? DBNull.Value);
            command.Parameters.AddWithValue("@NationalNumber", entity.NationalNumber);
            command.Parameters.AddWithValue("@DormitoryId", (object)entity.DormitoryId ?? DBNull.Value);
            command.Parameters.AddWithValue("@RoleId", entity.RoleId);
        }
    }
}