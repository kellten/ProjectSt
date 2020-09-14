using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalysisSt.Main.Mdi
{
    public partial class mdiAnalysisSt : Form
    {
        
        public mdiAnalysisSt()
        {
            InitializeComponent();
            AnalysisSt.KiwoomVB.ModStatus._ModMainStock = ucMain;
            ucMain.OnEventConnect += new AnalysisSt.KiwoomVB.ucMainStockVer2.OnEventConnectEventHandler(OnEventConnect);
            ucMain.Connection();
            InitMdiLoad();
        }

        private void InitMdiLoad()
        {
        
        }

        public event OnEventConnectEventHandler OnEventConnectStatus;
        public event SelectedOnlyReturnStockCodeEventHandler SelectedOnlyReturnStockCodeEvent;
        
        public delegate void OnEventConnectEventHandler(string status);
        public delegate void SelectedOnlyReturnStockCodeEventHandler(string stockCode);

         private void OnEventConnect(string status)
        {
            if (status == "로그인 성공")
            {
            
            }
        }

        private string _openType = "1";

        public  void ShowChildForm(Form childForm)
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
                    if (_openType == "1")
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

#region Menu1
        private void mnuItemFavManage_Click(object sender, EventArgs e)
        {
            Form frm = new AnalysisSt.Common.Forms.frmFavManage();
            ShowChildForm(frm);
        }
#endregion


        private void tsbTester01_Click(object sender, EventArgs e)
        {
            Form frm = new AnalysisSt.Tester.Forms.frmDbTester(ucMain);
            ShowChildForm(frm);
        }

        private void tsbBatchTradeInfo_Click(object sender, EventArgs e)
        {
            Form frm = new AnalysisSt.BatchWorkerVB.frmBatchTradeInfo();
            ShowChildForm(frm);
        }

        private void tsbAnalysisTradeByDate_Click(object sender, EventArgs e)
        {
            Form frm = new AnalysisSt.Analysis.Forms.frmAnalysisTradeByDate();
            ShowChildForm(frm);
        }

        private void tsbChartTest_Click(object sender, EventArgs e)
        {
            Form frm = new AnalysisSt.Chart.Forms.frmTradeAnalyChart();
            ShowChildForm(frm);
        }

        private void mnuStockWaveInfo_Click(object sender, EventArgs e)
        {
            Form frm = new AnalysisSt.Common.Forms.frmStockWaveInfo();
            ShowChildForm(frm);
        }

        private void tsbTradeQty_Click(object sender, EventArgs e)
        {
            Form frm = new AnalysisSt.Analysis.Forms.frmAnalysisTradeQty();
            ShowChildForm(frm);
        }

        private void tsbChartTest2_Click(object sender, EventArgs e)
        {
            Form frm = new AnalysisSt.Chart.Forms.frmChartTester();
            ShowChildForm(frm);
        }

        private void tsbTotalAnlysis_Click(object sender, EventArgs e)
        {
            Form frm = new AnalysisSt.Analysis.Forms.frmTotalAnaylsis();
            ShowChildForm(frm);
        }

        private void tsbToday_Click(object sender, EventArgs e)
        {
            Form frm = new AnalysisSt.Analysis.Forms.frmTodayVolume();
            ShowChildForm(frm);
        }
     
    }
}
