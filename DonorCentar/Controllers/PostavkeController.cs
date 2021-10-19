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
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace DonorCentar.Controllers
{
    [Autorizacija(donor: true, primalac:true,admin:true,partner:true)]

    public class PostavkeController : Controller
    {
        private BazaPodataka db;
        private readonly IWebHostEnvironment webHostEnviroment;
        private IHubContext<NotificationHub> _hubContext;
        private readonly ILogger<PostavkeController> _logger;

        public PostavkeController(ILogger<PostavkeController> logger, IHubContext<NotificationHub> hubContext, BazaPodataka _db, IWebHostEnvironment webHostEnviroment)
        {
            db = _db;
            this.webHostEnviroment = webHostEnviroment;
            _logger = logger;
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            Korisnik k1 = db.Korisnik.Where(o => o.Id == k.Id)
               .Include(k => k.LicniPodaci)
               .Include(k => k.Grad)
               .Include(k => k.TipKorisnika)
               .Include(k => k.LoginPodaci).FirstOrDefault();

           



            var korisnikViewModel = new KorisnikVM
            {
                
                Ime = k1.LicniPodaci.Ime,
                Prezime=k1.LicniPodaci.Prezime,
                Adresa = k1.LicniPodaci.Adresa,
                BrojTelefona = k1.LicniPodaci.BrojTelefona,
                Email = k1.LicniPodaci.Email,
                Sifra= k1.LoginPodaci.Sifra,
                ProfilnaSlika = k1.LicniPodaci.ProfilnaSlika
            };

            this.PostaviViewBag("Index");
            return View(korisnikViewModel);
        }


        [ValidateAntiForgeryToken]
        public ActionResult SpasiPostavke(KorisnikVM viewModel)
        {
            Korisnik korisnik = HttpContext.GetLogiraniKorisnik();
            
            Korisnik k1 = db.Korisnik.Where(o => o.Id == korisnik.Id)
               .Include(k => k.LicniPodaci)
               .Include(k => k.Grad)
               .Include(k => k.TipKorisnika)
               .Include(k => k.LoginPodaci).FirstOrDefault();

            

            k1.LicniPodaci.Adresa = viewModel.Adresa;
            k1.LicniPodaci.BrojTelefona = viewModel.BrojTelefona;
            k1.LicniPodaci.Email = viewModel.Email;
            k1.LicniPodaci.Ime = viewModel.Ime;
            k1.LicniPodaci.Prezime = viewModel.Prezime;
            if(!string.IsNullOrEmpty(viewModel.Sifra))
                k1.LoginPodaci.Sifra = PasswordHelper.HashSifru(viewModel.Sifra);
            
            db.SaveChanges();


            return this.RedirectToDashboard();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UploadSlika(KorisnikVM vm)
        {
            if (vm.UploadSlika != null)
            {


                Korisnik k = HttpContext.GetLogiraniKorisnik();
                Korisnik k1 = db.Korisnik.Where(o => o.Id == k.Id)
               .Include(k => k.LicniPodaci)
               .Include(k => k.Grad)
               .Include(k => k.TipKorisnika)
               .Include(k => k.LoginPodaci).FirstOrDefault();
                
                if (k1 != null)
                {
                    
                    k1.LicniPodaci.ProfilnaSlika = System.IO.File.ReadAllBytes(vm.UploadSlika.FileName);
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


