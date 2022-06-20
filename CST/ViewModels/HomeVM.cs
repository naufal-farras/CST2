using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CST.ViewModels
{
    public class HomeVM
    { 
        public List<string> NamaNasabah{ get; set; }
        public List<int> JumlahEbook { get; set; } 

    }
}
