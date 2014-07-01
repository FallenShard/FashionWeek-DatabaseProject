using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NHibernate;
using DataAccessLayer.Entiteti;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ISession session = DataAccessLayer.DataLayer.GetSession();
                session.Close();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    protected void buttonOverview_Click(object sender, EventArgs e)
    {
        Response.Redirect("OverviewForm.aspx");
    }

    protected void buttonShows_Click(object sender, EventArgs e)
    {
        Response.Redirect("Shows/ShowsForm.aspx");
    }

    protected void buttonDesigners_Click(object sender, EventArgs e)
    {
        Response.Redirect("Designers/DesignersForm.aspx");
    }

    protected void buttonModels_Click(object sender, EventArgs e)
    {
        Response.Redirect("Models/ModelsForm.aspx");
    }

    protected void buttonAgencies_Click(object sender, EventArgs e)
    {
        Response.Redirect("Agencies/AgenciesForm.aspx");
    }
}