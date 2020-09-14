using SDataAccess;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Woom.DataAccess.OptCaller.Class;

namespace SDataProcessing.Batch.Forms
{
    public partial class frmOpt10060Caller : Form
    {


        #region 전역변수
        private DataTable _dtStockCode;
        private int _seqNo = 0;
        private string _stdDate = "";
        private string _FormId = "03";


        private ClsOpt10060 _opt10060Ps = new ClsOpt10060();
        private ClsOpt10060 _opt10060Pd = new ClsOpt10060();
        private ClsOpt10060 _opt10060Qs = new ClsOpt10060();
        private ClsOpt10060 _opt10060Qd = new ClsOpt10060();

        // 마지막으로 돌린 일자
        private string _LastPsDate = "";
        private string _LastPdDate = "";
        private string _LastQsDate = "";
        private string _LastQdDate = "";

        // 마지막으로 돌린 일자
        private string _FirstPsDate = "";
        private string _FirstPdDate = "";
        private string _FirstQsDate = "";
        private string _FirstQdDate = "";

        // 종목의 매수매도 자료가 다 받아졌는지에 대한 Boolean
        private string _stockCodePs = "";
        private string _stockCodePd = "";
        private string _stockCodeQs = "";
        private string _stockCodeQd = "";
        #endregion

        #region 이벤트
        private delegate void Delegate_OnGetStockCode();
        private event Delegate_OnGetStockCode _OnGetStockCode;

        private delegate void Delegate_OnGetEndPs();
        private event Delegate_OnGetEndPs _OnGetEndJob;
        #endregion

        #region enum
        private enum MMIndex
        {
            PriceMaeSu,
            PriceMaeDo,
            QtyMaeSu,
            QtyMaeDo
        }
        #endregion

        public frmOpt10060Caller()
        {
            InitializeComponent();

            _OnGetStockCode += new Delegate_OnGetStockCode(OnGetStockCode);
            _OnGetEndJob += new Delegate_OnGetEndPs(OnGetEndJob);

            // RichQuery oRichQuery = new RichQuery();

            // Func<DataTable> funcGetStockData = () => oRichQuery.p_ScodeQuery("1", "", "", false).Tables[0].Copy();
            Func<DataTable> funcGetStockData = () =>
            {
                RichQuery oRichQuery = new RichQuery();
                return oRichQuery.p_ScodeQuery("1", "", "", false).Tables[0].Copy();
            };

            _dtStockCode = funcGetStockData();

            proBar10060.Maximum = _dtStockCode.Rows.Count;

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

            _opt10060Ps.Opt10060_OnReceived += new ClsOpt10060.OnReceivedEventHandler(OnReceiveTrData_Opt10060PriceMaeSu);
            _opt10060Pd.Opt10060_OnReceived += new ClsOpt10060.OnReceivedEventHandler(OnReceiveTrData_Opt10060PriceMaedo);
            _opt10060Qs.Opt10060_OnReceived += new ClsOpt10060.OnReceivedEventHandler(OnReceiveTrData_Opt10060QtyMaeSu);
            _opt10060Qd.Opt10060_OnReceived += new ClsOpt10060.OnReceivedEventHandler(OnReceiveTrData_Opt10060QtyMaedo);

        }

        private void btn10060All_Click(object sender, EventArgs e)
        {
            OnGetStockCode();
        }

        private void OnGetEndJob()
        {
            if (_stockCodePd != "")
            {
                if ((_stockCodePd == _stockCodePs) && (_stockCodePd == _stockCodeQs) && (_stockCodePd == _stockCodeQd))
                {
                    OnGetStockCode();
                }
            }
        }


        #region GetStockCode : 작업할 StockCode를 가져온다.
        /// <summary>
        ///  작업할 StockCode를 가져온다.
        /// </summary>
        /// <returns>StockCode</returns>
        private string GetStockCode()
        {

            if (_dtStockCode != null)
            {

                string strStockCode = "";

                

                    DataTable dtDate = new DataTable();

                    strStockCode = _dtStockCode.Rows[_seqNo]["STOCK_CODE"].ToString();

                    _seqNo += 1;
                

                if (_seqNo + 1 == _dtStockCode.Rows.Count)
                {
                    return "Finish";
                }
                else
                {

                    return strStockCode;
                }
            }
            else
            {
                return "None";
            }
        }
        #endregion
        private void OnGetStockCode()
        {

            _stockCodePs = "";
            _stockCodePd = "";
            _stockCodeQs = "";
            _stockCodeQd = "";

            string strStockCode = "";

            Task t = Task.Run(() => strStockCode = GetStockCode());

            t.ContinueWith(task => GetOpt10060Caller(strStockCode));

            proBar10060.Value = _seqNo;

            lblStockName.Text = strStockCode + " 작업 중";

        }
        private bool GetOpt10060Caller(string strStockCode)
        {

            switch (strStockCode)
            {
                case "Finish":
                    {
                        MessageBox.Show("작업이 완료되었습니다.");
                        return true;
                    }
                case "None":
                    {
                        MessageBox.Show("작업할 내역이 없습니다.");
                        return true;
                    }

                default:
                    break;
            }

            _LastPsDate = "";
            _LastPdDate = "";
            _LastQsDate = "";
            _LastQdDate = "";

            _FirstPsDate = "";
            _FirstPdDate = "";
            _FirstQsDate = "";
            _FirstQdDate = "";

            bool blnChk = false;
            DataTable dtDate = new DataTable();

            Func<DataTable> funcOpt10060MaxQuery = () =>
            {
                KiwoomQuery kiwoomQuery = new KiwoomQuery();
                return kiwoomQuery.p_Opt10060MinMaxQuery("1", strStockCode, "", "", false).Tables[0].Copy();
            };

            Func<DataTable> funcOpt10060MinQuery = () =>
            {
                KiwoomQuery kiwoomQuery = new KiwoomQuery();
                return kiwoomQuery.p_Opt10060MinMaxQuery("2", strStockCode, "", "", false).Tables[0].Copy();
            };


            dtDate = funcOpt10060MaxQuery();

            if (dtDate != null)
            {
                // 한개라도 작업을 돌리게 있으면 돌려라.
                if (Convert.ToInt32(_stdDate) > Convert.ToInt32(dtDate.Rows[0]["MAESU_PRICE"].ToString()))
                {
                    blnChk = true;
                }
                if (Convert.ToInt32(_stdDate) > Convert.ToInt32(dtDate.Rows[0]["MEADO_PRICE"].ToString()))
                {
                    blnChk = true;
                }
                if (Convert.ToInt32(_stdDate) > Convert.ToInt32(dtDate.Rows[0]["MAESU_QTY"].ToString()))
                {
                    blnChk = true;
                }
                if (Convert.ToInt32(_stdDate) > Convert.ToInt32(dtDate.Rows[0]["MAEDO_QTY"].ToString()))
                {
                    blnChk = true;
                }

                _LastPsDate = dtDate.Rows[0]["MAESU_PRICE"].ToString();
                _LastPdDate = dtDate.Rows[0]["MEADO_PRICE"].ToString();
                _LastQsDate = dtDate.Rows[0]["MAESU_QTY"].ToString();
                _LastQdDate = dtDate.Rows[0]["MAEDO_QTY"].ToString();
            }

            // 기준일자보다 크므로 작업할게 없다.
            if (blnChk == false)
            {
                dtDate = null;
                dtDate = new DataTable();
                return true;
            }

            dtDate = null;
            dtDate = new DataTable();

            dtDate = funcOpt10060MinQuery();

            if (dtDate != null)
            {

                _FirstPsDate = dtDate.Rows[0]["MAESU_PRICE"].ToString();
                _FirstPdDate = dtDate.Rows[0]["MEADO_PRICE"].ToString();
                _FirstQsDate = dtDate.Rows[0]["MAESU_QTY"].ToString();
                _FirstQdDate = dtDate.Rows[0]["MAEDO_QTY"].ToString();
            }

            dtDate = null;

            ExecPs(stockCode: strStockCode);

            return false;
        }

        private void WriteTextSafe(string strMessage)
        {
            if (lblStockName.InvokeRequired)
            {
                Invoke((MethodInvoker)delegate () {
                    WriteTextSafe(strMessage);
                });
            }
            else
            {
                lblStockName.Text = strMessage;
            }

        }

        private void ExecPs(string stockCode)
        {
            WriteTextSafe(stockCode + " Price 매수 작업 중 ");

            Task t = new Task(() => {
                _opt10060Ps.SetInit(_FormId + "01");
                _opt10060Ps.SetValue(_stdDate, stockCode, "", "1", "1", "1");
                _opt10060Ps.Opt10060();
            });

            t.Start();
        }

        private void ExecPd(string stockCode)
        {

            WriteTextSafe(stockCode + " Price 매도 작업 중 ");

            Task t = new Task(() => {
            _opt10060Pd.SetInit(_FormId + "02");
            _opt10060Pd.SetValue(_stdDate, stockCode, "", "1", "2", "1");
            _opt10060Pd.Opt10060();
            });

            t.Start();
        }

        private void ExecQs(string stockCode)
        {
            WriteTextSafe(stockCode + " Qty 매수 작업 중 ");

            Task t = new Task(() =>
            {
                _opt10060Qs.SetInit(_FormId + "03");
                _opt10060Qs.SetValue(_stdDate, stockCode, "", "2", "1", "1");
                _opt10060Qs.Opt10060();
            });

            t.Start();
        }

        private void ExecQd(string stockCode)
        {
            WriteTextSafe(stockCode + " Qty 매도 작업 중 ");

            Task t = new Task(() =>
            {
                _opt10060Qd.SetInit(_FormId + "04");
                _opt10060Qd.SetValue(_stdDate, stockCode, "", "2", "2", "1");
                _opt10060Qd.Opt10060();
            });

            t.Start();
        }

        #region 매수매도 이벤트
        private void OnReceiveTrData_Opt10060PriceMaeSu(string stockCode, DataTable dt, int sPreNext)
        {

            //lblStockName.Text = stockCode + "Price 매수 작업 중";

            ArrayParam arrParam = new ArrayParam();
            Sql oSql = new Sql("EDPB2F011\\VADIS", "KIWOOMDB");

            Task t = Task.Run(() =>
            {

                if (dt != null)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["일자"].ToString() == _LastPsDate)
                        {
                            Sql oSql2 = new Sql("EDPB2F011\\VADIS", "RICHDB");

                            arrParam.Clear();
                            arrParam.Add("@ACTION_GB", "C7");
                            arrParam.Add("@STOCK_CODE", stockCode);
                            arrParam.Add("@STOCK_NAME", "");
                            arrParam.Add("@YBJONG_CODE", "");
                            arrParam.Add("@OPT10059_QTY", "");
                            arrParam.Add("@OPT10059_PRICE", "");
                            arrParam.Add("@OPT10081", "");
                            arrParam.Add("@OPT10060_QTY", "");
                            arrParam.Add("@OPT10060_PRICE", "S");
                            arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                            oSql2.ExecuteNonQuery("p_ScodeAdd", CommandType.StoredProcedure, arrParam);

                            _stockCodePs = stockCode;
                            _opt10060Ps.Dispose();
                            ExecPd(stockCode);
                            return;
                        }

                        arrParam.Clear();
                        arrParam.Add("@ACTION_GB", "A");
                        arrParam.Add("@STOCK_CODE", stockCode);
                        arrParam.Add("@STOCK_DATE", dr["일자"]);
                        arrParam.Add("@MAEME_GB", "1");
                        arrParam.Add("@DATE_SEQNO", 0);
                        arrParam.Add("@NUJUK_TRDAEGUM", dr["누적거래대금"]);
                        arrParam.Add("@GAIN_PRICE", dr["개인투자자"]);
                        arrParam.Add("@FORE_PRICE", dr["외국인투자자"]);
                        arrParam.Add("@GIGAN_PRICE", dr["기관계"]);
                        arrParam.Add("@GUMY_PRICE", dr["금융투자"]);
                        arrParam.Add("@BOHUM_PRICE", dr["보험"]);
                        arrParam.Add("@TOSIN_PRICE", dr["투신"]);
                        arrParam.Add("@GITA_PRICE", dr["기타금융"]);
                        arrParam.Add("@BANK_PRICE", dr["은행"]);
                        arrParam.Add("@YEONGI_PRICE", dr["연기금등"]);
                        arrParam.Add("@SAMO_PRICE", dr["사모펀드"]);
                        arrParam.Add("@NATION_PRICE", dr["국가"]);
                        arrParam.Add("@BUBIN_PRICE", dr["기타법인"]);
                        arrParam.Add("@IOFORE_PRICE", dr["내외국인"]);
                        arrParam.Add("@GIGAN_SUM_PRICE", Convert.ToInt32(dr["금융투자"]) + Convert.ToInt32(dr["보험"]) + Convert.ToInt32(dr["투신"]) +
                                                       Convert.ToInt32(dr["기타금융"]) + Convert.ToInt32(dr["은행"]) + Convert.ToInt32(dr["연기금등"]) +
                                                       Convert.ToInt32(dr["사모펀드"]) + Convert.ToInt32(dr["국가"]));
                        arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                        oSql.ExecuteNonQuery("p_Opt10060PriceAdd", CommandType.StoredProcedure, arrParam);

                    }
                }
            });

            t.ContinueWith(task =>
            {
                if (sPreNext == 2)
                {
                    _opt10060Ps.Opt10060();
                    return;
                }
                else
                {
                    Sql oSql2 = new Sql("EDPB2F011\\VADIS", "RICHDB");

                    arrParam.Clear();
                    arrParam.Add("@ACTION_GB", "C7");
                    arrParam.Add("@STOCK_CODE", stockCode);
                    arrParam.Add("@STOCK_NAME", "");
                    arrParam.Add("@YBJONG_CODE", "");
                    arrParam.Add("@OPT10059_QTY", "");
                    arrParam.Add("@OPT10059_PRICE", "");
                    arrParam.Add("@OPT10081", "");
                    arrParam.Add("@OPT10060_QTY", "");
                    arrParam.Add("@OPT10060_PRICE", "S");
                    arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                    oSql2.ExecuteNonQuery("p_ScodeAdd", CommandType.StoredProcedure, arrParam);

                    _stockCodePs = stockCode;
                    _opt10060Ps.Dispose();
                    ExecPd(stockCode);
                    return;


                }
            });
        }
        private void OnReceiveTrData_Opt10060PriceMaedo(string stockCode, DataTable dt, int sPreNext)
        {

            //lblStockName.Text = stockCode + "Price 매도 작업 중";

            ArrayParam arrParam = new ArrayParam();
            Sql oSql = new Sql("EDPB2F011\\VADIS", "KIWOOMDB");
            Task t = Task.Run(() =>
            {

                if (dt != null)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["일자"].ToString() == _LastPdDate)
                        {
                            Sql oSql2 = new Sql("EDPB2F011\\VADIS", "RICHDB");

                            arrParam.Clear();
                            arrParam.Add("@ACTION_GB", "C7");
                            arrParam.Add("@STOCK_CODE", stockCode);
                            arrParam.Add("@STOCK_NAME", "");
                            arrParam.Add("@YBJONG_CODE", "");
                            arrParam.Add("@OPT10059_QTY", "");
                            arrParam.Add("@OPT10059_PRICE", "");
                            arrParam.Add("@OPT10081", "");
                            arrParam.Add("@OPT10060_QTY", "");
                            arrParam.Add("@OPT10060_PRICE", "Y");
                            arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                            oSql2.ExecuteNonQuery("p_ScodeAdd", CommandType.StoredProcedure, arrParam);

                            _stockCodePd = stockCode;
                            _opt10060Pd.Dispose();
                            ExecQs(stockCode);
                            return;

                        }
                        arrParam.Clear();
                        arrParam.Add("@ACTION_GB", "A");
                        arrParam.Add("@STOCK_CODE", stockCode);
                        arrParam.Add("@STOCK_DATE", dr["일자"]);
                        arrParam.Add("@MAEME_GB", "2");
                        arrParam.Add("@DATE_SEQNO", 0);
                        arrParam.Add("@NUJUK_TRDAEGUM", dr["누적거래대금"]);
                        arrParam.Add("@GAIN_PRICE", dr["개인투자자"]);
                        arrParam.Add("@FORE_PRICE", dr["외국인투자자"]);
                        arrParam.Add("@GIGAN_PRICE", dr["기관계"]);
                        arrParam.Add("@GUMY_PRICE", dr["금융투자"]);
                        arrParam.Add("@BOHUM_PRICE", dr["보험"]);
                        arrParam.Add("@TOSIN_PRICE", dr["투신"]);
                        arrParam.Add("@GITA_PRICE", dr["기타금융"]);
                        arrParam.Add("@BANK_PRICE", dr["은행"]);
                        arrParam.Add("@YEONGI_PRICE", dr["연기금등"]);
                        arrParam.Add("@SAMO_PRICE", dr["사모펀드"]);
                        arrParam.Add("@NATION_PRICE", dr["국가"]);
                        arrParam.Add("@BUBIN_PRICE", dr["기타법인"]);
                        arrParam.Add("@IOFORE_PRICE", dr["내외국인"]);
                        arrParam.Add("@GIGAN_SUM_PRICE", Convert.ToInt32(dr["금융투자"]) + Convert.ToInt32(dr["보험"]) + Convert.ToInt32(dr["투신"]) +
                                                       Convert.ToInt32(dr["기타금융"]) + Convert.ToInt32(dr["은행"]) + Convert.ToInt32(dr["연기금등"]) +
                                                       Convert.ToInt32(dr["사모펀드"]) + Convert.ToInt32(dr["국가"]));
                        arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                        oSql.ExecuteNonQuery("p_Opt10060PriceAdd", CommandType.StoredProcedure, arrParam);

                    }
                }
            });

            t.ContinueWith(task =>
            {
                if (sPreNext == 2)
                {
                    _opt10060Pd.Opt10060();
                    return;
                }
                else
                {
                    Sql oSql2 = new Sql("EDPB2F011\\VADIS", "RICHDB");

                    arrParam.Clear();
                    arrParam.Add("@ACTION_GB", "C7");
                    arrParam.Add("@STOCK_CODE", stockCode);
                    arrParam.Add("@STOCK_NAME", "");
                    arrParam.Add("@YBJONG_CODE", "");
                    arrParam.Add("@OPT10059_QTY", "");
                    arrParam.Add("@OPT10059_PRICE", "");
                    arrParam.Add("@OPT10081", "");
                    arrParam.Add("@OPT10060_QTY", "");
                    arrParam.Add("@OPT10060_PRICE", "Y");
                    arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                    oSql2.ExecuteNonQuery("p_ScodeAdd", CommandType.StoredProcedure, arrParam);

                    _stockCodePd = stockCode;
                    _opt10060Pd.Dispose();
                    ExecQs(stockCode);
                    return;

                }
            });
        }

        /// <summary>
        /// Price 매수 
        /// </summary>
        /// <param name="stockCode"></param>
        /// <param name="dt"></param>
        /// <param name="sPreNext"></param>
        /// <returns>True - 완료, False - 계속 </returns>
        private void OnReceiveTrData_Opt10060QtyMaeSu(string stockCode, DataTable dt, int sPreNext)
        {
            // lblStockName.Text = stockCode + "Qty 매수 작업 중";

            ArrayParam arrParam = new ArrayParam();
            Sql oSql = new Sql("EDPB2F011\\VADIS", "KIWOOMDB");

            Task t = Task.Run(() =>
            {
                if (dt != null)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        // 가장 최근일자와 같으면 종료
                        if (dr["일자"].ToString() == _LastQsDate)
                        {
                            Sql oSql2 = new Sql("EDPB2F011\\VADIS", "RICHDB");

                            arrParam.Clear();
                            arrParam.Add("@ACTION_GB", "C6");
                            arrParam.Add("@STOCK_CODE", stockCode);
                            arrParam.Add("@STOCK_NAME", "");
                            arrParam.Add("@YBJONG_CODE", "");
                            arrParam.Add("@OPT10059_QTY", "");
                            arrParam.Add("@OPT10059_PRICE", "");
                            arrParam.Add("@OPT10081", "");
                            arrParam.Add("@OPT10060_QTY", "S");
                            arrParam.Add("@OPT10060_PRICE", "");
                            arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                            oSql2.ExecuteNonQuery("p_ScodeAdd", CommandType.StoredProcedure, arrParam);

                            // 다음 자료가 없으면 
                            _stockCodeQs = stockCode;
                            _opt10060Qs.Dispose();
                            ExecQd(stockCode);
                            return;
                        }

                        arrParam.Clear();
                        arrParam.Add("@ACTION_GB", "A");
                        arrParam.Add("@STOCK_CODE", stockCode);
                        arrParam.Add("@STOCK_DATE", dr["일자"]);
                        arrParam.Add("@MAEME_GB", "1");
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

                        oSql.ExecuteNonQuery("p_Opt10060QtyAdd", CommandType.StoredProcedure, arrParam);


                    }
                }
            });

            t.ContinueWith(task =>
            {
                if (sPreNext == 2)
                {
                    _opt10060Qs.Opt10060();
                    return;
                }
                else
                {

                    Sql oSql2 = new Sql("EDPB2F011\\VADIS", "RICHDB");

                    arrParam.Clear();
                    arrParam.Add("@ACTION_GB", "C6");
                    arrParam.Add("@STOCK_CODE", stockCode);
                    arrParam.Add("@STOCK_NAME", "");
                    arrParam.Add("@YBJONG_CODE", "");
                    arrParam.Add("@OPT10059_QTY", "");
                    arrParam.Add("@OPT10059_PRICE", "");
                    arrParam.Add("@OPT10081", "");
                    arrParam.Add("@OPT10060_QTY", "S");
                    arrParam.Add("@OPT10060_PRICE", "");
                    arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                    oSql2.ExecuteNonQuery("p_ScodeAdd", CommandType.StoredProcedure, arrParam);
                    // 다음 자료가 없으면 
                    _stockCodeQs = stockCode;
                    _opt10060Qs.Dispose();
                    ExecQd(stockCode);
                    return;

                }
            });
        }
        private void OnReceiveTrData_Opt10060QtyMaedo(string stockCode, DataTable dt, int sPreNext)
        {
            // lblStockName.Text = stockCode + "Qty 매도 작업 중";

            ArrayParam arrParam = new ArrayParam();
            Sql oSql = new Sql("EDPB2F011\\VADIS", "KIWOOMDB");

            Task t = Task.Run(() =>
            {
                if (dt != null)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["일자"].ToString() == _LastQdDate)
                        {
                            Sql oSql2 = new Sql("EDPB2F011\\VADIS", "RICHDB");

                            arrParam.Clear();
                            arrParam.Add("@ACTION_GB", "C6");
                            arrParam.Add("@STOCK_CODE", stockCode);
                            arrParam.Add("@STOCK_NAME", "");
                            arrParam.Add("@YBJONG_CODE", "");
                            arrParam.Add("@OPT10059_QTY", "");
                            arrParam.Add("@OPT10059_PRICE", "");
                            arrParam.Add("@OPT10081", "");
                            arrParam.Add("@OPT10060_QTY", "Y");
                            arrParam.Add("@OPT10060_PRICE", "");
                            arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);
                            oSql2.ExecuteNonQuery("p_ScodeAdd", CommandType.StoredProcedure, arrParam);

                            // 다음 자료가 없으면 
                            _stockCodeQd = stockCode;
                            _opt10060Qd.Dispose();
                            OnGetEndJob();
                            return;
                        }
                        arrParam.Clear();
                        arrParam.Add("@ACTION_GB", "A");
                        arrParam.Add("@STOCK_CODE", stockCode);
                        arrParam.Add("@STOCK_DATE", dr["일자"]);
                        arrParam.Add("@MAEME_GB", "2");
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

                        oSql.ExecuteNonQuery("p_Opt10060QtyAdd", CommandType.StoredProcedure, arrParam);

                    }
                }
            });

            t.ContinueWith(task =>
            {
                if (sPreNext == 2)
                {
                    _opt10060Qd.Opt10060();
                    return;
                }
                else
                {

                    Sql oSql2 = new Sql("EDPB2F011\\VADIS", "RICHDB");

                    arrParam.Clear();
                    arrParam.Add("@ACTION_GB", "C6");
                    arrParam.Add("@STOCK_CODE", stockCode);
                    arrParam.Add("@STOCK_NAME", "");
                    arrParam.Add("@YBJONG_CODE", "");
                    arrParam.Add("@OPT10059_QTY", "");
                    arrParam.Add("@OPT10059_PRICE", "");
                    arrParam.Add("@OPT10081", "");
                    arrParam.Add("@OPT10060_QTY", "Y");
                    arrParam.Add("@OPT10060_PRICE", "");
                    arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);
                    oSql2.ExecuteNonQuery("p_ScodeAdd", CommandType.StoredProcedure, arrParam);

                    // 다음 자료가 없으면 
                    _stockCodeQd = stockCode;
                    _opt10060Qd.Dispose();
                    OnGetEndJob();
                    return;

                }
            });
        }

        #endregion

    }


}
