using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using Ionic.Zip;

namespace CASTruck.forms
{
    public partial class frmReport : Form
    {
        public string strNameMenu;
    
     
        public frmReport()
        {
            InitializeComponent();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;
            
        }
        private void getSqlDate()
        {
            string msg = string.Empty;
            string sql = string.Empty;
            string conditions = "WHERE WEIGHING.DATEREGISTER >= '" + string.Format("{0:yyyyMMdd}", Convert.ToDateTime(dtpFrom.Value)) +
                               "' AND WEIGHING.DATEREGISTER <= '" + string.Format("{0:yyyyMMdd}", Convert.ToDateTime(dtpTo.Value).AddDays(1)) + "'";

           
            sql = "SELECT WEIGHING.WEIGHINGID AS BOLETA, USERS.USERNAME AS OPERADOR, " +
                    "WEIGHING.DATEREGISTER AS FECHA, WEIGHING.VEHICLEID AS TIPO, WEIGHING.PLATEVEHICLE AS PLACA, WEIGHING.DRIVERNAME AS CONDUCTOR, " +
                    "WEIGHING.LICENSE AS LICENCIA, WEIGHING.WEIGHT1 AS PESO_EJE_1, WEIGHING.WEIGHT2 AS PESO_EJE_2, " +
                    "WEIGHING.WEIGHT3 AS PESO_EJE_3, WEIGHING.WEIGHT4 AS PESO_EJE_4, " +
                    "WEIGHING.WEIGHTBRUTO AS PESO_BRUTO, " +
                    "WEIGHING.WEIGHTTARA AS TARA, WEIGHING.WEIGHTNETO AS NETO, WEIGHING.INSIDE AS INGRESO, WEIGHING.OUTSIDE AS SALIDA, " +
                    "WEIGHING.SOURCE AS ORIGEN, WEIGHING.TARGET AS DESTINO, WEIGHING.PROVIDER AS PROVEEDOR, WEIGHING.PRODUCT AS PRODUCTO, WEIGHING.CLIENT AS CLIENTE " +
                    "FROM WEIGHING INNER JOIN USERS ON USERS.USERID = WEIGHING.USERID ";
            msg = "Consulta de pesos";

            
          
           
            Constants.dbs.sql = sql + conditions;

            DataTable dt = Constants.dbs.getDataTable();
            CommonMethods.setLogDatabase(msg);
            if (dt.Rows.Count > 0)
            {
                dgvReport.DataSource = dt;
            }
            else
            {
                dgvReport.DataSource = null;
                if (dgvReport.Rows.Count > 0)
                {
                    dgvReport.Rows.Clear();
                }
               
            }
        }

        private void cmdFind_Click(object sender, EventArgs e)
        {
            getSqlDate();
        }

        private void cmdExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "export.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                
                ToCsV(dgvReport, sfd.FileName); 
            }

        }

        private void ToCsV(DataGridView dGV, string filename)
        {
            string stOutput = "";
            // Export titles:
            string sHeaders = "";

            for (int j = 0; j < dGV.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
            stOutput += sHeaders + "\r\n";
            // Export data.
            for (int i = 0; i < dGV.RowCount - 1; i++)
            {
                string stLine = "";
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                stOutput += stLine + "\r\n";
            }
            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(output, 0, output.Length); //write the encoded file
            bw.Flush();
            bw.Close();
            fs.Close();
        }

        private void frmReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            CommonMethods.enabledOptionMenu(strNameMenu);
        }

        private void cmdJson_Click(object sender, EventArgs e)
        {

        }
    }
}
