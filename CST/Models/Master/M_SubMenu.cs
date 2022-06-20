namespace CST.Models.Master
{
    public class M_SubMenu
    {
        public int Id { get; set; }   
        public string Nama { get; set; }
        public M_Nasabah Nasabah { get; set; }
        public int? NasabahId { get; set; }
    }
}
