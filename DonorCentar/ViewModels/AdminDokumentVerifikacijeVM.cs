using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.Web.ViewModels
{
    public class AdminDokumentVerifikacijeVM
    {
        public int Id { get; set; }
        public byte[] Dokument { get; set; }
        public bool Verifikovan { get; set; }
    }
}
