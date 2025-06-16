using LaundrySystem.Entities.DTOs;
using LaundrySystem.Entities.Models;

namespace LaundrySystem.Services.Abstract
{
    public interface IAuthService
    {
        Task SignUpAsync(UserForSignUpDto userForSignUpDto);

        Task<User> SignInAsync(UserForSignInDto userForSignInDto);
    }
}