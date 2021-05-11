﻿using System;
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
        private static string _stockCode10001 = "";
        private static string _stockCode10014 = "";
        private static string _stockCode10081 = "";
        private static string _stockCode10086 = "";
        private static string _stockCode10060 = "";
        private static string _stockCode20068 = "";

        public static void AddOnReceivedEventHandler()
        {
            AxKH.OnReceiveTrData += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEventHandler(AxKH_OnReceiveTrData);
        }

        public static void AddOnReceiveMsgEventHandler()
        {
            AxKH.OnReceiveMsg += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveMsgEventHandler(AxKH_OnReceiveMsg);
        }

        public static void AddOnReceiveRealDataEventHandler()
        {
            AxKH.OnReceiveRealData += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEventHandler(AxKH_OnReceiveRealData);
        }

        public enum OptType
        {
            Opt10001, Opt10081, Opt10060, Opt10014, Opt20068, Opt10086

        }

        public static event OnReceivedEventHandler AxKH_10001_OnReceived;
        public static event OnReceivedEventHandler AxKH_10014_OnReceived;
        public static event OnReceivedEventHandler AxKH_10081_OnReceived;
        public static event OnReceivedEventHandler AxKH_10086_OnReceived;
        public static event OnReceivedEventHandler AxKH_10060_OnReceived;
        public static event OnReceivedEventHandler AxKH_20068_OnReceived;

        public static void Opt10001(string stockCode)
        {
            ClsAxKH.AxKH.SetInputValue("종목코드", stockCode);
        }
        /// <summary>
        ///  [ opt10014 : 공매도추이요청 ]
        /// </summary>
        /// <param name="stockCode">종목코드 = 전문 조회할 종목코드</param>
        /// <param name="timeGb">시간구분 = 0:시작일, 1:기간</param>
        /// <param name="startDate">시작일자 = YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        /// <param name="endDate">종료일자 = YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        public static void Opt10014(string stockCode, string timeGb, string startDate, string endDate )
        {
            ClsAxKH.AxKH.SetInputValue("종목코드", stockCode);
            ClsAxKH.AxKH.SetInputValue("시간구분", timeGb);
            ClsAxKH.AxKH.SetInputValue("시작일자", startDate);
            ClsAxKH.AxKH.SetInputValue("종료일자", endDate);
            
        }
        public static void Opt10081(string stockCode, string stdDate, string modifyJugaGb)
        {
            ClsAxKH.AxKH.SetInputValue("종목코드", stockCode);
            ClsAxKH.AxKH.SetInputValue("기준일자", stdDate);
            ClsAxKH.AxKH.SetInputValue("수정주가구분", modifyJugaGb);
        }
        /// <summary>
        /// opt10086 : 일별주가요청
        /// </summary>
        /// <param name="stockCode"></param>
        /// <param name="stdDate"></param>
        /// <param name="modifyJugaGb">표시구분 = 0:수량, 1:금액(백만원)</param>
        public static void Opt10086(string stockCode, string stdDate, string displayGb)
        {
            ClsAxKH.AxKH.SetInputValue("종목코드", stockCode);
            ClsAxKH.AxKH.SetInputValue("조회일자", stdDate);
            ClsAxKH.AxKH.SetInputValue("표시구분", displayGb);
        }

        public static void Opt10060(string startDate, string stockCode, string amountQtyGb, string maeMaeGb, string unitGb)
        {
            ClsAxKH.AxKH.SetInputValue("일자", startDate);
            ClsAxKH.AxKH.SetInputValue("종목코드", stockCode);
            ClsAxKH.AxKH.SetInputValue("금액수량구분", amountQtyGb);
            ClsAxKH.AxKH.SetInputValue("매매구분", maeMaeGb);
            ClsAxKH.AxKH.SetInputValue("단위구분", unitGb);
        }
        /// <summary>
        ///  [ opt20068 : 대차거래추이요청(종목별) ]
        /// </summary>
        /// <param name="startDate">시작일자 = YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식</param>
        /// <param name="endDate">종료일자 = YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        /// <param name="allGb">전체구분 = 0:종목코드 입력종목만 표시, 1: 전체표시(지원안함. OPT10068사용).</param>
        /// <param name="stockCode">종목코드 = 전문 조회할 종목코드</param>
        public static void Opt20068(string startDate, string endDate, string allGb, string stockCode)
        {
            ClsAxKH.AxKH.SetInputValue("시작일자", startDate);
            ClsAxKH.AxKH.SetInputValue("종료일자", endDate);
            ClsAxKH.AxKH.SetInputValue("전체구분", allGb);
            ClsAxKH.AxKH.SetInputValue("종목코드", stockCode);
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

                TimeSpan dateDiff = nowDate - _SendDateTime;
                // 호출 횟수가 1시간에 천회가 넘어가면 에러 발생
                if (Convert.ToInt16(dateDiff.TotalSeconds) < 4)
                {
                    Delay(((4 - Convert.ToInt16(dateDiff.TotalSeconds)) * 1000) + 600);
                }

                _SendDateTime = nowDate;

                if (_sendCommRqDataCount == 0)
                {
                    firstSendTime = DateTime.Now;
                }

                _sendCommRqDataCount = _sendCommRqDataCount + 1;

                switch (optType)
                {
                    case OptType.Opt10001:
                        _stockCode10001 = "";

                        _stockCode10001 = optCall[0].ToString();
                        Opt10001(optCall[0].ToString());
                        break;
                    case OptType.Opt10081:
                        _stockCode10081 = "";

                        _stockCode10081 = optCall[0].ToString();
                        Opt10081(optCall[0].ToString(), optCall[1].ToString(), optCall[2].ToString());
                        break;
                    case OptType.Opt10086:
                        _stockCode10086 = "";

                        _stockCode10086 = optCall[0].ToString();
                        Opt10086(optCall[0].ToString(), optCall[1].ToString(), optCall[2].ToString());
                        break;

                    case OptType.Opt10060:
                        _stockCode10060 = "";
                        _stockCode10060 = optCall[1].ToString();
                        Opt10060(optCall[0].ToString(), optCall[1].ToString(), optCall[2].ToString(), optCall[3].ToString(), optCall[4].ToString());
                        break;

                    case OptType.Opt20068:
                        _stockCode20068 = "";
                        _stockCode20068 = optCall[3].ToString();
                        Opt20068(optCall[0].ToString(), optCall[1].ToString(), optCall[2].ToString(), optCall[3].ToString());
                        break;
                    default:
                        return;
                }
             
                
                int  nRet = AxKH.CommRqData(item[0].ToString(), item[1].ToString(), Convert.ToInt32(item[2]), item[3].ToString());
              

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

        /// <summary>
        /// 한번에 100종목까지 조회할 수 있는 복수종목 조회함수 입니다.
        /// 함수인자로 사용하는 종목코드 리스트는 조회하려는 종목코드 사이에 구분자';'를 추가해서 만들면 됩니다
        /// 수신되는 데이터는 TR목록에서 복수종목정보요청(OPTKWFID) Output을 참고하시면 됩니다.
        /// </summary>
        /// <param name="sArrCode">조회하려는 종목코드 리스트</param>
        /// <param name="bNext">연속조회 여부 0:기본값, 1:연속조회(지원안함)</param>
        /// <param name="nCodeCount">종목코드 갯수</param>
        /// <param name="nTypeFlag">0:주식 종목, 3:선물옵션 종목</param>
        /// <param name="sRQName">사용자 구분명</param>
        /// <param name="sScreenNo">화면번호</param>
        public static void CommKwRqData(string sArrCode, bool bNext, int nCodeCount, int nTypeFlag, string sRQName, string sScreenNo)
        {
            AxKH.CommKwRqData(sArrCode: sArrCode, bNext: 0, nCodeCount: nCodeCount, nTypeFlag: nTypeFlag, sRQName: sRQName, sScreenNo: sScreenNo);
        }

        public static void AxKH_OnReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            DataTable dt = new DataTable();
            object handler;
            int nCnt = 0;

            nCnt = ClsAxKH.AxKH.GetRepeatCnt(e.sTrCode, e.sRQName);
            
            switch (e.sRQName)
            {
                case "주식기본정보요청":
                    using (ClsColumnSets oBasicDataType = new ClsColumnSets())
                    {
                        foreach (int i in Enum.GetValues(typeof(ClsColumnSets.Column10001Index)))
                        {
                            int j = 0;
                            j = (int)Enum.Parse(typeof(ClsColumnSets.ColumnNameIndex), Enum.GetName(typeof(ClsColumnSets.Column10001Index), i));
                            dt.Columns.Add(oBasicDataType.GetDataColumn10001((ClsColumnSets.ColumnNameIndex)j));
                        }
                    };

                    handler = AxKH_10001_OnReceived;

                        DataRow dr2th = dt.NewRow();
                        for (int intColumName = 0; intColumName < dt.Columns.Count; intColumName++)
                        {
                            var type = dt.Columns[intColumName].DataType;

                            dr2th[dt.Columns[intColumName].ColumnName.ToString()] = Convert.ChangeType(ClsAxKH.AxKH.GetCommData(e.sTrCode, e.sRQName, 0, dt.Columns[intColumName].ColumnName.ToString()).ToString().Trim(), type);
                        }

                        dt.Rows.Add(dr2th);
                    
                    if (handler != null)
                    {
                        if (Convert.ToInt32(e.sPrevNext) != 2)
                        {
                            //_OptStatus.InitOptCallingStatus();
                        }
                        //AxKH_10081_OnReceived(e.sTrCode, dt, Convert.ToInt32(e.sPrevNext));
                        AxKH_10001_OnReceived(_stockCode10001, dt, Convert.ToInt32(e.sPrevNext));
                        return;
                    }

                    break;
                case "공매도추이요청 ":
                    using (ClsColumnSets oBasicDataType = new ClsColumnSets())
                    {
                        foreach (int i in Enum.GetValues(typeof(ClsColumnSets.Column10014Index)))
                        {
                            int j = 0;
                            j = (int)Enum.Parse(typeof(ClsColumnSets.ColumnNameIndex), Enum.GetName(typeof(ClsColumnSets.Column10014Index), i));
                            dt.Columns.Add(oBasicDataType.GetDataColumn((ClsColumnSets.ColumnNameIndex)j));
                        }
                    };
                    handler = AxKH_10014_OnReceived;

                    if (nCnt == 0)
                    {
                        if (handler != null)
                        {

                            AxKH_10014_OnReceived(_stockCode10014, null, 0);
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

                        AxKH_10014_OnReceived(_stockCode10014, dt, Convert.ToInt32(e.sPrevNext));
                        return;
                    }

                    break;
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
                            AxKH_10081_OnReceived(_stockCode10081, null, 0);
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
                            
                            AxKH_10060_OnReceived(_stockCode10060, null, 0);
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

                case "대차거래추이요청(종목별)":
                    using (ClsColumnSets oBasicDataType = new ClsColumnSets())
                    {
                        foreach (int i in Enum.GetValues(typeof(ClsColumnSets.Column20068Index)))
                        {
                            int j = 0;
                            j = (int)Enum.Parse(typeof(ClsColumnSets.ColumnNameIndex), Enum.GetName(typeof(ClsColumnSets.Column20068Index), i));
                            // _dt.Columns.Add(oBasicDataType.GetDataColumn((ClsColumnSets.ColumnNameIndex)i));
                            dt.Columns.Add(oBasicDataType.GetDataColumn((ClsColumnSets.ColumnNameIndex)j));
                        }
                    };
                    handler = AxKH_20068_OnReceived;

                    if (nCnt == 0)
                    {
                        if (handler != null)
                        {
                            AxKH_20068_OnReceived(_stockCode20068, null, 0);
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

                        AxKH_20068_OnReceived(_stockCode20068, dt, Convert.ToInt32(e.sPrevNext));
                        return;
                    }

                    break;

                default:
                    return;
            }
        }

        public static void AxKH_OnReceiveMsg(Object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveMsgEvent e)
        {

        }


        #region "화면번호관리"

        #endregion
        public static void AxKH_OnReceiveRealData(Object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEvent e)
        {
            switch (e.sRealKey.ToString().Trim())
            {
                case "주식시세":

                    break;
                case "주식체결":

                    break;
                case "주식우선호가":

                    break;
                case "주식호가잔량":

                    break;
                case "주식시간외호가":

                    break;
                case "주식당일거래원":

                    break;
                case "주식예상체결":

                    break;
                case "주식종목정보":

                    break;
                case "주식거래원":

                    break;
                default:
                    break;
            }
        }

    }
}