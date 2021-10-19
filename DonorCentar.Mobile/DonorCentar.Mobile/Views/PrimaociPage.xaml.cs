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
    public partial class PrimaociPage : ContentPage
    {
        private PrimaociViewModel model;

        public PrimaociPage()
        {

            InitializeComponent();
            this.BindingContext= model = new PrimaociViewModel();
            
        }

        protected async override void OnAppearing()
        {
            await model.Init();
        }
    }
}