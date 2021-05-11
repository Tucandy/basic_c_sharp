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

namespace baitaplon
{
    public partial class sanpham : Form
    {
        public sanpham()
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
            string sql_kt = "select * from t_sanpham where id_sanpham=N'" + txtid.Text + "'";
            SqlCommand cmd = new SqlCommand(sql_kt, strconn);
            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                if (txtid.Text == Dr[0].ToString())
                {
                    kt = false;
                    MessageBox.Show("Trùng mã sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtid.Clear();
                    txtid.Focus();
                    break;

                }

            }
            strconn.Close();
            return kt;

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void sanpham_Load(object sender, EventArgs e)
        {
            Hienthi_DGV();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtid.Text = DGV_SANPHAM.CurrentRow.Cells[1].Value.ToString();
            txtname.Text = DGV_SANPHAM.CurrentRow.Cells[2].Value.ToString();
            txtunit.Text = DGV_SANPHAM.CurrentRow.Cells[3].Value.ToString();
            txtmanu.Text = DGV_SANPHAM.CurrentRow.Cells[4].Value.ToString();
            txtcate.Text = DGV_SANPHAM.CurrentRow.Cells[5].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            strconn = new SqlConnection(chuoi);
            strconn.Open();
            //lấy dữ liệu trên form
            string id = txtid.Text;
            string name = txtname.Text;
            string unit = txtunit.Text;
            string manufacture = txtmanu.Text;
            string category = txtcate.Text;


            if (id == "")
            {
                MessageBox.Show("Chưa nhập mã sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtid.Focus();
            }

            else
            {

                //thực thi câu lệnh Insert
                string SQL_them = "Insert into t_sanpham values(N'" + id + "',N'" + name + "',N'" + unit + "',N'" + manufacture + "',N'" + category + "')";

                //khai báo đối tượng Command
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
            String sql_xoa = "Delete from t_sanpham where id_sanpham=N'" + txtid.Text + "'";
            SqlCommand cmd = new SqlCommand(sql_xoa, strconn);

            if (txtid.Text == "")
            {
                MessageBox.Show("Chưa nhập mã sản phẩm cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtid.Focus();
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
            txtid.Clear();
            txtname.Clear();
            txtmanu.Clear();
            txtcate.Clear();
            txtid.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            strconn = new SqlConnection(chuoi);
            strconn.Open();

            SqlCommand cm = new SqlCommand("Update t_sanpham Set tensp = N'" + txtname.Text + "', donVi=N'" + txtunit.Text + "', nhaSanXuat=N'" + txtmanu.Text + "' , danhMuc=N'" + txtcate.Text + "' where id_sanpham=N'" + txtid.Text + "'", strconn);
            try
            {
                DialogResult DRsult = MessageBox.Show("Có chắc chắn sửa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DRsult == DialogResult.Yes)
                {
                    cm.ExecuteReader();
                    MessageBox.Show("Cập nhật " + txtid.Text + " Thành Công");
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
        private void Hienthi_DGV()
        {
            //kết nối

            strconn = new SqlConnection(chuoi);
            strconn.Open();

            DataTable DT = new DataTable();//khai báo DT
            string SQL_hienthi = "Select * from t_sanpham";
            SqlDataAdapter Mydata = new SqlDataAdapter(SQL_hienthi, strconn);//nhận giá trị trả về từ câu lệnh SQL

            Mydata.Fill(DT);//Đổ dữ liệu từ DataAdapter vào DT
            DGV_SANPHAM.DataSource = DT;//hiển thị dữ liệu từ bảng DT lên DGView


            strconn.Close();
        }

        private void DGV_SANPHAM_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            for (int i = 0; i < DGV_SANPHAM.RowCount - 1; i++)
            {
                DGV_SANPHAM.Rows[i].Cells[0].Value = i + 1;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //kết nối

            strconn = new SqlConnection(chuoi);
            strconn.Open();
            if (txtkey.Text == "")
            {
                Hienthi_DGV();
            }
            else
            {
                string SQL_hienthi = "Select * from t_sanpham where id_sanpham like N'%" + txtkey.Text + "%' or tensp like '%" + txtkey.Text + "%'";
                SqlDataAdapter Mydata = new SqlDataAdapter(SQL_hienthi, strconn);//nhận giá trị trả về từ câu lệnh SQL
                DataTable DT = new DataTable();//khai báo DT
                Mydata.Fill(DT);//Đổ dữ liệu từ DataAdapter vào DT

                DGV_SANPHAM.DataSource = DT;//hiển thị dữ liệu từ bảng DT lên DGView
                txtkey.Clear();
            }

            strconn.Close();
        }
    }
}
