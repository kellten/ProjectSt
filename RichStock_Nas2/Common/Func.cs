using PaikRichStock.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Common
{
    public class Func
    {
        public  DataTable GetDartApi(string stockCode, string stockDate)
        {
            DataSet ds;
            string startDate;
            string endDate;
            using (DataAccess da = new DataAccess())
            {
                ds = da.p_stock_day_data_query("7", stockCode, stockDate, false);
            }

            if (ds.Tables[0].Rows.Count < 1) return null;
            startDate = ds.Tables[0].Rows[0]["STOCK_DATE"].ToString();
            if (ds.Tables[1].Rows.Count < 1)
            {
                endDate = startDate;
            }
            else
            {
                endDate = ds.Tables[1].Rows[0]["STOCK_DATE"].ToString();
            }

            ds = Cls.Dart(stockCode, startDate, endDate);
            if (ds.Tables.Count == 1)
            {
                return new DataTable("DART");
            }
            ds.Tables[1].TableName = "DART";
            return ds.Tables[1].Copy();
        }
        public  DataTable GetCredit(string stockCode, string stockDate)
        {
            DataSet ds;
            using (DataAccess da = new DataAccess())
            {
                ds = da.p_stock_credit_query("2", stockCode, stockDate, false);
            }
            ds.Tables[0].TableName = "CREDIT";
            return ds.Tables[0].Copy();
        }
        public  DataTable GetBuySell(string stockCode, string stockDate)
        {
            DataSet ds;
            using (DataAccess da = new DataAccess())
            {
                ds = da.p_stock_buysell_state_query("2", stockCode, stockDate, false, null);
            }
            ds.Tables[0].TableName = "BUYSELL";
            return ds.Tables[0].Copy();
        }
    }
}
