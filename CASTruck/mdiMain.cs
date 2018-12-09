using CASTruck.forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CASTruck
{
    public partial class mdiMain : Form
    {
        public mdiMain()
        {
            InitializeComponent();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void pesoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCaptureWeight frmWork = new frmCaptureWeight() { MdiParent = this };
            frmWork.strNameMenu = "pesoToolStripMenuItem";
            pesoToolStripMenuItem.Enabled = false;
            frmWork.Show();
        }

        private void mdiMain_Load(object sender, EventArgs e)
        {
            this.Height = Screen.PrimaryScreen.Bounds.Height;
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Top = 0;
            this.Left = (int)((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2);
            CommonMethods.createStatusBar(this, statusStrip1);
           
            if (CommonMethods.verifyDatabase())
            {             
                initTables();

            }
           
        }

        private void initTables()
        {
            Constants.dbs.sql = "SELECT * FROM " + Constants.TABLE_CONFIGURATION;
            Constants.dtPermittedWeightsAndDimensions = Constants.dbs.getDataTable();    
        }

        private void baseDeDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConnectDatabase frmWork = new frmConnectDatabase() { MdiParent = this };
            frmWork.strNameMenu = "baseDeDatosToolStripMenuItem";
            baseDeDatosToolStripMenuItem.Enabled = false;
            frmWork.Show();
        }

        private void hardwareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConfigurationHardware frmWork = new frmConfigurationHardware() { MdiParent = this };
            frmWork.strNameMenu = "hardwareToolStripMenuItem";
            hardwareToolStripMenuItem.Enabled = false;
            frmWork.Show();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUser frmWork = new frmUser() { MdiParent = this };
            frmWork.strNameButton = "usuariosToolStripMenuItem";
            usuariosToolStripMenuItem.Enabled = false;
            frmWork.Show();
        }

        private void ingresarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin frmWork = new frmLogin();
            frmWork.strNameMenu = "ingresarToolStripMenuItem";
            ingresarToolStripMenuItem.Enabled = false;
            frmWork.ShowDialog();
        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.configuraciónToolStripMenuItem.Visible = false;
            this.registroToolStripMenuItem.Visible = false;
            this.reportesToolStripMenuItem.Visible = false;
            Constants.USERID_NOW = string.Empty;
            Constants.NAMES_NOW = string.Empty;
            Constants.SURNAMES_NOW = string.Empty;
            CommonMethods.closeForm("frmCaptureWeight");
        }

        private void pesajeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReport frmWork = new frmReport();
            frmWork.strNameMenu = "pesajeToolStripMenuItem";
            pesajeToolStripMenuItem.Enabled = false;
            frmWork.ShowDialog();
        }
    }
}
