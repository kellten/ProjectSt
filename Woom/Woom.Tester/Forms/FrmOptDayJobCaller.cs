using SDataAccess;
using System;
using System.Collections;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Woom.DataAccess;
using Woom.DataAccess.OptCaller.Class;
using Woom.DataAccess.PlugIn;
using Woom.DataDefine.Util;

namespace Woom.Tester.Forms
{
    public partial class FrmOptDayJobCaller : Form
    {
        private ClsDataAccessUtil _clsDataAccessUtil;

        #region 전역변수
        private DataTable _dtStockCode;
        private int _seqNo = 0;
        private string _stdDate = "";
        private string _FormId = "99";
        private string _FirstStockDate = ""; // 상장일

        private ClsOpt10015 _opt10015 = new ClsOpt10015();
        private ClsOpt10081 _opt10081 = new ClsOpt10081();
        private ClsOpt10060 _opt10060 = new ClsOpt10060();

        private Queue _StockQueue = new Queue();
        private string _minDate = "";

        #endregion 전역변수
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt">작업할 종목들</param>
        /// <param name="stdDate">기준일로부터 최근 3개월</param>
        public FrmOptDayJobCaller(DataTable dt, String stdDate)
        {
            InitializeComponent();
            _stdDate = stdDate;
            _clsDataAccessUtil = new ClsDataAccessUtil();

            ClsAxKH.AxKH_10015_OnReceived += new ClsAxKH.OnReceivedEventHandler(Opt10015_OnReceived);
            ClsAxKH.AxKH_10081_OnReceived += new ClsAxKH.OnReceivedEventHandler(Opt10081_OnReceived);

            if (dt == null)
            {
                Func<DataTable> funcGetStockData = () =>
                {
                    RichQuery oRichQuery = new RichQuery();
                    return oRichQuery.p_ScodeQuery("1", "", "", false).Tables[0].Copy();
                };

                _dtStockCode = funcGetStockData();
            }
            else
            {
                Func<DataTable> funcGetStockData = () =>
                {
                    RichQuery oRichQuery = new RichQuery();
                    return oRichQuery.p_ScodeQuery("1", "", "", false).Tables[0].Copy();
                };

                _dtStockCode = dt.Copy();
                dt = null;
            }

            foreach (DataRow dr in _dtStockCode.Rows)
            {
                if (ClsAxKH.GetMasterCodeName(dr["STOCK_CODE"].ToString().Trim()) == "")
                {
                    continue;
                }

                _StockQueue.Enqueue(dr["STOCK_CODE"].ToString());
            }

            proBar10015.Maximum = _dtStockCode.Rows.Count;
            proBar10060.Maximum = _dtStockCode.Rows.Count;
            proBar10081.Maximum = _dtStockCode.Rows.Count;

            if (_stdDate == "")
            { 

                if (System.DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                {
                    _stdDate = DateTime.Today.AddDays(-1).ToString("yyyyMMdd");
                }
                else if (System.DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                {
                    _stdDate = DateTime.Today.AddDays(-2).ToString("yyyyMMdd");
                }
                else
                {
                    int i = Int32.Parse(System.DateTime.Now.ToString("HH") + System.DateTime.Now.ToString("ss"));

                    if (i > 1600)
                    { _stdDate = CDateTime.FormatDate(System.DateTime.Now.Date.ToShortDateString()); }
                    else if (System.DateTime.Now.DayOfWeek == DayOfWeek.Monday)
                    {
                        _stdDate = DateTime.Today.AddDays(-3).ToString("yyyyMMdd");
                    }
                    else
                    {
                        _stdDate = DateTime.Today.AddDays(-1).ToString("yyyyMMdd");
                    }
                }
            }

            string reDate = "";
            ClsUtil clsUtil = new ClsUtil();

            reDate = clsUtil.Mid(_stdDate, 1, 4) + "-" + clsUtil.Mid(_stdDate, 5, 2) + clsUtil.Mid(_stdDate, 7, 2);

            DateTime dtDate = Convert.ToDateTime(reDate);

            _minDate = dtDate.AddDays(Convert.ToInt32(DayOfWeek.Monday) - Convert.ToInt32(dtDate.DayOfWeek)).ToString("yyyyMMdd");

            proBar10015.Value = _StockQueue.Count;
            proBar10081.Value = _StockQueue.Count;
            proBar10060.Value = _StockQueue.Count;

            OnGetStockCode();

            BtnStart.Text = "작업 중";

        }
        private void WaitTime()
        {
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();
            //Task.Delay(3000).Wait();
            _clsDataAccessUtil.Delay(3600);
            tcs.SetResult(true);
        }
        private void WriteTextSafe(string strMessage)
        {
            if (lblStockName.InvokeRequired)
            {
                Invoke((MethodInvoker)delegate ()
                {
                    WriteTextSafe(strMessage);
                });
            }
            else
            {
                lblStockName.Text = strMessage;
            }
        }
        private void OnGetStockCode()
        {
            string strStockCode = "";

            strStockCode = GetStockCode();

            if (strStockCode == "End")
            {
                BtnStart.Text = "작업 완료";
                return;
            }

            _seqNo = _seqNo + 1;
            proBar10015.Value = _seqNo;
            proBar10081.Value = _seqNo;
            proBar10060.Value = _seqNo;

            if (strStockCode == "")
            {
                OnGetStockCode();
                return;
            }

            WaitTime();

            string stockName = ClsAxKH.GetMasterCodeName(stockCode: strStockCode);

            // 종목명을 못 가져오면 상장폐지된 종목으로 생각.
            if (stockName == "")
            {
                OnGetStockCode();
                return;
            }

            if (_NoJob10015 != true)
            {
                GetOpt10015Caller(strStockCode, _stdDate, "");
            }
            else if (_NoJob10081 != true)
            {
                GetOpt10081Caller(strStockCode);
            }
            else if (_NoJob10060 != true)
            {
                GetOpt10060Caller( Opt10060TransType.PriceMaesu, strStockCode, "", "");
            }
            else
            {
                OnGetStockCode();
            }

            WriteTextSafe(strStockCode + " 작업 중");
            //   tcs.SetResult(true);
        }
        private void GetOpt10015Caller(string stockCode, string MaxDate, string MinDate)
        {
            if (_opt10015 != null)
            {
                _opt10015.Dispose();
                _opt10015 = null;
            }

            _opt10015 = new ClsOpt10015();

            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            if (tcs == null || tcs.Task.IsCompleted)
            {
                return;
            }

            _opt10015.SetInit(_FormId);
            _opt10015.JustRequest(stockCode, _stdDate, "", 0);

            tcs.SetResult(true);

        }
        private void GetOpt10081Caller(string stockCode)
        {
            if (_NoJob10081 != true)
            {
                if (_opt10081 != null)
                {
                    _opt10081.Dispose();
                    _opt10081 = null;
                }

                _opt10081 = new ClsOpt10081();

                TaskCompletionSource<bool> tcs = null;
                tcs = new TaskCompletionSource<bool>();

                if (tcs == null || tcs.Task.IsCompleted)
                {
                    return;
                }

                _opt10081.SetInit(_FormId);
                _opt10081.Opt10081(stockCode, "", _stdDate, "1");

                tcs.SetResult(true);
            }
            else
            { 
            GetOpt10060Caller(Opt10060TransType.PriceMaesu, stockCode: stockCode, MaxDate: "", MinDate: "");
            }
        }
        private enum Opt10060TransType
        {
            PriceMaesu, PriceMaedo, QtyMaesu, QtyMaeDo
        }

        private void GetOpt10060Caller(Opt10060TransType opt10060TransType, string stockCode, string MaxDate, string MinDate)
        {
            if (_opt10060 != null)
            {
                _opt10060.Dispose();
                _opt10060 = null;
            }
            _opt10060 = new ClsOpt10060();
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();
            if (tcs == null || tcs.Task.IsCompleted)
            {
                return;
            }

            DataTable dtDate = new DataTable();
            KiwoomQuery kiwoom = new KiwoomQuery();
           
            _opt10060.SetInit(_FormId);

            switch (opt10060TransType)
            {
                case Opt10060TransType.PriceMaesu:
                    WriteTextSafe(stockCode + "(" + ClsAxKH.GetMasterCodeName(stockCode) + ")" + " Price(매수)_" + _StockQueue.Count.ToString());
                    ClsAxKH.AxKH_10060_OnReceived += new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060PriceMaeSu);
                    _opt10060.JustRequest(StartDate: _stdDate, StockCode: stockCode, StockName: "", AmountQtyGb: "1", MaeMaeGb: "1", UnitG: "", nPrevNext: 0);
                    break;
                case Opt10060TransType.PriceMaedo:
                    WaitTime();
                    WriteTextSafe(stockCode + " Price(매도)_" + _StockQueue.Count.ToString());
                    ClsAxKH.AxKH_10060_OnReceived += new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060PriceMaedo);
                    _opt10060.JustRequest(StartDate: _stdDate, StockCode: stockCode, StockName: "", AmountQtyGb: "1", MaeMaeGb: "2", UnitG: "", nPrevNext: 0);
                    break;
                case Opt10060TransType.QtyMaesu:
                    WaitTime();
                    WriteTextSafe(stockCode + " QTY(매수)_" + _StockQueue.Count.ToString());
                    ClsAxKH.AxKH_10060_OnReceived += new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060MaeSu);
                    _opt10060.JustRequest(StartDate: _stdDate, StockCode: stockCode, StockName: "", AmountQtyGb: "2", MaeMaeGb: "1", UnitG: "", nPrevNext: 0);
                    break;
                case Opt10060TransType.QtyMaeDo:
                    WaitTime();
                    WriteTextSafe(stockCode + " QTY(매도)_" + _StockQueue.Count.ToString());
                    ClsAxKH.AxKH_10060_OnReceived += new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060Maedo);
                    _opt10060.JustRequest(StartDate: _stdDate, StockCode: stockCode, StockName: "", AmountQtyGb: "2", MaeMaeGb: "2", UnitG: "", nPrevNext: 0);
                    break;
                default:
                    break;
            }

            tcs.SetResult(true);

        }

        private bool _NoJob10015 = false;
        private bool _NoJob10060 = false;
        private bool _NoJob10081 = false;

        private string GetStockCode()
        {
            string reValue;

            _NoJob10015 = false;
            _NoJob10060 = false;
            _NoJob10081 = false;

            if (_StockQueue.Count == 0)
            {
                MessageBox.Show("작업이 완료되었습니다.");
                return "End";
            }
            reValue = _StockQueue.Dequeue().ToString();

            KiwoomQuery kiwoomQuery = new KiwoomQuery();
            DataTable dt = new DataTable();

            dt = kiwoomQuery.p_Opt10015Query("3", reValue.ToString().Trim(), "", "", "", false).Tables[0].Copy();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["MAX_STOCK_DATE"].ToString().Trim() != "" && dt.Rows[0]["MIN_STOCK_DATE"].ToString().Trim() != "")
                {
                    if (dt.Rows[0]["MAX_STOCK_DATE"].ToString().Trim() == _stdDate && Convert.ToInt16(dt.Rows[0]["MIN_STOCK_DATE"].ToString().Trim()) < Convert.ToInt16(_minDate))
                    {
                        _NoJob10015 = true;
                    }
                }   
            }

            dt = null;

            dt = kiwoomQuery.p_Opt10060MinMaxQuery("1", reValue.ToString().Trim(), "", "", false).Tables[0].Copy();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["MAESU_PRICE"].ToString().Trim() != "19990101" && dt.Rows[0]["MEADO_PRICE"].ToString().Trim() != "19990101" && dt.Rows[0]["MAESU_QTY"].ToString().Trim() != "19990101" && dt.Rows[0]["MAEDO_QTY"].ToString().Trim() != "19990101")
                {
                    if (dt.Rows[0]["MAESU_PRICE"].ToString().Trim() != _stdDate && dt.Rows[0]["MEADO_PRICE"].ToString().Trim() != _stdDate && dt.Rows[0]["MAESU_QTY"].ToString().Trim() != _stdDate && dt.Rows[0]["MAEDO_QTY"].ToString().Trim() != _stdDate)
                    {
                        _NoJob10060 = true;
                    }
                }
            }

            dt = null;

            dt = kiwoomQuery.p_Opt10060MinMaxQuery("2", reValue.ToString().Trim(), "", "", false).Tables[0].Copy();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["MAESU_PRICE"].ToString().Trim() != "19990101" && dt.Rows[0]["MEADO_PRICE"].ToString().Trim() != "19990101" && dt.Rows[0]["MAESU_QTY"].ToString().Trim() != "19990101" && dt.Rows[0]["MAEDO_QTY"].ToString().Trim() != "19990101")
                {
                    if (Convert.ToInt16(dt.Rows[0]["MAESU_PRICE"].ToString().Trim()) < Convert.ToInt16(_minDate) && Convert.ToInt16(dt.Rows[0]["MEADO_PRICE"].ToString().Trim()) < Convert.ToInt16(_minDate) && Convert.ToInt16(dt.Rows[0]["MAESU_QTY"].ToString().Trim()) < Convert.ToInt16(_minDate) && Convert.ToInt16(dt.Rows[0]["MAEDO_QTY"].ToString().Trim()) < Convert.ToInt16(_minDate))
                    {
                        _NoJob10060 = true;
                    }
                }
            }

            dt = null;

            dt = kiwoomQuery.p_Opt10060MinMaxQuery("2", reValue.ToString().Trim(), "", "", false).Tables[0].Copy();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["MAESU_PRICE"].ToString().Trim() != "19990101" && dt.Rows[0]["MEADO_PRICE"].ToString().Trim() != "19990101" && dt.Rows[0]["MAESU_QTY"].ToString().Trim() != "19990101" && dt.Rows[0]["MAEDO_QTY"].ToString().Trim() != "19990101")
                {
                    if (Convert.ToInt16(dt.Rows[0]["MAESU_PRICE"].ToString().Trim()) < Convert.ToInt16(_minDate) && Convert.ToInt16(dt.Rows[0]["MEADO_PRICE"].ToString().Trim()) < Convert.ToInt16(_minDate) && Convert.ToInt16(dt.Rows[0]["MAESU_QTY"].ToString().Trim()) < Convert.ToInt16(_minDate) && Convert.ToInt16(dt.Rows[0]["MAEDO_QTY"].ToString().Trim()) < Convert.ToInt16(_minDate))
                    {
                        _NoJob10060 = true;
                    }
                }
            }

            dt = null;

            dt = kiwoomQuery.p_Opt10081MinMaxQuery("3", reValue.ToString().Trim(), "",   false).Tables[0].Copy();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["MAX_STOCK_DATE"].ToString().Trim() != "" && dt.Rows[0]["MIN_STOCK_DATE"].ToString().Trim() != "")
                {
                    if (dt.Rows[0]["MAX_STOCK_DATE"].ToString().Trim() == _stdDate && Convert.ToInt16(dt.Rows[0]["MIN_STOCK_DATE"].ToString().Trim()) < Convert.ToInt16(_minDate))
                    {
                        _NoJob10081 = true;
                    }
                }
            }

            dt = null;

            return reValue;
        }
        private void Opt10015_OnReceived(string stockCode, DataTable dt, int sPreNext)
        {

            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            if (tcs == null || tcs.Task.IsCompleted)
            {
                return;
            }
            // 최근 100일 내역만 질의한다.
            if (dt != null)
            {
                ArrayParam arrParam = new ArrayParam();
                Sql oSql = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "KIWOOMDB");

                foreach (DataRow dr in dt.Rows)
                {
                    WriteTextSafe(stockCode + "(일자별상세)" + "[" + dr["일자"].ToString() + "] 작업중");

                    arrParam.Clear();
                    arrParam.Add("@ACTION_GB", "A");
                    arrParam.Add("@STOCK_CODE", stockCode);
                    arrParam.Add("@STOCK_DATE", dr["일자"].ToString());
                    arrParam.Add("@LAST_PRICE", dr["종가"]);
                    arrParam.Add("@OAGO_DAEBI_SYMBOL", dr["전일대비기호"]);
                    arrParam.Add("@OAGO_DAEBI", dr["전일대비"]);
                    arrParam.Add("@UPDOWN_RATE", dr["등락율"]);
                    arrParam.Add("@TRADE_QTY", dr["거래량"]);
                    arrParam.Add("@TRADE_DAEGUM", dr["거래대금"]);
                    arrParam.Add("@BETRADE_QTY", dr["장전거래량"]);
                    arrParam.Add("@BETRADE_BIJUNG", dr["장전거래비중"]);
                    arrParam.Add("@INTRADE_QTY", dr["장중거래량"]);
                    arrParam.Add("@INTRADE_BIJUNG", dr["장중거래비중"]);
                    arrParam.Add("@AFTRADE_QTY", dr["장후거래량"]);
                    arrParam.Add("@AFTRADE_BIJUNG", dr["장후거래비중"]);
                    arrParam.Add("@SUM3", dr["합계3"]);
                    arrParam.Add("@GITRADE_QTY", dr["기간중거래량"]);
                    arrParam.Add("@BETRADE_DAEGUM", dr["장전거래대금"]);
                    arrParam.Add("@BETRADE_DBIJUNG", dr["장전거래대금비중"]);
                    arrParam.Add("@INTRADE_DAEGUM", dr["장중거래대금"]);
                    arrParam.Add("@INTRADE_DBIJUNG", dr["장중거래대금비중"]);
                    arrParam.Add("@AFTRADE_DAEGUM", dr["장후거래대금"]);
                    arrParam.Add("@AFTRADE_DBIJUNG", dr["장후거래대금비중"]);
                    arrParam.Add("@DEUNG_DATE", "");
                    arrParam.Add("@DEUNG_TIME", "");
                    arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                    oSql.ExecuteNonQuery("p_Opt10015Add", CommandType.StoredProcedure, arrParam);

                }
            }

            _opt10015.Dispose();
            tcs.SetResult(true);

            GetOpt10081Caller(stockCode: stockCode);
        }
        private void Opt10081_OnReceived(string stockCode, DataTable dt, int sPreNext)
        {

            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            if (tcs == null || tcs.Task.IsCompleted)
            {
                return;
            }

            if (dt != null)
            {
                ArrayParam arrParam = new ArrayParam();
                Sql oSql = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "KIWOOMDB");

                foreach (DataRow dr in dt.Rows)
                {
                    
                        arrParam.Clear();
                        arrParam.Add("@ACTION_GB", "A");
                        arrParam.Add("@STOCK_CODE", stockCode);
                        arrParam.Add("@STOCK_DATE", dr["일자"]);
                        arrParam.Add("@DATE_SEQNO", 0);
                        arrParam.Add("@NOW_PRICE", dr["현재가"]);
                        arrParam.Add("@TRADE_QTY", dr["거래량"]);
                        arrParam.Add("@TRADE_DAEGUM", dr["거래대금"]);
                        arrParam.Add("@START_PRICE", dr["시가"]);
                        arrParam.Add("@HIGH_PRICE", dr["고가"]);
                        arrParam.Add("@LOW_PRICE", dr["저가"]);
                        arrParam.Add("@CHG_JUGA_GB", dr["수정주가구분"]);
                        arrParam.Add("@CHG_RATE", dr["수정비율"]);
                        arrParam.Add("@CHG_JUGA_EVENT", dr["수정주가이벤트"]);
                        arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                        oSql.ExecuteNonQuery("p_Opt10081Add", CommandType.StoredProcedure, arrParam);
                    
                }
            }

            _opt10081.Dispose();
            tcs.SetResult(true);
            OnGetStockCode();
        }
        private void OnReceiveTrData_Opt10060PriceMaeSu(string stockCode, DataTable dt, int sPreNext)
        {
            ArrayParam arrParam = new ArrayParam();
            Sql oSql = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "KIWOOMDB");

            DataTable dtDate = new DataTable();
            KiwoomQuery kiwoom = new KiwoomQuery();

            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            if (dt != null)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    WriteTextSafe(stockCode + "(PRICE매수)" + "[" + dr["일자"].ToString() + "] 작업중");
                   
                    arrParam.Clear();
                    arrParam.Add("@ACTION_GB", "A");
                    arrParam.Add("@STOCK_CODE", stockCode);
                    arrParam.Add("@STOCK_DATE", dr["일자"]);
                    arrParam.Add("@MAEME_GB", "1");
                    arrParam.Add("@DATE_SEQNO", 0);
                    arrParam.Add("@NUJUK_TRDAEGUM", dr["누적거래대금"]);
                    arrParam.Add("@GAIN_PRICE", dr["개인투자자"]);
                    arrParam.Add("@FORE_PRICE", dr["외국인투자자"]);
                    arrParam.Add("@GIGAN_PRICE", dr["기관계"]);
                    arrParam.Add("@GUMY_PRICE", dr["금융투자"]);
                    arrParam.Add("@BOHUM_PRICE", dr["보험"]);
                    arrParam.Add("@TOSIN_PRICE", dr["투신"]);
                    arrParam.Add("@GITA_PRICE", dr["기타금융"]);
                    arrParam.Add("@BANK_PRICE", dr["은행"]);
                    arrParam.Add("@YEONGI_PRICE", dr["연기금등"]);
                    arrParam.Add("@SAMO_PRICE", dr["사모펀드"]);
                    arrParam.Add("@NATION_PRICE", dr["국가"]);
                    arrParam.Add("@BUBIN_PRICE", dr["기타법인"]);
                    arrParam.Add("@IOFORE_PRICE", dr["내외국인"]);
                    arrParam.Add("@GIGAN_SUM_PRICE", Convert.ToInt32(dr["금융투자"]) + Convert.ToInt32(dr["보험"]) + Convert.ToInt32(dr["투신"]) +
                                                   Convert.ToInt32(dr["기타금융"]) + Convert.ToInt32(dr["은행"]) + Convert.ToInt32(dr["연기금등"]) +
                                                   Convert.ToInt32(dr["사모펀드"]) + Convert.ToInt32(dr["국가"]));
                    arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                    oSql.ExecuteNonQuery("p_Opt10060PriceAdd", CommandType.StoredProcedure, arrParam);

                }
            }

            _opt10060.Dispose();

            tcs.SetResult(true);
            ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060PriceMaeSu);
            GetOpt10060Caller(Opt10060TransType.PriceMaedo, stockCode, "", "");

        }
        private void OnReceiveTrData_Opt10060PriceMaedo(string stockCode, DataTable dt, int sPreNext)
        {
            ArrayParam arrParam = new ArrayParam();
            Sql oSql = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "KIWOOMDB");

            DataTable dtDate = new DataTable();
            KiwoomQuery kiwoom = new KiwoomQuery();

            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            if (dt != null)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    WriteTextSafe(stockCode + "(PRICE매도)" + "[" + dr["일자"].ToString() + "] 작업중");
                   
                    arrParam.Clear();
                    arrParam.Add("@ACTION_GB", "A");
                    arrParam.Add("@STOCK_CODE", stockCode);
                    arrParam.Add("@STOCK_DATE", dr["일자"]);
                    arrParam.Add("@MAEME_GB", "2");
                    arrParam.Add("@DATE_SEQNO", 0);
                    arrParam.Add("@NUJUK_TRDAEGUM", dr["누적거래대금"]);
                    arrParam.Add("@GAIN_PRICE", dr["개인투자자"]);
                    arrParam.Add("@FORE_PRICE", dr["외국인투자자"]);
                    arrParam.Add("@GIGAN_PRICE", dr["기관계"]);
                    arrParam.Add("@GUMY_PRICE", dr["금융투자"]);
                    arrParam.Add("@BOHUM_PRICE", dr["보험"]);
                    arrParam.Add("@TOSIN_PRICE", dr["투신"]);
                    arrParam.Add("@GITA_PRICE", dr["기타금융"]);
                    arrParam.Add("@BANK_PRICE", dr["은행"]);
                    arrParam.Add("@YEONGI_PRICE", dr["연기금등"]);
                    arrParam.Add("@SAMO_PRICE", dr["사모펀드"]);
                    arrParam.Add("@NATION_PRICE", dr["국가"]);
                    arrParam.Add("@BUBIN_PRICE", dr["기타법인"]);
                    arrParam.Add("@IOFORE_PRICE", dr["내외국인"]);
                    arrParam.Add("@GIGAN_SUM_PRICE", Convert.ToInt32(dr["금융투자"]) + Convert.ToInt32(dr["보험"]) + Convert.ToInt32(dr["투신"]) +
                                                   Convert.ToInt32(dr["기타금융"]) + Convert.ToInt32(dr["은행"]) + Convert.ToInt32(dr["연기금등"]) +
                                                   Convert.ToInt32(dr["사모펀드"]) + Convert.ToInt32(dr["국가"]));
                    arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                    oSql.ExecuteNonQuery("p_Opt10060PriceAdd", CommandType.StoredProcedure, arrParam);

                }
            }

            _opt10060.Dispose();

            tcs.SetResult(true);

            ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060PriceMaedo);

            GetOpt10060Caller(Opt10060TransType.QtyMaesu, stockCode, "", "");

        }
        private void OnReceiveTrData_Opt10060MaeSu(string stockCode, DataTable dt, int sPreNext)
        {
            ArrayParam arrParam = new ArrayParam();
            Sql oSql = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "KIWOOMDB");

            DataTable dtDate = new DataTable();
            KiwoomQuery kiwoom = new KiwoomQuery();

            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            if (tcs == null || tcs.Task.IsCompleted)
            {
                return;
            }

            if (dt != null)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    WriteTextSafe(stockCode + "(QTY매수)" + "[" + dr["일자"].ToString() + "] 작업중");
               
                    arrParam.Clear();
                    arrParam.Add("@ACTION_GB", "A");
                    arrParam.Add("@STOCK_CODE", stockCode);
                    arrParam.Add("@STOCK_DATE", dr["일자"]);
                    arrParam.Add("@MAEME_GB", "1");
                    arrParam.Add("@DATE_SEQNO", 0);
                    arrParam.Add("@NUJUK_TRDAEGUM", dr["누적거래대금"]);
                    arrParam.Add("@GAIN_QTY", dr["개인투자자"]);
                    arrParam.Add("@FORE_QTY", dr["외국인투자자"]);
                    arrParam.Add("@GIGAN_QTY", dr["기관계"]);
                    arrParam.Add("@GUMY_QTY", dr["금융투자"]);
                    arrParam.Add("@BOHUM_QTY", dr["보험"]);
                    arrParam.Add("@TOSIN_QTY", dr["투신"]);
                    arrParam.Add("@GITA_QTY", dr["기타금융"]);
                    arrParam.Add("@BANK_QTY", dr["은행"]);
                    arrParam.Add("@YEONGI_QTY", dr["연기금등"]);
                    arrParam.Add("@SAMO_QTY", dr["사모펀드"]);
                    arrParam.Add("@NATION_QTY", dr["국가"]);
                    arrParam.Add("@BUBIN_QTY", dr["기타법인"]);
                    arrParam.Add("@IOFORE_QTY", dr["내외국인"]);
                    arrParam.Add("@GIGAN_SUM_QTY", Convert.ToInt32(dr["금융투자"]) + Convert.ToInt32(dr["보험"]) + Convert.ToInt32(dr["투신"]) +
                                                   Convert.ToInt32(dr["기타금융"]) + Convert.ToInt32(dr["은행"]) + Convert.ToInt32(dr["연기금등"]) +
                                                   Convert.ToInt32(dr["사모펀드"]) + Convert.ToInt32(dr["국가"]));
                    arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                    oSql.ExecuteNonQuery("p_Opt10060QtyAdd", CommandType.StoredProcedure, arrParam);


                }
            }

            _opt10060.Dispose();

            tcs.SetResult(true);

            ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060MaeSu);

            GetOpt10060Caller(Opt10060TransType.QtyMaeDo, stockCode, "", "");

        }
        private void OnReceiveTrData_Opt10060Maedo(string stockCode, DataTable dt, int sPreNext)
        {
            ArrayParam arrParam = new ArrayParam();
            Sql oSql = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "KIWOOMDB");

            DataTable dtDate = new DataTable();
            KiwoomQuery kiwoom = new KiwoomQuery();

            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();


            if (dt != null)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    WriteTextSafe(stockCode + "(QTY매도)" + "[" + dr["일자"].ToString() + "] 작업중");
                  
                    arrParam.Clear();
                    arrParam.Add("@ACTION_GB", "A");
                    arrParam.Add("@STOCK_CODE", stockCode);
                    arrParam.Add("@STOCK_DATE", dr["일자"]);
                    arrParam.Add("@MAEME_GB", "2");
                    arrParam.Add("@DATE_SEQNO", 0);
                    arrParam.Add("@NUJUK_TRDAEGUM", dr["누적거래대금"]);
                    arrParam.Add("@GAIN_QTY", dr["개인투자자"]);
                    arrParam.Add("@FORE_QTY", dr["외국인투자자"]);
                    arrParam.Add("@GIGAN_QTY", dr["기관계"]);
                    arrParam.Add("@GUMY_QTY", dr["금융투자"]);
                    arrParam.Add("@BOHUM_QTY", dr["보험"]);
                    arrParam.Add("@TOSIN_QTY", dr["투신"]);
                    arrParam.Add("@GITA_QTY", dr["기타금융"]);
                    arrParam.Add("@BANK_QTY", dr["은행"]);
                    arrParam.Add("@YEONGI_QTY", dr["연기금등"]);
                    arrParam.Add("@SAMO_QTY", dr["사모펀드"]);
                    arrParam.Add("@NATION_QTY", dr["국가"]);
                    arrParam.Add("@BUBIN_QTY", dr["기타법인"]);
                    arrParam.Add("@IOFORE_QTY", dr["내외국인"]);
                    arrParam.Add("@GIGAN_SUM_QTY", Convert.ToInt32(dr["금융투자"]) + Convert.ToInt32(dr["보험"]) + Convert.ToInt32(dr["투신"]) +
                                                   Convert.ToInt32(dr["기타금융"]) + Convert.ToInt32(dr["은행"]) + Convert.ToInt32(dr["연기금등"]) +
                                                   Convert.ToInt32(dr["사모펀드"]) + Convert.ToInt32(dr["국가"]));
                    arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                    oSql.ExecuteNonQuery("p_Opt10060QtyAdd", CommandType.StoredProcedure, arrParam);

                }
            }

            _opt10060.Dispose();
            tcs.SetResult(true);

            ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060Maedo);

            OnGetStockCode();
        }
        private void BtnStart_Click(object sender, EventArgs e)
        {
            OnGetStockCode();
        }
    }
}
