using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace LDmp
{
    public enum ChartPeriod { day, week, month, all }
    public partial class ManualItemSelling : Form
    {
        System.Threading.AutoResetEvent are = new System.Threading.AutoResetEvent(false);

        int item_id;
        int entity_id;
        public ManualItemSelling(int entity_id, int item_id)
        {
            InitializeComponent();
            this.entity_id = entity_id;
            this.item_id = item_id;
        }

        private void NmItemSettingMinPriceSell_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }
        static List<DBItemTradeStats> GetNeededIntervalSstats(List<DBItemTradeStats> tmpStats, int hours)
        {
            if (tmpStats == null || tmpStats.Count == 0)
                return null;
            var itemTradeStats = new List<DBItemTradeStats>();
            if (hours <= 1)
            {
                for (int i = 0; i < tmpStats.Count; i++)
                {
                    //if (i > 0 && tmpStats[i].time != tmpStats[i - 1].time.AddHours(1))
                    //    tmpStats.Insert(i, new DBItemTradeStats()
                    //    {
                    //        entity_id = tmpStats[i].entity_id,
                    //        sale_count = 0,
                    //        sum = 0,
                    //        time = tmpStats[i - 1].time.AddHours(1)
                    //    });
                    itemTradeStats.Add(new DBItemTradeStats()
                    {
                        entity_id = tmpStats[i].entity_id,
                        sale_count = tmpStats[i].sale_count,
                        sum = Math.Round(tmpStats[i].sum * Global.settings.EuroRate, 2, MidpointRounding.AwayFromZero),
                        time = tmpStats[i].time.ToLocalTime()
                    });
                }
                return itemTradeStats;
            }
            decimal avgSum = 0;
            int totalSaleCount = 0;
            int curHours = 0;
            DateTime lastTime = DateTime.MinValue;
            for (int i = 0; i < tmpStats.Count; i++)
            {
                var curStat = tmpStats[i];
                if (lastTime == DateTime.MinValue)
                    lastTime = curStat.time;

                curHours++;
                avgSum += curStat.sum;
                totalSaleCount += curStat.sale_count;

                if (i == tmpStats.Count - 1 || (tmpStats[i + 1].time - lastTime).TotalHours > hours)
                {
                    itemTradeStats.Add(new DBItemTradeStats()
                    {
                        entity_id = curStat.entity_id,
                        sale_count = totalSaleCount,
                        sum = Math.Round(avgSum / curHours * Global.settings.EuroRate, 2, MidpointRounding.AwayFromZero),
                        time = curStat.time.ToLocalTime()
                    });
                    avgSum = 0;
                    totalSaleCount = 0;
                    curHours = 0;
                    lastTime = curStat.time;
                }
            }
            return itemTradeStats;
        }
        static public Chart GetChart(int entity_id, ChartPeriod period)
        {
            List<DBItemTradeStats> itemTradeStats = null;
            List<DBItemTradeStats> tmpStats;
            var waiterTask = Global.dbLogic.GetAwaiterForTradeStats();
            waiterTask.Wait();
            switch (period)
            {
                case ChartPeriod.day:
                    tmpStats = Global.dbLogic.GetItemTradeStatsFromDB(entity_id, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow);
                    itemTradeStats = GetNeededIntervalSstats(tmpStats, 1);
                    break;
                case ChartPeriod.week:
                    tmpStats = Global.dbLogic.GetItemTradeStatsFromDB(entity_id, DateTime.UtcNow.AddDays(-7), DateTime.UtcNow);
                    itemTradeStats = GetNeededIntervalSstats(tmpStats, 6);
                    break;
                case ChartPeriod.month:
                    tmpStats = Global.dbLogic.GetItemTradeStatsFromDB(entity_id, DateTime.UtcNow.AddDays(-30), DateTime.UtcNow);
                    itemTradeStats = GetNeededIntervalSstats(tmpStats, 24);
                    break;
                case ChartPeriod.all:
                    tmpStats = Global.dbLogic.GetItemTradeStatsFromDB(entity_id);
                    itemTradeStats = GetNeededIntervalSstats(tmpStats, 24);
                    break;
                default:
                    break;
            }
            if (itemTradeStats == null)
                return null;

            var bs = new BindingSource();
            bs.DataSource = itemTradeStats;


            var chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chart.DataSource = bs;
            chart.Legends.Clear();
            chart.ChartAreas.Clear();
            chart.Dock = System.Windows.Forms.DockStyle.Fill;

            var mainArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea("MainArea");
            chart.ChartAreas.Add(mainArea);
            mainArea.AxisX.LabelStyle.Format = "dd.MM.yyyy HH:mm:ss";
            mainArea.AxisX.LabelStyle.Angle = -60;
            mainArea.AxisX.LabelStyle.Font = new System.Drawing.Font("Arial", 7);
            mainArea.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            ////mainArea.AxisX.LabelStyle.IntervalType = DateTimeIntervalType.Hours;
            mainArea.AxisX.Interval = 1;
            ////mainArea.AxisX.IntervalType = DateTimeIntervalType.Hours;
            //mainArea.AxisX.LabelStyle.Interval = 1;
            //mainArea.AxisX.IsLabelAutoFit = false;
            mainArea.AxisX.TitleFont = new System.Drawing.Font("Verdana", 11, System.Drawing.FontStyle.Bold);
            //mainArea.AxisX.LabelStyle.IntervalType = DateTimeIntervalType.Auto;
            //mainArea.AxisX.LabelStyle.IntervalType = DateTimeIntervalType.Auto;
            //mainArea.AxisX.Title = "Market Stats";
            mainArea.AxisY.Title = "Sales";
            mainArea.AxisY2.Title = "Sum";
            //mainArea.AxisX.MajorGrid.Enabled = false;
            //mainArea.AxisX.MajorGrid.LineWidth = 0;
            mainArea.AxisY.MajorGrid.Enabled = false;
            
            chart.Legends.Add("Legend1");
            chart.Legends[0].Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            chart.Legends[0].Alignment = StringAlignment.Center;
            //chart.Legends.Add("Legend1");

            chart.Series.Clear();
            var series = chart.Series.Add($"Sales");
            series.IsXValueIndexed = true;
            series.ChartArea = "MainArea";
            series.IsValueShownAsLabel = true;
            series.ChartType = SeriesChartType.Column;
            series.XValueType = ChartValueType.DateTime;
            series.XValueMember = "time";
            series.YValueType = ChartValueType.Int32;
            series.YValueMembers = "sale_count";
            series.Color = System.Drawing.Color.DeepSkyBlue;
            //series.ToolTip = "Date = #VALX, Sales = #VALY";

            series = chart.Series.Add($"Sum");
            series.IsXValueIndexed = true;
            series.IsValueShownAsLabel = true;
            series.Font = new System.Drawing.Font("Arial", 7, FontStyle.Italic);
            //series.LabelForeColor = Color.Green;
            series.ChartArea = "MainArea";
            series.XValueType = ChartValueType.DateTime;
            series.ChartType = SeriesChartType.Line;
            series.BorderWidth = 2;
            series.BorderColor = System.Drawing.Color.DarkOrange;
            series.XValueMember = "time";
            series.YValueType = ChartValueType.Double;
            series.YValueMembers = "sum";
            series.Color = System.Drawing.Color.Green;
            series.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            //series.ToolTip = "Date = #VALX, Sum = #VALY";

            return chart;
        }

        bool formLoaded = false;
        AvailableItemInfo aii = null;
        ExecutingItem execI;
        private async void ManualItemSelling_Load(object sender, EventArgs e)
        {
            cbPeriod.SelectedIndex = 0;
            foreach (Control item in groupBox2.Controls)
            {
                item.Enabled = false;
            }
            lblMISInfo.Text = "Waiting for current selling checkings is done...";
            AII lowestValues = null;
            
            Chart chart = null;
            string title = null;
            ChartPeriod periodSelected = (ChartPeriod)cbPeriod.SelectedIndex;
            await Task.Run(() =>
            {
                execI = PublicRequestManager.SetSellingWhenFree(entity_id, are);
                lowestValues = AvailableItemsManager.GetPrevOrdersStats(entity_id);
                aii = AvailableItemsManager.GetItemInfo(entity_id, item_id);
                title = Global.dbLogic.GetDBItems().Find(x => x.entity_id == entity_id)?.title;
                chart = GetChart(entity_id, periodSelected);
            });

            txtBoughtPrice.Text = aii?.boughtPrice.ToString();
            txtMinPosOrder.Text = lowestValues?.position;
            txtMinPrice.Text = lowestValues?.minPrice.ToString();
            txtMyMinPrice.Text = aii?.myPrice.ToString();
            if (aii != null)
            {
                cbSellLessThenBought.Checked = aii.sellLessThenBought;
                cbDontUp.Checked = aii.dontUpItem;
                cbDontCheckSecondPrice.Checked = aii.dontCheckSecondPrice;
            }
            if (!String.IsNullOrEmpty(txtMinPrice.Text))
            {
                char separ = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
                string minP = txtMinPrice.Text.Replace('.', separ).Replace(',', separ);
                if (decimal.TryParse(minP, out decimal minPrice))
                {
                    if (minPrice > 3.02m)
                        nmItemSettingMinPriceSell.Value = minPrice - 0.01m;
                }
            }
            lblMISInfo.Text = title == null ? "???" : title;
            SetChartToForm(chart);
            foreach (Control item in groupBox2.Controls)
            {
                item.Enabled = true;
            }
            formLoaded = true;
            if (aii == null)
                btnSell.Enabled = false;
        }

        void SetChartToForm(Chart chart)
        {
            if (chart == null)
                return;
            string chrtName = "myChart_";
            chart.Name = chrtName;
            if (tableLayoutPanel1.Controls.ContainsKey(chrtName))
                tableLayoutPanel1.Controls.RemoveByKey(chrtName);
            tableLayoutPanel1.Controls.Add(chart, 0, 1);
        }

        private void ManualItemSelling_FormClosed(object sender, FormClosedEventArgs e)
        {
            are.Set();
            //are.Close();
        }

        private void BtnCancelSell_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        async private void CbPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!formLoaded)
                return;
            cbPeriod.Enabled = false;
            Chart chart = null;
            ChartPeriod periodSelected = (ChartPeriod)cbPeriod.SelectedIndex;
            await Task.Run(() =>
                  chart = GetChart(entity_id, periodSelected)
            );
            SetChartToForm(chart);
            cbPeriod.Enabled = true;
        }

        async private void BtnSell_Click(object sender, EventArgs e)
        {
            decimal mySellPrice = nmItemSettingMinPriceSell.Value;
            if (mySellPrice < 3) return;
            btnSell.Enabled = false;
            bool canSellLessThenBought = cbSellLessThenBought.Checked;
            bool dontUpItem = cbDontUp.Checked;
            bool dontCheckSecondPrice = cbDontCheckSecondPrice.Checked;
            bool selled = false;
            await Task.Run(() =>
            {
                if (String.IsNullOrEmpty(aii.order_id) || MainWork.WorkingReqManager.CancelOrder(aii.order_id))
                {
                    var info = UnitChecker.SellItem(execI, item_id, mySellPrice);
                    if (!String.IsNullOrEmpty(info?.id))
                    {
                        AvailableItemsManager.SetOrderToItem(entity_id, item_id, info.id, mySellPrice, new AdditSettingsAItem()
                        {
                            dontCheckSecondPrice = dontCheckSecondPrice,
                            dontUpItem = dontUpItem,
                            sellLessThenBought = canSellLessThenBought
                        });
                        selled = true;
                    }
                }
            });
            if (selled)
            {
                this.Close();
                return;
            }
            MessageBox.Show("Something wrong!");
            btnSell.Enabled = true;
        }

        async private void BtnApplyAndClose_Click(object sender, EventArgs e)
        {
            bool canSellLessThenBought = cbSellLessThenBought.Checked;
            bool dontUpItem = cbDontUp.Checked;
            bool dontCheckSecondPrice = cbDontCheckSecondPrice.Checked;
            await Task.Run(() =>
            {
                if (String.IsNullOrEmpty(aii.order_id) || MainWork.WorkingReqManager.CancelOrder(aii.order_id))
                {
                    AvailableItemsManager.SetAdditSettingsAItem(entity_id, item_id, new AdditSettingsAItem()
                    {
                        dontCheckSecondPrice = dontCheckSecondPrice,
                        dontUpItem = dontUpItem,
                        sellLessThenBought = canSellLessThenBought
                    });
                }
            });
            this.Close();
        }
    }
}
