﻿using CST.Data;
using CST.Models.EBook;
using CST.Models.Master;
using CST.ViewModels.EBook;
using CST.ViewModels.HelperVM;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CST.Repository
{
    public class T_Laporan_Repository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<M_User> _userManager;
        private readonly SignInManager<M_User> _signInManager;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public T_Laporan_Repository(AppDbContext context, IWebHostEnvironment webHostEnvironment,
            SignInManager<M_User> signInManager, UserManager<M_User> userManager)
        {
            _webHostEnvironment = webHostEnvironment;
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        #region PROSES 
        public JsonResult TambahEbook2(List<IFormFile> selectedUpload, string Nama, string Kelompok, int RumusanId, DateTime TanggalSampul, string createId)
        {

            int transId = 0;
            var trans = new T_Transaksi();
            trans.Nama = Nama;
            trans.Kelompok = Kelompok;
            trans.Status = 0;
            trans.CreatedDate = DateTime.Now;
            trans.TanggalSampul = TanggalSampul;
            trans.IsDelete = false;
            trans.CreaterId = createId;
            trans.RumusanNasabahId = RumusanId;
            _context.T_Transaksi.Add(trans);
            _context.SaveChanges();

            transId = trans.Id;
            DateTime date = DateTime.Now;
            var Index = 0;


            foreach (var item in selectedUpload)
            {

                var transDetail = new T_TransDetail();

                string webRootPath = _webHostEnvironment.WebRootPath;
                string path = Path.Combine(webRootPath, "Files/Data");

                if (item.FileName == "PDFkosong.pdf")
                {
                    string generateNameFile = item.FileName;
                    FileStream savingFile = new FileStream(Path.Combine(path, generateNameFile), FileMode.Create);

                    item.CopyTo(savingFile);
                    savingFile.Close();
                    transDetail.TransaksiId = transId;
                    transDetail.Index = Index++;
                    transDetail.Path = generateNameFile;
                }
                else
                {
                    string generateNameFile = Index + "_" + DateTime.Now.ToString("ddMMyyHHMMss") + "_" + item.FileName;

                    FileStream savingFile = new FileStream(Path.Combine(path, generateNameFile), FileMode.Create);

                    item.CopyTo(savingFile);
                    savingFile.Close();
                    transDetail.TransaksiId = transId;
                    transDetail.Index = Index++;
                    transDetail.Path = generateNameFile;

                }

                _context.T_TransDetail.Add(transDetail);
                _context.SaveChanges();


            }

            return new JsonResult(trans);

        }

        //public JsonResult TambahEbook(List<IFormFile> selectedUpload, string Nama, string Kelompok, int RumusanId, DateTime TanggalSampul, string createId)
        public JsonResult TambahEbook(List<string> indx, List<IFormFile> selectedUploads, string Nama, string Kelompok, int RumusanId, DateTime TanggalSampul, string createId)
        {

            int transId = 0;
            var trans = new T_Transaksi();
            trans.Nama = Nama;
            trans.Kelompok = Kelompok;
            trans.Status = 0;
            trans.CreatedDate = DateTime.Now;
            trans.TanggalSampul = TanggalSampul;
            trans.IsDelete = false;
            trans.CreaterId = createId;
            trans.RumusanNasabahId = RumusanId;
            _context.T_Transaksi.Add(trans);
            _context.SaveChanges();

            transId = trans.Id;

            bool result = false;
            DateTime date = DateTime.Now;
            //var Index = 0;
            foreach (var item in selectedUploads)
            {
                var delPdf = item.FileName.Replace(".pdf", "");
                var nameHasil = delPdf.Split('-').ToList();

                foreach (var item2 in indx)
                {
                    var transDetail = new T_TransDetail();


                    string webRootPath = _webHostEnvironment.WebRootPath;
                    string path = Path.Combine(webRootPath, "Files/Data");

                    var delPdf2 = item2.Replace(".pdf", "");
                    var indxHasil = delPdf2.Split('-').ToList();
                    if (nameHasil[1] == "PDFkosong" && indxHasil.Count() < 3)
                    {
                        if (indxHasil[0] == nameHasil[0])
                        {
                            string generateNameFile = nameHasil[1]+".pdf";
                            FileStream savingFile = new FileStream(Path.Combine(path, generateNameFile), FileMode.Create);

                            item.CopyTo(savingFile);
                            savingFile.Close();
                            transDetail.TransaksiId = transId;
                            transDetail.Index = Convert.ToInt32(indxHasil[0]);
                            transDetail.Path = generateNameFile;
                            _context.T_TransDetail.Add(transDetail);
                            _context.SaveChanges();
                            result = true; 
                            continue;
                        }
                      
                    }
                    if (indxHasil.Count > 2 && nameHasil.Count > 1)
                    {
                        if (indxHasil[1].Replace(" ","").ToLower() == nameHasil[0].Replace(" ", "").ToLower() && indxHasil[2].Replace(" ", "").ToLower() == nameHasil[1].Replace(" ", "").ToLower())
                        {
                            int indexFile = Convert.ToInt32(indxHasil[0]);
                            var cekDetail = _context.T_TransDetail.Include(x => x.Transaksi).Where(x => x.TransaksiId == transId && x.Index == indexFile).Count();
                            if (cekDetail == 0)
                            {
                                string generateNameFile = indxHasil[0] + "_" + item.FileName.ToUpper();

                                FileStream savingFile = new FileStream(Path.Combine(path, generateNameFile), FileMode.Create);

                                item.CopyTo(savingFile);
                                savingFile.Close();
                                transDetail.TransaksiId = transId;
                                transDetail.Index = Convert.ToInt32(indxHasil[0]);
                                transDetail.Path = generateNameFile;



                                _context.T_TransDetail.Add(transDetail);
                                _context.SaveChanges();
                                result = true;
                                continue;
                            }
                            else
                            {
                                continue;

                            }

                        }
                    }
                   
                }
            }
           


            //foreach (var item in selectedUploads)
            //{

            //    var transDetail = new T_TransDetail();

            //    string webRootPath = _webHostEnvironment.WebRootPath;
            //    string path = Path.Combine(webRootPath, "Files/Data");

            //    if (item.FileName == "PDFkosong.pdf")
            //    {
            //        string generateNameFile = item.FileName;
            //        FileStream savingFile = new FileStream(Path.Combine(path, generateNameFile), FileMode.Create);

            //        item.CopyTo(savingFile);
            //        savingFile.Close();
            //        transDetail.TransaksiId = transId;
            //        transDetail.Index = Index++;
            //        transDetail.Path = generateNameFile;
            //    }
            //    else
            //    {
            //        string generateNameFile = Index + "_" + DateTime.Now.ToString("ddMMyyHHMMss") + "_" + item.FileName;

            //        FileStream savingFile = new FileStream(Path.Combine(path, generateNameFile), FileMode.Create);

            //        item.CopyTo(savingFile);
            //        savingFile.Close();
            //        transDetail.TransaksiId = transId;
            //        transDetail.Index = Index++;
            //        transDetail.Path = generateNameFile;

            //    }

            //    _context.T_TransDetail.Add(transDetail);
            //    _context.SaveChanges();
            //    result = true;


            //}

            return new JsonResult(result);

        }


        //public JsonResult TambahEbook2(List<TempFileVM> TempFileVM, string Nama, string Kelompok, int RumusanId, DateTime TanggalSampul, string createId)
        //public JsonResult TambahEbook2(List<IFormFile> selectedUpload, string Nama, string Kelompok, int RumusanId, DateTime TanggalSampul, string createId)
        //{
        //    int transId = 0;
        //    var trans = new T_Transaksi();
        //    trans.Nama = Nama;
        //    trans.Kelompok = Kelompok;
        //    trans.Status = 0;
        //    trans.CreatedDate = DateTime.Now;
        //    trans.TanggalSampul = TanggalSampul;
        //    trans.IsDelete = false;
        //    trans.CreaterId = createId;
        //    trans.RumusanNasabahId = RumusanId;
        //    _context.T_Transaksi.Add(trans);
        //    _context.SaveChanges();
        //    transId = trans.Id;

        //    bool result = false;
        //    DateTime date = DateTime.Now;

        //    foreach (var item in selectedUpload)
        //    {
        //        var transDetail = new T_TransDetail();
        //        string webRootPath = _webHostEnvironment.WebRootPath;
        //        string path = Path.Combine(webRootPath, "Files/Data");

        //        foreach (var item2 in item)
        //        {
        //            FileStream savingFile = new FileStream(Path.Combine(path, item2.selectUploads.FileName), FileMode.Create);

        //            item2.selectUploads.CopyTo(savingFile);
        //            savingFile.Close();
        //            transDetail.Index = item.Index;
        //            transDetail.TransaksiId = transId;
        //            transDetail.Path = item2.selectUploads.FileName;
        //            _context.T_TransDetail.Add(transDetail);
        //            _context.SaveChanges();
        //        }
        //        result = true;
        //    }

        //    return new JsonResult(result);

        //}


        //Update
        public JsonResult UpdateFileEbook(List<IFormFile> selectedUpload, int Index, int RumusanId)
        {
            bool result = false;
            var getData = _context.T_TransDetail.Where(x => x.Index == Index && x.TransaksiId == RumusanId).FirstOrDefault();
            var getDataTrans = _context.T_Transaksi.Where(x => x.Id == RumusanId).FirstOrDefault();
            DateTime date = DateTime.Now;
            getDataTrans.UpdatedDate = date;
            foreach (var item in selectedUpload)
            {


                string webRootPath = _webHostEnvironment.WebRootPath;
                string path = Path.Combine(webRootPath, "Files/Data");


                string generateNameFile = "Updated_" +item.FileName ;
                FileStream savingFile = new FileStream(Path.Combine(path, generateNameFile), FileMode.Create);
                item.CopyTo(savingFile);
                savingFile.Close();

                getData.Path = generateNameFile;


                _context.Entry(getData).State = EntityState.Modified;
                _context.Entry(getDataTrans).State = EntityState.Modified;
                _context.SaveChanges();
                result = true;

            }

            return new JsonResult(result);

        }

         public JsonResult RemoveFileEbook(string selectedUpload, int Index, int RumusanId)
        {
            bool result = false;
            var getData = _context.T_TransDetail.Where(x => x.Index == Index && x.TransaksiId == RumusanId).FirstOrDefault();
            var getDataTrans = _context.T_Transaksi.Where(x => x.Id == RumusanId).FirstOrDefault();
            DateTime date = DateTime.Now;
            getDataTrans.UpdatedDate = date;

            string generateNameFile = "PDFkosong.pdf";

            getData.Path = generateNameFile;

            _context.Entry(getData).State = EntityState.Modified;
            _context.Entry(getDataTrans).State = EntityState.Modified;
            _context.SaveChanges();
            result = true;
          
            return new JsonResult(result);

        }

        public JsonResult UpdateJudul(int Id, string NamaEbook, string Kelompok, DateTime TanggalSampul)
        {
            bool result = false;
            var getData = _context.T_Transaksi.Where(x => x.Id == Id).FirstOrDefault();
            DateTime date = DateTime.Now;

            if (NamaEbook != "" || Kelompok != "")
            {
                getData.Nama = NamaEbook;
                getData.Kelompok = Kelompok;
                getData.TanggalSampul = TanggalSampul;
                getData.UpdatedDate = date;
                _context.Entry(getData).State = EntityState.Modified;
                _context.SaveChanges();
                result = true;
            }




            return new JsonResult(result);

        }

        #endregion PROSES

    }
}
