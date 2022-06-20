using CST.ViewModels.Account;
using CST.Models.Master;
using CST.Repository;
using CST.ViewModels;
using CST.ViewModels.HelperVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APD.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<M_User> _signInManager;
        private readonly UserManager<M_User> _userManager;
        private readonly M_User_Repository _userRepository;

        public AccountController(SignInManager<M_User> signInManager, UserManager<M_User> userManager,
            M_User_Repository userRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userRepository = userRepository;

        }

        #region VIEW
        [HttpGet, AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet("Account/ChangePassword"), AllowAnonymous]
        public IActionResult ChangePassword()
        {
            return View("~/Views/UserManagement/ChangePassword.cshtml");
        }
        [HttpGet("Account/Forbiden"), AllowAnonymous]
        public IActionResult ForbidenAccess()
        {
            return View("~/Views/Account/403Page.cshtml");
        }
        #endregion

        #region PROCESS
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            StatusHelperVM resultLogin = new StatusHelperVM();

            if (_userRepository.CheckIsDeletedUser(loginVM.NPP))
            {
                resultLogin.StatusCategory = StatusCategory.Failed;
                resultLogin.Message = "Akun ada sudah terhapus. Silahkan menghubungi Admin";
            }
            else
            {
                try
                {
                    var result = await _signInManager.PasswordSignInAsync(loginVM.NPP, loginVM.Password, loginVM.RememberMe, false);
                    if (result.Succeeded)
                    {
                        _userRepository.UpdateLastLogin(loginVM.NPP);
                        resultLogin.StatusCategory = StatusCategory.Success;
                        resultLogin.Message = "Login berhasil. Tunggu beberapa saat...";
                    }
                    else
                    {
                        resultLogin.StatusCategory = StatusCategory.Failed;
                        resultLogin.Message = "Login gagal. Pastikan NPP dan Password sudah sesuai!";
                    }
                }
                catch (Exception ex)
                {
                    resultLogin.StatusCategory = StatusCategory.Error;
                    resultLogin.Message = "Error. Message : " + ex.Message;
                }

            }

            return Json(resultLogin);
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        
        [HttpPost]
        public async Task<JsonResult> ChangePassword(ManageAccountVM data)
        {
            StatusHelperVM result = new StatusHelperVM();


            var user = await _userManager.FindByNameAsync(data.NPP);
            if (user != null)
            {
                var changePassword = await _userManager.ChangePasswordAsync(user, data.OldPassword, data.NewPassword);

                if (changePassword.Succeeded)
                {
                    result.StatusCategory = StatusCategory.Success;
                    result.Message = "Password berhasil diubah";
                }
                else
                {
                    result.StatusCategory = StatusCategory.Failed;
                    result.Message = "Failed. Message : ";
                    foreach (var error in changePassword.Errors)
                    {
                        result.Message += error.Description + " , ";
                    }
                }
            }
            else
            {
                result.StatusCategory = StatusCategory.NotFound;
                result.Message = "User tidak ditemukan!";
            }

            return Json(result);
        }
        #endregion
    }
}
