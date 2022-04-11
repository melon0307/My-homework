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
        }

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
                SqlCommand command = new SqlCommand("select * " +
                                                                                                      " from Products p" +
                                                                                                      " join Categories c " +
                                                                                                      "on p.CategoryID=c.CategoryID " +
                                                                                                      "where CategoryName='" + input + "'" + 
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
    }
}
