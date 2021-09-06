using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDmp
{
    public class CustomDateTimeConverter : Newtonsoft.Json.Converters.IsoDateTimeConverter
    { public CustomDateTimeConverter() { base.DateTimeFormat = "yyyy-MM-ddTHH:mm:ss"; } }

    public class ProxyInfo : ICloneable
    {
        public string address { get; set; }
        public string port { get; set; }
        public string login { get; set; }
        public string pass { get; set; }
        public ProxyInfo(string address_, string port_, string login_, string pass_)
        {
            address = address_;
            port = port_;
            login = login_;
            pass = pass_;
        }
        public ProxyInfo()
        {

        }

        public object Clone()
        {
            return new ProxyInfo { address = this.address, port = this.port, login = this.login, pass = this.pass };
        }
    }

    public class ProxyData
    {
        public bool alreadyUsing { get; set; } = false;
        public ProxyInfo proxy { get; set; }
        public int reqBeforeBan { get; set; } = 0;
        public int badProxyCount { get; set; } = 0;
        public int speed { get; set; } = int.MaxValue;
        public bool isBanned { get; set; } = false;
        public DateTime lastUsingTime { get; set; } = DateTime.MinValue;
        public List<int> reqsBeforeBan { get; set; } = new List<int>();
        public bool badProxy { get; set; } = false;

        public double GetAroundedTotalMinutes()
        {
            return new TimeSpan(lastUsingTime.Ticks).TotalMinutes / 10;
        }

        public ProxyData(string address_, string port_, string login_, string pass_)
        {
            proxy = new ProxyInfo(address_, port_, login_, pass_);
        }
        public ProxyData(ProxyInfo proxy_)
        {
            proxy = proxy_;
        }
        public ProxyData()
        {

        }
    }

    public class ProxyLogic : IDisposable
    {
        List<ProxyData> proxyes = new List<ProxyData>();
        DateTime lastSavedTime = DateTime.Now;
        bool needSave = false;
        string proxyPath;
        public event Action<Exception> SomeError;
        public int IntervalInMinutesToNeedSave { get; set; } = 1;

        public ProxyLogic()
        {
            proxyPath = Path.Combine(Environment.CurrentDirectory, "proxyData.json");
            LoadProxyesData();
            ResetAlreadyUsing();
        }

        void SendExseption(Exception ex)
        {
            if (SomeError != null)
                SomeError(ex);
        }

        void ResetAlreadyUsing()
        {
            foreach (var item in proxyes)
            {
                item.alreadyUsing = false;
            }
        }

        static readonly object lockCore = new object();
        object Core(Func<object, object> someAction, object param)
        {
            lock (lockCore)
            {
                if (needSave && lastSavedTime.AddMinutes(IntervalInMinutesToNeedSave) < DateTime.Now)
                {
                    SaveProxyesData();
                    needSave = false;
                    lastSavedTime = DateTime.Now;
                }

                return someAction(param);
            }
        }

        void ResetBadProxy()
        {
            int minBPC = -1;
            int badCnt = 0;
            int usingCnt = 0;

            for (int i = 0; i < proxyes.Count; i++)
            {
                //if (proxyes[i].lastUsingTime.AddMinutes(5) < DateTime.Now)
                //{
                //    proxyes[i].badProxy = false;
                //}
                if (!proxyes[i].alreadyUsing)
                {
                    if (minBPC < 0)
                        minBPC = i;
                    else if (proxyes[minBPC].badProxyCount > proxyes[i].badProxyCount)
                        minBPC = i;
                }
                else
                    usingCnt++;
                if (proxyes[i].badProxy)
                    badCnt++;
            }

            if (proxyes.Count != 0 && (badCnt == proxyes.Count || badCnt >= proxyes.Count - usingCnt))
                proxyes[minBPC].badProxy = false;
        }

        ProxyInfo GetBestProxy()
        {
            ResetBadProxy();
            SetMiddleValueOfSpeed();
            proxyes = proxyes.OrderBy(x => x.speed < middleVal).ThenBy(x => x.GetAroundedTotalMinutes()).ThenBy(x => x.isBanned).ThenBy(x => x.reqsBeforeBan.Count).ThenBy(x => x.reqBeforeBan).ToList();
            foreach (var item in proxyes)
            {
                if (!item.alreadyUsing && !item.badProxy)
                {
                    item.alreadyUsing = true;
                    return item.proxy;
                }
            }
            return null;
        }

        int middleVal;
        void SetMiddleValueOfSpeed()
        {
            int notZero = 0;
            foreach (var item in proxyes)
            {
                if (item.speed > 0 && item.speed != int.MaxValue)
                    notZero++;
            }
            proxyes = proxyes.OrderByDescending(x => x.speed).ToList();
            middleVal = proxyes[notZero / 2].speed;
        }

        private object GetBestAndFastProxy()
        {
            ResetBadProxy();
            SetMiddleValueOfSpeed();
            proxyes = proxyes.OrderBy(x => x.speed > middleVal).ThenBy(x => x.GetAroundedTotalMinutes()).ThenBy(x => x.isBanned).ThenBy(x => x.reqsBeforeBan.Count).ThenBy(x => x.reqBeforeBan).ToList();
            foreach (var item in proxyes)
            {
                if (!item.alreadyUsing && !item.badProxy)
                {
                    item.alreadyUsing = true;
                    return item.proxy;
                }
            }
            return null;
        }

        readonly object saveLocker = new object();
        void SaveProxyesData()
        {
            lock (saveLocker)
            {
                File.WriteAllText(proxyPath, Newtonsoft.Json.JsonConvert.SerializeObject(proxyes, new CustomDateTimeConverter()));
            }
        }

        void LoadProxyesData()
        {
            if (File.Exists(proxyPath))
            {
                proxyes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProxyData>>(File.ReadAllText(proxyPath), new CustomDateTimeConverter());
                if (proxyes == null)
                    proxyes = new List<ProxyData>();
            }
            else
                proxyes = new List<ProxyData>();
        }

        bool AlreadyHaveProxy(string address)
        {
            foreach (var item in proxyes)
            {
                if (item.proxy.address == address)
                    return true;
            }
            return false;
        }

        #region GetProxy
        public ProxyInfo GetProxy(bool fast)
        {
            needSave = true;
            try
            {
                return (ProxyInfo)Core(x =>
                {
                    if (fast)
                        return GetBestAndFastProxy();
                    else
                        return GetBestProxy();
                }, null);
            }
            catch (Exception ex)
            {
                SendExseption(ex);
                return null;
            }
        }
        #endregion

        #region GetUsingProxyCount
        public int GetUsingProxyCount()
        {
            try
            {
                return (int)Core(Get_UsingProxyCount, null);
            }
            catch (Exception ex)
            {
                SendExseption(ex);
                return -1;
            }
        }

        object Get_UsingProxyCount(object param)
        {
            int prxUse = 0;
            foreach (var item in proxyes)
            {
                if (item.alreadyUsing)
                    prxUse++;
            }
            return prxUse;
        }
        #endregion

        #region GetBannedIP
        public int GetBannedIP()
        {
            try
            {
                return (int)Core(Get_BannedIP, null);
            }
            catch (Exception ex)
            {
                SendExseption(ex);
                return -1;
            }
        }

        object Get_BannedIP(object param)
        {
            int prxUse = 0;
            foreach (var item in proxyes)
            {
                if (item.isBanned)
                    prxUse++;
            }
            return prxUse;
        }
        #endregion

        #region AddProxy
        public bool AddProxy(ProxyInfo proxy)
        {
            try
            {
                return (bool)Core(Add_Proxy, proxy);
            }
            catch (Exception ex)
            {
                SendExseption(ex);
                return false;
            }
        }

        object Add_Proxy(object param)
        {
            ProxyInfo proxy = (ProxyInfo)param;
            if (!AlreadyHaveProxy(proxy.address))
            {
                proxyes.Add(new ProxyData(proxy));
                SaveProxyesData();
            }
            return true;
        }
        #endregion

        #region ClearProxy
        public bool ClearProxy(ProxyInfo proxy)
        {
            try
            {
                return (bool)Core(Clear_Proxy, null);
            }
            catch (Exception ex)
            {
                SendExseption(ex);
                return false;
            }
        }

        object Clear_Proxy(object param)
        {
            proxyes.Clear();
            SaveProxyesData();
            return true;
        }
        #endregion

        #region GetProxyList
        public List<ProxyInfo> GetProxyList()
        {
            try
            {
                return (List<ProxyInfo>)Core(Get_ProxyList, null);
            }
            catch (Exception ex)
            {
                SendExseption(ex);
                return null;
            }
        }

        object Get_ProxyList(object param)
        {
            List<ProxyInfo> list = new List<ProxyInfo>();
            foreach (var item in proxyes)
            {
                list.Add(item.proxy);
            }
            return list;
        }
        #endregion

        #region ReseetBadProy
        public void ReseetBadProy()
        {
            try
            {
                Core(Reseet_BadProy, null);
            }
            catch (Exception ex)
            {
                SendExseption(ex);
            }
        }

        object Reseet_BadProy(object param)
        {
            foreach (var item in proxyes)
            {
                item.badProxy = false;
            }
            return null;
        }
        #endregion

        #region ChangeProxy
        public ProxyInfo ChangeProxy(string address, int requests, bool fast)
        {
            needSave = true;
            try
            {
                return (ProxyInfo)Core(z=>
                {
                    int ind = proxyes.FindIndex(x => x.proxy.address == address);
                    if (ind != -1)
                    {
                        proxyes[ind].alreadyUsing = false;
                        proxyes[ind].reqBeforeBan += requests;
                        proxyes[ind].lastUsingTime = DateTime.Now;
                    }
                    if (fast)
                        return GetBestAndFastProxy();
                    else
                        return GetBestProxy();
                }, null);
            }
            catch (Exception ex)
            {
                SendExseption(ex);
                return null;
            }
        }
        #endregion

        public void SetProxySpeed(string address, int time)
        {
            Core(z =>
            {
                var prox = proxyes.Find(x => x.proxy.address == address);
                if (prox != null)
                    prox.speed = time;
                return null;
            }, null);
        }

        #region ChangeBannedProxy
        public ProxyInfo ChangeBannedProxy(string address, int requests, bool unbanned, bool fast)
        {
            needSave = true;
            try
            {
                return (ProxyInfo)Core(z=>
                {
                    int ind = proxyes.FindIndex(x => x.proxy.address == address);
                    if (ind != -1)
                    {
                        if (proxyes[ind].reqBeforeBan != 0)
                            proxyes[ind].reqsBeforeBan.Add(proxyes[ind].reqBeforeBan + requests);
                        proxyes[ind].alreadyUsing = false;
                        proxyes[ind].reqBeforeBan = 0;
                        proxyes[ind].isBanned = !unbanned;
                        proxyes[ind].lastUsingTime = DateTime.Now;
                    }
                    if (fast)
                        return GetBestAndFastProxy();
                    else
                        return GetBestProxy();
                }, null);
            }
            catch (Exception ex)
            {
                SendExseption(ex);
                return null;
            }
        }
        #endregion

        #region GetUsingProxyCount
        public int GetBadProxyCount()
        {
            int prxBad = 0;
            for (var i = 0; i < proxyes.Count; i++)
            {
                if (proxyes[i].badProxy)
                    prxBad++;
            }
            return prxBad;
        }
        #endregion

        #region UnbannedProxy
        public void UnbannedProxy(string address)
        {
            needSave = true;
            try
            {
                Core(Unbanned_Proxy, address);
            }
            catch (Exception ex)
            {
                SendExseption(ex);
                return;
            }
        }

        object Unbanned_Proxy(object param)
        {
            string address = (string)param;
            int ind = proxyes.FindIndex(x => x.proxy.address == address);
            if (ind != -1)
            {
                proxyes[ind].isBanned = false;
                proxyes[ind].lastUsingTime = DateTime.Now;
            }
            return null;
        }
        #endregion

        #region ReleaseProxy
        public void ReleaseProxy(string address)
        {
            needSave = true;
            try
            {
                Core(Release_Proxy, address);
            }
            catch (Exception ex)
            {
                SendExseption(ex);
                return;
            }
        }

        object Release_Proxy(object param)
        {
            string address = (string)param;
            int ind = proxyes.FindIndex(x => x.proxy.address == address);
            if (ind != -1)
            {
                proxyes[ind].alreadyUsing = false;
            }
            return null;
        }
        #endregion

        #region ChangeBadProxy
        public ProxyInfo ChangeBadProxy(string address, bool fast)
        {
            needSave = true;
            try
            {
                return (ProxyInfo)Core(z=>
                {
                    int ind = proxyes.FindIndex(x => x.proxy.address == address);
                    if (ind != -1)
                    {
                        proxyes[ind].badProxyCount++;
                        proxyes[ind].alreadyUsing = false;
                        proxyes[ind].badProxy = true;
                        proxyes[ind].lastUsingTime = DateTime.Now;
                    }
                    if (fast)
                        return GetBestAndFastProxy();
                    else
                        return GetBestProxy();
                }, null);
            }
            catch (Exception ex)
            {
                SendExseption(ex);
                return null;
            }
        }
        #endregion

        public int GetProxyCount()
        {
            return proxyes.Count;
        }

        public void Dispose()
        {
            SaveProxyesData();
        }
    }
}
