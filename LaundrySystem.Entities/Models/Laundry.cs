using LaundrySystem.Entities.Enums;

namespace LaundrySystem.Entities.Models
{
    public class Laundry
    {
        public int LaundryId { get; set; }
        public int DormitoryId { get; set; }
        public string LocationDescription { get; set; } = string.Empty;
        public int SessionDurationMinutes { get; set; }
        public LaundryStatus Status { get; set; }


        public Dormitory? Dormitory { get; set; }
        public ICollection<Machine> Machines { get; set; } = new List<Machine>();
    }
}
