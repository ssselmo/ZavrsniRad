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
    public class PartneriViewModel : BaseViewModel
    {
        private readonly APIService _servicepartner = new APIService("Partner");

        public ObservableCollection<Partner> Partneri { get; set; } = new ObservableCollection<Partner>();
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
            await UcitajPartnere();


        }



        public PartneriViewModel()
        {
            InfoCommand = new Command<Partner>(OnInfoClicked);
            UcitajCommand = new Command(async () => await UcitajPartnere());



        }

        private async Task UcitajPartnere()
        {
            var list = await _servicepartner.Get<List<Partner>>(new PartnerSearchRequest
            {
                ImePrezime = pretraga
            });

            Partneri.Clear();
            foreach (var item in list)
            {

                Partneri.Add(item);
            }


        }





        private void OnInfoClicked(Partner obj)
        {
            

        }
    }
}
