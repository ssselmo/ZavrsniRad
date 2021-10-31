using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.Web.ViewModels
{
    public class PartnerDonacijeBezTransportaVM
    {
       
            public List<Row> rows { get; set; }
            public class Row
            {
            public int DonacijaId { get; set; }
            public string TipDonacije { get; set; }
                
                public string GradDonora { get; set; }
                public string GradPrimaoca { get; set; }

        }

    }
}
