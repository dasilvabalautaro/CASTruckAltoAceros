namespace CASTruck.forms
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUSERNAME = new System.Windows.Forms.TextBox();
            this.txtPASSWORD = new System.Windows.Forms.TextBox();
            this.cmdOk = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(160, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre de usuario:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(160, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Contraseña:";
            // 
            // txtUSERNAME
            // 
            this.txtUSERNAME.Location = new System.Drawing.Point(163, 34);
            this.txtUSERNAME.MaxLength = 50;
            this.txtUSERNAME.Name = "txtUSERNAME";
            this.txtUSERNAME.Size = new System.Drawing.Size(178, 20);
            this.txtUSERNAME.TabIndex = 2;
            // 
            // txtPASSWORD
            // 
            this.txtPASSWORD.Location = new System.Drawing.Point(163, 88);
            this.txtPASSWORD.MaxLength = 50;
            this.txtPASSWORD.Name = "txtPASSWORD";
            this.txtPASSWORD.PasswordChar = '*';
            this.txtPASSWORD.Size = new System.Drawing.Size(178, 20);
            this.txtPASSWORD.TabIndex = 3;
            // 
            // cmdOk
            // 
            this.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdOk.Image = global::CASTruck.Properties.Resources.Apply;
            this.cmdOk.Location = new System.Drawing.Point(293, 130);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(48, 50);
            this.cmdOk.TabIndex = 26;
            this.cmdOk.TabStop = false;
            this.cmdOk.Tag = "0";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCancel.Image = global::CASTruck.Properties.Resources.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(222, 130);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(48, 50);
            this.cmdCancel.TabIndex = 27;
            this.cmdCancel.TabStop = false;
            this.cmdCancel.Tag = "0";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(15, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(132, 163);
            this.pictureBox1.TabIndex = 28;
            this.pictureBox1.TabStop = false;
            // 
            // frmLogin
            // 
            this.AcceptButton = this.cmdOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 193);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.txtPASSWORD);
            this.Controls.Add(this.txtUSERNAME);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.Text = "Ingreso";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLogin_FormClosing);
            this.Load += new System.EventHandler(this.frmLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUSERNAME;
        private System.Windows.Forms.TextBox txtPASSWORD;
        internal System.Windows.Forms.Button cmdOk;
        internal System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}