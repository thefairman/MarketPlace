namespace LDmp
{
    partial class SecStep
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
            this.btnGetCpt = new System.Windows.Forms.Button();
            this.txtCpt = new System.Windows.Forms.TextBox();
            this.lblCpt = new System.Windows.Forms.Label();
            this.pbCpt = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtSmsCode = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbCpt)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGetCpt
            // 
            this.btnGetCpt.Location = new System.Drawing.Point(109, 246);
            this.btnGetCpt.Name = "btnGetCpt";
            this.btnGetCpt.Size = new System.Drawing.Size(126, 23);
            this.btnGetCpt.TabIndex = 17;
            this.btnGetCpt.Text = "Обновить картчу";
            this.btnGetCpt.UseVisualStyleBackColor = true;
            this.btnGetCpt.Click += new System.EventHandler(this.btnGetCpt_Click);
            // 
            // txtCpt
            // 
            this.txtCpt.Location = new System.Drawing.Point(109, 275);
            this.txtCpt.Name = "txtCpt";
            this.txtCpt.Size = new System.Drawing.Size(190, 20);
            this.txtCpt.TabIndex = 16;
            this.txtCpt.TextChanged += new System.EventHandler(this.txtCpt_TextChanged);
            // 
            // lblCpt
            // 
            this.lblCpt.AutoSize = true;
            this.lblCpt.Location = new System.Drawing.Point(39, 278);
            this.lblCpt.Name = "lblCpt";
            this.lblCpt.Size = new System.Drawing.Size(45, 13);
            this.lblCpt.TabIndex = 15;
            this.lblCpt.Text = "Каптча:";
            // 
            // pbCpt
            // 
            this.pbCpt.Location = new System.Drawing.Point(2, 103);
            this.pbCpt.Name = "pbCpt";
            this.pbCpt.Size = new System.Drawing.Size(358, 143);
            this.pbCpt.TabIndex = 14;
            this.pbCpt.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(257, 325);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 22);
            this.button1.TabIndex = 13;
            this.button1.Text = "Отмена";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 302);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Код из смс:";
            // 
            // txtHeader
            // 
            this.txtHeader.Location = new System.Drawing.Point(2, 3);
            this.txtHeader.Multiline = true;
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.ReadOnly = true;
            this.txtHeader.Size = new System.Drawing.Size(358, 94);
            this.txtHeader.TabIndex = 11;
            this.txtHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(134, 325);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 10;
            this.btnSend.Text = "Отправить";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtSmsCode
            // 
            this.txtSmsCode.Location = new System.Drawing.Point(109, 299);
            this.txtSmsCode.Name = "txtSmsCode";
            this.txtSmsCode.Size = new System.Drawing.Size(190, 20);
            this.txtSmsCode.TabIndex = 9;
            this.txtSmsCode.TextChanged += new System.EventHandler(this.txtSmsCode_TextChanged);
            // 
            // SecStep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 353);
            this.Controls.Add(this.btnGetCpt);
            this.Controls.Add(this.txtCpt);
            this.Controls.Add(this.lblCpt);
            this.Controls.Add(this.pbCpt);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtHeader);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtSmsCode);
            this.Name = "SecStep";
            this.Text = "SecStep";
            this.Load += new System.EventHandler(this.SecStep_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbCpt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetCpt;
        private System.Windows.Forms.TextBox txtCpt;
        private System.Windows.Forms.Label lblCpt;
        private System.Windows.Forms.PictureBox pbCpt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHeader;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtSmsCode;
    }
}