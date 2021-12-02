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
    public class DonacijeSaTransportomViewModel : BaseViewModel
    {
        private readonly APIService _servicedonacija = new APIService("Donacija");

        public ObservableCollection<Donacija> Donacije { get; set; } = new ObservableCollection<Donacija>();


        public ICommand UcitajCommand { get; }
        public ICommand UkloniCommand { get; }


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


 


        public DonacijeSaTransportomViewModel()
        {

            UcitajCommand = new Command(async () => await UcitajDonacije());
            UkloniCommand = new Command<Donacija>(OnUkloniClicked);


        }


        private async Task UcitajDonacije()
        {
            var list = await _servicedonacija.Get<List<Donacija>>(new DonacijaSearchRequest
            {
                
                Grad = Pretraga,
                PartnerId=APIService.Korisnik.Id
            
            });

            Donacije.Clear();
            foreach (var item in list)
            {
                if(item.DonorId!=null)
                Donacije.Add(item);
            }


        }

        private async void OnUkloniClicked(Donacija obj)
        {

            var entity = await _servicedonacija.Delete<Donacija>(obj.DonacijaId);

            if (entity != null)
                await UcitajDonacije();
        }
    }
}
