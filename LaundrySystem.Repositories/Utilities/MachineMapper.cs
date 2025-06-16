using LaundrySystem.Entities.Enums;
using LaundrySystem.Entities.Models;
using Microsoft.Data.SqlClient;

namespace LaundrySystem.Repositories.Utilities
{
    public static class MachineMapper
    {
        public static Machine MapToMachine(SqlDataReader reader)
        {
            return new Machine
            {
                MachineId = Convert.ToInt32(reader["MachineId"]),
                LaundryId = Convert.ToInt32(reader["LaundryId"]),
                MachineNumber = Convert.ToInt32(reader["MachineNumber"]),
                MachineType = Enum.Parse<MachineType>(reader["MachineType"].ToString()!, true),
                Status = Enum.Parse<MachineStatus>(reader["Status"].ToString()!, true)
            };
        }

        public static void AddMachineParameters(SqlCommand command, Machine entity)
        {
            command.Parameters.AddWithValue("@LaundryId", entity.LaundryId);
            command.Parameters.AddWithValue("@MachineNumber", entity.MachineNumber);
            command.Parameters.AddWithValue("@MachineType", entity.MachineType.ToString());
            command.Parameters.AddWithValue("@Status", entity.Status.ToString());
        }
    }
}