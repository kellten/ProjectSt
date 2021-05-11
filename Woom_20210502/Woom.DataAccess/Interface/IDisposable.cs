using System.Runtime.InteropServices;

namespace Woom.DataAccess.Interface
{
    [ComVisible(true)]
    internal interface IDisposable
    {
        void Dispose();
    }
}