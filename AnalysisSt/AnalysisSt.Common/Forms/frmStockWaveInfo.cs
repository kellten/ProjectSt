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

namespace AnalysisSt.Common.Forms
{
    public partial class frmStockWaveInfo : Form
    {
        public frmStockWaveInfo()
        {
            InitializeComponent();
            ucFav.OnSelect += new Uc.ucFav.OnSelectEventHandler(ucFav_onCliked_Fsa01Data);
            ucStockList.OnSelect += new Uc.ucStockList.OnSelectEventHandler(ucFav_onCliked_Fsa01Data);
            InitForm();
        }

        public void InitForm()
        {
            dgvSca01Init();
        }

        private void InitpnSca01()
        {
            lblStockCode2.Text = "";
            txtBigFlow.Text = "";          
            lblStarDate.Text = "";
            lblLowPrice.Text = "";
            lblEndDate.Text = "";
            lblHighPrice.Text = "";
            mskStartDate.Text = "";
            mskEndDate.Text = "";
        }

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

        #region DisplayChart
        private void DisplayChart(DataSet ds)
        { 

        }
        #endregion

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
        #endregion

        #region ControlEvent
        private void ucFav_onCliked_Fsa01Data(object sender, EventArgs e)
        {
            lblStockCode.Text = ucFav.propStockCode.STOCK_CODE;
            lblStockName.Text = ucFav.propStockCode.STOCK_NAME;
            InitpnSca01();
            lblStockCode2.Text = ucFav.propStockCode.STOCK_CODE;
            GetSca01Data(lblStockCode.Text.Trim());
        }

        private void dgvSca01_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSca01.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString() == "")
            {
                return;
            }

           InitpnSca01();

           lblStockCode2.Text = dgvSca01.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString();
           txtBigFlow.Text = dgvSca01.Rows[e.RowIndex].Cells["BIG_FLOW"].Value.ToString();
           dtpFromDate.Text = CDateTime.FormatDate(dgvSca01.Rows[e.RowIndex].Cells["시작일자"].Value.ToString(), "-");
           dtpToDate.Text = CDateTime.FormatDate(dgvSca01.Rows[e.RowIndex].Cells["종료일자"].Value.ToString(), "-");           

        }

        private void btnGetMaxMinSca01_Click(object sender, EventArgs e)
        {
            DataSet ds;
            KiwoomQuery oKiwoomQuery = new KiwoomQuery();

            lblStarDate.Text = "";
            lblLowPrice.Text = "";
            lblEndDate.Text = "";
            lblHighPrice.Text = "";
            mskStartDate.Text = "";
            mskEndDate.Text = "";

            ds = oKiwoomQuery.p_Opt10081MaxMinPriceDateQuery("1", lblStockCode2.Text.Trim(), CDateTime.FormatDate(dtpFromDate.Text), CDateTime.FormatDate(dtpToDate.Text), false);

            if (ds == null || ds.Tables[0].Rows.Count < 1)
            {
                ds.Reset();
                MessageBox.Show("해당일자의 OPT10081(최소가격)이 없습니다.");
                return;
            }

            lblStarDate.Text = ds.Tables[0].Rows[0]["STOCK_DATE"].ToString().Trim();
            lblLowPrice.Text = ds.Tables[0].Rows[0]["LOW_PRICE"].ToString().Trim();

            ds.Reset();

            ds = oKiwoomQuery.p_Opt10081MaxMinPriceDateQuery("2", lblStockCode2.Text.Trim(), CDateTime.FormatDate(dtpFromDate.Text), CDateTime.FormatDate(dtpToDate.Text), false);

            if (ds == null || ds.Tables[0].Rows.Count < 1)
            {
                ds.Reset();
                MessageBox.Show("해당일자의 OPT10081(최대가격)이 없습니다.");
                return;
            }

            lblEndDate.Text = ds.Tables[0].Rows[0]["STOCK_DATE"].ToString().Trim();
            lblHighPrice.Text = ds.Tables[0].Rows[0]["HIGH_PRICE"].ToString().Trim();

            ds.Reset();

        }

        private void btnSca01Add_Click(object sender, EventArgs e)
        {
            DataBaseFunc.ArrayParam arrParam = new DataBaseFunc.ArrayParam();
            DataBaseFunc.Sql oSql = new DataBaseFunc.Sql("EDPB2F011\\VADIS", "RICHDB");
            
            try
            {
                arrParam.Clear();
                arrParam.Add("@ACTION_GB", "A");
                arrParam.Add("@STOCK_CODE", lblStockCode2.Text.Trim());
                arrParam.Add("@BIG_FLOW", Convert.ToInt16(txtBigFlow.Text));
                arrParam.Add("@START_DATE", CDateTime.FormatDate(mskStartDate.Text));
                arrParam.Add("@END_DATE",CDateTime.FormatDate(mskEndDate.Text));
                arrParam.Add("@STOCK_INFO", "");
                arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                oSql.ExecuteNonQuery("p_Sca01Add", CommandType.StoredProcedure, arrParam);

                GetSca01Data(lblStockCode2.Text.Trim());

            }
            catch (Exception t)
            {
                MessageBox.Show(t.ToString());
                throw;
            }
        }
        #endregion 

        private void dgvSca02_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }
}
