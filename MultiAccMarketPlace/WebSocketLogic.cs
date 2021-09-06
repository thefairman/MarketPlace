using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocket4Net;

namespace LDmp
{
    class WebSocketLogic
    {
        static WebSocket websocket;
        WebSocketData webSocketData;
        public ManualResetEvent mre = new ManualResetEvent(false);
        bool first = true;
        int uid = 1;
        System.Timers.Timer timerPing = new System.Timers.Timer(180000);
        // Объявляем делегат
        public delegate void PriceChangedHandler(int product, decimal price);
        // Событие, возникающее при выводе денег
        public event PriceChangedHandler PriceChanged;
        public WebSocketLogic()
        {
            timerPing.Elapsed += TimerPing_Elapsed;
        }

        public static string GetLastActiveTime()
        {
            if (websocket != null)
                return websocket.LastActiveTime.ToString("dd.MM.yyyy HH:mm:ss.fff");
            return "?";
        }

        private void TimerPing_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            websocket.Send($"{{\"method\":\"ping\",\"uid\":\"{GetUID()}\"}}");
        }

        int GetUID()
        {
            return uid++;
        }

        internal void Start()
        {
            webSocketData = AdditionalUnitsManager.GetAdditionalUnit().GetWebSocketData();
            string orgigin = "https://lootdog.io";
            websocket = new WebSocket(webSocketData.wsSettings.url, "", webSocketData.cookies, null, webSocketData.userAgent, orgigin, WebSocketVersion.Rfc6455);
            //websocket.EnableAutoSendPing = true;
            //websocket.AutoSendPingInterval = 180000;
            //websocket.
            websocket.Opened += new EventHandler(Websocket_Opened);
            websocket.Error += new EventHandler<ErrorEventArgs>(Websocket_Error);
            websocket.Closed += new EventHandler(Websocket_Closed);
            websocket.MessageReceived += new EventHandler<MessageReceivedEventArgs>(Websocket_MessageReceived);
            mre.Reset();
            first = true;
            uid = 1;
            websocket.Open();
        }

        private void Websocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            if (first)
            {
                first = false;
                websocket.Send($"{{\"method\":\"subscribe\",\"params\":{{\"channel\":\"{webSocketData.wsSettings.channel}\"}},\"uid\":\"{GetUID()}\"}}");
                return;
            }
            try
            {
                var msg = Newtonsoft.Json.JsonConvert.DeserializeObject<WSMessage>(((WebSocket4Net.MessageReceivedEventArgs)e).Message);
                if (msg.method == "message" && msg.body.data.type == "product_price_changed")
                    PriceChanged?.Invoke(msg.body.data.product, msg.body.data.price.RU.RUB.amount);
            }
            catch (Exception ex)
            {
                Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage(ex.Message));
            }
        }

        private void Websocket_Closed(object sender, EventArgs e)
        {
            while (true)
            {
                try
                {
                    if (!MainWork.PausedOrPausing())
                        Start();
                    else
                        mre.Set();
                    break;
                }
                catch (Exception ex)
                {
                    Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage(ex.Message));
                }
            }
        }

        private void Websocket_Error(object sender, ErrorEventArgs e)
        {
            Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage(e.Exception.Message));
        }

        private void Websocket_Opened(object sender, EventArgs e)
        {
            websocket.Send($"{{\"method\":\"connect\",\"params\":{{\"user\":\"\",\"info\":\"\",\"timestamp\":\"{webSocketData.wsSettings.timestamp}\",\"token\":\"{webSocketData.wsSettings.token}\"}},\"uid\":\"{GetUID()}\"}}");
        }

        internal void Close()
        {
            websocket.Close();
        }
    }
}
