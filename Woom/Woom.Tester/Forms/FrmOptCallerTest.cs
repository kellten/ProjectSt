using System;
using System.Data;
using System.Windows.Forms;
using Woom.DataAccess.OptCaller.Class;

namespace Woom.Tester.Forms
{
    public partial class FrmOptCallerTest : Form
    {
        public FrmOptCallerTest()
        {
            InitializeComponent();
            _opt10059.Opt10059_OnReceived += Opt10059_OnReceived;
            _opt100592.Opt10059_OnReceived += Opt10059_OnReceived2;
            _opt10081.Opt10081_OnReceived += Opt10081_OnReceived;
        }
        private ClsOpt10059 _opt10059 = new ClsOpt10059();

        private ClsOpt10059 _opt100592 = new ClsOpt10059();

        private ClsOpt10081 _opt10081 = new ClsOpt10081();
        private void button1_Click(object sender, EventArgs e)
        {
            _opt10059.SetInit("01");
            if (_opt10059.SetValue("20170101", "088910", "동우팜투테이블", "1", "0", "1") == false)
            {
                return;
            }
            _opt10059.Opt10059();


        }

        private void Opt10059_OnReceived(string stockCode, DataTable dt, int sPreNext)
        {
            richTextBox1.Text = richTextBox1.Text + stockCode + "/" + dt.Rows[0]["일자"].ToString() +
                                           " - " + dt.Rows[dt.Rows.Count - 1]["일자"].ToString() + "\r\n";
            richTextBox1.SelectionStart = richTextBox1.Text.LastIndexOfAny(Environment.NewLine.ToCharArray()) + 1;

            richTextBox1.ScrollToCaret();

            if (sPreNext == 2)
            {
                _opt10059.Opt10059(true);
            }
            else
            {
                _opt10059.Dispose();
            }
        }

        private void Opt10059_OnReceived2(string stockCode, DataTable dt, int sPreNext)
        {
            richTextBox1.Text = richTextBox1.Text + stockCode + dt.Rows[0]["일자"].ToString() +
                                           " - " + dt.Rows[dt.Rows.Count - 1]["일자"].ToString() + "\r\n";
            richTextBox1.SelectionStart = richTextBox1.Text.LastIndexOfAny(Environment.NewLine.ToCharArray()) + 1;

            richTextBox1.ScrollToCaret();

            if (sPreNext == 2)
            {
                _opt100592.Opt10059(true);
            }
            else
            {
                _opt100592.Dispose();
            }
        }

        private void Opt10081_OnReceived(string stockCode, DataTable dt, int sPreNext)
        {
            richTextBox2.Text = richTextBox2.Text + dt.Rows[0]["일자"].ToString() +
                                           " - " + dt.Rows[dt.Rows.Count - 1]["일자"].ToString() + "\r\n";
            richTextBox2.SelectionStart = richTextBox2.Text.LastIndexOfAny(Environment.NewLine.ToCharArray()) + 1;

            richTextBox2.ScrollToCaret();

            if (sPreNext == 2)
            {
                _opt10081.Opt10081(StockCode: "088910", StockName: "동우팜투테이블", StdDate: "20170101", ModifyJugaGb: "1");
            }
            else
            {
                _opt10081.Dispose();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _opt10081.SetInit("01");
     
            _opt10081.Opt10081(StockCode: "088910", StockName: "동우팜투테이블", StdDate: "20170101", ModifyJugaGb: "1");
        }

        private void button3_Click(object sender, EventArgs e)
        {

            _opt100592.SetInit("01");
            if (_opt100592.SetValue("20170101", "136480", "하림", "1", "0", "1") == false)
            {
                return;
            }
            _opt100592.Opt10059();
        }
    }
}
