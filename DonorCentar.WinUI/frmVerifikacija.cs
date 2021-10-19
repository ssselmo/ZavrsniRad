using DonorCentar.Model;
using DonorCentar.Model.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DonorCentar.WinUI
{
    public partial class frmVerifikacija : Form
    {

        APIService korisniciService = new APIService("Primalac");
        public frmVerifikacija()
        {

            InitializeComponent();

            dgvKorisnici.AutoGenerateColumns = false;
        }

        private async void frmVerifikacija_Load(object sender, EventArgs e)
        {
            await UcitajKorisnike();
        }

        private async Task UcitajKorisnike()
        {
            PrimalacSearchRequest searchRequest = new PrimalacSearchRequest()
            {
                ImePrezime = txtImePrezime.Text
                
                
            };

            var list = await korisniciService.GetAll<List<Primalac>>(searchRequest);

            

            
            dgvKorisnici.DataSource = list;
        }

        private async void txtImePrezime_TextChanged(object sender, EventArgs e)
        {
            await UcitajKorisnike();

        }


        private async void btnVerifikuj_Click(object sender, EventArgs e)
        {

            

            var selektovani = dgvKorisnici.SelectedRows[0].DataBoundItem as Model.Primalac;

            var entity = await korisniciService.Update<Model.Primalac>(selektovani.Id, null, "Verifikuj");
            if (entity != null)
                MessageBox.Show("Korisnik je verifikovan.");


            await UcitajKorisnike();
        }

        private void dgvKorisnici_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==5)
            {
                var selektovani = dgvKorisnici.SelectedRows[0].DataBoundItem as Model.Primalac;
                var frm = new frmPrikaziDokument(selektovani);
                frm.ShowDialog();
            }
              


        }
    }
}
