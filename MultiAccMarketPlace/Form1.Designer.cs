namespace LDmp
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDelAcc = new System.Windows.Forms.Button();
            this.btnAddAcc = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAccPassAdd = new System.Windows.Forms.TextBox();
            this.txtAccLoginAdd = new System.Windows.Forms.TextBox();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nmToSellingHoldTime = new System.Windows.Forms.NumericUpDown();
            this.nmFromSellingHoldTime = new System.Windows.Forms.NumericUpDown();
            this.cbOnlyAdditional = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.nmAdditThreadsNum = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nmThreadsNum = new System.Windows.Forms.NumericUpDown();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.cbUsingProxyAdditional = new System.Windows.Forms.CheckBox();
            this.nmReqMax = new System.Windows.Forms.NumericUpDown();
            this.cbSwitchToProxyIfBanned = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbUsingProxyToUser = new System.Windows.Forms.CheckBox();
            this.cbUsingProxy = new System.Windows.Forms.CheckBox();
            this.nmTimeOutProxySec = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tPmessages = new System.Windows.Forms.TabPage();
            this.dgvMessages = new System.Windows.Forms.DataGridView();
            this.MesMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MesTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tPErrors = new System.Windows.Forms.TabPage();
            this.dgvErrors = new System.Windows.Forms.DataGridView();
            this.ErrMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErrTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblBadProxyCount = new System.Windows.Forms.Label();
            this.lblUsingProxyCount = new System.Windows.Forms.Label();
            this.lblBlockedIP = new System.Windows.Forms.Label();
            this.cbOnlyBuyed = new System.Windows.Forms.CheckBox();
            this.lblSLAT = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tPLogRecords = new System.Windows.Forms.TabPage();
            this.dgvLogOfActions = new System.Windows.Forms.DataGridView();
            this.Account = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColLogId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColLogPairs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColLogStrategy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColLogResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColLogTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tPAvailableItems = new System.Windows.Forms.TabPage();
            this.dgvAvailableItems = new System.Windows.Forms.DataGridView();
            this.AIID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AITitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AIState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AILastOrderInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AIPositionOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AILowestPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AIMyPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AIBoghtPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AIBoughtTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.dgvReqTimeInfo = new System.Windows.Forms.DataGridView();
            this.IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReqExecuteTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeBetween = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.Id_item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title_item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountItems = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Popularity_item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FixPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Percent_item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinAmountProfit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinPecentProfit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OnlyGlobalChecking = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinPriceSell = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxDropAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxDropPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxDPByAVG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AutoDrop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.btnAdditionallyClear = new System.Windows.Forms.Button();
            this.btnAdditionallyAccept = new System.Windows.Forms.Button();
            this.nmItemsPriceTo = new System.Windows.Forms.NumericUpDown();
            this.nmItemsPriceFrom = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.cbDescending = new System.Windows.Forms.CheckBox();
            this.rbSortByCost = new System.Windows.Forms.RadioButton();
            this.rbSortByPopularity = new System.Windows.Forms.RadioButton();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.rbDisplayedItems = new System.Windows.Forms.RadioButton();
            this.rbDisplayedItemsNotUsing = new System.Windows.Forms.RadioButton();
            this.rbDisplayedItemsUsing = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbAutoSelling = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nmMaxPrice = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.cbAutoSellingWhenBuy = new System.Windows.Forms.CheckBox();
            this.lblSR = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnFillNeedItems = new System.Windows.Forms.Button();
            this.btnFillAndRefreshNeedItems = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnResetBadProxy = new System.Windows.Forms.Button();
            this.lblProxyes = new System.Windows.Forms.Label();
            this.btnSaveProxyes = new System.Windows.Forms.Button();
            this.rtbProxyes = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmToSellingHoldTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmFromSellingHoldTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmAdditThreadsNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmThreadsNum)).BeginInit();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmReqMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmTimeOutProxySec)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tPmessages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMessages)).BeginInit();
            this.tPErrors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvErrors)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tPLogRecords.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogOfActions)).BeginInit();
            this.tPAvailableItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableItems)).BeginInit();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReqTimeInfo)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmItemsPriceTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmItemsPriceFrom)).BeginInit();
            this.groupBox9.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmMaxPrice)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblVersion);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnDelAcc);
            this.groupBox1.Controls.Add(this.btnAddAcc);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtAccPassAdd);
            this.groupBox1.Controls.Add(this.txtAccLoginAdd);
            this.groupBox1.Location = new System.Drawing.Point(8, 7);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(267, 320);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Accounts Users";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(7, 288);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(46, 17);
            this.lblVersion.TabIndex = 3;
            this.lblVersion.Text = "label9";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label5.Location = new System.Drawing.Point(4, 62);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "Pass:";
            this.label5.Click += new System.EventHandler(this.Label5_Click);
            // 
            // btnDelAcc
            // 
            this.btnDelAcc.Location = new System.Drawing.Point(196, 97);
            this.btnDelAcc.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelAcc.Name = "btnDelAcc";
            this.btnDelAcc.Size = new System.Drawing.Size(64, 28);
            this.btnDelAcc.TabIndex = 5;
            this.btnDelAcc.Text = "Delete";
            this.btnDelAcc.UseVisualStyleBackColor = true;
            this.btnDelAcc.Click += new System.EventHandler(this.BtnDelAcc_Click);
            // 
            // btnAddAcc
            // 
            this.btnAddAcc.Location = new System.Drawing.Point(8, 96);
            this.btnAddAcc.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddAcc.Name = "btnAddAcc";
            this.btnAddAcc.Size = new System.Drawing.Size(100, 28);
            this.btnAddAcc.TabIndex = 4;
            this.btnAddAcc.Text = "Add";
            this.btnAddAcc.UseVisualStyleBackColor = true;
            this.btnAddAcc.Click += new System.EventHandler(this.BtnAddAcc_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Login:";
            // 
            // txtAccPassAdd
            // 
            this.txtAccPassAdd.Location = new System.Drawing.Point(60, 58);
            this.txtAccPassAdd.Margin = new System.Windows.Forms.Padding(4);
            this.txtAccPassAdd.Name = "txtAccPassAdd";
            this.txtAccPassAdd.PasswordChar = '*';
            this.txtAccPassAdd.Size = new System.Drawing.Size(199, 22);
            this.txtAccPassAdd.TabIndex = 1;
            // 
            // txtAccLoginAdd
            // 
            this.txtAccLoginAdd.Location = new System.Drawing.Point(60, 25);
            this.txtAccLoginAdd.Margin = new System.Windows.Forms.Padding(4);
            this.txtAccLoginAdd.Name = "txtAccLoginAdd";
            this.txtAccLoginAdd.Size = new System.Drawing.Size(199, 22);
            this.txtAccLoginAdd.TabIndex = 0;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPage1);
            this.tabControlMain.Controls.Add(this.tabPage3);
            this.tabControlMain.Controls.Add(this.tabPage4);
            this.tabControlMain.Controls.Add(this.tabPage2);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Margin = new System.Windows.Forms.Padding(4);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(937, 554);
            this.tabControlMain.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(929, 525);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.nmToSellingHoldTime);
            this.groupBox3.Controls.Add(this.nmFromSellingHoldTime);
            this.groupBox3.Controls.Add(this.cbOnlyAdditional);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.nmAdditThreadsNum);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.nmThreadsNum);
            this.groupBox3.Controls.Add(this.groupBox8);
            this.groupBox3.Location = new System.Drawing.Point(557, 7);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(353, 320);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Settings";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 171);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(143, 17);
            this.label8.TabIndex = 31;
            this.label8.Text = "To Selling Hold Time:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 139);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(158, 17);
            this.label7.TabIndex = 30;
            this.label7.Text = "From Selling Hold Time:";
            // 
            // nmToSellingHoldTime
            // 
            this.nmToSellingHoldTime.Location = new System.Drawing.Point(175, 169);
            this.nmToSellingHoldTime.Margin = new System.Windows.Forms.Padding(4);
            this.nmToSellingHoldTime.Name = "nmToSellingHoldTime";
            this.nmToSellingHoldTime.Size = new System.Drawing.Size(88, 22);
            this.nmToSellingHoldTime.TabIndex = 29;
            this.nmToSellingHoldTime.ValueChanged += new System.EventHandler(this.SettingChanged);
            // 
            // nmFromSellingHoldTime
            // 
            this.nmFromSellingHoldTime.Location = new System.Drawing.Point(175, 137);
            this.nmFromSellingHoldTime.Margin = new System.Windows.Forms.Padding(4);
            this.nmFromSellingHoldTime.Name = "nmFromSellingHoldTime";
            this.nmFromSellingHoldTime.Size = new System.Drawing.Size(88, 22);
            this.nmFromSellingHoldTime.TabIndex = 28;
            this.nmFromSellingHoldTime.ValueChanged += new System.EventHandler(this.SettingChanged);
            // 
            // cbOnlyAdditional
            // 
            this.cbOnlyAdditional.AutoSize = true;
            this.cbOnlyAdditional.Location = new System.Drawing.Point(9, 288);
            this.cbOnlyAdditional.Margin = new System.Windows.Forms.Padding(4);
            this.cbOnlyAdditional.Name = "cbOnlyAdditional";
            this.cbOnlyAdditional.Size = new System.Drawing.Size(125, 21);
            this.cbOnlyAdditional.TabIndex = 27;
            this.cbOnlyAdditional.Text = "Only Additional";
            this.cbOnlyAdditional.UseVisualStyleBackColor = true;
            this.cbOnlyAdditional.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 251);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 17);
            this.label6.TabIndex = 26;
            this.label6.Text = "Addit thrds num:";
            // 
            // nmAdditThreadsNum
            // 
            this.nmAdditThreadsNum.Location = new System.Drawing.Point(121, 249);
            this.nmAdditThreadsNum.Margin = new System.Windows.Forms.Padding(4);
            this.nmAdditThreadsNum.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nmAdditThreadsNum.Name = "nmAdditThreadsNum";
            this.nmAdditThreadsNum.Size = new System.Drawing.Size(83, 22);
            this.nmAdditThreadsNum.TabIndex = 25;
            this.nmAdditThreadsNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmAdditThreadsNum.ValueChanged += new System.EventHandler(this.SettingChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 219);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 17);
            this.label2.TabIndex = 24;
            this.label2.Text = "Threads num:";
            // 
            // nmThreadsNum
            // 
            this.nmThreadsNum.Location = new System.Drawing.Point(121, 217);
            this.nmThreadsNum.Margin = new System.Windows.Forms.Padding(4);
            this.nmThreadsNum.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nmThreadsNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmThreadsNum.Name = "nmThreadsNum";
            this.nmThreadsNum.Size = new System.Drawing.Size(83, 22);
            this.nmThreadsNum.TabIndex = 23;
            this.nmThreadsNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmThreadsNum.ValueChanged += new System.EventHandler(this.SettingChanged);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.cbUsingProxyAdditional);
            this.groupBox8.Controls.Add(this.nmReqMax);
            this.groupBox8.Controls.Add(this.cbSwitchToProxyIfBanned);
            this.groupBox8.Controls.Add(this.label10);
            this.groupBox8.Controls.Add(this.cbUsingProxyToUser);
            this.groupBox8.Controls.Add(this.cbUsingProxy);
            this.groupBox8.Controls.Add(this.nmTimeOutProxySec);
            this.groupBox8.Controls.Add(this.label11);
            this.groupBox8.Location = new System.Drawing.Point(8, 23);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox8.Size = new System.Drawing.Size(337, 108);
            this.groupBox8.TabIndex = 22;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Requests";
            // 
            // cbUsingProxyAdditional
            // 
            this.cbUsingProxyAdditional.AutoSize = true;
            this.cbUsingProxyAdditional.Location = new System.Drawing.Point(240, 50);
            this.cbUsingProxyAdditional.Margin = new System.Windows.Forms.Padding(4);
            this.cbUsingProxyAdditional.Name = "cbUsingProxyAdditional";
            this.cbUsingProxyAdditional.Size = new System.Drawing.Size(97, 21);
            this.cbUsingProxyAdditional.TabIndex = 15;
            this.cbUsingProxyAdditional.Text = "UsPr Addit";
            this.cbUsingProxyAdditional.UseVisualStyleBackColor = true;
            this.cbUsingProxyAdditional.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // nmReqMax
            // 
            this.nmReqMax.Location = new System.Drawing.Point(131, 20);
            this.nmReqMax.Margin = new System.Windows.Forms.Padding(4);
            this.nmReqMax.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nmReqMax.Name = "nmReqMax";
            this.nmReqMax.Size = new System.Drawing.Size(65, 22);
            this.nmReqMax.TabIndex = 14;
            this.nmReqMax.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nmReqMax.ValueChanged += new System.EventHandler(this.SettingChanged);
            // 
            // cbSwitchToProxyIfBanned
            // 
            this.cbSwitchToProxyIfBanned.AutoSize = true;
            this.cbSwitchToProxyIfBanned.Location = new System.Drawing.Point(148, 79);
            this.cbSwitchToProxyIfBanned.Margin = new System.Windows.Forms.Padding(4);
            this.cbSwitchToProxyIfBanned.Name = "cbSwitchToProxyIfBanned";
            this.cbSwitchToProxyIfBanned.Size = new System.Drawing.Size(69, 21);
            this.cbSwitchToProxyIfBanned.TabIndex = 11;
            this.cbSwitchToProxyIfBanned.Text = "STPiB";
            this.cbSwitchToProxyIfBanned.UseVisualStyleBackColor = true;
            this.cbSwitchToProxyIfBanned.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 20);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 17);
            this.label10.TabIndex = 13;
            this.label10.Text = "Max Req:";
            // 
            // cbUsingProxyToUser
            // 
            this.cbUsingProxyToUser.AutoSize = true;
            this.cbUsingProxyToUser.Location = new System.Drawing.Point(124, 50);
            this.cbUsingProxyToUser.Margin = new System.Windows.Forms.Padding(4);
            this.cbUsingProxyToUser.Name = "cbUsingProxyToUser";
            this.cbUsingProxyToUser.Size = new System.Drawing.Size(116, 21);
            this.cbUsingProxyToUser.TabIndex = 10;
            this.cbUsingProxyToUser.Text = "UsingPrxUser";
            this.cbUsingProxyToUser.UseVisualStyleBackColor = true;
            this.cbUsingProxyToUser.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // cbUsingProxy
            // 
            this.cbUsingProxy.AutoSize = true;
            this.cbUsingProxy.Checked = true;
            this.cbUsingProxy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUsingProxy.Location = new System.Drawing.Point(17, 50);
            this.cbUsingProxy.Margin = new System.Windows.Forms.Padding(4);
            this.cbUsingProxy.Name = "cbUsingProxy";
            this.cbUsingProxy.Size = new System.Drawing.Size(105, 21);
            this.cbUsingProxy.TabIndex = 6;
            this.cbUsingProxy.Text = "Using Proxy";
            this.cbUsingProxy.UseVisualStyleBackColor = true;
            this.cbUsingProxy.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // nmTimeOutProxySec
            // 
            this.nmTimeOutProxySec.Location = new System.Drawing.Point(81, 78);
            this.nmTimeOutProxySec.Margin = new System.Windows.Forms.Padding(4);
            this.nmTimeOutProxySec.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmTimeOutProxySec.Name = "nmTimeOutProxySec";
            this.nmTimeOutProxySec.Size = new System.Drawing.Size(56, 22);
            this.nmTimeOutProxySec.TabIndex = 8;
            this.nmTimeOutProxySec.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nmTimeOutProxySec.ValueChanged += new System.EventHandler(this.SettingChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 80);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 17);
            this.label11.TabIndex = 9;
            this.label11.Text = "TOutPrx:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tableLayoutPanel1);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage3.Size = new System.Drawing.Size(929, 525);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "View";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tabControl3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(921, 517);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tPmessages);
            this.tabControl3.Controls.Add(this.tPErrors);
            this.tabControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl3.Location = new System.Drawing.Point(4, 339);
            this.tabControl3.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(913, 174);
            this.tabControl3.TabIndex = 3;
            // 
            // tPmessages
            // 
            this.tPmessages.Controls.Add(this.dgvMessages);
            this.tPmessages.Location = new System.Drawing.Point(4, 25);
            this.tPmessages.Margin = new System.Windows.Forms.Padding(4);
            this.tPmessages.Name = "tPmessages";
            this.tPmessages.Padding = new System.Windows.Forms.Padding(4);
            this.tPmessages.Size = new System.Drawing.Size(905, 145);
            this.tPmessages.TabIndex = 0;
            this.tPmessages.Text = "Messages";
            this.tPmessages.UseVisualStyleBackColor = true;
            // 
            // dgvMessages
            // 
            this.dgvMessages.AllowUserToAddRows = false;
            this.dgvMessages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMessages.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MesMessage,
            this.MesTime});
            this.dgvMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMessages.Location = new System.Drawing.Point(4, 4);
            this.dgvMessages.Margin = new System.Windows.Forms.Padding(4);
            this.dgvMessages.Name = "dgvMessages";
            this.dgvMessages.RowHeadersVisible = false;
            this.dgvMessages.RowHeadersWidth = 51;
            this.dgvMessages.Size = new System.Drawing.Size(897, 137);
            this.dgvMessages.TabIndex = 0;
            this.dgvMessages.VirtualMode = true;
            this.dgvMessages.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.DgvMessages_CellValueNeeded);
            // 
            // MesMessage
            // 
            this.MesMessage.HeaderText = "Message";
            this.MesMessage.MinimumWidth = 6;
            this.MesMessage.Name = "MesMessage";
            this.MesMessage.ReadOnly = true;
            this.MesMessage.Width = 125;
            // 
            // MesTime
            // 
            this.MesTime.HeaderText = "Time";
            this.MesTime.MinimumWidth = 6;
            this.MesTime.Name = "MesTime";
            this.MesTime.ReadOnly = true;
            this.MesTime.Width = 125;
            // 
            // tPErrors
            // 
            this.tPErrors.Controls.Add(this.dgvErrors);
            this.tPErrors.Location = new System.Drawing.Point(4, 25);
            this.tPErrors.Margin = new System.Windows.Forms.Padding(4);
            this.tPErrors.Name = "tPErrors";
            this.tPErrors.Padding = new System.Windows.Forms.Padding(4);
            this.tPErrors.Size = new System.Drawing.Size(905, 145);
            this.tPErrors.TabIndex = 1;
            this.tPErrors.Text = "Errors";
            this.tPErrors.UseVisualStyleBackColor = true;
            // 
            // dgvErrors
            // 
            this.dgvErrors.AllowUserToAddRows = false;
            this.dgvErrors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvErrors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ErrMessage,
            this.ErrTime});
            this.dgvErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvErrors.Location = new System.Drawing.Point(4, 4);
            this.dgvErrors.Margin = new System.Windows.Forms.Padding(4);
            this.dgvErrors.Name = "dgvErrors";
            this.dgvErrors.RowHeadersVisible = false;
            this.dgvErrors.RowHeadersWidth = 51;
            this.dgvErrors.Size = new System.Drawing.Size(897, 137);
            this.dgvErrors.TabIndex = 0;
            this.dgvErrors.VirtualMode = true;
            this.dgvErrors.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.DgvErrors_CellValueNeeded);
            // 
            // ErrMessage
            // 
            this.ErrMessage.HeaderText = "Message";
            this.ErrMessage.MinimumWidth = 6;
            this.ErrMessage.Name = "ErrMessage";
            this.ErrMessage.ReadOnly = true;
            this.ErrMessage.Width = 125;
            // 
            // ErrTime
            // 
            this.ErrTime.HeaderText = "Time";
            this.ErrTime.MinimumWidth = 6;
            this.ErrTime.Name = "ErrTime";
            this.ErrTime.ReadOnly = true;
            this.ErrTime.Width = 125;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblBadProxyCount);
            this.groupBox5.Controls.Add(this.lblUsingProxyCount);
            this.groupBox5.Controls.Add(this.lblBlockedIP);
            this.groupBox5.Controls.Add(this.cbOnlyBuyed);
            this.groupBox5.Controls.Add(this.lblSLAT);
            this.groupBox5.Controls.Add(this.btnStart);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(4, 4);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(913, 54);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Main";
            // 
            // lblBadProxyCount
            // 
            this.lblBadProxyCount.AutoSize = true;
            this.lblBadProxyCount.Location = new System.Drawing.Point(639, 14);
            this.lblBadProxyCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBadProxyCount.Name = "lblBadProxyCount";
            this.lblBadProxyCount.Size = new System.Drawing.Size(87, 17);
            this.lblBadProxyCount.TabIndex = 9;
            this.lblBadProxyCount.Text = "Bad proxy: ?";
            // 
            // lblUsingProxyCount
            // 
            this.lblUsingProxyCount.AutoSize = true;
            this.lblUsingProxyCount.Location = new System.Drawing.Point(504, 11);
            this.lblUsingProxyCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUsingProxyCount.Name = "lblUsingProxyCount";
            this.lblUsingProxyCount.Size = new System.Drawing.Size(98, 17);
            this.lblUsingProxyCount.TabIndex = 8;
            this.lblUsingProxyCount.Text = "Using proxy: ?";
            // 
            // lblBlockedIP
            // 
            this.lblBlockedIP.AutoSize = true;
            this.lblBlockedIP.Location = new System.Drawing.Point(504, 31);
            this.lblBlockedIP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBlockedIP.Name = "lblBlockedIP";
            this.lblBlockedIP.Size = new System.Drawing.Size(90, 17);
            this.lblBlockedIP.TabIndex = 5;
            this.lblBlockedIP.Text = "Blocked IP: ?";
            // 
            // cbOnlyBuyed
            // 
            this.cbOnlyBuyed.AutoSize = true;
            this.cbOnlyBuyed.Location = new System.Drawing.Point(285, 30);
            this.cbOnlyBuyed.Margin = new System.Windows.Forms.Padding(4);
            this.cbOnlyBuyed.Name = "cbOnlyBuyed";
            this.cbOnlyBuyed.Size = new System.Drawing.Size(189, 21);
            this.cbOnlyBuyed.TabIndex = 4;
            this.cbOnlyBuyed.Text = "Show only buyed records";
            this.cbOnlyBuyed.UseVisualStyleBackColor = true;
            // 
            // lblSLAT
            // 
            this.lblSLAT.AutoSize = true;
            this.lblSLAT.Location = new System.Drawing.Point(639, 31);
            this.lblSLAT.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSLAT.Name = "lblSLAT";
            this.lblSLAT.Size = new System.Drawing.Size(116, 17);
            this.lblSLAT.TabIndex = 3;
            this.lblSLAT.Text = "Socket LastAT: ?";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(8, 18);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 28);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tPLogRecords);
            this.tabControl1.Controls.Add(this.tPAvailableItems);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(4, 66);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(913, 265);
            this.tabControl1.TabIndex = 6;
            // 
            // tPLogRecords
            // 
            this.tPLogRecords.Controls.Add(this.dgvLogOfActions);
            this.tPLogRecords.Location = new System.Drawing.Point(4, 25);
            this.tPLogRecords.Margin = new System.Windows.Forms.Padding(4);
            this.tPLogRecords.Name = "tPLogRecords";
            this.tPLogRecords.Padding = new System.Windows.Forms.Padding(4);
            this.tPLogRecords.Size = new System.Drawing.Size(905, 236);
            this.tPLogRecords.TabIndex = 0;
            this.tPLogRecords.Text = "Log records";
            this.tPLogRecords.UseVisualStyleBackColor = true;
            // 
            // dgvLogOfActions
            // 
            this.dgvLogOfActions.AllowUserToAddRows = false;
            this.dgvLogOfActions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLogOfActions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Account,
            this.ColLogId,
            this.ColLogPairs,
            this.ColLogStrategy,
            this.ColLogResult,
            this.ColLogTime});
            this.dgvLogOfActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLogOfActions.Location = new System.Drawing.Point(4, 4);
            this.dgvLogOfActions.Margin = new System.Windows.Forms.Padding(4);
            this.dgvLogOfActions.Name = "dgvLogOfActions";
            this.dgvLogOfActions.RowHeadersVisible = false;
            this.dgvLogOfActions.RowHeadersWidth = 51;
            this.dgvLogOfActions.Size = new System.Drawing.Size(897, 228);
            this.dgvLogOfActions.TabIndex = 4;
            this.dgvLogOfActions.VirtualMode = true;
            this.dgvLogOfActions.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.DgvLogOfActions_CellValueNeeded);
            // 
            // Account
            // 
            this.Account.HeaderText = "Account";
            this.Account.MinimumWidth = 6;
            this.Account.Name = "Account";
            this.Account.ReadOnly = true;
            this.Account.Width = 125;
            // 
            // ColLogId
            // 
            this.ColLogId.HeaderText = "ID";
            this.ColLogId.MinimumWidth = 6;
            this.ColLogId.Name = "ColLogId";
            this.ColLogId.ReadOnly = true;
            this.ColLogId.Width = 125;
            // 
            // ColLogPairs
            // 
            this.ColLogPairs.HeaderText = "Title";
            this.ColLogPairs.MinimumWidth = 6;
            this.ColLogPairs.Name = "ColLogPairs";
            this.ColLogPairs.ReadOnly = true;
            this.ColLogPairs.Width = 125;
            // 
            // ColLogStrategy
            // 
            this.ColLogStrategy.HeaderText = "Message";
            this.ColLogStrategy.MinimumWidth = 6;
            this.ColLogStrategy.Name = "ColLogStrategy";
            this.ColLogStrategy.ReadOnly = true;
            this.ColLogStrategy.Width = 125;
            // 
            // ColLogResult
            // 
            this.ColLogResult.HeaderText = "Cost";
            this.ColLogResult.MinimumWidth = 6;
            this.ColLogResult.Name = "ColLogResult";
            this.ColLogResult.ReadOnly = true;
            this.ColLogResult.Width = 125;
            // 
            // ColLogTime
            // 
            this.ColLogTime.HeaderText = "Time";
            this.ColLogTime.MinimumWidth = 6;
            this.ColLogTime.Name = "ColLogTime";
            this.ColLogTime.ReadOnly = true;
            this.ColLogTime.Width = 125;
            // 
            // tPAvailableItems
            // 
            this.tPAvailableItems.Controls.Add(this.dgvAvailableItems);
            this.tPAvailableItems.Location = new System.Drawing.Point(4, 25);
            this.tPAvailableItems.Margin = new System.Windows.Forms.Padding(4);
            this.tPAvailableItems.Name = "tPAvailableItems";
            this.tPAvailableItems.Padding = new System.Windows.Forms.Padding(4);
            this.tPAvailableItems.Size = new System.Drawing.Size(905, 236);
            this.tPAvailableItems.TabIndex = 2;
            this.tPAvailableItems.Text = "Available Items";
            this.tPAvailableItems.UseVisualStyleBackColor = true;
            // 
            // dgvAvailableItems
            // 
            this.dgvAvailableItems.AllowUserToAddRows = false;
            this.dgvAvailableItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAvailableItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AIID,
            this.AITitle,
            this.AIState,
            this.AILastOrderInfo,
            this.AIPositionOrder,
            this.AILowestPrice,
            this.AIMyPrice,
            this.AIBoghtPrice,
            this.AIBoughtTime});
            this.dgvAvailableItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAvailableItems.Location = new System.Drawing.Point(4, 4);
            this.dgvAvailableItems.Margin = new System.Windows.Forms.Padding(4);
            this.dgvAvailableItems.Name = "dgvAvailableItems";
            this.dgvAvailableItems.RowHeadersVisible = false;
            this.dgvAvailableItems.RowHeadersWidth = 51;
            this.dgvAvailableItems.Size = new System.Drawing.Size(897, 228);
            this.dgvAvailableItems.TabIndex = 0;
            this.dgvAvailableItems.VirtualMode = true;
            this.dgvAvailableItems.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DgvAvailableItems_CellMouseDoubleClick);
            this.dgvAvailableItems.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.DgvAvailableItems_CellValueNeeded);
            // 
            // AIID
            // 
            this.AIID.HeaderText = "ID";
            this.AIID.MinimumWidth = 6;
            this.AIID.Name = "AIID";
            this.AIID.ReadOnly = true;
            this.AIID.Width = 125;
            // 
            // AITitle
            // 
            this.AITitle.HeaderText = "Title";
            this.AITitle.MinimumWidth = 6;
            this.AITitle.Name = "AITitle";
            this.AITitle.ReadOnly = true;
            this.AITitle.Width = 125;
            // 
            // AIState
            // 
            this.AIState.HeaderText = "State";
            this.AIState.MinimumWidth = 6;
            this.AIState.Name = "AIState";
            this.AIState.ReadOnly = true;
            this.AIState.Width = 125;
            // 
            // AILastOrderInfo
            // 
            this.AILastOrderInfo.HeaderText = "Last Order Info";
            this.AILastOrderInfo.MinimumWidth = 6;
            this.AILastOrderInfo.Name = "AILastOrderInfo";
            this.AILastOrderInfo.ReadOnly = true;
            this.AILastOrderInfo.Width = 125;
            // 
            // AIPositionOrder
            // 
            this.AIPositionOrder.HeaderText = "Position Order";
            this.AIPositionOrder.MinimumWidth = 6;
            this.AIPositionOrder.Name = "AIPositionOrder";
            this.AIPositionOrder.ReadOnly = true;
            this.AIPositionOrder.Width = 125;
            // 
            // AILowestPrice
            // 
            this.AILowestPrice.HeaderText = "Lowest Price";
            this.AILowestPrice.MinimumWidth = 6;
            this.AILowestPrice.Name = "AILowestPrice";
            this.AILowestPrice.ReadOnly = true;
            this.AILowestPrice.Width = 125;
            // 
            // AIMyPrice
            // 
            this.AIMyPrice.HeaderText = "My Price";
            this.AIMyPrice.MinimumWidth = 6;
            this.AIMyPrice.Name = "AIMyPrice";
            this.AIMyPrice.ReadOnly = true;
            this.AIMyPrice.Width = 125;
            // 
            // AIBoghtPrice
            // 
            this.AIBoghtPrice.HeaderText = "Bought Price";
            this.AIBoghtPrice.MinimumWidth = 6;
            this.AIBoghtPrice.Name = "AIBoghtPrice";
            this.AIBoghtPrice.ReadOnly = true;
            this.AIBoghtPrice.Width = 125;
            // 
            // AIBoughtTime
            // 
            this.AIBoughtTime.HeaderText = "BoughtTime";
            this.AIBoughtTime.MinimumWidth = 6;
            this.AIBoughtTime.Name = "AIBoughtTime";
            this.AIBoughtTime.ReadOnly = true;
            this.AIBoughtTime.Width = 125;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.dgvReqTimeInfo);
            this.tabPage6.Location = new System.Drawing.Point(4, 25);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage6.Size = new System.Drawing.Size(905, 236);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "Requests Time Info";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // dgvReqTimeInfo
            // 
            this.dgvReqTimeInfo.AllowUserToAddRows = false;
            this.dgvReqTimeInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReqTimeInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IP,
            this.TimeStart,
            this.ReqExecuteTime,
            this.TimeBetween});
            this.dgvReqTimeInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReqTimeInfo.Location = new System.Drawing.Point(4, 4);
            this.dgvReqTimeInfo.Margin = new System.Windows.Forms.Padding(4);
            this.dgvReqTimeInfo.Name = "dgvReqTimeInfo";
            this.dgvReqTimeInfo.RowHeadersVisible = false;
            this.dgvReqTimeInfo.RowHeadersWidth = 51;
            this.dgvReqTimeInfo.Size = new System.Drawing.Size(897, 228);
            this.dgvReqTimeInfo.TabIndex = 0;
            this.dgvReqTimeInfo.VirtualMode = true;
            this.dgvReqTimeInfo.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.DgvReqTimeInfo_CellValueNeeded);
            // 
            // IP
            // 
            this.IP.HeaderText = "IP";
            this.IP.MinimumWidth = 6;
            this.IP.Name = "IP";
            this.IP.ReadOnly = true;
            this.IP.Width = 125;
            // 
            // TimeStart
            // 
            this.TimeStart.HeaderText = "Time Start";
            this.TimeStart.MinimumWidth = 6;
            this.TimeStart.Name = "TimeStart";
            this.TimeStart.ReadOnly = true;
            this.TimeStart.Width = 125;
            // 
            // ReqExecuteTime
            // 
            this.ReqExecuteTime.HeaderText = "Execute Time";
            this.ReqExecuteTime.MinimumWidth = 6;
            this.ReqExecuteTime.Name = "ReqExecuteTime";
            this.ReqExecuteTime.ReadOnly = true;
            this.ReqExecuteTime.Width = 125;
            // 
            // TimeBetween
            // 
            this.TimeBetween.HeaderText = "Time Between";
            this.TimeBetween.MinimumWidth = 6;
            this.TimeBetween.Name = "TimeBetween";
            this.TimeBetween.ReadOnly = true;
            this.TimeBetween.Width = 125;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.tableLayoutPanel2);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage4.Size = new System.Drawing.Size(929, 525);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "ItemsSettings";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.dgvItems, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.groupBox6, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 197F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(921, 517);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id_item,
            this.Title_item,
            this.LastCost,
            this.CountItems,
            this.Popularity_item,
            this.FixPrice,
            this.Percent_item,
            this.MaxPrice,
            this.MinAmountProfit,
            this.MinPecentProfit,
            this.OnlyGlobalChecking,
            this.MinPriceSell,
            this.MaxDropAmount,
            this.MaxDropPercent,
            this.MaxDPByAVG,
            this.AutoDrop});
            this.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItems.Location = new System.Drawing.Point(4, 201);
            this.dgvItems.Margin = new System.Windows.Forms.Padding(4);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.ReadOnly = true;
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.RowHeadersWidth = 51;
            this.dgvItems.Size = new System.Drawing.Size(913, 312);
            this.dgvItems.TabIndex = 0;
            this.dgvItems.VirtualMode = true;
            this.dgvItems.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DgvItems_CellMouseClick);
            this.dgvItems.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DgvItems_CellMouseDoubleClick);
            this.dgvItems.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.DgvItems_CellValueNeeded);
            // 
            // Id_item
            // 
            this.Id_item.HeaderText = "ID";
            this.Id_item.MinimumWidth = 6;
            this.Id_item.Name = "Id_item";
            this.Id_item.ReadOnly = true;
            this.Id_item.Width = 43;
            // 
            // Title_item
            // 
            this.Title_item.HeaderText = "Title";
            this.Title_item.MinimumWidth = 6;
            this.Title_item.Name = "Title_item";
            this.Title_item.ReadOnly = true;
            this.Title_item.Width = 52;
            // 
            // LastCost
            // 
            this.LastCost.HeaderText = "Last Cost";
            this.LastCost.MinimumWidth = 6;
            this.LastCost.Name = "LastCost";
            this.LastCost.ReadOnly = true;
            this.LastCost.Width = 70;
            // 
            // CountItems
            // 
            this.CountItems.HeaderText = "Count";
            this.CountItems.MinimumWidth = 6;
            this.CountItems.Name = "CountItems";
            this.CountItems.ReadOnly = true;
            this.CountItems.Width = 60;
            // 
            // Popularity_item
            // 
            this.Popularity_item.HeaderText = "Popularity";
            this.Popularity_item.MinimumWidth = 6;
            this.Popularity_item.Name = "Popularity_item";
            this.Popularity_item.ReadOnly = true;
            this.Popularity_item.Width = 78;
            // 
            // FixPrice
            // 
            this.FixPrice.HeaderText = "Fix Price";
            this.FixPrice.MinimumWidth = 6;
            this.FixPrice.Name = "FixPrice";
            this.FixPrice.ReadOnly = true;
            this.FixPrice.Width = 67;
            // 
            // Percent_item
            // 
            this.Percent_item.HeaderText = "Need percent";
            this.Percent_item.MinimumWidth = 6;
            this.Percent_item.Name = "Percent_item";
            this.Percent_item.ReadOnly = true;
            this.Percent_item.Width = 89;
            // 
            // MaxPrice
            // 
            this.MaxPrice.HeaderText = "Max Price";
            this.MaxPrice.MinimumWidth = 6;
            this.MaxPrice.Name = "MaxPrice";
            this.MaxPrice.ReadOnly = true;
            this.MaxPrice.Width = 92;
            // 
            // MinAmountProfit
            // 
            this.MinAmountProfit.HeaderText = "Min Amount Profit";
            this.MinAmountProfit.MinimumWidth = 6;
            this.MinAmountProfit.Name = "MinAmountProfit";
            this.MinAmountProfit.ReadOnly = true;
            this.MinAmountProfit.Width = 105;
            // 
            // MinPecentProfit
            // 
            this.MinPecentProfit.HeaderText = "Min Percent Profit";
            this.MinPecentProfit.MinimumWidth = 6;
            this.MinPecentProfit.Name = "MinPecentProfit";
            this.MinPecentProfit.ReadOnly = true;
            this.MinPecentProfit.Width = 106;
            // 
            // OnlyGlobalChecking
            // 
            this.OnlyGlobalChecking.HeaderText = "Only Global";
            this.OnlyGlobalChecking.MinimumWidth = 6;
            this.OnlyGlobalChecking.Name = "OnlyGlobalChecking";
            this.OnlyGlobalChecking.ReadOnly = true;
            this.OnlyGlobalChecking.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.OnlyGlobalChecking.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OnlyGlobalChecking.Width = 60;
            // 
            // MinPriceSell
            // 
            this.MinPriceSell.HeaderText = "Min Price Sell";
            this.MinPriceSell.MinimumWidth = 6;
            this.MinPriceSell.Name = "MinPriceSell";
            this.MinPriceSell.ReadOnly = true;
            this.MinPriceSell.Width = 88;
            // 
            // MaxDropAmount
            // 
            this.MaxDropAmount.HeaderText = "Max Drop Amount";
            this.MaxDropAmount.MinimumWidth = 6;
            this.MaxDropAmount.Name = "MaxDropAmount";
            this.MaxDropAmount.ReadOnly = true;
            this.MaxDropAmount.Width = 125;
            // 
            // MaxDropPercent
            // 
            this.MaxDropPercent.HeaderText = "Max Drop Percent";
            this.MaxDropPercent.MinimumWidth = 6;
            this.MaxDropPercent.Name = "MaxDropPercent";
            this.MaxDropPercent.ReadOnly = true;
            this.MaxDropPercent.Width = 108;
            // 
            // MaxDPByAVG
            // 
            this.MaxDPByAVG.HeaderText = "Max D.P. By AVG";
            this.MaxDPByAVG.MinimumWidth = 6;
            this.MaxDPByAVG.Name = "MaxDPByAVG";
            this.MaxDPByAVG.ReadOnly = true;
            this.MaxDPByAVG.Width = 125;
            // 
            // AutoDrop
            // 
            this.AutoDrop.HeaderText = "Auto Drop";
            this.AutoDrop.MinimumWidth = 6;
            this.AutoDrop.Name = "AutoDrop";
            this.AutoDrop.ReadOnly = true;
            this.AutoDrop.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AutoDrop.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.AutoDrop.Width = 55;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.groupBox10);
            this.groupBox6.Controls.Add(this.groupBox9);
            this.groupBox6.Controls.Add(this.groupBox7);
            this.groupBox6.Controls.Add(this.groupBox4);
            this.groupBox6.Controls.Add(this.groupBox2);
            this.groupBox6.Controls.Add(this.lblSR);
            this.groupBox6.Controls.Add(this.txtSearch);
            this.groupBox6.Controls.Add(this.btnFillNeedItems);
            this.groupBox6.Controls.Add(this.btnFillAndRefreshNeedItems);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(4, 4);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox6.Size = new System.Drawing.Size(913, 189);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Items Contols";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.btnAdditionallyClear);
            this.groupBox10.Controls.Add(this.btnAdditionallyAccept);
            this.groupBox10.Controls.Add(this.nmItemsPriceTo);
            this.groupBox10.Controls.Add(this.nmItemsPriceFrom);
            this.groupBox10.Controls.Add(this.label4);
            this.groupBox10.Location = new System.Drawing.Point(267, 91);
            this.groupBox10.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox10.Size = new System.Drawing.Size(279, 91);
            this.groupBox10.TabIndex = 14;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Additionally";
            // 
            // btnAdditionallyClear
            // 
            this.btnAdditionallyClear.Location = new System.Drawing.Point(161, 53);
            this.btnAdditionallyClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdditionallyClear.Name = "btnAdditionallyClear";
            this.btnAdditionallyClear.Size = new System.Drawing.Size(100, 28);
            this.btnAdditionallyClear.TabIndex = 6;
            this.btnAdditionallyClear.Text = "Clear";
            this.btnAdditionallyClear.UseVisualStyleBackColor = true;
            this.btnAdditionallyClear.Click += new System.EventHandler(this.BtnAdditionallyClear_Click);
            // 
            // btnAdditionallyAccept
            // 
            this.btnAdditionallyAccept.Location = new System.Drawing.Point(12, 53);
            this.btnAdditionallyAccept.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdditionallyAccept.Name = "btnAdditionallyAccept";
            this.btnAdditionallyAccept.Size = new System.Drawing.Size(100, 28);
            this.btnAdditionallyAccept.TabIndex = 5;
            this.btnAdditionallyAccept.Text = "Accept";
            this.btnAdditionallyAccept.UseVisualStyleBackColor = true;
            this.btnAdditionallyAccept.Click += new System.EventHandler(this.BtnAdditionallyAccept_Click);
            // 
            // nmItemsPriceTo
            // 
            this.nmItemsPriceTo.DecimalPlaces = 2;
            this.nmItemsPriceTo.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nmItemsPriceTo.Location = new System.Drawing.Point(165, 21);
            this.nmItemsPriceTo.Margin = new System.Windows.Forms.Padding(4);
            this.nmItemsPriceTo.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.nmItemsPriceTo.Name = "nmItemsPriceTo";
            this.nmItemsPriceTo.Size = new System.Drawing.Size(96, 22);
            this.nmItemsPriceTo.TabIndex = 4;
            this.nmItemsPriceTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumericUpDown_KeyPressReplaceDotToComa);
            // 
            // nmItemsPriceFrom
            // 
            this.nmItemsPriceFrom.DecimalPlaces = 2;
            this.nmItemsPriceFrom.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nmItemsPriceFrom.Location = new System.Drawing.Point(61, 21);
            this.nmItemsPriceFrom.Margin = new System.Windows.Forms.Padding(4);
            this.nmItemsPriceFrom.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.nmItemsPriceFrom.Name = "nmItemsPriceFrom";
            this.nmItemsPriceFrom.Size = new System.Drawing.Size(96, 22);
            this.nmItemsPriceFrom.TabIndex = 3;
            this.nmItemsPriceFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumericUpDown_KeyPressReplaceDotToComa);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 23);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Price:";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.cbDescending);
            this.groupBox9.Controls.Add(this.rbSortByCost);
            this.groupBox9.Controls.Add(this.rbSortByPopularity);
            this.groupBox9.Location = new System.Drawing.Point(133, 91);
            this.groupBox9.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox9.Size = new System.Drawing.Size(125, 91);
            this.groupBox9.TabIndex = 13;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Sorting";
            // 
            // cbDescending
            // 
            this.cbDescending.AutoSize = true;
            this.cbDescending.Checked = true;
            this.cbDescending.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDescending.Location = new System.Drawing.Point(8, 65);
            this.cbDescending.Margin = new System.Windows.Forms.Padding(4);
            this.cbDescending.Name = "cbDescending";
            this.cbDescending.Size = new System.Drawing.Size(105, 21);
            this.cbDescending.TabIndex = 2;
            this.cbDescending.Text = "Descending";
            this.cbDescending.UseVisualStyleBackColor = true;
            // 
            // rbSortByCost
            // 
            this.rbSortByCost.AutoSize = true;
            this.rbSortByCost.Location = new System.Drawing.Point(8, 44);
            this.rbSortByCost.Margin = new System.Windows.Forms.Padding(4);
            this.rbSortByCost.Name = "rbSortByCost";
            this.rbSortByCost.Size = new System.Drawing.Size(57, 21);
            this.rbSortByCost.TabIndex = 1;
            this.rbSortByCost.TabStop = true;
            this.rbSortByCost.Text = "Cost";
            this.rbSortByCost.UseVisualStyleBackColor = true;
            this.rbSortByCost.CheckedChanged += new System.EventHandler(this.RbDisplayedItems_CheckedChanged);
            // 
            // rbSortByPopularity
            // 
            this.rbSortByPopularity.AutoSize = true;
            this.rbSortByPopularity.Location = new System.Drawing.Point(8, 23);
            this.rbSortByPopularity.Margin = new System.Windows.Forms.Padding(4);
            this.rbSortByPopularity.Name = "rbSortByPopularity";
            this.rbSortByPopularity.Size = new System.Drawing.Size(92, 21);
            this.rbSortByPopularity.TabIndex = 0;
            this.rbSortByPopularity.TabStop = true;
            this.rbSortByPopularity.Text = "Popularity";
            this.rbSortByPopularity.UseVisualStyleBackColor = true;
            this.rbSortByPopularity.CheckedChanged += new System.EventHandler(this.RbDisplayedItems_CheckedChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.rbDisplayedItems);
            this.groupBox7.Controls.Add(this.rbDisplayedItemsNotUsing);
            this.groupBox7.Controls.Add(this.rbDisplayedItemsUsing);
            this.groupBox7.Location = new System.Drawing.Point(8, 91);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox7.Size = new System.Drawing.Size(117, 91);
            this.groupBox7.TabIndex = 12;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "View";
            // 
            // rbDisplayedItems
            // 
            this.rbDisplayedItems.AutoSize = true;
            this.rbDisplayedItems.Checked = true;
            this.rbDisplayedItems.Location = new System.Drawing.Point(8, 23);
            this.rbDisplayedItems.Margin = new System.Windows.Forms.Padding(4);
            this.rbDisplayedItems.Name = "rbDisplayedItems";
            this.rbDisplayedItems.Size = new System.Drawing.Size(44, 21);
            this.rbDisplayedItems.TabIndex = 9;
            this.rbDisplayedItems.TabStop = true;
            this.rbDisplayedItems.Text = "All";
            this.rbDisplayedItems.UseVisualStyleBackColor = true;
            this.rbDisplayedItems.CheckedChanged += new System.EventHandler(this.RbDisplayedItems_CheckedChanged);
            // 
            // rbDisplayedItemsNotUsing
            // 
            this.rbDisplayedItemsNotUsing.AutoSize = true;
            this.rbDisplayedItemsNotUsing.Location = new System.Drawing.Point(8, 65);
            this.rbDisplayedItemsNotUsing.Margin = new System.Windows.Forms.Padding(4);
            this.rbDisplayedItemsNotUsing.Name = "rbDisplayedItemsNotUsing";
            this.rbDisplayedItemsNotUsing.Size = new System.Drawing.Size(91, 21);
            this.rbDisplayedItemsNotUsing.TabIndex = 11;
            this.rbDisplayedItemsNotUsing.Text = "Not Using";
            this.rbDisplayedItemsNotUsing.UseVisualStyleBackColor = true;
            this.rbDisplayedItemsNotUsing.CheckedChanged += new System.EventHandler(this.RbDisplayedItems_CheckedChanged);
            // 
            // rbDisplayedItemsUsing
            // 
            this.rbDisplayedItemsUsing.AutoSize = true;
            this.rbDisplayedItemsUsing.Location = new System.Drawing.Point(8, 44);
            this.rbDisplayedItemsUsing.Margin = new System.Windows.Forms.Padding(4);
            this.rbDisplayedItemsUsing.Name = "rbDisplayedItemsUsing";
            this.rbDisplayedItemsUsing.Size = new System.Drawing.Size(65, 21);
            this.rbDisplayedItemsUsing.TabIndex = 10;
            this.rbDisplayedItemsUsing.Text = "Using";
            this.rbDisplayedItemsUsing.UseVisualStyleBackColor = true;
            this.rbDisplayedItemsUsing.CheckedChanged += new System.EventHandler(this.RbDisplayedItems_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbAutoSelling);
            this.groupBox4.Location = new System.Drawing.Point(495, 15);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(197, 75);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Selling settings";
            // 
            // cbAutoSelling
            // 
            this.cbAutoSelling.AutoSize = true;
            this.cbAutoSelling.Location = new System.Drawing.Point(8, 23);
            this.cbAutoSelling.Margin = new System.Windows.Forms.Padding(4);
            this.cbAutoSelling.Name = "cbAutoSelling";
            this.cbAutoSelling.Size = new System.Drawing.Size(105, 21);
            this.cbAutoSelling.TabIndex = 4;
            this.cbAutoSelling.Text = "Auto Selling";
            this.cbAutoSelling.UseVisualStyleBackColor = true;
            this.cbAutoSelling.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.nmMaxPrice);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cbAutoSellingWhenBuy);
            this.groupBox2.Location = new System.Drawing.Point(700, 14);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(203, 123);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Buying settings";
            // 
            // nmMaxPrice
            // 
            this.nmMaxPrice.Location = new System.Drawing.Point(85, 23);
            this.nmMaxPrice.Margin = new System.Windows.Forms.Padding(4);
            this.nmMaxPrice.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nmMaxPrice.Name = "nmMaxPrice";
            this.nmMaxPrice.Size = new System.Drawing.Size(103, 22);
            this.nmMaxPrice.TabIndex = 5;
            this.nmMaxPrice.ValueChanged += new System.EventHandler(this.SettingChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 28);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "maxPrice:";
            // 
            // cbAutoSellingWhenBuy
            // 
            this.cbAutoSellingWhenBuy.AutoSize = true;
            this.cbAutoSellingWhenBuy.Location = new System.Drawing.Point(11, 55);
            this.cbAutoSellingWhenBuy.Margin = new System.Windows.Forms.Padding(4);
            this.cbAutoSellingWhenBuy.Name = "cbAutoSellingWhenBuy";
            this.cbAutoSellingWhenBuy.Size = new System.Drawing.Size(174, 21);
            this.cbAutoSellingWhenBuy.TabIndex = 6;
            this.cbAutoSellingWhenBuy.Text = "Auto Sell When Buying";
            this.cbAutoSellingWhenBuy.UseVisualStyleBackColor = true;
            this.cbAutoSellingWhenBuy.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // lblSR
            // 
            this.lblSR.AutoSize = true;
            this.lblSR.Location = new System.Drawing.Point(252, 63);
            this.lblSR.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSR.Name = "lblSR";
            this.lblSR.Size = new System.Drawing.Size(16, 17);
            this.lblSR.TabIndex = 3;
            this.lblSR.Text = "_";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(8, 59);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(235, 22);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearch_KeyDown);
            // 
            // btnFillNeedItems
            // 
            this.btnFillNeedItems.Location = new System.Drawing.Point(8, 23);
            this.btnFillNeedItems.Margin = new System.Windows.Forms.Padding(4);
            this.btnFillNeedItems.Name = "btnFillNeedItems";
            this.btnFillNeedItems.Size = new System.Drawing.Size(100, 28);
            this.btnFillNeedItems.TabIndex = 1;
            this.btnFillNeedItems.Text = "Fill";
            this.btnFillNeedItems.UseVisualStyleBackColor = true;
            this.btnFillNeedItems.Click += new System.EventHandler(this.BtnFillNeedItems_Click);
            // 
            // btnFillAndRefreshNeedItems
            // 
            this.btnFillAndRefreshNeedItems.Location = new System.Drawing.Point(116, 23);
            this.btnFillAndRefreshNeedItems.Margin = new System.Windows.Forms.Padding(4);
            this.btnFillAndRefreshNeedItems.Name = "btnFillAndRefreshNeedItems";
            this.btnFillAndRefreshNeedItems.Size = new System.Drawing.Size(128, 28);
            this.btnFillAndRefreshNeedItems.TabIndex = 0;
            this.btnFillAndRefreshNeedItems.Text = "Fill And Refresh";
            this.btnFillAndRefreshNeedItems.UseVisualStyleBackColor = true;
            this.btnFillAndRefreshNeedItems.Click += new System.EventHandler(this.BtnFillAndRefreshNeedItems_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnResetBadProxy);
            this.tabPage2.Controls.Add(this.lblProxyes);
            this.tabPage2.Controls.Add(this.btnSaveProxyes);
            this.tabPage2.Controls.Add(this.rtbProxyes);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(929, 525);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Proxy";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnResetBadProxy
            // 
            this.btnResetBadProxy.Location = new System.Drawing.Point(643, 5);
            this.btnResetBadProxy.Margin = new System.Windows.Forms.Padding(4);
            this.btnResetBadProxy.Name = "btnResetBadProxy";
            this.btnResetBadProxy.Size = new System.Drawing.Size(148, 28);
            this.btnResetBadProxy.TabIndex = 19;
            this.btnResetBadProxy.Text = "Reset Bad Proxy";
            this.btnResetBadProxy.UseVisualStyleBackColor = true;
            this.btnResetBadProxy.Click += new System.EventHandler(this.BtnResetBadProxy_Click);
            // 
            // lblProxyes
            // 
            this.lblProxyes.AutoSize = true;
            this.lblProxyes.Location = new System.Drawing.Point(457, 49);
            this.lblProxyes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProxyes.Name = "lblProxyes";
            this.lblProxyes.Size = new System.Drawing.Size(12, 17);
            this.lblProxyes.TabIndex = 18;
            this.lblProxyes.Text = ".";
            // 
            // btnSaveProxyes
            // 
            this.btnSaveProxyes.Location = new System.Drawing.Point(459, 6);
            this.btnSaveProxyes.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveProxyes.Name = "btnSaveProxyes";
            this.btnSaveProxyes.Size = new System.Drawing.Size(153, 28);
            this.btnSaveProxyes.TabIndex = 17;
            this.btnSaveProxyes.Text = "Save Proxyes";
            this.btnSaveProxyes.UseVisualStyleBackColor = true;
            this.btnSaveProxyes.Click += new System.EventHandler(this.BtnSaveProxyes_Click);
            // 
            // rtbProxyes
            // 
            this.rtbProxyes.Location = new System.Drawing.Point(7, 5);
            this.rtbProxyes.Margin = new System.Windows.Forms.Padding(4);
            this.rtbProxyes.Name = "rtbProxyes";
            this.rtbProxyes.Size = new System.Drawing.Size(441, 509);
            this.rtbProxyes.TabIndex = 16;
            this.rtbProxyes.Text = "";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 554);
            this.Controls.Add(this.tabControlMain);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmToSellingHoldTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmFromSellingHoldTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmAdditThreadsNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmThreadsNum)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmReqMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmTimeOutProxySec)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tPmessages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMessages)).EndInit();
            this.tPErrors.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvErrors)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tPLogRecords.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogOfActions)).EndInit();
            this.tPAvailableItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableItems)).EndInit();
            this.tabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReqTimeInfo)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmItemsPriceTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmItemsPriceFrom)).EndInit();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmMaxPrice)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDelAcc;
        private System.Windows.Forms.Button btnAddAcc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAccPassAdd;
        private System.Windows.Forms.TextBox txtAccLoginAdd;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnResetBadProxy;
        private System.Windows.Forms.Label lblProxyes;
        private System.Windows.Forms.Button btnSaveProxyes;
        private System.Windows.Forms.RichTextBox rtbProxyes;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.NumericUpDown nmReqMax;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.CheckBox cbSwitchToProxyIfBanned;
        private System.Windows.Forms.CheckBox cbUsingProxyToUser;
        private System.Windows.Forms.CheckBox cbUsingProxy;
        private System.Windows.Forms.NumericUpDown nmTimeOutProxySec;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridView dgvLogOfActions;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tPmessages;
        private System.Windows.Forms.DataGridView dgvMessages;
        private System.Windows.Forms.DataGridViewTextBoxColumn MesMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn MesTime;
        private System.Windows.Forms.TabPage tPErrors;
        private System.Windows.Forms.DataGridView dgvErrors;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrTime;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblSLAT;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn Account;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColLogId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColLogPairs;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColLogStrategy;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColLogResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColLogTime;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label lblSR;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnFillNeedItems;
        private System.Windows.Forms.Button btnFillAndRefreshNeedItems;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tPLogRecords;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.DataGridView dgvReqTimeInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReqExecuteTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeBetween;
        private System.Windows.Forms.NumericUpDown nmMaxPrice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbAutoSelling;
        private System.Windows.Forms.CheckBox cbAutoSellingWhenBuy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nmThreadsNum;
        private System.Windows.Forms.CheckBox cbOnlyBuyed;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbDisplayedItemsNotUsing;
        private System.Windows.Forms.RadioButton rbDisplayedItemsUsing;
        private System.Windows.Forms.RadioButton rbDisplayedItems;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.RadioButton rbSortByCost;
        private System.Windows.Forms.RadioButton rbSortByPopularity;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckBox cbDescending;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nmItemsPriceTo;
        private System.Windows.Forms.NumericUpDown nmItemsPriceFrom;
        private System.Windows.Forms.Button btnAdditionallyClear;
        private System.Windows.Forms.Button btnAdditionallyAccept;
        private System.Windows.Forms.Label lblBlockedIP;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nmAdditThreadsNum;
        private System.Windows.Forms.CheckBox cbOnlyAdditional;
        private System.Windows.Forms.Label lblBadProxyCount;
        private System.Windows.Forms.Label lblUsingProxyCount;
        private System.Windows.Forms.CheckBox cbUsingProxyAdditional;
        private System.Windows.Forms.NumericUpDown nmFromSellingHoldTime;
        private System.Windows.Forms.NumericUpDown nmToSellingHoldTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabPage tPAvailableItems;
        private System.Windows.Forms.DataGridView dgvAvailableItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn AIID;
        private System.Windows.Forms.DataGridViewTextBoxColumn AITitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn AIState;
        private System.Windows.Forms.DataGridViewTextBoxColumn AILastOrderInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn AIPositionOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn AILowestPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn AIMyPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn AIBoghtPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn AIBoughtTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id_item;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title_item;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn Popularity_item;
        private System.Windows.Forms.DataGridViewTextBoxColumn FixPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Percent_item;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinAmountProfit;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinPecentProfit;
        private System.Windows.Forms.DataGridViewTextBoxColumn OnlyGlobalChecking;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinPriceSell;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxDropAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxDropPercent;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxDPByAVG;
        private System.Windows.Forms.DataGridViewTextBoxColumn AutoDrop;
        private System.Windows.Forms.Label lblVersion;
    }
}

