using AnSt.Singleton.ChaPro;
using AnSt.Util.Func;
using SDataAccess;
using System;
using System.Data;
using System.Windows.Forms;

namespace AnSt.BasicSetting.WaveInfo
{
    public partial class FrmWaveInfo : Form
    {
        ClsPassingStockCode clsPassingStockCode;
        ClsPassingFormId clsPassingFormId;
        private bool _StockCodeEventCheck;
        private string _FormId;
        public string FormId { get { return _FormId; } set { _FormId = value; } }

        public FrmWaveInfo()
        {
            InitializeComponent();
            clsPassingStockCode = ClsPassingStockCode.Instance();
            clsPassingFormId = ClsPassingFormId.Instance();
            _StockCodeEventCheck = false;
            ucWaveInfo1.OnSelected += OnSelected;
        }
        public void AddStockCodeEventProperty()
        {
            if (_StockCodeEventCheck == false)
            {
                clsPassingStockCode.PropertyChanged += PropertyChanged;
                _StockCodeEventCheck = true;
            }

        }

        public void RemoveStockCodeEventProperty()
        {
            if (_StockCodeEventCheck == true)
            {
                clsPassingStockCode.PropertyChanged -= PropertyChanged;
                _StockCodeEventCheck = false;
            }
        }
        private void FrmWaveInfo_Activated(object sender, EventArgs e)
        {
            clsPassingFormId.FormId = _FormId;
            AddStockCodeEventProperty();
        }
        private void PropertyChanged(object sender, EventArgs e)
        {
            if (_FormId == clsPassingFormId.FormId)
            {
                ucWaveInfo1.clsStockAttribute.StockName = clsPassingStockCode.StockName;
                ucWaveInfo1.clsStockAttribute.StockCode = clsPassingStockCode.StockCode;
            }

        }

        private void BtnBigFlow_Click(object sender, EventArgs e)
        {
            ClsSca01Manage clsSca01Manage = new ClsSca01Manage();

            if (clsSca01Manage.SaveSca01(lblModify.Text.Trim(), ucWaveInfo1.clsStockAttribute.StockCode, Convert.ToInt32(txtBigFlow.Text),
                                            CDateTime.FormatDate(mskStartDate.Text),
                                            CDateTime.FormatDate(mskEndDate.Text),
                                            txtStockInfo.Text.Trim(),
                                            CDateTime.FormatDate(mskLowDate.Text),
                                            CDateTime.FormatDate(mskHighDate.Text)) == false)
            {
                MessageBox.Show("입력이 실패하였습니다.");
                return;
            }
            else
            {
                ucWaveInfo1.GetSca01Data(ucWaveInfo1.clsStockAttribute.StockCode);
            }

        }
        private void OnSelected(object sender, string bigFlow)
        {
            lblModify.Text = "C";
            txtBigFlow.Text = ucWaveInfo1.BigFlow.ToString();
            mskStartDate.Text = ucWaveInfo1.StartDate;
            mskEndDate.Text = ucWaveInfo1.EndDate;
            mskLowDate.Text = ucWaveInfo1.LowDate;
            mskHighDate.Text = ucWaveInfo1.HighDate;
            txtStockInfo.Text = ucWaveInfo1.StockInfo;

            GetHighLowData();
        }

        private void btnAutoGetLowHigh_Click(object sender, EventArgs e)
        {
            if (mskStartDate.Text == "" || mskEndDate.Text == "") { return; }
            GetHighLowData();
        }

        private void GetHighLowData()
        {
            KiwoomQuery kiwoomQuery = new KiwoomQuery();
            DataTable dt = new DataTable();
            ClsUtilFunc clsUtilFunc = new ClsUtilFunc();

            dt = kiwoomQuery.p_Opt10081Query("3", ucWaveInfo1.clsStockAttribute.StockCode, clsUtilFunc.DateToString(mskStartDate.Text), clsUtilFunc.DateToString(mskEndDate.Text), false).Tables[0].Copy();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    mskLowDate.Text = dt.Rows[0]["STOCK_DATE"].ToString();
                    lblLowPrice.Text = dt.Rows[0]["LOW_PRICE"].ToString();
                }
            }

            dt = null;
            dt = new DataTable();

            dt = kiwoomQuery.p_Opt10081Query("4", ucWaveInfo1.clsStockAttribute.StockCode, clsUtilFunc.DateToString(mskStartDate.Text), clsUtilFunc.DateToString(mskEndDate.Text), false).Tables[0].Copy();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    mskHighDate.Text = dt.Rows[0]["STOCK_DATE"].ToString();
                    lblHighPrice.Text = dt.Rows[0]["HIGH_PRICE"].ToString();
                }
            }

            dt = null;
            dt = new DataTable();

        }
    }
}
