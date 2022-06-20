using System;

namespace CST.Models.Master
{
    public class T_TransDetail
    {
        public int Id { get; set; }

        public T_Transaksi Transaksi { get; set; }
        public int TransaksiId { get; set; }
        public int Index { get; set; }
        public string Path { get; set; }

        //public string CreaterId { get; set; }
        //public M_Bab Babs { get; set; }
        //public int BabsId { get; set; }

        //public M_SubMenu SubBabs { get; set; }
        //public int SubBabsId { get; set; }
        //public M_SubSubMenu SubSubBabs { get; set; }
        //public int SubSubBabsId { get; set; }


    }
}
