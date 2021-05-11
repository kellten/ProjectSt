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
        

        public enum Column10081Index
        {
            종목코드, 현재가, 거래량, 거래대금, 일자, 시가, 고가, 저가,
            수정주가구분, 수정비율, 대업종구분, 소업종구분, 종목정보,
            수정주가이벤트
            //, 전일종가
        }
        public enum Column10060Index
        {
            일자, 현재가, 전일대비, 누적거래대금, 개인투자자, 외국인투자자, 기관계, 금융투자, 보험, 투신, 기타금융, 은행,
            연기금등, 사모펀드, 국가, 기타법인, 내외국인
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
            최저250, 상한가, 하한가, 기준가, 예상체결가, 예상체결수량, 최고가일250, 최고가대비율250, 최저가일250, 최저가대비율250, 거래대비, 액면가단위
        }

        public DataColumn GetDataColumn(ColumnNameIndex ci)
        {
            DataColumn dc = new DataColumn();
            dc.ColumnName = Enum.GetName(typeof(ColumnNameIndex), ci);
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
                    dc.DataType = typeof(Int32);
                    break;
                case ColumnNameIndex.자본금:
                    dc.DataType = typeof(Int64);
                    break;
                case ColumnNameIndex.상장주식:
                    dc.DataType = typeof(Int64);
                    break;
                case ColumnNameIndex.신용비율:
                    dc.DataType = typeof(Decimal);
                    break;
                case ColumnNameIndex.연중최고:
                    dc.DataType = typeof(Int32);
                    break;
                case ColumnNameIndex.연중최저:
                    dc.DataType = typeof(Int32);
                    break;
                case ColumnNameIndex.시가총액:
                    dc.DataType = typeof(Int64);
                    break;
                case ColumnNameIndex.시가총액비중:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.외인소진률:
                    dc.DataType = typeof(Decimal);
                    break;
                case ColumnNameIndex.대용가:
                    dc.DataType = typeof(Int32);
                    break;
                case ColumnNameIndex.PER:
                    dc.DataType = typeof(Decimal);
                    break;
                case ColumnNameIndex.EPS:
                    dc.DataType = typeof(Decimal);
                    break;
                case ColumnNameIndex.ROE:
                    dc.DataType = typeof(Decimal);
                    break;
                case ColumnNameIndex.PBR:
                    dc.DataType = typeof(Decimal);
                    break;
                case ColumnNameIndex.EV:
                    dc.DataType = typeof(Decimal);
                    break;
                case ColumnNameIndex.BPS:
                    dc.DataType = typeof(Decimal);
                    break;
                case ColumnNameIndex.매출액:
                    dc.DataType = typeof(Int64);
                    break;
                case ColumnNameIndex.영업이익:
                    dc.DataType = typeof(Int64);
                    break;
                case ColumnNameIndex.당기순이익:
                    dc.DataType = typeof(Int64);
                    break;
                case ColumnNameIndex.최고250:
                    dc.DataType = typeof(Int32);
                    break;
                case ColumnNameIndex.최저250:
                    dc.DataType = typeof(Int32);
                    break;
                case ColumnNameIndex.상한가:
                    dc.DataType = typeof(Int32);
                    break;

                case ColumnNameIndex.하한가:
                    dc.DataType = typeof(Int32);
                    break;
                case ColumnNameIndex.기준가:
                    dc.DataType = typeof(Int32);
                    break;
                case ColumnNameIndex.예상체결가:
                    dc.DataType = typeof(Int32);
                    break;
                case ColumnNameIndex.예상체결수량:
                    dc.DataType = typeof(Int32);
                    break;
                case ColumnNameIndex.최고가일250:
                    dc.DataType = typeof(Int32);
                    break;
                case ColumnNameIndex.최고가대비율250:
                    dc.DataType = typeof(Int32);
                    break;
                case ColumnNameIndex.최저가일250:
                    dc.DataType = typeof(Int32);
                    break;
                case ColumnNameIndex.최저가대비율250:
                    dc.DataType = typeof(Int32);
                    break;
                case ColumnNameIndex.거래대비:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.액면가단위:
                       dc.DataType = typeof(string);
                    break;

                default:
                    break;
            }

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