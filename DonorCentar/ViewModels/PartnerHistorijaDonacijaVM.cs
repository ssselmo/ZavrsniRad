using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.Web.ViewModels
{
    public class PartnerHistorijaDonacijaVM
    {
       
            public List<Row> rows { get; set; }
        public class Row
        {
            public string Donor { get; set; }
         

            public string Primalac { get; set; }
            public int DonacijaId { get; set; }
        }

        }
    }
