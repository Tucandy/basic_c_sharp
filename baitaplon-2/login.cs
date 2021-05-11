using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;

namespace baitaplon_2
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        string chuoi = @"Data Source=rog-strix-g\sqlexpress;Initial Catalog=QL_LKDT;Integrated Security=True";
        SqlConnection strconn;


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection strconn = new SqlConnection(chuoi);
            try
            {

                strconn.Open();
                SqlCommand cmd = new SqlCommand("select username,password,role from t_login where username='" + txtuser.Text + "'and password='" + txtpass.Text + "' and role='Admin' or username='" + txtuser.Text + "'and password='" + txtpass.Text + "' and role='User'", strconn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable data = new DataTable();
                da.Fill(data);

                if (data.Rows.Count > 0) {

                    SqlCommand  check = new SqlCommand("select username,password,role from t_login where username='" + txtuser.Text + "'and password='" + txtpass.Text + "' and role='Admin'", strconn);
                    SqlDataReader reader = check.ExecuteReader();

                    if (reader.Read())
                    {
                        reader.Read();
                        MessageBox.Show("Xin chào " + txtuser.Text + " (Quyền của bạn là Admin) !", "Đăng nhập thàng công ");
                        Form1.quyen = "Admin";
                        this.Hide();



                    }
                    else
                    {
                        reader.Read();
                        MessageBox.Show("Xin chào " + txtuser.Text + " (Quyền của bạn là user) !", "Đăng nhập thàng công  ");
                        Form1.quyen = "user";
                        this.Hide();

                    }
                    Form1 fmain = new Form1();
                    fmain.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Sai tài khoản, vui lòng nhập lại");
                }
                strconn.Close();
            }

            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
}


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtpass.UseSystemPasswordChar = false;
            }
            else
            {
                txtpass.UseSystemPasswordChar = true;
            }
        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void login_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://twitter.com/home?lang=vi");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/live.and.work.like.a.boss/");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.instagram.com");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Phần mềm này là bài tâp lớn của Hoàng Anh Tú thuộc nhóm 1 bộ môn công nghệ .NET trường đại học Tài Nguyên và Môi Trường", "Thông tin về phầm mềm", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
