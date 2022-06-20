using CST.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CST.ViewModels.Account
{
    public class UserDataVM
    {
        public string Id { get; set; }
        public string Nama { get; set; }
        public string NPP { get; set; }
        public string UserName { get; set; }        

        public M_Jabatan Jabatan { get; set; }
        public int JabatanId { get; set; }

        public M_Unit Unit { get; set; }
        public Nullable<int> UnitId { get; set; }
        public StatusPegawai StatusPegawai { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? LastLogin { get; set; }
        public IList<string> Role { get; set; }
    }
}
