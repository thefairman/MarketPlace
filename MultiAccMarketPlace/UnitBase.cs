using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDmp
{
    public abstract class UnitBase : IDisposable
    {
        public RequestLogic reqLogic { get; private set; }
        public AccountData accData { get; private set; }

        protected readonly double updateSessionIntervalInMinute = 1;
        protected readonly int secondsWhenUpdate = 30;
        protected Task sessionUpdater;
        public UnitBase(AccountData accData, bool usingProxy, bool fast)
        {
            this.accData = accData;
            if (accData == null)
            {
                reqLogic = new RequestLogic(usingProxy, fast);
            }
            else
            {
                reqLogic = new RequestLogic(usingProxy, accData.Login, accData.Pass, fast);

            }
        }

        bool initiated = false;
        virtual public bool Init(AccountManager accounts)
        {
            if (initiated)
                return reqLogic.IsAutorizedOnLootDog;
            initiated = true;

            reqLogic.SetAccounts(accounts);
            if (!reqLogic.RegionIsAllowedAndSetCSRF())
                return false;
            if (accounts == null)
                return true;
            
            reqLogic.AutorizOnLootDog();

            return reqLogic.IsAutorizedOnLootDog;
        }

        protected DateTime lastUpdateSessionTime;
        protected void SessionUpdater()
        {
            while (!MainWork.PausedOrPausing())
            {
                DateTime now = DateTime.Now;
                DateTime timeWhenShouldUpdate = lastUpdateSessionTime.AddMinutes(updateSessionIntervalInMinute);
                timeWhenShouldUpdate = new DateTime(timeWhenShouldUpdate.Year, timeWhenShouldUpdate.Month, timeWhenShouldUpdate.Day, timeWhenShouldUpdate.Hour, timeWhenShouldUpdate.Minute, secondsWhenUpdate);
                int toUpdateTime = (int)(timeWhenShouldUpdate - now).TotalMilliseconds;
                // updateTokenIntervalinMinutes * 60 * 1000 - (now - lastUpdateTokenTime).TotalMilliseconds;
                if (toUpdateTime > 0) // awaiting block
                {
                    if (toUpdateTime > 100)
                        toUpdateTime = 100;
                    System.Threading.Thread.Sleep(toUpdateTime);
                    continue;
                }
                reqLogic.RegionIsAllowedAndSetCSRF();
                //reqLogic.RegionIsAllowedAndSetCSRF();
                lastUpdateSessionTime = DateTime.Now;
            }
        }

        abstract public void WaitForAllDone();

        public void Dispose()
        {
            reqLogic.Dispose();
        }
    }
}
