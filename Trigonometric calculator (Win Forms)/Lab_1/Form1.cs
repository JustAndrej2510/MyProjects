using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_1
{
   

   
    public partial class Form1 : Form
    {
        Calculator calc = new Calculator();
        private string buffer { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            calc.Sin(textBox1);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            calc.Cos(textBox1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            calc.Ctg(textBox1);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            calc.Tan(textBox1);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            calc.Sqrt2(textBox1);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            calc.Sqrt3(textBox1);
        }
        private void button8_Click(object sender, EventArgs e)
        {
            calc.PowN(textBox1, label1);
        }
        private void button9_Click(object sender, EventArgs e)
        {
            calc.Pow2(textBox1);
        }
        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text = buffer;
        }
        private void button11_Click(object sender, EventArgs e)
        {
            buffer = textBox1.Text;
        }
        private void button12_Click(object sender, EventArgs e)
        {
            calc.ResultPowN(textBox1, label1);
        }
        private void button13_Click(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }

    interface Operations
    {
        void Sin(TextBox textBox);
        void Cos(TextBox textBox);
        void Tan(TextBox textBox);
        void Ctg(TextBox textBox);
        void Sqrt2(TextBox textBox);
        void Sqrt3(TextBox textBox);
        void Pow2(TextBox textBox);
        void PowN(TextBox textBox, Label label);
        void ResultPowN(TextBox textBox, Label label);

    }

    public class Calculator : Operations
    {
        private double temp;
        public void Sin(TextBox textBox)
        {
            try
            {
                temp = double.Parse(textBox.Text)*(Math.PI/180);
                textBox.Clear();
                textBox.Text = Math.Sin(temp).ToString();

            }
            catch
            {
                textBox.Clear();
                textBox.Text = "Error";
            }
        }

        public void Cos(TextBox textBox)
        {
            try
            {
                temp = double.Parse(textBox.Text) * (Math.PI / 180);
                textBox.Clear();
                textBox.Text = Math.Cos(temp).ToString();

            }
            catch
            {
                textBox.Clear();
                textBox.Text = "Error";
            }
        }

        public void Tan(TextBox textBox)
        {
            try
            {
                temp = double.Parse(textBox.Text) * (Math.PI / 180);
                textBox.Clear();
                textBox.Text = Math.Tan(temp).ToString();

            }
            catch
            {
                textBox.Clear();
                textBox.Text = "Error";
            }
        }

        public void Ctg(TextBox textBox)
        {
            try
            {
                temp = double.Parse(textBox.Text) * (Math.PI / 180);
                textBox.Clear();
                textBox.Text = (Math.Cos(temp)/Math.Sin(temp)).ToString();

            }
            catch
            {
                textBox.Clear();
                textBox.Text = "Error";
            }
        }

        public void Sqrt2(TextBox textBox)
        {
            try
            {
                temp = double.Parse(textBox.Text);
                textBox.Clear();
                textBox.Text = Math.Sqrt(temp).ToString();

            }
            catch
            {
                textBox.Clear();
                textBox.Text = "Error";
            }
        }

        public void Sqrt3(TextBox textBox)
        {
            try
            {
                temp = double.Parse(textBox.Text);
                textBox.Clear();
                textBox.Text = Math.Pow(temp,1.0/3.0).ToString();

            }
            catch
            {
                textBox.Clear();
                textBox.Text = "Error";
            }
        }

        public void Pow2(TextBox textBox)
        {
            try
            {
                temp = double.Parse(textBox.Text);
                textBox.Clear();
                textBox.Text = Math.Pow(temp, 2.0).ToString();

            }
            catch
            {
                textBox.Clear();
                textBox.Text = "Error";
            }
        }

        public void PowN(TextBox textBox, Label label)
        {
            try
            {
                if (label.Text == "")
                {
                    temp = double.Parse(textBox.Text);
                    textBox.Clear();
                    label.Text = "^" + temp.ToString();
                   
                }
                else
                    throw new Exception();
               
            }
            catch
            {
                textBox.Clear();
                textBox.Text = "Error";
            }
        }

        public void ResultPowN(TextBox textBox, Label label)
        {
            string temp2;
            try
            {
                if (label.Text != "")
                {
                    temp = double.Parse(textBox.Text);
                    textBox.Clear();
                    label.Text = label.Text.Remove(0, label.Text.IndexOf('^')+1);
                    temp2 = label.Text;
                    label.Text = "";
                    textBox.Text = Math.Pow(double.Parse(temp2), temp).ToString();
                   

                }
                

            }
            catch
            {
                textBox.Clear();
                textBox.Text = "Error";
            }
        }
    }
}
