using DonorCentar.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.ViewModels
{
    public class DodajDonacijuViewModel
    {

        public Donacija Donacija { get; set; }
        public List<SelectListItem> TipDonacije { get; set; }
        public List<SelectListItem> Primalac { get; set; }
        public List<SelectListItem> Transport { get; set; }
        public List<SelectListItem> VrstaDonacije { get; set; }
        public List<SelectListItem> Status { get; set; }
        public List<SelectListItem> Informacije{ get; set; }



    }
}
