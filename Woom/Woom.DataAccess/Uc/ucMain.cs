using System.Windows.Forms;

namespace Woom.DataAccess.Uc
{
    public partial class ucMain : UserControl
    {
        public ucMain()
        {
            InitializeComponent();
        }

        public AxKHOpenAPILib.AxKHOpenAPI AxKHApi { get { return AxKH; } }
    }
}