using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace baitaplon_2
{
    public partial class account : Form
    {
        public account()
        {
            InitializeComponent();
        }

        string chuoi = @"Data Source=rog-strix-g\sqlexpress;Initial Catalog=QL_LKDT;Integrated Security=True";
        SqlConnection strconn;

        private Boolean kiemtra()
        {
            Boolean kt = true;
            strconn = new SqlConnection(chuoi);
            strconn.Open();
            string sql_kt = "select * from t_login where username=N'" + txtuser.Text + "'";
            SqlCommand cmd = new SqlCommand(sql_kt, strconn);
            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                if (txtuser.Text == Dr[0].ToString())
                {
                    kt = false;
                    MessageBox.Show("Tên đăng nhập đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtuser.Clear();
                    txtuser.Focus();
                    break;

                }

            }
            strconn.Close();
            return kt;

        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void account_Load(object sender, EventArgs e)
        {
            Hienthi_DGV();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            strconn = new SqlConnection(chuoi);
            strconn.Open();
            string user = txtuser.Text;
            string pass = txtpass.Text;
            string role = txtrole.Text;

            if (user == "")
            {
                MessageBox.Show("Chưa nhập tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtuser.Focus();
            }

            else
            {
                string SQL_them = "Insert into t_login values(N'" + user + "',N'" + pass + "',N'" + role + "')";
                SqlCommand cmd = new SqlCommand(SQL_them, strconn);

                if (kiemtra())
                {
                    cmd.ExecuteNonQuery();
                    xoa();
                    Hienthi_DGV();
                }
            }



            strconn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            strconn = new SqlConnection(chuoi);
            strconn.Open();

            SqlCommand cm = new SqlCommand("Update t_login Set username = N'" + txtuser.Text + "', password=N'" + txtpass.Text + "', role=N'" + txtrole.Text  + "' where username =N'" + txtuser.Text + "'", strconn);
            try
            {
                DialogResult DRsult = MessageBox.Show("Có chắc chắn sửa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DRsult == DialogResult.Yes)
                {
                    cm.ExecuteReader();
                    MessageBox.Show("Cập nhật " + txtuser.Text + " Thành Công");
                    xoa();
                    Hienthi_DGV();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex);
            }

            strconn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            strconn = new SqlConnection(chuoi);
            strconn.Open();
            String sql_xoa = "Delete from t_login where username=N'" + txtuser.Text + "'";
            SqlCommand cmd = new SqlCommand(sql_xoa, strconn);

            if (txtuser.Text == "")
            {
                MessageBox.Show("Chưa nhập username cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtuser.Focus();
            }
            else
            {

                DialogResult DRsult = MessageBox.Show("Bạn có chắc muốn xoá ko?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DRsult == DialogResult.Yes)
                {
                    cmd.ExecuteNonQuery();
                    Hienthi_DGV();
                    xoa();

                }

            }
            strconn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            xoa();
        }
        private void xoa()
        {
            txtuser.Clear();
            txtpass.Clear();
            txtuser.Focus();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtuser.Text = DGV_USER.CurrentRow.Cells[1].Value.ToString();
            txtrole.Text = DGV_USER.CurrentRow.Cells[2].Value.ToString();
          
        }

        private void Hienthi_DGV()
        {
            strconn = new SqlConnection(chuoi);
            strconn.Open();

            DataTable DT = new DataTable();
            string SQL_hienthi = "Select username,role from t_login";
            SqlDataAdapter Mydata = new SqlDataAdapter(SQL_hienthi, strconn);
            Mydata.Fill(DT);
            DGV_USER.DataSource = DT;
            strconn.Close();
        }

        private void DGV_USER_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            for (int i = 0; i < DGV_USER.RowCount ; i++)
            {
                DGV_USER.Rows[i].Cells[0].Value = i + 1;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            strconn = new SqlConnection(chuoi);
            strconn.Open();
            if (txtkey2.Text == "")
            {
                Hienthi_DGV();
            }
            else
            {
                string SQL_hienthi = "Select * from t_login where username like N'%" + txtkey2.Text + "%' or tensp like '%" + txtkey2.Text + "%'";
                SqlDataAdapter Mydata = new SqlDataAdapter(SQL_hienthi, strconn);
                DataTable DT = new DataTable();
                Mydata.Fill(DT);

                DGV_USER.DataSource = DT;
                txtkey2.Clear();
            }

            strconn.Close();
        }

        private void account_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void txtrole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtpass_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtuser_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
