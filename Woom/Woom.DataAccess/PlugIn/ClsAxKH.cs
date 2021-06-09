using System;
using System.Data;
using System.Collections;
using System.Collections.Concurrent;
using System.Windows.Forms;
using Woom.DataAccess.ErrorManage.Class;
using Woom.DataDefine.OptData;
using Woom.DataDefine.Util;
using System.Threading.Tasks;
using Woom.DataAccess;
using Woom.DataAccess.Logger;
using System.Timers;

namespace Woom.DataAccess.PlugIn
{
    public static class ClsAxKH
    {
        
        public static AxKHOpenAPILib.AxKHOpenAPI AxKH;
        public static ConcurrentQueue<ArrayList> AxKHQueueList = new ConcurrentQueue<ArrayList>();

        public delegate void OnReceivedEventHandler(string stockCode, DataTable dt, int sPreNext);

        private static System.Timers.Timer aTimer;

        private static string SendsRQName = "";
        
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
            Opt10001, Opt10005, Opt10015, Opt10081, Opt10060, Opt10014, Opt20068, Opt10086, Opt90002

        }

        public static event OnReceivedEventHandler AxKH_10001_OnReceived;
        public static event OnReceivedEventHandler AxKH_10005_OnReceived;
        public static event OnReceivedEventHandler AxKH_10015_OnReceived;
        public static event OnReceivedEventHandler AxKH_10014_OnReceived;
        public static event OnReceivedEventHandler AxKH_10081_OnReceived;
        //public static event OnReceivedEventHandler AxKH_10086_OnReceived;
        public static event OnReceivedEventHandler AxKH_10060_OnReceived;
        public static event OnReceivedEventHandler AxKH_20068_OnReceived;
        public static event OnReceivedEventHandler AxKH_90002_OnReceived;

        public static void Opt10001(string stockCode)
        {
            ClsAxKH.AxKH.SetInputValue("종목코드", stockCode);
        }
        public static void Opt10005(string stockCode)
        {
            ClsAxKH.AxKH.SetInputValue("종목코드", stockCode);
        }
        /// <summary>
        ///  [ opt10015 : 일별거래상세요청 ]
        /// </summary>
        /// <param name="stockCode">종목코드</param>
        /// <param name="startDate">시작일자 = YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        public static void Opt10015(string stockCode, string startDate)
        {
            ClsAxKH.AxKH.SetInputValue("종목코드", stockCode);
            ClsAxKH.AxKH.SetInputValue("시작일자", startDate);
        }
        /// <summary>
        ///  [ opt10014 : 공매도추이요청 ]
        /// </summary>
        /// <param name="stockCode">종목코드 = 전문 조회할 종목코드</param>
        /// <param name="timeGb">시간구분 = 0:시작일, 1:기간</param>
        /// <param name="startDate">시작일자 = YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        /// <param name="endDate">종료일자 = YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        public static void Opt10014(string stockCode, string timeGb, string startDate, string endDate)
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
        /// <summary>
        /// 테마그룹별요청 
        /// </summary>
        /// <param name="dateGb"> 0:전체검색, 1:테마검색, 2:종목검색</param>
        /// <param name="kthCode">종목코드 = 검색하려는 종목코드</param>
        public static void Opt90002(string dateGb, string kthCode)
        {
            ClsAxKH.AxKH.SetInputValue("날짜구분", dateGb);
            ClsAxKH.AxKH.SetInputValue("종목코드", kthCode);
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
        //private static int _sendCommRqDataCount = 0;
        //private static DateTime firstSendTime;

        public static string RetStockCodeBysRqName(OptType optType, string sRQName)
        {
            string[] sRQNameArray = sRQName.ToString().Trim().Split(',');
            string stockCode = "";

            switch (optType)
            {
                case OptType.Opt10001:
                    stockCode = sRQNameArray[1];
                    break;
                case OptType.Opt10005:
                    stockCode = sRQNameArray[1];
                    break;
                case OptType.Opt10015:
                    stockCode = sRQNameArray[1];
                    break;
                case OptType.Opt10081:
                    stockCode = sRQNameArray[1];
                    break;
                case OptType.Opt10060:
                    stockCode = sRQNameArray[2];
                    break;
                case OptType.Opt10014:
                    stockCode = sRQNameArray[1];
                    break;
                case OptType.Opt20068:
                    stockCode = sRQNameArray[4];
                    break;
                case OptType.Opt10086:
                    stockCode = sRQNameArray[1];
                    break;
                case OptType.Opt90002:
                    stockCode = sRQNameArray[2];
                    break;
                default:
                    break;
            }

            return stockCode;
        }

        public static string RetStdDateBysRqName(OptType optType, string sRQName)
        {
            string[] sRQNameArray = sRQName.ToString().Trim().Split(',');
            string stdDate = "";

            switch (optType)
            {

                case OptType.Opt10015:
                    stdDate = sRQNameArray[2];
                    break;
                case OptType.Opt10081:
                    stdDate = sRQNameArray[2];
                    break;
                case OptType.Opt10060:
                    stdDate = sRQNameArray[1];
                    break;
                case OptType.Opt10014:
                    stdDate = sRQNameArray[3];
                    break;
                case OptType.Opt20068:
                    stdDate = sRQNameArray[1];
                    break;
                case OptType.Opt10086:
                    stdDate = sRQNameArray[2];
                    break;
                default:
                    break;
            }

            return stdDate;
        }

        private static void RegularSpandTime()
        {
            DateTime nowDate = DateTime.Now;

            TimeSpan dateDiff = nowDate - _SendDateTime;
            // 호출 횟수가 1시간에 천회가 넘어가면 에러 발생
            if (Convert.ToInt16(dateDiff.TotalSeconds) < 4)
            {
                Delay(((4 - Convert.ToInt16(dateDiff.TotalSeconds)) * 1000) + 600);
            }

            _SendDateTime = nowDate;

            //if (_sendCommRqDataCount == 0)
            //{
            //    firstSendTime = DateTime.Now;
            //}

            //_sendCommRqDataCount = _sendCommRqDataCount + 1;
        }

        public static bool SPEED_CALL = false;

        public static DateTime _limitTime;
        private static bool _firstCall = false;
        public static int _limitCount = 999;
        private static void SpeedSpendTime()
        {
            // 1시간 동안 1천회 전송 가능하므로 초기 값을 1천회 세팅
            if (_firstCall == false)
            {
                _firstCall = true;

                _limitTime = DateTime.Now.AddHours(1);
                _limitCount = 999;
            }
            else
            {
                DateTime nowDate = DateTime.Now;

                TimeSpan dateDiff = nowDate - _SendDateTime;
                // 호출 횟수가 1시간에 천회가 넘어가면 에러 발생
                if (Convert.ToInt16(dateDiff.TotalSeconds) < 1)
                {
                    Delay(600);
                }

                _SendDateTime = nowDate;
            }
            // 1천회 세팅한 값보다 현재 시간이 같거나 많으면 다시 리미트 타임을 1시간 뒤로 세팅
            if (DateTime.Now >= _limitTime)
            {
                _limitTime = DateTime.Now.AddHours(1);
                _limitCount = 999;
            }
            else
            {
                _limitCount = _limitCount - 1;
            }

            if (_limitCount == 0)
            {
                TimeSpan dateDiff = _limitTime - DateTime.Now;
                Delay(Convert.ToInt16(dateDiff.TotalSeconds) * 1000);
            }
        }
        public static void SendCommRqData(OptType optType, ArrayList optCall, string sRQName, string sTrCode, int nPrevNext, string sScreenNo)
        {
            lock (lockObject)
            {


                if (SPEED_CALL == true)
                {
                    SpeedSpendTime();
                }
                else
                {
                    RegularSpandTime();
                }

                string stockCode = "";

                ArrayList arrlist = new ArrayList();
                arrlist.Add(sRQName);
                arrlist.Add(sTrCode);
                arrlist.Add(nPrevNext);
                arrlist.Add(sScreenNo);

                AxKHQueueList.Enqueue(arrlist);

                ArrayList item;

                AxKHQueueList.TryDequeue(out item);

                string sRQNameSet = "";

                switch (optType)
                {

                    case OptType.Opt10001:
                        //_stockCode10001 = "";

                        //_stockCode10001 = optCall[0].ToString();
                        stockCode = optCall[0].ToString();
                        Opt10001(stockCode: optCall[0].ToString());

                        break;
                    case OptType.Opt10005:
                        //_stockCode10005 = "";

                        //_stockCode10005 = optCall[0].ToString();

                        stockCode = optCall[0].ToString();
                        Opt10005(stockCode: optCall[0].ToString());
                        break;

                    case OptType.Opt10015:
                        //_stockCode10015 = "";

                        //_stockCode10015 = optCall[0].ToString();
                        stockCode = optCall[0].ToString();
                        Opt10015(stockCode: optCall[0].ToString(), startDate: optCall[1].ToString());
                        break;
                    case OptType.Opt10081:
                        //_stockCode10081 = "";

                        //_stockCode10081 = optCall[0].ToString();
                        stockCode = optCall[0].ToString();
                        Opt10081(stockCode: optCall[0].ToString(), stdDate: optCall[1].ToString(), modifyJugaGb: optCall[2].ToString());
                        break;
                    case OptType.Opt10086:
                        // _stockCode10086 = "";

                        //_stockCode10086 = optCall[0].ToString();
                        stockCode = optCall[0].ToString();
                        Opt10086(stockCode: optCall[0].ToString(), stdDate: optCall[1].ToString(), displayGb: optCall[2].ToString());
                        break;

                    case OptType.Opt10060:
                        // _stockCode10060 = "";
                        //_stockCode10060 = optCall[1].ToString();
                        stockCode = optCall[1].ToString();
                        Opt10060(startDate: optCall[0].ToString(), stockCode: optCall[1].ToString(), amountQtyGb: optCall[2].ToString(), maeMaeGb: optCall[3].ToString(), unitGb: optCall[4].ToString());
                        break;

                    case OptType.Opt20068:
                        //_stockCode20068 = "";
                        //_stockCode20068 = optCall[3].ToString();
                        stockCode = optCall[3].ToString();
                        Opt20068(startDate: optCall[0].ToString(), endDate: optCall[1].ToString(), allGb: optCall[2].ToString(), stockCode: optCall[3].ToString());
                        break;

                    case OptType.Opt90002:
                        //_kthCode90002 = "";
                        //_kthCode90002 = optCall[1].ToString();
                        stockCode = optCall[1].ToString();
                        Opt90002(dateGb: optCall[0].ToString(), kthCode: optCall[1].ToString());
                        break;
                    default:
                        return;
                }

                for (int i = 0; i < optCall.Count; i++)
                {
                    if (sRQNameSet == "")
                    {
                        sRQNameSet = optCall[i].ToString();
                    }
                    else
                    {
                        sRQNameSet = sRQNameSet + "," + optCall[i].ToString();
                    }
                }

                ClsDbLogger.StoreLogger(loggergb: ClsDbLogger.LoggerGb.SendLoger, optCallNo: item[0].ToString() + " : " + "SetInputValue", transText: sRQNameSet);

                string sCommRqData = "";

                for (int i = 0; i < item.Count; i++)
                {
                    if (sCommRqData == "")
                    {
                        sCommRqData = item[i].ToString();
                    }
                    else
                    {
                        sCommRqData = sCommRqData + "," + item[i].ToString();
                    }
                }

                ClsDbLogger.StoreLogger(loggergb: ClsDbLogger.LoggerGb.SendLoger, optCallNo: item[0].ToString() + " : " + "CommRqData", transText: sRQNameSet + "@" + sCommRqData);

                int nRet = AxKH.CommRqData(sRQName: item[0].ToString() + "," + sRQNameSet, sTrCode: item[1].ToString(), nPrevNext: Convert.ToInt32(item[2]), sScreenNo: item[3].ToString());

                //_CallOptTime = DateTime.Now; 

                SendsRQName = item[0].ToString() + "," + sRQNameSet;

                if (aTimer.Enabled == false)
                {
                    StartTimer();
                }

                if (ClsWoomErrorCode.GetErrorMessage(nRet) == true)
                {
                    //   Logger(Log.일반, "[OPT10081] : " + lsWoomErrorCode.GetErrorMessage());
                }
                else
                {
                    string erroText = ClsWoomErrorCode.GetErrorMessage();
                    ClsDbLogger.StoreLogger(loggergb: ClsDbLogger.LoggerGb.ErrorLoger, optCallNo: item[0].ToString(), transText: erroText);

                    // Logger(Log.에러, "[OPT10081] : " + Error.GetErrorMessage());
                }
            }
        }
        public static void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(10000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            //aTimer.Enabled = true;
        }

        private static void StartTimer()
        {
            aTimer.Enabled = true;
            aTimer.Start();
        }


        private static void StopTimer()
        {
            aTimer.Enabled = false;
            aTimer.Stop();
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            string[] sRQNameArray = SendsRQName.ToString().Trim().Split(',');
            object handler;
            switch (sRQNameArray[0].ToString().Trim())
            {
                case "종목별투자자기관별차트요청":
                    handler = AxKH_10060_OnReceived;
                    AxKH_10060_OnReceived(SendsRQName.ToString().Trim(), null, 0);
                    return;                    
                default:
                    break;
            }

            aTimer.Stop();

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
            StopTimer();
                 
            DataTable dt = new DataTable();
            object handler;
            int nCnt = 0;

            nCnt = ClsAxKH.AxKH.GetRepeatCnt(e.sTrCode, e.sRQName);

            ClsUtil clsUtil = new ClsUtil();

            //stockCode = clsUtil.Mid(e.sRQName, 1, 6);
            //sRQName = e.sRQName.ToString().Replace(stockCode + "_", "");

            string[] sRQNameArray = e.sRQName.ToString().Trim().Split(',');

            ClsDbLogger.StoreLogger(loggergb: ClsDbLogger.LoggerGb.RecieveLoger, optCallNo: "AxKH_OnReceiveTrData", transText: e.sTrCode.ToString().Trim() + "@" + e.sRQName.ToString().Trim());

            switch (sRQNameArray[0].ToString().Trim())
            {
                case "주식기본정보요청":

                    try
                    {

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
                            AxKH_10001_OnReceived(e.sRQName.ToString().Trim(), dt, Convert.ToInt32(e.sPrevNext));
                            return;
                        }

                        break;
                    }
                    catch (Exception)
                    {
                        AxKH_10001_OnReceived(e.sRQName.ToString().Trim(), null, Convert.ToInt32(e.sPrevNext));
                        throw;
                    }

                case "주식일주월시분요청":
                    using (ClsColumnSets oBasicDataType = new ClsColumnSets())
                    {
                        foreach (int i in Enum.GetValues(typeof(ClsColumnSets.Column10005Index)))
                        {
                            int j = 0;
                            j = (int)Enum.Parse(typeof(ClsColumnSets.ColumnNameIndex), Enum.GetName(typeof(ClsColumnSets.Column10005Index), i));
                            dt.Columns.Add(oBasicDataType.GetDataColumn10001((ClsColumnSets.ColumnNameIndex)j));
                        }
                    };

                    handler = AxKH_10005_OnReceived;


                    if (nCnt == 0)
                    {
                        if (handler != null)
                        {
                            //_OptStatus.InitOptCallingStatus();
                            AxKH_10005_OnReceived(e.sRQName.ToString().Trim(), null, 0);
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

                        AxKH_10005_OnReceived(e.sRQName.ToString().Trim(), dt, Convert.ToInt32(e.sPrevNext));
                        return;
                    }


                    break;
                case "일별거래상세요청":
                    using (ClsColumnSets oBasicDataType = new ClsColumnSets())
                    {
                        foreach (int i in Enum.GetValues(typeof(ClsColumnSets.Column10015Index)))
                        {
                            int j = 0;
                            j = (int)Enum.Parse(typeof(ClsColumnSets.ColumnNameIndex), Enum.GetName(typeof(ClsColumnSets.Column10015Index), i));
                            dt.Columns.Add(oBasicDataType.GetDataColumn((ClsColumnSets.ColumnNameIndex)j));
                        }
                    };

                    handler = AxKH_10015_OnReceived;

                    if (nCnt == 0)
                    {
                        if (handler != null)
                        {
                            //_OptStatus.InitOptCallingStatus();
                            AxKH_10015_OnReceived(e.sRQName.ToString().Trim(), null, 0);
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
                        //AxKH_10015_OnReceived(e.sTrCode, dt, Convert.ToInt32(e.sPrevNext));
                        AxKH_10015_OnReceived(e.sRQName.ToString().Trim(), dt, Convert.ToInt32(e.sPrevNext));
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
                            //_OptStatus.InitOptCallingStatus();
                            AxKH_10014_OnReceived(e.sRQName.ToString().Trim(), null, 0);
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

                        AxKH_10014_OnReceived(e.sRQName.ToString().Trim(), dt, Convert.ToInt32(e.sPrevNext));
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
                            AxKH_10081_OnReceived(e.sRQName.ToString().Trim(), null, 0);
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
                        AxKH_10081_OnReceived(e.sRQName.ToString().Trim(), dt, Convert.ToInt32(e.sPrevNext));
                        return;
                    }

                    break;

                case "종목별투자자기관별차트요청":
                    try
                    {


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
                                AxKH_10060_OnReceived(e.sRQName.ToString().Trim(), null, 0);
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

                            AxKH_10060_OnReceived(e.sRQName.ToString().Trim(), dt, Convert.ToInt32(e.sPrevNext));
                            return;
                        }
                        else
                        {
                            AxKH_10060_OnReceived(e.sRQName.ToString().Trim(), null, 0);
                            return;
                        }


                        break;

                    }
                    catch (Exception)
                    {
                        AxKH_10060_OnReceived(e.sRQName.ToString().Trim(), null, 0);
                        break;
                        throw;

                    }



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
                            //_OptStatus.InitOptCallingStatus();
                            AxKH_20068_OnReceived(e.sRQName.ToString().Trim(), null, 0);
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

                        AxKH_20068_OnReceived(e.sRQName.ToString().Trim(), dt, Convert.ToInt32(e.sPrevNext));
                        return;
                    }

                    break;
                case "테마그룹별요청":
                    using (ClsColumnSets oBasicDataType = new ClsColumnSets())
                    {
                        foreach (int i in Enum.GetValues(typeof(ClsColumnSets.Column90002Index)))
                        {
                            int j = 0;
                            j = (int)Enum.Parse(typeof(ClsColumnSets.ColumnNameIndex), Enum.GetName(typeof(ClsColumnSets.Column90002Index), i));
                            // _dt.Columns.Add(oBasicDataType.GetDataColumn((ClsColumnSets.ColumnNameIndex)i));
                            dt.Columns.Add(oBasicDataType.GetDataColumn((ClsColumnSets.ColumnNameIndex)j));
                        }
                    };
                    handler = AxKH_90002_OnReceived;

                    if (nCnt == 0)
                    {
                        if (handler != null)
                        {
                            //_OptStatus.InitOptCallingStatus();
                            AxKH_90002_OnReceived(e.sRQName.ToString().Trim(), null, 0);
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

                        AxKH_90002_OnReceived(e.sRQName.ToString().Trim(), dt, Convert.ToInt32(e.sPrevNext));
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

        public static void SetRealReg(string screenNo, string codeList, string fidList, string realType)
        {
            AxKH.SetRealReg(screenNo, codeList, fidList, realType);
        }

        public static void DisconnectRealData(string ScreenNo)
        {
            AxKH.DisconnectRealData(ScreenNo);
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