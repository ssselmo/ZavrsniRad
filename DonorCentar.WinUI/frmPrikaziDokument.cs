using DonorCentar.Model;
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
    public partial class frmPrikaziDokument : Form
    {
        private Primalac selektovani;

       

        public frmPrikaziDokument(Primalac selektovani)
        {
            this.selektovani = selektovani;
            InitializeComponent();


            if (selektovani.DokumentVerifikacije != null && selektovani.DokumentVerifikacije.Length > 0)
            {
                MemoryStream ms = new MemoryStream(selektovani.DokumentVerifikacije);
                pictureBox1.Image = Image.FromStream(ms);
            }
        }
      

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
