using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDmp
{
    public enum BusyState { Free, CheckingSomeUser, CheckingItem, TryBuyByFixPrice, DontKnown }
    public class UnitCheckerAdditional : UnitChecker
    {
        public BusyState busyState { get; private set; } = BusyState.Free;
        public DateTime startUserChecking { get; private set; }
        public Task<UnitCheckerAdditional> CurTask { get; private set; }

        private bool stopCycle = false;
        public UnitCheckerAdditional(bool usingProxy) : base(usingProxy, true)
        {
            CurTask = Task.Run(() => { return this; });
            sessionUpdater = Task.Factory.StartNew(SessionUpdater);
            lastUpdateSessionTime = DateTime.Now;
        }

        public Task<UnitCheckerAdditional> GetThisUnit()
        {
            busyState = BusyState.DontKnown;
            stopCycle = true;
            startUserChecking = DateTime.Now.AddMinutes(1);
            return CurTask;
        }

        public void ForceMainWorkInChecker(ExecutingItem executingItem)
        {
            busyState = BusyState.CheckingItem;
            startUserChecking = DateTime.Now;
            var task = Task.Run(() =>
            {
                do
                {
                    OrdersAnswer ordersAnswer = reqLogic.GetOrders(executingItem.entity_id, 5);
                    if (ordersAnswer == null || ordersAnswer.results == null || ordersAnswer.results.Count < 0)
                        continue;
                    if (ordersAnswer.results.Count == 0)
                        return;
                    else if (ordersAnswer.results.Count == 1)
                    {
                        ordersAnswer.results.Add(ordersAnswer.results.First());
                    }
                    executingItem.lastPrice = ordersAnswer.results.First().buy_price.amount;
                    if (executingItem.buyingItem)
                    {
                        if (BuyingCheckerLogic(executingItem, ordersAnswer, BuyingFrom.FromAddit))
                            return;
                    }
                    executingItem.buyingBusy = false;
                    var sellingEvent = new System.Threading.AutoResetEvent(false);
                    if (PublicRequestManager.SetSellingIfFree(executingItem.entity_id, sellingEvent))
                    {
                        var watch = System.Diagnostics.Stopwatch.StartNew(); // tmp
                        if (Global.settings.AutoSelling && executingItem.sellingItem)
                            SellingCheckerLogic(executingItem, ordersAnswer);
                        watch.Stop();
                        var elapsedMs = watch.ElapsedMilliseconds;
                        sellingEvent.Set();
                        //sellingEvent.Close();
                    }
                    else
                        sellingEvent.Close();
                    break;
                }
                while (!MainWork.PausedOrPausing());
            });
            //executingItem.buyingTask = task;
            CurTask = task.ContinueWith(x => { if (busyState != BusyState.DontKnown) busyState = BusyState.Free; executingItem.buyingBusy = false; return this; });
        }

        public void CheckingUser(string userId)
        {
            busyState = BusyState.CheckingSomeUser;
            startUserChecking = DateTime.Now;
            stopCycle = false;
            var task = Task.Run(() =>
            {
                DateTime endOfChecking = DateTime.Now.AddMinutes(Global.settings.CheckingUserInMinutes);
                while (!stopCycle && !MainWork.PausedOrPausing() && DateTime.Now <= endOfChecking)
                {
                    var userOrders = reqLogic.GetOrdersByUser(userId, 5);
                    if (userOrders.Count == 0)
                        return;
                    foreach (var userOrder in userOrders)
                    {
                        var executingItem = PublicRequestManager.TryGetExecutingItemForce(userOrder.product.id);
                        if (executingItem == null)
                            continue;
                        var neededSettings = executingItem.itemSettings.buyingSettings;
                        if (Global.settings.MaxPrice > 0 && Global.settings.MaxPrice < userOrder.buy_price.amount)
                            continue;

                        var checkForBuy = CheckBuyingConditions(neededSettings, userOrder.buy_price.amount, 0);
                        if (checkForBuy != CheckForBuy.NotOk)
                            MakeBuy(userOrder, BuyingFrom.FromAdditByUserChecking, checkForBuy);
                    }
                    //executingItem.lastPrice = userOrders.results.First().buy_price.amount;
                    //if (executingItem.buyingItem)
                    //{
                    //    if (BuyingCheckerLogic(executingItem, userOrders, BuyingFrom.FromAddit))
                    //        return;
                    //}
                    //executingItem.buyingBusy = false;
                    //if (PublicRequestManager.SetSellingIfFree(executingItem.entity_id, sellingEvent))
                    //{
                    //    var watch = System.Diagnostics.Stopwatch.StartNew(); // tmp
                    //    if (Global.settings.AutoSelling && executingItem.sellingItem)
                    //        SellingCheckerLogic(executingItem, userOrders);
                    //    watch.Stop();
                    //    var elapsedMs = watch.ElapsedMilliseconds;
                    //    sellingEvent.Set();
                    //}
                    //break;
                }
            });
            //executingItem.buyingTask = task;
            CurTask = task.ContinueWith(x => { if (busyState != BusyState.DontKnown) busyState = BusyState.Free; return this; });
        }

        void MakeBuy(OrderInfo order, BuyingFrom buyingFrom, CheckForBuy checkForBuy)
        {
            var buyInfo = MainWork.WorkingReqManager.BuyItem(order, buyingFrom, checkForBuy);
            if (!buyInfo.completed)
                return;
            AvailableItemsManager.AddItem(buyInfo.transaction.product.id, buyInfo.transaction.item.id, buyInfo.transaction.price.amount);
        }

        public void TryBuyByFixPrice(ExecutingItem executingItem)
        {
            busyState = BusyState.TryBuyByFixPrice;
            startUserChecking = DateTime.Now;
            var task = Task.Run(() =>
            {
                do
                {
                    MarketItemInfo marketItemInfo = reqLogic.GetMarketItemInfo(executingItem.entity_id);
                    if (marketItemInfo == null)
                        continue;
                    BuyingBuyFixPriceLogic(executingItem, marketItemInfo);
                    break;
                }
                while (!MainWork.PausedOrPausing());
            });
            //executingItem.buyingTask = task;
            CurTask = task.ContinueWith(x => { if (busyState != BusyState.DontKnown) busyState = BusyState.Free; executingItem.buyingBusy = false; return this; });
        }

        void BuyingBuyFixPriceLogic(ExecutingItem itemSettings, MarketItemInfo marketItemInfo)
        {
            var neededSettings = itemSettings.itemSettings.buyingSettings;
            if (Global.settings.MaxPrice > 0 && Global.settings.MaxPrice < marketItemInfo.price_info.best_price.amount)
                return;

            var checkForBuy = CheckBuyingConditions(neededSettings, marketItemInfo.price_info.best_price.amount, 0);
            if (checkForBuy != CheckForBuy.NotOk)
                MakeBuy(marketItemInfo, itemSettings, checkForBuy);
        }

        void MakeBuy(MarketItemInfo marketItemInfo, ExecutingItem itemSettings, CheckForBuy checkForBuy)
        {
            var buyInfo = MainWork.WorkingReqManager.BuyItem(new OrderInfo()
            {
                buy_price = marketItemInfo.price_info.best_price,
                id = marketItemInfo.best_order_id,
                product = new ItemOrderIfoAnswer()
                {
                    id = itemSettings.entity_id
                }
            }, BuyingFrom.FromAddit, checkForBuy);
            if (!buyInfo.completed)
                return;
            AvailableItemsManager.AddItem(buyInfo.transaction.product.id, buyInfo.transaction.item.id, marketItemInfo.price_info.best_price.amount);
            //itemSettings.forceSell = true;
            //itemSettings.sellingHoldTo = DateTime.Now.AddSeconds(new Random().Next(Global.settings.FromSellingHoldTime, Global.settings.ToSellingHoldTime));
        }

        public override void WaitForAllDone()
        {
            CurTask?.Wait();
            sessionUpdater?.Wait();
        }
    }
}
