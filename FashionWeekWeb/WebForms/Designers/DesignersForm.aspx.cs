using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NHibernate;
using DataAccessLayer;
using DataAccessLayer.Entiteti;

public partial class WebForms_Designers_DesignersForm : System.Web.UI.Page
{
    IList<MODNI_KREATOR> m_designers;                       // Designers being displayed

    protected void Page_Load(object sender, EventArgs e)
    {
        getDesigners();
        initTable();

        if (!IsPostBack)
        {
            initListBox();
        }
    }

    private void getDesigners()
    {
        ISession session = DataAccessLayer.DataLayer.GetSession();

        m_designers = session.CreateQuery("FROM MODNI_KREATOR").List<MODNI_KREATOR>();

        session.Close();
    }

    private void initTable()
    {
        tableOverview.Rows.Clear();
        TableHeaderRow thRow = new TableHeaderRow();
        tableOverview.Rows.Add(thRow);
        for (int i = 0; i < 7; i++)
            thRow.Cells.Add(new TableHeaderCell());
        thRow.Cells[0].Text = "Personal Number";
        thRow.Cells[1].Text = "First Name";
        thRow.Cells[2].Text = "Last Name";
        thRow.Cells[3].Text = "Birth Date";
        thRow.Cells[4].Text = "Fashion House";
        thRow.Cells[5].Text = "Gender";
        thRow.Cells[6].Text = "Country";

        int k = 0;  // For CSS
        foreach (MODNI_KREATOR designer in m_designers)
        {
            TableRow tRow = addRowToTable(tableOverview, 7);
            tRow.Cells[0].Text = designer.MATICNI_BROJ.ToString();
            tRow.Cells[1].Text = designer.IME;
            tRow.Cells[2].Text = designer.PREZIME;
            tRow.Cells[3].Text = designer.DATUM_RODJENJA.ToString("dd/MM/yyyy");
            tRow.Cells[4].Text = designer.MODNA_KUCA;
            tRow.Cells[5].Text = designer.POL.ToString();
            tRow.Cells[6].Text = designer.ZEMLJA_POREKLA;
            tRow.CssClass = k++ % 2 == 0 ? "RowOne" : "RowTwo";
        }
    }

    private void initListBox()
    {
        listBoxItems.Items.Clear();

        foreach (MODNI_KREATOR designer in m_designers)
        {
            ListItem showItem = new ListItem(designer.IME_PREZIME);
            showItem.Value = designer.MATICNI_BROJ.ToString();
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
        Response.Redirect("DesignersAddForm.aspx");
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
            Response.Write("Please select a designer first.");
            return;
        }
        if (maticniBroj == -1)
        {
            Response.Write("Please select a designer first.");
            return;
        }

        Response.Redirect("DesignersEditForm.aspx?matbr=" + maticniBroj);
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
            Response.Write("Please select a designer first.");
            return;
        }

        ISession session = DataAccessLayer.DataLayer.GetSession();

        MODNI_KREATOR designer = session.Get<MODNI_KREATOR>(maticniBroj);

        if (maticniBroj != -1 && designer != null)
        {
            // Workaround
            designer.modneRevije.Clear();
            session.SaveOrUpdate(designer);
            session.Flush();

            // Delete the selected designer
            session.Delete(designer);
            session.Flush();
        }

        session.Close();
        Response.Redirect("DesignersForm.aspx");
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
            Response.Write("Please select a designer first.");
            return;
        }
        if (maticniBroj == -1)
        {
            Response.Write("Please select a designer first.");
            return;
        }

        Response.Redirect("DesignersDetailsForm.aspx?matbr=" + maticniBroj);
    }

    protected void buttonBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../MainForm.aspx");
    }

    protected void buttonHistory_Click(object sender, EventArgs e)
    {
        // Algorithm for determining whether two or more designers have common history on events
        IList<string> inputStrings = new List<string>();

        // Get the selected designers first and add them to input keys
        foreach (ListItem item in listBoxItems.Items)
        {
            if (item.Selected)
                inputStrings.Add(item.Value);
        }

        // If only one was selected, emit error
        if (inputStrings.Count < 2)
        {
            labelHistory.Text = "Select at least two designers <br /> to see their history!";
            return;
        }

        try
        {
            // Grab all the shows in order to find history
            ISession session = DataAccessLayer.DataLayer.GetSession();
            IList<MODNA_REVIJA> shows = session.CreateQuery("FROM MODNA_REVIJA").List<MODNA_REVIJA>();

            // Found will be true if at least one show is found
            bool found = false;
            
            // sharedShows keeps track of shows where all of the selected designers appeared together
            IList<string> sharedShows = new List<string>();

            foreach (MODNA_REVIJA show in shows)
            {
                // Get designers for the current show
                IList<MODNI_KREATOR> designers = show.modniKreatori;
                IList<string> designersNumbers = new List<string>();

                // Extract only their personal numbers for comparison
                foreach (MODNI_KREATOR designer in designers)
                {
                    designersNumbers.Add(designer.MATICNI_BROJ.ToString());
                }

                int matchings = 0;

                // How many input keys does the current list of show's designers' keys contain?
                foreach (string inputNumber in inputStrings)
                {
                    if (designersNumbers.Contains(inputNumber)) matchings++;
                }

                // If it contains all of them, there's a match and common history
                if (matchings == inputStrings.Count)
                {
                    found = true;
                    sharedShows.Add(show.NAZIV);
                }
            }

            // Form the output string to display
            string outString = "";

            if (!found) outString = "These designers didn't <br /> organize any shows together.";
            else
            {
                outString = "Designers organized the <br /> following shows together:<br />";
                foreach (string showName in sharedShows)
                    outString += "<br /> - " + showName;
            }

            labelHistory.Text = outString;

            session.Close();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}