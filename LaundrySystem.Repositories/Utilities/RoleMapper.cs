using LaundrySystem.Entities.Models;
using Microsoft.Data.SqlClient;

namespace LaundrySystem.Repositories.Utilities
{
    public static class RoleMapper
    {
        public static Role MapToRole(SqlDataReader reader)
        {
            return new Role
            {
                RoleId = Convert.ToInt32(reader["RoleId"]),
                RoleName = reader["RoleName"].ToString()!
            };
        }
    }
}