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
    public class PartnerController : ControllerBase
    {
        private readonly IPartnerService _service;

        public PartnerController(IPartnerService service)
        {
            _service = service;
        }


        [HttpGet]
        public IEnumerable<Model.Partner> GetAll([FromQuery] PartnerSearchRequest request)
        {
            return _service.Get(request);
        }


       
    }
}
