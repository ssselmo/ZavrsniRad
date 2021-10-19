using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DonorCentar.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DonorCentar.ViewModels
{
    public class HomeZaboravljenaLozinkaVM
    {
        
       
        
        [StringLength(40)]
        [EmailAddress(ErrorMessage = "Unesite ispravnu email adresu (npr. donor@centar.ba")]
        public string Email { get; set; }

        public string BrojMobitela { get; set; }

    }
}
