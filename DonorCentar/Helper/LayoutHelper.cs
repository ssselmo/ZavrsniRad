using DonorCentar.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.Helper
{
    public static class LayoutHelper
    {
        public static void PostaviViewBag(this Controller c, string active)
        {
            var db = c.HttpContext.RequestServices.GetService(typeof(BazaPodataka)) as BazaPodataka;
            var k = c.HttpContext.GetLogiraniKorisnik();
            
           
            c.ViewBag.brojObavijesti = db.Obavijest.Count();
            if(k.TipKorisnikaId==3)
                c.ViewBag.verifikovan = db.Primalac.Where(d => d.KorisnikId == k.Id).SingleOrDefault().Verifikovan;
            c.ViewBag.Active = active;
            
        }

        public static ActionResult RedirectToDashboard (this Controller c)
        {
            var korisnik = c.HttpContext.GetLogiraniKorisnik();

            if (korisnik != null)
            {
                if (korisnik.TipKorisnikaId == 4)
                {
                    return new RedirectToActionResult("Index", "Donor", new { });
                }
                else if (korisnik.TipKorisnikaId == 3)
                {
                    return new RedirectToActionResult("Index", "Primalac", new { });

                }
                else if (korisnik.TipKorisnikaId == 2)
                {
                    return new RedirectToActionResult("Index", "Partner", new { });
                }
                else
                {
                    return new RedirectToActionResult("Index", "Administrator", new { });
                }
            }
            else
            {
                c.TempData["error_poruka"] = "Nije pronađen korisnik.";
                return new ViewResult() { ViewName = "Index" };
            }
        }
    }

    
}
