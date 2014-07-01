using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NHibernate;
using DataAccessLayer;

namespace SBPDemo
{
    public partial class SelectorForm : Form
    {
        public SelectorForm()
        {
            InitializeComponent();
            labelTitle.Left = (this.ClientSize.Width - labelTitle.Width) / 2;
        }

        private void SelectorForm_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            SplashScreen ss = new SplashScreen();
            ss.Show();

            ISession session = DataLayer.GetSession();
            session.Close();

            ss.Close();
            this.Visible = true;
        }

        private void buttonShows_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool foundForm = false;
            foreach (Form frm in fc)
            {
                if (frm is ShowsForm)
                {
                    foundForm = true;
                    frm.BringToFront();
                    break;
                }
            }
            if (!foundForm)
            {
                ShowsForm showsForm = new ShowsForm();
                showsForm.Show();
            }
        }

        private void buttonModels_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool foundForm = false;
            foreach (Form frm in fc)
            {
                if (frm is ModelsForm)
                {
                    foundForm = true;
                    frm.BringToFront();
                    break;
                }
            }
            if (!foundForm)
            {
                ModelsForm modelsForm = new ModelsForm();
                modelsForm.Show();
            }
        }

        private void buttonDesigners_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool foundForm = false;
            foreach (Form frm in fc)
            {
                if (frm is DesignersForm)
                {
                    foundForm = true;
                    frm.BringToFront();
                    break;
                }
            }
            if (!foundForm)
            {
                DesignersForm designersForm = new DesignersForm();
                designersForm.Show();
            }
        }

        private void buttonOverview_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool foundForm = false;
            foreach (Form frm in fc)
            {
                if (frm is OverviewForm)
                {
                    foundForm = true;
                    frm.BringToFront();
                    break;
                }
            }
            if (!foundForm)
            {
                OverviewForm overviewForm = new OverviewForm();
                overviewForm.Show();
            }
        }

        private void buttonAgencies_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool foundForm = false;
            foreach (Form frm in fc)
            {
                if (frm is AgenciesForm)
                {
                    foundForm = true;
                    frm.BringToFront();
                    break;
                }
            }
            if (!foundForm)
            {
                AgenciesForm agenciesForm = new AgenciesForm();
                agenciesForm.Show();
            }
        }

        private void buttonSpecialGuests_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool foundForm = false;
            foreach (Form frm in fc)
            {
                if (frm is AgenciesForm)
                {
                    foundForm = true;
                    frm.BringToFront();
                    break;
                }
            }
            if (!foundForm)
            {
                SpecialGuestsForm sgForm = new SpecialGuestsForm();
                sgForm.Show();
            }
        }
    }
}
