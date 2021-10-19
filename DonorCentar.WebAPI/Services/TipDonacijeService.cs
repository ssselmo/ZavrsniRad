using AutoMapper;
using DonorCentar.WebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.WebAPI.Services
{
    public class TipDonacijeService : BaseReadService<Model.TipDonacije, Database.TipDonacije, object>, ITipDonacijeService
    {
        public TipDonacijeService(BazaPodataka context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
