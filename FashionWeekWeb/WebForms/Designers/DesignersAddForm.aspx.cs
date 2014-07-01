using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NHibernate;
using DataAccessLayer.Entiteti;

public partial class WebForms_Designers_DesignersAddForm : System.Web.UI.Page
{
    private IList<MODNA_REVIJA> m_shows;                        // Shows that a designer can register to
    private IList<int> m_designerPrimaryKeys = new List<int>(); // Used to check primary key input validation

    protected void Page_Load(object sender, EventArgs e)
    {
        GetShows();

        if (!IsPostBack)
        {
            PopulateListBox();
        }
    }

    private void GetShows()
    {
        // Get a session to read in fashion shows
        ISession session = DataAccessLayer.DataLayer.GetSession();

        // Grab the available shows with a query
        m_shows = session.CreateQuery("FROM MODNA_REVIJA").List<MODNA_REVIJA>();

        // Use the open session to grab primary keys as well
        IList<MODNI_KREATOR> designers = session.CreateQuery("FROM MODNI_KREATOR").List<MODNI_KREATOR>();
        foreach (MODNI_KREATOR mk in designers)
            m_designerPrimaryKeys.Add(mk.MATICNI_BROJ);

        // Close the opened session
        session.Close();
    }

    private void PopulateListBox()
    {
        boxShows.DataSource = m_shows;
        boxShows.DataValueField = "REDNI_BROJ";
        boxShows.DataTextField = "NAZIV";
        boxShows.DataBind();
    }

    private void SetAttributes(MODNI_KREATOR designer)
    {
        // Personal number
        designer.MATICNI_BROJ = Int32.Parse(textBoxPN.Text);

        // First name
        designer.IME = textBoxFirstName.Text;

        // Last name
        designer.PREZIME = textBoxLastName.Text;

        // Birth date
        designer.DATUM_RODJENJA = datePicker.SelectedDate;

        // Gender
        if (radioButtonFemale.Checked) designer.POL = 'F';
        else if (radioButtonMale.Checked) designer.POL = 'M';

        // Country
        designer.ZEMLJA_POREKLA = textBoxCountry.Text;

        // Fashion house
        designer.MODNA_KUCA = textBoxFashionHouse.Text;

        // Fashion shows
        foreach (ListItem item in boxShows.Items)
        {
            if (item.Selected)
                foreach (MODNA_REVIJA show in m_shows)
                {
                    if (item.Value == show.REDNI_BROJ.ToString())
                    {
                        designer.modneRevije.Add(show);
                        break;
                    }
                }
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
            errorMessages.Add("First name should be 1-20 characters long");
        if (System.Text.RegularExpressions.Regex.IsMatch(textBoxLastName.Text, @"\d"))
            errorMessages.Add("First name should contain only alphabet characters");

        // Birth date
        DateTime pickedDate = datePicker.SelectedDate;
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
            // If this is executed, no database-breaking errors occured
            return true;
        else
        {
            // This branch displays the errors on the screen
            string message = "The following errors have been found: " + "<br />";
            foreach (string error in errorMessages)
                message += "  -  " + error + "<br />";

            labelError.Text = message;
            return false;
        }
    }

    protected void buttonOK_Click(object sender, EventArgs e)
    {
        if (ValidateInput())
        {
            // Create new designer
            MODNI_KREATOR designer = new MODNI_KREATOR();
            SetAttributes(designer);

            ISession session = DataAccessLayer.DataLayer.GetSession();

            try
            {
                // Try to save the current designer
                session.Save(designer);
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

            Response.Redirect("DesignersForm.aspx");
        }
    }

    protected void buttonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("DesignersForm.aspx");
    }
}