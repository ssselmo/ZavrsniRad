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
    [QueryProperty(nameof(DonacijaId), nameof(DonacijaId))]
    public class InfoPotrebeViewModel : BaseViewModel
    {
        private readonly APIService _servicedonacija = new APIService("Donacija");

        private Donacija donacija;

        public Donacija Donacija
        {
            get { return donacija; }
            set { SetProperty(ref donacija, value); }
        }
        private int donacijaid;

        public int DonacijaId
        {
            get { return donacijaid; }
            set { SetProperty(ref donacijaid , value);
                UcitajCommand.Execute(null);
            }

        }




        public ICommand UcitajCommand { get; }

       
        public InfoPotrebeViewModel()
        {
            
            UcitajCommand = new Command(async () => await UcitajDonaciju());


        }

        

        private async Task UcitajDonaciju()
        {
            var entity = await _servicedonacija.GetById<Donacija>(donacijaid);

            Donacija = entity;
       
        }






    }
}
