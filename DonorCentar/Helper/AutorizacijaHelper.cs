using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DonorCentar.Models;

namespace DonorCentar.Helper
{
    public class AutorizacijaAttribute : TypeFilterAttribute
    {
        public AutorizacijaAttribute(bool donor = false, bool primalac = false, bool partner = false, bool admin = false)
            : base(typeof(AutorizacijaHelper))
        {
            Arguments = new object[] { donor,primalac,partner,admin };
        }
    }


    public class AutorizacijaHelper : IAsyncActionFilter
    {
        private readonly bool donor;
        private readonly bool primalac;
        private readonly bool partner;
        private readonly bool admin;

        public AutorizacijaHelper(bool donor, bool primalac, bool partner, bool admin)
        {
            this.donor = donor;
            this.primalac = primalac;
            this.partner = partner;
            this.admin = admin;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            Korisnik k = filterContext.HttpContext.GetLogiraniKorisnik();

            if (k == null)
            {
                if (filterContext.Controller is Controller controller)
                {
                    controller.TempData["error_poruka"] = "Niste logirani";
                }

                filterContext.Result = new RedirectToActionResult("Index", "Home", new { @area = "" });
                return;
            }

           

            if (donor && k.TipKorisnikaId== 4)
            {
                await next(); //ok - ima pravo pristupa
                return;
            }
            if (primalac && k.TipKorisnikaId == 3)
            {
                await next(); //ok - ima pravo pristupa
                return;
            }
            if (partner && k.TipKorisnikaId == 2)
            {
                await next(); //ok - ima pravo pristupa
                return;
            }
            if (admin && k.TipKorisnikaId == 1)
            {
                await next(); //ok - ima pravo pristupa
                return;
            }

            if (filterContext.Controller is Controller c1)
            {
                c1.TempData["error_poruka"] = "Nemate pravo pristupa";
            }
            filterContext.Result = new RedirectToActionResult("Index", "Home", new { @area = "" });
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // throw new NotImplementedException();
        }
    }
}
