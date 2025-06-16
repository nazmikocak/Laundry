using LaundrySystem.Entities.Models;

namespace LaundrySystem.Web.Models
{
    public class AdminDashboardViewModel
    {
        public DateTime SelectedDate { get; set; } = DateTime.Today;
        public IEnumerable<Appointment> TodaysAppointments { get; set; } = new List<Appointment>();
        public IEnumerable<LaundryManagementViewModel> LaundryManagements { get; set; } = new List<LaundryManagementViewModel>();
    }
}
