using System.ComponentModel.DataAnnotations;

namespace LaundrySystem.Entities.DTOs
{
    public class ChangePasswordDto
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Mevcut parola alanı zorunludur.")]
        public string CurrentPassword { get; set; } = string.Empty;


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Yeni parola alanı zorunludur.")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Parolanız en az 8 en fazla 16 karakter uzunluğunda olmalıdır.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)[A-Za-z\d]*$", ErrorMessage = "Parolanız en az bir büyük harf ve bir sayı içermelidir.")]
        public string NewPassword { get; set; } = string.Empty;


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Yeni parola tekrar alanı zorunludur.")]
        [Compare("NewPassword", ErrorMessage = "Yeni parolalar eşleşmiyor.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}