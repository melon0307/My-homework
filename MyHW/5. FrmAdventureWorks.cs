using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class FrmAdventureWorks : Form
    {
        public FrmAdventureWorks()
        {

            InitializeComponent();
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=AdventureWorks;Integrated Security=True");
            SqlDataAdapter adapter = new SqlDataAdapter(
                "select distinct datepart(yyyy,[ModifiedDate]) as 'Date' from[Production].[ProductPhoto] group by datepart(yyyy,[ModifiedDate])", conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "Date";


            this.productPhotoTableAdapter1.Fill(this.awDataSet1.ProductPhoto);
            this.bindingSource1.DataSource = awDataSet1.ProductPhoto;
            this.dataGridView1.DataSource = this.bindingSource1;
            this.bindingNavigator1.BindingSource = this.bindingSource1;            
        }

        bool judge = true;

        private void button13_Click(object sender, EventArgs e)
        {
            this.bindingSource1.MoveFirst();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.bindingSource1.MovePrevious();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.bindingSource1.MoveNext();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            this.bindingSource1.MoveLast();
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            this.label4.Text = $"{this.bindingSource1.Position + 1} / {this.bindingSource1.Count}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime date1 = dateTimePicker1.Value;
            DateTime date2 = dateTimePicker2.Value;
            
            this.productPhotoTableAdapter1.DateBetween(awDataSet1.ProductPhoto, date1, date2);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (judge)
            {
                this.dataGridView1.Sort(this.dataGridView1.Columns["ModifiedDate"], ListSortDirection.Descending);
                judge = !judge;
            }
            else
            {
                this.dataGridView1.Sort(this.dataGridView1.Columns["ModifiedDate"], ListSortDirection.Ascending);
                judge = !judge;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string yyyy = comboBox1.Text;
            this.productPhotoTableAdapter1.FillByYYYY(this.awDataSet1.ProductPhoto, yyyy);            
        }
    }
}
