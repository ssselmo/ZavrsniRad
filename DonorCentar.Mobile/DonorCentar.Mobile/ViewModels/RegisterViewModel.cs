using DonorCentar.Mobile.Validators;
using DonorCentar.Mobile.Validators.Rules;
using DonorCentar.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DonorCentar.Mobile.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly APIService _servicekorisnici = new APIService("Korisnici");
        private readonly APIService _servicegradovi = new APIService("Gradovi");
        private readonly APIService _servicetipkorisnika = new APIService("TipKorisnika");

        private ValidatableObject<string> email = new ValidatableObject<string>();
        private ValidatableObject<string> korisnickoime = new ValidatableObject<string>();
        private ValidatableObject<string> password = new ValidatableObject<string>();
        private ValidatableObject<string> ime = new ValidatableObject<string>();
        private ValidatableObject<string> prezime = new ValidatableObject<string>();

        private ValidatableObject<string> adresa = new ValidatableObject<string>();
        private ValidatableObject<string> brojtelefona = new ValidatableObject<string>();

        private Model.Grad odabranigrad;
        public ObservableCollection<Model.Grad> Gradovi { get; set; } = new ObservableCollection<Model.Grad>();

        private Model.TipKorisnika odabranitipkorisnika;

        public RegisterViewModel()
        {
            AddValidationRules();
        }

        public ObservableCollection<Model.TipKorisnika> TipoviKorisnika { get; set; } = new ObservableCollection<Model.TipKorisnika>();

        public ICommand LoginCommand => new Command(OnLogin);
        public ICommand RegisterCommand => new Command(OnRegister);


        public async Task Init()
        {
            var listgradovi = await _servicegradovi.Get<List<Model.Grad>>(null);
            foreach (var item in listgradovi)
            {
                Gradovi.Add(item);

            }

            var listtipkorisnika = await _servicetipkorisnika.Get<List<Model.TipKorisnika>>(null);
            foreach (var item in listtipkorisnika)
            {
                if(item.Tip!="Administrator")
                TipoviKorisnika.Add(item);

            }
        }

        private async void OnRegister()
        {
            if (!AreFieldsValid())
                return;
            if (OdabraniTipKorisnika == null)
            {
                await Application.Current.MainPage.DisplayAlert("Greška", "Potrebno unijeti tip korisnika!", "OK");
                return;

            }
            if (OdabraniGrad == null)
            {
                await Application.Current.MainPage.DisplayAlert("Greška", "Potrebno unijeti grad!", "OK");
                return;

            }
            Model.Requests.KorisniciInsertRequest request = new Model.Requests.KorisniciInsertRequest
            {
                
                GradId = odabranigrad.Id,
                
                LicniPodaci = new Model.LicniPodaci
                {
                    Adresa = adresa.Value,
                    BrojTelefona = brojtelefona.Value,
                    Email = email.Value,
                    Ime = ime.Value,
                    Prezime = prezime.Value


                },
                LoginPodaci = new Model.LoginPodaci
                {
                    Sifra = password.Value,
                    KorisnickoIme = korisnickoime.Value
                },
                TipKorisnikaId = odabranitipkorisnika.Id

            };

            var entity = await _servicekorisnici.Insert<Model.Korisnik> (request);

            if(entity!=null)
            {
                await Application.Current.MainPage.DisplayAlert("", "Registracija uspješna.", "OK");
                APIService.Username = korisnickoime.Value;
                APIService.Password = password.Value;
                APIService.Korisnik = entity;

                if (APIService.Korisnik.Tip == "Donor")
                    Application.Current.MainPage = new AppShellDonor();

                if (APIService.Korisnik.Tip == "Primalac")
                    Application.Current.MainPage = new AppShellPrimalac();

                if (APIService.Korisnik.Tip == "Partner")
                    Application.Current.MainPage = new AppShellPartner();
              
            }

        }

        public bool AreFieldsValid()
        {
            bool isEmailValid = this.Email.Validate();
            bool isPasswordValid = this.Password.Validate();
            bool isAdresaValid = this.Adresa.Validate();
            bool isBrojValid = this.BrojTelefona.Validate();
            bool isImeValid = this.Ime.Validate();

            bool isKImeValid = this.KorisnickoIme.Validate();
            return isEmailValid && isPasswordValid && isAdresaValid && isBrojValid &&isImeValid  &&isKImeValid;
        }

        

     
        private void AddValidationRules()
        {
            this.Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password potreban" });
            this.Adresa.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Adresa potrebna" });
            this.Email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Email potreban" });
            this.BrojTelefona.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Broj telefona potreban" });
            this.Ime.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Ime potrebno" });
            this.KorisnickoIme.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Korisničko ime potrebno" });
           
        }

        private void OnLogin()
        {
            Application.Current.MainPage = new LoginPage();
        }


        #region properties
        public ValidatableObject<string> Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (this.password == value)
                {
                    return;
                }

                this.SetProperty(ref this.password, value);
            }
        }
        public ValidatableObject<string> Email
        {
            get
            {
                return this.email;
            }

            set
            {
                if (this.email == value)
                {
                    return;
                }

                this.SetProperty(ref this.email, value);
            }
        }
        public ValidatableObject<string> Ime
        {
            get
            {
                return this.ime;
            }

            set
            {
                if (this.ime == value)
                {
                    return;
                }

                this.SetProperty(ref this.ime, value);
            }
        }
        public ValidatableObject<string> Prezime
        {
            get
            {
                return this.prezime;
            }

            set
            {
                if (this.prezime == value)
                {
                    return;
                }

                this.SetProperty(ref this.prezime, value);
            }
        }
        public ValidatableObject<string> Adresa
        {
            get
            {
                return this.adresa;
            }

            set
            {
                if (this.adresa == value)
                {
                    return;
                }

                this.SetProperty(ref this.adresa, value);
            }
        }
        public ValidatableObject<string> BrojTelefona
        {
            get
            {
                return this.brojtelefona;
            }

            set
            {
                if (this.brojtelefona == value)
                {
                    return;
                }

                this.SetProperty(ref this.brojtelefona, value);
            }
        }

        public Model.Grad OdabraniGrad
        {
            get
            {
                return this.odabranigrad;
            }

            set
            {
                if (this.odabranigrad == value)
                {
                    return;
                }

                this.SetProperty( ref this.odabranigrad ,value);
            }
        }
        public Model.TipKorisnika OdabraniTipKorisnika
        {
            get
            {
                return this.odabranitipkorisnika;
            }

            set
            {
                if (this.odabranitipkorisnika == value)
                {
                    return;
                }

                this.SetProperty(ref this.odabranitipkorisnika, value);
            }
        }

        public ValidatableObject<string> KorisnickoIme
        {
            get
            {
                return this.korisnickoime;
            }

            set
            {
                if (this.korisnickoime == value)
                {
                    return;
                }

                this.SetProperty(ref this.korisnickoime, value);
            }
        }
        #endregion 


       
    }
}
