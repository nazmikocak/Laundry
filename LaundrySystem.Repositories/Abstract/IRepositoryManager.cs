using LaundrySystem.Repositories.Contracts;

namespace LaundrySystem.Repositories.Abstract
{
    public interface IRepositoryManager
    {
        IAppointmentRepository Appointment { get; }
        IDormitoryRepository Dormitory { get; }
        ILaundryRepository Laundry { get; }
        IMachineRepository Machine { get; }
        IRoleRepository Role { get; }
        IUserRepository User { get; }
    }
}