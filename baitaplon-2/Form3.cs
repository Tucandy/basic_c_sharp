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
using System.Globalization;

namespace baitaplon_2
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        string chuoi = @"Data Source=rog-strix-g\sqlexpress;Initial Catalog=QL_LKDT;Integrated Security=True";
        SqlConnection strconn;
        private void Form3_Load(object sender, EventArgs e)
        {
            strconn = new SqlConnection(chuoi);
            strconn.Open();
            string SQL_hienthi = "Select * from t_sanpham";
            string SQL_hienthi2 = "Select * from t_nhanvien";
            SqlDataAdapter Mydata = new SqlDataAdapter(SQL_hienthi, strconn);
            SqlDataAdapter Mydata2 = new SqlDataAdapter(SQL_hienthi2, strconn);
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            Mydata.Fill(dt);
            Mydata2.Fill(dt2);
            txtid2.DisplayMember = "id_sanpham";
            txtid2.ValueMember = "tensp";
            txtid2.DataSource = dt;
            txtstaff.DisplayMember = "id_nhanvien";
            txtstaff.ValueMember = "tenNhanVien";
            txtstaff.DataSource = dt2;
            strconn.Close();
            Hienthi_DGV();
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            result.Text = txtid2.SelectedValue.ToString();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void result_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private Boolean kiemtra()
        {
            Boolean kt = true;
            strconn = new SqlConnection(chuoi);
            strconn.Open();
            string sql_kt = "select * from t_hangnhap where id_hangnhap='" + txtid3.Text + "'";
            SqlCommand cmd = new SqlCommand(sql_kt, strconn);
            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                if (txtid3.Text == Dr[0].ToString())
                {
                    kt = false;
                    MessageBox.Show("Trùng mã nhập hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtid3.Clear();
                    txtid3.Focus();
                    break;

                }

            }
            strconn.Close();
            return kt;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            strconn = new SqlConnection(chuoi);
            strconn.Open();
            //lấy dữ liệu trên form
            string id = txtid3.Text;
            string id2 = txtid2.Text;
            string date = txtdate.Text;
            string amount = txtamount.Text;
            string staff = txtstaff.Text;
            string iprice = txtiprice.Text;
            string price = txtprice.Text;
            


            if (id == "")
            {
                MessageBox.Show("Chưa nhập mã nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtid3.Focus();
            }

            else
            {
                string SQL_them = "Insert into t_hangnhap values(N'" + id + "',N'" + id2 + "',N'" + date + "',N'" + amount + "',N'" + staff + "',N'" + iprice + "',N'" + price + "')";
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

        private void txtid_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)8;
            if(e.KeyChar == (char)13)
            {
                txtiprice.Text = string.Format(CultureInfo.CreateSpecificCulture("vi-VN"),"{0:C}", double.Parse(txtiprice.Text));
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)8;
            if (e.KeyChar == (char)13)
            {
                txtprice.Text = string.Format(CultureInfo.CreateSpecificCulture("vi-VN"), "{0:C}", double.Parse(txtprice.Text));
            }
        }

        private void txtstaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            result2.Text = txtstaff.SelectedValue.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            strconn = new SqlConnection(chuoi);
            strconn.Open();

            SqlCommand cm = new SqlCommand("Update t_hangnhap Set id_sanpham = N'" + txtid2.Text + "', ngaynhap=N'" + txtdate.Text + "', soluong=N'" + txtamount.Text + "' , id_nhanvien=N'" + txtstaff.Text + "' , gianhap=N'" + txtiprice.Text + "' , giaban=N'" + txtprice.Text + "' where id_hangnhap=N'" + txtid3.Text + "'", strconn);
            try
            {
                DialogResult DRsult = MessageBox.Show("Có chắc chắn sửa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DRsult == DialogResult.Yes)
                {
                    cm.ExecuteReader();
                    MessageBox.Show("Cập nhật " + txtid3.Text + " Thành Công");
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
            strconn = new SqlConnection(chuoi);
            strconn.Open();

            DataTable DT = new DataTable();
            string SQL_hienthi = "Select * from t_hangnhap";
            SqlDataAdapter Mydata = new SqlDataAdapter(SQL_hienthi, strconn);

            Mydata.Fill(DT);
            DGV_HANGNHAP.DataSource = DT;


            strconn.Close();
        }

        private void DGV_HANGNHAP_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            for (int i = 0; i < DGV_HANGNHAP.RowCount ; i++)
            {
                DGV_HANGNHAP.Rows[i].Cells[0].Value = i + 1;
            }
        }

        private void xoa()
        {
            txtid3.Clear();
            txtamount.Clear();
            txtiprice.Clear();
            txtprice.Clear();
            txtid3.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            strconn = new SqlConnection(chuoi);
            strconn.Open();
            String sql_xoa = "Delete from t_hangnhap where id_hangnhap='" + txtid3.Text + "'";
            SqlCommand cmd = new SqlCommand(sql_xoa, strconn);

            if (txtid3.Text == "")
            {
                MessageBox.Show("Chưa nhập mã hàng nhập cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtid3.Focus();
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

        private void ToExcel(DataGridView DGV_HANGNHAP, string fileName)
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
                for (int i = 0; i < DGV_HANGNHAP.ColumnCount; i++)
                {
                    worksheet.Cells[1, i + 1] = DGV_HANGNHAP.Columns[i].HeaderText;
                }
                for (int i = 0; i < DGV_HANGNHAP.RowCount; i++)
                {
                    for (int j = 0; j < DGV_HANGNHAP.ColumnCount; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = DGV_HANGNHAP.Rows[i].Cells[j].Value.ToString();
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

        private void button5_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ToExcel(DGV_HANGNHAP, saveFileDialog1.FileName);
            }
        }

        private void DGV_HANGNHAP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtid3.Text = DGV_HANGNHAP.CurrentRow.Cells[1].Value.ToString();
            txtid2.Text = DGV_HANGNHAP.CurrentRow.Cells[2].Value.ToString();
            txtamount.Text = DGV_HANGNHAP.CurrentRow.Cells[4].Value.ToString();
            txtstaff.Text = DGV_HANGNHAP.CurrentRow.Cells[5].Value.ToString();
            txtiprice.Text = DGV_HANGNHAP.CurrentRow.Cells[6].Value.ToString();
            txtprice.Text = DGV_HANGNHAP.CurrentRow.Cells[7].Value.ToString();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
