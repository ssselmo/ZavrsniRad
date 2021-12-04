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
    public class DonacijeBezTransportaViewModel : BaseViewModel
    {
        private readonly APIService _servicedonacija = new APIService("Donacija");

        public ObservableCollection<Donacija> Donacije { get; set; } = new ObservableCollection<Donacija>();
        public ICommand PrihvatiCommand { get; }


        public ICommand UcitajCommand { get; }


        private string pretraga;

        public string Pretraga
        {
            get { return pretraga; }
            set
            {
                SetProperty(ref pretraga, value);
                UcitajCommand.Execute(null);
            }
        }


 


        public DonacijeBezTransportaViewModel()
        {

            PrihvatiCommand = new Command<Donacija>(OnPrihvatiClicked);
            UcitajCommand = new Command(async () => await UcitajDonacije());


        }
        private async void OnPrihvatiClicked(Donacija obj)
        {
            obj.InformacijeId = 4;
            obj.TransportId = APIService.Korisnik.Id;
            var entity = await _servicedonacija.Update<Donacija>(obj.DonacijaId, obj);

            if (entity != null)
                await UcitajDonacije();

        }

        private async Task UcitajDonacije()
        {
            var list = await _servicedonacija.Get<List<Donacija>>(new DonacijaSearchRequest
            {
                Grad=Pretraga,
                PartnerId=0,
                Vrsta="Donacija"
            
            });

            Donacije.Clear();
            foreach (var item in list)
            {

                Donacije.Add(item);
            }


        }
    }
}
