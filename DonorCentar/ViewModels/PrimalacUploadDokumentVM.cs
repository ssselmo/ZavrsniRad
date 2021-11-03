using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.ViewModels
{
    public class PrimalacUploadDokumentVM
    {
        public IFormFile UploadDokument { get; set; }
        public byte[] dokument { get; internal set; }
    }
}
