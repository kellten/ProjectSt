using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Woom.DataAccess.PlugIn;
using Woom.DataAccess.OptCaller.Class;
using SDataAccess;

namespace Woom.Tester.Forms
{
    public partial class FrmCsvToDataGridView : Form
    {
        public FrmCsvToDataGridView()
        {
            InitializeComponent();
            SetDataGridView2();
            ClsGetKoaStudioMethod clsGetKoaStudioMethod = new ClsGetKoaStudioMethod();
            _dtStockCode = clsGetKoaStudioMethod.GetCodeListByMarketCallBackDataTable("999").Copy();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //OpenFileDialog fdlg = new OpenFileDialog();

            //fdlg.Title = "Select file";

            //fdlg.InitialDirectory = @"c:\";

            //fdlg.FileName = txtFileName.Text;

            //fdlg.Filter = "Text and CSV Files(*.txt, *.csv)|*.txt;*.csv|Text Files(*.txt)|*.txt|CSV Files(*.csv)|*.csv|All Files(*.*)|*.*";

            //fdlg.FilterIndex = 1;

            //fdlg.RestoreDirectory = true;

            //if (fdlg.ShowDialog() == DialogResult.OK)

            //{

            //    txtFileName.Text = fdlg.FileName;

            //    Import();

            //    Application.DoEvents();

            //}
            //BindData(txtFileName.Text);
        }

        DataTable _dt = new DataTable();
        DataTable _dtStockCode;

        private void BindData(string filePath)
        {
            DataTable dt = new DataTable();
            string[] lines = System.IO.File.ReadAllLines(filePath, Encoding.UTF8);
            if (lines.Length > 0)
            {
                //first line to create header
                string firstLine = lines[0];
                string[] headerLabels = firstLine.Split(',');
                foreach (string headerWord in headerLabels)
                {
                    if (headerWord == "")
                    {
                        continue;
                    }
                    dt.Columns.Add(new DataColumn(headerWord));
                }
                //For Data
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] dataWords = lines[i].Split(',');
                    DataRow dr = dt.NewRow();
                    int columnIndex = 0;
                    foreach (string headerWord in headerLabels)
                    {
                        if (headerWord == "")
                        {
                            continue;
                        }

                        dr[headerWord] = dataWords[columnIndex++];
                    }
                    dt.Rows.Add(dr);
                }
            }
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
            }

        }

        private void BindData2(string kInfoGpNo, string filePath, string ThemaName)
        {
            int colIndex = 0;

            string[] lines = System.IO.File.ReadAllLines(filePath, Encoding.UTF8);
            if (lines.Length > 0)
            {
                //first line to create header
                string firstLine = lines[0];
                string[] headerLabels = firstLine.Split(',');

                foreach (string headerWord in headerLabels)
                {
                    if (headerWord == "종목명")
                    {
                        break;
                    }
                    colIndex = colIndex + 1;
                }
                //For Data
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] dataWords = lines[i].Split(',');
                    DataRow dr = _dt.NewRow();

                    if (dataWords[colIndex].ToString().Trim() == "")
                    {
                        continue;
                    }
                    dr["테마코드"] = kInfoGpNo;
                    dr["테마명"] = ThemaName;
                    dr["종목명"] = dataWords[colIndex].ToString().Trim();
                    DataTable dtSt = _dtStockCode.AsEnumerable().Where(Row => Row.Field<string>("STOCK_NAME").ToString().Trim() == dataWords[colIndex].ToString().Trim()).CopyToDataTable();

                    dr["종목코드"] = dtSt.Rows[0]["STOCK_CODE"].ToString().Trim();

                    _dt.Rows.Add(dr);

                }

            }
        }
                
        
        private void SetDataGridView2()
        {
            _dt.Columns.Add(new DataColumn("테마코드"));
            _dt.Columns.Add(new DataColumn("테마명"));
            _dt.Columns.Add(new DataColumn("종목명"));
            _dt.Columns.Add(new DataColumn("종목코드"));

        }

        private DataTable GetFileListFromFolderPath(string FolderName)
        { DirectoryInfo di = new DirectoryInfo(FolderName); // 해당 폴더 정보를 가져옵니다. 
            DataTable dt1 = new DataTable(); // 새로운 테이블 작성합니다.(FileInfo 에서 가져오기 원하는 속성을 열로 추가합니다.) 
            dt1.Columns.Add("Folder", typeof(string)); // 파일의 폴더
            dt1.Columns.Add("FileName", typeof(string)); // 파일 이름(확장자 포함) 
            dt1.Columns.Add("Extension", typeof(string)); // 확장자 
            dt1.Columns.Add("CreationTime", typeof(DateTime)); // 생성 일자 
            dt1.Columns.Add("LastWriteTime", typeof(DateTime)); // 마지막 수정 일자 
            dt1.Columns.Add("LastAccessTime", typeof(DateTime)); // 마지막 접근 일자
            foreach (FileInfo File in di.GetFiles()) // 선택 폴더의 파일 목록을 스캔합니다. 
            { dt1.Rows.Add(File.DirectoryName, File.Name, File.Extension, File.CreationTime, File.LastWriteTime, File.LastAccessTime); // 개별 파일 별로 정보를 추가합니다.
            } if(checkBox1.Checked == true) // 하위 폴더 포함될 경우 
            { DirectoryInfo[] di_sub = di.GetDirectories(); // 하위 폴더 목록들의 정보 가져옵니다.
                foreach (DirectoryInfo di1 in di_sub) // 하위 폴더목록을 스캔합니다. 
                { foreach (FileInfo File in di1.GetFiles()) // 선택 폴더의 파일 목록을 스캔합니다. 
                    { dt1.Rows.Add(File.DirectoryName, File.Name, File.Extension, File.CreationTime, File.LastWriteTime, File.LastAccessTime); // 개별 파일 별로 정보를 추가합니다.
                    }
                }
            }
            return dt1;
        }

        private void ShowDataFromDataTableToDataGridView(DataTable dt1, DataGridView dgv1)
        { dgv1.Rows.Clear(); // 이전 정보가 있을 경우, 모든 행을 삭제합니다.
            dgv1.Columns.Clear(); // 이전 정보가 있을 경우, 모든 열을 삭제합니다. 
            foreach (DataColumn dc1 in dt1.Columns) // 선택한 파일 목록이 들어있는 DataTable의 모든 열을 스캔합니다. 
            { dgv1.Columns.Add(dc1.ColumnName, dc1.ColumnName); // 출력할 DataGridView에 열을 추가합니다.
            }
            int row_index = 0; // 행 인덱스 번호(초기 값) 
            foreach (DataRow dr1 in dt1.Rows) // 선택한 파일 목록이 들어있는 DataTable의 모든 행을 스캔합니다. 
            { dgv1.Rows.Add(); // 빈 행을 하나 추가합니다. 
                foreach (DataColumn dc1 in dt1.Columns) // 선택한 파일 목록이 들어있는 DataTable의 모든 열을 스캔합니다.
                { dgv1.Rows[row_index].Cells[dc1.ColumnName].Value = dr1[dc1.ColumnName]; // 선택 행 별로, 스캔하는 열에 해당하는 셀 값을 입력합니다. 
                } row_index++; // 다음 행 인덱스를 선택하기 위해 1을 더해줍니다.
            } foreach(DataGridViewColumn drvc1 in dgv1.Columns) // 결과를 출력할 DataGridView의 모든 열을 스캔합니다. 
            { drvc1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; // 선택 열의 너비를 자동으로 설정합니다. 
            }
        }


            private void btnGetDirectoryFile_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog(); // 새로운 폴더 선택 Dialog 를 생성합니다. 
            dialog.IsFolderPicker = true; // 
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok) // 폴더 선택이 정상적으로 되면 아래 코드를 실행합니다.
            { label2.Text = dialog.FileName; // 선택한 폴더 이름을 label2에 출력합니다. 
                DataTable dt_filelistinfo = GetFileListFromFolderPath(dialog.FileName);
                ShowDataFromDataTableToDataGridView(dt_filelistinfo, dataGridView1);
            }

        
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                int j = 0;
                string path = "";
                path  = dataGridView1.Rows[i].Cells[0].Value.ToString() + "\\" +  dataGridView1.Rows[i].Cells[1].Value.ToString();
                j = i + 1;
                BindData2("K" + j.ToString("D7"), path, dataGridView1.Rows[i].Cells[1].Value.ToString().Replace(".csv", ""));
            }

            dataGridView2.DataSource = _dt;
        }

        private void btnStoreDb_Click(object sender, EventArgs e)
        {
            if (_dt.Rows.Count > 0)
            {
                ArrayParam arrParam = new ArrayParam();
                Sql oSql = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "KIWOOMDB");

                foreach (DataRow dr in _dt.Rows)
                {
                    arrParam.Clear();
                    arrParam.Add("@ACTION_GB", "A");
                    arrParam.Add("@KIFGP_CODE", dr["테마코드"].ToString().Trim());
                    arrParam.Add("@KIFGP_NAME", dr["테마명"].ToString().Trim());
                    arrParam.Add("@STOCK_CODE", dr["종목코드"].ToString().Trim());
                    arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                    oSql.ExecuteNonQuery("p_KIFSTAdd", CommandType.StoredProcedure, arrParam);

                }

                MessageBox.Show("작업이 완료되었습니다.");

            }



        }
    }
}
