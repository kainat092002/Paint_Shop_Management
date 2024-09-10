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
    public partial class Accessories : Form
    {
        SqlCommand cmd;
        SqlConnection cn;
        public Accessories()
        {
            InitializeComponent();
        }

        private void Accessories_Load(object sender, EventArgs e)
        {

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
            this.Hide();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
                string qry = "insert into Accessories (Name,Quantity,Size,Cost,Stock)values(@Name,@Quantity,@Size,@Cost,@Stock)";
                cmd = new SqlCommand(qry, cn);
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
                    cmd.Parameters.AddWithValue("@Size", cmbSize.SelectedItem.ToString());

                }
                cmd.Parameters.AddWithValue("@Stock", txtStock.Text);
                cmd.Parameters.AddWithValue("@Cost", txtCost.Text);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("New Record Saved", "ATTENTION", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                //Clearall();
                // Fillgrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
