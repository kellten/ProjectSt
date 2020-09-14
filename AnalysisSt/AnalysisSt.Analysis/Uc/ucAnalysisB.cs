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
    public partial class ucAnalysisB : UserControl
    {
        public ucAnalysisB()
        {
            InitializeComponent();
        }


        private string _stockCode;
        private string _FromDate0;
        private string _ToDate0;
        private string _FromDate1;
        private string _ToDate1;
        private string _FromDate2;
        private string _ToDate2;
        private string _FromDate3;
        private string _ToDate3;

        public string StockCode { get { return _stockCode; } set { _stockCode = value; PassingUcControl(); } }
        public string FromDate0 { get { return _FromDate0; } set { _FromDate0 = value; } }
        public string ToDate0 { get { return _ToDate0; } set { _ToDate0 = value; } }
        public string FromDate1 { get { return _FromDate1; } set { _FromDate1 = value; } }
        public string ToDate1 { get { return _ToDate1; } set { _ToDate1 = value; } }
        public string FromDate2 { get { return _FromDate2; } set { _FromDate2 = value; } }
        public string ToDate2 { get { return _ToDate2; } set { _ToDate2 = value; } }
        public string FromDate3 { get { return _FromDate3; } set { _FromDate3 = value; } }
        public string ToDate3 { get { return _ToDate3; } set { _ToDate3 = value; } }

        private void PassingUcControl()
        {
            if (_stockCode == "" || _stockCode == null) { return; }

            if (_FromDate0 != "" && _FromDate0 != null)
            {
                ucPrice0.FromDate = FromDate0;
                ucPrice0.ToDate = ToDate0;
                ucPrice0.StockCode = StockCode;

                ucVolume0.FromDate = FromDate0;
                ucVolume0.ToDate = ToDate0;
                ucVolume0.StockCode = StockCode;

                tableLayoutPanel.RowStyles[0] = new RowStyle(SizeType.Percent, 50);
                tableLayoutPanel.RowStyles[1] = new RowStyle(SizeType.Percent, 50);
                tableLayoutPanel.RowStyles[2] = new RowStyle(SizeType.Percent, 0);
                tableLayoutPanel.RowStyles[3] = new RowStyle(SizeType.Percent, 0);
                tableLayoutPanel.RowStyles[4] = new RowStyle(SizeType.Percent, 0);
                tableLayoutPanel.RowStyles[5] = new RowStyle(SizeType.Percent, 0);
                tableLayoutPanel.RowStyles[6] = new RowStyle(SizeType.Percent, 0);
                tableLayoutPanel.RowStyles[7] = new RowStyle(SizeType.Percent, 0);
            }

            if (_FromDate1 != "" && _FromDate0 != null)
            {
                ucPrice1.FromDate = FromDate1;
                ucPrice1.ToDate = ToDate1;
                ucPrice1.StockCode = StockCode;

                ucVolume1.FromDate = FromDate1;
                ucVolume1.ToDate = ToDate1;
                ucVolume1.StockCode = StockCode;
                tableLayoutPanel.RowStyles[0] = new RowStyle(SizeType.Percent, 25);
                tableLayoutPanel.RowStyles[1] = new RowStyle(SizeType.Percent, 25);
                tableLayoutPanel.RowStyles[2] = new RowStyle(SizeType.Percent, 25);
                tableLayoutPanel.RowStyles[3] = new RowStyle(SizeType.Percent, 25);
                tableLayoutPanel.RowStyles[4] = new RowStyle(SizeType.Percent, 0);
                tableLayoutPanel.RowStyles[5] = new RowStyle(SizeType.Percent, 0);
                tableLayoutPanel.RowStyles[6] = new RowStyle(SizeType.Percent, 0);
                tableLayoutPanel.RowStyles[7] = new RowStyle(SizeType.Percent, 0);
            }

            if (_FromDate2 != "" && _FromDate0 != null)
            {
                ucPrice2.FromDate = FromDate2;
                ucPrice2.ToDate = ToDate2;
                ucPrice2.StockCode = StockCode;

                ucVolume2.FromDate = FromDate2;
                ucVolume2.ToDate = ToDate2;
                ucVolume2.StockCode = StockCode;

                tableLayoutPanel.RowStyles[0] = new RowStyle(SizeType.Percent, 18);
                tableLayoutPanel.RowStyles[1] = new RowStyle(SizeType.Percent, 18);
                tableLayoutPanel.RowStyles[2] = new RowStyle(SizeType.Percent, 16);
                tableLayoutPanel.RowStyles[3] = new RowStyle(SizeType.Percent, 16);
                tableLayoutPanel.RowStyles[4] = new RowStyle(SizeType.Percent, 16);
                tableLayoutPanel.RowStyles[5] = new RowStyle(SizeType.Percent, 16);
                tableLayoutPanel.RowStyles[6] = new RowStyle(SizeType.Percent, 0);
                tableLayoutPanel.RowStyles[7] = new RowStyle(SizeType.Percent, 0);
            }

            if (_FromDate3 != "" && _FromDate0 != null)
            {
                ucPrice3.FromDate = FromDate3;
                ucPrice3.ToDate = ToDate3;
                ucPrice3.StockCode = StockCode;

                ucVolume3.FromDate = FromDate3;
                ucVolume3.ToDate = ToDate3;
                ucVolume3.StockCode = StockCode;

                tableLayoutPanel.RowStyles[0] = new RowStyle(SizeType.Percent, (float)12.5d);
                tableLayoutPanel.RowStyles[1] = new RowStyle(SizeType.Percent, (float)12.5d);
                tableLayoutPanel.RowStyles[2] = new RowStyle(SizeType.Percent, (float)12.5d);
                tableLayoutPanel.RowStyles[3] = new RowStyle(SizeType.Percent, (float)12.5d);
                tableLayoutPanel.RowStyles[4] = new RowStyle(SizeType.Percent, (float)12.5d);
                tableLayoutPanel.RowStyles[5] = new RowStyle(SizeType.Percent, (float)12.5d);
                tableLayoutPanel.RowStyles[6] = new RowStyle(SizeType.Percent, (float)12.5d);
                tableLayoutPanel.RowStyles[7] = new RowStyle(SizeType.Percent, (float)12.5d);
            }
                      
        }
    }
}
