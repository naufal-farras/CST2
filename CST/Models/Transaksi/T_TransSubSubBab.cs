using System;

namespace CST.Models.Master
{
    public class T_TransSubSubBab
    {
        public int Id { get; set; } 

        public T_TransSubBab TransSubBab { get; set; }
        public int TransSubBabId { get; set; }
        public M_SubSubMenu SubSubBab { get; set; }
        public int SubSubBabId { get; set; }

        public string Path { get; set; }

        public DateTime CreatedDate { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }
        public Nullable<DateTime> DeletedDate { get; set; }
        public bool IsDelete { get; set; }


    }
}
