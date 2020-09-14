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
using AnalysisSt.KiwoomVB;
using AnalysisSt.Dasin;
using AnalysisSt.Dasin.ClsDasinCom;

namespace AnalysisSt.Tester.Forms
{
    public partial class frmDbTester : Form
    {
        private AnalysisSt.KiwoomVB.ucMainStockVer2 _ucMainStockVer2 = new AnalysisSt.KiwoomVB.ucMainStockVer2();

        public frmDbTester(AnalysisSt.KiwoomVB.ucMainStockVer2 ucMain)
        {
            InitializeComponent();
            _ucMainStockVer2 = ucMain;
        }

        private void frmDbTester_Load(object sender, EventArgs e)
        {
            GetAllStockCode();
        } 

        private void GetAllStockCode()
        { 
            DataSet ds = new DataSet();
            //DataRow dr = new DataRow();
            RichQuery oRichQuery = new RichQuery();

            ds = oRichQuery.p_ScodeQuery("1", "", "", false);

            if (ds.Tables[0].Rows.Count < 1)
            {
                ds.Reset();
            }
            else
            {
                dgvStockList.DataSource = ds.Tables[0];
            }
                        
        }

        private void dgvStockList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            label1.Text = dgvStockList.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e) 
        {
            OptEventCaller oOptEventCaller = new OptEventCaller();
            oOptEventCaller.GetOpt10059(label1.Text);
        }

        private clsCpSysDib _clsCpSysDib = new clsCpSysDib();

        private void button2_Click(object sender, EventArgs e)
        {
            Dasin.ModDasin.ModDasinApi modDaApi = new Dasin.ModDasin.ModDasinApi();
            modDaApi.GetAllStockCode();
            _clsCpSysDib.CpSvr7254_SetInputValue(modDaApi._ds.Tables[0].Rows[0]["STOCK_CODE"].ToString(), 0, 20170701, 20170726, 0);

            //Dasin.ModDasin.ModDasinApi modDaApi = new Dasin.ModDasin.ModDasinApi();
            //modDaApi.MainRegEvent();
            ////modDaApi.funcCpSvr7254(label1.Text);


        }


    }
}
