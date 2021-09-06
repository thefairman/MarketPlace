using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDmp
{
    public class ItemBuyingSettings
    {
        public bool onlyGlobal;
        public decimal percent;
        public decimal fixPrice;
        public decimal maxPrice;
        public decimal minProfitPercent;
        public decimal minProfitAmount;
        public bool IsBuyedItem()
        {
            return fixPrice > 0 || maxPrice > 0 || minProfitAmount > 0 || minProfitPercent > 0 || percent > 0;
        }
    }
    public class ItemSellingSettings
    {
        public decimal minPrice;
        public decimal maxDropPercent;
        public decimal maxDropAmount;
        public decimal maxDropPercentByAvg = 10;
        public bool autoDrop;
        public bool IsSellnigItem()
        {
            return autoDrop;
        }
    }
    public class ItemSettings
    {
        public int entity_id;
        public ItemBuyingSettings buyingSettings = new ItemBuyingSettings();
        public ItemSellingSettings sellingSettings = new ItemSellingSettings();
    }
    public class ItemsManager
    {
        readonly string path = "ItemsSettings.json";
        public ConcurrentDictionary<int, ItemSettings> ItemsSettings { get; private set; }
        public ItemsManager()
        {
            LoadItems();
        }

        private void LoadItems()
        {
            if (File.Exists(path))
            {
                ItemsSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<ConcurrentDictionary<int, ItemSettings>>(File.ReadAllText(path), new CustomDateTimeConverter());
            }
            if (ItemsSettings == null)
                ItemsSettings = new ConcurrentDictionary<int, ItemSettings>();
        }

        public void AddItem(int id, ItemBuyingSettings buyingSettings, ItemSellingSettings sellingSettings)
        {
            if (ItemsSettings == null)
                ItemsSettings = new ConcurrentDictionary<int, ItemSettings>();
            ItemsSettings[id] = new ItemSettings()
            {
                entity_id = id,
                buyingSettings = buyingSettings,
                sellingSettings = sellingSettings
            };
            SaveItems();
        }

        public List<ItemSettings> GetActualItems()
        {
            return ItemsSettings.Values.Where(x => x.buyingSettings.IsBuyedItem() || x.sellingSettings.IsSellnigItem()).ToList();
        }

        readonly static private object lockerSaveItems = new object();
        public void SaveItems()
        {
            lock (lockerSaveItems)
            {
                File.WriteAllText(path, Newtonsoft.Json.JsonConvert.SerializeObject(ItemsSettings, new CustomDateTimeConverter()));
            }
        }
    }
}
