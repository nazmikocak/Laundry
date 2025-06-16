namespace LaundrySystem.Entities.Models
{
    public class Dormitory
    {
        public int DormitoryId { get; set; }
        public string DormitoryName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string? Address { get; set; }


        public IEnumerable<Laundry> Laundries { get; set; } = new List<Laundry>();
    }
}