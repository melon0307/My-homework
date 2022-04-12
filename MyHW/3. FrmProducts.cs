﻿using System;
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
            this.bindingSource1.DataSource = this.nwDataSet1.Products;
            this.dataGridView1.DataSource = this.bindingSource1;
            this.bindingNavigator1.BindingSource = this.bindingSource1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.productsTableAdapter1.UnitPriceBetween(this.nwDataSet1.Products, int.Parse(textBox1.Text), int.Parse(textBox2.Text));
            this.dataGridView1.DataSource = this.nwDataSet1.Products;
            this.lblResult.Text = $"結果  {this.textBox1.Text} 元 到 {this.textBox2.Text} 元, 共 {this.bindingSource1.Count}   筆";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.productsTableAdapter1.ProductName(this.nwDataSet1.Products, "%"+textBox3.Text+"%");
            this.dataGridView1.DataSource = this.nwDataSet1.Products;
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            this.lblResult.Text = $"結果   {this.bindingSource1.Count}   筆";
            this.label2.Text = $"{this.bindingSource1.Position + 1} / {this.bindingSource1.Count}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.bindingSource1.Position = 0;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.bindingSource1.Position -= 1;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.bindingSource1.Position += 1;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.bindingSource1.Position = this.bindingSource1.Count;
        }
    }
}
