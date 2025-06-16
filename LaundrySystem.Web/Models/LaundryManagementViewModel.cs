using LaundrySystem.Entities.Models;

namespace LaundrySystem.Web.Models
{
    public class LaundryManagementViewModel
    {
        public Laundry Laundry { get; set; } = new();
        public IEnumerable<Machine> Machines { get; set; } = new List<Machine>();
    }
}