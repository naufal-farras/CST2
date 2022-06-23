using CST.Models.EBook;
using CST.Models.Master;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace CST.ViewModels.EBook
{
    public class InputDataVM
    {
        public string NamaEbook { get; set; }
        public DateTime? TanggalSampul { get; set; }
        public string Kelompok { get; set; }
        public List<T_TransDetail> T_TransDetail { get; set; }

        public IEnumerable<Babs> Babs { get; set; }

    }
    public class Babs
    {
        public string namaBab { get; set; }
        public int BabId { get; set; }
        public IEnumerable<SubBabs> SubBabs { get; set; }


    }
    public class SubBabs
    {
        public string namaSubBab { get; set; }

        public int SubBabId { get; set; }
        public IEnumerable<SubSubBabs> SubSubBabs { get; set; }


    }
    public class SubSubBabs
    {
        public string namaSubSubBab { get; set; }

        public int SubSubBabId { get; set; } 

    }
   
}
