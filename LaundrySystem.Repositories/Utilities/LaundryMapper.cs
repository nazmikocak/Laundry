using LaundrySystem.Entities.Enums;
using LaundrySystem.Entities.Models;
using Microsoft.Data.SqlClient;

namespace LaundrySystem.Repositories.Utilities
{
    public static class LaundryMapper
    {
        public static Laundry MapToLaundry(SqlDataReader reader)
        {
            return new Laundry
            {
                LaundryId = Convert.ToInt32(reader["LaundryId"]),
                DormitoryId = Convert.ToInt32(reader["DormitoryId"]),
                LocationDescription = reader["LocationDescription"].ToString()!,
                SessionDurationMinutes = Convert.ToInt32(reader["SessionDurationMinutes"]),
                Status = Enum.Parse<LaundryStatus>(reader["Status"].ToString()!, true)
            };
        }

        public static void AddLaundryParameters(SqlCommand command, Laundry entity)
        {
            command.Parameters.AddWithValue("@DormitoryId", entity.DormitoryId);
            command.Parameters.AddWithValue("@LocationDescription", entity.LocationDescription);
            command.Parameters.AddWithValue("@SessionDurationMinutes", entity.SessionDurationMinutes);
            command.Parameters.AddWithValue("@Status", entity.Status.ToString());
        }
    }
}