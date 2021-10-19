using DonorCentar.Model.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DonorCentar.WinUI
{
    public partial class frmObavijestDetalji : Form
    {
        private readonly Model.Obavijest Obavijest ;
        private ObavijestInsertRequest request = new ObavijestInsertRequest();
        private readonly APIService ObavijestService = new APIService("Obavijest");
       

        public frmObavijestDetalji()
        {
            InitializeComponent();
        }

        public frmObavijestDetalji(Model.Obavijest kor)
        {
            Obavijest = kor;
            InitializeComponent();
        }

       

        private  void frmObavijestDetalji_Load(object sender, EventArgs e)
        {
          
            if (Obavijest != null)
                PrikaziPodatke();
        }

      


       
            
                

      

        private async void btnSnimi_Click(object sender, EventArgs e)
        {
            request.Naslov = txtNaslov.Text;
            request.Sadrzaj = txtSadrzaj.Text;
            request.Vrijeme = DateTime.Now;

            Model.Obavijest entity;

            if(Obavijest!=null)
                entity = await ObavijestService.Update<Model.Obavijest>(Obavijest.ObavijestId,request);
            else
                entity = await ObavijestService.Insert<Model.Obavijest>(request);


            if(entity!=null)
            {
                if(Obavijest!=null)
                    MessageBox.Show("Obavijest je izmijenjena.");
                else
                    MessageBox.Show("Obavijest je uspješno dodana.");
                Close();
            }

        }

        private void PrikaziPodatke()
        {
            txtNaslov.Text = Obavijest.Naslov;
            txtSadrzaj.Text = Obavijest.Sadrzaj;

          
            
        }


    }
}
