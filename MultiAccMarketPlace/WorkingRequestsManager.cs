using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDmp
{
    public class WorkingRequestsManager : RequestManagerBase<UnitWork>
    {
        public static string UserID { get; set; }
        public static bool WaitingForSMS { get; set; } = false;
        static Dictionary<int, HashSet<int>> accInventoryItems = new Dictionary<int, HashSet<int>>();
        static Dictionary<int, bool> accOrdersItems = new Dictionary<int, bool>();
        
        public WorkingRequestsManager(AccountManager account) : base(account)
        {
            var acc = account.GetAccounts();
            if (acc != null)
            {
                units.Add(new UnitWork(acc, Global.settings.UsingProxyToUser, true, false));
                units.Add(new UnitWork(acc, Global.settings.UsingProxyToUser, false, true));
            }
        }

        public BuyItemAnswer BuyItem(OrderInfo order, BuyingFrom buyingFrom, CheckForBuy checkForBuy)
        {
            return units[0].BuyItem(order, buyingFrom, checkForBuy);
        }

        public List<OrderInfo> GetMyOrdersByItem(int itemID)
        {
            return units[1].GetMyOrdersByItem(itemID);
        }

        public OrderInfo GetOrderInfo(string orderId)
        {
            return units[1].GetOrderInfo(orderId);
        }

        public OrderInfo SellItem(int itemID, decimal price)
        {
            return units[0].SellItem(itemID, price);
        }

        public bool CancelOrder(string orderID)
        {
            return units[0].CancelOrder(orderID);
        }
    }
}
