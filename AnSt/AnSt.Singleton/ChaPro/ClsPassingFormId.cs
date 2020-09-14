using System.Data;

namespace AnSt.Singleton.ChaPro
{
    public class ClsPassingFormId
    {

        private static ClsPassingFormId _instance = null;
        private static readonly object padlock = new object();

        private string _FormId;
        private DataTable _DtFormId;

        public string FormId { get { return _FormId; } set { _FormId = value; } }
        protected ClsPassingFormId() { }

        public static ClsPassingFormId Instance()
        {
            if (_instance == null)
            {
                lock (padlock)
                {
                    _instance = new ClsPassingFormId();
                    _instance.DtFormIdInit();
                }
            }

            return _instance;

        }

        private void DtFormIdInit()
        {
            if (_DtFormId != null) { _DtFormId = null; }
            _DtFormId = new DataTable();

            _DtFormId.Columns.Add("FORM_ID", typeof(string));
            _DtFormId.Columns.Add("USE_GB", typeof(string));

            for (int i = 1; i < 100; i++)
            {
                DataRow dr;

                dr = _DtFormId.NewRow();
                if (i < 10)
                {
                    dr["FORM_ID"] = "0" + i.ToString();
                }
                else
                {
                    dr["FORM_ID"] = i.ToString();
                }

                dr["USE_GB"] = "N";

                _DtFormId.Rows.Add(dr);
            }
        }

        public string GetFormId()
        {
            foreach (DataRow dr in _DtFormId.Rows)
            {
                if (dr["USE_GB"].ToString().Trim() == "N")
                {
                    dr["USE_GB"] = "Y";
                    return dr["FORM_ID"].ToString().Trim();
                }
            }

            return "";
        }

        public void RemoveFormId(string FormId)
        {
            foreach (DataRow dr in _DtFormId.Rows)
            {
                if (dr["FORM_ID"].ToString().Trim() == FormId)
                {
                    dr["USE_GB"] = "N";
                    return;
                }
            }
        }
    }
}
