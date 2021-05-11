using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Windows.Forms;
using Woom.DataAccess.ErrorManage.Class;
using static Woom.DataAccess.OptCaller.Class.ClsOptCallerMain;

namespace Woom.DataAccess.PlugIn
{
    public static class ClsAxKH
    {
        public static AxKHOpenAPILib.AxKHOpenAPI AxKH;
        public static ConcurrentQueue<ArrayList> AxKHQueueList = new ConcurrentQueue<ArrayList>();

        private static void Opt10081(string stockCode, string stdDate, string modifyJugaGb)
        {
            ClsAxKH.AxKH.SetInputValue("종목코드", stockCode);
            ClsAxKH.AxKH.SetInputValue("기준일자", stdDate);
            ClsAxKH.AxKH.SetInputValue("수정주가구분", modifyJugaGb);
        }

        public static void Opt10060(string startDate, string stockCode, string amountQtyGb, string maeMaeGb, string unitGb)
        {
            ClsAxKH.AxKH.SetInputValue("일자", startDate);
            ClsAxKH.AxKH.SetInputValue("종목코드", stockCode);
            ClsAxKH.AxKH.SetInputValue("금액수량구분", amountQtyGb);
            ClsAxKH.AxKH.SetInputValue("매매구분", maeMaeGb);
            ClsAxKH.AxKH.SetInputValue("단위구분", unitGb);
        }

        private static object lockObject = new object();

        public static void SendCommRqData(OptType optType, ArrayList optCall, string sRQName, string sTrCode, int nPrevNext, string sScreenNo)
        {
            lock (lockObject)
            {
                ArrayList arrlist = new ArrayList();
                arrlist.Add(sRQName);
                arrlist.Add(sTrCode);
                arrlist.Add(nPrevNext);
                arrlist.Add(sScreenNo);

                AxKHQueueList.Enqueue(arrlist);

                ArrayList item;

                AxKHQueueList.TryDequeue(out item);

                switch (optType)
                {
                    case OptType.Opt10081:
                        Opt10081(optCall[0].ToString(), optCall[1].ToString(), optCall[2].ToString());
                        break;

                    case OptType.Opt10060:
                        Opt10060(optCall[0].ToString(), optCall[1].ToString(), optCall[2].ToString(), optCall[3].ToString(), optCall[4].ToString());
                        break;

                    default:
                        return;
                }

                int nRet = AxKH.CommRqData(item[0].ToString(), item[1].ToString(), Convert.ToInt32(item[2]), item[3].ToString());

                if (ClsWoomErrorCode.GetErrorMessage(nRet) == true)
                {
                    //   Logger(Log.일반, "[OPT10081] : " + lsWoomErrorCode.GetErrorMessage());
                }
                else
                {
                    MessageBox.Show(ClsWoomErrorCode.GetErrorMessage());
                    // Logger(Log.에러, "[OPT10081] : " + Error.GetErrorMessage());
                }
            }
        }
    }
}