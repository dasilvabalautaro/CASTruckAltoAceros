using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using MessagingToolkit.QRCode.Codec;
using System.Text;
using System.Threading;

namespace CASTruck.forms
{
    public partial class frmCaptureWeight : Form
    {
        #region variables globales
        public string strNameMenu;
        #endregion
        #region constantes
        private const string PORT1 = "Port1";
        private const string PORT2 = "Port2";
        private const string PORT3 = "Port3";
        private const string PORT4 = "Port4";
        private const string NAME_POINT_CONTROL = "NamePoint";
        private const string ERROR_INPUT_DATA = "Datos incorretos. Verifique por favor.";
        private const string NOT_FOUND = "Not found";
        #endregion

        #region variables locales
        string port1;
        string port2;
        string port3;
        string port4;
        string pointControl;
        StatusStrip status;
        public delegate void AddDataDelegate(String strData, TextBox controlText);
        private AddDataDelegate delegateCatchWeight;
        private Weighing weighing = new Weighing();
        System.Drawing.Bitmap bmpQr;
        DataRow dataRowWeight = null;
        int optionPrint = 0;
        HttpTools httptools = new HttpTools();
        AutoCompleteStringCollection collectionDriverName = new AutoCompleteStringCollection();
        AutoCompleteStringCollection collectionLicense = new AutoCompleteStringCollection();
        AutoCompleteStringCollection collectionPlateVehicle = new AutoCompleteStringCollection();
        AutoCompleteStringCollection collectionProvider = new AutoCompleteStringCollection();
        AutoCompleteStringCollection collectionProduct = new AutoCompleteStringCollection();
        AutoCompleteStringCollection collectionClient = new AutoCompleteStringCollection();
        AutoCompleteStringCollection collectionSource = new AutoCompleteStringCollection();
        AutoCompleteStringCollection collectionTarget = new AutoCompleteStringCollection();
        AutoCompleteStringCollection collectionMark = new AutoCompleteStringCollection();
        string maxIdWeighing = string.Empty;
        string typeCapture = "";
        #endregion

        public frmCaptureWeight()
        {
            
            InitializeComponent();
        }

        private void frmCaptureWeight_Load(object sender, EventArgs e)
        {
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.PerformAutoScale();
            this.Top = 0;
            this.Left = (int)((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2);
            status = CommonMethods.getStatusStripMain();
            this.delegateCatchWeight = new AddDataDelegate(addWeightToText);

            if (CommonMethods.verifyDatabase())
            {
                initVariables();
                setCboTypeVehicle();
                backgroundWorkerInitPorts.RunWorkerAsync();
            }

        }

        private void setWeightInControl(string inputWeight, TextBox controlText)
        {
            ResultPort resultPort = new ResultPort
            {
                objectText = controlText,
                valueWeight = inputWeight
            };

            try
            {
                switch (controlText.Name)
                {
                    case "txtWeight1":
                        {
                            if (backgroundWorkerGetWeight.IsBusy != true)
                            {
                                backgroundWorkerGetWeight.RunWorkerAsync(resultPort);
                            }

                        }
                        break;
                    case "txtWeight2":
                        {
                            if (backgroundWorkerGetWeight2.IsBusy != true)
                            {
                                backgroundWorkerGetWeight2.RunWorkerAsync(resultPort);
                            }

                        }
                        break;
                    case "txtWeight3":
                        {
                            if (backgroundWorkerGetWeigh3.IsBusy != true)
                            {
                                backgroundWorkerGetWeigh3.RunWorkerAsync(resultPort);
                            }

                        }
                        break;
                    case "txtWeight4":
                        {
                            if (backgroundWorkerGetWeigh4.IsBusy != true)
                            {
                                backgroundWorkerGetWeigh4.RunWorkerAsync(resultPort);
                            }

                        }
                        break;
                    default:
                        {

                        }
                        break;
                }


            }
            catch (InvalidOperationException io)
            {
                if (!Constants.USERID_NOW.Equals("0"))
                    CommonMethods.setLogHardware("InvalidOperationException", io.Message);
            }

        }

        public void addWeightToText(string inputWeight, TextBox controlText)
        {
            ResultPort resultPort = new ResultPort
            {
                objectText = controlText,
                valueWeight = inputWeight
            };

            try
            {
                switch (controlText.Name)
                {
                    case "txtWeight1":
                        {
                            if (backgroundWorkerGetWeight.IsBusy != true)
                            {
                                backgroundWorkerGetWeight.RunWorkerAsync(resultPort);
                            }

                        }
                        break;
                    case "txtWeight2":
                        {
                            if (backgroundWorkerGetWeight2.IsBusy != true)
                            {
                                backgroundWorkerGetWeight2.RunWorkerAsync(resultPort);
                            }

                        }
                        break;
                    case "txtWeight3":
                        {
                            if (backgroundWorkerGetWeigh3.IsBusy != true)
                            {
                                backgroundWorkerGetWeigh3.RunWorkerAsync(resultPort);
                            }

                        }
                        break;
                    case "txtWeight4":
                        {
                            if (backgroundWorkerGetWeigh4.IsBusy != true)
                            {
                                backgroundWorkerGetWeigh4.RunWorkerAsync(resultPort);
                            }

                        }
                        break;
                    default:
                        {

                        }
                        break;
                }


            }
            catch (InvalidOperationException io)
            {
                if (!Constants.USERID_NOW.Equals("0"))
                    CommonMethods.setLogHardware("InvalidOperationException", io.Message);
            }


        }

        //public void addWeightToText(string inputWeight, TextBox controlText)
        //{

        //    string exaControl = getBalance(inputWeight);
        //    switch (exaControl)
        //    {
        //        case "01-3F":
        //            {
        //                controlText = txtWeight1;

        //            }
        //            break;
        //        case "02-3F":
        //            {
        //                controlText = txtWeight2;

        //            }
        //            break;
        //        case "03-3F":
        //            {
        //                controlText = txtWeight3;

        //            }
        //            break;
        //        case "04-3F":
        //            {
        //                controlText = txtWeight4;

        //            }
        //            break;
        //        default:{
        //                controlText = null;
        //            }
        //            break;
        //    }

        //    if(controlText != null)
        //    {
        //        setWeightInControl(inputWeight, controlText);
        //    }

        //}

        private void initVariables()
        {
            backgroundWorkerGetWeight.WorkerSupportsCancellation = true;
            backgroundWorkerGetWeight2.WorkerSupportsCancellation = true;
            backgroundWorkerGetWeigh3.WorkerSupportsCancellation = true;
            backgroundWorkerGetWeigh4.WorkerSupportsCancellation = true;
            backgroundWorkerSendCloud.WorkerSupportsCancellation = true;

            port1 = Convert.ToString(CommonMethods.getKeyValueRegistry(Constants.PATH_KEY, PORT1));
            port2 = Convert.ToString(CommonMethods.getKeyValueRegistry(Constants.PATH_KEY, PORT2));
            port3 = Convert.ToString(CommonMethods.getKeyValueRegistry(Constants.PATH_KEY, PORT3));
            port4 = Convert.ToString(CommonMethods.getKeyValueRegistry(Constants.PATH_KEY, PORT4));
            pointControl = Convert.ToString(CommonMethods.getKeyValueRegistry(Constants.PATH_KEY, NAME_POINT_CONTROL));
            txtUserName.Text = Constants.NAMES_NOW + " " + Constants.SURNAMES_NOW;
            lvwListPending.Columns.Add("ID", 50, HorizontalAlignment.Left);
            lvwListPending.Columns.Add("PLACA", 100, HorizontalAlignment.Left);
            getListVehiclePending();
            backgroundWorkerAutoComplete.RunWorkerAsync();

           
        }


        private void initAllPorts()
        {
            initPort(serialPort1, port1);            
            initPort(serialPort2, port2);
            initPort(serialPort3, port3);
            initPort(serialPort4, port4);
        }

        private void setCboTypeVehicle()
        {

            for (int i = 1; i <= Constants.dtPermittedWeightsAndDimensions.Rows.Count; i++)
            {
                cboTypeVehicle.Items.Add(Convert.ToString(i));

            }

        }

        private void initPort(SerialPort serialPort, string port)
        {
            if (!string.IsNullOrEmpty(port) && !port.Equals(NOT_FOUND))
            {
                try
                {
                    SerialPortFixer.Execute(port);
                    CommonMethods.initComPort(serialPort, port);
                    if (!string.IsNullOrEmpty(CommonMethods.messageError))
                    {

                        if (!Constants.USERID_NOW.Equals("0"))
                            CommonMethods.setLogHardware("Serial Port", CommonMethods.messageError);
                        CommonMethods.setValueStatusStrip("Error Hardware Port " + port, 0, status);

                    }
                }
                catch(IOException io)
                {
                    if (!Constants.USERID_NOW.Equals("0"))
                        CommonMethods.setLogHardware("Serial Port", io.Message);
                    CommonMethods.setValueStatusStrip("Error Hardware Port " + port, 0, status);
                }
               
            }
        }

        private void frmCaptureWeight_FormClosing(object sender, FormClosingEventArgs e)
        {
            CommonMethods.enabledOptionMenu(strNameMenu);
            try
            {
                if (backgroundWorkerGetWeight.WorkerSupportsCancellation == true)
                {
                    // Cancel the asynchronous operation.
                    backgroundWorkerGetWeight.CancelAsync();
                }

                if (backgroundWorkerGetWeight2.WorkerSupportsCancellation == true)
                {
                    // Cancel the asynchronous operation.
                    backgroundWorkerGetWeight2.CancelAsync();
                }
                if (backgroundWorkerGetWeigh3.WorkerSupportsCancellation == true)
                {
                    // Cancel the asynchronous operation.
                    backgroundWorkerGetWeigh3.CancelAsync();
                }
                if (backgroundWorkerGetWeigh4.WorkerSupportsCancellation == true)
                {
                    // Cancel the asynchronous operation.
                    backgroundWorkerGetWeigh4.CancelAsync();
                }

                if (backgroundWorkerSendCloud.WorkerSupportsCancellation == true)
                {
                    backgroundWorkerSendCloud.CancelAsync();
                }

                if (serialPort1.IsOpen)
                {
                    serialPort1.DiscardInBuffer();
                    serialPort1.DiscardOutBuffer();
                

                }
            
                if (serialPort2.IsOpen)
                {
                    serialPort2.DiscardInBuffer();
                    serialPort2.DiscardOutBuffer();                

                }
                
                if (serialPort3.IsOpen)
                {
                    serialPort3.DiscardInBuffer();
                    serialPort3.DiscardOutBuffer();
                    

                }
                if (serialPort4.IsOpen)
                {
                    serialPort4.DiscardInBuffer();
                    serialPort4.DiscardOutBuffer();
                    

                }
                base.Dispose(true);
            }
            catch (IOException ex)
            {
                Console.Write(ex.Message);
            }
            catch (Exception et)
            {
                Console.Write(et.Message);
            }
            finally
            {
                CommonMethods.setValueStatusStrip("", 0, status);
            }
        }

        private void backgroundWorkerInitPorts_DoWork(object sender, DoWorkEventArgs e)
        {
            initAllPorts();

        }

        private void writeSerialPort1()
        {
            try
            {
              
                var data = new byte[] {0x31, (byte)'M', (byte)'Z', 13, 10};
                serialPort1.Write(data, 0, data.Length);
                txtWeight1.Text = "0";

            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
            catch(TimeoutException te)
            {
                Console.WriteLine(te.Message);
            }
            catch (NullReferenceException ne)
            {
                Console.WriteLine(ne.Message);
            }
            catch (InvalidOperationException ie)
            {
                Console.WriteLine(ie.Message);
            }
        }

        private void writeSerialPort2()
        {
            try
            {
                var data = new byte[] {0x32, (byte)'M', (byte)'Z', 13, 10};
                serialPort2.Write(data, 0, data.Length);
                txtWeight2.Text = "0";

            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
            catch (TimeoutException te)
            {
                Console.WriteLine(te.Message);
            }
            catch (NullReferenceException ne)
            {
                Console.WriteLine(ne.Message);
            }
            catch (InvalidOperationException ie)
            {
                Console.WriteLine(ie.Message);
            }
        }

        private void writeSerialPort3()
        {
            try
            {
                var data = new byte[] {0x33, (byte)'M', (byte)'Z', 13, 10};
                serialPort3.Write(data, 0, data.Length);
                txtWeight3.Text = "0";

            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
            catch (TimeoutException te)
            {
                Console.WriteLine(te.Message);
            }
            catch (NullReferenceException ne)
            {
                Console.WriteLine(ne.Message);
            }
            catch (InvalidOperationException ie)
            {
                Console.WriteLine(ie.Message);
            }
        }

        private void writeSerialPort4()
        {
            try
            {
                var data = new byte[] {0x34, (byte)'M', (byte)'Z', 13, 10};
                serialPort4.Write(data, 0, data.Length);
                txtWeight4.Text = "0";

            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
            catch (TimeoutException te)
            {
                Console.WriteLine(te.Message);
            }
            catch (NullReferenceException ne)
            {
                Console.WriteLine(ne.Message);
            }
            catch (InvalidOperationException ie)
            {
                Console.WriteLine(ie.Message);
            }
        }
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                System.Threading.Thread.Sleep(50);
                string strReceived = "";
                SerialPort serialPort = (SerialPort)sender;
                strReceived = serialPort.ReadLine();
                this.Invoke(this.delegateCatchWeight, new Object[] { strReceived, txtWeight1 });

            }
            catch (InvalidOperationException ie)
            {
                if (!Constants.USERID_NOW.Equals("0"))
                    CommonMethods.setLogHardware("InvalidOperationException", ie.Message);
            }catch(IOException ei)
            {
                Console.Write(ei.Message);
            }

        }

        private string getWeight(string input, TextBox control)
        {

            int k;
            string strValue = string.Empty;
            char[] arrayInput = input.ToCharArray();

            for (int i = 0; i < arrayInput.Length; i++)
            {
                if (int.TryParse(Convert.ToString(arrayInput[i]), out k))
                {
                    strValue += Convert.ToString(k);
                }
                else if (Convert.ToString(arrayInput[i]).Equals("-"))
                {
                    strValue += Convert.ToString(arrayInput[i]);
                }
            }

            return strValue;
        }

        private string getBalance(string input)
        {
            string hexString = "";
            string[] pieces = input.Split(',');

            if (pieces.Length == 4)
            {
                byte[] bytePiece = Encoding.Default.GetBytes(pieces[2].Trim());
                hexString = BitConverter.ToString(bytePiece);

            }
            return hexString;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void backgroundWorkerGetWeight_DoWork(object sender, DoWorkEventArgs e)
        {
            ResultPort resultPort = e.Argument as ResultPort;

            string valueWeight = getWeight(resultPort.valueWeight, resultPort.objectText);
            resultPort.valueWeight = valueWeight;

            e.Result = resultPort;
        }

        private void backgroundWorkerGetWeight_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ResultPort resultPort = e.Result as ResultPort;

            if (!string.IsNullOrEmpty(resultPort.valueWeight))
            {
                resultPort.objectText.Text = resultPort.valueWeight;
            }
        }

        private void serialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                System.Threading.Thread.Sleep(50);
                string strReceived = "";
                SerialPort serialPort = (SerialPort)sender;
                strReceived = serialPort.ReadLine();
                this.Invoke(this.delegateCatchWeight, new Object[] { strReceived, txtWeight2 });

            }
            catch (InvalidOperationException ie)
            {
                if (!Constants.USERID_NOW.Equals("0"))
                    CommonMethods.setLogHardware("InvalidOperationException", ie.Message);
            }
            catch (IOException ei)
            {
                Console.Write(ei.Message);
            }
        }

        private void serialPort3_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                System.Threading.Thread.Sleep(50);
                string strReceived = "";
                SerialPort serialPort = (SerialPort)sender;
                strReceived = serialPort.ReadLine();
                this.Invoke(this.delegateCatchWeight, new Object[] { strReceived, txtWeight3 });

            }
            catch (InvalidOperationException ie)
            {
                if (!Constants.USERID_NOW.Equals("0"))
                    CommonMethods.setLogHardware("InvalidOperationException", ie.Message);
            }
        }

        private void serialPort4_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                System.Threading.Thread.Sleep(50);
                string strReceived = "";
                SerialPort serialPort = (SerialPort)sender;
                strReceived = serialPort.ReadLine();
                this.Invoke(this.delegateCatchWeight, new Object[] { strReceived, txtWeight4 });

            }
            catch (InvalidOperationException ie)
            {
                if (!Constants.USERID_NOW.Equals("0"))
                    CommonMethods.setLogHardware("InvalidOperationException", ie.Message);
            }
        }

        private void serialPort1_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            CommonMethods.setValueStatusStrip(e.EventType.GetType().ToString() + " " + port1, 0, status);
        }

        private void serialPort2_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            CommonMethods.setValueStatusStrip(e.EventType.GetType().ToString() + " " + port2, 0, status);
        }

        private void serialPort3_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            CommonMethods.setValueStatusStrip(e.EventType.GetType().ToString() + " " + port3, 0, status);
        }

        private void serialPort4_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            CommonMethods.setValueStatusStrip(e.EventType.GetType().ToString() + " " + port4, 0, status);
        }

        private void cboTypeVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataRow[] foundRows;
                DataRow findRow;

                if (cboTypeVehicle.SelectedIndex != -1)
                {
                    foundRows = Constants.dtPermittedWeightsAndDimensions.Select("VehicleId = " + Convert.ToString(cboTypeVehicle.Text));
                    findRow = foundRows[0];
                    lblDescription.Text = findRow["AXLESANDWHEELS"].ToString();
                    picTruck.ImageLocation = Application.StartupPath + "\\" + Constants.FILE_IMAGES + "\\f" + cboTypeVehicle.Text + ".jpg";
                    int val;

                    if (int.TryParse(cboTypeVehicle.Text.ToString(), out val))
                    {
                        weighing.Vehicleid = val;
                    }

                }
                else
                {
                    picTruck.ImageLocation = Application.StartupPath + "\\" + Constants.FILE_IMAGES + "\\f0.jpg";
                }

            }
            catch (IndexOutOfRangeException ie)
            {
                if (!Constants.USERID_NOW.Equals("0"))
                    CommonMethods.setLogHardware("IndexOutOfRangeException", ie.Message);

            }
        }

        private void drivername_TextChanged(object sender, EventArgs e)
        {
            weighing.Drivername = drivername.Text;
        }

        private void license_TextChanged(object sender, EventArgs e)
        {
            weighing.License = license.Text;
        }

        private void platevehicle_TextChanged(object sender, EventArgs e)
        {
            weighing.Platevehicle = platevehicle.Text;
            
           
        }

        private void mark_TextChanged(object sender, EventArgs e)
        {
            weighing.Mark = mark.Text;
        }

        private void source_TextChanged(object sender, EventArgs e)
        {
            weighing.Source = source.Text;
        }

        private void provider_TextChanged(object sender, EventArgs e)
        {
            weighing.Provider = provider.Text;
        }

        private void product_TextChanged(object sender, EventArgs e)
        {
            weighing.Product = product.Text;
        }

        private void client_TextChanged(object sender, EventArgs e)
        {
            weighing.Client = client.Text;
        }

        private void target_TextChanged(object sender, EventArgs e)
        {
            weighing.Target = target.Text;
        }

        private void txtObservation_TextChanged(object sender, EventArgs e)
        {
            weighing.Observation = txtObservation.Text;
        }

        private void weightbruto_TextChanged(object sender, EventArgs e)
        {
            int val;

            if (string.IsNullOrEmpty(weightbruto.Text.Trim()))
            {
                weightbruto.Text = "0";
            }

            if (int.TryParse(weightbruto.Text.ToString(), out val))
            {
                weighing.Weightbruto = val;
            }

            setWeightNeto();
        }

        private void weighttara_TextChanged(object sender, EventArgs e)
        {
            int val;

            if (string.IsNullOrEmpty(weighttara.Text.Trim()))
            {
                weighttara.Text = "0";
            }

            if (int.TryParse(weighttara.Text.ToString(), out val))
            {
                weighing.Weighttara = val;
            }

            setWeightNeto();
        }

        private void weightneto_TextChanged(object sender, EventArgs e)
        {
            int val;

            if (int.TryParse(weightneto.Text.ToString(), out val))
            {
                weighing.Weightneto = val;
            }
        }

        private void txtWeight1_TextChanged(object sender, EventArgs e)
        {
            int val;

            if (int.TryParse(txtWeight1.Text.ToString(), out val))
            {
                weighing.Weight1 = val;
            }
        }

        private void txtWeight2_TextChanged(object sender, EventArgs e)
        {
            int val;

            if (int.TryParse(txtWeight2.Text.ToString(), out val))
            {
                weighing.Weight2 = val;
            }
        }

        private void txtWeight3_TextChanged(object sender, EventArgs e)
        {
            int val;

            if (int.TryParse(txtWeight3.Text.ToString(), out val))
            {
                weighing.Weight3 = val;
            }
        }

        private void txtWeight4_TextChanged(object sender, EventArgs e)
        {
            int val;

            if (int.TryParse(txtWeight4.Text.ToString(), out val))
            {
                weighing.Weight4 = val;
            }
        }

        private void setWeightNeto()
        {
            int bruto;
            int tara;

            if (int.TryParse(weightbruto.Text.ToString(), out bruto))
            {
                if (int.TryParse(weighttara.Text.ToString(), out tara))
                {
                    int neto = bruto - tara;

                    weightneto.Text = Convert.ToString(neto);
                }
            }
        }

        private void lblDescription_TextChanged(object sender, EventArgs e)
        {
            weighing.Description = lblDescription.Text;
        }

        private void weightbruto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar))
            {

                e.Handled = false;

            }
            else
            {
                e.Handled = true;
            }
        }

        private void weighttara_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar))
            {
                typeCapture = "TM";
                e.Handled = false;

            }
            else
            {
                e.Handled = true;
            }
        }

        private bool validateInput()
        {
            if (cboTypeVehicle.SelectedIndex != -1 && weightneto.Text != "0"
                && !string.IsNullOrEmpty(drivername.Text)
                && !string.IsNullOrEmpty(license.Text)
                && !string.IsNullOrEmpty(platevehicle.Text))
            {
                return true;
            }

            return false;
        }

        private string buildSQLUpdate()
        {
            string sql = "UPDATE [dbo].[WEIGHING] SET [VEHICLEID] = @vehicleid, [PLATEVEHICLE] = @platevehicle, " +
                "[DRIVERNAME] = @drivername, [LICENSE] = @license, [MARK] = @mark, [OUTSIDE] = GetDate(), [SOURCE] =  @source, " +
                "[TARGET] = @target, [PROVIDER] = @provider, [PRODUCT] = @product, [CLIENT] = @client, [WEIGHTBRUTO] = @weightbruto, " +
                "[WEIGHTTARA] = @weighttara, [WEIGHTNETO] = @weightneto, [OBSERVATION] = @observation, [DESCRIPTION] = @description, [MOBILE] = @mobile";

            if ((Convert.ToInt32(weightbruto.Text) != 0) && (Convert.ToInt32(weightbruto.Text) != Convert.ToInt32(weightbruto.Tag)))
            {
                sql += ", [WEIGHT1] = @weight1, [WEIGHT2] = @weight2, [WEIGHT3] = @weight3, [WEIGHT4] = @weight4";
            }

            sql += " WHERE WEIGHINGID = " + WEIGHINGID.Text;
            return sql;
        }

        private string buildSQLInsert()
        {
            weighing.Userid = Convert.ToInt16(Constants.USERID_NOW);

            string sqlFields = "INSERT INTO [dbo].[WEIGHING] ([USERID], [VEHICLEID], " +
                "[DATEREGISTER], [PLATEVEHICLE], [DRIVERNAME], [LICENSE], " +
                "[MARK], [INSIDE], [SOURCE], [TARGET], [PROVIDER], " +
                "[PRODUCT], [CLIENT], [WEIGHT1], [WEIGHT2], [WEIGHT3], [WEIGHT4], " +
                "[WEIGHTBRUTO], [WEIGHTTARA], [WEIGHTNETO], [OBSERVATION], [DESCRIPTION], [MOBILE]) ";
            string sqlValues = "VALUES (@userid, @vehicleid, " +
                "GetDate(), @platevehicle, " +
                "@drivername, @license, @mark, " +
                "GetDate(), @source, " +
                "@target, @provider, @product, " +
                "@client, @weight1, @weight2, " +
                "@weight3, @weight4, " +
                "@weightbruto, @weighttara, " +
                "@weightneto, @observation, @description, @mobile)";

            return sqlFields + sqlValues;
        }

        private string buildSQLInsertChild()
        {          
            string sqlFields = "INSERT INTO [dbo].[WEIGHTCHILD] ([WEIGHINGID], [INSIDE], " +
                "[WEIGHT1], [WEIGHT2], [WEIGHT3], [WEIGHT4], " +
                "[WEIGHTBRUTO], [WEIGHTTARA], [WEIGHTNETO], [OBSERVATION]) ";
            string sqlValues = "VALUES (@weighingid, " +
                "GetDate(), @weight1, @weight2, " +
                "@weight3, @weight4, " +
                "@weightbruto, @weighttara, " +
                "@weightneto, @observation)";

            return sqlFields + sqlValues;
        }

        private SqlParameter[] buildParametersChild(int weighingid)
        {

            SqlParameter[] parameters =
            {
              new SqlParameter("@weighingid", SqlDbType.Int) { Value = weighingid },
              new SqlParameter("@weight1", SqlDbType.Decimal) { Value = weighing.Weight1 },
              new SqlParameter("@weight2", SqlDbType.Decimal) { Value = weighing.Weight2 },
              new SqlParameter("@weight3", SqlDbType.Decimal) { Value = weighing.Weight3 },
              new SqlParameter("@weight4", SqlDbType.Decimal) { Value = weighing.Weight4 },
              new SqlParameter("@weightbruto", SqlDbType.Decimal) { Value = weighing.Weightbruto },
              new SqlParameter("@weighttara", SqlDbType.Decimal) { Value = weighing.Weighttara },
              new SqlParameter("@weightneto", SqlDbType.Decimal) { Value = weighing.Weightneto },
              new SqlParameter("@observation", SqlDbType.VarChar, 200) { Value = (object)typeCapture?? DBNull.Value },             
            };

            return parameters;
        }

        private SqlParameter[] buildParametersUpdateOutEje()
        {
            SqlParameter[] parameters =
           {

              new SqlParameter("@vehicleid", SqlDbType.Int) { Value = weighing.Vehicleid },
              new SqlParameter("@platevehicle", SqlDbType.VarChar, 20) { Value = weighing.Platevehicle },
              new SqlParameter("@drivername", SqlDbType.VarChar, 50) { Value = weighing.Drivername },
              new SqlParameter("@license", SqlDbType.VarChar, 20) { Value = weighing.License },
              new SqlParameter("@mark", SqlDbType.VarChar, 20) { Value = (object)weighing.Mark ?? DBNull.Value },
              new SqlParameter("@source", SqlDbType.VarChar, 50) { Value = (object)weighing.Source ?? DBNull.Value },
              new SqlParameter("@target", SqlDbType.VarChar, 50) { Value = (object)weighing.Target ?? DBNull.Value },
              new SqlParameter("@provider", SqlDbType.VarChar, 50) { Value = (object)weighing.Provider ?? DBNull.Value },
              new SqlParameter("@product", SqlDbType.VarChar, 50) { Value = (object)weighing.Product ?? DBNull.Value },
              new SqlParameter("@client", SqlDbType.VarChar, 50) { Value = (object)weighing.Client ?? DBNull.Value },
              new SqlParameter("@weightbruto", SqlDbType.Decimal) { Value = weighing.Weightbruto },
              new SqlParameter("@weighttara", SqlDbType.Decimal) { Value = weighing.Weighttara },
              new SqlParameter("@weightneto", SqlDbType.Decimal) { Value = weighing.Weightneto },
              new SqlParameter("@observation", SqlDbType.VarChar, 200) { Value = (object)weighing.Observation ?? DBNull.Value },
              new SqlParameter("@description", SqlDbType.VarChar, 50) { Value = weighing.Description },
              new SqlParameter("@mobile", SqlDbType.VarChar, 20) { Value = (object)weighing.Mobile ?? DBNull.Value }

            };

            return parameters;
        }

        private SqlParameter[] buildParametersUpdate()
        {
            decimal weight1 = 0;
            decimal weight2 = 0;
            decimal weight3 = 0;
            decimal weight4 = 0;

            if (checkPB.Checked)
            {
                weight1 = weighing.Weight1;
                weight2 = weighing.Weight2;
                weight3 = weighing.Weight3;
                weight4 = weighing.Weight3;
            }
            SqlParameter[] parameters =
           {

              new SqlParameter("@vehicleid", SqlDbType.Int) { Value = weighing.Vehicleid },
              new SqlParameter("@platevehicle", SqlDbType.VarChar, 20) { Value = weighing.Platevehicle },
              new SqlParameter("@drivername", SqlDbType.VarChar, 50) { Value = weighing.Drivername },
              new SqlParameter("@license", SqlDbType.VarChar, 20) { Value = weighing.License },
              new SqlParameter("@mark", SqlDbType.VarChar, 20) { Value = (object)weighing.Mark ?? DBNull.Value },
              new SqlParameter("@source", SqlDbType.VarChar, 50) { Value = (object)weighing.Source ?? DBNull.Value },
              new SqlParameter("@target", SqlDbType.VarChar, 50) { Value = (object)weighing.Target ?? DBNull.Value },
              new SqlParameter("@provider", SqlDbType.VarChar, 50) { Value = (object)weighing.Provider ?? DBNull.Value },
              new SqlParameter("@product", SqlDbType.VarChar, 50) { Value = (object)weighing.Product ?? DBNull.Value },
              new SqlParameter("@client", SqlDbType.VarChar, 50) { Value = (object)weighing.Client ?? DBNull.Value },
              new SqlParameter("@weight1", SqlDbType.Decimal) { Value = weight1 },
              new SqlParameter("@weight2", SqlDbType.Decimal) { Value = weight2 },
              new SqlParameter("@weight3", SqlDbType.Decimal) { Value = weight3 },
              new SqlParameter("@weight4", SqlDbType.Decimal) { Value = weight4 },
              new SqlParameter("@weightbruto", SqlDbType.Decimal) { Value = weighing.Weightbruto },
              new SqlParameter("@weighttara", SqlDbType.Decimal) { Value = weighing.Weighttara },
              new SqlParameter("@weightneto", SqlDbType.Decimal) { Value = weighing.Weightneto },
              new SqlParameter("@observation", SqlDbType.VarChar, 200) { Value = (object)weighing.Observation ?? DBNull.Value },
              new SqlParameter("@description", SqlDbType.VarChar, 50) { Value = weighing.Description },
              new SqlParameter("@mobile", SqlDbType.VarChar, 20) { Value = (object)weighing.Mobile ?? DBNull.Value }

            };

            return parameters;
        }

        private SqlParameter[] buildParameters()
        {
            decimal weight1 = 0;
            decimal weight2 = 0;
            decimal weight3 = 0;
            decimal weight4 = 0;

            if (checkPB.Checked)
            {
                weight1 = weighing.Weight1;
                weight2 = weighing.Weight2;
                weight3 = weighing.Weight3;
                weight4 = weighing.Weight3;
            }

            SqlParameter[] parameters =
            {
              new SqlParameter("@userid", SqlDbType.Int) { Value = weighing.Userid },
              new SqlParameter("@vehicleid", SqlDbType.Int) { Value = weighing.Vehicleid },
              new SqlParameter("@platevehicle", SqlDbType.VarChar, 20) { Value = weighing.Platevehicle },
              new SqlParameter("@drivername", SqlDbType.VarChar, 50) { Value = weighing.Drivername },
              new SqlParameter("@license", SqlDbType.VarChar, 20) { Value = weighing.License },
              new SqlParameter("@mark", SqlDbType.VarChar, 20) { Value = (object)weighing.Mark ?? DBNull.Value },
              new SqlParameter("@source", SqlDbType.VarChar, 50) { Value = (object)weighing.Source ?? DBNull.Value },
              new SqlParameter("@target", SqlDbType.VarChar, 50) { Value = (object)weighing.Target ?? DBNull.Value },
              new SqlParameter("@provider", SqlDbType.VarChar, 50) { Value = (object)weighing.Provider ?? DBNull.Value },
              new SqlParameter("@product", SqlDbType.VarChar, 50) { Value = (object)weighing.Product ?? DBNull.Value },
              new SqlParameter("@client", SqlDbType.VarChar, 50) { Value = (object)weighing.Client ?? DBNull.Value },
              new SqlParameter("@weight1", SqlDbType.Decimal) { Value = weight1 },
              new SqlParameter("@weight2", SqlDbType.Decimal) { Value = weight2 },
              new SqlParameter("@weight3", SqlDbType.Decimal) { Value = weight3 },
              new SqlParameter("@weight4", SqlDbType.Decimal) { Value = weight4 },
              new SqlParameter("@weightbruto", SqlDbType.Decimal) { Value = weighing.Weightbruto },
              new SqlParameter("@weighttara", SqlDbType.Decimal) { Value = weighing.Weighttara },
              new SqlParameter("@weightneto", SqlDbType.Decimal) { Value = weighing.Weightneto },
              new SqlParameter("@observation", SqlDbType.VarChar, 200) { Value = (object)weighing.Observation ?? DBNull.Value },
              new SqlParameter("@description", SqlDbType.VarChar, 50) { Value = weighing.Description },
              new SqlParameter("@mobile", SqlDbType.VarChar, 20) { Value = (object)weighing.Mobile ?? DBNull.Value }

            };

            return parameters;
        }

        private void afterInsert()
        {
            CommonMethods.setLogDatabase("Boleta insertada");
            grbGeneral.Enabled = false;
            getListVehiclePending();

        }

        private void clearControls()
        {
            grbGeneral.Enabled = true;
            inside.Text = string.Empty;
            outside.Text = string.Empty;
            drivername.Text = string.Empty;
            license.Text = string.Empty;
            platevehicle.Text = string.Empty;
            mark.Text = string.Empty;
            source.Text = string.Empty;
            product.Text = string.Empty;
            provider.Text = string.Empty;
            client.Text = string.Empty;
            target.Text = string.Empty;
            mobile.Text = string.Empty;
            weightbruto.Text = "0";
            weighttara.Text = "0";
            weightneto.Text = "0";
            txtObservation.Text = string.Empty;
            cboTypeVehicle.SelectedIndex = -1;
            lblDescription.Text = string.Empty;
            btnSave.Enabled = true;
            btnPrint.Enabled = false;
            btnPrintTicket.Enabled = false;
            btnSave.Tag = 1;
            checkPB.Checked = true;
            weighing = new Weighing();
            WEIGHINGID.Text = string.Empty;
            WEIGHINGID.Visible = false;
            dataRowWeight = null;
            typeCapture = "";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            //txtObservation.Text = typeCapture + " " + txtObservation.Text;
            int tag = Convert.ToInt16(btnSave.Tag);
            switch (tag)
            {
                case 1:
                    {
                        saveInside();
                    }
                    break;
                case 2:
                    {
                        saveOutside();
                    }
                    break;
                case 3:
                    {
                        saveChild();
                    }
                    break;
                default:
                    {

                    }
                    break;
            }
            if (backgroundWorkerSendCloud.IsBusy != true)
            {
                backgroundWorkerSendCloud.RunWorkerAsync();
            }
            if (backgroundWorkerAutoComplete.IsBusy != true)
            {
                backgroundWorkerAutoComplete.RunWorkerAsync();
            }
            
        }

        private void saveOutside()
        {
            if (validateInput())
            {
                SqlParameter[] parameters;
                Constants.dbs.sql = buildSQLUpdate();
                if (checkPB.Checked)
                {
                    parameters = buildParametersUpdate();
                }
                else
                {
                    parameters = buildParametersUpdateOutEje();
                }

                if (Constants.dbs.executeNonSQL(false, parameters))
                {
                    MessageBox.Show("Registro actualizado con éxito.", "Exito");
                    afterInsert();
                    btnSave.Enabled = false;
                    btnPrint.Enabled = true;
                    btnPrintTicket.Enabled = true;
                    int weightId = Convert.ToInt16(WEIGHINGID.Text.ToString());
                    saveWeightChild(weightId);
                    dataRowWeight = getWeightById(WEIGHINGID.Text);
                    maxIdWeighing = WEIGHINGID.Text;
                }
                else
                {
                    MessageBox.Show(Constants.dbs.messageError, "Verificar");
                }
            }
            else
            {
                MessageBox.Show("Datos incompletos. Verifique por favor.", "Verificar");
            }
        }

        private void saveChild()
        {
            int weightId = Convert.ToInt16(WEIGHINGID.Text.ToString());
            saveWeightChild(weightId);
            MessageBox.Show("Registro guardado con éxito.", "Exito");
            afterInsert();
            btnSave.Enabled = false;
            btnPrint.Enabled = true;
            btnPrintTicket.Enabled = true;
            //string maxId = getMaxIdWeighingChild();
            dataRowWeight = getWeightById(WEIGHINGID.Text); //getWeightByIdChild(maxId);
        }

        private void saveWeightChild(int weightId)
        {
            Constants.dbs.sql = buildSQLInsertChild();
            SqlParameter[] parameters = buildParametersChild(weightId);
            if (!Constants.dbs.executeNonSQL(false, parameters))
            {
                Console.Write(Constants.dbs.messageError);
            
            }
        }

        private void saveInside()
        {
            if (validateInput())
            {
                Constants.dbs.sql = buildSQLInsert();
                SqlParameter[] parameters = buildParameters();
                if (Constants.dbs.executeNonSQL(false, parameters))
                {
                    MessageBox.Show("Registro guardado con éxito.", "Exito");
                    afterInsert();
                    btnSave.Enabled = false;
                    btnPrint.Enabled = true;
                    btnPrintTicket.Enabled = true;
                    WEIGHINGID.Text = getMaxIdWeighing();
                    int weightId = Convert.ToInt16(WEIGHINGID.Text.ToString());
                    saveWeightChild(weightId);
                    dataRowWeight = getWeightById(WEIGHINGID.Text);
                    maxIdWeighing = WEIGHINGID.Text;

                }
                else
                {
                    MessageBox.Show(Constants.dbs.messageError, "Verificar");
                }
            }
            else
            {
                MessageBox.Show("Datos incompletos. Verifique por favor.", "Verificar");
            }
        }

        private string getMaxIdWeighingChild()
        {
            string weightId = "";
            Constants.dbs.sql = "SELECT MAX(ID) FROM " + Constants.TABLE_WEIGHINGCHILD;

            DataTable dt = Constants.dbs.getDataTable();
            if (dt != null && dt.Rows[0].ItemArray[0] != DBNull.Value)
            {

                weightId = Convert.ToString(dt.Rows[0].ItemArray[0]);
            }

            return weightId;
        }

        private string getMaxIdWeighing()
        {
            string weightId = "";
            Constants.dbs.sql = "SELECT MAX(WEIGHINGID) FROM " + Constants.TABLE_WEIGHING;

            DataTable dt = Constants.dbs.getDataTable();
            if (dt != null && dt.Rows[0].ItemArray[0] != DBNull.Value)
            {

                weightId = Convert.ToString(dt.Rows[0].ItemArray[0]);
            }

            return weightId;
        }

        private void btnZoomView_Click(object sender, EventArgs e)
        {
            int tag = Convert.ToInt16(btnZoomView.Tag);
            switch (tag)
            {
                case 1:
                    {
                        this.Width = 1013;
                        btnZoomView.Tag = 2;
                        btnZoomView.Image = Properties.Resources.Left;
                    }
                    break;
                case 2:
                    {
                        this.Width = 846;
                        btnZoomView.Tag = 1;
                        btnZoomView.Image = Properties.Resources.Right;
                    }
                    break;
                default:
                    {

                    }
                    break;
            }


        }

        private void getListVehiclePending()
        {
            Constants.dbs.sql = "SELECT WEIGHINGID, PLATEVEHICLE FROM " + Constants.TABLE_WEIGHING + " WHERE OUTSIDE IS NULL";
            DataTable dt = Constants.dbs.getDataTable();
            lvwListPending.Items.Clear();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    ListViewItem lvi = new ListViewItem(dr["WEIGHINGID"].ToString());
                    lvi.SubItems.Add(dr["PLATEVEHICLE"].ToString());
                    lvwListPending.Items.Add(lvi);


                }

            }
        }

        private void lvwListPending_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private DataRow getWeightById(string id)
        {
            DataRow dataRow = null;
            Constants.dbs.sql = string.Format("SELECT WEIGHING.*, USERS.NAMES, USERS.SURNAMES FROM WEIGHING " +
               "JOIN USERS ON (WEIGHING.USERID = USERS.USERID) WHERE WEIGHING.WEIGHINGID = " + id);
            DataTable dt = Constants.dbs.getDataTable();
            if(dt != null && dt.Rows.Count > 0)
            {
                dataRow = dt.Rows[0];
            }

            return dataRow;
        }

        private DataRow getWeightByIdChild(string id)
        {
            DataRow dataRow = null;
            Constants.dbs.sql = string.Format("SELECT WEIGHTCHILD.* FROM WEIGHTCHILD " +
               "WHERE WEIGHTCHILD.ID = " + id);
            DataTable dt = Constants.dbs.getDataTable();
            if (dt != null && dt.Rows.Count > 0)
            {
                dataRow = dt.Rows[0];
            }

            return dataRow;
        }

        private void searchWeightById(string id)
        {
            clearControls();
            Constants.dbs.sql = string.Format("SELECT WEIGHING.*, USERS.NAMES, USERS.SURNAMES FROM WEIGHING " +
                "JOIN USERS ON (WEIGHING.USERID = USERS.USERID) WHERE WEIGHING.WEIGHINGID = " + id);
            DataTable dt = Constants.dbs.getDataTable();

            if(dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    CommonMethods.setValueComboBox(cboTypeVehicle, dr["VEHICLEID"].ToString());
                    txtUserName.Text = dr["NAMES"].ToString() + " " + dr["SURNAMES"].ToString();
                    inside.Text = dr["INSIDE"].ToString();
                    drivername.Text = dr["DRIVERNAME"].ToString();
                    license.Text = dr["LICENSE"].ToString();
                    platevehicle.Text = dr["PLATEVEHICLE"].ToString();
                    mark.Text = dr["MARK"].ToString();
                    outside.Text = dr["OUTSIDE"].ToString();
                    source.Text = dr["SOURCE"].ToString();
                    provider.Text = dr["PROVIDER"].ToString();
                    product.Text = dr["PRODUCT"].ToString();
                    client.Text = dr["CLIENT"].ToString();
                    target.Text = dr["TARGET"].ToString();
                    mobile.Text = dr["MOBILE"].ToString();

                    txtObservation.Text = dr["OBSERVATION"].ToString();
                    weightbruto.Text = Convert.ToInt32(dr["WEIGHTBRUTO"]).ToString();
                    weightbruto.Tag = Convert.ToInt32(dr["WEIGHTBRUTO"]);
                    if (Convert.ToInt32(weightbruto.Tag) == 0)
                    {
                        checkPB.Checked = true;
                    }
                    else
                    {
                        checkTara.Checked = true;
                    }
                    weighttara.Text = Convert.ToInt32(dr["WEIGHTTARA"]).ToString();
                    weightneto.Text = Convert.ToInt32(dr["WEIGHTNETO"]).ToString();                    
                    btnPrint.Enabled = true;
                    btnPrintTicket.Enabled = true;
                    WEIGHINGID.Text = id;
                    WEIGHINGID.Visible = true;
                    dataRowWeight = getWeightById(WEIGHINGID.Text);
                }

                questionOfClose();

            }

           
        }

        private void questionOfClose()
        {
            DialogResult result = MessageBox.Show("Cerrar proceso de registro", 
                "Capturar nuevo peso", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                btnSave.Tag = 2;
              
            }
            else if (result == DialogResult.No)
            {
                btnSave.Tag = 3;
                checkPB.Checked = true;
                weightbruto.Text = "0";
            }          
        }

        private void lvwListPending_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            searchWeightById(e.Item.Text);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clearControls();
        }

        private string getWeightTotal()
        {
            int w1 = Convert.ToInt32(txtWeight1.Text);
            int w2 = Convert.ToInt32(txtWeight2.Text);
            int w3 = Convert.ToInt32(txtWeight3.Text);
            int w4 = Convert.ToInt32(txtWeight4.Text);

            int t = w1 + w2 + w3 + w4;

            return Convert.ToString(t);
        }

        private void btnGetWeight_Click(object sender, EventArgs e)
        {
            if (checkPB.Checked)
            {
                weightbruto.Text = getWeightTotal();
            }
            if (checkTara.Checked)
            {
                weighttara.Text = getWeightTotal();
                typeCapture = "TA";
            }
        }

        private Image QRGen(string input, int qrlevel)
        {

            MessagingToolkit.QRCode.Codec.QRCodeEncoder qe = new MessagingToolkit.QRCode.Codec.QRCodeEncoder();
            qe.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qe.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            qe.QRCodeVersion = qrlevel;
            System.Drawing.Bitmap bm = qe.Encode(input);
            return bm;
        }

        private string getStringDataForQR()
        {
            string strReturn = string.Empty;
            strReturn = Convert.ToString(DateTime.Now) + '|' +
                    Constants.USERID_NOW + '|' + cboTypeVehicle.Text + '|' + platevehicle.Text + " " +
                    '|' + license.Text + '|';
            strReturn += txtWeight1.Text + '|' + txtWeight2.Text + '|' + txtWeight3.Text + '|' + txtWeight4.Text + '|';
            strReturn += weightbruto.Text + '|' + weighttara.Text + '|' + weightneto.Text;

            return strReturn;
        }

        private int nextLevelPrint(int marginTop, int first, int step)
        {
            return marginTop + (first * step);
        }

        private string getDateTimeCustom(string value)
        {
            string v = value;
            if (!string.IsNullOrEmpty(v))
            {
                try
                {
                    DateTime dt = DateTime.Parse(v);
                    v = dt.ToString("dd/MM/yyyy HH:mm");

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return v;
        }

        private void printDocument(System.Drawing.Printing.PrintPageEventArgs e)
        {

            int marginLeft = 20;
            int marginLeftCol1 = 360;
            int marginTop = 30;
            int levelTop = 20;          

            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = StringAlignment.Far;

            string strWork = "ACEROS AREQUIPA";

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 14, FontStyle.Bold, GraphicsUnit.Point))
            {

                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeft, marginTop);

            }

            strWork = "CORPORACION ACEROS DEL ALTIPLANO";

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 14, FontStyle.Bold, GraphicsUnit.Point))
            {

                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeft, nextLevelPrint(marginTop, levelTop, 1));

            }

            strWork = "Dirección: Av. Hilbo #100 Zona Kenko Pucarani . La Paz-Bolivia.";

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 10, FontStyle.Bold, GraphicsUnit.Point))
            {

                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeft, nextLevelPrint(marginTop, levelTop, 2));

            }

            strWork = "REGISTRO DE PESAJE #";

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeft, nextLevelPrint(marginTop, levelTop, 3));
            }

            strWork = dataRowWeight["WEIGHINGID"].ToString();

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, nextLevelPrint(marginTop, levelTop, 3), 350, 17), strFormat);
            }

            strWork = "DATOS GENERALES";

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeft, nextLevelPrint(marginTop, levelTop, 4));
            }

            strWork = "OPERADOR:";

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeft, nextLevelPrint(marginTop, levelTop, 5));
            }

            strWork = txtUserName.Text;

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, nextLevelPrint(marginTop, levelTop, 5), 350, 17), strFormat);
            }

            strWork = "FECHA/HORA:";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeft, nextLevelPrint(marginTop, levelTop, 6));
            }

            strWork = string.Format("{0:d}", DateTime.Now.ToString("dd/MM/yyyy"));
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 150, nextLevelPrint(marginTop, levelTop, 6));
            }

            strWork = string.Format("{0:t}", DateTime.Now);
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, nextLevelPrint(marginTop, levelTop, 6), 350, 17), strFormat);
            }

            strWork = "ENTRADA:";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeft, nextLevelPrint(marginTop, levelTop, 7));
            }

            strWork = dataRowWeight["INSIDE"].ToString();
            strWork = getDateTimeCustom(strWork);

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, nextLevelPrint(marginTop, levelTop, 7), 350, 17), strFormat);
            }

            strWork = "SALIDA:";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeft, nextLevelPrint(marginTop, levelTop, 8));
            }

            strWork = dataRowWeight["OUTSIDE"].ToString();
            strWork = getDateTimeCustom(strWork);
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, nextLevelPrint(marginTop, levelTop, 8), 350, 17), strFormat);
            }

            strWork = "DATOS DEL VEHÍCULO:";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeft, nextLevelPrint(marginTop, levelTop, 9));
            }

            strWork = "PLACA:";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeft, nextLevelPrint(marginTop, levelTop, 10));
            }

            strWork = platevehicle.Text;
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, nextLevelPrint(marginTop, levelTop, 10), 350, 17), strFormat);
            }

            strWork = "MARCA:";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeft, nextLevelPrint(marginTop, levelTop, 11));
            }

            strWork = mark.Text;
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, nextLevelPrint(marginTop, levelTop, 11), 350, 17), strFormat);
            }

            strWork = "TIPO:";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeft, nextLevelPrint(marginTop, levelTop, 12));
            }

            strWork = lblDescription.Text;
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, nextLevelPrint(marginTop, levelTop, 12), 350, 17), strFormat);
            }

            strWork = "DATOS DEL PESAJE:";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeft, nextLevelPrint(marginTop, levelTop, 13));
            }

            strWork = "BRUTO:";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeft, nextLevelPrint(marginTop, levelTop, 14));
            }

            strWork = Convert.ToString(Convert.ToInt32(weightbruto.Text)) + " Kg.";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, nextLevelPrint(marginTop, levelTop, 14), 350, 17), strFormat);
            }

            strWork = "TARA:";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeft, nextLevelPrint(marginTop, levelTop, 15));
            }

            strWork = Convert.ToString(Convert.ToInt32(weighttara.Text)) + " Kg.";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, nextLevelPrint(marginTop, levelTop, 15), 350, 17), strFormat);
            }

            strWork = "NETO:";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeft, nextLevelPrint(marginTop, levelTop, 16));
            }

            strWork = Convert.ToString(Convert.ToInt32(weightneto.Text)) + " Kg.";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, nextLevelPrint(marginTop, levelTop, 16), 350, 17), strFormat);
            }

            strWork = "OPERADOR";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, (marginLeft + 250), nextLevelPrint(marginTop, levelTop, 22));
            }

            //Columm two
            strWork = "DATOS DEL CONDUCTOR";

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeftCol1, nextLevelPrint(marginTop, levelTop, 4));
            }

            strWork = "NOMBRE:";

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeftCol1, nextLevelPrint(marginTop, levelTop, 5));
            }

            strWork = drivername.Text;
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, nextLevelPrint(marginTop, levelTop, 5), 700, 17), strFormat);
            }

            strWork = "IDENTIDAD:";

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeftCol1, nextLevelPrint(marginTop, levelTop, 6));
            }

            strWork = license.Text;
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, nextLevelPrint(marginTop, levelTop, 6), 700, 17), strFormat);
            }

            strWork = "DATOS DE LA CARGA";

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeftCol1, nextLevelPrint(marginTop, levelTop, 7));
            }

            strWork = "PROCEDENCIA:";

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeftCol1, nextLevelPrint(marginTop, levelTop, 8));
            }

            strWork = source.Text;
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, nextLevelPrint(marginTop, levelTop, 8), 700, 17), strFormat);
            }

            strWork = "DESTINO:";

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeftCol1, nextLevelPrint(marginTop, levelTop, 9));
            }

            strWork = target.Text;
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, nextLevelPrint(marginTop, levelTop, 9), 700, 17), strFormat);
            }

            strWork = "PROVEEDOR:";

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeftCol1, nextLevelPrint(marginTop, levelTop, 10));
            }

            strWork = provider.Text;
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, nextLevelPrint(marginTop, levelTop, 10), 700, 17), strFormat);
            }

            strWork = "TELÉFONO:";

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeftCol1, nextLevelPrint(marginTop, levelTop, 11));
            }

            strWork = mobile.Text;
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, nextLevelPrint(marginTop, levelTop, 11), 700, 17), strFormat);
            }

            strWork = "PRODUCTO:";

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeftCol1, nextLevelPrint(marginTop, levelTop, 12));
            }

            strWork = product.Text;
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, nextLevelPrint(marginTop, levelTop, 12), 700, 17), strFormat);
            }

            strWork = "CLIENTE:";

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeftCol1, nextLevelPrint(marginTop, levelTop, 13));
            }

            strWork = client.Text;
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, nextLevelPrint(marginTop, levelTop, 13), 700, 17), strFormat);
            }

            strWork = "PESOS (Kg.):";

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeftCol1, nextLevelPrint(marginTop, levelTop, 14));
            }

            strWork = "EJE 1:";

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeftCol1, nextLevelPrint(marginTop, levelTop, 15));
            }

            strWork = Convert.ToInt32(dataRowWeight["WEIGHT1"]).ToString() + " Kg.";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, nextLevelPrint(marginTop, levelTop, 15), 700, 17), strFormat);
            }

            strWork = "EJE 2:";

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeftCol1, nextLevelPrint(marginTop, levelTop, 16));
            }

            strWork = Convert.ToInt32(dataRowWeight["WEIGHT2"]).ToString() + " Kg.";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, nextLevelPrint(marginTop, levelTop, 16), 700, 17), strFormat);
            }

            strWork = "EJE 3:";

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeftCol1, nextLevelPrint(marginTop, levelTop, 17));
            }

            strWork = Convert.ToInt32(dataRowWeight["WEIGHT3"]).ToString() + " Kg.";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, nextLevelPrint(marginTop, levelTop, 17), 700, 17), strFormat);
            }

            strWork = "EJE 4:";

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeftCol1, nextLevelPrint(marginTop, levelTop, 18));
            }

            strWork = Convert.ToInt32(dataRowWeight["WEIGHT4"]).ToString() + " Kg.";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, nextLevelPrint(marginTop, levelTop, 18), 700, 17), strFormat);
            }

            strWork = "TRANSPORTISTA";

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 12, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, marginLeftCol1 + 200, nextLevelPrint(marginTop, levelTop, 22));
            }


            PictureBox picQR = new PictureBox();

            picQR.SizeMode = PictureBoxSizeMode.StretchImage;
            picQR.Image = bmpQr;

            e.Graphics.DrawImage(picQR.Image, 30, nextLevelPrint(marginTop, levelTop, 18), 140, 140);


        }

        private void printTicket(System.Drawing.Printing.PrintPageEventArgs e)
        {
            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = StringAlignment.Far;

            string strWork = "BOLETA DE CONTROL DE PESOS";

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 11, FontStyle.Bold, GraphicsUnit.Point))
            {

                e.Graphics.DrawString(strWork, font2, Brushes.Black, 38, 4);

            }

            strWork = " SISTEMA DE PESAJE - CAS";

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 11, FontStyle.Bold, GraphicsUnit.Point))
            {

                e.Graphics.DrawString(strWork, font2, Brushes.Black, 38, 21);

            }

            strWork = pointControl;

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {

                e.Graphics.DrawString(strWork, font2, Brushes.Black, 38, 42);

            }

            //strWork = pointControl;

            //using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            //{
            //    e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, 42, 274, 17), strFormat);
            //}

            strWork = "REGISTRO DE PESAJE #";

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 4, 59);
            }

            strWork = dataRowWeight["WEIGHINGID"].ToString();

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, 59, 274, 17), strFormat);
            }

            strWork = "FECHA/HORA:";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 4, 76);
            }

            strWork = string.Format("{0:d}", DateTime.Now.ToString("dd/MM/yyyy"));
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 133, 76);
            }

            strWork = string.Format("{0:t}", DateTime.Now);
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, 76, 274, 17), strFormat);
            }

            strWork = "OPERADOR(A):";

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 4, 93);
            }

            strWork = txtUserName.Text;

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, 93, 274, 17), strFormat);
            }

            strWork = "CONFIGURACIÓN:";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 4, 110);
            }

            strWork = lblDescription.Text;
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, 110, 274, 17), strFormat);
            }

            strWork = "PLACA:";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 4, 127);
            }

            strWork = platevehicle.Text;
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, 127, 274, 17), strFormat);
            }

            strWork = "CONDUCTOR:";

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 4, 144);
            }

            strWork = drivername.Text;

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, 144, 274, 17), strFormat);
            }

            strWork = "LICENCIA:";

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 4, 161);
            }

            strWork = license.Text;

            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, 161, 274, 17), strFormat);
            }

            strWork = "DATOS DEL CONTROL:";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 4, 176);
            }

            strWork = "PESOS (Kg.)";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 60, 191);
            }

            #region numbers
            strWork = "1°";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 19, 205); //219
            }

            strWork = "2°";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 19, 219); //233
            }

            strWork = "3°";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 19, 233);
            }

            strWork = "4°";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 19, 247);
            }

            
            #endregion

            #region axes
            strWork = Convert.ToInt32(dataRowWeight["WEIGHT1"]).ToString();
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 70, 205);
            }

            strWork = Convert.ToInt32(dataRowWeight["WEIGHT2"]).ToString();
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 70, 219);
            }


            strWork = Convert.ToInt32(dataRowWeight["WEIGHT3"]).ToString();
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 70, 233);
            }


            strWork = Convert.ToInt32(dataRowWeight["WEIGHT4"]).ToString();
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 70, 247);
            }

            strWork = "BRUTO:";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 120, 205);
            }

            strWork = Convert.ToString(Convert.ToInt32(weightbruto.Text));
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 180, 205);
            }

            strWork = "TARA:";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 120, 219);
            }

            strWork = Convert.ToString(Convert.ToInt32(weighttara.Text));
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 180, 219);
            }

            strWork = "NETO:";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 120, 233);
            }

            strWork = Convert.ToString(Convert.ToInt32(weightneto.Text));
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 180, 233);
            }

            strWork = "ENTRADA:";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 7, 261);
            }

            strWork = dataRowWeight["INSIDE"].ToString();
            strWork = getDateTimeCustom(strWork);
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, 261, 274, 17), strFormat);
            }
            strWork = "SALIDA:";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 7, 275);
            }

            strWork = dataRowWeight["OUTSIDE"].ToString();
            strWork = getDateTimeCustom(strWork);
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, 275, 274, 17), strFormat);
            }

            strWork = "ORIGEN:";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 7, 289);
            }

            strWork = source.Text;
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, 289, 274, 17), strFormat);
            }

            strWork = "PROVEEDOR:";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 7, 303);
            }

            strWork = provider.Text;
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, 303, 274, 17), strFormat);
            }

            strWork = "PRODUCTO:";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 7, 317);
            }

            strWork = product.Text;
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, 317, 274, 17), strFormat);
            }

            strWork = "DESTINO:";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 7, 331);
            }

            strWork = target.Text;
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, 331, 274, 17), strFormat);
            }

            strWork = "CLIENTE:";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 7, 345);
            }

            strWork = client.Text;
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, 345, 274, 17), strFormat);
            }

            strWork = "FIRMA OPERADOR";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, 7, 400);
            }

            strWork = "FIRMA TRANSPORTISTA";
            using (System.Drawing.Font font2 = new System.Drawing.Font("Calibri", 9, FontStyle.Bold, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(strWork, font2, Brushes.Black, new RectangleF(0, 400, 274, 17), strFormat);
            }
            #endregion

            PictureBox picQR = new PictureBox();

            picQR.SizeMode = PictureBoxSizeMode.StretchImage;
            picQR.Image = bmpQr;

            e.Graphics.DrawImage(picQR.Image, 71, 430, 140, 140);

        }

        private void printBallot_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if(optionPrint == 1)
            {
                printDocument(e);
            }
            if (optionPrint == 2)
            {
                printTicket(e);
            }

        }

        private void executePrint()
        {
            if (dataRowWeight != null)
            {
                bmpQr = (System.Drawing.Bitmap)QRGen(getStringDataForQR(), Convert.ToInt32(8));
                printBallot.PrinterSettings.PrinterName = CommonMethods.getPrintDefault();
                if (chkDisplayPrint.Checked)
                {

                    printPreview.Document = printBallot;
                    printPreview.ShowDialog();

                }
                else
                {
                    try
                    {
                        printBallot.Print();
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                    }

                }

            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            optionPrint = 1;
            executePrint();

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                searchWeightById(txtSearch.Text);
            }

        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar))
            {

                e.Handled = false;

            }
            else
            {
                e.Handled = true;
            }
        }

        private void mobile_TextChanged(object sender, EventArgs e)
        {
            weighing.Mobile = mobile.Text;
        }

        private void mobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) || (char.IsControl(e.KeyChar)))
            {

                e.Handled = false;

            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnPrintTicket_Click(object sender, EventArgs e)
        {
            optionPrint = 2;
            executePrint();
        }

        private void backgroundWorkerGetWeight2_DoWork(object sender, DoWorkEventArgs e)
        {
            ResultPort resultPort = e.Argument as ResultPort;

            string valueWeight = getWeight(resultPort.valueWeight, resultPort.objectText);
            resultPort.valueWeight = valueWeight;

            e.Result = resultPort;
        }

        private void backgroundWorkerGetWeigh3_DoWork(object sender, DoWorkEventArgs e)
        {
            ResultPort resultPort = e.Argument as ResultPort;

            string valueWeight = getWeight(resultPort.valueWeight, resultPort.objectText);
            resultPort.valueWeight = valueWeight;

            e.Result = resultPort;
        }

        private void backgroundWorkerGetWeigh4_DoWork(object sender, DoWorkEventArgs e)
        {
            ResultPort resultPort = e.Argument as ResultPort;

            string valueWeight = getWeight(resultPort.valueWeight, resultPort.objectText);
            resultPort.valueWeight = valueWeight;

            e.Result = resultPort;
        }

        private void backgroundWorkerGetWeight2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ResultPort resultPort = e.Result as ResultPort;

            if (!string.IsNullOrEmpty(resultPort.valueWeight))
            {
                resultPort.objectText.Text = resultPort.valueWeight;
            }
        }

        private void backgroundWorkerGetWeigh3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ResultPort resultPort = e.Result as ResultPort;

            if (!string.IsNullOrEmpty(resultPort.valueWeight))
            {
                resultPort.objectText.Text = resultPort.valueWeight;
            }
        }

        private void backgroundWorkerGetWeigh4_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ResultPort resultPort = e.Result as ResultPort;

            if (!string.IsNullOrEmpty(resultPort.valueWeight))
            {
                resultPort.objectText.Text = resultPort.valueWeight;
            }
        }

        private void mobile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                //e.SuppressKeyPress = false;
            }

        }

        private void backgroundWorkerSendCloud_DoWork(object sender, DoWorkEventArgs e)
        {

            DataRow dataLast = dataRowWeight;
            if (dataLast != null && (Convert.ToInt32(dataLast["WEIGHTTARA"])) != 0 && 
                (Convert.ToInt32(dataLast["WEIGHTBRUTO"])) != 0)
            {
                Dictionary<string, object> values = new Dictionary<string, object>();
                values.Add("VCCodigo", dataLast["WEIGHINGID"].ToString());
                if (pointControl.Length > 19) pointControl = pointControl.Substring(0, 19);
                values.Add("PuestoControlCID", pointControl);
                values.Add("VCPlaca1", dataLast["PLATEVEHICLE"]);
                values.Add("VCCondNombre", dataLast["DRIVERNAME"]);
                values.Add("VCCondLicencia", dataLast["LICENSE"]);
                values.Add("ConfiguracionVehiculoCID", dataLast["DESCRIPTION"]);
                values.Add("ControlFecha", dataLast["DATEREGISTER"].ToString());
                string nameUser = dataLast["NAMES"] + " " + dataLast["SURNAMES"];
                if (nameUser.Length > 19) nameUser = nameUser.Substring(0, 19);
                values.Add("EmpleadoCodigo", nameUser);
                values.Add("ControlPesoTotal", (Convert.ToInt32(dataLast["WEIGHTNETO"])));
                values.Add("provider", (object)dataLast["PROVIDER"] ?? DBNull.Value);
                values.Add("product", (object)dataLast["PRODUCT"] ?? DBNull.Value);
                values.Add("client", (object)dataLast["CLIENT"] ?? DBNull.Value);
                httptools.Data = values;
                httptools.sendServerWeb();
            }

        }

        private void backgroundWorkerSendCloud_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!string.IsNullOrEmpty(httptools.Result))
            {
                CommonMethods.setLogDatabase("Send Cloud: " + httptools.Result);
            }
            
        }
       
        private void searchDataForPlate(String value)
        {
            Constants.dbs.sql = "SELECT TOP 1 DRIVERNAME, LICENSE, VEHICLEID, PLATEVEHICLE, " +
                "PROVIDER, PRODUCT, CLIENT, SOURCE, TARGET, MOBILE, MARK, WEIGHTTARA FROM " +
                "WEIGHING WHERE PLATEVEHICLE = '" + value + "' ORDER BY WEIGHINGID DESC";
            DataTable dt = Constants.dbs.getDataTable();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    CommonMethods.setValueComboBox(cboTypeVehicle, dr["VEHICLEID"].ToString());                                      
                    drivername.Text = dr["DRIVERNAME"].ToString();
                    license.Text = dr["LICENSE"].ToString();
                    platevehicle.Text = dr["PLATEVEHICLE"].ToString();
                    mark.Text = dr["MARK"].ToString();                   
                    source.Text = dr["SOURCE"].ToString();
                    provider.Text = dr["PROVIDER"].ToString();
                    product.Text = dr["PRODUCT"].ToString();
                    client.Text = dr["CLIENT"].ToString();
                    target.Text = dr["TARGET"].ToString();
                    mobile.Text = dr["MOBILE"].ToString();                                      
                    weighttara.Text = Convert.ToInt32(dr["WEIGHTTARA"]).ToString();
                    break;
                }
            }

        }

        private void autoCompleteControls()
        {
            Constants.dbs.sql = "SELECT TOP 50 DRIVERNAME, LICENSE, PLATEVEHICLE, PROVIDER, PRODUCT, CLIENT, SOURCE, TARGET, MARK FROM WEIGHING ORDER BY WEIGHINGID ASC";
            DataTable dt = Constants.dbs.getDataTable();

            this.collectionDriverName = new AutoCompleteStringCollection();
            this.collectionLicense = new AutoCompleteStringCollection();
            this.collectionPlateVehicle = new AutoCompleteStringCollection();
            this.collectionProvider = new AutoCompleteStringCollection();
            this.collectionProduct = new AutoCompleteStringCollection();
            this.collectionClient = new AutoCompleteStringCollection();
            this.collectionSource = new AutoCompleteStringCollection();
            this.collectionTarget = new AutoCompleteStringCollection();
            this.collectionMark = new AutoCompleteStringCollection();

            try
            {
               
                foreach (DataRow r in dt.Rows)
                {
                    this.collectionDriverName.Add(r["DRIVERNAME"].ToString());
                    this.collectionLicense.Add(r["LICENSE"].ToString());
                    this.collectionPlateVehicle.Add(r["PLATEVEHICLE"].ToString());
                    this.collectionProvider.Add(r["PROVIDER"].ToString());
                    this.collectionProduct.Add(r["PRODUCT"].ToString());
                    this.collectionClient.Add(r["CLIENT"].ToString());
                    this.collectionSource.Add(r["SOURCE"].ToString());
                    this.collectionTarget.Add(r["TARGET"].ToString());
                    this.collectionMark.Add(r["MARK"].ToString());

                }

               

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void drivername_Enter(object sender, EventArgs e)
        {
            
        }

        private void backgroundWorkerAutoComplete_DoWork(object sender, DoWorkEventArgs e)
        {
            autoCompleteControls();
        }

        private void backgroundWorkerAutoComplete_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            drivername.AutoCompleteCustomSource = collectionDriverName;
            license.AutoCompleteCustomSource = collectionLicense;
            platevehicle.AutoCompleteCustomSource = collectionPlateVehicle;
            provider.AutoCompleteCustomSource = collectionProvider;
            product.AutoCompleteCustomSource = collectionProduct;
            client.AutoCompleteCustomSource = collectionClient;
            source.AutoCompleteCustomSource = collectionSource;
            target.AutoCompleteCustomSource = collectionTarget;
            mark.AutoCompleteCustomSource = collectionMark;
        }

        private void platevehicle_Validated(object sender, EventArgs e)
        {
            
        }

        private void platevehicle_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void platevehicle_MouseUp(object sender, MouseEventArgs e)
        {
           
        }

        private void platevehicle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                searchDataForPlate(platevehicle.Text);
            }

        }

        private void license_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            //{
            //    searchDataForPlate(license.Text);
            //}
        }

        private void btnZero1_Click(object sender, EventArgs e)
        {
            writeSerialPort1();
        }

        private void btnZero2_Click(object sender, EventArgs e)
        {
            writeSerialPort2();
            
        }

        private void btnZero3_Click(object sender, EventArgs e)
        {
            writeSerialPort3();
            
        }

        private void btnZero4_Click(object sender, EventArgs e)
        {
            writeSerialPort4();
            
        }
    }
}
