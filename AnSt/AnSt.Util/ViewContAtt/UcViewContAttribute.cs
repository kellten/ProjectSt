using AnSt.Singleton.ChaPro;
using System;
using System.Windows.Forms;

namespace AnSt.Util.ViewContAtt
{
    public partial class UcViewContAttribute : UserControl
    {
        ClsPassingViewContAtt clsPassingViewContAtt;
        public UcViewContAttribute()
        {
            InitializeComponent();
            clsPassingViewContAtt = ClsPassingViewContAtt.Instance();
            clsPassingViewContAtt.ViewContAttPropertyChanged += ViewContAttPropertyChanged;
        }

        public void SetPropertyGrid(object oControl)
        {
            if (oControl == null) { return; }

            proPertyGrid.SelectedObject = oControl;
            proPertyGrid.Refresh();

        }

        public void btnRefresh_Click(object sender, EventArgs e)
        {
            proPertyGrid.Refresh();
        }

        private void ViewContAttPropertyChanged(object sender, EventArgs e)
        {
            SetPropertyGrid(clsPassingViewContAtt.OControl);
        }
    }
}
