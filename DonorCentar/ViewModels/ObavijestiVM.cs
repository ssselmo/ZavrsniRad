using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.ViewModels
{
    public class ObavijestiVM
    {
        public List<Row> rows { get; set; }
        public class Row
        {
           
            public int ObavijestId { get; set; }
            public string Sadrzaj { get; set; }
            public string Naslov { get; set; }
            public DateTime Vrijeme { get; set; }
          
        }
    }
}
