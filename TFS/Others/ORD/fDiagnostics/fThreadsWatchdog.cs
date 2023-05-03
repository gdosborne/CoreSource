using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SNC.OptiRamp.Services.fDiagnostics
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ThreadsWatchdog : System.ComponentModel.Component, IThreadsWatchdog
    {
        private string sLogName;
        private static int iInstance = 0;
        private string sLogPrefix = (iInstance++ == 0) ? "ThreadsWatchdog: " : string.Format("ThreadsWatchdog{0}: ", iInstance);
        private OptiRampLog log;
        private System.Threading.Timer pTimer;
        private int iIn_Timer_Call_Back = 0;
        private int iLast_Alive_Message = System.Environment.TickCount;
        const int ctAlive_Message_Interval =  60  *  60 * 1000;

        //
        //
        private void Log(string msg)
        {
            try
            {
                log.WriteRecord(sLogPrefix + msg, sLogName);
            } catch (Exception err) {
                System.Diagnostics.Trace.WriteLine(err.ToString()); // paranoidal
            }
        }

        //
        //
        private string Thread_to_string(System.Threading.Thread pThread)
        {
            try
            {
                string sThreadName = pThread.Name;
                string sRet = string.Format("thread 0x{0:X}", pThread.ManagedThreadId);
                return string.IsNullOrEmpty(sThreadName) ? sRet : (sRet + string.Format(" \"{0}\"", sThreadName));
            } catch (Exception err) {
                return err.Message;
            }
        } // ......................................... Thread_to_string .........................................

        //
        //
        private string ThreadInfo_to_string(System.Threading.Thread pThread)
        {
            try
            {
                var tRet = new System.Text.StringBuilder(64);
                if (pThread.IsBackground)
                {
                    if (tRet.Length != 0) tRet.Append(", ");
                    tRet.Append("IsBackground");
                }
                if (pThread.IsThreadPoolThread)
                {
                    if (tRet.Length != 0) tRet.Append(", ");
                    tRet.Append("IsThreadPoolThread");
                }
                if (pThread.CurrentCulture !=  System.Globalization.CultureInfo.InvariantCulture)
                {
                    if (tRet.Length != 0) tRet.Append(", ");
                    tRet.Append("Culture=" + pThread.CurrentCulture.Name);
                }
                if (tRet.Length != 0) tRet.Append(", ");
                tRet.Append(pThread.ThreadState.ToString());
                return tRet.ToString();
            }
            catch (Exception err)
            {
                return err.Message;
            }
        } // ................................................. ThreadInfo_to_string ....................................

        //
        //
        private void Check_Thread_Culture(System.Threading.Thread pThread)
        {
            if (pThread.CurrentCulture !=  System.Globalization.CultureInfo.InvariantCulture)
            {
                Log("warning wrong culture: " + Thread_to_string(pThread) + ", " + ThreadInfo_to_string(pThread) + "!");
            }
        } // ................................ Check_Thread_Culture ....................................


        //
        //
        //
        private sealed class SingleThreadInfo
        {
            public int Watchdog_mSeconds, LastNudge;
            public enum E_Log_Status : int { None, Stalled, Running };
            public E_Log_Status tLog_Status = E_Log_Status.None;
            //
            public SingleThreadInfo(int WatchdogSeconds_)
            {
                Watchdog_mSeconds = (WatchdogSeconds_ == int.MaxValue) ? int.MaxValue : (WatchdogSeconds_ * 1100);
                LastNudge = Environment.TickCount;
            }

        }
        //>>>
        private Object ProtectThreadsInfo = new Object();
        private Dictionary<System.Threading.Thread, SingleThreadInfo> ThreadsInfo = new Dictionary<System.Threading.Thread, SingleThreadInfo>();
        //<<<

        /// <summary>
        /// 
        /// </summary>
        private void Timer_Call_Back(System.Object o)
        {
            try
            {
                if (System.Threading.Interlocked.Increment(ref iIn_Timer_Call_Back) == 1)
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
                    int iThreads_Count = 0, iThreads_Stalled = 0;
                    int iCurrent_Time = System.Environment.TickCount;
                    lock (ProtectThreadsInfo)
                    { 
                        iThreads_Count = ThreadsInfo.Count;
                        foreach (var tIter in ThreadsInfo)
                        {
                            var iWatchdog_mSeconds = tIter.Value.Watchdog_mSeconds;
                            if (iWatchdog_mSeconds == int.MaxValue) continue;

                            var Buf_Info = tIter.Value;


                            if (iCurrent_Time - Buf_Info.LastNudge > iWatchdog_mSeconds)
                            {
                                ++iThreads_Stalled;
                                if (Buf_Info.tLog_Status != SingleThreadInfo.E_Log_Status.Stalled)
                                {
                                    Log("stalled " + Thread_to_string(tIter.Key) + "; " + ThreadInfo_to_string(tIter.Key));
                                    Buf_Info.tLog_Status = SingleThreadInfo.E_Log_Status.Stalled;
                                }
                            } else {
                              if (Buf_Info.tLog_Status == SingleThreadInfo.E_Log_Status.Stalled)
                              {
                                  Log("recovered " + Thread_to_string(tIter.Key));
                                  Buf_Info.tLog_Status = SingleThreadInfo.E_Log_Status.Running;
                              }
                            }
                        }
                    } // lock (ProtectThreadsInfo) ......

                    if (iCurrent_Time - iLast_Alive_Message > ctAlive_Message_Interval)
                    {
                        Log(string.Format("is alive, watched threads={0}, stalled={1}, GC memory={2:N0}", 
                            iThreads_Count, iThreads_Stalled, System.GC.GetTotalMemory( false )
                            ));

                        iLast_Alive_Message = iCurrent_Time;
                    }

                } // if (System.Threading.Interlocked.Increment .........
            } catch (Exception err) {
                Log(err.Message);
            } finally {
                System.Threading.Interlocked.Decrement(ref iIn_Timer_Call_Back);
            }

        } // ............................ Timer_Call_Back ...............................


        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="log"></param>
        public ThreadsWatchdog(OptiRampLog log_, string sLogName_)
        {
            // log
            if (log_ == null || string.IsNullOrEmpty(sLogName_))
            {
                throw new System.ArgumentNullException("log is not defined");
            }
            sLogName = sLogName_;
            log = log_;

            pTimer = new System.Threading.Timer(new System.Threading.TimerCallback(this.Timer_Call_Back), null, 1000, 1000);

            var pThread = System.Threading.Thread.CurrentThread;
            Log("created on " + Thread_to_string(pThread));
            Check_Thread_Culture(pThread);
        } // ............................. ctor ........................

        // -------------------------------- IDispose ---------------------------------
        /// <summary>
        /// 
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (pTimer != null)
            {
                var pWait = new System.Threading.AutoResetEvent(false);
                pTimer.Dispose(pWait);
                pWait.WaitOne(100);
                pTimer = null;
                pWait.Dispose();
            }
            if (ThreadsInfo != null)
            {
                Log("disposing on " + Thread_to_string(System.Threading.Thread.CurrentThread));
                if (ThreadsInfo.Count > 0) foreach (var tIter in ThreadsInfo)
                {
                  Log("still registered " + Thread_to_string(tIter.Key));
                }
                ThreadsInfo = null;
            }
        } // ............................. Dispose .............................

        // ------------------------- IThreadsWatchdog --------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="WatchdogSeconds">
        /// 
        /// </param>
        public void ThreadRegister(int WatchdogSeconds = int.MaxValue)
        {
            try
            {
                var pCurrentThread = System.Threading.Thread.CurrentThread;
                SingleThreadInfo Buf_Info;
                lock (ProtectThreadsInfo)
                {
                    if (ThreadsInfo.TryGetValue(pCurrentThread, out Buf_Info))
                    {
                        Log(string.Format("Double call of ThreadRegister from {0}", Thread_to_string(pCurrentThread) ) );
                        return;
                    }
                    Buf_Info = new SingleThreadInfo(WatchdogSeconds);
                    ThreadsInfo.Add(pCurrentThread, Buf_Info);
                }

            } catch (Exception Err) {
                Log(Err.ToString());
            }

        } // ............................ ThreadRegister ..........................

        /// <summary>
        /// 
        /// </summary>
        public void ThreadUnregister()
        {
            try
            {
                var pCurrentThread = System.Threading.Thread.CurrentThread;
                lock (ProtectThreadsInfo)
                {
                    if (!ThreadsInfo.Remove(pCurrentThread))
                    {
                        Log(string.Format("Double call of ThreadUnregister from {0}", Thread_to_string(pCurrentThread)));
                    }
                }
            }
            catch (Exception Err)
            {
                Log(Err.ToString());
            }
        } // ................................. ThreadUnregister .........................


        /// <summary>
        /// 
        /// </summary>
        public void ThreadNudge()
        {
            try
            {
                var pCurrentThread = System.Threading.Thread.CurrentThread;
                SingleThreadInfo Buf_Info;
                lock (ProtectThreadsInfo)
                {
                    if (!ThreadsInfo.TryGetValue(pCurrentThread, out Buf_Info)) { Buf_Info = null; }
                }
                if (Buf_Info == null)
                {
                    Log(string.Format("not registered nudge from {0}", Thread_to_string(pCurrentThread)));
                }
                else
                {
                    Buf_Info.LastNudge = System.Environment.TickCount;
                }
            }
            catch (Exception Err)
            {
                Log(Err.ToString());
            }
        } // ............................................... ThreadNudge ..........................


    } // ........................ ThreadsWatchdog .........................
}
