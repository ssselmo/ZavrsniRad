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
    public class ObavijestController : BaseCRUDController<Model.Obavijest, ObavijestSearchRequest, ObavijestInsertRequest, ObavijestUpdateRequest>
        {
            public ObavijestController(IObavijestService service) : base(service)
            {
            }

        }
}


