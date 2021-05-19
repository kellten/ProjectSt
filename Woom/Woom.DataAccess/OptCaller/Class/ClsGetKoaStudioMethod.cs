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
        /// <param name="stockGb">0 : 코스피  10 : 코스닥 3 : ELW  8 : ETF       50 : KONEX      4 :  뮤추얼펀드  5 : 신주인수권       6 : 리츠  9 : 하이얼펀드   30 : K-OTC  999 : ALL(코스피, 코스닥) </param>
        /// <returns></returns>
        public DataTable GetCodeListByMarketCallBackDataTable(string stockGb)
        {
            DataTable Dt = new DataTable();
            DataRow dr;
            string CodeList;
            string[] ArrayStockCode;

            Dt.Columns.Add("STOCK_CODE", Type.GetType("System.String"));
            Dt.Columns.Add("STOCK_NAME", Type.GetType("System.String"));

            if (stockGb != "999")
            {
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
                        dr["STOCK_NAME"] = ClsAxKH.GetMasterCodeName(stockCode);

                        Dt.Rows.Add(dr);
                    }
                }
            }
            else
            {
                CodeList = ClsAxKH.AxKH.GetCodeListByMarket("0");

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
                        dr["STOCK_NAME"] = ClsAxKH.GetMasterCodeName(stockCode);

                        Dt.Rows.Add(dr);
                    }
                }

                CodeList = "";

                CodeList = ClsAxKH.AxKH.GetCodeListByMarket("10");

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
                        dr["STOCK_NAME"] = ClsAxKH.GetMasterCodeName(stockCode);

                        Dt.Rows.Add(dr);
                    }
                }

            }
                       

            return Dt;
        }
        /// <summary>
        /// 입력한 종목코드에 해당하는 종목 상장주식수를 전달합니다.
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        public int GetMasterListedStockCnt(string stockCode)
        {
            int stockCount = 0;

            stockCount = ClsAxKH.AxKH.GetMasterListedStockCnt(stockCode);

            return stockCount;
        }
        /// <summary>
        /// 입력한 종목코드에 해당하는 종목의 감리구분을 전달합니다.
        /// (정상, 투자주의, 투자경고, 투자위험, 투자주의환기종목)
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        public string GetMasterConstruction(string stockCode)
        {
            string stockConstruction = "";

            stockConstruction = ClsAxKH.AxKH.GetMasterConstruction(stockCode);

            return stockConstruction;
        }
        /// <summary>
        ///  입력한 종목의 전일종가를 전달합니다.
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        public string GetMasterLastPrice(string stockCode)
        {
            if (stockCode == "")
            {
                return "";
            }
            string lastPrice = "";

            lastPrice = ClsAxKH.AxKH.GetMasterLastPrice(stockCode);
            
            return String.Format("{0:0,0}",Convert.ToInt32(lastPrice));
        }
        /// <summary>
        /// 입력한 종목의 증거금 비율, 거래정지, 관리종목, 감리종목, 투자융의종목, 담보대출, 액면분할, 신용가능 여부를 전달합니다.
        /// </summary>
        /// <param name="stockCode"></param> 
        /// <returns></returns>
        public string GetMasterStockState(string stockCode)
        {

            string StockState = "";

            StockState = ClsAxKH.AxKH.GetMasterLastPrice(stockCode);

            return StockState;
        }


    }
}
