using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NHibernate;
using DataAccessLayer.Entiteti;

public partial class WebForms_Models_ModelsDetailsForm : System.Web.UI.Page
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

            InitializeModelData(maticniBroj);
        }
    }

    private void InitializeModelData(int maticniBroj)
    {
        ISession session = DataAccessLayer.DataLayer.GetSession();
        MANEKEN model = session.Get<MANEKEN>(maticniBroj);

        // Personal Number
        labelPNVal.Text = model.MATICNI_BROJ.ToString();
        labelPageTitle.Text = model.IME_PREZIME;
        Head1.Title = model.IME_PREZIME;

        // First Name
        labelFirstNameVal.Text = model.IME;

        // Last Name
        labelLastNameVal.Text = model.PREZIME;

        // Agency
        labelAgencyVal.Text = model.modnaAgencija == null ? "-None-" : model.modnaAgencija.NAZIV;

        // Birth Date
        if (model.DATUM_RODJENJA == DateTime.MinValue)
            labelDateVal.Text = "Unknown";
        else
            labelDateVal.Text = model.DATUM_RODJENJA.ToString("dd/MM/yyyy");

        // Gender
        if (model.POL == 'F') labelGenderVal.Text = "Female";
        if (model.POL == 'M') labelGenderVal.Text = "Male";

        // Eyes
        labelEyesVal.Text = model.BOJA_OCIJU;

        // Hair
        labelHairVal.Text = model.BOJA_KOSE;

        // Height, Weight and Clothing Size
        labelHeightVal.Text = model.VISINA.ToString();
        labelWeightVal.Text = model.TEZINA.ToString();
        labelCSizeVal.Text = model.KONFEKCIJSKI_BROJ.ToString();

        // Special Guest Flag
        if (model is SPECIJALNI_GOST)
        {
            labelOccupation.Visible = true;
            labelOccupationVal.Visible = true;
            labelOccupationVal.Text = (model as SPECIJALNI_GOST).ZANIMANJE;
            labelSGVal.Text = "True";
        }
        else
        {
            labelOccupation.Visible = false;
            labelOccupationVal.Visible = false;
            labelSGVal.Text = "False";
        }

        // Magazines
        foreach (CASOPIS cas in model.casopisi)
            boxMagazines.Items.Add(cas.NASLOV_CASOPISA);

        // Shows
        foreach (MODNA_REVIJA mr in model.modneRevije)
        {
            boxShows.Items.Add(mr.NAZIV);
        }

        session.Close();
    }

    protected void buttonBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ModelsForm.aspx");
    }
}