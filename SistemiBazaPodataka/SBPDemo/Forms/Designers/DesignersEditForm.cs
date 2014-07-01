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
    public partial class DesignersEditForm : Form
    {
        private ISession m_session;

        private MODNI_KREATOR m_designer;           // Designer currently being edited

        private IList<MODNA_REVIJA> m_shows;        // List of available shows

        private IList<int> m_designerPrimaryKeys = new List<int>();   // Used to check primary key input validation

        public DesignersEditForm(ISession session, MODNI_KREATOR designer)
        {
            InitializeComponent();

            // Received session holds the edited designer
            m_session = session;
            m_designer = designer;

            // Load the available shows
            GetShows();

            // Clear the listbox, set shows as data source and display their title
            checkedListBoxShows.Items.Clear();
            checkedListBoxShows.DataSource = m_shows;
            checkedListBoxShows.DisplayMember = "NAZIV";
        }

        private void GetShows()
        {
            // Grab the available shows with a query
            m_shows = m_session.CreateQuery("FROM MODNA_REVIJA").List<MODNA_REVIJA>();

            // Use the open session to grab primary keys as well
            IList<MODNI_KREATOR> designers = m_session.CreateQuery("FROM MODNI_KREATOR").List<MODNI_KREATOR>();
            foreach (MODNI_KREATOR mk in designers)
                m_designerPrimaryKeys.Add(mk.MATICNI_BROJ);
            m_designerPrimaryKeys.Remove(m_designer.MATICNI_BROJ);
        }

        private void InitializeDesignerData()
        {
            // Personal Number
            textBoxPN.Text = m_designer.MATICNI_BROJ.ToString();

            // First Name
            textBoxFirstName.Text = m_designer.IME;

            // Last Name
            textBoxLastName.Text = m_designer.PREZIME;

            // Birth Date
            if (m_designer.DATUM_RODJENJA == DateTime.MinValue)
                dateTimePicker1.Value = DateTime.Now;
            else
                dateTimePicker1.Value = m_designer.DATUM_RODJENJA;

            // Gender
            if (m_designer.POL == 'F') radioButtonFemale.Checked = true;
            if (m_designer.POL == 'M') radioButtonMale.Checked = true;
            
            // Country
            textBoxCountry.Text = m_designer.ZEMLJA_POREKLA;

            // Fashion House
            textBoxFashionHouse.Text = m_designer.MODNA_KUCA;

            // Fashion Shows
            IList<MODNA_REVIJA> shows = m_designer.modneRevije;
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

        private int SetAttributes(MODNI_KREATOR designer)
        {
            // Personal Number
            int newKey = Int32.Parse(textBoxPN.Text);

            // First name
            designer.IME = textBoxFirstName.Text;

            // Last name
            designer.PREZIME = textBoxLastName.Text;

            // Birth date
            designer.DATUM_RODJENJA = dateTimePicker1.Value;

            // Gender
            if (radioButtonFemale.Checked) designer.POL = 'F';
            if (radioButtonMale.Checked) designer.POL = 'M';

            // Country
            designer.ZEMLJA_POREKLA = textBoxCountry.Text;

            // Fashion house
            designer.MODNA_KUCA = textBoxFashionHouse.Text;

            // Fashion shows
            CheckedListBox.CheckedItemCollection selected = checkedListBoxShows.CheckedItems;

            // Iterate the selected shows and add them to the new designer
            m_designer.modneRevije.Clear();
            foreach (var item in selected)
            {
                MODNA_REVIJA mr = item as MODNA_REVIJA;
                designer.modneRevije.Add(mr);
            }

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
                if (m_designerPrimaryKeys.Contains(temp))
                    errorMessages.Add("There is already a designer with this personal number");
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
                errorMessages.Add("Designer should be at least 16 years old");

            // Gender
            if (!radioButtonFemale.Checked && !radioButtonMale.Checked)
                errorMessages.Add("Please select a gender for the designer");


            // Country
            if (textBoxCountry.Text.Length > 40)
                errorMessages.Add("Country should be 0-40 characters long");
            if (System.Text.RegularExpressions.Regex.IsMatch(textBoxCountry.Text, @"\d"))
                errorMessages.Add("Country should contain only alphabet characters");

            // Fashion house
            if (textBoxFashionHouse.Text.Length > 30)
                errorMessages.Add("Fashion house should be 0-30 characters long");

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
                int newKey = SetAttributes(m_designer);
                if (newKey != m_designer.MATICNI_BROJ)
                    m_designer = PrimaryKeyChanged(newKey);

                try
                {
                    // Try to save the current designer
                    m_session.SaveOrUpdate(m_designer);
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

        private MODNI_KREATOR PrimaryKeyChanged(int newKey)
        {
            MODNI_KREATOR newDesigner = new MODNI_KREATOR();

            // Fill the new designer with data
            newDesigner.MATICNI_BROJ = newKey;
            newDesigner.IME = m_designer.IME;
            newDesigner.PREZIME = m_designer.PREZIME;
            newDesigner.MODNA_KUCA = m_designer.MODNA_KUCA;
            newDesigner.DATUM_RODJENJA = m_designer.DATUM_RODJENJA;
            newDesigner.POL = m_designer.POL;
            newDesigner.ZEMLJA_POREKLA = m_designer.ZEMLJA_POREKLA;

            foreach (MODNA_REVIJA show in m_designer.modneRevije)
                newDesigner.modneRevije.Add(show);

            // Time to delete the old designer, without cascade-deleting our tables
            // Workaround
            m_designer.modneRevije.Clear();
            m_session.SaveOrUpdate(m_designer);
            m_session.Flush();

            // Delete the selected designer
            m_session.Delete(m_designer);
            m_session.Flush();

            return newDesigner;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close this window?",
                Text, MessageBoxButtons.OKCancel) == DialogResult.OK)
                Close();
        }

        private void DesignersEditForm_Load(object sender, EventArgs e)
        {
            if (m_designer == null)
            {
                MessageBox.Show("Error receiving data on designer");
                this.Close();
            }

            // Load designer data into UI controls
            InitializeDesignerData();
        }
    }
}
