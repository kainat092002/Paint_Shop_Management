using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaintShopMgt
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void companyMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CompanyMaster cm = new CompanyMaster();
            cm.MdiParent = this;
            cm.Show();
            //this.Hide();
        }

        private void productDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductMaster pm = new ProductMaster();
            pm.MdiParent = this;
            pm.Show();
            //this.Hide();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerMaster cm = new CustomerMaster();
            cm.MdiParent = this;
            cm.Show();
            //this.Hide();
        }

        private void invoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Invoice i = new Invoice();
            i.MdiParent = this;
            i.Show();
            //this.Hide();
        }

        private void customerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmrptCustomer cust = new frmrptCustomer();
            cust.MdiParent = this;
            cust.Show();
            //this.Hide();
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmrptProduct pro = new frmrptProduct();
            pro.MdiParent = this;
            pro.Show();
            //this.Hide();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            
        }

        private void invoiceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmrptRecipt r = new frmrptRecipt();
            r.MdiParent = this;
            r.Show();
        }

        private void supplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Supplier s = new Supplier();
            s.MdiParent = this;
            s.Show();
        }

        private void companyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmrptCompany cmp = new frmrptCompany();
            cmp.MdiParent = this;
            cmp.Show();
        }

       
    }
}
