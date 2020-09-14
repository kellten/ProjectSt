using SDataAccess;
using System.Data;
using System.Windows.Forms;

namespace AnSt.Volume.Uc
{
    public partial class UcVolumeInfo : UserControl
    {
        public UcVolumeInfo()
        {
            InitializeComponent();
        }

        private string _stockCode = "";
        public string StockCode { get { return _stockCode; } set { _stockCode = value; } }

        private void GetVolumneInfo()
        {
            DataTable dt;
            SDataAccess.KiwoomQuery kiwoomQuery = new KiwoomQuery();



        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
