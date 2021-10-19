using DonorCentar.Model.Requests;
using DonorCentar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.WebAPI.Services
{
    
        public interface IObavijestService : ICRUDService<Obavijest, ObavijestSearchRequest, ObavijestInsertRequest, ObavijestUpdateRequest>
        {
            
        }
    
}
