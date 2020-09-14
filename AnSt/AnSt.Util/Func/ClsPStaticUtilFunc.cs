using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace AnSt.Util.Func
{
    public static class ClsPStaticUtilFunc
    {

        /// <summary>
        /// 문자열 왼쪽편처음부터 지정된 문자열값 리턴(VBScript Left기능)
        /// </summary>
        /// <param name="target">얻을 문자열</param>
        /// <param name="length">얻을 문자열길이</param>
        /// <returns>얻은 문자열 값</returns>
        public static string Left(string target, int length)
        {
            if (length <= target.Length)
            {
                return target.Substring(0, length);
            }
            return target;
        }

        /// <summary>
        /// 지정된 위치이후 모든 문자열 리턴 (VBScript Mid기능)
        /// </summary>
        /// <param name="target">얻을 문자열</param>
        /// <param name="start">얻을 시작위치</param>
        /// <returns>지정된 위치 이후 모든 문자열리턴</returns>
        public static string Mid(string target, int start)
        {
            if (start <= target.Length)
            {
                return target.Substring(start - 1);
            }
            return string.Empty;
        }
        /// <summary>
        /// 문자열이 지정된 위치에서 지정된 길이만큼까지의 문자열 리턴 (VBScript Mid기능)
        /// </summary>
        /// <param name="target">얻을 문자열</param>
        /// <param name="start">얻을 시작위치</param>
        /// <param name="length">얻을 문자열길이</param>
        /// <returns>지정된 길이만큼의 문자열 리턴</returns>
        public static string Mid(string target, int start, int length)
        {
            if (start <= target.Length)
            {
                if (start + length - 1 <= target.Length)
                {
                    return target.Substring(start - 1, length);
                }
                return target.Substring(start - 1);
            }
            return string.Empty;
        }

        /// <summary>
        /// 문자열 오른쪽편처음부터 지정된 문자열값 리턴(VBScript Right기능) 
        /// </summary>
        /// <param name="target">얻을 문자열</param>
        /// <param name="length">얻을 문자열길이</param>
        /// <returns>얻은 문자열 값</returns>
        public static string Right(string target, int length)
        {
            if (length <= target.Length)
            {
                return target.Substring(target.Length - length);
            }
            return target;
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
    }
}
