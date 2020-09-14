using AnSt.Singleton.ChaPro;
using System;
using System.Data;
using System.Windows.Forms;

namespace AnSt.Mdi
{
    public partial class MdiAnSt : Form
    {
        ClsPassingFormId clsPassingFormId;

        public MdiAnSt()
        {
            InitializeComponent();
            clsPassingFormId = ClsPassingFormId.Instance();
            ucDasinFav1.onChooseStockCode += new BasicSetting.Favorite.UcDasinFav.ChooseStockCode(OnChooseStockCode);
            ucDasinFav1.onChoosGroupCode += new AnSt.BasicSetting.Favorite.UcDasinFav.ChooseGroupCodeEventHandler(ucDasinFav_onChoosGroupCode);
        }

        private void OnChooseStockCode(string stockCode)
        {

        }

        private void ucDasinFav_onChoosGroupCode(DataTable dt, string groupCode, string groupName)
        {

        }

        public void ShowChildForm(Form childForm, string openType)
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
                    if (openType == "1")
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

        private void ToolStripMenuItemFrmFavTester_Click(object sender, EventArgs e)
        {
            Form form = new AnSt.Test.FrmFavTester();
            ShowChildForm(form, "1");
        }

        private void 종목Wave입력ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AnSt.BasicSetting.Tester.FrmWaveInfo form = new AnSt.BasicSetting.Tester.FrmWaveInfo();
            //form.FormId = clsPassingFormId.GetFormId();
            //ShowChildForm(form, "1");

        }

        private void MdiAnSt_Load(object sender, EventArgs e)
        {

        }
        private void tsBtnFav_Click(object sender, EventArgs e)
        {
            AnSt.BasicSetting.Favorite.FrmSimpleFavList form = new AnSt.BasicSetting.Favorite.FrmSimpleFavList();
            ShowChildForm(form, "1");
        }

        private void tsBtnStock_Click(object sender, EventArgs e)
        {
            AnSt.BasicSetting.StockList.FrmStockList form = new AnSt.BasicSetting.StockList.FrmStockList();
            ShowChildForm(form, "1");
        }

        #region MenuItem1
        private void tsbMenuItem1110_Click(object sender, EventArgs e)
        {
            BasicSetting.Favorite.FrmSimpleFavList form = new BasicSetting.Favorite.FrmSimpleFavList();
            ShowChildForm(form, "1");
        }

        private void tsbMenuItem1120_Click(object sender, EventArgs e)
        {
            BasicSetting.Favorite.FrmFavList form = new BasicSetting.Favorite.FrmFavList();
            ShowChildForm(form, "0");

        }
        private void tsbMenuItem1130_Click(object sender, EventArgs e)
        {
            BasicSetting.StockList.FrmStockList form = new BasicSetting.StockList.FrmStockList();
            ShowChildForm(form, "1");

        }
        private void tsbWaveMenuItem1220_Click(object sender, EventArgs e)
        {
            BasicSetting.WaveInfo.FrmWaveInfo form = new BasicSetting.WaveInfo.FrmWaveInfo();
            form.FormId = clsPassingFormId.GetFormId();
            ShowChildForm(form, "1");
        }
        #endregion

        private void 속성값보기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnSt.Util.ViewContAtt.FrmViewContAtt form = new AnSt.Util.ViewContAtt.FrmViewContAtt();
            ShowChildForm(form, "1");
        }

        #region MenuItem2
        private void MenuItem2100_Click(object sender, EventArgs e)
        {
            AnSt.Chart.Forms.FrmAnStChart form = new Chart.Forms.FrmAnStChart();
            ShowChildForm(form, "1");
        }
        #endregion

        private void tsBtnDanFav_Click(object sender, EventArgs e)
        {
            AnSt.BasicSetting.Favorite.FrmDasinFav form = new AnSt.BasicSetting.Favorite.FrmDasinFav();
            ShowChildForm(form, "1");
        }

        private void ucDasinFav1_Load(object sender, EventArgs e)
        {

        }

        private void 종목볼륨기본ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnSt.Analysis.Volume.FrmAnVolume form = new AnSt.Analysis.Volume.FrmAnVolume();
            ShowChildForm(form, "1");
        }
    }
}
