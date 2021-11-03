using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.WebAPI.Database
{
    public class Obavijest
    {

        public int ObavijestId { get; set; }
        public string Naslov { get; set; }
        public string Sadrzaj { get; set; }
        public DateTime Vrijeme { get; set; }
        public Administrator Admin { get; set; }
        public int AdminId { get; set; }
    }
}
