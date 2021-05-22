using SDataAccess;
using System;
using System.Collections;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Woom.DataAccess;
using Woom.DataAccess.OptCaller.Class;
using Woom.DataAccess.PlugIn;

namespace Woom.Tester.Forms
{
    public partial class FrmOpt10060Caller_New : Form
    {
        public FrmOpt10060Caller_New(DataTable UserDt)
        {
            _dtStockCode = UserDt.Copy();
        }

        public FrmOpt10060Caller_New(string stockCode)
        {
            _dtStockCode = new DataTable();
            _dtStockCode.Columns.Add("STOCK_CODE", Type.GetType("String"));
            DataRow dr;

            dr = _dtStockCode.NewRow();
            dr["STOCK_CODE"] = stockCode;
            _dtStockCode.Rows.Add(dr);
            
        }

        private DataTable _dtStockCode;
        private int _seqNo = 0;
        private string _stdDate = ""; // 기준일자
        private string _FormId = "82";
        private Queue _StockQueue = new Queue();
        private ClsDataAccessUtil _clsDataAccessUtil = new ClsDataAccessUtil();
        private ClsOpt10060 _ClsOpt10060;
        private DataTable _UserDt;

        // 마지막으로 돌린 일자
        private string _LastPsDate = "";
        private string _LastPdDate = "";
        private string _LastQsDate = "";
        private string _LastQdDate = "";

        private string _FirstPsDate = "";
        private string _FirstPdDate = "";
        private string _FirstQsDate = "";
        private string _FirstQdDate = "";

        private string _FirstStockDate = ""; // 상장일

        private enum Opt10060TransType
        {
            PriceMaesu, PriceMaedo, QtyMaesu, QtyMaeDo
        }

        public FrmOpt10060Caller_New()
        {
            InitializeComponent();

            _ClsOpt10060 = new ClsOpt10060();

            //ClsAxKH.AxKH_10060_OnReceived += new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060MaeSu);

            if (_dtStockCode == null)
            {
                Func<DataTable> funcGetStockData = () =>
                {
                    RichQuery oRichQuery = new RichQuery();
                    return oRichQuery.p_ScodeQuery("1", "", "", false).Tables[0].Copy();
                };

                _dtStockCode = funcGetStockData();
            }

            proBar10060.Maximum = _dtStockCode.Rows.Count;

            foreach (DataRow dr in _dtStockCode.Rows)
            {
                if (ClsAxKH.GetMasterCodeName(dr["STOCK_CODE"].ToString().Trim()) == "")
                { continue; }

                _StockQueue.Enqueue(dr["STOCK_CODE"].ToString());

            }

            proBar10060.Maximum = _dtStockCode.Rows.Count;

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

        private void OnGetStockCode()
        {
            
            string strStockCode = "";

            strStockCode = GetStockCode();
            
            if (strStockCode == "End")
            { return; }
            _seqNo = _seqNo + 1;
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

            _FirstStockDate = ClsAxKH.GetMasterListedStockDate(strStockCode);

            GetOpt10060Caller(Opt10060TransType.PriceMaesu, strStockCode, _LastPsDate, _FirstPsDate);
            
            WriteTextSafe(strStockCode + " 작업 중");
            //   tcs.SetResult(true);
        }

    
        private string GetStockCode()
        {
            if (_StockQueue.Count == 0)
            {
                WriteTextSafe("작업이 완료되었습니다.");
                return "End";
            }

            string reValue;
           
            reValue = _StockQueue.Dequeue().ToString();
            
            _LastPsDate = "";
            _LastPdDate = "";
            _LastQsDate = "";
            _LastQdDate = "";

            _FirstPsDate = "";
            _FirstPdDate = "";
            _FirstQsDate = "";
            _FirstQdDate = "";

            bool blnChk = false;
            DataTable dtDate = new DataTable();

            Func<DataTable> funcOpt10060MaxQuery = () =>
            {
                KiwoomQuery kiwoomQuery = new KiwoomQuery();

                return kiwoomQuery.p_Opt10060MinMaxQuery("1", reValue, "", "", false).Tables[0].Copy();
            };

            Func<DataTable> funcOpt10060MinQuery = () =>
            {
                KiwoomQuery kiwoomQuery = new KiwoomQuery();

                return kiwoomQuery.p_Opt10060MinMaxQuery("2", reValue, "", "", false).Tables[0].Copy();
            };

            dtDate = funcOpt10060MaxQuery();

            if (dtDate != null)
            {
                // 한개라도 작업을 돌리게 있으면 돌려라.
                if (Convert.ToInt32(_stdDate) > Convert.ToInt32(dtDate.Rows[0]["MAESU_PRICE"].ToString()))
                {
                    blnChk = true;
                }
                if (Convert.ToInt32(_stdDate) > Convert.ToInt32(dtDate.Rows[0]["MEADO_PRICE"].ToString()))
                {
                    blnChk = true;
                }
                if (Convert.ToInt32(_stdDate) > Convert.ToInt32(dtDate.Rows[0]["MAESU_QTY"].ToString()))
                {
                    blnChk = true;
                }
                if (Convert.ToInt32(_stdDate) > Convert.ToInt32(dtDate.Rows[0]["MAEDO_QTY"].ToString()))
                {
                    blnChk = true;
                }

                _LastPsDate = dtDate.Rows[0]["MAESU_PRICE"].ToString();
                _LastPdDate = dtDate.Rows[0]["MEADO_PRICE"].ToString();
                _LastQsDate = dtDate.Rows[0]["MAESU_QTY"].ToString();
                _LastQdDate = dtDate.Rows[0]["MAEDO_QTY"].ToString();
            }

            // 기준일자보다 크므로 작업할게 없다.
            if (blnChk == false)
            {
                dtDate = null;
                dtDate = new DataTable();
                return "";
            }

            dtDate = null;
            dtDate = new DataTable();

            dtDate = funcOpt10060MinQuery();

            if (dtDate != null)
            {
                _FirstPsDate = dtDate.Rows[0]["MAESU_PRICE"].ToString();
                _FirstPdDate = dtDate.Rows[0]["MEADO_PRICE"].ToString();
                _FirstQsDate = dtDate.Rows[0]["MAESU_QTY"].ToString();
                _FirstQdDate = dtDate.Rows[0]["MAEDO_QTY"].ToString();
            }
            
            dtDate = null;

            return reValue;
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

        private void GetOpt10060Caller(Opt10060TransType opt10060TransType, string stockCode, string MaxDate, string MinDate)
        {
            if (_ClsOpt10060 != null)
            {
                _ClsOpt10060.Dispose();
                _ClsOpt10060 = null;
            }
            _ClsOpt10060 = new ClsOpt10060();
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();
            if (tcs == null || tcs.Task.IsCompleted)
            {
                return;
            }

            string startDate = "";
            DataTable dtDate = new DataTable();
            KiwoomQuery kiwoom = new KiwoomQuery();

            startDate = _stdDate;            

            _ClsOpt10060.SetInit(_FormId);

            switch (opt10060TransType)
            {
                case Opt10060TransType.PriceMaesu:
                    WriteTextSafe(stockCode + "(" + ClsAxKH.GetMasterCodeName(stockCode) + ")" + " Price(매수)_" + _StockQueue.Count.ToString());
                    ClsAxKH.AxKH_10060_OnReceived += new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060PriceMaeSu);
                    _ClsOpt10060.JustRequest(StartDate: startDate, StockCode: stockCode, StockName: "", AmountQtyGb: "1", MaeMaeGb: "1", UnitG: "", nPrevNext: 0);
                    break;
                case Opt10060TransType.PriceMaedo:
                    WaitTime();
                    WriteTextSafe(stockCode + " Price(매도)_" + _StockQueue.Count.ToString());
                    ClsAxKH.AxKH_10060_OnReceived += new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060PriceMaedo);
                    _ClsOpt10060.JustRequest(StartDate: startDate, StockCode: stockCode, StockName: "", AmountQtyGb: "1", MaeMaeGb: "2", UnitG: "", nPrevNext: 0);
                    break;
                case Opt10060TransType.QtyMaesu:
                    WaitTime();
                    WriteTextSafe(stockCode + " QTY(매수)_" + _StockQueue.Count.ToString());
                    ClsAxKH.AxKH_10060_OnReceived += new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060MaeSu);
                    _ClsOpt10060.JustRequest(StartDate: startDate, StockCode: stockCode, StockName: "", AmountQtyGb: "2", MaeMaeGb: "1", UnitG: "", nPrevNext: 0);
                    break;
                case Opt10060TransType.QtyMaeDo:
                    WaitTime();
                    WriteTextSafe(stockCode + " QTY(매도)_" + _StockQueue.Count.ToString());
                    ClsAxKH.AxKH_10060_OnReceived += new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060Maedo);
                    _ClsOpt10060.JustRequest(StartDate: startDate, StockCode: stockCode, StockName: "", AmountQtyGb: "2", MaeMaeGb: "2", UnitG: "", nPrevNext: 0);
                    break;
                default:
                    break;
            }         
                        
           tcs.SetResult(true);

        }

        private void WaitTime()
        {
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();
            //Task.Delay(3000).Wait();
            _clsDataAccessUtil.Delay(3000);
            tcs.SetResult(true);
        }


        private void OnReceiveTrData_Opt10060PriceMaeSu(string sRQName, DataTable dt, int sPreNext)
        {
            ArrayParam arrParam = new ArrayParam();
            Sql oSql = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "KIWOOMDB");

            DataTable dtDate = new DataTable();
            KiwoomQuery kiwoom = new KiwoomQuery();

            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();
            
            string[] sRQNameArray = sRQName.Split(',');

            string stockCode = ClsAxKH.RetStockCodeBysRqName(ClsAxKH.OptType.Opt10060, sRQName);

            if (dt != null)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    WriteTextSafe(stockCode + "(PRICE매수)" + "[" + dr["일자"].ToString() + "] 작업중");
                    if (dr["일자"].ToString() == _LastPsDate)
                    {
                        
                        if (_FirstPsDate == _FirstStockDate)
                        {
                            Sql oSql2 = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "RICHDB");

                            arrParam.Clear();
                            arrParam.Add("@ACTION_GB", "C7");
                            arrParam.Add("@STOCK_CODE", stockCode);
                            arrParam.Add("@STOCK_NAME", "");
                            arrParam.Add("@YBJONG_CODE", "");
                            arrParam.Add("@OPT10059_QTY", "");
                            arrParam.Add("@OPT10059_PRICE", "");
                            arrParam.Add("@OPT10081", "");
                            arrParam.Add("@OPT10060_QTY", "");
                            arrParam.Add("@OPT10060_PRICE", "S");
                            arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                            oSql2.ExecuteNonQuery("p_ScodeAdd", CommandType.StoredProcedure, arrParam);

                            tcs.SetResult(true);

                            _ClsOpt10060.Dispose();

                            ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060PriceMaeSu);

                            GetOpt10060Caller(Opt10060TransType.PriceMaedo, stockCode, _LastPdDate, _FirstPdDate);

                            return;
                        }
                        else
                        {
                            tcs.SetResult(true);

                            WaitTime();

                            _ClsOpt10060.SetInit(_FormId);
                            _ClsOpt10060.JustRequest(StartDate: _FirstPsDate.ToString(), StockCode: stockCode, StockName: "", AmountQtyGb: "1", MaeMaeGb: "1", UnitG: "", nPrevNext: 0);
                            return;
                        }
                        
                    }

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

            if (sPreNext == 2)
            {
                int minDate = Convert.ToInt32(dt.Compute("min([일자])", string.Empty));

                tcs.SetResult(true);

                WaitTime();

                _ClsOpt10060.SetInit(_FormId);
                _ClsOpt10060.JustRequest(StartDate: sRQNameArray[1].ToString().Trim(), StockCode: sRQNameArray[2].ToString().Trim(), StockName: "", AmountQtyGb: sRQNameArray[3].ToString().Trim(), MaeMaeGb: sRQNameArray[4].ToString().Trim(), UnitG: sRQNameArray[5].ToString().Trim(), nPrevNext: 2);
            }
            else
            {
                Sql oSql2 = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "RICHDB");

                arrParam.Clear();
                arrParam.Add("@ACTION_GB", "C7");
                arrParam.Add("@STOCK_CODE", stockCode);
                arrParam.Add("@STOCK_NAME", "");
                arrParam.Add("@YBJONG_CODE", "");
                arrParam.Add("@OPT10059_QTY", "");
                arrParam.Add("@OPT10059_PRICE", "");
                arrParam.Add("@OPT10081", "");
                arrParam.Add("@OPT10060_QTY", "");
                arrParam.Add("@OPT10060_PRICE", "S");
                arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                oSql2.ExecuteNonQuery("p_ScodeAdd", CommandType.StoredProcedure, arrParam);

                _ClsOpt10060.Dispose();

                tcs.SetResult(true);
                ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060PriceMaeSu);
                GetOpt10060Caller(Opt10060TransType.PriceMaedo, stockCode, _LastPdDate, _FirstPdDate);
            }
        }
        private void OnReceiveTrData_Opt10060PriceMaedo(string sRQName, DataTable dt, int sPreNext)
        {
            ArrayParam arrParam = new ArrayParam();
            Sql oSql = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "KIWOOMDB");

            DataTable dtDate = new DataTable();
            KiwoomQuery kiwoom = new KiwoomQuery();

            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            string[] sRQNameArray = sRQName.Split(',');

            string stockCode = ClsAxKH.RetStockCodeBysRqName(ClsAxKH.OptType.Opt10060, sRQName);

            if (dt != null)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    WriteTextSafe(stockCode + "(PRICE매도)" + "[" + dr["일자"].ToString() + "] 작업중");
                    if (dr["일자"].ToString() == _LastPdDate)
                    {
                        
                        if (_FirstPdDate == _FirstStockDate)
                        {
                            Sql oSql2 = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "RICHDB");

                            arrParam.Clear();
                            arrParam.Add("@ACTION_GB", "C7");
                            arrParam.Add("@STOCK_CODE", stockCode);
                            arrParam.Add("@STOCK_NAME", "");
                            arrParam.Add("@YBJONG_CODE", "");
                            arrParam.Add("@OPT10059_QTY", "");
                            arrParam.Add("@OPT10059_PRICE", "");
                            arrParam.Add("@OPT10081", "");
                            arrParam.Add("@OPT10060_QTY", "");
                            arrParam.Add("@OPT10060_PRICE", "Y");
                            arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                            oSql2.ExecuteNonQuery("p_ScodeAdd", CommandType.StoredProcedure, arrParam);

                            tcs.SetResult(true);

                            _ClsOpt10060.Dispose();

                            ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060PriceMaedo);

                            GetOpt10060Caller(Opt10060TransType.QtyMaesu, stockCode, _LastQsDate, _FirstQsDate);

                            return;
                        }
                        else
                        {
                            tcs.SetResult(true);

                            WaitTime();

                            _ClsOpt10060.SetInit(_FormId);
                            _ClsOpt10060.JustRequest(StartDate: _FirstPsDate, StockCode: stockCode, StockName: "", AmountQtyGb: "1", MaeMaeGb: "2", UnitG: "", nPrevNext: 0);
                            return;
                        }
                        

                    }
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

            if (sPreNext == 2)
            {
                int minDate = Convert.ToInt32(dt.Compute("min([일자])", string.Empty));

                tcs.SetResult(true);

                WaitTime();

                _ClsOpt10060.SetInit(_FormId);
                //_ClsOpt10060.JustRequest(StartDate: minDate.ToString(), StockCode: stockCode, StockName: "", AmountQtyGb: "1", MaeMaeGb: "2", UnitG: "", nPrevNext: 2);
                _ClsOpt10060.JustRequest(StartDate: sRQNameArray[1].ToString().Trim(), StockCode: sRQNameArray[2].ToString().Trim(), StockName: "", AmountQtyGb: sRQNameArray[3].ToString().Trim(), MaeMaeGb: sRQNameArray[4].ToString().Trim(), UnitG: sRQNameArray[5].ToString().Trim(), nPrevNext: 2);
            }
            else
            {
                Sql oSql2 = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "RICHDB");

                arrParam.Clear();
                arrParam.Add("@ACTION_GB", "C7");
                arrParam.Add("@STOCK_CODE", stockCode);
                arrParam.Add("@STOCK_NAME", "");
                arrParam.Add("@YBJONG_CODE", "");
                arrParam.Add("@OPT10059_QTY", "");
                arrParam.Add("@OPT10059_PRICE", "");
                arrParam.Add("@OPT10081", "");
                arrParam.Add("@OPT10060_QTY", "");
                arrParam.Add("@OPT10060_PRICE", "Y");
                arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                oSql2.ExecuteNonQuery("p_ScodeAdd", CommandType.StoredProcedure, arrParam);

                _ClsOpt10060.Dispose();

                tcs.SetResult(true);

                ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060PriceMaedo);

                GetOpt10060Caller(Opt10060TransType.QtyMaesu, stockCode, _LastPdDate, _FirstPdDate);

            }
        }

        private void OnReceiveTrData_Opt10060MaeSu(string sRQName, DataTable dt, int sPreNext)
        {
            ArrayParam arrParam = new ArrayParam();
            Sql oSql = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "KIWOOMDB");
            
            DataTable dtDate = new DataTable();
            KiwoomQuery kiwoom = new KiwoomQuery();

            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            string[] sRQNameArray = sRQName.Split(',');

            string stockCode = ClsAxKH.RetStockCodeBysRqName(ClsAxKH.OptType.Opt10060, sRQName);

            if (tcs == null || tcs.Task.IsCompleted)
            {
                return;
            }

            if (dt != null)
            {
              
                    foreach (DataRow dr in dt.Rows)
                {
                    WriteTextSafe(stockCode + "(QTY매수)" + "[" + dr["일자"].ToString() + "] 작업중");
                    if (dr["일자"].ToString() == _LastQsDate)
                    {
                        if (_FirstQsDate == _FirstStockDate)
                        {
                            Sql oSql2 = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "RICHDB");

                            arrParam.Clear();
                            arrParam.Add("@ACTION_GB", "C6");
                            arrParam.Add("@STOCK_CODE", stockCode);
                            arrParam.Add("@STOCK_NAME", "");
                            arrParam.Add("@YBJONG_CODE", "");
                            arrParam.Add("@OPT10059_QTY", "");
                            arrParam.Add("@OPT10059_PRICE", "");
                            arrParam.Add("@OPT10081", "");
                            arrParam.Add("@OPT10060_QTY", "S");
                            arrParam.Add("@OPT10060_PRICE", "");
                            arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                            oSql2.ExecuteNonQuery("p_ScodeAdd", CommandType.StoredProcedure, arrParam);

                            tcs.SetResult(true);

                            _ClsOpt10060.Dispose();

                            ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060MaeSu);

                            GetOpt10060Caller(Opt10060TransType.QtyMaeDo, stockCode, _LastQdDate, _FirstQdDate);

                            return;
                        }
                        else
                        {
                            tcs.SetResult(true);
                            WaitTime();
                            _ClsOpt10060.SetInit(_FormId);
                            _ClsOpt10060.JustRequest(StartDate: _FirstQsDate.ToString(), StockCode: stockCode, StockName: "", AmountQtyGb: "2", MaeMaeGb: "1", UnitG: "", nPrevNext: 0);
                            return;
                        }
                        
                    }
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

            if (sPreNext == 2)
            {

                int minDate = Convert.ToInt32(dt.Compute("min([일자])", string.Empty));

                tcs.SetResult(true);
                WaitTime();
                _ClsOpt10060.SetInit(_FormId);
                //_ClsOpt10060.JustRequest(StartDate: minDate.ToString(), StockCode: stockCode, StockName: "", AmountQtyGb: "2", MaeMaeGb: "1", UnitG: "", nPrevNext: 2);
                _ClsOpt10060.JustRequest(StartDate: sRQNameArray[1].ToString().Trim(), StockCode: sRQNameArray[2].ToString().Trim(), StockName: "", AmountQtyGb: sRQNameArray[3].ToString().Trim(), MaeMaeGb: sRQNameArray[4].ToString().Trim(), UnitG: sRQNameArray[5].ToString().Trim(), nPrevNext: 2);
            }
            else
            {

                Sql oSql2 = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "RICHDB");

                arrParam.Clear();
                arrParam.Add("@ACTION_GB", "C6");
                arrParam.Add("@STOCK_CODE", stockCode);
                arrParam.Add("@STOCK_NAME", "");
                arrParam.Add("@YBJONG_CODE", "");
                arrParam.Add("@OPT10059_QTY", "");
                arrParam.Add("@OPT10059_PRICE", "");
                arrParam.Add("@OPT10081", "");
                arrParam.Add("@OPT10060_QTY", "S");
                arrParam.Add("@OPT10060_PRICE", "");
                arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                oSql2.ExecuteNonQuery("p_ScodeAdd", CommandType.StoredProcedure, arrParam);

                _ClsOpt10060.Dispose();

                tcs.SetResult(true);

                ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060MaeSu);

                GetOpt10060Caller(Opt10060TransType.QtyMaeDo, stockCode, _LastQdDate, _FirstQdDate);

            }

        }
        private void OnReceiveTrData_Opt10060Maedo(string sRQName, DataTable dt, int sPreNext)
        {
            ArrayParam arrParam = new ArrayParam();
            Sql oSql = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "KIWOOMDB");

            DataTable dtDate = new DataTable();
            KiwoomQuery kiwoom = new KiwoomQuery();

            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            string[] sRQNameArray = sRQName.Split(',');

            string stockCode = ClsAxKH.RetStockCodeBysRqName(ClsAxKH.OptType.Opt10060, sRQName);

            if (dt != null)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    WriteTextSafe(stockCode + "(QTY매도)" + "[" + dr["일자"].ToString() + "] 작업중");
                    if (dr["일자"].ToString() == _LastQdDate)
                    {
                        if (_FirstQdDate == _FirstStockDate)
                        {
                            Sql oSql2 = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "RICHDB");

                            arrParam.Clear();
                            arrParam.Add("@ACTION_GB", "C6");
                            arrParam.Add("@STOCK_CODE", stockCode);
                            arrParam.Add("@STOCK_NAME", "");
                            arrParam.Add("@YBJONG_CODE", "");
                            arrParam.Add("@OPT10059_QTY", "");
                            arrParam.Add("@OPT10059_PRICE", "");
                            arrParam.Add("@OPT10081", "");
                            arrParam.Add("@OPT10060_QTY", "Y");
                            arrParam.Add("@OPT10060_PRICE", "");
                            arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);
                            oSql2.ExecuteNonQuery("p_ScodeAdd", CommandType.StoredProcedure, arrParam);

                            tcs.SetResult(true);
                            _ClsOpt10060.Dispose();
                            ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060Maedo);
                            OnGetStockCode();

                            return;
                        }
                        else
                        {
                            tcs.SetResult(true);
                            WaitTime();
                            _ClsOpt10060.SetInit(_FormId);
                            _ClsOpt10060.JustRequest(StartDate: _FirstQdDate.ToString(), StockCode: stockCode, StockName: "", AmountQtyGb: "2", MaeMaeGb: "2", UnitG: "", nPrevNext: 0);
                            return;
                        }
                           
                    }
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

            if (sPreNext == 2)
            {
                int minDate = Convert.ToInt32(dt.Compute("min([일자])", string.Empty));

                tcs.SetResult(true);
                WaitTime();
                _ClsOpt10060.SetInit(_FormId);
                //_ClsOpt10060.JustRequest(StartDate: minDate.ToString(), StockCode: stockCode, StockName: "", AmountQtyGb: "2", MaeMaeGb: "2", UnitG: "", nPrevNext: 2);
                _ClsOpt10060.JustRequest(StartDate: sRQNameArray[1].ToString().Trim(), StockCode: sRQNameArray[2].ToString().Trim(), StockName: "", AmountQtyGb: sRQNameArray[3].ToString().Trim(), MaeMaeGb: sRQNameArray[4].ToString().Trim(), UnitG: sRQNameArray[5].ToString().Trim(), nPrevNext: 2);
            }
            else
            {
                Sql oSql2 = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "RICHDB");

                arrParam.Clear();
                arrParam.Add("@ACTION_GB", "C6");
                arrParam.Add("@STOCK_CODE", stockCode);
                arrParam.Add("@STOCK_NAME", "");
                arrParam.Add("@YBJONG_CODE", "");
                arrParam.Add("@OPT10059_QTY", "");
                arrParam.Add("@OPT10059_PRICE", "");
                arrParam.Add("@OPT10081", "");
                arrParam.Add("@OPT10060_QTY", "Y");
                arrParam.Add("@OPT10060_PRICE", "");
                arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);
                oSql2.ExecuteNonQuery("p_ScodeAdd", CommandType.StoredProcedure, arrParam);

                _ClsOpt10060.Dispose();
                tcs.SetResult(true);

                ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060Maedo);

                OnGetStockCode();

            }
        }

  
        private void btn10060All_Click(object sender, EventArgs e)
        {
            OnGetStockCode();
        }
    }
}
