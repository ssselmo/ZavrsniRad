using AutoMapper;
using DonorCentar.Model.Requests;
using DonorCentar.WebAPI.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.WebAPI.Services
{
    public class DonorService : BaseReadService<Model.Donor, Database.Donor, DonorSearchRequest>, IDonorService
    {
        public DonorService(BazaPodataka context, IMapper mapper) : base(context, mapper)
        {
        }




      



        public override IEnumerable<Model.Donor> Get(DonorSearchRequest search)
        {
            var query = Context.Donor.Where(x => x.Korisnik.Izbrisan == false).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search?.ImePrezime))
            {
                search.ImePrezime = search.ImePrezime.ToLower();
                query = query.Where(x => x.Korisnik.LicniPodaci.Ime.ToLower().Contains(search.ImePrezime) || x.Korisnik.LicniPodaci.Prezime.ToLower().Contains(search.ImePrezime) || (x.Korisnik.LicniPodaci.Ime + " " + x.Korisnik.LicniPodaci.Prezime).ToLower().Contains(search.ImePrezime));

            }

          

            query = query.Include(x => x.Korisnik.Grad.Kanton);
            query = query.Include(x => x.Korisnik.LicniPodaci);
            query = query.Include(x => x.Korisnik.TipKorisnika);
            query = query.Include(x => x.Korisnik.LoginPodaci);

            var entities = query.ToList();


            var result = _mapper.Map<IEnumerable<Model.Donor>>(entities);



            return result;
        }
    }
}
