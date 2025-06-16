using LaundrySystem.Entities.Models;

namespace LaundrySystem.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Appointment> ActiveAppointments { get; set; } = new List<Appointment>();

        public IEnumerable<Appointment> PastAppointments { get; set; } = new List<Appointment>();

        public IEnumerable<Laundry> ActiveLaundries { get; set; } = new List<Laundry>();
    }
}