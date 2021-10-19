using DonorCentar.Model.Requests;
using DonorCentar.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.WebAPI.Controllers
{
    public class DonacijaController : BaseCRUDController<Model.Donacija, DonacijaSearchRequest, DonacijaInsertRequest, DonacijaInsertRequest>
    {
        public DonacijaController(IDonacijaService service) : base(service)
        {
        }

    }
}
