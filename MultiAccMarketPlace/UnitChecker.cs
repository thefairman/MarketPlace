using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LDmp
{
    public enum CheckForBuy { FixPriceOk, OtherOk, NotOk }
    public class UnitChecker : UnitBase
    {
        Task mainTask = null;
        public UnitChecker(bool usingProxy, bool fast) : base(null, usingProxy, fast)
        {
        }

        public override void WaitForAllDone()
        {
            mainTask?.Wait();
        }

        public void Start()
        {
            if (mainTask == null)
                mainTask = Task.Factory.StartNew(Cycle);
        }

        void Cycle()
        {
            while (!MainWork.PausedOrPausing())
            {
                if (WorkingRequestsManager.WaitingForSMS)
                {
                    Thread.Sleep(100);
                    continue;
                }
                MainWorkInChecker(PublicRequestManager.GetExecutingItem());
                //Thread.Sleep(1000);
            }
        }

        //readonly AutoResetEvent buyingEvent = new AutoResetEvent(false);
        //protected AutoResetEvent sellingEvent = new AutoResetEvent(false);
        public void MainWorkInChecker(ExecutingItem executingItem)
        {
            do
            {
                OrdersAnswer ordersAnswer = reqLogic.GetOrders(executingItem.entity_id, 5);
                if (ordersAnswer == null || ordersAnswer.results == null || ordersAnswer.results.Count < 0)
                    continue;
                if (ordersAnswer.results.Count == 0)
                {
                    executingItem.buyingBusy = false;
                    return;
                }
                else if (ordersAnswer.results.Count == 1)
                {
                    ordersAnswer.results.Add(ordersAnswer.results.First());
                }
                executingItem.lastPrice = ordersAnswer.results.First().buy_price.amount;
                bool shouldSell = true;
                if (executingItem.buyingItem)
                {
                    shouldSell = !BuyingCheckerLogic(executingItem, ordersAnswer, BuyingFrom.Usual);
                }
                executingItem.buyingBusy = false;
                var sellingEvent = new AutoResetEvent(false);
                if (PublicRequestManager.SetSellingIfFree(executingItem.entity_id, sellingEvent))
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew(); // tmp
                    if (shouldSell && Global.settings.AutoSelling && executingItem.sellingItem)
                        SellingCheckerLogic(executingItem, ordersAnswer);
                    #region UpdatingMarketStats
                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;
                    watch.Restart();
                    var prevLastMS = PublicRequestManager.GetLastMarketStatInfo(executingItem.entity_id);
                    var marketState = reqLogic.GetItemMarketStats(executingItem.entity_id, prevLastMS != null && prevLastMS.time.Date == DateTime.UtcNow.Date ? MarketStatPeriod.day : MarketStatPeriod.month);
                    DateTime atTime = DateTime.UtcNow.AddDays(-1);
                    PublicRequestManager.SetLastMarketStatInfo(executingItem.entity_id, marketState.Where(x => x.time >= atTime).ToList());
                    var curLastMS = PublicRequestManager.GetLastMarketStatInfo(executingItem.entity_id);
                    if (curLastMS != null)
                    {
                        if (prevLastMS == null)
                        {
                            Global.dbLogic.AddItemTradeStats(executingItem.entity_id, marketState);
                        }
                        else
                        {
                            var newMSlist = marketState.Where(x => x.time >= prevLastMS.time).ToList();
                            if (newMSlist.Count > 0)
                            {
                                var firstOfNeMS = newMSlist.First();
                                if (firstOfNeMS.sum == curLastMS.sum && firstOfNeMS.time == curLastMS.time && firstOfNeMS.trades == curLastMS.trades)
                                    newMSlist.RemoveAt(0);
                                Global.dbLogic.AddItemTradeStats(executingItem.entity_id, newMSlist);
                            }
                        }
                    }
                    #endregion
                    watch.Stop();
                    elapsedMs = watch.ElapsedMilliseconds;
                    sellingEvent.Set();
                    //sellingEvent.Close();
                }
                else
                    sellingEvent.Close();
                // добавить marketState
                break;
            }
            while (!MainWork.PausedOrPausing());
        }

        //bool BuyingCheckerLogicWhithOnlyPrice()
        //{

        //}

        protected bool BuyingCheckerLogic(ExecutingItem itemSettings, OrdersAnswer ordersAnswer, BuyingFrom buyingFrom)
        {
            var neededSettings = itemSettings.itemSettings.buyingSettings;
            OrderInfo firstOrder = ordersAnswer.results.First();
            if (Global.settings.MaxPrice > 0 && Global.settings.MaxPrice < firstOrder.buy_price.amount)
                return false;

            var checkForBuy = CheckBuyingConditions(neededSettings, firstOrder.buy_price.amount, ordersAnswer.results[1].buy_price.amount);
            if (checkForBuy != CheckForBuy.NotOk)
                return MakeBuy(ordersAnswer, itemSettings, buyingFrom, checkForBuy);
            //if (neededSettings.fixPrice > 0)
            //{
            //    if (neededSettings.fixPrice >= firstOrder.buy_price.amount)
            //        return MakeBuy(ordersAnswer, itemSettings);
            //}

            //if (neededSettings.percent > 0)
            //{
            //    if (neededSettings.maxPriceForPercent > 0 && neededSettings.maxPriceForPercent < firstOrder.buy_price.amount)
            //        return false;
            //    decimal percentDifrence = 100 - 100 / ordersAnswer.results[1].buy_price.amount * firstOrder.buy_price.amount;
            //    if (neededSettings.percent > percentDifrence)
            //        return false;
            //    return MakeBuy(ordersAnswer, itemSettings);
            //}

            //if (neededSettings.minProfitAmount > 0)
            //{
            //    decimal profitAmount = (ordersAnswer.results[1].buy_price.amount - 0.01m) * 0.85m - firstOrder.buy_price.amount;
            //    if (neededSettings.minProfitAmount > profitAmount)
            //        return false;
            //    return MakeBuy(ordersAnswer, itemSettings);
            //}

            //if (neededSettings.minProfitPercent > 0)
            //{
            //    decimal percentProfit = 100 / firstOrder.buy_price.amount * ((ordersAnswer.results[1].buy_price.amount - 0.01m) * 0.85m) - 100;
            //    if (neededSettings.minProfitPercent > percentProfit)
            //        return false;
            //    return MakeBuy(ordersAnswer, itemSettings);
            //}

            return false;
            // после покупки проверить не мой ли ордер сверху, если да, то продавать не следует. просто добавить купленный предмет в items
        }

        public static CheckForBuy CheckBuyingConditions(ItemBuyingSettings neededSettings, decimal curPrice, decimal prevPrice)
        {
            if (neededSettings.fixPrice > 0)
            {
                if (neededSettings.fixPrice >= curPrice)
                    return CheckForBuy.FixPriceOk;
            }

            if (prevPrice <= 0)
                return CheckForBuy.NotOk;
            if (neededSettings.maxPrice > 0 && neededSettings.maxPrice < curPrice)
                return CheckForBuy.NotOk;
            if (neededSettings.percent > 0)
            {
                decimal percentDifrence = 100 - 100 / prevPrice * curPrice;
                if (neededSettings.percent > percentDifrence)
                    return CheckForBuy.NotOk;
                return CheckForBuy.OtherOk;
            }

            if (neededSettings.minProfitAmount > 0)
            {
                decimal profitAmount = (prevPrice - 0.01m) * 0.85m - curPrice;
                if (neededSettings.minProfitAmount > profitAmount)
                    return CheckForBuy.NotOk;
                return CheckForBuy.OtherOk;
            }

            if (neededSettings.minProfitPercent > 0)
            {
                decimal percentProfit = 100 / curPrice * ((prevPrice - 0.01m) * 0.85m) - 100;
                if (neededSettings.minProfitPercent > percentProfit)
                    return CheckForBuy.NotOk;
                return CheckForBuy.OtherOk;
            }
            return CheckForBuy.NotOk;
        }

        bool MakeBuy(OrdersAnswer ordersAnswer, ExecutingItem itemSettings, BuyingFrom buyingFrom, CheckForBuy checkForBuy)
        {
            var buyInfo = MainWork.WorkingReqManager.BuyItem(ordersAnswer.results.First(), buyingFrom, checkForBuy);
            if (!buyInfo.completed)
                return false;
            AvailableItemsManager.AddItem(buyInfo.transaction.product.id, buyInfo.transaction.item.id, buyInfo.transaction.price.amount);
            OrderInfo secondOrder = ordersAnswer.results[1];
            if (secondOrder.user.id == WorkingRequestsManager.UserID)
                return true;
            if (!Global.settings.AutoSellingWhenBuy)
            {
                ordersAnswer.results.RemoveAt(0);
                return false;
            }
            return true;
        }

        protected void SellingCheckerLogic(ExecutingItem itemSettings, OrdersAnswer ordersAnswer)
        {
            ItemSellingSettings sellingSettings = itemSettings.itemSettings.sellingSettings;
            int entity_id = itemSettings.entity_id;
            if (!AvailableItemsManager.HaveItemCanBeSelled(entity_id) && !AvailableItemsManager.HaveItemOnOrders(entity_id))
                return;
            // ищем наш ордер в уже полученных ордерах
            decimal secondPriceOfSecondUser;
            int myFirstOrderNum;
            int neededOrdersCount = 20;
            do
            {
                string userBeforMe = "";
                secondPriceOfSecondUser = 0;
                myFirstOrderNum = -1;
                for (int i = 0; i < ordersAnswer.results.Count; i++)
                {
                    if (ordersAnswer.results[i].user.id == WorkingRequestsManager.UserID)
                    {
                        if (myFirstOrderNum < 0)
                            myFirstOrderNum = i;
                        if (i == 0) // если наш ордер первый, то можно выходить
                        {
                            if (!AvailableItemsManager.DontUpItem(itemSettings.entity_id, ordersAnswer.results[0].item.id) && ordersAnswer.results[1].buy_price.amount - .05m > ordersAnswer.results[0].buy_price.amount)
                            {
                                if (MainWork.WorkingReqManager.CancelOrder(ordersAnswer.results[0].id))
                                {
                                    AvailableItemsManager.AddItem(ordersAnswer.results[0].product.id, ordersAnswer.results[0].item.id, 0);
                                    ordersAnswer.results.RemoveAt(0);
                                    i--;
                                    myFirstOrderNum = -1;
                                    continue;
                                }
                            }
                            AvailableItemsManager.AddPrevOrdersStats(itemSettings.entity_id, "0", ordersAnswer.results[0].buy_price.amount);
                            return;
                        }
                        //else
                        //{
                        //    CheckAndSell(itemSettings, ordersAnswer.results.First(), ordersAnswer.results[i], true, usersBeforMe.Count);
                        //    return;
                        //}
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(userBeforMe))
                            userBeforMe = ordersAnswer.results[i].user.id;
                        else
                        {
                            if (secondPriceOfSecondUser == 0 && userBeforMe != ordersAnswer.results[i].user.id)
                                secondPriceOfSecondUser = ordersAnswer.results[i].buy_price.amount;
                            if (myFirstOrderNum >= 0)
                                break;
                        }
                    }
                }
                if (myFirstOrderNum >= 0)
                {
                    AvailableItemsManager.AddPrevOrdersStats(itemSettings.entity_id, myFirstOrderNum.ToString(), ordersAnswer.results[0].buy_price.amount);
                    CheckAndSell(itemSettings, ordersAnswer.results.First(), ordersAnswer.results[myFirstOrderNum], myFirstOrderNum, secondPriceOfSecondUser);
                    return;
                }
                else
                {
                    if (ordersAnswer.results.Count < neededOrdersCount && ordersAnswer.results.Count < ordersAnswer.count)
                    {
                        ordersAnswer = reqLogic.GetOrders(itemSettings.entity_id, neededOrdersCount);
                        continue;
                    }
                    AvailableItemsManager.AddPrevOrdersStats(itemSettings.entity_id, $"> {ordersAnswer.results.Count}", ordersAnswer.results[0].buy_price.amount);
                    break;
                }
            } while (true);
            // если не нашли свой одрер запрашиваем ордера по предмету
            var myOrders = AvailableItemsManager.HaveItemOnOrders(entity_id) ? MainWork.WorkingReqManager.GetMyOrdersByItem(entity_id) : null;
            AvailableItemsManager.UpdateItemsOnOrders(entity_id, myOrders);
            if (myOrders == null || myOrders.Count == 0)
            {
                //WorkingRequestsManager.DellItem(entity_id, 0);
                CheckAndSell(itemSettings, ordersAnswer.results.First(), null);
            }
            else
                CheckAndSell(itemSettings, ordersAnswer.results.First(), myOrders.First(), myFirstOrderNum < 0 ? ordersAnswer.results.Count + 1 : myFirstOrderNum, secondPriceOfSecondUser);
        }
        bool maxDropIsOk(ExecutingItem itemSettings, decimal firstOrderPrice, decimal myOrderPrice)
        {
            ItemSellingSettings sellingSettings = itemSettings.itemSettings.sellingSettings;
            if (sellingSettings.maxDropPercent <= 0)
                return true;
            decimal droppedPercent = 100 - 100 / myOrderPrice * firstOrderPrice;
            if (sellingSettings.maxDropPercent < droppedPercent)
                return false;
            return true;
        }
        void CheckAndSell(ExecutingItem itemSettings, OrderInfo firstOrder, OrderInfo myOrder, int myOrderCount = 0, decimal secondUserPrice = 0)
        {
            ItemSellingSettings sellingSettings = itemSettings.itemSettings.sellingSettings;
            int entity_id = itemSettings.entity_id;
            if (sellingSettings.minPrice > 0 && sellingSettings.minPrice > firstOrder.buy_price.amount - .01m)
                return;

            var msi = PublicRequestManager.GetLastMarketStatInfo(itemSettings.entity_id);
            int maxOrdersBefore = msi == null ? Global.settings.MaxOrdersBeforeMe : msi.trades; // получаем максимально кол-во ордеров перед мной (исходя из настрое или кол-ва торгов за час)
            if (sellingSettings.maxDropAmount > 0 && myOrder != null && sellingSettings.maxDropAmount < myOrder.amount - firstOrder.amount)
                return;

            if (sellingSettings.maxDropPercent > 0 && myOrder != null && myOrderCount < maxOrdersBefore)
            {
                if (!maxDropIsOk(itemSettings, firstOrder.buy_price.amount, myOrder.buy_price.amount))
                    return;
                //decimal droppedPercent = 100 - 100 / myOrder.buy_price.amount * firstOrder.buy_price.amount;
                //if (sellingSettings.maxDropPercent < droppedPercent)
                //    return;
            }

            if (sellingSettings.maxDropPercentByAvg > 0)
            {
                var mstats = PublicRequestManager.GetLastMarketStatsInfo(itemSettings.entity_id);
                if (mstats?.Count > 1)
                {
                    decimal totalAVG = 0;
                    foreach (var item in mstats)
                    {
                        totalAVG += item.sum.amount;
                    }
                    totalAVG /= mstats.Count;
                    totalAVG *= Global.settings.EuroRate;
                    if (totalAVG / 100 * (100 - sellingSettings.maxDropPercentByAvg) > firstOrder.buy_price.amount - 0.01m)
                        return;
                }
            }

            if (myOrder != null) // если у нас уже есть ордер с таким предметом, то нужно его отменить
            {
                if (!AvailableItemsManager.DontCheckSecondPrice(entity_id, myOrder.item.id) && secondUserPrice >= myOrder.buy_price.amount)
                {
                    if (!maxDropIsOk(itemSettings, firstOrder.buy_price.amount, secondUserPrice))
                        return;
                }
                if (MainWork.WorkingReqManager.CancelOrder(myOrder.id))
                    AvailableItemsManager.AddItem(itemSettings.entity_id, myOrder.item.id, 0);
                else
                    return;
                //    return;
                //if (MainWork.WorkingReqManager.CancelOrder(myOrder.id))
                //    SellItem(itemSettings, myOrder.item.id, firstOrder.buy_price.amount - 0.01m);
                //return;
            }
            decimal mySellPrice = firstOrder.buy_price.amount - 0.01m;
            int itemId = AvailableItemsManager.PopItemFromInventory(entity_id, mySellPrice);
            if (itemId > 0)
            {
                var info = SellItem(itemSettings, itemId, mySellPrice);
                if (!String.IsNullOrEmpty(info?.id))
                    AvailableItemsManager.SetOrderToItem(itemSettings.entity_id, itemId, info.id, mySellPrice);
                else
                    AvailableItemsManager.SetOrderToItem(itemSettings.entity_id, itemId, "", 0);
            }
        }

        public static OrderInfo SellItem(ExecutingItem itemSettings, int itemId, decimal amount)
        {
            if (itemSettings != null)
                itemSettings.lastPrice = amount;
            return MainWork.WorkingReqManager.SellItem(itemId, amount);
        }

        public void GetItemsList()
        {
            var mpAnswer = reqLogic.GetIAlltems();
            Global.dbLogic.AddItemToQueue(new KeyValuePair<DateTime, List<ItemMPAnswer>>(mpAnswer.Key.StartAndHalfOfRequestTime, mpAnswer.Value));
            Global.dbLogic.SetMinusLastUpdateDBItemsTime();
        }

        public WebSocketData GetWebSocketData()
        {
            return reqLogic.GetWebSocketData();
        }
    }
}
