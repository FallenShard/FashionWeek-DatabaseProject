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
    public partial class AgenciesAddForm : Form
    {
        private IList<MANEKEN> m_models = new List<MANEKEN>();      // List of available models for signing
        private IList<int> m_agencyPrimaryKeys = new List<int>();   // Used to check primary key input validation

        public AgenciesAddForm()
        {
            InitializeComponent();

            // Load the available models
            GetModels();

            // Clear the listbox, set models as data source and display their names
            checkedListBoxModels.Items.Clear();
            checkedListBoxModels.DataSource = m_models;
            checkedListBoxModels.DisplayMember = "IME_PREZIME";

            // Initially disable controls for international agencies until user enables them via radio button
            listBoxCountries.Enabled = false;
            buttonAdd.Enabled = false;
            buttonRemove.Enabled = false;
            textBoxCountries.Enabled = false;
        }

        private void GetModels()
        {
            // Get a session to read in fashion models
            ISession session = DataAccessLayer.DataLayer.GetSession();

            // Grab the available models with a query
            IList<MANEKEN> models = session.CreateQuery("FROM MANEKEN").List<MANEKEN>();
            for (int i = 0; i < models.Count; i++)
                if (models[i].modnaAgencija == null) m_models.Add(models[i]);

            // Use the open session to grab primary keys as well
            IList<MODNA_AGENCIJA> agencies = session.CreateQuery("FROM MODNA_AGENCIJA").List<MODNA_AGENCIJA>();
            foreach (MODNA_AGENCIJA ag in agencies)
                m_agencyPrimaryKeys.Add(ag.PIB);

            // Close the opened session
            session.Close();
        }

        private void SetAttributes(MODNA_AGENCIJA mag)
        {
            // PIN Validation
            mag.PIB = Int32.Parse(textBoxPIN.Text);

            // Name
            mag.NAZIV = textBoxName.Text;

            // Date
            mag.DATUM_OSNIVANJA = dateTimePicker1.Value;

            // Headquarters
            mag.SEDISTE = textBoxHQ.Text;

            // Models
            CheckedListBox.CheckedItemCollection selected = checkedListBoxModels.CheckedItems;

            // Iterate the selected models and add them to the new agency
            foreach (var item in selected)
            {
                MANEKEN model = item as MANEKEN;
                model.modnaAgencija = mag;
                mag.manekeni.Add(model);
            }
        }

        private bool ValidateInput()
        {
            // Concatenated error messages from multiple inputs
            IList<string> errorMessages = new List<string>();

            // Registration Number
            if (textBoxPIN.Text.Length > 8 || textBoxPIN.Text.Length == 0)
                errorMessages.Add("Registration number should be 1-8 digits long");
            try
            {
                int temp = Int32.Parse(textBoxPIN.Text);
                if (m_agencyPrimaryKeys.Contains(temp))
                    errorMessages.Add("There is already an agency registered with this number");
            }
            catch (Exception)
            {
                errorMessages.Add("Registration number should contain only digits");
            }

            // Agency Name
            if (textBoxName.Text.Length == 0 || textBoxName.Text.Length > 30)
                errorMessages.Add("Agency name should be 1-30 characters long");

            // Headquarters
            if (textBoxHQ.Text.Length > 30)
                errorMessages.Add("Headquarters location should be 0-30 characters long");
            if (System.Text.RegularExpressions.Regex.IsMatch(textBoxHQ.Text, @"\d"))
                errorMessages.Add("Headquarters location should contain only alphabet characters");

            // Scope (Agency type)
            if (!radioButtonLoc.Checked && !radioButtonInt.Checked)
                errorMessages.Add("Please specify whether the agency is local or international");

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
            bool validation = ValidateInput();

            // Local agency was selected
            if (validation && radioButtonLoc.Checked)
            {
                DOMACA_AGENCIJA dag = new DOMACA_AGENCIJA();
                SetAttributes(dag);

                ISession session = DataAccessLayer.DataLayer.GetSession();

                try
                {
                    // Try to save the current agency
                    session.Save(dag);
                    session.Flush();

                    // Everything went fine
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception saveExc)
                {
                    MessageBox.Show("Failed to save current session:\r\n" + saveExc.Message);
                    this.DialogResult = DialogResult.Cancel;
                }
                finally
                {
                    // Close the session and the form
                    session.Close();
                    Close();
                }
            }
            else if (validation && radioButtonInt.Checked)
            {
                INTERNACIONALNA_AGENCIJA iag = new INTERNACIONALNA_AGENCIJA();
                SetAttributes(iag);

                // Add the countries, if any
                foreach (var item in listBoxCountries.Items)
                {
                    DRZAVA country = new DRZAVA();
                    country.NAZIV_DRZAVE = item as string;
                    country.int_agencija = iag;
                    iag.drzave.Add(country);
                }

                ISession session = DataAccessLayer.DataLayer.GetSession();

                try
                {
                    // Try to save the current agency
                    session.Save(iag);
                    session.Flush();

                    // Everything went fine
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception saveExc)
                {
                    MessageBox.Show("Failed to save current session:\n" + saveExc.Message);
                    this.DialogResult = DialogResult.Cancel;
                }
                finally
                {
                    // Close the session and the form
                    session.Close();
                    Close();
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close this window?",
                Text, MessageBoxButtons.OKCancel) == DialogResult.OK)
                Close();
        }

        private void radioButtonInt_CheckedChanged(object sender, EventArgs e)
        {
            listBoxCountries.Enabled = radioButtonInt.Checked;
            buttonAdd.Enabled = radioButtonInt.Checked;
            buttonRemove.Enabled = radioButtonInt.Checked;
            textBoxCountries.Enabled = radioButtonInt.Checked;
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            listBoxCountries.Items.Remove(listBoxCountries.SelectedItem);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            IList<string> errorMessages = new List<string>();

            if (textBoxCountries.Text.Length == 0 || textBoxCountries.Text.Length > 40)
                errorMessages.Add("Country names should contain 1-40 characters");

            if (System.Text.RegularExpressions.Regex.IsMatch(textBoxCountries.Text, @"\d"))
                errorMessages.Add("Country names should contain only alphabet characters");

            if (listBoxCountries.Items.Contains(textBoxCountries.Text))
                errorMessages.Add("Current list of countries already contains this entry");

            if (errorMessages.Count == 0)
            {
                listBoxCountries.Items.Add(textBoxCountries.Text);
                textBoxCountries.Clear();
            }
            else
            {
                string message = "The following errors have been found: " + Environment.NewLine + Environment.NewLine;
                foreach (string error in errorMessages)
                    message += "  -  " + error + Environment.NewLine;

                MessageBox.Show(message, "Add New Country");
            }
        }
    }
}
