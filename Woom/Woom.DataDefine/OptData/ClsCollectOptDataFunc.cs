using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SDataAccess;

namespace Woom.DataDefine.OptData
{
   public class ClsCollectOptDataFunc
    {

        public enum OptPeriod
        { 
           END_NOT_EXISTS, FULL, EMPTY
        }
        public OptPeriod CheckValidationOptPeriod(string stockCode, string optCall)
        {
            KiwoomQuery kiwoom = new KiwoomQuery();
            DataTable dt = new DataTable();
            string strToday = DateTime.Now.ToString("yyyyMMdd");

            dt = kiwoom.p_OptCaEndMagamStockCodeQuery(query: "1", stdDate: "", stockCode: stockCode, optcall: optCall, jobDate: "", jobIngGb: "", bln3tier: false).Tables[0].Copy();

            if (dt == null)
            { return OptPeriod.END_NOT_EXISTS; }
            else
            {
                
                    if (Convert.ToInt32(dt.Rows[0]["CHAIN_MAX_DATE"].ToString().Trim()) >= Convert.ToInt32(AvailableTradingDate()))
                    {
                        return OptPeriod.FULL;
                    }
                    else
                    {
                        return OptPeriod.EMPTY;
                    }
            }
        }

        public string AvailableTradingDate()
        {
            RichQuery rich = new RichQuery();
            DataTable dt = new DataTable();

            dt = rich.p_TDATEQuery(query: "2", tradeDate: "", fromDate: "", toDate:"", bln3tier:false).Tables[0].Copy();

            return dt.Rows[0]["MAX_TRADE_DATE"].ToString().Trim();

        }

        public string GetAvailableDate()
        {
            string stDate = "";

            if (System.DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                stDate = DateTime.Today.AddDays(-1).ToString("yyyyMMdd");
            }
            else if (System.DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                stDate = DateTime.Today.AddDays(-2).ToString("yyyyMMdd");
            }
            else
            {
                int i = Int32.Parse(System.DateTime.Now.ToString("HH") + System.DateTime.Now.ToString("ss"));

                if (i > 1600)
                { stDate = CDateTime.FormatDate(System.DateTime.Now.Date.ToShortDateString()); }
                else if (System.DateTime.Now.DayOfWeek == DayOfWeek.Monday)
                {
                    stDate = DateTime.Today.AddDays(-3).ToString("yyyyMMdd");
                }
                else
                {
                    stDate = DateTime.Today.AddDays(-1).ToString("yyyyMMdd");
                }
            }

            return stDate;
        }


    }
}
