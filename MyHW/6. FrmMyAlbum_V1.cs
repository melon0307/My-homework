using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHW
{
    public partial class FrmMyAlbum_V1 : Form
    {
        public FrmMyAlbum_V1()
        {
            InitializeComponent();

            this.cityTableAdapter1.Fill(this.hW61.City);

            for(int i =0;i < hW61.City.Rows.Count; i++)
            {
                LinkLabel lab = new LinkLabel();
                lab.Text = $"{hW61.City.Rows[i]["CityName"]}";
                lab.Top = 100+(80 * i);
                lab.Left = 10;
                lab.Tag = i;
                lab.Click += Lab_Click;
                this.splitContainer2.Panel1.Controls.Add(lab);
            }
        }

        private void Lab_Click(object sender, EventArgs e)
        {
            LinkLabel x = sender as  LinkLabel;
            this.photoTableAdapter1.FillByCityID(this.hW61.Photo, (int)x.Tag);
            this.dataGridView1.DataSource = this.hW61.Photo;
        }
    }
}
