using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SDataAccess;

namespace Woom.CallForm.Uc
{
    public partial class UcStockList : UserControl
    {

        public event OnSelectedStockCodeEventHandler OnSelectedStockCode;
        public delegate void OnSelectedStockCodeEventHandler(string stockCode);

        public UcStockList()
        {
            InitializeComponent();

            DataTable dt = new DataTable();
            RichQuery richQuery = new RichQuery();
            int row = 0;


            dt = richQuery.p_ScodeQuery(query: "2", stockCode: "", ybYongCode: "", bln3tier: false).Tables[0].Copy();

            foreach (DataRow dr in dt.Rows)
            {

                dgvAllStockList.Rows.Add();
                dgvAllStockList.Rows[row].Cells["STOCK_CODE"].Value = dr["STOCK_CODE"].ToString().Trim();
                dgvAllStockList.Rows[row].Cells["STOCK_NAME"].Value = dr["STOCK_NAME"].ToString().Trim();

                row = row + 1;

            }

        }

        private void dgvAllStockList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) { return; }
            if (e.ColumnIndex < 0) { return; }
            if (dgvAllStockList.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim() == "")
            {
                return;
            }

            var handler = OnSelectedStockCode;
            if (handler != null)
            {
                OnSelectedStockCode(dgvAllStockList.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim());
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
