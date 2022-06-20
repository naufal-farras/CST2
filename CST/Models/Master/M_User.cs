using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CST.Models.Master
{
    public class M_User : IdentityUser
    {        
        public string Nama { get; set; }       
        public string NPP { get; set; }
        public M_Unit Unit { get; set; }
        public Nullable<int> UnitId { get; set; }

        public M_Jabatan Jabatan { get; set; }
        public int JabatanId { get; set; }
        public StatusPegawai StatusPegawai { get; set; }

        public DateTime CreatedDate { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }
        public Nullable<DateTime> DeletedDate { get; set; }
        public Nullable<DateTime> LastLogin { get; set; }
        public bool IsDelete { get; internal set; }
    }
    public enum StatusPegawai
    {
        PGS,
        Definitif
    }
}
