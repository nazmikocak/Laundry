using LaundrySystem.Entities.Enums;

namespace LaundrySystem.Entities.Models
{
    public class Machine
    {
        public int MachineId { get; set; }
        public int LaundryId { get; set; } 
        public int MachineNumber { get; set; }
        public MachineType MachineType { get; set; }
        public MachineStatus Status { get; set; }

        public Laundry? Laundry { get; set; }
    }
}