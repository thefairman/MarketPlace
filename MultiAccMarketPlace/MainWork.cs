using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LDmp
{
    class MainWork
    {
        static public AccountManager AccountUser { get; private set; } = null;
        static public bool Running { get; private set; }
        static bool Paused { get; set; } = false;
        static public SoundPlayer sp = null;
        public MainWork()
        {
            try
            {
                sp = new SoundPlayer(Properties.Resources.alarm);
            }
            catch (Exception ex)
            {
                Global.UIdata.ErrororsUI.AddRecordAsync(new UIMessage(ex.Message));
            }

            AccountUser = new AccountManager(Path.Combine(Environment.CurrentDirectory, "accounts.json"));
            
            Rucaptcha.Key = "***";
        }

        public static bool PausedOrPausing()
        {
            return !Running || Paused;
        }

        public static WorkingRequestsManager WorkingReqManager { get; private set; }
        public static PublicRequestManager PublicReqManager { get; private set; }
        void MainWorkCycle()
        {
            int totalTheoryThreads = Global.settings.ThreadsNum + Global.settings.AdditThreadsNum + 3;
            ThreadPool.SetMinThreads(totalTheoryThreads, totalTheoryThreads);
            System.Net.ServicePointManager.DefaultConnectionLimit = totalTheoryThreads;
            PublicReqManager = new PublicRequestManager(Global.settings.ThreadsNum, Global.settings.AdditThreadsNum, Global.settings.OnlyAdditional);
            if (!PublicReqManager.InitsUnits())
                return;
            WorkingReqManager = new WorkingRequestsManager(AccountUser);
            if (!WorkingReqManager.InitsUnits())
                return;
            PublicReqManager.Start();
            PublicReqManager.WaitForAllDone(); // сначала должны дождаться пока закончится publicReqManager, т.к. в нем основные потоки
            WorkingReqManager.WaitForAllDone();

            PublicReqManager.Dispose();
            WorkingReqManager.Dispose();
            //if (publicReqManager.UnitsTasks.Count > 0)
            //    Task.WaitAll(publicReqManager.UnitsTasks.ToArray());
            //if (Paused)
            //{
            //    Running = false;
            //    List<Task> activeActions = GetListActiveActions();
            //    if (activeActions.Count > 0)
            //        Task.WaitAll(activeActions.ToArray());
            //}
            //Paused = false;
            //return;

            //while (true)
            //{
            //    if (Paused)
            //    {
            //        List<Task> activeActions = GetListActiveActions();
            //        if (activeActions.Count > 0)
            //            Task.WaitAll(activeActions.ToArray());
            //        Running = false;
            //        if (publicReqManager.UnitsTasks.Count > 0)
            //            Task.WaitAll(publicReqManager.UnitsTasks.ToArray());
            //        Paused = false;
            //        return;
            //    }

            //    try
            //    {
            //        // some actions
            //    }
            //    catch (Exception ex)
            //    {
            //        Global.UIdata.ErrororsUI.AddRecord(new UIMessages(ex.Message));
            //    }
            //}
        }

        Task mwc = null;
        public void Start()
        {
            if (mwc == null || mwc.IsCompleted)
            {
                mwc = Task.Factory.StartNew(MainWorkCycle).ContinueWith(task => { Running = false; Paused = false; AccountUser?.ResetCurrentAcc(); });
            }

            Running = true;
            Paused = false;
        }

        public static event Action Stopped;
        public void Stop()
        {
            Stopped?.Invoke();
            Paused = true;
        }
    }
}
