using AnSt.Define.Attribute;
using AnSt.Define.Header;
using AnSt.Define.SetData;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AnSt.BasicSetting.WaveInfo
{
    public partial class UcWaveInfo : UserControl
    {
        public ClsStockAttribute clsStockAttribute;
        public delegate void OnSelectedEventHandler(object sender, string bifFlow);
        public event OnSelectedEventHandler OnSelected;

        #region Property
        private int _bigFlow;
        private string _startDate;
        private string _endDate;
        private string _lowDate;
        private string _highDate;
        private string _stockInfo;

        public int BigFlow { get { return _bigFlow; } }
        public string StartDate { get { return _startDate; } }
        public string EndDate { get { return _endDate; } }
        public string LowDate { get { return _lowDate; } }
        public string HighDate { get { return _highDate; } }
        public string StockInfo { get { return _stockInfo; } }
        #endregion

        public UcWaveInfo()
        {
            InitializeComponent();
            clsStockAttribute = new ClsStockAttribute();
            clsStockAttribute.PropertyChanged += PropertyChanged;
        }

        private void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (clsStockAttribute.StockCode == "") { return; }

            lblStockCode.Text = clsStockAttribute.StockCode;
            lblStockName.Text = clsStockAttribute.StockName;
            GetSca01Data(clsStockAttribute.StockCode);
        }
        public void GetSca01Data(string stockCode)
        {
            ClsDgvDefine clsDgvDefine = new ClsDgvDefine();
            ClsDgvSetData clsDgvSetData = new ClsDgvSetData();

            clsDgvDefine.Sca01(ref dgvSca01);
            clsDgvSetData.GetSca01Data(ref dgvSca01, stockCode);
        }
        private void dgvSca01_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) { return; }
            if (e.ColumnIndex < 0) { return; }
            if (dgvSca01.Rows[e.RowIndex].Cells["BIG_FLOW"].Value == null) { return; }
            if (dgvSca01.Rows[e.RowIndex].Cells["BIG_FLOW"].Value.ToString().Trim() == "")
            {
                return;
            }
            _bigFlow = Convert.ToInt32(dgvSca01.Rows[e.RowIndex].Cells["BIG_FLOW"].Value);
            _startDate = dgvSca01.Rows[e.RowIndex].Cells["START_DATE"].Value.ToString();
            _endDate = dgvSca01.Rows[e.RowIndex].Cells["END_DATE"].Value.ToString();
            _lowDate = dgvSca01.Rows[e.RowIndex].Cells["LOW_DATE"].Value.ToString();
            _highDate = dgvSca01.Rows[e.RowIndex].Cells["HIGH_DATE"].Value.ToString();
            _stockInfo = dgvSca01.Rows[e.RowIndex].Cells["STOCK_INFO"].Value.ToString();

            var handler = OnSelected;
            if (handler != null)
            {
                this.OnSelected(this, dgvSca01.Rows[e.RowIndex].Cells["BIG_FLOW"].Value.ToString().Trim());
            }
        }
    }
}
