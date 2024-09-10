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
        
    public partial class ProductMaster : Form
    {
        SqlCommand cmd;
        SqlConnection cn;
        public ProductMaster()
        {
            InitializeComponent();
        }

        public string GetAssurance()
        {
            string a = "";
            if (rdbYes.Checked)
                a = rdbYes.Text;
            else if (rdbNo.Checked)
                a = rdbNo.Text;
            return a;
        }
        public string GetToken()
        {
            string t = "";
            if (rdbAvlb.Checked)
                t = rdbAvlb.Text;
            else if (rdbNAvlb.Checked)
                t = rdbNAvlb.Text;
            return t;
        }

        private void ProductMaster_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
            lblDate.Text = DateTime.Now.ToLongDateString();
            lblTime.Text = DateTime.Now.ToLongDateString();
            Fillgrid();
            FillCmpName();

        }

        public void FillCmpName()
        {
            cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
            string qry = "select *  from CompanyMaster";
            cmd = new SqlCommand(qry, cn);
            DataTable dt = new DataTable();
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dgvCmp.DataSource = dt;
            cn.Close();
        }

        private void Fillgrid()
        {
            cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
            string qry = "select *  from ProductMaster";
            cmd = new SqlCommand(qry, cn);
            DataTable dt = new DataTable();
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dgvProduct.DataSource = dt;
            cn.Close();
        }
        private void dgvProduct_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            lblCode.Text = dgvProduct.Rows[e.RowIndex].Cells[0].Value.ToString();
            cmbCmp.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtQty.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
            cmbSize.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
            cmbType.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtCost.Text = dgvProduct.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtDur.Text = dgvProduct.Rows[e.RowIndex].Cells[7].Value.ToString();
            string a = dgvProduct.Rows[e.RowIndex].Cells[8].Value.ToString();
            if (a == "Yes")
                rdbYes.Checked = true;
            else if (a == "No")
                rdbNo.Checked = true;
            txtStock.Text = dgvProduct.Rows[e.RowIndex].Cells[9].Value.ToString();
            string t = dgvProduct.Rows[e.RowIndex].Cells[10].Value.ToString();
            if (t == "Available")
                rdbAvlb.Checked = true;
            else if (t == "Not Available")
                rdbNAvlb.Checked = true;
        }

        private void dgvCmp_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            cmbCmp.Text = dgvCmp.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtName.Text = dgvCmp.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void Clearall()
        {
            lblCode.Text = "";          
            txtName.Text = "";
            txtQty.Text = "";
            txtCost.Text = "";
            txtDur.Text = "";
            rdbYes.Checked = false;
            rdbNo.Checked = false;
            txtStock.Text = "";
            rdbAvlb.Checked = false;
            rdbNAvlb.Checked = false;

        }
        

        private void cmbCmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
            string qry = "select *  from CompanyMaster where CompanyNames=@cmpname";
            cmd = new SqlCommand(qry, cn);
            cmd.Parameters.AddWithValue("@cmpname", cmbCmp.SelectedItem.ToString());
            DataTable dt = new DataTable();
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dgvCmp.DataSource = dt;
            cn.Close();
        }
        private void cmbCmp_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
                string qry = "select *  from ProductMaster where Company=@cmp";
                cmd = new SqlCommand(qry, cn);
                cmd.Parameters.AddWithValue("@cmp", cmbCmp.SelectedItem.ToString());
                DataTable dt = new DataTable();
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                dgvProduct.DataSource = dt;
                cn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            lblTime.Text = System.DateTime.Now.ToLongTimeString();
            timer1.Start();
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
            string qry = "select * from ProductMaster where Company=@Cname";
            cmd = new SqlCommand(qry, cn);
            cmd.Parameters.AddWithValue("@Cname", textBox2.Text);
            DataTable dt = new DataTable();
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dgvProduct.DataSource = dt;
            cn.Close();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (Validate() == true)
            {
                try
                {
                    cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
                    string qry = "insert into ProductMaster (Company,Name,Quantity,Size,Type,Cost,Durability,Assurance,Stock,Token)values(@Company,@Name,@Quantity,@Size,@Type,@Cost,@Durability,@Assurance,@Stock,@Token)";
                    cmd = new SqlCommand(qry, cn);
                    cmd.Parameters.AddWithValue("@Company", cmbCmp.Text);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    if (txtQty.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@Quantity", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Quantity", txtQty.Text);
                    }
                    if (cmbSize.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@Size", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Size", cmbSize.Text);
                    }
                    if (cmbType.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@Type", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Type", cmbType.Text);
                    }
                    cmd.Parameters.AddWithValue("@Cost", txtCost.Text);
                    if (txtDur.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@Durability", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Durability", txtDur.Text);
                    }
                    cmd.Parameters.AddWithValue("@Assurance", GetAssurance());
                    cmd.Parameters.AddWithValue("@Stock", txtStock.Text);

                    cmd.Parameters.AddWithValue("@Token", GetToken());
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("New Record Saved");
                    Clearall();
                    Fillgrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            if (Validate() == true)
            {
                try
                {
                    cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
                    String qry = "update ProductMaster set Quantity=@Quantity,Size=@Size,Type=@Type,Cost=@Cost,Durability=@Durability,Assurance=@Assurance,Stock=@Stock,Token=@Token where Code=@Code";
                    cmd = new SqlCommand(qry, cn);
                    cmd.Parameters.AddWithValue("@Quantity", txtQty.Text);
                    cmd.Parameters.AddWithValue("@Size", cmbSize.Text);
                    cmd.Parameters.AddWithValue("@Type", cmbType.Text);
                    cmd.Parameters.AddWithValue("@Cost", txtCost.Text);
                    cmd.Parameters.AddWithValue("@Durability", txtDur.Text);
                    cmd.Parameters.AddWithValue("@Assurance", GetAssurance());
                    cmd.Parameters.AddWithValue("@Stock", txtStock.Text);
                    cmd.Parameters.AddWithValue("@Token", GetToken());
                    cmd.Parameters.AddWithValue("@Code", lblCode.Text);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record Updated", "ATTENTION", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    Clearall();
                    Fillgrid();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (Validate() == true)
            {
                try
                {
                    cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
                    string qry = "delete from ProductMaster where Code=@Code";
                    cmd = new SqlCommand(qry, cn);
                    cmd.Parameters.AddWithValue("@Code", lblCode.Text);
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

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            Clearall();
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }

        private bool Validate()
        {
            if(cmbCmp.Text=="")
            {
                MessageBox.Show("Please Select Company Name", "ATTENTION", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                cmbCmp.Focus();
                return false;
            }
            else if(txtName.Text=="")
            {
                MessageBox.Show("Please Enter Product Name", "ATTENTION", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }
            else if(txtCost.Text=="")
            {
                MessageBox.Show("Please Enter Cost", "ATTENTION", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                txtCost.Focus();
                return false;
            }
            else if(txtStock.Text=="")
            {
                MessageBox.Show("Please Enter Stock", "ATTENTION", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                txtStock.Focus();
                return false;
            }
            return true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
