using AutoMapper;
using DonorCentar.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.WebAPI.Mapping
{
    public class DonorCentarProfile : Profile
    {
        public DonorCentarProfile()
        {
            CreateMap<Database.Korisnik, Model.Korisnik>().ReverseMap();
            CreateMap<Database.Administrator, Model.Administrator>().ReverseMap();
            CreateMap<Database.Dojam, Model.Dojam>().ReverseMap();
            CreateMap<Database.DojamKorisnik, Model.DojamKorisnik>().ReverseMap();
            CreateMap<Database.Donacija, Model.Donacija>().ReverseMap();
            CreateMap<Database.Donor, Model.Donor>().ReverseMap();
            CreateMap<Database.Grad, Model.Grad>().ReverseMap();
            CreateMap<Database.InformacijeTransporta, Model.InformacijeTransporta>().ReverseMap();
            CreateMap<Database.Kanton, Model.Kanton>().ReverseMap();
            CreateMap<Database.LicniPodaci, Model.LicniPodaci>().ReverseMap();
            CreateMap<Database.LoginPodaci, Model.LoginPodaci>().ReverseMap();
            CreateMap<Database.Partner, Model.Partner>().ReverseMap();
            CreateMap<Database.Primalac, Model.Primalac>().ReverseMap();
            CreateMap<Database.Status, Model.Status>().ReverseMap();
            CreateMap<Database.TipDonacije, Model.TipDonacije>().ReverseMap();
            CreateMap<Database.TipKorisnika, Model.TipKorisnika>().ReverseMap();
            CreateMap<Database.VrstaDonacije, Model.VrstaDonacije>().ReverseMap();
            CreateMap<Database.Obavijest, Model.Obavijest>().ReverseMap();

            CreateMap<KorisniciInsertRequest, Database.Korisnik>().ReverseMap();
            CreateMap<KorisniciUpdateRequest, Database.Korisnik>().ReverseMap();


            CreateMap<ObavijestUpdateRequest, Database.Obavijest>().ReverseMap();
            CreateMap<ObavijestInsertRequest, Database.Obavijest>().ReverseMap();

           
            CreateMap<DonacijaInsertRequest, Database.Donacija>().ReverseMap();


        }

    }
}
