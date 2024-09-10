using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.Configuration;

namespace Project
{
    public partial class DuluxAccessories : Form
    {
        SqlCommand cmd;
        SqlConnection cn;
        public DuluxAccessories()
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
                    string qry = "insert into DuluxAccessories (Name,Quantity,Size,Cost,Stock)values(@Name,@Quantity,@Size,@Cost,@Stock)";
                    cmd = new SqlCommand(qry, cn);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    if (cmbQty.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@Quantity", DBNull.Value);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Quantity", cmbQty.SelectedItem.ToString());

                    }

                    if (cmbSize.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@Size", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Size", cmbSize.SelectedItem.ToString());

                    }
                    cmd.Parameters.AddWithValue("@Stock", txtStock.Text);
                    cmd.Parameters.AddWithValue("@Cost", txtCost.Text);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("New Record Saved", "ATTENTION", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    Clearall();
                    Fillgrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
            this.Hide();
        }

        private void Clearall()
        {
            lblCode.Text="";
            txtName.Text="";
            cmbQty.Text="";
            cmbSize.Text="";
            txtStock.Text="";
            txtCost.Text="";
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            Clearall();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
                string qry = "update DuluxAccessories set Name=@Name,Quantity=@Quantity,Size=@Size,Cost=@Cost,Stock=@Stock where Code=@Code";
                cmd = new SqlCommand(qry, cn);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                if (cmbQty.Text == "")
                {
                    cmd.Parameters.AddWithValue("@Quantity", DBNull.Value);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Quantity", cmbQty.SelectedItem.ToString());

                }
                if (cmbSize.Text == "")
                {
                    cmd.Parameters.AddWithValue("@Size", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Size", cmbSize.SelectedItem.ToString());

                }
                cmd.Parameters.AddWithValue("@Stock", txtStock.Text);
                cmd.Parameters.AddWithValue("@Cost", txtCost.Text);
                cmd.Parameters.AddWithValue("@Code", lblCode.Text);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Record Updated", "ATTENTION", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                Clearall();
                Fillgrid();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Fillgrid()
        {
            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseconn"].ConnectionString);
                string qry = "select * from DuluxAccessories";
                cmd = new SqlCommand(qry, cn);
                DataTable dt = new DataTable();
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                dgvDA.DataSource = dt;
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvDA_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            lblCode.Text = dgvDA.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvDA.Rows[e.RowIndex].Cells[1].Value.ToString();
            cmbQty.Text = dgvDA.Rows[e.RowIndex].Cells[2].Value.ToString();
            cmbSize.Text = dgvDA.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtCost.Text = dgvDA.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtStock.Text = dgvDA.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void DuluxAccessories_Load(object sender, EventArgs e)
        {
            Fillgrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
                string qry = "delete from DuluxAccessories where Code=@Code";
                cmd = new SqlCommand(qry, cn);
                cmd.Parameters.AddWithValue("@Code", lblCode.Text);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Record Deleted", "ATTENTION", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                Clearall();
                Fillgrid();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool Validate()
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Enter Name", "ATTENTION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return true;
            }
            else if (txtCost.Text=="")
            {
                MessageBox.Show("Enter Cost", "ATTENTION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCost.Focus();
                return true;
            }
            else if(txtStock.Text=="")
            {
                MessageBox.Show("Enter Stock", "ATTENTION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStock.Focus();
                return true;
            }
            else return false;
        }
    }
}
