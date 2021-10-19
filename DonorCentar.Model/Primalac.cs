using System;
using System.Collections.Generic;
using System.Text;


namespace DonorCentar.Model
{
    public class Primalac
    {

        public int Id { get; set; }
        public Korisnik Korisnik { get; set; }
        public int KorisnikId { get; set; }
        public DateTime DatumRegistracije { get; set; }
        public bool Verifikovan { get; set; }
        public byte[] DokumentVerifikacije { get; set; }

        public string Ime => Korisnik?.Ime;
        public string Prezime => Korisnik?.Prezime;
        public string Email => Korisnik?.Email;
        public string GradNaziv => Korisnik?.GradNaziv;
        public string Tip => Korisnik?.Tip;

        public override string ToString()
        {
            return Ime +" "+ Prezime; 
        }
    }
}
