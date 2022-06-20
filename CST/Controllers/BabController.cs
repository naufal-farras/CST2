using CST.Models.Master;
using CST.Repository;
using CST.ViewModels.HelperVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CST.Controllers
{
    public class BabController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<M_User> _userManager;
        private readonly M_User_Repository _userRepository;
        private readonly M_Bab_Repository _babRepository;

        public BabController(IWebHostEnvironment webHostEnvironment, UserManager<M_User> userManager,
            M_User_Repository userRepository, M_Bab_Repository babRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _userRepository = userRepository;
            _babRepository = babRepository;
        }

        #region VIEW
        [Authorize(Roles = "Inputer,Admin")]
        [HttpGet("Bab/Input")]
        public IActionResult Input()
        {
            string currentUserNPP = _userManager.GetUserName(User) ?? null;
            var currentUser = _userRepository.GetByNPP(currentUserNPP);
            ViewBag.CurrentUser = currentUser;
            var result = _babRepository.GetAllNasabah();
            
            return View("Input",result);
        }
        #endregion

        #region PROSES
        [HttpPost("Bab/Input")]
        public JsonResult Input(M_Bab data)
        {
            StatusHelperVM result = new StatusHelperVM();
            if (data.Nama != null)
            {
                #region INSERT TO DATABASE
                M_Bab bab = new M_Bab();
                bab.Nama = data.Nama;
                bab.NasabahId = data.NasabahId;

                var insertData = _babRepository.Create(bab);
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

        [HttpPut("Bab/Update")]
        public JsonResult Update(M_Bab data)
        {
            StatusHelperVM result = new StatusHelperVM();
            bool isPassedCheck = true;

            if (isPassedCheck)
            {
                #region UPDATE TO DATABASE
                M_Bab bab = new M_Bab();
                bab.Id = data.Id;
                bab.Nama = data.Nama;
                bab.NasabahId = data.NasabahId;

                var updateData = _babRepository.Update(bab);
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

        [HttpDelete("Bab/Delete")]
        public JsonResult Delete(int dataId)
        {
            StatusHelperVM result = new StatusHelperVM();

            var getData = _babRepository.GetById(dataId);
            if (getData != null)
            {
                var resultDelete = _babRepository.Delete(dataId);
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
        [HttpGet("Bab/Data/Input")]
        public JsonResult GetDataAllSubMenu()
        {
            var result = _babRepository.GetAll();
            return Json(new { data = result });
        }
        #endregion

    }
}