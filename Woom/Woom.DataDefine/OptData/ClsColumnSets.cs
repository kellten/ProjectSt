using System;
using System.Data;

namespace Woom.DataDefine.OptData
{
    public class ClsColumnSets : IDisposable
    {
        public enum Column10001Index
        {
            종목코드, 종목명, 결산월, 액면가, 자본금, 상장주식, 신용비율, 연중최고, 연중최저, 시가총액, 시가총액비중, 외인소진률, 대용가, PER, EPS, ROE, PBR, EV, BPS, 매출액, 영업이익, 당기순이익, 최고250,
            최저250, 시가, 고가, 저가, 상한가, 하한가, 기준가, 예상체결가, 예상체결수량, 최고가일250, 최고가대비율250, 최저가일250, 최저가대비율250, 현재가, 대비기호, 전일대비, 등락율, 거래량, 거래대비, 액면가단위, 유통주식, 유통비율
        }

        public enum Column10005Index
        {
            날짜, 시가, 고가, 종가, 대비, 등락률, 거래량, 거래대금, 체결강도, 외인보유, 외인비중, 외인순매수, 기관순매수, 개인순매수, 외국계, 신용잔고율, 프로그램
        }

        public enum Column10015Index
        {
            일자, 종가, 전일대비기호, 전일대비, 등락율, 거래량, 거래대금, 장전거래량, 장전거래비중, 장중거래량, 장중거래비중, 장후거래량, 장후거래비중
           , 합계3, 기간중거래량, 장전거래대금, 장전거래대금비중
           , 장중거래대금, 장중거래대금비중, 장후거래대금, 장후거래대금비중
        }

        #region Enum

        public enum Column10014Index
        {
            일자, 종가, 전일대비기호, 전일대비, 등락율, 거래량, 공매도량, 매매비중, 공매도거래대금, 공매도평균가
        }

        public enum Column10059Index
        {
            일자, 현재가, 대비기호, 전일대비, 등락율, 누적거래대금, 개인투자자,
            외국인투자자, 기관계, 금융투자, 보험, 투신, 기타금융, 은행,
            연기금등, 사모펀드, 국가, 기타법인, 내외국인
        }

        #endregion Enum

        public enum Column10081Index
        {
            종목코드, 현재가, 거래량, 거래대금, 일자, 시가, 고가, 저가,
            수정주가구분, 수정비율, 대업종구분, 소업종구분, 종목정보,
            수정주가이벤트
            //, 전일종가
        }

        public enum Column10086Index
        {
            날짜, 시가, 고가, 저가, 종가, 전일비, 등락률, 거래량, 금액백만, 신용비, 개인, 기관, 외인수량, 외국계, 프로그램, 외인비, 체결강도, 외인보유, 외인비중, 외인순매수, 개인순매수, 신용잔고율
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

        public enum Column90002Index
        {
            종목코드, 종목명
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
            날짜, 전일비, 등락률, 금액백만, 신용비, 개인, 기관, 외인수량, 외국계, 프로그램, 외인비, 체결강도, 기관순매수, 외인보유, 외인비중, 외인순매수, 개인순매수, 신용잔고율, 대비,
            장전거래량, 장전거래비중, 장중거래량, 장중거래비중, 장후거래량, 장후거래비중
           , 합계3, 기간중거래량, 장전거래대금, 장전거래대금비중
           , 장중거래대금, 장중거래대금비중, 장후거래대금, 장후거래대금비중, 유통주식, 유통비율
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

                case "최저가일250":
                    dc.ColumnName = "250최저가일";
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

                case ColumnNameIndex.기관순매수:
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

                case ColumnNameIndex.금액백만:
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
                    dc.DataType = typeof(string);
                    break;

                case ColumnNameIndex.외인보유:
                    dc.DataType = typeof(string);
                    break;

                case ColumnNameIndex.외인비중:
                    dc.DataType = typeof(string);
                    break;

                case ColumnNameIndex.외인순매수:
                    dc.DataType = typeof(string);
                    break;

                case ColumnNameIndex.개인순매수:
                    dc.DataType = typeof(string);
                    break;

                case ColumnNameIndex.신용잔고율:
                    dc.DataType = typeof(string);
                    break;

                case ColumnNameIndex.대비:
                    dc.DataType = typeof(Int32);
                    break;

                case ColumnNameIndex.장전거래량:
                    dc.DataType = typeof(Int32);
                    break;

                case ColumnNameIndex.장전거래비중:
                    dc.DataType = typeof(string);
                    break;

                case ColumnNameIndex.장중거래량:
                    dc.DataType = typeof(Int32);
                    break;

                case ColumnNameIndex.장중거래비중:
                    dc.DataType = typeof(string);
                    break;

                case ColumnNameIndex.장후거래량:
                    dc.DataType = typeof(Int32);
                    break;

                case ColumnNameIndex.장후거래비중:
                    dc.DataType = typeof(string);
                    break;

                case ColumnNameIndex.합계3:
                    dc.DataType = typeof(Int64);
                    break;

                case ColumnNameIndex.기간중거래량:
                    dc.DataType = typeof(Int32);
                    break;

                case ColumnNameIndex.장전거래대금:
                    dc.DataType = typeof(Int32);
                    break;

                case ColumnNameIndex.장전거래대금비중:
                    dc.DataType = typeof(string);
                    break;

                case ColumnNameIndex.장중거래대금:
                    dc.DataType = typeof(Int32);
                    break;

                case ColumnNameIndex.장중거래대금비중:
                    dc.DataType = typeof(string);
                    break;

                case ColumnNameIndex.장후거래대금:
                    dc.DataType = typeof(Int32);
                    break;

                case ColumnNameIndex.장후거래대금비중:
                    dc.DataType = typeof(string);
                    break;
                case ColumnNameIndex.유통주식:
                    dc.DataType = typeof(int);
                    break;
                case ColumnNameIndex.유통비율:
                    dc.DataType = typeof(decimal);
                    break;
                default:
                    break;
            }

            return dc;
        }

        public string TableColumnToKorean(string value)
        {
            switch (value)
            {
                case "BALANCE_CNT": return "잔고주수";
                case "BALANCE_PRICE": return "잔고금액";
                case "BANK_PRICE": return "은행";
                case "BANK_QTY": return "은행";
                case "BIG_FLOW": return "큰 흐름";
                case "BOHUM_PRICE": return "보험";
                case "BOHUM_QTY": return "보험";
                case "BUBIN_PRICE": return "기타법인";
                case "BUBIN_QTY": return "기타법인";
                case "CALL_DATE": return "조회일자";
                case "CHART_GB": return "구분";
                case "CHG_JUGA_EVENT": return "수정주가이벤트";
                case "CHG_JUGA_GB": return "수정주가구분";
                case "CHG_RATE": return "수정비율";
                case "CREATE_DATE": return "생성일자";
                case "DATE_SEQNO": return "일자순서";
                case "END_DATE": return "종료일자";
                case "FORE_PRICE": return "외국인투자자";
                case "FORE_QTY": return "외국인투자자";
                case "FROM_DATE": return "시작일자";
                case "GAIN_PRICE": return "개인투자자";
                case "GAIN_QTY": return "개인투자자";
                case "GIGAN_PRICE": return "기관계";
                case "GIGAN_QTY": return "기관계";
                case "GIGAN_SUM_PRICE": return "기관계합";
                case "GIGAN_SUM_QTY": return "기관계합";
                case "GITA_PRICE": return "기타금융";
                case "GITA_QTY": return "기타금융";
                case "GUMY_PRICE": return "금융투자";
                case "GUMY_QTY": return "금융투자";
                case "HIGH_PRICE": return "고가";
                case "IOFORE_PRICE": return "내외국인";
                case "IOFORE_QTY": return "내외국인";
                case "KT_CODE": return "회원사코드";
                case "KT_NAME": return "회원사명";
                case "LOW_PRICE": return "저가";
                case "LT_CON_CNT": return "대차거래체결주수";
                case "LT_INCRE": return "대차거래증감";
                case "LT_REPAY_CNT": return "대차거래상환주수";
                case "MAEME_GB": return "매매구분";
                case "MAEME-GB": return "매수구분";
                case "MID_FLOW": return "중간흐름";
                case "MID_STOCK_INFO": return "정보";
                case "MIMA_GB": return "구분";
                case "NATION_PRICE": return "국가";
                case "NATION_QTY": return "국가";
                case "NOW_PRICE": return "현재가";
                case "NUJUK_TRDAEGUM": return "누적거래대금";
                case "OPT10001_TEXT": return "내역";
                case "SAMO_PRICE": return "사모펀드";
                case "SAMO_QTY": return "사모펀드";
                case "SEQ_NO": return "순서";
                case "SET_INFO": return "설정정보";
                case "SGROUP_CODE": return "그룹코드";
                case "SGROUP_INFO": return "그룹정보";
                case "SGROUP_NAME": return "그룹명";
                case "START_DATE": return "시작일자";
                case "START_PRICE": return "시가";
                case "STOCK_CODE": return "종목코드";
                case "STOCK_DATE": return "거래일자";
                case "STOCK_INFO": return "정보";
                case "STOCK_NAME": return "종목이름";
                case "TO_DATE": return "종료일자";
                case "TOSIN_PRICE": return "투신";
                case "TOSIN_QTY": return "투신";
                case "TRADE_DAEGUM": return "거래대금";
                case "TRADE_DATE": return "거래일";
                case "TRADE_QTY": return "거래량";
                case "YBJONG_CODE": return "업종코드";
                case "YBJONG_NAME": return "업종명";
                case "YEONGI_PRICE": return "연기금등";
                case "YEONGI_QTY": return "연기금등";
            }
            return "";
        }

        public string ColumnToKoreanOpt10015(string strValue)
        {
            switch (strValue)
            {
                case "종목": return "STOCK_CODE";
                case "일자": return "STOCK_DATE";
                case "종가": return "LAST_PRICE";
                case "전일대비기호": return "OAGO_DAEBI_SYMBOL";
                case "전일대비": return "OAGO_DAEBI";
                case "등락율": return "UPDOWN_RATE";
                case "거래량": return "TRADE_QTY";
                case "거래대금": return "TRADE_DAEGUM";
                case "장전거래량": return "BETRADE_QTY";
                case "장전거래비중": return "BETRADE_BIJUNG";
                case "장중거래량": return "INTRADE_QTY";
                case "장중거래비중": return "INTRADE_BIJUNG";
                case "장후거래량": return "AFTRADE_QTY";
                case "장후거래비중": return "	AFTRADE_BIJUNG";
                case "합계3": return "SUM3";
                case "기간중거래량": return "GITRADE_QTY";
                case "장전거래대금": return "BETRADE_DAEGUM";
                case "장전거래대금비중": return "BETRADE_DBIJUNG";
                case "장중거래대금": return "INTRADE_DAEGUM";
                case "장중거래대금비중": return "INTRADE_DBIJUNG";
                case "장후거래대금": return "AFTRADE_DAEGUM";
                case "장후거래대금비중": return "AFTRADE_DBIJUNG";
                case "STOCK_CODE": return "종목";
                case "STOCK_DATE":
                    return "일자";

                case "LAST_PRICE":
                    return "종가";

                case "OAGO_DAEBI_SYMBOL":
                    return "전일대비기호";

                case "OAGO_DAEBI":
                    return "전일대비";

                case "UPDOWN_RATE":
                    return "등락율";

                case "TRADE_QTY":
                    return "거래량";

                case "TRADE_DAEGUM":
                    return "거래대금";

                case "BETRADE_QTY":
                    return "장전거래량";

                case "BETRADE_BIJUNG":
                    return "장전거래비중";

                case "INTRADE_QTY":
                    return "장중거래량";

                case "INTRADE_BIJUNG":
                    return "장중거래비중";

                case "AFTRADE_QTY":
                    return "장후거래량";

                case "	AFTRADE_BIJUNG":
                    return "장후거래비중";
                case "SUM3":
                    return "합계3";

                case "GITRADE_QTY":
                    return "기간중거래량";

                case "BETRADE_DAEGUM":
                    return "장전거래대금";

                case "BETRADE_DBIJUNG":
                    return "장전거래대금비중";

                case "INTRADE_DAEGUM":
                    return "장중거래대금";

                case "INTRADE_DBIJUNG":
                    return "장중거래대금비중";

                case "AFTRADE_DAEGUM":
                    return "장후거래대금";

                case "AFTRADE_DBIJUNG":
                    return "장후거래대금비중";
            }

            return "";
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

                case "최저가일250":
                    dc.ColumnName = "250최저가일";
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