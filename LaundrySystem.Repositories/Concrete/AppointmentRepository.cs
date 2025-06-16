using LaundrySystem.Entities.DTOs;
using LaundrySystem.Entities.Enums;
using LaundrySystem.Entities.Models;
using LaundrySystem.Repositories.Contracts;
using LaundrySystem.Repositories.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace LaundrySystem.Repositories.Concrete
{
    public class AppointmentRepository : RepositoryBase<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(IConfiguration configuration) : base(configuration)
        {
        }



        public override async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            throw new NotImplementedException("sp_GetAllAppointments stored procedure is not defined.");
        }



        public async Task<IEnumerable<Appointment>> GetAppointmentsByStudentAsync(int studentId)
        {
            var appointments = new List<Appointment>();
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand("sp_GetAppointmentsByStudent", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@StudentId", studentId);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                appointments.Add(new Appointment
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
                });
            }

            return appointments;
        }



        public override async Task<Appointment?> GetByIdAsync(int id)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            await using var command = new SqlCommand("sp_GetAppointmentById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@AppointmentId", id);

            await using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return AppointmentMapper.MapToAppointment(reader);
            }

            return null;
        }






        public override async Task CreateAsync(Appointment entity)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            await using var command = new SqlCommand("sp_CreateAppointment", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@StudentId", entity.StudentId);
            command.Parameters.AddWithValue("@MachineId", entity.MachineId);
            command.Parameters.AddWithValue("@StartTime", entity.StartTime);
            command.Parameters.AddWithValue("@EndTime", entity.EndTime);

            await command.ExecuteScalarAsync();
        }



        public override async Task UpdateAsync(Appointment entity)
        {
            throw new NotImplementedException("A full appointment update SP is not defined. Use UpdateStatusAsync instead.");
        }



        public async Task UpdateStatusAsync(int appointmentId, AppointmentStatus status)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            await using var command = new SqlCommand("sp_UpdateAppointmentStatus", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@AppointmentId", appointmentId);
            command.Parameters.AddWithValue("@Status", status.ToString());

            await command.ExecuteNonQueryAsync();
        }



        public override async Task DeleteAsync(int id)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            await using var command = new SqlCommand("sp_DeleteAppointment", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@AppointmentId", id);
            await command.ExecuteNonQueryAsync();
        }


        public async Task<IEnumerable<AvailableSlotDto>> GetAvailableSlotsAsync(int laundryId, DateTime date, MachineType machineType)
        {
            var slots = new List<AvailableSlotDto>();

            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            await using var command = new SqlCommand("sp_GetAvailableSlots", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@LaundryId", laundryId);
            command.Parameters.AddWithValue("@Date", date.Date);
            command.Parameters.AddWithValue("@MachineType", machineType.ToString());

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                slots.Add(new AvailableSlotDto
                {
                    MachineId = Convert.ToInt32(reader["MachineId"]),
                    MachineNumber = Convert.ToInt32(reader["MachineNumber"]),
                    SlotStart = Convert.ToDateTime(reader["SlotStart"]),
                    SlotEnd = Convert.ToDateTime(reader["SlotEnd"]),
                    SlotStatus = reader["SlotStatus"].ToString()!,
                    MachineStatus = reader["MachineStatus"].ToString()!
                });
            }
            return slots;
        }
    }
}