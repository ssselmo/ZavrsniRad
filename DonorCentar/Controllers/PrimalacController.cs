using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DonorCentar.Helper;
using DonorCentar.Hubs;
using DonorCentar.Models;
using DonorCentar.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace DonorCentar.Controllers
{
    [Autorizacija(primalac: true)]

    public class PrimalacController : Controller
    {
        private BazaPodataka db;
        private readonly IWebHostEnvironment webHostEnviroment;
        private IHubContext<NotificationHub> _hubContext;

        public PrimalacController(IHubContext<NotificationHub> hubContext, BazaPodataka _db, IWebHostEnvironment webHostEnviroment)
        {
            _hubContext = hubContext;
            db = _db;
            this.webHostEnviroment = webHostEnviroment;
        }

        public IActionResult Index()
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            

            int primalacId = db.Primalac.Where(p => p.KorisnikId == k.Id).FirstOrDefault().Id;
            bool verifikovan = db.Primalac.Where(p => p.KorisnikId == k.Id).FirstOrDefault().Verifikovan;

            Korisnik k1 = db.Korisnik.Where(o => o.Id == k.Id)
               .Include(k => k.LicniPodaci)
               .Include(k => k.Grad)
               .Include(k => k.TipKorisnika)
               .Include(k => k.LoginPodaci).FirstOrDefault();

            var korisnikViewModel = new KorisnikVM
            {
                Id = primalacId,
                Ime = k1.LicniPodaci.Ime,
                Prezime = k1.LicniPodaci.Prezime,

                TipKorisnika = k1.TipKorisnika.Tip,
                Adresa = k1.LicniPodaci.Adresa,
                BrojTelefona = k1.LicniPodaci.BrojTelefona,
                Email = k1.LicniPodaci.Email,
                ProfilnaSlika = k1.LicniPodaci.ProfilnaSlika,
                Grad = k1.Grad.Naziv,
                Verifikovan = verifikovan,
                PozitivniDojmovi = db.DojamKorisnik.Where(d => d.Donacija.PrimalacId == primalacId).Count(d => d.DojamId == 3),
                NeutralniDojmovi = db.DojamKorisnik.Where(d => d.Donacija.PrimalacId == primalacId).Count(d => d.DojamId == 2),
                NegativniDojmovi = db.DojamKorisnik.Where(d => d.Donacija.PrimalacId == primalacId).Count(d => d.DojamId == 1)
            };

            this.PostaviViewBag("Index");
            return View(korisnikViewModel);
        }

        public ActionResult Obavijesti(int? page)
        {

            IEnumerable<Obavijest> obavijesti = db.Obavijest.ToList();


            this.PostaviViewBag("Obavijesti");
            return View(obavijesti.ToPagedList(page ?? 1, 2));
        }

        public ActionResult DodajPotrebu()
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            

            var viewModel = new DodajDonacijuPotrebuViewModel
            {
                TipDonacije = db.TipDonacije.Select(t => new SelectListItem { Value = t.TipDonacijeId.ToString(), Text = t.Tip }).ToList(),
               
            };

            this.PostaviViewBag("DodajPotrebu");
            return View(viewModel);
        }

        public ActionResult Uredi(int donacijaId)
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            

            Donacija donacija = db.Donacija.Find(donacijaId);

            DodajDonacijuPotrebuViewModel viewModel = new DodajDonacijuPotrebuViewModel
            {
                TipDonacije = db.TipDonacije.Select(t => new SelectListItem { Value = t.TipDonacijeId.ToString(), Text = t.Tip }).ToList(),
                DonacijaId = donacija.DonacijaId,
                JedinicaMjere = (JedinicaMjere)donacija.JedinicaMjere,
                Kolicina = (int)donacija.Kolicina,
                Opis = donacija.Opis,
                TipDonacijeId = donacija.TipDonacijeId
            };

            this.PostaviViewBag("MojePotrebe");
            return View("DodajPotrebu", viewModel);
        }

        //public ActionResult IzbrisiObavijest(int obavijestId)
        //{
        //    Obavijest o = db.Obavijest.Find(obavijestId);
        //    db.Obavijest.Remove(o);
        //    db.SaveChanges();

        //    return RedirectToAction("Obavijesti");
        //}

        public ActionResult IzbrisiPotrebu(int donacijaId, int? page2)
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
           

            Donacija d = db.Donacija.Find(donacijaId);
            db.Donacija.Remove(d);
            db.SaveChanges();

            IEnumerable<Donacija> donacija = db.Donacija.Where(s => s.DonorId == null)
                .Where(s => s.PrimalacId == k.Id)
                .Where(s => s.VrstaDonacijeId == 2)
                .Include(s => s.Status)
                .Include(s => s.TipDonacije)
                .ToList();

            ViewBag.Prikazi = "prikazi";
            this.PostaviViewBag("MojePotrebe");
            return View("MojePotrebe", donacija.ToPagedList(page2 ?? 1, 5));
        }

        public ActionResult SpasiPotrebu(DodajDonacijuPotrebuViewModel viewModel)
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            

            Donacija d = new Donacija
            {
                JedinicaMjere = (JedinicaMjere)viewModel.JedinicaMjere,
                Kolicina = (int)viewModel.Kolicina,
                Opis = viewModel.Opis,
                PrimalacId = k.Id,
                StatusId = 1,
                TipDonacijeId = viewModel.TipDonacijeId,
                VrstaDonacijeId = 2,
                InformacijeId = viewModel.Transport ? 4 : 1
            };

            if (viewModel.DonacijaId == 0)
            {
                db.Donacija.Add(d);
            }
            else
            {
                var objectDb = db.Donacija.Find(viewModel.DonacijaId);
                objectDb.JedinicaMjere = (JedinicaMjere)viewModel.JedinicaMjere;
                objectDb.Kolicina = (int)viewModel.Kolicina;
                objectDb.Opis = viewModel.Opis;
                objectDb.TipDonacijeId = viewModel.TipDonacijeId;
            }

            db.SaveChanges();
            
            ViewBag.Prikazi = "prikazi";
            return RedirectToAction("MojePotrebe");
        }

        public ActionResult MojePotrebe(int? page)
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            

            IEnumerable<Donacija> donacija = db.Donacija.Where(s => s.DonorId == null).Where(s => s.PrimalacId == k.Id).Where(s => s.VrstaDonacijeId == 2).Include(s => s.Status).Include(s => s.TipDonacije).ToList();

            this.PostaviViewBag("MojePotrebe");
            return View(donacija.ToPagedList(page ?? 1, 5));
        }

        public ActionResult PregledDonacija(int? page)
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            

            IEnumerable<Donacija> donacija = db.Donacija.Where(s => s.StatusId == 5)
                .Where(s => s.VrstaDonacijeId == 1).Include(s => s.Status).Include(s => s.TipDonacije).
                Include(s => s.Donor.LicniPodaci).ToList();

            this.PostaviViewBag("PregledDonacija");
            return View(donacija.ToPagedList(page ?? 1, 4));
        }

        public ActionResult ZatrazeneDonacije(int? page)
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
           

            IEnumerable<Donacija> donacija = db.Donacija.Where(s => s.StatusId == 1)
            .Where(s => s.PrimalacId == k.Id)
            .Include(s => s.Status).Include(s => s.TipDonacije)
            .Include(s => s.Donor)
            .Include(s => s.Donor.LicniPodaci).ToList();

            this.PostaviViewBag("ZatrazeneDonacije");
            return View(donacija.ToPagedList(page ?? 1, 5));
        }

        public ActionResult DetaljiDonacije(int donacijaId, int akcija)
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            

            PrimalacDetaljiDonacijeVM model = db.Donacija.Where(d => d.DonacijaId == donacijaId)
                .Select(d => new PrimalacDetaljiDonacijeVM
            {
                DonacijaId = d.DonacijaId,
                DonorAdresa = d.Donor.LicniPodaci.Adresa,
                DonorGrad = d.Donor.Grad.Naziv,
                DonorNaziv = d.Donor.LicniPodaci.ImePrezime,
                JedinicaMjere = (JedinicaMjere)d.JedinicaMjere,
                Kolicina = d.Kolicina,
                Opis = d.Opis,
                TipDonacije = d.TipDonacije.Tip,
                TransportId = (int)d.TransportId,
                InformacijeId = (int)d.InformacijeId
            }).SingleOrDefault();

            ViewBag.akcija = akcija;

            if(akcija == 1)
            {
                this.PostaviViewBag("ZatrazeneDonacije");
            }
            else
            {
                this.PostaviViewBag("PregledDonacija");
            }

            return View(model);
        }

        public ActionResult ZatraziDonaciju(int donacijaId, int? page)
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            
            db.Donacija.Find(donacijaId).StatusId = 1;
            db.Donacija.Find(donacijaId).PrimalacId = k.Id;
            db.SaveChanges();

            int donorId = (int)db.Donacija.Find(donacijaId).DonorId;

           

           

         

            ViewBag.Prikazi = "Zatrazeno";

            IEnumerable<Donacija> donacija = db.Donacija.Where(s => s.StatusId == 5)
            .Where(s => s.VrstaDonacijeId == 1).Include(s => s.Status).Include(s => s.TipDonacije)
            .Include(s => s.Donor.LicniPodaci).ToList();

            this.PostaviViewBag("PregledDonacija");

            return View("PregledDonacija", donacija.ToPagedList(page ?? 1, 5));
        }

        public ActionResult HistorijaDonacija()
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            
            this.PostaviViewBag("HistorijaDonacija");
            return View();
        }

        public ActionResult OstaliKorisnici()
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            
            this.PostaviViewBag("OstaliKorisnici");
            return View();
        }

        public ActionResult PonistiZahtjev(int donacijaId, int? page)
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            

            Donacija d = db.Donacija.Find(donacijaId);

            d.StatusId = 6;
            d.PrimalacId = null;

            db.SaveChanges();

            IEnumerable<Donacija> donacija = db.Donacija.Where(s => s.StatusId == 1)
            .Where(s => s.VrstaDonacijeId == 1).Include(s => s.Status).Include(s => s.TipDonacije)
            .Include(s => s.Donor.LicniPodaci).ToList();

            ViewBag.Prikazi = "Ponisteno";

            this.PostaviViewBag("ZatrazeneDonacije");
            return View("ZatrazeneDonacije", donacija.ToPagedList(page ?? 1, 5));
        }

        public ActionResult ObezbjediTransport(int donacijaId, int? page)
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            

            Donacija d = db.Donacija.Find(donacijaId);

            d.TransportId = k.Id;
            d.StatusId = 2;

         

            db.SaveChanges();


            IEnumerable<Donacija> donacija = db.Donacija.Where(s => s.StatusId == 1)
                .Where(s => s.VrstaDonacijeId == 1).Include(s => s.Status).Include(s => s.TipDonacije)
                .Include(s => s.Donor.LicniPodaci).ToList();

            ViewBag.Prikazi = "Transport";

            this.PostaviViewBag("ZatrazeneDonacije");
            return View("ZatrazeneDonacije", donacija.ToPagedList(page ?? 1, 5));
        }

        public ActionResult DonacijaJeStigla(int donacijaId)
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            

            Donacija d = db.Donacija.Find(donacijaId);
            d.StatusId = 2;
            db.SaveChanges();

            PrimalacDonacijaJeStiglaVM model = new PrimalacDonacijaJeStiglaVM
            {
                ListaDojmova = db.Dojam.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.VrstaDojma
                }).ToList(),
                DonacijaId = donacijaId,
                KorisnikId = (int)d.DonorId
            };


            this.PostaviViewBag("ZatrazeneDonacije");
            return View(model);
        }

        public ActionResult DodajDojam(PrimalacDonacijaJeStiglaVM model)
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            

            DojamKorisnik dk = new DojamKorisnik
            {
                Datum = DateTime.Now,
                DojamId = (int)model.DojamId,
                DonacijaId = model.DonacijaId,
                Opis = model.Opis,
                KorisnikId = model.KorisnikId
            };

           

            db.DojamKorisnik.Add(dk);
            
            db.SaveChanges();

           

            return RedirectToAction("HistorijaDonacija");
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

        public ActionResult UploadDokument()
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            var primalac = db.Primalac.FirstOrDefault(x => x.KorisnikId == k.Id);
            if(primalac!= null && primalac.Verifikovan)
            {
                return RedirectToAction("Index");
            }
            var vm = new PrimalacUploadDokumentVM();
            vm.dokument = primalac.DokumentVerifikacije;

            this.PostaviViewBag("UploadDokument");
            return View(vm);
        }
        public IActionResult Documents(int Id)
        {
            var primalac = db.Primalac.FirstOrDefault(x => x.KorisnikId == Id);
            return File(primalac.DokumentVerifikacije, "image/jpeg");

        }

        [HttpPost]
        public  IActionResult UploadDokument(PrimalacUploadDokumentVM vm)
        {
            if (vm.UploadDokument != null)
            {
                

                Korisnik k = HttpContext.GetLogiraniKorisnik();
                var primalac = db.Primalac.FirstOrDefault(x => x.KorisnikId == k.Id);
                if(primalac!=null)
                {
                    using (var ms = new MemoryStream())
                    {
                        vm.UploadDokument.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        primalac.DokumentVerifikacije = fileBytes;
                    }
                    
                   
                    db.SaveChanges();
                }



            }

            return RedirectToAction("Index");
        }
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
    }
}