using SDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace AnSt.BasicSetting.Favorite.Class
{
    public class ClsFavFunc
    {

        public bool AddFavList(List<string> list, string sGroupCode)
        {
            try
            {

                foreach (string stockCode in list)
                {

                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
                throw;
            }
        }

        public bool FCodeAdd(string ActionGb, string sGroupcode, string sGroupName, string sGroupInfo)
        {
            try
            {
                SDataAccess.ArrayParam arrayParam = new SDataAccess.ArrayParam();
                SDataAccess.Sql oSql = new SDataAccess.Sql(SDataAccess.ClsServerInfo.VADISSEVER, SDataAccess.ClsServerInfo.RICHDB);
                string sysDate = CDateTime.FormatDate(DateTime.Now.Date.ToString());

                arrayParam.Clear();
                arrayParam.Add("@ACTION_GB", ActionGb);
                arrayParam.Add("@SGROUP_CODE", sGroupcode);
                arrayParam.Add("@SGROUP_NAME", sGroupName);
                arrayParam.Add("@SGROUP_INFO", sGroupInfo);
                arrayParam.Add("@CREATE_DATE", sysDate);
                arrayParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                oSql.ExecuteNonQuery("p_FCodeAdd", CommandType.StoredProcedure, arrayParam);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
                throw;
            }
        }

        public bool Fsa01Add(string sGroupCode, string stockCode)
        {
            try
            {
                if (MessageBox.Show(stockCode +
                                  "을 입력하시겠습니까?", "관심종목 입력", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return false;
                }

                int i = 0;

                i = ReturnFsa01SeqNo(sGroupCode, stockCode);

                if (i == 9999)
                {
                    MessageBox.Show("이미 해당 그룹에 존재하는 종목입니다.");
                    return false;
                }
                else
                {
                    SDataAccess.ArrayParam arrayParam = new SDataAccess.ArrayParam();
                    SDataAccess.Sql oSql = new SDataAccess.Sql(SDataAccess.ClsServerInfo.VADISSEVER, SDataAccess.ClsServerInfo.RICHDB);


                    arrayParam.Add("@ACTION_GB", "A");
                    arrayParam.Add("@SGROUP_CODE", sGroupCode);
                    arrayParam.Add("@STOCK_CODE", stockCode);
                    arrayParam.Add("@SEQ_NO", i);
                    arrayParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                    oSql.ExecuteNonQuery("p_Fsa01Add", CommandType.StoredProcedure, arrayParam);

                    return true;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return false;
                throw;
            }

        }
        public bool Fsa01Del(string sGroupCode, string stockCode)
        {
            try
            {
                if (MessageBox.Show(stockCode +
                                  "을 삭제하시겠습니까?", "관심종목 삭제", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return false;
                }


                SDataAccess.ArrayParam arrayParam = new SDataAccess.ArrayParam();
                SDataAccess.Sql oSql = new SDataAccess.Sql(SDataAccess.ClsServerInfo.VADISSEVER, SDataAccess.ClsServerInfo.RICHDB);


                arrayParam.Add("@ACTION_GB", "D");
                arrayParam.Add("@SGROUP_CODE", sGroupCode);
                arrayParam.Add("@STOCK_CODE", stockCode);
                arrayParam.Add("@SEQ_NO", 0);
                arrayParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                oSql.ExecuteNonQuery("p_Fsa01Add", CommandType.StoredProcedure, arrayParam);

                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return false;
                throw;
            }

        }
        private int ReturnFsa01SeqNo(String sGroupCode, String stockCode)
        {
            SDataAccess.RichQuery oQuery = new SDataAccess.RichQuery();
            DataSet ds = oQuery.p_Fsa01Query("2", sGroupCode, stockCode, "", false);

            if (ds.Tables[0].Rows.Count < 1 || ds == null)
            {
                ds.Reset();
                ds = oQuery.p_Fsa01Query("1", sGroupCode, "", "", false);
                int i = (int)ds.Tables[0].Rows[0]["SEQ_NO"];
                ds.Reset();

                return i;
            }
            else
            {
                ds.Reset();
                return 9999;

            }


        }
    }
}
