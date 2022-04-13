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
    public partial class FrmCategoryProducts : Form
    {
        
        public FrmCategoryProducts()
        {
            InitializeComponent();
            inputcbx1();
            inputcbx2();
        }
        
        //===============================================================
        //      Connected

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Step1: SqlConnection
            // Step2: SqlCommand
            // Step3: SqlDataReader
            // Step4: UI Control
            string input = this.comboBox1.Text;
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection("Data Source=.;Initial Catalog=Northwind;Integrated Security=True");
                conn.Open();
                SqlCommand command = new SqlCommand
                    (" select * " +
                     " from Products p" +
                     " join Categories c " + 
                     " on p.CategoryID=c.CategoryID " +
                     " where CategoryName='" + input + "'" + 
                     " order by UnitPrice", conn);
                SqlDataReader dataReader = command.ExecuteReader();
                listBox1.Items.Clear();
                while (dataReader.Read())
                {
                    string s = $"{dataReader["ProductName"],-35} - {dataReader["UnitPrice"]:c2}";
                    listBox1.Items.Add(s);
                }
                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

        }

        private void inputcbx1()
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection("Data Source=.;Initial Catalog=Northwind;Integrated Security=True");
                conn.Open();
                SqlCommand command = new SqlCommand("Select * from Categories", conn);
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    string s = $"{dataReader["CategoryName"]}";
                    comboBox1.Items.Add(s);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        //========================================================
        //       DisConnected
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            string input = this.comboBox2.Text;
            // Step1: SqlConnection
            // Step2: SqlDataAdapter
            // Step3: DataSet            - In Memmory DB
            // Step4: UI Control        - DataGridView  -  Table
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=Northwind;Integrated Security=True");
            SqlDataAdapter adapter = new SqlDataAdapter(" select * " +
                     " from Products p" +
                     " join Categories c " +
                     " on p.CategoryID=c.CategoryID " +
                     " where CategoryName='" + input + "'" +
                     " order by UnitPrice", conn);
            DataSet dataSet = new DataSet();            
            adapter.Fill(dataSet);
            listBox2.Items.Clear();
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                string s = $"{row["ProductName"],-35} - {row["UnitPrice"]:c2}";
                listBox2.Items.Add(s);
            }
        }

        private void inputcbx2()
        {
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=Northwind;Integrated Security=True");
            SqlDataAdapter adapter = new SqlDataAdapter("select CategoryName from Categories", conn);
            DataSet dataSet = new DataSet();
            // DataTable dataTable = new DataTable();
            // adapter.Fill(dataTable);
            // comboBox1.DataSourse = dataTable;
            // comboBox1.DisplayMember = "CategoryName";
            adapter.Fill(dataSet);
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                comboBox2.Items.Add(row["CategoryName"]);
            }
        }
    }
}
