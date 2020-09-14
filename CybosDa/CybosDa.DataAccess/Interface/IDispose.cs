using System.Runtime.InteropServices;

namespace CybosDa.DataAccess.Interface
{
    [ComVisible(true)]
    internal interface IDisposable
    {
        void Dispose();
    }
}