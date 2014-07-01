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
    public partial class AgenciesEditForm : Form
    {
        private ISession m_session;

        private MODNA_AGENCIJA m_agency;                            // Agency currently being edited

        private IList<MANEKEN> m_models = new List<MANEKEN>();      // List of available models for signing
        
        private IList<int> m_agencyPrimaryKeys = new List<int>();   // Used to check primary key input validation

        public AgenciesEditForm(ISession session, MODNA_AGENCIJA agency)
        {
            InitializeComponent();

            // Received session holds the edited agency
            m_session = session;
            m_agency = agency;

            // Load the available models
            GetModels();

            // Clear the listbox, set shows as data source and display their title
            checkedListBoxModels.Items.Clear();
            checkedListBoxModels.DataSource = m_models;
            checkedListBoxModels.DisplayMember = "IME_PREZIME";

            radioButtonLoc.Enabled = false;
            radioButtonInt.Enabled = false;
        }

        private void GetModels()
        {
            // Grab the available models with a query
            IList<MANEKEN> models = m_session.CreateQuery("FROM MANEKEN").List<MANEKEN>();
            for (int i = 0; i < models.Count; i++)
                if (models[i].modnaAgencija == null || models[i].modnaAgencija.PIB == m_agency.PIB) 
                    m_models.Add(models[i]);

            // Use the open session to grab primary keys as well
            IList<MODNA_AGENCIJA> agencies = m_session.CreateQuery("FROM MODNA_AGENCIJA").List<MODNA_AGENCIJA>();
            foreach (MODNA_AGENCIJA ag in agencies)
                m_agencyPrimaryKeys.Add(ag.PIB);
            m_agencyPrimaryKeys.Remove(m_agency.PIB);
        }

        private void InitializeAgencyData()
        {
            // PIN
            textBoxPIN.Text = m_agency.PIB.ToString();

            // Title
            textBoxName.Text = m_agency.NAZIV;

            // Date & Time
            dateTimePicker1.Value = m_agency.DATUM_OSNIVANJA;

            // Location
            textBoxHQ.Text = m_agency.SEDISTE;

            // Fashion Models
            IList<MANEKEN> manekeni = m_agency.manekeni;
            if (manekeni.Count > 0)
            {
                foreach (MANEKEN maneken in manekeni)
                {
                    for (int i = 0; i < checkedListBoxModels.Items.Count; i++)
                    {
                        MANEKEN listManeken = checkedListBoxModels.Items[i] as MANEKEN;
                        if (maneken.MATICNI_BROJ == listManeken.MATICNI_BROJ)
                            checkedListBoxModels.SetItemChecked(i, true);
                    }
                }
            }

            // Scope
            if (m_agency is INTERNACIONALNA_AGENCIJA)
            {
                IList<DRZAVA> countries = (m_agency as INTERNACIONALNA_AGENCIJA).drzave;
                foreach (DRZAVA country in countries)
                    listBoxCountries.Items.Add(country.NAZIV_DRZAVE);
                radioButtonInt.Checked = true;
            }
            else
            {
                listBoxCountries.Enabled = false;
                buttonAdd.Enabled = false;
                buttonRemove.Enabled = false;
                textBoxCountries.Enabled = false;
                radioButtonLoc.Checked = true;
            }
        }

        private int SetAttributes(MODNA_AGENCIJA mag)
        {
            // Registration Number - Primary key
            int newKey = Int32.Parse(textBoxPIN.Text);

            // Name
            mag.NAZIV = textBoxName.Text;

            // Date
            mag.DATUM_OSNIVANJA = dateTimePicker1.Value;

            // Headquarters
            mag.SEDISTE = textBoxHQ.Text;

            // Models
            CheckedListBox.CheckedItemCollection selected = checkedListBoxModels.CheckedItems;
            foreach (MANEKEN man in mag.manekeni)
                man.modnaAgencija = null;
            mag.manekeni.Clear();

            // Iterate the selected models and add them to the new agency
            foreach (var item in selected)
            {
                MANEKEN model = item as MANEKEN;
                model.modnaAgencija = mag;
                mag.manekeni.Add(model);
            }

            // Countries
            if (mag is INTERNACIONALNA_AGENCIJA)
            {
                INTERNACIONALNA_AGENCIJA intMag = mag as INTERNACIONALNA_AGENCIJA;
                intMag.drzave.Clear();
                m_session.Update(intMag); // NHibernate refuses to make it work without this Update & Flush
                m_session.Flush();

                foreach (string item in listBoxCountries.Items)
                {
                    DRZAVA country = new DRZAVA();
                    country.NAZIV_DRZAVE = item;
                    country.int_agencija = intMag;

                    intMag.drzave.Add(country);
                }
            }

            return newKey;
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

        private void AgenciesEditForm_Load(object sender, EventArgs e)
        {
            if (m_agency == null)
            {
                MessageBox.Show("Error receiving data on agency");
                this.Close();
            }

            // Load designer data into UI controls
            InitializeAgencyData();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                int newKey = SetAttributes(m_agency);
                if (newKey != m_agency.PIB)
                    m_agency = PrimaryKeyChanged(newKey);

                try
                {
                    m_session.SaveOrUpdate(m_agency);
                    m_session.Flush();

                    // Everything went fine
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception saveExc)
                {
                    MessageBox.Show("Failed to save current session:\n" + saveExc.Message);
                    this.DialogResult = DialogResult.Cancel;
                }

                Close();
            }
        }

        private MODNA_AGENCIJA PrimaryKeyChanged(int newKey)
        {
            // Create an appropriate object
            MODNA_AGENCIJA newAgency = null;
            if (m_agency is INTERNACIONALNA_AGENCIJA)
                newAgency = new INTERNACIONALNA_AGENCIJA();
            else
                newAgency = new DOMACA_AGENCIJA();

            // Set the attributes of old mag to new mag, and reset to oldKey
            newAgency.PIB = newKey;
            newAgency.NAZIV = m_agency.NAZIV;
            newAgency.DATUM_OSNIVANJA = m_agency.DATUM_OSNIVANJA;
            newAgency.SEDISTE = m_agency.SEDISTE;
            newAgency.manekeni = new List<MANEKEN>(m_agency.manekeni);
            if (newAgency is INTERNACIONALNA_AGENCIJA)
            {
                // Build new countries if the agency is international
                foreach (DRZAVA drzava in (m_agency as INTERNACIONALNA_AGENCIJA).drzave)
                {
                    DRZAVA country = new DRZAVA();
                    country.NAZIV_DRZAVE = drzava.NAZIV_DRZAVE;
                    country.int_agencija = (newAgency as INTERNACIONALNA_AGENCIJA);
                    (newAgency as INTERNACIONALNA_AGENCIJA).drzave.Add(country);
                }
            }

            // Time to delete the old agency, remove all ties to other entities in the database
            // or else we'll cascade delete all things known to this agency
            foreach (MANEKEN model in m_agency.manekeni)
                model.modnaAgencija = null;
            m_agency.manekeni.Clear();
            if (m_agency is INTERNACIONALNA_AGENCIJA)
                (m_agency as INTERNACIONALNA_AGENCIJA).drzave.Clear();
            m_session.Update(m_agency);
            m_session.Flush();

            // Delete the old agency
            m_session.Delete(m_agency);
            m_session.Flush();

            // Reconnect the models with the new agency object
            foreach (MANEKEN model in newAgency.manekeni)
                model.modnaAgencija = newAgency;

            // Return the brand new agency with the new primary key
            return newAgency;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close this window?",
                Text, MessageBoxButtons.OKCancel) == DialogResult.OK)
                Close();
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

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            listBoxCountries.Items.Remove(listBoxCountries.SelectedItem);
        }
    }
}
