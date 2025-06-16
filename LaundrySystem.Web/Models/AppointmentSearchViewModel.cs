using LaundrySystem.Entities.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LaundrySystem.Web.Models
{
    public class AppointmentSearchViewModel
    {
        public MachineType MachineType { get; set; }
        public int? LaundryId { get; set; } 
        public DateTime AppointmentDate { get; set; } = DateTime.Today;

        public SelectList? Laundries { get; set; }
    }
}