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
    public class TipKorisnikaController : BaseReadController<Model.TipKorisnika, object>
    {
        public TipKorisnikaController(ITipKorisnikaService service)
            : base(service)
        {
        }

    }
}
