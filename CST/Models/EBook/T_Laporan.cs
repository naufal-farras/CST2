using CST.Models.Master;
using System;
using System.ComponentModel.DataAnnotations;

namespace CST.Models.EBook
{
    public class T_Laporan
    {
        [Key]
        public int Id { get; set; }
        public string NamaNasabah { get; set; }
        public string File { get; set; }
        public string InputerId { get; set; }
        public M_User Inputer { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }
        public bool isDelete { get; set; }
    }
}
