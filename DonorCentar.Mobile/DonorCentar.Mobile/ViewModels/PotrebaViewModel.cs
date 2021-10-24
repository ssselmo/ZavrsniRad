using DonorCentar.Mobile.Validators;
using DonorCentar.Mobile.Views;
using DonorCentar.Model;
using DonorCentar.Model.Requests;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;



namespace DonorCentar.Mobile.ViewModels
{
    public class PotrebaViewModel : BaseViewModel
    {
        private readonly APIService _servicedonacija = new APIService("Donacija");
        private readonly APIService _servicetip = new APIService("TipDonacije");
       
        private DonacijaInsertRequest donacija = new DonacijaInsertRequest();
        private TipDonacije tipdonacije;

       


        private bool transport;

        public bool Transport
        {
            get { return transport; }
            set { SetProperty(ref transport , value); }
        }


        public TipDonacije TipDonacije
        {
            get { return tipdonacije; }
            set { SetProperty(ref tipdonacije ,value); }
        }

        


        public DonacijaInsertRequest Donacija
        {
            get { return donacija; }
            set { SetProperty(ref donacija, value); }
        }


        private string kolicina;

        public string Kolicina
        {
            get { return kolicina; }
            set { SetProperty(ref kolicina, value); }
        }

        private Models.JedinicaMjere jedinicamjere;

        public Models.JedinicaMjere JedinicaMjere
        {
            get { return jedinicamjere; }
            set { SetProperty(ref jedinicamjere, value); }
        }



        public ObservableCollection<TipDonacije> TipoviDonacija { get; set; } = new ObservableCollection<TipDonacije>();
        public ObservableCollection<Models.JedinicaMjere> JediniceMjere { get; set; } = new ObservableCollection<Models.JedinicaMjere>();


        public ICommand ZatraziCommand { get; }
        public ICommand UcitajCommand { get; }


        private async void OnZatraziClicked()
        {


            if (tipdonacije==null)
            {
                await Application.Current.MainPage.DisplayAlert("Greška", "Potrebno unijeti tip potrebe!", "OK");
                return;

            }

            if (string.IsNullOrEmpty(Donacija.Opis))
            {
                await Application.Current.MainPage.DisplayAlert("Greška", "Potrebno unijeti opis!", "OK");
                return;
            }
            if (jedinicamjere == null)
            {
                await Application.Current.MainPage.DisplayAlert("Greška", "Potrebno unijeti jedinicu mjere!", "OK");
                return;

            }
            if (kolicina == null || !decimal.TryParse(kolicina, out decimal val) || val < 1)
            {
                await Application.Current.MainPage.DisplayAlert("Greška", "Potrebno unijeti količinu!", "OK");
                return;

            }

            Donacija.InformacijeId = transport ? 4 : 1;
            Donacija.TipDonacijeId = tipdonacije.TipDonacijeId;

            Donacija.Kolicina = decimal.Parse(kolicina);

            Donacija.VrstaDonacijeId = 2;
            Donacija.StatusId = 1;
            Donacija.JedinicaMjere = jedinicamjere.jedinicaMjere;
            Donacija.PrimalacId = APIService.Korisnik.Id;

           
          
            

        
            var entity = await _servicedonacija.Insert<Donacija>(Donacija);
            
            if (entity != null)
            {
                Donacija = new DonacijaInsertRequest();

                JedinicaMjere = null;


                TipDonacije = null;
               
                Transport = false;
                await Shell.Current.GoToAsync("///"+nameof(MojePotrebePage));

            }

        }



        public async Task Init()
        {
            await UcitajTipove();
            UcitajJediniceMjere();




        }

        public PotrebaViewModel()
        {
            ZatraziCommand = new Command(OnZatraziClicked);
            UcitajCommand = new Command(async () => await Init());



        }

        private async Task UcitajTipove()
        {
            var list = await _servicetip.Get<List<TipDonacije>>(null);

            TipoviDonacija.Clear();
            foreach (var item in list)
            {

                TipoviDonacija.Add(item);
            }


        }

        private void UcitajJediniceMjere()
        {
            var list = Enum.GetValues(typeof(JedinicaMjere));

            JediniceMjere.Clear();
            foreach (JedinicaMjere item in list)
            {

                JediniceMjere.Add(new Models.JedinicaMjere { jedinicaMjere = item });
            }


        }



    }
}
