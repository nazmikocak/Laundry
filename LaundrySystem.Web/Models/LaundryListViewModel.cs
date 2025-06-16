using LaundrySystem.Entities.Models;

namespace LaundrySystem.Web.Models
{
    public class LaundryAvailability
    {
        public Laundry Laundry { get; set; }
        public IEnumerable<DateTime> AvailableSlots { get; set; } = new List<DateTime>();
    }

    public class LaundryListViewModel
    {
        public IEnumerable<LaundryAvailability> LaundriesWithSlots { get; set; } = new List<LaundryAvailability>();
        public DateTime AppointmentDate { get; set; }
        public string MachineType { get; set; }
    }
}