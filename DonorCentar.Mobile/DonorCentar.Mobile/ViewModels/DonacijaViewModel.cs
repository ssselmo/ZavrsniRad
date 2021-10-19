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
    [QueryProperty(nameof(PrimalacId), nameof(PrimalacId))]
    [QueryProperty(nameof(TipDonacijeId), nameof(TipDonacijeId))]
    public class DonacijaViewModel : BaseViewModel
    {
        private readonly APIService _servicedonacija = new APIService("Donacija");
        private readonly APIService _servicetip = new APIService("TipDonacije");
        private readonly APIService _serviceprimalac = new APIService("Primalac");
        private DonacijaInsertRequest donacija = new DonacijaInsertRequest();
        private TipDonacije tipdonacije;

        private int primalacId;

        public int PrimalacId
        {
            get { return primalacId; }
            set { SetProperty(ref primalacId ,value); }
        }

        private int tipdonacijeId;
                    
        public int TipDonacijeId
        {
            get { return tipdonacijeId; }
            set { SetProperty(ref tipdonacijeId , value); }
        }



        private string kolicina;

        public string Kolicina
        {
            get { return kolicina; }
            set { SetProperty(ref kolicina, value); }
        }

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

        private Primalac primalac;

        public Primalac Primalac
        {
            get { return primalac; }
            set { SetProperty(ref primalac ,value); }
        }


        public DonacijaInsertRequest Donacija
        {
            get { return donacija; }
            set { SetProperty(ref donacija, value); }
        }

        private Models.JedinicaMjere jedinicamjere;

        public Models.JedinicaMjere JedinicaMjere
        {
            get { return jedinicamjere; }
            set { SetProperty(ref jedinicamjere , value); }
        }




        public ObservableCollection<TipDonacije> TipoviDonacija { get; set; } = new ObservableCollection<TipDonacije>();
        public ObservableCollection<Primalac> Primaoci { get; set; } = new ObservableCollection<Primalac>();

        public ObservableCollection<Models.JedinicaMjere> JediniceMjere { get; set; } = new ObservableCollection<Models.JedinicaMjere>();
        public ICommand DonirajCommand { get; }

        private async void OnDonirajClicked()
        {

            if(primalac==null)
            {
                await Application.Current.MainPage.DisplayAlert("Greška", "Potrebno unijeti primaoca!", "OK");
                return;
            }
            if (tipdonacije == null)
            {
                await Application.Current.MainPage.DisplayAlert("Greška", "Potrebno unijeti tip donacije!", "OK");
                return;

            }
            if (jedinicamjere == null)
            {
                await Application.Current.MainPage.DisplayAlert("Greška", "Potrebno unijeti jedinicu mjere!", "OK");
                return;

            }
            if (kolicina == null || !decimal.TryParse(kolicina,out decimal val) || val<1)
            {
                await Application.Current.MainPage.DisplayAlert("Greška", "Potrebno unijeti količinu!", "OK");
                return;

            }

            if( string.IsNullOrEmpty( Donacija.Opis))
            {
                await Application.Current.MainPage.DisplayAlert("Greška", "Potrebno unijeti opis!", "OK");
                return;
            }


            Donacija.Kolicina = decimal.Parse(kolicina);
            Donacija.InformacijeId = transport ? 4 : 1;
            Donacija.TipDonacijeId = tipdonacije.TipDonacijeId;
            Donacija.PrimalacId = primalac.KorisnikId;
            Donacija.VrstaDonacijeId = 1;
            Donacija.StatusId =  5 ;
            Donacija.JedinicaMjere = jedinicamjere.jedinicaMjere;
            
           

            Donacija.DonorId = APIService.Korisnik.Id;
            var entity = await _servicedonacija.Insert<Donacija>(Donacija);
            
            if (entity != null)
            {
                Donacija = new DonacijaInsertRequest();
                JedinicaMjere = null;
                
              
                TipDonacije = null;
                Primalac = null;
                Transport = false;
                await Shell.Current.GoToAsync("///"+nameof(MojeDonacijePage));

            }




        }




        public async Task Init()
        {
            await UcitajTipove();
            await UcitajPrimaoce();
            UcitajJediniceMjere();



        }

        public DonacijaViewModel()
        {
            DonirajCommand = new Command(OnDonirajClicked);



        }

        private async Task UcitajTipove()
        {
            var list = await _servicetip.Get<List<TipDonacije>>(null);

            TipoviDonacija.Clear();
            foreach (var item in list)
            {

                TipoviDonacija.Add(item);

                if (tipdonacijeId != 0 && item.TipDonacijeId==tipdonacijeId)
                {
                    TipDonacije = item;
                }
            }

            


        }

        private async Task UcitajPrimaoce()
        {
            var list = await _serviceprimalac.Get<List<Primalac>>(null);

            Primaoci.Clear();
            foreach (var item in list)
            {

                Primaoci.Add(item);

                if (primalacId != 0 && item.KorisnikId == primalacId)
                {
                    Primalac = item;
                }
            }


        }

        private void UcitajJediniceMjere()
        {
            var list = Enum.GetValues(typeof(JedinicaMjere));

            JediniceMjere.Clear();
            foreach (JedinicaMjere item in list)
            {

                JediniceMjere.Add(new Models.JedinicaMjere { jedinicaMjere = item }) ;
            }


        }
     

    }
}
