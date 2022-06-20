using System;

namespace CST.Models.Master
{
    public class T_TransBab
    {
        public int Id { get; set; }

        //public T_TransDetail TransDetail { get; set; }
        //public int TransDetailId { get; set; }
        public T_Transaksi Transaksis { get; set; }
        public int TransaksisId { get; set; }
        public M_Bab Bab { get; set; }
        public int BabId { get; set; }


    }
}
