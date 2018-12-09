using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CASTruck.forms
{
    public partial class frmLogin : Form
    {
        public string strNameMenu;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.PerformAutoScale();
            this.Top = (int)((Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            this.Left = (int)((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2);
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void enabledMenuOperator()
        {
            Form frm = new Form();
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "mdiMain")
                    frm = f;
            }
            foreach (ToolStripMenuItem mnu in frm.MainMenuStrip.Items)
            {
                if (mnu.Name == "registroToolStripMenuItem")
                    mnu.Visible = true;
               
            }

            foreach (ToolStripMenuItem mnu in frm.MainMenuStrip.Items)
            {
                foreach (ToolStripDropDownItem smnu in mnu.DropDownItems)
                {
                   
                    if (smnu.Name == "usuariosToolStripMenuItem")
                        smnu.Visible = false;
                    if (smnu.Name == "configuraciónToolStripMenuItem")
                        smnu.Visible = false;
                }
            }
        }
        private void enabledMenuAdministrator()
        {
            Form frm = new Form();
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "mdiMain")
                    frm = f;
            }
            foreach (ToolStripMenuItem mnu in frm.MainMenuStrip.Items)
            {
                if (mnu.Name == "registroToolStripMenuItem")
                    mnu.Visible = true;
                if (mnu.Name == "reportesToolStripMenuItem")
                    mnu.Visible = true;
            }

            foreach (ToolStripMenuItem mnu in frm.MainMenuStrip.Items)
            {
                foreach (ToolStripDropDownItem smnu in mnu.DropDownItems)
                {
                    // foreach (ToolStripDropDownItem semnu in smnu.DropDownItems)
                    if (smnu.Name == "configuraciónToolStripMenuItem")
                        smnu.Visible = true;
                    if (smnu.Name == "usuariosToolStripMenuItem")
                        smnu.Visible = true;

                }
            }
        }
        private bool verifySuperAdministrator()
        {
            
            if("gM7W1MjS95Hh58VAOnzvaw==" == Constants.cu.encript(txtPASSWORD.Text) &&
                txtUSERNAME.Text == "admin")
            {
                enabledMenuAdministrator();
                return true;
            }
            return false;       
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUSERNAME.Text) && !string.IsNullOrEmpty(txtPASSWORD.Text))
            {
                if (!verifySuperAdministrator())
                {
                    string pass = Constants.cu.encript(txtPASSWORD.Text.Trim());
                    Constants.dbs.sql = string.Format("SELECT USERID, NAMES, SURNAMES, USERTYPEID FROM {0} WHERE USERNAME = '{1}' AND PASSWORD = '{2}'",
                        Constants.TABLE_USERS, txtUSERNAME.Text, pass);
                    DataTable dt = Constants.dbs.getDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        Constants.USERID_NOW = Convert.ToString(dr["USERID"]);
                        Constants.NAMES_NOW = Convert.ToString(dr["NAMES"]);
                        Constants.SURNAMES_NOW = Convert.ToString(dr["SURNAMES"]);
                        int type = Convert.ToInt16(dr["USERTYPEID"]);
                        switch (type)
                        {
                            case 1:
                                enabledMenuAdministrator();
                                break;
                            case 2:
                                enabledMenuOperator();
                                break;
                        }
                        CommonMethods.setLogDatabase("Acceso de usuario");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("El usuario no existe.", "Resultado");
                    }

                }
                else
                {
                    Constants.USERID_NOW = "0";
                    Constants.NAMES_NOW = string.Empty;
                    Constants.SURNAMES_NOW = string.Empty;
                    this.Close();
                }

            }

        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            CommonMethods.enabledOptionMenu(strNameMenu);

        }
    }
}
