using LaundrySystem.Entities.Enums;
using LaundrySystem.Entities.Models;
using LaundrySystem.Services.Abstract;
using LaundrySystem.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaundrySystem.Web.Controllers
{
    public class AppointmentController : BaseController
    {
        private readonly IServiceManager _serviceManager;



        public AppointmentController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allAppointments = await _serviceManager.AppointmentService.GetAppointmentsByStudentAsync(CurrentUserId);

            var viewModel = new HomeViewModel
            {
                ActiveAppointments = allAppointments.Where(a => a.Status == AppointmentStatus.Scheduled)
                                                   .OrderBy(a => a.StartTime),

                PastAppointments = allAppointments.Where(a => a.Status != AppointmentStatus.Scheduled)
                                                  .OrderByDescending(a => a.StartTime)
            };

            return View(viewModel);
        }



        [HttpGet]
        public async Task<IActionResult> Search(int? laundryId, MachineType? type)
        {
            var user = await _serviceManager.UserService.GetUserByIdAsync(CurrentUserId);

            var laundriesInDorm = user.DormitoryId.HasValue
                ? await _serviceManager.LaundryService.GetLaundriesByDormitoryAsync(user.DormitoryId.Value)
                : new List<Laundry>();

            var viewModel = new AppointmentSearchViewModel
            {
                Laundries = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(laundriesInDorm, "LaundryId", "LocationDescription"),
                LaundryId = laundryId,
                MachineType = type ?? MachineType.Washing
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(AppointmentSearchViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            return RedirectToAction("LaundryList", new
            {
                date = viewModel.AppointmentDate,
                machineType = viewModel.MachineType,
                laundryId = viewModel.LaundryId
            });
        }



        [HttpGet]
        public async Task<IActionResult> LaundryList(DateTime date, MachineType machineType, int? laundryId)
        {
            var user = await _serviceManager.UserService.GetUserByIdAsync(CurrentUserId);

            var laundriesToSearch = laundryId.HasValue
                ? new List<Laundry> { await _serviceManager.LaundryService.GetLaundryByIdAsync(laundryId.Value) }
                : (await _serviceManager.LaundryService.GetLaundriesByDormitoryAsync(user.DormitoryId.Value))
                    .Where(l => l.Status == LaundryStatus.Active);

            var viewModel = new LaundryListViewModel
            {
                AppointmentDate = date,
                MachineType = machineType.ToString()
            };

            var availabilityList = new List<LaundryAvailability>();
            foreach (var laundry in laundriesToSearch)
            {
                var slots = await _serviceManager.AppointmentService.GetAvailableSlotsAsync(laundry.LaundryId, date, machineType);

                var now = DateTime.Now;
                var availableSlots = slots.Where(s => s.SlotStatus == "Available")
                                          .Select(s => s.SlotStart)
                                          .Distinct()
                                          .OrderBy(s => s.TimeOfDay)
                                          .Where(s => date.Date > now.Date || s.TimeOfDay > now.TimeOfDay);

                availabilityList.Add(new LaundryAvailability
                {
                    Laundry = laundry,
                    AvailableSlots = availableSlots
                });
            }
            viewModel.LaundriesWithSlots = availabilityList;

            return View(viewModel);
        }



        [HttpGet]
        public async Task<IActionResult> SelectMachine(int laundryId, DateTime slotStart)
        {
            var laundry = await _serviceManager.LaundryService.GetLaundryByIdAsync(laundryId);
            var machineType = (MachineType)Enum.Parse(typeof(MachineType), HttpContext.Request.Query["machineType"]);

            var allSlotsForTime = (await _serviceManager.AppointmentService.GetAvailableSlotsAsync(laundryId, slotStart.Date, machineType))
                                    .Where(s => s.SlotStart == slotStart);

            ViewBag.LaundryName = laundry.LocationDescription;
            ViewBag.SlotTime = slotStart.ToString("HH:mm");

            return View(allSlotsForTime);
        }



        [HttpGet]
        public async Task<IActionResult> Confirm(int machineId, DateTime slotStart)
        {
            var machine = await _serviceManager.MachineService.GetMachineByIdAsync(machineId);
            var laundry = await _serviceManager.LaundryService.GetLaundryByIdAsync(machine.LaundryId);
            var dormitory = await _serviceManager.DormitoryService.GetDormitoryByIdAsync(laundry.DormitoryId);
            var user = await _serviceManager.UserService.GetUserByIdAsync(CurrentUserId);

            machine.Laundry = laundry;
            laundry.Dormitory = dormitory;
            user.Dormitory = dormitory;

            var appointmentForView = new Appointment
            {
                Machine = machine,
                Student = user,
                StartTime = slotStart,
                EndTime = slotStart.AddMinutes(laundry.SessionDurationMinutes),
                MachineId = machineId,
                StudentId = CurrentUserId
            };

            return View(appointmentForView);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int machineId, DateTime startTime)
        {
            try
            {
                await _serviceManager.AppointmentService.CreateAppointmentAsync(CurrentUserId, machineId, startTime);

                TempData["SuccessMessage"] = "Randevunuz başarıyla oluşturuldu!";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Randevu Oluşturulamadı";
                TempData["ErrorDetail"] = ex.Message;
                return RedirectToAction("Search");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            try
            {
                await _serviceManager.AppointmentService.CancelAppointmentAsync(id, CurrentUserId);
                TempData["SuccessMessage"] = "Randevunuz başarıyla iptal edildi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Randevu iptal edilemedi: {ex.Message}";
            }

            return RedirectToAction("Index", "Home");
        }
    }
}