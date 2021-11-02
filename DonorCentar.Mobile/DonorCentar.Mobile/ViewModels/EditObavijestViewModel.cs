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
    [QueryProperty(nameof(ObavijestId), nameof(ObavijestId))]
    public class EditObavijestViewModel : BaseViewModel
    {
        private readonly APIService _serviceobavijest = new APIService("Obavijest");

        private ObavijestInsertRequest obavijest = new ObavijestInsertRequest();

       

        public ObavijestInsertRequest Obavijest
        {
            get { return obavijest; }
            set { SetProperty(ref obavijest, value); }
        }

        private int obavijestid;

        public int ObavijestId
        {
            get { return obavijestid; }
            set
            {
                SetProperty(ref obavijestid, value);
                UcitajCommand.Execute(null);
            }

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


        public ICommand EditCommand { get; }
        public ICommand UcitajCommand { get; }


        private async void OnEditClicked()
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


           
          
            

        
            var entity = await _serviceobavijest.Update<Obavijest>(ObavijestId,Obavijest);
            
            if (entity != null)
            {
                Obavijest = new ObavijestInsertRequest();
               
                
              
                await Shell.Current.GoToAsync("///"+nameof(AdminObavijestiPage));

            }

        }



        public async Task Init()
        {
            
            await UcitajObavijest();

           


        }

        public EditObavijestViewModel()
        {
            EditCommand = new Command(OnEditClicked);
            UcitajCommand = new Command(async () => await Init());



        }


        private async Task UcitajObavijest()
        {
            var entity = await _serviceobavijest.GetById<Obavijest>(obavijestid);

            Obavijest = new ObavijestInsertRequest
            {
                Naslov = entity.Naslov,
                Sadrzaj = entity.Sadrzaj

            };
        }
    }
}
