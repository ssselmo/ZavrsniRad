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
    public class MojePotrebeViewModel : BaseViewModel
    {
        private readonly APIService _servicedonacija = new APIService("Donacija");

        public ObservableCollection<Donacija> Donacije { get; set; } = new ObservableCollection<Donacija>();
        public ICommand InfoCommand { get; }
       
        public ICommand UkloniCommand { get; }
        public ICommand EditCommand { get; }

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



        public MojePotrebeViewModel()
        {
            InfoCommand = new Command<Donacija>(OnInfoClicked);
            UkloniCommand = new Command<Donacija>(OnUkloniClicked);
            UcitajCommand = new Command(async () => await UcitajPotrebe());
            EditCommand = new Command<Donacija>(OnEditClicked);



        }

        private async void OnInfoClicked(Donacija obj)
        {

            if (obj == null)
                return;


            await Shell.Current.GoToAsync($"{nameof(InfoMojePotrebePage)}?{nameof(InfoMojePotrebeViewModel.DonacijaId)}={obj.DonacijaId}");


        }

        private async void OnUkloniClicked(Donacija obj)
        {

            var entity = await _servicedonacija.Delete<Donacija>(obj.DonacijaId);

            if (entity != null)
                await UcitajPotrebe();
        }

        private async void OnEditClicked(Donacija obj)
        {

            if (obj == null)
                return;


            await Shell.Current.GoToAsync($"{nameof(EditPotrebePage)}?{nameof(EditPotrebeViewModel.DonacijaId)}={obj.DonacijaId}");


        }

        private async Task UcitajPotrebe()
        {
            var list = await _servicedonacija.Get<List<Donacija>>(new DonacijaSearchRequest
            {
                Tip = Pretraga,
                Vrsta = "Potreba",
                PrimalacId = APIService.Korisnik.Id
            });

            Donacije.Clear();
            foreach (var item in list)
            {

                Donacije.Add(item);
            }


        }
    }
}
