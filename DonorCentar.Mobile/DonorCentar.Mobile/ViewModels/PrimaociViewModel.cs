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
    public class PrimaociViewModel : BaseViewModel
    {
        private readonly APIService _serviceprimalac = new APIService("Primalac");
        public ObservableCollection<Primalac> Primaoci { get; set; } = new ObservableCollection<Primalac>();
        public ICommand InfoCommand { get; }

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
            await UcitajPrimaoce();


        }







        public PrimaociViewModel()
        {
            InfoCommand = new Command<Primalac>(OnInfoClicked);
            UcitajCommand = new Command(async () => await UcitajPrimaoce());

        }

        private async Task UcitajPrimaoce()
        {
            var list = await _serviceprimalac.Get<List<Primalac>>(new PrimalacSearchRequest
            {
                ImePrezime=pretraga
            });

            Primaoci.Clear();
            foreach (var item in list)
            {

                Primaoci.Add(item);
            }


        }
        private async void OnInfoClicked(Primalac obj)
        {
            if (obj == null)
                return;


            await Shell.Current.GoToAsync($"{nameof(InfoPrimaociPage)}?{nameof(InfoPrimaociViewModel.PrimalacId)}={obj.Id}");

        }
    }
}
