using LaundrySystem.Entities.DTOs;
using LaundrySystem.Services.Abstract;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Data.SqlClient;

namespace LaundrySystem.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IServiceManager _serviceManager;


        public AuthController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }


        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(UserForSignInDto userForSignInDto)
        {
            if (!ModelState.IsValid)
            {
                return View(userForSignInDto);
            }
            try
            {
                var user = await _serviceManager.AuthService.SignInAsync(userForSignInDto);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.RoleName),
                    new Claim("FullName", $"{user.FirstName} {user.LastName}")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                TempData["SuccessMessage"] = $"Hoş geldin, {user.FirstName}!";

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Giriş Başarısız";
                ViewData["ErrorDetail"] = ex.Message;
                return View(userForSignInDto);
            }
        }


        [HttpGet]
        public async Task<IActionResult> SignUp()
        {
            ViewBag.Dormitories = await _serviceManager.DormitoryService.GetAllDormitoriesAsync();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(UserForSignUpDto userForSignUpDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Dormitories = await _serviceManager.DormitoryService.GetAllDormitoriesAsync();
                return View(userForSignUpDto);
            }

            try
            {
                await _serviceManager.AuthService.SignUpAsync(userForSignUpDto);

                TempData["SuccessMessage"] = "Kayıt başarılı! Şimdi giriş yapabilirsiniz.";
                return RedirectToAction("SignIn");
            }
            catch (Exception ex)
            {
                string errorMessage = "Kayıt sırasında bir hata oluştu. Lütfen bilgilerinizi kontrol edip tekrar deneyin.";
                if (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2627 || sqlEx.Number == 2601))
                {
                    if (sqlEx.Message.Contains("Email"))
                        errorMessage = "Bu e-posta adresi zaten kayıtlı.";
                    else if (sqlEx.Message.Contains("NationalNumber"))
                        errorMessage = "Bu T.C. Kimlik Numarası zaten kayıtlı.";
                }
                else
                {
                    errorMessage = ex.Message;
                }

                ViewData["ErrorMessage"] = "Kayıt Başarısız";
                ViewData["ErrorDetail"] = errorMessage;
                ViewBag.Dormitories = await _serviceManager.DormitoryService.GetAllDormitoriesAsync();
                return View(userForSignUpDto);
            }
        }


        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("SignIn");
        }
    }
}
