using AnSt.Singleton.ChaPro;
using AnSt.Util.Func;
using System;
using System.Windows.Forms;

namespace AnSt.Chart.Forms
{
    public partial class FrmAnStChart : Form
    {
        #region 멤버변수
        ClsPassingStockCode clsPassingStockCode;
        ClsPassingFormId clsPassingFormId;
        ClsUtilFunc clsUtilFunc;
        private string _FormId;
        public string FormId { get { return _FormId; } set { _FormId = value; } }
        #endregion

        #region 멤버변수Series

        #endregion

        public FrmAnStChart()
        {
            InitializeComponent();
            clsPassingStockCode = ClsPassingStockCode.Instance();
            clsPassingFormId = ClsPassingFormId.Instance();
            clsUtilFunc = new ClsUtilFunc();
            clsPassingStockCode.PropertyChanged += PropertyChanged;
            ucPriceChart1.PriceChartWaveOnSelected += PriceChartWaveOnSelected;
        }
        private void PropertyChanged(object sender, EventArgs e)
        {
            if (_FormId == clsPassingFormId.FormId)
            {
                lblStockCode.Text = clsPassingStockCode.StockCode;
                lblStockName.Text = clsPassingStockCode.StockName;
                ucPriceChart1.clsPriceAttribute.FromDate = clsUtilFunc.DateToString(dtpFromDate.Text);
                ucPriceChart1.clsPriceAttribute.ToDate = clsUtilFunc.DateToString(dtpToDate.Text);
                ucPriceChart1.clsPriceAttribute.clsStockAttribute.StockName = clsPassingStockCode.StockName;
                ucPriceChart1.clsPriceAttribute.clsStockAttribute.StockCode = clsPassingStockCode.StockCode;
            }
        }

        private void PriceChartWaveOnSelected(object sender, string fromDate, string toDate)
        {
            dtpFromDate.Text = clsUtilFunc.StringToDate(fromDate);
            dtpToDate.Text = clsUtilFunc.StringToDate(toDate);
        }

        private void btnReflesh_Click(object sender, EventArgs e)
        {

        }
    }
}
