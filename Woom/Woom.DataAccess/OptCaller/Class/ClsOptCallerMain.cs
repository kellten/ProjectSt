using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Woom.DataAccess.OptCaller.Class
{
    public static class ClsOptCallerMain
    {
        public static Queue<string> AxKHQueue = new Queue<string>();
        public static DateTime AxKHCanCallTime;

        private static object lockObject = new object();

        public static async Task OptCommRqData(string sRQName, string sTrCode, int nPrevNext, string sScreenNo)
        {
            if (AxKHCanCallTime == null)
            {
                AxKHCanCallTime = DateTime.Now;
            }

            bool blnCall = false;

            blnCall = await CheckCanCall();

            AxKHQueue.Enqueue(sRQName + "/" + sTrCode + "/" + nPrevNext.ToString() + "/" + sScreenNo);

            if (blnCall == true)
            {
                string[] arrayCm = AxKHQueue.Dequeue().ToString().Split('/');
                CallOptCommRqData(arrayCm[0], arrayCm[1], Convert.ToInt32(arrayCm[2]), arrayCm[3]);
            }
        }

        //public static void OptCommRqData(string sRQName, string sTrCode, int nPrevNext, string sScreenNo)
        //{
        //    if (AxKHCanCallTime == null)
        //    {
        //        AxKHCanCallTime = DateTime.Now;
        //    }

        //    AxKHQueue.Enqueue(sRQName + "/" + sTrCode + "/" + nPrevNext.ToString() + "/" + sScreenNo);

        //    CheckCanCall();

        //    string[] arrayCm = AxKHQueue.Dequeue().ToString().Split('/');
        //    CallOptCommRqData(arrayCm[0], arrayCm[1], Convert.ToInt32(arrayCm[2]), arrayCm[3]);
        //}

        //private static bool CheckCanCall()
        //{
        //    if (AxKHCanCallTime < DateTime.Now)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        while (true)
        //        {
        //            Thread.Sleep(1000);

        //            if (AxKHCanCallTime < DateTime.Now)
        //            {
        //                break;
        //            }
        //        }

        //        return true;
        //    }
        //}

        private static async Task<bool> CheckCanCall()
        {
            bool blnCall = false;

            Task<bool> t = new Task<bool>(() =>
            {
                if (AxKHCanCallTime < DateTime.Now)
                {
                    return true;
                }
                else
                {
                    lock (lockObject)
                    {
                        while (true)
                        {
                            Thread.Sleep(1000);

                            if (AxKHCanCallTime < DateTime.Now)
                            {
                                break;
                            }
                        }
                    }

                    return true;
                }
            });

            t.Start();

            blnCall = await t;

            return blnCall;
        }

        private static void CallOptCommRqData(string sRQName, string sTrCode, int nPrevNext, string sScreenNo)
        {
            Woom.DataAccess.PlugIn.ClsAxKH.AxKH.CommRqData(sRQName, sTrCode, nPrevNext, sScreenNo);

            AxKHCanCallTime = DateTime.Now.AddSeconds(5);
        }
    }
}