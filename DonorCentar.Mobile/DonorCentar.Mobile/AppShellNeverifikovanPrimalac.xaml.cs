using DonorCentar.Mobile.ViewModels;
using DonorCentar.Mobile.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace DonorCentar.Mobile
{
    public partial class AppShellNeverifikovanPrimalac : Xamarin.Forms.Shell
    {
        public AppShellNeverifikovanPrimalac()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(InfoObavijestiPage), typeof(InfoObavijestiPage));
            Routing.RegisterRoute(nameof(InfoMojePotrebePage), typeof(InfoMojePotrebePage));
            Routing.RegisterRoute(nameof(EditPotrebePage), typeof(EditPotrebePage));
            Routing.RegisterRoute(nameof(InfoMojeDonacijePage), typeof(InfoMojeDonacijePage));









        }



        private  void OnMenuItemClicked(object sender, EventArgs e)
        {
           
            Application.Current.MainPage = new LoginPage();
        }
    }
}
