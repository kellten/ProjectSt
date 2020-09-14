using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnalysisSt.DataBaseFunc;
using AnalysisSt.Common;

namespace AnalysisSt.Analysis.VolumeAnalysis.Uc
{
    public partial class ucTodayVolume : UserControl
    {
        public ucTodayVolume()
        {
            InitializeComponent();
            InitDataGridView();
            splitCon0.SplitterDistance = 25;
        }

        private AnalysisSt.TechnicalFunc.TradeAnalysis.clsBaseFunc _oBaseFunc = new AnalysisSt.TechnicalFunc.TradeAnalysis.clsBaseFunc();

        private string _sGroupCode;
        private string _tradeDate;

        public string SGroupCode { get { return _sGroupCode; } set { _sGroupCode = value; GetTodayTradeInfo(); } }
        public string TradeDate { get { return _tradeDate; } set { _tradeDate = value; dtpStdDate.Text = CDateTime.FormatDate(_tradeDate, "-"); } }

        private void InitDataGridView()
        {
            dgvTodayVolume0.ColumnCount = 16;
            dgvTodayVolume0.Columns[0].Name = "종목명";
            dgvTodayVolume0.Columns[1].Name = "거래량";
            dgvTodayVolume0.Columns[2].Name = "거래대금";
            dgvTodayVolume0.Columns[3].Name = "주체";
            dgvTodayVolume0.Columns[4].Name = "수량";
            dgvTodayVolume0.Columns[5].Name = "매수수량";
            dgvTodayVolume0.Columns[5].HeaderText = "매수";
            dgvTodayVolume0.Columns[6].Name = "PerQS";
            dgvTodayVolume0.Columns[6].HeaderText = "퍼센";
            dgvTodayVolume0.Columns[7].Name = "매도수량";
            dgvTodayVolume0.Columns[7].HeaderText = "매도";
            dgvTodayVolume0.Columns[8].Name = "PerQM";
            dgvTodayVolume0.Columns[8].HeaderText = "퍼센";
            dgvTodayVolume0.Columns[9].Name = "금액";
            dgvTodayVolume0.Columns[10].Name = "매수대금";
            dgvTodayVolume0.Columns[10].HeaderText = "매수";
            dgvTodayVolume0.Columns[11].Name = "PerGS";
            dgvTodayVolume0.Columns[11].HeaderText = "퍼센";
            dgvTodayVolume0.Columns[12].Name = "매도대금";
            dgvTodayVolume0.Columns[12].HeaderText = "매도";
            dgvTodayVolume0.Columns[13].Name = "PerGM";
            dgvTodayVolume0.Columns[13].HeaderText = "퍼센";
            dgvTodayVolume0.Columns[14].Name = "매수평균가";
            dgvTodayVolume0.Columns[15].Name = "매도평균가";
        }

        private void GetTodayTradeInfo()
        {
            if (_sGroupCode == "" || _sGroupCode == null) { return; }
            String sysDate = CDateTime.FormatDate(DateTime.Now.Date.ToString());
            DataBaseFunc.KiwoomQuery oKiwoomQuery = new DataBaseFunc.KiwoomQuery();
            DataSet ds = oKiwoomQuery.p_TodayTradeInfoQueryQuery("2", _sGroupCode, "", CDateTime.FormatDate(_tradeDate), false);

            dgvTodayVolume0.Rows.Clear();

            if (ds == null || ds.Tables[0].Rows.Count < 1)
            {
                ds.Reset();
            }
            else
            {

                int i = 0;

                DataView dv = new DataView(ds.Tables[0]);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["GB"].ToString() == "1")
                    { 
                    dgvTodayVolume0.Rows.Add();
                    dgvTodayVolume0.Rows[i].Cells["종목명"].Value = dr["STOCK_NAME"].ToString();
                    dgvTodayVolume0.Rows[i].Cells["거래량"].Value = dr["TRADE_QTY"].ToString();
                    dgvTodayVolume0.Rows[i].Cells["거래대금"].Value = dr["TRADE_DAEGUM"].ToString();
                    dgvTodayVolume0.Rows[i].Cells["주체"].Value = AnalysisSt.Common.Class.clsDicDefine.GetVolumeJucheName(dr["WHO_VOLUME"].ToString().Trim());
                    dgvTodayVolume0.Rows[i].Cells["매수수량"].Value = dr["VOLUME"].ToString();
                    dgvTodayVolume0.Rows[i].Cells["매수수량"].Style.ForeColor = Color.Red;
                    dgvTodayVolume0.Rows[i].Cells["PerQS"].Value = _oBaseFunc.PercentCal(Convert.ToDecimal(dr["TRADE_QTY"]), Convert.ToDecimal(dr["VOLUME"]));

                    dv.RowFilter = String.Format("종목 = '{0}' AND WHO_VOLUME = '{1}' AND GB = '{2}'",
                                                dr["종목"].ToString().Trim(), dr["WHO_VOLUME"].ToString().Trim(), "2");

                    foreach (DataRowView drDv  in dv)
                    {
                        dgvTodayVolume0.Rows[i].Cells["매도수량"].Value = drDv["VOLUME"].ToString();
                        dgvTodayVolume0.Rows[i].Cells["매도수량"].Style.ForeColor = Color.Blue;
                        dgvTodayVolume0.Rows[i].Cells["PerQM"].Value = _oBaseFunc.PercentCal(Convert.ToDecimal(drDv["TRADE_QTY"]), Convert.ToDecimal(drDv["VOLUME"]));    
                    }

                    dv.RowFilter = String.Format("종목 = '{0}' AND WHO_VOLUME = '{1}' AND GB = '{2}'",
                                         dr["종목"].ToString().Trim(), dr["WHO_VOLUME"].ToString().Replace("_QTY", "_PRICE").Trim(), "3");

                    foreach (DataRowView drDv in dv)
                    {
                        dgvTodayVolume0.Rows[i].Cells["매수대금"].Value = drDv["VOLUME"].ToString();
                        dgvTodayVolume0.Rows[i].Cells["매수대금"].Style.ForeColor = Color.Red;
                        dgvTodayVolume0.Rows[i].Cells["PerGS"].Value = _oBaseFunc.PercentCal(Convert.ToDecimal(drDv["TRADE_DAEGUM"]), Convert.ToDecimal(drDv["VOLUME"]));
                    }

                    dv.RowFilter = String.Format("종목 = '{0}' AND WHO_VOLUME = '{1}' AND GB = '{2}'",
                    dr["종목"].ToString().Trim(), dr["WHO_VOLUME"].ToString().Replace("_QTY", "_PRICE").Trim(), "4");

                    foreach (DataRowView drDv in dv)
                    {
                        dgvTodayVolume0.Rows[i].Cells["매도대금"].Value = drDv["VOLUME"].ToString();
                        dgvTodayVolume0.Rows[i].Cells["매도대금"].Style.ForeColor = Color.Blue;
                        dgvTodayVolume0.Rows[i].Cells["PerGM"].Value = _oBaseFunc.PercentCal(Convert.ToDecimal(drDv["TRADE_DAEGUM"]), Convert.ToDecimal(drDv["VOLUME"]));
                    }
                        dgvTodayVolume0.Rows[i].Cells["수량"].Value = Convert.ToInt32(dgvTodayVolume0.Rows[i].Cells["매수수량"].Value) + Convert.ToInt32(dgvTodayVolume0.Rows[i].Cells["매도수량"].Value);
                        if (Convert.ToInt32(dgvTodayVolume0.Rows[i].Cells["수량"].Value) > 0)
                        { dgvTodayVolume0.Rows[i].Cells["수량"].Style.ForeColor = Color.Red; }
                        else
                        { dgvTodayVolume0.Rows[i].Cells["수량"].Style.ForeColor = Color.Blue; }
                        dgvTodayVolume0.Rows[i].Cells["금액"].Value = Convert.ToInt32(dgvTodayVolume0.Rows[i].Cells["매수대금"].Value) + Convert.ToInt32(dgvTodayVolume0.Rows[i].Cells["매도대금"].Value);
                        if (Convert.ToInt32(dgvTodayVolume0.Rows[i].Cells["금액"].Value) > 0)
                        { dgvTodayVolume0.Rows[i].Cells["금액"].Style.ForeColor = Color.Red; }
                        else
                        { dgvTodayVolume0.Rows[i].Cells["금액"].Style.ForeColor = Color.Blue; }

                        if (Convert.ToInt32(dgvTodayVolume0.Rows[i].Cells["매수수량"].Value) > 0)
                        {
                            dgvTodayVolume0.Rows[i].Cells["매수평균가"].Value = Convert.ToInt32(Convert.ToInt32(dgvTodayVolume0.Rows[i].Cells["매수대금"].Value) * 1000000 /
                                                                               Convert.ToInt32(dgvTodayVolume0.Rows[i].Cells["매수수량"].Value));
                        }

                        if (Convert.ToInt32(dgvTodayVolume0.Rows[i].Cells["매도수량"].Value) < 0)
                        {
                            dgvTodayVolume0.Rows[i].Cells["매도평균가"].Value = Convert.ToInt32(Convert.ToInt32(dgvTodayVolume0.Rows[i].Cells["매도대금"].Value) * -1000000 /
                                                                                                Convert.ToInt32(dgvTodayVolume0.Rows[i].Cells["매도수량"].Value) * -1);
                        }

                        i = i + 1;
                    }
                }

                dv.RowFilter = String.Format("GB = '{0}'", "2");

                string stockCode = "";

                for (int j = 0; j < dgvTodayVolume0.Rows.Count - 1; j++)
                {
                    if (Convert.ToInt32(dgvTodayVolume0.Rows[j].Cells["매도대금"].Value) == 0 && Convert.ToInt32(dgvTodayVolume0.Rows[j].Cells["매수대금"].Value) == 0)
                    {
                        dgvTodayVolume0.Rows[j].Height = 0;
                    }

                    if (stockCode == "")
                    {
                        stockCode = dgvTodayVolume0.Rows[j].Cells["종목명"].Value.ToString().Trim();
                    }
                    else
                    {
                        if (stockCode != dgvTodayVolume0.Rows[j].Cells["종목명"].Value.ToString().Trim())
                        {
                            stockCode = dgvTodayVolume0.Rows[j].Cells["종목명"].Value.ToString().Trim();
                            
                        }
                        else
                        {
                            dgvTodayVolume0.Rows[j].Cells["종목명"].Value = "";
                            dgvTodayVolume0.Rows[i].Cells["거래량"].Value = "";
                            dgvTodayVolume0.Rows[i].Cells["거래대금"].Value = "";
                        }
                    }
                    
                }

                for (int j = dgvTodayVolume0.Rows.Count - 1; j > -1; j--)
                {
                    if (dgvTodayVolume0.Rows[j].Height < 5)
                    {
                        dgvTodayVolume0.Rows.RemoveAt(j);
                    }
                }

                for (int k = 0; k < dgvTodayVolume0.Columns.Count; k++)
                {
                    int colw = dgvTodayVolume0.Columns[k].Width;
                    dgvTodayVolume0.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgvTodayVolume0.Columns[k].Width = colw;
                }
         
            }
        }


        private void btnNext_Click(object sender, EventArgs e)
        {
            dtpStdDate.Text = dtpStdDate.Value.AddDays(1).ToShortDateString();
            GetTodayTradeInfo();
        }
        private void btnPrv_Click(object sender, EventArgs e)
        {
            dtpStdDate.Text = dtpStdDate.Value.AddDays(-1).ToShortDateString();
            GetTodayTradeInfo();
        }

        private void btnPar_Click(object sender, EventArgs e)
        {
            //ParellGetTodayTradeInfo();
        }
    }
}
