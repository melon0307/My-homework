using MyHW.Properties;
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

namespace MyHW
{
    public partial class FrmTreeView : Form
    {
        public FrmTreeView()
        {
            InitializeComponent();
            CreateNodes();
            treeView1.NodeMouseClick += TreeView1_NodeMouseClick;            
        }

        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            listView1.Items.Clear();
            listView1.Columns.Clear();
            TreeNode p = e.Node;
            this.listView1.View = View.List;

            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    if(p.Parent == null)
                    {
                        cmd.CommandText = $"select distinct City from Customers where Country = '{p.Text}'";
                        cmd.Connection = conn;
                        conn.Open();
                        SqlDataReader dataReader = cmd.ExecuteReader();

                        listView1.Items.Add("Cities: ");
                        listView1.Items.Add("=========================");

                        while (dataReader.Read())
                        {
                            listView1.Items.Add(dataReader[0].ToString());
                        }
                    }
                    else 
                    {
                        CreateListViewColumns();
                        cmd.CommandText = $"select *from Customers where City = '{p.Text}'";
                        cmd.Connection = conn;
                        conn.Open();
                        SqlDataReader dataReader = cmd.ExecuteReader();

                        while (dataReader.Read())
                        {
                            ListViewItem lvi = this.listView1.Items.Add(dataReader[0].ToString());
                            for (int i = 1; i < dataReader.FieldCount; i++)
                            {
                                if (dataReader.IsDBNull(i))
                                {
                                    lvi.SubItems.Add("N/A");
                                }
                                else
                                {
                                    lvi.SubItems.Add(dataReader[i].ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateListViewColumns()
        {
            this.listView1.View = View.Details;
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = $"select * from Customers";
                    cmd.Connection = conn;
                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    DataTable dataTable = dataReader.GetSchemaTable();

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        this.listView1.Columns.Add(dataTable.Rows[i][0].ToString());
                    }
                    this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateNodes()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Select country, city from Customers order By Country, City";
                    cmd.Connection = conn;
                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    List<string> lt = new List<string>();
                    TreeNode CountryNode = new TreeNode();
                    TreeNode CityNode = new TreeNode();

                    while (dataReader.Read())
                    {
                        string Country = dataReader["Country"].ToString();
                        string City = dataReader["City"].ToString();

                        if (lt.Contains(Country))
                        {
                            if (!lt.Contains(City))
                            {
                                CityNode = new TreeNode(City);
                                CountryNode.Nodes.Add(City);
                                CityNode.Name = City;
                                lt.Add(Country);
                                lt.Add(City);
                            }
                        }
                        else
                        {
                            CountryNode = treeView1.Nodes.Add(Country);
                            CountryNode.Name = Country;
                            lt.Add(Country);

                            CityNode = new TreeNode(City);
                            CountryNode.Nodes.Add(City);
                            CityNode.Name = City;                            
                            lt.Add(City);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        bool Flag = true;
        private void button1_Click(object sender, EventArgs e)
        {
            if (Flag)
            {
                treeView1.ExpandAll();
                button1.Text = $"全部收合";
                treeView1.Nodes[0].EnsureVisible();
            }
            else
            {
                treeView1.CollapseAll();
                button1.Text = $"全部展開";
            }

            Flag = !Flag;
        }
    }
}
