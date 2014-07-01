using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NHibernate;
using DataAccessLayer.Entiteti;

public partial class WebForms_Agencies_AgenciesEditForm : System.Web.UI.Page
{
    private IList<MANEKEN> m_models;                            // Used to connect available models with the agency
    private IList<int> m_agencyPrimaryKeys = new List<int>();   // Used to check primary key input validation

    protected void Page_Load(object sender, EventArgs e)
    {
        // Grab the models on each page load
        GetModels();

        if (!IsPostBack)
        {
            PopulateListBox();

            int pib = -1;

            try
            {
                pib = Int32.Parse(Request.QueryString["pib"]);

            }
            catch (Exception exc)
            {
                Response.Write(exc.Message);
            }

            InitializeAgencyData(pib);
        }
    }

    private void GetModels()
    {
        // Get a session to read in fashion models
        ISession session = DataAccessLayer.DataLayer.GetSession();

        // Grab the available models with a query - we want the ones without an agency, or already tied to current agency
        int currentPIB = Int32.Parse(Request.QueryString["pib"]);
        m_models = session.CreateQuery("FROM MANEKEN WHERE (MODNA_AGENCIJA_PIB = null OR MODNA_AGENCIJA_PIB = " + currentPIB + ")").List<MANEKEN>();

        // Use the open session to grab primary keys as well for validation
        IList<MODNA_AGENCIJA> agencies = session.CreateQuery("FROM MODNA_AGENCIJA").List<MODNA_AGENCIJA>();
        foreach (MODNA_AGENCIJA mk in agencies)
            m_agencyPrimaryKeys.Add(mk.PIB);

        // Remove the key that current agency holds, when we validate against it, we want to pass the validation
        m_agencyPrimaryKeys.Remove(Int32.Parse(Request.QueryString["pib"]));

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

    private void InitializeAgencyData(int pib)
    {
        // Grab a session and get the agency
        ISession session = DataAccessLayer.DataLayer.GetSession();

        // We use Get instead of Load because Load is lazy and doesn't contain type information
        // Besides, Get is good, it doesn't return a proxy if there is no object, it returns null
        MODNA_AGENCIJA agency = session.Get<MODNA_AGENCIJA>(pib);

        // PIN
        textBoxRegNum.Text = agency.PIB.ToString();

        // Title
        textBoxName.Text = agency.NAZIV;

        // Date & Time
        datePicker.SelectedDate = agency.DATUM_OSNIVANJA;

        // Location
        textBoxHQ.Text = agency.SEDISTE;

        // Fashion Models
        foreach (MANEKEN maneken in agency.manekeni)
        {
            for (int i = 0; i < boxModels.Items.Count; i++)
            {
                if (maneken.MATICNI_BROJ.ToString() == boxModels.Items[i].Value)
                    boxModels.Items[i].Selected = true;
            }
        }

        // Scope - this check here is why we used Get instead of Load
        if (agency is INTERNACIONALNA_AGENCIJA)
        {
            // Enable UI for country editing
            IList<DRZAVA> countries = (agency as INTERNACIONALNA_AGENCIJA).drzave;
            foreach (DRZAVA country in countries)
                boxCountries.Items.Add(country.NAZIV_DRZAVE);
            checkBoxInt.Checked = true;
            boxCountries.Enabled = true;
            buttonAdd.Enabled = true;
            buttonRemove.Enabled = true;
            textBoxCountries.Enabled = true;
        }
        else
        {
            // Agency is local, no need to have international agency-specific UI visible
            boxCountries.Visible = false;
            buttonAdd.Visible = false;
            buttonRemove.Visible = false;
            textBoxCountries.Visible = false;
            checkBoxInt.Visible = false;
            labelCountries.Visible = false;
            labelInt.Visible = false;
        }
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
            return true;
        else
        {
            string message = "The following errors have been found: " + "<br />";
            foreach (string error in errorMessages)
                message += "  -  " + error + "<br />";

            labelError.Text = message;
            return false;
        }
    }

    private int SetAttributes(MODNA_AGENCIJA mag, ISession session)
    {
        // Registration Number, we don't store it immediately, because it can change from the previous one
        // Instead, it is returned as result to be checked later
        int newKey = Int32.Parse(textBoxRegNum.Text);

        // Name
        mag.NAZIV = textBoxName.Text;

        // Date
        mag.DATUM_OSNIVANJA = datePicker.SelectedDate;

        // Headquarters
        mag.SEDISTE = textBoxHQ.Text;

        // Models
        m_models = session.CreateQuery("FROM MANEKEN WHERE (MODNA_AGENCIJA_PIB = null OR MODNA_AGENCIJA_PIB = " + Int32.Parse(Request.QueryString["pib"]) + ")").List<MANEKEN>();
        foreach (MANEKEN man in mag.manekeni)
            man.modnaAgencija = null;
        mag.manekeni.Clear();

        // Clear all the models first and then iterate and add the selected ones
        foreach (ListItem item in boxModels.Items)
        {
            if (item.Selected)
            {
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

        // Countries
        if (mag is INTERNACIONALNA_AGENCIJA)
        {
            INTERNACIONALNA_AGENCIJA intMag = mag as INTERNACIONALNA_AGENCIJA;
            intMag.drzave.Clear();
            session.Update(intMag); // NHibernate refuses to make it work without this Update & Flush
            session.Flush();

            foreach (ListItem item in boxCountries.Items)
            {
                DRZAVA country = new DRZAVA();
                country.NAZIV_DRZAVE = item.Text;
                country.int_agencija = intMag;

                intMag.drzave.Add(country);
            }
        }

        return newKey;
    }

    protected void buttonOK_Click(object sender, EventArgs e)
    {
        if (ValidateInput())
        {
            ISession session = DataAccessLayer.DataLayer.GetSession();
            MODNA_AGENCIJA mag = session.Get<MODNA_AGENCIJA>(Int32.Parse(Request.QueryString["pib"]));

            int newKey = SetAttributes(mag, session);

            // This is where it gets tricky
            // If we changed the key, perform a delete-insert chain
            // (NHibernate doesn't like direct updating of primary key)
            if (newKey != mag.PIB)
                mag = PrimaryKeyChanged(mag, session, newKey);

            try
            {
                session.SaveOrUpdate(mag);
                session.Flush();

            }
            catch (Exception saveExc)
            {
                Response.Write(saveExc.Message);
            }
            finally
            {
                // Close the session and the form
                session.Close();
            }

            Response.Redirect("AgenciesForm.aspx");
        }
    }

    private MODNA_AGENCIJA PrimaryKeyChanged(MODNA_AGENCIJA oldAgency, ISession session, int newKey)
    {
        // Create an appropriate object
        MODNA_AGENCIJA newAgency = null;
        if (oldAgency is INTERNACIONALNA_AGENCIJA)
            newAgency = new INTERNACIONALNA_AGENCIJA();
        else
            newAgency = new DOMACA_AGENCIJA();
        
        // Set the attributes of old mag to new mag, and reset to oldKey
        newAgency.PIB = newKey;
        newAgency.NAZIV = oldAgency.NAZIV;
        newAgency.DATUM_OSNIVANJA = oldAgency.DATUM_OSNIVANJA;
        newAgency.SEDISTE = oldAgency.SEDISTE;
        newAgency.manekeni = new List<MANEKEN>(oldAgency.manekeni);
        if (newAgency is INTERNACIONALNA_AGENCIJA)
        {
            // Build new countries if the agency is international
            foreach (DRZAVA drzava in (oldAgency as INTERNACIONALNA_AGENCIJA).drzave)
            {
                DRZAVA country = new DRZAVA();
                country.NAZIV_DRZAVE = drzava.NAZIV_DRZAVE;
                country.int_agencija = (newAgency as INTERNACIONALNA_AGENCIJA);
                (newAgency as INTERNACIONALNA_AGENCIJA).drzave.Add(country);
            }
        }

        // Time to delete the old agency, remove all ties to other entities in the database
        // or else we'll cascade delete all things known to this agency
        foreach (MANEKEN model in oldAgency.manekeni)
            model.modnaAgencija = null;
        oldAgency.manekeni.Clear();
        if (oldAgency is INTERNACIONALNA_AGENCIJA)
            (oldAgency as INTERNACIONALNA_AGENCIJA).drzave.Clear();
        session.Update(oldAgency);
        session.Flush();

        // Delete the old agency
        session.Delete(oldAgency);
        session.Flush();

        // Reconnect the models with the new agency object
        foreach (MANEKEN model in newAgency.manekeni)
            model.modnaAgencija = newAgency;

        // Return the brand new agency with the new primary key
        return newAgency;
    }

    protected void buttonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AgenciesForm.aspx");
    }

    protected void buttonRemove_Click(object sender, EventArgs e)
    {
        boxCountries.Items.Remove(boxCountries.SelectedItem);
    }

    protected void buttonAdd_Click(object sender, EventArgs e)
    {
        // Error checking for new country additions
        IList<string> errorMessages = new List<string>();

        if (textBoxCountries.Text.Length == 0 || textBoxCountries.Text.Length > 40)
            errorMessages.Add("Country names should contain 1-40 characters");

        if (System.Text.RegularExpressions.Regex.IsMatch(textBoxCountries.Text, @"\d"))
            errorMessages.Add("Country names should contain only alphabet characters");

        if (boxCountries.Items.Contains(new ListItem(textBoxCountries.Text)))
            errorMessages.Add("Current list of countries already contains this entry");

        if (errorMessages.Count == 0)
        {
            boxCountries.Items.Add(textBoxCountries.Text);
            textBoxCountries.Text = "";
        }
        else
        {
            string message = "The following errors have been found: " + "<br />";
            foreach (string error in errorMessages)
                message += "  -  " + error + "<br />";

            labelError.Text = message;
        }
    }
}