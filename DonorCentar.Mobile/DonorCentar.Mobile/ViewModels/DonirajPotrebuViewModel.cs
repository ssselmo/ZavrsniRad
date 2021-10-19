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
    public class DonirajPotrebuViewModel : BaseViewModel
    {
        private readonly APIService _servicedonacija = new APIService("Donacija");
        public ObservableCollection<Donacija> Donacije { get; set; } = new ObservableCollection<Donacija>();
        public ICommand InfoCommand { get; }
       
        public ICommand DonirajCommand { get; }

        private async void OnDonirajClicked(Donacija obj)
        {
            obj.DonorId = APIService.Korisnik.Id;
            var entity = await _servicedonacija.Update<Donacija>(obj.DonacijaId, obj);

            if (entity != null)
                await UcitajPotrebe();

           

            
        }

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

        public async Task Init()
        {
            await UcitajPotrebe();


        }

        public DonirajPotrebuViewModel()
        {
            InfoCommand = new Command<Donacija>(OnInfoClicked);
            DonirajCommand = new Command<Donacija>(OnDonirajClicked);
            UcitajCommand = new Command(async () => await UcitajPotrebe());



        }

        private async Task UcitajPotrebe()
        {
            var list = await _servicedonacija.Get<List<Donacija>>(new DonacijaSearchRequest
            {
                Tip = Pretraga,
                Vrsta = "Potreba"

            });

            Donacije.Clear();
            foreach (var item in list)
            {

                Donacije.Add(item);
            }


        }
        private async void OnInfoClicked(Donacija obj)
        {

            if (obj == null)
                return;


            await Shell.Current.GoToAsync($"{nameof(InfoPotrebePage)}?{nameof(InfoPotrebeViewModel.DonacijaId)}={obj.DonacijaId}");



        }

    }
}
