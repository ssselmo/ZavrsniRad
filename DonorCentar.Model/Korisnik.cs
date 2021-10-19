using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DonorCentar.Model
{
    public class Korisnik
    {
        public int Id { get; set; }
        public LoginPodaci LoginPodaci { get; set; }
        public int LoginPodaciId { get; set; }
        public LicniPodaci LicniPodaci { get; set; }
        public int LicniPodaciId { get; set; }
        public Grad Grad { get; set; }
        public int GradId { get; set; }
        public TipKorisnika TipKorisnika { get; set; }
        public int TipKorisnikaId { get; set; }
        public bool Izbrisan { get; set; }


        public string Ime => LicniPodaci?.Ime;
        public string Prezime => LicniPodaci?.Prezime;
        public string Email => LicniPodaci?.Email;
        public string GradNaziv => Grad?.Naziv;

        public string Tip => TipKorisnika?.Tip;

    }
}
