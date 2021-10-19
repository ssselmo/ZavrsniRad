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
    public partial class InfoPrimaociPage : ContentPage
    {
        public InfoPrimaociPage()
        {

            InitializeComponent();
            this.BindingContext = new InfoPrimaociViewModel();
            
        }
    }
}