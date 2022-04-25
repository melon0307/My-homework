using MyHomeWork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace MyHW
{
    public partial class FrmHomePage : Form
    {
        public FrmHomePage()
        {
            InitializeComponent();
        }
        
        private void btnPractice_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel2.Controls.Clear();
            FrmHomeWork f = new FrmHomeWork();
            f.TopLevel = false;
            f.Visible = true;
            this.splitContainer2.Panel2.Controls.Add(f);
        }

        private void btnFrmCategoryProducts_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel2.Controls.Clear();
            FrmCategoryProducts f = new FrmCategoryProducts();
            f.TopLevel = false;
            f.Visible = true;
            this.splitContainer2.Panel2.Controls.Add(f);
        }

        private void btnFrmProduct_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel2.Controls.Clear();
            FrmProducts f = new FrmProducts();
            f.TopLevel = false;
            f.Visible = true;
            this.splitContainer2.Panel2.Controls.Add(f);
        }

        private void btnFrmDataSet_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel2.Controls.Clear();
            FrmDataSet_結構 f = new FrmDataSet_結構(); ;
            f.TopLevel = false;
            f.Visible = true;
            this.splitContainer2.Panel2.Controls.Add(f);
        }

        private void btnFrmAdventureWorks_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel2.Controls.Clear();
            FrmAdventureWorks f = new FrmAdventureWorks();
            f.TopLevel = false;
            f.Visible = true;
            this.splitContainer2.Panel2.Controls.Add(f);
        }

        private void btnFrmMyAlbumV2_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel2.Controls.Clear();
            FrmMyAlbum_V1 f = new FrmMyAlbum_V1();
            f.TopLevel = false;
            f.Visible = true;
            this.splitContainer2.Panel2.Controls.Add(f);
        }

        private void btnLogonCustomers_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel2.Controls.Clear();
            FrmCustomers f = new FrmCustomers();
            f.TopLevel = false;
            f.Visible = true;
            this.splitContainer2.Panel2.Controls.Add(f);
        }

        private void buttonFrmTreeView_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel2.Controls.Clear();
            FrmTreeView f = new FrmTreeView();
            f.TopLevel = false;
            f.Visible = true;
            this.splitContainer2.Panel2.Controls.Add(f);
        }
    }
}
