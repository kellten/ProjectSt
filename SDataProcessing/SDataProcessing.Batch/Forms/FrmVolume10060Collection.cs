using SDataAccess;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Woom.DataAccess.OptCaller.Class;

namespace SDataProcessing.Batch.Forms
{
    public partial class FrmVolume10060Collection : Form
    {
        public FrmVolume10060Collection()
        {
            InitializeComponent();

            GetStockDataSet();

            proBar10060.Maximum = _dtScode.Rows.Count;

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

            lblStdDate.Text = _stdDate;
        }

        #region 전역변수
        private ClsOpt10060 _opt10060 = new ClsOpt10060();
        private DataTable _dtScode = new DataTable();

        private bool _priceMaeSu = false;
        private bool _priceMaeDo = false;
        private bool _qtyMaeSu = false;
        private bool _qtyMaeDo = false;

        private string _minStartDate = "";

        private string _FormId = "02";
        private string _stdDate = "";
        #endregion

        #region Event
        public delegate void OnReceivedEventHandler();
        public event OnReceivedEventHandler Opt10060_EndData;
        #endregion

        #region func
        private void GetStockDataSet()
        {
            RichQuery richQuery = new RichQuery();

            _dtScode = richQuery.p_ScodeQuery("1", "", "", false).Tables[0].Copy();

        }
        #endregion

        #region Opt10060
        private async Task GetOpt10060Caller()
        {
            string errorStockName = "";
            try
            {

                for (int i = 0; i < _dtScode.Rows.Count; i++)
                {

                    string CallstockCode = "";
                    string CallstockName = "";
                    bool blnChk = false;
                    DataTable dtDate = new DataTable();
                    KiwoomQuery kiwoom = new KiwoomQuery();

                    _priceMaeSu = false;
                    _priceMaeDo = false;
                    _qtyMaeSu = false;
                    _qtyMaeDo = false;

                    CallstockCode = _dtScode.Rows[i]["STOCK_CODE"].ToString().Trim();
                    CallstockName = _dtScode.Rows[i]["STOCK_NAME"].ToString().Trim();
                    errorStockName = CallstockName;
                    proBar10060.Value = i + 1;

                    if (chkLastDay.Checked == true)
                    {
                        dtDate = kiwoom.p_Opt10060MinMaxQuery("1", CallstockCode, "", "", false).Tables[0].Copy();

                        if (dtDate != null)
                        {
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
                        }

                        if (blnChk == false)
                        {
                            dtDate = null;
                            dtDate = new DataTable();
                            continue;
                        }

                        dtDate = null;
                        dtDate = new DataTable();

                    }


                    dtDate = kiwoom.p_Opt10059PriceMinMaxQuery("2", CallstockCode, "", false).Tables[0].Copy();

                    _minStartDate = dtDate.Rows[0]["MIN_STOCK_DATE"].ToString().Trim();

                    dtDate = null;
                    dtDate = new DataTable();


                    TaskCompletionSource<bool> tcs = null;
                    tcs = new TaskCompletionSource<bool>();

                    {
                        if (tcs == null || tcs.Task.IsCompleted)
                        { return; }

                        bool result = await Opt10060PriceMaeSuCaller(CallstockCode, CallstockName);
                        tcs.SetResult(result);
                    };
                    tcs.Task.Dispose();
                    tcs = null;

                    TaskCompletionSource<bool> tcs2 = null;
                    tcs2 = new TaskCompletionSource<bool>();

                    {
                        if (tcs2 == null || tcs2.Task.IsCompleted)
                        { return; }

                        bool result = await Opt10060PriceMaeDoCaller(CallstockCode, CallstockName);
                        tcs2.SetResult(result);
                    };
                    tcs2.Task.Dispose();
                    tcs2 = null;

                    TaskCompletionSource<bool> tcs3 = null;
                    tcs3 = new TaskCompletionSource<bool>();

                    {
                        if (tcs3 == null || tcs3.Task.IsCompleted)
                        { return; }

                        bool result = await Opt10060QtyMaeSuCaller(CallstockCode, CallstockName);
                        tcs3.SetResult(result);
                    };
                    tcs3.Task.Dispose();
                    tcs3 = null;

                    TaskCompletionSource<bool> tcs4 = null;
                    tcs4 = new TaskCompletionSource<bool>();

                    {
                        if (tcs4 == null || tcs4.Task.IsCompleted)
                        { return; }

                        bool result = await Opt10060QtyMaeDoCaller(CallstockCode, CallstockName);
                        tcs4.SetResult(result);
                    };
                    tcs4.Task.Dispose();
                    tcs4 = null;



                    //TaskCompletionSource<bool> tcs = null;
                    //tcs = new TaskCompletionSource<bool>();
                    //Opt10060PriceMaeSuCaller<string, string> = (CallstockCode, CallstockName) =>
                    //{
                    //    if (tcs == null || tcs.Task.IsCompleted)
                    //    { return; }

                    //    tcs.SetResult(true);
                    //};

                    //var t1 = Task.Run(async () => { await Opt10060PriceMaeSuCaller(CallstockCode, CallstockName); });
                    //Task.WaitAll(t1);
                    //var t2 = Task.Run(async () => { await Opt10060PriceMaeDoCaller(CallstockCode, CallstockName); });
                    //var t3 = Task.Run(async () => { await Opt10060QtyMaeSuCaller(CallstockCode, CallstockName); });
                    //var t4 = Task.Run(async () => { await Opt10060QtyMaeDoCaller(CallstockCode, CallstockName); });

                    //await Task.Delay(TimeSpan.FromSeconds(3));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(errorStockName + ex.ToString());
                throw;
            }
        }

        private async Task<bool> Opt10060PriceMaeSuCaller(string stockCode, string stockName)
        {
            int cnt = 0;
            bool firstCall = true;
            string startDate = "";
            DataTable dtDate = new DataTable();
            KiwoomQuery kiwoom = new KiwoomQuery();

            // _priceMaeSu가 뭐냐? true가 아니면 true이면,...
            while (_priceMaeSu != true)
            {
                cnt = cnt + 1;

                lblStockName.Text = stockName + " PRICE(매수)_" + cnt.ToString();

                if (firstCall == true)
                {
                    if (chkLastDay.Checked == true)
                    { dtDate = kiwoom.p_Opt10060PriceMinMaxQuery("1", stockCode, "1", "", false).Tables[0].Copy(); }
                    else
                    { dtDate = kiwoom.p_Opt10060PriceMinMaxQuery("2", stockCode, "1", "", false).Tables[0].Copy(); }

                    if (dtDate != null)
                    {
                        // 최근일부터
                        if (chkLastDay.Checked == true)
                        {
                            if (dtDate.Rows[0]["MAX_STOCK_DATE"].ToString().Trim() != "")
                            {
                                _minStartDate = dtDate.Rows[0]["MAX_STOCK_DATE"].ToString().Trim();
                                startDate = _stdDate;
                            }
                            else
                            {
                                startDate = _stdDate;
                            }
                        }
                        else
                        {
                            // 처음부터
                            if (dtDate.Rows[0]["MIN_STOCK_DATE"].ToString().Trim() != "")
                            {
                                startDate = dtDate.Rows[0]["MIN_STOCK_DATE"].ToString().Trim();
                            }
                            else
                            {
                                startDate = _stdDate;
                            }

                        }

                    }
                    else
                    {
                        startDate = _stdDate;
                    }
                    dtDate = null;
                    dtDate = new DataTable();
                    // 해당시작일자부터 주세요~~~~
                    SetOpt10060(startDate, stockCode, stockName, "1", "1", "1");
                    firstCall = false;

                    await GetOpt10060PriceMaeSu(true);
                }
                else
                {
                    await GetOpt10060PriceMaeSu(_priceMaeSu);
                }
            }

            firstCall = true;
            startDate = "";
            _opt10060.Dispose();
            cnt = 0;
            await Task.Delay(TimeSpan.FromSeconds(3));

            return true;
        }
        private async Task<bool> Opt10060PriceMaeDoCaller(string stockCode, string stockName)
        {
            int cnt = 0;
            bool firstCall = true;
            string startDate = "";
            DataTable dtDate = new DataTable();
            KiwoomQuery kiwoom = new KiwoomQuery();

            while (_priceMaeDo != true)
            {
                cnt = cnt + 1;

                lblStockName.Text = stockName + " PRICE(매도)_" + cnt.ToString();

                if (firstCall == true)
                {

                    if (chkLastDay.Checked == true)
                    {
                        dtDate = kiwoom.p_Opt10060PriceMinMaxQuery("1", stockCode, "2", "", false).Tables[0].Copy();
                    }
                    else
                    {
                        dtDate = kiwoom.p_Opt10060PriceMinMaxQuery("2", stockCode, "2", "", false).Tables[0].Copy();
                    }

                    if (dtDate != null)
                    {
                        if (chkLastDay.Checked == true)
                        {
                            if (dtDate.Rows[0]["MAX_STOCK_DATE"].ToString().Trim() != "")
                            {
                                _minStartDate = dtDate.Rows[0]["MAX_STOCK_DATE"].ToString().Trim();
                                startDate = _stdDate;
                            }
                            else
                            {
                                startDate = _stdDate;
                            }
                        }
                        else
                        {
                            if (dtDate.Rows[0]["MIN_STOCK_DATE"].ToString().Trim() != "")
                            {
                                startDate = dtDate.Rows[0]["MIN_STOCK_DATE"].ToString().Trim();
                            }
                            else
                            {
                                startDate = _stdDate;
                            }
                        }

                    }
                    else
                    {
                        startDate = _stdDate;
                    }
                    dtDate = null;
                    dtDate = new DataTable();

                    SetOpt10060(startDate, stockCode, stockName, "1", "2", "1");
                    firstCall = false;
                    await GetOpt10060PriceMaeDo(true);
                }
                else
                {
                    await GetOpt10060PriceMaeDo(_priceMaeDo);
                }
            }

            firstCall = true;
            startDate = "";
            _opt10060.Dispose();
            await Task.Delay(TimeSpan.FromSeconds(3));

            return true;

        }
        private async Task<bool> Opt10060QtyMaeSuCaller(string stockCode, string stockName)
        {

            int cnt = 0;
            bool firstCall = true;
            string startDate = "";
            DataTable dtDate = new DataTable();
            KiwoomQuery kiwoom = new KiwoomQuery();

            while (_qtyMaeSu != true)
            {
                cnt = cnt + 1;
                lblStockName.Text = stockName + " QTY(매수)_" + cnt.ToString();

                if (firstCall == true)
                {
                    if (chkLastDay.Checked == true)
                    {
                        dtDate = kiwoom.p_Opt10060QtyMinMaxQuery("1", stockCode, "1", "", false).Tables[0].Copy();
                    }
                    else
                    {
                        dtDate = kiwoom.p_Opt10060QtyMinMaxQuery("2", stockCode, "1", "", false).Tables[0].Copy();
                    }

                    if (dtDate != null)
                    {
                        if (chkLastDay.Checked == true)
                        {

                            if (dtDate.Rows[0]["MAX_STOCK_DATE"].ToString().Trim() != "")
                            {
                                _minStartDate = dtDate.Rows[0]["MAX_STOCK_DATE"].ToString().Trim();
                                startDate = _stdDate;
                            }
                            else
                            {
                                startDate = _stdDate;
                            }
                        }
                        else
                        {

                            if (dtDate.Rows[0]["MIN_STOCK_DATE"].ToString().Trim() != "")
                            {
                                startDate = dtDate.Rows[0]["MIN_STOCK_DATE"].ToString().Trim();
                            }
                            else
                            {
                                startDate = _stdDate;
                            }
                        }

                    }
                    else
                    {
                        startDate = _stdDate;
                    }
                    firstCall = false;
                    SetOpt10060(startDate, stockCode, stockName, "2", "1", "1");
                    await GetOpt10060QtyMaeSu(true);
                }
                else
                {
                    await GetOpt10060QtyMaeSu(_qtyMaeSu);
                }
            }

            firstCall = true;
            startDate = "";
            _opt10060.Dispose();
            await Task.Delay(TimeSpan.FromSeconds(3));

            return true;
        }
        private async Task<bool> Opt10060QtyMaeDoCaller(string stockCode, string stockName)
        {
            int cnt = 0;
            bool firstCall = true;
            string startDate = "";
            DataTable dtDate = new DataTable();
            KiwoomQuery kiwoom = new KiwoomQuery();

            while (_qtyMaeDo != true)
            {

                cnt = cnt + 1;
                lblStockName.Text = stockName + " QTY(매수)_" + cnt.ToString(); ;

                if (firstCall == true)
                {
                    if (chkLastDay.Checked == true)
                    {
                        dtDate = kiwoom.p_Opt10060QtyMinMaxQuery("1", stockCode, "2", "", false).Tables[0].Copy();
                    }
                    else
                    {
                        dtDate = kiwoom.p_Opt10060QtyMinMaxQuery("2", stockCode, "2", "", false).Tables[0].Copy();
                    }

                    if (dtDate != null)
                    {
                        if (chkLastDay.Checked == true)
                        {
                            if (dtDate.Rows[0]["MAX_STOCK_DATE"].ToString().Trim() != "")
                            {
                                _minStartDate = dtDate.Rows[0]["MAX_STOCK_DATE"].ToString().Trim();
                                startDate = _stdDate;
                            }
                            else
                            {
                                startDate = _stdDate;
                            }

                        }
                        else
                        {
                            if (dtDate.Rows[0]["MIN_STOCK_DATE"].ToString().Trim() != "")
                            {
                                startDate = dtDate.Rows[0]["MIN_STOCK_DATE"].ToString().Trim();
                            }
                            else
                            {
                                startDate = _stdDate;
                            }
                        }
                    }
                    else
                    {
                        startDate = _stdDate;
                    }
                    dtDate = null;
                    dtDate = new DataTable();

                    firstCall = false;
                    SetOpt10060(startDate, stockCode, stockName, "2", "2", "1");
                    await GetOpt10060QtyMaeDo(true);
                }
                else
                {
                    await GetOpt10060QtyMaeDo(_qtyMaeDo);
                }
            }

            firstCall = true;
            startDate = "";
            _opt10060.Dispose();
            await Task.Delay(TimeSpan.FromSeconds(3));

            return true;
        }

        private async Task<bool> GetOpt10060PriceMaeSu(bool isNext)
        {

            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            _opt10060.Opt10060_OnReceived += (string stockCode, DataTable dt, int sPreNext) =>
            {
                if (tcs == null || tcs.Task.IsCompleted)
                { return; }
                OnReceiveTrData_Opt10060PriceMaeSu(stockCode, dt, sPreNext);
                tcs.SetResult(true);
            };

            JustQuest(isNext);
            await tcs.Task;
            tcs.Task.Dispose();
            tcs = null;

            await Task.Delay(TimeSpan.FromSeconds(2));

            return true;
        }
        private async Task<bool> GetOpt10060PriceMaeDo(bool isNext)
        {

            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            _opt10060.Opt10060_OnReceived += (string stockCode, DataTable dt, int sPreNext) =>
            {
                if (tcs == null || tcs.Task.IsCompleted)
                { return; }
                OnReceiveTrData_Opt10060PriceMaedo(stockCode, dt, sPreNext);
                tcs.SetResult(true);
            };

            JustQuest(isNext);
            await tcs.Task;
            tcs.Task.Dispose();
            tcs = null;

            await Task.Delay(TimeSpan.FromSeconds(2));

            return true;
        }
        private async Task<bool> GetOpt10060QtyMaeSu(bool isNext)
        {
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            _opt10060.Opt10060_OnReceived += (string stockCode, DataTable dt, int sPreNext) =>
            {
                if (tcs == null || tcs.Task.IsCompleted)
                { return; }
                OnReceiveTrData_Opt10060MaeSu(stockCode, dt, sPreNext);
                tcs.SetResult(true);
            };

            JustQuest(isNext);
            await tcs.Task;
            tcs.Task.Dispose();
            tcs = null;

            await Task.Delay(TimeSpan.FromSeconds(2));

            return true;
        }
        private async Task<bool> GetOpt10060QtyMaeDo(bool isNext)
        {

            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            _opt10060.Opt10060_OnReceived += (string stockCode, DataTable dt, int sPreNext) =>
            {
                if (tcs == null || tcs.Task.IsCompleted)
                { return; }
                OnReceiveTrData_Opt10060Maedo(stockCode, dt, sPreNext);
                tcs.SetResult(true);
            };
            JustQuest(isNext);
            await tcs.Task;
            tcs.Task.Dispose();
            tcs = null;

            await Task.Delay(TimeSpan.FromSeconds(2));

            return true;

        }

        private void SetOpt10060(string StartDate, string StockCode, string StockName, string AmountQtyGb, string MaeMaeGb, string UnitGb)
        {
            _opt10060.SetInit(_FormId);
            if (_opt10060.SetValue(StartDate, StockCode, StockName, AmountQtyGb, MaeMaeGb, UnitGb) == false)
            {
                return;
            }
        }
        private void JustQuest(bool isNext)
        {
            // await Task.Delay(TimeSpan.FromSeconds(4));
            if (isNext == true)
            {
                _opt10060.Opt10060();
            }
            else
            {
                _opt10060.Opt10060(true);
            }

        }

        private async void OnReceiveTrData_Opt10060MaeSu(string stockCode, DataTable dt, int sPreNext)
        {
            ArrayParam arrParam = new ArrayParam();
            Sql oSql = new Sql("EDPB2F011\\VADIS", "KIWOOMDB");

            if (dt != null)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["일자"].ToString() == _minStartDate)
                    {

                        Sql oSql2 = new Sql("EDPB2F011\\VADIS", "RICHDB");

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

                        _qtyMaeSu = true;
                        _opt10060.Dispose();
                        await Task.Delay(TimeSpan.FromSeconds(4));

                        return;
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
                _qtyMaeSu = false;
                await Task.Delay(TimeSpan.FromSeconds(4));
            }
            else
            {

                Sql oSql2 = new Sql("EDPB2F011\\VADIS", "RICHDB");

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
                _qtyMaeSu = true;
                _opt10060.Dispose();
                await Task.Delay(TimeSpan.FromSeconds(4));


            }

        }
        private async void OnReceiveTrData_Opt10060Maedo(string stockCode, DataTable dt, int sPreNext)
        {
            ArrayParam arrParam = new ArrayParam();
            Sql oSql = new Sql("EDPB2F011\\VADIS", "KIWOOMDB");

            if (dt != null)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["일자"].ToString() == _minStartDate)
                    {
                        Sql oSql2 = new Sql("EDPB2F011\\VADIS", "RICHDB");

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

                        _qtyMaeDo = true;
                        _opt10060.Dispose();
                        await Task.Delay(TimeSpan.FromSeconds(3));

                        return;
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
                //_opt10060.Opt10060(true);
                _qtyMaeDo = false;
                await Task.Delay(TimeSpan.FromSeconds(3));
            }
            else
            {

                Sql oSql2 = new Sql("EDPB2F011\\VADIS", "RICHDB");

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
                _opt10060.Dispose();
                _qtyMaeDo = true;

                await Task.Delay(TimeSpan.FromSeconds(3));

            }
        }
        private async void OnReceiveTrData_Opt10060PriceMaeSu(string stockCode, DataTable dt, int sPreNext)
        {
            ArrayParam arrParam = new ArrayParam();
            Sql oSql = new Sql("EDPB2F011\\VADIS", "KIWOOMDB");

            if (dt != null)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["일자"].ToString() == _minStartDate)
                    {
                        Sql oSql2 = new Sql("EDPB2F011\\VADIS", "RICHDB");

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

                        _priceMaeSu = true;
                        _opt10060.Dispose();
                        await Task.Delay(TimeSpan.FromSeconds(3));

                        return;
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
                _priceMaeSu = false;
                await Task.Delay(TimeSpan.FromSeconds(3));
            }
            else
            {
                Sql oSql2 = new Sql("EDPB2F011\\VADIS", "RICHDB");

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

                _opt10060.Dispose();
                _priceMaeSu = true;
                await Task.Delay(TimeSpan.FromSeconds(3));


            }
        }
        private async void OnReceiveTrData_Opt10060PriceMaedo(string stockCode, DataTable dt, int sPreNext)
        {
            ArrayParam arrParam = new ArrayParam();
            Sql oSql = new Sql("EDPB2F011\\VADIS", "KIWOOMDB");

            if (dt != null)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["일자"].ToString() == _minStartDate)
                    {
                        Sql oSql2 = new Sql("EDPB2F011\\VADIS", "RICHDB");

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

                        _priceMaeDo = true;
                        _opt10060.Dispose();
                        await Task.Delay(TimeSpan.FromSeconds(4));

                        return;
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
                _priceMaeDo = false;
                await Task.Delay(TimeSpan.FromSeconds(4));
            }
            else
            {
                Sql oSql2 = new Sql("EDPB2F011\\VADIS", "RICHDB");

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

                _opt10060.Dispose();
                _priceMaeDo = true;

                await Task.Delay(TimeSpan.FromSeconds(4));

            }
        }

        #endregion

        private void FrmVolume10060Collection_Load(object sender, EventArgs e)
        {

        }

        private void btn10060All_Click(object sender, EventArgs e)
        {
            GetOpt10060Caller();
        }

        private void proBar10060_Click(object sender, EventArgs e)
        {

        }

        private void lblStockName_Click(object sender, EventArgs e)
        {

        }

        private void chkLastDay_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblStdDate_Click(object sender, EventArgs e)
        {

        }
    }


}
