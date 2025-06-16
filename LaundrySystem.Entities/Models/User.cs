namespace LaundrySystem.Entities.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } 
        public string NationalNumber { get; set; } = string.Empty;
        public int? DormitoryId { get; set; }
        public int RoleId { get; set; }


        public Role? Role { get; set; }
        public Dormitory? Dormitory { get; set; }
    }
}