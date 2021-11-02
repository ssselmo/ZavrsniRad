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
    public class DonorController : ControllerBase
    {
        private readonly IDonorService _service;

        public DonorController(IDonorService service)
        {
            _service = service;
        }


        [HttpGet]
        public IEnumerable<Model.Donor> GetAll([FromQuery] DonorSearchRequest request)
        {
            return _service.Get(request);
        }


       
    }
}
