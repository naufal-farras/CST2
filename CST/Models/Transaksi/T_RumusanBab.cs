using System;

namespace CST.Models.Master
{
    public class T_RumusanBab
    {
        public int Id { get; set; } 

        public M_Bab Bab { get; set; } 
        public int BabId { get; set; }

        public T_RumusanNasabah RumusanNasabah{ get; set; } 
        public int RumusanNasabahId { get; set; }



    }
}
