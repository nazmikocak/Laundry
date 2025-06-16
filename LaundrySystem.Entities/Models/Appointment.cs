using LaundrySystem.Entities.Enums;

namespace LaundrySystem.Entities.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int StudentId { get; set; }
        public int MachineId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public AppointmentStatus Status { get; set; }


        public User? Student { get; set; }
        public Machine? Machine { get; set; }
    }
}