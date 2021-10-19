using DonorCentar.Model.Requests;
using DonorCentar.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KorisniciController : ControllerBase
    {
        private readonly IKorisniciService _service;

        public KorisniciController(IKorisniciService service)
        {
            _service = service;
        }

        [HttpGet]
        public IList<Model.Korisnik> GetAll([FromQuery] KorisniciSearchRequest request)
        {
            return _service.GetAll(request);
        }

        [HttpGet("{id}")]
        public Model.Korisnik GetById(int id)
        {
            return _service.GetById(id);
        }

        [HttpPost]
        public Model.Korisnik Insert(KorisniciInsertRequest korisnici)
        {
            return _service.Insert(korisnici);
        }

        [HttpPut("{id}")]
        public Model.Korisnik Update(int id, [FromBody] KorisniciUpdateRequest request)
        {
            return _service.Update(id, request);
        }

        [HttpGet("Profil")]
        [Authorize]
        public Model.Korisnik Profil()
        {
            return _service.Profil();
        }


        [HttpDelete("{id}")]
        public Model.Korisnik Delete(int id)
        {
            return _service.Delete(id);
        }

        [HttpGet("GetPreporuka")]
        public IList<Model.Donacija> GetPreporuka()
        {
            return _service.GetPreporuka();
        }


    }
}
