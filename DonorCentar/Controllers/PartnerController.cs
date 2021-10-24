using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DonorCentar.Helper;
using DonorCentar.Hubs;
using DonorCentar.Models;
using DonorCentar.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace DonorCentar.Controllers
{
    [Autorizacija(partner: true)]

    public class PartnerController : Controller
    {
        private BazaPodataka db;
        private IHubContext<NotificationHub> _hubContext;

        public PartnerController(IHubContext<NotificationHub> hubContext, BazaPodataka _db)
        {
            db = _db;
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            

            int partnerId = db.Partner.Where(d => d.KorisnikId == k.Id).FirstOrDefault().Id;

            Korisnik k1 = db.Korisnik.Where(o => o.Id == k.Id)
               .Include(k => k.LicniPodaci)
               .Include(k => k.Grad)
               .Include(k => k.TipKorisnika)
               .Include(k => k.LoginPodaci).FirstOrDefault();
            var korisnikViewModel = new KorisnikVM
            {
                Id = partnerId,
                Ime = k1.LicniPodaci.Ime,
                Prezime=k1.LicniPodaci.Prezime,
                TipKorisnika = k1.TipKorisnika.Tip,
                Adresa = k1.LicniPodaci.Adresa,
                BrojTelefona = k1.LicniPodaci.BrojTelefona,
                ProfilnaSlika = k1.LicniPodaci.ProfilnaSlika,
                Email = k1.LicniPodaci.Email,
                Grad = k1.Grad.Naziv,
                PozitivniDojmovi = db.DojamKorisnik.Where(d => d.KorisnikId == k.Id).Count(d => d.DojamId == 1),
                NeutralniDojmovi = db.DojamKorisnik.Where(d => d.KorisnikId == k.Id).Count(d => d.DojamId == 2),
                NegativniDojmovi = db.DojamKorisnik.Where(d => d.KorisnikId == k.Id).Count(d => d.DojamId == 3)
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

                    Vrijeme = o.Vrijeme,
                    Naslov = o.Naslov,
                    ObavijestId = o.ObavijestId,
                    Sadrzaj = o.Sadrzaj

                }).ToList()
            };

            this.PostaviViewBag("Obavijesti");
            return View(model);
            
        }
        public ActionResult IzbrisiObavijest(int obavijestId)
        {
            Obavijest o = db.Obavijest.Find(obavijestId);
            db.Obavijest.Remove(o);
            db.SaveChanges();

            return RedirectToAction("Obavijesti");
        }
        public ActionResult DonacijeBezTransporta()
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
           

            this.PostaviViewBag("DonacijeBezTransporta");
            return View();
        }
        public ActionResult HistorijaDonacija()
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            

            this.PostaviViewBag("HistorijaDonacija");
            return View();
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

    }
}