using CST.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using CST.ViewModels.Rumusan;
using Microsoft.EntityFrameworkCore;
using CST.Models.Master;
using System.Threading.Tasks;
using CST.ViewModels.HelperVM;
using System;
using CST.ViewModels.EBook;

namespace CST.Repository
{
    public class T_RumusanNasabah_Repository
    {
        private readonly AppDbContext _context;

        public T_RumusanNasabah_Repository(AppDbContext context)
        {
            _context = context;
        }


        //public List<ViewRumusVM> Index()
        //{
        //    List<ViewRumusVM> result = new List<ViewRumusVM>();

        //    result = (from head in _context.T_RumusanNasabah.Include(x => x.Nasabah).ToList()
        //              select new ViewRumusVM
        //              {
        //                  Id = head.Id,
        //                  NamaNasabah = head.Nasabah.Nama,
        //                  Template = head.Nama,
        //                  Bab = _context.T_RumusanBab.Where(x => x.RumusanNasabahId == head.Id).Count(),
        //                  //SubBab = _context.T_RumusanSubBab.Where(x => x.RumusanBab.RumusanNasabahId == head.Id).Count(),
        //                  //SubSubBab = _context.T_RumusanSubSubBab.Where(x => x.RumusanSubBab.RumusanBab.RumusanNasabahId== head.Id).Count()
        //              }).ToList();

        //    return result;
        //}


        public List<ViewTableVM> GetTableBab()
        {
            List<ViewTableVM> result = new List<ViewTableVM>();


            result = (from head in _context.T_RumusanNasabah.Include(x => x.Nasabah).ToList()
                      select new ViewTableVM
                      {
                          Id = head.Id,
                          nasabahId = head.NasabahId,
                          NamaNasabah = head.Nasabah.Nama,
                          Template = head.Nama,
                          Bab = _context.T_RumusanBab.Where(x => x.RumusanNasabahId == head.Id).Count(),
                          SubBab = _context.T_RumusanSubBab.Where(x => x.RumusanBab.RumusanNasabahId == head.Id).Count(),
                          SubSubBab = _context.T_RumusanSubSubBab.Where(x => x.RumusanSubBab.RumusanBab.RumusanNasabahId == head.Id).Count()
                      }).ToList();

            return result;


        }

        public InputDataVM GetTemplate(int Id)
        {
            InputDataVM result = new InputDataVM();


            result = (from head in _context.T_RumusanNasabah.Where(x => x.Id == Id).ToList()
                      select new InputDataVM
                      {
                          Babs = (from head2 in _context.T_RumusanBab.Include(x => x.Bab).Where(x => x.RumusanNasabahId == head.Id).ToList()
                                  select new Babs
                                  {
                                      BabId = head2.BabId,
                                      namaBab = head2.Bab.Nama,

                                      SubBabs = (from head3 in _context.T_RumusanSubBab.Include(x => x.SubBab).Where(x => x.RumusanBabId == head2.Id).ToList()
                                                 select new SubBabs
                                                 {
                                                     SubBabId = head3.SubBabId,
                                                     namaSubBab = head3.SubBab.Nama,

                                                     SubSubBabs = (from head4 in _context.T_RumusanSubSubBab.Include(x => x.SubSubBab).Where(x => x.RumusanSubBabId == head3.Id).ToList()
                                                                   select new SubSubBabs
                                                                   {
                                                                       namaSubSubBab = head4.SubSubBab.Nama,
                                                                       SubSubBabId = head4.SubSubBabId,
                                                                   }).ToList(),

                                                 }).ToList(),

                                  }).ToList(),


                      }).FirstOrDefault();

            return result;



        }

        #region GET DATA  
        public RumusVM GetMaster()
        {
            RumusVM result = new RumusVM();

            result.M_Nasabah = _context.M_Nasabah.ToList();
            result.M_Bab = _context.M_Bab.ToList();
            result.M_SubBab = _context.M_SubBab.ToList();
            result.M_SubSubBab = _context.M_SubSubBab.ToList();
            result.T_RumusanSubBab = _context.T_RumusanSubBab.ToList();
            return result;

        }


        public JsonResult GetDataById(int Id)
        {
            var result = _context.T_RumusanBab.Include(x => x.RumusanNasabah).Include(x => x.Bab).Where(x => x.RumusanNasabah.Id == Id && x.Bab.NasabahId == x.RumusanNasabah.NasabahId).ToList();

            return new JsonResult(result);
        }
        public JsonResult GetBabById(int Id)
        {
            //var result = _context.T_RumusanBab.Include(x => x.RumusanNasabah).Include(x => x.Bab).Where(x => x.RumusanNasabah.Id == Id).ToList();
            var result = _context.T_RumusanBab.Include(x => x.RumusanNasabah).Include(x => x.Bab).Where(x => x.RumusanNasabah.Id == Id && x.Bab.Id == x.BabId).ToList();

            return new JsonResult(result);
        }
        public JsonResult GetBabByNasabah(int NasabahId)
        {
            //var result = _context.T_RumusanBab.Include(x => x.RumusanNasabah).Include(x => x.Bab).Where(x => x.RumusanNasabah.Id == Id).ToList();
            var result = _context.M_Bab.Where(x => x.NasabahId == NasabahId).ToList();

            return new JsonResult(result);
        }
        public JsonResult GetSubBabById(int Id)
        {
            var result = _context.T_RumusanSubBab.Include(x => x.RumusanBab).Include(x => x.RumusanBab.RumusanNasabah).Include(x => x.SubBab).Where(x => x.RumusanBab.RumusanNasabah.Id == Id).ToList();

            return new JsonResult(result);
        }

        public JsonResult GetSubBabByBab(int Id, int BabId)
        {
            var result = _context.T_RumusanSubBab.Include(x => x.RumusanBab).Include(x => x.RumusanBab.RumusanNasabah).Include(x => x.SubBab).Include(x => x.RumusanBab.RumusanNasabah).Where(x => x.RumusanBab.RumusanNasabah.Id == Id && x.RumusanBab.BabId == BabId).ToList();

            return new JsonResult(result);
        }
        public JsonResult GetSubBabByNasabah(int BabId)
        {
            var nasabahId = _context.M_Bab.Where(x => x.Id == BabId).FirstOrDefault();
            var result = _context.M_SubBab.Where(x => x.NasabahId == nasabahId.NasabahId).ToList();
            return new JsonResult(result);
        }
        public JsonResult GetSubSubBabBySubBab(int Id, int BabId, int SubBabId)
        {
            //var result = _context.T_RumusanSubSubBab.Include(x => x.RumusanSubBab).Include(x => x.SubSubBab).Where(x => x.RumusanSubBab.RumusanBab.RumusanNasabahId == Id && x.RumusanSubBab.RumusanBab.BabId == BabId && x.RumusanSubBab.Id==SubBabId).ToList();
            var result = _context.T_RumusanSubSubBab.Include(x => x.RumusanSubBab).Include(x => x.RumusanSubBab.RumusanBab).Include(x => x.SubSubBab).Where(x => x.RumusanSubBab.RumusanBab.RumusanNasabahId == Id && x.RumusanSubBab.RumusanBab.BabId == BabId && x.RumusanSubBab.SubBabId == SubBabId).ToList();
            //var result = _context.T_RumusanSubSubBab.Include(x => x.RumusanSubBab).Include(x => x.RumusanSubBab.RumusanBab).Include(x => x.SubSubBab).Where(x => x.RumusanSubBab.RumusanBab.RumusanNasabahId == Id && x.RumusanSubBab.RumusanBab.BabId == BabId && x.RumusanSubBab.SubBabId == SubBabId).ToList();
            return new JsonResult(result);
        }
        public JsonResult GetSubSubBabByNasabah(int SubBabId)
        {
            var subbabId = _context.T_RumusanSubBab.Where(x => x.Id == SubBabId).FirstOrDefault().SubBabId;
            var nasabahId = _context.M_SubBab.Where(x => x.Id == subbabId).FirstOrDefault();
            var result = _context.M_SubSubBab.Where(x => x.NasabahId == nasabahId.NasabahId).ToList();
            return new JsonResult(result);
        }
        public JsonResult GetSubById(int Id)
        {
            var result = _context.T_RumusanSubBab.Include(x => x.RumusanBab).Include(x => x.SubBab).Include(x => x.RumusanBab.Bab).Where(x => x.RumusanBab.RumusanNasabahId == Id).ToList();


            return new JsonResult(result);
        }


        public JsonResult GetSubBab(int id, int babId)
        {
            var index = _context.T_RumusanNasabah.Where(x => x.Id == id).AsEnumerable().Last().Index;
            var babid = _context.T_RumusanBab.Where(x => x.BabId == babId).FirstOrDefault().BabId;

            var getData = _context.T_RumusanSubBab.Include(x => x.RumusanBab)
                         .Include(x => x.RumusanBab.RumusanNasabah).Include(x => x.RumusanBab.Bab)
                         .Include(x => x.SubBab)
                         .Where(x => x.RumusanBab.RumusanNasabah.Id == id && x.RumusanBab.RumusanNasabah.Index == index
                            && x.RumusanBab.BabId == babid)
                         .ToList();


            //var Index = getData.AsEnumerable().Last().RumusanBab.RumusanNasabah.Index;

            return new JsonResult(getData);

        }

        public JsonResult UpdateBab(int Id, string Nama)
        {
            var result = false;
            var getData = _context.T_RumusanNasabah.Where(x => x.Id == Id).FirstOrDefault();
            if (getData != null)
            {
                //getData.isDelete = true;
                getData.Nama = Nama;
                _context.Update(getData);
                var delete = _context.SaveChanges();
                result = true;
            }
            return new JsonResult(result);
        }
        public JsonResult UpdateSubBab(List<BabVM2> data, int BabId, int NasabahId)
        {
            var result = false;
            try
            {
                //var getBab = _context.T_RumusanBab.Where(x => x.BabId == && x.RumusanNasabahId == NasabahId);
                var getData = _context.T_RumusanSubBab.Include(x => x.RumusanBab).Where(x => x.RumusanBab.BabId == BabId && x.RumusanBab.RumusanNasabahId == NasabahId).ToList();

                int i = 0;
                foreach (var main in data)
                {
                    getData[i].SubBabId = main.SubBabId;


                    _context.T_RumusanSubBab.Update(getData[i]);
                    _context.SaveChanges();
                    i++;
                }
                result = true;

            }
            catch (Exception ex)
            {

            }


            return new JsonResult(result);


        }

        public JsonResult UpdateSubSubBab(List<BabVM2> data, int BabId, int SubBabId, int NasabahId)
        {
            var result = false;
            try
            {
                //tes
                //var getBab = _context.T_RumusanBab.Where(x => x.BabId == && x.RumusanNasabahId == NasabahId);
                var getData = _context.T_RumusanSubSubBab.Include(x => x.RumusanSubBab).Include(x => x.RumusanSubBab.RumusanBab).Where(x => x.RumusanSubBab.SubBabId == SubBabId && x.RumusanSubBab.RumusanBab.BabId == BabId && x.RumusanSubBab.RumusanBab.RumusanNasabahId == NasabahId).ToList();

                int i = 0;
                foreach (var main in data)
                {
                    getData[i].SubSubBabId = main.SubSubBabId;


                    _context.T_RumusanSubSubBab.Update(getData[i]);
                    _context.SaveChanges();
                    i++;
                }
                result = true;

            }
            catch (Exception ex)
            {

            }


            return new JsonResult(result);


        }

        //modified
        public JsonResult DeleteBab(int Id)
        {
            var result = false;
            var getData = _context.T_RumusanNasabah.Where(x => x.Id == Id).SingleOrDefault();
            if (getData != null)
            {
                //getData.isDelete = true;
                _context.Remove(getData);
                var delete = _context.SaveChanges();
                result = true;
            }

            return new JsonResult(result);
        }

        public JsonResult DeleteSubBab(int Id, int BabId, int SubBabId)
        {
            var result = false;
            //var getId = _context.T_RumusanBab.Where(x => x.RumusanNasabahId  == Id).SingleOrDefault().Id;
            //var getData = _context.T_RumusanSubBab.Where(x => x.RumusanSubBabId == getId).ToList();
            var getId = _context.T_RumusanSubBab.Include(x => x.RumusanBab).Include(x => x.RumusanBab.RumusanNasabah).Where(x => x.RumusanBab.RumusanNasabah.Id == Id && x.RumusanBab.BabId == BabId && x.SubBabId == SubBabId).FirstOrDefault().Id;
            var getData = _context.T_RumusanSubBab.Where(x => x.Id == getId).SingleOrDefault();
            if (getData != null)
            {
                //getData.isDelete = true;

                _context.Remove(getData);
                var delete = _context.SaveChanges();
                result = true;
            }

            return new JsonResult(result);
        }


        public JsonResult DeleteSubSubBab(int Id, int BabId, int SubBabId, int SubSubBabId)
        {
            var result = false;
            //var getId = _context.T_RumusanBab.Where(x => x.RumusanNasabahId  == Id).SingleOrDefault().Id;
            //var getData = _context.T_RumusanSubBab.Where(x => x.RumusanSubBabId == getId).ToList();
            var getId = _context.T_RumusanSubSubBab.Include(x => x.RumusanSubBab).Include(x => x.RumusanSubBab.RumusanBab).Include(x => x.RumusanSubBab.RumusanBab.RumusanNasabah).Where(x => x.RumusanSubBab.RumusanBab.RumusanNasabah.Id == Id && x.RumusanSubBab.RumusanBab.BabId == BabId && x.RumusanSubBab.SubBabId == SubBabId && x.SubSubBabId == SubSubBabId).FirstOrDefault().Id;
            var getData = _context.T_RumusanSubSubBab.Where(x => x.Id == getId).SingleOrDefault();
            if (getData != null)
            {
                //getData.isDelete = true;

                _context.Remove(getData);
                var delete = _context.SaveChanges();
                result = true;
            }

            return new JsonResult(result);
        }
        #endregion

        #region GET SAVE 
        public JsonResult SaveBab(List<BabVM2> data, int nasabahId, string template)
        {
            var result = false;
            try
            {
                int rumusId = 0;
                int Index = 0;
                var ceklastrumus = _context.T_RumusanNasabah.Where(x => x.NasabahId == nasabahId).OrderByDescending(x => x.Index).FirstOrDefault();
                if (ceklastrumus != null)
                {
                    Index = ceklastrumus.Index + 1;
                }
                if (rumusId == 0)
                {
                    var rumus = new T_RumusanNasabah();
                    rumus.Index = Index;
                    rumus.NasabahId = nasabahId;
                    rumus.Nama = template;
                    rumus.CreatedDate = DateTime.Now;

                    _context.T_RumusanNasabah.Add(rumus);
                    _context.SaveChanges();

                    rumusId = rumus.Id;

                }
                else
                {
                    var rumus = _context.T_RumusanNasabah.Single(x => x.Id == rumusId);

                    rumus.Index = Index;
                    rumus.NasabahId = nasabahId;
                    rumus.Nama = template;
                    rumus.UpdatedDate = DateTime.Now;

                    _context.Entry(rumus).State = EntityState.Modified;
                    _context.SaveChanges();

                    var getRumusan = _context.T_RumusanBab.Where(x => x.RumusanNasabahId == rumus.Id);
                    _context.T_RumusanBab.RemoveRange(getRumusan);
                    _context.SaveChanges();

                    rumusId = rumus.Id;
                }


                foreach (var main in data)
                {
                    var Bab = new T_RumusanBab();
                    Bab.RumusanNasabahId = rumusId;
                    Bab.BabId = main.BabId;
                    _context.T_RumusanBab.Add(Bab);
                    _context.SaveChanges();

                }
                result = true;

            }
            catch (Exception ex)
            {

            }


            return new JsonResult(result);


        }


        public JsonResult SaveSubBab(List<BabVM2> data, int rumusanId, int BabId)
        {
            var result = false;
            try
            {
                var getData = _context.T_RumusanBab.Where(x => x.BabId == BabId && x.RumusanNasabahId == rumusanId).SingleOrDefault().Id;

                foreach (var main in data)
                {
                    var SubBab = new T_RumusanSubBab();
                    SubBab.RumusanBabId = getData;
                    SubBab.SubBabId = main.SubBabId;
                    _context.T_RumusanSubBab.Add(SubBab);
                    _context.SaveChanges();

                }
                result = true;

            }
            catch (Exception ex)
            {

            }


            return new JsonResult(result);


        }

        public JsonResult SaveSubSubBab(List<BabVM2> data, int rumusanId, int SubBabId)
        {
            var result = false;
            try
            {

                foreach (var main in data)
                {
                    var SubSubBab = new T_RumusanSubSubBab();
                    SubSubBab.RumusanSubBabId = SubBabId;
                    SubSubBab.SubSubBabId = main.SubSubBabId;
                    _context.T_RumusanSubSubBab.Add(SubSubBab);
                    _context.SaveChanges();

                }
                result = true;

            }
            catch (Exception ex)
            {

            }


            return new JsonResult(result);


        }


        public JsonResult SaveRumusan(List<AktifitasVM> data, int rumusanNasabahId, int nasabahId, string template)

        {
            var result = false;

            if (data != null)
            {
                try
                {
                    int rumusId = 0;
                    int Index = 0;
                    var ceklastrumus = _context.T_RumusanNasabah.Where(x => x.NasabahId == nasabahId).OrderByDescending(x => x.Index).FirstOrDefault();
                    if (ceklastrumus != null)
                    {
                        Index = ceklastrumus.Index + 1;
                    }


                    if (rumusanNasabahId == 0)
                    {
                        var rumus = new T_RumusanNasabah();
                        rumus.Index = Index;
                        rumus.NasabahId = nasabahId;
                        rumus.Nama = template;
                        rumus.CreatedDate = DateTime.Now;

                        _context.T_RumusanNasabah.Add(rumus);
                        _context.SaveChanges();

                        rumusId = rumus.Id;

                    }
                    else
                    {
                        var rumus = _context.T_RumusanNasabah.Single(x => x.Id == rumusanNasabahId);

                        rumus.Index = Index;
                        rumus.NasabahId = nasabahId;
                        rumus.Nama = template;
                        rumus.UpdatedDate = DateTime.Now;

                        _context.Entry(rumus).State = EntityState.Modified;
                        _context.SaveChanges();

                        var getRumusan = _context.T_RumusanBab.Where(x => x.RumusanNasabahId == rumus.Id);
                        _context.T_RumusanBab.RemoveRange(getRumusan);
                        _context.SaveChanges();

                        rumusId = rumus.Id;
                    }

                    foreach (var main in data)
                    {
                        var Bab = new T_RumusanBab();
                        Bab.RumusanNasabahId = rumusId;
                        Bab.BabId = main.AktifitasId;
                        _context.T_RumusanBab.Add(Bab);
                        _context.SaveChanges();

                        foreach (var sub in main.SubAktifitas)
                        {
                            var detail = new T_RumusanSubBab();
                            detail.RumusanBabId = Bab.Id;
                            detail.SubBabId = sub.SubId;

                            _context.T_RumusanSubBab.Add(detail);
                            _context.SaveChanges();


                        }
                    }
                }


                catch (Exception ex)
                {

                    //statusHelperVM.StatusCategory = StatusCategory.Error;
                    //statusHelperVM.Message = "Error saving data. Message : " + ex.Message;
                }
                result = true;

            }

            //else
            //{
            //    statusHelperVM.StatusCategory = StatusCategory.DataExists;
            //    statusHelperVM.Message = "Sudah ada nama tersebut!";
            //}
            return new JsonResult(result);

        }
        #endregion

        //public JsonResult Save(List<BabVM>? data, int rumusanNasabahId, int nasabahId, string template)
        //{
        //    //StatusHelperVM statusHelperVM = new StatusHelperVM();

        //    var result = false;

        //    if (data != null)
        //    {
        //        try
        //        {
        //            int rumusId = 0;
        //            int Index = 0;
        //            var ceklastrumus = _context.T_RumusanNasabah.Where(x => x.NasabahId == nasabahId).OrderByDescending(x => x.Index).FirstOrDefault();
        //            if (ceklastrumus != null)
        //            {
        //                Index = ceklastrumus.Index++;
        //            }


        //            if (rumusanNasabahId == 0)
        //            {
        //                var rumus = new T_RumusanNasabah();
        //                rumus.Index = Index;
        //                rumus.NasabahId = nasabahId;
        //                rumus.Nama = template;
        //                rumus.CreatedDate = DateTime.Now;

        //                _context.T_RumusanNasabah.Add(rumus);
        //                _context.SaveChanges();

        //                rumusId = rumus.Id;

        //            }
        //            else
        //            {
        //                var rumus = _context.T_RumusanNasabah.Single(x => x.Id == rumusanNasabahId);

        //                rumus.Index = Index;
        //                rumus.NasabahId = nasabahId;
        //                rumus.Nama = template;
        //                rumus.UpdatedDate = DateTime.Now;

        //                _context.Entry(rumus).State = EntityState.Modified;
        //                _context.SaveChanges();

        //                var getRumusan = _context.T_RumusanBab.Where(x => x.RumusanNasabahId == rumus.Id);
        //                _context.T_RumusanBab.RemoveRange(getRumusan);
        //                _context.SaveChanges();

        //                rumusId = rumus.Id;
        //            }

        //            foreach (var main in data)
        //            {
        //                var Bab = new T_RumusanBab();
        //                Bab.RumusanNasabahId = rumusId;
        //                Bab.BabId = main.BabId;
        //                _context.T_RumusanBab.Add(Bab);
        //                _context.SaveChanges();

        //                foreach (var sub in main.SubBab)
        //                {
        //                    var detail = new T_RumusanSubBab();
        //                    detail.RumusanBabId = Bab.Id;
        //                    detail.SubBabId = sub.SubBabId;

        //                    _context.T_RumusanSubBab.Add(detail);
        //                    _context.SaveChanges();

        //                    foreach (var subsub in main.SubSubBab)
        //                    {
        //                        var detail2 = new T_RumusanSubSubBab();
        //                        detail2.RumusanSubBabId = sub.SubBabId;
        //                        detail2.SubSubBabId = subsub.SubSubBabId;

        //                        _context.T_RumusanSubSubBab.Add(detail2);
        //                        _context.SaveChanges();

        //                    }
        //                }
        //            }
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

        //public async Task<StatusHelperVM> Save (List<BabVM> data, int RumusanNasabahId, int NasabahId, string Template)
        //{
        //    StatusHelperVM statusHelperVM = new StatusHelperVM();

        //    if (data != null)
        //    {
        //        try
        //        {
        //            int rumusId = 0;
        //            int Index = 0;
        //            var ceklastrumus = _context.T_RumusanNasabah.Where(x => x.NasabahId == NasabahId).OrderByDescending(x => x.Id).FirstOrDefault();
        //            if (ceklastrumus != null)
        //            {
        //                Index = ceklastrumus.Index++;
        //            }


        //            if (RumusanNasabahId == 0)
        //            {
        //                var rumus = new T_RumusanNasabah();
        //                rumus.Index = Index;
        //                rumus.NasabahId = NasabahId;
        //                rumus.Nama = Template;

        //                _context.T_RumusanNasabah.Add(rumus);
        //                _context.SaveChanges();

        //                rumusId = rumus.Id;

        //            }
        //            else
        //            {
        //                var rumus = _context.T_RumusanNasabah.Single(x => x.Id == RumusanNasabahId);

        //                rumus.Index = Index;
        //                rumus.NasabahId = NasabahId;
        //                rumus.Nama = Template;

        //                _context.Entry(rumus).State = EntityState.Modified;
        //                _context.SaveChanges();

        //                var getRumusan = _context.T_RumusanBab.Where(x => x.RumusanNasabahId == rumus.Id);
        //                _context.T_RumusanBab.RemoveRange(getRumusan);
        //                _context.SaveChanges();

        //                rumusId = rumus.Id;
        //            }

        //            foreach (var main in data)
        //            {
        //                var Bab = new T_RumusanBab();
        //                Bab.RumusanNasabahId = rumusId;
        //                Bab.BabId = main.BabId;
        //                _context.T_RumusanBab.Add(Bab);
        //                _context.SaveChanges();

        //                foreach (var sub in main.SubBab)
        //                {
        //                    var detail = new T_RumusanSubBab();
        //                    detail.RumusanBabId = Bab.Id;
        //                    detail.SubBabId = sub.SubBabId;

        //                    _context.T_RumusanSubBab.Add(detail);
        //                    _context.SaveChanges();

        //                    foreach (var subsub in main.SubSubBab)
        //                    {
        //                        var detail2 = new T_RumusanSubSubBab();
        //                        detail2.RumusanSubBabId = sub.SubBabId;
        //                        detail2.SubSubBabId = subsub.SubSubBabId;

        //                        _context.T_RumusanSubSubBab.Add(detail2);
        //                        _context.SaveChanges();

        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            statusHelperVM.StatusCategory = StatusCategory.Error;
        //            statusHelperVM.Message = "Error saving data. Message : " + ex.Message;
        //        } 
        //    }
        //    else
        //    {
        //        statusHelperVM.StatusCategory = StatusCategory.DataExists;
        //        statusHelperVM.Message = "Sudah ada nama tersebut!";
        //    }
        //    return statusHelperVM;
        //}

        //public ActionResult Input(int? Id)
        //{
        //    if (Id == null)
        //    {
        //        var result = new InputRumusVM();
        //        result.Edit = false;
        //        result.ProjectArea = _context.ProjectArea.ToList();

        //        return View(result);
        //    }
        //    else
        //    {
        //        var result = new InputRumusVM();
        //        result.Edit = true;
        //        result.IdRumusan = Id ?? 0;
        //        result.Template = _context.Rumusan.Where(x => x.Id == Id).Select(x => x.Nama).FirstOrDefault();
        //        result.ProjectArea = _context.ProjectArea.ToList();
        //        result.PASelected = _context.Rumusan.Single(x => x.Id == Id).ProjectAreaId;
        //        result.Aktivitas = _context.Aktivitas.ToList();
        //        result.AktivitasRumusan = _context.AktivitasRumusan.Include(x => x.Aktivitas).Where(x => x.RumusanId == Id).ToList();

        //        return View(result);
        //    }
        //}

        //public JsonResult GetBab()
        //{
        //    var result = _context.Aktivitas.ToList();

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult GetSubBab()
        //{
        //    var result = _context.SubAktivitas.ToList();

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult GetSubEdit(int BabId, int RumusanId)
        //{
        //    var result = _context.DetailAktivitasRumusan.Include(x => x.SubAktivitas).Where(x => x.AktivitasRumusan.AktivitasId == BabId && x.AktivitasRumusan.RumusanId == RumusanId).ToList();

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}



        //public JsonResult GetById(int Id)
        //{
        //    List<ViewRumusModalVM> result = new List<ViewRumusModalVM>();

        //    result = (from head in _context.AktivitasRumusan.Include(x => x.Rumusan).Include(x => x.Rumusan.ProjectArea).Include(x => x.Aktivitas).Where(x => x.RumusanId == Id).ToList()
        //              select new ViewRumusModalVM
        //              {
        //                  AktivitasRumusan = head,
        //                  DetailAktivitasRumusan = _context.DetailAktivitasRumusan.Include(x => x.SubAktivitas).Where(x => x.AktivitasRumusanId == head.Id).ToList()
        //              }).ToList();

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult Delete(List<int> Ids)
        //{
        //    bool result = false;
        //    foreach (var Id in Ids)
        //    {
        //        var trans = _context.Rumusan.SingleOrDefault(x => x.Id == Id);
        //        trans.IsDelete = true;
        //        trans.DeleteDate = DateTime.Now;
        //        _context.Entry(trans).State = EntityState.Modified;
        //        _context.SaveChanges();
        //        result = true;
        //    }

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

    }
}
