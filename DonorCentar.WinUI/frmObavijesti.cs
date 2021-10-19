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
    public partial class frmObavijesti : Form
    {

        APIService obavijestService = new APIService("Obavijest");
        public frmObavijesti()
        {

            InitializeComponent();

            dgvObavijesti.AutoGenerateColumns = false;
        }

        private async void frmObavijest_Load(object sender, EventArgs e)
        {
            await UcitajObavijesti();
        }

        private async Task UcitajObavijesti()
        {
            ObavijestSearchRequest searchRequest = new ObavijestSearchRequest
            {
                Naslov = txtNaslov.Text
            };

            var list = await obavijestService.GetAll<List<Obavijest>>(searchRequest);


            dgvObavijesti.DataSource = list;
        }

        private async void txtNaslov_TextChanged(object sender, EventArgs e)
        {
            await UcitajObavijesti();

        }

        
        private async void btnUredi_Click(object sender, EventArgs e)
        {
            var selektovani = dgvObavijesti.SelectedRows[0].DataBoundItem as Model.Obavijest;
            frmObavijestDetalji frm = new frmObavijestDetalji(selektovani);
            frm.ShowDialog();
             await UcitajObavijesti();
        }

        private async void btnUkloni_Click(object sender, EventArgs e)
        {
            var selektovani = dgvObavijesti.SelectedRows[0].DataBoundItem as Model.Obavijest;

            await obavijestService.Delete<Model.Obavijest>(selektovani.ObavijestId);
            await UcitajObavijesti();
        }
    }
}
