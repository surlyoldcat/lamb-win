namespace Lamb
{
    partial class CredentialsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CredentialsForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSecretAccessKey = new System.Windows.Forms.TextBox();
            this.txtAccessKeyId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAccount = new System.Windows.Forms.TextBox();
            this.radioEnterCreds = new System.Windows.Forms.RadioButton();
            this.radioProfileCreds = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSecretAccessKey);
            this.groupBox1.Controls.Add(this.txtAccessKeyId);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtAccount);
            this.groupBox1.Controls.Add(this.radioEnterCreds);
            this.groupBox1.Controls.Add(this.radioProfileCreds);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(294, 147);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select AWS Credentials for this Session";
            // 
            // txtSecretAccessKey
            // 
            this.txtSecretAccessKey.Enabled = false;
            this.txtSecretAccessKey.Location = new System.Drawing.Point(105, 119);
            this.txtSecretAccessKey.Name = "txtSecretAccessKey";
            this.txtSecretAccessKey.ReadOnly = true;
            this.txtSecretAccessKey.Size = new System.Drawing.Size(181, 20);
            this.txtSecretAccessKey.TabIndex = 7;
            // 
            // txtAccessKeyId
            // 
            this.txtAccessKeyId.Enabled = false;
            this.txtAccessKeyId.Location = new System.Drawing.Point(105, 93);
            this.txtAccessKeyId.Name = "txtAccessKeyId";
            this.txtAccessKeyId.ReadOnly = true;
            this.txtAccessKeyId.Size = new System.Drawing.Size(181, 20);
            this.txtAccessKeyId.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Secret Key";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Access Key";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Account";
            // 
            // txtAccount
            // 
            this.txtAccount.Enabled = false;
            this.txtAccount.Location = new System.Drawing.Point(105, 67);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.ReadOnly = true;
            this.txtAccount.Size = new System.Drawing.Size(181, 20);
            this.txtAccount.TabIndex = 2;
            // 
            // radioEnterCreds
            // 
            this.radioEnterCreds.AutoSize = true;
            this.radioEnterCreds.Location = new System.Drawing.Point(7, 44);
            this.radioEnterCreds.Name = "radioEnterCreds";
            this.radioEnterCreds.Size = new System.Drawing.Size(108, 17);
            this.radioEnterCreds.TabIndex = 1;
            this.radioEnterCreds.Text = "Enter Credentials:";
            this.radioEnterCreds.UseVisualStyleBackColor = true;
            this.radioEnterCreds.CheckedChanged += new System.EventHandler(this.radioEnterCreds_CheckedChanged);
            // 
            // radioProfileCreds
            // 
            this.radioProfileCreds.AutoSize = true;
            this.radioProfileCreds.Checked = true;
            this.radioProfileCreds.Location = new System.Drawing.Point(7, 20);
            this.radioProfileCreds.Name = "radioProfileCreds";
            this.radioProfileCreds.Size = new System.Drawing.Size(133, 17);
            this.radioProfileCreds.TabIndex = 0;
            this.radioProfileCreds.TabStop = true;
            this.radioProfileCreds.Text = "Configured User Profile";
            this.radioProfileCreds.UseVisualStyleBackColor = true;
            this.radioProfileCreds.CheckedChanged += new System.EventHandler(this.radioProfileCreds_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(12, 169);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "O&K";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(231, 169);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Ca&ncel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // CredentialsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(312, 198);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CredentialsForm";
            this.Text = "AWS Credentials";
            this.Load += new System.EventHandler(this.CredentialsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSecretAccessKey;
        private System.Windows.Forms.TextBox txtAccessKeyId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAccount;
        private System.Windows.Forms.RadioButton radioEnterCreds;
        private System.Windows.Forms.RadioButton radioProfileCreds;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}