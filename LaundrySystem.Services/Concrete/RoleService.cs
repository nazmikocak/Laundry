using LaundrySystem.Entities.Models;
using LaundrySystem.Repositories.Abstract;
using LaundrySystem.Services.Abstract;

namespace LaundrySystem.Services.Concrete
{
    public class RoleService : IRoleService
    {
        private readonly IRepositoryManager _repositoryManager;


        public RoleService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }


        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _repositoryManager.Role.GetAllAsync();
        }
    }
}