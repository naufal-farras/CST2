using CST.Data;
using CST.Models.Master;
using CST.ViewModels.EBook;
using CST.ViewModels.Rumusan;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CST.Repository
{

    public class T_EBook_Repository
    {
        private readonly AppDbContext _context;

        public T_EBook_Repository(AppDbContext context)
        {
            _context = context;
        }
        public EbookVM GetRumusan()
        {
            EbookVM result = new EbookVM(); 
         
            result.rumusanNasabah = _context.T_RumusanNasabah.ToList();

            return result; 

        }
        public List<T_Transaksi> GetTable(int id)
        {
            if(id == 0)
            {
                var result = _context.T_Transaksi.Where(x => x.IsDelete == false).OrderByDescending(x=> x.CreatedDate).ToList();
                return result;
            }
            else
            {
                var result = _context.T_Transaksi.Where(x => x.RumusanNasabahId == id && x.IsDelete == false).OrderByDescending(x => x.CreatedDate).ToList();
                return result;
            }




        }
        public RumusVM GetDataNasabah()
        {
            RumusVM result = new RumusVM();
            result.T_RumusanNasabah = _context.T_RumusanNasabah.ToList(); 

            return result;



        }
        public InputDataVM GetTemplateEdit(int Id)
        {
            InputDataVM result = new InputDataVM();

            var getIdTemplate = _context.T_Transaksi.Where(x => x.Id == Id).FirstOrDefault().RumusanNasabahId;

           

            result = (from head in _context.T_RumusanNasabah.Where(x => x.Id == getIdTemplate).ToList()
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
            result.T_TransDetail = _context.T_TransDetail.Where(x => x.TransaksiId == Id).ToList();
            result.Kelompok = _context.T_Transaksi.Where(x => x.Id == Id).SingleOrDefault().Nama;
            result.TanggalSampul = _context.T_Transaksi.Where(x => x.Id == Id).SingleOrDefault().TanggalSampul;
            result.NamaEbook = _context.T_Transaksi.Where(x => x.Id == Id).SingleOrDefault().Kelompok;

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
        public InputDataVM GetTemplatePDF(int getIdrumusan)
        {
            InputDataVM result = new InputDataVM();


            result = (from head in _context.T_RumusanNasabah.Where(x => x.Id == getIdrumusan).ToList()
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

        public JsonResult DeleteEbook(int id)
        {
            var result = false;
            if (id != 0)
            {
                var Trans = _context.T_Transaksi.Where(x => x.Id == id).SingleOrDefault();
                Trans.IsDelete = true;
                _context.Update(Trans);
                _context.SaveChanges();
                result = true;
            }


            return new JsonResult(result);
        }


        //public InputDataVM GetTemplateEditTES()
        //{
        //    InputDataVM result = new InputDataVM();

        //    var getIdTemplate = _context.T_Transaksi.Where(x => x.Id == Id).FirstOrDefault().RumusanNasabahId;



        //    result = (from head in _context.T_RumusanNasabah.Where(x => x.Id == getIdTemplate).ToList()
        //              select new InputDataVM
        //              {
        //                  Babs = (from head2 in _context.T_RumusanBab.Include(x => x.Bab).Where(x => x.RumusanNasabahId == head.Id).ToList()
        //                          select new Babs
        //                          {
        //                              BabId = head2.BabId,
        //                              namaBab = head2.Bab.Nama,

        //                              SubBabs = (from head3 in _context.T_RumusanSubBab.Include(x => x.SubBab).Where(x => x.RumusanBabId == head2.Id).ToList()
        //                                         select new SubBabs
        //                                         {
        //                                             SubBabId = head3.SubBabId,
        //                                             namaSubBab = head3.SubBab.Nama,

        //                                             SubSubBabs = (from head4 in _context.T_RumusanSubSubBab.Include(x => x.SubSubBab).Where(x => x.RumusanSubBabId == head3.Id).ToList()
        //                                                           select new SubSubBabs
        //                                                           {
        //                                                               namaSubSubBab = head4.SubSubBab.Nama,
        //                                                               SubSubBabId = head4.SubSubBabId,


        //                                                           }).ToList(),

        //                                         }).ToList(),

        //                          }).ToList(),


        //              }).FirstOrDefault();
        //    result.T_TransDetail = _context.T_TransDetail.Where(x => x.TransaksiId == Id).ToList();
        //    result.Kelompok = _context.T_Transaksi.Where(x => x.Id == Id).SingleOrDefault().Nama;
        //    result.NamaEbook = _context.T_Transaksi.Where(x => x.Id == Id).SingleOrDefault().Kelompok;

        //    return result;



        //}
   
    }
}
