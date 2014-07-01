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
    public partial class ModelsForm : Form
    {
        private ISession m_session;

        private IList<MANEKEN> m_models;

        public ModelsForm()
        {
            InitializeComponent();

            m_session = DataAccessLayer.DataLayer.GetSession();

            // Clear the listView and prepare the columns
            listViewModels.Clear();
            listViewModels.Columns.Add("PERSONAL NUMBER");
            listViewModels.Columns.Add("FIRST NAME");
            listViewModels.Columns.Add("LAST NAME");
            listViewModels.Columns.Add("BIRTH DATE");
            listViewModels.Columns.Add("GENDER");
            listViewModels.Columns.Add("EYE COLOR");
            listViewModels.Columns.Add("HAIR COLOR");
            listViewModels.Columns.Add("WEIGHT");
            listViewModels.Columns.Add("HEIGHT");
            listViewModels.Columns.Add("CLOTHING SIZE");
            listViewModels.Columns.Add("MODELING AGENCY");
        }

        private void RefreshModels()
        {
            listViewModels.Items.Clear();

            IQuery q = m_session.CreateQuery("FROM MANEKEN");
            m_models = q.List<MANEKEN>();

            foreach (MANEKEN man in m_models)
            {
                ListViewItem lvi = new ListViewItem(man.MATICNI_BROJ.ToString());
                lvi.SubItems.Add(man.IME);
                lvi.SubItems.Add(man.PREZIME);
                lvi.SubItems.Add(man.DATUM_RODJENJA.ToShortDateString());
                lvi.SubItems.Add(man.POL.ToString());
                lvi.SubItems.Add(man.BOJA_OCIJU);
                lvi.SubItems.Add(man.BOJA_KOSE);
                lvi.SubItems.Add(man.TEZINA.ToString());
                lvi.SubItems.Add(man.VISINA.ToString());
                lvi.SubItems.Add(man.KONFEKCIJSKI_BROJ.ToString());
                lvi.SubItems.Add(man.modnaAgencija != null ? man.modnaAgencija.NAZIV : "");
                lvi.Tag = man;
                listViewModels.Items.Add(lvi);
            }

            ListView.ColumnHeaderCollection lch = listViewModels.Columns;
            for (int i = 0; i < lch.Count; i++)
            {
                lch[i].Width = -1;
                int dataSize = lch[i].Width;
                lch[i].Width = -2;
                int colSize = lch[i].Width;
                lch[i].Width = dataSize > colSize ? -1 : -2;
            }
        }

        private void ModelsForm_Load(object sender, EventArgs e)
        {
            RefreshModels();
        }

        public MANEKEN GetSelectedModel()
        {
            // If no designers have been selected, display error message
            if (listViewModels.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a model first.", "Error");
                return null;
            }

            // Otherwise, return the first selected one
            MANEKEN man = (MANEKEN)listViewModels.SelectedItems[0].Tag;
            return man;
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            RefreshModels();
        }

        private void buttonAddNew_Click(object sender, EventArgs e)
        {
            ModelsAddForm modelsAddForm = new ModelsAddForm();

            // Show the created form as a dialog
            if (modelsAddForm.ShowDialog() == DialogResult.OK)
            {
                // Refresh the listView if user confirmed the addition
                RefreshModels();
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            // Get the selected designer
            MANEKEN selectedModel = GetSelectedModel();

            if (selectedModel != null)
            {
                ModelsEditForm editModelForm = new ModelsEditForm(m_session, selectedModel);

                if (editModelForm.ShowDialog() == DialogResult.OK)
                {
                    // Refresh the listView if user confirmed the edit
                    RefreshModels();
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // Get the selected designer
            MANEKEN model = GetSelectedModel();

            if (model != null&& MessageBox.Show("Are you sure you want to delete the selected model?",
                "Delete Model", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                // Workaround
                model.modneRevije.Clear();
                model.casopisi.Clear();
                model.modnaAgencija = null;
                m_session.SaveOrUpdate(model);
                m_session.Flush();

                // Delete the selected designer
                m_session.Delete(model);
                m_session.Flush();

                RefreshModels();
            }
        }

        private void buttonDetails_Click(object sender, EventArgs e)
        {
            // Get the selected show
            MANEKEN selectedModel = GetSelectedModel();

            if (selectedModel != null)
            {
                ModelsDetailsForm detailsModelForm = new ModelsDetailsForm(selectedModel.MATICNI_BROJ);
                detailsModelForm.Show();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ModelsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_session.Close();
        }

        private void listViewModels_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Get the selected show
            MANEKEN selectedModel = GetSelectedModel();

            if (selectedModel != null)
            {
                ModelsDetailsForm detailsModelForm = new ModelsDetailsForm(selectedModel.MATICNI_BROJ);
                detailsModelForm.Show();
            }
        }
    }
}
