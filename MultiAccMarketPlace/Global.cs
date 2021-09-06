using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDmp
{
    public static class Global
    {
        static public MySettings settings;
        public static ProxyLogic proxyLogic = new ProxyLogic();
        public static ItemsManager itemsManager = new ItemsManager();
        public static UIDataMain UIdata { get; set; } = new UIDataMain();
        static public double lastRuCaptchaBalance = -1;
        public static DBLogic dbLogic { get; set; } = new DBLogic();
    }
}
