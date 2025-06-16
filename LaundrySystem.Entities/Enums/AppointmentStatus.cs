using System.ComponentModel.DataAnnotations;

namespace LaundrySystem.Entities.Enums
{
    public enum AppointmentStatus
    {
        [Display(Name = "Planlandı")]
        Scheduled,

        [Display(Name = "Tamamlandı")]
        Completed,

        [Display(Name = "İptal Edildi")]
        Cancelled
    }
}