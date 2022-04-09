
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FrmHomeWork : Form
    {
        int A, B, C;
        StringBuilder s = new StringBuilder();
        public FrmHomeWork()
        {
            InitializeComponent();
        }

        bool 偶數(int n)
        {
            return n % 2 == 0 ? true : false;
        }

        bool isnum()
        {
            if (int.TryParse(txtFrom.Text, out A))
            {
                if (int.TryParse(txtTo.Text, out B))
                {
                    if (int.TryParse(txtStep.Text, out C))
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("請輸入數值");
                    }
                    return false;
                }
                else
                {
                    MessageBox.Show("請輸入數值");
                }
                return false;
            }
            else
            {
                MessageBox.Show("請輸入數值");
            }
            return false;
        }

        private int Max(params int[] array)
        {
            int max = array[0];
            for(int i = 0; i < array.Length; i++)
            {
                if(max < array[i])
                {
                    max = array[i];
                }
            }
            return max ;
        }

        private int Min(params int[] array)
        {
            int min = Int32.MaxValue;
            for (int i = 0; i < array.Length; i++)
            {
                if (min > array[i])
                {
                    min = array[i];
                }
            }
            return min;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int a = 100;
            int b = 66;
            int c = 77;
            int max = a > b ? a : b;
            int max1 = max > c ? max : c;
            lblResult.Text = "3個數 100, 66, 77 中，最大數為 : " + max1.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int odd = 0, even = 0;
            int[] nums = { 33, 4, 5, 11, 222, 34 };
            foreach(int n in nums)
            {
                if (偶數(n))
                {
                    even++;
                }
                else
                {
                    odd++;
                }
            }
            lblResult.Text = "int[] nums 陣列中{33, 4, 5, 11, 222, 34}, 奇數有 " + odd +"個, "+ " 偶數有 " + even+"個";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string[] names = { "aaa", "ksdkfjsdk"};
            int max = names[0].Length;
            int index = 0;
            for(int i = 0; i < names.Length; i++)
            {
                if (max <= names[i].Length)
                {
                    max = names[i].Length;
                    index = i;
                }
                else { }
                lblResult.Text = "string 陣列 names { aaa, ksdkfjsdk} 中\r\n最長名字為 : " + names[index];
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            int n;
            if (int.TryParse(txtNum.Text, out n))
            {
                if (偶數(n))
                    lblResult.Text = "輸入的數 " + n + "為  偶數。";
                else
                    lblResult.Text = "輸入的數 " + n + "為  奇數。";
            }
            else
            {
                MessageBox.Show("請輸入數值");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int sum = 0;
            if (isnum())
            {
                for (int i = A; i <= B; i += C)
                    sum += i;
                lblResult.Text = A.ToString() + "  到  " + B.ToString() + "  相隔  " + (C - 1).ToString() + "\r\n加總為  " + sum;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int sum = 0;
            if (isnum())
            {
                int i = A;
                while (i <= B)
                {
                    sum += i;
                    i += C;
                    lblResult.Text = A.ToString() + "  到  " + B.ToString() + "  相隔  " + (C - 1).ToString() + "\r\n加總為  " + sum;
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int sum = 0;
            if (isnum())
            {
                int i = A;
                do
                {
                    sum += i;
                    i += C;
                    lblResult.Text = A.ToString() + "  到  " + B.ToString() + "  相隔  " + (C - 1).ToString() + "\r\n加總為  " + sum;
                } while (i <= B);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            s.Clear();
            s.Append("九九乘法表\r\n");
            for (int i = 1; i <= 9; i++)
            {
                for (int j = 2; j <= 9; j++)
                {
                    string 積 = i * j < 10 ? "0" + i * j : (i * j).ToString();
                    s.Append(" "+ j + "x " + i + "=" + 積 +" | " );
                }
                if (i != 9)
                {
                    s.Append("\r\n");
                }
                else { }
            }
            lblResult.Text = s.ToString();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            int n = 100;
            int x;
            string binary = "";
            while (n > 0)
            {
                x = n % 2;
                n /= 2;
                binary = x.ToString() + binary;
            }
            lblResult.Text = binary;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            lblResult.Text = "結果";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string[] names = { "aaa", "ksdkfjsdk" };
            int Cc = 0;
            for (int i = 0; i < names.Length; i++)
            {
                foreach (char c in names[i])
                {
                    if (c == 'C' || c == 'c')
                    {
                        Cc++;
                        break;   
                    }
                    else { }
                }
            }
            lblResult.Text = "string 陣列 names { aaa, ksdkfjsdk} 中\r\n名字內有C或c的共 "+Cc+" 位";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[] scores = { 2, 3, 46, 33, 22, 100, 150, 33, 55 };
            lblResult.Text = "int 陣列 scores { 2, 3, 46, 33, 22, 100, 150, 33, 55 } 中\r\n最大數為 : " + Max(scores).ToString();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            lblResult.Text = "";
            Random rd = new Random();
            int[] lottery = new int[6];
            for(int i = 0; i < 6; i++)
            {
                lottery[i] = rd.Next(1, 50);
                for(int j=0; j < i; j++)
                {
                    if (lottery[i] == lottery[j])
                    {
                        lottery[i] = rd.Next(1, 50);
                    }
                }
                lblResult.Text += lottery[i].ToString()+", ";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int[] scores = { 2, 3, 46, 33, 22, 100,150, 33,55};

            int max =  scores.Max();
            //MessageBox.Show("Max = " + max);

            //Array.Sort(scores);
            //MessageBox.Show("Max =" + scores[scores.Length - 1]);

            //================================

            //Point[] points = new Point[3];
            //points[0].X = 3;
            //points[0].Y = 4;
            ////System.InvalidOperationException: '無法比較陣列中的兩個元素。'

            //Array.Sort(points);

            //=================================
            lblResult.Text = "int 陣列 scores { 2, 3, 46, 33, 22, 100, 150, 33, 55 } 中\r\n最大數為 : " + max.ToString() + "\r\n最小數為 : " + Min(scores).ToString();

        }

        int MyMinScore(int[] nums)
        {
            return 10;
        }
    }
}
