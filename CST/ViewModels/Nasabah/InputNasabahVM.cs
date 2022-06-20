using System;

namespace CST.ViewModels.Nasabah
{
    public class InputNasabahVM
    {
        public int Id { get; set; }
        public string KodeNasabah { get; set; }
        public string NamaNasabah { get; set; }
        public Nullable<int> SubMenuId { get; set; }
    }
}
