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
    public partial class DesignersDetailsForm : Form
    {
        // Designer currently being displayed
        private int m_designerKey = -1;

        public DesignersDetailsForm(int designerKey)
        {
            InitializeComponent();

            // Received session holds the shown designer
            m_designerKey = designerKey;

            // Clear the listbox
            listBoxShows.Items.Clear();
        }

        private void DesignersDetailsForm_Load(object sender, EventArgs e)
        {
            if (m_designerKey == -1)
            {
                MessageBox.Show("Error receiving data on designer");
                this.Close();
            }

            // Load designer data into UI controls
            InitializeDesignerData();
        }

        private void InitializeDesignerData()
        {
            ISession session = DataAccessLayer.DataLayer.GetSession();
            MODNI_KREATOR designer = session.Get<MODNI_KREATOR>(m_designerKey);

            // Personal Number
            labelPn.Text = designer.MATICNI_BROJ.ToString();

            // First Name
            labelFirstName.Text = designer.IME;

            // Last Name
            labelLastName.Text = designer.PREZIME;
            Text = labelFirstName.Text + " " + labelLastName.Text;

            // Birth Date
            if (designer.DATUM_RODJENJA == DateTime.MinValue)
                labelBirthDate.Text = "Unknown";
            else
                labelBirthDate.Text = designer.DATUM_RODJENJA.ToShortDateString();

            // Gender
            char gender = designer.POL;
            if (gender == 'F') labelGender.Text = "Female";
            if (gender == 'M') labelGender.Text = "Male";

            // Country
            labelCountry.Text = designer.ZEMLJA_POREKLA;

            // Fashion House
            labelFashionHouse.Text = designer.MODNA_KUCA;

            // Fashion Shows
            IList<MODNA_REVIJA> shows = designer.modneRevije;
            if (shows.Count > 0)
            {
                listBoxShows.DataSource = shows;
                listBoxShows.DisplayMember = "NAZIV";
            }

            session.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
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
    }
}
