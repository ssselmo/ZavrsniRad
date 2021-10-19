using System;
using System.Collections.Generic;
using System.Text;

namespace DonorCentar.Model.Requests
{
    public class ObavijestInsertRequest
    {
        public string Naslov { get; set; }
        public string Sadrzaj { get; set; }

        public DateTime Vrijeme { get; set; }
    }
}
