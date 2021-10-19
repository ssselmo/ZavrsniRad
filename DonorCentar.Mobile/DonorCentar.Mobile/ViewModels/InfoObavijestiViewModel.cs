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
    public class InfoObavijestiViewModel : BaseViewModel
    {
        private readonly APIService _serviceobavijest = new APIService("Obavijest");

        private Obavijest obavijest;

        public Obavijest Obavijest
        {
            get { return obavijest; }
            set { SetProperty(ref obavijest, value); }
        }
        private int obavijestid;

        public int ObavijestId
        {
            get { return obavijestid; }
            set { SetProperty(ref obavijestid, value);
                UcitajCommand.Execute(null);
            }

        }




        public ICommand UcitajCommand { get; }

       
        public InfoObavijestiViewModel()
        {
            
            UcitajCommand = new Command(async () => await UcitajObavijest());


        }

        

        private async Task UcitajObavijest()
        {
            var entity = await _serviceobavijest.GetById<Obavijest>(obavijestid);

            Obavijest = entity;
       
        }






    }
}
