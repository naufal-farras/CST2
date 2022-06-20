using CST.Models.Master;
using CST.Repository;
using CST.ViewModels.HelperVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CST.Controllers
{
    public class SubSubMenuController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<M_User> _userManager;
        private readonly M_User_Repository _userRepository;
        private readonly M_SubSubMenu_Repository _subSubMenuRepository;

        public SubSubMenuController(IWebHostEnvironment webHostEnvironment, UserManager<M_User> userManager,
            M_User_Repository userRepository, M_SubSubMenu_Repository subSubMenuRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _userRepository = userRepository;
            _subSubMenuRepository = subSubMenuRepository;
        }

        #region VIEW
        [Authorize(Roles = "Inputer,Admin")]
        [HttpGet("SubSubMenu/Tambah")]
        public IActionResult Input()
        {
            string currentUserNPP = _userManager.GetUserName(User) ?? null;
            var currentUser = _userRepository.GetByNPP(currentUserNPP);
            ViewBag.CurrentUser = currentUser;
            var result = _subSubMenuRepository.GetAllNasabah();
            return View("Input", result);
        }
        #endregion

        #region PROSES
        [HttpPost("SubSubMenu/Input")]
        public JsonResult Input(M_SubSubMenu data)
        {
            StatusHelperVM result = new StatusHelperVM();
            if (data.Nama != null)
            {
                #region INSERT TO DATABASE
                M_SubSubMenu subSubMenu = new M_SubSubMenu();
                subSubMenu.Nama = data.Nama;
                subSubMenu.NasabahId = data.NasabahId;
                var insertData = _subSubMenuRepository.Create(subSubMenu);
                if (insertData.StatusCategory == StatusCategory.Success)
                {
                    result.StatusCategory = insertData.StatusCategory;
                    result.Message = insertData.Message;
                }
                else
                {
                    result.StatusCategory = insertData.StatusCategory;
                    result.Message = insertData.Message;
                }
                #endregion

            }
            else
            {
                result.StatusCategory = StatusCategory.Failed;
                result.Message = "Tidak ada Nama !!!";
            }

            return Json(result);
        }

        [HttpPut("SubSubMenu/Update")]
        public JsonResult Update(M_SubSubMenu data)
        {
            StatusHelperVM result = new StatusHelperVM();
            bool isPassedCheck = true;

            if (isPassedCheck)
            {
                #region UPDATE TO DATABASE
                M_SubSubMenu subSubMenu = new M_SubSubMenu();
                subSubMenu.Id = data.Id;
                subSubMenu.Nama = data.Nama;
                subSubMenu.NasabahId = data.NasabahId;
                var updateData = _subSubMenuRepository.Update(subSubMenu);
                if (updateData.StatusCategory == StatusCategory.Success)
                {
                    result.StatusCategory = updateData.StatusCategory;
                    result.Message = updateData.Message;
                }
                else
                {
                    result.StatusCategory = updateData.StatusCategory;
                    result.Message = updateData.Message;
                }
                #endregion

                return Json(result);
            }
            else
            {
                return Json(result);
            }
        }

        [HttpDelete("SubSubMenu/Delete")]
        public JsonResult Delete(int dataId)
        {
            StatusHelperVM result = new StatusHelperVM();

            var getData = _subSubMenuRepository.GetById(dataId);
            if (getData != null)
            {
                var resultDelete = _subSubMenuRepository.Delete(dataId);
                if (resultDelete.StatusCategory == StatusCategory.Success)
                {
                    result.StatusCategory = StatusCategory.Success;
                    result.Message = "Data berhasil terhapus";
                }
            }
            else
            {
                result.StatusCategory = StatusCategory.NotFound;
                result.Message = "Data tidak ditemukan";
            }

            return Json(result);
        }
        #endregion

        #region GET DATA        
        [HttpGet("SubSubMenu/Data/Input")]
        public JsonResult GetDataAllSubMenu()
        {
            var result = _subSubMenuRepository.GetAll();
            return Json(new { data = result });
        }
        #endregion
    }
}
