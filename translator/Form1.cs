using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Translation;

namespace translator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            var prepare = new Preparing();

            double num;
            string sumOFNum = richTextBox1.Text;
            string slavnum = "";

            richTextBox2.Clear();
            richTextBox3.Clear();
            richTextBox4.Clear();

            try
            {
                num = prepare.Calculating(sumOFNum);
            }
            catch (Exception ex)
            {
                richTextBox4.Text = ex.Message;
                richTextBox4.BackColor = Color.Red;
                return;
            }
            slavnum = prepare.ToSlav((int)num);
            richTextBox2.Text = slavnum;
            richTextBox3.Text = (num.ToString());
            richTextBox4.BackColor = Color.Green;
            richTextBox4.Text = "Everything went well!";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var Types = new Dictionary<int, int>();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void clearingFields()
        {
            richTextBox4.BackColor = Color.RoyalBlue;
            richTextBox1.Clear();
            richTextBox2.Clear();
            richTextBox3.Clear();
            richTextBox4.Clear();
            richTextBox4.Text = "Waiting for input ...";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clearingFields();
        }

    }
}
