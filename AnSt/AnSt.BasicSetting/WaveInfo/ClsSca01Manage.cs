using SDataAccess;
using System;
using System.Data;

namespace AnSt.BasicSetting.WaveInfo
{
    internal class ClsSca01Manage
    {
        internal bool SaveSca01(string actionGb, string stockCode, int bigFlow, string startDate, string endDate, string stockInfo, string lowDate, string highDate)
        {
            if (actionGb == "" || stockCode == "" || startDate == "" || endDate == "" || lowDate == "" || highDate == "") { return false; }

            ArrayParam array = new ArrayParam();
            Sql oSql = new SDataAccess.Sql(ClsServerInfo.VADISSEVER, SDataAccess.ClsServerInfo.RICHDB);

            try
            {
                array.Clear();
                array.Add("@ACTION_GB", actionGb);
                array.Add("@STOCK_CODE", stockCode);
                array.Add("@BIG_FLOW", bigFlow);
                array.Add("@START_DATE", startDate);
                array.Add("@END_DATE", endDate);
                array.Add("@STOCK_INFO", stockInfo);
                array.Add("@LOW_DATE", lowDate);
                array.Add("@HIGH_DATE", highDate);
                array.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                oSql.ExecuteNonQuery("p_Sca01Add", CommandType.StoredProcedure, array);

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }


        }
    }
}
