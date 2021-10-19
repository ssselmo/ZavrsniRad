using System;
using System.Collections.Generic;
using System.Text;


namespace DonorCentar.Model
{
    public class Donor
    {
        public int Id { get; set; }
        public Korisnik Korisnik { get; set; }
        public int KorisnikId { get; set; }
        public DateTime DatumRegistracije { get; set; }
    }
}
