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
    public partial class frmLogin : Form
    {
        APIService korisniciService = new APIService("Korisnici");
        public frmLogin()
        {

            InitializeComponent();
        }



        private async void button1_Click(object sender, EventArgs e)
        {
            APIService.Username = txtKorisnickoIme.Text;
            APIService.Password = txtLozinka.Text;
            try
            {
                APIService.CurrentUser = await korisniciService.GetAll<Model.Korisnik>(null, "Profil");


               
                if (APIService.CurrentUser.Tip != "Administrator")
                {
                    MessageBox.Show("Nemate dozvolu da se prijavite.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult = DialogResult.OK;
                
                

            }
            catch (Exception)
            {
                MessageBox.Show("Neispravni podaci za prijavu.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
