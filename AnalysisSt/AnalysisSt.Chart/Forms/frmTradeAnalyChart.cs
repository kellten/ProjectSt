using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using AnalysisSt.DataBaseFunc;
using System.Threading;

namespace AnalysisSt.Chart.Forms
{
    public partial class frmTradeAnalyChart : Form
    {
        public frmTradeAnalyChart()
        {
            InitializeComponent();
            ucFav.OnSelect += new AnalysisSt.Common.Uc.ucFav.OnSelectEventHandler(ucFav_onCliked_Fsa01Data);
            ucStockList.OnSelect += new AnalysisSt.Common.Uc.ucStockList.OnSelectEventHandler(ucFav_onCliked_Fsa01Data);
        }

        private void ucFav_onCliked_Fsa01Data(object sender, EventArgs e)
        {
            AnalysisSt.Chart.Uc.ucTradeAnalyChart.StockCode stockCd;
            stockCd.STOCK_CODE = ucFav.propStockCode.STOCK_CODE;
            stockCd.STOCK_NAME = ucFav.propStockCode.STOCK_NAME;
            ucTradeAnalyChart.propStockCode = stockCd;
            ucBaseChart.StockName = ucFav.propStockCode.STOCK_NAME;
            ucBaseChart.StockCode = ucFav.propStockCode.STOCK_CODE;

        }
    }
}
