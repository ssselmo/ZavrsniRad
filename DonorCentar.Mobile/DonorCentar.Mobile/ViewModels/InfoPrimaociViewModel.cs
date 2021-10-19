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
    [QueryProperty(nameof(PrimalacId), nameof(PrimalacId))]
    public class InfoPrimaociViewModel : BaseViewModel
    {
        private readonly APIService _serviceprimalac = new APIService("Primalac");

        private Primalac primalac;

        public Primalac Primalac
        {
            get { return primalac; }
            set { SetProperty(ref primalac, value); }
        }
        private int primalacid;

        public int PrimalacId
        {
            get { return primalacid; }
            set { SetProperty(ref primalacid, value);
                UcitajCommand.Execute(null);
            }

        }




        public ICommand UcitajCommand { get; }

       
        public InfoPrimaociViewModel()
        {
            
            UcitajCommand = new Command(async () => await UcitajPrimaoca());


        }

        

        private async Task UcitajPrimaoca()
        {
            var entity = await _serviceprimalac.GetById<Primalac>(primalacid);

            Primalac = entity;
       
        }






    }
}
