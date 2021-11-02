
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
    public class DodajObavijestViewModel : BaseViewModel
    {
        private readonly APIService _serviceobavijest = new APIService("Obavijest");


        private ObavijestInsertRequest obavijest = new ObavijestInsertRequest();



        public ObavijestInsertRequest Obavijest
        {
            get { return obavijest; }
            set { SetProperty(ref obavijest, value); }
        }


        private string naslov;

        public string Naslov
        {
            get { return naslov; }
            set { SetProperty(ref naslov, value); }
        }

        private string sadrzaj;

        public string Sadrzaj
        {
            get { return sadrzaj; }
            set { SetProperty(ref sadrzaj, value); }
        }


        public ICommand DodajCommand { get; }
        public ICommand UcitajCommand { get; }


        private async void OnDodajClicked()
        {


            if (string.IsNullOrEmpty(Obavijest.Naslov))
            {
                await Application.Current.MainPage.DisplayAlert("Greška", "Potrebno unijeti naslov!", "OK");
                return;
            }
            if (string.IsNullOrEmpty(Obavijest.Sadrzaj))
            {
                await Application.Current.MainPage.DisplayAlert("Greška", "Potrebno unijeti sadržaj", "OK");
                return;

            }

          
            Obavijest.Vrijeme = DateTime.Now;
          






            var entity = await _serviceobavijest.Insert<Obavijest>(Obavijest);

            if (entity != null)
            {
                Obavijest = new ObavijestInsertRequest();

                
                await Shell.Current.GoToAsync("///" + nameof(AdminObavijestiPage));

            }

        }


        public DodajObavijestViewModel()
        {
            DodajCommand = new Command(OnDodajClicked);
           

        }

      



    }
}
