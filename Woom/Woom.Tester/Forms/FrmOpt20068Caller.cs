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
    public partial class FrmOpt20068Caller : Form
    {
        private Queue _StockQueue = new Queue();

        #region 전역변수

        private DataTable _dtStockCode;
        private int _seqNo = 0;
        private string _stdDate = "";
        private string _FormId = "81";
        private string _stockName;

        private ClsOpt20068 _opt20068 = new ClsOpt20068();

        // 마지막으로 돌린 일자
        private string _LastPsDate = "";
        // 마지막으로 돌린 일자
        private string _FirstPsDate = "";
        #endregion 전역변수
        private ClsDataAccessUtil _clsDataAccessUtil;


        public FrmOpt20068Caller()
        {
            InitializeComponent();

            _clsDataAccessUtil = new ClsDataAccessUtil();

            ClsAxKH.AxKH_20068_OnReceived += new ClsAxKH.OnReceivedEventHandler(Opt20068_OnReceived);

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

            proBar20068.Maximum = _dtStockCode.Rows.Count;

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
            GetOpt20068Caller(strStockCode);

            proBar20068.Value = _seqNo;

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

        private string _MaxStockDate20068 = "";

        private string GetStockCode()
        {
            string reValue;

            if (_StockQueue.Count == 0)
            {
                MessageBox.Show("작업이 완료되었습니다.");
                return "End";
            }
            reValue = _StockQueue.Dequeue().ToString();

            _MaxStockDate20068 = "";

            KiwoomQuery kiwoomQuery = new KiwoomQuery();
            DataTable dt = new DataTable();

            dt = kiwoomQuery.p_Opt20068Query(query: "3", STOCK_CODE: reValue, STOCK_DATE: "", bln3tier: false).Tables[0].Copy();

            if (dt.Rows.Count <= 0)
            {
                _MaxStockDate20068 = "";
            }
            else
            {
                _MaxStockDate20068 = dt.Rows[0]["MAX_STOCK_DATE"].ToString().Trim();
            }
            

            _seqNo = _seqNo + 1;

            return reValue;
        }

        private void GetOpt20068Caller(string stockCode)
        {
            if (_opt20068 != null)
            {
                _opt20068.Dispose();
                _opt20068 = null;
            }

            _opt20068 = new ClsOpt20068();

            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            if (tcs == null || tcs.Task.IsCompleted)
            {
                return;
            }

            _opt20068.SetInit(_FormId);
            _opt20068.JustRequest(startDate:dtpStartDate.Value.ToString("yyyyMMdd"),  endDate: dtpEndDate.Value.ToString("yyyyMMdd"), allGb: "0", stockCode: stockCode, nPrevNext: 0);

            tcs.SetResult(true);

        }

        private void Opt20068_OnReceived(string stockCode, DataTable dt, int sPreNext)
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
                    if (_MaxStockDate20068 == dr["일자"].ToString().Trim())
                    {
                        _opt20068.Dispose();

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
                        arrParam.Add("@LT_CON_CNT", dr["대차거래체결주수"]);
                        arrParam.Add("@LT_REPAY_CNT", dr["대차거래상환주수"]);
                        arrParam.Add("@LT_INCRE", dr["대차거래증감"]);
                        arrParam.Add("@BALANCE_CNT", dr["잔고주수"]);
                        arrParam.Add("@BALANCE_PRICE", dr["잔고금액"]);
                        arrParam.Add("@R_ErrorCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                        oSql.ExecuteNonQuery("p_Opt20068Add", CommandType.StoredProcedure, arrParam);
                    }
                }
            }
            if (sPreNext == 2)
            {
                tcs.SetResult(true);

                _opt20068.SetInit(_FormId);
                _opt20068.JustRequest(startDate: dtpStartDate.Value.ToString("yyyyMMdd"), endDate: dtpEndDate.Value.ToString("yyyyMMdd"), allGb: "0", stockCode: stockCode, nPrevNext: 1);
            }
            else
            {
                _opt20068.Dispose();

                tcs.SetResult(true);
            }

            OnGetStockCode();
        }

        private void btn20068_Click_1(object sender, EventArgs e)
        {
            OnGetStockCode();
        }
    }
}
