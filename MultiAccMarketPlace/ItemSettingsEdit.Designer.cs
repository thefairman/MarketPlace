namespace LDmp
{
    partial class ItemSettingsEdit
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
            this.nmItemSettingMinPercentProfit = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nmItemSettingMinAmountProfit = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nmItemSettingMaxPriceForPercent = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nmItemSettingNeedPercent = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbItemSettingOnlyGlobal = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nmItemSettingFixPrice = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nmMaxDropPercentByAVG = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.nmItemSettingMaxDropAmount = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.nmItemSettingMinPriceSellWOFee = new System.Windows.Forms.NumericUpDown();
            this.cbItemSettingAutoDrop = new System.Windows.Forms.CheckBox();
            this.nmItemSettingMaxDropPercent = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.nmItemSettingMinPriceSell = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSaveItemSettings = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cbPeriod = new System.Windows.Forms.ComboBox();
            this.lblLastCostISE = new System.Windows.Forms.Label();
            this.lblCountISE = new System.Windows.Forms.Label();
            this.lblPopularityISE = new System.Windows.Forms.Label();
            this.lblEntity_idISE = new System.Windows.Forms.Label();
            this.lblTitleISE = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblMaxDPByAVG = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmItemSettingMinPercentProfit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmItemSettingMinAmountProfit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmItemSettingMaxPriceForPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmItemSettingNeedPercent)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmItemSettingFixPrice)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmMaxDropPercentByAVG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmItemSettingMaxDropAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmItemSettingMinPriceSellWOFee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmItemSettingMaxDropPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmItemSettingMinPriceSell)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nmItemSettingMinPercentProfit);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.nmItemSettingMinAmountProfit);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.nmItemSettingMaxPriceForPercent);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.nmItemSettingNeedPercent);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(6, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 114);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "BuyingSettings";
            // 
            // nmItemSettingMinPercentProfit
            // 
            this.nmItemSettingMinPercentProfit.DecimalPlaces = 2;
            this.nmItemSettingMinPercentProfit.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nmItemSettingMinPercentProfit.Location = new System.Drawing.Point(254, 88);
            this.nmItemSettingMinPercentProfit.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nmItemSettingMinPercentProfit.Name = "nmItemSettingMinPercentProfit";
            this.nmItemSettingMinPercentProfit.Size = new System.Drawing.Size(72, 20);
            this.nmItemSettingMinPercentProfit.TabIndex = 10;
            this.nmItemSettingMinPercentProfit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumericUpDown_KeyPressReplaceDotToComa);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(160, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Min Percent Profit:";
            // 
            // nmItemSettingMinAmountProfit
            // 
            this.nmItemSettingMinAmountProfit.DecimalPlaces = 2;
            this.nmItemSettingMinAmountProfit.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nmItemSettingMinAmountProfit.Location = new System.Drawing.Point(254, 62);
            this.nmItemSettingMinAmountProfit.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.nmItemSettingMinAmountProfit.Name = "nmItemSettingMinAmountProfit";
            this.nmItemSettingMinAmountProfit.Size = new System.Drawing.Size(72, 20);
            this.nmItemSettingMinAmountProfit.TabIndex = 8;
            this.nmItemSettingMinAmountProfit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumericUpDown_KeyPressReplaceDotToComa);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(160, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Min Amount Profit:";
            // 
            // nmItemSettingMaxPriceForPercent
            // 
            this.nmItemSettingMaxPriceForPercent.DecimalPlaces = 2;
            this.nmItemSettingMaxPriceForPercent.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nmItemSettingMaxPriceForPercent.Location = new System.Drawing.Point(254, 36);
            this.nmItemSettingMaxPriceForPercent.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.nmItemSettingMaxPriceForPercent.Name = "nmItemSettingMaxPriceForPercent";
            this.nmItemSettingMaxPriceForPercent.Size = new System.Drawing.Size(72, 20);
            this.nmItemSettingMaxPriceForPercent.TabIndex = 6;
            this.nmItemSettingMaxPriceForPercent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumericUpDown_KeyPressReplaceDotToComa);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(160, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Max Price:";
            // 
            // nmItemSettingNeedPercent
            // 
            this.nmItemSettingNeedPercent.DecimalPlaces = 2;
            this.nmItemSettingNeedPercent.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nmItemSettingNeedPercent.Location = new System.Drawing.Point(254, 10);
            this.nmItemSettingNeedPercent.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            65536});
            this.nmItemSettingNeedPercent.Name = "nmItemSettingNeedPercent";
            this.nmItemSettingNeedPercent.Size = new System.Drawing.Size(72, 20);
            this.nmItemSettingNeedPercent.TabIndex = 3;
            this.nmItemSettingNeedPercent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumericUpDown_KeyPressReplaceDotToComa);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbItemSettingOnlyGlobal);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.nmItemSettingFixPrice);
            this.groupBox3.Location = new System.Drawing.Point(6, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(145, 75);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "GlobalOptions";
            // 
            // cbItemSettingOnlyGlobal
            // 
            this.cbItemSettingOnlyGlobal.AutoSize = true;
            this.cbItemSettingOnlyGlobal.Location = new System.Drawing.Point(10, 19);
            this.cbItemSettingOnlyGlobal.Name = "cbItemSettingOnlyGlobal";
            this.cbItemSettingOnlyGlobal.Size = new System.Drawing.Size(80, 17);
            this.cbItemSettingOnlyGlobal.TabIndex = 0;
            this.cbItemSettingOnlyGlobal.Text = "Only Global";
            this.cbItemSettingOnlyGlobal.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "FixPrice:";
            // 
            // nmItemSettingFixPrice
            // 
            this.nmItemSettingFixPrice.DecimalPlaces = 2;
            this.nmItemSettingFixPrice.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nmItemSettingFixPrice.Location = new System.Drawing.Point(60, 41);
            this.nmItemSettingFixPrice.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.nmItemSettingFixPrice.Name = "nmItemSettingFixPrice";
            this.nmItemSettingFixPrice.Size = new System.Drawing.Size(72, 20);
            this.nmItemSettingFixPrice.TabIndex = 2;
            this.nmItemSettingFixPrice.ValueChanged += new System.EventHandler(this.NmItemSettingFixPrice_ValueChanged);
            this.nmItemSettingFixPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumericUpDown_KeyPressReplaceDotToComa);
            this.nmItemSettingFixPrice.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NmItemSettingFixPrice_MouseDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(160, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Need Percent:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblMaxDPByAVG);
            this.groupBox2.Controls.Add(this.nmMaxDropPercentByAVG);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.nmItemSettingMaxDropAmount);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.nmItemSettingMinPriceSellWOFee);
            this.groupBox2.Controls.Add(this.cbItemSettingAutoDrop);
            this.groupBox2.Controls.Add(this.nmItemSettingMaxDropPercent);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.nmItemSettingMinPriceSell);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(345, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(269, 139);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SellingSettings";
            // 
            // nmMaxDropPercentByAVG
            // 
            this.nmMaxDropPercentByAVG.DecimalPlaces = 2;
            this.nmMaxDropPercentByAVG.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nmMaxDropPercentByAVG.Location = new System.Drawing.Point(108, 113);
            this.nmMaxDropPercentByAVG.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            65536});
            this.nmMaxDropPercentByAVG.Name = "nmMaxDropPercentByAVG";
            this.nmMaxDropPercentByAVG.Size = new System.Drawing.Size(72, 20);
            this.nmMaxDropPercentByAVG.TabIndex = 18;
            this.nmMaxDropPercentByAVG.ValueChanged += new System.EventHandler(this.NmMaxDropPercentByAVG_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 115);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(97, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "Max D. P. By AVG:";
            // 
            // nmItemSettingMaxDropAmount
            // 
            this.nmItemSettingMaxDropAmount.DecimalPlaces = 2;
            this.nmItemSettingMaxDropAmount.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nmItemSettingMaxDropAmount.Location = new System.Drawing.Point(108, 63);
            this.nmItemSettingMaxDropAmount.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            65536});
            this.nmItemSettingMaxDropAmount.Name = "nmItemSettingMaxDropAmount";
            this.nmItemSettingMaxDropAmount.Size = new System.Drawing.Size(72, 20);
            this.nmItemSettingMaxDropAmount.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 65);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Max Drop Amount:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(186, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "WO Fee:";
            // 
            // nmItemSettingMinPriceSellWOFee
            // 
            this.nmItemSettingMinPriceSellWOFee.DecimalPlaces = 2;
            this.nmItemSettingMinPriceSellWOFee.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nmItemSettingMinPriceSellWOFee.Location = new System.Drawing.Point(186, 38);
            this.nmItemSettingMinPriceSellWOFee.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.nmItemSettingMinPriceSellWOFee.Name = "nmItemSettingMinPriceSellWOFee";
            this.nmItemSettingMinPriceSellWOFee.Size = new System.Drawing.Size(72, 20);
            this.nmItemSettingMinPriceSellWOFee.TabIndex = 13;
            this.nmItemSettingMinPriceSellWOFee.ValueChanged += new System.EventHandler(this.NmItemSettingMinPriceSellWOFee_ValueChanged);
            // 
            // cbItemSettingAutoDrop
            // 
            this.cbItemSettingAutoDrop.AutoSize = true;
            this.cbItemSettingAutoDrop.Checked = true;
            this.cbItemSettingAutoDrop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbItemSettingAutoDrop.Location = new System.Drawing.Point(12, 19);
            this.cbItemSettingAutoDrop.Name = "cbItemSettingAutoDrop";
            this.cbItemSettingAutoDrop.Size = new System.Drawing.Size(74, 17);
            this.cbItemSettingAutoDrop.TabIndex = 12;
            this.cbItemSettingAutoDrop.Text = "Auto Drop";
            this.cbItemSettingAutoDrop.UseVisualStyleBackColor = true;
            // 
            // nmItemSettingMaxDropPercent
            // 
            this.nmItemSettingMaxDropPercent.DecimalPlaces = 2;
            this.nmItemSettingMaxDropPercent.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nmItemSettingMaxDropPercent.Location = new System.Drawing.Point(108, 88);
            this.nmItemSettingMaxDropPercent.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            65536});
            this.nmItemSettingMaxDropPercent.Name = "nmItemSettingMaxDropPercent";
            this.nmItemSettingMaxDropPercent.Size = new System.Drawing.Size(72, 20);
            this.nmItemSettingMaxDropPercent.TabIndex = 11;
            this.nmItemSettingMaxDropPercent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumericUpDown_KeyPressReplaceDotToComa);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Max Drop Percent:";
            // 
            // nmItemSettingMinPriceSell
            // 
            this.nmItemSettingMinPriceSell.DecimalPlaces = 2;
            this.nmItemSettingMinPriceSell.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nmItemSettingMinPriceSell.Location = new System.Drawing.Point(108, 38);
            this.nmItemSettingMinPriceSell.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.nmItemSettingMinPriceSell.Name = "nmItemSettingMinPriceSell";
            this.nmItemSettingMinPriceSell.Size = new System.Drawing.Size(72, 20);
            this.nmItemSettingMinPriceSell.TabIndex = 3;
            this.nmItemSettingMinPriceSell.ValueChanged += new System.EventHandler(this.NmItemSettingMinPriceSell_ValueChanged);
            this.nmItemSettingMinPriceSell.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumericUpDown_KeyPressReplaceDotToComa);
            this.nmItemSettingMinPriceSell.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NmItemSettingMinPriceSell_MouseDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Min Price Sell:";
            // 
            // btnSaveItemSettings
            // 
            this.btnSaveItemSettings.Location = new System.Drawing.Point(264, 135);
            this.btnSaveItemSettings.Name = "btnSaveItemSettings";
            this.btnSaveItemSettings.Size = new System.Drawing.Size(75, 23);
            this.btnSaveItemSettings.TabIndex = 3;
            this.btnSaveItemSettings.Text = "Save";
            this.btnSaveItemSettings.UseVisualStyleBackColor = true;
            this.btnSaveItemSettings.Click += new System.EventHandler(this.BtnSaveItemSettings_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(626, 423);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cbPeriod);
            this.groupBox5.Controls.Add(this.lblLastCostISE);
            this.groupBox5.Controls.Add(this.lblCountISE);
            this.groupBox5.Controls.Add(this.lblPopularityISE);
            this.groupBox5.Controls.Add(this.lblEntity_idISE);
            this.groupBox5.Controls.Add(this.lblTitleISE);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(3, 196);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(620, 54);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Info";
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
            this.cbPeriod.Location = new System.Drawing.Point(539, 8);
            this.cbPeriod.Name = "cbPeriod";
            this.cbPeriod.Size = new System.Drawing.Size(75, 21);
            this.cbPeriod.TabIndex = 14;
            this.cbPeriod.SelectedIndexChanged += new System.EventHandler(this.CbPeriod_SelectedIndexChanged);
            // 
            // lblLastCostISE
            // 
            this.lblLastCostISE.AutoSize = true;
            this.lblLastCostISE.Location = new System.Drawing.Point(405, 35);
            this.lblLastCostISE.Name = "lblLastCostISE";
            this.lblLastCostISE.Size = new System.Drawing.Size(54, 13);
            this.lblLastCostISE.TabIndex = 4;
            this.lblLastCostISE.Text = "Last Cost:";
            // 
            // lblCountISE
            // 
            this.lblCountISE.AutoSize = true;
            this.lblCountISE.Location = new System.Drawing.Point(257, 35);
            this.lblCountISE.Name = "lblCountISE";
            this.lblCountISE.Size = new System.Drawing.Size(38, 13);
            this.lblCountISE.TabIndex = 3;
            this.lblCountISE.Text = "Count:";
            // 
            // lblPopularityISE
            // 
            this.lblPopularityISE.AutoSize = true;
            this.lblPopularityISE.Location = new System.Drawing.Point(109, 35);
            this.lblPopularityISE.Name = "lblPopularityISE";
            this.lblPopularityISE.Size = new System.Drawing.Size(50, 13);
            this.lblPopularityISE.TabIndex = 2;
            this.lblPopularityISE.Text = "Ppularity:";
            // 
            // lblEntity_idISE
            // 
            this.lblEntity_idISE.AutoSize = true;
            this.lblEntity_idISE.Location = new System.Drawing.Point(12, 35);
            this.lblEntity_idISE.Name = "lblEntity_idISE";
            this.lblEntity_idISE.Size = new System.Drawing.Size(21, 13);
            this.lblEntity_idISE.TabIndex = 1;
            this.lblEntity_idISE.Text = "ID:";
            // 
            // lblTitleISE
            // 
            this.lblTitleISE.AutoSize = true;
            this.lblTitleISE.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTitleISE.Location = new System.Drawing.Point(9, 16);
            this.lblTitleISE.Name = "lblTitleISE";
            this.lblTitleISE.Size = new System.Drawing.Size(40, 13);
            this.lblTitleISE.TabIndex = 0;
            this.lblTitleISE.Text = "Title: ";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox1);
            this.groupBox4.Controls.Add(this.groupBox2);
            this.groupBox4.Controls.Add(this.btnSaveItemSettings);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(3, 256);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(620, 164);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Settings";
            // 
            // lblMaxDPByAVG
            // 
            this.lblMaxDPByAVG.AutoSize = true;
            this.lblMaxDPByAVG.Location = new System.Drawing.Point(186, 116);
            this.lblMaxDPByAVG.Name = "lblMaxDPByAVG";
            this.lblMaxDPByAVG.Size = new System.Drawing.Size(13, 13);
            this.lblMaxDPByAVG.TabIndex = 19;
            this.lblMaxDPByAVG.Text = "?";
            // 
            // ItemSettingsEdit
            // 
            this.AcceptButton = this.btnSaveItemSettings;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 423);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ItemSettingsEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ItemSettingsEdit";
            this.Load += new System.EventHandler(this.ItemSettingsEdit_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmItemSettingMinPercentProfit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmItemSettingMinAmountProfit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmItemSettingMaxPriceForPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmItemSettingNeedPercent)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmItemSettingFixPrice)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmMaxDropPercentByAVG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmItemSettingMaxDropAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmItemSettingMinPriceSellWOFee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmItemSettingMaxDropPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmItemSettingMinPriceSell)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbItemSettingOnlyGlobal;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown nmItemSettingFixPrice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nmItemSettingNeedPercent;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nmItemSettingMaxPriceForPercent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nmItemSettingMinPercentProfit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nmItemSettingMinAmountProfit;
        private System.Windows.Forms.NumericUpDown nmItemSettingMinPriceSell;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbItemSettingAutoDrop;
        private System.Windows.Forms.NumericUpDown nmItemSettingMaxDropPercent;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSaveItemSettings;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nmItemSettingMinPriceSellWOFee;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblTitleISE;
        private System.Windows.Forms.Label lblPopularityISE;
        private System.Windows.Forms.Label lblEntity_idISE;
        private System.Windows.Forms.Label lblCountISE;
        private System.Windows.Forms.Label lblLastCostISE;
        private System.Windows.Forms.ComboBox cbPeriod;
        private System.Windows.Forms.NumericUpDown nmItemSettingMaxDropAmount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nmMaxDropPercentByAVG;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblMaxDPByAVG;
    }
}