using CST.Models.Master;
using CST.Repository;
using CST.ViewModels.HelperVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CST.Controllers
{
    public class SubMenuController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<M_User> _userManager;
        private readonly M_User_Repository _userRepository;
        private readonly M_SubMenu_Repository _subMenuRepository;

        public SubMenuController(IWebHostEnvironment webHostEnvironment, UserManager<M_User> userManager,
            M_User_Repository userRepository, M_SubMenu_Repository subMenuRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _userRepository = userRepository;
            _subMenuRepository = subMenuRepository;
        }

        #region VIEW
        [Authorize(Roles = "Inputer,Admin")]
        [HttpGet("SubMenu/Input")]
        public IActionResult Input()
        {
            string currentUserNPP = _userManager.GetUserName(User) ?? null;
            var currentUser = _userRepository.GetByNPP(currentUserNPP);
            ViewBag.CurrentUser = currentUser;
            var result = _subMenuRepository.GetAllNasabah();
            return View("Input", result);
        }
        #endregion

        #region PROSES
        [HttpPost("SubMenu/Input")]
        public JsonResult Input(M_SubMenu data)
        {
            StatusHelperVM result = new StatusHelperVM();
            if (data.Nama != null)
            {
                #region INSERT TO DATABASE
                M_SubMenu subMenu = new M_SubMenu();
                subMenu.Nama = data.Nama;
                subMenu.NasabahId= data.NasabahId;
                var insertData = _subMenuRepository.Create(subMenu);
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

        [HttpPut("SubMenu/Update")]
        public JsonResult Update(M_SubMenu data)
        {
            StatusHelperVM result = new StatusHelperVM();
            bool isPassedCheck = true;

            if (isPassedCheck)
            {
                #region UPDATE TO DATABASE
                M_SubMenu subMenu = new M_SubMenu();
                subMenu.Id = data.Id;
                subMenu.Nama = data.Nama;
                subMenu.NasabahId = data.NasabahId;
                var updateData = _subMenuRepository.Update(subMenu);
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

        [HttpDelete("SubMenu/Delete")]
        public JsonResult Delete(int dataId)
        {
            StatusHelperVM result = new StatusHelperVM();

            var getData = _subMenuRepository.GetById(dataId);
            if (getData != null)
            {
                var resultDelete = _subMenuRepository.Delete(dataId);
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
        [HttpGet("SubMenu/Data/Input")]
        public JsonResult GetDataAllSubMenu()
        {
            var result = _subMenuRepository.GetAll();
            return Json(new { data = result });
        }
        #endregion
    }
}
