using DonorCentar.Mobile.ViewModels;
using DonorCentar.Mobile.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace DonorCentar.Mobile
{
    public partial class AppShellAdmin : Xamarin.Forms.Shell
    {
        public AppShellAdmin()
        {
            InitializeComponent();
           
         
            Routing.RegisterRoute(nameof(InfoPrimaociPage), typeof(InfoPrimaociPage));
            Routing.RegisterRoute(nameof(InfoObavijestiPage), typeof(InfoObavijestiPage));
            Routing.RegisterRoute(nameof(AdminObavijestiPage), typeof(AdminObavijestiPage));
            Routing.RegisterRoute(nameof(EditObavijestPage), typeof(EditObavijestPage));


            Routing.RegisterRoute(nameof(DojamPage), typeof(DojamPage));






        }



        private  void OnMenuItemClicked(object sender, EventArgs e)
        {
           
            Application.Current.MainPage = new LoginPage();
        }
    }
}
