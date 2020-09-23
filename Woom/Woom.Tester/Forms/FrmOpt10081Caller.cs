using System;
using SDataAccess;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        private ClsOpt10081 _opt10081 = new ClsOpt10081();

        // 마지막으로 돌린 일자
        private string _LastPsDate = "";
        // 마지막으로 돌린 일자
        private string _FirstPsDate = "";
        #endregion 전역변수


        public FrmOpt10081Caller()
        {
            InitializeComponent();

            Func<DataTable> funcGetStockData = () =>
            {
                RichQuery oRichQuery = new RichQuery();
                return oRichQuery.p_ScodeQuery("1", "", "", false).Tables[0].Copy();
            };

            _dtStockCode = funcGetStockData();

            foreach (DataRow dr in _dtStockCode.Rows)
            {
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

            string strStockCode = "";

            Task<string> t = Task.Run(() => strStockCode = GetStockCode());

            t.ContinueWith(task => GetOpt10081Caller(strStockCode));

            proBar10081.Value = _seqNo;

            WriteTextSafe(t.Result + " 작업 중");
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
            //_opt10081.Opt10081_OnReceived += new ClsOpt10081.OnReceivedEventHandler(Opt10081_OnReceived);
            ClsOptCallerMain.AxKH_10081_OnReceived += new ClsOptCallerMain.OnReceivedEventHandler(Opt10081_OnReceived);

            _opt10081.SetInit(_FormId);
            if (_opt10081.SetValue(stockCode, "", _stdDate, "1") == false)
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

            Task<int> funcTaskAsync = Task.Run(() =>
            {

                if (dt != null)
                {
                    ArrayParam arrParam = new ArrayParam();
                    Sql oSql = new Sql("EDPB2F011\\VADIS", "KIWOOMDB");

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (_MaxStockDate10081 == dr["일자"].ToString().Trim())
                        {
                            return 1;
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
                    return 0;
                }
                else
                {
                    return 1;
                }
            });

            if (await funcTaskAsync == 1)
            {
                WriteTextSafe(stockCode + "완료 다음으로..");
                OnGetStockCode();
                return;
            }

            Task<int> t = Task.Run(() =>
            {
                if (sPreNext == 2)
                {

                    return 0;
                    //_opt10081.Opt10081(true);
                }
                else
                {

                    return 1;
                    //await Task.Delay(TimeSpan.FromSeconds(4));
                    //_opt10081.Dispose();
                    // return;
                }
            });


            if (await t == 1)
            {
                OnGetStockCode();
                return;
            }
            else
            {
                WriteTextSafe(stockCode + "(다음일자로..)");
                _opt10081.Opt10081(true);
            }
        }
        private void btn10081_Click(object sender, EventArgs e)
        {
            OnGetStockCode();
        }
    }
}
