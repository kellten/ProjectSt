using SDataAccess;
using System;
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

        public static bool OptCallMagamStoredData(string optCaller, string stockCode, string stdDate, string maxDate, string minDate, string jobIngGb)
        {

            try
            {

            
            string jobDate = DateTime.Now.ToString("yyyyMMdd");
            ArrayParam arrParam = new ArrayParam();
            Sql oSql = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "KIWOOMDB");

            arrParam.Clear();
            arrParam.Add("@ACTION_GB", "A");
            arrParam.Add("@STD_DATE", stdDate);
            arrParam.Add("@STOCK_CODE", stockCode);
            arrParam.Add("@OPT_CALL", optCaller);
            arrParam.Add("@JOB_DATE", jobDate);
            arrParam.Add("@MAX_DATE", maxDate);
            arrParam.Add("@MIN_DATE", minDate);
            arrParam.Add("@JOB_ING_GB", jobIngGb);
            arrParam.Add("@R_ErrorCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

            oSql.ExecuteNonQuery("p_OPTCA_MAGAMAdd", CommandType.StoredProcedure, arrParam);

                if (jobIngGb == "E")
                {
                    // 마감 되어 있는 내역
                    arrParam.Clear();
                    arrParam.Add("@ACTION_GB", "A");
                    arrParam.Add("@STD_DATE", stdDate);
                    arrParam.Add("@STOCK_CODE", stockCode);
                    arrParam.Add("@OPT_CALL", optCaller);
                    arrParam.Add("@JOB_DATE", jobDate);
                    arrParam.Add("@MAX_DATE", maxDate);
                    arrParam.Add("@MIN_DATE", minDate);
                    arrParam.Add("@JOB_ING_GB", jobIngGb);
                    arrParam.Add("@R_ErrorCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                    oSql.ExecuteNonQuery("p_OPTCA_EndMAGAMAdd", CommandType.StoredProcedure, arrParam);

                }

                return true;

            }
            catch (Exception)
            {

                return false;
            }

        }

    }
}
