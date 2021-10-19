using DonorCentar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.ViewModels
{
    public class UniqueNaziv : ValidationAttribute
    {
    //    private BazaPodataka db = new BazaPodataka();
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var db = (BazaPodataka)validationContext.GetService(typeof(BazaPodataka));

            var korisnik = (HomeRegistracijaVM)validationContext.ObjectInstance;

            var licniP = db.LicniPodaci.ToList();

            foreach (var lp in licniP)
            {
                if (korisnik.Ime == lp.Ime && korisnik.Prezime ==lp.Prezime)
                {
                    return new ValidationResult("Ime već postoji");
                }
            }
            return ValidationResult.Success;
        }
    }
}
