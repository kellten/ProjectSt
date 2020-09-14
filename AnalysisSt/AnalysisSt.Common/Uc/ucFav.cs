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
        
    public partial class ucFav : UserControl
    {
 
        public ucFav()
        {
            InitializeComponent();
            dgvFCodeInit();
            dgvFsa01Init();
            SetInit();
        }
        
        private clsGetRichData _oGetRichData = new clsGetRichData();
        private DataSet _dsFsa01Data;
        public DataSet DataFsa01 { get {return _dsFsa01Data;}  }

        public struct StockCode
        {
            public String STOCK_CODE;
            public String STOCK_NAME;
        }

        private StockCode _StockCode;
        private string _sGroupCode;
        public string SGroupCode { get { return _sGroupCode; } set { _sGroupCode = value; } }

        public StockCode propStockCode
        {
            get
            { return _StockCode; }
        }

        private void SetInit()
        {
            GetFCode();
        }

        #region UserEvent
        public event OnSelectEventHandler OnSelect;
        public event OnSelectFCodeEventHandler OnSelectFCode;
        public delegate void OnSelectEventHandler(object sender, EventArgs e);
        public delegate void OnSelectFCodeEventHandler(object sender, EventArgs e); 
        #endregion
                
        private void GetFCode()
        {
            DataSet ds = _oGetRichData.GetFcodeData();
            int i = 0;

            dgvFCode.Rows.Clear();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dgvFCode.Rows.Add();
                dgvFCode.Rows[i].Cells["SGROUP_CODE"].Value = dr["SGROUP_CODE"].ToString();
                dgvFCode.Rows[i].Cells["SGROUP_NAME"].Value = dr["SGROUP_NAME"].ToString();
                dgvFCode.Rows[i].Cells["SGROUP_INFO"].Value = dr["SGROUP_INFO"].ToString();

                i = i + 1;
            }

            ds.Reset();

        }

        private void dgvFsa01Init()
        {
            dgvFsa01.ColumnCount = 6;
            dgvFsa01.Columns[0].Name = "STOCK_CODE";
            dgvFsa01.Columns[1].Name = "STOCK_NAME";
            dgvFsa01.Columns[2].Name = "YBJONG_NAME";
            dgvFsa01.Columns[3].Name = "SEQ_NO";
            dgvFsa01.Columns[4].Name = "SGROUP_CODE";
            dgvFsa01.Columns[5].Name = "SGROUP_NAME";
        }

        private void dgvFCodeInit()
        {
            dgvFCode.ColumnCount = 3;
            dgvFCode.Columns[0].Name = "SGROUP_CODE";
            dgvFCode.Columns[1].Name = "SGROUP_NAME";
            dgvFCode.Columns[2].Name = "SGROUP_INFO";
        }

        private void dgvFCode_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFCode.Rows[e.RowIndex].Cells["SGROUP_CODE"].ToString().Trim() == "")
            {
                return;
            }

            lblSGroupCode.Text = dgvFCode.Rows[e.RowIndex].Cells["SGROUP_CODE"].Value.ToString().Trim();
            txtSGroupName.Text = dgvFCode.Rows[e.RowIndex].Cells["SGROUP_NAME"].Value.ToString().Trim();
            SGroupCode = lblSGroupCode.Text;
            GetFsa01Data(lblSGroupCode.Text);
        }

        private void GetFsa01Data(String sGroupCode)
        {
            DataSet ds = _oGetRichData.GetFsa01Data(sGroupCode);
            int i = 0;

            if (_dsFsa01Data == null)
            {
                _dsFsa01Data = ds.Copy();
            }
            else
            {
                _dsFsa01Data.Reset();
                _dsFsa01Data = ds.Copy();
            }

            dgvFsa01.Rows.Clear();

            if (ds.Tables[0].Rows.Count > 0 && ds != null)
            {
                //dgvFsa01.RowCount = ds.Tables[0].Rows.Count;

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dgvFsa01.Rows.Add();
                    dgvFsa01.Rows[i].Cells["STOCK_CODE"].Value = dr["STOCK_CODE"];
                    dgvFsa01.Rows[i].Cells["STOCK_NAME"].Value = dr["STOCK_NAME"];
                    dgvFsa01.Rows[i].Cells["YBJONG_NAME"].Value = dr["YBJONG_NAME"];
                    dgvFsa01.Rows[i].Cells["SEQ_NO"].Value = dr["SEQ_NO"];
                    dgvFsa01.Rows[i].Cells["SGROUP_CODE"].Value = dr["SGROUP_CODE"];
                    dgvFsa01.Rows[i].Cells["SGROUP_NAME"].Value = dr["SGROUP_NAME"];

                    i = i + 1;
                }
                dgvFsa01.SuspendLayout();
                ds.Reset();

                if (OnSelectFCode != null)
                { OnSelectFCode(this, new EventArgs()); }

            }
            else
            { ds.Reset(); }
        }

        private void dgvFsa01_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFsa01.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim() == "")
            {
                return;
            }

            _StockCode.STOCK_CODE = dgvFsa01.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim();
            _StockCode.STOCK_NAME = dgvFsa01.Rows[e.RowIndex].Cells["STOCK_NAME"].Value.ToString().Trim();

            if (OnSelect != null)
            { OnSelect(this, new EventArgs()); }
        }
   


    }


}
