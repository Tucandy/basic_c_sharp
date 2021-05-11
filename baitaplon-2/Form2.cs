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
    public partial class Form2 : Form
    {
        public Form2()
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
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
                string SQL_them = "Insert into t_sanpham values(N'" + id + "',N'" + name + "',N'" + unit + "',N'" + manufacture + "',N'" + category + "')";
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

        private void button3_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
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

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txtid.Text = DGV_SANPHAM.CurrentRow.Cells[1].Value.ToString();
            txtname.Text = DGV_SANPHAM.CurrentRow.Cells[2].Value.ToString();
            txtunit.Text = DGV_SANPHAM.CurrentRow.Cells[3].Value.ToString();
            txtmanu.Text = DGV_SANPHAM.CurrentRow.Cells[4].Value.ToString();
            txtcate.Text = DGV_SANPHAM.CurrentRow.Cells[5].Value.ToString();
        }

        private void Hienthi_DGV()
        {
            strconn = new SqlConnection(chuoi);
            strconn.Open();

            DataTable DT = new DataTable();
            string SQL_hienthi = "Select * from t_sanpham";
            SqlDataAdapter Mydata = new SqlDataAdapter(SQL_hienthi, strconn);
            Mydata.Fill(DT);
            DGV_SANPHAM.DataSource = DT;


            strconn.Close();
        }

        private void DGV_SANPHAM_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            for (int i = 0; i < DGV_SANPHAM.RowCount ; i++)
            {
                DGV_SANPHAM.Rows[i].Cells[0].Value = i + 1;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            strconn = new SqlConnection(chuoi);
            strconn.Open();
            if (txtkey.Text == "")
            {
                Hienthi_DGV();
            }
            else
            {
                string SQL_hienthi = "Select * from t_sanpham where id_sanpham like N'%" + txtkey.Text + "%' or tensp like '%" + txtkey.Text + "%'";
                SqlDataAdapter Mydata = new SqlDataAdapter(SQL_hienthi, strconn);
                DataTable DT = new DataTable();
                Mydata.Fill(DT);
                DGV_SANPHAM.DataSource = DT;
                txtkey.Clear();
            }

            strconn.Close();
        }

      

        private void txtid_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtkey_TextChanged(object sender, EventArgs e)
        {

        }

     
       

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (saveFileDialog2.ShowDialog() == DialogResult.OK)
            {
                ToExcel(DGV_SANPHAM, saveFileDialog2.FileName);
            }
        }

        private void ToExcel(DataGridView DGV_SANPHAM, string fileName)
        {
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook workbook;
            Microsoft.Office.Interop.Excel.Worksheet worksheet;

            try
            {
                excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = false;
                excel.DisplayAlerts = false;

                workbook = excel.Workbooks.Add(Type.Missing);

                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets["Sheet1"];
                worksheet.Name = "BAI TAP LON";
                for (int i = 0; i < DGV_SANPHAM.ColumnCount; i++)
                {
                    worksheet.Cells[1, i + 1] = DGV_SANPHAM.Columns[i].HeaderText;
                }
                for (int i = 0; i < DGV_SANPHAM.RowCount; i++)
                {
                    for (int j = 0; j < DGV_SANPHAM.ColumnCount; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = DGV_SANPHAM.Rows[i].Cells[j].Value.ToString();
                    }
                }
                workbook.SaveAs(fileName);
                workbook.Close();
                excel.Quit();
                MessageBox.Show("Export product excel successful");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                workbook = null;
                worksheet = null;
            }
        }

        private void txtunit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
