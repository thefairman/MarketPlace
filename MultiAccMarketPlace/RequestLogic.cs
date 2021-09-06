using xNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using HtmlAgilityPack;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;

namespace LDmp
{
    public enum ReqPrioritity { Fast, Medium, Usual }
    public enum MarketStatPeriod { day, month }
    public class ReqCounter
    {
        List<DateTime> listReqs = new List<DateTime>();
        int maxInMinute;
        int maxInSecond;
        public int AvgNeeded { get; private set; }

        public ReqCounter(int maxInMinute, int maxInSecond)
        {
            this.maxInMinute = maxInMinute;
            this.maxInSecond = maxInSecond;
            AvgNeeded = 60000 / maxInMinute + 1;
        }

        public void Add()
        {
            listReqs.Add(DateTime.UtcNow);
        }

        public int ReqsInSecondLimit()
        {
            return listReqs.FindAll(x => x >= DateTime.UtcNow.AddSeconds(-1)).Count;
        }

        public int ReqsInMinuteLimit()
        {
            listReqs.RemoveAll(x => x < DateTime.UtcNow.AddMinutes(-1));
            return listReqs.Count;
        }

        public int GetAwaitingTimeUsual()
        {
            if (maxInMinute <= 0)
                return 0;
            ReqsInMinuteLimit();
            if (listReqs.Count == 0 || (DateTime.UtcNow - listReqs.Last()).TotalMilliseconds > AvgNeeded)
                return 0;
            if (listReqs.Count < 2) //WTF ?!
                return AvgNeeded;
            double sum = 0;
            for (int i = 1; i < listReqs.Count; i++)
            {
                sum += (listReqs[i] - listReqs[i - 1]).TotalMilliseconds;
            }
            double a = sum / listReqs.Count;
            double b = AvgNeeded / a;
            double reVal = b * listReqs.Count;
            if (reVal < AvgNeeded) reVal = AvgNeeded;
            return (int)((reVal - (long)reVal > 0.01) ? reVal + 1 : reVal);
        }

        public int GetAwaitingTimeImmediately()
        {
            if (maxInSecond <= 0)
                return 0;
            if (ReqsInSecondLimit() < maxInSecond)
                return 0;
            //return 1000 - (int)(DateTime.UtcNow - listReqs[listReqs.Count - 5]).TotalMilliseconds + 1;
            int awaitingForSec = 1000 - (int)(DateTime.UtcNow - listReqs[listReqs.Count - maxInSecond]).TotalMilliseconds + 1;
            if (maxInMinute <= 0)
                return awaitingForSec;
            if (ReqsInMinuteLimit() >= maxInMinute)
            {
                int awaitingForMin = 60000 - (int)(DateTime.UtcNow - listReqs[listReqs.Count - maxInMinute]).TotalMilliseconds + 1;
                return awaitingForSec > awaitingForMin ? awaitingForSec : awaitingForMin;
            }
            return awaitingForSec;
        }

        public int GetAwaitingTimeSlow()
        {
            if (listReqs.Count > 0)
            {
                int awaitingForSec = AvgNeeded * 2 - (int)(DateTime.UtcNow - listReqs.Last()).TotalMilliseconds;
                return awaitingForSec > 0 ? awaitingForSec : 0;
            }
            return AvgNeeded * 2;
        }
    }
   
    public class TimesRequest
    {
        public int RequestTimeExecute { get; set; }
        public DateTime StartRequestTime { get; set; }
        public DateTime EndRequestTime { get; set; }
        public DateTime StartAndHalfOfRequestTime { get; set; }
        public string IP { get; set; }
    }
    public class RequestLogic : IDisposable
    {
        bool usingProxy;
        bool fast;
        public string Login { get; private set; } = "";
        private string pass = "";
        readonly HttpRequest request = new HttpRequest();
        ProxyInfo proxyInfo = null;
        AccountManager accountsManager = null;
        ReqCounter reqCounter = new ReqCounter(Global.settings.MaxReqsInMinute, 1);
        //public ConcurrentQueue<TimesRequest> TimesRequests { get; private set; } = new ConcurrentQueue<TimesRequest>();

        public RequestLogic(bool usingProxyToUser, string login, string pass, bool fast)
        {
            usingProxy = usingProxyToUser;
            this.Login = login;
            this.pass = pass;
            this.fast = fast;

            SetRequestSettings();
            if (usingProxyToUser)
                proxyInfo = Global.proxyLogic.GetProxy(fast);
        }
        public RequestLogic(bool usingProxyToUser, bool fast)
        {
            usingProxy = usingProxyToUser;
            this.fast = fast;
            //cookiesString = cookiesStr;
            SetRequestSettings();
            isAutorizedOnMailRu = true;
            if (usingProxyToUser)
                proxyInfo = Global.proxyLogic.GetProxy(fast);
        }
        //public RequestLogic(bool usingProxyToUser, string login, string pass, string cookiesStr, SharedCookies sharedCookies, bool initiatedCopy)
        //{
        //    usingProxy = usingProxyToUser;
        //    this.Login = login;
        //    this.pass = pass;
        //    cookiesString = cookiesStr;
        //    IsAutorizedOnLootDog = initiatedCopy;
        //    this.sharedCookies = sharedCookies;
        //    SetRequestSettings();
        //    if (usingProxyToUser)
        //        proxyInfo = Global.proxyLogic.GetProxy();

        //}
        public bool AccIsSet { get; private set; } = false;
        int numOfAcc = 0;
        public void SetAccounts(AccountManager accounts)
        {
            AccIsSet = true;
            accountsManager = accounts;
            FillCookies(accountsManager?.GetCookiesFromAcc(Login, out numOfAcc));
        }
        
        //public RequestLogic(bool usingProxyToUser, string cookiesStr, SharedCookies sharedCookies, bool initiatedCopy)
        //{
        //    usingProxy = usingProxyToUser;
        //    cookiesString = cookiesStr;
        //    this.sharedCookies = sharedCookies;
        //    SetRequestSettings();
        //    IsAutorizedOnLootDog = initiatedCopy;
        //    if (usingProxyToUser)
        //        proxyInfo = Global.proxyLogic.GetProxy();
        //}

        void FillCookies(string cookiesString)
        {
            request.Cookies = FromStringToCookieDictionary(cookiesString);
        }

        public static string FromCookieDictionaryToString(CookieDictionary cookies)
        {
            if (cookies == null)
                return "";
            string coocks = "";
            foreach (var item in cookies)
            {
                coocks += item.Key + "=" + item.Value + "; ";
            }
            return coocks;
        }

        public static CookieDictionary FromStringToCookieDictionary(string cookies)
        {
            CookieDictionary cd = new CookieDictionary();
            if (cookies != null)
            {
                string[] cooks = cookies.Trim().Split(new char[] { ';' });
                foreach (var item in cooks)
                {
                    string[] cook = item.Trim().Split(new char[] { '=' });
                    if (cook.Length > 1)
                        cd.Add(cook[0].Trim(), cook[1].Trim());
                }
            }
            return cd;
        }

        void SetRequestSettings()
        {
            request.AllowAutoRedirect = true;
            if (String.IsNullOrEmpty(Login))
                request.UserAgent = Http.ChromeUserAgent();
            else
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/73.0.3683.103 Safari/537.36";
            request.ConnectTimeout = 5000;
            //request.KeepAliveTimeout = 5000;
            request.ReadWriteTimeout = 5000;
            request.KeepAliveTimeout = 2 * 60 * 1000;
            //request.MaximumKeepAliveRequests = 1000;
            request.EnableEncodingContent = true;
            request.Reconnect = true;
        }

        bool isAutorizedOnMailRu = false;

        string AutorizOnMailRu()
        {
            if (isAutorizedOnMailRu) return null;
            string retStr = null;

            request.ConnectTimeout = 60000;
            request.KeepAliveTimeout = 60000;
            request.ReadWriteTimeout = 60000;
            request.AllowAutoRedirect = false;
            while (true)
            {
                try
                {
                    if (request.Cookies.Count == 0)
                    {
                        ReqData reqData = new ReqData()
                        {
                            post = false,
                            url = @"https://m.mail.ru/login",
                            referer = "",
                            headers = new Dictionary<string, string>()
                            {
                                ["Upgrade-Insecure-Requests"] = "1",
                                ["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8",
                                ["Accept-Language"] = "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7"
                            },
                            MaxTimeOut = 60000
                        };

                        string content = MakeRequest(reqData).Value;

                        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                        doc.LoadHtml(content);
                        HtmlNode bodyNode = doc.DocumentNode.SelectSingleNode("//form[@id='authform']");
                        if (bodyNode != null)
                        {
                            string act = bodyNode.Attributes["action"]?.Value;
                            if (act == null)
                            {
                                Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage($"{Login}: Ошибка авторизации"));
                                break;
                            }
                            reqData = new ReqData()
                            {
                                post = true,
                                url = act,
                                referer = @"https://m.mail.ru/login",
                                headers = new Dictionary<string, string>()
                                {
                                    ["Cache-Control"] = "max-age=0",
                                    ["Origin"] = @"https://m.mail.ru",
                                    ["Upgrade-Insecure-Requests"] = "1",
                                    ["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8",
                                    ["Accept-Language"] = "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7"
                                },
                                reqParams = new RequestParams()
                                {
                                     new KeyValuePair<string, string>("mhost", "m.mail.ru"),
                                     new KeyValuePair<string, string>("act_token", request.Cookies["act"]),
                                     new KeyValuePair<string, string>("Login", Login),
                                     new KeyValuePair<string, string>("Domain", "mail.ru"),
                                     new KeyValuePair<string, string>("Password", pass)
                                },
                                MaxTimeOut = 60000
                            };
                            MakeRequest(reqData);
                            if (!request.Cookies.ContainsKey("t"))
                            {
                                Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage($"{Login}: Ошибка авторизации"));
                                break;
                            }
                            ////
                            // Saving autorization
                            ////
                            retStr = FromCookieDictionaryToString(request.Cookies);
                        }
                        else
                        {
                            request.Cookies.Clear();
                            continue;
                        }
                    }
                    else
                    {
                        /// Checking autorization
                        /// 
                        ReqData reqData = new ReqData()
                        {
                            post = false,
                            url = @"https://wf.mail.ru/dynamic/user/check_data.php?do=auth",
                            referer = @"https://wf.mail.ru/",
                            headers = new Dictionary<string, string>()
                            {
                                ["DNT"] = "1",
                                ["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8",
                                ["Accept-Language"] = "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7"
                            },
                            MaxTimeOut = 60000
                        };

                        string content = MakeRequest(reqData).Value;
                        if (!content.Contains("created"))
                        {
                            request.Cookies.Clear();
                            continue;
                        }
                    }

                    isAutorizedOnMailRu = true;
                    Global.UIdata.MessagesUI.AddRecordAsync(new UIMessage($"{Login}: успешная авторизация на mail.ru"));
                    break;
                }
                catch (Exception ex)
                {
                    Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage($"{Login}: {ex.Message}"));
                }
            }
            SetRequestSettings();
            return retStr;
        }

        bool SetUserInfo()
        {
            return (bool)Core(x => 
            {
                string content = MakeRequest(new ReqData()
                {
                    post = false,
                    referer = @"https://lootdog.io/",
                    url = @"https://lootdog.io/api/current_user/?format=json",
                    headers = new Dictionary<string, string>() { ["Accept"] = "*/*", ["Accept-Language"] = "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7" }
                }).Value;
                if (content.Contains("NotAuthenticated"))
                    return false;
                UserInfoAnswer userInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<UserInfoAnswer>(content);
                WorkingRequestsManager.UserID = userInfo.id;
                return true;
            }, null);
        }

        public bool IsAutorizedOnLootDog { get; private set; } = false;
        public void AutorizOnLootDog(bool delCurrentCookies = false)
        {
            if (IsAutorizedOnLootDog && !delCurrentCookies) return;

            string retStr = null;
            if (delCurrentCookies)
            {
                request.Cookies.Clear();
                isAutorizedOnMailRu = false;
            }
            request.ConnectTimeout = 60000;
            request.KeepAliveTimeout = 60000;
            request.ReadWriteTimeout = 60000;
            request.KeepAlive = true;
            request.AllowAutoRedirect = true;
            while (true)
            {
                try
                {
                    if (!request.Cookies.ContainsKey("csrftoken") || !request.Cookies.ContainsKey("sessionid"))
                    {
                        if (!isAutorizedOnMailRu)
                        {
                            AutorizOnMailRu();
                            if (!isAutorizedOnMailRu)
                                break;
                        }

                        //string content = MakeRequest(new ReqData()
                        //{
                        //    post = false,
                        //    skipIPBlockChecking = true,
                        //    referer = @"https://lootdog.io/",
                        //    url = @"https://lootdog.io/social/login/o2mailru/?next=%2F",
                        //    headers = new Dictionary<string, string>()
                        //    {
                        //        ["Upgrade-Insecure-Requests"] = "1",
                        //        ["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3",
                        //        ["Accept-Language"] = "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7"
                        //    }
                        //}).Value;

                        int prevVal = request.MaximumAutomaticRedirections;
                        request.MaximumAutomaticRedirections = 100;
                        string content = MakeRequest(new ReqData()
                        {
                            post = false,
                            skipIPBlockChecking = true,
                            referer = @"https://account.my.games/oauth2/login/?continue=https%3A%2F%2Faccount.my.games%2Foauth2%2F%3Fredirect_uri%3Dhttps%253A%252F%252Flootdog.io%252Fsocial%252Fcomplete%252Fo2mygames%252F%26client_id%3Dlootdog_io%26response_type%3Dcode%26signup_social%3Dmailru%2Cfb%2Cok%2Cvk%2Cg%2Ctwitch%2Ctw%26signup_method%3Demail%252Cphone%26lang%3Dru_RU&client_id=lootdog_io&lang=ru_RU&signup_method=email%2Cphone&signup_social=mailru%2Cfb%2Cok%2Cvk%2Cg%2Ctwitch%2Ctw",
                            url = @"https://auth-ac.my.games/social/mailru?display=popup&continue=https%3A%2F%2Faccount.my.games%2Fsocial_back%2F%3Fcontinue%3Dhttps%253A%252F%252Faccount.my.games%252Foauth2%252F%253Fredirect_uri%253Dhttps%25253A%25252F%25252Flootdog.io%25252Fsocial%25252Fcomplete%25252Fo2mygames%25252F%2526client_id%253Dlootdog_io%2526response_type%253Dcode%2526signup_social%253Dmailru%252Cfb%252Cok%252Cvk%252Cg%252Ctwitch%252Ctw%2526signup_method%253Demail%25252Cphone%2526lang%253Dru_RU%26client_id%3Dlootdog_io%26popup%3D1&failure=https%3A%2F%2Faccount.my.games%2Fsocial_back%2F%3Fsoc_error%3D1%26continue%3Dhttps%253A%252F%252Faccount.my.games%252Foauth2%252Flogin%252F%253Fcontinue%253Dhttps%25253A%25252F%25252Faccount.my.games%25252Foauth2%25252Flogin%25252F%25253Fcontinue%25253Dhttps%2525253A%2525252F%2525252Faccount.my.games%2525252Foauth2%2525252F%2525253Fredirect_uri%2525253Dhttps%252525253A%252525252F%252525252Flootdog.io%252525252Fsocial%252525252Fcomplete%252525252Fo2mygames%252525252F%25252526client_id%2525253Dlootdog_io%25252526response_type%2525253Dcode%25252526signup_social%2525253Dmailru%2525252Cfb%2525252Cok%2525252Cvk%2525252Cg%2525252Ctwitch%2525252Ctw%25252526signup_method%2525253Demail%252525252Cphone%25252526lang%2525253Dru_RU%252526client_id%25253Dlootdog_io%252526lang%25253Dru_RU%252526signup_method%25253Demail%2525252Cphone%252526signup_social%25253Dmailru%2525252Cfb%2525252Cok%2525252Cvk%2525252Cg%2525252Ctwitch%2525252Ctw%2526amp%253Bclient_id%253Dlootdog_io%2526amp%253Blang%253Dru_RU%2526amp%253Bsignup_method%253Demail%25252Cphone%2526amp%253Bsignup_social%253Dmailru%25252Cfb%25252Cok%25252Cvk%25252Cg%25252Ctwitch%25252Ctw",
                            headers = new Dictionary<string, string>()
                            {
                                ["Upgrade-Insecure-Requests"] = "1",
                                ["Sec-Fetch-Site"] = "same-site",
                                ["Sec-Fetch-Mode"] = "navigate",
                                ["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3",
                                ["Accept-Language"] = "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7"
                            }
                        }).Value;

                        content = MakeRequest(new ReqData()
                        {
                            post = true,
                            skipIPBlockChecking = true,
                            referer = request.Address.AbsoluteUri,
                            url = @"https://o2.mail.ru/login",
                            headers = new Dictionary<string, string>()
                            {
                                ["Origin"] = @"https://o2.mail.ru",
                                ["Upgrade-Insecure-Requests"] = "1",
                                ["Sec-Fetch-Site"] = "same-site",
                                ["Sec-Fetch-Mode"] = "navigate",
                                ["Sec-Fetch-User"] = "?1",
                                ["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3",
                                ["Accept-Language"] = "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7"
                            },
                            reqParams = new RequestParams() {
                                new KeyValuePair<string, string>("o2csrf", request.Cookies["o2csrf"]),
                                new KeyValuePair<string, string>("mode", "default"),
                                new KeyValuePair<string, string>("browser_data", "{\"screen\":{\"availWidth\":\"1366\",\"availHeight\":\"728\",\"width\":\"1366\",\"height\":\"768\",\"colorDepth\":\"24\",\"pixelDepth\":\"24\",\"availLeft\":\"0\",\"availTop\":\"0\"},\"navigator\":{\"vendorSub\":\"\",\"productSub\":\"20030107\",\"vendor\":\"Google Inc.\",\"maxTouchPoints\":\"0\",\"hardwareConcurrency\":\"4\",\"cookieEnabled\":\"true\",\"appCodeName\":\"Mozilla\",\"appName\":\"Netscape\",\"appVersion\":\"5.0 (Windows NT 6.1; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 74.0.3729.131 Safari / 537.36\",\"platform\":\"Win32\",\"product\":\"Gecko\",\"userAgent\":\"Mozilla / 5.0(Windows NT 6.1; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 74.0.3729.131 Safari / 537.36\",\"language\":\"ru - RU\",\"onLine\":\"true\",\"doNotTrack\":\"inaccessible\",\"deviceMemory\":\"8\"},\"flash\":{\"version\":\"inaccessible\"}}"),
                                new KeyValuePair<string, string>("login", Login),
                                new KeyValuePair<string, string>("lang", "ru-RU")
                            }
                        }).Value;
                        request.MaximumAutomaticRedirections = prevVal;
                        //var unixTimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                        content = MakeRequest(new ReqData()
                        {
                            post = false,
                            skipIPBlockChecking = true,
                            referer = @"https://account.my.games/oauth2/login/?continue=https://account.my.games/oauth2/?redirect_uri=https://lootdog.io/social/complete/o2mygames/&client_id=lootdog_io&response_type=code&signup_social=mailru,fb,ok,vk,g,twitch,tw&signup_method=email,phone&lang=ru_RU&client_id=lootdog_io&lang=ru_RU&signup_method=email,phone&signup_social=mailru,fb,ok,vk,g,twitch,tw",
                            url = @"https://account.my.games/oauth2/?redirect_uri=https://lootdog.io/social/complete/o2mygames/&client_id=lootdog_io&response_type=code&signup_social=mailru,fb,ok,vk,g,twitch,tw&signup_method=email,phone&lang=ru_RU",
                            headers = new Dictionary<string, string>()
                            {
                                ["Upgrade-Insecure-Requests"] = "1",
                                ["Sec-Fetch-Site"] = "same-site",
                                ["Sec-Fetch-Mode"] = "navigate",
                                ["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3",
                                ["Accept-Language"] = "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7"
                            }
                        }).Value;

                        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                        doc.LoadHtml(content);
                        HtmlNode bodyNode = doc.DocumentNode.SelectSingleNode("//input[@name='hash']");
                        if (bodyNode != null)
                        {
                            string hashForm = bodyNode.Attributes["value"]?.Value;
                            if (String.IsNullOrEmpty(hashForm))
                            {
                                Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage($"{Login}: Ошибка авторизации [empty hashForm]"));
                                break;
                            }

                            bodyNode = doc.DocumentNode.SelectSingleNode("//input[@name='csrfmiddlewaretoken']");
                            string csrfmiddlewaretoken = bodyNode?.Attributes["value"]?.Value;
                            if (String.IsNullOrEmpty(csrfmiddlewaretoken))
                            {
                                Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage($"{Login}: Ошибка авторизации [empty csrfmiddlewaretoken]"));
                                break;
                            }

                            content = MakeRequest(new ReqData()
                            {
                                post = true,
                                skipIPBlockChecking = true,
                                referer = "https://account.my.games/oauth2/?redirect_uri=https://lootdog.io/social/complete/o2mygames/&amp;client_id=lootdog_io&amp;response_type=code&amp;signup_social=mailru,fb,ok,vk,g,twitch,tw&amp;signup_method=email,phone&amp;lang=ru_RU",
                                url = @"https://account.my.games/oauth2/",
                                headers = new Dictionary<string, string>()
                                {
                                    ["Origin"] = @"https://account.my.games",
                                    ["Upgrade-Insecure-Requests"] = "1",
                                    ["Sec-Fetch-Site"] = "same-site",
                                    ["Sec-Fetch-Mode"] = "navigate",
                                    ["Sec-Fetch-User"] = "?1",
                                    ["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3",
                                    ["Accept-Language"] = "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7"
                                },
                                reqParams = new RequestParams() {
                                new KeyValuePair<string, string>("csrfmiddlewaretoken", csrfmiddlewaretoken),
                                new KeyValuePair<string, string>("response_type", "code"),
                                new KeyValuePair<string, string>("client_id", "lootdog_io"),
                                new KeyValuePair<string, string>("redirect_uri", @"https://lootdog.io/social/complete/o2mygames/"),
                                new KeyValuePair<string, string>("hash", hashForm)
                            }
                            }).Value;
                        }
                        else
                        {
                            Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage($"{Login}: Ошибка авторизации [empty form whitData]"));
                            break;
                        }

                        if (SetUserInfo())
                            retStr = FromCookieDictionaryToString(request.Cookies);
                        else
                        {
                            Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage($"{Login}: Ошибка авторизации"));
                            break;
                        }
                    }
                    else // check for autorize is active
                    {
                        /// Checking autorization
                        /// 
                        if (!SetUserInfo())
                        {
                            request.Cookies.Remove("csrftoken");
                            request.Cookies.Remove("sessionid");
                            continue;
                        }
                    }

                    IsAutorizedOnLootDog = true;
                    Global.UIdata.MessagesUI.AddRecordAsync(new UIMessage($"{Login}: успешная авторизация на lootdog.io"));
                    break;
                }
                catch (Exception ex)
                {
                    Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage($"{Login}: {ex.Message}"));
                }
            }
            SetRequestSettings();
            if (!String.IsNullOrEmpty(retStr))
                accountsManager?.SetCookiesToAcc(Login, numOfAcc, retStr);
            return;
        }

        void SetProxyToReq(HttpRequest req, ProxyInfo proxyInfo)
        {
            if (proxyInfo == null)
                return;
            string prxstr = $"{proxyInfo.address}:{proxyInfo.port}";
            if (proxyInfo.login != "" && proxyInfo.pass != "")
                prxstr += ":" + proxyInfo.login + ":" + proxyInfo.pass;

            req.Proxy = ProxyClient.Parse(ProxyType.Http, prxstr);

            req.Proxy.ConnectTimeout = Global.settings.TimeOutProxySec * 1000;
            req.Proxy.ReadWriteTimeout = Global.settings.TimeOutProxySec * 1000;
        }

        int GetBalance()
        {
            Global.UIdata.MessagesUI.AddRecordAsync(new UIMessage("Rukaptcha # Getting Balance"));
            string bbalance = "?";

            try
            {
                bbalance = Rucaptcha.Balance();
            }
            catch (Exception ex) { Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage(ex.Message)); return 3; }

            Global.UIdata.MessagesUI.AddRecordAsync(new UIMessage("Rukaptcha # Balance: " + bbalance));

            try
            {
                double blnc = Convert.ToDouble(bbalance.Replace('.', ','));
                Global.lastRuCaptchaBalance = blnc;
                if (blnc < 0.1)
                {
                    return 1;
                }
            }
            catch (Exception ex) { Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage(ex.Message)); return 3; }

            return 0;
        }

        bool UnBlockedIp(ProxyInfo proxyInfo)
        {
            Global.UIdata.MessagesUI.AddRecordAsync(new UIMessage($"UnBlockedIp IP: {proxyInfo.address} # Start to unblocking"));
            ///string ip = await GetCurIp();
            using (HttpRequest req = new HttpRequest())
            {
                req.AllowAutoRedirect = true;
                req.UserAgent = Http.ChromeUserAgent();
                req.Cookies = new CookieDictionary();
                req.CharacterSet = Encoding.GetEncoding(65001);
                req.KeepAlive = true;
                req.ConnectTimeout = 2000;

                SetProxyToReq(req, proxyInfo);

                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        HttpResponse resp = null;

                        resp = req.Get(@"https://c.mail.ru/0");

                        if (resp == null)
                        {
                            //MessageBox.Show("Ошибка получения запроса");
                            throw new Exception("error getting image");
                        }
                        byte[] imageByte = resp.ToBytes();
                        using (MemoryStream ms = new MemoryStream(imageByte, 0, imageByte.Length))
                        {
                            ms.Write(imageByte, 0, imageByte.Length);
                            Bitmap bmp = new Bitmap(Image.FromStream(ms, true));

                            string imgDirPath = Environment.CurrentDirectory + "\\img\\";
                            if (!Directory.Exists(imgDirPath))
                                Directory.CreateDirectory(imgDirPath);

                            Random rnd = new Random();
                            string imgPath = imgDirPath + "v" + (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds
                                + "_" + rnd.Next(0, 10000) + ".jpg";
                            bmp.Save(imgPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                            bmp.Dispose();
                            string cptText = "";

                            if (GetBalance() == 1)
                                throw (new Exception("Low balance!"));

                            Global.UIdata.MessagesUI.AddRecordAsync(new UIMessage($"UnBlockedIp IP: {proxyInfo.address} # waiting for blocked Captcha answer..."));

                            try
                            {
                                cptText = Rucaptcha.Recognize(imgPath);
                            }
                            catch (Exception ex)
                            {
                                Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage(ex.Message));
                            }

                            if (!cptText.ToLower().Contains("error") && cptText != "")
                            {
                                Global.UIdata.MessagesUI.AddRecordAsync(new UIMessage($"UnBlockedIp IP: {proxyInfo.address} # entering blocked captcha..."));

                                req.Get(@"https://wf.mail.ru/validate/process.php?captcha_input=" + cptText);

                                resp = req.Get(@"https://wf.mail.ru/dynamic/auth/?profile_reload=0");

                                string respstr = resp.ToString();

                                if (!respstr.ToLower().Contains("validate"))
                                {
                                    Global.UIdata.MessagesUI.AddRecordAsync(new UIMessage($"UnBlockedIp IP: {proxyInfo.address} # unblocked success!"));
                                    return true;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage(ex.Message));
                    }
                }
            }

            return false;
        }

        bool CheckIsBlockedString(string content)
        {
            return content.Contains("captcha_input") || content.Contains("redirect");
        }

        bool CheckContentForErrors(string content, out ErrorAnswer errorAnswer)
        {
            errorAnswer = null;
            if (String.IsNullOrEmpty(content))
                return false;
            if (!content.Contains("площадке проводятся технические работы"))
            {
                try
                {
                    errorAnswer = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorAnswer>(content);
                }
                catch (Exception)
                {
                    return false;
                }
                if (errorAnswer == null || errorAnswer.code == null || errorAnswer.detail == null)
                    return false;
                if (errorAnswer.code == "NotAuthenticated")
                {
                    RegionIsAllowedAndSetCSRF();
                    if (accountsManager != null)
                        AutorizOnLootDog(true);
                }
            }
            else
            {
                errorAnswer = new ErrorAnswer() { code = "технические работы", detail = "технические работы" };
                blockedIP = true;
            }
            if (errorAnswer.code != "технические работы")
            {
                Logman.Log(content);
                Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage($"{errorAnswer.detail} | code: {errorAnswer.code}"));
            }
            return true;
        }

        object lockCore = new object();
        object Core(Func<object, object> someAction, object param)
        {
            lock (lockCore)
            {
                return someAction(param);
            }
        }

        string GetCurIp()
        {
            return proxyInfo == null ? "main IP" : proxyInfo.address;
        }

        private const int maxBadProxyCount = 5;
        int requestsOnceIP = 0;
        int numOfBadPoxy = 0;
        bool blockedIP = false;
        class ReqData
        {
            public bool post = false;
            public string url;
            public RequestParams reqParams = new RequestParams();
            public Dictionary<string, string> headers = new Dictionary<string, string>();
            public string referer = null;
            public bool skipIPBlockChecking = false;
            public int MaxTimeOut = 10000;
            public bool deleteMethod = false;
            public bool multipartForm = false;
        }

        void SetProxyAndResetReqsInfo()
        {
            usingProxy = true;
            proxyInfo = Global.proxyLogic.GetProxy(fast);
            requestsOnceIP = 0;
            numOfBadPoxy = 0;
            blockedIP = false;
        }

        DateTime serverDate = DateTime.MinValue;
        KeyValuePair<TimesRequest, string> MakeRequest(ReqData reqData, ReqPrioritity prior = ReqPrioritity.Usual)
        {
            DateTime timeBeforeReq = DateTime.Now;
            while (true)
            {
                try
                {
                    //int slp;
                    //if (priority == ReqPriority.NORMAL)
                    //{
                    //    slp = reqCounter.GetAwaitingTimeUsual();
                    //    if (slp > 0) Thread.Sleep(slp);
                    //}
                    //else if (priority == ReqPriority.SLOW)
                    //{
                    //    slp = reqCounter.GetAwaitingTimeSlow();
                    //    if (slp > 0) Thread.Sleep(slp);
                    //}

                    int slp = reqCounter.GetAwaitingTimeUsual();
                    if (prior != ReqPrioritity.Fast && slp > 0) Thread.Sleep(prior == ReqPrioritity.Usual ? slp : slp / 2);

                    if (usingProxy)
                    {
                        if ((Global.settings.ReqMax != 0 && requestsOnceIP >= Global.settings.ReqMax) || numOfBadPoxy >= maxBadProxyCount || blockedIP)
                        {
                            if (blockedIP)
                            {
                                //ProxyInfo prxUnBlocked = (ProxyInfo)proxyInfo.Clone();
                                proxyInfo = Global.proxyLogic.ChangeBannedProxy(proxyInfo.address, requestsOnceIP, false, fast);
                                //Task.Factory.StartNew(() => 
                                //{
                                //    ProxyInfo prxUnBlocked1 = prxUnBlocked;
                                //    if (UnBlockedIp(prxUnBlocked1))
                                //        Global.proxyLogic.UnbannedProxy(prxUnBlocked1.address);
                                //});
                            }
                            else
                            {
                                if (Global.settings.ReqMax != 0 && requestsOnceIP >= Global.settings.ReqMax)
                                    proxyInfo = Global.proxyLogic.ChangeProxy(proxyInfo.address, requestsOnceIP, fast);
                                else
                                    proxyInfo = Global.proxyLogic.ChangeBadProxy(proxyInfo.address, fast);
                            }
                            requestsOnceIP = 0;
                            numOfBadPoxy = 0;
                            blockedIP = false;
                            SetProxyToReq(request, proxyInfo);
                        }
                        if (request.Proxy == null)
                            SetProxyToReq(request, proxyInfo);
                    }
                    else
                    {
                        if (blockedIP && Global.settings.SwitchToProxyIfBanned)
                        {
                            SetProxyAndResetReqsInfo();
                            continue;
                        }
                        if (WinInet.IEProxyEnable)
                        {
                            string strProxy = WinInet.GetIEProxy();
                            if (!String.IsNullOrEmpty(strProxy))
                            {
                                string searHttps = "https=";
                                int indPrxStart = strProxy.IndexOf(searHttps);
                                if (indPrxStart >= 0)
                                {
                                    indPrxStart += searHttps.Length;
                                    int indPrxEnd = strProxy.IndexOf(";", indPrxStart);
                                    string prxstr = null;
                                    if (indPrxEnd > 0)
                                        prxstr = strProxy.Substring(indPrxStart, indPrxEnd - indPrxStart);
                                    else
                                        prxstr = strProxy.Substring(indPrxStart);
                                    if (!String.IsNullOrEmpty(prxstr))
                                        request.Proxy = ProxyClient.Parse(ProxyType.Http, prxstr);
                                }
                            }
                        }
                        else
                            request.Proxy = null;
                    }


                    requestsOnceIP++;
                    timeBeforeReq = DateTime.Now;

                    if (reqData.headers.Count > 0)
                    {
                        string agent = request.UserAgent;
                        request.ClearAllHeaders();
                        foreach (var item in reqData.headers)
                        {
                            request.AddHeader(item.Key, item.Value);
                        }
                        request.UserAgent = agent;
                    }
                    else
                    {
                        request.AddHeader("Accept", "text/html, application/xhtml+xml, */*");
                        request.AddHeader("X-Requested-With", "XMLHttpRequest");
                        request.AddHeader("Accept-Language", "ru-RU");
                        request.AddHeader("DNT", "1");
                        request.AddHeader("Cache-Control", "no-cache");
                    }
                    // request block
                    
                    request.IgnoreProtocolErrors = true;
                    request.Referer = reqData.referer;

                    int timeOutTime = reqData.MaxTimeOut > 0 ? reqData.MaxTimeOut : 10000;

                    request.ConnectTimeout = timeOutTime;
                    request.KeepAliveTimeout = timeOutTime;
                    request.ReadWriteTimeout = timeOutTime;
                    request.CharacterSet = Encoding.UTF8;

                    HttpResponse resp;
                    reqCounter.Add();
                    if (reqData.deleteMethod)
                    {
                        var multipartContent = new MultipartContent("----WebKitFormBoundary" + MultipartContent.GetRandomString(16));
                        foreach (var item in reqData.reqParams)
                        {
                            if (item.Value != null && !String.IsNullOrEmpty(item.Key))
                                multipartContent.Add(new StringContent(item.Value), item.Key);
                        }
                        resp = request.Raw(HttpMethod.DELETE, reqData.url, multipartContent);
                    }
                    else if (reqData.post)
                    {
                        if (reqData.multipartForm)
                        {
                            var multipartContent = new MultipartContent("----WebKitFormBoundary" + MultipartContent.GetRandomString(16));
                            foreach (var item in reqData.reqParams)
                            {
                                if (item.Value != null && !String.IsNullOrEmpty(item.Key))
                                    multipartContent.Add(new StringContent(item.Value), item.Key);
                            }
                            resp = request.Post(new Uri(reqData.url), multipartContent);
                        }
                        else if (reqData.reqParams.Count > 0)
                            resp = request.Post(new Uri(reqData.url), reqData.reqParams);
                        else
                            resp = request.Post(new Uri(reqData.url));
                    }
                    else
                    {
                        if (reqData.reqParams.Count > 0)
                            resp = request.Get(new Uri(reqData.url), reqData.reqParams);
                        else
                            resp = request.Get(new Uri(reqData.url));
                    }
                    
                    string content = resp.ToString();

                    if (!reqData.skipIPBlockChecking && (resp.StatusCode == HttpStatusCode.ServiceUnavailable
                        || CheckIsBlockedString(content)))
                    {
                        Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage($"blocked IP: {GetCurIp()}"));
                        blockedIP = true;
                        continue;
                    }
                    DateTime endReq = DateTime.Now;
                    int timeExec = (int)Math.Round((endReq - timeBeforeReq).TotalMilliseconds, 0, MidpointRounding.AwayFromZero);
                    var tr = new TimesRequest()
                    {
                        StartRequestTime = timeBeforeReq,
                        EndRequestTime = endReq,
                        RequestTimeExecute = timeExec,
                        StartAndHalfOfRequestTime = timeBeforeReq.AddMilliseconds(timeExec / 2d),
                        IP = GetCurIp()
                    };
                    System.Drawing.Color? color = null;
                    if (!String.IsNullOrEmpty(Login))
                        color = System.Drawing.Color.LightGreen;
                    Global.UIdata.LogOfRequestsTimesUI.AddRecordAsync(new UILogOfRequestTime(tr.IP, tr.StartRequestTime, tr.EndRequestTime, color));

                    return new KeyValuePair<TimesRequest, string>(tr, content);
                    // end request block
                    //retVal = someAction(param);
                }
                catch (ProxyException ex)
                {
                    Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage(ex.Message));
                    numOfBadPoxy = maxBadProxyCount;
                    continue;
                }
                catch (HttpException ex)
                {
                    Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage(ex.Message));
                    if (ex.Status == HttpExceptionStatus.ProtocolError)
                    {
                        if ((int)ex.HttpStatusCode != 401)
                            numOfBadPoxy++;
                    }
                    else
                        numOfBadPoxy++;
                }
                catch (Exception ex)
                {
                    Logman.LogAndDisplay(ex);
                    continue;
                }
            }
        }

        #region RegionIsAllowedAndSetCSRF
        public bool RegionIsAllowedAndSetCSRF()
        {
            return (bool)Core(x =>
            {
                int proxyCount = Global.proxyLogic.GetProxyCount();
                if (!usingProxy)
                    proxyCount++;
                for (int i = 0; i < proxyCount; i++)
                {
                    bool regIsOk = ReqgionIsAllowedReq(out TimesRequest tr);
                    if (usingProxy)
                        Global.proxyLogic.SetProxySpeed(proxyInfo.address, tr.RequestTimeExecute);
                    if (!regIsOk)
                    {
                        if (!usingProxy)
                        {
                            SetProxyAndResetReqsInfo();
                        }
                        else
                        {
                            //blockedIP = true;
                            numOfBadPoxy = maxBadProxyCount;
                        }
                    }
                    else
                        return true;
                }
                return false;
            }, null);
        }

        bool ReqgionIsAllowedReq(out TimesRequest tr)
        {
            var res = MakeRequest(new ReqData()
            {
                post = false,
                referer = @"https://lootdog.io/",
                url = @"https://lootdog.io/api/region_is_allowed/?format=json",
                headers = new Dictionary<string, string>() { ["Accept"] = "*/*", ["Accept-Language"] = "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7" }
            });
            string content = res.Value;
            tr = res.Key;
            if (!content.Contains("\"result\":true"))
            {
                Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage($"{GetCurIp()}: not allowed region"));
                return false;
            }
            return true;
        }
        #endregion

        int SetLimit(int count)
        {
            if (count <= 5)
                return 5;
            if (count >= 20)
                return 20;
            if (count % 5 == 0)
                return count;
            if (count < 10)
                return 10;
            return 20;
        }

        #region GetOrders
        public OrdersAnswer GetOrders(int entity_id, int count)
        {
            return (OrdersAnswer)Core(x =>
            {
                List<OrderInfo> list = new List<OrderInfo>();
                int needCount = count;
                int curPage = 1;
                int limit = SetLimit(count);
                int lastCount = 0;
                while (needCount > 0)
                {
                    ReqData reqData = new ReqData()
                    {
                        post = false,
                        url = $@"https://lootdog.io/api/orders/?format=json&product={entity_id}&page={curPage}&limit={limit}&is_buy=false&user=all&sorting=price",
                        referer = $@"https://lootdog.io/product/{entity_id}",
                        headers = new Dictionary<string, string>() { ["Accept"] = "*/*", ["Accept-Language"] = "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7" },
                        MaxTimeOut = 5000
                    };
                    do
                    {
                        var answer = MakeRequest(reqData);
                        if (CheckContentForErrors(answer.Value, out ErrorAnswer errorMsg))
                        {
                            continue;
                        }
                        try
                        {
                            var ordersAnswer = Newtonsoft.Json.JsonConvert.DeserializeObject<OrdersAnswer>(answer.Value);
                            lastCount = ordersAnswer.count;
                            needCount -= curPage++ * limit;
                            list.AddRange(ordersAnswer.results);
                            break;
                            //return Newtonsoft.Json.JsonConvert.DeserializeObject<OrdersAnswer>(answer.Value);
                        }
                        catch (Exception ex)
                        {
                            Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage("Не удалось отпарсить ответ от торговой площадки! Error: " + ex.Message));
                        }
                    } while (true);
                }
                if (list != null && list.Count > 0)
                {
                    var tmp = list.First();
                    Global.settings.EuroRate = tmp.buy_price.amount / tmp.amount;
                }
                return new OrdersAnswer() { count = lastCount, results = list};
            }, null);
        }
        #endregion

        #region GetWebSocketAnswer
        internal WebSocketData GetWebSocketData()
        {
            return (WebSocketData)Core(x =>
            {
                ReqData reqData = new ReqData()
                {
                    post = false,
                    url = $@"https://lootdog.io/api/settings/?format=json&lang=UK",
                    referer = $@"https://lootdog.io/buying?page=1&sorting=popular",
                    headers = new Dictionary<string, string>() { ["Accept"] = "*/*", ["Accept-Language"] = "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7" }
                };
                do
                {
                    var answer = MakeRequest(reqData);
                    if (CheckContentForErrors(answer.Value, out ErrorAnswer errorMsg))
                    {
                        continue;
                    }
                    try
                    {
                        int indexTmp = answer.Value.IndexOf("public_ws");
                        indexTmp = answer.Value.IndexOf("{", indexTmp);
                        string jsonPart = answer.Value.Substring(indexTmp, answer.Value.IndexOf("}", indexTmp) - indexTmp + 1);
                        var answerObj = Newtonsoft.Json.JsonConvert.DeserializeObject<SettingAnswer>(jsonPart);
                        List<KeyValuePair<string, string>> cookies = new List<KeyValuePair<string, string>>();
                        foreach (var item in request.Cookies)
                        {
                            cookies.Add(new KeyValuePair<string, string>(item.Key, item.Value));
                        }
                        return new WebSocketData()
                        {
                            cookies = cookies,
                            userAgent = request.UserAgent,
                            wsSettings = answerObj
                        };
                    }
                    catch (Exception ex)
                    {
                        Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage("Не удалось отпарсить ответ от торговой площадки! Error: " + ex.Message));
                    }
                } while (true);
            }, null);
        }
        #endregion

        #region GetAllMyOrders
        public List<OrderInfo> GetAllmyOrders()
        {
            return (List<OrderInfo>)Core(x =>
            {
                List<OrderInfo> list = new List<OrderInfo>();
                int needCount = 1;
                int curPage = 1;
                int limit = 20;
                while (needCount > 0)
                {
                    ReqData reqData = new ReqData()
                    {
                        post = false,
                        url = $@"https://lootdog.io/api/orders/?format=json&is_buy=0&kind=&sorting=date&page={curPage}&limit={limit}",
                        referer = @"https://lootdog.io/selling",
                        headers = new Dictionary<string, string>() { ["Accept"] = "*/*", ["Accept-Language"] = "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7" },
                        MaxTimeOut = 60000
                    };

                    do
                    {
                        var answer = MakeRequest(reqData);
                        if (CheckContentForErrors(answer.Value, out ErrorAnswer errorMsg))
                        {
                            continue;
                        }
                        try
                        {
                            var ordersAnswer = Newtonsoft.Json.JsonConvert.DeserializeObject<OrdersAnswer>(answer.Value);
                            needCount = ordersAnswer.count - curPage++ * limit;
                            list.AddRange(ordersAnswer.results);
                            break;
                        }
                        catch (Exception ex)
                        {
                            Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage("Не удалось отпарсить ответ от торговой площадки! Error: " + ex.Message));
                        }
                    } while (true);
                }
                return list;
            }, null);
        }
        #endregion

        #region GetMyOrdersByItem
        public List<OrderInfo> GetMyOrdersByItem(int itemId)
        {
            return (List<OrderInfo>)Core(x =>
            {
                List<OrderInfo> list = new List<OrderInfo>();
                int needCount = 1;
                int curPage = 1;
                int limit = 20;
                while (needCount > 0)
                {

                    ReqData reqData = new ReqData()
                    {
                        post = false,
                        url = $@"https://lootdog.io/api/orders/?format=json&is_buy=0&kind=&sorting=date&page={curPage}&limit={limit}&product={itemId}",
                        referer = $@"https://lootdog.io/product/{itemId}",
                        headers = new Dictionary<string, string>() { ["Accept"] = "*/*", ["Accept-Language"] = "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7" },
                        MaxTimeOut = 60000
                    };

                    do
                    {
                        var answer = MakeRequest(reqData);
                        if (CheckContentForErrors(answer.Value, out ErrorAnswer errorMsg))
                        {
                            continue;
                        }
                        try
                        {
                            var ordersAnswer = Newtonsoft.Json.JsonConvert.DeserializeObject<OrdersAnswer>(answer.Value);
                            needCount = ordersAnswer.count - curPage++ * limit;
                            list.AddRange(ordersAnswer.results);
                            break;
                        }
                        catch (Exception ex)
                        {
                            Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage("Не удалось отпарсить ответ от торговой площадки! Error: " + ex.Message));
                        }
                    } while (true);
                }
                return list;
            }, null);
        }
        #endregion

        #region GetOrdersByUser
        public List<OrderInfo> GetOrdersByUser(string userId, int count)
        {
            return (List<OrderInfo>)Core(x =>
            {
                List<OrderInfo> list = new List<OrderInfo>();
                int needCount = count;
                int curPage = 1;
                int limit = SetLimit(count);
                int lastCount = 0;
                while (needCount > 0)
                {
                    ReqData reqData = new ReqData()
                    {
                        post = false,
                        url = $@"https://lootdog.io/api/orders/?format=json&page={curPage}&limit={limit}&is_buy=false&user={userId}&sorting=date",
                        referer = $@"https://lootdog.io/profile/{userId}",
                        headers = new Dictionary<string, string>() { ["Accept"] = "*/*", ["Accept-Language"] = "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7" },
                        MaxTimeOut = 5000
                    };
                    do
                    {
                        var answer = MakeRequest(reqData, ReqPrioritity.Medium);
                        if (CheckContentForErrors(answer.Value, out ErrorAnswer errorMsg))
                        {
                            continue;
                        }
                        try
                        {
                            var ordersAnswer = Newtonsoft.Json.JsonConvert.DeserializeObject<OrdersAnswer>(answer.Value);
                            lastCount = ordersAnswer.count;
                            needCount -= curPage++ * limit;
                            list.AddRange(ordersAnswer.results);
                            break;
                            //return Newtonsoft.Json.JsonConvert.DeserializeObject<OrdersAnswer>(answer.Value);
                        }
                        catch (Exception ex)
                        {
                            Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage("Не удалось отпарсить ответ от торговой площадки! Error: " + ex.Message));
                        }
                    } while (true);
                }
                return list;
            }, null);
        }
        #endregion

        #region GetAllInventory
        public List<ItemsOrderOrInvetnroyInfo> GetAllInventory()
        {
            return (List<ItemsOrderOrInvetnroyInfo>)Core(x =>
            {
                List<ItemsOrderOrInvetnroyInfo> list = new List<ItemsOrderOrInvetnroyInfo>();
                int needCount = 1;
                int curPage = 1;
                int limit = 35;
                while (needCount > 0)
                {
                    ReqData reqData = new ReqData()
                    {
                        post = false,
                        url = $@"https://lootdog.io/api/user_inventory/?format=json&kind=mailru&status=remaining&search=&limit={limit}&page={curPage}&options=[object Object]",
                        referer = @"https://lootdog.io/inventory/mailru",
                        headers = new Dictionary<string, string>() { ["Accept"] = "*/*", ["Accept-Language"] = "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7" },
                        MaxTimeOut = 60000
                    };

                    do
                    {
                        var answer = MakeRequest(reqData);
                        if (CheckContentForErrors(answer.Value, out ErrorAnswer errorMsg))
                        {
                            continue;
                        }
                        try
                        {
                            var inventoryAnswer = Newtonsoft.Json.JsonConvert.DeserializeObject<InventoryAnswer>(answer.Value);
                            needCount = inventoryAnswer.count - curPage++ * limit;
                            list.AddRange(inventoryAnswer.results);
                            break;
                        }
                        catch (Exception ex)
                        {
                            Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage("Не удалось отпарсить ответ от торговой площадки! Error: " + ex.Message));
                        }
                    } while (true);
                }
                return list;
            }, null);
        }
        #endregion

        #region CancelOrder
        public bool CancelOrder(string orderID)
        {
            return (bool)Core(x =>
            {
                if (!request.Cookies.ContainsKey("csrftoken"))
                    return false;
                string csrf = request.Cookies["csrftoken"];
                ReqData reqData = new ReqData()
                {
                    deleteMethod = true,
                    url = $@"https://lootdog.io/api/orders/{orderID}/",
                    referer = @"https://lootdog.io/selling",
                    headers = new Dictionary<string, string>()
                    {
                        ["X-CSRFToken"] = csrf,
                        ["Accept"] = "*/*",
                        ["Accept-Language"] = "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7",
                        ["Origin"] = @"https://lootdog.io"
                    },
                    MaxTimeOut = 60000,
                    multipartForm = true,
                    reqParams = new RequestParams() { new KeyValuePair<string, string>("id", orderID) }
                };

                do
                {
                    string content = MakeRequest(reqData).Value;
                    if (CheckContentForErrors(content, out ErrorAnswer errorMsg))
                    {
                        if (errorMsg.code == "SecondFactorNeeded")
                        {
                            WorkingRequestsManager.WaitingForSMS = true;
                            SendSMS(errorMsg);
                            continue;
                        }
                        return false;
                    }
                    return true;
                } while (true);
                
            }, null);
        }
        #endregion

        #region SellItem
        public OrderInfo SellItem(int id, decimal price)
        {
            return (OrderInfo)Core(x =>
            {
                if (!request.Cookies.ContainsKey("csrftoken"))
                    return null;
                string csrf = request.Cookies["csrftoken"];
                ReqData reqData = new ReqData()
                {
                    post = true,
                    url = @"https://lootdog.io/api/orders/",
                    referer = $@"https://lootdog.io/inventory/mailru/sell_order_create/{id}",
                    headers = new Dictionary<string, string>()
                    {
                        ["X-CSRFToken"] = csrf,
                        ["Accept"] = "*/*",
                        ["Accept-Language"] = "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7",
                        ["Origin"] = @"https://lootdog.io"
                    },
                    MaxTimeOut = 60000,
                    multipartForm = true,
                    reqParams = new RequestParams()
                    {
                        new KeyValuePair<string, string>("is_buy", "false"),
                        new KeyValuePair<string, string>("item", id.ToString()),
                        new KeyValuePair<string, string>("price_val", price.ToString().Replace(",",".")),
                    }
                };

                do
                {
                    string content = MakeRequest(reqData).Value;
                    if (CheckContentForErrors(content, out ErrorAnswer errorMsg))
                    {
                        if (errorMsg.code == "SecondFactorNeeded")
                        {
                            WorkingRequestsManager.WaitingForSMS = true;
                            SendSMS(errorMsg);
                            continue;
                        }
                        return null;
                    }
                    try
                    {
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<OrderInfo>(content);
                    }
                    catch (Exception ex)
                    {
                        Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage("Не удалось отпарсить ответ от торговой площадки! Error: " + ex.Message));
                    }
                } while (true);

            }, null);
        }
        #endregion

        #region BuyItem
        public BuyItemAnswer BuyItem(int id, decimal price, string orderId)
        {
            return (BuyItemAnswer)Core(x =>
            {
                if (!request.Cookies.ContainsKey("csrftoken"))
                    return new BuyItemAnswer() { completed = false, error = "no csrftoken" };
                string csrf = request.Cookies["csrftoken"];
                ReqData reqData = new ReqData()
                {
                    post = true,
                    url = @"https://lootdog.io/api/instant_buy/",
                    referer = $@"https://lootdog.io/product/{id}/instant_buy/{orderId}",
                    headers = new Dictionary<string, string>()
                    {
                        ["X-CSRFToken"] = csrf,
                        ["Accept"] = "*/*",
                        ["Accept-Language"] = "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7",
                        ["Origin"] = @"https://lootdog.io"
                    },
                    MaxTimeOut = 60000,
                    multipartForm = true,
                    reqParams = new RequestParams()
                    {
                        new KeyValuePair<string, string>("order", orderId),
                        new KeyValuePair<string, string>("buy_price", price.ToString().Replace(",",".")),
                        new KeyValuePair<string, string>("source", ""),
                        new KeyValuePair<string, string>("is_gift", "false"),
                    }
                };

                do
                {
                    string content = MakeRequest(reqData, ReqPrioritity.Fast).Value;
                    if (CheckContentForErrors(content, out ErrorAnswer errorMsg))
                    {
                        if (errorMsg.code == "SecondFactorNeeded")
                        {
                            WorkingRequestsManager.WaitingForSMS = true;
                            SendSMS(errorMsg);
                            continue;
                        }
                        return new BuyItemAnswer() { completed = false, error = errorMsg.detail };
                    }
                    try
                    {
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<BuyItemAnswer>(content);
                    }
                    catch (Exception ex)
                    {
                        Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage("Не удалось отпарсить ответ от торговой площадки! Error: " + ex.Message));
                    }
                } while (true);

            }, null);
        }
        #endregion

        #region GetMainItemInfo
        public MarketItemInfo GetMarketItemInfo(int id)
        {
            return (MarketItemInfo)Core(x =>
            {
                ReqData reqData = new ReqData()
                {
                    post = false,
                    url = $@"https://lootdog.io/api/products/{id}/market_info/?format=json&id={id}",
                    referer = $@"https://lootdog.io/product/{id}",
                    headers = new Dictionary<string, string>() { ["Accept"] = "*/*", ["Accept-Language"] = "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7" }
                };
                do
                {
                    var answer = MakeRequest(reqData);
                    if (CheckContentForErrors(answer.Value, out ErrorAnswer errorMsg))
                    {
                        continue;
                    }
                    try
                    {
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<MarketItemInfo>(answer.Value);
                    }
                    catch (Exception ex)
                    {
                        Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage("Не удалось отпарсить ответ от торговой площадки! Error: " + ex.Message));
                    }
                } while (true);

            }, null);
        }
        #endregion

        #region GetOrderInfo
        public OrderInfo GetOrderInfo(string orderId)
        {
            return (OrderInfo)Core(x =>
            {
                ReqData reqData = new ReqData()
                {
                    post = false,
                    url = $@"https://lootdog.io/api/orders/{orderId}/?format=json&id={orderId}",
                    referer = $@"https://lootdog.io/product/585/instant_buy/{orderId}",
                    headers = new Dictionary<string, string>() { ["Accept"] = "*/*", ["Accept-Language"] = "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7" }
                };
                do
                {
                    var answer = MakeRequest(reqData);
                    if (CheckContentForErrors(answer.Value, out ErrorAnswer errorMsg))
                    {
                        continue;
                    }
                    try
                    {
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<OrderInfo>(answer.Value);
                    }
                    catch (Exception ex)
                    {
                        Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage("Не удалось отпарсить ответ от торговой площадки! Error: " + ex.Message));
                    }
                } while (true);

            }, null);
        }
        #endregion

        #region EnsurePremission
        public void EnsurePremisssion()
        {
            Core(x =>
            {
                ReqData reqData = new ReqData()
                {
                    post = false,
                    url = @"https://lootdog.io/api/current_user/ensure_permissions/?format=json&sf_req=true&wallet_req=true",
                    referer = $@"https://lootdog.io/inventory/mailru/sell_order_create/{new Random().Next(1000000, 10000000)}",
                    headers = new Dictionary<string, string>()
                    {
                        ["Accept"] = "*/*",
                        ["Accept-Language"] = "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7",
                    },
                    MaxTimeOut = 60000
                };

                string content = MakeRequest(reqData).Value;
                if (CheckContentForErrors(content, out ErrorAnswer errorMsg))
                {
                    if (errorMsg.code == "SecondFactorNeeded")
                    {
                        WorkingRequestsManager.WaitingForSMS = true;
                        SendSMS(errorMsg);
                    }
                }
                return null;

            }, null);
        }

        #endregion

        public string smsText;
        #region SendSMS
        void SendSMS(ErrorAnswer errorMsg)
        {
            do
            {
                SMSForm smsForm = new SMSForm(this, errorMsg.extra.phone);
                smsForm.ShowDialog();
                smsForm.Dispose();

                if (!request.Cookies.ContainsKey("csrftoken"))
                {
                    WorkingRequestsManager.WaitingForSMS = false;
                    return;
                }
                string csrf = request.Cookies["csrftoken"];

                ReqData reqData = new ReqData()
                {
                    post = true,
                    url = @"https://lootdog.io/api/current_user/confirm_code/",
                    referer = @"https://lootdog.io/selling",
                    headers = new Dictionary<string, string>()
                    {
                        ["X-CSRFToken"] = csrf,
                        ["Accept"] = "*/*",
                        ["Accept-Language"] = "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7",
                        ["Origin"] = @"https://lootdog.io"
                    },
                    MaxTimeOut = 60000,
                    multipartForm = true,
                    reqParams = new RequestParams() { new KeyValuePair<string, string>("code", smsText) }
                };

                string content = MakeRequest(reqData).Value;
                if (CheckContentForErrors(content, out ErrorAnswer errorMsgCur))
                {
                    if (errorMsgCur.code == "MissingParam")
                    {
                        continue;
                    };
                }
                break;
            } while (true);
            WorkingRequestsManager.WaitingForSMS = false;
        }
        #endregion

        #region GetAllItems
        public KeyValuePair<TimesRequest, List<ItemMPAnswer>> GetIAlltems()
        {
            return (KeyValuePair<TimesRequest, List<ItemMPAnswer>>)Core(x =>
            {
                List<ItemMPAnswer> list = new List<ItemMPAnswer>();
                TimesRequest lastTR = null;
                int needCount = 1;
                int curPage = 1;
                int limit = 72;
                while (needCount > 0)
                {
                    ReqData reqData = new ReqData()
                    {
                        post = false,
                        url = $@"https://lootdog.io/api/products/?format=json&search=&on_sale=1&game=&price_min=&price_max=&kind=&sorting=popular&page={curPage}&limit={limit}",
                        referer = $@"https://lootdog.io/buying?page={curPage}&sorting=popular",
                        headers = new Dictionary<string, string>() { ["Accept"] = "*/*", ["Accept-Language"] = "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7" },
                        MaxTimeOut = 80000
                    };

                    do
                    {
                        var answer = MakeRequest(reqData);
                        if (CheckContentForErrors(answer.Value, out ErrorAnswer errorMsg))
                        {
                            continue;
                        }
                        try
                        {
                            var mpAnswer = Newtonsoft.Json.JsonConvert.DeserializeObject<AllItemsMPAnswer>(answer.Value);
                            needCount = mpAnswer.count - curPage++ * limit;
                            list.AddRange(mpAnswer.results);
                            lastTR = answer.Key;
                            break;
                        }
                        catch (Exception ex)
                        {
                            Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage("Не удалось отпарсить ответ от торговой площадки! Error: " + ex.Message));
                        }
                    } while (true);
                }
                return new KeyValuePair<TimesRequest, List<ItemMPAnswer>>(lastTR, list);
            }, null);
        }
        #endregion

        #region GetItemMarketStats
        public List<MarketStatInfo> GetItemMarketStats(int id, MarketStatPeriod period)
        {
            return (List<MarketStatInfo>)Core(x =>
            {
                ReqData reqData = new ReqData()
                {
                    post = false,
                    url = $@"https://lootdog.io/api/products/{id}/trade_stats/?format=json&id={id}&granularity=hour&period={period}",
                    referer = $@"https://lootdog.io/inventory/mailru/sell_order_create/{new Random().Next(1000000, 10000000)}",
                    headers = new Dictionary<string, string>() { ["Accept"] = "*/*", ["Accept-Language"] = "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7" },
                    MaxTimeOut = 5000
                };
                do
                {
                    var answer = MakeRequest(reqData);
                    if (CheckContentForErrors(answer.Value, out ErrorAnswer errorMsg))
                    {
                        continue;
                    }
                    try
                    {
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<List<MarketStatInfo>>(answer.Value);
                    }
                    catch (Exception ex)
                    {
                        Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage("Не удалось отпарсить ответ от торговой площадки! Error: " + ex.Message));
                    }
                } while (true);

            }, null);
        }
        #endregion

        public void Dispose()
        {
            if (usingProxy)
            {
                Global.proxyLogic.ReleaseProxy(proxyInfo.address);
            }
            accountsManager?.SetCookiesToAcc(Login, numOfAcc, FromCookieDictionaryToString(request.Cookies));
        }
    }
}