using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LDmp
{
    public partial class Form1 : Form
    {
        MainWork work;
        public Form1()
        {
            InitializeComponent();
            AppSettingsManager.LoadSettings();
            Global.UIdata.LogOfRequestsTimesUI.SetRecordsLimit(Global.settings.RequestTimesCount);
            Global.proxyLogic.SomeError += ProxyLogic_SomeError;
            FillControlsBySettings();
        }

        void FillControlsBySettings()
        {
            nmReqMax.Value = Global.settings.ReqMax;
            nmTimeOutProxySec.Value = Global.settings.TimeOutProxySec;
            cbUsingProxy.Checked = Global.settings.UsingProxy;
            cbUsingProxyToUser.Checked = Global.settings.UsingProxyToUser;
            cbSwitchToProxyIfBanned.Checked = Global.settings.SwitchToProxyIfBanned;
            cbAutoSelling.Checked = Global.settings.AutoSelling;
            cbAutoSellingWhenBuy.Checked = Global.settings.AutoSellingWhenBuy;
            nmMaxPrice.Value = Global.settings.MaxPrice;
            nmThreadsNum.Value = Global.settings.ThreadsNum;
            nmAdditThreadsNum.Value = Global.settings.AdditThreadsNum;
            cbOnlyAdditional.Checked = Global.settings.OnlyAdditional;
            cbUsingProxyAdditional.Checked = Global.settings.UsingProxyAdditional;
            nmFromSellingHoldTime.Value = Global.settings.FromSellingHoldTime;
            nmToSellingHoldTime.Value = Global.settings.ToSellingHoldTime;
            alreadyFillingCtrls = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvLogOfActions.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgvLogOfActions.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //dgvLogOfActions.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvLogOfActions.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dgvLogOfActions.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvLogOfActions.Columns[5].Width = 70;
            dgvLogOfActions.Columns[5].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm:ss.fff";
            dgvLogOfActions.DoubleBuffered(true);

            dgvMessages.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgvMessages.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvMessages.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvMessages.Columns[1].Width = 70;
            dgvMessages.Columns[1].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm:ss.fff";

            dgvErrors.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgvErrors.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvErrors.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvErrors.Columns[1].Width = 70;
            dgvErrors.Columns[1].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm:ss.fff";

            dgvItems.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgvItems.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            for (int i = 0; i < dgvItems.Columns.Count; i++)
            {
                dgvItems.Columns[i].Width = 70;
            }
            dgvItems.Columns[1].Width = 100;
            dgvItems.Columns[1].MinimumWidth = 100;
            dgvItems.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvItems.DoubleBuffered(true);

            dgvAvailableItems.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgvAvailableItems.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvAvailableItems.DoubleBuffered(true);
            dgvAvailableItems.Columns[3].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm:ss.fff";
            dgvAvailableItems.Columns[8].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm:ss.fff";
            for (int i = 0; i < dgvAvailableItems.Columns.Count; i++)
            {
                dgvAvailableItems.Columns[i].Width = 70;
            }
            dgvAvailableItems.Columns[1].Width = 100;
            dgvAvailableItems.Columns[1].MinimumWidth = 100;
            dgvAvailableItems.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvReqTimeInfo.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgvReqTimeInfo.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvReqTimeInfo.Columns[1].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm:ss.fff";
            dgvReqTimeInfo.Columns[2].DefaultCellStyle.Format = @"hh\:mm\:ss\.fff";

            lblVersion.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            work = new MainWork();

            FillProxyes();

            FillAccount(MainWork.AccountUser);
        }

        void FillAccount(AccountManager am)
        {
            var acc = am.GetAccounts();
            txtAccLoginAdd.Text = acc.Login;
            txtAccPassAdd.Text = acc.Pass;
        }

        private void ProxyLogic_SomeError(Exception obj)
        {
            Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage(obj.Message));
        }

        private void BtnSaveProxyes_Click(object sender, EventArgs e)
        {
            btnSaveProxyes.Enabled = false;
            string[] stringArray = rtbProxyes.Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in stringArray)
            {
                string[] str = item.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (str.Count() == 4)
                {
                    ProxyInfo prx = new ProxyInfo(str[0].Trim(), str[1].Trim(), str[2].Trim(), str[3].Trim());

                    Global.proxyLogic.AddProxy(prx);
                }
            }

            FillProxyes();
            btnSaveProxyes.Enabled = true;
        }

        void FillProxyes()
        {
            List<ProxyInfo> list = Global.proxyLogic.GetProxyList();

            rtbProxyes.Clear();
            foreach (var item in list)
            {
                rtbProxyes.AppendText($"{item.address}:{item.port}:{item.login}:{item.pass}{Environment.NewLine}");
            }

            lblProxyes.Text = list.Count.ToString();
        }

        private void BtnResetBadProxy_Click(object sender, EventArgs e)
        {
            btnResetBadProxy.Enabled = false;
            Global.proxyLogic.ReseetBadProy();
            btnResetBadProxy.Enabled = true;
        }

        private void Label5_Click(object sender, EventArgs e)
        {
            if (txtAccPassAdd.PasswordChar == '\0')
                txtAccPassAdd.PasswordChar = '*';
            else
                txtAccPassAdd.PasswordChar = '\0';
        }

        private void BtnAddAcc_Click(object sender, EventArgs e)
        {
            if (txtAccLoginAdd.Text.Length < 5 || txtAccPassAdd.Text.Length < 5)
            {
                MessageBox.Show("Enter Login and Password");
                return;
            }
            MainWork.AccountUser.AddAccount(txtAccLoginAdd.Text, txtAccPassAdd.Text);
        }

        private void BtnDelAcc_Click(object sender, EventArgs e)
        {
            MainWork.AccountUser.DelAccount();
            txtAccLoginAdd.Text = txtAccPassAdd.Text = "";
        }

        bool alreadyFillingCtrls = false;
        void SettingChanged(object sender, EventArgs e)
        {
            if (!alreadyFillingCtrls)
                return;
            if (sender is NumericUpDown)
            {
                string propName = ((NumericUpDown)sender).Name.Replace("nm", "");
                Global.settings.GetType().GetProperty(propName).SetValue(Global.settings, (int)((NumericUpDown)sender).Value, null);
            }
            else if (sender is CheckBox)
            {
                string propName = ((CheckBox)sender).Name.Replace("cb", "");
                Global.settings.GetType().GetProperty(propName).SetValue(Global.settings, ((CheckBox)sender).Checked, null);
            }
            Global.settings.Save(AppSettingsManager.GetSettingsPath());
        }

        bool timerStarted = false;
        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (!timerStarted)
            {
                timerStarted = true;
                timer1.Start();
            }
            if (MainWork.Running)
                MakeAPause();
            else
                MakeAStart();
        }

        void MakeAPause()
        {
            btnStart.Enabled = false;
            btnStart.Text = "Pausing...";
            work.Stop();
        }

        void MakeAStart()
        {
            work.Start();
            btnStart.Text = "Pause";
        }

        List<UILogOfAction> logOfActions = null;
        List<AvailableItemsForDGV> availableItems = null;
        //List<UILogOfRequestTime> LogOfRequestTime = null;
        readonly object availableItemsLocker = new object();
        private void Timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            if (!MainWork.Running)
            {
                btnStart.Enabled = true;
                btnStart.Text = "Start";
            }

            bool lastVisible = LastRowIsVisible(dgvErrors);
            if (dgvErrors.RowCount != Global.UIdata.ErrororsUI.GetMessagesCount())
                dgvErrors.RowCount = Global.UIdata.ErrororsUI.GetMessagesCount();
            if (lastVisible) AutoscrollToLastRow(dgvErrors);
            lastVisible = LastRowIsVisible(dgvMessages);
            if (dgvMessages.RowCount != Global.UIdata.MessagesUI.GetMessagesCount())
                dgvMessages.RowCount = Global.UIdata.MessagesUI.GetMessagesCount();
            if (lastVisible) AutoscrollToLastRow(dgvMessages);
            lastVisible = LastRowIsVisible(dgvLogOfActions);
            if (cbOnlyBuyed.Checked)
            {
                var curLogs = Global.UIdata.LogOfActionsUI.GetColoredMessages(System.Drawing.Color.LightGreen);
                if (dgvLogOfActions.RowCount != curLogs.Count())
                    dgvLogOfActions.RowCount = curLogs.Count();
                logOfActions = curLogs;
            }
            else
            {
                logOfActions = Global.UIdata.LogOfActionsUI.GetAllMessages();
                if (dgvLogOfActions.RowCount != logOfActions.Count())
                    dgvLogOfActions.RowCount = logOfActions.Count();
            }
            if (lastVisible) AutoscrollToLastRow(dgvLogOfActions);
            if (dgvReqTimeInfo.RowCount == 0 || LastRowIsVisible(dgvReqTimeInfo))
            {
                dgvReqTimeInfo.RowCount = Global.UIdata.LogOfRequestsTimesUI.GetMessagesCount();
                //LogOfRequestTime = Global.UIdata.LogOfRequestsTimesUI.GetAllMessages();
                AutoscrollToLastRow(dgvReqTimeInfo);
                if (dgvReqTimeInfo.RowCount == Global.settings.RequestTimesCount)
                    dgvReqTimeInfo.Refresh();
            }

            lock (availableItemsLocker)
            {
                var curAvailabbleItems = AvailableItemsManager.GetAvailableItemsForDGV();
                if (dgvAvailableItems.RowCount != curAvailabbleItems.Count)
                {
                    if (curAvailabbleItems.Count > availableItems.Count)
                    {
                        availableItems = curAvailabbleItems;
                        dgvAvailableItems.RowCount = curAvailabbleItems.Count;
                    }
                    else
                    {
                        dgvAvailableItems.Rows.Clear();
                        availableItems = curAvailabbleItems;
                        if (curAvailabbleItems.Count > 0)
                            dgvAvailableItems.RowCount = curAvailabbleItems.Count;
                    }
                }
                else
                {
                    availableItems = curAvailabbleItems;
                    dgvAvailableItems.Refresh();
                }
            }
            
            //FillDGVCheckMaxRecords(dgvErrors, uiData.ErrororsUI);
            //FillDGVCheckMaxRecords(dgvMessages, uiData.MessagesUI);
            //FillDGVCheckMaxRecords(dgvLogOfActions, uiData.LogOfActionsUI);

            //FillReqsTimeInfo();

            lblSLAT.Text = $"Socket LastAT: {WebSocketLogic.GetLastActiveTime()}";
            lblBlockedIP.Text = $"Blocked IP: {Global.proxyLogic.GetBannedIP()}";
            lblUsingProxyCount.Text = $"Using proxy: {Global.proxyLogic.GetUsingProxyCount()}";
            lblBadProxyCount.Text = $"Bad proxy: {Global.proxyLogic.GetBadProxyCount()}";
            tPLogRecords.Text = $"Log records ({dgvLogOfActions.RowCount})";
            tPAvailableItems.Text = $"Available Items ({dgvAvailableItems.RowCount})";
            tPmessages.Text = $"Messages ({dgvMessages.RowCount})";
            tPErrors.Text = $"Errors ({dgvErrors.RowCount})";

            timer1.Start();
        }

        bool LastRowIsVisible(DataGridView dgv)
        {
            var vivibleRowsCount = dgv.DisplayedRowCount(true);
            var firstDisplayedRowIndex = dgv.FirstDisplayedCell?.RowIndex;
            if (firstDisplayedRowIndex == null)
                return false;
            var lastvibileRowIndex = (firstDisplayedRowIndex + vivibleRowsCount);
            return lastvibileRowIndex == dgv.RowCount;
        }

        void AutoscrollToLastRow(DataGridView dgv)
        {
            if (dgv.RowCount > 0)
                dgv.FirstDisplayedScrollingRowIndex = dgv.RowCount - 1;
        }

        void ReFillItems()
        {
            FillDispalyedList();
            if (displayedListItems.Count == 0)
                lblSR.Text = "Not found";
            else
                lblSR.Text = "";
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ReFillItems();
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            lblSR.Text = "";
        }

        async private void BtnFillAndRefreshNeedItems_Click(object sender, EventArgs e)
        {
            btnFillAndRefreshNeedItems.Enabled = false;
            UnitChecker unitChecker = new UnitChecker(Global.settings.UsingProxy, false);
            bool initiatedChecker = false;
            await Task.Run(() =>
            {
                initiatedChecker = unitChecker.Init(null);
                if (!initiatedChecker)
                    return;
                unitChecker.GetItemsList();
            });
            unitChecker.Dispose();
            if (!initiatedChecker)
            {
                MessageBox.Show("Can't initiated checker!");
                btnFillAndRefreshNeedItems.Enabled = true;
                return;
            }

            Global.dbLogic.QueueIsEmpty.WaitOne();
            //while (Global.dbLogic.GetQueueCount() > 0)
            //{
            //    await Task.Delay(50);
            //}
            FillItemsLists();
            btnFillAndRefreshNeedItems.Enabled = true;
        }

        List<ItemsInfoForDGV> fullListItems = new List<ItemsInfoForDGV>();
        public static List<ItemsInfoForDGV> displayedListItems = new List<ItemsInfoForDGV>();
        void FillItemsLists()
        {
            var items = Global.dbLogic.GetDBItems();
            var lastPrices = Global.dbLogic.GetDBLastPrices();
            fullListItems.Clear();
            foreach (var item in items)
            {
                ItemSettings itemS = new ItemSettings() { entity_id = item.entity_id };
                if (Global.itemsManager.ItemsSettings.TryGetValue(item.entity_id, out ItemSettings itemSettings))
                    itemS = itemSettings;
                decimal lastPrice = 0;
                int lastPopularity = 0;
                int lastCount = 0;
                var lp = lastPrices.Find(x => x.entity_id == item.entity_id);
                if (lp != null)
                {
                    lastPrice = lp.min_cost;
                    lastPopularity = lp.popularity;
                    lastCount = lp.count;
                }
                fullListItems.Add(new ItemsInfoForDGV()
                {
                    count = lastCount,
                    itemSettings = itemS,
                    last_cost = lastPrice,
                    popularity = lastPopularity,
                    title = item.title
                });
            }
            FillDispalyedList();
        }
        void FillDispalyedList()
        {
            int prevRowsCount = dgvItems.RowCount;
            List<ItemsInfoForDGV> newDisplayedListItems = new List<ItemsInfoForDGV>();
            if (rbDisplayedItems.Checked)
            {
                newDisplayedListItems = fullListItems;
            }
            else if (rbDisplayedItemsUsing.Checked)
            {
                newDisplayedListItems = fullListItems.Where(x => x.itemSettings.buyingSettings.IsBuyedItem() || x.itemSettings.sellingSettings.IsSellnigItem()).ToList();
            }
            else if (rbDisplayedItemsNotUsing.Checked)
            {
                newDisplayedListItems = fullListItems.Where(x => !x.itemSettings.buyingSettings.IsBuyedItem() && !x.itemSettings.sellingSettings.IsSellnigItem()).ToList();
            }
            if (txtSearch.Text.Length >= 2)
            {
                newDisplayedListItems = newDisplayedListItems.Where(x => x.title.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
            }
            if (rbSortByPopularity.Checked)
                newDisplayedListItems.Sort((x, y) => cbDescending.Checked ? -x.popularity.CompareTo(y.popularity) : x.popularity.CompareTo(y.popularity));
            else if (rbSortByCost.Checked)
                newDisplayedListItems.Sort((x, y) => cbDescending.Checked ? -x.last_cost.CompareTo(y.last_cost) : x.last_cost.CompareTo(y.last_cost));
            if (nmItemsPriceFrom.Value >= 3)
                newDisplayedListItems = newDisplayedListItems.Where(x => x.last_cost >= nmItemsPriceFrom.Value).ToList();
            if (nmItemsPriceTo.Value > 3)
                newDisplayedListItems = newDisplayedListItems.Where(x => x.last_cost <= nmItemsPriceTo.Value).ToList();
            if (prevRowsCount == newDisplayedListItems.Count)
            {
                displayedListItems = newDisplayedListItems;
                dgvItems.Refresh();
            }
            else
            {
                dgvItems.RowCount = 0;
                displayedListItems = newDisplayedListItems;
                dgvItems.RowCount = displayedListItems.Count;
                //if (displayedListItems.Count <= newDisplayedListItems.Count)
                //{
                //    displayedListItems = newDisplayedListItems;
                //    dgvItems.RowCount = newDisplayedListItems.Count;
                //}
                //else
                //{
                //    dgvItems.RowCount = newDisplayedListItems.Count;
                //    displayedListItems = newDisplayedListItems;
                //}
            }
        }
        private void BtnFillNeedItems_Click(object sender, EventArgs e)
        {
            btnFillNeedItems.Enabled = false;
            FillItemsLists();
            btnFillNeedItems.Enabled = true;
        }

        private void DgvLogOfActions_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                var row = logOfActions?[e.RowIndex];
                if (row == null) return;
                switch (e.ColumnIndex)
                {
                    case 0: e.Value = row.login; break;
                    case 1: e.Value = row.entity_id; break;
                    case 2: e.Value = row.title; break;
                    case 3: e.Value = row.message; break;
                    case 4: e.Value = row.cost; break;
                    case 5: e.Value = row.TimeRecord; break;
                }
                dgvLogOfActions.Rows[e.RowIndex].DefaultCellStyle.BackColor = row.color;
            }
            catch { }
        }

        private void DgvMessages_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            var row = Global.UIdata?.MessagesUI?.GetMessageByIndex(e.RowIndex);
            if (row == null) return;
            switch (e.ColumnIndex)
            {
                case 0: e.Value = row.message; break;
                case 1: e.Value = row.TimeRecord; break;
            }
        }

        private void DgvErrors_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            var row = Global.UIdata?.ErrororsUI?.GetMessageByIndex(e.RowIndex);
            if (row == null) return;
            switch (e.ColumnIndex)
            {
                case 0: e.Value = row.message; break;
                case 1: e.Value = row.TimeRecord; break;
            }
        }

        private void DgvReqTimeInfo_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            var row = Global.UIdata.LogOfRequestsTimesUI.GetMessageByIndex(e.RowIndex);
            if (row == null) return;
            switch (e.ColumnIndex)
            {
                case 0: e.Value = row.message; break;
                case 1: e.Value = row.TimeRecord; break;
                case 2: e.Value = row.TimeRecord - row.startTime; break;
                case 3:
                    if (e.RowIndex == 0)
                    {
                        e.Value = "-";
                        break;
                    }
                    e.Value = (Global.UIdata.LogOfRequestsTimesUI.GetMessageByIndex(e.RowIndex).TimeRecord - Global.UIdata.LogOfRequestsTimesUI.GetMessageByIndex(e.RowIndex - 1).TimeRecord).ToString(@"hh\:mm\:ss\.fff");
                    break;
            }
            dgvReqTimeInfo.Rows[e.RowIndex].DefaultCellStyle.BackColor = row.color;
        }

        private void DgvItems_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            var row = displayedListItems?[e.RowIndex];
            if (row == null) return;
            switch (e.ColumnIndex)
            {
                case 0: e.Value = row.itemSettings.entity_id; break;
                case 1: e.Value = row.title; break;
                case 2: e.Value = row.last_cost; break;
                case 3: e.Value = row.count; break;
                case 4: e.Value = row.popularity; break;
                case 5: e.Value = row.itemSettings.buyingSettings.fixPrice; break;
                case 6: e.Value = row.itemSettings.buyingSettings.percent; break;
                case 7: e.Value = row.itemSettings.buyingSettings.maxPrice; break;
                case 8: e.Value = row.itemSettings.buyingSettings.minProfitAmount; break;
                case 9: e.Value = row.itemSettings.buyingSettings.minProfitPercent; break;
                case 10: e.Value = row.itemSettings.buyingSettings.onlyGlobal ? "X" : ""; break;
                case 11: e.Value = row.itemSettings.sellingSettings.minPrice; break;
                case 12: e.Value = row.itemSettings.sellingSettings.maxDropAmount; break;
                case 13: e.Value = row.itemSettings.sellingSettings.maxDropPercent; break;
                case 14: e.Value = row.itemSettings.sellingSettings.maxDropPercentByAvg; break;
                case 15: e.Value = row.itemSettings.sellingSettings.autoDrop ? "X" : ""; break;
            }
            if (e.ColumnIndex > 4 && e.ColumnIndex < 10)
            {
                try
                {
                    var val = Convert.ToDecimal(e.Value);
                    if (val > 0)
                        dgvItems.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.LightGreen; // System.Drawing.Color.LightPink
                    else
                        dgvItems.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.Empty;
                }
                catch (Exception ex)
                {

                }
            }
            else if (e.ColumnIndex > 10 && e.ColumnIndex < 15)
            {
                try
                {
                    var val = Convert.ToDecimal(e.Value);
                    if (val > 0)
                        dgvItems.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.LightPink;
                    else
                        dgvItems.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.Empty;
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void RbDisplayedItems_CheckedChanged(object sender, EventArgs e)
        {
            ReFillItems();
        }

        private void DgvItems_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                ItemSettingsEdit itemSetForm = new ItemSettingsEdit(e.RowIndex);
                itemSetForm.ShowDialog();
                itemSetForm.Dispose();
                dgvItems.Refresh();
                //MessageBox.Show(e.RowIndex.ToString());
                //var v = dgvLogFormDB[0, e.RowIndex].Value.ToString();
                //StrategyActionInfo stratActInfo = new StrategyActionInfo(v);
                //stratActInfo.Show();
            }
        }

        private void BtnAdditionallyAccept_Click(object sender, EventArgs e)
        {
            btnAdditionallyAccept.Enabled = false;
            ReFillItems();
            btnAdditionallyAccept.Enabled = true;
        }

        private void BtnAdditionallyClear_Click(object sender, EventArgs e)
        {
            btnAdditionallyClear.Enabled = false;
            nmItemsPriceFrom.Value = 0;
            nmItemsPriceTo.Value = 0;
            ReFillItems();
            btnAdditionallyClear.Enabled = true;
        }

        private void DgvItems_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvItems.Rows[e.RowIndex].Selected = true;
        }

        private void NumericUpDown_KeyPressReplaceDotToComa(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void DgvAvailableItems_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            lock (availableItemsLocker)
            {
                try
                {
                    if (availableItems == null || availableItems?.Count < e.RowIndex + 1)
                        return;
                    var row = availableItems?[e.RowIndex];
                    if (row == null) return;
                    switch (e.ColumnIndex)
                    {
                        case 0: e.Value = row.entity_id; break;
                        case 1: e.Value = row.title; break;
                        case 2: e.Value = row.state; break;
                        case 3: e.Value = row.lastUpdateOrderInfo; break;
                        case 4: e.Value = row.position; break;
                        case 5: e.Value = row.lowestPrice; break;
                        case 6: e.Value = row.myPrice; break;
                        case 7: e.Value = row.boughPrice; break;
                        case 8: e.Value = row.boughtTime; break;
                    }
                    dgvAvailableItems.Rows[e.RowIndex].DefaultCellStyle.BackColor = row.color;
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }
        }

        private void DgvAvailableItems_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            lock (availableItemsLocker)
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    try
                    {
                        if (availableItems == null || availableItems?.Count < e.RowIndex + 1)
                            return;
                        var item = availableItems[e.RowIndex];
                        ManualItemSelling itemSetForm = new ManualItemSelling(item.entity_id, item.id);
                        itemSetForm.ShowDialog();
                        itemSetForm.Dispose();
                        dgvAvailableItems.Refresh();
                    }
                    catch { }
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global.settings.Save(AppSettingsManager.GetSettingsPath());
        }
    }

    public static class ExtensionMethods
    {
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
    }
}
