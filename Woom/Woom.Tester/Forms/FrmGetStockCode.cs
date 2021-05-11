using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Woom.DataAccess.OptCaller.Class;
using SDataAccess;
using Woom.DataAccess.PlugIn;

namespace Woom.Tester.Forms
{
    public partial class FrmGetStockCode : Form
    {
        public FrmGetStockCode()
        {
            InitializeComponent();
            splitContainer2.SplitterDistance = 25;
            GetNewStockCode();
        }

        private void GetNewStockCode()
        {
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            RichQuery richQuery = new RichQuery();
            ClsGetKoaStudioMethod clsGetKoaStudioMethod = new ClsGetKoaStudioMethod();
            dt = richQuery.p_ScodeQuery(query: "1", stockCode: "", ybYongCode:"", bln3tier: false).Tables[0].Copy();
            dt2 = clsGetKoaStudioMethod.GetCodeListByMarketCallBackDataTable(stockGb: "999");
            int row = 0;
            string stockName = "";

            dgv1.DataSource = dt;
            
            var rows = from t1 in dt2.AsEnumerable()
                       join t2 in dt.AsEnumerable() on t1.Field<string>("STOCK_CODE") equals t2.Field<string>("STOCK_CODE") into tg
                       from tcheck in tg.DefaultIfEmpty()
                       where tcheck == null
                       select t1;

            foreach (DataRow dr in rows)
            {
                if (dr["STOCK_CODE"].ToString().Trim() == "")
                {
                    continue;
                }

                stockName = ClsAxKH.GetMasterCodeName(dr["STOCK_CODE"].ToString());

                if (stockName.Contains("스팩") == true)
                {
                    continue;
                }

                if (stockName.Contains("KOSEF") == true)
                {
                    continue;
                }


                if (stockName.Contains("일본") == true)
                {
                    continue;
                }

                if (stockName.Contains("TIGER") == true)
                {
                    continue;
                }

                if (stockName.Contains("KBSTAR") == true)
                {
                    continue;
                }

                if (stockName.Contains("KINDEX") == true)
                {
                    continue;
                }

                if (stockName.Contains("국고") == true)
                {
                    continue;
                }

                if (stockName.Contains("단기") == true)
                {
                    continue;
                }


                if (stockName.Contains("선물") == true)
                {
                    continue;
                }

                if (stockName.Contains("나스닥") == true)
                {
                    continue;
                }

                if (stockName.Contains("ARIRANG") == true)
                {
                    continue;
                }

                if (stockName.Contains("HANARO") == true)
                {
                    continue;
                }

                if (stockName.Contains(" ETN") == true)
                {
                    continue;
                }

                if (stockName.Contains("KODEX") == true)
                {
                    continue;
                }

                if (stockName.Contains("QV ") == true)
                {
                    continue;
                }

                if (stockName.Contains("TRUE ") == true)
                {
                    continue;
                }

                if (stockName.Contains("미래에셋 ") == true)
                {
                    continue;
                }

                if (stockName.Contains("삼성 ") == true)
                {
                    continue;
                }

                if (stockName.Contains("신한 ") == true)
                {
                    continue;
                }

                if (stockName.Contains("FOCUS ") == true)
                {
                    continue;
                }

                if (stockName.Contains("SMART ") == true)
                {
                    continue;
                }

                if (stockName.Contains("TREX ") == true)
                {
                    continue;
                }

                if (stockName.Contains("파워 ") == true)
                {
                    continue;
                }

                if (stockName.Contains("흥국 ") == true)
                {
                    continue;
                }

                dgv0.Rows.Add();

                
              
                dgv0.Rows[row].Cells["STOCK_CODE"].Value = dr["STOCK_CODE"].ToString();
                dgv0.Rows[row].Cells["STOCK_NAME"].Value = stockName;

                row = row + 1;
            }
            
        }

        private void BtnStartSync_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(text: "신규종목을 입력하시겠습니까?", caption: "신규종목입력확인", buttons: MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                StoreScode();
            }
            else
            {

            }
        }

        private void StoreScode()
        {
            for (int i = 0; i < dgv0.RowCount - 1; i++)
            {
                if (dgv0.Rows[i].Cells["STOCK_CODE"].Value.ToString().Trim() == "")
                {
                    continue;
                }
                SDataAccess.ArrayParam arrayParam = new ArrayParam();
                SDataAccess.Sql oSql = new SDataAccess.Sql(SDataAccess.ClsServerInfo.VADISSEVER, "RICHDB");

                arrayParam.Clear();
                arrayParam.Add("@ACTION_GB", "A");
                arrayParam.Add("@STOCK_CODE", dgv0.Rows[i].Cells["STOCK_CODE"].Value.ToString().Trim());
                arrayParam.Add("@STOCK_NAME", dgv0.Rows[i].Cells["STOCK_NAME"].Value.ToString().Trim());
                arrayParam.Add("@YBJONG_CODE", "999999");
                arrayParam.Add("@OPT10059_QTY", "");
                arrayParam.Add("@OPT10059_PRICE", "");
                arrayParam.Add("@OPT10081", "");
                arrayParam.Add("@OPT10060_QTY", "");
                arrayParam.Add("@OPT10060_PRICE", "");
                arrayParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                oSql.ExecuteNonQuery("p_ScodeAdd", CommandType.StoredProcedure, arrayParam);

            }

            MessageBox.Show("작업이 완료되었습니다.");

            if (dgv0.DataSource != null)
            {
                dgv0.DataSource = (dgv0.DataSource as DataTable).Clone();
            }

            if (dgv1.DataSource != null)
            {
                dgv1.DataSource = (dgv1.DataSource as DataTable).Clone();
            }

            GetNewStockCode();

        }
    }
}
