using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDmp
{
    public class UIMessage
    {
        public DateTime TimeRecord { get; protected set; } = DateTime.Now;
        public System.Drawing.Color color = System.Drawing.Color.Empty;
        public string message;
        public UIMessage(string message, DateTime? time = null, System.Drawing.Color? color = null)
        {
            this.message = message;
            if (time != null)
                TimeRecord = (DateTime)time;
            if (color != null)
                this.color = (System.Drawing.Color)color;
        }
        //abstract public List<object> GetListForDGV();
        //protected string TimeToFormatString()
        //{
        //    return TimeRecord.ToString("dd.MM.yyyy HH:mm:ss.fff");
        //}
    }
    //public class UIDataUnitBase<T> where T : UIMessage
    //{
    //    public List<T> messages { get; protected set; } = new List<T>();
    //    private readonly object listLocker = new object();
    //    int recordsLimit;
    //    public void SetRecordsLimit(int limit)
    //    {
    //        recordsLimit = limit;
    //    }
    //    public void AddRecord(T record)
    //    {
    //        lock (listLocker)
    //        {
    //            messages.Add(record);
    //            if (recordsLimit > 0 && messages.Count > recordsLimit)
    //            {
    //                messages.RemoveAt(0);
    //            }
    //        }
    //    }

    //    public List<T> GetColoredMessages(System.Drawing.Color color)
    //    {
    //        lock (listLocker)
    //        {
    //            return messages.Where(x => x.color == color).ToList();
    //        }
    //    }
    //}

    public class UIDataUnitBase<T> where T : UIMessage
    {
        SortedList<DateTime, T> messages = new SortedList<DateTime, T>();
        private readonly object listLocker = new object();
        int recordsLimit;
        public void SetRecordsLimit(int limit)
        {
            recordsLimit = limit;
        }
        public Task AddRecordAsync(T record)
        {
            return Task.Run(() =>
            {
                lock (listLocker)
                {
                    DateTime timeKey = record.TimeRecord;
                    while (messages.ContainsKey(timeKey))
                    {
                        timeKey = timeKey.AddTicks(1);
                    }
                    messages.Add(timeKey, record);
                    if (recordsLimit > 0 && messages.Count > recordsLimit)
                    {
                        messages.RemoveAt(0);
                    }
                }
            });
        }

        public List<T> GetAllMessages()
        {
            lock (listLocker)
            {
                return messages.Values.ToList();
            }
        }

        public List<T> GetColoredMessages(System.Drawing.Color color)
        {
            lock (listLocker)
            {
                return messages.Values.Where(x => x.color == color).ToList();
            }
        }

        public T GetMessageByIndex(int index)
        {
            lock (listLocker)
            {
                if (index < 0 || index >= messages.Count)
                    return null;
                return messages.Values[index];
            }
        }

        public int GetMessagesCount()
        {
            lock (listLocker)
            {
                return messages.Count();
            }
        }
    }

    public class UILogOfAction : UIMessage
    {
        public string login;
        public int entity_id = 0;
        public decimal cost = 0;
        public string title;
        public UILogOfAction(string login, int entity_id, string title, string message, decimal cost, System.Drawing.Color? color = null, DateTime? time = null) : base(message, time, color)
        {
            this.login = login;
            this.entity_id = entity_id;
            this.title = title;
            this.cost = cost;
        }
        //public override List<object> GetListForDGV()
        //{
        //    List<object> list = new List<object>();
        //    list.Add(login);
        //    list.Add(entity_id);
        //    list.Add(title);
        //    list.Add(message);
        //    list.Add(cost);
        //    list.Add(TimeToFormatString());
        //    if (color != System.Drawing.Color.Empty)
        //        list.Add(color);
        //    return list;
        //}
    }

    public class UILogOfRequestTime : UIMessage
    {
        public DateTime startTime;
        public UILogOfRequestTime(string message, DateTime startReqTime, DateTime? time = null, System.Drawing.Color? color = null) : base(message, time, color)
        {
            startTime = startReqTime;
        }
    }

    public class UIDataMain
    {
        public UIDataUnitBase<UILogOfAction> LogOfActionsUI { get; protected set; } = new UIDataUnitBase<UILogOfAction>();
        public UIDataUnitBase<UILogOfRequestTime> LogOfRequestsTimesUI { get; protected set; } = new UIDataUnitBase<UILogOfRequestTime>();
        public UIDataUnitBase<UIMessage> MessagesUI { get; protected set; } = new UIDataUnitBase<UIMessage>();
        public UIDataUnitBase<UIMessage> ErrororsUI { get; protected set; } = new UIDataUnitBase<UIMessage>();
    }

    public class AccountData
    {
        public string Login { get; set; }
        public string Pass { get; set; }
        public List<string> Cookies { get; set; } = new List<string>();
    }

    public class ItemsInfoForDGV
    {
        public string title;
        public decimal last_cost;
        public int count;
        public int popularity;
        public ItemSettings itemSettings;
    }

    public class AvailableItemsForDGV
    {
        public int id;
        public int entity_id;
        public string title;
        public string state;
        public DateTime lastUpdateOrderInfo;
        public string position;
        public decimal lowestPrice;
        public decimal myPrice;
        public decimal boughPrice;
        public DateTime boughtTime;
        public System.Drawing.Color color;
    }

    public class WebSocketData
    {
        public List<KeyValuePair<string, string>> cookies;
        public string userAgent;
        public SettingAnswer wsSettings;
    }
}
