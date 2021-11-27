using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.ViewModels
{
    public class SelectListNeverifikovaniPrimaociVM
    {
        public int KorisnikId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }


        public string Email { get; set; }
        public string BrojTelefona { get; set; }
        public string Adresa { get; set; }
        public string Grad { get; set; }
        public byte[] DokumentVerifikacije { get; internal set; }

      
    }
}
