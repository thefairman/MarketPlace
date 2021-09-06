namespace LDmp
{
    partial class ManualItemSelling
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbDontCheckSecondPrice = new System.Windows.Forms.CheckBox();
            this.cbDontUp = new System.Windows.Forms.CheckBox();
            this.cbSellLessThenBought = new System.Windows.Forms.CheckBox();
            this.btnCancelSell = new System.Windows.Forms.Button();
            this.btnSell = new System.Windows.Forms.Button();
            this.nmItemSettingMinPriceSell = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBoughtPrice = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMinPrice = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMyMinPrice = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMinPosOrder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbPeriod = new System.Windows.Forms.ComboBox();
            this.lblMISInfo = new System.Windows.Forms.Label();
            this.btnApplyAndClose = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmItemSettingMinPriceSell)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(574, 360);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnApplyAndClose);
            this.groupBox2.Controls.Add(this.cbDontCheckSecondPrice);
            this.groupBox2.Controls.Add(this.cbDontUp);
            this.groupBox2.Controls.Add(this.cbSellLessThenBought);
            this.groupBox2.Controls.Add(this.btnCancelSell);
            this.groupBox2.Controls.Add(this.btnSell);
            this.groupBox2.Controls.Add(this.nmItemSettingMinPriceSell);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtBoughtPrice);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtMinPrice);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtMyMinPrice);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtMinPosOrder);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 263);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(568, 94);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sell Settings";
            // 
            // cbDontCheckSecondPrice
            // 
            this.cbDontCheckSecondPrice.AutoSize = true;
            this.cbDontCheckSecondPrice.Location = new System.Drawing.Point(223, 43);
            this.cbDontCheckSecondPrice.Name = "cbDontCheckSecondPrice";
            this.cbDontCheckSecondPrice.Size = new System.Drawing.Size(152, 17);
            this.cbDontCheckSecondPrice.TabIndex = 11;
            this.cbDontCheckSecondPrice.Text = "Don\'t Check Second Price";
            this.cbDontCheckSecondPrice.UseVisualStyleBackColor = true;
            // 
            // cbDontUp
            // 
            this.cbDontUp.AutoSize = true;
            this.cbDontUp.Location = new System.Drawing.Point(149, 42);
            this.cbDontUp.Name = "cbDontUp";
            this.cbDontUp.Size = new System.Drawing.Size(68, 17);
            this.cbDontUp.TabIndex = 10;
            this.cbDontUp.Text = "Don\'t Up";
            this.cbDontUp.UseVisualStyleBackColor = true;
            // 
            // cbSellLessThenBought
            // 
            this.cbSellLessThenBought.AutoSize = true;
            this.cbSellLessThenBought.Location = new System.Drawing.Point(10, 42);
            this.cbSellLessThenBought.Name = "cbSellLessThenBought";
            this.cbSellLessThenBought.Size = new System.Drawing.Size(133, 17);
            this.cbSellLessThenBought.TabIndex = 9;
            this.cbSellLessThenBought.Text = "Sell Less Then Bought";
            this.cbSellLessThenBought.UseVisualStyleBackColor = true;
            // 
            // btnCancelSell
            // 
            this.btnCancelSell.Location = new System.Drawing.Point(487, 62);
            this.btnCancelSell.Name = "btnCancelSell";
            this.btnCancelSell.Size = new System.Drawing.Size(75, 23);
            this.btnCancelSell.TabIndex = 1;
            this.btnCancelSell.Text = "Cancel";
            this.btnCancelSell.UseVisualStyleBackColor = true;
            this.btnCancelSell.Click += new System.EventHandler(this.BtnCancelSell_Click);
            // 
            // btnSell
            // 
            this.btnSell.Location = new System.Drawing.Point(177, 62);
            this.btnSell.Name = "btnSell";
            this.btnSell.Size = new System.Drawing.Size(75, 23);
            this.btnSell.TabIndex = 8;
            this.btnSell.Text = "Sell";
            this.btnSell.UseVisualStyleBackColor = true;
            this.btnSell.Click += new System.EventHandler(this.BtnSell_Click);
            // 
            // nmItemSettingMinPriceSell
            // 
            this.nmItemSettingMinPriceSell.DecimalPlaces = 2;
            this.nmItemSettingMinPriceSell.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nmItemSettingMinPriceSell.Location = new System.Drawing.Point(90, 65);
            this.nmItemSettingMinPriceSell.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.nmItemSettingMinPriceSell.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nmItemSettingMinPriceSell.Name = "nmItemSettingMinPriceSell";
            this.nmItemSettingMinPriceSell.Size = new System.Drawing.Size(72, 20);
            this.nmItemSettingMinPriceSell.TabIndex = 5;
            this.nmItemSettingMinPriceSell.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nmItemSettingMinPriceSell.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NmItemSettingMinPriceSell_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Min Price Sell:";
            // 
            // txtBoughtPrice
            // 
            this.txtBoughtPrice.Location = new System.Drawing.Point(480, 17);
            this.txtBoughtPrice.Name = "txtBoughtPrice";
            this.txtBoughtPrice.ReadOnly = true;
            this.txtBoughtPrice.Size = new System.Drawing.Size(56, 20);
            this.txtBoughtPrice.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(403, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Bought Price:";
            // 
            // txtMinPrice
            // 
            this.txtMinPrice.Location = new System.Drawing.Point(341, 17);
            this.txtMinPrice.Name = "txtMinPrice";
            this.txtMinPrice.ReadOnly = true;
            this.txtMinPrice.Size = new System.Drawing.Size(56, 20);
            this.txtMinPrice.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(285, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Min Price:";
            // 
            // txtMyMinPrice
            // 
            this.txtMyMinPrice.Location = new System.Drawing.Point(223, 17);
            this.txtMyMinPrice.Name = "txtMyMinPrice";
            this.txtMyMinPrice.ReadOnly = true;
            this.txtMyMinPrice.Size = new System.Drawing.Size(56, 20);
            this.txtMyMinPrice.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(146, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "My Min Price:";
            // 
            // txtMinPosOrder
            // 
            this.txtMinPosOrder.Location = new System.Drawing.Point(90, 17);
            this.txtMinPosOrder.Name = "txtMinPosOrder";
            this.txtMinPosOrder.ReadOnly = true;
            this.txtMinPosOrder.Size = new System.Drawing.Size(49, 20);
            this.txtMinPosOrder.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Min Pos Order:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbPeriod);
            this.groupBox1.Controls.Add(this.lblMISInfo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(568, 39);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Info";
            // 
            // cbPeriod
            // 
            this.cbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPeriod.FormattingEnabled = true;
            this.cbPeriod.Items.AddRange(new object[] {
            "day",
            "week",
            "month",
            "all"});
            this.cbPeriod.Location = new System.Drawing.Point(461, 12);
            this.cbPeriod.Name = "cbPeriod";
            this.cbPeriod.Size = new System.Drawing.Size(75, 21);
            this.cbPeriod.TabIndex = 9;
            this.cbPeriod.SelectedIndexChanged += new System.EventHandler(this.CbPeriod_SelectedIndexChanged);
            // 
            // lblMISInfo
            // 
            this.lblMISInfo.AutoSize = true;
            this.lblMISInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblMISInfo.Location = new System.Drawing.Point(6, 16);
            this.lblMISInfo.Name = "lblMISInfo";
            this.lblMISInfo.Size = new System.Drawing.Size(19, 13);
            this.lblMISInfo.TabIndex = 0;
            this.lblMISInfo.Text = "...";
            // 
            // btnApplyAndClose
            // 
            this.btnApplyAndClose.Location = new System.Drawing.Point(381, 39);
            this.btnApplyAndClose.Name = "btnApplyAndClose";
            this.btnApplyAndClose.Size = new System.Drawing.Size(93, 23);
            this.btnApplyAndClose.TabIndex = 12;
            this.btnApplyAndClose.Text = "Apply and close";
            this.btnApplyAndClose.UseVisualStyleBackColor = true;
            this.btnApplyAndClose.Click += new System.EventHandler(this.BtnApplyAndClose_Click);
            // 
            // ManualItemSelling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 360);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ManualItemSelling";
            this.Text = "ManualItemSelling";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ManualItemSelling_FormClosed);
            this.Load += new System.EventHandler(this.ManualItemSelling_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmItemSettingMinPriceSell)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtMinPrice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMyMinPrice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMinPosOrder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMISInfo;
        private System.Windows.Forms.TextBox txtBoughtPrice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nmItemSettingMinPriceSell;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCancelSell;
        private System.Windows.Forms.Button btnSell;
        private System.Windows.Forms.ComboBox cbPeriod;
        private System.Windows.Forms.CheckBox cbSellLessThenBought;
        private System.Windows.Forms.CheckBox cbDontUp;
        private System.Windows.Forms.CheckBox cbDontCheckSecondPrice;
        private System.Windows.Forms.Button btnApplyAndClose;
    }
}