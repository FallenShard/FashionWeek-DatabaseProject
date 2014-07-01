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
    public partial class ModelsEditForm : Form
    {
        private ISession m_session;

        private MANEKEN m_model;                                    // Designer currently being edited

        private IList<MODNA_REVIJA> m_shows;                        // List of available shows
        private IList<MODNA_AGENCIJA> m_agencies;                   // List of available agencies
        private IList<int> m_modelPrimaryKeys = new List<int>();    // Used to check primary key input validation
        private string[] m_eyeColors = { "Black", "Brown", "Blue", 
                                         "Amber", "Green", "Purple",
                                         "Grey", "Almond", "Hazel", "" };

        private string[] m_hairColors = { "Black", "Blonde", "Dark brown", 
                                          "Brown", "Red", "" };

        public ModelsEditForm(ISession session, MANEKEN model)
        {
            InitializeComponent();

            m_session = session;
            m_model = model;

            // Load the available shows and agencies
            GetShowsAndAgencies();

            // Clear the listbox, set shows as data source and display their title
            checkedListBoxShows.Items.Clear();
            checkedListBoxShows.DataSource = m_shows;
            checkedListBoxShows.DisplayMember = "NAZIV";

            comboBoxAgencies.Items.Clear();
            comboBoxAgencies.DataSource = m_agencies;
            comboBoxAgencies.DisplayMember = "NAZIV";
            comboBoxAgencies.SelectedItem = comboBoxAgencies.Items[comboBoxAgencies.Items.Count - 1];

            comboBoxEyes.Items.Clear();
            comboBoxEyes.DataSource = m_eyeColors;
            comboBoxEyes.SelectedItem = comboBoxEyes.Items[comboBoxEyes.Items.Count - 1];

            comboBoxHair.Items.Clear();
            comboBoxHair.DataSource = m_hairColors;
            comboBoxHair.SelectedItem = comboBoxHair.Items[comboBoxHair.Items.Count - 1];

            checkBoxSG.Enabled = false;

            bool status = checkBoxSG.Checked;
            if (!status)
            {
                labelOccupation.Enabled = false;
                textBoxOccupation.Enabled = false;
            }
        }

        private void GetShowsAndAgencies()
        {
            // Grab the available shows with a query
            m_shows = m_session.CreateQuery("FROM MODNA_REVIJA").List<MODNA_REVIJA>();

            // Grab the available agencies with a query, and add a "null" options
            m_agencies = m_session.CreateQuery("FROM MODNA_AGENCIJA").List<MODNA_AGENCIJA>();
            MODNA_AGENCIJA nullAgency = new MODNA_AGENCIJA();
            nullAgency.NAZIV = "(None)";
            m_agencies.Add(nullAgency);

            // Grab the primary keys for validation
            IList<MANEKEN> models = m_session.CreateQuery("FROM MANEKEN").List<MANEKEN>();
            foreach (MANEKEN man in models)
                m_modelPrimaryKeys.Add(man.MATICNI_BROJ);
            m_modelPrimaryKeys.Remove(m_model.MATICNI_BROJ);
        }

        private void InitializeModelData()
        {
            // Personal Number
            textBoxPN.Text = m_model.MATICNI_BROJ.ToString();

            // First Name
            textBoxFirstName.Text = m_model.IME;

            // Last Name
            textBoxLastName.Text = m_model.PREZIME;

            // Agency
            if (m_model.modnaAgencija != null)
            {
                foreach (MODNA_AGENCIJA mag in m_agencies)
                    if (m_model.modnaAgencija.PIB == mag.PIB)
                        comboBoxAgencies.SelectedItem = mag;
            }
            else
                comboBoxAgencies.SelectedItem = comboBoxAgencies.Items[comboBoxAgencies.Items.Count - 1];

            // Birth Date
            if (m_model.DATUM_RODJENJA == DateTime.MinValue)
                dateTimePicker1.Value = DateTime.Now;
            else
                dateTimePicker1.Value = m_model.DATUM_RODJENJA;

            // Gender
            if (m_model.POL == 'F') radioButtonFemale.Checked = true;
            if (m_model.POL == 'M') radioButtonMale.Checked = true;

            // Eyes
            foreach (string eyeColor in m_eyeColors)
                if (String.Equals(m_model.BOJA_OCIJU, eyeColor, StringComparison.OrdinalIgnoreCase))
                {
                    comboBoxEyes.SelectedItem = eyeColor;
                    break;
                }

            // Hair
            foreach (string hairColor in m_hairColors)
                if (String.Equals(m_model.BOJA_KOSE, hairColor, StringComparison.OrdinalIgnoreCase))
                {
                    comboBoxHair.SelectedItem = hairColor;
                    break;
                }

            // Height, Weight and Clothing Size
            textBoxHeight.Text = m_model.VISINA.ToString();
            textBoxWeight.Text = m_model.TEZINA.ToString();
            textBoxCSize.Text = m_model.KONFEKCIJSKI_BROJ.ToString();

            // Special Guest Flag
            if (m_model is SPECIJALNI_GOST)
            {
                checkBoxSG.Checked = true;
                textBoxOccupation.Text = (m_model as SPECIJALNI_GOST).ZANIMANJE;
            }
            else
                checkBoxSG.Checked = false;

            // Magazines
            foreach (CASOPIS cas in m_model.casopisi)
                listBoxMag.Items.Add(cas.NASLOV_CASOPISA);

            // Shows
            IList<MODNA_REVIJA> shows = m_model.modneRevije;
            if (shows.Count > 0)
            {
                foreach (MODNA_REVIJA mr in shows)
                {
                    for (int i = 0; i < checkedListBoxShows.Items.Count; i++)
                    {
                        MODNA_REVIJA mmr = checkedListBoxShows.Items[i] as MODNA_REVIJA;
                        if (mr.REDNI_BROJ == mmr.REDNI_BROJ)
                            checkedListBoxShows.SetItemChecked(i, true);
                    }
                }
            }
        }

        private int SetAttributes(MANEKEN model)
        {
            // Personal number
            int newKey = Int32.Parse(textBoxPN.Text);

            // First name
            model.IME = textBoxFirstName.Text;

            // Last name
            model.PREZIME = textBoxLastName.Text;

            // Agency
            MODNA_AGENCIJA agency = comboBoxAgencies.SelectedItem as MODNA_AGENCIJA;
            if (agency.NAZIV != "(None)")
                model.modnaAgencija = agency;
            else
                model.modnaAgencija = null;

            // Birth Date
            model.DATUM_RODJENJA = dateTimePicker1.Value;

            // Gender
            if (radioButtonFemale.Checked) model.POL = 'F';
            if (radioButtonMale.Checked) model.POL = 'M';

            // Eye & Hair colors
            model.BOJA_KOSE = comboBoxHair.SelectedItem as string;
            model.BOJA_OCIJU = comboBoxEyes.SelectedItem as string;

            // Height, weight and clothing number
            model.VISINA = Int32.Parse(textBoxHeight.Text);
            model.TEZINA = Int32.Parse(textBoxWeight.Text);
            model.KONFEKCIJSKI_BROJ = Int32.Parse(textBoxCSize.Text);

            // Fashion shows
            CheckedListBox.CheckedItemCollection selected = checkedListBoxShows.CheckedItems;

            // Iterate the selected shows and add them to the new model
            model.modneRevije.Clear();
            foreach (var item in selected)
            {
                MODNA_REVIJA mr = item as MODNA_REVIJA;
                model.modneRevije.Add(mr);
            }

            // Magazines
            model.casopisi.Clear();
            m_session.Update(model);      // NHibernate throws exception if model isn't updated & flushed here
            m_session.Flush();

            foreach (string magazine in listBoxMag.Items)
            {
                CASOPIS cas = new CASOPIS();
                cas.maneken = model;
                cas.NASLOV_CASOPISA = magazine;

                model.casopisi.Add(cas);
            }

            if (checkBoxSG.Checked)
                (m_model as SPECIJALNI_GOST).ZANIMANJE = textBoxOccupation.Text;

            return newKey;
        }

        private bool ValidateInput()
        {
            // Concatenated error messages from multiple inputs
            IList<string> errorMessages = new List<string>();

            // Registration Number
            if (textBoxPN.Text.Length > 8 || textBoxPN.Text.Length == 0)
                errorMessages.Add("Personal number should be 1-8 digits long");
            try
            {
                int temp = Int32.Parse(textBoxPN.Text);
                if (m_modelPrimaryKeys.Contains(temp))
                    errorMessages.Add("There is already a model with this personal number");
            }
            catch (Exception)
            {
                errorMessages.Add("Personal number should contain only digits");
            }

            // First name
            if (textBoxFirstName.Text.Length == 0 || textBoxFirstName.Text.Length > 20)
                errorMessages.Add("First name should be 1-20 characters long");
            if (System.Text.RegularExpressions.Regex.IsMatch(textBoxFirstName.Text, @"\d"))
                errorMessages.Add("First name should contain only alphabet characters");

            // Last name
            if (textBoxLastName.Text.Length == 0 || textBoxLastName.Text.Length > 20)
                errorMessages.Add("Last name should be 1-20 characters long");
            if (System.Text.RegularExpressions.Regex.IsMatch(textBoxLastName.Text, @"\d"))
                errorMessages.Add("Last name should contain only alphabet characters");

            // Birth date
            DateTime pickedDate = dateTimePicker1.Value;
            if (DateTime.Now.Year - pickedDate.Year < 16)
                errorMessages.Add("Model should be at least 16 years old");

            // Gender
            if (!radioButtonFemale.Checked && !radioButtonMale.Checked)
                errorMessages.Add("Please select a gender for the model");

            // Height
            try
            {
                int temp = Int32.Parse(textBoxHeight.Text);
                if (radioButtonMale.Checked)
                {
                    if (temp != 0 && (temp < 170 || temp > 220))
                        errorMessages.Add("Height (male) should be in 170-220 cm range or 0 if unknown");
                }
                else if (radioButtonFemale.Checked)
                {
                    if (temp != 0 && (temp < 140 || temp > 190))
                        errorMessages.Add("Height (female) should be in 140-190 cm range or 0 if unknown");
                }
                else
                    errorMessages.Add("Gender was not selected, height validation could not be determined");
            }
            catch (Exception)
            {
                errorMessages.Add("Height should contain exactly 3 digits");
            }

            // Weight
            try
            {
                int temp = Int32.Parse(textBoxWeight.Text);
                if (radioButtonMale.Checked)
                {
                    if (temp != 0 && (temp < 70 || temp > 110))
                        errorMessages.Add("Weight (male) should be in 70-110 kg range or 0 if unknown");
                }
                else if (radioButtonFemale.Checked)
                {
                    if (temp != 0 && (temp < 45 || temp > 70))
                        errorMessages.Add("weight (female) should be in 45-70 kg range or 0 if unknown");
                }
                else
                    errorMessages.Add("Gender was not selected, weight validation could not be determined");
            }
            catch (Exception)
            {
                errorMessages.Add("Weight should contain only 2-3 digits");
            }

            // Clothing Size
            try
            {
                int temp = Int32.Parse(textBoxCSize.Text);
                if (radioButtonMale.Checked)
                {
                    if (temp != 0 && (temp < 40 || temp > 70))
                        errorMessages.Add("Height (male) should be in 40-70 range or 0 if unknown");
                }
                else if (radioButtonFemale.Checked)
                {
                    if (temp != 0 && (temp < 34 || temp > 58))
                        errorMessages.Add("Height (female) should be in 34-58 range or 0 if unknown");
                }
                else
                    errorMessages.Add("Gender was not selected, clothing size validation could not be determined");
            }
            catch (Exception)
            {
                errorMessages.Add("Clothing size should contain exactly 2 digits");
            }

            // Occupation
            if (checkBoxSG.Checked)
            {
                if (textBoxOccupation.Text.Length > 30)
                    errorMessages.Add("Occupation should be 0-30 characters long");
                if (System.Text.RegularExpressions.Regex.IsMatch(textBoxOccupation.Text, @"\d"))
                    errorMessages.Add("Occupation should contain only alphabet characters");
            }

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

        private void ModelsEditForm_Load(object sender, EventArgs e)
        {
            if (m_model == null)
            {
                MessageBox.Show("Error receiving data on model");
                this.Close();
            }

            // Load model data into UI controls
            InitializeModelData();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                int newKey = SetAttributes(m_model);
                if (newKey != m_model.MATICNI_BROJ)
                    m_model = PrimaryKeyChanged(newKey);

                try
                { 
                    m_session.SaveOrUpdate(m_model);
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

        private MANEKEN PrimaryKeyChanged(int newKey)
        {
            MANEKEN newModel;
            if (m_model is SPECIJALNI_GOST)
                newModel = new SPECIJALNI_GOST();
            else
                newModel = new MANEKEN();

            // Copy old data into new object
            newModel.MATICNI_BROJ = newKey;
            newModel.IME = m_model.IME;
            newModel.BOJA_KOSE = m_model.BOJA_KOSE;
            newModel.BOJA_OCIJU = m_model.BOJA_OCIJU;
            newModel.DATUM_RODJENJA = m_model.DATUM_RODJENJA;
            newModel.KONFEKCIJSKI_BROJ = m_model.KONFEKCIJSKI_BROJ;
            newModel.POL = m_model.POL;
            newModel.PREZIME = m_model.PREZIME;
            newModel.TEZINA = m_model.TEZINA;
            newModel.VISINA = m_model.VISINA;
            newModel.modnaAgencija = m_model.modnaAgencija;
            if (m_model is SPECIJALNI_GOST)
                (newModel as SPECIJALNI_GOST).ZANIMANJE = (m_model as SPECIJALNI_GOST).ZANIMANJE;
            foreach (MODNA_REVIJA show in m_model.modneRevije)
                newModel.modneRevije.Add(show);
            foreach (CASOPIS cas in m_model.casopisi)
            {
                CASOPIS magazine = new CASOPIS();
                magazine.NASLOV_CASOPISA = cas.NASLOV_CASOPISA;
                magazine.maneken = newModel;
                newModel.casopisi.Add(magazine);
            }

            // Time to delete the old model, but clear it first from all other entities to prevent cascade deletes
            m_model.modneRevije.Clear();
            m_model.casopisi.Clear();
            m_model.modnaAgencija = null;
            m_session.SaveOrUpdate(m_model);
            m_session.Flush();

            // Delete the old model
            m_session.Delete(m_model);
            m_session.Flush();

            // Return a brand new model with modified key
            return newModel;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close this window?",
                Text, MessageBoxButtons.OKCancel) == DialogResult.OK)
                Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            labelOccupation.Enabled = checkBoxSG.Checked;
            textBoxOccupation.Enabled = checkBoxSG.Checked;
        }

        private void buttonAddMag_Click(object sender, EventArgs e)
        {
            IList<string> errorMessages = new List<string>();

            if (textBoxMag.Text.Length == 0 || textBoxMag.Text.Length > 15)
                errorMessages.Add("Magazine titles should contain 1-15 characters");

            if (listBoxMag.Items.Contains(textBoxMag.Text))
                errorMessages.Add("Current list of countries already contains this entry");

            if (errorMessages.Count == 0)
            {
                listBoxMag.Items.Add(textBoxMag.Text);
                textBoxMag.Clear();
            }
            else
            {
                string message = "The following errors have been found: " + Environment.NewLine + Environment.NewLine;
                foreach (string error in errorMessages)
                    message += "  -  " + error + Environment.NewLine;

                MessageBox.Show(message, "Add New Magazine");
            }
        }

        private void buttonRemoveMag_Click(object sender, EventArgs e)
        {
            listBoxMag.Items.Remove(listBoxMag.SelectedItem);
        }
    }
}
