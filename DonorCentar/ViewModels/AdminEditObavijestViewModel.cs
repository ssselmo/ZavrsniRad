﻿using DonorCentar.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.Web.ViewModels
{
    public class AdminEditObavijestViewModel
    {
        public string Naslov { get; set; }
        public string Sadrzaj { get; set; }
        public int ObavijestId { get; set; }


    }
}
