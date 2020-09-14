using AnSt.Define.Attribute;
using AnSt.Define.ChartAttribute;
using System.ComponentModel;
using System.Windows.Forms;

namespace AnSt.Chart.ChartMenu
{
    public partial class UcChartMenu : UserControl
    {
        private ClsChartMenuAttirbute clsChartMenuAttirbute;
        private ClsChartDefineMember clsChartDefineMember;

        #region 이벤트
        public event OnChartMenuAttributeHandler OnChartMenuAttribute;
        public delegate void OnChartMenuAttributeHandler(object sender, PropertyChangedEventArgs e);
        #endregion

        public UcChartMenu()
        {
            InitializeComponent();
            clsChartMenuAttirbute = new ClsChartMenuAttirbute();
            clsChartDefineMember = new ClsChartDefineMember();
            ppGridChartMenu.SelectedObject = clsChartMenuAttirbute;
            clsChartMenuAttirbute.ChartMenuAttributeChanged += ChartMenuAttributeChanged;
        }

        private void ChartMenuAttributeChanged(object sender, PropertyChangedEventArgs e)
        {

        }
    }
}
