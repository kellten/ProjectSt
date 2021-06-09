using SDataAccess;
using System;
using System.Collections;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Woom.DataAccess;
using Woom.DataAccess.OptCaller.Class;
using Woom.DataAccess.PlugIn;
using Woom.DataDefine.OptData;
using Woom.DataDefine.Util;
using Woom.DataAccess.Logger;


namespace Woom.Tester.Forms
{
    public partial class FrmOpt10060CallerPer : Form
    {

        private DataTable _dtStockCode;
        private int _seqNo = 0;
        private string _stdDate = ""; // 기준일자
        private string _FormId = "82";
        private int _ScreenNo = 100;
        private Queue _StockQueue = new Queue();
        private ClsDataAccessUtil _clsDataAccessUtil = new ClsDataAccessUtil();
        private ClsOpt10060 _ClsOpt10060;
        private DataTable _UserDt;

        private ClsUtil _clsUtil = new ClsUtil();
        private ClsCollectOptDataFunc _clsCollectOptDataFunc = new ClsCollectOptDataFunc();
        private enum Opt10060TransType
        {
            PriceMaesu, PriceMaedo, QtyMaesu, QtyMaeDo
        }


        public FrmOpt10060CallerPer()
        {
            InitializeComponent();

            _ClsOpt10060 = new ClsOpt10060();

            _clsDataAccessUtil = new ClsDataAccessUtil();

            Func<DataTable> funcGetStockData = () =>
            {
                RichQuery oRichQuery = new RichQuery();
                return oRichQuery.p_ScodeQuery("1", "", "", false).Tables[0].Copy();
            };

            _dtStockCode = funcGetStockData();

            foreach (DataRow dr in _dtStockCode.Rows)
            {
                if (ClsAxKH.GetMasterCodeName(dr["STOCK_CODE"].ToString().Trim()) == "")
                {
                    continue;
                }

                _StockQueue.Enqueue(dr["STOCK_CODE"].ToString());
            }

            _stdDate = _clsCollectOptDataFunc.GetAvailableDate();

            dtpStdDate.Value = _clsUtil.StringToDateTime(_stdDate);

            GetOptCallMagamaData(_stdDate);

            proBar10060PriceBuy.Maximum = _StockQueue.Count;

            proBar10060PriceSell.Maximum = _StockQueue.Count;

            proBar10060QtyBuy.Maximum = _StockQueue.Count;
            proBar10060QtySell.Maximum = _StockQueue.Count;

        }
        private void InitDataPriceBuy()
        {
            

            _stdDate = _clsCollectOptDataFunc.GetAvailableDate();
            dtpStdDate.Value = _clsUtil.StringToDateTime(_stdDate);
            
        }
        private void SetFormId()
        {
            ClsUtil clsUtil = new ClsUtil();
            _FormId = clsUtil.Right(_ScreenNo.ToString(), 2);

            _ScreenNo = _ScreenNo + 1;
        }
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
        private void OnGetStockCode(Opt10060TransType opt10060Trans)
        {

            string strStockCode = "";

            strStockCode = GetStockCode(opt10060Trans);

            if (strStockCode == "End")
            { return; }

            _seqNo = _seqNo + 1;

            switch (opt10060Trans)
            {
                case Opt10060TransType.PriceMaesu:
                    proBar10060PriceBuy.Value = _seqNo;
                    break;
                case Opt10060TransType.PriceMaedo:
                    proBar10060PriceSell.Value = _seqNo;
                    break;
                case Opt10060TransType.QtyMaesu:
                    proBar10060QtyBuy.Value = _seqNo;
                    break;
                case Opt10060TransType.QtyMaeDo:
                    proBar10060QtySell.Value = _seqNo;
                    break;
                default:
                    break;
            }
            

            if (strStockCode == "")
            {
                OnGetStockCode(opt10060Trans);
                return;

            }

            string stockName = ClsAxKH.GetMasterCodeName(stockCode: strStockCode);

            // 종목명을 못 가져오면 상장폐지된 종목으로 생각.
            if (stockName == "")
            {
                OnGetStockCode(opt10060Trans);
                return;
            }

            WaitTime();

            GetOpt10060PriceBuyCaller(opt10060Trans, strStockCode);

            WriteTextSafe(strStockCode + " 작업 중");

        }

        private void GetOpt10060PriceBuyCaller(Opt10060TransType opt10060Trans, string stockCode)
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

            SetFormId();
            _ClsOpt10060.SetInit(_FormId);

            switch (opt10060Trans)
            {
                case Opt10060TransType.PriceMaesu:
                    WriteTextSafe(stockCode + "(" + ClsAxKH.GetMasterCodeName(stockCode) + ")" + " Price(매수)_" + _StockQueue.Count.ToString());
                    _ClsOpt10060.JustRequest(StartDate: _stdDate, StockCode: stockCode, StockName: "", AmountQtyGb: "1", MaeMaeGb: "1", UnitG: "", nPrevNext: 0);
                    break;
                case Opt10060TransType.PriceMaedo:
                    WriteTextSafe(stockCode + " Price(매도)_" + _StockQueue.Count.ToString());
                    _ClsOpt10060.JustRequest(StartDate: _stdDate, StockCode: stockCode, StockName: "", AmountQtyGb: "1", MaeMaeGb: "2", UnitG: "", nPrevNext: 0);
                    break;
                case Opt10060TransType.QtyMaesu:
                    WriteTextSafe(stockCode + " QTY(매수)_" + _StockQueue.Count.ToString());
                    _ClsOpt10060.JustRequest(StartDate: _stdDate, StockCode: stockCode, StockName: "", AmountQtyGb: "2", MaeMaeGb: "1", UnitG: "", nPrevNext: 0);
                    break;
                case Opt10060TransType.QtyMaeDo:
                    WriteTextSafe(stockCode + " QTY(매도)_" + _StockQueue.Count.ToString());
                    _ClsOpt10060.JustRequest(StartDate: _stdDate, StockCode: stockCode, StockName: "", AmountQtyGb: "2", MaeMaeGb: "2", UnitG: "", nPrevNext: 0);
                    break;
                default:
                    break;
            }

            tcs.SetResult(true);

        }

        private string GetStockCode(Opt10060TransType opt10060Trans)
        {
            string reValue;

            if (_StockQueue.Count == 0)
            {
                MessageBox.Show("작업이 완료되었습니다.");
                return "End";
            }
            reValue = _StockQueue.Dequeue().ToString();

            _seqNo = _seqNo + 1;

            if (chk100.Checked == true)
            {

                EnumerableRowCollection<DataRow> rows;

                switch (opt10060Trans)
                {
                    case Opt10060TransType.PriceMaesu:
                         rows = _dtOptCalMagamPs.AsEnumerable().Where(Row => Row.Field<string>("STOCK_CODE") == reValue);
                        break;
                    case Opt10060TransType.PriceMaedo:
                         rows = _dtOptCalMagamPd.AsEnumerable().Where(Row => Row.Field<string>("STOCK_CODE") == reValue);
                        break;
                    case Opt10060TransType.QtyMaesu:
                         rows = _dtOptCalMagamQs.AsEnumerable().Where(Row => Row.Field<string>("STOCK_CODE") == reValue);
                        break;
                    case Opt10060TransType.QtyMaeDo:
                         rows = _dtOptCalMagamQd.AsEnumerable().Where(Row => Row.Field<string>("STOCK_CODE") == reValue);
                        break;
                    default:
                        return "";
                }                

                foreach (DataRow dr2th in rows)
                {
                    if (dr2th["JOB_ING_GB"].ToString().Trim() == "C")
                    {
                        if (_stdDate == dr2th["MAX_DATE"].ToString().Trim())
                        {
                            return "";
                        }
                        else
                        {
                            return reValue;
                        }
                    }
                }
            }
            else
            {

                EnumerableRowCollection<DataRow> rows;

                switch (opt10060Trans)
                {
                    case Opt10060TransType.PriceMaesu:
                        rows = _dtOptCalMagamPs.AsEnumerable().Where(Row => Row.Field<string>("STOCK_CODE") == reValue);
                        break;
                    case Opt10060TransType.PriceMaedo:
                        rows = _dtOptCalMagamPd.AsEnumerable().Where(Row => Row.Field<string>("STOCK_CODE") == reValue);
                        break;
                    case Opt10060TransType.QtyMaesu:
                        rows = _dtOptCalMagamQs.AsEnumerable().Where(Row => Row.Field<string>("STOCK_CODE") == reValue);
                        break;
                    case Opt10060TransType.QtyMaeDo:
                        rows = _dtOptCalMagamQd.AsEnumerable().Where(Row => Row.Field<string>("STOCK_CODE") == reValue);
                        break;
                    default:
                        return "";
                }

                foreach (DataRow dr2th in rows)
                {
                    // Y - 연속조회 완료  E - 에러 (DT값이 NULL반환)
                    if (dr2th["CHAIN_COMP_GB"].ToString().Trim() == "Y" || dr2th["CHAIN_COMP_GB"].ToString().Trim() == "E")
                    {
                        if (_stdDate == dr2th["CHAIN_MAX_DATE"].ToString().Trim())
                        {
                            return "";
                        }
                        else
                        {
                            return reValue;
                        }
                    }
                }
            }

            return reValue;
        }

        private DataTable _dtOptCalMagamPs; // OPT10060_PRICE_매수
        private DataTable _dtOptCalMagamPd; // OPT10060_PRICE_매도
        private DataTable _dtOptCalMagamQs; // OPT10060_QTY_매수
        private DataTable _dtOptCalMagamQd; // OPT10060_QTY_매도
        private void GetOptCallMagamaData(string stdDate)
        {
            if (_dtOptCalMagamPs != null)
            {
                _dtOptCalMagamPs = null;
                _dtOptCalMagamPs = new DataTable();
            }

            KiwoomQuery kiwoomQuery = new KiwoomQuery();
            _dtOptCalMagamPs = kiwoomQuery.p_OptCaMagamOptCallQuery(query: "1", stockCode: "", optcall: "OPS10060", jobDate: "", jobIngGb: "", bln3tier: false).Tables[0].Copy();
            _dtOptCalMagamPd = kiwoomQuery.p_OptCaMagamOptCallQuery(query: "1", stockCode: "", optcall: "OPD10060", jobDate: "", jobIngGb: "", bln3tier: false).Tables[0].Copy();
            _dtOptCalMagamQs = kiwoomQuery.p_OptCaMagamOptCallQuery(query: "1", stockCode: "", optcall: "OQS10060", jobDate: "", jobIngGb: "", bln3tier: false).Tables[0].Copy();
            _dtOptCalMagamQd = kiwoomQuery.p_OptCaMagamOptCallQuery(query: "1", stockCode: "", optcall: "OQD10060", jobDate: "", jobIngGb: "", bln3tier: false).Tables[0].Copy();
        }


        private void OnReceiveTrData_Opt10060PriceMaeSu(string sRQName, DataTable dt, int sPreNext)
        {

            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            string[] sRQNameArray = sRQName.Split(',');

            string stockCode = "";
            string stdDate = "";
            string maxDate = "";
            string minDate = "";
            string chainMaxDate = "";

            stockCode = ClsAxKH.RetStockCodeBysRqName(ClsAxKH.OptType.Opt10060, sRQName);
            stdDate = ClsAxKH.RetStdDateBysRqName(ClsAxKH.OptType.Opt10060, sRQName);

            try
            {
                if (dt != null)
                {
                    maxDate = dt.Compute("max([일자])", string.Empty).ToString().Trim();
                    minDate = dt.Compute("min([일자])", string.Empty).ToString().Trim();

                    ClsDbLogger.OptCallMagamStoredData(actionGb: "A", optCaller: "OPS10060", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "", chainCompGb: "", chainMaxDate: "", chainMinDate: "");

                    var rows = _dtOptCalMagamPs.AsEnumerable().Where(Row => Row.Field<string>("STOCK_CODE") == stockCode);

                    foreach (DataRow dr2th in rows)
                    {
                        if (dr2th["CHAIN_COMP_GB"].ToString().Trim() == "Y")
                        {
                            chainMaxDate = dr2th["CHAIN_MAX_DATE"].ToString().Trim();
                        }
                    }
                }


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

                        WriteTextSafe(stockCode + "(" + dr["일자"] + ")");

                        arrParam.Clear();
                        arrParam.Add("@ACTION_GB", "A");
                        arrParam.Add("@STOCK_CODE", stockCode);
                        arrParam.Add("@STOCK_DATE", dr["일자"]);
                        arrParam.Add("@MAEME_GB", "1"); //  1 - 매수 2 - 매도
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

                        if (chainMaxDate != "")
                        {
                            if (chainMaxDate == dr["일자"].ToString().Trim())
                            {
                                ClsDbLogger.OptCallMagamStoredData(actionGb: "A", optCaller: "OPS10060", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "C", chainCompGb: "", chainMaxDate: "", chainMinDate: "");
                                _ClsOpt10060.Dispose();

                                tcs.SetResult(true);

                                OnGetStockCode(Opt10060TransType.PriceMaesu);

                                return;
                            }
                        }
                    }
                }
                else
                {
                    ClsDbLogger.OptCallMagamStoredData(actionGb: "EE", optCaller: "OPS10060", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "F", chainCompGb: "", chainMaxDate: "", chainMinDate: minDate);

                    _ClsOpt10060.Dispose();
                    tcs.SetResult(true);

                    OnGetStockCode(Opt10060TransType.PriceMaesu);

                    return;
                }


                // 최근 거래일 100일을 가져오는걸로 한다면
                if (chk100.Checked == true)
                {
                    ClsDbLogger.OptCallMagamStoredData(actionGb: "A", optCaller: "OPS10060", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "C", chainCompGb: "", chainMaxDate: "", chainMinDate: "");

                    _ClsOpt10060.Dispose();

                    tcs.SetResult(true);

                    OnGetStockCode(Opt10060TransType.PriceMaesu);

                    return;

                }
                else
                {
                    if (sPreNext == 2)
                    {
                        tcs.SetResult(true);

                        ClsDbLogger.OptCallMagamStoredData(actionGb: "A", optCaller: "OPS10060", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "S", chainCompGb: "", chainMaxDate: "", chainMinDate: "");

                        WaitTime();

                        _ClsOpt10060.SetInit(_FormId);
                        _ClsOpt10060.JustRequest(StartDate: sRQNameArray[1].ToString().Trim(), StockCode: sRQNameArray[2].ToString().Trim(), StockName: "", AmountQtyGb: sRQNameArray[3].ToString().Trim(), MaeMaeGb: sRQNameArray[4].ToString().Trim(), UnitG: sRQNameArray[5].ToString().Trim(), nPrevNext: 2);

                        lblTimePerCount.Text = ClsAxKH._limitCount.ToString();
                        lblTime.Text = ClsAxKH._limitTime.ToString();

                        return;
                    }
                    else
                    {

                        if (dt != null)
                        {
                            ClsDbLogger.OptCallMagamStoredData(actionGb: "CE", optCaller: "OPS10060", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "C", chainCompGb: "", chainMaxDate: "", chainMinDate: minDate);
                        }
                        else
                        {
                            ClsDbLogger.OptCallMagamStoredData(actionGb: "EE", optCaller: "OPS10060", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "F", chainCompGb: "", chainMaxDate: "", chainMinDate: minDate);
                        }

                        _ClsOpt10060.Dispose();

                        tcs.SetResult(true);

                        OnGetStockCode(Opt10060TransType.PriceMaesu);

                        return;
                    }
                }
            }

            catch (Exception )
            {

                _ClsOpt10060.Dispose();

                OnGetStockCode(Opt10060TransType.PriceMaesu);                

                throw;
            }
        }
        private void OnReceiveTrData_Opt10060PriceMaedo(string sRQName, DataTable dt, int sPreNext)
        {
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            string[] sRQNameArray = sRQName.Split(',');

            string stockCode = "";
            string stdDate = "";
            string maxDate = "";
            string minDate = "";
            string chainMaxDate = "";

            stockCode = ClsAxKH.RetStockCodeBysRqName(ClsAxKH.OptType.Opt10060, sRQName);
            stdDate = ClsAxKH.RetStdDateBysRqName(ClsAxKH.OptType.Opt10060, sRQName);

            try
            {
                if (dt != null)
                {
                    maxDate = dt.Compute("max([일자])", string.Empty).ToString().Trim();
                    minDate = dt.Compute("min([일자])", string.Empty).ToString().Trim();

                    ClsDbLogger.OptCallMagamStoredData(actionGb: "A", optCaller: "OPD10060", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "", chainCompGb: "", chainMaxDate: "", chainMinDate: "");

                    var rows = _dtOptCalMagamPd.AsEnumerable().Where(Row => Row.Field<string>("STOCK_CODE") == stockCode);

                    foreach (DataRow dr2th in rows)
                    {
                        if (dr2th["CHAIN_COMP_GB"].ToString().Trim() == "Y")
                        {
                            chainMaxDate = dr2th["CHAIN_MAX_DATE"].ToString().Trim();
                        }
                    }
                }


                if (tcs == null || tcs.Task.IsCompleted)
                {
                    _ClsOpt10060.Dispose();

                    tcs.SetResult(true);

                    OnGetStockCode(Opt10060TransType.PriceMaedo);
                    return;
                }

                if (dt != null)
                {
                    ArrayParam arrParam = new ArrayParam();
                    Sql oSql = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "KIWOOMDB");


                    foreach (DataRow dr in dt.Rows)
                    {

                        WriteTextSafe(stockCode + "(" + dr["일자"] + ")");

                        arrParam.Clear();
                        arrParam.Add("@ACTION_GB", "A");
                        arrParam.Add("@STOCK_CODE", stockCode);
                        arrParam.Add("@STOCK_DATE", dr["일자"]);
                        arrParam.Add("@MAEME_GB", "2"); // 1 - 매수 , 2 - 매도
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

                        if (chainMaxDate != "")
                        {
                            if (chainMaxDate == dr["일자"].ToString().Trim())
                            {
                                ClsDbLogger.OptCallMagamStoredData(actionGb: "A", optCaller: "OPD10060", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "C", chainCompGb: "", chainMaxDate: "", chainMinDate: "");
                                _ClsOpt10060.Dispose();

                                tcs.SetResult(true);

                                OnGetStockCode(Opt10060TransType.PriceMaedo);

                                return;

                            }
                        }
                    }
                }
                else
                {
                    ClsDbLogger.OptCallMagamStoredData(actionGb: "EE", optCaller: "OPD10060", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "F", chainCompGb: "", chainMaxDate: "", chainMinDate: minDate);

                    _ClsOpt10060.Dispose();
                    tcs.SetResult(true);

                    OnGetStockCode(Opt10060TransType.PriceMaedo);

                    return;
                }


                // 최근 거래일 100일을 가져오는걸로 한다면
                if (chk100.Checked == true)
                {
                    ClsDbLogger.OptCallMagamStoredData(actionGb: "A", optCaller: "OPD10060", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "C", chainCompGb: "", chainMaxDate: "", chainMinDate: "");

                    _ClsOpt10060.Dispose();
              
                    tcs.SetResult(true);

                    OnGetStockCode(Opt10060TransType.PriceMaedo);

                    return;

                }
                else
                {
                    if (sPreNext == 2)
                    {
                        tcs.SetResult(true);

                        ClsDbLogger.OptCallMagamStoredData(actionGb: "A", optCaller: "OPD10060", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "S", chainCompGb: "", chainMaxDate: "", chainMinDate: "");

                        WaitTime();

                        _ClsOpt10060.SetInit(_FormId);
                        _ClsOpt10060.JustRequest(StartDate: sRQNameArray[1].ToString().Trim(), StockCode: sRQNameArray[2].ToString().Trim(), StockName: "", AmountQtyGb: sRQNameArray[3].ToString().Trim(), MaeMaeGb: sRQNameArray[4].ToString().Trim(), UnitG: sRQNameArray[5].ToString().Trim(), nPrevNext: 2);

                        lblTimePerCount.Text = ClsAxKH._limitCount.ToString();
                        lblTime.Text = ClsAxKH._limitTime.ToString();

                        return;
                    }
                    else
                    {

                        if (dt != null)
                        {
                            ClsDbLogger.OptCallMagamStoredData(actionGb: "CE", optCaller: "OPD10060", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "C", chainCompGb: "", chainMaxDate: "", chainMinDate: minDate);
                        }
                        else
                        {
                            ClsDbLogger.OptCallMagamStoredData(actionGb: "EE", optCaller: "OPD10060", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "F", chainCompGb: "", chainMaxDate: "", chainMinDate: minDate);
                        }

                        _ClsOpt10060.Dispose();
                        tcs.SetResult(true);

                        OnGetStockCode(Opt10060TransType.PriceMaedo);

                        return;
                    }
                }

            }

            catch (Exception )
            {
                _ClsOpt10060.Dispose();

                OnGetStockCode(Opt10060TransType.PriceMaedo);


                throw;
            }
        }
        private void OnReceiveTrData_Opt10060QtyMaeSu(string sRQName, DataTable dt, int sPreNext)
        {
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            string[] sRQNameArray = sRQName.Split(',');

            string stockCode = "";
            string stdDate = "";
            string maxDate = "";
            string minDate = "";
            string chainMaxDate = "";

            stockCode = ClsAxKH.RetStockCodeBysRqName(ClsAxKH.OptType.Opt10060, sRQName);
            stdDate = ClsAxKH.RetStdDateBysRqName(ClsAxKH.OptType.Opt10060, sRQName);

            try
            {
                if (dt != null)
                {
                    maxDate = dt.Compute("max([일자])", string.Empty).ToString().Trim();
                    minDate = dt.Compute("min([일자])", string.Empty).ToString().Trim();

                    ClsDbLogger.OptCallMagamStoredData(actionGb: "A", optCaller: "OQS10060", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "", chainCompGb: "", chainMaxDate: "", chainMinDate: "");

                    var rows = _dtOptCalMagamQs.AsEnumerable().Where(Row => Row.Field<string>("STOCK_CODE") == stockCode);

                    foreach (DataRow dr2th in rows)
                    {
                        if (dr2th["CHAIN_COMP_GB"].ToString().Trim() == "Y")
                        {
                            chainMaxDate = dr2th["CHAIN_MAX_DATE"].ToString().Trim();
                        }
                    }
                }


                if (tcs == null || tcs.Task.IsCompleted)
                {
                    _ClsOpt10060.Dispose();

                    tcs.SetResult(true);

                    OnGetStockCode(Opt10060TransType.QtyMaesu);
                    return;
                }

                if (dt != null)
                {
                    ArrayParam arrParam = new ArrayParam();
                    Sql oSql = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "KIWOOMDB");


                    foreach (DataRow dr in dt.Rows)
                    {

                        WriteTextSafe(stockCode + "(" + dr["일자"] + ")");

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

                        if (chainMaxDate != "")
                        {
                            if (chainMaxDate == dr["일자"].ToString().Trim())
                            {
                                ClsDbLogger.OptCallMagamStoredData(actionGb: "A", optCaller: "OQS10060", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "C", chainCompGb: "", chainMaxDate: "", chainMinDate: "");
                                _ClsOpt10060.Dispose();

                                tcs.SetResult(true);

                                OnGetStockCode(Opt10060TransType.QtyMaesu);

                                return;

                            }
                        }
                    }
                }
                else
                {
                    ClsDbLogger.OptCallMagamStoredData(actionGb: "EE", optCaller: "OQS10060", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "F", chainCompGb: "", chainMaxDate: "", chainMinDate: minDate);

                    _ClsOpt10060.Dispose();
                    tcs.SetResult(true);

                    OnGetStockCode(Opt10060TransType.QtyMaesu);

                    return;
                }

                // 최근 거래일 100일을 가져오는걸로 한다면
                if (chk100.Checked == true)
                {
                    ClsDbLogger.OptCallMagamStoredData(actionGb: "A", optCaller: "OQS10060", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "C", chainCompGb: "", chainMaxDate: "", chainMinDate: "");

                    _ClsOpt10060.Dispose();
                    tcs.SetResult(true);

                    OnGetStockCode(Opt10060TransType.QtyMaesu);

                    return;
                }
                else
                {
                    if (sPreNext == 2)
                    {
                        tcs.SetResult(true);

                        ClsDbLogger.OptCallMagamStoredData(actionGb: "A", optCaller: "OQS10060", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "S", chainCompGb: "", chainMaxDate: "", chainMinDate: "");

                        //_clsDataAccessUtil.Delay(3600);
                        WaitTime();

                        _ClsOpt10060.SetInit(_FormId);
                        _ClsOpt10060.JustRequest(StartDate: sRQNameArray[1].ToString().Trim(), StockCode: sRQNameArray[2].ToString().Trim(), StockName: "", AmountQtyGb: sRQNameArray[3].ToString().Trim(), MaeMaeGb: sRQNameArray[4].ToString().Trim(), UnitG: sRQNameArray[5].ToString().Trim(), nPrevNext: 2);

                        lblTimePerCount.Text = ClsAxKH._limitCount.ToString();
                        lblTime.Text = ClsAxKH._limitTime.ToString();

                        return;
                    }
                    else
                    {

                        if (dt != null)
                        {
                            ClsDbLogger.OptCallMagamStoredData(actionGb: "CE", optCaller: "OQS10060", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "C", chainCompGb: "", chainMaxDate: "", chainMinDate: minDate);
                        }
                        else
                        {
                            ClsDbLogger.OptCallMagamStoredData(actionGb: "EE", optCaller: "OQS10060", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "F", chainCompGb: "", chainMaxDate: "", chainMinDate: minDate);
                        }

                        _ClsOpt10060.Dispose();
                        tcs.SetResult(true);

                        OnGetStockCode(Opt10060TransType.QtyMaesu);

                        return;
                    }
                }

            }

            catch (Exception)
            {
                _ClsOpt10060.Dispose();

                OnGetStockCode(Opt10060TransType.QtyMaesu);

                return;
            }

        }
        private void OnReceiveTrData_Opt10060QtyMaedo(string sRQName, DataTable dt, int sPreNext)
        {
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            string[] sRQNameArray = sRQName.Split(',');

            string stockCode = "";
            string stdDate = "";
            string maxDate = "";
            string minDate = "";
            string chainMaxDate = "";

            stockCode = ClsAxKH.RetStockCodeBysRqName(ClsAxKH.OptType.Opt10060, sRQName);
            stdDate = ClsAxKH.RetStdDateBysRqName(ClsAxKH.OptType.Opt10060, sRQName);

            try
            {

                if (dt != null)
                {
                    maxDate = dt.Compute("max([일자])", string.Empty).ToString().Trim();
                    minDate = dt.Compute("min([일자])", string.Empty).ToString().Trim();

                    ClsDbLogger.OptCallMagamStoredData(actionGb: "A", optCaller: "OQD10060", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "", chainCompGb: "", chainMaxDate: "", chainMinDate: "");

                    var rows = _dtOptCalMagamQd.AsEnumerable().Where(Row => Row.Field<string>("STOCK_CODE") == stockCode);

                    foreach (DataRow dr2th in rows)
                    {
                        if (dr2th["CHAIN_COMP_GB"].ToString().Trim() == "Y")
                        {
                            chainMaxDate = dr2th["CHAIN_MAX_DATE"].ToString().Trim();
                        }
                    }
                }


                if (tcs == null || tcs.Task.IsCompleted)
                {
                    _ClsOpt10060.Dispose();
                    tcs.SetResult(true);

                    OnGetStockCode(Opt10060TransType.QtyMaeDo);
                    return;
                }

                if (dt != null)
                {
                    ArrayParam arrParam = new ArrayParam();
                    Sql oSql = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "KIWOOMDB");


                    foreach (DataRow dr in dt.Rows)
                    {

                        WriteTextSafe(stockCode + "(" + dr["일자"] + ")");

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

                        if (chainMaxDate != "")
                        {
                            if (chainMaxDate == dr["일자"].ToString().Trim())
                            {
                                ClsDbLogger.OptCallMagamStoredData(actionGb: "A", optCaller: "OQD10060", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "C", chainCompGb: "", chainMaxDate: "", chainMinDate: "");
                                _ClsOpt10060.Dispose();
                                tcs.SetResult(true);

                                OnGetStockCode(Opt10060TransType.QtyMaeDo);

                                return;
                            }
                        }
                    }
                }
                else
                {
                    ClsDbLogger.OptCallMagamStoredData(actionGb: "EE", optCaller: "OQD10060", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "F", chainCompGb: "", chainMaxDate: "", chainMinDate: minDate);

                    _ClsOpt10060.Dispose();
                    tcs.SetResult(true);

                    OnGetStockCode(Opt10060TransType.QtyMaeDo);

                    return;
                }


                // 최근 거래일 100일을 가져오는걸로 한다면
                if (chk100.Checked == true)
                {
                    ClsDbLogger.OptCallMagamStoredData(actionGb: "A", optCaller: "OQD10060", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "C", chainCompGb: "", chainMaxDate: "", chainMinDate: "");

                    _ClsOpt10060.Dispose();
                    tcs.SetResult(true);

                    OnGetStockCode(Opt10060TransType.QtyMaeDo);

                    return;

                }
                else
                {
                    if (sPreNext == 2)
                    {
                        tcs.SetResult(true);

                        ClsDbLogger.OptCallMagamStoredData(actionGb: "A", optCaller: "OQD10060", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "S", chainCompGb: "", chainMaxDate: "", chainMinDate: "");

                        WaitTime();

                        _ClsOpt10060.SetInit(_FormId);
                        _ClsOpt10060.JustRequest(StartDate: sRQNameArray[1].ToString().Trim(), StockCode: sRQNameArray[2].ToString().Trim(), StockName: "", AmountQtyGb: sRQNameArray[3].ToString().Trim(), MaeMaeGb: sRQNameArray[4].ToString().Trim(), UnitG: sRQNameArray[5].ToString().Trim(), nPrevNext: 2);

                        lblTimePerCount.Text = ClsAxKH._limitCount.ToString();
                        lblTime.Text = ClsAxKH._limitTime.ToString();

                        return;
                    }
                    else
                    {

                        if (dt != null)
                        {
                            ClsDbLogger.OptCallMagamStoredData(actionGb: "CE", optCaller: "OQD10060", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "C", chainCompGb: "", chainMaxDate: "", chainMinDate: minDate);
                        }
                        else
                        {
                            ClsDbLogger.OptCallMagamStoredData(actionGb: "EE", optCaller: "OQD10060", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "F", chainCompGb: "", chainMaxDate: "", chainMinDate: minDate);
                        }

                        _ClsOpt10060.Dispose();
                        tcs.SetResult(true);

                        OnGetStockCode(Opt10060TransType.QtyMaeDo);

                        return;
                    }
                }

            }

            catch (Exception ex)
            {
                _ClsOpt10060.Dispose();

                OnGetStockCode(Opt10060TransType.QtyMaeDo);

                throw;
            }
        }


        private void Btn10060QtySellJob_Click(object sender, EventArgs e)
        {
            if (_ClsOpt10060 != null)
            {
                _ClsOpt10060.Dispose();
            }
            else
            {
                _ClsOpt10060 = new ClsOpt10060();
            }

            ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060PriceMaeSu);
            ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060PriceMaedo);
            ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060QtyMaeSu);
            ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060QtyMaedo);
            
            ClsAxKH.AxKH_10060_OnReceived += new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060QtyMaedo);

            OnGetStockCode(Opt10060TransType.QtyMaeDo);
        }

        private void Btn10060QtyBuyJob_Click(object sender, EventArgs e)
        {
            if (_ClsOpt10060 != null)
            {
                _ClsOpt10060.Dispose();
            }
            else
            {
                _ClsOpt10060 = new ClsOpt10060();
            }

            ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060PriceMaeSu);
            ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060PriceMaedo);
            ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060QtyMaeSu);
            ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060QtyMaedo);

            ClsAxKH.AxKH_10060_OnReceived += new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060QtyMaeSu);

            OnGetStockCode(Opt10060TransType.QtyMaesu);
        }

        private void Btn10060PriceSellJob_Click(object sender, EventArgs e)
        {
            if (_ClsOpt10060 != null)
            {
                _ClsOpt10060.Dispose();
            }
            else
            {
                _ClsOpt10060 = new ClsOpt10060();
            }

            ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060PriceMaeSu);
            ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060PriceMaedo);
            ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060QtyMaeSu);
            ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060QtyMaedo);

            ClsAxKH.AxKH_10060_OnReceived += new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060PriceMaedo);
            OnGetStockCode( Opt10060TransType.PriceMaedo);
        }

        private void btn10060PriceBuyJob_Click(object sender, EventArgs e)
        {
            if (_ClsOpt10060 != null)
            {
                _ClsOpt10060.Dispose();
            }
            else
            {
                _ClsOpt10060 = new ClsOpt10060();
            }

            ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060PriceMaeSu);
            ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060PriceMaedo);
            ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060QtyMaeSu);
            ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060QtyMaedo);

            ClsAxKH.AxKH_10060_OnReceived += new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060PriceMaeSu);

            OnGetStockCode(Opt10060TransType.PriceMaesu);
        }

        private void dtpStdDate_ValueChanged(object sender, EventArgs e)
        {
            _stdDate = dtpStdDate.Value.ToString("yyyyMMdd");
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

        private void FrmOpt10060CallerPer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ClsAxKH.SPEED_CALL == true)
            { ClsAxKH.SPEED_CALL = false; }


            ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060PriceMaeSu);
            ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060PriceMaedo);
            ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060QtyMaeSu);
            ClsAxKH.AxKH_10060_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10060QtyMaedo);

            _ClsOpt10060.Dispose();
        }
    }
}
