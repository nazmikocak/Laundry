using LaundrySystem.Entities.DTOs;
using LaundrySystem.Entities.Models;

namespace LaundrySystem.Services.Abstract
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<User> GetUserByIdAsync(int userId);

        Task UpdateUserAsync(int userId, User userForUpdate);

        Task ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto);
    }
}