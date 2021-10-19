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

    public class DojamViewModel : BaseViewModel
    {
        private readonly APIService _servicedojam = new APIService("Dojam");

       
        private DojamKorisnik dojam = new DojamKorisnik();
        private Dojam tipdojma;

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




        public Dojam TipDojma
        {
            get { return tipdojma; }
            set { SetProperty(ref tipdojma, value); }
        }

        


        public DojamKorisnik Dojam
        {
            get { return dojam; }
            set { SetProperty(ref dojam, value); }
        }

     




        public ObservableCollection<Dojam> TipoviDojmova { get; set; } = new ObservableCollection<Dojam>();
        

        public ICommand SpremiCommand { get; }
        public ICommand UcitajCommand { get; }


        private async void OnSpremiClicked()
        {


            if (tipdojma==null)
            {
                await Application.Current.MainPage.DisplayAlert("Greška", "Potrebno unijeti tip dojma!", "OK");
                return;

            }

            if (string.IsNullOrEmpty(Dojam.Opis))
            {
                await Application.Current.MainPage.DisplayAlert("Greška", "Potrebno unijeti opis!", "OK");
                return;
            }

            Dojam.Datum = DateTime.Now;
            Dojam.DojamId = TipDojma.Id;
            Dojam.KorisnikId = APIService.Korisnik.Id;

            Dojam.DonacijaId = donacijaid;

            DojamKorisnik entity;

            if (Dojam.Id == 0)
            {
                entity = await _servicedojam.Insert<DojamKorisnik>(Dojam);
         }
            else
            {
                entity = await _servicedojam.Update<DojamKorisnik>(donacijaid,Dojam);

            }





            if (entity != null)
            {
                Dojam = new DojamKorisnik();
               
                
              
                TipDojma = null;
               
                await Shell.Current.GoToAsync("///"+nameof(MojeDonacijePage));

            }

        }



        public async Task Init()
        {
            await UcitajDojmove();
            await UcitajPostojeciDojam();
            


        }

        private async Task UcitajPostojeciDojam()
        {
            var entity = await _servicedojam.GetById<DojamKorisnik>(donacijaid);

            if (entity != null)
            {
                Dojam = entity;

                foreach (var item in TipoviDojmova)
                {
                    if(item.Id==Dojam.DojamId)
                    {
                        TipDojma = item;
                    }
                }
            }

           
        }

        public DojamViewModel()
        {
            SpremiCommand = new Command(OnSpremiClicked);
            UcitajCommand = new Command(async () => await Init());



        }


        private async Task UcitajDojmove()
        {
            var list = await _servicedojam.Get<List<Dojam>>(null);

            TipoviDojmova.Clear();
            foreach (var item in list)
            {

                TipoviDojmova.Add(item);
            }


        }

        

    }
}
