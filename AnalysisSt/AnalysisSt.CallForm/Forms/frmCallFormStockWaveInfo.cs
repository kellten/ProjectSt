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

namespace AnalysisSt.CallForm.Forms
{
    public partial class frmCallFormStockWaveInfo : Form
    {
        public frmCallFormStockWaveInfo()
        {
            InitializeComponent();
        }

        public struct StockCode
        {
            public String STOCK_CODE;
            public String STOCK_NAME;
        }

        private StockCode _stockCode;
        public StockCode propStockCode
        {
            get { return _stockCode; }
            set
            {
                _stockCode = value;
                SetStockCode();
            }
        }

        public struct ScareDate
        {
            public String FROM_DATE;
            public String TO_DATE;
        }

        private ScareDate _scareDate;

        public ScareDate propScareDate
        {
            get { return _scareDate; }
        }
        
        private void SetStockCode()
        {
            lblStockCode.Text = _stockCode.STOCK_CODE;
            lblStockName.Text = _stockCode.STOCK_NAME;
        }

        private void frmCallFormStockWaveInfo_Load(object sender, EventArgs e)
        {
            dgvSca01Init();
            GetSca01Data();
        }

        private void GetSca01Data()
        {
            DataSet ds;
            RichQuery oRichQuery = new RichQuery();
            int i = 0;

            if (lblStockCode.Text == "")
            {
                return;
            }

            dgvSca01.Rows.Clear();

            ds = oRichQuery.p_Sca01Query("1", lblStockCode.Text, 0, 0, "", "", false);

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

        private void dgvSca01_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSca01.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString() == "")
            {
                return;
            }

           _scareDate.FROM_DATE = CDateTime.FormatDate(dgvSca01.Rows[e.RowIndex].Cells["시작일자"].Value.ToString(), "-");
           _scareDate.TO_DATE = CDateTime.FormatDate(dgvSca01.Rows[e.RowIndex].Cells["종료일자"].Value.ToString(), "-");

           this.DialogResult = DialogResult.OK;
        }

        
    }
}
