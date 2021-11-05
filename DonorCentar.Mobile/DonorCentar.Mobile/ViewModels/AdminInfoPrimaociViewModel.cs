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
    public class AdminInfoPrimaociViewModel : BaseViewModel
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

        private byte[] dokument;

     


        public ICommand UcitajCommand { get; }
        public ICommand VerifikujCommand { get; }



        public AdminInfoPrimaociViewModel()
        {
            
            UcitajCommand = new Command(async () => await UcitajPrimaoca());
            VerifikujCommand = new Command(async () => await OnVerifikujClicked());



        }



        private async Task UcitajPrimaoca()
        {
            var entity = await _serviceprimalac.GetById<Primalac>(primalacid);

            Primalac = entity;
            Dokument = entity.DokumentVerifikacije;
        }

        private async Task OnVerifikujClicked()
        {
            

            var entity = await _serviceprimalac.Update<Primalac>(primalacid, null,"ToggleVerifikuj");

            Primalac = entity;

        }







        public byte[] Dokument
        {
            get
            {
                return this.dokument;
            }

            set
            {

                this.SetProperty(ref this.dokument, value);
            }
        }

        


    }
}
