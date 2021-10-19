
using Acr.UserDialogs;
using DonorCentar.Mobile.Models;
using DonorCentar.Mobile.Views;
using DonorCentar.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Stripe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DonorCentar.Mobile.ViewModels
{
    public class PaymentGatewayViewModel : BindableBase
    {

        #region Variable
        private readonly APIService _serviceprimalac = new APIService("Primalac");
        private readonly APIService _servicedonacija = new APIService("Donacija");


        private CreditCardModel _creditCardModel;
        private TokenService Tokenservice;
        private Token stripeToken;
      
        private bool _isCarcValid;
        private bool _isTransectionSuccess;
        private string _expMonth;
        private string _expYear;
        private string _title;

        #endregion Variable

        #region Public Property
        

        private Primalac primalac;

        public Primalac Primalac
        {
            get { return primalac; }
            set { SetProperty(ref primalac, value); }
        }
        public string ExpMonth
        {
            get { return _expMonth; }
            set { SetProperty(ref _expMonth, value); }
        }

        public bool IsCarcValid
        {
            get { return _isCarcValid; }
            set { SetProperty(ref _isCarcValid, value); }
        }

        public bool IsTransectionSuccess
        {
            get { return _isTransectionSuccess; }
            set { SetProperty(ref _isTransectionSuccess, value); }
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public string ExpYear
        {
            get { return _expYear; }
            set { SetProperty(ref _expYear, value); }
        }

        public CreditCardModel CreditCardModel
        {
            get { return _creditCardModel; }
            set { SetProperty(ref _creditCardModel, value); }
        }

        private string iznos;

        public string Iznos
        {
            get { return iznos; }
            set { SetProperty(ref iznos, value); }
        }


        public ObservableCollection<Primalac> Primaoci { get; set; } = new ObservableCollection<Primalac>();
        #endregion Public Property

        #region Constructor

        public PaymentGatewayViewModel()
        {
            CreditCardModel = new CreditCardModel();
            Title = "Card Details";
        }

        #endregion Constructor

        #region Command

        public DelegateCommand SubmitCommand => new DelegateCommand(async () =>
        {

            if (primalac == null)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Greška", "Potrebno unijeti primaoca!", "OK");
                return;
            }

            CreditCardModel.ExpMonth = Convert.ToInt64(ExpMonth);
            CreditCardModel.ExpYear = Convert.ToInt64(ExpYear);
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;
            try
            {
                UserDialogs.Instance.ShowLoading("Procesiranje..");
                await Task.Run(async () =>
                {

                    var Token = CreateToken();
                    Console.Write("Payment Gateway" + "Token :" + Token);
                    if (Token != null)
                    {
                        IsTransectionSuccess = MakePayment(Token);
                    }
                    else
                    {
                        UserDialogs.Instance.Alert("Bad card credentials", null, "OK");
                    }
                });
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert(ex.Message, null, "OK");
              
            }
            finally
            {
                if (IsTransectionSuccess)
                {
                    

                    UserDialogs.Instance.HideLoading();
                    

                    var Donacija = new Model.Requests.DonacijaInsertRequest();



                    Donacija.InformacijeId = 1;
                    Donacija.TipDonacijeId = 3;
                    Donacija.PrimalacId = primalac.KorisnikId;
                    Donacija.VrstaDonacijeId = 1;
                    Donacija.StatusId =  5;
                    Donacija.JedinicaMjere = Model.JedinicaMjere.KM;
                    Donacija.Kolicina = decimal.Parse(Iznos);
                    Donacija.Opis = "Online donacija novca";


                    Donacija.DonorId = APIService.Korisnik.Id;
                    var entity = await _servicedonacija.Insert<Donacija>(Donacija);

                    if (entity != null)
                    {
                        
                        await Shell.Current.GoToAsync("///" + nameof(MojeDonacijePage));

                    }

                }
                else
                {

                    UserDialogs.Instance.HideLoading();
                    UserDialogs.Instance.Alert("Oops, something went wrong", "Payment failed", "OK");
                    
                }
            }

        });

        #endregion Command

        #region Method

        private string CreateToken()
        {
            try
            {
                var service = new ChargeService();
                var Tokenoptions = new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Number = CreditCardModel.Number,
                        ExpYear = CreditCardModel.ExpYear,
                        ExpMonth = CreditCardModel.ExpMonth,
                        Cvc = CreditCardModel.Cvc,
                        Name = APIService.Korisnik.Ime,
                        /*AddressLine1 = "18",
                        AddressLine2 = "SpringBoard",
                        AddressCity = "Gurgoan",
                        AddressZip = "284005",
                        AddressState = "Haryana",
                        AddressCountry = "India",*/
                        Currency = "USD",
                    }
                };

                Tokenservice = new TokenService();
                stripeToken = Tokenservice.Create(Tokenoptions);
                return stripeToken.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task UcitajPrimaoce()
        {
            var list = await _serviceprimalac.Get<List<Primalac>>(null);

            Primaoci.Clear();
            foreach (var item in list)
            {

                Primaoci.Add(item);
            }


        }

        public bool MakePayment(string token)
        {
            try
            {
                var options = new ChargeCreateOptions
                {
                    Amount = (long)(decimal.Parse(Iznos) * 100m),
                    Currency = "USD",
                    Description = $"Donacija za {Primalac.Ime} {Primalac.Prezime}",
                    Source = stripeToken.Id,
                    Capture = true,
                };
                //Make Payment
                var service = new ChargeService();
                Charge charge = service.Create(options);
                return true;
            }
            catch (Exception ex)
            {
                Console.Write("Payment Gatway (CreateCharge)" + ex.Message);
                throw ex;
            }
        }

       

        #endregion Method
    }
}