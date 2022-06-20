using CST.Models.Master;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CST.ViewModels.EBook
{
    public class UploadVM
    {
        public string Nama { get; set; }
        public string Kelompok{ get; set; }
        public int RumusanId { get; set; } 

        public List<IFormFile> selectedUpload { get; set; }

    }
    public class UploadDetail
    {
        public IFormFile Path { get; set; }


    }
}
 