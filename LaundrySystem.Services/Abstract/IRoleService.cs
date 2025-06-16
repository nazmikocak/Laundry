using LaundrySystem.Entities.Models;

namespace LaundrySystem.Services.Abstract
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllRolesAsync();
    }
}