using AnSt.Define.Attribute;
using AnSt.Define.Header;
using AnSt.Define.SetData;
using AnSt.Singleton.ChaPro;
using System;
using System.Windows.Forms;

namespace AnSt.BasicSetting.Favorite
{
    public partial class UcFav : UserControl
    {
        ClsStockAttribute stockAttribute = new ClsStockAttribute();
        public const int MAINPANEL1SIZE = 370;
        public const int MAINPANEL2SIZE = 220;
        ClsPassingStockCode clsPassingStockCode;
        public UcFav()
        {
            InitializeComponent();
            InitDgv();
            ucStockList1.EditMode = "E";
            ucStockList1.OnChangeFsa01 += OnChangeFsa01;
            clsPassingStockCode = ClsPassingStockCode.Instance();
        }
        private void InitDgv()
        {
            ClsDgvDefine clsDgvDefine = new ClsDgvDefine();

            clsDgvDefine.FCode(ref dgvFCode);
            clsDgvDefine.Fsa01(ref dgvStockList);
            lblSGroupName.Text = "";
            lblSGroupName.Tag = "";
        }
        private void dgvFCode_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFCode.Rows[e.RowIndex].Cells["SGROUP_CODE"].Value == null) { return; }
            if (dgvFCode.Rows[e.RowIndex].Cells["SGROUP_CODE"].Value.ToString().Trim() == "")
            {
                return;
            }

            ClsDgvSetData clsDgvSetData = new ClsDgvSetData();
            clsDgvSetData.GetFsa01Data(ref dgvStockList, dgvFCode.Rows[e.RowIndex].Cells["SGROUP_CODE"].Value.ToString().Trim());
            ucStockList1.SGroupCode = dgvFCode.Rows[e.RowIndex].Cells["SGROUP_CODE"].Value.ToString().Trim();
            lblSGroupName.Text = dgvFCode.Rows[e.RowIndex].Cells["SGROUP_NAME"].Value.ToString().Trim();
            lblSGroupName.Tag = dgvFCode.Rows[e.RowIndex].Cells["SGROUP_CODE"].Value.ToString().Trim();

        }

        private void dgvStockList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvStockList.Rows[e.RowIndex].Cells["STOCK_CODE"].Value == null) { return; }
            if (dgvStockList.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim() == "")
            {
                return;
            }
            stockAttribute.StockName = dgvStockList.Rows[e.RowIndex].Cells["STOCK_NAME"].Value.ToString().Trim();
            stockAttribute.StockCode = dgvStockList.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim();

            clsPassingStockCode.StockName = dgvStockList.Rows[e.RowIndex].Cells["STOCK_NAME"].Value.ToString().Trim();
            clsPassingStockCode.StockCode = dgvStockList.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim();

        }
        private void btnUpDown_Click(object sender, EventArgs e)
        {
            splitConFav.Panel1Collapsed = !(splitConFav.Panel1Collapsed);
        }

        private void OnChangeFsa01()
        {
            SuspendDgvStockList(ucStockList1.SGroupCode);
        }

        private void dgvStockList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode.ToString() == "D")
            {
                Favorite.Class.ClsFavFunc clsFavFunc = new Favorite.Class.ClsFavFunc();
                clsFavFunc.Fsa01Del(lblSGroupName.Tag.ToString().Trim(), dgvStockList.Rows[dgvStockList.CurrentCell.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim());
                SuspendDgvStockList(lblSGroupName.Tag.ToString().Trim());
            }
        }

        private void SuspendDgvStockList(string sGroupCode)
        {
            ClsDgvSetData clsDgvSetData = new ClsDgvSetData();
            clsDgvSetData.GetFsa01Data(ref dgvStockList, sGroupCode);
        }

        private void dgvFCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode.ToString() == "C")
            {
                int i = dgvFCode.CurrentCell.RowIndex;
                Favorite.Class.ClsFavFunc clsFavFunc = new Favorite.Class.ClsFavFunc();
                if (dgvFCode.Rows[i].Cells["SGROUP_CODE"].Value.ToString().Trim() == "")
                {
                    if (dgvFCode.Rows[i].Cells["SGROUP_NAME"].Value == null)
                    {
                        MessageBox.Show("그룹명이 누락되었습니다.");
                        return;
                    }

                    if (dgvFCode.Rows[i].Cells["SGROUP_NAME"].Value.ToString().Trim() == "")
                    {
                        MessageBox.Show("그룹명이 누락되었습니다.");
                        return;
                    }

                    if (MessageBox.Show(dgvFCode.Rows[i].Cells["SGROUP_NAME"].Value.ToString().Trim() +
                                 "을 입력하시겠습니까?", "관심그룹 입력", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        SDataAccess.RichQuery richQuery = new SDataAccess.RichQuery();
                        string newSGroupCode = "";

                        newSGroupCode = richQuery.p_FCodeQuery("1", "", "", "", false).Tables[0].Rows[0]["NEW_SGOUP_CODE"].ToString().Trim();

                        if (newSGroupCode == "" || newSGroupCode == "Error")
                        {
                            MessageBox.Show("신규 그룹코드를 가져오지 못했습니다.");
                            return;
                        }

                        clsFavFunc.FCodeAdd("A", newSGroupCode, dgvFCode.Rows[i].Cells["SGROUP_NAME"].Value.ToString().Trim(), dgvFCode.Rows[i].Cells["SGROUP_INFO"].Value.ToString().Trim());
                        InitDgv();

                    }

                }
                else
                {
                    if (MessageBox.Show(dgvFCode.Rows[i].Cells["SGROUP_NAME"].Value.ToString().Trim() +
                                 "을 수정하시겠습니까?", "관심그룹 수정", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        clsFavFunc.FCodeAdd("A", dgvFCode.Rows[i].Cells["SGROUP_CODE"].Value.ToString().Trim(), dgvFCode.Rows[i].Cells["SGROUP_NAME"].Value.ToString().Trim(), dgvFCode.Rows[i].Cells["SGROUP_INFO"].Value.ToString().Trim());
                        InitDgv();
                    }

                }
            }
        }

        private void dgvFCode_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFCode.Rows[e.RowIndex].IsNewRow == true)
            {
                for (int i = 0; i < dgvFCode.ColumnCount; i++)
                {
                    dgvFCode.Rows[e.RowIndex].Cells[i].Value = "";
                }
            }
        }

        public void ChangeFavSplitter(string name)
        {
            if (name == "FAV")
            {
                splitConMain.Panel1Collapsed = !(splitConMain.Panel1Collapsed);
                if (splitConMain.Panel1Collapsed == false && splitConMain.Panel2Collapsed == false)
                {
                    this.Width = MAINPANEL1SIZE + MAINPANEL2SIZE + 20;
                }
                else
                {
                    if (splitConMain.Panel1Collapsed == true)
                    {
                        this.Width = MAINPANEL2SIZE + 20;
                    }
                    else
                    {
                        this.Width = MAINPANEL1SIZE + 20;
                    }
                }

            }
            else
            {
                splitConMain.Panel2Collapsed = !(splitConMain.Panel2Collapsed);
                if (splitConMain.Panel1Collapsed == false && splitConMain.Panel2Collapsed == false)
                {
                    this.Width = MAINPANEL1SIZE + MAINPANEL2SIZE + 20;
                }
                else
                {
                    if (splitConMain.Panel2Collapsed == true)
                    {
                        this.Width = MAINPANEL1SIZE + 20;
                    }
                    else
                    {
                        this.Width = MAINPANEL2SIZE + 20;
                    }
                }

            }
        }
        public void ChangeHeightUcFav(int Height)
        {
            this.Height = Height;
        }

    }
}
