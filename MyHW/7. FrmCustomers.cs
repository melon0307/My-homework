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
    public partial class FrmCustomers : Form
    {
        public FrmCustomers()
        {
            InitializeComponent();
            CreateListViewColumns();
            LoadCountryToCombox();
        }

        //TODO HW

        //1. All Country


        //================================
        //2. ContextMenuStrip2
        //選擇性作業
        //Groups
        //USA (100) 
        //UK (20)

        //this.listview1.visible = false;
        //ListViewItem lvi = this.listView1.Items.Add(dataReader[0].ToString());

        //if (this.listView1.Groups["USA"] == null)
        //{                       {
        //    ListViewGroup group = this.listView1.Groups.Add("USA", "USA"); //Add(string key, string headerText);
        //    group.Tag = 0;
        //    lvi.Group = group; 
        //}
        //else
        //{
        //    ListViewGroup group = this.listView1.Groups["USA"]; 
        //    lvi.Group = group;
        //}

        //this.listView1.Groups["USA"].Tag = 
        //this.listView1.Groups["USA"].Header = 
        bool groups = false;

        private void CreateListViewColumns()
        {
            this.listView1.View = View.Details;            
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = "select * from Customers";
                    comm.Connection = conn;

                    SqlDataReader dataReader = comm.ExecuteReader();
                    DataTable dt = dataReader.GetSchemaTable();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.listView1.Columns.Add(dt.Rows[i][0].ToString());
                    }
                    this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }           
        }

        private void LoadCountryToCombox()
        {            
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = "select distinct Country from Customers";
                    comm.Connection = conn;

                    SqlDataReader dataReader = comm.ExecuteReader();
                    comboBox1.Items.Clear();
                    comboBox1.Items.Add("All Countries");
                    while (dataReader.Read())
                    {
                        comboBox1.Items.Add(dataReader["Country"]);
                    }   
                    
                    comboBox1.Text = "請選擇... ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }           
        }

        private void CountryFlag(string CountryName, ListViewItem lvi)
        {
            if (CountryName == "Argentina")
            {
                lvi.ImageIndex = 19;
            }
            else if (CountryName == "Austria")
            {
                lvi.ImageIndex = 12;
            }
            else if (CountryName == "Belgium")
            {
                lvi.ImageIndex = 13;
            }
            else if (CountryName == "Brazil")
            {
                lvi.ImageIndex = 14;
            }
            else if (CountryName == "Canada")
            {
                lvi.ImageIndex = 15;
            }
            else if (CountryName == "Denmark")
            {
                lvi.ImageIndex = 16;
            }
            else if (CountryName == "Finland")
            {
                lvi.ImageIndex = 17;
            }
            else if (CountryName == "France")
            {
                lvi.ImageIndex = 18;
            }
            else if (CountryName == "Germany")
            {
                lvi.ImageIndex = 0;
            }
            else if (CountryName == "Ireland")
            {
                lvi.ImageIndex = 20;
            }
            else if (CountryName == "Italy")
            {
                lvi.ImageIndex = 4;
            }
            else if (CountryName == "Mexico")
            {
                lvi.ImageIndex = 6;
            }
            else if (CountryName == "Norway")
            {
                lvi.ImageIndex = 21;
            }
            else if (CountryName == "Poland")
            {
                lvi.ImageIndex = 22;
            }
            else if (CountryName == "Portugal")
            {
                lvi.ImageIndex = 23;
            }
            else if (CountryName == "Spain")
            {
                lvi.ImageIndex = 8;
            }
            else if (CountryName == "Sweden")
            {
                lvi.ImageIndex = 24;
            }
            else if (CountryName == "Switzerland")
            {
                lvi.ImageIndex = 25;
            }
            else if (CountryName == "UK")
            {
                lvi.ImageIndex = 9;
            }
            else if (CountryName == "USA")
            {
                lvi.ImageIndex = 10;
            }
            else if (CountryName == "Venezuela")
            {
                lvi.ImageIndex = 26;
            }
        }

        private void LoadListView()
        {
            if (groups == false)
            {
                this.listView1.Items.Clear();
                this.listView1.Groups.Clear();
                if (comboBox1.Text == "All Countries")
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                        {
                            conn.Open();
                            SqlCommand comm = new SqlCommand();
                            comm.CommandText = "select * from Customers";
                            comm.Connection = conn;
                            SqlDataReader dataReader = comm.ExecuteReader();

                            this.listView1.Items.Clear();
                            while (dataReader.Read())
                            {
                                ListViewItem lvi = this.listView1.Items.Add(dataReader[0].ToString());
                                if (lvi.Index % 2 == 0)
                                {
                                    lvi.BackColor = Color.SandyBrown;
                                }
                                else
                                {
                                    lvi.BackColor = Color.AntiqueWhite;
                                }

                                CountryFlag(dataReader["Country"].ToString(), lvi);

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
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                        {
                            conn.Open();
                            string CountryName = this.comboBox1.Text;
                            SqlCommand command = new SqlCommand();
                            command.CommandText = $"select * from Customers where Country = '{CountryName}'";
                            command.Connection = conn;
                            SqlDataReader dataReader = command.ExecuteReader();

                            this.listView1.Items.Clear();
                            while (dataReader.Read())
                            {
                                ListViewItem lvi = this.listView1.Items.Add(dataReader[0].ToString());
                                if (lvi.Index % 2 == 0)
                                {
                                    lvi.BackColor = Color.SandyBrown;
                                }
                                else
                                {
                                    lvi.BackColor = Color.AntiqueWhite;
                                }

                                CountryFlag(dataReader["Country"].ToString(), lvi);

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
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                this.listView1.Items.Clear();
                this.listView1.Groups.Clear();
                if (comboBox1.Text == "All Countries")
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                        {
                            conn.Open();
                            SqlCommand comm = new SqlCommand();
                            comm.CommandText = "select * from Customers";
                            comm.Connection = conn;
                            SqlDataReader dataReader = comm.ExecuteReader();

                            this.listView1.Items.Clear();
                            while (dataReader.Read())
                            {
                                ListViewItem lvi = this.listView1.Items.Add(dataReader[0].ToString());
                                if (lvi.Index % 2 == 0)
                                {
                                    lvi.BackColor = Color.SandyBrown;
                                }
                                else
                                {
                                    lvi.BackColor = Color.AntiqueWhite;
                                }

                                CountryFlag(dataReader["Country"].ToString(), lvi);
                                Groups(dataReader["Country"].ToString(), lvi);

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
                            for (int i = 0; i < this.listView1.Groups.Count; i++)
                            {
                                this.listView1.Groups[i].Header = $"{this.listView1.Groups[i].Name}({this.listView1.Groups[i].Items.Count})";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                        {
                            conn.Open();
                            string CountryName = this.comboBox1.Text;
                            SqlCommand command = new SqlCommand();
                            command.CommandText = $"select * from Customers where Country = '{CountryName}'";
                            command.Connection = conn;
                            SqlDataReader dataReader = command.ExecuteReader();

                            this.listView1.Items.Clear();
                            while (dataReader.Read())
                            {
                                ListViewItem lvi = this.listView1.Items.Add(dataReader[0].ToString());
                                if (lvi.Index % 2 == 0)
                                {
                                    lvi.BackColor = Color.SandyBrown;
                                }
                                else
                                {
                                    lvi.BackColor = Color.AntiqueWhite;
                                }

                                CountryFlag(dataReader["Country"].ToString(), lvi);
                                Groups(dataReader["Country"].ToString(), lvi);

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
                            for (int i = 0; i < this.listView1.Groups.Count; i++)
                            {
                                this.listView1.Groups[i].Header = $"{this.listView1.Groups[i].Name}({this.listView1.Groups[i].Items.Count})";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadListView();              
        }

        private void toolItemLargeIcon_Click(object sender, EventArgs e)
        {
            this.listView1.View = View.LargeIcon;
        }

        private void toolItemSmallIcon_Click(object sender, EventArgs e)
        {
            this.listView1.View = View.SmallIcon;
        }

        private void toolItemDetails_Click(object sender, EventArgs e)
        {
            this.listView1.View = View.Details;
        }

        private void customerIDAscToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.listView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
        }

        private void customerIDDescToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.listView1.Sorting = System.Windows.Forms.SortOrder.Descending;
        }

        private void Groups(string GroupName, ListViewItem lvi)
        {            
            if (this.listView1.Groups[GroupName] == null)
            {
                {
                    ListViewGroup group = this.listView1.Groups.Add(GroupName, GroupName);                    
                    lvi.Group = group;                   
                }
            }
            else
            {                
                ListViewGroup group = this.listView1.Groups[GroupName];                
                lvi.Group = group;                
            }
        }

        private void countryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groups = true;
            LoadListView();
        }

        private void 無ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groups = false;
            LoadListView();
        }
    }
}                               
