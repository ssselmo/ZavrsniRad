using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.Model
{
    public class TipDonacije
    {
        public int TipDonacijeId { get; set; }
        public string Tip { get; set; }

        public override string ToString()
        {
            return Tip.ToString();
        }
    }
}
