using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NHibernate;
using DataAccessLayer.Entiteti;

public partial class WebForms_Designers_DesignersEditForm : System.Web.UI.Page
{
    private IList<MODNA_REVIJA> m_shows;        // List of available shows

    private IList<int> m_designerPrimaryKeys = new List<int>();   // Used to check primary key input validation

    protected void Page_Load(object sender, EventArgs e)
    {
        GetShows();

        if (!IsPostBack)
        {
            PopulateListBox();

            int maticniBroj = -1;

            try
            {
                maticniBroj = Int32.Parse(Request.QueryString["matbr"]);

            }
            catch (Exception exc)
            {
                Response.Write(exc.Message);
            }

            InitializeDesignerData(maticniBroj);
        }

    }

    private void GetShows()
    {
        ISession session = DataAccessLayer.DataLayer.GetSession();

        // Grab the available shows with a query
        m_shows = session.CreateQuery("FROM MODNA_REVIJA").List<MODNA_REVIJA>();

        
        // Use the open session to grab primary keys as well
        IList<MODNI_KREATOR> designers = session.CreateQuery("FROM MODNI_KREATOR").List<MODNI_KREATOR>();
        foreach (MODNI_KREATOR mk in designers)
            m_designerPrimaryKeys.Add(mk.MATICNI_BROJ);
        m_designerPrimaryKeys.Remove(Int32.Parse(Request.QueryString["matbr"]));

        session.Close();
    }

    private void PopulateListBox()
    {
        boxShows.DataSource = m_shows;
        boxShows.DataValueField = "REDNI_BROJ";
        boxShows.DataTextField = "NAZIV";
        boxShows.DataBind();
    }

    private void InitializeDesignerData(int maticniBroj)
    {
        ISession session = DataAccessLayer.DataLayer.GetSession();
        MODNI_KREATOR designer = session.Get<MODNI_KREATOR>(maticniBroj);

        // Personal Number
        textBoxPN.Text = designer.MATICNI_BROJ.ToString();
        m_designerPrimaryKeys.Remove(maticniBroj);

        // First Name
        textBoxFirstName.Text = designer.IME;

        // Last Name
        textBoxLastName.Text = designer.PREZIME;

        // Birth Date
        if (designer.DATUM_RODJENJA == DateTime.MinValue)
            datePicker.SelectedDate = DateTime.Now;
        else
            datePicker.SelectedDate = designer.DATUM_RODJENJA;

        // Gender
        if (designer.POL == 'F') radioButtonFemale.Checked = true;
        if (designer.POL == 'M') radioButtonMale.Checked = true;

        // Country
        textBoxCountry.Text = designer.ZEMLJA_POREKLA;

        // Fashion House
        textBoxFashionHouse.Text = designer.MODNA_KUCA;

        // Fashion Shows
        IList<MODNA_REVIJA> shows = designer.modneRevije;
        if (shows.Count > 0)
        {
            foreach (MODNA_REVIJA show in shows)
            {
                for (int i = 0; i < boxShows.Items.Count; i++)
                {
                    if (show.REDNI_BROJ.ToString() == boxShows.Items[i].Value)
                        boxShows.Items[i].Selected = true;
                }
            }
        }
        session.Close();
    }

    private int SetAttributes(MODNI_KREATOR designer, ISession session)
    {
        // Save the new key for later
        int newKey = Int32.Parse(textBoxPN.Text);

        // First name
        designer.IME = textBoxFirstName.Text;

        // Last name
        designer.PREZIME = textBoxLastName.Text;

        // Birth date
        designer.DATUM_RODJENJA = datePicker.SelectedDate;

        // Gender
        if (radioButtonFemale.Checked) designer.POL = 'F';
        if (radioButtonMale.Checked) designer.POL = 'M';

        // Country
        designer.ZEMLJA_POREKLA = textBoxCountry.Text;

        // Fashion house
        designer.MODNA_KUCA = textBoxFashionHouse.Text;

        // Fashion shows
        m_shows = session.CreateQuery("FROM MODNA_REVIJA").List<MODNA_REVIJA>();

        // Iterate the selected shows and add them to the new designer
        designer.modneRevije.Clear();
        foreach (ListItem item in boxShows.Items)
        {
            if (item.Selected)
                foreach (MODNA_REVIJA show in m_shows)
                    if (show.REDNI_BROJ.ToString() == item.Value)
                    {
                        designer.modneRevije.Add(show);
                        break;
                    }
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

    protected void buttonOK_Click(object sender, EventArgs e)
    {
        if (ValidateInput())
        {
            ISession session = DataAccessLayer.DataLayer.GetSession();
            MODNI_KREATOR designer = session.Get<MODNI_KREATOR>(Int32.Parse(Request.QueryString["matbr"]));

            int newKey = SetAttributes(designer, session);

            // This is where it gets tricky
            // If we changed the key, perform a delete-insert chain
            // (NHibernate doesn't like direct updating of primary key)
            if (newKey != designer.MATICNI_BROJ)
                designer = PrimaryKeyChanged(designer, session, newKey);

            try
            {
                // Try to update the current designer
                session.SaveOrUpdate(designer);
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

    private MODNI_KREATOR PrimaryKeyChanged(MODNI_KREATOR oldDesigner, ISession session, int newKey)
    {
        MODNI_KREATOR newDesigner = new MODNI_KREATOR();

        // Fill the new designer with data
        newDesigner.MATICNI_BROJ = newKey;
        newDesigner.IME = oldDesigner.IME;
        newDesigner.PREZIME = oldDesigner.PREZIME;
        newDesigner.MODNA_KUCA = oldDesigner.MODNA_KUCA;
        newDesigner.DATUM_RODJENJA = oldDesigner.DATUM_RODJENJA;
        newDesigner.POL = oldDesigner.POL;
        newDesigner.ZEMLJA_POREKLA = oldDesigner.ZEMLJA_POREKLA;

        foreach (MODNA_REVIJA show in oldDesigner.modneRevije)
            newDesigner.modneRevije.Add(show);

        // Time to delete the old designer, without cascade-deleting our tables
        // Workaround
        oldDesigner.modneRevije.Clear();
        session.SaveOrUpdate(oldDesigner);
        session.Flush();

        // Delete the selected designer
        session.Delete(oldDesigner);
        session.Flush();

        return newDesigner;
    }

    protected void buttonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("DesignersForm.aspx");
    }
}