using DonorCentar.WebAPI.Database;
using DonorCentar.WebAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DonorCentar.WebAPI
{
    public class DatabaseInitializer
    {
        public static void Initialize(BazaPodataka context)
        {
            context.Database.Migrate();

            if (context.Korisnik.Any()) // Database is not empty, don't add on top of existing data
                return;

            var kantoni = new List<Kanton>
            {
                new Kanton
                {
                    Naziv="Unsko-sanski kanton"
                },
                new Kanton
                {
                    Naziv="Posavski kanton"
                },
                new Kanton
                {
                    Naziv="Tuzlanski kanton"
                },
                new Kanton
                {
                     Naziv="Zeničko-dobojski kanton"
                },
                new Kanton
                {
                     Naziv="Bosansko-podrinjski kanton Goražde"
                },
                new Kanton
                {
                     Naziv="Srednjobosanski kanton"
                },
                new Kanton
                {
                     Naziv="Hercegovačko-neretvanski kanton"
                },
                new Kanton
                {
                     Naziv="Zapadnohercegovački kanton"
                },
                new Kanton
                {
                     Naziv="Kanton Sarajevo"
                },
                new Kanton
                {
                     Naziv="Kanton 10"
                }
            };
            context.Kanton.AddRange(kantoni);
            context.SaveChanges();

            var gradovi = new List<Grad>
            {
                new Grad
                {
                    Naziv="Bihać",
                    Kanton = kantoni.ElementAt(0)
                },
                new Grad
                {
                    Naziv="Orašje",
                    Kanton = kantoni.ElementAt(1)
                },
                new Grad
                {
                    Naziv="Tuzla",
                    Kanton = kantoni.ElementAt(2)
                },
                new Grad
                {
                    Naziv="Zenica",
                    Kanton = kantoni.ElementAt(3)
                },
                new Grad
                {
                    Naziv="Goražde",
                    Kanton = kantoni.ElementAt(4)
                },
                new Grad
                {
                    Naziv="Travnik",
                    Kanton = kantoni.ElementAt(5)
                },
                new Grad
                {
                    Naziv="Mostar",
                    Kanton = kantoni.ElementAt(6)
                },
                new Grad
                {
                    Naziv="Široki Brijeg",
                    Kanton = kantoni.ElementAt(7)
                },
                new Grad
                {
                    Naziv="Sarajevo",
                    Kanton = kantoni.ElementAt(8)
                },
                new Grad
                {
                    Naziv="Livno",
                    Kanton = kantoni.ElementAt(9)
                },

            };
            context.Grad.AddRange(gradovi);
            context.SaveChanges();

            var statusi = new List<Status>
            {new Status
                {
                    Opis="Nova"
                },
                 new Status
                {
                    Opis="Bez primaoca"
                },
                 new Status
                {
                    Opis="Bez donora"
                },
                 new Status
                {
                    Opis="Bez transporta"
                },
                new Status
                {
                    Opis="Završena"
                },
                 new Status
                {
                    Opis="Zatražena"
                }
               
            };
            context.Status.AddRange(statusi);
            context.SaveChanges();

            var tipovidonacija = new List<TipDonacije>
            {new TipDonacije
                {
                    Tip="Odjeća"
                },
                 new TipDonacije
                {
                    Tip="Hrana"
                },
                 new TipDonacije
                {
                    Tip="Novac"
                },
                 new TipDonacije
                {
                    Tip="Lijekovi"
                },
                new TipDonacije
                {
                    Tip="Ostalo"
                }

            };
            context.TipDonacije.AddRange(tipovidonacija);
            context.SaveChanges();

            var tipovikorisnika = new List<TipKorisnika>
            {new TipKorisnika
             {
                    Tip="Donor"
                },
                 new TipKorisnika
                {
                    Tip="Primalac"
                },
                 new TipKorisnika
                {
                    Tip="Partner"
                },
                 new TipKorisnika
                {
                    Tip="Administrator"
                },
               

            };
            context.TipKorisnika.AddRange(tipovikorisnika);
            context.SaveChanges();

            var vrstedonacija = new List<VrstaDonacije>
            {new VrstaDonacije
             
                {
                    Vrsta="Donacija"
                },
                 new VrstaDonacije
                {
                    Vrsta="Potreba"
                }
                


            };
            context.VrstaDonacije.Add(vrstedonacija[0]);
            context.SaveChanges();
            context.VrstaDonacije.Add(vrstedonacija[1]);
            context.SaveChanges();

            var obavijesti = new List<Obavijest>
            {new Obavijest

                {
                    Naslov="Nova organizacija",
                    Sadrzaj="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis ac lacinia urna. Donec nulla turpis, rutrum nec arcu bibendum, facilisis iaculis dolor.",
                    Vrijeme=new DateTime(2021,9,8)
                },
                 new Obavijest
                {
                    Naslov="Pomoć",
                    Sadrzaj="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis ac lacinia urna. Donec nulla turpis, rutrum nec arcu bibendum, facilisis iaculis dolor.",
                    Vrijeme=new DateTime(2021,9,8)
                },
                  new Obavijest
                {
                    Naslov="Nova",
                    Sadrzaj="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis ac lacinia urna. Donec nulla turpis, rutrum nec arcu bibendum, facilisis iaculis dolor.",
                    Vrijeme=new DateTime(2021,9,8)
                }



            };
            context.Obavijest.AddRange(obavijesti);
            context.SaveChanges();

            var informacije = new List<InformacijeTransporta>
            {new InformacijeTransporta

                {
                    Opis="Obezbjeđujem transport"
                },
                new InformacijeTransporta
                {
                    Opis="Imam transport"
                },
                new InformacijeTransporta
                {
                    Opis="Nemam transport"
                },
                 new InformacijeTransporta
                {
                    Opis="Ne obezbjeđujem transport"
                }



            };
            context.InformacijeTransporta.AddRange(informacije);
            context.SaveChanges();

            var dojmovi = new List<Dojam>
            {new Dojam

                {
                    VrstaDojma="Pozitivan"
                },
                new Dojam
                {
                    VrstaDojma="Neutralan"
                },
                new Dojam
                {
                    VrstaDojma="Negativan"
                }


            };
            context.Dojam.AddRange(dojmovi);
            context.SaveChanges();

            #region Users
            
var admin = new Administrator
            {
                Korisnik= new Korisnik
                {
                    Grad=gradovi.ElementAt(1),
                    TipKorisnika=tipovikorisnika.ElementAt(3),
                    LicniPodaci=new LicniPodaci
                    {
                        Adresa= "Marijin Dvor",
                        BrojTelefona= "062578965",
                        Email= "selma.mujan@edu.fit.ba",
                        Ime="Selma",
                        Prezime="Mujan",
                        ProfilnaSlika=File.ReadAllBytes("profilepic.png"),
                        
                    },
                    LoginPodaci= new LoginPodaci
                    {
                       KorisnickoIme= "admin",
                       Sifra=KorisniciService.HashSifru("1234")
                    }
                    
                }
                

          };
            context.Administrator.Add(admin);
            context.SaveChanges();

            var primalac = new Primalac
            {
                Korisnik = new Korisnik
                {
                    Grad = gradovi.ElementAt(2),
                    TipKorisnika = tipovikorisnika.ElementAt(1),
                    LicniPodaci = new LicniPodaci
                    {
                        Adresa = "Potoci bb",
                        BrojTelefona = "062578965",
                        Email = "sef.losi@edu.fit.ba",
                        Ime = "Ammar",
                        Prezime = "Mičijević",
                        ProfilnaSlika = File.ReadAllBytes("profilepic.png"),

                    },
                    LoginPodaci = new LoginPodaci
                    {
                        KorisnickoIme = "primalac",
                        Sifra = KorisniciService.HashSifru("1234")
                    }

                },
                DokumentVerifikacije = File.ReadAllBytes("profilepic.png"),
                Verifikovan=true,
                DatumRegistracije=DateTime.Now

            };
            context.Primalac.Add(primalac);
            context.SaveChanges();

            var primalac2 = new Primalac
            {
                Korisnik = new Korisnik
                {
                    Grad = gradovi.ElementAt(3),
                    TipKorisnika = tipovikorisnika.ElementAt(1),
                    LicniPodaci = new LicniPodaci
                    {
                        Adresa = "Mostar bb",
                        BrojTelefona = "061578965",
                        Email = "sef@edu.fit.ba",
                        Ime = "Alem",
                        Prezime = "Bećić",
                        ProfilnaSlika = File.ReadAllBytes("profilepic.png"),
                        

                    },
                    LoginPodaci = new LoginPodaci
                    {
                        KorisnickoIme = "primalac2",
                        Sifra = KorisniciService.HashSifru("1234")
                    }

                },
                DokumentVerifikacije= File.ReadAllBytes("profilepic.png"),
                DatumRegistracije = DateTime.Now,
                Verifikovan = true

            };
            context.Primalac.Add(primalac2);
            context.SaveChanges();

            var primalac3 = new Primalac
            {
                Korisnik = new Korisnik
                {
                    Grad = gradovi.ElementAt(3),
                    TipKorisnika = tipovikorisnika.ElementAt(1),
                    LicniPodaci = new LicniPodaci
                    {
                        Adresa = "Vinova 45",
                        BrojTelefona = "061578965",
                        Email = "crveniosmijeh@edu.fit.ba",
                        Ime = "Crveni Osmijeh",
                        ProfilnaSlika = File.ReadAllBytes("profilepic.png"),


                    },
                    LoginPodaci = new LoginPodaci
                    {
                        KorisnickoIme = "primalac3",
                        Sifra = KorisniciService.HashSifru("1234")
                    }

                },
                DokumentVerifikacije = File.ReadAllBytes("profilepic.png"),
                DatumRegistracije = DateTime.Now,
                Verifikovan=true

            };
            context.Primalac.Add(primalac3);
            context.SaveChanges();

            var donor = new Donor
            {
                Korisnik = new Korisnik
                {
                    Grad = gradovi.ElementAt(3),
                    TipKorisnika = tipovikorisnika.ElementAt(0),
                    LicniPodaci = new LicniPodaci
                    {
                        Adresa = "Alekse Šantića 5",
                        BrojTelefona = "062578969",
                        Email = "rijad.t@edu.fit.ba",
                        Ime = "Rijad",
                        Prezime = "Tinjak",
                        ProfilnaSlika = File.ReadAllBytes("profilepic.png"),

                    },
                    LoginPodaci = new LoginPodaci
                    {
                        KorisnickoIme = "donor",
                        Sifra = KorisniciService.HashSifru("1234")
                    }

                },
                DatumRegistracije = DateTime.Now

            };
            context.Donor.Add(donor);
            context.SaveChanges();

            var partner = new Partner
            {
                Korisnik = new Korisnik
                {
                    Grad = gradovi.ElementAt(4),
                    TipKorisnika = tipovikorisnika.ElementAt(2),
                    LicniPodaci = new LicniPodaci
                    {
                        Adresa = "Zalik 35",
                        BrojTelefona = "062578769",
                        Email = "ilho.bot@edu.fit.ba",
                        Ime = "Ilhan",
                        Prezime = "Music",
                        ProfilnaSlika = File.ReadAllBytes("profilepic.png"),

                    },
                    LoginPodaci = new LoginPodaci
                    {
                        KorisnickoIme = "partner",
                        Sifra = KorisniciService.HashSifru("1234")
                    }

                },
                DatumRegistracije = DateTime.Now

            };
            context.Partner.Add(partner);
            context.SaveChanges();


            var donacija = new Donacija
            {
                Donor=donor.Korisnik,
                Informacije=informacije.ElementAt(0),
                Kolicina=2m,
                JedinicaMjere=JedinicaMjere.kom,
                TipDonacije=tipovidonacija.ElementAt(0),
                Opis="Dvije majice",
                Primalac=primalac.Korisnik,
                Status=statusi.ElementAt(4),
                Transport=partner.Korisnik,
                VrstaDonacije=vrstedonacija.ElementAt(0)
                


            };
            context.Donacija.Add(donacija);
            context.SaveChanges();

            var donacija2 = new Donacija
            {
                Donor = donor.Korisnik,
                Informacije = informacije.ElementAt(0),
                Kolicina = 2m,
                JedinicaMjere = JedinicaMjere.kom,
                TipDonacije = tipovidonacija.ElementAt(1),
                Opis = "Dva hljeba",
                Primalac = primalac.Korisnik,
                Status = statusi.ElementAt(4),
                VrstaDonacije = vrstedonacija.ElementAt(0)



            };
            context.Donacija.Add(donacija2);
            context.SaveChanges();

            var potreba = new Donacija
            {
                
                Informacije = informacije.ElementAt(0),
                Kolicina = 2m,
                JedinicaMjere = JedinicaMjere.kom,
                TipDonacije = tipovidonacija.ElementAt(0),
                Opis = "Dvije jakne",
                Primalac = primalac.Korisnik,
                Status = statusi.ElementAt(4),
                VrstaDonacije = vrstedonacija.ElementAt(1)



            };
            context.Donacija.Add(potreba);
            context.SaveChanges();

            var potreba2 = new Donacija
            {

                Informacije = informacije.ElementAt(0),
                Kolicina = 100m,
                JedinicaMjere = JedinicaMjere.KM,
                TipDonacije = tipovidonacija.ElementAt(2),
                Opis = "u KM",
                Primalac = primalac2.Korisnik,
                Status = statusi.ElementAt(4),
                VrstaDonacije = vrstedonacija.ElementAt(1)



            };
            context.Donacija.Add(potreba2);
            context.SaveChanges();


            var dojamkorisnik = new DojamKorisnik
            {
                Korisnik=donor.Korisnik,
                Datum=DateTime.UtcNow,
                Dojam=dojmovi.ElementAt(0),
                Opis="Sve ok!",
                Donacija=donacija
                

            };
            context.DojamKorisnik.Add(dojamkorisnik);
            context.SaveChanges();

            var dojamkorisnik2 = new DojamKorisnik
            {
                Korisnik = donor.Korisnik,
                Datum = DateTime.UtcNow,
                Dojam = dojmovi.ElementAt(0),
                Opis = "Sve ok!",
                Donacija = donacija2,
                


            };
            context.DojamKorisnik.Add(dojamkorisnik2);
            context.SaveChanges();

            #endregion


        }
    
}
}
