namespace LaundrySystem.Services.Abstract
{
    public interface IServiceManager
    {
        IAppointmentService AppointmentService { get; }
        IAuthService AuthService { get; }
        IDormitoryService DormitoryService { get; }
        ILaundryService LaundryService { get; }
        IMachineService MachineService { get; }
        IRoleService RoleService { get; }
        IUserService UserService { get; }
    }
}