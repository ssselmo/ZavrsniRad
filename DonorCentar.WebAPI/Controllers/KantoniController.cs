﻿using DonorCentar.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KantoniController : BaseReadController<Model.Kanton, object>
    {
        public KantoniController(IKantonService service)
            : base(service)
        {
        }

    }
}
