using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NHibernate;
using DataAccessLayer.Entiteti;

public partial class WebForms_Designers_DesignersDetailsForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int maticniBroj = -1;

            try
            {
                maticniBroj = Int32.Parse(Request.QueryString["matbr"]);

            }
            catch (Exception exc)
            {
                Response.Write(exc.Message);
            }

            InitializeShowData(maticniBroj);
        }
    }

    private void InitializeShowData(int matBroj)
    {
        ISession session = DataAccessLayer.DataLayer.GetSession();
        MODNI_KREATOR designer = session.Get<MODNI_KREATOR>(matBroj);

        // Name
        labelPNVal.Text = designer.MATICNI_BROJ.ToString();
        labelFirstNameVal.Text = designer.IME;
        labelLastNameVal.Text = designer.PREZIME;
        labelPageTitle.Text = designer.IME_PREZIME;
        Head1.Title = designer.IME_PREZIME;

        // Birth Date
        labelDateVal.Text = designer.DATUM_RODJENJA.ToString("dd/MM/yyyy");

        // Country
        labelCountryVal.Text = designer.ZEMLJA_POREKLA;

        // Gender
        labelGenderVal.Text = designer.POL == 'F' ? "Female" : "Male";

        // Fashion House
        labelFHouseVal.Text = designer.MODNA_KUCA;

        // Fashion Shows
        foreach (MODNA_REVIJA revija in designer.modneRevije)
        {
            ListItem item = new ListItem(revija.NAZIV);
            item.Value = revija.REDNI_BROJ.ToString();
            boxShows.Items.Add(item);
        }

        session.Close();
    }

    protected void buttonBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("DesignersForm.aspx");
    }
}