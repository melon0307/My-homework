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
    public partial class FrmDataSet_結構 : Form
    {
        public FrmDataSet_結構()
        { 
            InitializeComponent();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            this.categoriesTableAdapter1.Fill(this.nwDataSet1.Categories);
            this.customersTableAdapter1.Fill(this.nwDataSet1.Customers);

            this.dataGridView1.DataSource = this.nwDataSet1.Products;
            this.dataGridView2.DataSource = this.nwDataSet1.Categories;
            this.dataGridView3.DataSource = this.nwDataSet1.Customers;

            this.listBox1.Items.Clear();

            for (int i = 0; i < this.nwDataSet1.Tables.Count; i++)
            {
                DataTable dt = this.nwDataSet1.Tables[i];
                this.listBox1.Items.Add(dt.TableName);
                string s = "", st = "";
                for (int column = 0; column < dt.Columns.Count; column++)
                {
                    s += $"{dt.Columns[column],-35}|";
                }
                listBox1.Items.Add(s);

                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        st += $"{dt.Rows[row][j],-35}|";
                    }
                    listBox1.Items.Add(st);
                    st = "";
                }
                //s = "";
                //foreach (DataRow row in nwDataSet1.Tables[i].Rows)
                //{
                //    for (int j = 0; j < nwDataSet1.Tables[i].Columns.Count; j++)
                //    {
                //        s += $"{row[j],-35}|";
                //    }
                //    listBox1.Items.Add(s);
                //    s = "";
                //}

                listBox1.Items.Add("=====================================================================" +
                    "================================================================================" +
                    "================================================================================" +
                    "================================================================================" +
                    "==============================================================");
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            // Weak Type

            MessageBox.Show("Weak Type: [0][Name]  "+this.nwDataSet1.Products.Rows[0]["ProductName"]);
            MessageBox.Show("Weak Type: [0][1]  "+this.nwDataSet1.Products.Rows[0][1]);

            // Strong Type

            MessageBox.Show("Strong Type: Porducts[0].ProductName  "+this.nwDataSet1.Products[0].ProductName);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            this.nwDataSet1.Products.WriteXml("Products.xml",XmlWriteMode.WriteSchema);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            this.nwDataSet1.Products.Clear();
            this.nwDataSet1.Products.ReadXml("Products.xml");
            this.dataGridView1.DataSource = this.nwDataSet1.Products;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            this.splitContainer2.Panel1Collapsed = !this.splitContainer2.Panel1Collapsed;
        }
    }
}
