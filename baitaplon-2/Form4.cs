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

namespace baitaplon_2
{
    public partial class Form4 : Form
    {
        public Form4()
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
            string sql_kt = "select * from t_nhanvien where id_nhanvien=N'" + txtstaff.Text + "'";
            SqlCommand cmd = new SqlCommand(sql_kt, strconn);
            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                if (txtstaff.Text == Dr[0].ToString())
                {
                    kt = false;
                    MessageBox.Show("Trùng mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtstaff.Clear();
                    txtstaff.Focus();
                    break;

                }

            }
            strconn.Close();
            return kt;

        }
        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void DGV_NHANVIEN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtstaff.Text = DGV_NHANVIEN.CurrentRow.Cells[1].Value.ToString();
            txtname.Text = DGV_NHANVIEN.CurrentRow.Cells[2].Value.ToString();
            txtgender.Text = DGV_NHANVIEN.CurrentRow.Cells[3].Value.ToString();
            txtaddress.Text = DGV_NHANVIEN.CurrentRow.Cells[4].Value.ToString();
            txtphone.Text = DGV_NHANVIEN.CurrentRow.Cells[5].Value.ToString();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            Hienthi_DGV();

        }
        private void Hienthi_DGV()
        { 
            strconn = new SqlConnection(chuoi);
            strconn.Open();

            DataTable DT = new DataTable();
            string SQL_hienthi = "Select * from t_nhanvien";
            SqlDataAdapter Mydata = new SqlDataAdapter(SQL_hienthi, strconn);

            Mydata.Fill(DT);
            DGV_NHANVIEN.DataSource = DT;


            strconn.Close();
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            strconn = new SqlConnection(chuoi);
            strconn.Open();
            string id = txtstaff.Text;
            string name = txtname.Text;
            string gender = txtgender.Text;
            string address = txtaddress.Text;
            string phone = txtphone.Text;


            if (id == "")
            {
                MessageBox.Show("Chưa nhập mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtstaff.Focus();
            }

            else
            {
                string SQL_them = "Insert into t_nhanvien values(N'" + id + "',N'" + name + "',N'" + gender + "',N'" + address + "',N'" + phone + "')";
                SqlCommand cmd = new SqlCommand(SQL_them, strconn);

                if (kiemtra())
                {
                    cmd.ExecuteNonQuery();

                }
            }

            strconn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            strconn = new SqlConnection(chuoi);
            strconn.Open();

            SqlCommand cm = new SqlCommand("Update t_nhanvien Set tenNhanVien = N'" + txtname.Text + "', gioitinh=N'" + txtgender.Text + "', diachi=N'" + txtaddress.Text + "' , soDienThoai=N'" + txtphone.Text + "' where id_nhanvien='" + txtstaff.Text + "'", strconn);
            try
            {
                DialogResult DRsult = MessageBox.Show("Có chắc chắn sửa thông tin nhân viên này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DRsult == DialogResult.Yes)
                {
                    cm.ExecuteReader();
                    MessageBox.Show("Cập nhật " + txtstaff.Text + " Thành Công");
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
            String sql_xoa = "Delete from t_nhanvien where id_nhanvien='" + txtstaff.Text + "'";
            SqlCommand cmd = new SqlCommand(sql_xoa, strconn);

            if (txtstaff.Text == "")
            {
                MessageBox.Show("Chưa nhập mã nhân viên cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtstaff.Focus();
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
            txtstaff.Clear();
            txtname.Clear();
            txtaddress.Clear();
            txtphone.Clear();
            txtstaff.Focus();
        }

        private void DGV_NHANVIEN_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            for (int i = 0; i < DGV_NHANVIEN.RowCount ; i++)
            {
                DGV_NHANVIEN.Rows[i].Cells[0].Value = i + 1;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}