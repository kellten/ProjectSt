using AnSt.Define.Attribute;
using AnSt.Define.Header;
using AnSt.Singleton.ChaPro;
using System;
using System.Windows.Forms;

namespace AnSt.BasicSetting.StockList
{
    public partial class UcStockList : UserControl
    {
        ClsStockAttribute stockAttribute = new ClsStockAttribute();
        ClsPassingStockCode clsPassingStockCode;

        public event OnChangeFsa01EventHandler OnChangeFsa01;
        public delegate void OnChangeFsa01EventHandler();

        public UcStockList()
        {
            InitializeComponent();
            InitDgv();
            _EditMode = "";
            _sGroupCode = "";
            clsPassingStockCode = ClsPassingStockCode.Instance();
        }

        private string _EditMode;
        public string EditMode { set { _EditMode = value; chkEditMode.Checked = true; } }
        private string _sGroupCode;
        public string SGroupCode { get { return _sGroupCode; } set { _sGroupCode = value; } }

        private void InitDgv()
        {
            ClsDgvDefine clsDgvDefine = new ClsDgvDefine();
            clsDgvDefine.StockListDB(ref dgvAllStockList);

        }
        private void dgvAllStockList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) { return; }
            if (e.ColumnIndex < 0) { return; }
            if (dgvAllStockList.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim() == "")
            {
                return;
            }

            stockAttribute.StockName = dgvAllStockList.Rows[e.RowIndex].Cells["STOCK_NAME"].Value.ToString().Trim();
            stockAttribute.StockCode = dgvAllStockList.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim();

            if (chkEditMode.Checked == true)
            {
                if (_sGroupCode != "")
                {
                    Favorite.Class.ClsFavFunc clsFavFunc = new Favorite.Class.ClsFavFunc();
                    clsFavFunc.Fsa01Add(_sGroupCode, dgvAllStockList.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim());

                    var handler = OnChangeFsa01;
                    if (handler != null)
                    {
                        this.OnChangeFsa01();
                    }

                }
            }
            else
            {
                clsPassingStockCode.StockName = dgvAllStockList.Rows[e.RowIndex].Cells["STOCK_NAME"].Value.ToString().Trim();
                clsPassingStockCode.StockCode = dgvAllStockList.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dgvAllStockList.DataSource;
            bs.Filter = string.Format("CONVERT(" + dgvAllStockList.Columns["STOCK_NAME"].DataPropertyName +
                                      ", System.String) like '%" + txtSearch.Text.Replace("'", "''") + "%'");
        }
    }
}
