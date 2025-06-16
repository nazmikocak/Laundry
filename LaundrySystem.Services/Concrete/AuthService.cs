using LaundrySystem.Entities.DTOs;
using LaundrySystem.Entities.Models;
using LaundrySystem.Repositories.Abstract;
using LaundrySystem.Services.Abstract;
using LaundrySystem.Services.Exceptions;
using LaundrySystem.Services.Utilities;

namespace LaundrySystem.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IRepositoryManager _repositoryManager;


        public AuthService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }


        public async Task SignUpAsync(UserForSignUpDto dto)
        {
            var existingUser = await _repositoryManager.User.GetUserByEmailAsync(dto.Email);

            if (existingUser is not null)
            {
                throw new BadRequestException("Email is already registered.");
            }

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                NationalNumber = dto.NationalNumber,
                DormitoryId = dto.DormitoryId,
                RoleId = 4,
                PasswordHash = PasswordHelper.HashPassword(dto.Password)
            };

            await _repositoryManager.User.CreateAsync(user);
        }


        public async Task<User> SignInAsync(UserForSignInDto dto)
        {
            var user = await _repositoryManager.User.GetUserByEmailAsync(dto.Email)
                ?? throw new BadRequestException("Invalid email or password.");

            if (!PasswordHelper.VerifyPassword(dto.Password, user.PasswordHash)) 
            {
                throw new Exception("Invalid email or password.");
            }

            return user;
        }
    }
}