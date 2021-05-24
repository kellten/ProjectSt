using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDataAccess;
using Woom.DataAccess.OptCaller.Class;
using Woom.DataAccess.PlugIn;
using System.Data;

namespace Woom.DataAccess.Logger
{
    public static class ClsDbLogger
    {

        public enum LoggerGb { SendLoger, RecieveLoger, ErrorLoger }
        public static void StoreLogger(LoggerGb loggergb, string optCallNo, string transText)
        {

            try
            {

            ArrayParam arrParam = new ArrayParam();
            Sql oSql = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "KIWOOMDB");

            arrParam.Clear();
            arrParam.Add("@ACTION_GB", "A");
            arrParam.Add("@LOG_SEQ_NO", 0);
            switch (loggergb)
            {
                case LoggerGb.SendLoger:
                    arrParam.Add("@TRANS_GB", "1");
                    break;
                case LoggerGb.RecieveLoger:
                    arrParam.Add("@TRANS_GB", "2");
                    break;
                case LoggerGb.ErrorLoger:
                    arrParam.Add("@TRANS_GB", "3");
                    break;
                default:
                    arrParam.Add("@TRANS_GB", "");
                    break;
            }

            arrParam.Add("@OPT_CALL_NO", optCallNo);
            arrParam.Add("@TRANS_TEXT", transText);
            arrParam.Add("@DEUNG_DATE", "");
            arrParam.Add("@DEUNG_TIME", "");
            arrParam.Add("@WORK_COM", System.Windows.Forms.SystemInformation.ComputerName);
            arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

            oSql.ExecuteNonQuery("p_OPTCA_LOGAdd", CommandType.StoredProcedure, arrParam);

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
