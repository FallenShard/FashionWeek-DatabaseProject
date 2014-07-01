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
    public partial class DesignersForm : Form
    {
        private ISession m_session;

        private IList<MODNI_KREATOR> m_designers;

        public DesignersForm()
        {
            InitializeComponent();

            m_session = DataAccessLayer.DataLayer.GetSession();

            // Clear the listView and prepare the columns
            listViewDesigners.Clear();
            listViewDesigners.Columns.Add("PERSONAL NUMBER");
            listViewDesigners.Columns.Add("FIRST NAME");
            listViewDesigners.Columns.Add("LAST NAME");
            listViewDesigners.Columns.Add("BIRTH DATE");
            listViewDesigners.Columns.Add("FASHION HOUSE");
            listViewDesigners.Columns.Add("GENDER");
            listViewDesigners.Columns.Add("COUNTRY");
        }

        private void RefreshDesigners()
        {
            // Clear the items inside the listView
            listViewDesigners.Items.Clear();

            // Grab the designers with a query from the open session
            IQuery q = m_session.CreateQuery("FROM MODNI_KREATOR");
            m_designers = q.List<MODNI_KREATOR>();

            // Iterate and add data from the designers
            foreach (MODNI_KREATOR mk in m_designers)
            {
                ListViewItem lvi = new ListViewItem(mk.MATICNI_BROJ.ToString());
                lvi.SubItems.Add(mk.IME);
                lvi.SubItems.Add(mk.PREZIME);
                lvi.SubItems.Add(mk.DATUM_RODJENJA.ToString("MM/dd/yyyy"));
                lvi.SubItems.Add(mk.MODNA_KUCA);
                lvi.SubItems.Add(mk.POL.ToString());
                lvi.SubItems.Add(mk.ZEMLJA_POREKLA);
                lvi.Tag = mk;

                listViewDesigners.Items.Add(lvi);
            }

            // Adjust initial column widths
            ListView.ColumnHeaderCollection lch = listViewDesigners.Columns;
            for (int i = 0; i < lch.Count; i++)
            {
                lch[i].Width = -1;
                int dataSize = lch[i].Width;
                lch[i].Width = -2;
                int colSize = lch[i].Width;
                lch[i].Width = dataSize > colSize ? -1 : -2;
            }
        }

        private void DesignersForm_Load(object sender, EventArgs e)
        {
            // Initial display
            RefreshDesigners();
        }

        public MODNI_KREATOR GetSelectedDesigner()
        {
            // If no designers have been selected, display error message
            if (listViewDesigners.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a designer first.", "Error");
                return null;
            }

            // Otherwise, return the first selected one
            MODNI_KREATOR mk = (MODNI_KREATOR) listViewDesigners.SelectedItems[0].Tag;
            return mk;
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            RefreshDesigners();
        }

        private void buttonAddNew_Click(object sender, EventArgs e)
        {
            DesignersAddForm addDesignerForm = new DesignersAddForm();

            // Show the created form as a dialog
            if (addDesignerForm.ShowDialog() == DialogResult.OK)
            {
                // Refresh the listView if user confirmed the addition
                RefreshDesigners();
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            // Get the selected designer
            MODNI_KREATOR selectedDesigner = GetSelectedDesigner();

            if (selectedDesigner != null)
            {
                DesignersEditForm editDesignerForm = new DesignersEditForm(m_session, selectedDesigner);

                if (editDesignerForm.ShowDialog() == DialogResult.OK)
                {
                    // Refresh the listView if user confirmed the edit
                    RefreshDesigners();
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // Get the selected designer
            MODNI_KREATOR designer = GetSelectedDesigner();

            if (designer != null && MessageBox.Show("Are you sure you want to delete the selected designer?",
                "Delete Designer", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                // Workaround
                designer.modneRevije.Clear();
                m_session.SaveOrUpdate(designer);
                m_session.Flush();

                // Delete the selected designer
                m_session.Delete(designer);
                m_session.Flush();

                RefreshDesigners();
            }
        }

        private void buttonDetails_Click(object sender, EventArgs e)
        {
            // Get the selected designer
            MODNI_KREATOR selectedDesigner = GetSelectedDesigner();

            if (selectedDesigner != null)
            {
                DesignersDetailsForm detailsDesignerForm = new DesignersDetailsForm(selectedDesigner.MATICNI_BROJ);

                detailsDesignerForm.Show();
            }
        }

        private void buttonHistory_Click(object sender, EventArgs e)
        {
            IList<string> inputStrings = new List<string>();

            if (listViewDesigners.SelectedItems.Count > 1)
            {
                // If designers were selected, extract their personal numbers
                foreach (ListViewItem item in listViewDesigners.SelectedItems)
                    inputStrings.Add(item.SubItems[0].Text);
            }

            if (inputStrings.Count == 0)
            {
                MessageBox.Show("Select at least two designers to see their history!", "Check Details");
                return;
            }

            try
            {
                IQuery q = m_session.CreateQuery("FROM MODNA_REVIJA");
                IList<MODNA_REVIJA> shows = q.List<MODNA_REVIJA>();

                bool found = false;
                IList<string> sharedShows = new List<string>();

                foreach (MODNA_REVIJA show in shows)
                {
                    IList<MODNI_KREATOR> designers = show.modniKreatori;
                    IList<string> designersNumbers = new List<string>();

                    foreach (MODNI_KREATOR designer in designers)
                    {
                        designersNumbers.Add(designer.MATICNI_BROJ.ToString());
                    }

                    int matchings = 0;

                    foreach (string inputNumber in inputStrings)
                    {
                        if (designersNumbers.Contains(inputNumber)) matchings++;
                    }

                    if (matchings == inputStrings.Count)
                    {
                        found = true;
                        sharedShows.Add(show.NAZIV);
                    }
                }

                string outString = "";

                if (!found) outString = "These designers didn't organize any shows together.";
                else
                {
                    outString = "Designers organized the following shows together:\n";
                    foreach (string showName in sharedShows)
                        outString += "\n\t" + showName;
                }

                MessageBox.Show(outString, "Show History");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DesignersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_session.Close();
        }

        private void listViewDesigners_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Get the selected designer
            MODNI_KREATOR selectedDesigner = GetSelectedDesigner();

            if (selectedDesigner != null)
            {
                DesignersDetailsForm detailsDesignerForm = new DesignersDetailsForm(selectedDesigner.MATICNI_BROJ);

                detailsDesignerForm.Show();
            }
        }
    }
}
