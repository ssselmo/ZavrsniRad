using DonorCentar.Model.Requests;
using DonorCentar.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrimalacController : ControllerBase
    {
        private readonly IPrimalacService _service;

        public PrimalacController(IPrimalacService service)
        {
            _service = service;
        }


        [HttpGet]
        public IEnumerable<Model.Primalac> GetAll([FromQuery] PrimalacSearchRequest request)
        {
            return _service.Get(request);
        }


        [HttpPut("Verifikuj/{id}")]
        public Model.Primalac Verifikuj(int id)
        {
            return _service.Verifikuj(id);
        }


        [HttpPut("ToggleVerifikuj/{id}")]
        public Model.Primalac ToggleVerifikuj(int id)
        {
            return _service.ToggleVerifikuj(id);
        }

        [HttpGet("{id}")]
        public Model.Primalac GetById(int id)
        {
            
            return _service.GetById(id);

        }

        [HttpGet("GetByKorisnikId/{id}")]
        public Model.Primalac GetByKorisnikId(int id)
        {
            return _service.GetByKorisnikId(id);
        }

        [HttpPut("UpdateDokument/{id}")]
        public Model.Primalac UpdateDokument(int id,byte[] dokument)
        {
            return _service.UpdateDokument(id,dokument);
        }
    }
}
