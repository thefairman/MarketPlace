using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDmp
{
    public class DBItem
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int? id { get; set; } = null;
        [NotNull, Unique]
        public int entity_id { get; set; }
        [NotNull]
        public string title { get; set; } = "Unkown";

        public override int GetHashCode()
        {
            return entity_id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as DBItem;
            if (other == null) return false;
            return other.entity_id == entity_id;
        }
    }

    public class DBItemHistory
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int? id { get; set; } = null;
        [NotNull]
        public int entity_id { get; set; }
        [NotNull]
        public decimal min_cost { get; set; }
        [NotNull]
        public int count { get; set; }
        public int popularity { get; set; }

        public DateTime time { get; set; } = DateTime.Now;
    }

    public class DBAvailableItem
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int? id { get; set; } = null;
        [NotNull]
        public string user_id { get; set; }
        [NotNull]
        public int entity_id { get; set; }
        [NotNull]
        public int item_id { get; set; }
        [NotNull]
        public decimal bought_price { get; set; }
        public DateTime bought_time { get; set; } = DateTime.MinValue;
        public bool sellLessThenBought { get; set; }
        public bool dontUpItem { get; set; }
        public bool dontCheckSecondPrice { get; set; }
    }

    public class DBItemTradeStats
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int? id { get; set; } = null;
        [NotNull]
        public int entity_id { get; set; }
        [NotNull]
        public int sale_count { get; set; }
        [NotNull]
        public decimal sum { get; set; }
        public DateTime time { get; set; } = DateTime.Now;
        [NotNull, Unique]
        public string uniqtimeid { get; set; }
    }
}
