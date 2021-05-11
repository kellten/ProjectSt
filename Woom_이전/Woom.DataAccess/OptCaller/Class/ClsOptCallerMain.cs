using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Woom.DataAccess.PlugIn;
using Woom.DataDefine.OptData;

namespace Woom.DataAccess.OptCaller.Class
{
    public static class ClsOptCallerMain
    {
        public static ConcurrentQueue<ArrayList> AxKHQueue = new ConcurrentQueue<ArrayList>();
        public static DateTime AxKHCanCallTime = DateTime.Now;
        private static bool _firstCaller = false;

        public delegate void OnReceivedEventHandler(string stockCode, DataTable dt, int sPreNext);

        public static event OnReceivedEventHandler AxKH_10081_OnReceived;
        public static event OnReceivedEventHandler AxKH_10060_OnReceived;

        public enum OptType
        { 
        Opt10081, Opt10060

        }

    


        public static async Task OptCommRqData(OptType optType, ArrayList arrayOpt, string sRQName, string sTrCode, int nPrevNext, string sScreenNo)
        {
            if (_firstCaller == false)
            {
                ClsAxKH.AxKH.OnReceiveTrData += AxKH_OnReceiveTrData;
                _firstCaller = true;

                AxKHCanCallTime = DateTime.Now;
            }
            else
            {
                if (AxKHCanCallTime < DateTime.Now)
                {
                    AxKHCanCallTime = DateTime.Now.AddSeconds(5);
                }
                else
                { AxKHCanCallTime = AxKHCanCallTime.AddSeconds(5); }
            }

            Task t = new Task(() =>
            {
                ArrayList arrlist = new ArrayList();

                arrlist.Add(AxKHCanCallTime);
                arrlist.Add(sRQName);
                arrlist.Add(sTrCode);
                arrlist.Add(nPrevNext);
                arrlist.Add(sScreenNo);

                AxKHQueue.Enqueue(arrlist);

                if (AxKHQueue.TryDequeue(out ArrayList item) == true)
                {

                    ClsAxKH.SendCommRqData(optType, arrayOpt, item[1].ToString(), item[2].ToString(), Convert.ToInt32(item[3]), item[4].ToString());

                    //AxKHCanCallTime = DateTime.Now.AddSeconds(5);

                    if (AxKHCanCallTime < DateTime.Now)
                    {
                        AxKHCanCallTime = DateTime.Now.AddSeconds(5);
                    }
                    else
                    { AxKHCanCallTime = AxKHCanCallTime.AddSeconds(5); }

                    //CallOptCommRqData(item[1].ToString(), item[2].ToString(), Convert.ToInt32(item[3]), item[4].ToString());
                }
                else
                {
                    return;
                }
            });

            await CheckCanCall();

            t.Start();

            await t;

            return;
        }

        private static async Task CheckCanCall()
        {
            Task t = new Task(() =>
            {
                if (AxKHCanCallTime < DateTime.Now)
                {
                    return;
                }
                else
                {
                    //lock (lockObject)
                    //{
                    while (true)
                    {
                        Thread.Sleep(1000);

                        if (AxKHCanCallTime < DateTime.Now)
                        {
                            break;
                        }
                    }
                    //}

                    return;
                }
            });

            t.Start();

            await t;
        }

        private static void CallOptCommRqData(string sRQName, string sTrCode, int nPrevNext, string sScreenNo)
        {
           
        }

        public static void AxKH_OnReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            DataTable dt = new DataTable();
            object handler;
            int nCnt = 0;

            nCnt = ClsAxKH.AxKH.GetRepeatCnt(e.sTrCode, e.sRQName);

            switch (e.sRQName)
            {
                case "주식일봉차트조회":
                    using (ClsColumnSets oBasicDataType = new ClsColumnSets())
                    {
                        foreach (int i in Enum.GetValues(typeof(ClsColumnSets.Column10081Index)))
                        {
                            int j = 0;
                            j = (int)Enum.Parse(typeof(ClsColumnSets.ColumnNameIndex), Enum.GetName(typeof(ClsColumnSets.Column10081Index), i));
                            // _dt.Columns.Add(oBasicDataType.GetDataColumn((ClsColumnSets.ColumnNameIndex)i));
                            dt.Columns.Add(oBasicDataType.GetDataColumn((ClsColumnSets.ColumnNameIndex)j));
                        }
                    };

                    handler = AxKH_10081_OnReceived;

                    if (nCnt == 0)
                    {
                        if (handler != null)
                        {
                            //_OptStatus.InitOptCallingStatus();
                            // Opt10081_OnReceived(_stockCode, null, 0);
                            return;
                        }
                    }

                    for (int i = 0; i < nCnt; i++)
                    {
                        DataRow dr = dt.NewRow();
                        for (int intColumName = 0; intColumName < dt.Columns.Count; intColumName++)
                        {
                            var type = dt.Columns[intColumName].DataType;
                            dr[dt.Columns[intColumName].ColumnName.ToString()] = Convert.ChangeType(ClsAxKH.AxKH.GetCommData(e.sTrCode, e.sRQName, i, dt.Columns[intColumName].ColumnName.ToString()).ToString().Trim(), type);
                        }

                        dt.Rows.Add(dr);
                    }

                    if (handler != null)
                    {
                        if (Convert.ToInt32(e.sPrevNext) != 2)
                        {
                            //_OptStatus.InitOptCallingStatus();
                        }
                        AxKH_10081_OnReceived(e.sTrCode, dt, Convert.ToInt32(e.sPrevNext));
                        return;
                    }

                    break;

                case "종목별투자자기관별차트요청":
                    using (ClsColumnSets oBasicDataType = new ClsColumnSets())
                    {
                        foreach (int i in Enum.GetValues(typeof(ClsColumnSets.Column10060Index)))
                        {
                            int j = 0;
                            j = (int)Enum.Parse(typeof(ClsColumnSets.ColumnNameIndex), Enum.GetName(typeof(ClsColumnSets.Column10060Index), i));
                            // _dt.Columns.Add(oBasicDataType.GetDataColumn((ClsColumnSets.ColumnNameIndex)i));
                            dt.Columns.Add(oBasicDataType.GetDataColumn((ClsColumnSets.ColumnNameIndex)j));
                        }
                    };
                    handler = AxKH_10060_OnReceived;

                    if (nCnt == 0)
                    {
                        if (handler != null)
                        {
                            //_OptStatus.InitOptCallingStatus();
                            // Opt10081_OnReceived(_stockCode, null, 0);
                            return;
                        }
                    }

                    for (int i = 0; i < nCnt; i++)
                    {
                        DataRow dr = dt.NewRow();
                        for (int intColumName = 0; intColumName < dt.Columns.Count; intColumName++)
                        {
                            var type = dt.Columns[intColumName].DataType;
                            dr[dt.Columns[intColumName].ColumnName.ToString()] = Convert.ChangeType(ClsAxKH.AxKH.GetCommData(e.sTrCode, e.sRQName, i, dt.Columns[intColumName].ColumnName.ToString()).ToString().Trim(), type);
                        }

                        dt.Rows.Add(dr);
                    }

                    if (handler != null)
                    {
                        if (Convert.ToInt32(e.sPrevNext) != 2)
                        {
                            //_OptStatus.InitOptCallingStatus();
                        }
                        AxKH_10081_OnReceived(e.sTrCode, dt, Convert.ToInt32(e.sPrevNext));
                        return;
                    }

                    break;

                default:
                    return;
            }
        }
    }
}