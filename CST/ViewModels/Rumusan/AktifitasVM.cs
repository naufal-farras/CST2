using System.Collections.Generic;

namespace CST.ViewModels.Rumusan
{
     
    public class AktifitasVM
    {
        public int AktifitasId { get; set; }
        public string NamaBab { get; set; }
        public List<SubAktifitas> SubAktifitas { get; set; }
    }

    public class SubAktifitas
    {
        public int SubId { get; set; }
        public string NamaSubBab { get; set; }

        public List<SubSubAktifitas> SubSubAktifitas { get; set; }

    }

    public class SubSubAktifitas
    {
        public int SubSubId { get; set; }
    }
}
