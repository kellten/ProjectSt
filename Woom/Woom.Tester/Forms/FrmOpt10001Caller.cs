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

namespace Woom.Tester.Forms
{
    public partial class FrmOpt10001Caller : Form
    {
        #region 전역변수

        private DataTable _dtStockCode;
        private int _seqNo = 0;
        private string _stdDate = "";
        private string _FormId = "";
        private int _ScreenNo = 100;

        private ClsOpt10001 _opt10001 = new ClsOpt10001();

        private Queue _StockQueue = new Queue();

        private DataTable _dtOptCalMagam;
        private ClsDataAccessUtil _clsDataAccessUtil;
        private ClsUtil _clsUtil = new ClsUtil();
        private ClsCollectOptDataFunc _clsCollectOptDataFunc = new ClsCollectOptDataFunc();

        #endregion 전역변수

        public FrmOpt10001Caller()
        {
            InitializeComponent();

            _clsDataAccessUtil = new ClsDataAccessUtil();

            ClsAxKH.AxKH_10001_OnReceived += new ClsAxKH.OnReceivedEventHandler(Opt10001_OnReceived);

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

            proBar10001.Maximum = _dtStockCode.Rows.Count;
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
            { return; }

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
                        arrParam.Add("@STOCK_CODE", dr["STOCK_CODE"]);
                        arrParam.Add("@CALL_DATE", "");
                        arrParam.Add("@CALL_TIME", "");
                        arrParam.Add("@결산월", dr["결산월"]);
                        arrParam.Add("@액면가", dr["액면가"]);
                        arrParam.Add("@자본금", dr["자본금"]);
                        arrParam.Add("@상장주식", dr["상장주식"]);
                        arrParam.Add("@신용비율", dr["신용비율"]);
                        arrParam.Add("@연중최고", dr["연중최고"]);
                        arrParam.Add("@연중최저", dr["연중최저"]);
                        arrParam.Add("@시가총액", dr["시가총액"]);
                        arrParam.Add("@시가총액비중", dr["시가총액비중"]);
                        arrParam.Add("@외인소진률", dr["외인소진률"]);
                        arrParam.Add("@대용가", dr["대용가"]);
                        arrParam.Add("@PER", dr["PER"]);
                        arrParam.Add("@EPS", dr["EPS"]);
                        arrParam.Add("@ROE", dr["ROE"]);
                        arrParam.Add("@PBR", dr["PBR"]);
                        arrParam.Add("@EV", dr["EV"]);
                        arrParam.Add("@BPS", dr["BPS"]);
                        arrParam.Add("@매출액", dr["매출액"]);
                        arrParam.Add("@영업이익", dr["영업이익"]);
                        arrParam.Add("@당기순이익", dr["당기순이익"]);
                        arrParam.Add("@최고250", dr["최고250"]);
                        arrParam.Add("@최저250", dr["최저250"]);
                        arrParam.Add("@시가", dr["시가"]);
                        arrParam.Add("@고가", dr["고가"]);
                        arrParam.Add("@저가", dr["저가"]);
                        arrParam.Add("@상한가", dr["상한가"]);
                        arrParam.Add("@하한가", dr["하한가"]);
                        arrParam.Add("@기준가", dr["기준가"]);
                        arrParam.Add("@예상체결가", dr["예상체결가"]);
                        arrParam.Add("@예상체결수량", dr["예상체결수량"]);
                        arrParam.Add("@최고가일250", dr["최고가일250"]);
                        arrParam.Add("@최고가대비율250", dr["최고가대비율250"]);
                        arrParam.Add("@최저가일250", dr["최저가일250"]);
                        arrParam.Add("@최저가대비율250", dr["최저가대비율250"]);
                        arrParam.Add("@현재가", dr["현재가"]);
                        arrParam.Add("@대비기호", dr["대비기호"]);
                        arrParam.Add("@전일대비", dr["전일대비"]);
                        arrParam.Add("@등락율", dr["등락율"]);
                        arrParam.Add("@거래량", dr["거래량"]);
                        arrParam.Add("@거래대비액", dr["거래대비액"]);
                        arrParam.Add("@액면가단위", dr["액면가단위"]);
                        arrParam.Add("@유통주식", dr["유통주식"]);
                        arrParam.Add("@유통비율", dr["유통비율"]);
                        arrParam.Add("@R_ErrorCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                        oSql.ExecuteNonQuery("p_Opt10001Add", CommandType.StoredProcedure, arrParam);
                    
                }
            }

            
                _opt10001.Dispose();

                tcs.SetResult(true);
            
            OnGetStockCode();

            }
            catch (Exception)
            {
                _opt10001.Dispose();

                tcs.SetResult(true);

                OnGetStockCode();
                tcs.SetResult(true);
                return;
                throw;
            }
        }

        private void btn10001_Click_1(object sender, EventArgs e)
        {
            OnGetStockCode();
        }
    }
}
