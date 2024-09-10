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
    public partial class Supplier : Form
    {
        SqlConnection cn;
        SqlCommand cmd;
        public Supplier()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
                string qry = "insert into SupplierMaster (Name,ContactNo,Address,Email)values(@Name,@ContactNo,@Address,@Email)";
                cmd = new SqlCommand(qry, cn);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@ContactNo", txtCntNo.Text);
                cmd.Parameters.AddWithValue("@Address", txtAdd.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
                {
                    cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
                    string qry = "update SupplierMaster  set Name=@Name,ContactNo=@ContactNo,Address=@Address,Email=@Email where ID=@ID ";
                    cmd = new SqlCommand(qry, cn);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@ContactNo", txtCntNo.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAdd.Text);
                    cmd.Parameters.AddWithValue("@Email",txtEmail.Text);
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
             try
                {
                    cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
                    string qry = "delete from SupplierMaster where ID=@ID";
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

        private void Clearall()
        {
            lblID.Text = "";
            txtName.Text = "";
            txtCntNo.Text = "";
            txtAdd.Text = "";
            txtEmail.Text = "";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clearall();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Fillgrid()
        {
            cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
            string qry = "select *  from SupplierMaster";
            cmd = new SqlCommand(qry, cn);
            DataTable dt = new DataTable();
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dgvSD.DataSource = dt;
            cn.Close();
        }

        private void Supplier_Load(object sender, EventArgs e)
        {
            Fillgrid();
            timer1.Start();
            timer2.Start();
            label7.Text = DateTime.Now.ToLongTimeString();
            label8.Text = DateTime.Now.ToLongDateString();
        }

        private void dgvCD_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            lblID.Text = dgvSD.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvSD.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtCntNo.Text = dgvSD.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtAdd.Text = dgvSD.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtEmail.Text = dgvSD.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
            string qry = "select * from SupplierMaster where Name=@name";
            cmd = new SqlCommand(qry, cn);
            cmd.Parameters.AddWithValue("@name", textBox1.Text);
            DataTable dt = new DataTable();
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dgvSD.DataSource = dt;
            cn.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label7.Text = System.DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        
    }
}
