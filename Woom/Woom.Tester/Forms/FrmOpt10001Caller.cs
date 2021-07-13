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
using Woom.DataDefine.OptData;
using Woom.Telegram.Class;
using Woom.Tester.Class;

namespace Woom.Tester.Forms
{
    public partial class FrmOpt10001Caller : Form
    {
        #region 전역변수

        private DataTable _dtStockCode;
        private int _seqNo = 0;
        private string _FormId = "";
        private int _ScreenNo = 100;

        private ClsOpt10001 _opt10001 = new ClsOpt10001();

        private Queue _StockQueue = new Queue();

        private ClsDataAccessUtil _clsDataAccessUtil;
        private ClsUtil _clsUtil = new ClsUtil();
        private ClsCollectOptDataFunc _clsCollectOptDataFunc = new ClsCollectOptDataFunc();

        #endregion 전역변수
        private bool _AutoStart;
        public FrmOpt10001Caller(DataTable UserDt, bool AutoStart = false, bool chk100Click = false)
        {
            InitializeComponent();

            _clsDataAccessUtil = new ClsDataAccessUtil();

            ClsAxKH.AxKH_10001_OnReceived += new ClsAxKH.OnReceivedEventHandler(Opt10001_OnReceived);

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

            proBar10001.Maximum = _dtStockCode.Rows.Count;

            chk100.Checked = chk100Click;

            _AutoStart = AutoStart;

            if (AutoStart == true)
            {
                string text = "";
                string errorMessage = null;
                text = "OPT10001 작업 Start";
                ClsTelegramBot.SendMessage(text, out errorMessage);
                OnGetStockCode();
            }
        }

        private void SetFormId()
        {
            ClsUtil clsUtil = new ClsUtil();
            _FormId = clsUtil.Right(_ScreenNo.ToString(), 2);

            _ScreenNo = _ScreenNo + 1;
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

            KiwoomQuery kiwoomQuery = new KiwoomQuery();
            DataTable dt = new DataTable();

            dt = kiwoomQuery.p_Opt10001Query(query: "1", stockCode: reValue, callDate: "", bln3tier: false).Tables[0].Copy();

            if (dt.Rows.Count <= 0)
            {
                return reValue;
            }
            else
            {
                if (DateTime.Now.ToString("yyyyMMdd") == dt.Rows[0]["CALL_DATE"].ToString().Trim())
                {
                    return "";
                }
                else
                {
                    return reValue;
                }
            }
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
            string strStockCode = "";

            strStockCode = GetStockCode();

            if (strStockCode == "End")
            {
                string text = "";
                string errorMessage = null;
                text = "OPT10001 작업 완료";
                ClsTelegramBot.SendMessage(text, out errorMessage);
                if (_AutoStart == true)
                {
                    ClsTesterUtil clsTesterUtil = new ClsTesterUtil();
                    Form oform = new Woom.Tester.Forms.FrmOpt10060CallerPer(null, true, true);

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

            GetOpt10001Caller(strStockCode);

            proBar10001.Value = _seqNo;

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
        private void GetOpt10001Caller(string stockCode)
        {
            if (_opt10001 != null)
            {
                _opt10001.Dispose();
                _opt10001 = null;
            }

            _opt10001 = new ClsOpt10001();

            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            if (tcs == null || tcs.Task.IsCompleted)
            {
                return;
            }

            _opt10001.SetInit(_FormId);
            _opt10001.JustRequest(StockCode:stockCode,StockName:"",nPrevNext: 0);

            tcs.SetResult(true);

        }

        private void Opt10001_OnReceived(string sRQName, DataTable dt, int sPreNext)
        {

            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            string[] sRQNameArray = sRQName.Split(',');

            string stockCode = ClsAxKH.RetStockCodeBysRqName(ClsAxKH.OptType.Opt10001, sRQName);

            if (tcs == null || tcs.Task.IsCompleted)
            {
                return;
            }


            try
            {

            if (dt != null)
            {
                ArrayParam arrParam = new ArrayParam();
                Sql oSql = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "KIWOOMDB");

                foreach (DataRow dr in dt.Rows)
                {
                   
                        arrParam.Clear();
                        arrParam.Add("@ACTION_GB", "A");
                        arrParam.Add("@STOCK_CODE", dr["종목코드"]);
                        arrParam.Add("@CALL_DATE", "");
                        arrParam.Add("@CALL_TIME", "");
                        arrParam.Add("@결산월", dr["결산월"]);
                        arrParam.Add("@액면가", ConvertInt32ToString(dr["액면가"].ToString()));
                        arrParam.Add("@자본금", ConvertInt32ToString(dr["자본금"].ToString()));
                        arrParam.Add("@상장주식", ConvertInt32ToString(dr["상장주식"].ToString()));
                        arrParam.Add("@신용비율", ConvertDecimalToString(dr["신용비율"].ToString()));
                        arrParam.Add("@연중최고", ConvertInt32ToString(dr["연중최고"].ToString()));
                        arrParam.Add("@연중최저", ConvertInt32ToString(dr["연중최저"].ToString()));
                        arrParam.Add("@시가총액", ConvertInt32ToString(dr["시가총액"].ToString()));
                        arrParam.Add("@시가총액비중", dr["시가총액비중"].ToString());
                        arrParam.Add("@외인소진률", ConvertDecimalToString(dr["외인소진률"].ToString()));
                        arrParam.Add("@대용가", ConvertInt32ToString(dr["대용가"].ToString()));
                        arrParam.Add("@PER", ConvertDecimalToString(dr["PER"].ToString()));
                        arrParam.Add("@EPS", ConvertInt32ToString(dr["EPS"].ToString()));
                        arrParam.Add("@ROE", ConvertDecimalToString(dr["ROE"].ToString()));
                        arrParam.Add("@PBR", ConvertDecimalToString(dr["PBR"].ToString()));
                        arrParam.Add("@EV", ConvertDecimalToString(dr["EV"].ToString()));
                        arrParam.Add("@BPS", ConvertInt32ToString(dr["BPS"].ToString()));
                        arrParam.Add("@매출액", ConvertInt32ToString(dr["매출액"].ToString()));
                        arrParam.Add("@영업이익", ConvertDecimalToString(dr["영업이익"].ToString()));
                        arrParam.Add("@당기순이익", ConvertDecimalToString(dr["당기순이익"].ToString()));
                        arrParam.Add("@최고250", ConvertInt32ToString(dr["250최고"].ToString()));
                        arrParam.Add("@최저250", ConvertInt32ToString(dr["250최저"].ToString()));
                        arrParam.Add("@시가", ConvertInt32ToString(dr["시가"].ToString()));
                        arrParam.Add("@고가", ConvertInt32ToString(dr["고가"].ToString()));
                        arrParam.Add("@저가", ConvertInt32ToString(dr["저가"].ToString()));
                        arrParam.Add("@상한가", ConvertInt32ToString(dr["상한가"].ToString()));
                        arrParam.Add("@하한가", ConvertInt32ToString(dr["하한가"].ToString()));
                        arrParam.Add("@기준가", ConvertInt32ToString(dr["기준가"].ToString()));
                        arrParam.Add("@예상체결가", ConvertInt32ToString(dr["예상체결가"].ToString()));
                        arrParam.Add("@예상체결수량", ConvertInt32ToString(dr["예상체결수량"].ToString()));
                        arrParam.Add("@최고가일250", dr["250최고가일"].ToString());
                        arrParam.Add("@최고가대비율250", ConvertDecimalToString(dr["250최고가대비율"].ToString()));
                        arrParam.Add("@최저가일250", dr["250최저가일"].ToString());
                        arrParam.Add("@최저가대비율250", ConvertDecimalToString(dr["250최저가대비율"].ToString()));
                        arrParam.Add("@현재가", ConvertInt32ToString(dr["현재가"].ToString()));
                        arrParam.Add("@대비기호", dr["대비기호"].ToString());
                        arrParam.Add("@전일대비", ConvertDecimalToString(dr["전일대비"].ToString()));
                        arrParam.Add("@등락율", ConvertDecimalToString(dr["등락율"].ToString()));
                        arrParam.Add("@거래량", ConvertInt32ToString(dr["거래량"].ToString()));
                        arrParam.Add("@거래대비액", ConvertDecimalToString(dr["거래대비"].ToString()));
                        arrParam.Add("@액면가단위", dr["액면가단위"].ToString());
                        arrParam.Add("@유통주식", ConvertInt32ToString(dr["유통주식"].ToString()));
                        arrParam.Add("@유통비율", ConvertDecimalToString(dr["유통비율"].ToString()));
                        arrParam.Add("@R_ErrorCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                        oSql.ExecuteNonQuery("p_Opt10001Add", CommandType.StoredProcedure, arrParam);
                    
                }
            }

            
                _opt10001.Dispose();

                tcs.SetResult(true);
            
            OnGetStockCode();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                _opt10001.Dispose();

                tcs.SetResult(true);

                OnGetStockCode();
                
                return;
                throw;
            }
        }

        private int ConvertInt32ToString(string str)
        {
            if (str.Contains(".") == true)
            {
                return Convert.ToInt32(Convert.ToDecimal(str));
            }

            if (str == "")
            { return 0; }
            else
            { return Convert.ToInt32(str); }
        }

        private decimal ConvertDecimalToString(string str)
        {
            if (str == "")
            { return 0; }
            else
            { return Convert.ToDecimal(str); }
        }

        private void btn10001_Click(object sender, EventArgs e)
        {
            string text = "";
            string errorMessage = null;
            text = "OPT10001 작업 Start";
            ClsTelegramBot.SendMessage(text, out errorMessage);

            OnGetStockCode();
        }
    }
}
