namespace Woom.DataAccess.OptCaller.InterFace
{
    internal interface IOptCaller
    {
        void SetInit(string FormId);

        string ScreenNoFooter { get; }

        void MakeDataTable();
    }
}