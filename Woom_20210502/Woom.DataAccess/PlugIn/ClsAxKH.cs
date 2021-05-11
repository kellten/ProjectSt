using System;
using System.Data;
using System.Collections;
using System.Collections.Concurrent;
using System.Windows.Forms;
using Woom.DataAccess.ErrorManage.Class;
using Woom.DataDefine.OptData;
using System.Threading.Tasks;
using Woom.DataAccess;

namespace Woom.DataAccess.PlugIn
{
    public static class ClsAxKH
    {
        public static AxKHOpenAPILib.AxKHOpenAPI AxKH;
        public static ConcurrentQueue<ArrayList> AxKHQueueList = new ConcurrentQueue<ArrayList>();

        public delegate void OnReceivedEventHandler(string stockCode, DataTable dt, int sPreNext);
        private static string _stockCode10081 = "";
        private static string _stockCode10060 = "";

        public static void AddOnReceivedEventHandler()
        {
            AxKH.OnReceiveTrData += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEventHandler(AxKH_OnReceiveTrData);
        }

        public enum OptType
        {
            Opt10081, Opt10060

        }

        public static event OnReceivedEventHandler AxKH_10081_OnReceived;
        public static event OnReceivedEventHandler AxKH_10060_OnReceived;

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

        private static DateTime Delay(int MS)

        {

            DateTime ThisMoment = DateTime.Now;

            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);

            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)

            {

                System.Windows.Forms.Application.DoEvents();

                ThisMoment = DateTime.Now;

            }

            return DateTime.Now;

        }

        public static string GetMasterListedStockDate(string stockCode)
        {
            string stockDate = "";
            stockDate = ClsAxKH.AxKH.GetMasterListedStockDate(stockCode);

            return stockDate;
        }

        public static string GetMasterCodeName(string stockCode)
        {
            string stockName = "";
            stockName = ClsAxKH.AxKH.GetMasterCodeName(stockCode);

            return stockName;
        }

        private static object lockObject = new object();

        private static DateTime _SendDateTime = DateTime.Now;
        private static int _sendCommRqDataCount = 0;
        private static DateTime firstSendTime;

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

                DateTime nowDate = DateTime.Now;

                TimeSpan dateDiff = _SendDateTime - nowDate;
                // 호출 횟수가 1시간에 천회가 넘어가면 에러 발생(1,2,3초)
                if (Convert.ToInt16(dateDiff.TotalSeconds) < 4)
                {
                    // 1초 = 2.6초 후 2초 = 1.6초 후 3초 = 3.6초후에 발생
                    Delay((3 - (Convert.ToInt16(dateDiff.TotalSeconds)) * 1000) + 600);
                }

                _SendDateTime = nowDate;

                if (_sendCommRqDataCount == 0)
                {
                    firstSendTime = DateTime.Now;
                }

                _sendCommRqDataCount = _sendCommRqDataCount + 1;


                switch (optType)
                {
                    case OptType.Opt10081:
                        _stockCode10081 = "";

                        _stockCode10081 = optCall[0].ToString();
                        Opt10081(optCall[0].ToString(), optCall[1].ToString(), optCall[2].ToString());
                        break;

                    case OptType.Opt10060:
                        _stockCode10060 = "";
                        _stockCode10060 = optCall[1].ToString();
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
                        //AxKH_10081_OnReceived(e.sTrCode, dt, Convert.ToInt32(e.sPrevNext));
                        AxKH_10081_OnReceived(_stockCode10081, dt, Convert.ToInt32(e.sPrevNext));
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
                        //AxKH_10060_OnReceived(e.sTrCode, dt, Convert.ToInt32(e.sPrevNext));
                        AxKH_10060_OnReceived(_stockCode10060, dt, Convert.ToInt32(e.sPrevNext));
                        return;
                    }

                    break;

                default:
                    return;
            }
        }
    }
}