﻿using SDataAccess;
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
using Woom.DataDefine.OptData;
using Woom.Telegram.Class;
using Woom.Tester.Class;

namespace Woom.Tester.Forms
{
    public partial class FrmOpt10015Caller : Form
    {
        #region 전역변수

        private DataTable _dtStockCode;
        private int _seqNo = 0;
        private string _stdDate = "";
        private string _FormId = "";
        private int _ScreenNo = 100;

        private ClsOpt10015 _opt10015 = new ClsOpt10015();

        private Queue _StockQueue = new Queue();

        private DataTable _dtOptCalMagam;
        private ClsDataAccessUtil _clsDataAccessUtil;
        private ClsUtil _clsUtil = new ClsUtil();
        private ClsCollectOptDataFunc _clsCollectOptDataFunc = new ClsCollectOptDataFunc();

        #endregion 전역변수

        public FrmOpt10015Caller(DataTable UserDt,  bool AutoStart, bool chk100Click = false)
        {
 
            InitializeComponent();

            _clsDataAccessUtil = new ClsDataAccessUtil();

            ClsAxKH.AxKH_10015_OnReceived += new ClsAxKH.OnReceivedEventHandler(Opt10015_OnReceived);

            Func<DataTable> funcGetStockData = () =>
            {
                if (UserDt != null)
                {
                    return UserDt.Copy();
                }
                else
                {
                    RichQuery oRichQuery = new RichQuery();
                    return oRichQuery.p_ScodeQuery("1", "", "", false).Tables[0].Copy();
                }
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

            proBar10015.Maximum = _dtStockCode.Rows.Count;

            _stdDate = _clsCollectOptDataFunc.GetAvailableDate();

            dtpStdDate.Value = _clsUtil.StringToDateTime(_stdDate);

            chk100.Checked = chk100Click;

            if (AutoStart == true)
            {
                btn10015_Click(null, new EventArgs());
                Form form = this.MdiParent;
            }

        }

        private void GetOptCallMagamaData(string stdDate)
        {
            if (_dtOptCalMagam != null)
            {
                _dtOptCalMagam = null;
                _dtOptCalMagam = new DataTable();
            }

            KiwoomQuery kiwoomQuery = new KiwoomQuery();
            _dtOptCalMagam = kiwoomQuery.p_OptCaMagamOptCallQuery(query: "1", stockCode: "", optcall: "OPT10015", jobDate: "", jobIngGb: "", bln3tier: false).Tables[0].Copy();
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
            { _clsDataAccessUtil.Delay(550); }
            else
            { _clsDataAccessUtil.Delay(3650); }
            
            tcs.SetResult(true);
        }

        private void OnGetStockCode()
        {
            string strStockCode = "";

            strStockCode = GetStockCode();

            if (strStockCode == "End")
            {
                string text = "";
                string errorMessage = null;
                text = "OPT10015 작업 완료";
                ClsTelegramBot.SendMessage(text, out errorMessage);
                if (chkDailyJob.Checked == true)
                {
                    ClsTesterUtil clsTesterUtil = new ClsTesterUtil();
                    Form oform = new Woom.Tester.Forms.FrmOpt10081Caller(null, true, true);

                    clsTesterUtil.ShowChildForm(oform, false, this);
                }
                return; 
            }
                        
            if (strStockCode == "")
            {
                OnGetStockCode();
                return;
            }

            string stockName = ClsAxKH.GetMasterCodeName(stockCode: strStockCode);

            // 종목명을 못 가져오면 상장폐지된 종목으로 생각.
            if (stockName == "")
            {
                OnGetStockCode();
                return;
            }

             WaitTime();

            GetOpt10015Caller( strStockCode);

            proBar10015.Value = _seqNo;

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
                //MessageBox.Show("작업이 완료되었습니다.");
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

        private void GetOpt10015Caller(string stockCode)
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

            OPS10060.Text = ClsAxKH._limitCount.ToString();
            lblTime.Text = ClsAxKH._limitTime.ToString();

            tcs.SetResult(true);

        }
             

        private void Opt10015_OnReceived(string sRQName, DataTable dt, int sPreNext)
        {

            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            string[] sRQNameArray = sRQName.Split(',');

            string stockCode = "";
            string stdDate = "";
            string maxDate = "";
            string minDate = "";
            string chainMaxDate = "";

            stockCode = ClsAxKH.RetStockCodeBysRqName(ClsAxKH.OptType.Opt10015, sRQName);
            stdDate = ClsAxKH.RetStdDateBysRqName(ClsAxKH.OptType.Opt10015, sRQName);

            try
            {

                if (dt != null)
                {
                   
                    maxDate = dt.Compute("max([일자])", string.Empty).ToString().Trim();
                    minDate = dt.Compute("min([일자])", string.Empty).ToString().Trim();

                    ClsDbLogger.OptCallMagamStoredData(actionGb: "A", optCaller: "OPT10015", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "", chainCompGb: "", chainMaxDate: "", chainMinDate: "");

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

                        if (chainMaxDate != "")
                        {
                            if (chainMaxDate == dr["일자"].ToString().Trim())
                            {
                                ClsDbLogger.OptCallMagamStoredData(actionGb: "A", optCaller: "OPT10015", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "C", chainCompGb: "", chainMaxDate: "", chainMinDate: "");
                                _opt10015.Dispose();

                                tcs.SetResult(true);

                                WaitTime();

                                OnGetStockCode();

                                return;

                            }
                        }
                    }
                }

                // 최근 거래일 100일을 가져오는걸로 한다면
                if (chk100.Checked == true)
                {
                    ClsDbLogger.OptCallMagamStoredData(actionGb: "A", optCaller: "OPT10015", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "C", chainCompGb: "", chainMaxDate: "", chainMinDate: "");

                    _opt10015.Dispose();

                    tcs.SetResult(true);

                    OnGetStockCode();

                    return;

                }
                else
                {
                    if (sPreNext == 2)
                    {
                        tcs.SetResult(true);

                        ClsDbLogger.OptCallMagamStoredData(actionGb: "A", optCaller: "OPT10015", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "S", chainCompGb: "", chainMaxDate: "", chainMinDate: "");

                        WaitTime();

                        _opt10015.SetInit(_FormId);
                        _opt10015.JustRequest(StockCode: sRQNameArray[1].ToString().Trim(), StartDate: sRQNameArray[2].ToString().Trim(), StockName: "", nPrevNext: 2);

                        OPS10060.Text = ClsAxKH._limitCount.ToString();
                        lblTime.Text = ClsAxKH._limitTime.ToString();

                        return;
                    }
                    else
                    {

                        if (dt != null)
                        {
                            ClsDbLogger.OptCallMagamStoredData(actionGb: "CE", optCaller: "OPT10015", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "C", chainCompGb: "", chainMaxDate: "", chainMinDate: minDate);
                        }
                        else
                        {
                            ClsDbLogger.OptCallMagamStoredData(actionGb: "EE", optCaller: "OPT10015", stockCode: stockCode, stdDate: stdDate, maxDate: maxDate, minDate: minDate, jobIngGb: "F", chainCompGb: "", chainMaxDate: "", chainMinDate: minDate);
                        }

                        _opt10015.Dispose();

                        tcs.SetResult(true);

                        OnGetStockCode();

                        return;
                    }
                }

            }

            catch (Exception ex)
            {
                _opt10015.Dispose();

                tcs.SetResult(true);

                OnGetStockCode();

                MessageBox.Show(ex.Message.ToString());
                throw;
            }
        }

        private void btn10015_Click(object sender, EventArgs e)
        {
            string text = "";
            string errorMessage = null;
            text = "OPT10015 작업 Start";
            ClsTelegramBot.SendMessage(text, out errorMessage);

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
