using System.Collections.Generic;

namespace CST.ViewModels.Rumusan
{
    public class BabVM
    {
        public int BabId { get; set; }
        public List<SubBab> SubBab { get; set; }
        public List<SubSubBab> SubSubBab { get; set; }


    }
    public class SubBab
    {
        public int SubBabId { get; set; }

    }
    public class SubSubBab
    {
        public int SubSubBabId { get; set; }
    }
}
