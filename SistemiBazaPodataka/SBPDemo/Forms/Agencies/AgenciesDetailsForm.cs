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
    public partial class AgenciesDetailsForm : Form
    {
        private int m_agencyKey = -1;

        public AgenciesDetailsForm(int agencyKey)
        {
            InitializeComponent();

            m_agencyKey = agencyKey;

            // Clear the listbox
            listBoxCountries.Items.Clear();
            listBoxModels.Items.Clear();
        }

        private void AgenciesDetailsForm_Load(object sender, EventArgs e)
        {
            if (m_agencyKey == -1)
            {
                MessageBox.Show("Error receiving data on designer");
                this.Close();
            }

            // Load agency data into UI controls
            InitializeAgencyData();
        }

        private void InitializeAgencyData()
        {
            ISession session = DataAccessLayer.DataLayer.GetSession();
            MODNA_AGENCIJA agency = session.Get<MODNA_AGENCIJA>(m_agencyKey);

            // PIN
            labelPIN.Text = agency.PIB.ToString();

            // Title
            labelName.Text = agency.NAZIV;
            Text = agency.NAZIV;

            // Date & Time
            labelDate.Text = agency.DATUM_OSNIVANJA.ToString("dd/MM/yyyy");

            // Location
            labelHQ.Text = agency.SEDISTE;

            // Fashion Models
            IList<MANEKEN> manekeni = agency.manekeni;
            if (manekeni.Count > 0)
            {
                listBoxModels.DataSource = manekeni;
                listBoxModels.DisplayMember = "IME_PREZIME";
            }

            if (agency is INTERNACIONALNA_AGENCIJA)
            {
                IList<DRZAVA> countries = (agency as INTERNACIONALNA_AGENCIJA).drzave;
                if (countries.Count > 0)
                {
                    listBoxCountries.DataSource = countries;
                    listBoxCountries.DisplayMember = "NAZIV_DRZAVE";
                }
                labelType.Text = "International Agency";
            }
            else
            {
                listBoxCountries.Visible = false;
                labelType.Text = "Local Agency";
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listBoxModels_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBoxModels.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                MANEKEN man = listBoxModels.Items[index] as MANEKEN;

                ModelsDetailsForm manDetailsForm = new ModelsDetailsForm(man.MATICNI_BROJ);
                manDetailsForm.Show();
            }
        }
    }
}
