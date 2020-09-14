using System;
using System.Data;  
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CybosDa.DataAccess.Plugin.ClsCybosDa;
using System.Runtime.Remoting.Messaging;

namespace CybosDa.DataAccess.Connection
{
    public class clsCybosConnection : CPUTILLib._ICpCybosEvents
    {
        public bool CybosConnection()
        {
            try
            {

                S_CpCybos.OnDisconnect += OnDisconnect;

                if (S_CpCybos.IsConnect == 1)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
                throw;
            }
            

            return false;
        }

        public DataTable LoadStockCode()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SEQ_NO", typeof(Int32));
            dt.Columns.Add("DA_STOCK_CODE", typeof(string));
            dt.Columns.Add("STOCK_NAME", typeof(string));
            dt.Columns.Add("STOCK_CODE", typeof(string));

            DataRow dr;
            for (int i = 0; i < S_CpStockCode.GetCount() - 1; i++)
            {
                dr = dt.NewRow();

                dr["SEQ_NO"] = i;
                dr["DA_STOCK_CODE"] = Convert.ToString(S_CpStockCode.GetData(0, (short)i));
                dr["STOCK_NAME"] = Convert.ToString(S_CpStockCode.GetData(1, (short)i));
                string stockCode = Convert.ToString(S_CpStockCode.GetData(0, (short)i));
                dr["STOCK_CODE"] = stockCode.Substring(1, stockCode.Trim().Length - 1);

                dt.Rows.Add(dr);
            }

            return dt;

        }
        public void OnDisconnect()
        {
            
        }

    }
}
