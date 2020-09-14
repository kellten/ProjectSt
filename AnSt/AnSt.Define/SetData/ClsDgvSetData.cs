using SDataAccess;
using System;
using System.Data;
using System.Windows.Forms;

namespace AnSt.Define.SetData
{
    public class ClsDgvSetData
    {
        public void GetFCode(ref DataGridView dgv)
        {
            DataTable dt = new DataTable();
            RichQuery richQuery = new RichQuery();
            int i = 0;

            dgv.Rows.Clear();

            try
            {

                dt = richQuery.p_FCodeQuery("2", "", "", "", false).Tables[0].Copy();

                foreach (DataRow dr in dt.Rows)
                {
                    dgv.Rows.Add();
                    dgv.Rows[i].Cells["SGROUP_CODE"].Value = dr["SGROUP_CODE"].ToString();
                    dgv.Rows[i].Cells["SGROUP_NAME"].Value = dr["SGROUP_NAME"].ToString();
                    dgv.Rows[i].Cells["SGROUP_INFO"].Value = dr["SGROUP_INFO"].ToString();

                    i = i + 1;
                }

                dt = null;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.AutoResizeColumns();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                dt = null;
                throw;
            }

        }
        public void GetFsa01Data(ref DataGridView dgv, string sGroupCode)
        {
            DataTable dt = new DataTable();
            RichQuery richQuery = new RichQuery();
            int i = 0;

            dgv.Rows.Clear();

            try
            {
                dt = richQuery.p_FCodeQuery("3", sGroupCode, "", "", false).Tables[0].Copy();

                if (dt == null) { return; }
                if (dt.Rows.Count < 1) { return; }

                foreach (DataRow dr in dt.Rows)
                {
                    dgv.Rows.Add();
                    dgv.Rows[i].Cells["STOCK_CODE"].Value = dr["STOCK_CODE"];
                    dgv.Rows[i].Cells["STOCK_NAME"].Value = dr["STOCK_NAME"];
                    dgv.Rows[i].Cells["YBJONG_NAME"].Value = dr["YBJONG_NAME"];
                    dgv.Rows[i].Cells["SEQ_NO"].Value = dr["SEQ_NO"];
                    dgv.Rows[i].Cells["SGROUP_CODE"].Value = dr["SGROUP_CODE"];
                    dgv.Rows[i].Cells["SGROUP_NAME"].Value = dr["SGROUP_NAME"];

                    i = i + 1;
                }
                dgv.SuspendLayout();
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.AutoResizeColumns();

                dt = null;
            }
            catch (Exception ex)
            {
                dt = null;
                MessageBox.Show(ex.ToString());
                throw;
            }
        }
        public void GetAllStockDB(ref DataGridView dgv)
        {
            DataTable dt = new DataTable();
            RichQuery richQuery = new RichQuery();
            // int i = 0;

            try
            {


                dgv.Rows.Clear();

                dt = richQuery.p_ScodeQuery("2", "", "", false).Tables[0].Copy();

                //foreach (DataRow dr in dt.Rows)
                //{
                //    dgv.Rows.Add();
                //    dgv.Rows[i].Cells["STOCK_CODE"].Value = dr["STOCK_CODE"];
                //    dgv.Rows[i].Cells["STOCK_NAME"].Value = dr["STOCK_NAME"];

                //    i = i + 1;
                //}

                dgv.DataSource = dt;

                dgv.SuspendLayout();
                dt = null;
            }
            catch (Exception ex)
            {

                dt = null;
                MessageBox.Show(ex.ToString());
                throw;
            }
        }
        public void GetSca01Data(ref DataGridView dgv, string stockCode)
        {
            DataTable dt = new DataTable();
            RichQuery richQuery = new RichQuery();
            int i = 0;

            dgv.Rows.Clear();
            try
            {
                dt = richQuery.p_Sca01Query("1", stockCode, 0, 0, "", "", false).Tables[0].Copy();

                if (dt == null) { return; }
                if (dt.Rows.Count < 1) { return; }

                foreach (DataRow dr in dt.Rows)
                {
                    dgv.Rows.Add();
                    dgv.Rows[i].Cells["BIG_FLOW"].Value = dr["BIG_FLOW"];
                    dgv.Rows[i].Cells["START_DATE"].Value = dr["START_DATE"];
                    dgv.Rows[i].Cells["END_DATE"].Value = dr["END_DATE"];
                    dgv.Rows[i].Cells["LOW_DATE"].Value = dr["LOW_DATE"];
                    dgv.Rows[i].Cells["HIGH_DATE"].Value = dr["HIGH_DATE"];
                    dgv.Rows[i].Cells["LOW_PRICE"].Value = dr["LOW_PRICE"];
                    dgv.Rows[i].Cells["HIGH_PRICE"].Value = dr["HIGH_PRICE"];
                    dgv.Rows[i].Cells["STOCK_INFO"].Value = dr["STOCK_INFO"];

                    i = i + 1;
                }
                dgv.SuspendLayout();
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.AutoResizeColumns();

                dt = null;
            }
            catch (Exception ex)
            {
                dt = null;
                MessageBox.Show(ex.ToString());
                throw;
            }
        }
    }
}
