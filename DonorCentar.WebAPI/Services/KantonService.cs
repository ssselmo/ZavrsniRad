using AutoMapper;
using DonorCentar.WebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.WebAPI.Services
{
    public class KantonService : BaseReadService<Model.Kanton, Database.Kanton, object>, IKantonService
    {
        public KantonService(BazaPodataka context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
