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
    public class AdminObavijestiViewModel : BaseViewModel
    {
        private readonly APIService _serviceobavijest = new APIService("Obavijest");
        public ObservableCollection<Obavijest> Obavijesti { get; set; } = new ObservableCollection<Obavijest>();
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
            await UcitajObavijesti();


        }

        public AdminObavijestiViewModel()
        {
            InfoCommand = new Command<Obavijest>(OnInfoClicked);
            UcitajCommand = new Command(async () => await UcitajObavijesti());
            UkloniCommand = new Command<Obavijest>(OnUkloniClicked);
            EditCommand = new Command<Obavijest>(OnEditClicked);


        }

        private async void OnEditClicked(Obavijest obj)
        {

            if (obj == null)
                return;


            await Shell.Current.GoToAsync($"{nameof(EditObavijestPage)}?{nameof(EditObavijestViewModel.ObavijestId)}={obj.ObavijestId}");


        }

        private async void OnUkloniClicked(Obavijest obj)
        {

            var entity = await _serviceobavijest.Delete<Obavijest>(obj.ObavijestId);

            if (entity != null)
                await UcitajObavijesti();
        }

        private async Task UcitajObavijesti()
        {
            var list = await _serviceobavijest.Get<List<Obavijest>>(new ObavijestSearchRequest
            {
                Naslov=Pretraga
            });

            Obavijesti.Clear();
            foreach (var item in list)
            {

                Obavijesti.Add(item);
            }


        }

        private async void OnInfoClicked(Obavijest obj)
        {


            if (obj == null)
                return;


            await Shell.Current.GoToAsync($"{nameof(InfoObavijestiPage)}?{nameof(InfoObavijestiViewModel.ObavijestId)}={obj.ObavijestId}");

        }
    }
}
