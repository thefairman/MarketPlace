using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDmp
{
    public class AppSettings<T> where T : new()
    {
        public void Save(string filePath)
        {
            File.WriteAllText(filePath, Newtonsoft.Json.JsonConvert.SerializeObject(this, new CustomDateTimeConverter()));
        }
        public static T Load(string filePath)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(File.ReadAllText(filePath), new CustomDateTimeConverter());
        }
    }

    public class AppSettingsManager
    {
        public static void LoadSettings()
        {
            if (File.Exists(GetSettingsPath()))
            {
                Global.settings = MySettings.Load(GetSettingsPath());
            }
            if (Global.settings == null)
                Global.settings = new MySettings();
        }

        public static string GetSettingsPath()
        {
            return Path.Combine(Environment.CurrentDirectory, "settings.json");
        }
    }

    public class MySettings : AppSettings<MySettings>
    {
        public bool UsingProxy { get; set; } = true;
        public bool UsingProxyToUser { get; set; } = true;
        public int ReqMax { get; set; } = 50;
        public int TimeOutProxySec { get; set; } = 10;
        public bool SwitchToProxyIfBanned { get; set; } = true;
        public int MaxPrice { get; set; } = 0;
        public bool AutoSelling { get; set; }
        public bool AutoSellingWhenBuy { get; set; }
        public int ThreadsNum { get; set; } = 10;
        public int AdditThreadsNum { get; set; } = 10;
        public int RequestTimesCount { get; set; } = 100;
        public int FromSellingHoldTime { get; set; } = 10;
        public int ToSellingHoldTime { get; set; } = 40;
        public bool OnlyAdditional { get; set; } = false;
        public bool UsingProxyAdditional { get; set; } = false;
        public int MaxOrdersBeforeMe { get; set; } = 5;
        public int MaxReqsInMinute { get; set; } = 40;
        public decimal EuroRate { get; set; } = 71;
        public int CheckingUserInMinutes { get; set; } = 5;
    }
}
