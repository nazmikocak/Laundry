namespace LaundrySystem.Entities.DTOs
{
    public class UserForSignUpDto : UserForSignInDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string NationalNumber { get; set; } = string.Empty;
        public int? DormitoryId { get; set; }
    }
}