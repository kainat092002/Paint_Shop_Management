﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using System.Configuration;

namespace PaintShopMgt
{
    public partial class frmrptRecipt : Form
    {
        public frmrptRecipt()
        {
            InitializeComponent();
        }

        private void frmrptRecipt_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConn"].ConnectionString);
                string sql = "select * from InvoiceMaster InM INNER JOIN InvoiceDetails InD ON InM.InvoiceNo=InD.InvoiceNo where InM.InvoiceNo=@invno";
                SqlCommand cmd = new SqlCommand(sql, cn);
                //creating object of DataSet dscustomer and filling the DataSet using SQLDataAdapter
                dsProduct ds = new dsProduct();
                cmd.Parameters.AddWithValue("@invno", Invoice.InvoiceNo);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "DataTable1");

                //set Processing Mode of Report as Local
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                //set path of the Local report
                reportViewer1.LocalReport.ReportPath = @"../../rptRecipt.rdlc";
                //Providing DataSource for the Report
                ReportDataSource rds = new ReportDataSource("dsReport", ds.Tables[0]);
                //Add ReportDataSource
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
