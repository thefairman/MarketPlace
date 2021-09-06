namespace LDmp
{
    partial class SMSForm
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
            this.txtSMSText = new System.Windows.Forms.TextBox();
            this.btnSMSOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblSMSError = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtSMSText
            // 
            this.txtSMSText.Location = new System.Drawing.Point(93, 54);
            this.txtSMSText.Name = "txtSMSText";
            this.txtSMSText.Size = new System.Drawing.Size(144, 20);
            this.txtSMSText.TabIndex = 0;
            // 
            // btnSMSOk
            // 
            this.btnSMSOk.Location = new System.Drawing.Point(83, 120);
            this.btnSMSOk.Name = "btnSMSOk";
            this.btnSMSOk.Size = new System.Drawing.Size(75, 23);
            this.btnSMSOk.TabIndex = 1;
            this.btnSMSOk.Text = "OK";
            this.btnSMSOk.UseVisualStyleBackColor = true;
            this.btnSMSOk.Click += new System.EventHandler(this.BtnSMSOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Enter a SMS:";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(17, 19);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(37, 13);
            this.lblPhone.TabIndex = 3;
            this.lblPhone.Text = "phone";
            // 
            // lblSMSError
            // 
            this.lblSMSError.AutoSize = true;
            this.lblSMSError.ForeColor = System.Drawing.Color.Red;
            this.lblSMSError.Location = new System.Drawing.Point(19, 84);
            this.lblSMSError.Name = "lblSMSError";
            this.lblSMSError.Size = new System.Drawing.Size(0, 13);
            this.lblSMSError.TabIndex = 4;
            // 
            // SMSForm
            // 
            this.AcceptButton = this.btnSMSOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 155);
            this.Controls.Add(this.lblSMSError);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSMSOk);
            this.Controls.Add(this.txtSMSText);
            this.Name = "SMSForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SMSForm";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSMSText;
        private System.Windows.Forms.Button btnSMSOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblSMSError;
    }
}