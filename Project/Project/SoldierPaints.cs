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

namespace Project
{
    public partial class SoldierPaints : Form
    {
        SqlCommand cmd;
        SqlConnection cn;
        public SoldierPaints()
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
            if (rdbTokenA.Checked)
                t = rdbTokenA.Text;
            else if (rdbTokenNA.Checked)
                t = rdbTokenNA.Text;
            return t;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (validate() == true)
            {
                try
                {
                    cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
                    string qry = "insert into SoldierPaints(Name,Quantity,Cost,Type,Durability,Assurance,Stock,Token)values(@Name,@Quantity,@Cost,@Type,@Durability,@Assurance,@Stock,@Token)";
                    cmd = new SqlCommand(qry, cn);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Quantity", cmbQty.Text);
                    cmd.Parameters.AddWithValue("@Cost", txtCost.Text);
                    cmd.Parameters.AddWithValue("@Type", cmbType.Text);
                    cmd.Parameters.AddWithValue("@Durability", txtDur.Text);
                    cmd.Parameters.AddWithValue("@Assurance", GetAssurance());
                    cmd.Parameters.AddWithValue("@Stock", txtStock.Text);
                    cmd.Parameters.AddWithValue("@Token", GetToken());
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("New Record Saved", "ATTENTION", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    ClearAll();
                    Fillgrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void ClearAll()
        {
            lblCode.Text = "";
            txtName.Text = "";
            cmbQty.Text = "";
            txtCost.Text = "";
            cmbType.SelectedItem = "";
            txtDur.Text = "";
            rdbNo.Checked = false;
            rdbYes.Checked = false;
            txtStock.Text = "";
            rdbTokenA.Checked = false;
            rdbTokenNA.Checked = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (validate() == true)
            {
                try
                {
                    cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
                    string qry = "update SoldierPaints set Name=@Name,Quantity=@Quantity,Cost=@Cost,Type=@Type,Durability=@Durability,Assurance=@Assurance,Stock=@Stock,Token=@Token where Code=@Code";
                    cmd = new SqlCommand(qry, cn);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Quantity", cmbQty.Text);
                    cmd.Parameters.AddWithValue("@Cost", txtCost.Text);
                    cmd.Parameters.AddWithValue("@Type", cmbType.Text);
                    cmd.Parameters.AddWithValue("@Durability", txtDur.Text);
                    cmd.Parameters.AddWithValue("@Assurance", GetAssurance());
                    cmd.Parameters.AddWithValue("@Stock", txtStock.Text);
                    cmd.Parameters.AddWithValue("@Token", GetToken());
                    cmd.Parameters.AddWithValue("@Code", lblCode.Text);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record Updated", "ATTENTION", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    ClearAll();
                    Fillgrid();
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
            string qry = "select *  from SoldierPaints";
            cmd = new SqlCommand(qry, cn);
            DataTable dt = new DataTable();
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dgvSP.DataSource = dt;
            cn.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (validate() == true)
            {
                try
                {
                    cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
                    string qry = "delete from SoldierPaints where Code=@Code";
                    cmd = new SqlCommand(qry, cn);
                    cmd.Parameters.AddWithValue("@Code", lblCode.Text);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record Deleted", "ATTENTION", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    ClearAll();
                    Fillgrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void SoldierPaints_Load(object sender, EventArgs e)
        {
            Fillgrid();
        }

        private bool validate()
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Enter Name", "ATTENTION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }
            else if (cmbQty.Text == "")
            {
                MessageBox.Show("Select Quantity", "ATTENTION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbQty.Focus();
                return false;
            }
            else if (txtCost.Text == "")
            {
                MessageBox.Show("Enter Cost", "ATTENTION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCost.Focus();
                return false;
            }
            else if (cmbType.Text == "")
            {
                MessageBox.Show("Select Product Type", "ATTENTION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbType.Focus();
                return false;
            }
            else if (txtDur.Text == "")
            {
                MessageBox.Show("Enter Product Durability", "ATTENTION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDur.Focus();
                return false;
            }
            else if (rdbYes.Checked == false && rdbNo.Checked == false)
            {
                MessageBox.Show("Select Product Assurance", "ATTENTION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                rdbYes.Focus();
                return false;
            }
            else if (txtStock.Text == "")
            {
                MessageBox.Show("Enter Stcock", "ATTENTION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStock.Focus();
                return false;
            }
            else if (rdbTokenA.Checked == false && rdbTokenNA.Checked == false)
            {
                MessageBox.Show("Select Product Token", "ATTENTION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                rdbYes.Focus();
                return false;
            }

            return true;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
            this.Hide();
        }

        private void dgvSP_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            lblCode.Text = dgvSP.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvSP.Rows[e.RowIndex].Cells[1].Value.ToString();
            cmbQty.Text = dgvSP.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtCost.Text = dgvSP.Rows[e.RowIndex].Cells[3].Value.ToString();
            cmbType.Text = dgvSP.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtDur.Text = dgvSP.Rows[e.RowIndex].Cells[5].Value.ToString();
            string a = dgvSP.Rows[e.RowIndex].Cells[6].Value.ToString();
            if (a == "Yes")
                rdbYes.Checked = true;
            else if (a == "No")
                rdbNo.Checked = true;
            txtStock.Text = dgvSP.Rows[e.RowIndex].Cells[7].Value.ToString();
            string t = dgvSP.Rows[e.RowIndex].Cells[8].Value.ToString();
            if (t == "Available")
                rdbTokenA.Checked = true;
            else if (t == "Not Available")
                rdbTokenNA.Checked = true;
        }
    }
}
