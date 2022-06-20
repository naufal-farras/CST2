using CST.Models.Master;
using CST.Repository;
using CST.ViewModels.HelperVM;
using CST.ViewModels.Nasabah;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CST.Controllers
{
    public class NasabahController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<M_User> _userManager;
        private readonly M_User_Repository _userRepository;
        private readonly M_Nasabah_Repository _nasabahRepository;

        public NasabahController(IWebHostEnvironment webHostEnvironment, UserManager<M_User> userManager,
            M_User_Repository userRepository, M_Nasabah_Repository nasabahRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _userRepository = userRepository;
            _nasabahRepository = nasabahRepository;
        }

        #region VIEW
        [Authorize(Roles = "Inputer,Admin")]
        [HttpGet("Nasabah/Input")]
        public IActionResult Input()
        {
            string currentUserNPP = _userManager.GetUserName(User) ?? null;
            var currentUser = _userRepository.GetByNPP(currentUserNPP);
            ViewBag.CurrentUser = currentUser;
            return View("~/Views/Nasabah/Input.cshtml");
        }
        #endregion

        #region PROSES
        [HttpPost("Nasabah/Input")]
        public JsonResult Input(InputNasabahVM data)
        {
            StatusHelperVM result = new StatusHelperVM();
            if (data.KodeNasabah != null)
            {
                #region INSERT TO DATABASE
                M_Nasabah nasabah = new M_Nasabah();
                nasabah.KodeNasabah = data.KodeNasabah;
                nasabah.Nama = data.NamaNasabah;

                var insertData = _nasabahRepository.Create(nasabah);
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
                result.Message = "Tidak ada Kode !!!";
            }

            return Json(result);
        }

        [HttpPut("Nasabah/Update")]
        public JsonResult Update(InputNasabahVM data)
        {
            StatusHelperVM result = new StatusHelperVM();

            bool isPassedCheck = true;

            if (isPassedCheck)
            {
                #region UPDATE TO DATABASE
                M_Nasabah nasabah = new M_Nasabah();
                nasabah.Id = data.Id;
                nasabah.KodeNasabah = data.KodeNasabah;
                nasabah.Nama = data.NamaNasabah;
                var updateData = _nasabahRepository.Update(nasabah);
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

        [HttpDelete("Nasabah/Delete")]
        public JsonResult Delete(int dataId)
        {
            StatusHelperVM result = new StatusHelperVM();

            var getData = _nasabahRepository.GetById(dataId);
            if (getData != null)
            {
                var resultDelete = _nasabahRepository.Delete(dataId);
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
        [HttpGet("Nasabah/Data/Input")]
        public JsonResult GetDataAllNasabah()
        {
            var result = _nasabahRepository.GetAll();
            return Json(new { data = result });
        }

        #endregion
    }
}
