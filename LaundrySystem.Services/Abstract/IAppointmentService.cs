using LaundrySystem.Entities.DTOs;
using LaundrySystem.Entities.Enums;
using LaundrySystem.Entities.Models;

namespace LaundrySystem.Services.Abstract
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AvailableSlotDto>> GetAvailableSlotsAsync(int laundryId, DateTime date, MachineType machineType);

        Task CreateAppointmentAsync(int studentId, int machineId, DateTime startTime);

        Task CancelAppointmentAsync(int appointmentId, int studentId);

        Task<Appointment> GetAppointmentByIdAsync(int appointmentId);

        Task<IEnumerable<Appointment>> GetAppointmentsByStudentAsync(int studentId);

    }
}