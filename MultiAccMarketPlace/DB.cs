using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDmp
{
    public class DB : IDisposable
    {
        static readonly string DatabaseFile = "database.db";
        readonly SQLite.SQLiteConnection db;

        public DB()
        {
            db = new SQLite.SQLiteConnection(DatabaseFile, true);
            CreateTables();
        }

        void CreateTables()
        {
            db.CreateTable<DBItem>();
            db.CreateTable<DBAvailableItem>();
            db.CreateTable<DBItemHistory>();
            db.CreateTable<DBItemTradeStats>();
        }

        public void Dispose()
        {
            if (db.IsInTransaction)
            {
                db.Commit();
            }
            if (db != null)
            {
                db.Close();
            }
        }

        public int WriteItemAnswerToDB(ItemMPAnswer item)
        {
            if (item == null) return 0;
            return db.Insert(new DBItemHistory()
            {
                entity_id = item.id,
                count = item.on_sale_count,
                min_cost = item.buy_price.amount,
                popularity = item.popularity
            });
        }

        public DBItem AddItemToDB(ItemMPAnswer item)
        {
            if (item == null) return null;
            DBItem dbItem = null;
            db.RunInTransaction(() =>
            {
                var dbItemCur = new DBItem()
                {
                    entity_id = item.id,
                    title = item.name
                };
                if (db.InsertOrReplace(dbItemCur) > 0)
                    dbItem = dbItemCur;
                //var res = db.Table<DBItem>().Where(s => s.entity_id == item.id);
                //if (res.Count() == 0)
                //    db.Insert(new DBItem()
                //    {
                //        entity_id = item.id,
                //        title = item.name
                //    });
                //else
                //    dbItem = res.First();
            });
            return dbItem;
        }

        internal List<DBItem> GetDBItems()
        {
            List<DBItem> dbItems = new List<DBItem>();
            var res = db.Table<DBItem>();
            if (res.Count() > 0)
                dbItems = res.ToList();
            return dbItems;
        }

        internal List<DBItemHistory> GetDBLastPrices()
        {
            return db.Query<DBItemHistory>("SELECT *, MAX(time) FROM DBItemHistory GROUP BY `entity_id`");
        }

        internal int WriteItemsAnswerToDB(SortedDictionary<DateTime, List<ItemMPAnswer>> param)
        {
            if (param == null || param.Count == 0) return 0;
            int inserted = 0;
            db.RunInTransaction(() =>
            {
                foreach (var items in param)
                {
                    foreach (var item in items.Value)
                    {
                        inserted += db.Insert(new DBItemHistory()
                        {
                            entity_id = item.id,
                            count = item.on_sale_count,
                            min_cost = item.buy_price.amount,
                            time = items.Key,
                            popularity = item.popularity
                        });
                    }
                }
            });
            db.Commit();
            return inserted;
        }

        internal List<DBItem> AddItemsToDB(List<ItemMPAnswer> param)
        {
            if (param == null || param.Count == 0) return null;
            List<DBItem> dbItems = new List<DBItem>();
            db.RunInTransaction(() =>
            {
                foreach (var item in param)
                {
                    var dbItem = new DBItem()
                    {
                        entity_id = item.id,
                        title = item.name
                    };
                    if (db.InsertOrReplace(dbItem) > 0)
                        dbItems.Add(dbItem);
                    //var res = db.Table<DBItem>().Where(s => s.entity_id == item.id);
                    //if (res.Count() == 0)
                    //{
                    //    int insertedCount = db.Insert(new DBItem()
                    //    {
                    //        entity_id = item.id,
                    //        title = item.name
                    //    });
                    //    if (insertedCount > 0)
                    //        dbItems.Add(res.First());
                    //}
                    //else
                    //    dbItems.Add(res.First());
                }
            });
            db.Commit();
            return dbItems;
        }

        internal int AddAvailableItemsToDB(string userId, List<AvailableItemInfo> listOfNewItems)
        {
            if (listOfNewItems == null || listOfNewItems.Count == 0) return 0;
            int addRecordscount = 0;
            db.RunInTransaction(() =>
            {
                foreach (var item in listOfNewItems)
                {
                    var res = db.Table<DBAvailableItem>().Where(s => s.user_id == userId && s.item_id == item.item_id);
                    if (res.Count() == 0)
                    {
                        int insertedCount = db.Insert(new DBAvailableItem()
                        {
                            bought_price = item.boughtPrice,
                            bought_time = item.boughtTime,
                            entity_id = item.entity_id,
                            item_id = item.item_id,
                            user_id = userId
                        });
                        addRecordscount += insertedCount;
                    }
                    else
                        addRecordscount++;
                }
            });
            db.Commit();
            return addRecordscount;
        }

        internal List<DBAvailableItem> GetAvailableItemsFromDB(string useirID, int from, int to)
        {
            List<DBAvailableItem> dbItems = new List<DBAvailableItem>();
            var res = db.Table<DBAvailableItem>().Where(x => x.user_id == useirID && x.item_id >= from && x.item_id <= to);
            if (res.Count() > 0)
                dbItems = res.ToList();
            return dbItems;
        }

        public int AddItemTradeStats(int itemId, List<MarketStatInfo> marketStats)
        {
            if (marketStats == null || marketStats.Count == 0) return 0;
            int addRecordscount = 0;
            db.RunInTransaction(() =>
            {
                foreach (var item in marketStats)
                {
                    addRecordscount += db.InsertOrReplace(new DBItemTradeStats()
                    {
                        entity_id = itemId,
                        sale_count = item.trades,
                        sum = item.sum.amount,
                        time = item.time,
                        uniqtimeid = $"{item.time}|{itemId}"
                    });
                    //var res = db.Table<DBItemTradeStats>().Where(s => s.entity_id == itemId && s.time == item.time);
                    //if (res.Count() == 0)
                    //{
                    //    int insertedCount = db.Insert(new DBItemTradeStats()
                    //    {
                    //        entity_id = itemId,
                    //        sale_count = item.trades,
                    //        sum = item.sum.amount,
                    //        time = item.time
                    //    });
                    //    addRecordscount += insertedCount;
                    //}
                    //else
                    //{
                    //    var existItem = res.Last();
                    //    existItem.sale_count = item.trades;
                    //    existItem.sum = item.sum.amount;
                    //    addRecordscount += db.Update(existItem);
                    //}
                }
            });
            db.Commit();
            return addRecordscount;
        }

        internal List<DBItemTradeStats> GetItemTradeStatsFromDB(int itemId, DateTime from, DateTime to)
        {
            List<DBItemTradeStats> dbItems = new List<DBItemTradeStats>();
            var res = db.Table<DBItemTradeStats>().Where(x => x.entity_id == itemId && x.time >= from && x.time <= to);
            if (res.Count() > 0)
                dbItems = res.ToList();
            return dbItems;
        }

        internal int EditAvailableItemsToDB(string useirID, AvailableItemInfo availableItemInfo)
        {
            var res = db.Table<DBAvailableItem>().Where(x => x.user_id == useirID && x.item_id == availableItemInfo.item_id);
            if (res.Count() > 0)
            {
                var aitem = res.First();
                aitem.sellLessThenBought = availableItemInfo.sellLessThenBought;
                aitem.dontUpItem = availableItemInfo.dontUpItem;
                aitem.dontCheckSecondPrice = availableItemInfo.dontCheckSecondPrice;
                return db.Update(aitem);
            }
            return 0;
        }

        public List<DBItemTradeStats> GetLastRecordsItemsTradeStatsFromDB()
        {
            return db.Query<DBItemTradeStats>("SELECT *, MAX(time) FROM DBItemTradeStats GROUP BY `entity_id`");
        }

        internal List<DBItemTradeStats> GetItemTradeStatsFromDB(int itemId)
        {
            List<DBItemTradeStats> dbItems = new List<DBItemTradeStats>();
            var res = db.Table<DBItemTradeStats>().Where(x => x.entity_id == itemId);
            if (res.Count() > 0)
                dbItems = res.ToList();
            return dbItems;
        }

        internal object AddItemTradeStats(Dictionary<int, List<MarketStatInfo>> tradeStats)
        {
            int addRecordscount = 0;
            db.RunInTransaction(() =>
            {
                foreach (var item in tradeStats)
                {
                    if (item.Value == null || item.Value.Count == 0)
                        continue;
                    foreach (var elem in item.Value)
                    {
                        addRecordscount += db.InsertOrReplace(new DBItemTradeStats()
                        {
                            entity_id = item.Key,
                            sale_count = elem.trades,
                            sum = elem.sum.amount,
                            time = elem.time,
                            uniqtimeid = $"{elem.time}|{item.Key}"
                        });
                    }
                }
            });
            db.Commit();
            return addRecordscount;
        }
    }
}
