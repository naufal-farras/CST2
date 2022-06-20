using CST.Models.Master;
using System.Collections.Generic;

namespace CST.ViewModels.Rumusan
{
    public class RumusVM
    { 
 

        public List<M_Nasabah> M_Nasabah { get; set; }
        public List<M_Bab> M_Bab { get; set; }
        public List<M_SubMenu> M_SubBab { get; set; }
        public List<M_SubSubMenu> M_SubSubBab { get; set; }

        public List<T_RumusanNasabah> T_RumusanNasabah { get; set; }
        public List<T_RumusanNasabah> rumusanNasabah { get; set; }
        public List<T_RumusanBab> T_RumusanBab { get; set; }
        public List<T_RumusanSubBab> T_RumusanSubBab { get; set; }
        public List<T_RumusanSubSubBab> T_RumusanSubSubBab { get; set; }



    }



}
