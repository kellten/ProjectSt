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
    public partial class FrmOpt10081Caller : Form
    {

        private Queue _StockQueue = new Queue();

        #region 전역변수

        private DataTable _dtStockCode;
        private int _seqNo = 0;
        private string _stdDate = "";
        private string _FormId = "81";
        private string _stockName;

        private ClsOpt10081 _opt10081 = new ClsOpt10081();

        // 마지막으로 돌린 일자
        private string _LastPsDate = "";
        // 마지막으로 돌린 일자
        private string _FirstPsDate = "";
        #endregion 전역변수
        private ClsDataAccessUtil _clsDataAccessUtil;


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
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            //Task.Delay(3000).Wait();
            _clsDataAccessUtil.Delay(3000);

            tcs.SetResult(true);

            string strStockCode = "";
           
            strStockCode = GetStockCode();
            if (strStockCode == "End")
            { return; }
            GetOpt10081Caller(strStockCode);

            proBar10081.Value = _seqNo;

            WriteTextSafe(strStockCode + "(" + ClsAxKH.GetMasterCodeName(strStockCode) + ")" + " 작업 중");

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

        private string _MaxStockDate10081 = "";

        private string GetStockCode()
        {
            string reValue;

            if (_StockQueue.Count == 0)
            {
                MessageBox.Show("작업이 완료되었습니다.");
                return "End";
            }
            reValue = _StockQueue.Dequeue().ToString();

            _MaxStockDate10081 = "";

            KiwoomQuery kiwoomQuery = new KiwoomQuery();
            DataTable dt = new DataTable();

            dt = kiwoomQuery.p_Opt10081MinMaxQuery("1", reValue.ToString().Trim(), "", false).Tables[0].Copy();

            _MaxStockDate10081 = dt.Rows[0]["MAX_STOCK_DATE"].ToString().Trim();

            _seqNo = _seqNo + 1;

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

            _opt10081.SetInit(_FormId);
            _opt10081.Opt10081(stockCode, "", _stdDate, "1");

            tcs.SetResult(true);

        }

        private  void Opt10081_OnReceived(string stockCode, DataTable dt, int sPreNext)
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
                    if (_MaxStockDate10081 == dr["일자"].ToString().Trim())
                    {
                        _opt10081.Dispose();

                        tcs.SetResult(true);

                        OnGetStockCode();

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
                tcs.SetResult(true);

                _opt10081.SetInit(_FormId);
                _opt10081.Opt10081(StockCode: stockCode, StockName: "", StdDate: _stdDate, ModifyJugaGb: "1",nextCall: true); ;

            }
            else
            {
                _opt10081.Dispose();

                tcs.SetResult(true);
            }

            OnGetStockCode();
        }
        private void btn10081_Click(object sender, EventArgs e)
        {
            OnGetStockCode();
        }
    }
}
