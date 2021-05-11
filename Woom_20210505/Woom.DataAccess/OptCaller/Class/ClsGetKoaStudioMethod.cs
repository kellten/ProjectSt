using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woom.DataAccess.PlugIn;
using System.Data;

namespace Woom.DataAccess.OptCaller.Class
{
    public class ClsGetKoaStudioMethod
    {

        /// <summary>
        /// 주식 시장별 종목코드 리스트를 ';'로 구분해서 전달합니다. 
        /// 시장구분값을 ""공백으로하면 전체시장 코드리스트를 전달합니다.
        /// </summary>
        /// <param name="stockGb">0 : 코스피  10 : 코스닥 3 : ELW  8 : ETF       50 : KONEX      4 :  뮤추얼펀드  5 : 신주인수권       6 : 리츠  9 : 하이얼펀드   30 : K-OTC </param>
        /// <returns></returns>
        public string GetCodeListByMarket(string stockGb)
        {
            string CodeList;

            CodeList = ClsAxKH.AxKH.GetCodeListByMarket(stockGb);

            return CodeList;
        }

        /// <summary>
        /// 주식 시장별 종목코드 리스트를 ';'로 구분해서 전달합니다. 
        /// 시장구분값을 ""공백으로하면 전체시장 코드리스트를 전달합니다.
        /// </summary>
        /// <param name="stockGb">0 : 코스피  10 : 코스닥 3 : ELW  8 : ETF       50 : KONEX      4 :  뮤추얼펀드  5 : 신주인수권       6 : 리츠  9 : 하이얼펀드   30 : K-OTC </param>
        /// <returns></returns>
        public DataTable GetCodeListByMarketCallBackDataTable(string stockGb)
        {
            DataTable Dt = new DataTable();
            DataRow dr;
            string CodeList;
            string[] ArrayStockCode;

            Dt.Columns.Add("STOCK_CODE", Type.GetType("String"));

            CodeList = ClsAxKH.AxKH.GetCodeListByMarket(stockGb);

            if (CodeList == "")
            {
                return null;
            }
            else
            {
                ArrayStockCode = CodeList.Split(';');
                foreach (string stockCode in ArrayStockCode)
                {
                    dr = Dt.NewRow();
                    dr["STOCK_CODE"] = stockCode;

                    Dt.Rows.Add(dr);
                }

            }

            return Dt;
        }
    }
}
