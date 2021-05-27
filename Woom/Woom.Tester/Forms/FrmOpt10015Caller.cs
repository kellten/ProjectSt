using SDataAccess;
using System;
using System.Collections;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Woom.DataAccess;
using Woom.DataAccess.OptCaller.Class;
using Woom.DataAccess.PlugIn;
using Woom.DataAccess.Logger;
using Woom.DataDefine.Util;

namespace Woom.Tester.Forms
{
    public partial class FrmOpt10015Caller : Form
    {
        public FrmOpt10015Caller(DataTable dt)
        {
 
            InitializeComponent();

            _clsDataAccessUtil = new ClsDataAccessUtil();

            ClsAxKH.AxKH_10015_OnReceived += new ClsAxKH.OnReceivedEventHandler(Opt10015_OnReceived);

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

        private Queue _StockQueue = new Queue();
        private DataTable _dtOptCalMagam;

        private void GetOptCallMagamaData(string stdDate)
        {
            if (_dtOptCalMagam != null)
            {
                _dtOptCalMagam = null;
                _dtOptCalMagam = new DataTable();
            }

            KiwoomQuery kiwoomQuery = new KiwoomQuery();
            _dtOptCalMagam = kiwoomQuery.p_OptCaMagamStdDateQuery(query: "1", stdDate: stdDate, stockCode: "", optcall: "OPT10015", jobDate: "", jobIngGb: "", bln3tier: false).Tables[0].Copy();
        }

        #region 전역변수

        private DataTable _dtStockCode;
        private int _seqNo = 0;
        private string _stdDate = "";
        private string _FormId = "";
        private int _ScreenNo = 100;
        private string _FirstStockDate = ""; // 상장일

        private ClsOpt10015 _opt10015 = new ClsOpt10015();

        // 마지막으로 돌린 일자
        private string _LastDate = "";
        // 마지막으로 돌린 일자
        private string _FirstDate = "";
        #endregion 전역변수

        private ClsDataAccessUtil _clsDataAccessUtil;
        private void WaitTime()
        {
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();
            //Task.Delay(3000).Wait();
            if (ClsAxKH.SPEED_CALL == true)
            { _clsDataAccessUtil.Delay(500); }
            else
            { _clsDataAccessUtil.Delay(3600); }
            
            tcs.SetResult(true);
        }

        private void OnGetStockCode()
        {
            string strStockCode = "";

            strStockCode = GetStockCode();

            if (strStockCode == "End")
            { return; }
            _seqNo = _seqNo + 1;
            proBar10015.Value = _seqNo;
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

            GetOpt10015Caller( strStockCode, _LastDate, _FirstDate);

            WriteTextSafe(strStockCode + " 작업 중");
            //   tcs.SetResult(true);
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
        private string GetStockCode()
        {
            string reValue;

            if (_StockQueue.Count == 0)
            {
                MessageBox.Show("작업이 완료되었습니다.");
                return "End";
            }
            reValue = _StockQueue.Dequeue().ToString();

            //KiwoomQuery kiwoomQuery = new KiwoomQuery();
            //DataTable dt = new DataTable();

            //dt = kiwoomQuery.p_Opt10015Query("3", reValue.ToString().Trim(), "","", "", false).Tables[0].Copy();

            //if (dt.Rows.Count > 0)
            //{
            //    _LastDate = dt.Rows[0]["MAX_STOCK_DATE"].ToString().Trim();
            //    _FirstDate = dt.Rows[0]["MIN_STOCK_DATE"].ToString().Trim();
            //}
            //else
            //{
            //    _LastDate = "";
            //    _FirstDate = "";
            //}

            if (chk100.Checked == true)
            {
               var rows = _dtOptCalMagam.AsEnumerable().Where(Row => Row.Field<string>("STOCK_CODE") == reValue);
                
                foreach (DataRow dr2th in rows)
                {
                    if (dr2th["JOB_ING_GB"].ToString().Trim() == "C")
                    {
                        return "";
                    }
                }
            }
            else
            { 
            
            }

            return reValue;

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

            SetFormId();

            _opt10015.SetInit(_FormId);
            _opt10015.JustRequest(stockCode, _stdDate, "", 0);

            tcs.SetResult(true);

        }

        private void SetFormId()
        {
            ClsUtil clsUtil = new ClsUtil();
            _FormId = clsUtil.Right(_ScreenNo.ToString(), 2);

            _ScreenNo = _ScreenNo + 1;
        }

        private void Opt10015_OnReceived(string sRQName, DataTable dt, int sPreNext)
        {

            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            if (tcs == null || tcs.Task.IsCompleted)
            {
                return;
            }          
            
            string[] sRQNameArray = sRQName.Split(',');

            string stockCode = ClsAxKH.RetStockCodeBysRqName(ClsAxKH.OptType.Opt10015, sRQName);
            string stdDate = ClsAxKH.RetStdDateBysRqName(ClsAxKH.OptType.Opt10015, sRQName);
            string maxDate = dt.Compute("max([일자])",  string.Empty).ToString().Trim();
            string minDate = dt.Compute("min([일자])",string.Empty).ToString().Trim();

          //  ClsDbLogger.OptCallMagamStoredData(optCaller: "OPT10015", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "");

            if (dt != null)
            {
                ArrayParam arrParam = new ArrayParam();
                Sql oSql = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "KIWOOMDB");

                foreach (DataRow dr in dt.Rows)
                {
                    WriteTextSafe(stockCode + "(일자별상세)" + "[" + dr["일자"].ToString() + "] 작업중");

                    // 최근 거래일 100일을 가져오는걸로 한다면,
                    if (chk100.Checked == true)
                    {
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
                    else
                    {
                        if (_LastDate == dr["일자"].ToString().Trim())
                        {
                            if (_FirstDate == _FirstStockDate)
                            {
                                _opt10015.Dispose();

                                tcs.SetResult(true);

                                OnGetStockCode();

                                return;
                            }
                            else
                            {
                                tcs.SetResult(true);

                                WaitTime();

                                SetFormId();

                                _opt10015.SetInit(_FormId);

                                //e, string StartDate, string StockName, int nPrevNext
                                _opt10015.JustRequest(StockCode: stockCode, StartDate: _FirstDate.ToString(), StockName: "", nPrevNext: 0);
                                return;

                            }
                        }
                        else
                        {

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

                            _LastDate = dr["일자"].ToString();


                        }
                    }
                    
                }
            }

            // 최근 거래일 100일을 가져오는걸로 한다면
            if (chk100.Checked == true)
            {
              //  ClsDbLogger.OptCallMagamStoredData(optCaller: "OPT10015", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "C");

                _opt10015.Dispose();

                tcs.SetResult(true);

                OnGetStockCode();

            }
            else
            {
                if (sPreNext == 2)
                {
                    //int minDate = Convert.ToInt32(dt.Compute("min([일자])", string.Empty));

                 //   ClsDbLogger.OptCallMagamStoredData(optCaller: "OPT10015", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "S");

                    tcs.SetResult(true);

                    WaitTime();

                    _opt10015.SetInit(_FormId);
                    _opt10015.JustRequest(StockCode: sRQNameArray[1].ToString().Trim(), StartDate: sRQNameArray[2].ToString().Trim(), StockName: "", nPrevNext: 2);

                }
                else
                {

                  //  ClsDbLogger.OptCallMagamStoredData(optCaller: "OPT10015", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "E");

                    _opt10015.Dispose();

                    tcs.SetResult(true);

                    OnGetStockCode();

                }
            }
        
        }

        private void btn10015_Click(object sender, EventArgs e)
        {
            GetOptCallMagamaData(_stdDate);
            OnGetStockCode();
        }

        private void chkSpeedOn_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSpeedOn.Checked == true)
            {
                ClsAxKH.SPEED_CALL = true;
            }
            else
            {
                ClsAxKH.SPEED_CALL = false;
            }
        }
    }
}
