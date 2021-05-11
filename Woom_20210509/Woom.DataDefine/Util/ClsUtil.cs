using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woom.DataDefine.Util
{
    public class ClsUtil
    {

        /// <summary>
        /// 상승률 계산
        /// </summary>
        /// <param name="prevPrice">이전일자가격</param>
        /// <param name="curPrice">현재가 가격</param>
        /// <returns></returns>
        public double StockRate(int prevPrice, int curPrice)
        {
            double varStockRate = 0.00d;
            // 전일가 - 현재가 / 전일가 * 100 * -1
            varStockRate = (((curPrice - prevPrice) / curPrice) * 100) * -1;
            
                return Math.Round(varStockRate, 2, MidpointRounding.AwayFromZero); 
           

        }

    }
}
