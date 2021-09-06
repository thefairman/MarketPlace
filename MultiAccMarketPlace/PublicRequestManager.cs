using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LDmp
{
    public class ExecutingItem
    {
        public bool buyingItem = false;
        public bool onlyGlobal = false;
        public bool sellingItem = false;
        public int entity_id;
        public ItemSettings itemSettings = null;
        public bool buyingBusy = false;
        public Task sellingTask = null;
        public decimal lastPrice;
        public List<MarketStatInfo> marketStatInfo = null;
    }
    public class PublicRequestManager : RequestManagerBase<UnitChecker>
    {
        private static List<ExecutingItem> executingItems = new List<ExecutingItem>();
        public ManualResetEvent mre = new ManualResetEvent(false);
        bool onlyAddit;
        WebSocketLogic wsl = null;
        int additThreadsNum = 0;
        public PublicRequestManager(int threadsCount, int additThreadsNum, bool onlyAddit) : base(null)
        {
            this.onlyAddit = onlyAddit;
            this.additThreadsNum = additThreadsNum;
            if (!onlyAddit)
            {
                for (int i = 0; i < threadsCount; i++)
                {
                        units.Add(new UnitChecker(Global.settings.UsingProxy, false));
                }
            }
            if (additThreadsNum > 0)
                AdditionalUnitsManager.SetThreadsNum(additThreadsNum);
            
            FillExecutingItems();
            MainWork.Stopped += MainWork_Stopped;
        }

        private void MainWork_Stopped()
        {
            mre.Set();
        }

        void FillExecutingItems()
        {
            var itemsFromSettings = Global.itemsManager.GetActualItems();
            var allMarketStats = Global.dbLogic.GetLastRecordsItemsTradeStatsFromDB();
            foreach (var item in itemsFromSettings)
            {
                var curMarketStat = allMarketStats.Find(x => x.entity_id == item.entity_id);
                executingItems.Add(new ExecutingItem()
                {
                    buyingItem = item.buyingSettings.IsBuyedItem(),
                    entity_id = item.entity_id,
                    itemSettings = item,
                    onlyGlobal = item.buyingSettings.onlyGlobal,
                    sellingItem = item.sellingSettings.IsSellnigItem(),
                    marketStatInfo = curMarketStat == null ? null : new List<MarketStatInfo>() { new MarketStatInfo()
                    {
                        sum = new CurrencyInfo(){ amount = curMarketStat.sum },
                        time =  curMarketStat.time,
                        trades = curMarketStat.sale_count
                    } }
                });
            }
        }

        static int iteratorExecutingItems = 0;
        private static readonly object execLocker = new object();
        public static ExecutingItem GetExecutingItem()
        {
            lock (execLocker)
            {
                for (; true ; iteratorExecutingItems++)
                {
                    if (iteratorExecutingItems >= executingItems.Count)
                    {
                        Thread.Sleep(100);
                        iteratorExecutingItems = 0;
                    }
                    var execItems = executingItems[iteratorExecutingItems];
                    var lastMS = GetLastMarketStatInfo(executingItems[iteratorExecutingItems].entity_id);
                    if (lastMS != null && lastMS.time >= DateTime.UtcNow.Date.AddHours(DateTime.UtcNow.Hour) && ( execItems.onlyGlobal || !execItems.buyingItem))
                    {
                        if (!execItems.sellingItem || (!AvailableItemsManager.HaveItemCanBeSelled(execItems.entity_id) && !AvailableItemsManager.HaveItemOnOrders(execItems.entity_id)))
                            continue;
                    }
                    if (execItems.buyingBusy)
                        continue;
                    iteratorExecutingItems++;
                    execItems.buyingBusy = true;
                    return execItems;
                    //return Task.Run(() => unitChecker.MainWorkInChecker(execItems));
                    //if (executingItems[iteratorExecutingItems].task == null || executingItems[iteratorExecutingItems].task.IsCompleted)
                    //{
                    //    var execItems = executingItems[iteratorExecutingItems++];
                    //    return execItems.task = Task.Factory.StartNew(() => unitChecker.MainWorkInChecker(execItems));
                    //}
                }
            }
        }

        private static readonly object sellTaskLocker = new object();
        static public bool SetSellingIfFree(int entity_id, AutoResetEvent are)
        {
            lock (sellTaskLocker)
            {
                var execI = executingItems.Find(x => x.entity_id == entity_id);
                if (execI == null)
                    return false;
                if (execI.sellingTask != null && !execI.sellingTask.IsCompleted)
                    return false;
                execI.sellingTask = Task.Run(() => 
                {
                    //var watch = System.Diagnostics.Stopwatch.StartNew(); // tmp
                    are.WaitOne();
                    are.Close();
                    //watch.Stop();
                    //var elapsedMs = watch.ElapsedMilliseconds;
                });
                return true;
            }
        }
        static public ExecutingItem SetSellingWhenFree(int entity_id, AutoResetEvent are)
        {
            ExecutingItem execI;
            Task curTaskExecution;
            lock (sellTaskLocker)
            {
                execI = executingItems.Find(x => x.entity_id == entity_id);
                curTaskExecution = execI.sellingTask;
                var newTask = Task.Run(() => { are.WaitOne(); are.Close(); });
                execI.sellingTask = curTaskExecution == null ? newTask : curTaskExecution.ContinueWith(x => { newTask.Wait(); });
                //execI.sellingTask = Task.Run(() => { are.WaitOne(); });
            }
            curTaskExecution?.Wait();
            return execI;
        }

        readonly static object marketStatLocker = new object();
        public static MarketStatInfo GetLastMarketStatInfo(int entity_id)
        {
            lock (marketStatLocker)
            {
                for (int i = 0; i < executingItems.Count; i++)
                {
                    if (executingItems[i].entity_id == entity_id)
                    {
                        if (executingItems[i].marketStatInfo?.Count > 0)
                            return executingItems[i].marketStatInfo.Last();
                        else
                            return null;
                    }
                }
                return null;
            }
        }

        public static List<MarketStatInfo> GetLastMarketStatsInfo(int entity_id)
        {
            lock (marketStatLocker)
            {
                for (int i = 0; i < executingItems.Count; i++)
                {
                    if (executingItems[i].entity_id == entity_id)
                    {
                        if (executingItems[i].marketStatInfo?.Count > 0)
                            return new List<MarketStatInfo>(executingItems[i].marketStatInfo);
                        else
                            return null;
                    }
                }
                return null;
            }
        }

        public static void SetLastMarketStatInfo(int entity_id, List<MarketStatInfo> marketStatInfo)
        {
            if (marketStatInfo == null || marketStatInfo.Count == 0)
                return;
            lock (marketStatLocker)
            {
                for (int i = 0; i < executingItems.Count; i++)
                {
                    if (executingItems[i].entity_id == entity_id)
                    {
                        executingItems[i].marketStatInfo = marketStatInfo;
                        break;
                    }
                }
            }
        }
        public static ExecutingItem TryGetExecutingItemForce(int id)
        {
            return executingItems.Find(x => x.entity_id == id);
        }

        public void Start()
        {
            if (!onlyAddit)
            {
                foreach (var item in units)
                {
                    item.Start();
                }
            }
            if (additThreadsNum > 0)
            {
                wsl = new WebSocketLogic();
                wsl.PriceChanged += Wsl_PriceChanged;
                wsl.Start();
            }
        }

        private void Wsl_PriceChanged(int product, decimal price)
        {
            var executingItem = TryGetExecutingItemForce(product);
            if (executingItem == null)
                return;
            if (price != executingItem.lastPrice && (AvailableItemsManager.HaveItemOnOrders(product) || AvailableItemsManager.HaveItemCanBeSelled(product)))
            {
                while (WorkingRequestsManager.WaitingForSMS)
                {
                    Thread.Sleep(100);
                    continue;
                }
                var au = AdditionalUnitsManager.GetAdditionalUnit();
                au.ForceMainWorkInChecker(executingItem);
            }
            else if (!WorkingRequestsManager.WaitingForSMS)
            {
                var buyCheckRes = UnitChecker.CheckBuyingConditions(executingItem.itemSettings.buyingSettings, price, executingItem.lastPrice);
                if (buyCheckRes != CheckForBuy.NotOk)
                {
                    var au = AdditionalUnitsManager.GetAdditionalUnit();
                    switch (buyCheckRes)
                    {
                        case CheckForBuy.FixPriceOk:
                            au.TryBuyByFixPrice(executingItem);
                            break;
                        case CheckForBuy.OtherOk:
                            au.ForceMainWorkInChecker(executingItem);
                            break;
                    }
                }
            }
            executingItem.lastPrice = price;
        }

        public override void WaitForAllDone()
        {
            base.WaitForAllDone();
            mre.WaitOne();
            wsl?.Close();
            wsl?.mre.WaitOne();
            AdditionalUnitsManager.WaitForAllDone();
        }

        public override bool InitsUnits()
        {
            bool initiatedUnits = true;
            if (!onlyAddit)
                initiatedUnits = base.InitsUnits();
            if (!initiatedUnits)
                return false;
            if (additThreadsNum > 0)
                return AdditionalUnitsManager.InitsUnits();
            return true;
        }

        public override void Dispose()
        {
            base.Dispose();
            AdditionalUnitsManager.ReleaseItems();
        }
    }
}