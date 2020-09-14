using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CybosDa.DataAccess;

namespace CybosDa.Tester.Forms
{
    public partial class frmTester : Form
    {
        public frmTester()
        {
            InitializeComponent();
            _clsCpSvr7254.CpSvr7254_OnReceived += CpSvr7254_OnReceived;
            _clsCpSvr7254.CpSvr7254_OnEndGetData += CpSvr7254_OnEndGetData;
        }

        private CybosDa.DataAccess.Connection.clsCybosConnection _clsCybosConnection = new CybosDa.DataAccess.Connection.clsCybosConnection();
        private CybosDa.DataAccess.CpSvr.clsCpSvr7254 _clsCpSvr7254 = new CybosDa.DataAccess.CpSvr.clsCpSvr7254();
        private DataTable _dtStockCode = new DataTable();
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (_clsCybosConnection.CybosConnection() == true)
            {
                label1.Text = "성공";
            }

            //_dtStockCode = _clsCybosConnection.LoadStockCode();
        }
        private void _cpCybos_OnDisconnect(int IsConnect)
        {
            if (IsConnect != 1)
            {
                label1.Text = "접속 취소";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _clsCpSvr7254.UseCpSvr7254();
            _clsCpSvr7254.GetCpSvr7254("A088910", DataAccess.CpSvr.clsCpSvr7254.ChoiceGiganTypeIndex.일별, "20170901", "20170910", DataAccess.CpSvr.clsCpSvr7254.ChoiceTradeTypeIndex.순매수, Common.Class.ClsDefineDataType.TradeGb.TradeGbTypeIndex.전체, DataAccess.CpSvr.clsCpSvr7254.ChoiceDataGbIndex.추정금액백만원);
        }

        private async Task DoGetCpSvr7254()
        {
            TaskCompletionSource<bool> tcs = null;
            //_clsCpSvr7254.CpSvr7254_OnReceived += (string stockCode, DataTable dt) =>
            //{
            //    if (tcs == null || tcs.Task.IsCompleted)
            //    { return dt; }
            //    CpSvr7254_OnReceived(stockCode, dt);
            //    tcs.SetResult(true);
            //};

            //_clsCpSvr7254.CpSvr7254_OnReceived += (stockCode, dt) =>
            //{
            //    if (tcs == null || tcs.Task.IsCompleted)
            //    { return null; }
            //    CpSvr7254_OnReceived(stockCode, dt);
            //    tcs.SetResult(true);
            //};

            await tcs.Task;
            tcs = null;
            
        }

        private void CpSvr7254_OnReceived(string stockCode, DataTable dt, int NextCall)
        {
            richTextBox1.Text = richTextBox1.Text + dt.Rows[0]["일자"].ToString() +
                                            " - " + dt.Rows[dt.Rows.Count - 1]["일자"].ToString() + "\r\n";
            richTextBox1.SelectionStart = richTextBox1.Text.LastIndexOfAny(Environment.NewLine.ToCharArray()) + 1;

            richTextBox1.ScrollToCaret();
        }

        private void CpSvr7254_OnEndGetData()
        {
            MessageBox.Show("모든 자료를 다 가져왔습니다.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CybosDa.Common.Forms.FrmCybosHelp oform = new CybosDa.Common.Forms.FrmCybosHelp();
            oform.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _clsCpSvr7254.StopGetData();
        }
    }
}
