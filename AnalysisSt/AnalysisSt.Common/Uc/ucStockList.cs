using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnalysisSt.Common.Class;
using AnalysisSt.DataBaseFunc;

namespace AnalysisSt.Common.Uc
{
    public partial class ucStockList : UserControl
    {
        public ucStockList()
        {
            InitializeComponent();
            GetStockList();
        }

        private DataSet _dsAll;
        private clsGetRichData _oGetRichData = new clsGetRichData();
        public event OnSelectEventHandler OnSelect;
        public delegate void OnSelectEventHandler(object sender, EventArgs e);

        public struct StockCode
        {
            public String STOCK_CODE;
            public String STOCK_NAME;
        }

        private StockCode _StockCode;

        public StockCode propStockCode
        {
            get
            { return _StockCode; }
            }

        private void GetStockList()
        {
            DataSet ds = _oGetRichData.GetAllStock();
            _dsAll = ds.Copy();
            ds.Reset();
            dgvAllStockList.DataSource = _dsAll.Tables[0];
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dgvAllStockList.DataSource;
            bs.Filter = string.Format("CONVERT(" + dgvAllStockList.Columns["STOCK_NAME"].DataPropertyName +
                                      ", System.String) like '%" + txtSearch.Text.Replace("'", "''") + "%'");
        }

        private void dgvAllStockList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvAllStockList.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim() == "")
            {
                return;
            }

            _StockCode.STOCK_CODE = dgvAllStockList.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim();
            _StockCode.STOCK_NAME = dgvAllStockList.Rows[e.RowIndex].Cells["STOCK_NAME"].Value.ToString().Trim();

            if (OnSelect != null)
            { OnSelect(this, new EventArgs()); }

        }


    }
}
