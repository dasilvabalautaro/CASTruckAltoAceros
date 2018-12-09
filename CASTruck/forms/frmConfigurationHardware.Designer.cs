namespace CASTruck.forms
{
    partial class frmConfigurationHardware
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblOkName = new System.Windows.Forms.Label();
            this.btnSavePointControl = new System.Windows.Forms.Button();
            this.txtNamePointControl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblOkPorts = new System.Windows.Forms.Label();
            this.btnSavePorts = new System.Windows.Forms.Button();
            this.cboPort4 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboPort3 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboPort2 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboPort1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOut = new System.Windows.Forms.Button();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblOkName);
            this.groupBox1.Controls.Add(this.btnSavePointControl);
            this.groupBox1.Controls.Add(this.txtNamePointControl);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 104);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Puesto de Control";
            // 
            // lblOkName
            // 
            this.lblOkName.AutoSize = true;
            this.lblOkName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOkName.ForeColor = System.Drawing.Color.Red;
            this.lblOkName.Location = new System.Drawing.Point(176, 66);
            this.lblOkName.Name = "lblOkName";
            this.lblOkName.Size = new System.Drawing.Size(0, 13);
            this.lblOkName.TabIndex = 12;
            // 
            // btnSavePointControl
            // 
            this.btnSavePointControl.Image = global::CASTruck.Properties.Resources.Apply;
            this.btnSavePointControl.Location = new System.Drawing.Point(206, 54);
            this.btnSavePointControl.Name = "btnSavePointControl";
            this.btnSavePointControl.Size = new System.Drawing.Size(81, 37);
            this.btnSavePointControl.TabIndex = 2;
            this.btnSavePointControl.UseVisualStyleBackColor = true;
            this.btnSavePointControl.Click += new System.EventHandler(this.btnSavePointControl_Click);
            // 
            // txtNamePointControl
            // 
            this.txtNamePointControl.Location = new System.Drawing.Point(81, 24);
            this.txtNamePointControl.Name = "txtNamePointControl";
            this.txtNamePointControl.Size = new System.Drawing.Size(206, 20);
            this.txtNamePointControl.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblOkPorts);
            this.groupBox2.Controls.Add(this.btnSavePorts);
            this.groupBox2.Controls.Add(this.cboPort4);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cboPort3);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cboPort2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cboPort1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(13, 124);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(308, 161);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Balanza";
            // 
            // lblOkPorts
            // 
            this.lblOkPorts.AutoSize = true;
            this.lblOkPorts.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOkPorts.ForeColor = System.Drawing.Color.Red;
            this.lblOkPorts.Location = new System.Drawing.Point(176, 119);
            this.lblOkPorts.Name = "lblOkPorts";
            this.lblOkPorts.Size = new System.Drawing.Size(0, 13);
            this.lblOkPorts.TabIndex = 11;
            // 
            // btnSavePorts
            // 
            this.btnSavePorts.Image = global::CASTruck.Properties.Resources.Apply;
            this.btnSavePorts.Location = new System.Drawing.Point(206, 107);
            this.btnSavePorts.Name = "btnSavePorts";
            this.btnSavePorts.Size = new System.Drawing.Size(81, 37);
            this.btnSavePorts.TabIndex = 10;
            this.btnSavePorts.UseVisualStyleBackColor = true;
            this.btnSavePorts.Click += new System.EventHandler(this.btnSavePorts_Click);
            // 
            // cboPort4
            // 
            this.cboPort4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPort4.FormattingEnabled = true;
            this.cboPort4.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10",
            "COM11",
            "COM12",
            "COM13",
            "COM14",
            "COM15",
            "COM16",
            "COM17",
            "COM18",
            "Not found"});
            this.cboPort4.Location = new System.Drawing.Point(90, 123);
            this.cboPort4.Name = "cboPort4";
            this.cboPort4.Size = new System.Drawing.Size(81, 21);
            this.cboPort4.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(14, 127);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Eje 4:";
            // 
            // cboPort3
            // 
            this.cboPort3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPort3.FormattingEnabled = true;
            this.cboPort3.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10",
            "COM11",
            "COM12",
            "COM13",
            "COM14",
            "COM15",
            "COM16",
            "COM17",
            "COM18",
            "Not found"});
            this.cboPort3.Location = new System.Drawing.Point(90, 96);
            this.cboPort3.Name = "cboPort3";
            this.cboPort3.Size = new System.Drawing.Size(81, 21);
            this.cboPort3.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(14, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Eje 3:";
            // 
            // cboPort2
            // 
            this.cboPort2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPort2.FormattingEnabled = true;
            this.cboPort2.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10",
            "COM11",
            "COM12",
            "COM13",
            "COM14",
            "COM15",
            "COM16",
            "COM17",
            "COM18",
            "Not found"});
            this.cboPort2.Location = new System.Drawing.Point(90, 69);
            this.cboPort2.Name = "cboPort2";
            this.cboPort2.Size = new System.Drawing.Size(81, 21);
            this.cboPort2.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Eje 2:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(113, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Puerto";
            // 
            // cboPort1
            // 
            this.cboPort1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPort1.FormattingEnabled = true;
            this.cboPort1.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10",
            "COM11",
            "COM12",
            "COM13",
            "COM14",
            "COM15",
            "COM16",
            "COM17",
            "COM18",
            "Not found"});
            this.cboPort1.Location = new System.Drawing.Point(90, 42);
            this.cboPort1.Name = "cboPort1";
            this.cboPort1.Size = new System.Drawing.Size(81, 21);
            this.cboPort1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Eje 1:";
            // 
            // btnOut
            // 
            this.btnOut.Image = global::CASTruck.Properties.Resources.Out;
            this.btnOut.Location = new System.Drawing.Point(240, 298);
            this.btnOut.Name = "btnOut";
            this.btnOut.Size = new System.Drawing.Size(81, 37);
            this.btnOut.TabIndex = 11;
            this.btnOut.UseVisualStyleBackColor = true;
            this.btnOut.Click += new System.EventHandler(this.btnOut_Click);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            // 
            // frmConfigurationHardware
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 351);
            this.Controls.Add(this.btnOut);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmConfigurationHardware";
            this.Text = "Configuración de Hardware";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmConfigurationHardware_FormClosing);
            this.Load += new System.EventHandler(this.frmConfigurationHardware_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtNamePointControl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSavePointControl;
        private System.Windows.Forms.Button btnSavePorts;
        private System.Windows.Forms.ComboBox cboPort4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboPort3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboPort2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboPort1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOut;
        private System.Windows.Forms.Label lblOkName;
        private System.Windows.Forms.Label lblOkPorts;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
    }
}