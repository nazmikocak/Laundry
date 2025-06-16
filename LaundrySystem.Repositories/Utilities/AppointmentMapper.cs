using LaundrySystem.Entities.Enums;
using LaundrySystem.Entities.Models;
using Microsoft.Data.SqlClient;

namespace LaundrySystem.Repositories.Utilities
{
    public static class AppointmentMapper
    {
        public static Appointment MapToAppointment(SqlDataReader reader)
        {
            return new Appointment
            {
                AppointmentId = Convert.ToInt32(reader["AppointmentId"]),
                StudentId = Convert.ToInt32(reader["StudentId"]),
                MachineId = Convert.ToInt32(reader["MachineId"]),
                StartTime = Convert.ToDateTime(reader["StartTime"]),
                EndTime = Convert.ToDateTime(reader["EndTime"]),
                Status = Enum.Parse<AppointmentStatus>(reader["Status"].ToString()!, true)
            };
        }

        public static Appointment MapToAppointmentWithDetails(SqlDataReader reader)
        {
            return new Appointment
            {
                AppointmentId = Convert.ToInt32(reader["AppointmentId"]),
                StartTime = Convert.ToDateTime(reader["StartTime"]),
                EndTime = Convert.ToDateTime(reader["EndTime"]),
                Status = Enum.Parse<AppointmentStatus>(reader["Status"].ToString()!, true),
                Machine = new Machine
                {
                    MachineNumber = Convert.ToInt32(reader["MachineNumber"]),
                    MachineType = Enum.Parse<MachineType>(reader["MachineType"].ToString()!, true),
                    Laundry = new Laundry
                    {
                        LocationDescription = reader["LocationDescription"].ToString()!,
                        Dormitory = new Dormitory
                        {
                            DormitoryName = reader["DormitoryName"].ToString()!
                        }
                    }
                }
            };
        }
    }
}