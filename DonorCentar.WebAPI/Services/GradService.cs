using AutoMapper;
using DonorCentar.Model.Requests;
using DonorCentar.WebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.WebAPI.Services
{
    public class GradService : BaseReadService<Model.Grad, Database.Grad, GradSearchRequest>, IGradService
    {
        public GradService(BazaPodataka context, IMapper mapper) : base(context, mapper)
        {
        }


        public override IEnumerable<Model.Grad> Get(GradSearchRequest search)
        {
            var query = Context.Grad.AsQueryable();
            if(search.KantonID!=0)
            {
                query = query.Where(x => x.KantonId == search.KantonID);

                
            }

            var list = query.ToList();

            return _mapper.Map<List<Model.Grad>>(list);
        }
    }
}
