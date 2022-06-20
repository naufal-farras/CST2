using System;

namespace CST.Models.Master
{
    public class T_RumusanNasabah
    {
        public int Id { get; set; } 

        public string Nama { get; set; }
        public M_Nasabah Nasabah { get; set; } 
        public int NasabahId { get; set; }

        public int Index { get; set; }

        public DateTime CreatedDate { get; set; }
        public Nullable<DateTime> DeletedDate { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }


    }
}
