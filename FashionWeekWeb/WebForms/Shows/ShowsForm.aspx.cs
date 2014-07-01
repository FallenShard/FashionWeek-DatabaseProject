using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NHibernate;
using DataAccessLayer;
using DataAccessLayer.Entiteti;

public partial class WebForms_ShowsForm : System.Web.UI.Page
{
    List<MODNA_REVIJA> m_shows;

    protected void Page_Load(object sender, EventArgs e)
    {
        getShows();
        initTable();

        if (!IsPostBack)
        {
            initListBox();
        }
    }

    private void getShows()
    {
        ISession session = DataAccessLayer.DataLayer.GetSession();

        m_shows = new List<MODNA_REVIJA>(session.CreateQuery("FROM MODNA_REVIJA").List<MODNA_REVIJA>());
        m_shows.Sort((x, y) => DateTime.Compare(x.DATUM_VREME, y.DATUM_VREME));

        session.Close();
    }
    private void initTable()
    {
        tableOverview.Rows.Clear();
        TableHeaderRow thRow = new TableHeaderRow();
        tableOverview.Rows.Add(thRow);
        for (int i = 0; i < 5; i++)
            thRow.Cells.Add(new TableHeaderCell());
        thRow.Cells[0].Text = "No.";
        thRow.Cells[1].Text = "Title";
        thRow.Cells[2].Text = "Date";
        thRow.Cells[3].Text = "Time";
        thRow.Cells[4].Text = "Location";

        int k = 0;  // For CSS
        foreach (MODNA_REVIJA show in m_shows)
        {
            TableRow tRow = addRowToTable(tableOverview, 5);
            tRow.Cells[0].Text = (k + 1).ToString();
            tRow.Cells[1].Text = show.NAZIV;
            tRow.Cells[2].Text = show.DATUM_VREME.ToString("dd/MM/yyyy");
            tRow.Cells[3].Text = show.DATUM_VREME.ToShortTimeString();
            tRow.Cells[4].Text = show.MESTO;
            tRow.CssClass = k++ % 2 == 0 ? "RowOne" : "RowTwo";
        }
    }

    private void initListBox()
    {
        listBoxItems.Items.Clear();

        foreach (MODNA_REVIJA show in m_shows)
        {
            ListItem showItem = new ListItem(show.NAZIV);
            showItem.Value = show.REDNI_BROJ.ToString();
            listBoxItems.Items.Add(showItem);
        }

        listBoxItems.DataValueField = "REDNI_BROJ";
    }

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
        Response.Redirect("ShowsAddForm.aspx");
    }

    protected void buttonEdit_Click(object sender, EventArgs e)
    {
        int redniBroj = -1;

        try
        {
            redniBroj = Int32.Parse(listBoxItems.SelectedItem.Value);
        }
        catch (Exception)
        {
            Response.Write("Please select a show first.");
            return;
        }
        if (redniBroj == -1)
        {
            Response.Write("Please select a show first.");
            return;
        }

        Response.Redirect("ShowsEditForm.aspx?rbr=" + redniBroj);
    }

    protected void buttonDelete_Click(object sender, EventArgs e)
    {
        int redniBroj = -1;

        try
        {
            redniBroj = Int32.Parse(listBoxItems.SelectedItem.Value);
        }
        catch (Exception)
        {
            Response.Write("Please select a show first.");
            return;
        }

        ISession session = DataAccessLayer.DataLayer.GetSession();

        MODNA_REVIJA show = session.Get<MODNA_REVIJA>(redniBroj);

        if (redniBroj != -1 && show != null)
        {
            // Workaround
            foreach (MODNI_KREATOR mk in show.modniKreatori)
            {
                mk.modneRevije.Remove(show);
                session.SaveOrUpdate(mk);
            }

            foreach (MANEKEN maneken in show.manekeni)
            {
                maneken.modneRevije.Remove(show);
                session.SaveOrUpdate(maneken);
            }

            session.Flush();

            show.modniKreatori.Clear();
            show.manekeni.Clear();

            // Delete the selected show
            session.Delete(show);
            session.Flush();
        }

        session.Close();
        Response.Redirect("ShowsForm.aspx");
    }

    protected void buttonBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../MainForm.aspx");
    }
    protected void buttonDetails_Click(object sender, EventArgs e)
    {
        int redniBroj = -1;

        try
        {
            redniBroj = Int32.Parse(listBoxItems.SelectedItem.Value);
        }
        catch (Exception)
        {
            Response.Write("Please select a show first.");
            return;
        }
        if (redniBroj == -1)
        {
            Response.Write("Please select a show first.");
            return;
        }

        Response.Redirect("ShowsDetailsForm.aspx?rbr=" + redniBroj);
    }
}