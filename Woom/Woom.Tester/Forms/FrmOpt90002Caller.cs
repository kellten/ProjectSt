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

            //Func<DataTable> funcGetStockData = () =>
            //{
            //    RichQuery oRichQuery = new RichQuery();
            //    return oRichQuery.p_ScodeQuery("1", "", "", false).Tables[0].Copy();
            //};

            //_dtStockCode = funcGetStockData();

            Func<DataTable> funcGetKthGp = () =>
            {
                KiwoomQuery kiwoom = new KiwoomQuery();
                return kiwoom.p_KthgpQuery("1", "", "", false).Tables[0].Copy();
            };

            _dtKthgp = funcGetKthGp();

            proBar90002.Maximum = _dtKthgp.Rows.Count;

            foreach (DataRow dr in _dtKthgp.Rows)
            {
                _StockQueue.Enqueue(dr["KTH_CODE"].ToString());
            }


            //_dt = new DataTable();
            ///// 999 - 코스피, 코스닥
            //_dt = _clsGetKoaStudioMethod.GetCodeListByMarketCallBackDataTable("0").Copy();
        }

        private Queue _StockQueue = new Queue();

        #region 전역변수

        private DataTable _dtKthgp;
        private DataTable _dt;
        private Woom.DataAccess.OptCaller.Class.ClsGetKoaStudioMethod _clsGetKoaStudioMethod = new DataAccess.OptCaller.Class.ClsGetKoaStudioMethod();
        private int _seqNo = 0;
        private string _FormId = "01";
        private ClsOpt90002 _opt90002 = new ClsOpt90002();

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
            string kthCode = "";

            kthCode = GetKthCode();

            if (kthCode == "End")
            { return; }
            
            proBar90002.Value = _seqNo;
            if (kthCode == "")
            {
                OnGetKthCode();
                return;

            }

            WaitTime();

            GetOpt90002Caller(kthCode);


            WriteTextSafe(kthCode + " 작업 중");
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

        private void GetOpt90002Caller(string kthCode)
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
            _opt90002.JustRequest(dateGb: "1", kthCode: kthCode, nPrevNext: 0);

            tcs.SetResult(true);

        }

        private void Opt90002_OnReceived(string sRQName, DataTable dt, int sPreNext)
        {

            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();
            
            if (tcs == null || tcs.Task.IsCompleted)
            {
                return;
            }


            string[] sRQNameArray = sRQName.Split(',');

            string stockCode = ClsAxKH.RetStockCodeBysRqName(ClsAxKH.OptType.Opt90002, sRQName);

            if (dt != null)
            {
                ArrayParam arrParam = new ArrayParam();
                Sql oSql = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "KIWOOMDB");

                foreach (DataRow dr in dt.Rows)
                {
                   
                        arrParam.Clear();

                        //DataTable stockName = _dt.AsEnumerable().Where(Row => Row.Field<string>("STOCK_NAME") == dr["STOCK_NAME"].ToString().Trim()).CopyToDataTable();

                        arrParam.Add("@ACTION_GB", "A");
                        arrParam.Add("@KTH_CODE", stockCode);
                        arrParam.Add("@STOCK_CODE", dr["종목코드"].ToString().Trim());
                        arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                        oSql.ExecuteNonQuery("p_KthstAdd", CommandType.StoredProcedure, arrParam);

                        WriteTextSafe("(" + stockCode + ")" + dr["종목코드"].ToString().Trim());
                }
            }
            
                _opt90002.Dispose();

                tcs.SetResult(true);

                OnGetKthCode();

        }
        private void btn90002_Click(object sender, EventArgs e)
        {
            OnGetKthCode();
        }
    }
}
