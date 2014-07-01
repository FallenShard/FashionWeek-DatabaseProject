using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NHibernate;
using DataAccessLayer.Entiteti;

public partial class WebForms_Shows_ShowsAddForm : System.Web.UI.Page
{
    private IList<MODNI_KREATOR> m_designers;
    private IList<MANEKEN> m_models;

    protected void Page_Load(object sender, EventArgs e)
    {
        GetModelsAndDesigners();

        if (!IsPostBack)
        {
            PopulateListBoxes();
        }
    }

    private void GetModelsAndDesigners()
    {
        ISession session = DataAccessLayer.DataLayer.GetSession();
        // Grab the available designers with a query
        m_designers = session.CreateQuery("FROM MODNI_KREATOR").List<MODNI_KREATOR>();

        // Grab the available models with a query
        m_models = session.CreateQuery("FROM MANEKEN").List<MANEKEN>();

        session.Close();
    }

    private void PopulateListBoxes()
    {
        boxDesigners.DataSource = m_designers;
        boxDesigners.DataValueField = "MATICNI_BROJ";
        boxDesigners.DataTextField = "IME_PREZIME";
        boxDesigners.DataBind();

        boxModels.DataSource = m_models;
        boxModels.DataValueField = "MATICNI_BROJ";
        boxModels.DataTextField = "IME_PREZIME";
        boxModels.DataBind();
    }

    private void SetAttributes(MODNA_REVIJA show)
    {
        // Title
        show.NAZIV = textBoxTitle.Text;

        // Date & Time
        show.DATUM_VREME = datePicker.SelectedDate;

        // Location
        show.MESTO = textBoxCity.Text;

        ISession session = DataAccessLayer.DataLayer.GetSession();
        // Grab the available designers with a query
        m_designers = session.CreateQuery("FROM MODNI_KREATOR").List<MODNI_KREATOR>();

        // Grab the available models with a query
        m_models = session.CreateQuery("FROM MANEKEN").List<MANEKEN>();

        // Designers
        List<ListItem> selectedDesigners = new List<ListItem>();
        foreach (ListItem item in boxDesigners.Items)
            if (item.Selected) selectedDesigners.Add(item);
        foreach (ListItem selected in selectedDesigners)
        {
            foreach (MODNI_KREATOR designer in m_designers)
            {
            
                if (selected.Value == designer.MATICNI_BROJ.ToString())
                {
                    designer.modneRevije.Add(show);
                    show.modniKreatori.Add(designer);
                    break;
                }
            }        
        }

        // Models
        List<ListItem> selectedModels = new List<ListItem>();
        foreach (ListItem item in boxModels.Items)
            if (item.Selected) selectedModels.Add(item);
        foreach (ListItem selected in selectedModels)
        {
            foreach (MANEKEN model in m_models)
            {

                if (selected.Value == model.MATICNI_BROJ.ToString())
                {
                    model.modneRevije.Add(show);
                    show.manekeni.Add(model);
                    break;
                }
            }
        }

        session.Close();
    }

    private bool ValidateInput()
    {
        // Concatenated error messages from multiple inputs
        IList<string> errorMessages = new List<string>();

        // Title
        if (textBoxTitle.Text.Length > 30 || textBoxTitle.Text.Length == 0)
            errorMessages.Add("Title should be 1-30 characters long");
        if (System.Text.RegularExpressions.Regex.IsMatch(textBoxTitle.Text, @"\d"))
            errorMessages.Add("Title should contain only alphabet characters");

        // Location
        if (textBoxCity.Text.Length > 30)
            errorMessages.Add("Location should be 0-30 characters long");
        if (System.Text.RegularExpressions.Regex.IsMatch(textBoxCity.Text, @"\d"))
            errorMessages.Add("Location should contain only alphabet characters");

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
            MODNA_REVIJA show = new MODNA_REVIJA();

            SetAttributes(show);

            ISession session = DataAccessLayer.DataLayer.GetSession();

            try
            {
                session.Save(show);
                session.Flush();
            }
            catch (Exception saveExc)
            {
                Response.Write(saveExc.Message);
            }
            finally
            {
                session.Close();
            }

            Response.Redirect("ShowsForm.aspx");
        }
    }

    protected void buttonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShowsForm.aspx");
    }
}