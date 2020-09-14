using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalysisSt.Analysis.Forms
{
    public partial class frmAnalysisA : Form
    {
        public frmAnalysisA(string stCodeValue, string fromDateValue, string toDateValue)
        {
            InitializeComponent();

            FromDate = fromDateValue;
            ToDate = toDateValue;
            StockCode = stCodeValue;
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

            ucAnalysisA0.FromDate = FromDate;
            ucAnalysisA0.ToDate = ToDate;
            ucAnalysisA0.StockCode = StockCode;
        }
    }
}
