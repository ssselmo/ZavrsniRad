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
    public class DojamController : ControllerBase
    {
        private readonly IDojamService _service;

        public DojamController(IDojamService service)
        {
            _service = service;
        }

        [HttpGet]
        public IList<Model.Dojam> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet("{id}")]
        public Model.DojamKorisnik GetById(int id)
        {
            return _service.GetById(id);
        }

        [HttpPost]
        public Model.DojamKorisnik Insert([FromBody] Model.DojamKorisnik request)
        {
            return _service.Insert(request);
        }

        [HttpPut("{id}")]
        public Model.DojamKorisnik Update(int id,[FromBody] Model.DojamKorisnik request)
        {
            return _service.Update(id, request);
        }

       
    }
}
