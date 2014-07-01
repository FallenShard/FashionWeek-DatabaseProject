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
    public partial class ShowsAddForm : Form
    {
        private IList<MODNI_KREATOR> m_designers;
        private IList<MANEKEN> m_models;

        private ISession m_session;

        public ShowsAddForm()
        {
            InitializeComponent();

            m_session = DataAccessLayer.DataLayer.GetSession();

            // Load the available designers and models
            GetDesignersAndModels();

            // Clear the listbox, set shows as data source and display their title
            checkedListBoxDesigners.Items.Clear();
            checkedListBoxDesigners.DataSource = m_designers;
            checkedListBoxDesigners.DisplayMember = "IME_PREZIME";

            checkedListBoxModels.Items.Clear();
            checkedListBoxModels.DataSource = m_models;
            checkedListBoxModels.DisplayMember = "IME_PREZIME";
        }

        private void GetDesignersAndModels()
        {
            // Grab the available designers with a query
            m_designers = m_session.CreateQuery("FROM MODNI_KREATOR").List<MODNI_KREATOR>();

            // Grab the available models with a query
            m_models = m_session.CreateQuery("FROM MANEKEN").List<MANEKEN>();
        }

        private void SetAttributes(MODNA_REVIJA show)
        {
            // Title
            show.NAZIV = textBoxTitle.Text;

            // Date & Time
            show.DATUM_VREME = dateTimePicker.Value;

            // Location
            show.MESTO = textBoxLoc.Text;

            // Designers
            CheckedListBox.CheckedItemCollection selectedDesigners = checkedListBoxDesigners.CheckedItems;

            // Iterate the selected designers and add them to the new show
            foreach (var item in selectedDesigners)
            {
                MODNI_KREATOR mk = item as MODNI_KREATOR;
                mk.modneRevije.Add(show);
                show.modniKreatori.Add(mk);
            }

            // Models
            CheckedListBox.CheckedItemCollection selectedModels = checkedListBoxModels.CheckedItems;

            // Iterate the selected models and add them to the new show
            foreach (var item in selectedModels)
            {
                MANEKEN maneken = item as MANEKEN;
                maneken.modneRevije.Add(show);
                show.manekeni.Add(maneken);
            }
        }

        private bool ValidateInput()
        {
            // Concatenated error messages from multiple inputs
            IList<string> errorMessages = new List<string>();

            // Title
            if (textBoxTitle.Text.Length > 30 || textBoxTitle.Text.Length == 0)
                errorMessages.Add("Title should be 1-30 characters long");
            if (System.Text.RegularExpressions.Regex.IsMatch(textBoxTitle.Text, @"\d"))
                errorMessages.Add("Title should contain only alphabet characters");

            // Location
            if (textBoxLoc.Text.Length > 30)
                errorMessages.Add("Location should be 0-30 characters long");
            if (System.Text.RegularExpressions.Regex.IsMatch(textBoxLoc.Text, @"\d"))
                errorMessages.Add("Location should contain only alphabet characters");

            if (errorMessages.Count == 0)
                return true;
            else
            {
                string message = "The following errors have been found: " + Environment.NewLine + Environment.NewLine;
                foreach (string error in errorMessages)
                    message += "  -  " + error + Environment.NewLine;

                MessageBox.Show(message, Text);
                return false;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                // Create new show
                MODNA_REVIJA show = new MODNA_REVIJA();
                SetAttributes(show);

                try
                {
                    m_session.Save(show);
                    m_session.Flush();

                    // Everything went fine
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception saveExc)
                {
                    MessageBox.Show("Failed to save current session:\n" + saveExc.Message);
                    this.DialogResult = DialogResult.Cancel;
                }

                // Close the form
                Close();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close this window?",
                Text, MessageBoxButtons.OKCancel) == DialogResult.OK)
                Close();
        }

        private void ShowsAddForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_session.Close();
        }
    }
}
