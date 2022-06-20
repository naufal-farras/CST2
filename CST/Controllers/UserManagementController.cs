using CST.Constants;
using CST.Models.Master;
using CST.Repository;
using CST.ViewModels.Account;
using CST.ViewModels.HelperVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CST.Controllers.Master
{
    public class UserManagementController : Controller
    {

        private readonly M_User_Repository _userRepository;
        private readonly M_Jabatan_Repository _jabatanRepository;
        private readonly M_Unit_Repository _unitRepository;
        private readonly UserManager<M_User> _userManager;

        public UserManagementController(M_User_Repository userRepository,
            M_Jabatan_Repository jabatanRepository, M_Unit_Repository unitRepository,
            UserManager<M_User> userManager)
        {
            _userRepository = userRepository;
            _jabatanRepository = jabatanRepository;
            _unitRepository = unitRepository;
            _userManager = userManager;
        }

        #region CREATE, EDIT, DELETE
        [HttpPost("Master/User")]
        public async Task<JsonResult> AddUser(RegisterVM registerVM)
        {
            var result = await _userRepository.CreateUser(registerVM);

            return Json(result);
        }
        [HttpPut("Master/User")]
        public async Task<JsonResult> EditUser(RegisterVM registerVM)
        {
            var result = await _userRepository.EditUser(registerVM);

            return Json(result);
        }
        [HttpDelete("Master/User")]
        public async Task<JsonResult> DeleteUser(string id)
        {
            var result = await _userRepository.DeleteUser(id);

            return Json(result);
        }
        #endregion

        #region PROCESS       
        [HttpPost("Master/User/ResetPassword")]
        public async Task<JsonResult> ResetPassword(ManageAccountVM data)
        {
            StatusHelperVM result = new StatusHelperVM();

            var user = await _userManager.FindByNameAsync(data.NPP);
            if (user != null)
            {
                //string hashedPassword = _userManager.PasswordHasher.HashPassword(user.Result,plainPassword);//ini untuk hash password                
                //var resetPassword = await _userManager.ChangePasswordAsync(user.Result, currentPassword, plainPassword); //ini untuk change password

                string generateToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resetPassword = await _userManager.ResetPasswordAsync(user, generateToken, ConstantsAccount.DefaultPassword);

                if (resetPassword.Succeeded)
                {
                    result.StatusCategory = StatusCategory.Success;
                    result.Message = "Password berhasil direset";
                }
                else
                {
                    result.StatusCategory = StatusCategory.Failed;
                    result.Message = "Failed. Message : " + resetPassword.Errors;
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

        #region VIEW
        [HttpGet("Master/User")]
        public IActionResult Index()
        {
            var jabatan = _jabatanRepository.GetAll();
            ViewBag.Jabatan = jabatan;
            var unit = _unitRepository.GetAll();
            ViewBag.Unit = unit;
            return View();
        }
        #endregion

        #region GET DATA
        [HttpGet("Master/User/Data")]
        public JsonResult GetAllUser()
        {
            var result = _userRepository.GetAll();
            return Json(new { data = result });
        }
        #endregion
    }
}
