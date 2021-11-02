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
    public class DonoriViewModel : BaseViewModel
    {
        private readonly APIService _servicedonor = new APIService("Donor");

        public ObservableCollection<Donor> Donori { get; set; } = new ObservableCollection<Donor>();
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
            await UcitajDonore();


        }



        public DonoriViewModel()
        {
            InfoCommand = new Command<Donor>(OnInfoClicked);
            UcitajCommand = new Command(async () => await UcitajDonore());



        }

        private async Task UcitajDonore()
        {
            var list = await _servicedonor.Get<List<Donor>>(new DonorSearchRequest
            {
                ImePrezime = pretraga
            });

            Donori.Clear();
            foreach (var item in list)
            {
                Donori.Add(item);
            }


        }





        private void OnInfoClicked(Donor obj)
        {
            

        }
    }
}
