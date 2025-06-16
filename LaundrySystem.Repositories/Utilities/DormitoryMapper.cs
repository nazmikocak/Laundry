using LaundrySystem.Entities.Models;
using Microsoft.Data.SqlClient;

namespace LaundrySystem.Repositories.Utilities
{
    public static class DormitoryMapper
    {
        public static Dormitory MapToDormitory(SqlDataReader reader)
        {
            return new Dormitory
            {
                DormitoryId = Convert.ToInt32(reader["DormitoryId"]),
                DormitoryName = reader["DormitoryName"].ToString()!,
                City = reader["City"].ToString()!,
                Address = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : null
            };
        }
    }
}