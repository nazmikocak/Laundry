using LaundrySystem.Entities.Models;

namespace LaundrySystem.Repositories.Abstract
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User?> GetUserByEmailAsync(string email);

        Task UpdatePasswordAsync(int userId, string passwordHash);
    }
}