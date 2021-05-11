using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace baitaplon_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            login login = new login();
            login.Show();
            this.Hide();
        }

        public static string quyen;
        private void Form1_Load(object sender, EventArgs e)
        {
            if (quyen == "Admin")
            {
                loginToolStripMenuItem.Enabled = true;
                accountManagementToolStripMenuItem.Enabled = true;
                changePaswordToolStripMenuItem.Enabled = true;
            }
            else if (quyen == "user")
            {
                loginToolStripMenuItem.Enabled = true;
                accountManagementToolStripMenuItem.Enabled = false;
                changePaswordToolStripMenuItem.Enabled = true;
            }

            DateTime tb = DateTime.Now;
            label3.Text = tb.ToString("dd/MM/yyyy");
        }

        private void accountManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            account acc = new account();
            acc.Show();
            this.Hide();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            login ac = new login();
            ac.Show();
            this.Hide();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void productManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 a = new Form2();
            a.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://twitter.com/home?lang=vi");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/live.and.work.like.a.boss/"); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.instagram.com");
        }

        private void importProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 b = new Form3();
            b.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void sTAFFMANAGEMENTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 a = new Form4();
            a.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void mYCUSTOMERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 b = new Form5();
            b.Show();
            this.Hide();
        }

        private void billToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void billManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cart b = new cart();
            b.Show();
            this.Hide();
        }
    }
}
