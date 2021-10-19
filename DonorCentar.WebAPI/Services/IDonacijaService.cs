using DonorCentar.Model.Requests;
using DonorCentar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.WebAPI.Services
{
    
        public interface IDonacijaService : ICRUDService<Donacija, DonacijaSearchRequest, DonacijaInsertRequest, DonacijaInsertRequest>
        {
            
        }
    
}
