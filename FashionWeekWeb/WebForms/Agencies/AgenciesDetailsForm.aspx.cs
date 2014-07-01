using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NHibernate;
using DataAccessLayer.Entiteti;

public partial class WebForms_Agencies_AgenciesDetailsForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int pib = -1;
            try
            {
                pib = Int32.Parse(Request.QueryString["pib"]);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                return;
            }

            InitializeAgencyData(pib);
        }

    }

    private void InitializeAgencyData(int pib)
    {
        ISession session = DataAccessLayer.DataLayer.GetSession();
        MODNA_AGENCIJA agency = session.Get<MODNA_AGENCIJA>(pib);

        // PIN
        labelRegNumVal.Text = agency.PIB.ToString();

        // Title
        labelNameVal.Text = agency.NAZIV;
        labelPageTitle.Text = agency.NAZIV;
        Head1.Title = agency.NAZIV;

        // Date & Time
        labelDateVal.Text = agency.DATUM_OSNIVANJA.ToString("dd/MM/yyyy");

        // Location
        labelHQVal.Text = agency.SEDISTE;

        // Fashion Models
        foreach (MANEKEN maneken in agency.manekeni)
            boxModels.Items.Add(maneken.IME_PREZIME);

        // Scope
        if (agency is INTERNACIONALNA_AGENCIJA)
        {
            IList<DRZAVA> countries = (agency as INTERNACIONALNA_AGENCIJA).drzave;
            foreach (DRZAVA country in countries)
                boxCountries.Items.Add(country.NAZIV_DRZAVE);
            labelIntVal.Text = "International";
        }
        else
        {
            labelIntVal.Text = "Local";
            boxCountries.Visible = false;
            labelCountries.Visible = false;
        }
    }

    protected void buttonBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("AgenciesForm.aspx");
    }
}