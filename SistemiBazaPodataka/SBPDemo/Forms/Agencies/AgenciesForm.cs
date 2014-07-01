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
    public partial class AgenciesForm : Form
    {
        private ISession m_session;

        private IList<MODNA_AGENCIJA> m_agencies;

        public AgenciesForm()
        {
            InitializeComponent();

            m_session = DataAccessLayer.DataLayer.GetSession();

            // Clear the listView and prepare the columns
            listViewAgencies.Clear();
            listViewAgencies.Columns.Add("REG NUMBER");
            listViewAgencies.Columns.Add("NAME");
            listViewAgencies.Columns.Add("FOUNDED");
            listViewAgencies.Columns.Add("HEADQUARTERS");
            listViewAgencies.Columns.Add("SCOPE");
        }

        private void RefreshAgencies()
        {
            // Clear the items inside the listView
            listViewAgencies.Items.Clear();

            // Grab the agencies with a query from the open session
            IQuery q = m_session.CreateQuery("FROM MODNA_AGENCIJA");
            m_agencies = q.List<MODNA_AGENCIJA>();

            // Iterate and add data from the designers
            foreach (MODNA_AGENCIJA mag in m_agencies)
            {
                ListViewItem lvi = new ListViewItem(mag.PIB.ToString());
                lvi.SubItems.Add(mag.NAZIV);
                lvi.SubItems.Add(mag.DATUM_OSNIVANJA.ToString("dd/MM/yyyy"));
                lvi.SubItems.Add(mag.SEDISTE);
                if (mag is DOMACA_AGENCIJA)
                    lvi.SubItems.Add("Local");
                else if (mag is INTERNACIONALNA_AGENCIJA)
                    lvi.SubItems.Add("International");
                lvi.Tag = mag;

                listViewAgencies.Items.Add(lvi);
            }

            // Adjust initial column widths
            ListView.ColumnHeaderCollection lch = listViewAgencies.Columns;
            for (int i = 0; i < lch.Count; i++)
            {
                lch[i].Width = -1;
                int dataSize = lch[i].Width;
                lch[i].Width = -2;
                int colSize = lch[i].Width;
                lch[i].Width = dataSize > colSize ? -1 : -2;
            }
        }

        private void AgenciesForm_Load(object sender, EventArgs e)
        {
            // Initial Display
            RefreshAgencies();
        }

        public MODNA_AGENCIJA GetSelectedAgency()
        {
            // If no agencies have been selected, display error message
            if (listViewAgencies.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an agency first.", "Error");
                return null;
            }

            // Otherwise, return the first selected one
            MODNA_AGENCIJA mag = listViewAgencies.SelectedItems[0].Tag as MODNA_AGENCIJA;
            return mag;
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            RefreshAgencies();
        }

        private void buttonAddNew_Click(object sender, EventArgs e)
        {
            AgenciesAddForm addAgencyForm = new AgenciesAddForm();

            // Show the created form as a dialog
            if (addAgencyForm.ShowDialog() == DialogResult.OK)
            {
                // Refresh the listView if user confirmed the addition
                RefreshAgencies();
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            // Get the selected designer
            MODNA_AGENCIJA selectedAgency = GetSelectedAgency();

            if (selectedAgency != null)
            {
                AgenciesEditForm editAgencyForm = new AgenciesEditForm(m_session, selectedAgency);

                if (editAgencyForm.ShowDialog() == DialogResult.OK)
                {
                    // Refresh the listView if user confirmed the edit
                    RefreshAgencies();
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // Get the selected designer
            MODNA_AGENCIJA agency = GetSelectedAgency();

            if (agency != null && MessageBox.Show("Are you sure you want to delete the selected agency?",
                "Delete Agency", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                // Workaround
                foreach (MANEKEN man in agency.manekeni)
                    man.modnaAgencija = null;
                agency.manekeni.Clear();
                if (agency is INTERNACIONALNA_AGENCIJA)
                    (agency as INTERNACIONALNA_AGENCIJA).drzave.Clear();
                m_session.SaveOrUpdate(agency);
                m_session.Flush();

                // Delete the selected agency
                m_session.Delete(agency);
                m_session.Flush();

                RefreshAgencies();
            }
        }

        private void buttonDetails_Click(object sender, EventArgs e)
        {
            // Get the selected show
            MODNA_AGENCIJA selectedAgency = GetSelectedAgency();

            if (selectedAgency != null)
            {
                AgenciesDetailsForm detailsAgencyForm = new AgenciesDetailsForm(selectedAgency.PIB);
                detailsAgencyForm.Show();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AgenciesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_session.Close();
        }

        private void listViewAgencies_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Get the selected show
            MODNA_AGENCIJA selectedAgency = GetSelectedAgency();

            if (selectedAgency != null)
            {
                AgenciesDetailsForm detailsAgencyForm = new AgenciesDetailsForm(selectedAgency.PIB);
                detailsAgencyForm.Show();
            }
        }
    }
}
