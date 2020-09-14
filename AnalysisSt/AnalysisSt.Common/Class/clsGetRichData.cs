using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalysisSt.DataBaseFunc;

namespace AnalysisSt.Common.Class
{
    class clsGetRichData
    {
        AnalysisSt.DataBaseFunc.RichQuery _oRichQuery = new AnalysisSt.DataBaseFunc.RichQuery();
        /// <summary>
        /// 모든 종목을 가져온다.
        /// </summary>
        /// <returns>Dataset</returns>
        public DataSet GetAllStock()
        {
            return _oRichQuery.p_ScodeQuery("1", "", "", false);
        }

        /// <summary>
        /// 즐겨찾기 신규 그룹 번호를 가져온다.
        /// </summary>
        /// <returns>Dataset</returns>
        public DataSet GetNewCode_FcodeData()
        {
            return _oRichQuery.p_FCodeQuery("1", "", "", "", false);
        }

        /// <summary>
        /// 즐겨찾기 가져온다.
        /// </summary>
        /// <returns>Dataset</returns>
        public DataSet GetFcodeData()
        {
            return _oRichQuery.p_FCodeQuery("2", "", "", "", false);
        }

        /// <summary>
        /// 즐겨찾기 그룹 종목을 가져온다.
        /// </summary>
        /// <returns>Dataset</returns>
        public DataSet GetFsa01Data(String sGroupCode)
        {
            return _oRichQuery.p_FCodeQuery("3", sGroupCode, "", "", false);
        }

        

    }
}
