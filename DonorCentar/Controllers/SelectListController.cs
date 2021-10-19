﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DonorCentar.Models;
using DonorCentar.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DonorCentar.Controllers
{
    public class SelectListController : Controller
    {
        private BazaPodataka db;
        public SelectListController(BazaPodataka _db)
        {
            db = _db;
        }

        public JsonResult getNeverifikovaniPrimaoci()
        {
            var lista = new List<SelectListNeverifikovaniPrimaociVM>();

            foreach (var x in db.Primalac.Where(p => p.Verifikovan == false))
            {
                Korisnik k = db.Korisnik.Where(k => k.Id == x.KorisnikId)
                    .Include(k => k.LicniPodaci)
                    .Include(k => k.Grad)
                    .Include(k => k.LicniPodaci)
                    .SingleOrDefault();

                lista.Add(new SelectListNeverifikovaniPrimaociVM
                {
                    Adresa = k.LicniPodaci.Adresa,
                    BrojTelefona = k.LicniPodaci.BrojTelefona,
                    Email = k.LicniPodaci.Email,
                    Grad = k.Grad.Naziv,
                    Ime = k.LicniPodaci.Ime,
                    Prezime=k.LicniPodaci.Prezime,
                    KorisnikId = k.Id,
                    DokumentVerifikacije = x.DokumentVerifikacije
                }) ;
            }

            return Json(lista);
        }
    }
}