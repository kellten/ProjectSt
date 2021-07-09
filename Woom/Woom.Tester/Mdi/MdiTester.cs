using System;
using System.Windows.Forms;
using Woom.Telegram.Class;

namespace Woom.Tester.Mdi
{
    public partial class MdiTester : Form
    {
        // private int childFormNumber = 0;

        public MdiTester()
        {
            InitializeComponent();
            //Telegram_Send();
        }

        //private string _openType = "1";


        public void ShowChildForm(Form childForm, bool openType = false)
        {
            Boolean isAlreadyContained = false;
            FormCollection fc = Application.OpenForms;
            try
            {
                foreach (Form frm in fc)
                {
                    if (frm == childForm)
                    {
                        isAlreadyContained = true;
                        frm.Activate();
                    }
                }

                if (isAlreadyContained == false)
                {
                    if (openType == true)
                    {
                        childForm.Show();
                    }
                    else
                    {
                        childForm.MdiParent = this;
                        childForm.Show();
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { }
        }

        private void tsbConnectionInfo_Click(object sender, EventArgs e)
        {
            Form oform = new Woom.DataAccess.Forms.FrmConnectionInfo();
            ShowChildForm(oform);
        }

        private void mnuItem10059_Click(object sender, EventArgs e)
        {
            Form oform = new Woom.Tester.Forms.FrmOptCallerTest();
            ShowChildForm(oform);
        }

        private void opt10069ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form oform = new Woom.Tester.Forms.FrmOpt10060Caller_New();
            ShowChildForm(oform);
        }

        private void opt10081ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form oform = new Woom.Tester.Forms.FrmOpt10081Caller();
            ShowChildForm(oform);
        }

        private void tsbFrmStockList_Click(object sender, EventArgs e)
        {
            Form oform = new Woom.Volume.Forms.FrmStockList();
            ShowChildForm(oform, true);
        }

        private void opt20068ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form oform = new Woom.Tester.Forms.FrmOpt20068Caller();
            ShowChildForm(oform);
        }

        private void 종목관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form oform = new Woom.Tester.Forms.FrmGetStockCode();
            ShowChildForm(oform, true);
        }

        private void opt10005ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form oform = new Woom.Tester.Forms.FrmOpt10005Caller();
            ShowChildForm(oform);
        }

        private void 일별거래상세요청Opt10015ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form oform = new Woom.Tester.Forms.FrmOpt10015Caller(null);
            ShowChildForm(oform);
        }

        private void opt90002ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form oform = new Woom.Tester.Forms.FrmOpt90002Caller();
            ShowChildForm(oform);
        }

        private void 엑셀가져오기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form oform = new Woom.Tester.Forms.FrmCsvToDataGridView();
            ShowChildForm(oform);
        }

        private void 주식기본정보요청Opt10001ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form oform = new Woom.Tester.Forms.FrmOpt10001Caller();
            ShowChildForm(oform, true);
        }

        private void 테마그룹관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form oForm = new Woom.CallForm.Forms.FrmThemaManage();
            ShowChildForm(oForm, true);
        }

        private void 종목별투자자기관별차트요청Opt10060Ver2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form oform = new Woom.Tester.Forms.FrmOpt10060CallerPer();
            ShowChildForm(oform);
        }

        private void toolStripButton_finviz_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://finviz.com/map.ashx?t=sec");
        }

        private void toolStripButton_일정_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://kind.krx.co.kr/common/stockschedule.do?method=StockScheduleMain&index=11");
        }

        private void toolStripButton_Dart_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://dart.fss.or.kr/");
        }

        private void toolStripButton_DartNew_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://newdart.fss.or.kr/");
        }

        private void toolStripButton_IR_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.kirs.or.kr/information/broadcast.html");
        }

        private void 크롤링테스트ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form oform = new Woom.Selenium.Forms.FrmExam();
            ShowChildForm(oform);
        }

        private void 기술ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Form oform = new Woom.
            //ShowChildForm(oform);
        }

        private void toolStripButton1_세종_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://sejongdata.co.kr/");
        }

        private void Telegram_Send()
        {
            string text = "TEST";
            string Who = "";
            string errorMessage = null;
            bool ret = ClsTelegramBot.SendMessage(text, out errorMessage);

            switch (ClsTelegramBot._chatid)
            {
                case "chatId1":
                    Who = "사용자1";
                    break;

                default:
                    break;
            }

            ClsTelegramBot._chatid = SDataAccess.ClsApiInfo.TeleGramUserCode;
            text = "[" + Who + "] 전송시간 : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            bool ret2 = ClsTelegramBot.SendMessage(text, out errorMessage);

        }

        private void toolStripButton_에너지_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://kr.investing.com/commodities/energy");
        }

        private void chkLogOn_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLogOn.Checked == true)
            {
                Woom.DataAccess.Logger.ClsDbLogger.LOG_ON = true;
            }
            else
            {
                Woom.DataAccess.Logger.ClsDbLogger.LOG_ON = false;
            }
        }

        private void dart고유번호가져오기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form oform = new Woom.Dart.Forms.FrmDartCaller();
            ShowChildForm(oform);
        }
    }
}