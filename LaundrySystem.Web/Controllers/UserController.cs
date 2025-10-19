using LaundrySystem.Entities.DTOs;
using LaundrySystem.Services.Abstract;
using LaundrySystem.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LaundrySystem.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly IServiceManager _serviceManager;


        public UserController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }


        [HttpGet]
        public async Task<IActionResult> Account()
        {
            var user = await _serviceManager.UserService.GetUserByIdAsync(CurrentUserId);
            var dormitories = await _serviceManager.DormitoryService.GetAllDormitoriesAsync();

            var viewModel = new AccountInfoViewModel
            {
                User = user,
                Dormitories = new SelectList(dormitories, "DormitoryId", "DormitoryName", user.DormitoryId)
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Account(AccountInfoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var dormitories = await _serviceManager.DormitoryService.GetAllDormitoriesAsync();
                viewModel.Dormitories = new SelectList(dormitories, "DormitoryId", "DormitoryName", viewModel.User.DormitoryId);
                return View("Info", viewModel);
            }
            try
            {
                await _serviceManager.UserService.UpdateUserAsync(CurrentUserId, viewModel.User);
                TempData["SuccessMessage"] = "Hesap bilgileriniz başarıyla güncellendi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("Account");
        }


        [HttpGet]
        public async Task<IActionResult> Password()
        {
            return View();
        }
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Password(ChangePasswordDto changePasswordDto)
        {
            if (!ModelState.IsValid)
            {
                var firstError = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault();
                ViewData["ErrorMessage"] = "Giriş Hatası";
                ViewData["ErrorDetail"] = firstError?.ErrorMessage ?? "Lütfen girdiğiniz bilgileri kontrol edin.";

                return View(changePasswordDto);
            }
            try
            {
                await _serviceManager.UserService.ChangePasswordAsync(CurrentUserId, changePasswordDto);
                TempData["SuccessMessage"] = "Parolanız başarıyla değiştirildi.";

                return RedirectToAction("Password");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Parola Değiştirilemedi";
                ViewData["ErrorDetail"] = ex.Message;

                return View(changePasswordDto);
            }
        }
    }
}
