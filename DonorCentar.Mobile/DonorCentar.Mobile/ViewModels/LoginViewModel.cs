﻿using DonorCentar.Mobile.Validators;
using DonorCentar.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DonorCentar.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly APIService _servicekorisnici = new APIService("Korisnici");
        private readonly APIService _serviceprimalac = new APIService("Primalac");


        public Command LoginCommand { get; }
        private ValidatableObject<string> korisnickoime = new ValidatableObject<string> ();
        private ValidatableObject<string> password = new ValidatableObject<string>();
        public ICommand RegisterCommand => new Command(OnRegister);


        private void OnRegister()
        {
            Application.Current.MainPage = new RegisterPage();
        }

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

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked()
        {

            
                IsBusy = true;
                APIService.Username = KorisnickoIme.Value;
                APIService.Password = Password.Value;

                try
                {
                    APIService.Korisnik = await _servicekorisnici.Get<Model.Korisnik>(null, "Profil");
                    
                    
                    

                    if (APIService.Korisnik.Tip=="Administrator")
                    {

                    //await Application.Current.MainPage.DisplayAlert("Greška", "Niste autorizovani", "OK");
                    Application.Current.MainPage = new AppShellAdmin();
                }
                    else
                {
                    if(APIService.Korisnik.Tip=="Donor")
                    Application.Current.MainPage = new AppShellDonor();

                    if (APIService.Korisnik.Tip == "Primalac")
                    {
                        var primalac = await _serviceprimalac.GetById<Model.Primalac>(APIService.Korisnik.Id, "GetByKorisnikId");
                        if(primalac.Verifikovan)
                        Application.Current.MainPage = new AppShellPrimalac();
                        else
                            Application.Current.MainPage = new AppShellNeverifikovanPrimalac();

                    }



                    if (APIService.Korisnik.Tip == "Partner")
                        Application.Current.MainPage = new AppShellPartner();


                }
                       
                }
                catch (Exception ex)
                {

                }
            }

        }
    
}
