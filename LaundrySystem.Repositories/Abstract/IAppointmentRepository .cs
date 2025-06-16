using LaundrySystem.Entities.DTOs;
using LaundrySystem.Entities.Enums;
using LaundrySystem.Entities.Models;
using LaundrySystem.Repositories.Abstract;

namespace LaundrySystem.Repositories.Contracts
{
    public interface IAppointmentRepository : IRepositoryBase<Appointment>
    {
        Task<IEnumerable<Appointment>> GetAppointmentsByStudentAsync(int studentId);

        Task UpdateStatusAsync(int appointmentId, AppointmentStatus status);

        Task<IEnumerable<AvailableSlotDto>> GetAvailableSlotsAsync(int laundryId, DateTime date, MachineType machineType);
    }
}