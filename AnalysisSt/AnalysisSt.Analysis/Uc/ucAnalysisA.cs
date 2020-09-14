using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalysisSt.Analysis.Uc
{
    public partial class ucAnalysisA : UserControl
    {
        public ucAnalysisA()
        {
            InitializeComponent();
        }

        private string _stockCode;
        private string _FromDate;
        private string _ToDate;

        public string StockCode { get { return _stockCode; } set { _stockCode = value; PassingUcControl(); } }
        public string FromDate { get { return _FromDate; } set { _FromDate = value; } }
        public string ToDate { get { return _ToDate; } set { _ToDate = value; } }

        private void PassingUcControl()
        {
            if (_stockCode == "" || _stockCode == null) { return; }

            ucPrice0.FromDate = FromDate;
            ucPrice0.ToDate = ToDate;
            ucPrice0.StockCode = StockCode;

            ucVolume0.FromDate = FromDate;
            ucVolume0.ToDate = ToDate;
            ucVolume0.StockCode = StockCode;
        }

    }
}
