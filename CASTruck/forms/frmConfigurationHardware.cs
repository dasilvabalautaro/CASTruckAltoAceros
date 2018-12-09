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
   
    public partial class frmConfigurationHardware : Form
    {
        #region constantes
        private const string PORT1 = "Port1";
        private const string PORT2 = "Port2";
        private const string PORT3 = "Port3";
        private const string PORT4 = "Port4";
        private const string NAME_POINT_CONTROL = "NamePoint";
        private const string ERROR_INPUT_DATA = "Datos incorretos. Verifique por favor.";
        private const string NOT_FOUND = "Not found";
        #endregion


        public string strNameMenu;

        public frmConfigurationHardware()
        {
            InitializeComponent();
        }

        private void frmConfigurationHardware_Load(object sender, EventArgs e)
        {
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.PerformAutoScale();
            this.Top = 0;
            this.Left = (int)((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2);

            //backgroundWorker.RunWorkerAsync();
            getDataRegister();
        }

        private void setDataComboBox(ComboBox cboPort, string namePort)
        {
            if (string.IsNullOrEmpty(CommonMethods.getKeyValueRegistry(Constants.PATH_KEY, namePort)))
            {
                cboPort.SelectedIndex = cboPort.Items.Count - 1;
            }
            else
            {
                CommonMethods.setValueComboBox(cboPort, 
                    CommonMethods.getKeyValueRegistry(Constants.PATH_KEY, namePort));
            }

        }

        private void getDataRegister()
        {
            setDataComboBox(cboPort1, PORT1);
            setDataComboBox(cboPort2, PORT2);
            setDataComboBox(cboPort3, PORT3);
            setDataComboBox(cboPort4, PORT4);
            txtNamePointControl.Text = Convert.ToString(CommonMethods.getKeyValueRegistry(Constants.PATH_KEY,
                NAME_POINT_CONTROL));

        }

        private void btnSavePointControl_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNamePointControl.Text))
            {
                CommonMethods.setKeyValueRegistry(Constants.PATH_KEY, NAME_POINT_CONTROL, txtNamePointControl.Text);
                lblOkName.Text = "OK";

            }
            else
            {
                MessageBox.Show(ERROR_INPUT_DATA, "Verifique");
            }

        }


        private void setPortsRegister()
        {

            CommonMethods.setKeyValueRegistry(Constants.PATH_KEY, PORT1, cboPort1.Text);
            CommonMethods.setKeyValueRegistry(Constants.PATH_KEY, PORT2, cboPort2.Text);
            CommonMethods.setKeyValueRegistry(Constants.PATH_KEY, PORT3, cboPort3.Text);
            CommonMethods.setKeyValueRegistry(Constants.PATH_KEY, PORT4, cboPort4.Text);

        }

        private Boolean verifyIsEqualsPorts()
        {
            if (!cboPort1.Text.Equals(NOT_FOUND))
            {
                if ((cboPort1.Text.Equals(cboPort2.Text)) || (cboPort1.Text.Equals(cboPort3.Text)) ||
                    (cboPort1.Text.Equals(cboPort4.Text)))
                {
                    return false;
                }
            }
            if (!cboPort2.Text.Equals(NOT_FOUND))
            {
                if ((cboPort2.Text.Equals(cboPort3.Text)) || (cboPort2.Text.Equals(cboPort4.Text)))
                {
                    return false;
                }
            }
            if (!cboPort3.Text.Equals(NOT_FOUND))
            {
                if (cboPort3.Text.Equals(cboPort4.Text))
                {
                    return false;
                }
            }

            return true;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            getDataRegister();
        }

        private void btnSavePorts_Click(object sender, EventArgs e)
        {
            if (verifyIsEqualsPorts())
            {
                setPortsRegister();
                lblOkPorts.Text = "OK";
            }
            else
            {
                MessageBox.Show(ERROR_INPUT_DATA, "Verifique");
            }
        }

        private void frmConfigurationHardware_FormClosing(object sender, FormClosingEventArgs e)
        {
            CommonMethods.enabledOptionSubMenu(strNameMenu);
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
