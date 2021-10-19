using AutoMapper;
using DonorCentar.Model.Requests;
using DonorCentar.WebAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.WebAPI.Services
{


    public class ObavijestService : BaseCRUDService<Model.Obavijest, Database.Obavijest, ObavijestSearchRequest, ObavijestInsertRequest, ObavijestUpdateRequest>, IObavijestService
    {
        public ObavijestService(BazaPodataka context, IMapper mapper)
            : base(context, mapper)
        {
        }


        public override IEnumerable<Model.Obavijest> Get(ObavijestSearchRequest search = null)
        {

            var query = Context.Obavijest.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search?.Naslov))
            {
                search.Naslov = search.Naslov.ToLower();
                query = query.Where(x => x.Naslov.ToLower().Contains(search.Naslov));
            }

           

     

            var entities = query.ToList();


            var result = _mapper.Map<IList<Model.Obavijest>>(entities);



            return result;
        }


    }
}