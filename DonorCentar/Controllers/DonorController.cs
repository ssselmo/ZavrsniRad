using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DonorCentar.Helper;
using DonorCentar.Hubs;
using DonorCentar.Models;
using DonorCentar.ViewModels;
using DonorCentar.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace DonorCentar.Controllers
{
    [Autorizacija(donor:true)]
    public class DonorController : Controller
    {
        private BazaPodataka db;
        private IHubContext<NotificationHub> _hubContext;
        public DonorController(IHubContext<NotificationHub> hubContext, BazaPodataka _db)
        {
            _hubContext = hubContext;
            db = _db;
        }
        public IActionResult Index()
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            

            int donorId = db.Donor.Where(d => d.KorisnikId == k.Id).FirstOrDefault().Id;

            Korisnik k1 = db.Korisnik.Where(o => o.Id == k.Id)
               .Include(k => k.LicniPodaci)
               .Include(k => k.Grad)
               .Include(k => k.TipKorisnika)
               .Include(k => k.LoginPodaci).FirstOrDefault();
            var korisnikViewModel = new KorisnikVM
            {
                Id = donorId,
                Ime = k1.LicniPodaci.Ime,
                Prezime=k1.LicniPodaci.Prezime,
                TipKorisnika = k1.TipKorisnika.Tip,
                Adresa = k1.LicniPodaci.Adresa,
                BrojTelefona = k1.LicniPodaci.BrojTelefona,
                Email = k1.LicniPodaci.Email,
                ProfilnaSlika = k1.LicniPodaci.ProfilnaSlika,
                Grad = k1.Grad.Naziv
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
        public ActionResult DodajDonaciju()
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            
            DodajDonacijuViewModel vm = new DodajDonacijuViewModel
            {
                TipDonacije = db.TipDonacije.Select(t => new SelectListItem { Value = t.TipDonacijeId.ToString(), Text = t.Tip }).ToList(),
                Informacije = db.InformacijeTransporta.Select(t => new SelectListItem { Value = t.InformacijeTransportaId.ToString(), Text = t.Opis }).ToList(),
                Primalac = db.Primalac.Select(t => new SelectListItem { Value = t.KorisnikId.ToString(), Text = t.Korisnik.LicniPodaci.ImePrezime }).ToList()

            };
            /*PRILIKOM DODAVANJA DONACIJE -> DonorID = KorisnikID od donora*/

            this.PostaviViewBag("DodajDonaciju");
            return View(vm);
        }

        public ActionResult SpasiDonaciju(DodajDonacijuViewModel viewModel)
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            

            Donacija d = new Donacija
            {
                JedinicaMjere = (JedinicaMjere)viewModel.Donacija.JedinicaMjere,
                Kolicina = (int)viewModel.Donacija.Kolicina,
                Opis = viewModel.Donacija.Opis,
                PrimalacId = viewModel.Donacija.PrimalacId,
                StatusId = 2,
                TipDonacijeId = viewModel.Donacija.TipDonacijeId,
                VrstaDonacijeId = 1,
                InformacijeId = viewModel.Transport ? 4 : 1,
                DonorId = k.Id,
               

            };

            
            db.Donacija.Add(d);
            
            

            db.SaveChanges();

            ViewBag.Prikazi = "prikazi";
            return RedirectToAction("MojeAktivneDonacije");
        }

        public ActionResult PregledPotreba()
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            

            DonorPregledPotrebaVM model = new DonorPregledPotrebaVM
            {
                rows = db.Donacija.Where(d => d.VrstaDonacijeId==2 && d.StatusId==1 ).Select(d => new DonorPregledPotrebaVM.Row
                {
                    DonacijaId = d.DonacijaId,
                    LicniPodaciNaziv = d.Primalac.LicniPodaci.ImePrezime,
                    PrimalacId = (int)d.PrimalacId,
                    StatusId = d.StatusId,
                    StatusOpis = d.Status.Opis,
                    TipDonacije = d.TipDonacije.Tip,
                    Transport = d.TransportId == null

                }).ToList()
            };

            this.PostaviViewBag("PregledPotreba");
            return View(model);
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
            

            /*Ukoliko Donor nije ostavio dojam primaocu za zavrsenu donaciju,
              treba da postoji button "Ostavi dojam" */

            this.PostaviViewBag("HistorijaDonacija");
            return View();
        }

        public ActionResult OstaliKorisnici()
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            

            this.PostaviViewBag("OstaliKorisnici");
            return View();
        }

        public ActionResult DetaljiDonacije(int donacijaId, int akcija)
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            

            Donacija donacija = db.Donacija.Where(d => d.DonacijaId == donacijaId)
                .Include(d => d.Status)
                .Include(d => d.Primalac)
                .Include(d => d.Primalac.LicniPodaci)
                .Include(d => d.Primalac.Grad)
                .Include(d => d.TipDonacije)
                .FirstOrDefault();

            DonorDetaljiDonacijeVM model = new DonorDetaljiDonacijeVM
            {
                DonacijaId = donacijaId,
                JedinicaMjere = (JedinicaMjere)donacija.JedinicaMjere,
                Kolicina = donacija.Kolicina,
                Opis = donacija.Opis,
                StatusOpis = donacija.Status.Opis,
                TipDonacije = donacija.TipDonacije.Tip
            };

            if (donacija.Primalac != null)
            {
                model.PrimalacAdresa = donacija.Primalac.LicniPodaci.Adresa;
                model.PrimalacGrad = donacija.Primalac.Grad.Naziv;
                model.PrimalacLicniPodaciNaziv = donacija.Primalac.LicniPodaci.ImePrezime;
            } else
            {
                model.PrimalacAdresa = "Donacija je bez primaoca";
                model.PrimalacGrad = "";
                model.PrimalacLicniPodaciNaziv = "Donacija je bez primaoca";
            }

            if (akcija == 1)
            {
                this.PostaviViewBag("MojeAktivneDonacije");
            }
            else
            {
                this.PostaviViewBag("PregledPotreba");
            }

            ViewBag.Akcija = akcija;
            return View(model);
        }

        public ActionResult PrekiniDonaciju(int donacijaId)
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            

            Donacija d = db.Donacija.Find(donacijaId);

            

            d.PrimalacId = null;
            d.TransportId = null;
            d.StatusId = 6;
            db.SaveChanges();

          

            return RedirectToAction("MojeAktivneDonacije");
        }

        public ActionResult OdbijZahtjev(int donacijaId)
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            

            Donacija d = db.Donacija.Find(donacijaId);

          

            d.PrimalacId = null;
            d.TransportId = null;
            d.StatusId = 6;
            db.SaveChanges();


            return RedirectToAction("MojeAktivneDonacije");
        }

        public ActionResult MojeAktivneDonacije()
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();

           

            var model = new DonorMojeAktivneDonacijeVM
            {
                rows = db.Donacija.Where(d => d.DonorId == k.Id && d.StatusId!=6).Select(d => new DonorMojeAktivneDonacijeVM.Row
                {
                    DonacijaId = d.DonacijaId,
                    StatusOpis = d.Status.Opis,
                    TipDonacije = d.TipDonacije.Tip,
                    InformacijaId = d.InformacijeId,
                    PrimalacId = (int)d.PrimalacId,
                    Transport= d.TransportId==null
                    
                }).ToList()
            };

            for (int i = 0; i < model.rows.Count; i++)
            {
                string licniNaziv = db.Korisnik.Where(a => a.Id == model.rows[i].PrimalacId).Include(a => a.LicniPodaci).SingleOrDefault().LicniPodaci.ImePrezime;
                if (licniNaziv != null)
                {
                    
                    model.rows[i].LicniPodaciNaziv = licniNaziv;
                }
                else
                {
                    model.rows[i].LicniPodaciNaziv = "?";
                }
            }

            this.PostaviViewBag("MojeAktivneDonacije");
            return View(model);
        }

        public ActionResult ObezbjediTransport(int donacijaId)
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            

            Donacija d = db.Donacija.Find(donacijaId);

            d.TransportId = k.Id;
            d.StatusId = 2;

         

            db.SaveChanges();


            return RedirectToAction("MojeAktivneDonacije");
        }

        //public ActionResult IzbrisiObavijest(int obavijestId)
        //{
        //    Obavijest o = db.Obavijest.Find(obavijestId);

        //    if (o != null)
        //    {
        //        db.Obavijest.Remove(o);
        //        db.SaveChanges();
        //    }

        //    return RedirectToAction("Obavijesti");
        //}

        public ActionResult PrihvatiZahtjev(int donacijaId)
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            

            Donacija d = db.Donacija.Find(donacijaId);
            d.StatusId = 2;

          
            db.SaveChanges();


            this.PostaviViewBag("MojeAktivneDonacije");
            ViewBag.donacijaId = donacijaId;
            return View();
        }

        public ActionResult Doniraj(int donacijaId)
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            
            Donacija d = db.Donacija.Find(donacijaId);
            d.StatusId = 2;
            d.DonorId = k.Id;

          
            db.SaveChanges();


            this.PostaviViewBag("PregledPotreba");
            ViewBag.donacijaId = donacijaId;
            return View("PrihvatiZahtjev");
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

        


        public ActionResult PregledPartnera(int? page)
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
           

            IEnumerable<Partner> partner = db.Partner
                .Include(x => x.Korisnik.Grad.Kanton)
              .Include(x => x.Korisnik.LicniPodaci).ToList();


            this.PostaviViewBag("PregledPartnera");
            return View(partner.ToPagedList(page ?? 1, 4));
        }

        public IActionResult Dojam (int Id)
        {
            
            var dojam = db.DojamKorisnik.Where(x => x.DonacijaId == Id).FirstOrDefault();

           

            DonorDojamVM vm = new DonorDojamVM
            {
                DojamList = db.Dojam.Select(x => new SelectListItem
                {
                    Text = x.VrstaDojma,
                    Value = x.Id.ToString()
                }).ToList(),
                DonacijaId = Id

            };
            if (dojam != null)
            {
                vm.Opis = dojam.Opis;
                vm.DojamId = dojam.DojamId;
            }

            this.PostaviViewBag("Dojam");
            return View(vm);
        }
        public IActionResult SpasiDojam(DonorDojamVM vm)
        {
            var dojam = db.DojamKorisnik.Where(x => x.DonacijaId == vm.DonacijaId).FirstOrDefault();
            Korisnik k = HttpContext.GetLogiraniKorisnik();


            if (dojam!=null)
            {
                dojam.Opis = vm.Opis;
                dojam.DojamId = vm.DojamId;


            }
            else
            {
                dojam = new DojamKorisnik
                {
                    Datum = DateTime.Now,
                    DojamId = vm.DojamId,
                    Opis = vm.Opis,
                    DonacijaId = vm.DonacijaId,
                    KorisnikId = k.Id
                };
                db.Add(dojam);
            }
            db.SaveChanges();

            return RedirectToAction("MojeAktivneDonacije");
        }
    }
}