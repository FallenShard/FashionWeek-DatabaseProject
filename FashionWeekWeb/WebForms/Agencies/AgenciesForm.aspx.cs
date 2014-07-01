using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NHibernate;
using DataAccessLayer;
using DataAccessLayer.Entiteti;

public partial class WebForms_Agencies_AgenciesForm : System.Web.UI.Page
{
    IList<MODNA_AGENCIJA> m_agencies;                       // These are the agencies to display in the table

    protected void Page_Load(object sender, EventArgs e)
    {
        // On every page load, we need to refresh the agencies and the table
        getAgencies();
        initTable();

        if (!IsPostBack)
        {
            // ListBox can be initiated only once
            initListBox();
        }
    }

    private void getAgencies()
    {
        // Open a session to get the agencies
        ISession session = DataAccessLayer.DataLayer.GetSession();

        // Grab the agencies to display
        m_agencies = session.CreateQuery("FROM MODNA_AGENCIJA").List<MODNA_AGENCIJA>();

        // Close the session
        session.Close();
    }

    private void initTable()
    {
        // Add a header row to the table
        tableOverview.Rows.Clear();
        TableHeaderRow thRow = new TableHeaderRow();
        tableOverview.Rows.Add(thRow);
        for (int i = 0; i < 5; i++)
            thRow.Cells.Add(new TableHeaderCell());
        thRow.Cells[0].Text = "Registration Number";
        thRow.Cells[1].Text = "Name";
        thRow.Cells[2].Text = "Founded";
        thRow.Cells[3].Text = "Headquarters";
        thRow.Cells[4].Text = "Scope";

        // Fill the table with agencies' data
        int k = 0;  // For CSS
        foreach (MODNA_AGENCIJA agency in m_agencies)
        {
            TableRow tRow = addRowToTable(tableOverview, 5);
            tRow.Cells[0].Text = agency.PIB.ToString();
            tRow.Cells[1].Text = agency.NAZIV;
            tRow.Cells[2].Text = agency.DATUM_OSNIVANJA.ToString("dd/MM/yyyy");
            tRow.Cells[3].Text = agency.SEDISTE;
            tRow.Cells[4].Text = agency is DOMACA_AGENCIJA ? "Local" : "International";
            tRow.CssClass = k++ % 2 == 0 ? "RowOne" : "RowTwo";
        }
    }

    private void initListBox()
    {
        // Clear the items initially
        listBoxItems.Items.Clear();

        // Fill the box with useful data
        foreach (MODNA_AGENCIJA agency in m_agencies)
        {
            ListItem showItem = new ListItem(agency.NAZIV);
            showItem.Value = agency.PIB.ToString();
            listBoxItems.Items.Add(showItem);
        }

        listBoxItems.DataValueField = "PIB";
    }

    // Helper function for table element generation
    private TableRow addRowToTable(Table table, int cellsNumber)
    {
        TableRow tRow = new TableRow();
        for (int i = 0; i < cellsNumber; i++)
            tRow.Cells.Add(new TableCell());
        table.Rows.Add(tRow);

        return tRow;
    }

    protected void buttonAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AgenciesAddForm.aspx");
    }

    protected void buttonEdit_Click(object sender, EventArgs e)
    {
        int pib = -1;

        try
        {
            pib = Int32.Parse(listBoxItems.SelectedItem.Value);
        }
        catch (Exception)
        {
            Response.Write("Please select an agency first.");
            return;
        }
        if (pib == -1)
        {
            Response.Write("Please select an agency first.");
            return;
        }

        Response.Redirect("AgenciesEditForm.aspx?pib=" + pib);
    }

    protected void buttonDelete_Click(object sender, EventArgs e)
    {
        int pib = -1;

        try
        {
            pib = Int32.Parse(listBoxItems.SelectedItem.Value);
        }
        catch (Exception)
        {
            Response.Write("Please select an agency first.");
            return;
        }

        ISession session = DataAccessLayer.DataLayer.GetSession();

        MODNA_AGENCIJA agency = session.Get<MODNA_AGENCIJA>(pib);

        if (pib != -1 && agency != null)
        {
            // NHibernate is resilient on incorrectly cascade-deleting everything related to the agency
            // Therefore, we need to remove all ties of the agency with other entities
            // Workaround
            foreach (MANEKEN man in agency.manekeni)
                man.modnaAgencija = null;
            agency.manekeni.Clear();
            if (agency is INTERNACIONALNA_AGENCIJA)
                (agency as INTERNACIONALNA_AGENCIJA).drzave.Clear();
            session.SaveOrUpdate(agency);
            session.Flush();

            // Delete the selected agency
            session.Delete(agency);
            session.Flush();
        }

        session.Close();
        Response.Redirect("AgenciesForm.aspx");
    }

    protected void buttonDetails_Click(object sender, EventArgs e)
    {
        int pib = -1;

        try
        {
            pib = Int32.Parse(listBoxItems.SelectedItem.Value);
        }
        catch (Exception)
        {
            Response.Write("Please select an agency first.");
            return;
        }
        if (pib == -1)
        {
            Response.Write("Please select an agency first.");
            return;
        }

        Response.Redirect("AgenciesDetailsForm.aspx?pib=" + pib);
    }

    protected void buttonBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../MainForm.aspx");
    }
}