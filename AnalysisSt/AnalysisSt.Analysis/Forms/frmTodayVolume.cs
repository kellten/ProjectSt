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
    public partial class frmTodayVolume : Form
    {
        public frmTodayVolume()
        {
            InitializeComponent();
        }

        private string _sGroupCode;
        private string _tradeDate;

        public string SGroupCode { get { return _sGroupCode; } set { _sGroupCode = value; GetTodayTradeInfo(); } }
        public string TradeDate { get { return _tradeDate; } set { _tradeDate = value; } }

        private void GetTodayTradeInfo()
        {
            if (_sGroupCode == "" || _sGroupCode == null) { return; }

            ucTodayVolume0.TradeDate = _tradeDate;
            ucTodayVolume0.SGroupCode = _sGroupCode;
        }
    }
}
