using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Woom.Dart.Class;
using Woom.DataAccess.PlugIn;
using Woom.DataDefine.Util;

namespace Woom.Dart.Uc
{
    public partial class UcDartApiView : UserControl
    {
        public string PropStockCode
        {
            get { return _stockCode; }
            set
            {
                _stockCode = value;
                GetDartInfo(_stockCode);
            }
        }

        string _stockCode;
        int _row = 0;
        public UcDartApiView()
        {
            InitializeComponent();
        }


        public void GetDartInfo(string stockCode)
        {
            if (stockCode == null)
            {
                return;
            }

            DataTable dt = new DataTable();

            ClsDartApi clsDartApi = new ClsDartApi();
            dt = clsDartApi.GetDartSearchByDate(stockCode: stockCode, crtfc_key: "", corp_code: "", bgn_de: DateTime.Now.Date.AddMonths(-6).ToString("yyyyMMdd"), end_de: DateTime.Now.Date.ToString("yyyyMMdd"),
                                               last_report_at: "N", pbIntf_ty: "", pblntf_detail_ty: "A", corp_cls: "", sort: "date", sort_mth: "desc", page_no: "1", page_count: "10").Tables[0].Copy();

            ClsDataGridViewUtil clsDataGridViewUtil = new ClsDataGridViewUtil();

            if (chkAddSearch.Checked == true)
            {
                // clsDataGridViewUtil.RemoveGridViewRow(dgvNaverSearch);
            }
            else
            {
                clsDataGridViewUtil.RemoveGridViewRow(dgvDartView);
                _row = 0;
            }

            if (dt != null)
            {
                foreach (DataRow  dr in dt.Rows)
                {
                    dgvDartView.Rows.Add();
                    dgvDartView.Rows[_row].Cells["rcept_no"].Value = dr["rcept_no"].ToString().Trim();
                    dgvDartView.Rows[_row].Cells["rcept_dt"].Value = dr["rcept_dt"].ToString().Trim();
                    dgvDartView.Rows[_row].Cells["corp_code"].Value = dr["corp_code"].ToString().Trim();
                    dgvDartView.Rows[_row].Cells["stock_code"].Value = dr["stock_code"].ToString().Trim();
                    dgvDartView.Rows[_row].Cells["report_nm"].Value = dr["report_nm"].ToString().Trim();
                    dgvDartView.Rows[_row].Cells["stock_name"].Value = dr["corp_name"].ToString().Trim();

                    _row = _row + 1;
                }
                
            }

            dt = null;
        }

        private void dgvDartView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ClsDartApi clsDartApi = new ClsDartApi();
            string rcept_no = dgvDartView.Rows[e.RowIndex].Cells["rcept_no"].Value.ToString().Trim();
            //clsDartApi.GetDartDocuments(dgvDartView.Rows[e.RowIndex].Cells["rcept_no"].Value.ToString().Trim());
            webBrowser1.Navigate( "https://dart.fss.or.kr/dsaf001/main.do?rcpNo=" + rcept_no);
            //System.Diagnostics.Process.Start("https://dart.fss.or.kr/dsaf001/main.do?rcpNo=" + rcept_no);

        }
    }
}
