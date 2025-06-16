using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundrySystem.Entities.Models
{
    public class AppointmentStatusLog
    {
        public int LogId { get; set; }
        public int AppointmentId { get; set; }
        public string? OldStatus { get; set; }
        public string? NewStatus { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}
