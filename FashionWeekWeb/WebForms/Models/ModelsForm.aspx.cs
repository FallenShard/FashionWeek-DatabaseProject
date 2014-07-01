using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NHibernate;
using DataAccessLayer;
using DataAccessLayer.Entiteti;

public partial class WebForms_Models_ModelsForm : System.Web.UI.Page
{
    IList<MANEKEN> m_models;

    protected void Page_Load(object sender, EventArgs e)
    {
        initTable();

        if (!IsPostBack)
        {
            initListBox();
        }
    }

    private void initTable()
    {
        ISession session = DataAccessLayer.DataLayer.GetSession();

        m_models = session.CreateQuery("FROM MANEKEN").List<MANEKEN>();

        tableOverview.Rows.Clear();
        TableHeaderRow thRow = new TableHeaderRow();
        tableOverview.Rows.Add(thRow);
        for (int i = 0; i < 11; i++)
            thRow.Cells.Add(new TableHeaderCell());
        thRow.Cells[0].Text = "Personal Number";
        thRow.Cells[1].Text = "First Name";
        thRow.Cells[2].Text = "Last Name";
        thRow.Cells[3].Text = "Birth Date";
        thRow.Cells[4].Text = "Gender";
        thRow.Cells[5].Text = "Eye Color";
        thRow.Cells[6].Text = "Hair Color";
        thRow.Cells[7].Text = "Weight";
        thRow.Cells[8].Text = "Height";
        thRow.Cells[9].Text = "Clothing Size";
        thRow.Cells[10].Text = "Modeling Agency";

        int k = 0; //For CSS
        foreach (MANEKEN man in m_models)
        {
            TableRow tRow = addRowToTable(tableOverview, 11);
            tRow.Cells[0].Text = man.MATICNI_BROJ.ToString();
            tRow.Cells[1].Text = man.IME;
            tRow.Cells[2].Text = man.PREZIME;
            tRow.Cells[3].Text = man.DATUM_RODJENJA.ToString("dd/MM/yyyy");
            tRow.Cells[4].Text = man.POL.ToString();
            tRow.Cells[5].Text = man.BOJA_OCIJU;
            tRow.Cells[6].Text = man.BOJA_KOSE;
            tRow.Cells[7].Text = man.TEZINA.ToString();
            tRow.Cells[8].Text = man.VISINA.ToString();
            tRow.Cells[9].Text = man.KONFEKCIJSKI_BROJ.ToString();
            tRow.Cells[10].Text = man.modnaAgencija != null ? man.modnaAgencija.NAZIV : "";
            tRow.CssClass = k++ % 2 == 0 ? "RowOne" : "RowTwo";
        }

        session.Close();
    }

    private void initListBox()
    {
        listBoxItems.Items.Clear();

        foreach (MANEKEN model in m_models)
        {
            ListItem showItem = new ListItem(model.IME_PREZIME);
            showItem.Value = model.MATICNI_BROJ.ToString();
            listBoxItems.Items.Add(showItem);
        }

        listBoxItems.DataValueField = "MATICNI_BROJ";
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
        Response.Redirect("ModelsAddForm.aspx");
    }

    protected void buttonEdit_Click(object sender, EventArgs e)
    {
        int maticniBroj = -1;

        try
        {
            maticniBroj = Int32.Parse(listBoxItems.SelectedItem.Value);
        }
        catch (Exception)
        {
            Response.Write("Please select a model first.");
        }
        if (maticniBroj == -1)
            Response.Write("Please select a model first.");

        Response.Redirect("ModelsEditForm.aspx?matbr=" + maticniBroj);

    }

    protected void buttonDelete_Click(object sender, EventArgs e)
    {
        int maticniBroj = -1;

        try
        {
            maticniBroj = Int32.Parse(listBoxItems.SelectedItem.Value);
        }
        catch (Exception)
        {
            Response.Write("Please select a model first.");
        }

        ISession session = DataAccessLayer.DataLayer.GetSession();

        MANEKEN model = session.Get<MANEKEN>(maticniBroj);

        if (maticniBroj != -1 && model != null)
        {
            // Workaround
            model.modneRevije.Clear();
            model.casopisi.Clear();
            model.modnaAgencija = null;
            session.SaveOrUpdate(model);
            session.Flush();

            // Delete the selected designer
            session.Delete(model);
            session.Flush();
        }

        session.Close();
        Response.Redirect("ModelsForm.aspx");
    }

    protected void buttonDetails_Click(object sender, EventArgs e)
    {
        int maticniBroj = -1;

        try
        {
            maticniBroj = Int32.Parse(listBoxItems.SelectedItem.Value);
        }
        catch (Exception)
        {
            Response.Write("Please select a model first.");
        }
        if (maticniBroj == -1)
            Response.Write("Please select a model first.");

        Response.Redirect("ModelsDetailsForm.aspx?matbr=" + maticniBroj);
    }

    protected void buttonBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../MainForm.aspx");
    }
}