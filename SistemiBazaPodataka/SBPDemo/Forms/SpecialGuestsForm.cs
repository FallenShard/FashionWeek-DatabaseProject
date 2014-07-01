using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NHibernate;
using DataAccessLayer.Entiteti;

namespace SBPDemo
{
    public partial class SpecialGuestsForm : Form
    {
        public SpecialGuestsForm()
        {
            InitializeComponent();

            labelTitle1.Left = (ClientSize.Width - labelTitle1.PreferredWidth) / 2;
            labelTitle2.Left = (ClientSize.Width - labelTitle2.PreferredWidth) / 2;
            buttonClose.Left = (ClientSize.Width - buttonClose.Width) / 2;
            
        }

        private void SpecialGuestsForm_Load(object sender, EventArgs e)
        {
            ISession session = DataAccessLayer.DataLayer.GetSession();
            IQuery query = session.CreateQuery("FROM SPECIJALNI_GOST");
            IList<SPECIJALNI_GOST> list = query.List<SPECIJALNI_GOST>();

            int oldFormHeight = ClientSize.Height;
            int nextLabelY = 150;

            foreach (SPECIJALNI_GOST sg in list)
            {
                Label currLabel = new Label();
                currLabel.Font = new Font("Stars", 16);
                currLabel.Text = sg.IME_PREZIME;
                currLabel.ForeColor = Color.Blue;
                currLabel.AutoSize = true;

                currLabel.Left = (this.ClientSize.Width - currLabel.PreferredWidth) / 2;
                currLabel.Top = nextLabelY;
                nextLabelY += currLabel.Height + 30;

                Controls.Add(currLabel);
            }
            Height = nextLabelY + 120;
            buttonClose.Top = (ClientSize.Height - buttonClose.Height) - 20;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
