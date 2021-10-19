using AutoMapper;
using DonorCentar.WebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.WebAPI.Services
{
    public class TipKorisnikaService : BaseReadService<Model.TipKorisnika, Database.TipKorisnika, object>, ITipKorisnikaService
    {
        public TipKorisnikaService(BazaPodataka context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
