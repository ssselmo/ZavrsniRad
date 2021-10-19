using System;
using System.Collections.Generic;
using System.Text;

namespace DonorCentar.Mobile.Models
{
    public class JedinicaMjere
    {
        public Model.JedinicaMjere jedinicaMjere { get; set; }

        public override string ToString()
        {
            return jedinicaMjere.ToString();
        }
    }
}
