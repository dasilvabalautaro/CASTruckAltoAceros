namespace CASTruck.forms
{
    partial class frmUser
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
            this.label4 = new System.Windows.Forms.Label();
            this.cboUSERTYPEID = new System.Windows.Forms.ComboBox();
            this.txtPASSWORD = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUSERNAME = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSURNAMES = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNAMES = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdDelete = new System.Windows.Forms.Button();
            this.cmdUpdate = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvwListUsers = new System.Windows.Forms.ListView();
            this.txtUSERID = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtUSERID);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cboUSERTYPEID);
            this.groupBox1.Controls.Add(this.txtPASSWORD);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtUSERNAME);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtSURNAMES);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtNAMES);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(409, 155);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Registro de datos";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Tipo:";
            // 
            // cboUSERTYPEID
            // 
            this.cboUSERTYPEID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUSERTYPEID.FormattingEnabled = true;
            this.cboUSERTYPEID.Location = new System.Drawing.Point(104, 123);
            this.cboUSERTYPEID.Name = "cboUSERTYPEID";
            this.cboUSERTYPEID.Size = new System.Drawing.Size(134, 21);
            this.cboUSERTYPEID.TabIndex = 5;
            this.cboUSERTYPEID.SelectedIndexChanged += new System.EventHandler(this.cboUSERTYPEID_SelectedIndexChanged);
            // 
            // txtPASSWORD
            // 
            this.txtPASSWORD.Location = new System.Drawing.Point(104, 97);
            this.txtPASSWORD.MaxLength = 100;
            this.txtPASSWORD.Name = "txtPASSWORD";
            this.txtPASSWORD.PasswordChar = '*';
            this.txtPASSWORD.Size = new System.Drawing.Size(289, 20);
            this.txtPASSWORD.TabIndex = 4;
            this.txtPASSWORD.TextChanged += new System.EventHandler(this.txtPASSWORD_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Constraseña:";
            // 
            // txtUSERNAME
            // 
            this.txtUSERNAME.Location = new System.Drawing.Point(104, 71);
            this.txtUSERNAME.MaxLength = 50;
            this.txtUSERNAME.Name = "txtUSERNAME";
            this.txtUSERNAME.Size = new System.Drawing.Size(202, 20);
            this.txtUSERNAME.TabIndex = 3;
            this.txtUSERNAME.TextChanged += new System.EventHandler(this.txtUSERNAME_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Usuario:";
            // 
            // txtSURNAMES
            // 
            this.txtSURNAMES.Location = new System.Drawing.Point(104, 45);
            this.txtSURNAMES.MaxLength = 50;
            this.txtSURNAMES.Name = "txtSURNAMES";
            this.txtSURNAMES.Size = new System.Drawing.Size(202, 20);
            this.txtSURNAMES.TabIndex = 2;
            this.txtSURNAMES.TextChanged += new System.EventHandler(this.txtSURNAMES_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Apellidos:";
            // 
            // txtNAMES
            // 
            this.txtNAMES.Location = new System.Drawing.Point(104, 19);
            this.txtNAMES.MaxLength = 50;
            this.txtNAMES.Name = "txtNAMES";
            this.txtNAMES.Size = new System.Drawing.Size(202, 20);
            this.txtNAMES.TabIndex = 1;
            this.txtNAMES.TextChanged += new System.EventHandler(this.txtNAMES_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Nombres:";
            // 
            // cmdAdd
            // 
            this.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdAdd.Image = global::CASTruck.Properties.Resources.Apply;
            this.cmdAdd.Location = new System.Drawing.Point(377, 176);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(48, 50);
            this.cmdAdd.TabIndex = 25;
            this.cmdAdd.TabStop = false;
            this.cmdAdd.Tag = "0";
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdClose.Image = global::CASTruck.Properties.Resources.Out;
            this.cmdClose.Location = new System.Drawing.Point(376, 439);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(48, 50);
            this.cmdClose.TabIndex = 24;
            this.cmdClose.TabStop = false;
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdDelete.Image = global::CASTruck.Properties.Resources.Full_recycle_bin;
            this.cmdDelete.Location = new System.Drawing.Point(269, 176);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(48, 50);
            this.cmdDelete.TabIndex = 26;
            this.cmdDelete.TabStop = false;
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdUpdate.Image = global::CASTruck.Properties.Resources.Refresh;
            this.cmdUpdate.Location = new System.Drawing.Point(323, 176);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(48, 50);
            this.cmdUpdate.TabIndex = 27;
            this.cmdUpdate.TabStop = false;
            this.cmdUpdate.UseVisualStyleBackColor = true;
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lvwListUsers);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(16, 232);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(408, 201);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lista de usuarios";
            // 
            // lvwListUsers
            // 
            this.lvwListUsers.BackColor = System.Drawing.SystemColors.Window;
            this.lvwListUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvwListUsers.FullRowSelect = true;
            this.lvwListUsers.GridLines = true;
            this.lvwListUsers.HideSelection = false;
            this.lvwListUsers.Location = new System.Drawing.Point(14, 19);
            this.lvwListUsers.MultiSelect = false;
            this.lvwListUsers.Name = "lvwListUsers";
            this.lvwListUsers.Size = new System.Drawing.Size(380, 175);
            this.lvwListUsers.TabIndex = 41;
            this.lvwListUsers.TabStop = false;
            this.lvwListUsers.UseCompatibleStateImageBehavior = false;
            this.lvwListUsers.View = System.Windows.Forms.View.Details;
            // 
            // txtUSERID
            // 
            this.txtUSERID.Location = new System.Drawing.Point(320, 20);
            this.txtUSERID.MaxLength = 50;
            this.txtUSERID.Name = "txtUSERID";
            this.txtUSERID.Size = new System.Drawing.Size(73, 20);
            this.txtUSERID.TabIndex = 12;
            this.txtUSERID.TabStop = false;
            this.txtUSERID.Visible = false;
            // 
            // frmUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 495);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmdUpdate);
            this.Controls.Add(this.cmdDelete);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdAdd);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmUser";
            this.Text = "Registro de Usuario";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmUser_FormClosing);
            this.Load += new System.EventHandler(this.frmUser_Load);
            this.Shown += new System.EventHandler(this.frmUser_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPASSWORD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUSERNAME;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSURNAMES;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNAMES;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboUSERTYPEID;
        internal System.Windows.Forms.Button cmdAdd;
        internal System.Windows.Forms.Button cmdClose;
        internal System.Windows.Forms.Button cmdDelete;
        internal System.Windows.Forms.Button cmdUpdate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lvwListUsers;
        private System.Windows.Forms.TextBox txtUSERID;
    }
}