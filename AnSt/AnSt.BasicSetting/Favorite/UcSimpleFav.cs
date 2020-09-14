using AnSt.Define.Attribute;
using AnSt.Define.Header;
using AnSt.Define.SetData;
using AnSt.Singleton.ChaPro;
using System;
using System.Windows.Forms;

namespace AnSt.BasicSetting.Favorite
{
    public partial class UcSimpleFav : UserControl
    {
        ClsStockAttribute stockAttribute = new ClsStockAttribute();
        ClsPassingStockCode clsPassingStockCode;
        public UcSimpleFav()
        {
            InitializeComponent();
            InitDgv();
            clsPassingStockCode = ClsPassingStockCode.Instance();
        }

        private void InitDgv()
        {
            ClsDgvDefine clsDgvDefine = new ClsDgvDefine();

            clsDgvDefine.FCode(ref dgvFCode);
            clsDgvDefine.Fsa01(ref dgvStockList);
            lblSGroupName.Text = "";
            lblSGroupName.Tag = "";
        }

        private void dgvStockList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvStockList.Rows[e.RowIndex].Cells["STOCK_CODE"].Value == null) { return; }
            if (dgvStockList.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim() == "")
            {
                return;
            }
            stockAttribute.StockName = dgvStockList.Rows[e.RowIndex].Cells["STOCK_NAME"].Value.ToString().Trim();
            stockAttribute.StockCode = dgvStockList.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim();

            clsPassingStockCode.StockName = dgvStockList.Rows[e.RowIndex].Cells["STOCK_NAME"].Value.ToString().Trim();
            clsPassingStockCode.StockCode = dgvStockList.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim();

        }

        private void dgvFCode_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFCode.Rows[e.RowIndex].Cells["SGROUP_CODE"].Value == null) { return; }
            if (dgvFCode.Rows[e.RowIndex].Cells["SGROUP_CODE"].Value.ToString().Trim() == "")
            {
                return;
            }

            ClsDgvSetData clsDgvSetData = new ClsDgvSetData();
            clsDgvSetData.GetFsa01Data(ref dgvStockList, dgvFCode.Rows[e.RowIndex].Cells["SGROUP_CODE"].Value.ToString().Trim());
            lblSGroupName.Text = dgvFCode.Rows[e.RowIndex].Cells["SGROUP_NAME"].Value.ToString().Trim();
            lblSGroupName.Tag = dgvFCode.Rows[e.RowIndex].Cells["SGROUP_CODE"].Value.ToString().Trim();

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            ClsPassingViewContAtt clsPassingViewContAtt = ClsPassingViewContAtt.Instance();
            DataGridViewColumnCollection dgvColumn = dgvStockList.Columns;
            clsPassingViewContAtt.OControl = dgvColumn;
        }
    }
}
