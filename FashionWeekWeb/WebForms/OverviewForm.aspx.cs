using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NHibernate;
using DataAccessLayer;
using DataAccessLayer.Entiteti;

public partial class OverviewForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ISession session = DataLayer.GetSession();
            session.Close();

            //tableOverview.GridLines = GridLines.Both;
            populateListBox();
        }
    }

    private void populateListBox()
    {
        
        listBoxTables.Items.Clear();
        listBoxTables.Items.Add(new ListItem("Shows"));
        listBoxTables.Items.Add(new ListItem("Designers"));
        listBoxTables.Items.Add(new ListItem("Models"));
        listBoxTables.Items.Add(new ListItem("Special Guests"));
        listBoxTables.Items.Add(new ListItem("Magazines"));
        listBoxTables.Items.Add(new ListItem("Agencies (All)"));
        listBoxTables.Items.Add(new ListItem("Agencies (Local)"));
        listBoxTables.Items.Add(new ListItem("Agencies (International)"));
        listBoxTables.Items.Add(new ListItem("Countries"));
    }

    protected void listBoxTables_SelectedIndexChanged(object sender, EventArgs e)
    {

        string selected = listBoxTables.SelectedItem.Text;
        if (selected == null)
        {
            Response.Write("Please select one of available tables first.");
            return;
        }

        ISession session = DataLayer.GetSession();

        labelTable.Text = selected;
        labelDesc.Text = "";
        tableOverview.Rows.Clear();
        TableHeaderRow thRow = new TableHeaderRow();
        tableOverview.Rows.Add(thRow);

        int k = 0;  // For CSS
        if (selected == "Shows")
        {
            for (int i = 0; i < 5; i++)
                thRow.Cells.Add(new TableHeaderCell());
            thRow.Cells[0].Text = "No.";
            thRow.Cells[1].Text = "Title";
            thRow.Cells[2].Text = "Date";
            thRow.Cells[3].Text = "Time";
            thRow.Cells[4].Text = "Location";

            IQuery q = session.CreateQuery("FROM MODNA_REVIJA");
            IList<MODNA_REVIJA> lista = q.List<MODNA_REVIJA>();
            
            
            foreach (MODNA_REVIJA mr in lista)
            {
                TableRow tRow = addRowToTable(tableOverview, 5);
                tRow.Cells[0].Text = (k + 1).ToString();
                tRow.Cells[1].Text = mr.NAZIV;
                tRow.Cells[2].Text = mr.DATUM_VREME.ToString("dd/MM/yyyy");
                tRow.Cells[3].Text = mr.DATUM_VREME.ToShortTimeString();
                tRow.Cells[4].Text = mr.MESTO;
                tRow.CssClass = k++ % 2 == 0 ? "RowOne" : "RowTwo";
            }
        }
        else if (selected == "Designers")
        {
            for (int i = 0; i < 7; i++)
                thRow.Cells.Add(new TableHeaderCell());
            thRow.Cells[0].Text = "Personal Number";
            thRow.Cells[1].Text = "First Name";
            thRow.Cells[2].Text = "Last Name";
            thRow.Cells[3].Text = "Birth Date";
            thRow.Cells[4].Text = "Fashion House";
            thRow.Cells[5].Text = "Gender";
            thRow.Cells[6].Text = "Country";

            IList<MODNI_KREATOR> lista = session.CreateQuery("FROM MODNI_KREATOR").List<MODNI_KREATOR>();
            
            foreach (MODNI_KREATOR mk in lista)
            {
                TableRow tRow = addRowToTable(tableOverview, 7);
                tRow.Cells[0].Text = mk.MATICNI_BROJ.ToString();
                tRow.Cells[1].Text = mk.IME;
                tRow.Cells[2].Text = mk.PREZIME;
                tRow.Cells[3].Text = mk.DATUM_RODJENJA.ToString("dd/MM/yyyy");
                tRow.Cells[4].Text = mk.MODNA_KUCA;
                tRow.Cells[5].Text = mk.POL.ToString();
                tRow.Cells[6].Text = mk.ZEMLJA_POREKLA;
                tRow.CssClass = k++ % 2 == 0 ? "RowOne" : "RowTwo";
            }
        }
        else if (selected == "Models")
        {
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

            IList<MANEKEN> lista = session.CreateQuery("FROM MANEKEN").List<MANEKEN>();

            foreach (MANEKEN man in lista)
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
        }
        else if (selected == "Special Guests")
        {
            for (int i = 0; i < 12; i++)
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
            thRow.Cells[11].Text = "Occupation";

            IList<SPECIJALNI_GOST> lista = session.CreateQuery("FROM SPECIJALNI_GOST").List<SPECIJALNI_GOST>();

            foreach (SPECIJALNI_GOST sg in lista)
            {
                TableRow tRow = addRowToTable(tableOverview, 12);
                tRow.Cells[0].Text = sg.MATICNI_BROJ.ToString();
                tRow.Cells[1].Text = sg.IME;
                tRow.Cells[2].Text = sg.PREZIME;
                tRow.Cells[3].Text = sg.DATUM_RODJENJA.ToString("dd/MM/yyyy");
                tRow.Cells[4].Text = sg.POL.ToString();
                tRow.Cells[5].Text = sg.BOJA_OCIJU;
                tRow.Cells[6].Text = sg.BOJA_KOSE;
                tRow.Cells[7].Text = sg.TEZINA.ToString();
                tRow.Cells[8].Text = sg.VISINA.ToString();
                tRow.Cells[9].Text = sg.KONFEKCIJSKI_BROJ.ToString();
                tRow.Cells[10].Text = sg.modnaAgencija != null ? sg.modnaAgencija.NAZIV : "";
                tRow.Cells[11].Text = sg.ZANIMANJE;
                tRow.CssClass = k++ % 2 == 0 ? "RowOne" : "RowTwo";
            }
        }
        else if (selected == "Magazines")
        {
            for (int i = 0; i < 2; i++)
                thRow.Cells.Add(new TableHeaderCell());
            thRow.Cells[0].Text = "Magazine Title";
            thRow.Cells[1].Text = "Cover Model";

            IList<CASOPIS> lista = session.CreateQuery("FROM CASOPIS").List<CASOPIS>();

            foreach (CASOPIS cas in lista)
            {
                TableRow tRow = addRowToTable(tableOverview, 2);
                tRow.Cells[0].Text = cas.NASLOV_CASOPISA;
                tRow.Cells[1].Text = cas.maneken.IME_PREZIME;
                tRow.CssClass = k++ % 2 == 0 ? "RowOne" : "RowTwo";
            }
        }
        else if (selected == "Agencies (All)")
        {
            for (int i = 0; i < 5; i++)
                thRow.Cells.Add(new TableHeaderCell());
            thRow.Cells[0].Text = "Registration Number";
            thRow.Cells[1].Text = "Name";
            thRow.Cells[2].Text = "Founded";
            thRow.Cells[3].Text = "Headquarters";
            thRow.Cells[4].Text = "Scope";

            IList<MODNA_AGENCIJA> lista = session.CreateQuery("FROM MODNA_AGENCIJA").List<MODNA_AGENCIJA>();

            foreach (MODNA_AGENCIJA mag in lista)
            {
                TableRow tRow = addRowToTable(tableOverview, 5);
                tRow.Cells[0].Text = mag.PIB.ToString();
                tRow.Cells[1].Text = mag.NAZIV;
                tRow.Cells[2].Text = mag.DATUM_OSNIVANJA.ToString("dd/MM/yyyy");
                tRow.Cells[3].Text = mag.SEDISTE;
                if (mag is DOMACA_AGENCIJA)
                    tRow.Cells[4].Text = "Local";
                else if (mag is INTERNACIONALNA_AGENCIJA)
                    tRow.Cells[4].Text = "International";
                tRow.CssClass = k++ % 2 == 0 ? "RowOne" : "RowTwo";
            }
        }
        else if (selected == "Agencies (Local)")
        {
            for (int i = 0; i < 4; i++)
                thRow.Cells.Add(new TableHeaderCell());
            thRow.Cells[0].Text = "Registration Number";
            thRow.Cells[1].Text = "Name";
            thRow.Cells[2].Text = "Founded";
            thRow.Cells[3].Text = "Headquarters";

            IList<DOMACA_AGENCIJA> lista = session.CreateQuery("FROM DOMACA_AGENCIJA").List<DOMACA_AGENCIJA>();

            foreach (DOMACA_AGENCIJA mag in lista)
            {
                TableRow tRow = addRowToTable(tableOverview, 4);
                tRow.Cells[0].Text = mag.PIB.ToString();
                tRow.Cells[1].Text = mag.NAZIV;
                tRow.Cells[2].Text = mag.DATUM_OSNIVANJA.ToString("dd/MM/yyyy");
                tRow.Cells[3].Text = mag.SEDISTE;
                tRow.CssClass = k++ % 2 == 0 ? "RowOne" : "RowTwo";
            }
        }
        else if (selected == "Agencies (International)")
        {
            for (int i = 0; i < 4; i++)
                thRow.Cells.Add(new TableHeaderCell());
            thRow.Cells[0].Text = "Registration Number";
            thRow.Cells[1].Text = "Name";
            thRow.Cells[2].Text = "Founded";
            thRow.Cells[3].Text = "Headquarters";

            IList<INTERNACIONALNA_AGENCIJA> lista = session.CreateQuery("FROM INTERNACIONALNA_AGENCIJA")
                .List<INTERNACIONALNA_AGENCIJA>();

            foreach (INTERNACIONALNA_AGENCIJA mag in lista)
            {
                TableRow tRow = addRowToTable(tableOverview, 4);
                tRow.Cells[0].Text = mag.PIB.ToString();
                tRow.Cells[1].Text = mag.NAZIV;
                tRow.Cells[2].Text = mag.DATUM_OSNIVANJA.ToString("dd/MM/yyyy");
                tRow.Cells[3].Text = mag.SEDISTE;
                tRow.CssClass = k++ % 2 == 0 ? "RowOne" : "RowTwo";
            }
        }
        else if (selected == "Countries")
        {
            for (int i = 0; i < 2; i++)
                thRow.Cells.Add(new TableHeaderCell());
            thRow.Cells[0].Text = "Country Name";
            thRow.Cells[1].Text = "International Agency";

            IList<DRZAVA> lista = session.CreateQuery("FROM DRZAVA").List<DRZAVA>();

            foreach (DRZAVA drz in lista)
            {
                TableRow tRow = addRowToTable(tableOverview, 2);
                tRow.Cells[0].Text = drz.NAZIV_DRZAVE;
                tRow.Cells[1].Text = drz.int_agencija.NAZIV;
                tRow.CssClass = k++ % 2 == 0 ? "RowOne" : "RowTwo";
            }
        }

        session.Close();
    }

    private TableRow addRowToTable(Table table, int cellsNumber)
    {
        TableRow tRow = new TableRow();
        for (int i = 0; i < cellsNumber; i++)
            tRow.Cells.Add(new TableCell());
        table.Rows.Add(tRow);

        return tRow;
    }

    protected void buttonBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("MainForm.aspx");
    }
}