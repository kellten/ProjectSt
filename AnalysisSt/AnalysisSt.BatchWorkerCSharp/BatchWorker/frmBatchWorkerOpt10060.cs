using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnalysisSt.DataBaseFunc;
using AnalysisSt.KiwoomVB;

namespace AnalysisSt.BatchWorkerCSharp.BatchWorker
{
    public partial class frmBatchWorkerOpt10060 : Form
    {
        public frmBatchWorkerOpt10060(string stockCodeValue, string stockNameValue)
        {
            InitializeComponent();
            StockName = stockNameValue;
            StockCode = stockCodeValue;
        }
        private string _stockCode;
        private string _stockName;

        private String _minDate;
        private String _MaxDate;

        public string StockCode { get { return _stockCode; } set { _stockCode = value; CallGetOpt10060(); } }
        public string StockName { get { return _stockName; } set { _stockName = value; } }


        private void CallGetOpt10060()
        {
            lblStockName.Text = _stockName;
            lblStockCode.Text = _stockCode;
            GetOpt10060();
        }
        private async Task DoGetOpt10060(String stockCode, String stockName, String startDate)
        {
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();
            TaskCompletionSource<bool> tcs2 = null;
            tcs2 = new TaskCompletionSource<bool>();
            TaskCompletionSource<bool> tcs3 = null;
            tcs3 = new TaskCompletionSource<bool>();
            TaskCompletionSource<bool> tcs4 = null;
            tcs4 = new TaskCompletionSource<bool>();

            KiwoomVB.ModStatus._ModMainStock.OnReceiveTrData_Opt10060MaeSu += (d) =>
            {
                if (tcs == null || tcs.Task.IsCompleted)
                    return;
                OnReceiveTrData_Opt10060MaeSu(d);
                tcs.SetResult(true);
            };
            KiwoomVB.ModStatus._ModMainStock.OnReceiveTrData_Opt10060Maedo += (d) =>
            {
                if (tcs2 == null || tcs2.Task.IsCompleted)
                    return;
                OnReceiveTrData_Opt10060Maedo(d);
                tcs2.SetResult(true);
            };
            KiwoomVB.ModStatus._ModMainStock.OnReceiveTrData_Opt10060PriceMaeSu += (d) =>
            {
                if (tcs3 == null || tcs3.Task.IsCompleted)
                    return;
                OnReceiveTrData_Opt10060PriceMaeSu(d);
                tcs3.SetResult(true);
            };
            KiwoomVB.ModStatus._ModMainStock.OnReceiveTrData_Opt10060PriceMaedo += (d) =>
            {
                if (tcs4 == null || tcs4.Task.IsCompleted)
                    return;
                OnReceiveTrData_Opt10060PriceMaedo(d);
                tcs4.SetResult(true);
            };
            lblMsg.Text = startDate + " GetOpt10060QtyMaeSu 작업중";
            GetOpt10060QtyMaeSu(stockCode, stockName, startDate);
            await tcs.Task;
            tcs.Task.Dispose();
            tcs = null;
            lblMsg.Text = startDate + " GetOpt10060QtyMaeDo 작업중";
            GetOpt10060QtyMaeDo(stockCode, stockName, startDate);
            await tcs2.Task;
            tcs2.Task.Dispose();
            tcs2 = null;
            lblMsg.Text = startDate + " GetOpt10060PriceMaeSu 작업중";
            GetOpt10060PriceMaeSu(stockCode, stockName, startDate);
            await tcs3.Task;
            tcs3.Task.Dispose();
            tcs3 = null;
            lblMsg.Text = startDate + " GetOpt10060PriceMaeDo 작업중";
            GetOpt10060PriceMaeDo(stockCode, stockName, startDate);
            await tcs4.Task;
            tcs4.Task.Dispose();
            tcs4 = null;
            System.Threading.Thread.Sleep(10000);
            GetOpt10060();
        }
        String _stockCode_Opt10060MaeSu;
        String _stockCode_Opt10060MaeDo;
        String _stockCode_Opt10060PriceMaeSu;
        String _stockCode_Opt10060PriceMaeDo;
        private String _opt10060Date = "";
        private async void GetOpt10060()
        {
            //DataRow dr = SGroupCodeData();
            //if (dr == null)
            //{
            //    MessageBox.Show("해당일의 거래내역을 모두 가져왔습니다.");
            //    GetTodayTradeInfo(lblSGroupCode.Text);
            //    return;
            //}
            //String stockCode = dr["STOCK_CODE"].ToString().Trim();
            //String stockName = dr["STOCK_NAME"].ToString().Trim();
            if (_minDate == _opt10060Date && _minDate != "")
            {
                MessageBox.Show("작업이 완료되었습니다.");
                return;
            }
            String stdDate = "";
            if (_opt10060Date != "")
            {
                stdDate = Convert.ToDateTime(CDateTime.FormatDate(_opt10060Date, "-")).AddDays(-1).ToString("yyyyMMdd");
            }
            else
            {
                int i = Int32.Parse(System.DateTime.Now.ToString("HH") + System.DateTime.Now.ToString("ss"));

                if (i > 1600)
                { stdDate = CDateTime.FormatDate(System.DateTime.Now.Date.ToString()); }
                else
                { stdDate = DateTime.Today.AddDays(-1).ToString("yyyyMMdd"); }
            }

            await DoGetOpt10060(_stockCode, _stockName, stdDate);
        }
        private void setOpt1006Date(String stdDate)
        {
            if (_opt10060Date == "")
            {
                _opt10060Date = stdDate;
            }
            else
            {
                if (Int32.Parse(_opt10060Date) > Int32.Parse(stdDate))
                {
                    _opt10060Date = stdDate;
                }
            }
        }

        private void GetOpt10060QtyMaeSu(String stockCode, String stockName, String startDate)
        {
            _stockCode_Opt10060MaeSu = stockCode;
            KiwoomVB.ModStatus._ModMainStock.Opt10060MaeSu_OnReceiveTrData(startDate, stockCode, stockName, "", "", "");
        }
        private void GetOpt10060QtyMaeDo(String stockCode, String stockName, String startDate)
        {
            _stockCode_Opt10060MaeDo = stockCode;
            KiwoomVB.ModStatus._ModMainStock.Opt10060MaeDo_OnReceiveTrData(startDate, stockCode, stockName, "", "", "");
        }
        private void GetOpt10060PriceMaeSu(String stockCode, String stockName, String startDate)
        {
            _stockCode_Opt10060PriceMaeSu = stockCode;
            KiwoomVB.ModStatus._ModMainStock.Opt10060PriceMaeSu_OnReceiveTrData(startDate, stockCode, stockName, "", "", "");
        }
        private void GetOpt10060PriceMaeDo(String stockCode, String stockName, String startDate)
        {
            _stockCode_Opt10060PriceMaeDo = stockCode;
            KiwoomVB.ModStatus._ModMainStock.Opt10060PriceMaedo_OnReceiveTrData(startDate, stockCode, stockName, "", "", "");
        }

        private void OnReceiveTrData_Opt10060MaeSu(DataSet ds)
        {
            if (ds == null) { return; }

            try
            {
                ArrayParam arrParam = new ArrayParam();
                DataBaseFunc.Sql oSql = new DataBaseFunc.Sql("EDPB2F011\\VADIS", "KIWOOMDB");

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    setOpt1006Date(dr["일자"].ToString().Trim());
                    arrParam.Clear();
                    arrParam.Add("@ACTION_GB", "A");
                    arrParam.Add("@STOCK_CODE", _stockCode_Opt10060MaeSu);
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
                    arrParam.Add("@GIGAN_SUM_QTY", (int)dr["금융투자"] + (int)dr["보험"] + (int)dr["투신"] +
                                                   (int)dr["기타금융"] + (int)dr["은행"] + (int)dr["연기금등"] + (int)dr["사모펀드"] + (int)dr["국가"]);
                    arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                    oSql.ExecuteNonQuery("p_Opt10060QtyAdd", CommandType.StoredProcedure, arrParam);

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }

        }
        private void OnReceiveTrData_Opt10060Maedo(DataSet ds)
        {
            if (ds == null) { return; }

            try
            {
                ArrayParam arrParam = new ArrayParam();
                DataBaseFunc.Sql oSql = new DataBaseFunc.Sql("EDPB2F011\\VADIS", "KIWOOMDB");

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    setOpt1006Date(dr["일자"].ToString().Trim());
                    arrParam.Clear();
                    arrParam.Add("@ACTION_GB", "A");
                    arrParam.Add("@STOCK_CODE", _stockCode_Opt10060MaeDo);
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
                    arrParam.Add("@GIGAN_SUM_QTY", (int)dr["금융투자"] + (int)dr["보험"] + (int)dr["투신"] +
                                                   (int)dr["기타금융"] + (int)dr["은행"] + (int)dr["연기금등"] + (int)dr["사모펀드"] + (int)dr["국가"]);
                    arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                    oSql.ExecuteNonQuery("p_Opt10060QtyAdd", CommandType.StoredProcedure, arrParam);

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }
        private void OnReceiveTrData_Opt10060PriceMaeSu(DataSet ds)
        {
            if (ds == null) { return; }

            try
            {
                ArrayParam arrParam = new ArrayParam();
                DataBaseFunc.Sql oSql = new DataBaseFunc.Sql("EDPB2F011\\VADIS", "KIWOOMDB");

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    setOpt1006Date(dr["일자"].ToString().Trim());
                    arrParam.Clear();
                    arrParam.Add("@ACTION_GB", "A");
                    arrParam.Add("@STOCK_CODE", _stockCode_Opt10060PriceMaeSu);
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
                    arrParam.Add("@GIGAN_SUM_PRICE", (int)dr["금융투자"] + (int)dr["보험"] + (int)dr["투신"] +
                                                   (int)dr["기타금융"] + (int)dr["은행"] + (int)dr["연기금등"] + (int)dr["사모펀드"] + (int)dr["국가"]);
                    arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                    oSql.ExecuteNonQuery("p_Opt10060PriceAdd", CommandType.StoredProcedure, arrParam);

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }
        private void OnReceiveTrData_Opt10060PriceMaedo(DataSet ds)
        {
            if (ds == null) { return; }

            try
            {
                ArrayParam arrParam = new ArrayParam();
                DataBaseFunc.Sql oSql = new DataBaseFunc.Sql("EDPB2F011\\VADIS", "KIWOOMDB");

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    setOpt1006Date(dr["일자"].ToString().Trim());
                    arrParam.Clear();
                    arrParam.Add("@ACTION_GB", "A");
                    arrParam.Add("@STOCK_CODE", _stockCode_Opt10060PriceMaeDo);
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
                    arrParam.Add("@GIGAN_SUM_PRICE", (int)dr["금융투자"] + (int)dr["보험"] + (int)dr["투신"] +
                                                   (int)dr["기타금융"] + (int)dr["은행"] + (int)dr["연기금등"] + (int)dr["사모펀드"] + (int)dr["국가"]);
                    arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                    oSql.ExecuteNonQuery("p_Opt10060PriceAdd", CommandType.StoredProcedure, arrParam);

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }

    }
}
