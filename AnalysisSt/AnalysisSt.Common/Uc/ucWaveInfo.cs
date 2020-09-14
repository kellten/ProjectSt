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

namespace AnalysisSt.Common.Uc
{
    public partial class ucWaveInfo : UserControl
    {
        public ucWaveInfo()
        {
            InitializeComponent();
        }

        private string _stockCode;
        private string _fromDate;
        private string _toDate;

        private string _FromDate0;
        private string _ToDate0;
        private string _FromDate1;
        private string _ToDate1;
        private string _FromDate2;
        private string _ToDate2;
        private string _FromDate3;
        private string _ToDate3;

        private string _Last_FromDate;
        private string _Last_ToDate;

        public string StockCode { get { return _stockCode; } set { _stockCode = value; GetSca01Data(); } }
        public string FromDate { get { return _fromDate; } set { _fromDate = value; } }
        public string ToDate { get { return _toDate; } set { _toDate = value; } }

        public string FromDate0 { get { return _FromDate0; } set { _FromDate0 = value; } }
        public string ToDate0 { get { return _ToDate0; } set { _ToDate0 = value; } }
        public string FromDate1 { get { return _FromDate1; } set { _FromDate1 = value; } }
        public string ToDate1 { get { return _ToDate1; } set { _ToDate1 = value; } }
        public string FromDate2 { get { return _FromDate2; } set { _FromDate2 = value; } }
        public string ToDate2 { get { return _ToDate2; } set { _ToDate2 = value; } }
        public string FromDate3 { get { return _FromDate3; } set { _FromDate3 = value; } }
        public string ToDate3 { get { return _ToDate3; } set { _ToDate3 = value; } }

        public string Last_FromDate { get { return _Last_FromDate; } set { _Last_FromDate = value; } }
        public string Last_ToDate { get { return _Last_ToDate; } set { _Last_ToDate = value; } }

        public event OnSelectEventHandler OnSelect;
        public delegate void OnSelectEventHandler(object sender, EventArgs e);

        private void ucWaveInfo_Load(object sender, EventArgs e)
        {
            dgvSca01Init();
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
        #endregion

        #region func
        private void GetSca01Data()
        {
            if (_stockCode == null || _stockCode == "") { return; }

            DataSet ds;
            RichQuery oRichQuery = new RichQuery();
            KiwoomQuery oKiwommQuery = new KiwoomQuery();
            String strEndDate = "";
            int i = 0;

            ds = oRichQuery.p_Sca01Query("1", _stockCode, 0, 0, "", "", false);

            dgvSca01.Rows.Clear();

            if (ds == null || ds.Tables[0].Rows.Count < 1)
            {
                ds.Reset();
                ds = oKiwommQuery.p_Opt10081MinMaxQuery("3", _stockCode, "", false);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dgvSca01.Rows.Add();
                    dgvSca01.Rows[i].Cells["STOCK_CODE"].Value = _stockCode;
                    dgvSca01.Rows[i].Cells["BIG_FLOW"].Value = 99;
                    dgvSca01.Rows[i].Cells["시작일자"].Value = dr["MIN_STOCK_DATE"];
                    dgvSca01.Rows[i].Cells["종료일자"].Value = dr["MAX_STOCK_DATE"];
                    dgvSca01.Rows[i].Cells["STOCK_INFO"].Value = "전체기간";

                    Last_FromDate = dr["MIN_STOCK_DATE"].ToString();
                    Last_ToDate = dr["MAX_STOCK_DATE"].ToString();

                    i = i + 1;
                }
                ds.Reset();
                return;
            }
            else { 
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dgvSca01.Rows.Add();
                dgvSca01.Rows[i].Cells["STOCK_CODE"].Value = dr["STOCK_CODE"].ToString();
                dgvSca01.Rows[i].Cells["BIG_FLOW"].Value = dr["BIG_FLOW"].ToString();
                dgvSca01.Rows[i].Cells["시작일자"].Value = dr["START_DATE"].ToString();
                dgvSca01.Rows[i].Cells["종료일자"].Value = dr["END_DATE"].ToString();
                dgvSca01.Rows[i].Cells["STOCK_INFO"].Value = dr["STOCK_INFO"].ToString();

                if (strEndDate == "")
                {
                    strEndDate = dr["END_DATE"].ToString();
                }
                else
                {
                    if (Convert.ToInt32(strEndDate) < Convert.ToInt32(dr["END_DATE"].ToString()))
                    {
                        strEndDate = dr["END_DATE"].ToString();
                    }
                }

                i = i + 1;
            }
            ds.Reset();
            }

            ds = oKiwommQuery.p_Opt10081MinMaxQuery("3", _stockCode, "", false);

            strEndDate = Convert.ToDateTime(CDateTime.FormatDate(strEndDate, "-")).AddDays(+1).ToString("yyyyMMdd");

            dgvSca01.Rows.Add();
            dgvSca01.Rows[i].Cells["STOCK_CODE"].Value = _stockCode;
            dgvSca01.Rows[i].Cells["BIG_FLOW"].Value = 98;
            dgvSca01.Rows[i].Cells["시작일자"].Value = strEndDate;
            dgvSca01.Rows[i].Cells["종료일자"].Value = ds.Tables[0].Rows[0]["MAX_STOCK_DATE"];
            dgvSca01.Rows[i].Cells["STOCK_INFO"].Value = "최종기간";

            i = i + 1;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dgvSca01.Rows.Add();
                dgvSca01.Rows[i].Cells["STOCK_CODE"].Value = _stockCode;
                dgvSca01.Rows[i].Cells["BIG_FLOW"].Value = 99;
                dgvSca01.Rows[i].Cells["시작일자"].Value = dr["MIN_STOCK_DATE"];
                dgvSca01.Rows[i].Cells["종료일자"].Value = dr["MAX_STOCK_DATE"];
                dgvSca01.Rows[i].Cells["STOCK_INFO"].Value = "전체기간";

                Last_FromDate = dr["MIN_STOCK_DATE"].ToString();
                Last_ToDate = dr["MAX_STOCK_DATE"].ToString();
                i = i + 1;
            }

            if (dgvSca01.RowCount > 1)
            {
                int j = dgvSca01.RowCount - 2;

                if (j == 2)
                {
                    FromDate0 = dgvSca01.Rows[1].Cells["시작일자"].Value.ToString();
                    ToDate0 = dgvSca01.Rows[1].Cells["종료일자"].Value.ToString();

                    FromDate1 = dgvSca01.Rows[0].Cells["시작일자"].Value.ToString();
                    ToDate1 = dgvSca01.Rows[0].Cells["시작일자"].Value.ToString();
                }

                if (j == 3)
                {
                    FromDate0 = dgvSca01.Rows[2].Cells["시작일자"].Value.ToString();
                    ToDate0 = dgvSca01.Rows[2].Cells["종료일자"].Value.ToString();

                    FromDate1 = dgvSca01.Rows[1].Cells["시작일자"].Value.ToString();
                    ToDate1 = dgvSca01.Rows[1].Cells["시작일자"].Value.ToString();

                    FromDate2 = dgvSca01.Rows[0].Cells["시작일자"].Value.ToString();
                    ToDate2 = dgvSca01.Rows[0].Cells["시작일자"].Value.ToString();
                }

                if (j == 4)
                {
                    FromDate0 = dgvSca01.Rows[3].Cells["시작일자"].Value.ToString();
                    ToDate0 = dgvSca01.Rows[3].Cells["종료일자"].Value.ToString();

                    FromDate1 = dgvSca01.Rows[2].Cells["시작일자"].Value.ToString();
                    ToDate1 = dgvSca01.Rows[2].Cells["종료일자"].Value.ToString();

                    FromDate2 = dgvSca01.Rows[1].Cells["시작일자"].Value.ToString();
                    ToDate2 = dgvSca01.Rows[1].Cells["종료일자"].Value.ToString();

                    FromDate3 = dgvSca01.Rows[0].Cells["시작일자"].Value.ToString();
                    ToDate3 = dgvSca01.Rows[0].Cells["종료일자"].Value.ToString();
                }

                if (j >= 5)
                {
                    FromDate0 = dgvSca01.Rows[j].Cells["시작일자"].Value.ToString();
                    ToDate0 = dgvSca01.Rows[j].Cells["종료일자"].Value.ToString();

                    FromDate1 = dgvSca01.Rows[j - 1].Cells["시작일자"].Value.ToString();
                    ToDate1 = dgvSca01.Rows[j - 1].Cells["종료일자"].Value.ToString();

                    FromDate2 = dgvSca01.Rows[j - 2].Cells["시작일자"].Value.ToString();
                    ToDate2 = dgvSca01.Rows[j - 2].Cells["종료일자"].Value.ToString();

                    FromDate3 = dgvSca01.Rows[j - 3].Cells["시작일자"].Value.ToString();
                    ToDate3 = dgvSca01.Rows[j - 3].Cells["종료일자"].Value.ToString();
                }
            }
            
        }
        #endregion

        private void dgvSca01_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSca01.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim() == "")
            {
                return;
            }

            FromDate = dgvSca01.Rows[e.RowIndex].Cells["시작일자"].Value.ToString().Trim();
            ToDate = dgvSca01.Rows[e.RowIndex].Cells["종료일자"].Value.ToString().Trim();
            
            if (OnSelect != null)
            { OnSelect(this, new EventArgs()); }
        }

    }
}
