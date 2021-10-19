using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DonorCentar.Models;
using DonorCentar.Helper;
using DonorCentar.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.SignalR;
using DonorCentar.Hubs;
using Microsoft.Extensions.Configuration;
using Vonage.Messaging;
using Vonage.Request;

namespace DonorCentar.Controllers
{
    public class HomeController : Controller
    {
        private BazaPodataka db;
        private IHubContext<NotificationHub> _hubContext;
        private readonly ILogger<HomeController> _logger;
        public IConfiguration Configuration { get; set; }
        public HomeController(ILogger<HomeController> logger, IHubContext<NotificationHub> hubContext, BazaPodataka _db, IConfiguration config)
        {
            db = _db;
            _logger = logger;
            _hubContext = hubContext;
            Configuration = config;
        }



        public IActionResult Index()
        {
            return View(new HomeIndexVM
            {
                ZapamtiSifru = true
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult ONama()
        {
            return View();
        }

        public ActionResult Registracija()
        {
            var model = new HomeRegistracijaVM
            {
                Grad = db.Grad.Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Naziv
                }).ToList(),
                TipKorisnika = db.TipKorisnika.Where(t => t.Id != 1).Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Tip
                }).ToList()
            };

            return View(model);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Spasi(HomeRegistracijaVM korisnik)
        {
            if (!ModelState.IsValid)
            {
                if (korisnik.Sifra != korisnik.PonoviSifru)
                {
                    ModelState["korisnik.LoginPodaci.PonoviSifru"].Errors.Clear();
                    ModelState["korisnik.LoginPodaci.PonoviSifru"].Errors.Add("Šifre se ne poklapaju");
                }

                korisnik.Grad = db.Grad.Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Naziv
                }).ToList();

                korisnik.TipKorisnika = db.TipKorisnika.Where(t => t.Id != 1).Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Tip
                }).ToList();


                return View("Registracija", korisnik);
            }

            korisnik.Sifra = PasswordHelper.HashSifru(korisnik.Sifra);

            Korisnik k = new Korisnik
            {
                GradId = (int)korisnik.GradId,
                LicniPodaci = new LicniPodaci
                {
                    Adresa = korisnik.Adresa,
                    BrojTelefona = korisnik.BrojTelefona,
                    Email = korisnik.Email,
                    Ime = korisnik.Ime,
                    Prezime=korisnik.Prezime
                },
                LoginPodaci = new LoginPodaci
                {
                    KorisnickoIme = korisnik.KorisnickoIme,
                    Sifra = korisnik.Sifra
                },
                TipKorisnikaId = (int)korisnik.TipKorisnikaId
            };

            db.Korisnik.Add(k);
            db.SaveChanges();

            EmailHelper.SendMail(korisnik.Email, "Registracija", "Zahvaljujemo se na vašoj registraciji. Sada ste dio zajednice koje spaja humane osobe sa organizacijama širom Bosne i Hercegovine. Uživajte.");

            if (korisnik.TipKorisnikaId == 4)
            {
                var donor = new Donor();
                donor.KorisnikId = k.Id;
                donor.DatumRegistracije = DateTime.Now;
                db.Donor.Add(donor);
                db.SaveChanges();
            }
            else if (korisnik.TipKorisnikaId == 3)
            {
                var primalac = new Primalac();
                primalac.KorisnikId = k.Id;
                primalac.Verifikovan = false;
                primalac.DatumRegistracije = DateTime.Now;

              

                db.Primalac.Add(primalac);
                db.SaveChanges();


            }
            else
            {
                var partner = new Partner();
                partner.KorisnikId = k.Id;
                partner.DatumRegistracije = DateTime.Now;
                db.Partner.Add(partner);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }


        public ActionResult Prijava(HomeIndexVM input)
        {
            LoginPodaci login = db.LoginPodaci.SingleOrDefault(x => x.KorisnickoIme == input.KorisnickoIme && x.Sifra == PasswordHelper.HashSifru(input.Sifra));

            if (login == null)
            {
                var lp = db.LicniPodaci.FirstOrDefault(x => x.BrojTelefona == input.KorisnickoIme);

                if (lp != null)
                {

                    var Korisnik = db.Korisnik.Single(x => x.LicniPodaciId == lp.Id);
                    login = db.LoginPodaci.SingleOrDefault(x => x.Id == Korisnik.LoginPodaciId);
                }

                if(login==null)
                { 
                    TempData["error_poruka"] = "Pogrešno korisničko ime ili šifra";
                return View("Index", input);
                }
                
            }


            Korisnik korisnik = db.Korisnik.Where(k => k.LoginPodaciId == login.Id)
                .Include(k => k.LicniPodaci)
                .Include(k => k.Grad)
                .Include(k => k.TipKorisnika)
                .Include(k => k.LoginPodaci).Single();

            HttpContext.SetLogiraniKorisnik(korisnik);

            return this.RedirectToDashboard();
        }

        public ActionResult Odjava()
        {
            return RedirectToAction("Index");
        }

        public ActionResult ZaboravljenaLozinka()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        public ActionResult PosaljiNovuLozinku(HomeZaboravljenaLozinkaVM vm)
        {
            var k = db.LicniPodaci.SingleOrDefault(x => x.Email == vm.Email);

            if (k != null)
            {
                var lozinka = PasswordHelper.GenerisiLozinku(6);
                var login = db.LoginPodaci.SingleOrDefault(x => x.Id == k.Id);
                if (login != null)
                {
                    login.Sifra = PasswordHelper.HashSifru(lozinka);
                    EmailHelper.SendMail(k.Email, "Nova lozinka", lozinka);
                    TempData["success_poruka"] = "Lozinka uspješno poslana !";
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
            }
            TempData["error_poruka"] = "Email adresa nije pronađena. ";
            return RedirectToAction("Index"); ;
        }
        [HttpPost]
        public IActionResult PosaljiNovuLozinkuTel(HomeZaboravljenaLozinkaVM vm)
        {





            var lp = db.LicniPodaci.FirstOrDefault(x => x.BrojTelefona == vm.BrojMobitela);

            if (lp != null)
            {

                var lozinka = PasswordHelper.GenerisiLozinku(6);
                var Korisnik = db.Korisnik.Single(x => x.LicniPodaciId == lp.Id);
                var login = db.LoginPodaci.SingleOrDefault(x => x.Id == Korisnik.LoginPodaciId);
                if (login != null)
                {

                    login.Sifra = PasswordHelper.HashSifru(lozinka);
                    TempData["success_poruka"] = "Lozinka uspješno poslana !";
                    db.SaveChanges();
                    var VONAGE_API_KEY = Configuration["VONAGE_API_KEY"];
                    var VONAGE_API_SECRET = Configuration["VONAGE_API_SECRET"];
                    var VONAGE_PHONE = Configuration["VONAGE_PHONE"];
                    var credentials = Credentials.FromApiKeyAndSecret(VONAGE_API_KEY, VONAGE_API_SECRET);
                    var client = new SmsClient(credentials);
                    var request = new SendSmsRequest { To = VONAGE_PHONE, From = VONAGE_PHONE, Text = "Vaša nova lozinka je : " + lozinka };

                    var response = client.SendAnSms(request);
                    return RedirectToAction("Index");
                }
            }

            TempData["error_poruka"] = "Broj mobitela nije pronađen. ";
            return RedirectToAction("Index");

        }
    }
}
