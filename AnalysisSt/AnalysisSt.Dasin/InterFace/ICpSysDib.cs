using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPSYSDIBLib;

namespace AnalysisSt.Dasin.InterFaceICpSysDib
{
    public interface ICpSysDib
    {
      void CpSvr7254_SetInputValue(String stockCode, Int16 GiganSunTakGb, long fromDate, long toDate, Int16 trader);
      void CpSvr7254_OnReceived();
    }
}
