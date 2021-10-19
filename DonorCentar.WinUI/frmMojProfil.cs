﻿using DonorCentar.Model.Requests;
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
    public partial class frmMojProfil : Form
    {

        private readonly Model.Korisnik korisnik;
        private readonly APIService KantonService = new APIService("Kantoni");
        private readonly APIService GradService = new APIService("Gradovi");
        private KorisniciInsertRequest request = new KorisniciInsertRequest();
        private readonly APIService KorisnikService = new APIService("Korisnici");
        private readonly APIService TipKorisnikaService = new APIService("TipKorisnika");



        public frmMojProfil()
        {
            korisnik = APIService.CurrentUser;
            InitializeComponent();
        }

        private void btnOdaberiSliku_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var file = File.ReadAllBytes(openFileDialog1.FileName);
                request.LicniPodaci.ProfilnaSlika = file;
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private async void frmKorisniciDetalji_Load(object sender, EventArgs e)
        {
            await UcitajKantone();


            if (korisnik != null)
                await PrikaziPodatke();
        }


        private async Task UcitajKantone()
        {
            var list = await KantonService.GetAll<List<Model.Kanton>>();

            cmbKanton.DataSource = list;
            cmbKanton.DisplayMember = "Naziv";
        }

        private async void cmbKanton_SelectedIndexChanged(object sender, EventArgs e)
        {
            await UcitajGradove();
        }

        private async Task UcitajGradove()
        {
            var request = new GradSearchRequest()
            {
                KantonID = (cmbKanton.SelectedItem as Model.Kanton).Id
            };
            var list = await GradService.GetAll<List<Model.Grad>>(request);

            cmbGrad.DataSource = list;
            cmbGrad.DisplayMember = "Naziv";




        }

        private async void btnSnimi_Click(object sender, EventArgs e)
        {
            request.LicniPodaci.Ime = txtIme.Text;
            request.LicniPodaci.Prezime = txtPrezime.Text;
            request.LicniPodaci.Adresa = txtAdresa.Text;
            request.LicniPodaci.BrojTelefona = txtBrojTelefona.Text;
            request.LicniPodaci.Email = txtEmail.Text;

            request.LoginPodaci.KorisnickoIme = txtKorisnickoIme.Text;
            request.LoginPodaci.Sifra = txtSifra.Text;
            request.GradId = (cmbGrad.SelectedItem as Model.Grad).Id;
            request.TipKorisnikaId = korisnik.TipKorisnikaId;

            Model.Korisnik entity;


            entity = await KorisnikService.Update<Model.Korisnik>(korisnik.Id, request);



            if (entity != null)
            {
                if (!string.IsNullOrEmpty(request.LoginPodaci.Sifra))
                    APIService.Password = request.LoginPodaci.Sifra;
                APIService.CurrentUser = entity;
                MessageBox.Show("Profil je izmijenjen.");

                Close();
            }







        }

        private async Task PrikaziPodatke()
        {
            txtIme.Text = korisnik.LicniPodaci.Ime;
            txtPrezime.Text = korisnik.LicniPodaci.Prezime;
            txtAdresa.Text = korisnik.LicniPodaci.Adresa;
            txtBrojTelefona.Text = korisnik.LicniPodaci.BrojTelefona;
            txtEmail.Text = korisnik.LicniPodaci.Email;
            txtKorisnickoIme.Text = korisnik.LoginPodaci.KorisnickoIme;


            if (korisnik.LicniPodaci.ProfilnaSlika != null && korisnik.LicniPodaci.ProfilnaSlika.Length > 0)
            {
                MemoryStream ms = new MemoryStream(korisnik.LicniPodaci.ProfilnaSlika);
                pictureBox1.Image = Image.FromStream(ms);

            }


            foreach (Model.Kanton item in cmbKanton.Items)
            {
                if (item.Id == korisnik.Grad.KantonId)
                {
                    cmbKanton.SelectedItem = item;
                }
            }

            await UcitajGradove();

            foreach (Model.Grad item in cmbGrad.Items)
            {
                if (item.Id == korisnik.GradId)
                {
                    cmbGrad.SelectedItem = item;
                }
            }
        }


    }
}

