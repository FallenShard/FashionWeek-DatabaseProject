using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NHibernate;
using DataAccessLayer.Entiteti;

public partial class WebForms_Shows_ShowsEditForm : System.Web.UI.Page
{
    private IList<MODNI_KREATOR> m_designers;                           // These are all the available designers
    private IList<MANEKEN> m_models;                                    // These are all the available models

    private IList<MODNI_KREATOR> m_oldDesigners;                        // Old designers before the change
    private IList<MANEKEN> m_oldModels;                                 // Old models before the change

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get the initial list of designers/models every time
        GetModelsAndDesigners();

        if (!IsPostBack)
        {
            PopulateListBoxes();

            int redniBroj = -1;

            try
            {
                redniBroj = Int32.Parse(Request.QueryString["rbr"]);

            }
            catch (Exception exc)
            {
                Response.Write(exc.Message);
            }

            InitializeShowData(redniBroj);
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

    private void InitializeShowData(int redniBroj)
    {
        ISession session = DataAccessLayer.DataLayer.GetSession();
        MODNA_REVIJA show = session.Get<MODNA_REVIJA>(redniBroj);
        // Title
        textBoxTitle.Text = show.NAZIV;

        // Date & Time
        datePicker.SelectedDate = show.DATUM_VREME;

        // Location
        textBoxCity.Text = show.MESTO;

        // Fashion Designers
        IList<MODNI_KREATOR> designers = show.modniKreatori;
        if (designers.Count > 0)
        {
            foreach (MODNI_KREATOR kreator in designers)
            {
                for (int i = 0; i < boxDesigners.Items.Count; i++)
                {
                    if (kreator.MATICNI_BROJ.ToString() == boxDesigners.Items[i].Value)
                        boxDesigners.Items[i].Selected = true;
                }
            }
        }

        // Fashion Models
        IList<MANEKEN> models = show.manekeni;
        if (models.Count > 0)
        {
            foreach (MANEKEN maneken in models)
            {
                for (int i = 0; i < boxModels.Items.Count; i++)
                {
                    if (maneken.MATICNI_BROJ.ToString() == boxModels.Items[i].Value)
                        boxModels.Items[i].Selected = true;
                }
            }
        }

        session.Close();
    }

    private void SetAttributes(MODNA_REVIJA show, ISession session)
    {
        // Title
        show.NAZIV = textBoxTitle.Text;

        // Date & Time
        show.DATUM_VREME = datePicker.SelectedDate;

        // Location
        show.MESTO = textBoxCity.Text;


        // Refresh the designers by grabbing them from the new session
        m_designers = session.CreateQuery("FROM MODNI_KREATOR").List<MODNI_KREATOR>();

        // Refresh the models by grabbing them from the new session
        m_models = session.CreateQuery("FROM MANEKEN").List<MANEKEN>();

        // Designers

        // Keep track of old designers
        m_oldDesigners = new List<MODNI_KREATOR>(show.modniKreatori); 

        // Clear current designers in order to add newly selected
        show.modniKreatori.Clear();                     

        foreach (ListItem item in boxDesigners.Items)
            if (item.Selected)
            {
                foreach (MODNI_KREATOR designer in m_designers)
                {
                    if (item.Value == designer.MATICNI_BROJ.ToString())
                    {
                        // If selected designer doesn't contain current show, add it, otherwise it's already there
                        if (!designer.modneRevije.Contains(show))
                            designer.modneRevije.Add(show);
                        show.modniKreatori.Add(designer);
                        break;
                    }
                }
            }

        // Iterate through old designers and remove current show if new list doesn't contain them
        foreach (MODNI_KREATOR kreator in m_oldDesigners)
            if (!show.modniKreatori.Contains(kreator))
                kreator.modneRevije.Remove(show);

        // Models
        // The same algorithm here
        m_oldModels = new List<MANEKEN>(show.manekeni); 
        show.manekeni.Clear();

        foreach (ListItem item in boxModels.Items)
            if (item.Selected)
            {
                foreach (MANEKEN model in m_models)
                {
                    if (item.Value == model.MATICNI_BROJ.ToString())
                    {
                        if (!model.modneRevije.Contains(show))
                            model.modneRevije.Add(show);
                        show.manekeni.Add(model);
                        break;
                    }
                }
            } 

        foreach (MANEKEN model in m_oldModels)
            if (!show.manekeni.Contains(model))
                model.modneRevije.Remove(show);
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
            ISession session = DataAccessLayer.DataLayer.GetSession();
            MODNA_REVIJA show = session.Get<MODNA_REVIJA>(Int32.Parse(Request.QueryString["rbr"]));
            SetAttributes(show, session);

            try
            {
                session.SaveOrUpdate(show);
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