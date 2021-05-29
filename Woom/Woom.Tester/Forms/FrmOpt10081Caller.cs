using SDataAccess;
using System;
using System.Collections;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Woom.DataAccess;
using Woom.DataAccess.Logger;
using Woom.DataAccess.OptCaller.Class;
using Woom.DataAccess.PlugIn;
using Woom.DataDefine.OptData;
using Woom.DataDefine.Util;

namespace Woom.Tester.Forms
{
    public partial class FrmOpt10081Caller : Form
    {

        private Queue _StockQueue = new Queue();

        #region 전역변수

        private DataTable _dtStockCode;
        private int _seqNo = 0;
        private string _stdDate = "";
        private string _FormId = "81";
        private int _ScreenNo = 100;

        private ClsOpt10081 _opt10081 = new ClsOpt10081();

        #endregion 전역변수

        private ClsDataAccessUtil _clsDataAccessUtil;
        private ClsUtil _clsUtil = new ClsUtil();
        private ClsCollectOptDataFunc _clsCollectOptDataFunc = new ClsCollectOptDataFunc();

        public FrmOpt10081Caller()
        {
            InitializeComponent();

            _clsDataAccessUtil = new ClsDataAccessUtil();

            ClsAxKH.AxKH_10081_OnReceived += new ClsAxKH.OnReceivedEventHandler(Opt10081_OnReceived);

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

            proBar10081.Maximum = _dtStockCode.Rows.Count;

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
            { _clsDataAccessUtil.Delay(600); }
            else
            { _clsDataAccessUtil.Delay(3600); }

            tcs.SetResult(true);
        }

        private void OnGetStockCode()
        {
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            string strStockCode = "";
           
            strStockCode = GetStockCode();
            if (strStockCode == "End")
            { return; }

            if (strStockCode == "")
            {
                OnGetStockCode();
                return;
            }

            WaitTime();

            GetOpt10081Caller(strStockCode);

            proBar10081.Value = _seqNo;

            WriteTextSafe(strStockCode + "(" + ClsAxKH.GetMasterCodeName(strStockCode) + ")" + " 작업 중");

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

        private string GetStockCode()
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
                var rows = _dtOptCalMagam.AsEnumerable().Where(Row => Row.Field<string>("STOCK_CODE") == reValue);

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
                var rows = _dtOptCalMagam.AsEnumerable().Where(Row => Row.Field<string>("STOCK_CODE") == reValue);

                foreach (DataRow dr2th in rows)
                {
                    if (dr2th["CHAIN_COMP_GB"].ToString().Trim() == "Y")
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

        private void GetOpt10081Caller(string stockCode)
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

            SetFormId();
            _opt10081.SetInit(_FormId);
            _opt10081.JustRequest(StockCode: stockCode, StockName: "", StdDate: _stdDate, ModifyJugaGb: "1", nPrevNext: 0);

            tcs.SetResult(true);

        }
        private DataTable _dtOptCalMagam;
        private void GetOptCallMagamaData(string stdDate)
        {
            if (_dtOptCalMagam != null)
            {
                _dtOptCalMagam = null;
                _dtOptCalMagam = new DataTable();
            }

            KiwoomQuery kiwoomQuery = new KiwoomQuery();
            _dtOptCalMagam = kiwoomQuery.p_OptCaMagamOptCallQuery(query: "1",  stockCode: "", optcall: "OPT10081", jobDate: "", jobIngGb: "", bln3tier: false).Tables[0].Copy();
        }

        private void Opt10081_OnReceived(string sRQName, DataTable dt, int sPreNext)
        {

            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            string[] sRQNameArray = sRQName.Split(',');

            string stockCode = "";
            string stdDate = "";
            string maxDate = "";
            string minDate = "";
            string chainMaxDate = "";

            try
            {

                if (dt != null)
                {
                    stockCode = ClsAxKH.RetStockCodeBysRqName(ClsAxKH.OptType.Opt10081, sRQName);
                    stdDate = ClsAxKH.RetStdDateBysRqName(ClsAxKH.OptType.Opt10081, sRQName);
                    maxDate = dt.Compute("max([일자])", string.Empty).ToString().Trim();
                    minDate = dt.Compute("min([일자])", string.Empty).ToString().Trim();

                    ClsDbLogger.OptCallMagamStoredData(actionGb: "A", optCaller: "OPT10081", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "", chainCompGb: "", chainMaxDate: "", chainMinDate: "");

                    var rows = _dtOptCalMagam.AsEnumerable().Where(Row => Row.Field<string>("STOCK_CODE") == stockCode);

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

                        if (chainMaxDate != "")
                        {
                            if (chainMaxDate == dr["일자"].ToString().Trim())
                            {
                                ClsDbLogger.OptCallMagamStoredData(actionGb: "A", optCaller: "OPT10081", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "C", chainCompGb: "", chainMaxDate: "", chainMinDate: "");
                                _opt10081.Dispose();

                                tcs.SetResult(true);

                                OnGetStockCode();

                                return;

                            }
                        }
                    }
                }

                // 최근 거래일 100일을 가져오는걸로 한다면
                if (chk100.Checked == true)
                {
                    ClsDbLogger.OptCallMagamStoredData(actionGb: "A", optCaller: "OPT10081", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "C", chainCompGb: "", chainMaxDate: "", chainMinDate: "");

                    _opt10081.Dispose();

                    tcs.SetResult(true);

                    OnGetStockCode();

                    return;

                }
                else
                {
                    if (sPreNext == 2)
                    {
                        tcs.SetResult(true);

                        ClsDbLogger.OptCallMagamStoredData(actionGb: "A", optCaller: "OPT10081", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "S", chainCompGb: "", chainMaxDate: "", chainMinDate: "");

                        _clsDataAccessUtil.Delay(3600);

                        _opt10081.SetInit(_FormId);
                        _opt10081.JustRequest(StockCode: sRQNameArray[1].ToString().Trim(), StockName: "", StdDate: sRQNameArray[2].ToString().Trim(), ModifyJugaGb: sRQNameArray[3].ToString().Trim(), nPrevNext: 2);

                    }
                    else
                    {

                        if (dt != null)
                        {
                            ClsDbLogger.OptCallMagamStoredData(actionGb: "CE", optCaller: "OPT10081", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "C", chainCompGb: "", chainMaxDate: "", chainMinDate: minDate);
                        }
                        else
                        {
                            ClsDbLogger.OptCallMagamStoredData(actionGb: "A", optCaller: "OPT10081", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "F", chainCompGb: "", chainMaxDate: "", chainMinDate: minDate);
                        }

                        _opt10081.Dispose();

                        tcs.SetResult(true);

                        OnGetStockCode();
                        return;
                    }
                }

            }

            catch (Exception ex)
            {
                _opt10081.Dispose();

                tcs.SetResult(true);

                OnGetStockCode();

                MessageBox.Show(ex.Message.ToString());
                throw;
            }
        }
        private void btn10081_Click(object sender, EventArgs e)
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

        private void dtpStdDate_ValueChanged(object sender, EventArgs e)
        {
            _stdDate = dtpStdDate.Value.ToString("yyyyMMdd");
        }
    }
}
