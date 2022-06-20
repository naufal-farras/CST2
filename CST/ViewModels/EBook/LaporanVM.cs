using CST.Models.EBook;
using System;

namespace CST.ViewModels.EBook
{
    public class LaporanVM
    {
        public int Id { get; set; }
        public string NamaNasabah { get; set; }
        public string File { get; set; }
        public string InputerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }

        #region TABLE M_User
        public string Inputer { get; set; }
        public string NppInputer { get; set; }
        #endregion
    }
}
