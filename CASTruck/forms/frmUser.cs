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
    public partial class frmUser : Form
    {
        public string strNameButton;
        private bool flagView = true;
        private string changeUSERTYPEID;
        private string changeUSERNAME;
        private string changePASSWORD;
        private string changeNAMES;
        private string changeSURNAMES;

        public frmUser()
        {
            InitializeComponent();
        }

        private void frmUser_Load(object sender, EventArgs e)
        {
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.PerformAutoScale();
            this.Top = 0;
            this.Left = (int)((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2);
            initCrtls();

        }

        private void initCrtls()
        {
            if (Constants.dbs.isCnn())
            {
                DataTable dt;
                Constants.dbs.sql = "SELECT USERTYPEID, USERTYPENAME FROM " + Constants.TABLE_TYPE_USERS;
                dt = Constants.dbs.getDataTable();
                
                cboUSERTYPEID.DataSource = dt;
                cboUSERTYPEID.DisplayMember = "USERTYPENAME";
                cboUSERTYPEID.ValueMember = "USERTYPEID";
                cboUSERTYPEID.SelectedIndex = -1;
                lvwListUsers.Columns.Add("Usuario", 80, HorizontalAlignment.Left);
                lvwListUsers.Columns.Add("Nombres", 110, HorizontalAlignment.Left);
                lvwListUsers.Columns.Add("Apellidos", 110, HorizontalAlignment.Left);
                 
            }
            else
            {
                MessageBox.Show(Constants.dbs.messageError, "Verifique");
                flagView = false;
            }
        }
        private void frmUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            CommonMethods.enabledOptionMenu(strNameButton);
          
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUser_Shown(object sender, EventArgs e)
        {
            if (flagView == false)
                this.Close();
            else
                listUsers();
                    
        }

        private void listUsers()
        {
            Constants.dbs.sql = "SELECT USERID, USERNAME, NAMES, SURNAMES FROM " + Constants.TABLE_USERS;
            DataTable dt = Constants.dbs.getDataTable();
            lvwListUsers.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt32(dr["USERID"]) != 1)
                {
                    ListViewItem lvi = new ListViewItem(dr["USERNAME"].ToString());
                    lvi.SubItems.Add(dr["NAMES"].ToString());
                    lvi.SubItems.Add(dr["SURNAMES"].ToString());
                    lvi.Tag = dr["USERID"];
                    lvwListUsers.Items.Add(lvi);
                }
  
            }
        }
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            if (!string.IsNullOrEmpty(txtNAMES.Text.Trim()) &&
                !string.IsNullOrEmpty(txtPASSWORD.Text.Trim()) &&
                !string.IsNullOrEmpty(txtSURNAMES.Text.Trim()) &&
                !string.IsNullOrEmpty(txtUSERNAME.Text.Trim()) &&
                cboUSERTYPEID.SelectedIndex != -1)
            {
                if (Convert.ToInt16(cmdAdd.Tag) == 0)
                {
                    string pass = Constants.cu.encript(txtPASSWORD.Text.Trim());
                    Constants.dbs.sql = string.Format("INSERT INTO {0} (USERTYPEID,  USERNAME, PASSWORD, NAMES, SURNAMES) " +
                    "VALUES ({1}, '{2}', '{3}', '{4}', '{5}')", Constants.TABLE_USERS,
                    Convert.ToString(cboUSERTYPEID.SelectedValue), txtUSERNAME.Text.Trim(),
                    pass, txtNAMES.Text.Trim(), txtSURNAMES.Text.Trim());
                    msg = "Usuario insertado";
                }
                if (Convert.ToInt16(cmdAdd.Tag) == 1)
                {
                    string sAux = string.Format("UPDATE {0} SET {1}{2}{3}{4}{5}WHERE {6} = {7}",
                        Constants.TABLE_USERS, changeUSERTYPEID, changeUSERNAME, changePASSWORD,
                        changeNAMES, changeSURNAMES, Constants.FIELD_USERID, txtUSERID.Text);
                    sAux = sAux.Replace(", WHERE", " WHERE");
                    Constants.dbs.sql = sAux;
                    msg = "Usuario actualizado";
                }
                if (Constants.dbs.executeNonSQL(false))
                {
                    MessageBox.Show("Operación exitosa.", "Resultado");
                    CommonMethods.setLogDatabase(msg);
                    listUsers();
                }
                else
                {
                    MessageBox.Show(Constants.dbs.messageError, "Resultado");
                }
                clearctrls();
            }
            else
            {
                MessageBox.Show("Todos los campos deben ser llenados.", "Verificar");
            }
        }

        private void clearctrls()
        {
            txtNAMES.Text = string.Empty;
            txtPASSWORD.Text = string.Empty;
            txtSURNAMES.Text = string.Empty;
            txtUSERID.Text = string.Empty;
            txtUSERNAME.Text = string.Empty;
            cboUSERTYPEID.SelectedIndex = -1;
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (lvwListUsers.SelectedItems.Count > 0)
            {
                string userId = Convert.ToString(lvwListUsers.SelectedItems[0].Tag);
                Constants.dbs.sql = string.Format("SELECT * FROM {0} WHERE {1} = {2}", Constants.TABLE_USERS, Constants.FIELD_USERID, userId);
                DataTable dt = Constants.dbs.getDataTable();

                foreach (DataRow dr in dt.Rows)
                {
                    txtNAMES.Text = dr["NAMES"].ToString();
                    txtSURNAMES.Text = dr["SURNAMES"].ToString();
                    txtUSERNAME.Text = dr["USERNAME"].ToString();
                    txtPASSWORD.Text = dr["PASSWORD"].ToString();
                    txtUSERID.Text = dr["USERID"].ToString();
                    CommonMethods.setValueComboBox(cboUSERTYPEID, dr["USERTYPEID"].ToString());
                    cmdAdd.Tag = 1;
                    changeNAMES = string.Empty;
                    changePASSWORD = string.Empty;
                    changeSURNAMES = string.Empty;
                    changeUSERNAME = string.Empty;
                    changeUSERTYPEID = string.Empty;
                }

            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if(lvwListUsers.SelectedItems.Count > 0)
            {
                DialogResult result1 = MessageBox.Show("¿Está seguro de eliminar el registro?", "Confirmar",  MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {
                    string userId = Convert.ToString(lvwListUsers.SelectedItems[0].Tag);
                    Constants.dbs.sql = string.Format("DELETE FROM {0} WHERE {1} = {2}", Constants.TABLE_USERS, Constants.FIELD_USERID, userId);
                    if (Constants.dbs.executeNonSQL(false))
                    {
                        CommonMethods.setLogDatabase("Usuario eliminado");
                        listUsers();
                    }

                }
             
            }

        }

        private void txtNAMES_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt16(cmdAdd.Tag) == 1)
            {
                changeNAMES = "NAMES = '" + txtNAMES.Text + "', ";
            }
        }

        private void txtSURNAMES_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt16(cmdAdd.Tag) == 1)
            {
                changeSURNAMES = "SURNAMES = '" + txtSURNAMES.Text + "', ";
            }

        }

        private void txtUSERNAME_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt16(cmdAdd.Tag) == 1)
            {
                changeUSERNAME = "USERNAME = '" + txtUSERNAME.Text + "', ";
            }

        }

        private void txtPASSWORD_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt16(cmdAdd.Tag) == 1)
            {
                string pass = Constants.cu.encript(txtPASSWORD.Text.Trim());
                changePASSWORD = "PASSWORD = '" + pass + "', ";
            }

        }

        private void cboUSERTYPEID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt16(cmdAdd.Tag) == 1 && cboUSERTYPEID.SelectedIndex != -1)
            {
                changeUSERTYPEID = "USERTYPEID = " + Convert.ToString(cboUSERTYPEID.SelectedValue) + ", ";
            }
        }
    }
}
