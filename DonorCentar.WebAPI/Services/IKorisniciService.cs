using DonorCentar.Model;
using DonorCentar.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.WebAPI.Services
{
    public interface IKorisniciService
    {
        
            IList<Model.Korisnik> GetAll(KorisniciSearchRequest search);

            Model.Korisnik GetById(int id);

            Model.Korisnik Insert(KorisniciInsertRequest korisnici);

            Model.Korisnik Update(int id, KorisniciUpdateRequest korisnici);
        Task<Korisnik> Login(string username, string password);
        void SetLogiraniKorisnik(Korisnik user);

        Model.Korisnik Profil();

        Model.Korisnik Delete(int id);
        IList<Donacija> GetPreporuka();
    }
}
