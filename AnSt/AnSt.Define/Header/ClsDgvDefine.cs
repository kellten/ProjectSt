using AnSt.Define.SetData;
using System;
using System.Windows.Forms;

namespace AnSt.Define.Header
{
    public class ClsDgvDefine
    {
        public bool FCode(ref DataGridView dgv)
        {
            try
            {
                if (InitSetting(ref dgv) == false) { return false; }
                dgv.ColumnCount = 3;
                dgv.Columns[0].Name = "SGROUP_NAME";
                dgv.Columns[0].ValueType = typeof(string);
                dgv.Columns[0].HeaderText = "그룹명";
                dgv.Columns[0].Width = 100;
                dgv.Columns[1].Name = "SGROUP_INFO";
                dgv.Columns[1].ValueType = typeof(string);
                dgv.Columns[1].HeaderText = "정보";
                dgv.Columns[2].Name = "SGROUP_CODE";
                dgv.Columns[2].ValueType = typeof(string);
                dgv.Columns[2].HeaderText = "코드";
                dgv.Columns[2].Visible = false;

                ClsDgvSetData clsDgvSetData = new ClsDgvSetData();
                clsDgvSetData.GetFCode(ref dgv);
                HeaderCenter(ref dgv);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
                throw;
            }
        }
        public bool Fsa01(ref DataGridView dgv)
        {

            try
            {
                if (InitSetting(ref dgv) == false) { return false; }
                dgv.ColumnCount = 6;
                dgv.Columns[0].Name = "STOCK_NAME";
                dgv.Columns[0].ValueType = typeof(string);
                dgv.Columns[0].HeaderText = "종목명";
                dgv.Columns[1].Name = "YBJONG_NAME";
                dgv.Columns[1].ValueType = typeof(string);
                dgv.Columns[1].HeaderText = "업종명";
                dgv.Columns[2].Name = "SGROUP_NAME";
                dgv.Columns[2].ValueType = typeof(string);
                dgv.Columns[2].HeaderText = "그룹명";
                dgv.Columns[3].Name = "SEQ_NO";
                dgv.Columns[3].ValueType = typeof(string);
                dgv.Columns[3].HeaderText = "순서";
                dgv.Columns[3].Visible = false;
                dgv.Columns[4].Name = "SGROUP_CODE";
                dgv.Columns[4].ValueType = typeof(string);
                dgv.Columns[4].HeaderText = "그룹코드";
                dgv.Columns[4].Visible = false;
                dgv.Columns[5].Name = "STOCK_CODE";
                dgv.Columns[5].ValueType = typeof(string);
                dgv.Columns[5].HeaderText = "코드";
                dgv.Columns[5].Visible = false;

                HeaderCenter(ref dgv);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
                throw;
            }
        }
        public bool Sca01(ref DataGridView dgv)
        {
            try
            {
                if (InitSetting(ref dgv) == false) { return false; }
                dgv.ColumnCount = 8;
                dgv.Columns[0].Name = "BIG_FLOW";
                dgv.Columns[0].ValueType = typeof(int);
                dgv.Columns[0].HeaderText = "BF";
                dgv.Columns[1].Name = "START_DATE";
                dgv.Columns[1].ValueType = typeof(string);
                dgv.Columns[1].HeaderText = "시작일";
                dgv.Columns[2].Name = "END_DATE";
                dgv.Columns[2].ValueType = typeof(string);
                dgv.Columns[2].HeaderText = "종료일";
                dgv.Columns[3].Name = "LOW_DATE";
                dgv.Columns[3].ValueType = typeof(string);
                dgv.Columns[3].HeaderText = "최저점일";
                dgv.Columns[4].Name = "HIGH_DATE";
                dgv.Columns[4].ValueType = typeof(string);
                dgv.Columns[4].HeaderText = "최고점일";
                dgv.Columns[5].Name = "LOW_PRICE";
                dgv.Columns[5].ValueType = typeof(int);
                dgv.Columns[5].HeaderText = "최저점";
                dgv.Columns[6].Name = "HIGH_PRICE";
                dgv.Columns[6].ValueType = typeof(int);
                dgv.Columns[6].HeaderText = "최고점";
                dgv.Columns[7].Name = "STOCK_INFO";
                dgv.Columns[7].ValueType = typeof(string);
                dgv.Columns[7].HeaderText = "정보";

                HeaderCenter(ref dgv);

                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
                throw;
            }
        }
        public bool StockListDB(ref DataGridView dgv)
        {
            try
            {
                if (InitSetting(ref dgv) == false) { return false; }

                ClsDgvSetData clsDgvSetData = new ClsDgvSetData();
                clsDgvSetData.GetAllStockDB(ref dgv);

                //dgv.ColumnCount = 2;
                // dgv.Columns[0].Name = "STOCK_NAME";
                dgv.Columns[0].ValueType = typeof(string);
                dgv.Columns[0].HeaderText = "종목명";
                // dgv.Columns[1].Name = "STOCK_CODE";
                dgv.Columns[1].ValueType = typeof(string);
                dgv.Columns[1].HeaderText = "코드";

                HeaderCenter(ref dgv);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
                throw;
            }
        }
        public bool StockListApi(ref DataGridView dgv)
        {
            try
            {
                if (InitSetting(ref dgv) == false) { return false; }

                dgv.ColumnCount = 3;
                dgv.Columns[0].Name = "STOCK_NAME";
                dgv.Columns[0].ValueType = typeof(string);
                dgv.Columns[0].HeaderText = "종목명";
                dgv.Columns[1].Name = "STOCK_CODE";
                dgv.Columns[1].ValueType = typeof(string);
                dgv.Columns[1].HeaderText = "코드";
                dgv.Columns[2].Name = "DA_STOCK_CODE";
                dgv.Columns[2].ValueType = typeof(string);
                dgv.Columns[2].HeaderText = "코드";

                HeaderCenter(ref dgv);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
                throw;
            }
        }
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

    }
}
