using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DonorCentar.Helper;
using DonorCentar.Models;
using DonorCentar.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Mvc.Core;
using Microsoft.AspNetCore.SignalR;
using DonorCentar.Hubs;


namespace DonorCentar.Controllers
{
    [Autorizacija(admin:true)]
    public class AdministratorController : Controller
    {
        private BazaPodataka db;
        private IHubContext<NotificationHub> _hubContext;

        public AdministratorController(IHubContext<NotificationHub> hubContext, BazaPodataka _db)
        {
            _hubContext = hubContext;
            db = _db;
        }

        public IActionResult Index()
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            

            int adminId = db.Administrator.Where(p => p.KorisnikId == k.Id).FirstOrDefault().Id;

            Korisnik k1 = db.Korisnik.Where(o => o.Id == k.Id)
               .Include(k => k.LicniPodaci)
               .Include(k => k.Grad)
               .Include(k => k.TipKorisnika)
               .Include(k => k.LoginPodaci).FirstOrDefault();
            var korisnikViewModel = new KorisnikVM
            {
                Id = adminId,
                Ime = k1.LicniPodaci.Ime,
                Prezime= k1.LicniPodaci.Prezime,
                TipKorisnika = k1.TipKorisnika.Tip,
                Adresa = k1.LicniPodaci.Adresa,
                BrojTelefona = k1.LicniPodaci.BrojTelefona,
                Email = k1.LicniPodaci.Email,
                Grad = k1.Grad.Naziv,
                Verifikovan = true,
                ProfilnaSlika = k1.LicniPodaci.ProfilnaSlika
            };

            this.PostaviViewBag("Index");
            return View(korisnikViewModel);
        }

        public ActionResult Obavijesti()
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            

            ObavijestiVM model = new ObavijestiVM
            {
                rows = db.Obavijest.Select(o => new ObavijestiVM.Row
                {
                   Naslov=o.Naslov,
                   Sadrzaj=o.Sadrzaj,
                    ObavijestId = o.ObavijestId,
                   
                    Vrijeme = o.Vrijeme
                   
                }).ToList()
            };
            model.rows = model.rows.OrderByDescending(m => m.ObavijestId).ToList();

            this.PostaviViewBag("Obavijesti");
            return View(model);
        }

        public ActionResult NeverifikovaniPrimaoci()
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            

            #region old way
            //List<int> korisnikId = db.Primalac.Where(p => p.Verifikovan == false).Select(p => p.KorisnikId).ToList();
            //List<Korisnik> korisnici = new List<Korisnik>();
            //foreach (var x in korisnikId)
            //{
            //    korisnici.Add(db.Korisnik
            //        .Include(k => k.LicniPodaci)
            //        .Include(k => k.Grad)
            //        .Where(k => k.Id == x).FirstOrDefault());
            //}
            //korisnici = korisnici.OrderByDescending(t => t.Id).ToList();
            //return View(korisnici.ToPagedList(page ?? 1,5));
            #endregion

            this.PostaviViewBag("NeverifikovaniPrimaoci");
            return View();
        }

        public ActionResult Verifikuj(int korisnikId)
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            

            db.Primalac.Where(d => d.KorisnikId == korisnikId).FirstOrDefault().Verifikovan = true;
            db.SaveChanges();

            return RedirectToAction("NeverifikovaniPrimaoci");
        }

        public ActionResult DodajAdmina()
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            

            this.PostaviViewBag("DodajAdmina");
            return View();
        }

        

        public ActionResult IzbrisiObavijest(int obavijestId)
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            
            Obavijest o = db.Obavijest.Find(obavijestId);

            db.Obavijest.Remove(o);
            db.SaveChanges();

            return RedirectToAction("Obavijesti");
        }

        


        public ActionResult PregledPrimaoca(int? page)
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            

            IEnumerable<Primalac> primaoci = db.Primalac
                .Include(x => x.Korisnik.Grad.Kanton)
              .Include(x => x.Korisnik.LicniPodaci).ToList();
            


            this.PostaviViewBag("PregledPrimaoca");
            return View(primaoci.ToPagedList(page ?? 1, 4));
        }

        public ActionResult PregledDonora(int? page)
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            
            IEnumerable<Donor> donor = db.Donor
                .Include(x => x.Korisnik.Grad.Kanton)
              .Include(x => x.Korisnik.LicniPodaci).ToList();


            this.PostaviViewBag("PregledDonora");
            return View(donor.ToPagedList(page ?? 1, 4));
        }


        public ActionResult PregledPartnera(int? page)
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            

            IEnumerable<Partner> partner = db.Partner
                .Include(x => x.Korisnik.Grad.Kanton)
              .Include(x => x.Korisnik.LicniPodaci).ToList();


            this.PostaviViewBag("PregledPartnera");
            return View(partner.ToPagedList(page ?? 1, 4));
        }
    }
}