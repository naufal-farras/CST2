using CST.Data;
using CST.Models;
using CST.Models.Master;
using CST.Repository;
using CST.ViewModels.EBook;
using CST.ViewModels.HelperVM;
using CST.ViewModels.Rumusan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CST.Controllers
{
    //[Authorize]
    public class RumusanController : Controller
    {
        private readonly AppDbContext _context;

        //private readonly ILogger<RumusanController> _logger;
        //private readonly IWebHostEnvironment _webHostEnvironment;
        //private readonly UserManager<M_User> _userManager;
        //private readonly M_Usaser_Repository _userRepository;
        private readonly T_RumusanNasabah_Repository _rumusanNasabah_Repository;
        public RumusanController(AppDbContext context, T_RumusanNasabah_Repository rumusanNasabah_Repository)
        {
            _context = context;
            _rumusanNasabah_Repository = rumusanNasabah_Repository;

        }

        [AllowAnonymous]

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Rumusan/GetTemplate/{Id}")]
        public InputDataVM GetTemplate(int Id)
        {
            InputDataVM aa = new InputDataVM();
            aa = _rumusanNasabah_Repository.GetTemplate(Id);
            return aa;
        }

        public JsonResult GetTable()
        {
            var result = _rumusanNasabah_Repository.GetTableBab();
            return Json(new { data = result });
        }
        public IActionResult AddNasabah()
        {
            return View();
        }
        public IActionResult AddBab()
        {
            var result = _rumusanNasabah_Repository.GetMaster();
            return View(result);
        }
        public IActionResult AddSub()
        {
            var result = _rumusanNasabah_Repository.GetMaster();
            return View(result);
        }
        public IActionResult AddSubSub()
        {
            var result = _rumusanNasabah_Repository.GetMaster();
            return View(result);
        }
        public IActionResult Tambah()
        {
            var result = _rumusanNasabah_Repository.GetMaster();
            return View(result);
        }

        #region GET DATA      
        [HttpGet("Rumusan/GetAllData")]
        public JsonResult GetNasabah()
        {
            var result = _rumusanNasabah_Repository.GetMaster();
            return Json(new { data = result });
        }
        //[HttpGet("Rumusan/GetSubBabBefore")]
        //public JsonResult GetSubBabBefore(int id, int babId)
        //{
        //    var result = _rumusanNasabah_Repository.GetSubBab(id, babId);
        //    return Json(new { data = result });
        //}

        [HttpGet("Rumusan/GetDataById")]
        public JsonResult GetDataById(int Id)
        {
            var result = _rumusanNasabah_Repository.GetDataById(Id);
            return Json(new { data = result });
        }

        [HttpGet("Rumusan/GetBabById")]
        public JsonResult GetBabById(int Id)
        {
            var result = _rumusanNasabah_Repository.GetBabById(Id);
            return Json(new { data = result });
        }

        [HttpGet("Rumusan/GetBabByNasabah")]
        public JsonResult GetBabByNasabah(int NasabahId)
        {
            var result = _rumusanNasabah_Repository.GetBabByNasabah(NasabahId);
            return Json(new { data = result });
        }

        [HttpGet("Rumusan/GetSubBabById")]
        public JsonResult GetSubBabById(int Id)
        {
            var result = _rumusanNasabah_Repository.GetSubBabById(Id);
            return Json(new { data = result });
        }

        [HttpGet("Rumusan/GetSubBabByBab")]
        public JsonResult GetSubBabByBab(int Id, int BabId)
        {
            var result = _rumusanNasabah_Repository.GetSubBabByBab(Id, BabId);
            return Json(new { data = result });
        }

        [HttpGet("Rumusan/GetSubBabByNasabah")]

        public JsonResult GetSubBabByNasabah(int BabId)
        {
            var result = _rumusanNasabah_Repository.GetSubBabByNasabah(BabId);
            return Json(new { data = result });
        }

        [HttpGet("Rumusan/GetSubSubBabBySubBab")]
        public JsonResult GetSubSubBabBySubBab(int Id, int BabId, int SubBabId)
        {
            var result = _rumusanNasabah_Repository.GetSubSubBabBySubBab(Id, BabId, SubBabId);
            return Json(new { data = result });
        }
        [HttpGet("Rumusan/GetSubSubBabByNasabah")]
        public JsonResult GetSubSubBabByNasabah(int SubBabId)
        {
            var result = _rumusanNasabah_Repository.GetSubSubBabByNasabah(SubBabId);
            return Json(new { data = result });
        }


        [HttpGet("Rumusan/GetSubById")]
        public JsonResult GetSubById(int Id)
        {
            var result = _rumusanNasabah_Repository.GetSubById(Id);
            return Json(new { data = result });
        }

        #endregion

        #region  Process 
        //[HttpPost("Rumusan/SaveRumusan")]
        //public JsonResult SaveRumusan(List<AktifitasVM> data, int rumusanNasabahId, int nasabahId, string template)
        //{
        //    var result = _rumusanNasabah_Repository.SaveRumusan(data,rumusanNasabahId, nasabahId, template);
        //    return Json(new { data = result });
        //}

        [HttpPost("Rumusan/SaveBab")]
        public JsonResult SaveBab(List<BabVM2> data, int nasabahId, string template)
        {
            var result = _rumusanNasabah_Repository.SaveBab(data, nasabahId, template);
            return Json(new { data = result });
        }

        [HttpPost("Rumusan/SaveSubBab")]
        public JsonResult SaveSubBab(List<BabVM2> data, int rumusanId, int BabId)
        {
            var result = _rumusanNasabah_Repository.SaveSubBab(data, rumusanId, BabId);
            return Json(new { data = result });
        }

        [HttpPost("Rumusan/SaveSubSubBab")]
        public JsonResult SaveSubSubBab(List<BabVM2> data, int rumusanId, int SubBabId)
        {
            var result = _rumusanNasabah_Repository.SaveSubSubBab(data, rumusanId, SubBabId);
            return Json(new { data = result });
        }

        [HttpPost("Rumusan/UpdateBab")]
        public JsonResult UpdateBab(int Id, string Nama)
        {
            var result = _rumusanNasabah_Repository.UpdateBab(Id, Nama);
            return Json(new { data = result });

        }

        [HttpPost("Rumusan/UpdateSubBab")]
        public JsonResult UpdateSubBab(List<BabVM2> data, int BabId, int NasabahId)
        {
            var result = _rumusanNasabah_Repository.UpdateSubBab(data, BabId, NasabahId);
            return Json(new { data = result });

        }
        [HttpPost("Rumusan/UpdateSubSubBab")]
        public JsonResult UpdateSubSubBab(List<BabVM2> data, int BabId, int SubBabId, int NasabahId)
        {
            var result = _rumusanNasabah_Repository.UpdateSubSubBab(data, BabId, SubBabId, NasabahId);
            return Json(new { data = result });

        }


        [HttpDelete("Rumusan/DeleteBab")]
        public JsonResult DeleteBab(int Id)
        {
            var result = _rumusanNasabah_Repository.DeleteBab(Id);
            return Json(new { data = result });

        }

        [HttpDelete("Rumusan/DeleteSubBab")]
        public JsonResult DeleteSubBab(int Id, int BabId, int SubBabId)
        {
            var result = _rumusanNasabah_Repository.DeleteSubBab(Id, BabId, SubBabId);
            return Json(new { data = result });

        }

        [HttpDelete("Rumusan/DeleteSubSubBab")]
        public JsonResult DeleteSubSubBab(int Id, int BabId, int SubBabId, int SubSubBabId)
        {
            var result = _rumusanNasabah_Repository.DeleteSubSubBab(Id, BabId, SubBabId, SubSubBabId);
            return Json(new { data = result });

        }

        #endregion


        #region PROSES

        //[AllowAnonymous]
        //[Route("Rumusan/Save")]
        //[HttpPost]
        //public async Task<IActionResult> Save(RumusVM rumusVM)
        //{

        //    var result = false;

        //    if (rumusVM != null)
        //    {
        //        try
        //        {
        //            int rumusId = 0;
        //            int Index = 0;
        //            var ceklastrumus = _context.T_RumusanNasabah.Where(x => x.NasabahId == rumusVM.nasabahId).OrderByDescending(x => x.Index).FirstOrDefault();
        //            if (ceklastrumus != null)
        //            {
        //                Index = ceklastrumus.Index++;
        //            }


        //            if (rumusVM.rumusanNasabahId == 0)
        //            {
        //                var rumus = new T_RumusanNasabah();
        //                rumus.Index = Index;
        //                rumus.NasabahId = rumusVM.nasabahId;
        //                rumus.Nama = rumusVM.template;
        //                rumus.CreatedDate = DateTime.Now;

        //                _context.T_RumusanNasabah.Add(rumus);
        //                _context.SaveChanges();

        //                rumusId = rumus.Id;

        //            }
        //            else
        //            {
        //                var rumus = _context.T_RumusanNasabah.Single(x => x.Id == rumusVM.rumusanNasabahId);

        //                rumus.Index = Index;
        //                rumus.NasabahId = rumusVM.nasabahId;
        //                rumus.Nama = rumusVM.template;
        //                rumus.UpdatedDate = DateTime.Now;

        //                _context.Entry(rumus).State = EntityState.Modified;
        //                _context.SaveChanges();

        //                var getRumusan = _context.T_RumusanBab.Where(x => x.RumusanNasabahId == rumus.Id);
        //                _context.T_RumusanBab.RemoveRange(getRumusan);
        //                _context.SaveChanges();

        //                rumusId = rumus.Id;
        //            }

        //            //foreach (var main in data)
        //            //{
        //            //    var Bab = new T_RumusanBab();
        //            //    Bab.RumusanNasabahId = rumusId;
        //            //    Bab.BabId = main.BabId;
        //            //    _context.T_RumusanBab.Add(Bab);
        //            //    _context.SaveChanges();

        //            //    foreach (var sub in main.SubBab)
        //            //    {
        //            //        var detail = new T_RumusanSubBab();
        //            //        detail.RumusanBabId = Bab.Id;
        //            //        detail.SubBabId = sub.SubBabId;

        //            //        _context.T_RumusanSubBab.Add(detail);
        //            //        _context.SaveChanges();

        //            //        foreach (var subsub in main.SubSubBab)
        //            //        {
        //            //            var detail2 = new T_RumusanSubSubBab();
        //            //            detail2.RumusanSubBabId = sub.SubBabId;
        //            //            detail2.SubSubBabId = subsub.SubSubBabId;

        //            //            _context.T_RumusanSubSubBab.Add(detail2);
        //            //            _context.SaveChanges();

        //            //        }
        //            //    }
        //            //}

        //        }
        //        catch (Exception ex)
        //        {

        //            //statusHelperVM.StatusCategory = StatusCategory.Error;
        //            //statusHelperVM.Message = "Error saving data. Message : " + ex.Message;
        //        }
        //        result = true;

        //    }

        //    //else
        //    //{
        //    //    statusHelperVM.StatusCategory = StatusCategory.DataExists;
        //    //    statusHelperVM.Message = "Sudah ada nama tersebut!";
        //    //}
        //    return Json(result);
        //}



        #region GET DATA        
        [HttpGet("Rumusan/GetRumus")]
        public JsonResult GetRumus()
        {
            //string currentUserId = _userManager.GetUserId(User) ?? null;
            //bool isAdmin = User.IsInRole("Admin");

            var result = _context.T_RumusanNasabah.ToList();
            return Json(new { data = result });
        }
        #endregion


        #endregion

    }
}
