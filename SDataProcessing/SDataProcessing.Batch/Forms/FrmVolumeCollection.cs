using SDataAccess;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Woom.DataAccess.OptCaller.Class;

namespace SDataProcessing.Batch.Forms
{
    // 1시간 - 천번
    public partial class FrmVolumeCollection : Form
    {
        public FrmVolumeCollection()
        {
            InitializeComponent();
            //_clsCpSvr7254.CpSvr7254_OnReceived += CpSvr7254_OnReceived;
            //_clsCpSvr7254.CpSvr7254_OnEndGetData += CpSvr7254_OnEndGetData;

            // _opt10059.Opt10059_OnReceived += Opt10059_OnReceived;
            //_opt10081.Opt10081_OnReceived += Opt10081_OnReceived;

            _dtStockCode = _clsCybosConnection.LoadStockCode();
            GetStockDataSet();

            proBar10059Qty.Maximum = _dtScode.Rows.Count;
            proBar10059Price.Maximum = _dtScode.Rows.Count;
            proBar10081.Maximum = _dtScode.Rows.Count;

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
                { _stdDate = CDateTime.FormatDate(System.DateTime.Now.Date.ToString()); }
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
        #region 전역변수
        private CybosDa.DataAccess.Connection.clsCybosConnection _clsCybosConnection = new CybosDa.DataAccess.Connection.clsCybosConnection();
        private CybosDa.DataAccess.CpSvr.clsCpSvr7254 _clsCpSvr7254 = new CybosDa.DataAccess.CpSvr.clsCpSvr7254();
        private ClsOpt10059 _opt10059 = new ClsOpt10059();
        private ClsOpt10081 _opt10081 = new ClsOpt10081();

        private DataTable _dtStockCode = new DataTable();
        private DataTable _dtScode = new DataTable();

        private int _Cntopt10059Qty = 0;
        private int _Cntopt10059Price = 0;
        private int _Cntopt10081 = 0;

        private string _MaxStockDate10059Qty = "";
        private string _MaxStockDate10059Price = "";
        private string _MaxStockDate10081 = "";
        private string _MaxStockDate10060PriceMaesu = "";
        private string _MaxStockDate10060PriceMaedo = "";
        private string _MaxStockDate10060QtyMaesu = "";
        private string _MaxStockDate10060QtyMaedo = "";

        private string _FormId = "01";
        private string _stdDate = "";

        #endregion

        #region Func
        private void GetStockDataSet()
        {
            RichQuery richQuery = new RichQuery();

            _dtScode = richQuery.p_ScodeQuery("1", "", "", false).Tables[0].Copy();

            DataColumn newCol = new DataColumn("DA_STOCK_CODE", typeof(string));
            newCol.AllowDBNull = true;
            _dtScode.Columns.Add(newCol);
            foreach (DataRow dr in _dtStockCode.Rows)
            {
                var rowsToUpdate = _dtScode.AsEnumerable().Where(r => r.Field<string>("STOCK_CODE") == Convert.ToString(dr["STOCK_CODE"]));

                foreach (var row in rowsToUpdate)
                {
                    row.SetField("DA_STOCK_CODE", Convert.ToString(dr["DA_STOCK_CODE"]));
                }
            }

            foreach (DataRow dr in _dtScode.Rows)
            {
                if (dr["DA_STOCK_CODE"].ToString().Trim() == "")
                {
                    rcReport.Text = rcReport.Text + dr["STOCK_NAME"].ToString().Trim() + "\r\n";
                }
            }

            // newCol.AllowDBNull = false;
        }

        #region Opt10059Qty
        private async Task GetOpt10059QtyCaller()
        {
            for (int i = 0; i < _dtScode.Rows.Count; i++)
            {
                _MaxStockDate10059Qty = "";
                if (GetCheckOpt10059Qty(i) == false)
                {
                    continue;
                }
                string CallstockCode = "";
                CallstockCode = _dtScode.Rows[i]["STOCK_CODE"].ToString().Trim();

                proBar10059Qty.Value = i + 1;
                lblStockName.Text = _dtScode.Rows[i]["STOCK_NAME"].ToString().Trim();

                TaskCompletionSource<bool> tcs = null;
                tcs = new TaskCompletionSource<bool>();

                _opt10059.Opt10059_OnReceived += (string stockCode, DataTable dt, int sPreNext) =>
                {
                    if (tcs == null || tcs.Task.IsCompleted)
                    { return; }
                    Opt10059_OnReceived(stockCode, dt, sPreNext);
                    tcs.SetResult(true);
                };
                GetOpt10059Qty(CallstockCode, _dtScode.Rows[i]["STOCK_NAME"].ToString().Trim());
                await tcs.Task;
                await Task.Delay(TimeSpan.FromSeconds(5));
                _opt10059.Dispose();

            }
        }
        private bool GetCheckOpt10059Qty(int i)
        {
            KiwoomQuery kiwoomQuery = new KiwoomQuery();
            DataTable dt = new DataTable();

            dt = kiwoomQuery.p_Opt10059QtyMinMaxQuery("1", _dtScode.Rows[i]["STOCK_CODE"].ToString().Trim(), "", false).Tables[0].Copy();

            _MaxStockDate10059Qty = dt.Rows[0]["MAX_STOCK_DATE"].ToString().Trim();

            if (_stdDate == _MaxStockDate10059Qty)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        private void GetOpt10059Qty(string stockCode, string stockName)
        {
            _Cntopt10059Qty = _Cntopt10059Qty + 1;
            _opt10059.SetInit(_FormId);
            if (_opt10059.SetValue(_stdDate, stockCode, stockName, "2", "0", "1") == false)
            {
                return;
            }

            _opt10059.Opt10059();
        }
        private async void Opt10059_OnReceived(string stockCode, DataTable dt, int sPreNext)
        {
            //if (dt == null)
            //{
            //    await Task.Delay(TimeSpan.FromSeconds(3));
            //    _opt10059.Dispose();
            //    GetOpt10059QtyStockCode();
            //    return;
            //}

            if (dt != null)
            {
                ArrayParam arrParam = new ArrayParam();
                Sql oSql = new Sql("EDPB2F011\\VADIS", "KIWOOMDB");

                foreach (DataRow dr in dt.Rows)
                {
                    if (_MaxStockDate10059Qty == dr["일자"].ToString().Trim())
                    {
                        _opt10059.Dispose();
                        await Task.Delay(TimeSpan.FromSeconds(4));

                        return;
                    }
                    else
                    {
                        arrParam.Clear();
                        arrParam.Add("@ACTION_GB", "A");
                        arrParam.Add("@STOCK_CODE", stockCode);
                        arrParam.Add("@STOCK_DATE", dr["일자"]);
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

                        oSql.ExecuteNonQuery("p_Opt10059_QtyAdd", CommandType.StoredProcedure, arrParam);
                    }
                }
            }

            if (sPreNext == 2)
            {
                _opt10059.Opt10059(true);
            }
            else
            {
                await Task.Delay(TimeSpan.FromSeconds(4));
                _opt10059.Dispose();

            }

        }

        #endregion

        #region Opt10059Price
        private bool GetCheckOpt10059Price(int i)
        {
            KiwoomQuery kiwoomQuery = new KiwoomQuery();
            DataTable dt = new DataTable();

            dt = kiwoomQuery.p_Opt10059PriceMinMaxQuery("1", _dtScode.Rows[i]["STOCK_CODE"].ToString().Trim(), "", false).Tables[0].Copy();

            _MaxStockDate10059Price = dt.Rows[0]["MAX_STOCK_DATE"].ToString().Trim();

            if (_stdDate == _MaxStockDate10059Price)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        private void GetOpt10059PriceStockCode()
        {
            if (_Cntopt10059Price == _dtScode.Rows.Count)
            { return; }
            string stockCode;
            _MaxStockDate10059Price = "";

            for (int i = _Cntopt10059Price; i < _dtScode.Rows.Count; i++)
            {
                if (GetCheckOpt10059Price(i) == true && _dtScode.Rows[i]["DA_STOCK_CODE"].ToString().Trim() != "")
                {
                    _Cntopt10059Price = i;
                    break;
                }
            }
            proBar10059Price.Value = _Cntopt10059Price + 1;
            lblStockName2.Text = _dtScode.Rows[_Cntopt10059Price]["STOCK_NAME"].ToString().Trim();

            stockCode = _dtScode.Rows[_Cntopt10059Price]["DA_STOCK_CODE"].ToString().Trim();
            GetOpt10059Price(stockCode, _dtScode.Rows[_Cntopt10059Price]["STOCK_NAME"].ToString().Trim());
        }

        private async Task GetOpt10059PriceCaller()
        {
            for (int i = 0; i < _dtScode.Rows.Count; i++)
            {
                string GetStockCode;
                _MaxStockDate10059Price = "";

                if (GetCheckOpt10059Price(i) == false)
                {
                    continue;
                }
                proBar10059Price.Value = i + 1;
                lblStockName2.Text = _dtScode.Rows[i]["STOCK_NAME"].ToString().Trim();

                GetStockCode = _dtScode.Rows[_Cntopt10059Price]["DA_STOCK_CODE"].ToString().Trim();

                TaskCompletionSource<bool> tcs = null;
                tcs = new TaskCompletionSource<bool>();

                _clsCpSvr7254.CpSvr7254_OnReceived += (string stockCode, DataTable dt, int NextCall) =>
                {
                    if (tcs == null || tcs.Task.IsCompleted)
                    { return; }
                    CpSvr7254_OnReceived(stockCode, dt, NextCall);
                    tcs.SetResult(true);
                };
                GetOpt10059Price(GetStockCode, _dtScode.Rows[i]["STOCK_NAME"].ToString().Trim());
                await tcs.Task;
                await Task.Delay(TimeSpan.FromSeconds(4));
                _clsCpSvr7254.Dispose();

            }
        }
        private void GetOpt10059Price(string stockCode, string stockName)
        {
            _Cntopt10059Price = _Cntopt10059Price + 1;
            _clsCpSvr7254.UseCpSvr7254();
            _clsCpSvr7254.GetCpSvr7254(stockCode, CybosDa.DataAccess.CpSvr.clsCpSvr7254.ChoiceGiganTypeIndex.일별, CDateTime.FormatDate(DateTime.Today.AddDays(-1).ToString("yyyyMMdd")), CDateTime.FormatDate(DateTime.Today.AddDays(-1).ToString("yyyyMMdd")),
                                       CybosDa.DataAccess.CpSvr.clsCpSvr7254.ChoiceTradeTypeIndex.순매수,
                                       CybosDa.Common.Class.ClsDefineDataType.TradeGb.TradeGbTypeIndex.전체,
                                       CybosDa.DataAccess.CpSvr.clsCpSvr7254.ChoiceDataGbIndex.추정금액백만원);
        }
        private async void CpSvr7254_OnReceived(string stockCode, DataTable dt, int NextCall)
        {
            ArrayParam arrParam = new ArrayParam();
            Sql oSql = new Sql("EDPB2F011\\VADIS", "KIWOOMDB");

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["일자"].ToString().Trim().Length < 8)
                {
                    continue;
                }
                if (_MaxStockDate10059Price == dr["일자"].ToString().Trim())
                {
                    _clsCpSvr7254.Dispose();
                    await Task.Delay(TimeSpan.FromSeconds(3));
                    return;
                }
                else
                {
                    arrParam.Clear();
                    arrParam.Add("@ACTION_GB", "A");
                    arrParam.Add("@STOCK_CODE", stockCode.Substring(1, stockCode.Trim().Length - 1));
                    arrParam.Add("@STOCK_DATE", dr["일자"]);
                    arrParam.Add("@DATE_SEQNO", 0);
                    arrParam.Add("@NUJUK_TRDAEGUM", 0);
                    arrParam.Add("@GAIN_PRICE", dr["개인"]);
                    arrParam.Add("@FORE_PRICE", dr["외국인"]);
                    arrParam.Add("@GIGAN_PRICE", dr["기관계"]);
                    arrParam.Add("@GUMY_PRICE", dr["금융투자"]);
                    arrParam.Add("@BOHUM_PRICE", dr["보험"]);
                    arrParam.Add("@TOSIN_PRICE", dr["투신"]);
                    arrParam.Add("@GITA_PRICE", dr["기타금융"]);
                    arrParam.Add("@BANK_PRICE", dr["은행"]);
                    arrParam.Add("@YEONGI_PRICE", dr["연기금"]);
                    arrParam.Add("@SAMO_PRICE", dr["사모펀드"]);
                    arrParam.Add("@NATION_PRICE", dr["국가지자체"]);
                    arrParam.Add("@BUBIN_PRICE", dr["기타법인"]);
                    arrParam.Add("@IOFORE_PRICE", dr["기타외인"]);
                    arrParam.Add("@GIGAN_SUM_PRICE", Convert.ToInt32(dr["금융투자"]) + Convert.ToInt32(dr["보험"]) + Convert.ToInt32(dr["투신"]) +
                                                   Convert.ToInt32(dr["기타금융"]) + Convert.ToInt32(dr["은행"]) + Convert.ToInt32(dr["연기금"]) +
                                                   Convert.ToInt32(dr["사모펀드"]) + Convert.ToInt32(dr["국가지자체"]));
                    arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                    oSql.ExecuteNonQuery("p_Opt10059_PriceAdd", CommandType.StoredProcedure, arrParam);

                }
            }
            if (NextCall == 2)
            {
                _clsCpSvr7254.NextCaller();
            }
            else
            {
                _clsCpSvr7254.Dispose();
                await Task.Delay(TimeSpan.FromSeconds(4));
                return;
            }

        }
        private void CpSvr7254_OnEndGetData()
        {

        }

        #endregion

        #region Opt10081
        private async Task GetOpt10081Caller()
        {
            for (int i = 0; i < _dtScode.Rows.Count; i++)
            {
                _MaxStockDate10081 = "";
                if (GetCheckOpt10081(i) == false)
                {
                    continue;
                }
                string CallstockCode = "";
                CallstockCode = _dtScode.Rows[i]["STOCK_CODE"].ToString().Trim();

                proBar10081.Value = i + 1;
                lblStockName3.Text = _dtScode.Rows[i]["STOCK_NAME"].ToString().Trim();

                TaskCompletionSource<bool> tcs = null;
                tcs = new TaskCompletionSource<bool>();

                _opt10081.Opt10081_OnReceived += (string stockCode, DataTable dt, int sPreNext) =>
                {
                    if (tcs == null || tcs.Task.IsCompleted)
                    { return; }
                    Opt10081_OnReceived(stockCode, dt, sPreNext);
                    tcs.SetResult(true);
                };
                GetOpt10081(CallstockCode, _dtScode.Rows[i]["STOCK_NAME"].ToString().Trim());
                await tcs.Task;
                await Task.Delay(TimeSpan.FromSeconds(4));
                _opt10081.Dispose();
            }
        }

        private bool GetCheckOpt10081(int i)
        {
            KiwoomQuery kiwoomQuery = new KiwoomQuery();
            DataTable dt = new DataTable();

            dt = kiwoomQuery.p_Opt10081MinMaxQuery("1", _dtScode.Rows[i]["STOCK_CODE"].ToString().Trim(), "", false).Tables[0].Copy();

            _MaxStockDate10081 = dt.Rows[0]["MAX_STOCK_DATE"].ToString().Trim();

            if (_stdDate == _MaxStockDate10081)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        private void GetOpt10081(string stockCode, string stockName)
        {
            _Cntopt10081 = _Cntopt10081 + 1;
            _opt10081.SetInit(_FormId);
            if (_opt10081.SetValue(stockCode, stockName, _stdDate, "1") == false)
            {
                return;
            }

            _opt10081.Opt10081();
        }
        private async void Opt10081_OnReceived(string stockCode, DataTable dt, int sPreNext)
        {
            //if (dt == null)
            //{
            //    await Task.Delay(TimeSpan.FromSeconds(3));
            //    _opt10081.Dispose();
            //    GetOpt10081StockCode();
            //    return;
            //}
            if (dt != null)
            {
                ArrayParam arrParam = new ArrayParam();
                Sql oSql = new Sql("EDPB2F011\\VADIS", "KIWOOMDB");

                foreach (DataRow dr in dt.Rows)
                {
                    if (_MaxStockDate10081 == dr["일자"].ToString().Trim())
                    {
                        _opt10081.Dispose();
                        await Task.Delay(TimeSpan.FromSeconds(4));

                        return;
                    }
                    else
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
            }
            if (sPreNext == 2)
            {
                _opt10081.Opt10081(true);
            }
            else
            {
                await Task.Delay(TimeSpan.FromSeconds(4));
                _opt10081.Dispose();

                return;
            }
        }
        #endregion

        #endregion

        #region Event
        private async void btnStart_Click(object sender, EventArgs e)
        {
            // GetOpt10059QtyStockCode();
            // GetOpt10081StockCode();
            await GetOpt10081Caller();
            await GetOpt10059QtyCaller();
        }
        #endregion

        private async void btnStart10059Price_Click(object sender, EventArgs e)
        {
            await GetOpt10059PriceCaller();
        }

        private void BtnOpt10060All_Click(object sender, EventArgs e)
        {

        }

        private async void btn10059QtyStart_Click(object sender, EventArgs e)
        {
            await GetOpt10059QtyCaller();
        }
    }
}
