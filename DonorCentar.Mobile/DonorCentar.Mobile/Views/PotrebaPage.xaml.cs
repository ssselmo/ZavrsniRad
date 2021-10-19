using DonorCentar.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DonorCentar.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PotrebaPage : ContentPage
    {
        private PotrebaViewModel model;

        public PotrebaPage()
        {

            InitializeComponent();
            this.BindingContext =model= new PotrebaViewModel();
            
        }

        protected async override void OnAppearing()
        {
            await model.Init();
        }
    }
}


