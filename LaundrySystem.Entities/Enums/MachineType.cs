using System.ComponentModel.DataAnnotations;

namespace LaundrySystem.Entities.Enums
{
    public enum MachineType
    {
        [Display(Name = "Yıkama")]
        Washing,

        [Display(Name = "Kurutma")]
        Drying
    }
}