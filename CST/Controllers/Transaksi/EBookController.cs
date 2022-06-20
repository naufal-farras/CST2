﻿using CST.Data;
using CST.Models.EBook;
using CST.Models.Master;
using CST.Repository;
using CST.ViewModels.EBook;
using CST.ViewModels.HelperVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIORenderer;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Parsing;
//using Syncfusion.Pdf.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;  
using System.Drawing.Imaging;
using CST.Models;
using Syncfusion.Pdf.Interactive;
//using Syncfusion.EJ2.PdfViewer;

namespace CST.Controllers.Transaksi
{
    public class EBookController : Controller
    {

        //private readonly IWebHostEnvironment _webHostEnvironment;
        //private readonly UserManager<M_User> _userManager;
        //private readonly M_User_Repository _userRepository;
        //private readonly T_Laporan_Repository _laporanRepository;
        private readonly T_EBook_Repository _ebook_Repository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _context;
        private readonly T_Laporan_Repository _laporan_Repository;
       

        //public EBookController(IWebHostEnvironment webHostEnvironment, UserManager<M_User> userManager,
        //    M_User_Repository userRepository, T_Laporan_Repository laporanRepository)
        //{
        //    _webHostEnvironment = webHostEnvironment;
        //    _userManager = userManager;
        //    _userRepository = userRepository;
        //    _laporanRepository = laporanRepository;
        //}
        public EBookController(AppDbContext context,
            IWebHostEnvironment webHostEnvironment,
            T_EBook_Repository ebook_Repository,
            T_Laporan_Repository laporan_Repository)
        {
            _webHostEnvironment = webHostEnvironment;
            _ebook_Repository = ebook_Repository;
            _laporan_Repository = laporan_Repository;
            _context = context;
        }

        //public void UserCookieCheck()
        //{
        //    string currentUserId = _userManager.GetUserId(User) ?? null;
        //    if (currentUserId == null)
        //    {
        //        Redirect("/Account/Login");
        //    }
        //}

        #region VIEW
        //[Authorize(Roles = "Inputer,Admin")]
        [AllowAnonymous]
        //[HttpGet("EBook/Index")]
        //public IActionResult Index()
        //{
        //    //string currentUserNPP = _userManager.GetUserName(User) ?? null;
        //    //var currentUser = _userRepository.GetByNPP(currentUserNPP);
        //    //ViewBag.CurrentUser = currentUser;
        //    return View("~/Views/EBook/Index.cshtml");
        //}
        public IActionResult Index()
        {
            var result = _ebook_Repository.GetDataNasabah();
            return View("Index", result);
        }

        [AllowAnonymous] 
        [HttpGet("EBook/GetTable/{id}")] 
        public JsonResult GetTable(int id)
        {
            var result = _ebook_Repository.GetTable(id);
            return Json(new { data = result });
        }

        public IActionResult GabungFile()
        {
            return View();
        }

        [HttpDelete("Ebook/Delete")]
        public JsonResult Delete(int id)
        {
            var result = _ebook_Repository.DeleteEbook(id);

            return Json(result);
        }

        [HttpGet("EBook/DetailTambah/{Id}")] 
        public IActionResult DetailTambah(int Id)
        {
            var result = _ebook_Repository.GetTemplate(Id);
            return View("DetailTambah", result);
        }
     
        [HttpGet("EBook/EditEbook/{Id}")]
        public IActionResult EditEbook(int Id)
        {
            var result = _ebook_Repository.GetTemplateEdit(Id);
            return View("EditEbook", result);
        }

        [HttpPost("EBook/UpdateJudul")]
        public JsonResult UpdateJudul(int Id, string NamaEbook, string Kelompok)
        {
            var result = _laporan_Repository.UpdateJudul(Id, NamaEbook,Kelompok);
            return Json(new { data = result });

        }
        //[HttpGet("EBook/DetailTambahs")]
        //public InputDataVM DetailTambahs()
        //{
        //    InputDataVM aa = new InputDataVM();
        //     aa = _ebook_Repository.GetTemplateEditTES();
        //    return aa ;
        //}

        //[HttpGet("EBook/GetRumusan/{Id}")] 
        //public JsonResult GetRumusan(int Id)
        //{
        //    var result = _ebook_Repository.GetTemplate(Id);
        //    return View("DetailTambah", result);
        //}

        public IActionResult TambahEbook()
        {
            //string currentUserNPP = _userManager.GetUserName(User) ?? null;
            //var currentUser = _userRepository.GetByNPP(currentUserNPP);
            //ViewBag.CurrentUser = currentUser;
            var result = _ebook_Repository.GetRumusan();

            return View(result);
        }
        #endregion
         
        #region PROSES
        //[HttpPost("EBook/Upload")]
        //public JsonResult Upload(InputDataVM data)
        //{
        //    //string currentUserId = _userManager.GetUserId(User) ?? null;

        //    StatusHelperVM result = new StatusHelperVM();
        //    List<string> formatFile = new List<string>() { ".pdf" };
        //    if (data.DataFile != null)
        //    {
        //        FileInfo fileInfo = new FileInfo(data.DataFile.FileName);
        //        string ekstensiFile = fileInfo.Extension;
        //        if (formatFile.Contains(fileInfo.Extension.ToLower()) == false)
        //        {
        //            result.StatusCategory = StatusCategory.Failed;
        //            result.Message = "Harap upload File dengan format pdf!!!";
        //        }
        //        else if (data.File.Length > 512000)
        //        {
        //            result.StatusCategory = StatusCategory.Failed;
        //            result.Message = "Maksimal file yang diupload berukuran 500Kb!";
        //        }
        //        else
        //        {
        //            string generateNameFile = "T" + DateTime.Now.ToString("ddMMyyHHmmss") + "NR" + data.DataFile + ekstensiFile;

        //            #region INSERT TO DATABASE
        //            T_Laporan laporan = new T_Laporan();
        //            laporan.File = generateNameFile;
        //            laporan.NamaNasabah = data.NamaNasabah;
        //            laporan.InputerId = currentUserId;

        //            var insertData = _laporanRepository.Create(laporan);
        //            if (insertData.StatusCategory == StatusCategory.Success)
        //            {
        //                #region SAVE FILE SO
        //                string webRootPath = _webHostEnvironment.WebRootPath;
        //                string path = Path.Combine(webRootPath, "File");

        //                FileStream savingFile = new FileStream(Path.Combine(path, generateNameFile), FileMode.Create);
        //                data.DataFile.CopyTo(savingFile);
        //                savingFile.Close();
        //                #endregion

        //                result.StatusCategory = insertData.StatusCategory;
        //                result.Message = insertData.Message;
        //            }
        //            else
        //            {
        //                result.StatusCategory = insertData.StatusCategory;
        //                result.Message = insertData.Message;
        //            }
        //            #endregion                   
        //        }
        //    }
        //    else
        //    {
        //        result.StatusCategory = StatusCategory.Failed;
        //        result.Message = "Tidak ada File yang diupload!!!";
        //    }

        //    return Json(result);
        //}
        [AllowAnonymous]
        //[HttpPost("EBook/GabungFile")]
        public ActionResult GabungFilePDF(List<IFormFile> selectedUpload)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            string path = Path.Combine(webRootPath, "Files/DataGabung");
            System.IO.DirectoryInfo di = new DirectoryInfo(path);
         
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (var item2 in selectedUpload)
            {

                string generateNameFile = item2.FileName;
                FileStream savingFile = new FileStream(Path.Combine(path, generateNameFile), FileMode.Create);
                item2.CopyTo(savingFile);
                savingFile.Close();
            }

            return Ok(); 
        }

        public ActionResult GenerateFilePDF()
        {
              
            string webRootPath = _webHostEnvironment.WebRootPath;
            string path = Path.Combine(webRootPath, "Files/DataGabung");
            //Get the folder path into DirectoryInfo
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            //Get the PDF files in folder path into FileInfo
            FileInfo[] files = directoryInfo.GetFiles("*.pdf");

            //Create a new PDF document 
            PdfDocument document = new PdfDocument();

            //Set enable memory optimization as true 
            document.EnableMemoryOptimization = true;

            foreach (FileInfo file in files)
            {
                //Load the PDF document 
                FileStream fileStream = new FileStream(file.FullName, FileMode.Open);
                PdfLoadedDocument loadedDocument = new PdfLoadedDocument(fileStream);

                //Merge PDF file
                PdfDocumentBase.Merge(document, loadedDocument);

                //Close the existing PDF document 
                loadedDocument.Close(true);
            }

            //Save the PDF document
            //document.Save("MergedPDF.pdf");

            //Close the instance of PdfDocument
            //Save the document into stream

            MemoryStream stream = new MemoryStream();

            document.Save(stream);
            document.Close(true);

            stream.Position = 0; 

            string contentType = "application/pdf";

            //Define the file name

            string fileName = "DataGabung-" + DateTime.Now + ".pdf";

            //Creates a FileContentResult object by using the file contents, content type, and file name

            return File(stream, contentType, fileName);
  
        }

        [HttpPost("EBook/Upload")]
        public JsonResult Upload(List<IFormFile> selectedUpload, string Nama, string Kelompok, int RumusanId)
        {
            var result = _laporan_Repository.TambahEbook(selectedUpload, Nama, Kelompok, RumusanId);
            return Json(new { data = result });
        }
      
        [HttpPost("EBook/UpdateFileEbook")]
        public JsonResult UpdateFileEbook(List<IFormFile> selectedUpload, int Index, int RumusanId)
        {
            var result = _laporan_Repository.UpdateFileEbook(selectedUpload, Index, RumusanId);
            return Json(new { data = result });
        }
        [HttpGet("EBook/Preview/{Id}")]
        public ActionResult Preview(int Id)
        {
            var getPath = _context.T_TransDetail.Where(x => x.Id == Id).SingleOrDefault().Path;
            string webRootPath = _webHostEnvironment.WebRootPath;
            string path = Path.Combine(webRootPath, "Files/Data/");

            if (getPath == "PDFkosong.pdf")
            {
                 path = Path.Combine(webRootPath, "Files/Data/datakosong/");

            }
            else
            {
                 path = Path.Combine(webRootPath, "Files/Data/");

            }

            var fileStream = new FileStream(path + getPath,
                                     FileMode.Open,
                                     FileAccess.Read
                                   );
            var fsResult = new FileStreamResult(fileStream, "application/pdf");
            return fsResult;
        }

        PdfDocument finalDoc;
        PdfFont _font;
        [HttpGet("EBook/GenerateEbook/{Id}")]
        public ActionResult GenerateEbook(int Id)
        {
            finalDoc = new PdfDocument();
            InputDataVM getTemplate = new InputDataVM();

            var getIdrumusan = _context.T_Transaksi.Where(x => x.Id == Id).SingleOrDefault().RumusanNasabahId;
            getTemplate = _ebook_Repository.GetTemplatePDF(getIdrumusan);
            var getTrans = _context.T_Transaksi.Where(x => x.Id == Id).SingleOrDefault().Id;
            var getSampul = _context.T_Transaksi.Where(x => x.Id == Id).FirstOrDefault();
            var getData = _context.T_TransDetail.Where(x => x.TransaksiId == getTrans).OrderBy(x => x.Index).ToList();


            string webRootPath = _webHostEnvironment.WebRootPath;
            string path = Path.Combine(webRootPath, "Files/Data");

            //Loads a template document
            string webRootPaths= _webHostEnvironment.WebRootPath;
            string paths = Path.Combine(webRootPaths, "Files");

            FileStream fileStreamPaths = new FileStream(Path.Combine(paths, "SampulBPJS.docx"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            WordDocument document = new WordDocument(fileStreamPaths, FormatType.Docx);
            //document.ReplaceFirst = true;

            document.Replace("%%NamaSampul%%", getSampul.Nama, false, true);
            document.Replace("%%Kelompok%%", getSampul.Kelompok, false, true);
            document.Replace("%%Tanggal%%", DateTime.Now.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID")), false, true);

            var date = DateTime.Now.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));
           
            //Word Tdk di Pake
            #region
            //Creates a PDF document  
            //finalDoc.EnableMemoryOptimization = true;

            //IWSection section = document.AddSection();
            ////string paraText = "AdventureWorks Cycles, the fictitious company on which the AdventureWorks sample databases are based, is a large, multinational manufacturing company.";
            ////Adds the paragraph into the created section

            //IWParagraph paragraph = section.AddParagraph();
            //Appends the TOC field with LowerHeadingLevel and UpperHeadingLevel to determines the TOC entries
            //paragraph.AppendTOC(1, 3);
            //var nobab = 1;
            //foreach (var item1 in getTemplate.Babs)
            //{ 
            //    //Adds the section into the Word document
            //    section = document.AddSection();
            //    //Adds the paragraph into the created section
            //    paragraph = section.AddParagraph();
            //    //Adds the text for the headings
            //    paragraph.AppendText("BAB " + nobab + " " + item1.namaBab);
            //    //Sets a built-in heading style.
            //    paragraph.ApplyStyle(BuiltinStyle.Heading1);

            //    //Adds the text into the paragraph
            //    //section.AddParagraph().AppendText(paraText);
            //    var nosubbab = 1;

            //    foreach (var item2 in item1.SubBabs)
            //    {
            //        //Adds the section into the Word document
            //        section = document.AddSection();
            //        //Adds the paragraph into the created section
            //        paragraph = section.AddParagraph();
            //        //Adds the text for the headings
            //        paragraph.AppendText(nosubbab + ". " + item2.namaSubBab);
            //        //Sets a built-in heading style.
            //        paragraph.ApplyStyle(BuiltinStyle.Heading2);

            //        //Adds the text into the paragraph
            //        //section.AddParagraph().AppendText(paraText);
            //        var nosubsubbab = 1;

            //        foreach (var item3 in item2.SubSubBabs)
            //        {

            //            //Adds the section into the Word document
            //            section = document.AddSection();
            //            //Adds the paragraph into the created section
            //            paragraph = section.AddParagraph();
            //            //Adds the text into the headings
            //            paragraph.AppendText(nosubbab + "." + nosubsubbab + " " + item3.namaSubSubBab);
            //            //Sets a built-in heading style
            //            paragraph.ApplyStyle(BuiltinStyle.Heading3);

            //            //Adds the text into the paragraph.
            //            //section.AddParagraph().AppendText(paraText);
            //            var indxx = 0;

            //            foreach (var items4 in getData)
            //            {
            //                if (items4.Index == indxx)
            //                {

            //                    //FileStream stream1 = new FileStream(Path.Combine(path, items4.Path), FileMode.Open, FileAccess.Read);
            //                    //PdfLoadedDocument loadedDocument = new PdfLoadedDocument(stream1);
            //                    //PdfDocumentBase.Merge(finalDoc, loadedDocument);
            //                    //loadedDocument.Close(true);

            //                }
            //                continue;

            //            }
            //            indxx++;

            //            //Updates the table of contents
            //            nosubsubbab++;
            //            document.UpdateTableOfContents();
            //        }
            //        nosubbab++;
            //    }
            //    nobab++;
            //}


            #endregion

            ////Instantiation of DocIORenderer for Word to PDF conversion
            DocIORenderer render = new DocIORenderer(); 
            //Sets true to embed TrueType fonts
            render.Settings.EmbedFonts = true;  
            finalDoc = render.ConvertToPDF(document);  
            ////Creates a PDF document  
            finalDoc.EnableMemoryOptimization = true;

            
            //Add a new PDF page 
            PdfPage pageTOC = finalDoc.Pages.Add();
            //Set font for TOC and bookmark contents
            _font = new PdfStandardFont(PdfFontFamily.Helvetica, 13f);
            PdfFont fontBAB = new PdfStandardFont(PdfFontFamily.Helvetica, 20f, PdfFontStyle.Underline|PdfFontStyle.Bold);
            //Create a new instance of string format to set text layout information
            finalDoc.PageSettings.Margins.Bottom= 20;
            //PdfPageTemplateElement header = new PdfPageTemplateElement(30, 0, 75, 30);
            //finalDoc.Template.Top = header;
            //PdfPageTemplateElement footer = new PdfPageTemplateElement(10, 0, 50, 10);
            //finalDoc.Template.Bottom = footer;
            PdfStringFormat format = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
            pageTOC.Graphics.DrawString("DAFTAR ISI", fontBAB, PdfBrushes.Black, new RectangleF(new PointF(5, 10), new SizeF(pageTOC.Graphics.ClientSize.Width, 30)), format);

            PdfPage pageTOC2 = finalDoc.Pages.Add();  
            PdfPage pageTOC3 = finalDoc.Pages.Add();  
            PdfPage pageTOC4 = finalDoc.Pages.Add();   


            var indx = 0;
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument();

            var x = 15; 
            var y = 40; 
            var x2 = 51; 
            var x3 = 63;

            var xB = 15;
            var yB = 40;
            var x2B = 51;
            var x3B = 63;

            var xC = 15;
            var yC = 40;
            var x2C = 51;
            var x3C = 63;

            var xD = 15;
            var yD = 40;
            var x2D = 51;
            var x3D = 63;

            var nobab = 1;

            foreach (var items in getTemplate.Babs)
            {
                finalDoc.PageSettings.Orientation = PdfPageOrientation.Portrait;
                finalDoc.PageSettings.Size = PdfPageSize.A4;
                PdfPage page = finalDoc.Pages.Add();
                finalDoc.PageSettings.Orientation = PdfPageOrientation.Portrait; 
                finalDoc.PageSettings.Size = PdfPageSize.A4;
                PdfGraphics graphics = page.Graphics;
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica,30, PdfFontStyle.Bold );
                graphics.DrawString(items.namaBab, font, PdfBrushes.Black, new PointF(275, 410), format);

                //AddBookmark(finalDoc.Pages[finalDoc.Pages.Count - 1], pageTOC, "BAB " + nobab + " " + items.namaBab, new PointF(x, y));
                //y += 23;


                if (pageTOC.Annotations.Count >= 32 && pageTOC2.Annotations.Count < 33)
                { 
                    AddBookmark(finalDoc.Pages[finalDoc.Pages.Count - 1], pageTOC2, "BAB " + nobab + " " + items.namaBab, new PointF(xB, yB));

                    yB += 23;

                }
                else if (pageTOC2.Annotations.Count >= 32 && pageTOC3.Annotations.Count < 33)
                {
                     
                    AddBookmark(finalDoc.Pages[finalDoc.Pages.Count - 1], pageTOC3, "BAB " + nobab + " " + items.namaBab, new PointF(xC, yC));

                    yC += 23;

                }
                //else if (pageTOC2.Annotations.Count > 32 && pageTOC2.Annotations.Count < 64)
                //{
                 
                //    AddBookmark(finalDoc.Pages[finalDoc.Pages.Count - 1], pageTOC4, "BAB " + nobab + " " + items.namaBab, new PointF(xD, yD)); 
                //    yD += 23;

                //}
                else if(pageTOC.Annotations.Count < 33)
                {
                    AddBookmark(finalDoc.Pages[finalDoc.Pages.Count - 1], pageTOC, "BAB " + nobab + " " + items.namaBab, new PointF(x, y));
                    y += 23;


                }

                //Set the font.
                PdfFont fonts3 = new PdfStandardFont(PdfFontFamily.Helvetica, 12f);

                //Create page number field.
                PdfPageNumberField pageNumber = new PdfPageNumberField(fonts3, PdfBrushes.Black);

                //Create page count field.
                PdfPageCountField count = new PdfPageCountField(fonts3, PdfBrushes.Black);

                //Add the fields in composite fields.
                PdfCompositeField compositeField = new PdfCompositeField(fonts3, PdfBrushes.Black, "Page {0} of {1}", pageNumber, count);

                for (int i = 1; i < finalDoc.Pages.Count; i++)
                {
                    //Draw the composite field.
                    compositeField.Draw(finalDoc.Pages[i].Graphics, new PointF(finalDoc.Pages[i].Size.Width / 2 - 20, finalDoc.Pages[i].Size.Height - 20));
                }
                 
                var subnobab = 1;

                foreach (var items2 in items.SubBabs)
                {
                    finalDoc.PageSettings.Orientation = PdfPageOrientation.Portrait;
                    finalDoc.PageSettings.Size = PdfPageSize.A4;
                    PdfPage pages = finalDoc.Pages.Add();
                    finalDoc.PageSettings.Orientation = PdfPageOrientation.Portrait;
                    finalDoc.PageSettings.Size = PdfPageSize.A4;

                    PdfGraphics graphicss = pages.Graphics;
                    PdfFont fonts = new PdfStandardFont(PdfFontFamily.Helvetica, 25, PdfFontStyle.Bold);
                    graphicss.DrawString(items2.namaSubBab, fonts, PdfBrushes.Black, new PointF(270, 410), format);

                    //AddBookmark(finalDoc.Pages[finalDoc.Pages.Count - 1], pageTOC, subnobab + ". " + items2.namaSubBab, new PointF(x2, y));
                    //y += 23;

                    if (pageTOC.Annotations.Count >= 32 && pageTOC2.Annotations.Count < 33)
                    {
                        
                        AddBookmark(finalDoc.Pages[finalDoc.Pages.Count - 1], pageTOC2, subnobab + ". " + items2.namaSubBab, new PointF(x2B, yB));

                        yB += 23;

                    }
                    else if (pageTOC2.Annotations.Count >= 32 && pageTOC3.Annotations.Count < 33)
                    {
                        //y = 40; 
                        AddBookmark(finalDoc.Pages[finalDoc.Pages.Count - 1], pageTOC3, subnobab + ". " + items2.namaSubBab, new PointF(x2C, yC));

                        yC += 23;
                    }
                    //else if (pageTOC3.Annotations.Count > 33 && pageTOC3.Annotations.Count < 64)
                    //{
                    //    //y = 40;

                    //    AddBookmark(finalDoc.Pages[finalDoc.Pages.Count - 1], pageTOC4, subnobab + ". " + items2.namaSubBab, new PointF(x2D, yD));

                    //    yD += 23;
                    //}
                    else if (pageTOC.Annotations.Count < 33)
                    {
                        AddBookmark(finalDoc.Pages[finalDoc.Pages.Count - 1], pageTOC, subnobab + ". " + items2.namaSubBab, new PointF(x2, y));

                        y += 23;

                    }
                    
                    for (int i = 1; i < finalDoc.Pages.Count; i++)
                    {
                        //Draw the composite field.
                        compositeField.Draw(finalDoc.Pages[i].Graphics, new PointF(finalDoc.Pages[i].Size.Width / 2 - 20, finalDoc.Pages[i].Size.Height - 20));
                    }
                    var subsubnobab = 1;

                    foreach (var items3 in items2.SubSubBabs)
                    {
                        finalDoc.PageSettings.Orientation = PdfPageOrientation.Portrait;
                        finalDoc.PageSettings.Size = PdfPageSize.A4;
                        PdfPage pagess = finalDoc.Pages.Add(); 
                        finalDoc.PageSettings.Orientation = PdfPageOrientation.Portrait;
                        finalDoc.PageSettings.Size = PdfPageSize.A4;

                        PdfGraphics graphicsss = pagess.Graphics;
                        PdfFont fontss = new PdfStandardFont(PdfFontFamily.Helvetica, 25, PdfFontStyle.Bold);
                        graphicsss.DrawString(items3.namaSubSubBab, fontss, PdfBrushes.Black, new PointF(270, 410), format);

                        //y += 23;

                        if (pageTOC.Annotations.Count >= 32 && pageTOC2.Annotations.Count < 33)
                        {
                            //y = 40;

                            AddBookmark(finalDoc.Pages[finalDoc.Pages.Count - 1], pageTOC2, subnobab + "." + subsubnobab + " " + items3.namaSubSubBab, new PointF(x3B, yB));

                            yB += 23;

                        }
                        else if (pageTOC2.Annotations.Count >= 32 && pageTOC3.Annotations.Count < 33)
                        {
                            //y = 40;

                            AddBookmark(finalDoc.Pages[finalDoc.Pages.Count - 1], pageTOC3, subnobab + "." + subsubnobab + " " + items3.namaSubSubBab, new PointF(x3C, yC));

                            yC += 23;
                        }
                        //else if (pageTOC3.Annotations.Count > 32 && pageTOC3.Annotations.Count < 64)
                        //{

                        //    AddBookmark(finalDoc.Pages[finalDoc.Pages.Count - 1], pageTOC4, subnobab + "." + subsubnobab + " " + items3.namaSubSubBab, new PointF(x3D, yD));

                        //    yD += 23;
                        //}
                        else if(pageTOC.Annotations.Count < 33)
                        {
                            AddBookmark(finalDoc.Pages[finalDoc.Pages.Count - 1], pageTOC, subnobab + "." + subsubnobab + " " + items3.namaSubSubBab, new PointF(x3, y));

                            y += 23;

                        }


                        //for (int i = 1; i < finalDoc.Pages.Count; i++)
                        //{
                        //    //Draw the composite field.
                        //    compositeField.Draw(finalDoc.Pages[i].Graphics, new PointF(finalDoc.Pages[i].Size.Width / 2 - 20, finalDoc.Pages[i].Size.Height - 20));
                        //}
                        foreach (var items4 in getData)
                        {
                            if (items4.Index == indx)
                            {
                                if (items4.Path == "PDFkosong.pdf")
                                {
                                    FileStream stream1 =null; 
                                     
                                }
                                else
                                {
                                    FileStream stream1 = new FileStream(Path.Combine(path, items4.Path), FileMode.Open, FileAccess.Read);
                                    loadedDocument = new PdfLoadedDocument(stream1);
                                    PdfDocumentBase.Merge(finalDoc, loadedDocument);
                                }
                               
                            }
                            continue;

                        }
                        indx++;
                        subsubnobab++;
                    }
                    subnobab++;
                }
                nobab++;
            }
  
            //Save the document into stream 
            MemoryStream stream = new MemoryStream(); 
            finalDoc.Save(stream);
            stream.Position = 0; 
            finalDoc.Close(true); 

            string contentType = "application/pdf";  
            string fileName = "Laporan E-BOOK - "+getSampul.Nama+"-"+date+".pdf"; 
            return File(stream, contentType, fileName);

        }
        public void AddBookmark(PdfPage page, PdfPage toc, string content, PointF point)
        {

            PdfGraphics graphics = page.Graphics;
            //Add bookmark in PDF document
            PdfBookmark bookmarks = finalDoc.Bookmarks.Add(content);
            //Add table of contents
            AddTableOfContent(page, toc, content, point);
            //Adding bookmark with named destination
            PdfNamedDestination namedDestination = new PdfNamedDestination(content);
            namedDestination.Destination = new PdfDestination(page, new PointF(point.X, point.Y));
            namedDestination.Destination.Mode = PdfDestinationMode.FitToPage;
            finalDoc.NamedDestinationCollection.Add(namedDestination);
            bookmarks.NamedDestination = namedDestination;
        }

        //Add table of content with page number and document link annotations
        private void AddTableOfContent(PdfPage page, PdfPage toc, string content, PointF point)
        {
            //Draw title in TOC
            PdfTextElement element = new PdfTextElement(content, _font);
            //Set layout format for pagination of TOC
            PdfLayoutFormat format = new PdfLayoutFormat();
            format.Break = PdfLayoutBreakType.FitPage;
            format.Layout = PdfLayoutType.Paginate;
            PdfLayoutResult result = element.Draw(toc, point, format);
            //Draw page number in TOC
            PdfTextElement pageNumber = new PdfTextElement(finalDoc.Pages.Count.ToString(), _font, PdfBrushes.Black);
            pageNumber.Draw(toc, new PointF(toc.Graphics.ClientSize.Width - 40, point.Y));
            //Creates a new document link annotation
            RectangleF bounds = result.Bounds;
            bounds.Width = toc.Graphics.ClientSize.Width - point.X;
            PdfDocumentLinkAnnotation documentLinkAnnotation = new PdfDocumentLinkAnnotation(bounds);
            documentLinkAnnotation.AnnotationFlags = PdfAnnotationFlags.NoRotate;
            documentLinkAnnotation.Text = content;
            documentLinkAnnotation.Color = Color.Transparent;
            //Sets the destination
            documentLinkAnnotation.Destination = new PdfDestination(page);
            documentLinkAnnotation.Destination.Location = point;
            //Adds this annotation to a new page
            toc.Annotations.Add(documentLinkAnnotation);
        } 
        #endregion





    }
}
