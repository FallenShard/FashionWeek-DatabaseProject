using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NHibernate;
using DataAccessLayer.Entiteti;

public partial class WebForms_Models_ModelsAddForm : System.Web.UI.Page
{
    private IList<MODNA_REVIJA> m_shows;
    private IList<MODNA_AGENCIJA> m_agencies;
    private IList<int> m_modelPrimaryKeys = new List<int>();   // Used to check primary key input validation

    protected void Page_Load(object sender, EventArgs e)
    {
        GetShowsAndAgencies();

        if (!IsPostBack)
        {
            PopulateControls();
        }

        // Initially hide the special guest controls
        labelOccupation.Visible = checkBoxSG.Checked;
        textBoxOccupation.Visible = checkBoxSG.Checked;
    }

    private void GetShowsAndAgencies()
    {
        // Get a session to read in fashion shows
        ISession session = DataAccessLayer.DataLayer.GetSession();

        // Grab the available shows with a query
        m_shows = session.CreateQuery("FROM MODNA_REVIJA").List<MODNA_REVIJA>();
        m_agencies = session.CreateQuery("FROM MODNA_AGENCIJA").List<MODNA_AGENCIJA>();
        MODNA_AGENCIJA nullAgency = new MODNA_AGENCIJA();
        nullAgency.NAZIV = "(None)";
        nullAgency.PIB = -1;
        m_agencies.Add(nullAgency);

        // Use the open session to grab primary keys as well
        IList<MANEKEN> models = session.CreateQuery("FROM MANEKEN").List<MANEKEN>();
        foreach (MANEKEN mk in models)
            m_modelPrimaryKeys.Add(mk.MATICNI_BROJ);

        // Close the opened session
        session.Close();
    }

    private void PopulateControls()
    {
        boxShows.DataSource = m_shows;
        boxShows.DataValueField = "REDNI_BROJ";
        boxShows.DataTextField = "NAZIV";
        boxShows.DataBind();

        dropDownListAgency.DataSource = m_agencies;
        dropDownListAgency.DataValueField = "PIB";
        dropDownListAgency.DataTextField = "NAZIV";
        dropDownListAgency.DataBind();
        dropDownListAgency.SelectedIndex = dropDownListAgency.Items.Count - 1;

        var eyeColors = new[] { "Black", "Brown", "Blue", 
                                    "Amber", "Green", "Purple",
                                    "Grey", "Almond", "Hazel", "" };

        var hairColors = new[] { "Black", "Blonde", "Dark brown", 
                                     "Brown", "Red", "" };

        dropDownListEyes.DataSource = eyeColors;
        dropDownListEyes.DataBind();
        dropDownListEyes.SelectedIndex = dropDownListEyes.Items.Count - 1;

        dropDownListHair.DataSource = hairColors;
        dropDownListHair.DataBind();
        dropDownListHair.SelectedIndex = dropDownListHair.Items.Count - 1;
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
        ListItem selectedAgency = dropDownListAgency.SelectedItem;
        if (selectedAgency.Text != "(None)")
        {
            ISession session = DataAccessLayer.DataLayer.GetSession();
            model.modnaAgencija = session.Get<MODNA_AGENCIJA>(Int32.Parse(selectedAgency.Value));
            session.Close();
        }
        else
            model.modnaAgencija = null;
            
        // Birth Date
        model.DATUM_RODJENJA = datePicker.SelectedDate;

        // Gender
        if (radioButtonFemale.Checked) model.POL = 'F';
        if (radioButtonMale.Checked) model.POL = 'M';

        // Eye & Hair colors
        model.BOJA_KOSE = dropDownListHair.SelectedItem.Text;
        model.BOJA_OCIJU = dropDownListEyes.SelectedItem.Text;

        // Height, weight and clothing number
        model.VISINA = Int32.Parse(textBoxHeight.Text);
        model.TEZINA = Int32.Parse(textBoxWeight.Text);
        model.KONFEKCIJSKI_BROJ = Int32.Parse(textBoxCSize.Text);

        // Fashion shows
        List<ListItem> selectedShows = new List<ListItem>();
        foreach (ListItem item in boxShows.Items)
            if (item.Selected) selectedShows.Add(item);
        foreach (ListItem selected in selectedShows)
        {
            foreach (MODNA_REVIJA show in m_shows)
            {
                if (selected.Value == show.REDNI_BROJ.ToString())
                {
                    model.modneRevije.Add(show);
                    break;
                }
            }
        }

        // Magazines
        foreach (ListItem magazine in boxMagazines.Items)
        {
            CASOPIS cas = new CASOPIS();
            cas.maneken = model;
            cas.NASLOV_CASOPISA = magazine.Text;

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
        DateTime pickedDate = datePicker.SelectedDate;
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
                    errorMessages.Add("Height (male) should be in 170-220 range or 0 if unknown");
            }
            else if (radioButtonFemale.Checked)
            {
                if (temp != 0 && (temp < 140 || temp > 190))
                    errorMessages.Add("Height (female) should be in 140-190 range or 0 if unknown");
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
                    errorMessages.Add("Weight (male) should be in 70-110 range or 0 if unknown");
            }
            else if (radioButtonFemale.Checked)
            {
                if (temp != 0 && (temp < 45 || temp > 70))
                    errorMessages.Add("Weight (female) should be in 45-70 range or 0 if unknown");
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
            string message = "The following errors have been found: " + "<br />";
            foreach (string error in errorMessages)
                message += "  -  " + error + "<br />";

            labelError.Text = message;
            return false;
        }
    }

    protected void buttonAdd_Click(object sender, EventArgs e)
    {
        IList<string> errorMessages = new List<string>();

        if (textBoxMagazines.Text.Length == 0 || textBoxMagazines.Text.Length > 15)
            errorMessages.Add("Magazine titles should contain 1-15 characters");

        if (boxMagazines.Items.Contains(new ListItem(textBoxMagazines.Text)))
            errorMessages.Add("Current list of magazines already contains this entry");

        if (errorMessages.Count == 0)
        {
            boxMagazines.Items.Add(textBoxMagazines.Text);
            textBoxMagazines.Text = "";
        }
        else
        {
            string message = "The following errors have been found: " + "<br />";
            foreach (string error in errorMessages)
                message += "  -  " + error + "<br />";

            labelError.Text = message;
        }
    }
    protected void buttonRemove_Click(object sender, EventArgs e)
    {
        boxMagazines.Items.Remove(boxMagazines.SelectedItem);
    }

    protected void buttonOK_Click(object sender, EventArgs e)
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
                MANEKEN man = new MANEKEN();
                SetAttributes(man);

                ISession session = DataAccessLayer.DataLayer.GetSession();

                try
                {
                    // Try to save the current model
                    session.Save(man);
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

            Response.Redirect("ModelsForm.aspx");
        }
    }
    protected void buttonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ModelsForm.aspx");
    }

    protected void checkBoxSG_CheckedChanged(object sender, EventArgs e)
    {
        labelOccupation.Visible = checkBoxSG.Checked;
        textBoxOccupation.Visible = checkBoxSG.Checked;
    }
}