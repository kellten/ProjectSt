using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalysisSt.Chart.Forms
{
    public partial class frmChartTester : Form
    {
        public frmChartTester()
        {
            InitializeComponent();
        }

        #region struct
        public struct stSTOCK_CODE
        {
            public String STOCK_CODE;
            public String STOCK_NAME;
        }
        #endregion

        #region 전역변수
        private stSTOCK_CODE _Stock_Code;
        
        #endregion

        #region Property
        public stSTOCK_CODE STOCK_CODE { get { return _Stock_Code; } set { _Stock_Code = value; SetStockCode(); } }
        #endregion

        #region Func
        private void SetStockCode()
        { 
            ucBaseChartTester.StockName = _Stock_Code.STOCK_NAME;
            ucBaseChartTester.StockCode = _Stock_Code.STOCK_CODE;
            ucPrice1.FromDate = "20170101";
            ucPrice1.ToDate = "20170907";
            ucPrice1.StockCode = "088910";
        }
        #endregion

        private void frmChartTester_Load(object sender, EventArgs e)
        {
            stSTOCK_CODE st;
            st.STOCK_CODE =  "088910";
            st.STOCK_NAME = "동우팜투테이블";
            STOCK_CODE = st;
        }
    }
}
