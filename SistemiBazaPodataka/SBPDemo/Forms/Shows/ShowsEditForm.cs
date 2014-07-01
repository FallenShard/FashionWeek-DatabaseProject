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
    public partial class ShowsEditForm : Form
    {
        private ISession m_session;

        // Designer currently being edited
        private MODNA_REVIJA m_show;

        // Lists of available designers and models
        private IList<MODNI_KREATOR> m_designers;
        private IList<MANEKEN> m_models;

        private IList<MODNI_KREATOR> m_oldDesigners;
        private IList<MANEKEN> m_oldModels;

        public ShowsEditForm(ISession session, MODNA_REVIJA show)
        {
            InitializeComponent();

            // Received session holds the edited show
            m_session = session;
            m_show = show;

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

        private void InitializeShowData()
        {
            // Title
            textBoxTitle.Text = m_show.NAZIV; 

            // Date & Time
            dateTimePicker.Value = m_show.DATUM_VREME;

            // Location
            textBoxLoc.Text = m_show.MESTO;

            // Fashion Designers
            IList<MODNI_KREATOR> designers = m_show.modniKreatori;
            if (designers.Count > 0)
            {
                foreach (MODNI_KREATOR kreator in designers)
                {
                    for (int i = 0; i < checkedListBoxDesigners.Items.Count; i++)
                    {
                        MODNI_KREATOR listKreator = checkedListBoxDesigners.Items[i] as MODNI_KREATOR;
                        if (kreator.MATICNI_BROJ == listKreator.MATICNI_BROJ)
                            checkedListBoxDesigners.SetItemChecked(i, true);
                    }
                }
            }
            m_oldDesigners = new List<MODNI_KREATOR>(designers);

            // Fashion Models
            IList<MANEKEN> models = m_show.manekeni;
            if (models.Count > 0)
            {
                foreach (MANEKEN maneken in models)
                {
                    for (int i = 0; i < checkedListBoxModels.Items.Count; i++)
                    {
                        MANEKEN listManeken = checkedListBoxModels.Items[i] as MANEKEN;
                        if (listManeken.MATICNI_BROJ == maneken.MATICNI_BROJ)
                            checkedListBoxModels.SetItemChecked(i, true);
                    }
                }
            }
            m_oldModels = new List<MANEKEN>(models);
        }

        private void SetAttributes(MODNA_REVIJA mr)
        {
            // Title
            m_show.NAZIV = textBoxTitle.Text;

            // Name
            m_show.DATUM_VREME = dateTimePicker.Value;

            // Location
            m_show.MESTO = textBoxLoc.Text;

            // Designers
            // Iterate the selected designers and add them to the show
            CheckedListBox.CheckedItemCollection selectedDesigners = checkedListBoxDesigners.CheckedItems;
            m_show.modniKreatori.Clear();
            foreach (var item in selectedDesigners)
            {
                MODNI_KREATOR kreator = item as MODNI_KREATOR;
                if (!kreator.modneRevije.Contains(m_show))
                    kreator.modneRevije.Add(m_show);
                m_show.modniKreatori.Add(kreator);
            }

            foreach (MODNI_KREATOR kreator in m_oldDesigners)
                if (!m_show.modniKreatori.Contains(kreator))
                    kreator.modneRevije.Remove(m_show);

            
            // Models
            // Iterate the selected models and add them to the show
            CheckedListBox.CheckedItemCollection selectedModels = checkedListBoxModels.CheckedItems;
            m_show.manekeni.Clear();
            foreach (var item in selectedModels)
            {
                MANEKEN maneken = item as MANEKEN;
                if (!maneken.modneRevije.Contains(m_show))
                    maneken.modneRevije.Add(m_show);
                m_show.manekeni.Add(maneken);
            }

            foreach (MANEKEN model in m_oldModels)
                if (!m_show.manekeni.Contains(model))
                    model.modneRevije.Remove(m_show);
        }

        private void ShowsEditForm_Load(object sender, EventArgs e)
        {
            if (m_show == null)
            {
                MessageBox.Show("Error receiving data on show");
                this.Close();
            }

            // Load designer data into UI controls
            InitializeShowData();
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
                SetAttributes(m_show);
                try
                {
                    // Try to save the current show
                    m_session.SaveOrUpdate(m_show);
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
    }
}
