using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.Web.ViewModels
{
    public class DonorDojamVM
    {
        public int DojamId { get; set; }
        public List<SelectListItem> DojamList { get; set; }
        public string Opis { get; set; }
        public int DonacijaId { get; set; }
    }
}
