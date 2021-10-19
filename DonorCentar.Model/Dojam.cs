using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.Model
{
    public class Dojam
    {
        public int Id { get; set; }
        public string VrstaDojma { get; set; }

        public override string ToString()
        {
            return VrstaDojma;
        }
    }
}
