using LaundrySystem.Entities.Enums;
using LaundrySystem.Entities.Models;
using LaundrySystem.Services.Abstract;
using LaundrySystem.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaundrySystem.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IServiceManager _serviceManager;


        public HomeController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = CurrentUserId;

            if (userId == 0)
            {
                return RedirectToAction("SignIn", "Auth");
            }

            var user = await _serviceManager.UserService.GetUserByIdAsync(userId);
            var allAppointments = await _serviceManager.AppointmentService.GetAppointmentsByStudentAsync(userId);

            var activeLaundries = user.DormitoryId.HasValue
                ? await _serviceManager.LaundryService.GetLaundriesByDormitoryAsync(user.DormitoryId.Value)
                : new List<Laundry>();

            var viewModel = new HomeViewModel
            {
                ActiveAppointments = allAppointments.Where(a => a.Status == AppointmentStatus.Scheduled),
                PastAppointments = allAppointments.Where(a => a.Status != AppointmentStatus.Scheduled),
                ActiveLaundries = activeLaundries.Where(l => l.Status == LaundryStatus.Active)
            };

            return View(viewModel);
        }


        public async Task<IActionResult> Help() 
        {
            return View();
        }
    }
}