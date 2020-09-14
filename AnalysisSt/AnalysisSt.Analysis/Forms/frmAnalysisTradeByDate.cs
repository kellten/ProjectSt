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

namespace AnalysisSt.Analysis.Forms
{
    public partial class frmAnalysisTradeByDate : Form
    {
        public frmAnalysisTradeByDate()
        {
            InitializeComponent();
        }
              
        private void frmAnalysisTradeByDate_Load(object sender, EventArgs e)
        {
            dgvSca01Init();
            dgvNuJuk10059DataInit();
            ucFav.OnSelect += new AnalysisSt.Common.Uc.ucFav.OnSelectEventHandler(ucFav_onCliked_Fsa01Data);
            ucStockList.OnSelect += new AnalysisSt.Common.Uc.ucStockList.OnSelectEventHandler(ucFav_onCliked_Fsa01Data);
        }

        private void ucFav_onCliked_Fsa01Data(object sender, EventArgs e)
        {
            lblStockCode.Text = ucFav.propStockCode.STOCK_CODE;
            lblStockName.Text = ucFav.propStockCode.STOCK_NAME;
            GetSca01Data(ucFav.propStockCode.STOCK_CODE);
        }

      
        #region DataGridViewInit
        private void dgvSca01Init()
        {
            dgvSca01.ColumnCount = 5;
            dgvSca01.Columns[0].Name = "STOCK_CODE";
            dgvSca01.Columns[1].Name = "BIG_FLOW";
            dgvSca01.Columns[2].Name = "시작일자";
            dgvSca01.Columns[3].Name = "종료일자";
            dgvSca01.Columns[4].Name = "STOCK_INFO";
        }
        private void dgvNuJuk10059DataInit()
        {
            dgvNuJuk10059Data.ColumnCount = 23;
            dgvNuJuk10059Data.Columns[0].Name = "종목";
            dgvNuJuk10059Data.Columns[1].Name = "종목명";
            dgvNuJuk10059Data.Columns[2].Name = "일자";
            dgvNuJuk10059Data.Columns[3].Name = "거래량";
            dgvNuJuk10059Data.Columns[4].Name = "거래대금";
            dgvNuJuk10059Data.Columns[5].Name = "구분";
            dgvNuJuk10059Data.Columns[6].Name = "종가";
            dgvNuJuk10059Data.Columns[7].Name = "고가";
            dgvNuJuk10059Data.Columns[8].Name = "저가";
            dgvNuJuk10059Data.Columns[9].Name = "개인";
            dgvNuJuk10059Data.Columns[10].Name = "외국인";
            dgvNuJuk10059Data.Columns[11].Name = "기관";
            dgvNuJuk10059Data.Columns[12].Name = "금융";
            dgvNuJuk10059Data.Columns[13].Name = "보험";
            dgvNuJuk10059Data.Columns[14].Name = "투신";
            dgvNuJuk10059Data.Columns[15].Name = "기타금융";
            dgvNuJuk10059Data.Columns[16].Name = "은행";
            dgvNuJuk10059Data.Columns[17].Name = "연기금";
            dgvNuJuk10059Data.Columns[18].Name = "사모펀드";
            dgvNuJuk10059Data.Columns[19].Name = "국가";
            dgvNuJuk10059Data.Columns[20].Name = "기타법인";
            dgvNuJuk10059Data.Columns[21].Name = "기타외인";
            dgvNuJuk10059Data.Columns[22].Name = "기관합";

            for (int i = 6; i < dgvNuJuk10059Data.Columns.Count - 1; i++)
            {
                dgvNuJuk10059Data.Columns[i].ValueType = System.Type.GetType("System.Int64");
            }

        }
        #endregion
        
        #region Func
        private void GetSca01Data(String stockCode)
        {
            DataSet ds;
            RichQuery oRichQuery = new RichQuery();
            int i = 0;

            ds = oRichQuery.p_Sca01Query("1", stockCode, 0, 0, "", "", false);

            dgvSca01.Rows.Clear();

            if (ds == null || ds.Tables[0].Rows.Count < 1)
            {
                ds.Reset();
                return;
            }

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                dgvSca01.Rows.Add();
                dgvSca01.Rows[i].Cells["STOCK_CODE"].Value = dr["STOCK_CODE"].ToString();
                dgvSca01.Rows[i].Cells["BIG_FLOW"].Value = dr["BIG_FLOW"].ToString();
                dgvSca01.Rows[i].Cells["시작일자"].Value = dr["START_DATE"].ToString();
                dgvSca01.Rows[i].Cells["종료일자"].Value = dr["END_DATE"].ToString();
                dgvSca01.Rows[i].Cells["STOCK_INFO"].Value = dr["STOCK_INFO"].ToString();

                i = i + 1;
            }
            ds.Reset();
        }
        #endregion

        #region contolevent definition
        private void dgvSca01_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSca01.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString() == "")
            {
                return;
            }

            lblStockCode2.Text = dgvSca01.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString();
            txtBigFlow.Text = dgvSca01.Rows[e.RowIndex].Cells["BIG_FLOW"].Value.ToString();
            dtpFromDate.Text = CDateTime.FormatDate(dgvSca01.Rows[e.RowIndex].Cells["시작일자"].Value.ToString(), "-");
            dtpToDate.Text = CDateTime.FormatDate(dgvSca01.Rows[e.RowIndex].Cells["종료일자"].Value.ToString(), "-");           
        }
        private void btnView_Click(object sender, EventArgs e)
        {
            DataSet ds;
            KiwoomQuery oKiwoomQuery = new KiwoomQuery();

            ds = oKiwoomQuery.p_NuOPT10059QtyQuery("2", lblStockCode2.Text.Trim(), "", "", false);

            if (ds == null || ds.Tables[0].Rows.Count < 1)
            {
                ds.Reset();
                if (MessageBox.Show("NU_OPT10059QTY의 최종내역이 없습니다. 작업을 하시겠습니까?", "생성작업", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ds = oKiwoomQuery.p_Opt10059QtyNujukQuery("1", lblStockCode2.Text.Trim(), "", false);
                    MessageBox.Show("NU_OPT10059QTY 작업 완료");
                    return;
                }
            }

            ds.Reset();
            
            ds = oKiwoomQuery.p_NuOPT10059PriceQuery("2", lblStockCode2.Text.Trim(), "", "", false);

            if (ds == null || ds.Tables[0].Rows.Count < 1)
            {
                ds.Reset();
                if (MessageBox.Show("NU_OPT10059Price의 최종내역이 없습니다. 작업을 하시겠습니까?", "생성작업", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ds = oKiwoomQuery.p_Opt10059PriceNujukQuery("1", lblStockCode2.Text.Trim(), "", false);
                    MessageBox.Show("NU_OPT10059Price 작업 완료");
                    return;
                }
            }

            ds.Reset();

            if (rdoPrice.Checked == true)
            {
                ds = oKiwoomQuery.p_NuOPT10059PriceQuery("1", lblStockCode2.Text.Trim(), CDateTime.FormatDate(dtpFromDate.Text), CDateTime.FormatDate(dtpToDate.Text), false);
            }
            else
            {
                ds = oKiwoomQuery.p_NuOPT10059QtyQuery("1", lblStockCode2.Text.Trim(), CDateTime.FormatDate(dtpFromDate.Text), CDateTime.FormatDate(dtpToDate.Text), false);
            }

            if (ds == null || ds.Tables[0].Rows.Count < 1)
            {
                ds.Reset();
                MessageBox.Show("자료가 없습니다.");
                return;
            }
            
            dgvNuJuk10059Data.Rows.Clear();
            int i = 0;
            Decimal dPrice = 1;

            if (rdoPrice.Checked == true)
            {

                if (chkPrice.Checked == true)
                { dPrice = 1000000; }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dgvNuJuk10059Data.Rows.Add();
                    dgvNuJuk10059Data.Rows[i].Cells["종목"].Value = dr["STOCK_CODE"];
                    dgvNuJuk10059Data.Rows[i].Cells["종목명"].Value = dr["STOCK_NAME"];
                    dgvNuJuk10059Data.Rows[i].Cells["일자"].Value = dr["STOCK_DATE"];
                    dgvNuJuk10059Data.Rows[i].Cells["거래량"].Value = dr["TRADE_QTY"];
                    dgvNuJuk10059Data.Rows[i].Cells["거래대금"].Value = dr["TRADE_DAEGUM"];
                    dgvNuJuk10059Data.Rows[i].Cells["구분"].Value = "";
                    dgvNuJuk10059Data.Rows[i].Cells["종가"].Value = dr["NOW_PRICE"];
                    dgvNuJuk10059Data.Rows[i].Cells["고가"].Value = dr["HIGH_PRICE"];
                    dgvNuJuk10059Data.Rows[i].Cells["저가"].Value = dr["LOW_PRICE"];
                    dgvNuJuk10059Data.Rows[i].Cells["개인"].Value = Convert.ToDecimal(dr["GAIN_PRICE"]) * dPrice;
                    dgvNuJuk10059Data.Rows[i].Cells["외국인"].Value = Convert.ToDecimal(dr["FORE_PRICE"]) * dPrice;
                    dgvNuJuk10059Data.Rows[i].Cells["기관"].Value = Convert.ToDecimal(dr["GIGAN_PRICE"]) * dPrice;
                    dgvNuJuk10059Data.Rows[i].Cells["금융"].Value = Convert.ToDecimal(dr["GUMY_PRICE"]) * dPrice;
                    dgvNuJuk10059Data.Rows[i].Cells["보험"].Value = Convert.ToDecimal(dr["BOHUM_PRICE"]) * dPrice;
                    dgvNuJuk10059Data.Rows[i].Cells["투신"].Value = Convert.ToDecimal(dr["TOSIN_PRICE"]) * dPrice;
                    dgvNuJuk10059Data.Rows[i].Cells["기타금융"].Value = Convert.ToDecimal(dr["GITA_PRICE"]) * dPrice;
                    dgvNuJuk10059Data.Rows[i].Cells["은행"].Value = Convert.ToDecimal(dr["BANK_PRICE"]) * dPrice;
                    dgvNuJuk10059Data.Rows[i].Cells["연기금"].Value = Convert.ToDecimal(dr["YEONGI_PRICE"]) * dPrice;
                    dgvNuJuk10059Data.Rows[i].Cells["사모펀드"].Value = Convert.ToDecimal(dr["SAMO_PRICE"]) * dPrice;
                    dgvNuJuk10059Data.Rows[i].Cells["국가"].Value = Convert.ToDecimal(dr["NATION_PRICE"]) * dPrice;
                    dgvNuJuk10059Data.Rows[i].Cells["기타법인"].Value = Convert.ToDecimal(dr["BUBIN_PRICE"]) * dPrice;
                    dgvNuJuk10059Data.Rows[i].Cells["기타외인"].Value = Convert.ToDecimal(dr["IOFORE_PRICE"]) * dPrice;
                    dgvNuJuk10059Data.Rows[i].Cells["기관합"].Value = Convert.ToDecimal(dr["GIGAN_SUM_PRICE"]) * dPrice;

                    for (int j = 8; j < dgvNuJuk10059Data.ColumnCount - 1; j++)
                    {
                        if (Convert.ToInt64(dgvNuJuk10059Data.Rows[i].Cells[j].Value) > 0)
                        {
                            //  System.Drawing.SystemColors.
                            dgvNuJuk10059Data.Rows[i].Cells[j].Style.ForeColor = Color.Red;
                        }
                        else
                        {
                            dgvNuJuk10059Data.Rows[i].Cells[j].Style.ForeColor = Color.Blue;
                        }

                    }

                    i = i + 1;
                }
            }
            else
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dgvNuJuk10059Data.Rows.Add();
                    dgvNuJuk10059Data.Rows[i].Cells["종목"].Value = dr["STOCK_CODE"];
                    dgvNuJuk10059Data.Rows[i].Cells["종목명"].Value = dr["STOCK_NAME"];
                    dgvNuJuk10059Data.Rows[i].Cells["일자"].Value = dr["STOCK_DATE"];
                    dgvNuJuk10059Data.Rows[i].Cells["거래량"].Value = dr["TRADE_QTY"];
                    dgvNuJuk10059Data.Rows[i].Cells["거래대금"].Value = dr["TRADE_DAEGUM"];
                    dgvNuJuk10059Data.Rows[i].Cells["구분"].Value = "";
                    dgvNuJuk10059Data.Rows[i].Cells["종가"].Value = dr["NOW_PRICE"];
                    dgvNuJuk10059Data.Rows[i].Cells["고가"].Value = dr["HIGH_PRICE"];
                    dgvNuJuk10059Data.Rows[i].Cells["저가"].Value = dr["LOW_PRICE"];
                    dgvNuJuk10059Data.Rows[i].Cells["개인"].Value = dr["GAIN_QTY"];
                    dgvNuJuk10059Data.Rows[i].Cells["외국인"].Value = dr["FORE_QTY"];
                    dgvNuJuk10059Data.Rows[i].Cells["기관"].Value = dr["GIGAN_QTY"];
                    dgvNuJuk10059Data.Rows[i].Cells["금융"].Value = dr["GUMY_QTY"];
                    dgvNuJuk10059Data.Rows[i].Cells["보험"].Value = dr["BOHUM_QTY"];
                    dgvNuJuk10059Data.Rows[i].Cells["투신"].Value = dr["TOSIN_QTY"];
                    dgvNuJuk10059Data.Rows[i].Cells["기타금융"].Value = dr["GITA_QTY"];
                    dgvNuJuk10059Data.Rows[i].Cells["은행"].Value = dr["BANK_QTY"];
                    dgvNuJuk10059Data.Rows[i].Cells["연기금"].Value = dr["YEONGI_QTY"];
                    dgvNuJuk10059Data.Rows[i].Cells["사모펀드"].Value = dr["SAMO_QTY"];
                    dgvNuJuk10059Data.Rows[i].Cells["국가"].Value = dr["NATION_QTY"];
                    dgvNuJuk10059Data.Rows[i].Cells["기타법인"].Value = dr["BUBIN_QTY"];
                    dgvNuJuk10059Data.Rows[i].Cells["기타외인"].Value = dr["IOFORE_QTY"];
                    dgvNuJuk10059Data.Rows[i].Cells["기관합"].Value = dr["GIGAN_SUM_QTY"];

                    for (int j = 8; j < dgvNuJuk10059Data.ColumnCount - 1; j++)
                    {
                        if (Convert.ToInt64(dgvNuJuk10059Data.Rows[i].Cells[j].Value) > 0)
                        {
                            //  System.Drawing.SystemColors.
                            dgvNuJuk10059Data.Rows[i].Cells[j].Style.ForeColor = Color.Red;
                        }
                        else
                        {
                            dgvNuJuk10059Data.Rows[i].Cells[j].Style.ForeColor = Color.Blue;
                        }

                    }

                    i = i + 1;
                }
            }
        }
        #endregion

        private void btnJobStart_Click(object sender, EventArgs e)
        {
            if (lblStockCode.Text == "")
            {
                return;
            }
            AnalysisSt.Common.Forms.frmGetKiTradeInfo ofrm = new AnalysisSt.Common.Forms.frmGetKiTradeInfo();
            AnalysisSt.Common.Forms.frmGetKiTradeInfo.StockCode stockCode = new AnalysisSt.Common.Forms.frmGetKiTradeInfo.StockCode();
            stockCode.STOCK_CODE = lblStockCode.Text;
            stockCode.STOCK_NAME = lblStockName.Text;
            ofrm.propStockCode = stockCode;
            ofrm.ShowDialog(this);
            
        }
        
    }
}
