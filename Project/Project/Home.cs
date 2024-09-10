using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }
        private void xyzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DuluxPaints dp = new DuluxPaints();
            dp.Show();
            this.Hide();
        }

        

        private void paintsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NerolacPaints np = new NerolacPaints();
            np.Show();
            this.Hide();
        }

        private void paintsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SoldierPaints sp = new SoldierPaints();
            sp.Show();
            this.Hide();
        }

        

        private void customerDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerMaster cm = new CustomerMaster();
            cm.Show();
            this.Hide();
        }

        private void accesoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Accessories a = new Accessories();
            a.Show();
            this.Hide();
        }

       
    }
}
