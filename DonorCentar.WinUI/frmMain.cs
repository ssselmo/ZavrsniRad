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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void prikazKorisnikaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKorisnici frm = new frmKorisnici();

            frm.WindowState = FormWindowState.Maximized;
            frm.MdiParent = this;
            frm.Show();

        }

        private void dodajKorisnikaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmKorisniciDetalji frm = new frmKorisniciDetalji();
            frm.ShowDialog();

        }

        private void verifikujPrimaocaToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

                frmVerifikacija frm = new frmVerifikacija();
                frm.ShowDialog();

            
        }

        private void mojProfilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMojProfil frm = new frmMojProfil();
            frm.ShowDialog();

        }

        private void prikazObavijestiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmObavijesti frm = new frmObavijesti();
            frm.ShowDialog();
        }

        private void dodajObavijestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmObavijestDetalji frm = new frmObavijestDetalji();
            frm.ShowDialog();
        }
    }
}
