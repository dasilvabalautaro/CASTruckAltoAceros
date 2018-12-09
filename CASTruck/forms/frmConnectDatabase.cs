using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace CASTruck.forms
{
    public partial class frmConnectDatabase : Form
    {
        
        List<string> instancesServer;
        public string strNameMenu;
        public frmConnectDatabase()
        {
            InitializeComponent();
        }

        private void frmConnectDatabase_Load(object sender, EventArgs e)
        {
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.PerformAutoScale();
            this.Top = 0;
            this.Left = (int)((Screen.PrimaryScreen.WorkingArea.Width - this.Width)/2);
            timer.Enabled = true;
            lblCount.Text = "0";
            backgroundWorker.RunWorkerAsync();
   
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            instancesServer = Constants.dbs.getInstancesSQLSERVER();
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Error == null){
                CommonMethods.setComboBox(cboServers, instancesServer);
                cboServers.Refresh();
                lblCount.Text = "Ok";
                timer.Enabled = false;
            }
            else{
                MessageBox.Show(e.Error.Message, "Verificar");
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                Int16 i = Convert.ToInt16(lblCount.Text);
                i += 1;
                lblCount.Text = Convert.ToString(i); 
            }
            catch (FormatException f)
            {
                MessageBox.Show(f.Message, "Verificar");
            }
            catch (OverflowException o)
            {
                MessageBox.Show(o.Message, "Verificar");
            }
          
        }

        private void cmdTest_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(cboServers.Text)){
                if (rbAuthenticationSQLServer.Checked == true)
                {
                    if (txtLogin.TextLength != 0 & txtPassword.TextLength != 0)
                    {
                        Constants.dbs.integratedSecurity = 0;
                        Constants.dbs.userID = txtLogin.Text.Trim();
                        Constants.dbs.password = txtPassword.Text.Trim();
                    }
                    else
                    {
                        MessageBox.Show("Datos de usuario incompletos.", "Verificar");
                        return;
                    }
                }
                else
                {
                    Constants.dbs.integratedSecurity = 1;
                    Constants.dbs.userID = "sa";
                }
                Constants.dbs.serverName = cboServers.Text;
                Constants.dbs.databaseName = "master";
                Constants.dbs.closeConnection();
                if (Constants.dbs.connectDatabase() == true)
                {
                    MessageBox.Show("Prueba correcta!!", "Exito");
                    cmdCrearDatabase.Enabled = true;
                    cmdConnectDatabase.Enabled = true;
                }
                else
                {
                    MessageBox.Show(Constants.dbs.messageError, "Verificar");
                }
            }
        }

        private void setRegistryWindows()
        {
            CommonMethods.setKeyValueRegistry(Constants.PATH_KEY, Constants.INTEGRATED_SECURITY_KEY, Constants.dbs.integratedSecurity);
            CommonMethods.setKeyValueRegistry(Constants.PATH_KEY, Constants.NAME_KEY_DATABASE, txtNameDatabase.Text.Trim());
            CommonMethods.setKeyValueRegistry(Constants.PATH_KEY, Constants.PASSWORD_KEY, Constants.dbs.password);
            CommonMethods.setKeyValueRegistry(Constants.PATH_KEY, Constants.SERVER_NAME_KEY, Constants.dbs.serverName);
            CommonMethods.setKeyValueRegistry(Constants.PATH_KEY, Constants.USER_ID_KEY, Constants.dbs.userID);
          
           
        }

        private void cmdCrearDatabase_Click(object sender, EventArgs e)
        {
            string msg = "Base de datos ha sido creada con éxito. Reiniciar el sistema.";
            string s = txtNameDatabase.Text;

            if (!string.IsNullOrEmpty(s))
            {
                Constants.dbs.sql = "CREATE DATABASE " + txtNameDatabase.Text.Trim();
                if (Constants.dbs.executeNonSQL(false))
                {
                    Constants.dbs.databaseName = txtNameDatabase.Text.Trim();
                    Constants.dbs.closeConnection();
                    if (Constants.dbs.connectDatabase() == true)
                    {
                        Constants.dbs.sql = CommonMethods.readTextFile(Application.StartupPath + 
                            "\\" + Constants.FOLDER_SQL + "\\" + Constants.CREATE_DATABASE_FILE);
                        if (Constants.dbs.executeNonSQL(false))
                        {
                            Constants.dbs.sql = Constants.CREATE_TABLES_PROCEDURE;
                            if (Constants.dbs.executeNonSQL(true))
                            {
                                Constants.dbs.sql = CommonMethods.readTextFile(Application.StartupPath + 
                                    "\\" + Constants.FOLDER_SQL + "\\" + Constants.INSERT_PARAMETERS_FILE);
                                if (Constants.dbs.executeNonSQL(false))
                                {
                                    Constants.dbs.sql = Constants.INSERT_PARAMETERS_PROCEDURE;
                                    if (Constants.dbs.executeNonSQL(true))
                                    {
                                        setRegistryWindows();
                                        //CommonMethods.setLogDatabase("Creación de base de datos");
                                        //Application.Restart();
                                    }
                                }
                            }
                        }
                    }

                }
                if (Constants.dbs.messageError.Length > 0)
                {
                    msg = Constants.dbs.messageError;

                }
                MessageBox.Show(msg, "Resultado");
            }
            else
            {
                MessageBox.Show("Defina el nombre de la base de datos.", "Verifique");
            }
        }
        
        private void cmdConnectDatabase_Click(object sender, EventArgs e)
        {
            Constants.dbs.databaseName = txtNameDatabase.Text.Trim();
            Constants.dbs.closeConnection();
            if (Constants.dbs.connectDatabase() == true)
            {
                setRegistryWindows();
                //CommonMethods.setLogDatabase("Conexión a base de datos");
                MessageBox.Show("Conexión exitosa. Reiniciar el sistema.", "Resultado");
                //Application.Restart();
            }
            else
            {
                MessageBox.Show(Constants.dbs.messageError, "Resultado");
            }
   
        }

        private void frmConnectDatabase_FormClosing(object sender, FormClosingEventArgs e)
        {

            CommonMethods.enabledOptionSubMenu(strNameMenu);

        }

        private void rbAuthenticationSQLServer_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void rbAuthenticationSQLServer_Click(object sender, EventArgs e)
        {
            if (rbAuthenticationSQLServer.Checked)
            {
                txtLogin.ReadOnly = false;
                txtPassword.ReadOnly = false;
            }
        }

        private void rbAuthenticationWindows_Click(object sender, EventArgs e)
        {
            if (rbAuthenticationWindows.Checked)
            {
                txtLogin.ReadOnly = true;
                txtPassword.ReadOnly = true;
            }
        }
    }
}
