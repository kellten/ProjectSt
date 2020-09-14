using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisSt.TechnicalFunc.TradeAnalysis
{
    public class clsBaseFunc
    {
        public Decimal PercentCal(Decimal source, Decimal target)
        {
            if (source == 0)
            {
                return 0;
            }

            if (source < 0)
            {
                return -1;
            }

            Decimal per = 0;

            per = (target / source) * 100;
            return Decimal.Round(per, 2);
        }
    }
}
