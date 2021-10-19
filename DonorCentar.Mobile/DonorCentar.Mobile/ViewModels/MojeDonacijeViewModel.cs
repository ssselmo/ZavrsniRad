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
    public class MojeDonacijeViewModel : BaseViewModel
    {
        private readonly APIService _servicedonacija = new APIService("Donacija");
        public ObservableCollection<Donacija> Donacije { get; set; } = new ObservableCollection<Donacija>();
        public ICommand InfoCommand { get; }

        public ICommand UkloniCommand { get; }
       
        public ICommand DojamCommand { get; }

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
            await UcitajDonacije();


        }

        private async void OnDojamClicked(Donacija obj)
        {
            if (obj == null)
                return;


            await Shell.Current.GoToAsync($"{nameof(DojamPage)}?{nameof(DojamViewModel.DonacijaId)}={obj.DonacijaId}");


        }

        private async void OnUkloniClicked(Donacija obj)
        {
          var entity= await _servicedonacija.Delete<Donacija>(obj.DonacijaId);

            if(entity!=null)
                await UcitajDonacije();


        }


        public MojeDonacijeViewModel()
        {
            InfoCommand = new Command<Donacija>(OnInfoClicked);
            UkloniCommand = new Command<Donacija>(OnUkloniClicked);
            UcitajCommand = new Command(async () => await UcitajDonacije());
            DojamCommand = new Command<Donacija>(OnDojamClicked);


        }

        private async void OnInfoClicked(Donacija obj)
        {
           
                if (obj == null)
                    return;

                
                await Shell.Current.GoToAsync($"{nameof(InfoMojeDonacijePage)}?{nameof(InfoMojeDonacijeViewModel.DonacijaId)}={obj.DonacijaId}");
            


        }

        private async Task UcitajDonacije()
        {
            var list = await _servicedonacija.Get<List<Donacija>>(new DonacijaSearchRequest
            {
                Tip = Pretraga,
                Vrsta = "Donacija",
                DonorId = APIService.Korisnik.Id
            });

            Donacije.Clear();
            foreach (var item in list)
            {

                Donacije.Add(item);
            }


        }






    }
}
