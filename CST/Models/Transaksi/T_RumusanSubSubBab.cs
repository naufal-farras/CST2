using System;

namespace CST.Models.Master
{
    public class T_RumusanSubSubBab
    {
        public int Id { get; set; } 

        public M_SubSubMenu SubSubBab { get; set; } 
        public int SubSubBabId { get; set; }

        public T_RumusanSubBab RumusanSubBab { get; set; }
        public int RumusanSubBabId { get; set; }

    }
}
