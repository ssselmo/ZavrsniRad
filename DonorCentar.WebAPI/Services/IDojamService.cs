using DonorCentar.Model;
using DonorCentar.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.WebAPI.Services
{
    public interface IDojamService
    {
        
            IList<Model.Dojam> GetAll();

            Model.DojamKorisnik GetById(int id);

            Model.DojamKorisnik Insert(Model.DojamKorisnik request);

            Model.DojamKorisnik Update(int id, Model.DojamKorisnik request);
        
    }
}
