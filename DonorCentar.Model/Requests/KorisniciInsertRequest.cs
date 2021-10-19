using System;
using System.Collections.Generic;
using System.Text;

namespace DonorCentar.Model.Requests
{
    public class KorisniciInsertRequest
    {
        public KorisniciInsertRequest()
        {
            LoginPodaci = new LoginPodaci();
            LicniPodaci = new LicniPodaci();
        }

        public LoginPodaci LoginPodaci { get; set; }
       
        public LicniPodaci LicniPodaci { get; set; }
       
       
        public int GradId { get; set; }
    
        public int TipKorisnikaId { get; set; }
    }
}
