using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NHibernate;
using DataAccessLayer.Entiteti;

namespace SBPDemo
{
    public partial class ModelsDetailsForm : Form
    {
        // Model currently being displayed
        private int m_modelKey = -1;


        public ModelsDetailsForm(int modelKey)
        {
            InitializeComponent();

            m_modelKey = modelKey;

            // Clear the listbox
            listBoxShows.Items.Clear();
            listBoxMags.Items.Clear();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ModelsDetailsForm_Load(object sender, EventArgs e)
        {
            if (m_modelKey == -1)
            {
                MessageBox.Show("Error receiving data on designer");
                this.Close();
            }

            // Load model data into UI controls
            InitializeModelData();
        }

        private void InitializeModelData()
        {
            ISession session = DataAccessLayer.DataLayer.GetSession();
            IList<MANEKEN> models = session.CreateQuery("FROM MANEKEN").List<MANEKEN>();
            MANEKEN model = null;
            // CAN'T EXPOSE DERIVED TYPE (is/as) IF I JUST LOAD FROM SESSION WITH A KEY ?!?
            for (int i = 0; i < models.Count; i++)
            {
                if (models[i].MATICNI_BROJ == m_modelKey)
                {
                    model = models[i];
                    break;
                }
            }
            if (model == null)
            {
                session.Close();
                return;
            }

            // Personal Number
            labelPN.Text = model.MATICNI_BROJ.ToString();

            // First Name
            labelFirstName.Text = model.IME;

            // Last Name
            labelLastName.Text = model.PREZIME;
            Text = model.IME_PREZIME;

            // Agency
            if (model.modnaAgencija != null)
            {
                linkLabelAgency.Tag = model.modnaAgencija;
                linkLabelAgency.Text = model.modnaAgencija.NAZIV;
            }
            else
            { 
                linkLabelAgency.Text = "No Agency";
                linkLabelAgency.Enabled = false;
            }

            // Birth Date
            if (model.DATUM_RODJENJA == DateTime.MinValue)
                labelBirthDate.Text = "Unknown";
            else
                labelBirthDate.Text = model.DATUM_RODJENJA.ToShortDateString();

            // Gender
            char gender = model.POL;
            if (gender == 'F') labelGender.Text = "Female";
            if (gender == 'M') labelGender.Text = "Male";

            // Eyes
            if (model.BOJA_OCIJU == null || model.BOJA_OCIJU == "")
                labelEye.Text = "Unknown";
            else
                labelEye.Text = model.BOJA_OCIJU;

            // Hair
            if (model.BOJA_KOSE == null || model.BOJA_KOSE == "")
                labelHair.Text = "Unknown";
            else
                labelHair.Text = model.BOJA_KOSE;


            // Height, Weight and Clothing Size
            labelHeight.Text = model.VISINA <= 0 ? "Unknown" : model.VISINA.ToString();
            labelWeight.Text = model.TEZINA <= 0 ? "Unknown" : model.TEZINA.ToString();
            labelClothingSize.Text = model.KONFEKCIJSKI_BROJ <= 0 ? "Unknown" : model.KONFEKCIJSKI_BROJ.ToString();

            // Special Guest Flag
            if (model is SPECIJALNI_GOST)
            {
                labelSpecialGuest.Text = "Special Guest Status: True";
                labelOcc.Text = (model as SPECIJALNI_GOST).ZANIMANJE;
            }
            else
            {
                labelSpecialGuest.Text = "Special Guest Status: False";
                labelOcc.Visible = false;
                occLabel.Visible = false;
            }

            // Magazines
            IList<CASOPIS> magazines = model.casopisi;
            if (magazines.Count > 0)
            {
                listBoxMags.DataSource = magazines;
                listBoxMags.DisplayMember = "NASLOV_CASOPISA";
            }

            // Shows
            IList<MODNA_REVIJA> shows = model.modneRevije;
            if (shows.Count > 0)
            {
                listBoxShows.DataSource = shows;
                listBoxShows.DisplayMember = "NAZIV";
            }

            session.Close();
        }

        private void listBoxShows_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBoxShows.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                MODNA_REVIJA mr = listBoxShows.Items[index] as MODNA_REVIJA;

                ShowsDetailsForm mrDetailsForm = new ShowsDetailsForm(mr.REDNI_BROJ);
                mrDetailsForm.Show();
            }
        }

        private void linkLabelAgency_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkLabelAgency.Tag != null)
            {
                MODNA_AGENCIJA mag = linkLabelAgency.Tag as MODNA_AGENCIJA;

                AgenciesDetailsForm magDetailsForm = new AgenciesDetailsForm(mag.PIB);
                magDetailsForm.Show();
            }
        }
    }
}
