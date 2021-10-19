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
    public partial class frmKorisnici : Form
    {

        APIService korisniciService = new APIService("Korisnici");
        public frmKorisnici()
        {

            InitializeComponent();

            dgvKorisnici.AutoGenerateColumns = false;
        }

        private async void frmKorisnici_Load(object sender, EventArgs e)
        {
            await UcitajKorisnike();
        }

        private async Task UcitajKorisnike()
        {
            KorisniciSearchRequest searchRequest = new KorisniciSearchRequest()
            {
                ImePrezime = txtImePrezime.Text,
                TipKorisnika= cmbTipKorisnika.Text
            };

            var list = await korisniciService.GetAll<List<Korisnik>>(searchRequest);


            dgvKorisnici.DataSource = list;
        }

        private async void txtImePrezime_TextChanged(object sender, EventArgs e)
        {
            await UcitajKorisnike();

        }

        private async void cmbTipKorisnika_SelectedIndexChanged(object sender, EventArgs e)
        {
            await UcitajKorisnike();
        }

        private async void btnUredi_Click(object sender, EventArgs e)
        {
            var selektovani = dgvKorisnici.SelectedRows[0].DataBoundItem as Model.Korisnik;
            frmKorisniciDetalji frm = new frmKorisniciDetalji(selektovani);
            frm.ShowDialog();
             await UcitajKorisnike();
        }

        private async void btnUkloni_Click(object sender, EventArgs e)
        {
            var selektovani = dgvKorisnici.SelectedRows[0].DataBoundItem as Model.Korisnik;
            await korisniciService.Delete<Model.Korisnik>(selektovani.Id);
            await UcitajKorisnike();
            
        }
    }
}
