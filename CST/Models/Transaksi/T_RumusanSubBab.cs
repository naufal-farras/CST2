using System;

namespace CST.Models.Master
{
    public class T_RumusanSubBab
    {
        public int Id { get; set; } 

        public M_SubMenu SubBab { get; set; } 
        public int SubBabId { get; set; }

        public T_RumusanBab RumusanBab { get; set; }
        public int RumusanBabId { get; set; }



    }
}
