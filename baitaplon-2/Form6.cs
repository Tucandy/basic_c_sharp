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
    public partial class cart : Form
    {
        public cart()
        {
            InitializeComponent();
            fillcbo();
            fillcbo1();
            fillcbo2();
        }
        string chuoi = @"Data Source=rog-strix-g\sqlexpress;Initial Catalog=QL_LKDT;Integrated Security=True";
        SqlConnection strconn;
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
         
        }

        public void fillcbo()
        {
            strconn = new SqlConnection(chuoi);
            string sql = "Select * from t_khachhang ";
            SqlCommand cmd = new SqlCommand(sql, strconn);
            SqlDataReader myreader;
            try
            {
                strconn.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    String id = myreader.GetString(0);
                    cbo_idcus.Items.Add(id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            strconn = new SqlConnection(chuoi);
            string sql = "Select * from t_khachhang where id_khachhang ='" + cbo_idcus.Text + "';";
            SqlCommand cmd = new SqlCommand(sql, strconn);
            SqlDataReader myreader;
            try
            {
                strconn.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    
                    String name = myreader.GetString(1);
                    String address = myreader.GetString(2);
                    String phone = myreader.GetString(3);
                    txtnamecus.Text = name;
                    txtaddcus.Text = address;
                    txtphonecus.Text = phone;
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        public void fillcbo1()
        {
            strconn = new SqlConnection(chuoi);
            string sql = "Select * from t_nhanvien ";
            SqlCommand cmd = new SqlCommand(sql, strconn);
            SqlDataReader myreader;
            try
            {
                strconn.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    String id = myreader.GetString(0);
                    txtstaffid.Items.Add(id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void fillcbo2()
        {
            strconn = new SqlConnection(chuoi);
            string sql = "Select * from t_sanpham ";
            SqlCommand cmd = new SqlCommand(sql, strconn);
            SqlDataReader myreader;
            try
            {
                strconn.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    String id = myreader.GetString(0);
                    txtidproduct.Items.Add(id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            txtamount.Text = "0";
            txtprice.Text = "0";
            txttotalprice.Text = "0";
        }
       
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void Form6_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void txtphonecus_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtaddcus_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtnamecus_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtinvoiceid_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtnameproduct_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtstaffid_SelectedIndexChanged(object sender, EventArgs e)
        {
            strconn = new SqlConnection(chuoi);
            string sql = "Select * from t_nhanvien where id_nhanvien ='" + txtstaffid.Text + "';";
            SqlCommand cmd = new SqlCommand(sql, strconn);
            SqlDataReader myreader;
            try
            {
                strconn.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {

                    String name = myreader.GetString(1);
                    txtnamestaff.Text = name;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

       
        private void txtidproduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            strconn = new SqlConnection(chuoi);
            string sql = "Select * from t_hangnhap where id_sanpham ='" + txtidproduct.Text + "';";
            SqlCommand cmd = new SqlCommand(sql, strconn);
            SqlDataReader myreader;
            try
            {
                strconn.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {

                    String price = myreader.GetInt32(6).ToString();
                    txtprice.Text = price;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void add_Click(object sender, EventArgs e)
        {
           
        }

        private void addcart_Click(object sender, EventArgs e)
        {
            strconn = new SqlConnection(chuoi);
            strconn.Open();
            string id = txtinvoiceid.Text;
            string id1 = txtidproduct.Text;
            string amount = txtamount.Text;
            string price = txtprice.Text;
           


            if (id1 == "")
            {
                MessageBox.Show("Chưa chọn mã sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtidproduct.Focus();
            }

            else
            {
                string SQL_them = "Insert into t_hoadonchitiet values(N'" + id+ "',N'" + id1 + "',N'" + amount + "',N'" + price + "')";
                SqlCommand cmd = new SqlCommand(SQL_them, strconn);
                cmd.ExecuteNonQuery();
                Hienthi_DGV();
            }

            strconn.Close();

        }

        private void Hienthi_DGV()
        { 
            strconn = new SqlConnection(chuoi);
            strconn.Open();

            DataTable DT = new DataTable();
            string SQL_hienthi = "Select * from t_hoadonchitiet";
            SqlDataAdapter Mydata = new SqlDataAdapter(SQL_hienthi, strconn);

            Mydata.Fill(DT);
            DGV_INVOICE.DataSource = DT;


            strconn.Close();
        }
            private void add_Click_1(object sender, EventArgs e)
        {

        }

        private void txtamount_TextChanged(object sender, EventArgs e)
        {
            double tt, sl, dg;
            if (txtamount.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtamount.Text);
            if (txtprice.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtprice.Text);
            tt = sl * dg ;
            txttotalprice.Text = tt.ToString();
        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }
    }
}

