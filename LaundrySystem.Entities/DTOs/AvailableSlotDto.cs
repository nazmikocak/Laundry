namespace LaundrySystem.Entities.DTOs
{
    public class AvailableSlotDto
    {
        public int MachineId { get; set; }
        public int MachineNumber { get; set; }
        public DateTime SlotStart { get; set; }
        public DateTime SlotEnd { get; set; }
        public string SlotStatus { get; set; } = string.Empty;
        public string MachineStatus { get; set; } = string.Empty;
    }
}