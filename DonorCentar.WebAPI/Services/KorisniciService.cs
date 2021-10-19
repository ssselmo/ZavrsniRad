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
using Korisnici = DonorCentar.Model.Korisnik;
using Microsoft.EntityFrameworkCore;

namespace DonorCentar.WebAPI.Services
{
    public class KorisniciService : IKorisniciService
    {
        public BazaPodataka Context { get; set; }
        public Korisnici LogiraniKorisnik { get; private set; }

        protected readonly IMapper _mapper;

        public KorisniciService(BazaPodataka context, IMapper mapper)
        {
            Context = context;
            _mapper = mapper;
        }


        public IList<Korisnici> GetAll(KorisniciSearchRequest search)
        {
            var query = Context.Korisnik.Where(x=>x.Izbrisan==false).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search?.ImePrezime))
            {
                search.ImePrezime = search.ImePrezime.ToLower();
                query = query.Where(x => x.LicniPodaci.Ime.ToLower().Contains(search.ImePrezime) || x.LicniPodaci.Prezime.ToLower().Contains(search.ImePrezime) || (x.LicniPodaci.Ime + " " + x.LicniPodaci.Prezime).ToLower().Contains(search.ImePrezime));

            }

            if(!string.IsNullOrWhiteSpace(search?.TipKorisnika) && search.TipKorisnika!="Svi")
            {
                query = query.Where(x => x.TipKorisnika.Tip == search.TipKorisnika);
            }

            query = query.Include(x => x.Grad.Kanton);
            query = query.Include(x => x.LicniPodaci);
            query = query.Include(x => x.TipKorisnika);
            query = query.Include(x => x.LoginPodaci);

            var entities = query.ToList();
            

            var result = _mapper.Map<IList<Model.Korisnik>>(entities);



            return result;
        }

        public Korisnici GetById(int id)
        {
            var query = Context.Korisnik.AsQueryable();

            query = query.Include(x => x.Grad.Kanton);
            query = query.Include(x => x.LicniPodaci);
            query = query.Include(x => x.TipKorisnika);



            return _mapper.Map<Model.Korisnik>(query.FirstOrDefault(x=>x.Id==id));
        }

        public Model.Korisnik Insert(KorisniciInsertRequest request)
        {
            var entity = _mapper.Map<Database.Korisnik>(request);
            Context.Add(entity);



            entity.LoginPodaci.Sifra = HashSifru(request.LoginPodaci.Sifra);

            var uloga = Context.TipKorisnika.Find(request.TipKorisnikaId);
            if(uloga!=null)
            {
                if(uloga.Tip=="Partner")
                {
                    var partner = new Partner
                    {
                        Korisnik = entity,
                        DatumRegistracije = DateTime.Now
                    };

                    Context.Add(partner);
                }

                if (uloga.Tip == "Primalac")
                {
                    var Primalac = new Primalac
                    {
                        Korisnik = entity,
                        DatumRegistracije = DateTime.Now
                    };

                    Context.Add(Primalac);
                }

                if (uloga.Tip == "Donor")
                {
                    var Donor = new Donor
                    {
                        Korisnik = entity,
                        DatumRegistracije = DateTime.Now
                    };

                    Context.Add(Donor);
                }
            }

        

            Context.SaveChanges();

            
           

            return _mapper.Map<Model.Korisnik>(entity);
        }

        public Korisnici Update(int id, KorisniciUpdateRequest request)
        {
            var entity = Context.Korisnik.Where(x=>x.Id==id).Include(x=>x.LoginPodaci).Include(x => x.LicniPodaci).Include(x => x.Grad.Kanton).FirstOrDefault();
            entity.GradId = request.GradId;
            entity.TipKorisnikaId = request.TipKorisnikaId;
            entity.LicniPodaci.Adresa = request.LicniPodaci.Adresa;
            entity.LicniPodaci.BrojTelefona = request.LicniPodaci.BrojTelefona;
            entity.LicniPodaci.Email = request.LicniPodaci.Email;
       
            entity.LicniPodaci.Ime = request.LicniPodaci.Ime;

            entity.LicniPodaci.Prezime = request.LicniPodaci.Prezime;

            if(request.LicniPodaci.ProfilnaSlika!=null)
            entity.LicniPodaci.ProfilnaSlika = request.LicniPodaci.ProfilnaSlika;

            entity.LoginPodaci.KorisnickoIme = request.LoginPodaci.KorisnickoIme;
            if(!string.IsNullOrEmpty( request.LoginPodaci.Sifra ) )
            entity.LoginPodaci.Sifra = HashSifru(request.LoginPodaci.Sifra);





            Context.SaveChanges();
            return _mapper.Map<Model.Korisnik>(entity);
        }

     

        

        public static string HashSifru(string input)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public async Task<Model.Korisnik> Login(string username, string password)
        {
            var entity = await Context.Korisnik.Where(x => x.Izbrisan == false).Where(x => x.LoginPodaci.KorisnickoIme == username).Include(x => x.Grad.Kanton).Include(x => x.LicniPodaci).Include(x => x.TipKorisnika).Include(x => x.LoginPodaci).FirstOrDefaultAsync();


            if (entity != null)
                if (HashSifru(password) == entity.LoginPodaci.Sifra)
                    return _mapper.Map<Model.Korisnik>(entity);

           throw new UserException("Pogrešan username ili password"); 
        }

        public void SetLogiraniKorisnik(Korisnici user)
        {
            LogiraniKorisnik = user;
        }


        public Korisnici Profil()
        {
            var query = Context.Korisnik.AsQueryable();

            query = query.Include(x => x.Grad.Kanton);
            query = query.Include(x => x.LicniPodaci);
            query = query.Include(x => x.TipKorisnika);


            if (LogiraniKorisnik == null)
                return null;

            return _mapper.Map<Model.Korisnik>(query.FirstOrDefault(x => x.Id==LogiraniKorisnik.Id));
        }


        public Korisnici Delete(int id)
        {
            var entity = Context.Korisnik.Find(id);


            
            if (entity != null)
            {

                entity.Izbrisan = true;
                Context.SaveChanges();
            }

            return _mapper.Map<Model.Korisnik>(entity);
        }

        public IList<Model.Donacija> GetPreporuka()
        {
            var rng = new Random();
            var tipovidonacija = Context.TipDonacije.ToList();
            var donacije = Context.Donacija.Where(x => x.DonorId == LogiraniKorisnik.Id).ToList();
            var list = new List<Donacija>();

            if (donacije.Count()==0)
            {
                

                var primaoci = Context.Primalac.Where(x => x.Verifikovan && !x.Korisnik.Izbrisan).Include(x=>x.Korisnik.LicniPodaci).OrderBy(x => Guid.NewGuid()).Take(3);

                foreach (var item in primaoci)
                {
                    var tipdonacije = tipovidonacija[rng.Next(0, tipovidonacija.Count())];
                    list.Add(new Donacija
                    {
                        PrimalacId = item.KorisnikId,
                        TipDonacijeId = tipdonacije.TipDonacijeId,
                        Primalac=item.Korisnik,
                        TipDonacije=tipdonacije

                    });


                }

            }

            else
            {
               var tip= donacije.GroupBy(x => x.TipDonacijeId).OrderByDescending(x => x.Count()).FirstOrDefault();

                


               var primaoci = Context.Primalac.Where(x=>x.Verifikovan && !x.Korisnik.Izbrisan).Include(x => x.Korisnik.LicniPodaci)
                   .OrderByDescending(x => Context.DojamKorisnik.Count(y=>y.Donacija.PrimalacId==x.KorisnikId && y.Dojam.VrstaDojma=="Pozitivan")).ToList()
                   .Take(3);



                foreach (var item in primaoci)
                {
                    list.Add(new Donacija
                    {
                        PrimalacId = item.KorisnikId,
                        TipDonacijeId = tip.Key.Value,
                        Primalac=item.Korisnik,
                        TipDonacije=Context.TipDonacije.Find(tip.Key.Value)


                    });


                }


            }

            return _mapper.Map<List<Model.Donacija>>(list);
        }
    }
}








