using System;
using System.Data;
using System.Drawing.Printing;

namespace Woom.DataDefine.OptData
{
    public class ClsColumnSets : IDisposable
    {


        public enum Column10001Index
        {
            종목코드, 종목명, 결산월, 액면가, 자본금, 상장주식, 신용비율, 연중최고, 연중최저, 시가총액, 시가총액비중, 외인소진률, 대용가, PER, EPS, ROE, PBR, EV, BPS, 매출액, 영업이익, 당기순이익, 최고250,
            최저250, 시가, 고가, 저가, 상한가, 하한가, 기준가, 예상체결가, 예상체결수량, 최고가일250, 최고가대비율250, 최저가일250, 최저가대비율250, 현재가, 대비기호, 전일대비, 등락율, 거래량, 거래대비, 액면가단위
        }
        public enum Column10005Index
        {
            날짜,시가,고가,종가,대비,등락률,거래량,거래대금,체결강도,외인보유,외인비중,외인순매수,기관순매수,개인순매수,외국계,신용잔고율,프로그램
        }

        #region Enum

        public enum Column10014Index
        {
           일자, 종가,전일대비기호,전일대비,등락율,거래량,공매도량,매매비중,공매도거래대금,공매도평균가
        }

        public enum Column10059Index
        {
            일자, 현재가, 대비기호, 전일대비, 등락율, 누적거래대금, 개인투자자,
            외국인투자자, 기관계, 금융투자, 보험, 투신, 기타금융, 은행,
            연기금등, 사모펀드, 국가, 기타법인, 내외국인
        }
        #endregion

        public enum Column10081Index
        {
            종목코드, 현재가, 거래량, 거래대금, 일자, 시가, 고가, 저가,
            수정주가구분, 수정비율, 대업종구분, 소업종구분, 종목정보,
            수정주가이벤트
            //, 전일종가
        }
        public enum Column10086Index
        {
            날짜,시가,고가,저가,종가,전일비,등락률,거래량,금액백만,신용비,개인,기관,외인수량,외국계,프로그램,외인비,체결강도,외인보유,외인비중,외인순매수,개인순매수,신용잔고율
        }
        public enum Column10060Index
        {
            일자, 현재가, 전일대비, 누적거래대금, 개인투자자, 외국인투자자, 기관계, 금융투자, 보험, 투신, 기타금융, 은행,
            연기금등, 사모펀드, 국가, 기타법인, 내외국인
        }
        public enum Column20068Index
        {
            일자, 대차거래체결주수, 대차거래상환주수, 대차거래증감, 잔고주수, 잔고금액
        }


        public enum ColumnNameIndex
        {
            일자, 현재가, 대비기호, 전일대비, 등락율, 누적거래대금, 개인투자자,
            외국인투자자, 기관계, 금융투자, 보험, 투신, 기타금융, 은행,
            연기금등, 사모펀드, 국가, 기타법인, 내외국인,
            종목코드, 거래량, 거래대금, 시가, 고가, 저가,
            수정주가구분, 수정비율, 대업종구분, 소업종구분, 종목정보,
            수정주가이벤트, 전일종가,
            종목명, 결산월, 액면가, 자본금, 상장주식, 신용비율, 연중최고, 연중최저, 시가총액, 시가총액비중, 외인소진률, 대용가, PER, EPS, ROE, PBR, EV, BPS, 매출액, 영업이익, 당기순이익, 최고250,
            최저250, 상한가, 하한가, 기준가, 예상체결가, 예상체결수량, 최고가일250, 최고가대비율250, 최저가일250, 최저가대비율250, 거래대비, 액면가단위,
            대차거래체결주수, 대차거래상환주수, 대차거래증감, 잔고주수, 잔고금액,
            종가, 전일대비기호, 공매도량, 매매비중, 공매도거래대금, 공매도평균가,
            날짜,  전일비, 등락률,  금액백만, 신용비, 개인, 기관, 외인수량, 외국계, 프로그램, 외인비, 체결강도, 외인보유, 외인비중, 외인순매수, 개인순매수, 신용잔고율

        }

        public DataColumn GetDataColumn(ColumnNameIndex ci)
        {
            DataColumn dc = new DataColumn();
            
            // 숫자가 enum앞에 올수가 없어서, 예외처리
            switch (Enum.GetName(typeof(ColumnNameIndex), ci).ToString().Trim())
            {
                case "최고250":
                    dc.ColumnName = "250최고";
                    break;
                case "최저250":
                    dc.ColumnName = "250최저";
                    break;
                case "최고가일250":
                    dc.ColumnName = "250최고가일";
                    break;
                case "최고가대비율250":
                    dc.ColumnName = "250최고가대비율";
                    break;
                case "최저가대비율250":
                    dc.ColumnName = "250최저가대비율";
                    break;
                default:
                    dc.ColumnName = Enum.GetName(typeof(ColumnNameIndex), ci);
                    break;
            }

            if (ci.ToString() == "Column10001Index")
            {
                dc.DataType = typeof(string);
                return dc;
            }

            switch (ci)
            {
                case ColumnNameIndex.일자:
                    dc.DataType = typeof(string);
                    break;

                case ColumnNameIndex.현재가:
                    dc.DataType = typeof(Int32);
                    break;

                case ColumnNameIndex.대비기호:
                    dc.DataType = typeof(string);
                    break;

                case ColumnNameIndex.전일대비:
                    dc.DataType = typeof(string);
                    break;

                case ColumnNameIndex.등락율:
                    dc.DataType = typeof(string);
                    break;

                case ColumnNameIndex.누적거래대금:
                    dc.DataType = typeof(Int64);
                    break;

                case ColumnNameIndex.개인투자자:
                    dc.DataType = typeof(Int64);
                    break;

                case ColumnNameIndex.외국인투자자:
                    dc.DataType = typeof(Int64);
                    break;

                case ColumnNameIndex.기관계:
                    dc.DataType = typeof(Int64);
                    break;

                case ColumnNameIndex.금융투자:
                    dc.DataType = typeof(Int64);
                    break;

                case ColumnNameIndex.보험:
                    dc.DataType = typeof(Int64);
                    break;

                case ColumnNameIndex.투신:
                    dc.DataType = typeof(Int64);
                    break;

                case ColumnNameIndex.기타금융:
                    dc.DataType = typeof(Int64);
                    break;

                case ColumnNameIndex.은행:
                    dc.DataType = typeof(Int64);
                    break;

                case ColumnNameIndex.연기금등:
                    dc.DataType = typeof(Int64);
                    break;

                case ColumnNameIndex.사모펀드:
                    dc.DataType = typeof(Int64);
                    break;

                case ColumnNameIndex.국가:
                    dc.DataType = typeof(Int64);
                    break;

                case ColumnNameIndex.기타법인:
                    dc.DataType = typeof(Int64);
                    break;

                case ColumnNameIndex.내외국인:
                    dc.DataType = typeof(Int64);
                    break;

                case ColumnNameIndex.종목코드:
                    dc.DataType = typeof(string);
                    break;

                case ColumnNameIndex.거래량:
                    dc.DataType = typeof(Int64);
                    break;

                case ColumnNameIndex.거래대금:
                    dc.DataType = typeof(Int64);
                    break;

                case ColumnNameIndex.시가:
                    dc.DataType = typeof(Int32);
                    break;

                case ColumnNameIndex.고가:
                    dc.DataType = typeof(Int32);
                    break;

                case ColumnNameIndex.저가:
                    dc.DataType = typeof(Int32);
                    break;

                case ColumnNameIndex.수정주가구분:
                    dc.DataType = typeof(string);
                    break;

                case ColumnNameIndex.수정비율:
                    dc.DataType = typeof(string);
                    break;

                case ColumnNameIndex.대업종구분:
                    dc.DataType = typeof(string);
                    break;

                case ColumnNameIndex.소업종구분:
                    dc.DataType = typeof(string);
                    break;

                case ColumnNameIndex.종목정보:
                    dc.DataType = typeof(string);
                    break;

                case ColumnNameIndex.수정주가이벤트:
                    dc.DataType = typeof(string);
                    break;

                case ColumnNameIndex.전일종가:
                    dc.DataType = typeof(Int32);
                    break;
                case ColumnNameIndex.종목명:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.결산월:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.액면가:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.자본금:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.상장주식:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.신용비율:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.연중최고:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.연중최저:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.시가총액:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.시가총액비중:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.외인소진률:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.대용가:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.PER:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.EPS:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.ROE:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.PBR:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.EV:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.BPS:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.매출액:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.영업이익:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.당기순이익:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.최고250:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.최저250:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.상한가:
                    dc.DataType = typeof(string);
                    break;

                case ColumnNameIndex.하한가:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.기준가:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.예상체결가:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.예상체결수량:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.최고가일250:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.최고가대비율250:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.최저가일250:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.최저가대비율250:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.거래대비:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.액면가단위:
                       dc.DataType = typeof(string);
                    break;


                case ColumnNameIndex.대차거래체결주수:
                    dc.DataType = typeof(Int64);
                    break;

                case ColumnNameIndex.대차거래상환주수:
                    dc.DataType = typeof(Int64);
                    break;

                case ColumnNameIndex.대차거래증감:
                    dc.DataType = typeof(Int32);
                    break;

                case ColumnNameIndex.잔고주수:
                    dc.DataType = typeof(Int64);
                    break;

                case ColumnNameIndex.잔고금액:
                    dc.DataType = typeof(Int64);
                    break;
                case ColumnNameIndex.종가:
                    dc.DataType = typeof(Int32);
                    break;
                case ColumnNameIndex.전일대비기호:
                    dc.DataType = typeof(String);
                    break;
                case ColumnNameIndex.공매도량:
                    dc.DataType = typeof(Int64);
                    break;
                case ColumnNameIndex.매매비중:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.공매도거래대금:
                    dc.DataType = typeof(Int64);
                    break;
                case ColumnNameIndex.공매도평균가:
                    dc.DataType = typeof(Int64);
                    break;
                case ColumnNameIndex.날짜:
                    dc.DataType = typeof(String);
                    break;
                case ColumnNameIndex.전일비:
                    dc.DataType = typeof(Int16);
                    break;
                case ColumnNameIndex.등락률:
                    dc.DataType = typeof(Int16);
                    break;
                case ColumnNameIndex. 금액백만:
                    dc.DataType = typeof(Int64);
                    break;
                case ColumnNameIndex.신용비:
                    dc.DataType = typeof(Int64);
                    break;
                case ColumnNameIndex.개인:
                    dc.DataType = typeof(Int64);
                    break;
                case ColumnNameIndex.기관:
                    dc.DataType = typeof(Int64);
                    break;
                case ColumnNameIndex.외인수량:
                    dc.DataType = typeof(Int64);
                    break;
                case ColumnNameIndex.외국계:
                    dc.DataType = typeof(Int64);
                    break;
                case ColumnNameIndex.프로그램:
                    dc.DataType = typeof(Int64);
                    break;
                case ColumnNameIndex.외인비:
                    dc.DataType = typeof(Int64);
                    break;
                case ColumnNameIndex.체결강도:
                    dc.DataType = typeof(Int16);
                    break;
                case ColumnNameIndex.외인보유:
                    dc.DataType = typeof(Int64);
                    break;
                case ColumnNameIndex.외인비중:
                    dc.DataType = typeof(Int16);
                    break;
                case ColumnNameIndex.외인순매수:
                    dc.DataType = typeof(Int64);
                    break;
                case ColumnNameIndex.개인순매수:
                    dc.DataType = typeof(Int64);
                    break;
                case ColumnNameIndex.신용잔고율:
                    dc.DataType = typeof(Int32);
                    break;
                default:
                    break;
            }

            return dc;
        }

        public DataColumn GetDataColumn10001(ColumnNameIndex ci)
        {
            DataColumn dc = new DataColumn();

            // 숫자가 enum앞에 올수가 없어서, 예외처리
            switch (Enum.GetName(typeof(ColumnNameIndex), ci).ToString().Trim())
            {
                case "최고250":
                    dc.ColumnName = "250최고";
                    break;
                case "최저250":
                    dc.ColumnName = "250최저";
                    break;
                case "최고가일250":
                    dc.ColumnName = "250최고가일";
                    break;
                case "최고가대비율250":
                    dc.ColumnName = "250최고가대비율";
                    break;
                case "최저가대비율250":
                    dc.ColumnName = "250최저가대비율";
                    break;
                default:
                    dc.ColumnName = Enum.GetName(typeof(ColumnNameIndex), ci);
                    break;
            }

       
                dc.DataType = typeof(string);
                return dc;
         

        }

        public System.Windows.Forms.DataGridViewColumn GetDataGridViewColumn(ColumnNameIndex ci)
        {
            System.Windows.Forms.DataGridViewColumn dc = new System.Windows.Forms.DataGridViewColumn();

            // 숫자가 enum앞에 올수가 없어서, 예외처리
            switch (Enum.GetName(typeof(ColumnNameIndex), ci).ToString().Trim())
            {
                case "최고250":
                    dc.Name = "250최고";
                    break;
                case "최저250":
                    dc.Name = "250최저";
                    break;
                case "최고가일250":
                    dc.Name = "250최고가일";
                    break;
                case "최고가대비율250":
                    dc.Name = "250최고가대비율";
                    break;
                case "최저가대비율250":
                    dc.Name = "250최저가대비율";
                    break;
                default:
                    dc.Name = Enum.GetName(typeof(ColumnNameIndex), ci);
                    break;
            }
            System.Windows.Forms.DataGridViewCell cell = new System.Windows.Forms.DataGridViewTextBoxCell();

            dc.CellTemplate = cell;

            return dc;
        }


        #region IDisposable Support

        private bool disposedValue = false; // 중복 호출을 검색하려면

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
                }

                // TODO: 관리되지 않는 리소스(관리되지 않는 개체)를 해제하고 아래의 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.

                disposedValue = true;
            }
        }

        // TODO: 위의 Dispose(bool disposing)에 관리되지 않는 리소스를 해제하는 코드가 포함되어 있는 경우에만 종료자를 재정의합니다.
        // ~ClsBasicDataType() {
        //   // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
        //   Dispose(false);
        // }

        // 삭제 가능한 패턴을 올바르게 구현하기 위해 추가된 코드입니다.
        void IDisposable.Dispose()
        {
            // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
            Dispose(true);
            // TODO: 위의 종료자가 재정의된 경우 다음 코드 줄의 주석 처리를 제거합니다.
            // GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }
}