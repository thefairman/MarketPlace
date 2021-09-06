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
    public partial class ItemSettingsEdit : Form
    {
        ItemsInfoForDGV item;
        public ItemSettingsEdit(int id)
        {
            InitializeComponent();
            item = Form1.displayedListItems[id];
            FillForm();
        }

        void FillForm()
        {
            //txtItemSettingHeader.Text = $"Item info => ID: {item.itemSettings.entity_id} Name: {item.title}{Environment.NewLine}Popularity: {item.popularity} Count: {item.count} Last price: {item.last_cost}";

            lblCountISE.Text = $"Count: {item.count}";
            lblEntity_idISE.Text = $"Entity_id: {item.itemSettings.entity_id}";
            lblLastCostISE.Text = $"Last cost: {item.last_cost}";
            lblPopularityISE.Text = $"Popularity: {item.popularity}";
            lblTitleISE.Text = item.title;
            //cbItemSettingAutoDrop.Checked = item.itemSettings.sellingSettings.autoDrop;
            nmItemSettingMinPriceSell.Value = item.itemSettings.sellingSettings.minPrice;
            nmItemSettingMaxDropPercent.Value = item.itemSettings.sellingSettings.maxDropPercent;
            nmItemSettingMaxDropAmount.Value = item.itemSettings.sellingSettings.maxDropAmount;
            nmMaxDropPercentByAVG.Value = item.itemSettings.sellingSettings.maxDropPercentByAvg;

            cbItemSettingOnlyGlobal.Checked = item.itemSettings.buyingSettings.onlyGlobal;
            cbItemSettingAutoDrop.Checked = item.itemSettings.sellingSettings.autoDrop;
            nmItemSettingFixPrice.Value = item.itemSettings.buyingSettings.fixPrice;
            nmItemSettingNeedPercent.Value = item.itemSettings.buyingSettings.percent;
            nmItemSettingMaxPriceForPercent.Value = item.itemSettings.buyingSettings.maxPrice;
            nmItemSettingMinAmountProfit.Value = item.itemSettings.buyingSettings.minProfitAmount;
            nmItemSettingMinPercentProfit.Value = item.itemSettings.buyingSettings.minProfitPercent;
        }

        private void NumericUpDown_KeyPressReplaceDotToComa(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        private void NmItemSettingFixPrice_ValueChanged(object sender, EventArgs e)
        {
            if (nmItemSettingFixPrice.Value > 0 && nmItemSettingFixPrice.Value < 3)
            {
                if (fixPricePrevValue < nmItemSettingFixPrice.Value)
                    nmItemSettingFixPrice.Value = 3;
                else
                    nmItemSettingFixPrice.Value = 0;
            }
        }

        decimal fixPricePrevValue;
        private void NmItemSettingFixPrice_MouseDown(object sender, MouseEventArgs e)
        {
            fixPricePrevValue = nmItemSettingFixPrice.Value;
        }

        decimal minPriceSellPrevValue;
        private void NmItemSettingMinPriceSell_ValueChanged(object sender, EventArgs e)
        {
            if (nmItemSettingMinPriceSell.Value > 0 && nmItemSettingMinPriceSell.Value < 3)
            {
                if (minPriceSellPrevValue < nmItemSettingMinPriceSell.Value)
                    nmItemSettingMinPriceSell.Value = 3;
                else
                    nmItemSettingMinPriceSell.Value = 0;
            }
            nmItemSettingMinPriceSellWOFee.Value = nmItemSettingMinPriceSell.Value * .85m;
        }

        private void NmItemSettingMinPriceSell_MouseDown(object sender, MouseEventArgs e)
        {
            minPriceSellPrevValue = nmItemSettingMinPriceSell.Value;
        }

        private void BtnSaveItemSettings_Click(object sender, EventArgs e)
        {
            btnSaveItemSettings.Enabled = false;
            var buyinSettings = new ItemBuyingSettings()
            {
                fixPrice = nmItemSettingFixPrice.Value,
                maxPrice = nmItemSettingMaxPriceForPercent.Value,
                minProfitAmount = nmItemSettingMinAmountProfit.Value,
                minProfitPercent = nmItemSettingMinPercentProfit.Value,
                onlyGlobal = cbItemSettingOnlyGlobal.Checked,
                percent = nmItemSettingNeedPercent.Value
            };
            var sellingSettings = new ItemSellingSettings()
            {
                autoDrop = cbItemSettingAutoDrop.Checked,
                maxDropPercent = nmItemSettingMaxDropPercent.Value,
                maxDropAmount = nmItemSettingMaxDropAmount.Value,
                minPrice = nmItemSettingMinPriceSell.Value,
                maxDropPercentByAvg = nmMaxDropPercentByAVG.Value
            };
            Global.itemsManager.AddItem(item.itemSettings.entity_id, buyinSettings, sellingSettings);
            item.itemSettings.buyingSettings = buyinSettings;
            item.itemSettings.sellingSettings = sellingSettings;
            this.Close();
        }

        private void NmItemSettingMinPriceSellWOFee_ValueChanged(object sender, EventArgs e)
        {
            nmItemSettingMinPriceSell.Value = nmItemSettingMinPriceSellWOFee.Value / .85m;
        }

        bool formLoaded = false;
        async private void ItemSettingsEdit_Load(object sender, EventArgs e)
        {
            cbPeriod.SelectedIndex = 0;
            Chart chart = null;
            ChartPeriod periodSelected = (ChartPeriod)cbPeriod.SelectedIndex;
            await Task.Run(() =>
                chart = ManualItemSelling.GetChart(item.itemSettings.entity_id, periodSelected)
            );
            SetChartToForm(chart);
            SetAvgBy24H(chart);
            SetLabelMaxDropByAVG();
            formLoaded = true;
        }

        void SetLabelMaxDropByAVG()
        {
            if (nmMaxDropPercentByAVG.Value <= 0 || avgSum24H <= 0)
                return;
            lblMaxDPByAVG.Text = $"{Math.Round(avgSum24H / 100 * (100m - nmMaxDropPercentByAVG.Value), 2, MidpointRounding.AwayFromZero)}";
        }

        void SetAvgBy24H(Chart chart)
        {
            if (chart == null || chart.DataSource == null)
                return;
            var src = chart.DataSource as BindingSource;
            if (src == null)
                return;
            var dbtradeStats = src.DataSource as List<DBItemTradeStats>;
            if (dbtradeStats == null || dbtradeStats.Count == 0)
                return;
            var statsIn24H = dbtradeStats.Where(x => x.time <= DateTime.UtcNow.Date.AddHours(DateTime.UtcNow.Hour)).ToList();
            if (statsIn24H == null || statsIn24H.Count == 0)
                return;
            decimal avgTotal = 0;
            foreach (var item in statsIn24H)
            {
                avgTotal += item.sum;
            }
            avgSum24H = avgTotal / statsIn24H.Count;
        }
        decimal avgSum24H = 0;

        async private void CbPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!formLoaded)
                return;
            cbPeriod.Enabled = false;
            Chart chart = null;
            ChartPeriod periodSelected = (ChartPeriod)cbPeriod.SelectedIndex;
            await Task.Run(() =>
                chart = ManualItemSelling.GetChart(item.itemSettings.entity_id, periodSelected)
            );
            SetChartToForm(chart);
            cbPeriod.Enabled = true;
        }

        private void SetChartToForm(Chart chart)
        {
            if (chart == null)
                return;
            string chrtName = "myChart2_";
            chart.Name = chrtName;
            if (tableLayoutPanel1.Controls.ContainsKey(chrtName))
                tableLayoutPanel1.Controls.RemoveByKey(chrtName);
            tableLayoutPanel1.Controls.Add(chart, 0, 0);
        }

        private void NmMaxDropPercentByAVG_ValueChanged(object sender, EventArgs e)
        {
            SetLabelMaxDropByAVG();
        }
    }
}
