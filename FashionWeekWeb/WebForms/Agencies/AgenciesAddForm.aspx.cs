using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NHibernate;
using DataAccessLayer.Entiteti;

public partial class WebForms_Agencies_AgenciesAddForm : System.Web.UI.Page
{
    private IList<MANEKEN> m_models;                            // Used to connect the agency with models
    private IList<int> m_agencyPrimaryKeys = new List<int>();   // Used to check primary key input validation

    protected void Page_Load(object sender, EventArgs e)
    {
        // Grab available models on every page load
        getModels();

        if (!IsPostBack)
        {
            // If page is loaded for the first time, fill in the listbox
            PopulateListBox();
        }
    }

    private void getModels()
    {
        // Get a session to read in fashion models
        ISession session = DataAccessLayer.DataLayer.GetSession();

        // Grab the available models with a query, those without an agency
        m_models = session.CreateQuery("FROM MANEKEN WHERE MODNA_AGENCIJA_PIB = null").List<MANEKEN>();

        // Use the open session to grab primary keys as well to validate new agency's key
        IList<MODNA_AGENCIJA> agencies = session.CreateQuery("FROM MODNA_AGENCIJA").List<MODNA_AGENCIJA>();
        foreach (MODNA_AGENCIJA mk in agencies)
            m_agencyPrimaryKeys.Add(mk.PIB);

        // Close the opened session
        session.Close();
    }

    private void PopulateListBox()
    {
        boxModels.DataSource = m_models;
        boxModels.DataValueField = "MATICNI_BROJ";
        boxModels.DataTextField = "IME_PREZIME";
        boxModels.DataBind();
    }

    private void SetAttributes(MODNA_AGENCIJA mag)
    {
        // Registration Number
        mag.PIB = Int32.Parse(textBoxRegNum.Text);

        // Name
        mag.NAZIV = textBoxName.Text;

        // Date
        mag.DATUM_OSNIVANJA = datePicker.SelectedDate;

        // Headquarters
        mag.SEDISTE = textBoxHQ.Text;

        // Models
        ISession session = DataAccessLayer.DataLayer.GetSession();

        // We'll need to reload the models because the ones in the memory are from previous session
        m_models = session.CreateQuery("FROM MANEKEN WHERE MODNA_AGENCIJA_PIB = null").List<MANEKEN>();
        foreach (ListItem item in boxModels.Items)
        {
            if (item.Selected)
            {
                // If an item has been checked, add that model, but find the key in the list of models first
                foreach (MANEKEN model in m_models)
                {
                    if (model.MATICNI_BROJ.ToString() == item.Value)
                    {
                        model.modnaAgencija = mag;
                        mag.manekeni.Add(model);
                    }
                }
            }
        }

        // Close the session we used to regulate the models
        session.Close();
    }

    private bool ValidateInput()
    {
        // Concatenated error messages from multiple inputs
        IList<string> errorMessages = new List<string>();

        // Registration Number
        if (textBoxRegNum.Text.Length > 8 || textBoxRegNum.Text.Length == 0)
            errorMessages.Add("Registration number should be 1-8 digits long");
        try
        {
            int temp = Int32.Parse(textBoxRegNum.Text);
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

        if (errorMessages.Count == 0)
            // Everything's cool, no database-breaking input errors
            return true;
        else
        {
            // Process error messages and display them in the label
            string message = "The following errors have been found: " + "<br />";
            foreach (string error in errorMessages)
                message += "  -  " + error + "<br />";

            labelError.Text = message;
            return false;
        }
    }

    
   
    protected void buttonOK_Click(object sender, EventArgs e)
    {
        // Is the input okay?
        if (ValidateInput())
        {
            // Did we want an international agency?
            if (checkBoxInt.Checked)
            {
                // Create an international agency, fill its attributes and save it with a new session
                INTERNACIONALNA_AGENCIJA iag = new INTERNACIONALNA_AGENCIJA();
                SetAttributes(iag);
                foreach (ListItem item in boxCountries.Items)
                {
                    DRZAVA country = new DRZAVA();
                    country.NAZIV_DRZAVE = item.Text;
                    country.int_agencija = iag;
                    iag.drzave.Add(country);
                }

                ISession session = DataAccessLayer.DataLayer.GetSession();

                try
                {
                    // Try to save the current agency
                    session.Save(iag);
                    session.Flush();
                }
                catch (Exception saveExc)
                {
                    Response.Write(saveExc.Message);
                }
                finally
                {
                    // Close the session
                    session.Close();
                }
            }
            else
            {
                // Create a local agency, fill its attributes and save it with a new session
                DOMACA_AGENCIJA dag = new DOMACA_AGENCIJA();
                SetAttributes(dag);

                ISession session = DataAccessLayer.DataLayer.GetSession();

                try
                {
                    // Try to save the current agency
                    session.Save(dag);
                    session.Flush();
                }
                catch (Exception saveExc)
                {
                    Response.Write(saveExc.Message);
                }
                finally
                {
                    // Close the session
                    session.Close();
                }
            }

            // Everything went fine, we can go back to displaying agencies
            Response.Redirect("AgenciesForm.aspx");
        }
    }

    protected void buttonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AgenciesForm.aspx");
    }

    protected void buttonAdd_Click(object sender, EventArgs e)
    {
        // Error handling for adding new countries
        IList<string> errorMessages = new List<string>();

        if (textBoxCountries.Text.Length == 0 || textBoxCountries.Text.Length > 40)
            errorMessages.Add("Country names should contain 1-40 characters");

        if (System.Text.RegularExpressions.Regex.IsMatch(textBoxCountries.Text, @"\d"))
            errorMessages.Add("Country names should contain only alphabet characters");

        if (boxCountries.Items.Contains(new ListItem(textBoxCountries.Text)))
            errorMessages.Add("Current list of countries already contains this entry");

        if (errorMessages.Count == 0)
        {
            // Everything's okay, no database-breaking or obvious logical errors
            boxCountries.Items.Add(textBoxCountries.Text);
            textBoxCountries.Text = "";
        }
        else
        {
            // There's an error, display it to inform the user
            string message = "The following errors have been found: " + "<br />";
            foreach (string error in errorMessages)
                message += "  -  " + error + "<br />";

            labelError.Text = message;
        }
    }

    protected void buttonRemove_Click(object sender, EventArgs e)
    {
        boxCountries.Items.Remove(boxCountries.SelectedItem);
    }

    // This event controls the additional controls provided for international agencies
    protected void checkBoxInt_CheckedChanged(object sender, EventArgs e)
    {
        boxCountries.Enabled = checkBoxInt.Checked;
        buttonAdd.Enabled = checkBoxInt.Checked;
        buttonRemove.Enabled = checkBoxInt.Checked;
        textBoxCountries.Enabled = checkBoxInt.Checked;
    }
}