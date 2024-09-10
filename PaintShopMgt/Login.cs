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
    public partial class Login : Form
    {
        SqlCommand cmd;
        SqlConnection cn;
        public Login()
        {
            InitializeComponent();
        }

        
        private void Login_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
            label7.Text = DateTime.Now.ToLongTimeString();
            label8.Text = DateTime.Now.ToLongDateString();
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                txtPW.UseSystemPasswordChar = false;
            }
            else
            {
                txtPW.UseSystemPasswordChar = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            label4.Location = new Point(label4.Location.X + 5, label4.Location.Y);
            if (label4.Location.X > this.Width)
            {
                label4.Location = new Point(0 - label4.Width, label4.Location.Y);
            } 
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label7.Text = System.DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void btnLog_Click_1(object sender, EventArgs e)
        {
            if (Validate1() == true)
            {
                try
                {
                    cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
                    string qry = "select * from Admin where Username=@u and Password=@p";
                    cmd = new SqlCommand(qry, cn);
                    cmd.Parameters.AddWithValue("@u", txtUName.Text);
                    cmd.Parameters.AddWithValue("@p", txtPW.Text);
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        MessageBox.Show("Login Successful !!", "ATTENTION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Home h = new Home();
                        h.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Login Unsuccessful ! Try Again !!", "ATTENTION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtUName.SelectAll();
                        txtUName.Focus();
                        cn.Close();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnCan_Click_1(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void checkBox1_CheckedChanged_2(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPW.UseSystemPasswordChar = false;
            }
            else
            {
                txtPW.UseSystemPasswordChar = true;
            }
        }

        public bool Validate1()
        {
            if (txtUName.Text == "")
            {
                MessageBox.Show("Please Enter Valid Username", "ATTENTION", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                txtUName.Focus();
                return false;
            }
            else if (txtPW.Text == "")
            {
                MessageBox.Show("Please Enter Valid Password", "Attenstion", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                txtPW.Focus();
                return false;
            }
            return true;
        }
    }
}
