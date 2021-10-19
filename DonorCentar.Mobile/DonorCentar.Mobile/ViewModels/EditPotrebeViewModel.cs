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
    public class EditPotrebeViewModel : BaseViewModel
    {
        private readonly APIService _servicedonacija = new APIService("Donacija");
        private readonly APIService _servicetip = new APIService("TipDonacije");
       
        private DonacijaInsertRequest donacija = new DonacijaInsertRequest();
        private TipDonacije tipdonacije;

       


        private bool transport;

        public bool Transport
        {
            get { return transport; }
            set { SetProperty(ref transport , value); }
        }


        public TipDonacije TipDonacije
        {
            get { return tipdonacije; }
            set { SetProperty(ref tipdonacije ,value); }
        }

        


        public DonacijaInsertRequest Donacija
        {
            get { return donacija; }
            set { SetProperty(ref donacija, value); }
        }

        private int donacijaid;

        public int DonacijaId
        {
            get { return donacijaid; }
            set
            {
                SetProperty(ref donacijaid, value);
                UcitajCommand.Execute(null);
            }

        }




        public ObservableCollection<TipDonacije> TipoviDonacija { get; set; } = new ObservableCollection<TipDonacije>();
        

        public ICommand EditCommand { get; }
        public ICommand UcitajCommand { get; }


        private async void OnEditClicked()
        {
            
            if (tipdonacije == null)
            {
                await Application.Current.MainPage.DisplayAlert("Greška", "Potrebno unijeti tip potrebe!", "OK");
                return;

            }

          

            if (string.IsNullOrEmpty(Donacija.Opis))
            {
                await Application.Current.MainPage.DisplayAlert("Greška", "Potrebno unijeti opis!", "OK");
                return;
            }


            Donacija.InformacijeId = transport ? 1 : 4;
            Donacija.TipDonacijeId = tipdonacije.TipDonacijeId;
           
            Donacija.VrstaDonacijeId = 2;
           
          
            

        
            var entity = await _servicedonacija.Update<Donacija>(DonacijaId,Donacija);
            
            if (entity != null)
            {
                Donacija = new DonacijaInsertRequest();
               
                
              
                TipDonacije = null;
               
                Transport = false;
                await Shell.Current.GoToAsync("///"+nameof(MojePotrebePage));

            }

        }



        public async Task Init()
        {
            await UcitajTipove();
            await UcitajPotrebu();

            foreach (var item in TipoviDonacija)
            {
                if (item.TipDonacijeId == donacija.TipDonacijeId)
                    TipDonacije = item;
            }

            Transport = Donacija.InformacijeId == 1;


        }

        public EditPotrebeViewModel()
        {
            EditCommand = new Command(OnEditClicked);
            UcitajCommand = new Command(async () => await Init());



        }

        private async Task UcitajTipove()
        {
            var list = await _servicetip.Get<List<TipDonacije>>(null);

            TipoviDonacija.Clear();
            foreach (var item in list)
            {

                TipoviDonacija.Add(item);
            }


        }

        private async Task UcitajPotrebu()
        {
            var entity = await _servicedonacija.GetById<Donacija>(donacijaid);

            Donacija = new DonacijaInsertRequest { DonorId=entity.DonorId,
            InformacijeId=entity.InformacijeId,
            JedinicaMjere=entity.JedinicaMjere,
            Kolicina=entity.Kolicina,
            Opis=entity.Opis,
            PrimalacId=entity.PrimalacId,
            StatusId=entity.StatusId,
            TipDonacijeId=entity.TipDonacijeId,
            TransportId=entity.TransportId,
            VrstaDonacijeId=entity.VrstaDonacijeId};

        }

    }
}
