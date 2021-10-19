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
    public class PreporukaViewModel : BaseViewModel
    {
        private readonly APIService _servicekorisnici = new APIService("Korisnici");
        public ObservableCollection<Donacija> Preporuke { get; set; } = new ObservableCollection<Donacija>();
    

      
        public ICommand DonirajCommand { get; }

        private async void OnDonirajClicked(Donacija obj)
        {
            await Shell.Current.GoToAsync($"///{nameof(DonirajPage)}?{nameof(DonacijaViewModel.TipDonacijeId)}={obj.TipDonacijeId}&{nameof(DonacijaViewModel.PrimalacId)}={obj.PrimalacId}");

        }




        public async Task Init()
        {
            await UcitajPreporuke();



        }

        public PreporukaViewModel()
        {
            DonirajCommand = new Command<Donacija> (OnDonirajClicked);



        }

      

        private async Task UcitajPreporuke()
        {
            var list = await _servicekorisnici.Get<List<Donacija>>(null,"GetPreporuka");

            Preporuke.Clear();

            foreach (var item in list)
            {

                Preporuke.Add(item);
            }



        }

     

    }
}
