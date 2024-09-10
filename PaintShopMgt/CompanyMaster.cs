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
    public partial class CompanyMaster : Form
    {
        SqlCommand cmd;
        SqlConnection cn;
        public CompanyMaster()
        {
            InitializeComponent();
        }

       

        public void Fillgrid()
        {
            cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
            string qry = "select * from CompanyMaster";
            cmd = new SqlCommand(qry, cn);
            DataTable dt = new DataTable();
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dgvCM.DataSource = dt;
            cn.Close();
        }
        private void dgvCM_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            lblsrno.Text = dgvCM.Rows[e.RowIndex].Cells[0].Value.ToString();
            cmbCmpName.Text = dgvCM.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtProduct.Text = dgvCM.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void CompanyMaster_Load(object sender, EventArgs e)
        {
            Fillgrid();
            timer1.Start();
            timer2.Start();
            label7.Text = DateTime.Now.ToLongTimeString();
            label8.Text = DateTime.Now.ToLongDateString();
        }

       

        private void Clearall()
        {
            lblsrno.Text = "";
           //cmbCmpName.SelectedText.;
            txtProduct.Text = "";
        }
        

        private void timer1_Tick(object sender, EventArgs e)
        {
            label7.Text = System.DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

       

       private bool Validate()
        {
           if(cmbCmpName.Text=="")
           {
               MessageBox.Show("Please Select Company Name", "ATTENTION", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
               txtProduct.Focus();
               return false;
           }
            else if (txtProduct.Text == "")
            {
                MessageBox.Show("Please Enter Product Name", "ATTENTION", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                txtProduct.Focus();
                return false;
            }
            else return true;
        }

       private void btnSave_Click_1(object sender, EventArgs e)
       {
           if (Validate() == true)
           {
               try
               {
                   cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
                   string qry = "insert into CompanyMaster(CompanyNames,Products)values(@CompanyNames,@Products)";
                   cmd = new SqlCommand(qry, cn);
                   cmd.Parameters.AddWithValue("@CompanyNames", cmbCmpName.Text);
                   cmd.Parameters.AddWithValue("@Products", txtProduct.Text);
                   cn.Open();
                   cmd.ExecuteNonQuery();
                   cn.Close();
                   MessageBox.Show("Record Saved");
                   Fillgrid();
                   Clearall();
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
                   string qry = "update CompanyMaster set CompanyNames=@CompanyNames,Products=@Products where SrNo=@SrNo";
                   cmd = new SqlCommand(qry, cn);
                   cmd.Parameters.AddWithValue("@CompanyNames", cmbCmpName.Text);
                   cmd.Parameters.AddWithValue("@Products", txtProduct.Text);
                   cmd.Parameters.AddWithValue("@SrNo", lblsrno.Text);
                   cn.Open();
                   cmd.ExecuteNonQuery();
                   cn.Close();
                   MessageBox.Show("Record Updated");
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
                   string qry = "delete from CompanyMaster where SrNo=@SrNo";
                   cmd = new SqlCommand(qry, cn);
                   cmd.Parameters.AddWithValue("@SrNo", lblsrno);
                   cn.Open();
                   cmd.ExecuteNonQuery();
                   cn.Close();
                   MessageBox.Show("Record Deleted");
                   Clearall();
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
          // Home h = new Home();
           //h.Show();
           this.Hide();
       }

       private void button1_Click(object sender, EventArgs e)
       {
           cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
           string qry = "select * from CompanyMaster where CompanyNames=@Cname";
           cmd = new SqlCommand(qry, cn);
           cmd.Parameters.AddWithValue("@Cname", textBox1.Text);
           DataTable dt = new DataTable();
           cn.Open();
           SqlDataReader dr = cmd.ExecuteReader();
           dt.Load(dr);
           dgvCM.DataSource = dt;
           cn.Close();
       }
        
    }
}
