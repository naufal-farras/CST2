using CST.Models.Master;
using System;
using System.Collections.Generic;

namespace CST.ViewModels.Account
{
    public class RegisterVM
    {
        public string Id_user { get; set; }
        public string Nama { get; set; }
        public string NPP { get; set; }
        public int JabatanId { get; set; }
        public Nullable<int> UnitId { get; set; }
        public StatusPegawai StatusPegawai { get; set; }
        public IList<string> UserRole { get; set; } 
    }
}
