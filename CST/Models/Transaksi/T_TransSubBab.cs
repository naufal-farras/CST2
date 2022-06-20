using System;

namespace CST.Models.Master
{
    public class T_TransSubBab
    {
        public int Id { get; set; } 

        public T_TransBab TransBab { get; set; }
        public int TransBabId { get; set; }
        public M_SubMenu SubBab { get; set; }
        public int SubBabId { get; set; }


    }
}
