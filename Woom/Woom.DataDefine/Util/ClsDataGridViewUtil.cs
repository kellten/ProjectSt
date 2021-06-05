using System;
using System.Windows.Forms;

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

    }
}
