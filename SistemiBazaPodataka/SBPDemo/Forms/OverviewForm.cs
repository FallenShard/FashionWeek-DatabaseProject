using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NHibernate;
using DataAccessLayer;
using DataAccessLayer.Entiteti;

namespace SBPDemo
{
    public partial class OverviewForm : Form
    {
        ISession m_session = null;

        public OverviewForm()
        {
            InitializeComponent();
        }

        private void OverviewForm_Load(object sender, EventArgs e)
        {
            m_session = DataLayer.GetSession();

            listBoxTables.Items.Clear();
            listBoxTables.Items.Add("Shows");
            listBoxTables.Items.Add("Designers");
            listBoxTables.Items.Add("Models");
            listBoxTables.Items.Add("Special Guests");
            listBoxTables.Items.Add("Magazines");
            listBoxTables.Items.Add("Agencies (All)");
            listBoxTables.Items.Add("Agencies (Local)");
            listBoxTables.Items.Add("Agencies (International)");
            listBoxTables.Items.Add("Countries");
        }

        private void listBoxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = listBoxTables.SelectedItem as string;
            if (selected == null)
            {
                MessageBox.Show("Please select one of available tables first.", "Selection error");
                return;
            }

            label2.Text = selected;
            listViewOverview.Clear();

            if (selected == "Shows")
            {
                listViewOverview.Columns.Add("NO");
                listViewOverview.Columns.Add("TITLE");
                listViewOverview.Columns.Add("DATE");
                listViewOverview.Columns.Add("TIME");
                listViewOverview.Columns.Add("LOCATION");

                List<MODNA_REVIJA> lista = new List<MODNA_REVIJA>(m_session.CreateQuery("FROM MODNA_REVIJA").List<MODNA_REVIJA>());
                lista.Sort((x, y) => DateTime.Compare(x.DATUM_VREME, y.DATUM_VREME));

                // Iterate and add data from the shows
                int redBr = 1;
                foreach (MODNA_REVIJA mr in lista)
                {
                    ListViewItem lvi = new ListViewItem(redBr++.ToString());
                    lvi.SubItems.Add(mr.NAZIV);
                    lvi.SubItems.Add(mr.DATUM_VREME.ToShortDateString());
                    lvi.SubItems.Add(mr.DATUM_VREME.ToShortTimeString());
                    lvi.SubItems.Add(mr.MESTO);
                    lvi.Tag = mr;
                    listViewOverview.Items.Add(lvi);
                }
            }
            else if (selected == "Designers")
            {
                listViewOverview.Columns.Add("PERSONAL NUMBER");
                listViewOverview.Columns.Add("FIRST NAME");
                listViewOverview.Columns.Add("LAST NAME");
                listViewOverview.Columns.Add("BIRTH DATE");
                listViewOverview.Columns.Add("FASHION HOUSE");
                listViewOverview.Columns.Add("GENDER");
                listViewOverview.Columns.Add("COUNTRY");

                IList<MODNI_KREATOR> lista = m_session.CreateQuery("FROM MODNI_KREATOR").List<MODNI_KREATOR>();

                foreach (MODNI_KREATOR mk in lista)
                {
                    ListViewItem lvi = new ListViewItem(mk.MATICNI_BROJ.ToString());
                    lvi.SubItems.Add(mk.IME);
                    lvi.SubItems.Add(mk.PREZIME);
                    lvi.SubItems.Add(mk.DATUM_RODJENJA.ToString("MM/dd/yyyy"));
                    lvi.SubItems.Add(mk.MODNA_KUCA);
                    lvi.SubItems.Add(mk.POL.ToString());
                    lvi.SubItems.Add(mk.ZEMLJA_POREKLA);
                    lvi.Tag = mk;

                    listViewOverview.Items.Add(lvi);
                }
            }
            else if (selected == "Models")
            {
                listViewOverview.Columns.Add("PERSONAL NUMBER");
                listViewOverview.Columns.Add("FIRST NAME");
                listViewOverview.Columns.Add("LAST NAME");
                listViewOverview.Columns.Add("BIRTH DATE");
                listViewOverview.Columns.Add("GENDER");
                listViewOverview.Columns.Add("EYE COLOR");
                listViewOverview.Columns.Add("HAIR COLOR");
                listViewOverview.Columns.Add("WEIGHT");
                listViewOverview.Columns.Add("HEIGHT");
                listViewOverview.Columns.Add("CLOTHING SIZE");
                listViewOverview.Columns.Add("MODELING AGENCY");

                IList<MANEKEN> lista = m_session.CreateQuery("FROM MANEKEN").List<MANEKEN>();

                foreach (MANEKEN man in lista)
                {
                    ListViewItem lvi = new ListViewItem(man.MATICNI_BROJ.ToString());
                    lvi.SubItems.Add(man.IME);
                    lvi.SubItems.Add(man.PREZIME);
                    lvi.SubItems.Add(man.DATUM_RODJENJA.ToShortDateString());
                    lvi.SubItems.Add(man.POL.ToString());
                    lvi.SubItems.Add(man.BOJA_OCIJU);
                    lvi.SubItems.Add(man.BOJA_KOSE);
                    lvi.SubItems.Add(man.TEZINA.ToString());
                    lvi.SubItems.Add(man.VISINA.ToString());
                    lvi.SubItems.Add(man.KONFEKCIJSKI_BROJ.ToString());
                    lvi.SubItems.Add(man.modnaAgencija != null ? man.modnaAgencija.NAZIV : "");
                    lvi.Tag = man;
                    listViewOverview.Items.Add(lvi);
                }
            }
            else if (selected == "Special Guests")
            {
                listViewOverview.Columns.Add("PERSONAL NUMBER");
                listViewOverview.Columns.Add("FIRST NAME");
                listViewOverview.Columns.Add("LAST NAME");
                listViewOverview.Columns.Add("BIRTH DATE");
                listViewOverview.Columns.Add("GENDER");
                listViewOverview.Columns.Add("EYE COLOR");
                listViewOverview.Columns.Add("HAIR COLOR");
                listViewOverview.Columns.Add("WEIGHT");
                listViewOverview.Columns.Add("HEIGHT");
                listViewOverview.Columns.Add("CLOTHING SIZE");
                listViewOverview.Columns.Add("MODELING AGENCY");
                listViewOverview.Columns.Add("OCCUPATION");

                IList<SPECIJALNI_GOST> lista = m_session.CreateQuery("FROM SPECIJALNI_GOST").List<SPECIJALNI_GOST>();

                foreach (SPECIJALNI_GOST sg in lista)
                {
                    ListViewItem lvi = new ListViewItem(sg.MATICNI_BROJ.ToString());
                    lvi.SubItems.Add(sg.IME);
                    lvi.SubItems.Add(sg.PREZIME);
                    lvi.SubItems.Add(sg.DATUM_RODJENJA.ToShortDateString());
                    lvi.SubItems.Add(sg.POL.ToString());
                    lvi.SubItems.Add(sg.BOJA_OCIJU);
                    lvi.SubItems.Add(sg.BOJA_KOSE);
                    lvi.SubItems.Add(sg.TEZINA.ToString());
                    lvi.SubItems.Add(sg.VISINA.ToString());
                    lvi.SubItems.Add(sg.KONFEKCIJSKI_BROJ.ToString());
                    lvi.SubItems.Add(sg.modnaAgencija != null ? sg.modnaAgencija.NAZIV : "");
                    lvi.SubItems.Add(sg.ZANIMANJE);
                    lvi.Tag = sg;
                    listViewOverview.Items.Add(lvi);
                }
            }
            else if (selected == "Magazines")
            {
                listViewOverview.Columns.Add("MAGAZINE TITLE");
                listViewOverview.Columns.Add("COVER MODEL");

                IList<CASOPIS> lista = m_session.CreateQuery("FROM CASOPIS").List<CASOPIS>();

                foreach (CASOPIS cas in lista)
                {
                    ListViewItem lvi = new ListViewItem(cas.NASLOV_CASOPISA);
                    lvi.SubItems.Add(cas.maneken.IME_PREZIME);
                    lvi.Tag = cas.maneken;
                    listViewOverview.Items.Add(lvi);
                }
            }
            else if (selected == "Agencies (All)")
            {
                listViewOverview.Columns.Add("REG NUMBER");
                listViewOverview.Columns.Add("NAME");
                listViewOverview.Columns.Add("FOUNDED");
                listViewOverview.Columns.Add("HEADQUARTERS");
                listViewOverview.Columns.Add("SCOPE");

                IList<MODNA_AGENCIJA> lista = m_session.CreateQuery("FROM MODNA_AGENCIJA").List<MODNA_AGENCIJA>();

                foreach (MODNA_AGENCIJA mag in lista)
                {
                    ListViewItem lvi = new ListViewItem(mag.PIB.ToString());
                    lvi.SubItems.Add(mag.NAZIV);
                    lvi.SubItems.Add(mag.DATUM_OSNIVANJA.ToShortDateString());
                    lvi.SubItems.Add(mag.SEDISTE);
                    if (mag is DOMACA_AGENCIJA)
                        lvi.SubItems.Add("Local");
                    else if (mag is INTERNACIONALNA_AGENCIJA)
                        lvi.SubItems.Add("International");
                    lvi.Tag = mag;

                    listViewOverview.Items.Add(lvi);
                }
            }
            else if (selected == "Agencies (Local)")
            {
                listViewOverview.Columns.Add("REG NUMBER");
                listViewOverview.Columns.Add("NAME");
                listViewOverview.Columns.Add("FOUNDED");
                listViewOverview.Columns.Add("HEADQUARTERS");

                IList<DOMACA_AGENCIJA> lista = m_session.CreateQuery("FROM DOMACA_AGENCIJA").List<DOMACA_AGENCIJA>();

                foreach (DOMACA_AGENCIJA mag in lista)
                {
                    ListViewItem lvi = new ListViewItem(mag.PIB.ToString());
                    lvi.SubItems.Add(mag.NAZIV);
                    lvi.SubItems.Add(mag.DATUM_OSNIVANJA.ToShortDateString());
                    lvi.SubItems.Add(mag.SEDISTE);
                    lvi.Tag = mag;

                    listViewOverview.Items.Add(lvi);
                }
            }
            else if (selected == "Agencies (International)")
            {
                listViewOverview.Columns.Add("REG NUMBER");
                listViewOverview.Columns.Add("NAME");
                listViewOverview.Columns.Add("FOUNDED");
                listViewOverview.Columns.Add("HEADQUARTERS");

                IList<INTERNACIONALNA_AGENCIJA> lista = m_session.CreateQuery("FROM INTERNACIONALNA_AGENCIJA").List<INTERNACIONALNA_AGENCIJA>();

                foreach (INTERNACIONALNA_AGENCIJA mag in lista)
                {
                    ListViewItem lvi = new ListViewItem(mag.PIB.ToString());
                    lvi.SubItems.Add(mag.NAZIV);
                    lvi.SubItems.Add(mag.DATUM_OSNIVANJA.ToShortDateString());
                    lvi.SubItems.Add(mag.SEDISTE);
                    lvi.Tag = mag;

                    listViewOverview.Items.Add(lvi);
                }
            }
            else if (selected == "Countries")
            {
                listViewOverview.Columns.Add("COUNTRY NAME");
                listViewOverview.Columns.Add("INTERNATIONAL AGENCY");

                IList<DRZAVA> lista = m_session.CreateQuery("FROM DRZAVA").List<DRZAVA>();

                foreach (DRZAVA drz in lista)
                {
                    ListViewItem lvi = new ListViewItem(drz.NAZIV_DRZAVE);
                    lvi.SubItems.Add(drz.int_agencija.NAZIV);
                    lvi.Tag = drz.int_agencija;
                    listViewOverview.Items.Add(lvi);
                }
            }

            // Adjust initial column widths
            ListView.ColumnHeaderCollection lch = listViewOverview.Columns;
            for (int i = 0; i < lch.Count; i++)
            {
                lch[i].Width = -1;
                int dataSize = lch[i].Width;
                lch[i].Width = -2;
                int colSize = lch[i].Width;
                lch[i].Width = dataSize > colSize ? -1 : -2;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listViewOverview_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string selected = listBoxTables.SelectedItem as string;
            if (selected == null)
                return;
            var selectedItem = listViewOverview.SelectedItems[0].Tag;

            if (selected == "Shows")
            {
                ShowsDetailsForm sdf = new ShowsDetailsForm((selectedItem as MODNA_REVIJA).REDNI_BROJ);
                sdf.Show();
            }
            else if (selected == "Designers")
            {
                DesignersDetailsForm ddf = new DesignersDetailsForm((selectedItem as MODNI_KREATOR).MATICNI_BROJ);
                ddf.Show();
            }
            else if (selected == "Models")
            {
                ModelsDetailsForm mdf = new ModelsDetailsForm((selectedItem as MANEKEN).MATICNI_BROJ);
                mdf.Show();
            }
            else if (selected == "Special Guests")
            {
                ModelsDetailsForm mdf = new ModelsDetailsForm((selectedItem as SPECIJALNI_GOST).MATICNI_BROJ);
                mdf.Show();
            }
            else if (selected == "Magazines")
            {
                ModelsDetailsForm mdf = new ModelsDetailsForm((selectedItem as MANEKEN).MATICNI_BROJ);
                mdf.Show();
            }
            else if (selected == "Agencies (All)")
            {
                AgenciesDetailsForm adf = new AgenciesDetailsForm((selectedItem as MODNA_AGENCIJA).PIB);
                adf.Show();
            }
            else if (selected == "Agencies (Local)")
            {
                AgenciesDetailsForm adf = new AgenciesDetailsForm((selectedItem as DOMACA_AGENCIJA).PIB);
                adf.Show();
            }
            else if (selected == "Agencies (International)")
            {
                AgenciesDetailsForm adf = new AgenciesDetailsForm((selectedItem as INTERNACIONALNA_AGENCIJA).PIB);
                adf.Show();
            }
            else if (selected == "Countries")
            {
                AgenciesDetailsForm adf = new AgenciesDetailsForm((selectedItem as INTERNACIONALNA_AGENCIJA).PIB);
                adf.Show();
            }
        }

        private void OverviewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_session.Close();
        }
    }
}
