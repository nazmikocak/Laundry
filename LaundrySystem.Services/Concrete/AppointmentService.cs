using LaundrySystem.Entities.DTOs;
using LaundrySystem.Entities.Enums;
using LaundrySystem.Entities.Models;
using LaundrySystem.Repositories.Abstract;
using LaundrySystem.Services.Abstract;
using LaundrySystem.Services.Exceptions;
using Microsoft.Data.SqlClient;

namespace LaundrySystem.Services.Concrete
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IRepositoryManager _repositoryManager;


        public AppointmentService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }


        public async Task<IEnumerable<AvailableSlotDto>> GetAvailableSlotsAsync(int laundryId, DateTime date, MachineType machineType)
        {
            var laundry = await _repositoryManager.Laundry.GetByIdAsync(laundryId)
                ?? throw new NotFoundException($"'{laundryId}' ID'li çamaşırhane bulunamadı.");

            var availableSlots = await _repositoryManager.Appointment.GetAvailableSlotsAsync(laundry.LaundryId, date, machineType)
                ?? throw new NotFoundException($"Uygun yer bulunamadı.");

            return availableSlots;
        }


        public async Task CreateAppointmentAsync(int userId, int machineId, DateTime startTime)
        {
            var machine = await _repositoryManager.Machine.GetByIdAsync(machineId)
                ?? throw new Exception($"'{machineId}' kimliğine sahip makine bulunamadı.");

            var laundry = await _repositoryManager.Laundry.GetByIdAsync(machine.LaundryId)
                ?? throw new Exception($"'{machine.LaundryId}' ID'li çamaşırhane bulunamadı.");

            var newAppointment = new Appointment
            {
                StudentId = userId,
                MachineId = machineId,
                StartTime = startTime,
                EndTime = startTime.AddMinutes(laundry.SessionDurationMinutes),
                Status = AppointmentStatus.Scheduled
            };

            try
            {
                await _repositoryManager.Appointment.CreateAsync(newAppointment);
            }
            catch (SqlException ex) when (ex.Message.Contains("already booked"))
            {
                throw new Exception("Bugün için bu tipte bir randevu zaten oluşturdunuz.");
            }
        }


        public async Task CancelAppointmentAsync(int appointmentId, int userId)
        {
            var appointment = await _repositoryManager.Appointment.GetByIdAsync(appointmentId)
                ?? throw new Exception($"'{appointmentId}' ID'li randevu bulunamadı.");


            if (appointment.StudentId != userId)
            {
                throw new Exception("Bu randevuyu iptal etme yetkiniz yok.");
            }

            await _repositoryManager.Appointment.UpdateStatusAsync(appointmentId, AppointmentStatus.Cancelled);
        }


        public async Task<Appointment> GetAppointmentByIdAsync(int appointmentId)
        {
            var appointment = await _repositoryManager.Appointment.GetByIdAsync(appointmentId)
                ?? throw new NotFoundException($"'{appointmentId}' ID'li randevu bulunamadı.");

            return appointment;
        }


        public async Task<IEnumerable<Appointment>> GetAppointmentsByStudentAsync(int userId)
        {
            var user = await _repositoryManager.User.GetByIdAsync(userId)
                ?? throw new NotFoundException($"'{userId}' ID'li kullanıcı bulunamadı.");

            return await _repositoryManager.Appointment.GetAppointmentsByStudentAsync(userId);
        }
    }
}