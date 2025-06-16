using LaundrySystem.Entities.DTOs;
using LaundrySystem.Entities.Models;
using LaundrySystem.Repositories.Abstract;
using LaundrySystem.Services.Abstract;
using LaundrySystem.Services.Utilities;

namespace LaundrySystem.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager _repositoryManager;


        public UserService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
            

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _repositoryManager.User.GetAllAsync()
                ?? throw new Exception($"Kullanıcı bulunamadı.");
        }


        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await _repositoryManager.User.GetByIdAsync(userId)
                ?? throw new Exception($"'{userId}' ID'li kullanıcı bulunamadı.");

            return user;
        }


        public async Task UpdateUserAsync(int userId, User userForUpdate)
        {
            var user = await _repositoryManager.User.GetByIdAsync(userId)
                ?? throw new Exception($"'{userId}' ID'li kullanıcı bulunamadı.");

            var userWithSameEmail = await _repositoryManager.User.GetUserByEmailAsync(userForUpdate.Email);
            if (userWithSameEmail is not null && userWithSameEmail.UserId != userId)
            {
                throw new Exception("Bu e-posta adresi başka bir hesap tarafından kullanılıyor.");
            }

            user.FirstName = userForUpdate.FirstName;
            user.LastName = userForUpdate.LastName;
            user.Email = userForUpdate.Email;
            user.PhoneNumber = userForUpdate.PhoneNumber;
            user.DormitoryId = userForUpdate.DormitoryId;

            await _repositoryManager.User.UpdateAsync(user);
        }


        public async Task ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto)
        {
            var user = await _repositoryManager.User.GetByIdAsync(userId)
                ?? throw new Exception($"'{userId}' ID'li kullanıcı bulunamadı.");

            if (!PasswordHelper.VerifyPassword(changePasswordDto.CurrentPassword, user.PasswordHash))
            {
                throw new Exception("Mevcut parolanız yanlış.");
            }

            var newPasswordHash = PasswordHelper.HashPassword(changePasswordDto.NewPassword);

            await _repositoryManager.User.UpdatePasswordAsync(userId, newPasswordHash);
        }
    }
}