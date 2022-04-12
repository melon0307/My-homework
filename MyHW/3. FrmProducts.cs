using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class FrmProducts : Form
    {
        public FrmProducts()
        {
            InitializeComponent();
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            Output();
            lblResult.Text = $"結果: 共 {this.bindingSource1.Count} 筆";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                this.productsTableAdapter1.UnitPriceBetween(this.nwDataSet1.Products, int.Parse(textBox1.Text), int.Parse(textBox2.Text));                
                Output();
                this.lblResult.Text = $"結果: 單價介於 {this.textBox1.Text} 元到 {this.textBox2.Text} 元, 共 {this.bindingSource1.Count} 筆";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Trim() == "")
            {
                MessageBox.Show("請輸入文字");
                return;
            }
            else
            {
                string keyWord = $"%{textBox3.Text}%";
                this.productsTableAdapter1.ProductName(this.nwDataSet1.Products, keyWord);                
                Output();
                this.lblResult.Text = $"結果: 名字內有 {textBox3.Text} 的產品, 共 {this.bindingSource1.Count} 筆";
            }
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {            
            this.label2.Text = $"{this.bindingSource1.Position + 1} / {this.bindingSource1.Count}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.bindingSource1.MoveFirst() ;            
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.bindingSource1.MovePrevious();            
        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.bindingSource1.MoveNext(); ;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.bindingSource1.MoveLast();            
        }

        private void Output()
        {
            this.bindingSource1.DataSource = this.nwDataSet1.Products;
            this.dataGridView1.DataSource = this.bindingSource1;
            this.bindingNavigator1.BindingSource = this.bindingSource1;
        }
    }
}
