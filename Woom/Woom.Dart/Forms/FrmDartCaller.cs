using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Woom.Dart.Class;
using Woom.DataAccess.OptCaller.Class;
using Woom.DataDefine.Util;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace Woom.Dart.Forms
{
    public partial class FrmDartCaller : Form
    {
        public FrmDartCaller()
        {
            InitializeComponent();
        }

        DataSet _ds = new DataSet();
        DataTable _dt = new DataTable();

        private void BtnDartCall_Click(object sender, EventArgs e)
        {
            try
            {
                string path = @"C:\temp\";
                string filePath = "";
                string outStringFileName = "";
                ClsDartApi clsDartApi = new ClsDartApi();

                filePath = clsDartApi.callWebClientZipSave("https://opendart.fss.or.kr/api/corpCode.xml?crtfc_key=fc9f7996b19984e91edab1bed1dd0a6249836aa8", path);

                clsDartApi.UnZipFiles(filePath, path, "", true, out outStringFileName);

                FileStream fileStream = new FileStream(path + outStringFileName, FileMode.Open);

                _ds.ReadXml(fileStream);

                fileStream.Close();

                    //dgvList.DataSource = _ds.Tables[0];

                    GetDartCodeInStockName();
                    
                }
                catch (Exception)
                {
                    
                    throw;
                }

        }

        private void GetDartCodeInStockName()
        {
            ClsGetKoaStudioMethod clsGetKoaStudioMethod = new ClsGetKoaStudioMethod();

            _dt = clsGetKoaStudioMethod.GetCodeListByMarketCallBackDataTable("999").Copy();

            var Rows = from t1 in _dt.AsEnumerable()
                       join t2 in _ds.Tables[0].AsEnumerable()
                        on t1.Field<string>("STOCK_NAME") equals t2.Field<string>("corp_name")
                       select new {
                           STOCK_CODE = t1.Field<string>("STOCK_CODE").ToString().Trim(),
                           STOCK_NAME = t1.Field<string>("STOCK_NAME").ToString().Trim(),
                           corp_code = t2.Field<string>("corp_code").ToString().Trim(),
                           corp_name = t2.Field<string>("corp_name").ToString().Trim()
                       };

            ClsDataGridViewUtil clsDataGridViewUtil = new ClsDataGridViewUtil();
            int row = 0;

            clsDataGridViewUtil.RemoveGridViewRow(dgvList);

            foreach (var dr in Rows)
            {
                dgvList.Rows.Add();

                dgvList.Rows[row].Cells["corp_code"].Value = dr.corp_code;
                dgvList.Rows[row].Cells["STOCK_CODE"].Value = dr.STOCK_CODE;
                dgvList.Rows[row].Cells["STOCK_NAME"].Value = dr.STOCK_NAME;

                row = row + 1;
            }
                       
        }

        private void ExportExcel(bool captions)
        {
            this.saveFileDialog1.FileName = "TempName";
            this.saveFileDialog1.DefaultExt = "xls";
            this.saveFileDialog1.Filter = "Excel files (*.xls)|*.xls";
            this.saveFileDialog1.InitialDirectory = "c:\\";

            DialogResult result = saveFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                int num = 0;
                object missingType = Type.Missing;

                Excel.Application objApp;
                Excel._Workbook objBook;
                Excel.Workbooks objBooks;
                Excel.Sheets objSheets;
                Excel._Worksheet objSheet;
                Excel.Range range;

                string[] headers = new string[dgvList.ColumnCount];
                string[] columns = new string[dgvList.ColumnCount];

                for (int c = 0; c < dgvList.ColumnCount; c++)
                {
                    headers[c] = dgvList.Rows[0].Cells[c].OwningColumn.HeaderText.ToString();
                    num = c + 65;
                    columns[c] = Convert.ToString((char)num);
                }

                try
                {
                    objApp = new Excel.Application();
                    objBooks = objApp.Workbooks;
                    objBook = objBooks.Add(Missing.Value);
                    objSheets = objBook.Worksheets;
                    objSheet = (Excel._Worksheet)objSheets.get_Item(1);

                    if (captions)
                    {
                        for (int c = 0; c < dgvList.ColumnCount; c++)
                        {
                            range = objSheet.get_Range(columns[c] + "1", Missing.Value);
                            range.set_Value(Missing.Value, headers[c]);
                        }
                    }

                    for (int i = 0; i < dgvList.RowCount - 1; i++)
                    {
                        for (int j = 0; j < dgvList.ColumnCount; j++)
                        {
                            range = objSheet.get_Range(columns[j] + Convert.ToString(i + 2),
                                                                   Missing.Value);
                            range.set_Value(Missing.Value,
                                                  dgvList.Rows[i].Cells[j].Value.ToString().Trim());
                        }
                    }

                    objApp.Visible = false;
                    objApp.UserControl = false;

                    objBook.SaveAs(saveFileDialog1.FileName,
                              Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal,
                              missingType, missingType, missingType, missingType,
                              Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                              missingType, missingType, missingType, missingType, missingType);
                    objBook.Close(false, missingType, missingType);

                    Cursor.Current = Cursors.Default;

                    MessageBox.Show("Save Success!!!");
                }
                catch (Exception theException)
                {
                    String errorMessage;
                    errorMessage = "Error: ";
                    errorMessage = String.Concat(errorMessage, theException.Message);
                    errorMessage = String.Concat(errorMessage, " Line: ");
                    errorMessage = String.Concat(errorMessage, theException.Source);

                    MessageBox.Show(errorMessage, "Error");
                }

            }
        }
            private void BtnExcelExport_Click(object sender, EventArgs e)
        {
            ExportExcel(true);  
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
