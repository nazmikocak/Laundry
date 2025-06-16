using LaundrySystem.Entities.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LaundrySystem.Web.Models
{
    public class AccountInfoViewModel
    {
        public User User { get; set; } = new User();

        public SelectList? Dormitories { get; set; }
    }
}