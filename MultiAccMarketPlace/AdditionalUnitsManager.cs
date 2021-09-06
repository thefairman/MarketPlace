using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDmp
{
    class AdditionalUnitsManager
    {
        static List<UnitCheckerAdditional> unitsAdditional = new List<UnitCheckerAdditional>();
        public static void SetThreadsNum(int additThreadsNum)
        {
            for (int i = 0; i < additThreadsNum; i++)
            {
                unitsAdditional.Add(new UnitCheckerAdditional(Global.settings.UsingProxyAdditional));
            }
        }

        public static bool InitsUnits()
        {
            foreach (var item in unitsAdditional)
            {
                if (!item.Init(null))
                    return false;
            }
            return true;
        }

        public static UnitCheckerAdditional GetAdditionalUnit()
        {
            var uca = GetAdditionalUnitBack();
            uca.Wait();
            return uca.Result;
        }

        readonly static object locker = new object();
        static Task<UnitCheckerAdditional> GetAdditionalUnitBack()
        {
            lock (locker)
            {
                UnitCheckerAdditional maxTimeAU = null;
                foreach (var item in unitsAdditional)
                {
                    if (item.busyState == BusyState.Free)
                        return item.CurTask;
                    if (maxTimeAU == null)
                        maxTimeAU = item;
                    else
                    {
                        if (item.startUserChecking < maxTimeAU.startUserChecking)
                            maxTimeAU = item;
                    }
                }
                return maxTimeAU.GetThisUnit();
            }
        }

        internal static void WaitForAllDone()
        {
            foreach (var item in unitsAdditional)
            {
                item.WaitForAllDone();
            }
        }

        public static void ReleaseItems()
        {
            foreach (var item in unitsAdditional)
            {
                item.Dispose();
            }
        }
    }
}
