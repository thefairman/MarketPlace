using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LDmp
{
    public class DBLogic
    {
        readonly double updateDBItemsIntervalinMinutes = 1;
        readonly int secondsWhenUpdate = 30;
        DateTime lastUpdateDBItemsTime;
        readonly DB db = new DB();
        public HashSet<DBItem> DBItems { get; private set; }
        ConcurrentQueue<KeyValuePair<DateTime, List<ItemMPAnswer>>> queueMPAnswers = new ConcurrentQueue<KeyValuePair<DateTime, List<ItemMPAnswer>>>();
        public DBLogic()
        {
            DBItems = new HashSet<DBItem>(GetDBItems());
            lastUpdateDBItemsTime = DateTime.Now;
            Task.Factory.StartNew(QueueExecution);
            Task.Run(QueueTradeStatsExecution);
        }
        public AutoResetEvent QueueIsEmpty = new AutoResetEvent(false);

        Dictionary<int, KeyValuePair<DateTime, ItemMPAnswer>> prevItemsAnswer = new Dictionary<int, KeyValuePair<DateTime, ItemMPAnswer>>();
        void QueueExecution()
        {
            while (true) // todo
            {
                DateTime now = DateTime.Now;
                DateTime timeWhenShouldUpdate = lastUpdateDBItemsTime.AddMinutes(updateDBItemsIntervalinMinutes);
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

                bool someGotInQueue = false;
                bool prevAnswerNotContainsKey;
                SortedDictionary<DateTime, List<ItemMPAnswer>> sortedMPAnswers = new SortedDictionary<DateTime, List<ItemMPAnswer>>();
                List<ItemMPAnswer> listOfNewItems = new List<ItemMPAnswer>();
                while (queueMPAnswers.TryDequeue(out KeyValuePair<DateTime, List<ItemMPAnswer>> itemInQueue))
                {
                    someGotInQueue = true;
                    List<ItemMPAnswer> currentItemsWhoesNeedToUpdate = new List<ItemMPAnswer>();
                    foreach (var item in itemInQueue.Value)
                    {
                        prevAnswerNotContainsKey = !prevItemsAnswer.ContainsKey(item.id);
                        if (prevAnswerNotContainsKey || (itemInQueue.Key > prevItemsAnswer[item.id].Key && !prevItemsAnswer[item.id].Value.Equals(item)))
                        {
                            if (prevAnswerNotContainsKey)
                            {
                                if (!DBItems.Contains(new DBItem() { entity_id = item.id }))
                                    listOfNewItems.Add(item);
                            }
                            currentItemsWhoesNeedToUpdate.Add(item);
                            prevItemsAnswer[item.id] = new KeyValuePair<DateTime, ItemMPAnswer>(itemInQueue.Key, item);
                        }
                    }
                    if (currentItemsWhoesNeedToUpdate.Count > 0)
                        sortedMPAnswers.Add(itemInQueue.Key, currentItemsWhoesNeedToUpdate);
                }

                if (listOfNewItems != null && listOfNewItems.Count > 0)
                    DBItems.UnionWith(AddItemsToDB(listOfNewItems));
                WriteItemsAnswerToDB(sortedMPAnswers);
                if (someGotInQueue) QueueIsEmpty.Set();
                lastUpdateDBItemsTime = DateTime.Now;
                //KeyValuePair<DateTime, ItemAnswer> itemAnswer = queueMPAnswers.Take();
                //bool prevAnswerNotContainsKey = !prevItemsAnswer.ContainsKey(itemAnswer.Value.entity_id);
                //if (prevAnswerNotContainsKey || (itemAnswer.Key > prevItemsAnswer[itemAnswer.Value.entity_id].Key && !prevItemsAnswer[itemAnswer.Value.entity_id].Value.Equals(itemAnswer)))
                //{
                //    if (prevAnswerNotContainsKey)
                //    {
                //        if (!DBItems.Contains(new DBItem() { entity_id = itemAnswer.Value.entity_id }))
                //            DBItems.Add(AddItemToDB(itemAnswer.Value));
                //    }
                //    WriteItemAnswerToDB(itemAnswer.Value);
                //    prevItemsAnswer[itemAnswer.Value.entity_id] = itemAnswer;
                //}
            }
        }

        public int GetQueueCount()
        {
            return queueMPAnswers.Count;
        }

        public void SetMinusLastUpdateDBItemsTime()
        {
            lastUpdateDBItemsTime = DateTime.Now.AddMinutes(-(updateDBItemsIntervalinMinutes + 1));
        }

        public void AddItemToQueue(KeyValuePair<DateTime, List<ItemMPAnswer>> TimeAndItemsAnswer)
        {
            queueMPAnswers.Enqueue(TimeAndItemsAnswer);
        }

        static readonly object lockCore = new object();
        object Core(Func<object, object> someAction, object param)
        {
            lock (lockCore)
            {
                int marketErrorsCount = 0;
                while (true)
                {
                    object retVal = null;
                    try
                    {
                        retVal = someAction(param);
                    }
                    catch (Exception ex)
                    {
                        Logman.LogAndDisplay(ex);

                        Thread.Sleep(1000 * marketErrorsCount);
                        continue;
                    }
                    return retVal;
                }
            }
        }

        #region WriteItemAnswerToDB
        public void WriteItemAnswerToDB(ItemMPAnswer itemAnswer)
        {
            Core(WriteItemAnswerToDBBack, itemAnswer);
        }
        object WriteItemAnswerToDBBack(object param)
        {
            return db.WriteItemAnswerToDB((ItemMPAnswer)param);
        }
        #endregion

        #region AddItemToDB
        public DBItem AddItemToDB(ItemMPAnswer itemAnswer)
        {
            return (DBItem)Core(AddItemToDBBack, itemAnswer);
        }
        object AddItemToDBBack(object param)
        {
            return db.AddItemToDB((ItemMPAnswer)param);
        }
        #endregion

        #region GetDBItems
        public List<DBItem> GetDBItems()
        {
            return (List<DBItem>)Core(GetDBItemsBack, null);
        }
        object GetDBItemsBack(object param)
        {
            return db.GetDBItems();
        }
        #endregion

        #region GetDBLastPrices
        public List<DBItemHistory> GetDBLastPrices()
        {
            return (List<DBItemHistory>)Core(GetLDBastPricesBack, null);
        }
        object GetLDBastPricesBack(object param)
        {
            return db.GetDBLastPrices();
        }
        #endregion

        #region WriteItemsAnswerToDB
        private void WriteItemsAnswerToDB(SortedDictionary<DateTime, List<ItemMPAnswer>> sortedMPAnswers)
        {
            Core(WriteItemsAnswerToDBBack, sortedMPAnswers);
        }
        object WriteItemsAnswerToDBBack(object param)
        {
            return db.WriteItemsAnswerToDB((SortedDictionary<DateTime, List<ItemMPAnswer>>)param);
        }
        #endregion

        #region AddItemsToDB
        private List<DBItem> AddItemsToDB(List<ItemMPAnswer> listOfNewItems)
        {
            return (List<DBItem>)Core(AddItemsToDBBack, listOfNewItems);
        }
        object AddItemsToDBBack(object param)
        {
            return db.AddItemsToDB((List<ItemMPAnswer>)param);
        }
        #endregion

        #region AddAvailableItemsToDB
        public int AddAvailableItemsToDB(string useirID, List<AvailableItemInfo> listOfNewItems)
        {
            return (int)Core(x=>
            {
                return db.AddAvailableItemsToDB(useirID, listOfNewItems);
            }, null);
        }
        #endregion

        #region AddAvailableItemsToDB
        public int EditAvailableItemsToDB(string useirID, AvailableItemInfo availableItemInfo)
        {
            return (int)Core(x =>
            {
                return db.EditAvailableItemsToDB(useirID, availableItemInfo);
            }, null);
        }
        #endregion

        #region GetAvailableItemsFromDB
        public List<DBAvailableItem> GetAvailableItemsFromDB(string useirID, int from, int to)
        {
            return (List<DBAvailableItem>)Core(x =>
            {
                return db.GetAvailableItemsFromDB(useirID, from, to);
            }, null);
        }
        #endregion

        #region AddItemTradeStats
        readonly object tradeStatsLocker = new object();
        Dictionary<int, List<MarketStatInfo>> tradeStats = new Dictionary<int, List<MarketStatInfo>>();
        Dictionary<int, MarketStatInfo> lastTradeStatsUpdatedValues = new Dictionary<int, MarketStatInfo>();
        public void AddItemTradeStats(int itemId, List<MarketStatInfo> marketStats)
        {
            if (marketStats == null || marketStats.Count == 0) return;
            Task.Run(() =>
            {
                lock (tradeStatsLocker)
                {
                    //var watch = System.Diagnostics.Stopwatch.StartNew();
                    if (tradeStats.ContainsKey(itemId))
                    {
                        int itemsCount = tradeStats[itemId].Count;
                        if (itemsCount > 0)
                        {
                            var newestList = marketStats.Where(x => x.time >= tradeStats[itemId].Last().time).ToList();
                            if (newestList == null || newestList.Count == 0)
                                return;
                            var lastPrev = tradeStats[itemId].Last();
                            var firstNew = newestList.First();
                            if (lastPrev.time == firstNew.time)
                                tradeStats[itemId].RemoveAt(itemsCount - 1);
                            tradeStats[itemId].AddRange(newestList);
                        }
                        else
                            tradeStats[itemId] = marketStats;
                    }
                    else
                        tradeStats[itemId] = marketStats;
                    //watch.Stop();
                }
            });
        }

        bool forceTradeStatsExecution = false;
        readonly AutoResetEvent areTradeStats = new AutoResetEvent(false);
        public Task GetAwaiterForTradeStats()
        {
            Task tsk = Task.Run(() => areTradeStats.WaitOne());
            forceTradeStatsExecution = true;
            return tsk;
        }

        void QueueTradeStatsExecution()
        {
            while (true) // todo
            {
                if (!forceTradeStatsExecution)
                {
                    DateTime now = DateTime.Now;
                    DateTime timeWhenShouldUpdate = lastUpdateDBItemsTime.AddMinutes(5);
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
                }
                
                lock (tradeStatsLocker)
                {
                    bool haveNewRecors = false;
                    List<int> keys = new List<int>(tradeStats.Keys);
                    foreach (var key in keys)
                    {
                        if (lastTradeStatsUpdatedValues.ContainsKey(key))
                        {
                            var newesRecords = tradeStats[key].Where(x => x.time >= lastTradeStatsUpdatedValues[key].time).ToList();
                            if (newesRecords == null || newesRecords.Count == 0)
                                tradeStats[key].Clear();
                            else
                            {
                                var firstOfNewRecs = newesRecords.First();
                                if (lastTradeStatsUpdatedValues[key].time == firstOfNewRecs.time &&
                                    lastTradeStatsUpdatedValues[key].sum == firstOfNewRecs.sum &&
                                    lastTradeStatsUpdatedValues[key].trades == firstOfNewRecs.trades)
                                    newesRecords.RemoveAt(0);
                                tradeStats[key] = newesRecords;
                            }
                        }
                        if (tradeStats[key].Count > 0)
                            haveNewRecors = true;
                    }
                    if (haveNewRecors)
                    {
                        Core(x =>
                        {
                            return db.AddItemTradeStats(tradeStats);
                        }, null);

                        foreach (var key in keys)
                        {
                            if (tradeStats[key].Count > 0)
                            {
                                lastTradeStatsUpdatedValues[key] = tradeStats[key].Last();
                                tradeStats[key].Clear();
                                tradeStats[key].Add(lastTradeStatsUpdatedValues[key]);
                            }
                        }
                    }
                }
                if (forceTradeStatsExecution)
                {
                    areTradeStats.Set();
                    forceTradeStatsExecution = false;
                }
            }
        }

        #endregion

        #region GetItemTradeStatsFromDB
        public List<DBItemTradeStats> GetItemTradeStatsFromDB(int itemId, DateTime from, DateTime to)
        {
            return (List<DBItemTradeStats>)Core(x =>
            {
                return db.GetItemTradeStatsFromDB(itemId, from, to);
            }, null);
        }
        #endregion

        #region GetItemTradeStatsFromDBAll
        public List<DBItemTradeStats> GetItemTradeStatsFromDB(int itemId)
        {
            return (List<DBItemTradeStats>)Core(x =>
            {
                return db.GetItemTradeStatsFromDB(itemId);
            }, null);
        }
        #endregion

        #region GetLastRecordsItemsTradeStatsFromDB
        public List<DBItemTradeStats> GetLastRecordsItemsTradeStatsFromDB()
        {
            return (List<DBItemTradeStats>)Core(x =>
            {
                return db.GetLastRecordsItemsTradeStatsFromDB();
            }, null);
        }
        #endregion
    }
}
