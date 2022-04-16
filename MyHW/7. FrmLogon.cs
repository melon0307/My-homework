using MyHW;
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

namespace MyHomeWork
{
    public partial class FrmLogon : Form
    {
        public FrmLogon()
        {
            InitializeComponent();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            SqlConnection conn = null;
            try
            {
                string userName = this.UsernameTextBox.Text;
                string password = this.PasswordTextBox.Text;

                conn = new SqlConnection(Settings.Default.NorthwindConnectionString);
                SqlCommand comm = new SqlCommand();
                comm.CommandText = "Insert into MyMember(UserName,Password) values(@UserName,@Password)";
                comm.Connection = conn;

                comm.Parameters.Add("@UserName", SqlDbType.NVarChar, 16).Value = userName;
                comm.Parameters.Add("@Password", SqlDbType.NVarChar, 40).Value = password;

                conn.Open();
                comm.ExecuteNonQuery();

                MessageBox.Show("已註冊完成");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.UsernameTextBox.Text = "";
                this.PasswordTextBox.Text = "";
                this.UsernameTextBox.Select();

                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            SqlConnection conn = null;
            try
            {
                string userName = this.UsernameTextBox.Text;
                string password = this.PasswordTextBox.Text;

                conn = new SqlConnection(Settings.Default.NorthwindConnectionString);
                SqlCommand comm = new SqlCommand();
                comm.CommandText = "select * from MyMember where UserName = @UserName and Password = @Password";
                comm.Connection = conn;

                comm.Parameters.Add("@UserName", SqlDbType.NVarChar, 16).Value=userName;
                comm.Parameters.Add("@Password", SqlDbType.NVarChar, 40).Value = password;

                conn.Open();
                SqlDataReader dataReader = comm.ExecuteReader();
                if (dataReader.HasRows)
                {
                    MessageBox.Show("登入成功");
                    FrmCustomers frmCustomers = new FrmCustomers();
                    frmCustomers.Show();
                }
                else
                {
                    MessageBox.Show("UserName 或 Password 錯誤");
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.UsernameTextBox.Text = "";
                this.PasswordTextBox.Text = "";
                this.UsernameTextBox.Select();

                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
