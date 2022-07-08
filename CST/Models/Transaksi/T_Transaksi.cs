using System;

namespace CST.Models.Master
{
    public class T_Transaksi
    {
        public int Id { get; set; }  
        public string Nama { get; set; }
        public string Kelompok { get; set; }
        public int Status { get; set; } 
        public string CreaterId { get; set; }
        public bool IsDelete { get; set; }

        public DateTime CreatedDate { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }

        public T_RumusanNasabah RumusanNasabah { get; set; }
        public int? RumusanNasabahId { get; set; }
        public DateTime? TanggalSampul { get; set; }


    }
}
