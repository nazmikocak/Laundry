using LaundrySystem.Services.Abstract;

namespace LaundrySystem.Services.Concrete
{
    public class ServiceManager : IServiceManager
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IAuthService _authService;
        private readonly IDormitoryService _dormitoryService;
        private readonly ILaundryService _laundryService;
        private readonly IMachineService _machineService;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;


        public ServiceManager(
            IAppointmentService appointmentService,
            IAuthService authService,
            IDormitoryService dormitoryService,
            ILaundryService laundryService,
            IMachineService machineService,
            IRoleService roleService,
            IUserService userService)
        {
            _appointmentService = appointmentService;
            _authService = authService;
            _dormitoryService = dormitoryService;
            _laundryService = laundryService;
            _machineService = machineService;
            _roleService = roleService;
            _userService = userService;
        }


        public IAppointmentService AppointmentService => _appointmentService;

        public IAuthService AuthService => _authService;

        public IDormitoryService DormitoryService => _dormitoryService;

        public ILaundryService LaundryService => _laundryService;

        public IMachineService MachineService => _machineService;

        public IRoleService RoleService => _roleService;

        public IUserService UserService => _userService;
    }
}