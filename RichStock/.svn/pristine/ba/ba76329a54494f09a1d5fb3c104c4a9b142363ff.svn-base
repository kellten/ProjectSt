using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices; 

namespace PaikRichStockMain.Mdi
{
    public partial class PaikRichStockMdi : Form
    {

        private int childFormNumber = 0;
        private int LargePnSize = 360;
        private int SmallPnSize = 0;

        #region 초기
        public PaikRichStockMdi()
        {
            InitializeComponent();
            ucMainStockVer2.OnEventConnect += new PaikRichStock.Common.ucMainStockVer2.OnEventConnectEventHandler(OnEventConnect);
            ucFavList.SelectedOnlyReturnStockCode += new PaikRichStock.UcForm.ucFavList.SelectedOnlyReturnStockCodeEventHandler(SelectedOnlyReturnStockCode);
            ucMainStockVer2.Connection();
            InitMdiLoad();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "창 " + childFormNumber++;
            childForm.Show();
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
                private void pnMain_Paint(object sender, PaintEventArgs e)
        {

        }
        #endregion

        #region USerEvent
        //public event OnEventConnectEventHandler OnEventConnectStatus;
        //public event SelectedOnlyReturnStockCodeEventHandler SelectedOnlyReturnStockCodeEvent;
        
        public delegate void OnEventConnectEventHandler(string status);
        public delegate void SelectedOnlyReturnStockCodeEventHandler(string stockCode);

        private void OnEventConnect(string status)
        {
            if (status == "로그인 성공")
            {
                ucCondition.Connected = true;
                ucCondition.MainStock = ucMainStockVer2;
            }
        }
        private void SelectedOnlyReturnStockCode(string stockCode)
        {
            CallStockCodeInfo(stockCode);
        }
        #endregion

        #region Function
        /// <summary>
        /// 함수
        /// </summary>
        private void InitMdiLoad()
        {
            ucFavList.MainStock2 = ucMainStockVer2;
        }
        public void ShowChildForm(Form childForm)
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
                if (tsCbFormOpenType.SelectedIndex == 1)
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
            catch (Exception ex){ Console.WriteLine(ex.Message); }
            finally {}
         }

        private void CallStockCodeInfo(string stockCode)
        { 
            Form frm = this.ActiveMdiChild;
            if (frm == null)
            {
                
                foreach (Form frm2 in Application.OpenForms)
                {
                    SendFormTotockCode(frm2, stockCode);
                }
                return;
            }

            SendFormTotockCode(frm, stockCode);
        }

        private void SendFormTotockCode(Form frm,string stockCode)
        {
            switch (frm.Name)
            {
                case "frmFinance":
                    ((PaikRichStock.MenuItem2.frmFinance)frm).StockCode = stockCode;
                break;
                case "frmChart":
                  ((Chart.frmChart)frm).CallMdiGetChartData(stockCode);
                  ((Chart.frmChart)frm).Focus();
                break;
            }
        }
        #endregion
 
        #region ControlEvent
        private void tsBtnLeft_Click(object sender, EventArgs e)
        {
            if (pnStockList.Width == LargePnSize)
            {
                pnStockList.Width = 0;
            }

        }
        private void tsBtnRight_Click(object sender, EventArgs e)
        {
            if (pnStockList.Width == SmallPnSize)
            {
                pnStockList.Width = 360;
            }
        }
        private void tsMenuItem1110_Click(object sender, EventArgs e)
        {
            Form frm = new PaikRichStock.MenuItem1.frmStockVolumeScriptToXml_Ver2(ucMainStockVer2);
            ShowChildForm(frm);
        }
        private void tsMenuItem3100_Click(object sender, EventArgs e)
        {
            Form frm = new NewsFinder.frmMainVer2();
            ShowChildForm(frm);
        }
        private void tsMenuItem1200_Click(object sender, EventArgs e)
        {
            Form frm = new PaikRichStock.MenuItem1.frmFavManage(ucMainStockVer2);
            ShowChildForm(frm);
        }
        private void tsMenuItem2100_Click(object sender, EventArgs e)
        {
            Form frm = new PaikRichStock.MenuItem2.frmFinance(ucMainStockVer2);
            ShowChildForm(frm);
        }
        private void tsMenuItem2210_Click(object sender, EventArgs e)
        {
            Form frm = new PaikRichStock.MenuItem1.frmVolumeAnalysis(ucMainStockVer2);
            ShowChildForm(frm);
        }
        private void tsMenuItem4100_Click(object sender, EventArgs e)
        {
            Chart.frmChart frm = new Chart.frmChart();
            frm.MainStock = ucMainStockVer2;
            ShowChildForm(frm);
        }
        private void tsMenuItem2220_Click(object sender, EventArgs e)
        {
            Form frm = new PaikRichStock.MenuItem2.frmSearchConditionList(ucMainStockVer2);
            ShowChildForm(frm);
        }
        #endregion

        private void PaikRichStockMdi_KeyDown(object sender, KeyEventArgs e)
        {

        }


        private void mnuItemTestTask_Click(object sender, EventArgs e)
        {
            Form frm = new frmTester(ucMainStockVer2);
            ShowChildForm(frm);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]

        public static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, IntPtr lParam);
        
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message message, System.Windows.Forms.Keys keydata)
        {

            SendMessage(this.ActiveMdiChild.Handle, message.Msg, message.WParam, message.LParam);

            return base.ProcessCmdKey(ref message, keydata);

        }

    }
}
