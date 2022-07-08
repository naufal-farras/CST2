using CST.Models.EBook;
using CST.Models.Master;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace CST.ViewModels.EBook
{
    public class TempFileVM
    {
        public int Index { get; set; }
        public IFormFile selectUploads { get; set; }

        //public List<FileVM> FileVM { get; set; }


    }
    public class TempFileVM2
    {
        public string Indexx { get; set; }
        //public IFormFile selectUploads { get; set; }



    }
    public class FinaleVM
    {
        public List<TempFileVM> data { get; set; }



    }


    public class FileVM
    {
        public IFormFile selectUploads { get; set; }


    }
    public class Temp2
    {
        public string Nama { get; set; }
        public DateTime TanggalSampul { get; set; }
        public string Kelompok { get; set; }
        public int RumusanId { get; set; }
    }

    public class FinalTempVM
    {
        public List<TempFileVM> tempFileVM { get; set; }
        public Temp2 Temp2 { get; set; }
    }

    //public class TempFileVM
    //{
    //    public int Index { get; set; }
    //    public IEnumerable<Upload> Upload { get; set; }

    //}
    //public class Upload
    //{
    //    public IFormFile selectUploads { get; set; }



    //}
}
