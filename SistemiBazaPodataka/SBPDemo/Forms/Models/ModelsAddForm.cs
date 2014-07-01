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
    public partial class ModelsAddForm : Form
    {
        private IList<MODNA_REVIJA> m_shows;
        private IList<MODNA_AGENCIJA> m_agencies;
        private IList<int> m_modelPrimaryKeys = new List<int>();   // Used to check primary key input validation

        public ModelsAddForm()
        {
            InitializeComponent();

            // Load the available shows and magazines
            GetShowsAndAgencies();

            var eyeColors = new[] { "Black", "Brown", "Blue", 
                                    "Amber", "Green", "Purple",
                                    "Grey", "Almond", "Hazel", "" };

            var hairColors = new[] { "Black", "Blonde", "Dark brown", 
                                     "Brown", "Red", "" };

            // Clear the listbox, set shows as data source and display their title
            checkedListBoxShows.Items.Clear();
            checkedListBoxShows.DataSource = m_shows;
            checkedListBoxShows.DisplayMember = "NAZIV";

            // Clear the combobox, set agencies as data source and display their names
            comboBoxAgencies.Items.Clear();
            comboBoxAgencies.DataSource = m_agencies;
            comboBoxAgencies.DisplayMember = "NAZIV";
            comboBoxAgencies.SelectedItem = comboBoxAgencies.Items[comboBoxAgencies.Items.Count - 1];

            // Clear the combobox, set eye colors as data source and display their values
            comboBoxEyes.Items.Clear();
            comboBoxEyes.DataSource = eyeColors;
            comboBoxEyes.SelectedItem = comboBoxEyes.Items[comboBoxEyes.Items.Count - 1];

            // Clear the combobox, set hair colors as data source and display their values
            comboBoxHair.Items.Clear();
            comboBoxHair.DataSource = hairColors;
            comboBoxHair.SelectedItem = comboBoxHair.Items[comboBoxHair.Items.Count - 1];
            
            // Initially hide the special guest controls
            labelOccupation.Enabled = false;
            textBoxOccupation.Enabled = false;
        }

        private void GetShowsAndAgencies()
        {
            // Get a session to read in fashion shows
            ISession session = DataAccessLayer.DataLayer.GetSession();

            // Grab the available shows with a query
            m_shows = session.CreateQuery("FROM MODNA_REVIJA").List<MODNA_REVIJA>();

            // Grab the available agencies with a query, and add a "null" options
            m_agencies = session.CreateQuery("FROM MODNA_AGENCIJA").List<MODNA_AGENCIJA>();
            MODNA_AGENCIJA nullAgency = new MODNA_AGENCIJA();
            nullAgency.NAZIV = "(None)";
            m_agencies.Add(nullAgency);

            // Grab the primary keys for validation
            IList<MANEKEN> models = session.CreateQuery("FROM MANEKEN").List<MANEKEN>();
            foreach (MANEKEN man in models)
                m_modelPrimaryKeys.Add(man.MATICNI_BROJ);

            // Close the opened session
            session.Close();
        }

        private void SetAttributes(MANEKEN model)
        {
            // Personal number
            model.MATICNI_BROJ = Int32.Parse(textBoxPN.Text);

            // First name
            model.IME = textBoxFirstName.Text;

            // Last name
            model.PREZIME = textBoxLastName.Text;

            // Agency
            MODNA_AGENCIJA agency = comboBoxAgencies.SelectedItem as MODNA_AGENCIJA;
            if (agency.NAZIV != "(None)")
                model.modnaAgencija = agency;

            // Birth Date
            model.DATUM_RODJENJA = dateTimePicker1.Value;

            // Gender
            if (radioButtonFemale.Checked) model.POL = 'F';
            if (radioButtonMale.Checked) model.POL = 'M';

            // Eye & Hair colors
            model.BOJA_KOSE  = comboBoxHair.SelectedItem as string;
            model.BOJA_OCIJU = comboBoxEyes.SelectedItem as string;

            // Height, weight and clothing number
            model.VISINA = Int32.Parse(textBoxHeight.Text);
            model.TEZINA = Int32.Parse(textBoxWeight.Text);
            model.KONFEKCIJSKI_BROJ = Int32.Parse(textBoxCSize.Text);

            // Fashion shows
            CheckedListBox.CheckedItemCollection selected = checkedListBoxShows.CheckedItems;

            // Iterate the selected shows and add them to the new model
            foreach (var item in selected)
            {
                MODNA_REVIJA mr = item as MODNA_REVIJA;
                model.modneRevije.Add(mr);
            }

            // Magazines
            foreach (string magazine in listBoxMag.Items)
            {
                CASOPIS cas = new CASOPIS();
                cas.maneken = model;
                cas.NASLOV_CASOPISA = magazine;

                model.casopisi.Add(cas);
            }
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
                        errorMessages.Add("Weight (female) should be in 45-70 kg range or 0 if unknown");
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
                        errorMessages.Add("Clothing size (male) should be in 40-70 range or 0 if unknown");
                }
                else if (radioButtonFemale.Checked)
                {
                    if (temp != 0 && (temp < 34 || temp > 58))
                        errorMessages.Add("Clothing size (female) should be in 34-58 range or 0 if unknown");
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

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                if (checkBoxSG.Checked)
                {
                    SPECIJALNI_GOST sg = new SPECIJALNI_GOST();
                    sg.ZANIMANJE = textBoxOccupation.Text;
                    SetAttributes(sg);

                    ISession session = DataAccessLayer.DataLayer.GetSession();

                    try
                    {
                        // Try to save the current special guest
                        session.Save(sg);
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
                        // Close the session
                        session.Close();
                    }
                }
                else
                {
                    MANEKEN man = new MANEKEN();
                    SetAttributes(man);

                    ISession session = DataAccessLayer.DataLayer.GetSession();

                    try
                    {
                        // Try to save the current model
                        session.Save(man);
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
                        // Close the session
                        session.Close();
                    }
                }

                Close();
            }
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
                errorMessages.Add("Current list of magazines already contains this entry");

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
