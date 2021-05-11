namespace Woom.DataAccess.ScreenNo.Interface
{
    internal interface IScreenNo
    {
        string BasicGetScreenNo(string formId, string ScreenNoFooter);

        string RealGetScreenNo(string formId, string ScreenNoFooter);
    }
}