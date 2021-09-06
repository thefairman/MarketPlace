using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDmp
{
    public class AccountManager
    {
        AccountData Account = null;
        readonly string accountPath;
        public AccountManager(string accountPath)
        {
            this.accountPath = accountPath;
            LoadAccounts();
        }

        void LoadAccounts()
        {
            if (File.Exists(accountPath))
            {
                Account = Newtonsoft.Json.JsonConvert.DeserializeObject<AccountData>(File.ReadAllText(accountPath), new CustomDateTimeConverter());
            }
        }

        public void AddAccount(string login, string pass)
        {
            Account = new AccountData()
            {
                Login = login,
                Pass = pass,
            };
            SaveAcoounts();
        }

        private object lockerSaveAccounts = new object();
        public void SaveAcoounts()
        {
            lock (lockerSaveAccounts)
            {
                File.WriteAllText(accountPath, Newtonsoft.Json.JsonConvert.SerializeObject(Account, new CustomDateTimeConverter()));
            }
        }

        public void DelAccount()
        {
            Account = null;
            SaveAcoounts();
        }
        private object lockerCookie = new object();
        int currentAcc = 0;
        public string GetCookiesFromAcc(string acc, out int numOfAcc)
        {
            if (acc == null)
            {
                numOfAcc = -1;
                return null;
            }
            lock (lockerCookie)
            {
                if (Account.Cookies == null)
                {
                    Account.Cookies = new List<string>();
                    Account.Cookies.Add("");
                }
                else if (currentAcc >= Account.Cookies.Count)
                    Account.Cookies.Add("");

                numOfAcc = currentAcc;
                return Account.Cookies[currentAcc++];
            }
        }

        public void ResetCurrentAcc()
        {
            currentAcc = 0;
        }
        public void SetCookiesToAcc(string acc, int numOfAcc, string cookies)
        {
            if (Account.Login == acc)
            {
                if (Account.Cookies != null && Account.Cookies.Count >= numOfAcc + 1)
                {
                    Account.Cookies[numOfAcc] = cookies;
                    SaveAcoounts();
                }
            }
        }

        public AccountData GetAccounts()
        {
            return Account;
        }
    }
}
