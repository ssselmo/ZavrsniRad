using DonorCentar.Model;
using DonorCentar.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.WebAPI.Services
{

    public interface IPrimalacService : IReadService<Primalac, PrimalacSearchRequest>
    {

        Model.Primalac Verifikuj(int id);
        Model.Primalac GetByKorisnikId(int id);
        Model.Primalac UpdateDokument(int id, byte[] dokument);




    }
}
