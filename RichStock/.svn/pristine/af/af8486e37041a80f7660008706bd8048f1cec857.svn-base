using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSharp.Common;
using PaikRichStock.Common;

namespace PaikRichStockMain
{
    public partial class frmTester : Form
    {
        private ucMainStockVer2 _mainStockVer2;
        private CSharp.Common.EventManage.clsEventManger _clsEventManager = new CSharp.Common.EventManage.clsEventManger();
        private static string _stockCode = "088910";
        private static string _stockName = "동우";

        public frmTester(ucMainStockVer2 mainStockVer2)
        {
            InitializeComponent();
            _mainStockVer2 = mainStockVer2;
            _clsEventManager.MainStockVer2 = _mainStockVer2;
            _clsEventManager.dgv10059 = dgv10059;
            _clsEventManager.dgv10059Price = dgv10059Price;
            _clsEventManager.dgv10081New = dgv10081;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _clsEventManager.MainCombine(_stockCode, _stockName, DateTime.Now.ToString("yyyyMMdd"));
        }


    }
}
