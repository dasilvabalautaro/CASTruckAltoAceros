using System;

using System.Collections.Generic;
using Microsoft.Win32;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Printing;
using System.Drawing.Printing;
using System.IO.Ports;

namespace CASTruck
{
    static class CommonMethods
    {
        static string _messageError = string.Empty;
        public static string messageError
        {
            get
            {
                return _messageError;
            }
        }
 
        public static void setKeyValueRegistry(string strNameSubKey, 
            string strNameKey, object objValue){
            _messageError = string.Empty;
            try{
                RegistryKey regSet = Registry.CurrentUser.CreateSubKey(strNameSubKey);
                regSet.SetValue(strNameKey, objValue);
            }
            catch(Exception e){
            
                _messageError = e.Message;
                
            }
        }

        public static void setLogDatabase(string operations)
        {
            Constants.dbs.sql = "INSERT INTO " + Constants.TABLE_LOGSDATABASE +
                " (USERID, DATEREGISTER, OPERATION) VALUES (" + Constants.USERID_NOW +
                ", GetDate(), '" + operations + "')";
            if (!Constants.dbs.executeNonSQL(false))
            {
                //MessageBox.Show("Falla la inserción de logs.", "Error");
            }
        }

        public static void setLogHardware(string name, string msg)
        {
            Constants.dbs.sql = "INSERT INTO " + Constants.TABLE_LOGSHARDWARE + 
                " (USERID, DATEREGISTER, NAME, DESCRIPTION) VALUES (" + Constants.USERID_NOW + 
                ", GetDate(), '" + name + "', '" + msg + "')";
            if (!Constants.dbs.executeNonSQL(false))
            {
                //MessageBox.Show("Falla la inserción de logs.", "Error");
            }
        }
        public static string getKeyValueRegistry(string strNameSubKey, 
            string strNameKey){
            string strValueKey = string.Empty;
            _messageError = string.Empty;
            RegistryKey regSet = Registry.CurrentUser.CreateSubKey(strNameSubKey);
            if(regSet != null){
                try
                {
                    strValueKey = (string)regSet.GetValue(strNameKey);
                    if (strValueKey == null)
                        strValueKey = string.Empty;
                }
                catch (System.UnauthorizedAccessException e)
                {

                    _messageError = e.Message;

                }
                catch (ArgumentNullException e)
                {
                    _messageError = e.Message;
                }
                
            }
            return strValueKey;
        }

        public static string readTextFile(string strPath){
            StreamReader objStream;
            string strReturn = string.Empty;
            _messageError = string.Empty;

            try{
                if(File.Exists(strPath)){
                    objStream = new StreamReader(strPath, Encoding.Default);
                    strReturn = objStream.ReadToEnd();
                    objStream.Close();
                }
            
            }
            catch(System.IO.FileNotFoundException e){
                _messageError = e.Message;
            }
            return strReturn;
        }

        public static string spotTroubleUsingQueueAttributes(PrintQueue pq){
            string statusReport = string.Empty;

            if ((pq.QueueStatus & PrintQueueStatus.PaperProblem) == PrintQueueStatus.PaperProblem)
            {
                statusReport = statusReport + "Has a paper problem. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.NoToner) == PrintQueueStatus.NoToner)
            {
                statusReport = statusReport + "Is out of toner. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.DoorOpen) == PrintQueueStatus.DoorOpen)
            {
                statusReport = statusReport + "Has an open door. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.Error) == PrintQueueStatus.Error)
            {
                statusReport = statusReport + "Is in an error state. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.NotAvailable) == PrintQueueStatus.NotAvailable)
            {
                statusReport = statusReport + "Is not available. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.Offline) == PrintQueueStatus.Offline)
            {
                statusReport = statusReport + "Is off line. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.OutOfMemory) == PrintQueueStatus.OutOfMemory)
            {
                statusReport = statusReport + "Is out of memory. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.PaperOut) == PrintQueueStatus.PaperOut)
            {
                statusReport = statusReport + "Is out of paper. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.OutputBinFull) == PrintQueueStatus.OutputBinFull)
            {
                statusReport = statusReport + "Has a full output bin. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.PaperJam) == PrintQueueStatus.PaperJam)
            {
                statusReport = statusReport + "Has a paper jam. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.Paused) == PrintQueueStatus.Paused)
            {
                statusReport = statusReport + "Is paused. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.TonerLow) == PrintQueueStatus.TonerLow)
            {
                statusReport = statusReport + "Is low on toner. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.UserIntervention) == PrintQueueStatus.UserIntervention)
            {
                statusReport = statusReport + "Needs user intervention. ";
            }
            return statusReport;
        }

        public static bool verifyPrint(){
            bool blnReturn = true;
            
            PrintQueue defaultPrintQueue = LocalPrintServer.GetDefaultPrintQueue();

            string statusReport = spotTroubleUsingQueueAttributes(defaultPrintQueue);
            _messageError = string.Empty;
            if(statusReport != string.Empty){
                blnReturn = false;
                _messageError = statusReport;
            }
            return blnReturn;
            
        }
        public static void setValueComboBox(ComboBox cbo, string value)
        {
            for (int i = 0; i < cbo.Items.Count; i++)
            {
                cbo.SelectedIndex = i;
                if (cbo.Text == value || Convert.ToString(cbo.SelectedValue) == value)
                    return;
            }
           
        }
  
        public static void setComboBox(ComboBox cbo, List<string> values)
        {
           
            if(values != null){
                foreach (string s in values){
                    cbo.Items.Add(s);
                }
            }
        }

        public static void initComPort(SerialPort _comPort, string _port)
        {

            if (!_comPort.IsOpen)
            {

                _comPort.PortName = _port;
                _comPort.BaudRate = 19200;
                _comPort.Parity = Parity.None;
                _comPort.DataBits = 8;
                _comPort.StopBits = StopBits.One;
                _comPort.DiscardNull = true;
                if (portComOpen(_comPort))
                {
                    _messageError = string.Empty;
                }
            }
        }

        public static bool portComClose(SerialPort _serialPort)
        {
            bool blnReturn = true;

            try
            {
                _serialPort.Close();
            }
            catch (Exception ex)
            {
                _messageError = string.Format("Error: {0} Puerto Com", ex.Message);
                blnReturn = false;
            }

            return blnReturn;
        }

        public static bool portComOpen(SerialPort _serialPort)
        {
            bool blnReturn = true;

            try
            {
                _serialPort.Open();
            }
            catch (Exception ex)
            {
                _messageError = string.Format("Error: {0} Puerto Com", ex.Message);
                blnReturn = false;
            }

            return blnReturn;
        }

        public static string setDateRegister()
        {
            string dateControl;
            System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.CreateSpecificCulture("es-ES");
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            dateControl = string.Format("{0:D}", DateTime.Now);
            ci = System.Globalization.CultureInfo.CreateSpecificCulture("en-EN");
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;

            return dateControl;
        }
        public static void createStatusBar(Form frmWork, StatusStrip statusBar)
        {
            ToolStripLabel panel1 = new ToolStripLabel();
            ToolStripLabel panel2 = new ToolStripLabel();
            ToolStripLabel panel3 = new ToolStripLabel();
            System.Drawing.Size size = new System.Drawing.Size();

            size.Width = Convert.ToInt32(frmWork.Width * 0.2);
            panel1.Text = "";
            panel1.Size = size;
            panel2.Text = "";
            panel2.BackColor = System.Drawing.Color.Blue;
            panel2.ForeColor = System.Drawing.Color.White;
            panel2.Size = size;
            panel3.Text = setDateRegister();
            panel3.BackColor = System.Drawing.Color.Black;
            panel3.ForeColor = System.Drawing.Color.White;
            panel3.Alignment = ToolStripItemAlignment.Right;
            statusBar.Show();
            statusBar.Items.Add(panel1);
            statusBar.Items.Add(panel2);
            statusBar.Items.Add(panel3);

        }
        public static void closeForm(string name)
        {
            Form frm = new Form();
            try
            {
                foreach (Form f in Application.OpenForms)
                {
                    if (f.Name == name)
                    {
                        frm = f;
                        frm.Close();
                    }

                }

            }
            catch (InvalidOperationException ie)
            {
                _messageError = ie.Message;
            }
        }
        public static void enabledOptionMenu(string nameMenu)
        {      
            Form frm = new Form();
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "mdiMain")
                    frm = f;
            }
      

            foreach (ToolStripMenuItem mnu in frm.MainMenuStrip.Items)
            {
                foreach (ToolStripDropDownItem smnu in mnu.DropDownItems)
                {
                    // foreach (ToolStripDropDownItem semnu in smnu.DropDownItems)
                    if (smnu.Name == nameMenu)
                        smnu.Enabled = true;
                }
            }
        }

        public static void enabledOptionSubMenu(string nameOption)
        {
            Form frm = new Form();
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "mdiMain")
                    frm = f;

            }

            foreach (ToolStripMenuItem mnu in frm.MainMenuStrip.Items)
            {
                foreach (ToolStripDropDownItem smnu in mnu.DropDownItems)
                {
                    foreach (ToolStripDropDownItem semnu in smnu.DropDownItems)
                        if (semnu.Name == nameOption)
                            semnu.Enabled = true;
                }
            }
        }

        public static string getPrintDefault()
        {
            PrinterSettings print = new PrinterSettings();
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                print.PrinterName = PrinterSettings.InstalledPrinters[i].ToString();
                if (print.IsDefaultPrinter)
                    return PrinterSettings.InstalledPrinters[i].ToString();
            }
            return String.Empty;

        }
        public static void setValueStatusStrip(string strMessage, 
            int index, StatusStrip ctrl)
        {
           
            StatusStrip c = new StatusStrip();
            c = ctrl;
            c.Items[index].Text = strMessage;
        }

        public static StatusStrip getStatusStripMain()
        {
            StatusStrip status = null;

            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "mdiMain")
                {
                    mdiMain md = (mdiMain)f;
                   
                        foreach (Control c in f.Controls)
                        {
                            if (c is StatusStrip)
                            {
                                status = (StatusStrip) c;
                                break;

                            }
                        }
                }
            }

            return status;
        }

        public static bool verifyDatabase()
        {
            bool result = true;

            if (!Constants.dbs.getValuesConfig())
            {
                MessageBox.Show("La base de datos NO existe!!", "Verificar");
                result = false;
            }
            else
            {
                if (!Constants.dbs.connectDatabase())
                {
                    MessageBox.Show("La base de datos NO conecta!!", "Verificar");
                    result = false;
                }

            }
            return result;
        }

    }
}
