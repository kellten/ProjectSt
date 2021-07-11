using System;
using System.Windows.Forms;
using System.Data;

namespace Woom.DataDefine.Util
{
    public class ClsDataGridViewUtil
    {
        public bool HeaderCenter(ref DataGridView dgv)
        {
            try
            {
                for (int i = 0; i < dgv.ColumnCount; i++)
                {
                    dgv.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
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
        public bool InitSetting(ref DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                dgv.Columns.Clear();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
                throw;
            }

        }

        public void RemoveGridViewRow(DataGridView dgv)
        {
            do
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    try
                    {
                        dgv.Rows.Remove(row);
                    }
                    catch (Exception) { }
                }
            } while (dgv.Rows.Count > 1);

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
