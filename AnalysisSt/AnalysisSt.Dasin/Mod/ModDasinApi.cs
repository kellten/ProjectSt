using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPUTILLib;
using DSCBO1Lib;
using CPSYSDIBLib;

namespace AnalysisSt.Dasin.ModDasin
{
   public class ModDasinApi
    {
        public StockMst _stockMst = new StockMst();
        public  CPSYSDIBLib.CpSvr7254Class _cpSvr7254 = new CpSvr7254Class();
        public CPUTILLib.CpStockCodeClass _CpStockCodeClass = new CpStockCodeClass();
        public DataSet _ds = new DataSet();
                           
        public void MainRegEvent()
        {
             GetAllStockCode();
        }

        public void GetAllStockCode()
        {
            int i = 0;
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("STOCK_CODE", typeof(String));
            dt.Columns.Add("STOCK_NAME", typeof(String));

            i = _CpStockCodeClass.GetCount();

            for (Int16 j = 0; j < i; j++)
            {
                dr = dt.NewRow();
                dr["STOCK_CODE"] = _CpStockCodeClass.GetData(0, j);
                dr["STOCK_NAME"] = _CpStockCodeClass.GetData(1, j);

                dt.Rows.Add(dr);
            }

            _ds.Tables.Add(dt);

            //funcCpSvr7254(_ds.Tables[0].Rows[0]["STOCK_CODE"].ToString());

        }

    }
}
