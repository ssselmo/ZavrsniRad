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
    public partial class PrimljeneDonacijePage : ContentPage
    {
        private PrimljeneDonacijeViewModel model;

        public PrimljeneDonacijePage()
        {

            InitializeComponent();
            this.BindingContext = model = new PrimljeneDonacijeViewModel();
            
        }

        protected async override void OnAppearing()
        {
            await model.Init();
        }
    }
}