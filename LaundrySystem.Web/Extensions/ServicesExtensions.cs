using LaundrySystem.Repositories.Abstract;
using LaundrySystem.Repositories.Concrete;
using LaundrySystem.Repositories.Contracts;
using LaundrySystem.Services.Abstract;
using LaundrySystem.Services.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace LaundrySystem.Web.Extensions
{
    public static class ServicesExtensions
    {




        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();



        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();



        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IDormitoryRepository, DormitoryRepository>();
            services.AddScoped<ILaundryRepository, LaundryRepository>();
            services.AddScoped<IMachineRepository, MachineRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }



        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IDormitoryService, DormitoryService>();
            services.AddScoped<ILaundryService, LaundryService>();
            services.AddScoped<IMachineService, MachineService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
        }



        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "LaundryApp.AuthCookie";
                    options.LoginPath = "/Auth/SignIn";
                    options.LogoutPath = "/Auth/SignOut";
                    options.AccessDeniedPath = "/Home/AccessDenied";
                    options.ExpireTimeSpan = TimeSpan.FromDays(7);
                    options.SlidingExpiration = true;
                });
        }
    }
}