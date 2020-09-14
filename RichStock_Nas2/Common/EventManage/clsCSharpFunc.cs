using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CSharp.Common.EventManage
{
    class clsCSharpFunc
    {
        private  DataTable _dtSource =  new DataTable();

        public DataTable SourceDt
        {
            set {
                _dtSource = null;
                _dtSource = new DataTable();
                _dtSource = value.Copy();
                }           
        }
                    
        public IEnumerable<DataRow> YieldFSourceData()
        {
            foreach (DataRow dr in _dtSource.Rows)
            {
                if (dr["STOCK_CODE"].ToString().Trim() != "")
                {
                    yield return dr;
                }
            }
        }

        public void DataSetColumnCloneToDataSet(DataSet destDataSet, DataSet sourceDataSet)
        {
            try
            {
                for (int i = 0; i < sourceDataSet.Tables[0].Rows.Count; i++)
                {
                    destDataSet.Tables[0].Columns.Add(sourceDataSet.Tables[0].Columns[i].ColumnName, sourceDataSet.Tables[0].Columns[i].DataType);
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public string DayDateAdd(string sourceDate, int value)
        {
            return DateTime.ParseExact(sourceDate, "yyyyMMdd", null).AddDays(value).ToString("yyyyMMdd");
        }
        
    }
}
