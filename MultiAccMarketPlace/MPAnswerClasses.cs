using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDmp
{
    public class ExtraErrorAnswer
    {
        public string phone;
    }
    public class ErrorAnswer
    {
        public string code;
        public string detail;
        public ExtraErrorAnswer extra;
    }

    public class CurrencyInfo
    {
        public string currency;
        public decimal amount;
        public string caption;
    }
    public class ItemMPAnswer : IEquatable<ItemMPAnswer>
    {
        public CurrencyInfo buy_price;
        /// <summary>
        /// это id который относится к предмету на тп
        /// </summary>
        public int id;
        public int popularity;
        public int on_sale_count;
        public string name;
        public override bool Equals(object other)
        {
            return Equals(other as ItemMPAnswer);
        }
        public bool Equals(ItemMPAnswer other)
        {
            return other != null && other.id == this.id && other.on_sale_count == this.on_sale_count && other.buy_price.amount == this.buy_price.amount;
        }

        public override int GetHashCode()
        {
            return $"{id}{buy_price.amount}{on_sale_count}{name}".GetHashCode();
        }
    }
    public class ItemOrderIfoAnswer
    {
        /// <summary>
        /// это id который относится к предмету на тп
        /// </summary>
        public int id;
        public int popularity;
        public string name;
    }
    public class AllItemsMPAnswer
    {
        public int count;
        public List<ItemMPAnswer> results;
    }

    public class UserInfoAnswer
    {
        public CurrencyInfo balance;
        public int orders_count_on_sale;
        public string id;
    }
    public class ItemUinqueId
    {
        /// <summary>
        /// это уникальный id конкретного предмета, он уникален для каждого предмета
        /// </summary>
        public int id;
    }
    public class UserInfoInOrder
    {
        public string id;
    }
    public class OrderInfo
    {
        /// <summary>
        /// это id сделки, по нему совершается или отменяется сделка
        /// </summary>
        public string id;
        public CurrencyInfo buy_price;
        public UserInfoInOrder user;
        public ItemOrderIfoAnswer product;
        /// <summary>
        /// уникальные данные конкретного предмета, а не id на тп
        /// </summary>
        public ItemUinqueId item;
        /// <summary>
        /// цена предмета в евро
        /// </summary>
        public decimal amount;
    }
    public class OrdersAnswer
    {
        public int count;
        public List<OrderInfo> results;
    }

    public class ItemsOrderOrInvetnroyInfo
    {
        public ItemOrderIfoAnswer product;
        /// <summary>
        /// это уникальный id конкретного предмета, он уникален для каждого предмета
        /// </summary>
        public int id;
    }
    public class InventoryAnswer
    {
        public int count;
        public List<ItemsOrderOrInvetnroyInfo> results;
    }

    public class TransactionInfoAnswer
    {
        public ItemUinqueId item;
        public ItemOrderIfoAnswer product;
        public CurrencyInfo price;
    }
    public class BuyItemAnswer
    {
        public bool completed;
        public TransactionInfoAnswer transaction;
        public string error;
    }

    //public class WSAnswer
    //{
    //    public string url;
    //    public string timestamp;
    //    public string token;
    //    public string channel;
    //}
    public class SettingAnswer
    {
        //public WSAnswer public_ws;
        public string url;
        public string timestamp;
        public string token;
        public string channel;
    }
    public class WSPriceRU
    {
        public CurrencyInfo RUB;
    }
    public class WSPrice
    {
        public WSPriceRU RU;
    }
    public class WSMessageData
    {
        public int product;
        public string type;
        public WSPrice price;
        public int on_sale_count;
    }
    public class WSMessageBody
    {
        public WSMessageData data;
    }
    public class WSMessage
    {
        public string method;
        public WSMessageBody body;
    }

    public class MarketPriceInfo
    {
        public int orders_counter;
        public CurrencyInfo best_price;
    }
    public class MarketItemInfo
    {
        public int sold_today;
        public MarketPriceInfo price_info;
        public string best_order_id;
    }

    public class MarketStatInfo
    {
        public CurrencyInfo sum;
        public int trades;
        public DateTime time;
    }
}
