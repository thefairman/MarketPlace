using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDmp
{
    public class AII
    {
        public string position;
        public decimal minPrice;
        public DateTime lastUpdateOrdersInfo;
    }
    public class AdditSettingsAItem
    {
        public bool? sellLessThenBought = null;
        public bool? dontUpItem = null;
        public bool? dontCheckSecondPrice = null;
    }
    public class AvailableItemInfo
    {
        public int entity_id;
        public int item_id;
        public string order_id = "";
        public decimal boughtPrice;
        public DateTime boughtTime;
        public DateTime canSelledTime;
        public bool sellLessThenBought = false;
        public bool exist = true;
        public decimal myPrice;
        public bool dontUpItem;
        public bool dontCheckSecondPrice;
    }
    public class AvailableItemsManager
    {
        static Dictionary<int, Dictionary<int, AvailableItemInfo>> availableItems = new Dictionary<int, Dictionary<int, AvailableItemInfo>>();
        static Dictionary<int, AII> minPrevOrdersStats = new Dictionary<int, AII>();
        private static readonly object accItemsLocker = new object();
        public static bool HaveItem(int entity_id)
        {
            lock (accItemsLocker)
            {
                if (availableItems.ContainsKey(entity_id))
                {
                    return availableItems[entity_id] != null && availableItems[entity_id].Values.Where(x => x.exist)?.Count() > 0;
                }
                return false;
            }
        }
        public static bool HaveItemCanBeSelled(int entity_id)
        {
            lock (accItemsLocker)
            {
                if (availableItems.ContainsKey(entity_id))
                {
                    return availableItems[entity_id] != null && availableItems[entity_id].Values.Where(x => x.exist && x.canSelledTime <= DateTime.Now)?.Count() > 0;
                }
                return false;
            }
        }
        public static bool HaveItemOnOrders(int entity_id)
        {
            lock (accItemsLocker)
            {
                if (availableItems.ContainsKey(entity_id))
                {
                    return availableItems[entity_id] != null && availableItems[entity_id].Values.Where(x => x.exist && !String.IsNullOrEmpty(x.order_id))?.Count() > 0;

                    //if (availableItems[entity_id] == null)
                    //    return false;

                    //foreach (var item in availableItems[entity_id])
                    //{
                    //    if (!String.IsNullOrEmpty(item.Value.order_id))
                    //        return true;
                    //}
                }
                return false;
            }
        }
        /// <summary>
        /// Добавить/Восстановить предмет в список доступных предметов, а так же записать в дб (использовать только при покупке предмета или отмены ордера)
        /// </summary>
        /// <param name="entity_id">ид предмета на ТП</param>
        /// <param name="id">ИД конкретного предмета</param>
        /// <param name="boughtPrice">цена по кторой купили</param>
        public static void AddItem(int entity_id, int id, decimal boughtPrice)
        {
            lock (accItemsLocker)
            {
                AddItemBack(entity_id, id, boughtPrice);
            }
        }
        static void AddItemBack(int entity_id, int id, decimal boughtPrice)
        {
            if (availableItems.ContainsKey(entity_id) && availableItems[entity_id] != null)
            {
                if (availableItems[entity_id].ContainsKey(id))
                {
                    availableItems[entity_id][id].exist = true;
                    availableItems[entity_id][id].order_id = "";
                    return;
                }
            }
            else
                availableItems[entity_id] = new Dictionary<int, AvailableItemInfo>();
            var aii = new AvailableItemInfo()
            {
                boughtPrice = boughtPrice,
                boughtTime = boughtPrice > 0 ? DateTime.Now : DateTime.MinValue,
                canSelledTime = boughtPrice > 0 ? DateTime.Now.AddSeconds(new Random().Next(Global.settings.FromSellingHoldTime, Global.settings.ToSellingHoldTime)) : DateTime.MinValue,
                entity_id = entity_id,
                item_id = id
            };
            availableItems[entity_id].Add(id, aii);
            Global.dbLogic.AddAvailableItemsToDB(WorkingRequestsManager.UserID, new List<AvailableItemInfo>() { aii });
        }
        public static void AddItems(List<ItemsOrderOrInvetnroyInfo> allMyItems)
        {
            lock (accItemsLocker)
            {
                if (allMyItems == null || allMyItems.Count == 0)
                {
                    if (availableItems != null && availableItems.Count > 0)
                    {
                        foreach (var item in availableItems)
                        {
                            foreach (var itm in item.Value)
                            {
                                if (String.IsNullOrEmpty(itm.Value.order_id))
                                    itm.Value.exist = false;
                            }
                        }
                    }
                    return;
                }

                List<ItemsOrderOrInvetnroyInfo> listForSearchInDB = new List<ItemsOrderOrInvetnroyInfo>();
                int min = 0, max = 0;
                List<DBAvailableItem> dbArray = null;
                if (availableItems != null && availableItems.Count > 0)
                {
                    HashSet<int> currentItemsOnIventory = new HashSet<int>();
                    foreach (var item in allMyItems)
                    {
                        currentItemsOnIventory.Add(item.id);
                        if (!availableItems.ContainsKey(item.product.id) || !availableItems[item.product.id].ContainsKey(item.id))
                        {
                            listForSearchInDB.Add(item);
                            if (min == 0 || item.id < min)
                                min = item.id;
                            if (max == 0 || item.id > max)
                                max = item.id;
                        }
                    }
                    foreach (var item in availableItems)
                    {
                        foreach (var itm in item.Value)
                        {
                            if (String.IsNullOrEmpty(itm.Value.order_id) && !currentItemsOnIventory.Contains(itm.Value.item_id))
                                itm.Value.exist = false;
                        }
                    }
                }
                else
                {
                    min = allMyItems.Min(x => x.id);
                    max = allMyItems.Max(x => x.id);
                }
                if (availableItems.Count == 0 || listForSearchInDB.Count > 0)
                    dbArray = Global.dbLogic.GetAvailableItemsFromDB(WorkingRequestsManager.UserID, min, max);

                List<AvailableItemInfo> addToDbList = new List<AvailableItemInfo>();
                foreach (var item in allMyItems)
                {
                    if (availableItems.ContainsKey(item.product.id) && availableItems[item.product.id] != null)
                    {
                        if (availableItems[item.product.id].ContainsKey(item.id))
                        {
                            availableItems[item.product.id][item.id].exist = true;
                            availableItems[item.product.id][item.id].order_id = "";
                            continue;
                        }
                    }
                    else
                        availableItems[item.product.id] = new Dictionary<int, AvailableItemInfo>();

                    var curItemInDb = dbArray?.Find(x => x.item_id == item.id);
                    //availableItems[item.product.id].Add(item.id, new AvailableItemInfo()
                    //{
                    //    boughtPrice = boughtPrice,
                    //    boughtTime = boughtPrice > 0 ? DateTime.Now : DateTime.MinValue,
                    //    entity_id = item.product.id,
                    //    item_id = item.id
                    //});
                    var aii = new AvailableItemInfo()
                    {
                        boughtTime = DateTime.MinValue,
                        canSelledTime = DateTime.MinValue,
                        entity_id = item.product.id,
                        item_id = item.id
                    };
                    
                    if (curItemInDb == null)
                        addToDbList.Add(aii);
                    else
                    {
                        aii.boughtPrice = curItemInDb.bought_price;
                        aii.boughtTime = curItemInDb.bought_time;
                        aii.dontUpItem = curItemInDb.dontUpItem;
                        aii.sellLessThenBought = curItemInDb.sellLessThenBought;
                        aii.dontCheckSecondPrice = curItemInDb.dontCheckSecondPrice;
                        if (aii.boughtTime > DateTime.MinValue)
                            aii.boughtTime = aii.boughtTime.AddSeconds(new Random().Next(Global.settings.FromSellingHoldTime, Global.settings.ToSellingHoldTime));
                    }
                    availableItems[item.product.id].Add(item.id, aii);
                }
                Global.dbLogic.AddAvailableItemsToDB(WorkingRequestsManager.UserID, addToDbList);
            }
        }

        internal static void AddItems(List<OrderInfo> allMyOrders)
        {
            if (allMyOrders == null || allMyOrders.Count == 0)
                return;
            lock (accItemsLocker)
            {
                List<OrderInfo> listForSearchInDB = new List<OrderInfo>();
                int min = 0, max = 0;
                List<DBAvailableItem> dbArray = null;
                if (availableItems != null && availableItems.Count > 0)
                {
                    foreach (var item in allMyOrders)
                    {
                        if (!availableItems.ContainsKey(item.product.id) || !availableItems[item.product.id].ContainsKey(item.item.id))
                        {
                            listForSearchInDB.Add(item);
                            if (min == 0 || item.item.id < min)
                                min = item.item.id;
                            if (max == 0 || item.item.id > max)
                                max = item.item.id;
                        }
                    }
                }
                else
                {
                    min = allMyOrders.Min(x => x.item.id);
                    max = allMyOrders.Max(x => x.item.id);
                }
                if (availableItems.Count == 0 || listForSearchInDB.Count > 0)
                    dbArray = Global.dbLogic.GetAvailableItemsFromDB(WorkingRequestsManager.UserID, min, max);

                List<AvailableItemInfo> addToDbList = new List<AvailableItemInfo>();
                foreach (var item in allMyOrders)
                {
                    if (availableItems.ContainsKey(item.product.id) && availableItems[item.product.id] != null)
                    {
                        if (availableItems[item.product.id].ContainsKey(item.item.id))
                        {
                            availableItems[item.product.id][item.item.id].exist = true;
                            availableItems[item.product.id][item.item.id].order_id = item.id;
                            availableItems[item.product.id][item.item.id].myPrice = item.buy_price.amount;
                            continue;
                        }
                    }
                    else
                        availableItems[item.product.id] = new Dictionary<int, AvailableItemInfo>();
                    var curItemInDb = dbArray.Find(x => x.item_id == item.item.id);
                    var aii = new AvailableItemInfo()
                    {
                        boughtTime = DateTime.MinValue,
                        canSelledTime = DateTime.MinValue,
                        entity_id = item.product.id,
                        item_id = item.item.id
                    };

                    if (curItemInDb == null)
                        addToDbList.Add(aii);
                    else
                    {
                        aii.boughtPrice = curItemInDb.bought_price;
                        aii.boughtTime = curItemInDb.bought_time;
                        aii.dontUpItem = curItemInDb.dontUpItem;
                        aii.dontCheckSecondPrice = curItemInDb.dontCheckSecondPrice;
                        aii.sellLessThenBought = curItemInDb.sellLessThenBought;
                        if (aii.boughtTime > DateTime.MinValue)
                            aii.boughtTime = aii.boughtTime.AddSeconds(new Random().Next(Global.settings.FromSellingHoldTime, Global.settings.ToSellingHoldTime));
                    }
                    availableItems[item.product.id].Add(item.item.id, aii);
                }
                Global.dbLogic.AddAvailableItemsToDB(WorkingRequestsManager.UserID, addToDbList);
            }
        }

        internal static bool DontUpItem(int entity_id, int id)
        {
            lock (accItemsLocker)
            {
                if (availableItems.ContainsKey(entity_id) || availableItems[entity_id] == null || availableItems[entity_id].ContainsKey(id))
                {
                    return availableItems[entity_id][id].dontUpItem;

                    //if (availableItems[entity_id] == null)
                    //    return false;

                    //foreach (var item in availableItems[entity_id])
                    //{
                    //    if (!String.IsNullOrEmpty(item.Value.order_id))
                    //        return true;
                    //}
                }
                return false;
            }
        }

        public static void SetAdditSettingsAItem(int entity_id, int id, AdditSettingsAItem additSettings = null)
        {
            if (additSettings == null)
                return;
            lock (accItemsLocker)
            {
                if (availableItems.ContainsKey(entity_id))
                {
                    if (availableItems[entity_id] != null && availableItems[entity_id].ContainsKey(id))
                    {
                        if (additSettings.sellLessThenBought != null)
                            availableItems[entity_id][id].sellLessThenBought = (bool)additSettings.sellLessThenBought;
                        if (additSettings.dontUpItem != null)
                            availableItems[entity_id][id].dontUpItem = (bool)additSettings.dontUpItem;
                        if (additSettings.dontCheckSecondPrice != null)
                            availableItems[entity_id][id].dontCheckSecondPrice = (bool)additSettings.dontCheckSecondPrice;
                        Global.dbLogic.EditAvailableItemsToDB(WorkingRequestsManager.UserID, availableItems[entity_id][id]);
                    }
                }
            }
        }

        public static void SetOrderToItem(int entity_id, int id, string orderId, decimal price, AdditSettingsAItem additSettings = null)
        {
            lock (accItemsLocker)
            {
                if (availableItems.ContainsKey(entity_id))
                {
                    if (availableItems[entity_id] != null && availableItems[entity_id].ContainsKey(id))
                    {
                        availableItems[entity_id][id].order_id = orderId;
                        availableItems[entity_id][id].myPrice = price;
                        if (additSettings != null)
                        {
                            if (additSettings.sellLessThenBought != null)
                                availableItems[entity_id][id].sellLessThenBought = (bool)additSettings.sellLessThenBought;
                            if (additSettings.dontUpItem != null)
                                availableItems[entity_id][id].dontUpItem = (bool)additSettings.dontUpItem;
                            if (additSettings.dontCheckSecondPrice != null)
                                availableItems[entity_id][id].dontCheckSecondPrice = (bool)additSettings.dontCheckSecondPrice;
                            Global.dbLogic.EditAvailableItemsToDB(WorkingRequestsManager.UserID, availableItems[entity_id][id]);
                        }   
                    }
                }
            }
        }

        public static void UpdateItemsOnOrders(int entity_id, List<OrderInfo> orders)
        {
            lock (accItemsLocker)
            {
                if (availableItems[entity_id] == null || availableItems[entity_id].Count == 0)
                {
                    if (orders != null)
                    {
                        foreach (var item in orders)
                        {
                            AddItemBack(entity_id, item.item.id, 0);
                        }
                    }
                    return;
                }
                if (orders == null || orders.Count == 0)
                {
                    var itemsOnOrders = availableItems[entity_id].Values.Where(x => x.exist && !String.IsNullOrEmpty(x.order_id));
                    foreach (var item in itemsOnOrders)
                    {
                        item.exist = false;
                    }
                    return;
                }

                HashSet<int> editedIds = new HashSet<int>();
                foreach (var item in orders)
                {
                    editedIds.Add(item.item.id);
                    if (availableItems[entity_id].ContainsKey(item.item.id))
                    {
                        availableItems[entity_id][item.item.id].exist = true;
                        availableItems[entity_id][item.item.id].order_id = item.id;
                    }
                    else
                    {
                        AddItemBack(entity_id, item.item.id, 0);
                    }
                }

                foreach (var item in availableItems[entity_id])
                {
                    if (editedIds.Contains(item.Value.item_id) || String.IsNullOrEmpty(item.Value.order_id))
                        continue;
                    item.Value.exist = false;
                }
            }
        }

        public static void UpdateItemsOnInventory(int entity_id, List<OrderInfo> orders)
        {
            lock (accItemsLocker)
            {
                if (availableItems[entity_id] == null || availableItems[entity_id].Count == 0)
                {
                    if (orders != null)
                    {
                        foreach (var item in orders)
                        {
                            AddItemBack(entity_id, item.item.id, 0);
                        }
                    }
                    return;
                }
                if (orders == null || orders.Count == 0)
                {
                    var itemsOnOrders = availableItems[entity_id].Values.Where(x => x.exist && !String.IsNullOrEmpty(x.order_id));
                    foreach (var item in itemsOnOrders)
                    {
                        item.exist = false;
                    }
                    return;
                }

                HashSet<int> editedIds = new HashSet<int>();
                foreach (var item in orders)
                {
                    editedIds.Add(item.item.id);
                    if (availableItems[entity_id].ContainsKey(item.item.id))
                    {
                        availableItems[entity_id][item.item.id].exist = true;
                        availableItems[entity_id][item.item.id].order_id = item.id;
                    }
                    else
                    {
                        AddItemBack(entity_id, item.item.id, 0);
                    }
                }

                foreach (var item in availableItems[entity_id])
                {
                    if (editedIds.Contains(item.Value.item_id) || String.IsNullOrEmpty(item.Value.order_id))
                        continue;
                    item.Value.exist = false;
                }
            }
        }

        internal static bool DontCheckSecondPrice(int entity_id, int id)
        {
            lock (accItemsLocker)
            {
                if (availableItems.ContainsKey(entity_id) || availableItems[entity_id] == null || availableItems[entity_id].ContainsKey(id))
                {
                    return availableItems[entity_id][id].dontCheckSecondPrice;
                }
                return false;
            }
        }

        public static void DellItem(int entity_id, int id)
        {
            lock (accItemsLocker)
            {
                if (availableItems.ContainsKey(entity_id))
                {
                    if (availableItems[entity_id].ContainsKey(id))
                        availableItems[entity_id][id].exist = false;
                }
            }
        }

        public static int PopItemFromInventory(int entity_id, decimal potentialSellingPrice, bool force = false)
        {
            lock (accItemsLocker)
            {
                if (availableItems.ContainsKey(entity_id))
                {
                    if (availableItems[entity_id] == null || availableItems[entity_id].Count == 0)
                        return 0;
                    foreach (var item in availableItems[entity_id])
                    {
                        if (item.Value.exist && String.IsNullOrEmpty(item.Value.order_id) && (force || item.Value.canSelledTime <= DateTime.Now))
                        {
                            //! // еще добавить проверку на пустоту order_id?
                            if (item.Value.sellLessThenBought || item.Value.boughtPrice == 0 || item.Value.boughtPrice <= potentialSellingPrice * .85m)
                            {
                                item.Value.order_id = "?";
                                return item.Value.item_id;
                            }
                        }
                    }
                    //if (accInventoryItems[entity_id].Count > 0)
                    //{
                    //    int item = accInventoryItems[entity_id].First();
                    //    accInventoryItems[entity_id].Remove(item);
                    //    return item;
                    //}
                }
                return 0;
            }
        }

        public static AvailableItemInfo GetItemInfo(int entity_id, int item_id)
        {
            if (!availableItems.ContainsKey(entity_id))
                return null;
            if (!availableItems[entity_id].ContainsKey(item_id))
                return null;
            return availableItems[entity_id][item_id];
        }

        public static void AddPrevOrdersStats(int entity_id, string count, decimal minPrice)
        {
            minPrevOrdersStats[entity_id] = new AII() { minPrice = minPrice, position = count, lastUpdateOrdersInfo = DateTime.Now };
        }

        public static AII GetPrevOrdersStats(int entity_id)
        {
            if (minPrevOrdersStats.ContainsKey(entity_id))
                return minPrevOrdersStats[entity_id];
            return null;
        }

        static Dictionary<int, string> titles = null;
        public static List<AvailableItemsForDGV> GetAvailableItemsForDGV()
        {
            lock (accItemsLocker)
            {
                List<AvailableItemsForDGV> list = new List<AvailableItemsForDGV>();
                if (titles == null || titles.Count == 0)
                {
                    var dbItems = Global.dbLogic.GetDBItems();
                    titles = new Dictionary<int, string>();
                    foreach (var item in dbItems)
                    {
                        titles.Add(item.entity_id, item.title);
                    }
                }
                if (titles == null || titles.Count == 0)
                    return list;
                
                foreach (var itemEntity in availableItems)
                {
                    foreach (var item in itemEntity.Value)
                    {
                        if (!item.Value.exist)
                            continue;
                        var lowestValues = GetPrevOrdersStats(item.Value.entity_id);
                        System.Drawing.Color color = System.Drawing.Color.Empty;
                        if (item.Value.canSelledTime > DateTime.Now)
                            color = System.Drawing.Color.LightSkyBlue;
                        else if (!String.IsNullOrEmpty(item.Value.order_id) && lowestValues != null)
                        {
                            if (int.TryParse(lowestValues.position, out int posInt) && posInt == 0)
                                color = System.Drawing.Color.LightGreen;
                            else
                            {
                                if (lowestValues != null && item.Value.myPrice <= lowestValues.minPrice)
                                    color = System.Drawing.Color.PaleGoldenrod;
                                else
                                    color = System.Drawing.Color.LightPink;
                            }
                        }
                        list.Add(new AvailableItemsForDGV()
                        {
                            boughPrice = item.Value.boughtPrice,
                            boughtTime = item.Value.boughtTime,
                            lastUpdateOrderInfo = lowestValues != null ? lowestValues.lastUpdateOrdersInfo : DateTime.MinValue,
                            id = item.Value.item_id,
                            entity_id = item.Value.entity_id,
                            title = titles[item.Value.entity_id],
                            lowestPrice = lowestValues != null ? lowestValues.minPrice : 0,
                            myPrice = item.Value.myPrice,
                            position = lowestValues != null ? lowestValues.position : "?",
                            state = String.IsNullOrEmpty(item.Value.order_id) ? "Inventory" : "0n order",
                            color = color
                        });
                    }
                }
                return list.OrderBy(x => x.state).ThenBy(x => x.title).ThenByDescending(x => x.boughPrice).ToList();
            }
        }
    }
}
