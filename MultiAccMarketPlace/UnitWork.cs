using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LDmp
{
    public enum BuyingFrom { Usual, FromAddit, FromAdditByUserChecking }
    public class UnitWork : UnitBase
    {
        readonly double updateInventoryIntervalinMinutes = 3;

        bool initiatedCopy;
        Task inventoryUpdater;

        public UnitWork(AccountData accData, bool usingProxy, bool fast, bool initiatedCopy) : base(accData, usingProxy, fast)
        {
            this.initiatedCopy = initiatedCopy;
            if (initiatedCopy)
            {
                inventoryUpdater = Task.Factory.StartNew(InventoryUpdater);
                lastUpdateTokenTime = DateTime.Now;
            }
            else
            {
                sessionUpdater = Task.Factory.StartNew(SessionUpdater);
                lastUpdateSessionTime = DateTime.Now;
            }
        }

        DateTime lastUpdateTokenTime;
        void InventoryUpdater()
        {
            while (!MainWork.PausedOrPausing())
            {
                DateTime now = DateTime.Now;
                DateTime timeWhenShouldUpdate = lastUpdateTokenTime.AddMinutes(updateInventoryIntervalinMinutes);
                timeWhenShouldUpdate = new DateTime(timeWhenShouldUpdate.Year, timeWhenShouldUpdate.Month, timeWhenShouldUpdate.Day, timeWhenShouldUpdate.Hour, timeWhenShouldUpdate.Minute, secondsWhenUpdate);
                int toUpdateTime = (int)(timeWhenShouldUpdate - now).TotalMilliseconds;
                    // updateTokenIntervalinMinutes * 60 * 1000 - (now - lastUpdateTokenTime).TotalMilliseconds;
                if (toUpdateTime > 0) // awaiting block
                {
                    if (toUpdateTime > 100)
                        toUpdateTime = 100;
                    Thread.Sleep(toUpdateTime);
                    continue;
                }
                if (reqLogic.AccIsSet)
                    FillInventoryItems();
                //reqLogic.RegionIsAllowedAndSetCSRF();
                lastUpdateTokenTime = DateTime.Now;
            }
        }

        

        public override void WaitForAllDone()
        {
            inventoryUpdater?.Wait();
            sessionUpdater?.Wait();
        }

        public BuyItemAnswer BuyItem(OrderInfo order, BuyingFrom buyingFrom, CheckForBuy checkForBuy)
        {
            var buyAnswer = reqLogic.BuyItem(order.product.id, order.buy_price.amount, order.id);
            string productName = order.product.name;
            string addTitle = "";
            if (buyingFrom != BuyingFrom.Usual)
                addTitle += $" [{buyingFrom}]";
            addTitle += $" [{checkForBuy}]";
            if (String.IsNullOrEmpty(productName))
                productName = Global.dbLogic.GetDBItems().Find(x=>x.entity_id == order.product.id)?.title;
            if (buyAnswer.completed)
            {
                Global.UIdata.LogOfActionsUI.AddRecordAsync(new UILogOfAction(reqLogic.Login, order.product.id, productName, "buyed" + addTitle, order.buy_price.amount, System.Drawing.Color.LightGreen));
                MainWork.sp.Play();
            }
            else
            {
                Global.UIdata.LogOfActionsUI.AddRecordAsync(new UILogOfAction(reqLogic.Login, order.product.id, productName, buyAnswer.error + addTitle, order.buy_price.amount, System.Drawing.Color.LightPink));
                if (checkForBuy == CheckForBuy.FixPriceOk && buyingFrom != BuyingFrom.FromAdditByUserChecking)
                {
                    if (order.user == null || String.IsNullOrEmpty(order.user.id))
                    {
                        var orderInfo = MainWork.WorkingReqManager.GetOrderInfo(order.id);
                        order.user = orderInfo.user;
                    }
                    if (order.user != null && !String.IsNullOrEmpty(order.user.id))
                        Task.Run(() => AdditionalUnitsManager.GetAdditionalUnit().CheckingUser(order.user.id));
                }
            }
            return buyAnswer;
        }

        public bool CancelOrder(string orderID)
        {
            return reqLogic.CancelOrder(orderID);
        }

        public OrderInfo SellItem(int itemID, decimal price)
        {
            if (price < 3)
                return null;
            return reqLogic.SellItem(itemID, price);
        }

        public List<OrderInfo> GetMyOrdersByItem(int itemdID)
        {
            return reqLogic.GetMyOrdersByItem(itemdID);
        }

        public OrderInfo GetOrderInfo(string orderId)
        {
            return reqLogic.GetOrderInfo(orderId);
        }

        void FillInventoryItems()
        {
            var allMyItems = reqLogic.GetAllInventory();
            if (allMyItems != null)
            {
                AvailableItemsManager.AddItems(allMyItems);
            }
        }

        public override bool Init(AccountManager accounts)
        {
            var initiated = base.Init(accounts);
            if (!initiated)
                return false;
            // получить все мои предметы из инвентаря и ордеров
            if (!initiatedCopy)
                FillInventoryItems();
            var allMyOrders = reqLogic.GetAllmyOrders();
            if (allMyOrders != null)
            {
                AvailableItemsManager.AddItems(allMyOrders);
                //foreach (var item in allMyOrders)
                //{
                //    AvailableItemsManager.AddItems(item.product.id, 0);
                //}
            }
            if (!initiatedCopy)
                reqLogic.EnsurePremisssion();
            return true;
        }

        // сделать поток на апдейт токена каждые 2,5 минуты, реализовать ожидание завершения этого потока в WaitForAllDone
    }
}
