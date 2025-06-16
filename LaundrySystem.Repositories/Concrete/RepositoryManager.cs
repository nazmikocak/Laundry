using LaundrySystem.Repositories.Abstract;
using LaundrySystem.Repositories.Contracts;

namespace LaundrySystem.Repositories.Concrete
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDormitoryRepository _dormitoryRepository;
        private readonly ILaundryRepository _laundryRepository;
        private readonly IMachineRepository _machineRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;


        public RepositoryManager(
            IAppointmentRepository appointmentRepository,
            IDormitoryRepository dormitoryRepository,
            ILaundryRepository laundryRepository,
            IMachineRepository machineRepository,
            IRoleRepository roleRepository,
            IUserRepository userRepository)
        {
            _appointmentRepository = appointmentRepository;
            _dormitoryRepository = dormitoryRepository;
            _laundryRepository = laundryRepository;
            _machineRepository = machineRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }


        public IAppointmentRepository Appointment => _appointmentRepository;

        public IDormitoryRepository Dormitory => _dormitoryRepository;

        public ILaundryRepository Laundry => _laundryRepository;

        public IMachineRepository Machine => _machineRepository;

        public IRoleRepository Role => _roleRepository;

        public IUserRepository User => _userRepository;
    }
}