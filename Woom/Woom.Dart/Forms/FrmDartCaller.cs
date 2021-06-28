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

            //DataTable dt = from t1 in _dt.AsEnumerable()
            //           join t2 in _ds.Tables[0].AsEnumerable() on t1.Field<string>("STOCK_NAME") equals t2.Field<string>("corp_name") into tg
            //           from tcheck in tg.CopyToDataTable()
            //           select t1;

            //foreach (DataRow dr in rows)
            //{


            //}
        }
    }
}
