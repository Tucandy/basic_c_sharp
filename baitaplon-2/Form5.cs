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
    public partial class Form5 : Form
    {
        public Form5()
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
            string sql_kt = "select * from t_khachhang where id_khachhang=N'" + txtcus.Text + "'";
            SqlCommand cmd = new SqlCommand(sql_kt, strconn);
            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                if (txtcus.Text == Dr[0].ToString())
                {
                    kt = false;
                    MessageBox.Show("Trùng mã khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtcus.Clear();
                    txtcus.Focus();
                    break;

                }

            }
            strconn.Close();
            return kt;

        }
        private void Form5_Load(object sender, EventArgs e)
        {
            Hienthi_DGV();
        }
        private void Hienthi_DGV()
        {
           
            strconn = new SqlConnection(chuoi);
            strconn.Open();

            DataTable DT = new DataTable();
            string SQL_hienthi = "Select * from t_khachhang";
            SqlDataAdapter Mydata = new SqlDataAdapter(SQL_hienthi, strconn);

            Mydata.Fill(DT);
            DGV_KHACHHANG.DataSource = DT;


            strconn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            strconn = new SqlConnection(chuoi);
            strconn.Open();
            string id = txtcus.Text;
            string name = txtname.Text;
            string address = txtaddress.Text;
            string phone = txtphone.Text;
     


            if (id == "")
            {
                MessageBox.Show("Chưa nhập mã khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtcus.Focus();
            }

            else
            {
                string SQL_them = "Insert into t_khachhang values(N'" + id + "',N'" + name + "',N'" + address + "',N'" + phone + "')";
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

        private void button4_Click(object sender, EventArgs e)
        {
            xoa();
        }

        private void xoa()
        {
            txtcus.Clear();
            txtname.Clear();
            txtaddress.Clear();
            txtphone.Clear();
            txtcus.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            strconn = new SqlConnection(chuoi);
            strconn.Open();

            SqlCommand cm = new SqlCommand("Update t_khachhang Set tenkhachhang = N'" + txtname.Text + "', diachi=N'" + label.Text + "', soDienThoai=N'" + txtphone.Text + "'  where id_khachhang=N'" + txtcus.Text + "'", strconn);
            try
            {
                DialogResult DRsult = MessageBox.Show("Có chắc chắn sửa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DRsult == DialogResult.Yes)
                {
                    cm.ExecuteReader();
                    MessageBox.Show("Cập nhật " + txtcus.Text + " Thành Công");
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
            String sql_xoa = "Delete from t_khachhang where id_khachhang=N'" + txtcus.Text + "'";
            SqlCommand cmd = new SqlCommand(sql_xoa, strconn);

            if (txtcus.Text == "")
            {
                MessageBox.Show("Chưa nhập mã khach hàng cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtcus.Focus();
            }
            else
            {

                DialogResult DRsult = MessageBox.Show("Bạn có chắc muốn xoá khách hàng này ko?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DRsult == DialogResult.Yes)
                {
                    cmd.ExecuteNonQuery();
                    Hienthi_DGV();
                    xoa();

                }

            }
            strconn.Close();
        }

        private void DGV_KHACHHANG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtcus.Text = DGV_KHACHHANG.CurrentRow.Cells[1].Value.ToString();
            txtname.Text = DGV_KHACHHANG.CurrentRow.Cells[2].Value.ToString();
            txtaddress.Text = DGV_KHACHHANG.CurrentRow.Cells[3].Value.ToString();
            txtphone.Text = DGV_KHACHHANG.CurrentRow.Cells[4].Value.ToString();
        }

        private void DGV_KHACHHANG_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            for (int i = 0; i < DGV_KHACHHANG.RowCount; i++)
            {
                DGV_KHACHHANG.Rows[i].Cells[0].Value = i + 1;
            }
        }

        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
