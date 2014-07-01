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
    public partial class ShowsForm : Form
    {
        private ISession m_session;

        private List<MODNA_REVIJA> m_shows;

        public ShowsForm()
        {
            InitializeComponent();

            m_session = DataAccessLayer.DataLayer.GetSession();

            // Clear the listView and prepare the columns
            listViewShows.Clear();
            listViewShows.Columns.Add("NO");
            listViewShows.Columns.Add("TITLE");
            listViewShows.Columns.Add("DATE");
            listViewShows.Columns.Add("TIME");
            listViewShows.Columns.Add("LOCATION");
        }

        private void RefreshShows()
        {
            // Clear the items inside the listView
            listViewShows.Items.Clear();

            // Grab the shows with a query from the open session
            m_shows = new List<MODNA_REVIJA>(m_session.CreateQuery("FROM MODNA_REVIJA").List<MODNA_REVIJA>());
            m_shows.Sort((x, y) => DateTime.Compare(x.DATUM_VREME, y.DATUM_VREME));

            // Iterate and add data from the shows
            int redBr = 1;
            foreach (MODNA_REVIJA mr in m_shows)
            {
                ListViewItem lvi = new ListViewItem(redBr++.ToString());
                lvi.SubItems.Add(mr.NAZIV);
                lvi.SubItems.Add(mr.DATUM_VREME.ToShortDateString());
                lvi.SubItems.Add(mr.DATUM_VREME.ToShortTimeString());
                lvi.SubItems.Add(mr.MESTO);
                lvi.Tag = mr;
                listViewShows.Items.Add(lvi);
            }

            // Adjust initial column widths
            ListView.ColumnHeaderCollection lch = listViewShows.Columns;
            for (int i = 0; i < lch.Count; i++)
            {
                lch[i].Width = -1;
                int dataSize = lch[i].Width;
                lch[i].Width = -2;
                int colSize = lch[i].Width;
                lch[i].Width = dataSize > colSize ? -1 : -2;
            }
        }

        public MODNA_REVIJA GetSelectedShow()
        {
            // If no shows have been selected, display error message
            if (listViewShows.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a show first.", "Error");
                return null;
            }

            // Otherwise, return the first selected one
            MODNA_REVIJA mr = (MODNA_REVIJA)listViewShows.SelectedItems[0].Tag;
            return mr;
        }

        private void ShowsForm_Load(object sender, EventArgs e)
        {
            // Initial display
            RefreshShows();
        }

        private void ShowsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_session.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            // Initial display
            RefreshShows();
        }

        private void buttonAddNew_Click(object sender, EventArgs e)
        {
            ShowsAddForm addShowForm = new ShowsAddForm();

            // Show the created form as a dialog
            if (addShowForm.ShowDialog() == DialogResult.OK)
            {
                // Refresh the listView if user confirmed the addition
                RefreshShows();
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            // Get the selected designer
            MODNA_REVIJA selectedShow = GetSelectedShow();

            if (selectedShow != null)
            {
                ShowsEditForm editShowForm = new ShowsEditForm(m_session, selectedShow);

                if (editShowForm.ShowDialog() == DialogResult.OK)
                {
                    // Refresh the listView if user confirmed the edit
                    RefreshShows();
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // Get the selected show
            MODNA_REVIJA show = GetSelectedShow();

            if (show != null && MessageBox.Show("Are you sure you want to delete the selected show?",
                "Delete Show", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                // Workaround
                foreach (MODNI_KREATOR mk in show.modniKreatori)
                {
                    mk.modneRevije.Remove(show);
                    m_session.SaveOrUpdate(mk);
                    m_session.Flush();
                }

                foreach (MANEKEN maneken in show.manekeni)
                {
                    maneken.modneRevije.Remove(show);
                    m_session.SaveOrUpdate(maneken);
                    m_session.Flush();
                }

                show.modniKreatori.Clear();
                show.manekeni.Clear();

                // Delete the selected show
                m_session.Delete(show);
                m_session.Flush();

                RefreshShows();
            }
        }

        private void buttonDetails_Click(object sender, EventArgs e)
        {
            // Get the selected show
            MODNA_REVIJA selectedShow = GetSelectedShow();

            if (selectedShow != null)
            {
                ShowsDetailsForm detailsShowsForm = new ShowsDetailsForm(selectedShow.REDNI_BROJ);

                detailsShowsForm.Show();
            }
        }

        private void listViewShows_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Get the selected show
            MODNA_REVIJA selectedShow = GetSelectedShow();

            if (selectedShow != null)
            {
                ShowsDetailsForm detailsShowsForm = new ShowsDetailsForm(selectedShow.REDNI_BROJ);

                detailsShowsForm.Show();
            }
        }
    }
}
