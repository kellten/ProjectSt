using SDataAccess;
using System;
using System.Data;
using System.Windows.Forms.DataVisualization.Charting;

namespace AnSt.Chart.SetSeriesData
{
    public class ClsPriceSetSeriesData
    {
        public void PriceSetSeriesData(ref Series se, DataTable dt)
        {
            if (se == null) { return; }

            Double[] arrP = new Double[1];

            int pt = 0;
            DataView dv = new DataView(dt);
            int high = 0;
            int low = 0;

            dv.Sort = "STOCK_DATE asc";

            foreach (DataRowView dr in dv)
            {
                if (high == 0)
                {
                    high = int.Parse(dr["HIGH_PRICE"].ToString());
                }
                else
                {
                    if (high < int.Parse(dr["HIGH_PRICE"].ToString()))
                    {
                        high = int.Parse(dr["HIGH_PRICE"].ToString());
                    }
                }

                if (low == 0)
                {
                    low = int.Parse(dr["LOW_PRICE"].ToString());
                }
                else
                {
                    if (low > int.Parse(dr["LOW_PRICE"].ToString()))
                    {
                        low = int.Parse(dr["LOW_PRICE"].ToString());
                    }
                }

                se.Points.AddXY((object)dr["STOCK_DATE"], int.Parse(dr["HIGH_PRICE"].ToString()));
                se.Points[pt].YValues[1] = int.Parse(dr["LOW_PRICE"].ToString());
                se.Points[pt].YValues[2] = int.Parse(dr["START_PRICE"].ToString());
                se.Points[pt].YValues[3] = int.Parse(dr["NOW_PRICE"].ToString());

                if (int.Parse(dr["START_PRICE"].ToString()) > int.Parse(dr["NOW_PRICE"].ToString()))
                {
                    se.Points[pt].Color = System.Drawing.Color.Blue;
                }
                else
                {
                    se.Points[pt].Color = System.Drawing.Color.Red;
                }

                pt++;
            }
        }

        public DataTable GetOpt10081(string query, string stockCode, string fromDate, string toDate)
        {
            KiwoomQuery kiwoomQuery = new KiwoomQuery();
            DataTable dt = new DataTable();

            try
            {
                dt = kiwoomQuery.p_Opt10081Query(query, stockCode, fromDate, toDate, false).Tables[0].Copy();
                return dt;
            }
            catch (Exception)
            {
                return null;
                throw;
            }

        }
    }
}
