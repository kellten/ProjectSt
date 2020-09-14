using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnalysisSt.DataBaseFunc;

namespace AnalysisSt.Analysis.Forms
{
    public partial class frmTotalAnaylsis : Form
    {
        public frmTotalAnaylsis()
        {
            InitializeComponent();
            InitSetting();
            _splitConB_SplitterDistance = splitConB.SplitterDistance;
        }

        private int _splitConB_SplitterDistance;

        private string _StockCode;
        private string _StockName;
        private string _sGroupCode;

        public string StockCode { get { return _StockCode; } set { _StockCode = value; GetWaveInfo(); }}
        public string StockName { get { return _StockName; } set { _StockName = value; } }
        public string SGroupCode { get { return _sGroupCode; } set { _sGroupCode = value; } }

        private void InitSetting()
        {
            ucFav0.OnSelect += new AnalysisSt.Common.Uc.ucFav.OnSelectEventHandler(ucFav_onCliked_Fsa01Data);
            ucFav0.OnSelectFCode += new AnalysisSt.Common.Uc.ucFav.OnSelectFCodeEventHandler(ucFav_onCliked_FCodeData);
            ucStockList0.OnSelect += new AnalysisSt.Common.Uc.ucStockList.OnSelectEventHandler(ucStockList_onCliked_Fsa01Data);
            ucWaveInfo0.OnSelect += new AnalysisSt.Common.Uc.ucWaveInfo.OnSelectEventHandler(ucWaveInfo_onCliked_Fsa01Data);
        }

        private void InitGetData()
        {
            GetWaveInfo();
        }
        private void GetWaveInfo()
        {
            if (_StockCode == "" || _StockCode == null) { return; }
            ucWaveInfo0.StockCode = _StockCode;
        }

        #region Uc Event
        private void ucFav_onCliked_Fsa01Data(object sender, EventArgs e)
        {
            StockName = ucFav0.propStockCode.STOCK_NAME;
            StockCode =  ucFav0.propStockCode.STOCK_CODE;
            
        }
        private void ucFav_onCliked_FCodeData(object sender, EventArgs e)
        {
            SGroupCode = ucFav0.SGroupCode;
            lblSGroupCode.Text = SGroupCode;
        }
        private void ucStockList_onCliked_Fsa01Data(object sender, EventArgs e)
        {
            StockName = ucStockList0.propStockCode.STOCK_NAME;
            StockCode = ucStockList0.propStockCode.STOCK_CODE;
        }
        private void ucWaveInfo_onCliked_Fsa01Data(object sender, EventArgs e)
        {
            lblFromDate.Text = ucWaveInfo0.FromDate;
            lblToDate.Text = ucWaveInfo0.ToDate;
            ucAnalysisA0.FromDate = lblFromDate.Text;
            ucAnalysisA0.ToDate = lblToDate.Text;
            ucAnalysisA0.StockCode = _StockCode;       
            
        }
        #endregion

        #region Control Event
        private void chkLeftView_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLeftView.Checked == true)
            {
                splitConB.SplitterDistance = 0;
            }
            else
            {
                splitConB.SplitterDistance = _splitConB_SplitterDistance;
            }
        }
        #endregion

        private void btnAllAnalysisA_Click(object sender, EventArgs e)
        {
            if (ucWaveInfo0.FromDate0 != "" && ucWaveInfo0.FromDate0 != null)
            {
                Form oFrm0 = new AnalysisSt.Analysis.Forms.frmAnalysisA(_StockCode, ucWaveInfo0.FromDate0, ucWaveInfo0.ToDate0);
                oFrm0.Show();

            }
            if (ucWaveInfo0.FromDate1 != "" && ucWaveInfo0.FromDate0 != null)
            {
                Form oFrm1 = new AnalysisSt.Analysis.Forms.frmAnalysisA(_StockCode, ucWaveInfo0.FromDate1, ucWaveInfo0.ToDate1);
                oFrm1.Show();
            }
            if (ucWaveInfo0.FromDate2 != "" && ucWaveInfo0.FromDate0 != null)
            {
                Form oFrm2 = new AnalysisSt.Analysis.Forms.frmAnalysisA(_StockCode, ucWaveInfo0.FromDate2, ucWaveInfo0.ToDate2);
                oFrm2.Show();
            }
            if (ucWaveInfo0.FromDate3 != "" && ucWaveInfo0.FromDate0 != null)
            {
                Form oFrm3 = new AnalysisSt.Analysis.Forms.frmAnalysisA(_StockCode, ucWaveInfo0.FromDate3, ucWaveInfo0.ToDate3);
                oFrm3.Show();
            }
        }

        private void btnAllWave_Click(object sender, EventArgs e)
        {
            if (ucWaveInfo0.Last_FromDate != "" && ucWaveInfo0.Last_FromDate != null)
            {
                Form oFrm0 = new AnalysisSt.Analysis.Forms.frmAnalysisA(_StockCode, ucWaveInfo0.Last_FromDate, ucWaveInfo0.Last_ToDate);
                oFrm0.Show();

            }
        }

        private void btnTodayVolumeView_Click(object sender, EventArgs e)
        {
            if (_sGroupCode == "" || _sGroupCode == null)
            {
                MessageBox.Show("선택된 그룹이 없습니다.");
                return;
            }
            DataTable dt;
            DataTable dt2;
            RichQuery rc = new RichQuery();
            KiwoomQuery ki = new KiwoomQuery();
            string stdDate = AnalysisSt.Common.Class.clsDicDefine.GetVolumeData();

            dt = rc.p_FCodeQuery("4", _sGroupCode, "", "", false).Tables[0].Copy();

            foreach (DataRow dr in dt.Rows)
            {
                dt2 = ki.p_Opt10060QtyMinMaxQuery("1", dr["STOCK_CODE"].ToString().Trim(), "1", stdDate, false).Tables[0].Copy();

                if (dt2 == null || dt2.Rows.Count < 1 )
                {
                    if (MessageBox.Show(dr["STOCK_NAME"].ToString().Trim() +
                                 "10060자료가 없습니다. 자료를 입력하시겠습니까?", "10060 자료생성", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Form oFrm0 = new AnalysisSt.BatchWorkerCSharp.BatchWorker.frmBatchWorkerOpt10060(dr["STOCK_CODE"].ToString().Trim(),
                                                                                                          dr["STOCK_NAME"].ToString().Trim() );
                        oFrm0.Show();
                    }
                }

                if (dt2.Rows[0]["MAX_STOCK_DATE"].ToString().Trim() != stdDate)
                {
                    if (MessageBox.Show(dr["STOCK_NAME"].ToString().Trim() +
                                 "10060자료가 최신자료가 아닙니다. 자료를 입력하시겠습니까?", "10060 자료생성", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Form oFrm0 = new AnalysisSt.BatchWorkerCSharp.BatchWorker.frmBatchWorkerOpt10060(dr["STOCK_CODE"].ToString().Trim(),
                                                                                                          dr["STOCK_NAME"].ToString().Trim());
                        oFrm0.Show();
                    }
                }

                dt2.Reset();
            }
            

        }
    }
}
