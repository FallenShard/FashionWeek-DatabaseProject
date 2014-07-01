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
    public partial class ShowsDetailsForm : Form
    {
        // Show currently being displayed
        private int m_showNumber = -1;

        public ShowsDetailsForm(int number)
        {
            InitializeComponent();

            m_showNumber = number;

            // Clear the listboxes
            listBoxModels.Items.Clear();
            listBoxDesigners.Items.Clear();
        }

        private void ShowsDetailsForm_Load(object sender, EventArgs e)
        {
            if (m_showNumber == -1)
            {
                MessageBox.Show("Error receiving data on show");
                this.Close();
            }

            // Load designer data into UI controls
            InitializeShowData();
        }

        private void InitializeShowData()
        {
            ISession session = DataAccessLayer.DataLayer.GetSession();
            MODNA_REVIJA show = session.Get<MODNA_REVIJA>(m_showNumber);
            // Title
            labelTitle.Text = show.NAZIV;

            // Date
            labelDate.Text = show.DATUM_VREME.ToShortDateString();

            // Time
            labelTime.Text = show.DATUM_VREME.ToShortTimeString();
            Text = labelTitle.Text;

            // Location
            labelLocation.Text = show.MESTO;

            // Fashion Designers
            IList<MODNI_KREATOR> designers = show.modniKreatori;
            if (designers.Count > 0)
            {
                listBoxDesigners.DataSource = designers;
                listBoxDesigners.DisplayMember = "IME_PREZIME";
            }

            // Fashion Models
            IList<MANEKEN> models = show.manekeni;
            if (models.Count > 0)
            {
                listBoxModels.DataSource = models;
                listBoxModels.DisplayMember = "IME_PREZIME";
            }

            session.Close();
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
                MANEKEN maneken = listBoxModels.Items[index] as MANEKEN;

                ModelsDetailsForm modelDetailsForm = new ModelsDetailsForm(maneken.MATICNI_BROJ);
                modelDetailsForm.Show();
            }
        }

        private void listBoxDesigners_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBoxDesigners.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                MODNI_KREATOR mkr = listBoxDesigners.Items[index] as MODNI_KREATOR;

                DesignersDetailsForm mkrDetailsForm = new DesignersDetailsForm(mkr.MATICNI_BROJ);
                mkrDetailsForm.Show();
            }
        }
    }
}
