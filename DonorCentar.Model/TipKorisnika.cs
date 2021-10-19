using System;
using System.Collections.Generic;
using System.Text;

namespace DonorCentar.Model
{
    public class TipKorisnika
    {
        public int Id { get; set; }
        public string Tip { get; set; }

        public override string ToString()
        {
            return Tip.ToString();
        }
    }
}
