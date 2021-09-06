using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDmp
{
    public class RequestManagerBase<T> : IDisposable where T : UnitBase
    {
        protected List<T> units = new List<T>();
        protected AccountManager accounts;
        public RequestManagerBase(AccountManager accounts)
        {
            this.accounts = accounts;
        }
        virtual public void WaitForAllDone()
        {
            foreach (var item in units)
            {
                item.WaitForAllDone();
            }
        }

        virtual public bool InitsUnits()
        {
            foreach (var item in units)
            {
                if (!item.Init(accounts))
                    return false;
            }
            return true;
        }

        public virtual void Dispose()
        {
            foreach (var item in units)
            {
                item.Dispose();
            }
        }
    }
}
