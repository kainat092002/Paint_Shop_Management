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
    public partial class Login : Form
    {
        SqlCommand cmd;
        SqlConnection cn;
        public Login()
        {
            InitializeComponent();
        }

        private void btnLog_Click(object sender, EventArgs e)
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
                        MessageBox.Show("Login Successful !!","ATTENTION",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        Home h = new Home();
                        h.Show(); 
                        cn.Close();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Login Unsuccessful ! Try Again !!","ATTENTION",MessageBoxButtons.OK,MessageBoxIcon.Information);
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

        private void btnCan_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public bool Validate1()
        {
            if(txtUName.Text=="")
            {
                MessageBox.Show("Enter Valid Username", "ATTENTION", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                txtUName.Focus();
                return false;
            }
            else if(txtPW.Text=="")
            {
                MessageBox.Show("Enter Valid Password", "Attenstion", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                txtPW.Focus();
                return false;
            }
            return true;
        }

       

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
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
            label7.Location = new Point(label7.Location.X + 5, label7.Location.Y);
            if (label7.Location.X > this.Width)
            {
                label7.Location = new Point(0 - label7.Width, label7.Location.Y);
            } 
        }

        private void Login_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
