using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NHibernate;
using DataAccessLayer.Entiteti;

public partial class WebForms_Shows_ShowsDetailsForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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

    private void InitializeShowData(int redniBroj)
    {
        ISession session = DataAccessLayer.DataLayer.GetSession();
        MODNA_REVIJA show = session.Get<MODNA_REVIJA>(redniBroj);

        // Title
        labelTitleVal.Text = show.NAZIV;
        labelPageTitle.Text = show.NAZIV;
        Head1.Title = show.NAZIV;

        // Date & Time
        labelDateTimeVal.Text = show.DATUM_VREME.ToString("dd/MM/yyyy hh:mm");

        // Location
        labelCityVal.Text = show.MESTO;

        // Fashion Designers
        IList<MODNI_KREATOR> designers = show.modniKreatori;
        if (designers.Count > 0)
        {
            foreach (MODNI_KREATOR designer in designers)
            {
                ListItem item = new ListItem(designer.IME_PREZIME);
                item.Value = designer.MATICNI_BROJ.ToString();
                boxDesigners.Items.Add(item);
            }
        }

        // Fashion Models
        IList<MANEKEN> models = show.manekeni;
        if (models.Count > 0)
        {
            foreach (MANEKEN maneken in models)
            {
                ListItem item = new ListItem(maneken.IME_PREZIME);
                item.Value = maneken.MATICNI_BROJ.ToString();
                boxModels.Items.Add(item);
            }
        }

        session.Close();
    }

    protected void buttonBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShowsForm.aspx");
    }
}