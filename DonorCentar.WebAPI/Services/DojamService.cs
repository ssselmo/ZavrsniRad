using AutoMapper;
using DonorCentar.WebAPI.Database;
using DonorCentar.WebAPI.Filters;
using DonorCentar.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DonorCentar.WebAPI.Services
{
    public class DojamService : IDojamService
    {
        public BazaPodataka Context { get; set; }

        protected readonly IMapper _mapper;

        public DojamService(BazaPodataka context, IMapper mapper)
        {
            Context = context;
            _mapper = mapper;
        }


        public IList<Model.Dojam> GetAll()
        {
            var query = Context.Dojam.AsQueryable();

            
            var entities = query.ToList();
            

            var result = _mapper.Map<IList<Model.Dojam>>(entities);



            return result;
        }

        public Model.DojamKorisnik GetById(int id) //donacijaID
        {
            var query = Context.DojamKorisnik.Where(x=>x.DonacijaId==id).AsQueryable();




            return _mapper.Map<Model.DojamKorisnik>(query.FirstOrDefault());
        }

        public Model.DojamKorisnik Insert(Model.DojamKorisnik request)
        {
            var entity = _mapper.Map<Database.DojamKorisnik>(request);

            Context.Add(entity);



           

            Context.SaveChanges();

            
           

            return _mapper.Map<Model.DojamKorisnik>(entity);
        }

        public Model.DojamKorisnik Update(int id, Model.DojamKorisnik request)
        {
            var entity = Context.DojamKorisnik.Where(x=>x.DonacijaId==id).FirstOrDefault();

            _mapper.Map(request, entity);
                


            Context.SaveChanges();
            return _mapper.Map<Model.DojamKorisnik>(entity);
        }

     

        

       

       
    }
}








