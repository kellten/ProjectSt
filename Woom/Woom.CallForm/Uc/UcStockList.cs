using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Woom.CallForm.Uc
{
    public partial class UcStockList : UserControl
    {

        public event OnChangeFsa01EventHandler OnChangeFsa01;
        public delegate void OnChangeFsa01EventHandler();

        public UcStockList()
        {
            InitializeComponent();
        }

        private void dgvAllStockList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) { return; }
            if (e.ColumnIndex < 0) { return; }
            if (dgvAllStockList.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim() == "")
            {
                return;
            }

            //stockAttribute.StockName = dgvAllStockList.Rows[e.RowIndex].Cells["STOCK_NAME"].Value.ToString().Trim();
            //stockAttribute.StockCode = dgvAllStockList.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim();

            //if (chkEditMode.Checked == true)
            //{
            //    if (_sGroupCode != "")
            //    {
            //        Favorite.Class.ClsFavFunc clsFavFunc = new Favorite.Class.ClsFavFunc();
            //        clsFavFunc.Fsa01Add(_sGroupCode, dgvAllStockList.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim());

            //        var handler = OnChangeFsa01;
            //        if (handler != null)
            //        {
            //            this.OnChangeFsa01();
            //        }

            //    }
            //}
            //else
            //{
            //    clsPassingStockCode.StockName = dgvAllStockList.Rows[e.RowIndex].Cells["STOCK_NAME"].Value.ToString().Trim();
            //    clsPassingStockCode.StockCode = dgvAllStockList.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim();
            //}
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
