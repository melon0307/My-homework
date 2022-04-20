using MyHW.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
            AddLinkLabel();
            LoadCityToComboBox();
            FlowLayOutPanel3();
        }

        private void AddLinkLabel()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.HW6ConnectionString))
                {
                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = "select * from City";
                    comm.Connection = conn;
                    conn.Open();
                    SqlDataReader dataReader = comm.ExecuteReader();
                    for (int i =0; dataReader.Read();i++)
                    {
                        LinkLabel lab = new LinkLabel();
                        lab.Text = $"{dataReader["CityName"]}";
                        lab.Click += Lab_Click;
                        lab.Tag = i;
                        this.flowLayoutPanel2.Controls.Add(lab);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadCityToComboBox()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.HW6ConnectionString))
                {
                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = "select * from City";
                    comm.Connection = conn;
                    conn.Open();
                    IDataReader dataReader = comm.ExecuteReader();
                    while (dataReader.Read())
                    {
                        comboBox1.Items.Add(dataReader["CityName"]);
                    }
                    comboBox1.Text = "請選擇...";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FlowLayOutPanel3()
        {
            flowLayoutPanel3.AllowDrop = true;
            flowLayoutPanel3.DragEnter += FlowLayoutPanel3_DragEnter;
            flowLayoutPanel3.DragDrop += FlowLayoutPanel3_DragDrop;
        }

        private void FlowLayoutPanel3_DragDrop(object sender, DragEventArgs e)
        {
            if(comboBox1.Text == "請選擇...")
            {
                MessageBox.Show("請選擇要加入圖片的城市。");
                return;
            }
            else
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(Settings.Default.HW6ConnectionString))
                    {
                        SqlCommand comm = new SqlCommand();
                        comm.CommandText = "Insert into Photo(CityID,Photo) values(@CityID,@Photo)";
                        comm.Connection = conn;
                        conn.Open();

                        string[] file = (string[])e.Data.GetData(DataFormats.FileDrop);
                        for (int i = 0; i < file.Length; i++)
                        {
                            comm.Parameters.Clear();
                            PictureBox pic = new PictureBox();
                            pic.Image = Image.FromFile(file[i]);
                            pic.SizeMode = PictureBoxSizeMode.StretchImage;
                            pic.Width = 300;
                            pic.Height = 200;
                            pic.Tag = i;
                            pic.BorderStyle = BorderStyle.FixedSingle;
                            pic.Click += Pic_Click;
                            flowLayoutPanel3.Controls.Add(pic);

                            MemoryStream ms = new MemoryStream();
                            pic.Image.Save(ms, ImageFormat.Jpeg);
                            byte[] bytes = ms.GetBuffer();
                            comm.Parameters.Add("@CityID", SqlDbType.Int).Value = this.comboBox1.SelectedIndex;
                            comm.Parameters.Add("@Photo", SqlDbType.Image).Value = bytes;
                            comm.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }            
        }

        private void Pic_Click(object sender, EventArgs e)
        {
            Form f = new Form();
            f.BackgroundImage = ((PictureBox)sender).Image;
            f.BackgroundImageLayout = ImageLayout.Stretch;
            f.Width = 600;
            f.Height = 400;
            f.Show();
        }

        private void FlowLayoutPanel3_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void Lab_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.HW6ConnectionString))
                {
                    flowLayoutPanel1.Controls.Clear();
                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = $"select * from Photo where CityID = {((LinkLabel)sender).Tag}";
                    comm.Connection = conn;
                    conn.Open();

                    SqlDataReader dataReader = comm.ExecuteReader();
                    while (dataReader.Read())
                    {
                        byte[] bytes = (byte[])dataReader["Photo"];
                        MemoryStream ms = new MemoryStream(bytes);
                        PictureBox pic = new PictureBox();
                        pic.Image = Image.FromStream(ms);
                        pic.SizeMode = PictureBoxSizeMode.StretchImage;
                        pic.Width = 300;
                        pic.Height = 200;
                        pic.BorderStyle = BorderStyle.FixedSingle;
                        pic.Click += Pic_Click;
                        flowLayoutPanel1.Controls.Add(pic);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.HW6ConnectionString))
                {
                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = "Insert into Photo(CityID,Photo) values(@CityID,@Photo)";
                    comm.Connection = conn;
                    conn.Open();
                    
                    List<string> filteredFiles;
                    FolderBrowserDialog FolderBrowser = new FolderBrowserDialog();

                    DialogResult result = FolderBrowser.ShowDialog();
                    filteredFiles = Directory.GetFiles(FolderBrowser.SelectedPath, "*.*").Where(file => file.ToLower().EndsWith("jpg")).ToList();
                    for (int i = 0; i < filteredFiles.Count; i++)
                    {
                        comm.Parameters.Clear();
                        PictureBox pic = new PictureBox();
                        pic.Image = Image.FromFile(filteredFiles[i]);
                        pic.SizeMode = PictureBoxSizeMode.StretchImage;
                        pic.Width = 300;
                        pic.Height = 200;
                        pic.BorderStyle = BorderStyle.FixedSingle;
                        pic.Click += Pic_Click;
                        flowLayoutPanel3.Controls.Add(pic);

                        MemoryStream ms = new MemoryStream();
                        pic.Image.Save(ms, ImageFormat.Jpeg);
                        byte[] bytes = ms.GetBuffer();
                        comm.Parameters.Add("@CityID", SqlDbType.Int).Value = this.comboBox1.SelectedIndex;
                        comm.Parameters.Add("@Photo", SqlDbType.Image).Value = bytes;

                        comm.ExecuteNonQuery();
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
