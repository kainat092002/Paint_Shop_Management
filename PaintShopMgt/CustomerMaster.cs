using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace PaintShopMgt
{
    public partial class CustomerMaster : Form
    {
        SqlCommand cmd;
        SqlConnection cn;
        public CustomerMaster()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validate() == true)
            {
                try
                {
                    cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
                    string qry = "insert into CustomerMaster (Name,ContactNo,Address)values(@Name,@ContactNo,@Address)";
                    cmd = new SqlCommand(qry, cn);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@ContactNo", txtCntNo.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAdd.Text);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("New Record Saved", "ATTENTION", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    Fillgrid();
                    Clearall();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void Fillgrid()
        {
            cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
            string qry = "select *  from CustomerMaster";
            cmd = new SqlCommand(qry, cn);
            DataTable dt = new DataTable();
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dgvCD.DataSource = dt;
            cn.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Validate() == true)
            {
                try
                {
                    cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
                    string qry = "update CustomerMaster set Name=@Name,ContactNo=@ContactNo,Address=@Address where ID=@ID ";
                    cmd = new SqlCommand(qry, cn);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@ContactNo", txtCntNo.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAdd.Text);
                    cmd.Parameters.AddWithValue("@ID", lblID.Text);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record Updated!!", "ATTENTION", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    Fillgrid();
                    Clearall();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dgvCD_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            lblID.Text = dgvCD.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvCD.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtCntNo.Text = dgvCD.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtAdd.Text = dgvCD.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Validate() == true)
            {
                try
                {
                    cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
                    string qry = "delete from CustomerMaster where ID=@ID";
                    cmd = new SqlCommand(qry, cn);
                    cmd.Parameters.AddWithValue("@ID", lblID.Text);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record Deleted", "ATTENTION", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    Clearall();
                    Fillgrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private bool Validate()
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Please Enter Name", "ATTENTION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }
            else if (txtCntNo.Text == "")
            {
                MessageBox.Show("Please Enter Contact Number", "ATTENTION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCntNo.Focus();
                return false;
            }
            else if (txtAdd.Text == "")
            {
                MessageBox.Show("Please Enter Address", "ATTENTION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAdd.Focus();
                return false;
            }
            return true;
        }

        private void Clearall()
        {
            lblID.Text = "";
            txtName.Text = "";
            txtCntNo.Text = "";
            txtAdd.Text = "";
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            Clearall();
        }

        private void txtCntNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
                e.Handled = true;
        }

        private void CustomerMaster_Load(object sender, EventArgs e)
        {
            Fillgrid();
            timer1.Start();
            timer2.Start();
            label7.Text = DateTime.Now.ToLongTimeString();
            label8.Text = DateTime.Now.ToLongDateString();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Home h = new Home();
            //h.Show();
            this.Hide();
        }

        private void txtCntNo_SystemColorsChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
            string qry = "select * from CustomerMaster where Name=@name";
            cmd = new SqlCommand(qry, cn);
            cmd.Parameters.AddWithValue("@name", textBox1.Text);
            DataTable dt = new DataTable();
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dgvCD.DataSource = dt;
            cn.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label7.Text = System.DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

    }
}
