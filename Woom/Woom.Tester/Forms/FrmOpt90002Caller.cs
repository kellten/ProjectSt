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
    public partial class FrmOpt90002Caller : Form
    {
        public FrmOpt90002Caller()
        {

            InitializeComponent();

            _clsDataAccessUtil = new ClsDataAccessUtil();

            ClsAxKH.AxKH_90002_OnReceived += new ClsAxKH.OnReceivedEventHandler(Opt90002_OnReceived);

            Func<DataTable> funcGetStockData = () =>
            {
                RichQuery oRichQuery = new RichQuery();
                return oRichQuery.p_ScodeQuery("1", "", "", false).Tables[0].Copy();
            };

            _dtStockCode = funcGetStockData();

            Func<DataTable> funcGetKthGp = () =>
            {
                KiwoomQuery kiwoom = new KiwoomQuery();
                return kiwoom.p_KthgpQuery("1", "", "", false).Tables[0].Copy();
            };

            _dtKthgp = funcGetStockData();

            foreach (DataRow dr in _dtKthgp.Rows)
            {
                _StockQueue.Enqueue(dr["KTH_CODE"].ToString());
            }

            proBar90002.Maximum = _dtStockCode.Rows.Count;
        }

        private Queue _StockQueue = new Queue();

        #region 전역변수

        private DataTable _dtStockCode;
        private DataTable _dtKthgp;
        private int _seqNo = 0;
        private string _stdDate = "";
        private string _FormId = "15";
        private string _FirstStockDate = ""; // 상장일

        private ClsOpt90002 _opt90002 = new ClsOpt90002();

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
            _clsDataAccessUtil.Delay(3000);
            tcs.SetResult(true);
        }

        private void OnGetKthCode()
        {
            string strStockCode = "";

            strStockCode = GetKthCode();

            if (strStockCode == "End")
            { return; }
            _seqNo = _seqNo + 1;
            proBar90002.Value = _seqNo;
            if (strStockCode == "")
            {
                OnGetKthCode();
                return;

            }

            WaitTime();

            string stockName = ClsAxKH.GetMasterCodeName(stockCode: strStockCode);

            // 종목명을 못 가져오면 상장폐지된 종목으로 생각.
            if (stockName == "")
            {
                OnGetKthCode();
                return;
            }

            _FirstStockDate = ClsAxKH.GetMasterListedStockDate(strStockCode);

            GetOpt90002Caller(strStockCode, _LastDate, _FirstDate);

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

        private string _MaxStockDate90002 = "";

        private string GetKthCode()
        {
            string reValue;

            if (_StockQueue.Count == 0)
            {
                MessageBox.Show("작업이 완료되었습니다.");
                return "End";
            }
            reValue = _StockQueue.Dequeue().ToString();

            _seqNo = _seqNo + 1;

            return reValue;
        }

        private void GetOpt90002Caller(string stockCode, string MaxDate, string MinDate)
        {
            if (_opt90002 != null)
            {
                _opt90002.Dispose();
                _opt90002 = null;
            }

            _opt90002 = new ClsOpt90002();

            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            if (tcs == null || tcs.Task.IsCompleted)
            {
                return;
            }

            _opt90002.SetInit(_FormId);
            _opt90002.JustRequest(stockCode, _stdDate, "", 0);

            tcs.SetResult(true);

        }

        private void Opt90002_OnReceived(string stockCode, DataTable dt, int sPreNext)
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
                    WriteTextSafe(stockCode + "(일자별상세)" + "[" + dr["일자"].ToString() + "] 작업중");
                    if (_LastDate == dr["일자"].ToString().Trim())
                    {
                        if (_FirstDate == _FirstStockDate)
                        {
                            _opt90002.Dispose();

                            tcs.SetResult(true);

                            OnGetKthCode();

                            return;
                        }
                        else
                        {
                            tcs.SetResult(true);

                            WaitTime();

                            _opt90002.SetInit(_FormId);

                            //e, string StartDate, string StockName, int nPrevNext
                            _opt90002.JustRequest(StockCode: stockCode, StartDate: _FirstDate.ToString(), StockName: "", nPrevNext: 0);
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

                        oSql.ExecuteNonQuery("p_Opt90002Add", CommandType.StoredProcedure, arrParam);

                        _LastDate = dr["일자"].ToString();


                    }
                }
            }
            if (sPreNext == 2)
            {
                int minDate = Convert.ToInt32(dt.Compute("min([일자])", string.Empty));

                tcs.SetResult(true);

                WaitTime();

                _opt90002.SetInit(_FormId);
                _opt90002.JustRequest(StockCode: stockCode, StartDate: minDate.ToString(), StockName: "", nPrevNext: 2);

            }
            else
            {
                _opt90002.Dispose();

                tcs.SetResult(true);

                OnGetKthCode();
            }


        }

        private void btn90002_Click(object sender, EventArgs e)
        {

        }
    }
}
