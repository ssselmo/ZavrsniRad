using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DonorCentar.Model.Requests
{
    public class DonacijaInsertRequest
    {
       
       
        [Required(ErrorMessage = "Izaberite tip donacije")]
        public int? TipDonacijeId { get; set; }
        [Required(ErrorMessage = "Unesite opis")]
        public string Opis { get; set; }

        [Required(ErrorMessage = "Unesite kolicinu")]
        [Range(1, 9999999999999999.99, ErrorMessage = "Unesite veći od jedan")]
        public decimal Kolicina { get; set; }

        [Required(ErrorMessage = "Izaberite jedinicu mjere")]
        public JedinicaMjere? JedinicaMjere { get; set; }

      
        public int? PrimalacId { get; set; }

        
        public int? DonorId { get; set; }
        
        public int? TransportId { get; set; }

        
        public int VrstaDonacijeId { get; set; }
      
        public int StatusId { get; set; }

        
        public int? InformacijeId { get; set; }

    }
}
