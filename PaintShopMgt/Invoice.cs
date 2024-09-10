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

namespace PaintShopMgt
{
    public partial class Invoice : Form
    {
        SqlCommand cmd;
        SqlConnection cn;
        public int amt = 0;
        static public int InvoiceNo;
        public Invoice()
        {
            InitializeComponent();
        }

        private void Invoice_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
            lblTime.Text = DateTime.Now.ToLongTimeString();
            lblDate.Text = DateTime.Now.ToLongDateString();
            //label15.Text = System.DateTime.Now.ToLongDateString();
            FillCustomer();
            SelectName();
            FillProduct();
            SelsctCmpName();
            PurchaseGrid();
            InvoiceId();
            //CalculateBill();
        }

        private void FillCustomer()
        {
            cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
            string qry = "select ID,Name,ContactNo from CustomerMaster order by ID";
            cmd = new SqlCommand(qry, cn);
            DataTable dt = new DataTable();
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dgvCustomer.DataSource = dt;
            cn.Close();
        }
        private void SelectName()
        {
            cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
            string qry = "select Name from CustomerMaster";
            cmd = new SqlCommand(qry, cn);
            DataTable dt = new DataTable();
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                cmbCustName.Items.Add(dr[0]);
            }
            cn.Close();
        }

        private void cmbCustName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
            string qry = "select ID,Name,ContactNo from CustomerMaster where Name=@Name";
            cmd = new SqlCommand(qry, cn);
            cmd.Parameters.AddWithValue("@Name", cmbCustName.SelectedItem.ToString());
            DataTable dt = new DataTable();
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dgvCustomer.DataSource = dt;
        }

        private void dgvCustomer_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            lblID.Text = dgvCustomer.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtCustName.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtCustNo.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void FillProduct()
        {
            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
                string qry = "select Code,Company,PName,Quantity,Size,Cost from ProductMaster order by Code";
                cmd = new SqlCommand(qry, cn);
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
       
           public void SelsctCmpName()
        {
            cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
            string qry = "select distinct CompanyNames from CompanyMaster";
            cmd = new SqlCommand(qry, cn);
            DataTable dt = new DataTable();
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbCmpName.Items.Add(dr[0]);
            }
            cn.Close();
        }
        

        private void cmbCmpName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
                string qry = "select Code,Company,PName,Quantity,Size,Cost from ProductMaster where Company=@Company";
                cmd = new SqlCommand(qry, cn);
                cmd.Parameters.AddWithValue("@Company", cmbCmpName.SelectedItem.ToString());
                DataTable dt = new DataTable();
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                dgvProduct.DataSource = dt;
            }
            catch(Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }

        }

        private void dgvProduct_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            lblCode.Text = dgvProduct.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtCmp.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtProName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtSizeL.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtSizeI.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtProCost.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
          
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(Convert.ToInt32(txtQty.Text)>=0)
                {
                    int cost = Convert.ToInt32(txtProCost.Text);
                    int amt = cost * Convert.ToInt32(txtQty.Text);
                    txtAmt.Text = Convert.ToString(amt);
                }
            }
            catch (Exception)
            {
               // MessageBox.Show(ex.Message);
            }
        }
        private void CalculateBill()
        {
            int bill = Convert.ToInt32(txtAmt.Text);
            amt = amt + bill;
            txtTotal.Text = Convert.ToString(amt);
            double GST = amt * 0.18;
            txtGST.Text = Convert.ToString(GST);
            txtGrandTotal.Text = Convert.ToString(amt + GST);

        }
        

        private void InvoiceId()
        {
            cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
            string qry = "select max(InvoiceNo)+1 from InvoiceMaster";
            cmd = new SqlCommand(qry, cn);
            cn.Open();
            object obj = cmd.ExecuteScalar();
            if (obj == DBNull.Value)
                obj = 1;
            txtInNo.Text = Convert.ToString(obj);
            cn.Close();
        }

        private void PurchaseGrid()
        {
            cn=new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
            string qry = "select * from InvoiceTemp";
            cmd = new SqlCommand(qry, cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvPurchase.DataSource = dt;
            cn.Close();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (Validate() == true)
            {
                try
                {
                    cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
                    string qry = "insert into InvoiceTemp(InvoiceNo,Code,Company,PName,Cost,Size,Quantity,Amount)values(@InvoiceNo,@Code,@Company,@PName,@Cost,@Size,@Quantity,@Amount)";
                    cmd = new SqlCommand(qry, cn);
                    cmd.Parameters.AddWithValue("@InvoiceNo", txtInNo.Text);
                    cmd.Parameters.AddWithValue("@Code", lblCode.Text);
                    cmd.Parameters.AddWithValue("@Company", txtCmp.Text);
                    cmd.Parameters.AddWithValue("@PName", txtProName.Text);
                    cmd.Parameters.AddWithValue("@Cost", txtProCost.Text);
                    if (txtSizeL.Text=="")
                    {
                        cmd.Parameters.AddWithValue("@Size", txtSizeI.Text);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Size", txtSizeL.Text);
                    }
                    cmd.Parameters.AddWithValue("@Quantity", txtQty.Text);
                    cmd.Parameters.AddWithValue("@Amount", txtAmt.Text);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    CalculateBill();
                    PurchaseGrid();
                   // Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void clearSize()
        {
            txtSizeI.Text = "";
            txtSizeL.Text = "";
        }
        private void dgvPurchase_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                clearSize();
                txtInNo.Text = dgvPurchase.Rows[e.RowIndex].Cells[0].Value.ToString();
                lblCode.Text = dgvPurchase.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtCmp.Text = dgvPurchase.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtProName.Text = dgvPurchase.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtProCost.Text = dgvPurchase.Rows[e.RowIndex].Cells[4].Value.ToString();
                if (txtSizeI.Text == "")
                {
                    txtSizeI.Text = dgvPurchase.Rows[e.RowIndex].Cells[5].Value.ToString();
                }
                else if(txtSizeI.Text=="")
                {
                    txtSizeL.Text = dgvPurchase.Rows[e.RowIndex].Cells[5].Value.ToString();
                }
               // txtProCost.Text = dgvPurchase.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtQty.Text = dgvPurchase.Rows[e.RowIndex].Cells[6].Value.ToString();
                txtAmt.Text = dgvPurchase.Rows[e.RowIndex].Cells[7].Value.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {         
                try
                {
                    cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
                    string qry = "delete from InvoiceTemp where Code=@Code";
                    cmd = new SqlCommand(qry, cn);
                    cmd.Parameters.AddWithValue("@Code", lblCode.Text);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Product Cancelled", "ATTENTION", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    PurchaseGrid();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }           
        }

        private void Clear()
        {
            lblCode.Text = "";
            txtCmp.Text = "";
            txtProName.Text = "";
            txtSizeI.Text = "";
            txtProCost.Text = "";
            txtQty.Text = "";
            txtAmt.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Home h = new Home();
            //h.Show();
            this.Hide();
        }

        private void SaveDetails()
        {
            cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
            string sql = "insert into InvoiceDetails select * from InvoiceTemp";
            cmd = new SqlCommand(sql, cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        private void UpdateStock()
        {
            try
            { 
            cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
            string sql = "select * from InvoiceTemp";
            cmd = new SqlCommand(sql, cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            for (int k = 0; k < dt.Rows.Count; k++)
            {
                sql = "update ProductMaster set Stock=Stock-@Quantity where Code=@Code";
                cmd = new SqlCommand(sql, cn);
                int temp = Convert.ToInt32(dt.Rows[k]["Quantity"]);
                cmd.Parameters.AddWithValue("@Quantity", temp);
                cmd.Parameters.AddWithValue("@Code", dt.Rows[k][1]);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            }
            catch(Exception ex)
            {
              MessageBox.Show(ex.Message);
            }
        }

        private void DeleteTempInvoice()
        {
            cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
            string sql = "delete from InvoiceTemp";
            cmd = new SqlCommand(sql, cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        private void btnBill_Click(object sender, EventArgs e)
        {
            
                try
                {
                    cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
                    string sql = "insert into InvoiceMaster(InvoiceNo,ID,Name,ContactNo,DOP,GST,GrandTotal)values(@InvoiceNo,@ID,@Name,@ContactNo,@DOP,@GST,@GrandTotal)";
                    cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@InvoiceNo", txtInNo.Text);
                    cmd.Parameters.AddWithValue("@ID", lblID.Text);
                    cmd.Parameters.AddWithValue("@Name", txtCustName.Text);
                    cmd.Parameters.AddWithValue("@ContactNo", txtCustNo.Text);
                    cmd.Parameters.AddWithValue("@DOP", Convert.ToDateTime(lblDate.Text));
                    cmd.Parameters.AddWithValue("@GST", txtGST.Text);
                    cmd.Parameters.AddWithValue("@GrandTotal", txtGrandTotal.Text);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    SaveDetails();
                    UpdateStock();
                    DeleteTempInvoice();
                    this.Hide();
                    InvoiceNo = Convert.ToInt32(txtInNo.Text);

                    frmrptRecipt obj = new frmrptRecipt();
                    obj.Show();



                    //SaveDetails();
                    //MessageBox.Show("Records saved");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            
        }

        private bool Validate()
        {
            if(txtCustName.Text=="")
            {
                MessageBox.Show("Please Select Customer Name", "ATTENTION", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                txtCustName.Focus();
                return false;
            }
           else if(txtCmp.Text=="")
            {
                MessageBox.Show("Please Select Company Name", "ATTENTION", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                txtCmp.Focus();
                return false;
            }
            else if(txtQty.Text=="")
            {
                MessageBox.Show("Please Enter Quantity", "ATTENTION", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                txtQty.Focus();
                return false;
            }
            return true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            lblTime.Text = System.DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void txtAmt_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
