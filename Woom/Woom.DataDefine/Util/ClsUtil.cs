using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.ComponentModel;

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
        public double StockRate(double prevPrice, double curPrice)
        {
            double varStockRate = 0.00d;
            // 전일가 - 현재가 / 전일가 * 100 * -1
            varStockRate = (((curPrice - prevPrice) / curPrice) * 100) * -1;
            
           return Math.Round(varStockRate, 2, MidpointRounding.AwayFromZero); 
           

        }

        public string MondayDateOnWeekTypeString(string valueDate)
        {
            string reDate = "";

            reDate = Mid(valueDate, 1, 4) + "-" + Mid(valueDate, 5, 2) + "-" + Mid(valueDate, 7, 2);

            DateTime dtDate =  Convert.ToDateTime( reDate);

           reDate = dtDate.AddDays(Convert.ToInt32(DayOfWeek.Monday) - Convert.ToInt32(dtDate.DayOfWeek)).ToString("yyyyMMdd");

            return reDate;

        }

        public DateTime StringToDateTime(string valueDate)
        {
            string reDate = "";

            reDate = Mid(valueDate, 1, 4) + "-" + Mid(valueDate, 5, 2) + "-" + Mid(valueDate, 7, 2);

            DateTime dtDate = Convert.ToDateTime(reDate);

            return dtDate;

        }

        public DateTime MondayDateOnWeekTypeDateTime(DateTime valueDate)
        {
            return valueDate.AddDays(Convert.ToInt32(DayOfWeek.Monday) - Convert.ToInt32(valueDate.DayOfWeek));
            
        }

        //Mid
        /// <summary>
        /// 문자열 원본의 지정한 위치에서 부터 추출할 갯수 만큼 문자열을 가져옵니다.
        /// </summary>
        /// <param name="sString">문자열 원본</param>
        /// <param name="nStart">추출을 시작할 위치</param>
        /// <param name="nLength">추출할 갯수</param>
        /// <returns>추출된 문자열</returns>
        public string Mid(string sString, int nStart, int nLength)
        {
            string sReturn;

            //VB에서 문자열의 시작은 0이 아니므로 같은 처리를 하려면 
            //스타트 위치를 인덱스로 바꿔야 하므로 -1을 하여
            //1부터 시작하면 0부터 시작하도록 변경하여 준다.
            --nStart;

            //시작위치가 데이터의 범위를 안넘겼는지?
            if (nStart <= sString.Length)
            {
                //안넘겼다.

                //필요한 부분이 데이터를 넘겼는지?
                if ((nStart + nLength) <= sString.Length)
                {
                    //안넘겼다.
                    sReturn = sString.Substring(nStart, nLength);
                }
                else
                {
                    //넘겼다.

                    //데이터 끝까지 출력
                    sReturn = sString.Substring(nStart);
                }

            }
            else
            {
                //넘겼다.

                //그렇다는 것은 데이터가 없음을 의미한다.
                sReturn = string.Empty;
            }

            return sReturn;
        }

        //Left
        /// <summary>
        /// 문자열 원본에서 왼쪽에서 부터 추출한 갯수만큼 문자열을 가져옵니다.
        /// </summary>
        /// <param name="sString">문자열 원본</param>
        /// <param name="nLength">추출할 갯수</param>
        /// <returns>추출된 문자열</returns>
        public string Left(string sString, int nLength)
        {
            string sReturn;

            //추출할 갯수가 문자열 길이보다 긴지?
            if (nLength > sString.Length)
            {
                //길다!

                //길다면 원본의 길이만큼 리턴해 준다.
                nLength = sString.Length;
            }

            //문자열 추출
            sReturn = sString.Substring(0, nLength);

            return sReturn;
        }
        //Right
        /// <summary>
        /// 문자열 원본에서 오른쪽에서 부터 추출한 갯수만큼 문자열을 가져옵니다.
        /// </summary>
        /// <param name="sString">문자열 원본</param>
        /// <param name="nLength">추출할 갯수</param>
        /// <returns>추출된 문자열</returns>
        public string Right(string sString, int nLength)
        {
            string sReturn;

            //추출할 갯수가 문자열 길이보다 긴지?
            if (nLength > sString.Length)
            {
                //길다!

                //길다면 원본의 길이만큼 리턴해 준다.
                nLength = sString.Length;
            }

            //문자열 추출
            sReturn = sString.Substring(sString.Length - nLength, nLength);

            return sReturn;
        }

        /// <summary>
        /// Datagridview를 Datatable로 변환(https://www.codeproject.com/Questions/649440/How-can-I-get-DataTable-from-DataGridView)
        /// </summary>
        /// <param name="_DataGridView"></param>
        /// <returns></returns>
        public static DataTable GetDataGridViewAsDataTable(DataGridView _DataGridView)
        {
            try
            {
                if (_DataGridView.ColumnCount == 0) return null;
                DataTable dtSource = new DataTable();
                //////create columns
                foreach (DataGridViewColumn col in _DataGridView.Columns)
                {
                    if (col.ValueType == null)
                        dtSource.Columns.Add(col.Name, typeof(string));
                    else
                        dtSource.Columns.Add(col.Name, col.ValueType);
                    dtSource.Columns[col.Name].Caption = col.HeaderText;
                }
                ///////insert row data
                foreach (DataGridViewRow row in _DataGridView.Rows)
                {
                    DataRow drNewRow = dtSource.NewRow();
                    foreach (DataColumn col in dtSource.Columns)
                    {
                        drNewRow[col.ColumnName] = row.Cells[col.ColumnName].Value;
                    }
                    dtSource.Rows.Add(drNewRow);
                }
                return dtSource;
            }
            catch { return null; }
        }

        /// <summary>
        /// Datagridview를 Datatable로 변환(https://www.codeproject.com/Questions/649440/How-can-I-get-DataTable-from-DataGridView)
        /// </summary>
        /// <param name="_DataGridView"></param>
        /// <returns></returns>
        public static DataTable GetDataGridViewAsDataTableColumName(DataGridView _DataGridView)
        {
            try
            {
                if (_DataGridView.ColumnCount == 0) return null;
                DataTable dtSource = new DataTable();
                //////create columns
                foreach (DataGridViewColumn col in _DataGridView.Columns)
                {
                    if (col.ValueType == null)
                        dtSource.Columns.Add(col.HeaderText, typeof(string));
                    else
                        dtSource.Columns.Add(col.HeaderText, col.ValueType);
                    dtSource.Columns[col.HeaderText].Caption = col.Name.ToString();
                }
                ///////insert row data
                foreach (DataGridViewRow row in _DataGridView.Rows)
                {
                    DataRow drNewRow = dtSource.NewRow();
                    foreach (DataColumn col in dtSource.Columns)
                    {
                        drNewRow[col.ColumnName] = row.Cells[col.Caption].Value;
                    }
                    dtSource.Rows.Add(drNewRow);
                }
                return dtSource;
            }
            catch { return null; }
        }
        /// <summary>
        /// Datagridview를 Datatable로 변환(https://www.codeproject.com/Questions/649440/How-can-I-get-DataTable-from-DataGridView)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(IList<T> list)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in list)
            {
                for (int i = 0; i < values.Length; i++)
                    values[i] = props[i].GetValue(item) ?? DBNull.Value;
                table.Rows.Add(values);
            }
            return table;
        }
    }
}
