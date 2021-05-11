﻿Public Class clsKiwoomDefine

    Sub New()
        SetDicKiwoomDefine()
    End Sub

    ''' <summary>
    ''' [ opt10001 : 주식기본정보요청 ]
    ''' </summary>
    ''' <remarks>
    ''' 1. Open API 조회 함수 입력값을 설정합니다.
    ''' 종목코드 = 전문 조회할 종목코드
    ''' SetInputValue("종목코드"	,  "입력값 1");
    ''' 2. Open API 조회 함수를 호출해서 전문을 서버로 전송합니다.
    ''' CommRqData( "RQName"	,  "opt10001"	,  "0"	,  "화면번호"); 
    ''' </remarks>
    Public Opt10001 As String() = {"종목코드", "종목명", "결산월", "액면가", "자본금", "상장주식", "신용비율", _
                                  "연중최고", "연중최저", "시가총액", "시가총액비중", "외인소진률", "대용가", _
                                  "PER", "EPS", "ROE", "PBR", "EV", "BPS", "매출액", "영업이익", "당기순이익", "250최고", _
                                  "250최저", "시가", "고가", "저가", "상한가", "하한가", "기준가", "예상체결가", _
                                  "예상체결수량", "250최고가일", "250최고가대비율", "250최저가일", "250최저가대비율", _
                                  "현재가", "대비기호", "전일대비", "등락율", "거래량", "거래대비", "액면가단위"}
    ''' <summary>
    '''  [ OPT10002 : 주식거래원요청 ]
    ''' </summary>
    ''' <remarks>
    '''  1. Open API 조회 함수 입력값을 설정합니다.
    ''' 종목코드 = 전문 조회할 종목코드
    ''' SetInputValue("종목코드"	,  "입력값 1");
    '''  2. Open API 조회 함수를 호출해서 전문을 서버로 전송합니다.
    ''' CommRqData( "RQName"	,  "OPT10002"	,  "0"	,  "화면번호"); 
    ''' </remarks>
    Public Opt10002 As String() = {"종목코드", "종목명", "현재가", "등락부호", "기준가", _
                                   "전일대비", "등락률", _
                                   "매도거래원명1", "매도거래원1", "매도거래량1", "매수거래원명1", "매수거래원1", "매수거래량1", _
                                   "매도거래원명2", "매도거래원2", "매도거래량2", "매수거래원명2", "매수거래원2", "매수거래량2", _
                                   "매도거래원명3", "매도거래원3", "매도거래량3", "매수거래원명3", "매수거래원3", "매수거래량3", _
                                   "매도거래원명4", "매도거래원4", "매도거래량4", "매수거래원명4", "매수거래원4", "매수거래량4", _
                                   "매도거래원명5", "매도거래원5", "매도거래량5", "매수거래원명5", "매수거래원5", "매수거래량5"}


    '/********************************************************************/
    ' 1. Open API 조회 함수 입력값을 설정합니다.
    '	종목코드 = 전문 조회할 종목코드
    '	SetInputValue("종목코드"	,  "입력값 1");
    ' 2. Open API 조회 함수를 호출해서 전문을 서버로 전송합니다.
    '	CommRqData( "RQName"	,  "opt10003"	,  "0"	,  "화면번호"); 
    Public opt10003 As String() = {"종목코드", "시간", "현재가", "전일대비", "대비율", "우선매도호가단위", "우선매수호가단위", "체결거래량", _
                                "sign", "누적거래량", "누적거래대금", "체결강도"}

    Public Opt10004 As String() = {"호가잔량기준시간", _
                                    "매도10차선잔량대비", "매도10차선잔량", "매도10차선호가", "매도9차선잔량대비", "매도9차선잔량", "매도9차선호가", _
                                    "매도8차선잔량대비", "매도8차선잔량", "매도8차선호가", "매도7차선잔량대비", "매도7차선잔량", "매도7차선호가", _
                                    "매도6차선잔량대비", "매도6우선잔량", "매도6차선호가", "매도5차선잔량대비", "매도5차선잔량", "매도5차선호가", _
                                    "매도4차선잔량대비", "매도4차선잔량", "매도4차선호가", "매도3차선잔량대비", "매도3차선잔량", "매도3차선호가", _
                                    "매도2차선잔량대비", "매도2차선잔량", "매도2차선호가", "매도1차선잔량대비", "매도1차선잔량", "매도1차선호가", _
                                    "매도최우선잔량", "매도최우선호가", "매수최우선호가", "매수최우선잔량", _
                                    "매수1차선잔량대비", "매수1차선호가", "매수1차선잔량", "매수2차선잔량대비", "매수2차선호가", "매수2차선잔량", _
                                    "매수3차선잔량대비", "매수3차선호가", "매수3차선잔량", "매수4차선잔량대비", "매수4차선호가", "매수4차선잔량", _
                                    "매수5차선잔량대비", "매수5차선호가", "매수5차선잔량", "매수6차선잔량대비", "매수6우선호가", "매수6우선잔량", _
                                    "매수7차선잔량대비", "매수7차선호가", "매수7차선잔량", "매수8차선잔량대비", "매수8차선호가", "매수8차선잔량", _
                                    "매수9차선잔량대비", "매수9차선호가", "매수9차선잔량", "매수10차선잔량대비", "매수10차선호가", "매수10차선잔량", _
                                    "총매도잔량진적대비", "총매도잔량", "총매수잔량", "총매수잔량직전대비", _
                                    "시간외매도잔량대비", "시간외매도잔량", "시간외매수잔량", "시간외매수잔량대비"}

    Public Opt10005 As String() = {"날짜", "시가", "고가", "저가", "종가", "대비", "등락률", "거래량", "거래대금", _
                                   "체결강도", "외인보유", "외인비중", "외인순매수", "기관순매수", "개인순매수", "외국계", "신용잔고율", "프로그램"}

    '/********************************************************************/
    '[ OPT10006 : 주식시세요청 ]
    '1. Open API 조회 함수 입력값을 설정합니다.
    '종목코드 = 전문 조회할 종목코드
    'SetInputValue("종목코드"	,  "002140");
    '2. Open API 조회 함수를 호출해서 전문을 서버로 전송합니다.
    'CommRqData( "RQName"	,  "OPT10006"	,  "0"	,  "화면번호");     Public opt10003 As String() = {"종목코드", "시간", "현재가", "전일대비", "대비율", "우선매도호가단위", "우선매수호가단위", "체결거래량", _
    '                           "sign", "누적거래량", "누적거래대금", "체결강도"}
    Public opt10006 As String() = {"종목코드", "날짜", "시가", "고가", "저가", "종가", "대비", "등락률", _
                            "거래량", "거래대금", "체결강도"}

    Public opt10007 As String() = {"종목명", "종목코드", "날짜", "시간", "전일종가", "전일거래량", "상한가", "하한가", "전일거래대금", _
                                    "상장주식수", "현재가", "부호", "등락률", "전일비", "시가", "고가", "저가", "체결량", "거래량", _
                                    "거래대금", "예상체결가", "예상체결량", "예상매도우선호가", "예상매수우선호가", _
                                    "거래시작일", "행사가격", "최고가", "최저가", "최고가일", "최저가일", _
                                    "매도1호가", "매도2호가", "매도3호가", "매도4호가", "매도5호가", "매도6호가", "매도7호가", "매도8호가", "매도9호가", "매도10호가", _
                                    "매수1호가", "매수2호가", "매수3호가", "매수4호가", "매수5호가", "매수6호가", "매수7호가", "매수8호가", "매수9호가", "매수10호가", _
                                    "매도1호가잔량", "매도2호가잔량", "매도3호가잔량", "매도4호가잔량", "매도5호가잔량", _
                                    "매도6호가잔량", "매도7호가잔량", "매도8호가잔량", "매도9호가잔량", "매도10호가잔량", _
                                    "매수1호가잔량", "매수2호가잔량", "매수3호가잔량", "매수4호가잔량", "매수5호가잔량", _
                                    "매수6호가잔량", "매수7호가잔량", "매수8호가잔량", "매수9호가잔량", "매수10호가잔량", _
                                    "매도1호가직전대비", "매도2호가직전대비", "매도3호가직전대비", "매도4호가직전대비", "매도5호가직전대비", _
                                    "매도6호가직전대비", "매도7호가직전대비", "매도8호가직전대비", "매도9호가직전대비", "매도10호가직전대비", _
                                    "매수1호가직전대비", "매수2호가직전대비", "매수3호가직전대비", "매수4호가직전대비", "매수5호가직전대비", _
                                    "매수6호가직전대비", "매수7호가직전대비", "매수8호가직전대비", "매수9호가직전대비", "매수10호가직전대비", _
                                    "매도1호가건수", "매도2호가건수", "매도3호가건수", "매도4호가건수", "매도5호가건수", _
                                    "매수1호가건수", "매수2호가건수", "매수3호가건수", "매수4호가건수", "매수5호가건수", _
                                    "LP매도1호가잔량", "LP매도2호가잔량", "LP매도3호가잔량", "LP매도4호가잔량", "LP매도5호가잔량", _
                                    "LP매도6호가잔량", "LP매도7호가잔량", "LP매도8호가잔량", "LP매도9호가잔량", "LP매도10호가잔량", _
                                    "LP매수1호가잔량", "LP매수2호가잔량", "LP매수3호가잔량", "LP매수4호가잔량", "LP매수5호가잔량", _
                                    "LP매수6호가잔량", "LP매수7호가잔량", "LP매수8호가잔량", "LP매수9호가잔량", "LP매수10호가잔량", _
                                    "총매수잔량", "총매도잔량", "총매수건수", "총매도건수"}

    Public opt10008 As String() = {"일자", "종가", "전일대비", "거래량", "변동수량", "보유주식수", "비중", "취득가능주식수", "외국인한도", _
                                    "외국인한도증감", "한도소진율"}

    Public opt10009 As String() = {"날짜", "종가", "대비", "기관기간누적", "기관일변순매매", "외국인일변순매매", "외국인지분율"}

    Public opt10010 As String() = {"차익위탁매도수량", "차익위탁매도금액", "차익위탁매수수량", "차익위탁매수금액", _
                                "차익위탁순매수수량", "차익위탁순매수금액", "비차익위탁매도수량", "비차익위탁매도금액", _
                                "비차익위탁매수수량", "비차익위탁매수금액", "비차익위탁순매수수량", "비차익위탁순매수금액", _
                                "전체차익위탁매도수량", "전체차익위탁매도금액", "전체차익위탁매수수량", "전체차익위탁매수금액", _
                                "전체차익위탁순매수수량", "전체차익위탁순매수금액"}

    Public opt10011 As String() = {"코스피개인매도수량", "코스피개인매도금액", "코스피개인매수수량", "코스피개인매수금액", "코스피개인순매수수량", "코스피개인순매수금액",
                                   "코스피외국매도수량", "코스피외국매도금액", "코스피외국매수수량", "코스피외국매수금액", "코스피외국순매수수량", "코스피외국순매수금액",
                                   "코스피기관계매도수량", "코스피기관계매도금액", "코스피기관계매수수량", "코스피기관계매수금액", "코스피기관계순매수수량", "코스피기관계순매수금액",
                                   "코스피금융투자매도수량", "코스피금융투자매도금액", "코스피금융투자매수수량", "코스피금융투자매수금액", "코스피금융투자순매수수량", "코스피금융투자순매수금액",
                                   "코스피보험매도수량", "코스피보험매도금액", "코스피보험매수수량", "코스피보험매수금액", "코스피보험순매수수량", "코스피보험순매수금액",
                                   "코스피투자매도수량", "코스피투자매도금액", "코스피투자매수수량", "코스피투자매수금액", "코스피투자순매수수량", "코스피투자순매수금액",
                                   "코스닥개인매도수량", "코스닥개인매도금액", "코스닥개인매수수량", "코스닥개인매수금액", "코스닥개인순매수수량", "코스닥개인순매수금액",
                                   "코스닥개인매도수량", "코스닥개인매도금액", "코스닥개인매수수량", "코스닥개인매수금액", "코스닥개인순매수수량", "코스닥개인순매수금액",
                                   "코스닥외국매도수량", "코스닥외국매도금액", "코스닥외국매수수량", "코스닥외국매수금액", "코스닥외국순매수수량", "코스닥외국순매수금액",
                                   "코스닥기관계매도수량", "코스닥기관계매도금액", "코스닥기관계매수수량", "코스닥기관계매수금액", "코스닥기관계순매수수량", "코스닥기관계순매수금액",
                                   "코스닥금융투자매도수량", "코스닥금융투자매도금액", "코스닥금융투자매수수량", "코스닥금융투자매수금액", "코스닥금융투자순매수수량", "코스닥금융투자순매수금액",
                                   "코스닥보험매도수량", "코스닥보험매도금액", "코스닥보험매수수량", "코스닥보험매수금액", "코스닥보험순매수수량", "코스닥보험순매수금액",
                                   "코스닥투자매도수량", "코스닥투자매도금액", "코스닥투자매수수량", "코스닥투자매수금액", "코스닥투자순매수수량", "코스닥투자순매수금액",
                                   "코스닥개인매도수량", "코스닥개인매도금액", "코스닥개인매수수량", "코스닥개인매수금액", "코스닥개인순매수수량", "코스닥개인순매수금액"}

    Public opt10012 As String() = {"주문수량", "주문가격", "미체결수량", "체결누계금액", "원주문번호", "주문구분", "매매구분", "매도수구분", "주문/체결시간", _
                                    "체결가", "체결량", "주문상태", "단위체결가", "대출일", "신용구분", "만기일", "보유수량", "매입단가", "초매입가", _
                                    "주문가능수량", "당일매도수량", "당일매도금액", "당일매수수량", "당일매수금액", "당일매매수수료", "당일매매세금", _
                                    "당일hts매도수수료", "당일hts매수수수료", "당일매도손익", "당일순매수량", "매도/매수구분", "당일총매도손일", _
                                    "예수금", "사용가능현금", "사용가능대용", "전일재사용", "당일재사용", "담보현금", "신용금액", "신용이자", _
                                    "담보대출수량", "현물주문체결이상유무", "현물잔고이상유무", "현물예수금이상유무", "선물주문체결이상유무", _
                                    "선물잔고이상유무", "D+1추정예수금", "D+2추정예수금", "D+1매수/매도정산금", "D+2매수/매도정산금", _
                                    "D+1연체변제소요금", "D+2연체변제소요금", "D+1추정인출가능금", "D+2추정인출가능금", "현금증거금", "대용잔고", _
                                    "대용증거금", "수표금액", "현금미수금", "신용설정보증금", "인출가능금액"}

    Public opt10013 As String() = {"일자", "현재가", "전일대비기호", "전일대비", "거래량", "신규", "상환", "잔고", "금액", "대비", "공여율", "잔고율"}

    Public opt10014 As String() = {"일자", "종가", "전일대비기호", "전일대비", "등락율", "거래량", "공매도량", "매매비중", "공매도거래대금", "공매도평균가"}

    Public opt10015 As String() = {"일자", "종가", "전일대비기호", "전일대비", "등락율", "거래량", "거래대금", "장전거래량", "장전거래비중", "장중거래량", _
                                   "장중거래량", "장중거래비중", "장후거래량", "장후거래비중", "합계3", "기간중거래량", "체결강도", "외인보유", "외인비중", _
                                   "외인순매수", "기관순매수", "개인순매수", "외국계", "신용잔고율", "프로그램", "장전거래대금", "장전거래대금비중", _
                                   "장중거래대금", "장중거래대금비중", "장후거래대금", "장후거래대금지비중"}

    Public opt10059 As String() = {"일자", "현재가", "대비기호", "전일대비", "등락율", "누적거래대금", "개인투자자", _
                                   "외국인투자자", "기관계", "금융투자", "보험", "투신", "기타금융", "은행", _
                                   "연기금등", "사모펀드", "국가", "기타법인", "내외국인"}

    Public opt10059DataType As String() = {"System.String", "System.Int32", "System.String", "System.String", "System.String", "System.Int64", "System.Int32", _
                                   "System.Int32", "System.Int32", "System.Int32", "System.Int32", "System.Int32", "System.Int32", "System.Int32", _
                                   "System.Int32", "System.Int32", "System.Int32", "System.Int32", "System.Int32"}

    Public opt10060 As String() = {"일자", "현재가", "전일대비", "누적거래대금", "개인투자자", "외국인투자자", "기관계", "금융투자", "보험", "투신", "기타금융", "은행", _
                                   "연기금등", "사모펀드", "국가", "기타법인", "내외국인"}

    Public opt10060DataType As String() = {"System.String", "System.Int32", "System.String", "System.Int64", "System.Int32", _
                                 "System.Int32", "System.Int32", "System.Int32", "System.Int32", "System.Int32", "System.Int32", "System.Int32", _
                                 "System.Int32", "System.Int32", "System.Int32", "System.Int32", "System.Int32"}


    Public opt10061 As String() = {"개인투자자", "외국인투자자", "기관계", "금융투자", "보험", "투신", "기타금융", "은행", _
                                   "연기금등", "사모펀드", "국가", "기타법인", "내외국인"}

    Public opt10064 As String() = {"시간", "외국인투자자", "기관계", "투신", "보험", "은행", "연기금등", "기타법인", "국가"}
    Public opt10064DataType As String() = {"System.String", "System.Int32", "System.Int32", "System.Int32", "System.Int32", "System.Int32", "System.Int32", "System.Int32", "System.Int32"}

    Public Opt10078 As String() = {"일자", "현재가", "대비기호", "전일대비", "등락율", "누적거래량", "순매수량", "매수수량", "매도수량"}


    Public Opt10078DataType As String() = {"System.String", "System.Int32", "System.String", "System.String", "System.Int32", "System.Int32", "System.Int32", "System.Int32", "System.Int32"}

    ''' <summary>
    ''' [ opt10081 : 주식일봉차트조회요청 ]
    ''' </summary>
    ''' <remarks>
    ''' 1. Open API 조회 함수 입력값을 설정합니다.
    ''' 종목코드 = 전문 조회할 종목코드
    ''' SetInputValue("종목코드"	,  "입력값 1");
    ''' 기준일자 = YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)
    ''' SetInputValue("기준일자"	,  "입력값 2");
    ''' 수정주가구분 = 0 or 1, 수신데이터 1:유상증자, 2:무상증자, 4:배당락, 8:액면분할, 16:액면병합, 32:기업합병, 64:감자, 256:권리락
    ''' SetInputValue("수정주가구분"	,  "입력값 3");
    '''  2. Open API 조회 함수를 호출해서 전문을 서버로 전송합니다.
    ''' CommRqData( "RQName"	,  "opt10081"	,  "0"	,  "화면번호"); 
    '''</remarks>
    Public Opt10080 As String() = {"현재가", "거래량", "체결시간", "시가", "고가", "저가", _
                                   "수정주가구분", "수정비율", "대업종구분", "소업종구분", "종목정보", "수정주가이벤트", "전일종가"}

    Public Opt10081 As String() = {"종목코드", "현재가", "거래량", "거래대금", "일자", "시가", "고가", "저가", _
                                   "수정주가구분", "수정비율", "대업종구분", "소업종구분", "종목정보", "수정주가이벤트", "전일종가"}

    Public Opt10081DataType As String() = {"System.String", "System.Int32", "System.Int64", "System.Int64", "System.String", "System.Int32", "System.Int32", "System.Int32", _
                                   "System.String", "System.String", "System.String", "System.String", "System.String", "System.String", "System.String"}

    Public Opt10082 As String() = {"종목코드", "현재가", "거래량", "거래대금", "일자", "시가", "고가", "저가", _
                                   "수정주가구분", "수정비율", "대업종구분", "소업종구분", "종목정보", "수정주가이벤트", "전일종가"}

    Public Opt10082DataType As String() = {"System.String", "System.Int32", "System.Int64", "System.Int64", "System.String", "System.Int32", "System.Int32", "System.Int32", _
                                   "System.String", "System.String", "System.String", "System.String", "System.String", "System.String", "System.String"}

    ''' <summary>
    ''' [ OPT10038 : 종목별증권사순위요청 ]
    ''' </summary>
    ''' <remarks>
    ''' 1. Open API 조회 함수 입력값을 설정합니다.
    '''	종목코드 = 전문 조회할 종목코드
    ''' SetInputValue("종목코드"	,  "입력값 1");
    ''' 시작일자 = YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)
    '''	SetInputValue("시작일자"	,  "입력값 2");
    '''	종료일자 = YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)
    '''	SetInputValue("종료일자"	,  "입력값 3");
    '''	조회구분 = 0: 입력한 시작,종료 날짜로 조회, 1: 전일, 조회지정일 입력(5일 ~ 120일)
    '''	SetInputValue("조회구분"	,  "입력값 4");
    '''	SetInputValue("기간"	,  "입력값 5");
    ''' 2. Open API 조회 함수를 호출해서 전문을 서버로 전송합니다.
    '''	CommRqData( "RQName"	,  "OPT10038"	,  "0"	,  "화면번호"); 
    ''' </remarks>
    Public Opt10038 As String() = {"순위", "종목코드", "회원사명", "매수수량", "매도수량", "누적순매수수량", "순위1", "순위2", "순위3", "기간중거래량"}

    '    [ opw00018 : 계좌평가잔고내역요청 ]
    '1. Open API 조회 함수 입력값을 설정합니다.
    '계좌번호 = 전문 조회할 보유계좌번호
    'SetInputValue("계좌번호"	,  "입력값 1");
    '비밀번호 = 사용안함(공백)
    'SetInputValue("비밀번호"	,  "입력값 2");
    '비밀번호입력매체구분 = 00
    'SetInputValue("비밀번호입력매체구분"	,  "입력값 3");
    '조회구분 = 1:합산, 2:개별
    'SetInputValue("조회구분"	,  "입력값 4");
    '2. Open API 조회 함수를 호출해서 전문을 서버로 전송합니다.
    'CommRqData( "RQName"	,  "opw00018"	,  "0"	,  "화면번호"); 
    Public opw00018 As String() = {"종목번호", "종목명", "평가손익", "수익률(%)", "매입가", "전일종가", _
                                    "보유수량", "매매가능수량", "현재가", "전일매수수량", "전일매도수량", _
                                    "금일매수수량", "금일매도수량", "매입금액", "매입수수료", "평가금액", _
                                    "평가수수료", "세금", "수수료합", "보유비중(%)", "신용구분", "신용구분명", "대출일"}

    '    [ opt10085 : 계좌수익률요청 ]
    '1. Open API 조회 함수 입력값을 설정합니다.
    '계좌번호 = 전문 조회할 보유계좌번호
    'SetInputValue("계좌번호"	,  "입력값 1");
    '2. Open API 조회 함수를 호출해서 전문을 서버로 전송합니다.
    'CommRqData( "RQName"	,  "opt10085"	,  "0"	,  "화면번호"); 
    Public opt10085 As String() = {"일자", "종목코드", "종목명", "현재가", "매입가", "매입금액", "보유수량", _
                                "당일매도손익", "당일매매수수료", "당일매매세금", "신용구분", "대출일", _
                                "결제잔고", "청산가능수량", "신용금액", "신용이자", "만기일"}


    '[ opt10087 : 시간외단일가요청 ]
    '1. Open API 조회 함수 입력값을 설정합니다.
    'SetInputValue("종목코드"	,  "002140");
    '2. Open API 조회 함수를 호출해서 전문을 서버로 전송합니다.
    'CommRqData( "RQName"	,  "opt10087"	,  "0"	,  "화면번호"); 
    Public opt10087 As String() = {"호가잔량기준시간", _
                                   "시간외단일가_매도호가직전대비5", "시간외단일가_매도호가직전대비4", "시간외단일가_매도호가직전대비3", "시간외단일가_매도호가직전대비2", "시간외단일가_매도호가직전대비1", _
                                   "시간외단일가_매도호가수량5", "시간외단일가_매도호가수량4", "시간외단일가_매도호가수량3", "시간외단일가_매도호가수량2", "시간외단일가_매도호가수량1", _
                                   "시간외단일가_매도호가5", "시간외단일가_매도호가4", "시간외단일가_매도호가3", "시간외단일가_매도호가2", "시간외단일가_매도호가1", _
                                   "시간외단일가_매수호가1", "시간외단일가_매수호가2", "시간외단일가_매수호가3", "시간외단일가_매수호가4", "시간외단일가_매수호가5", _
                                   "시간외단일가_매수호가수량1", "시간외단일가_매수호가수량2", "시간외단일가_매수호가수량3", "시간외단일가_매수호가수량4", "시간외단일가_매수호가수량5", _
                                   "시간외단일가_매수호가직전대비1", "시간외단일가_매수호가직전대비2", "시간외단일가_매수호가직전대비3", "시간외단일가_매수호가직전대비4", "시간외단일가_매수호가직전대비5", _
                                   "시간외단일가_매도호가총잔량", "시간외단일가_매수호가총잔량", _
                                   "매도호가총잔량직전대비", "매도호가총잔량", "매수호가총잔량", "매수호가총잔량직전대비", _
                                   "시간외매도호가총잔량직전대비", "시간외매도호가총잔량", "시간외매수호가총잔량", "시간외매수호가총잔량직전대비" _
                                   }
    Public Opt10087DataType As String() = {"System.String", _
                                   "System.String", "System.String", "System.String", "System.String", "System.String", _
                                   "System.String", "System.String", "System.String", "System.String", "System.String", _
                                   "System.String", "System.String", "System.String", "System.String", "System.String", _
                                   "System.String", "System.String", "System.String", "System.String", "System.String", _
                                   "System.String", "System.String", "System.String", "System.String", "System.String", _
                                   "System.String", "System.String", "System.String", "System.String", "System.String", _
                                   "System.String", "System.String", _
                                   "System.String", "System.String", "System.String", "System.String", _
                                   "System.String", "System.String", "System.String", "System.String" _
                                   }


    '[ opt10075 : 실시간미체결요청 ]
    '1. Open API 조회 함수 입력값을 설정합니다.
    '계좌번호 = 전문 조회할 보유계좌번호
    'SetInputValue("계좌번호"	,  "입력값 1");
    '체결구분 = 0:전체, 1:종목
    'SetInputValue("체결구분"	,  "입력값 2");
    '매매구분 = 0:전체, 1:매도, 2:매수
    'SetInputValue("매매구분"	,  "입력값 3");
    '2. Open API 조회 함수를 호출해서 전문을 서버로 전송합니다.
    'CommRqData( "RQName"	,  "opt10075"	,  "0"	,  "화면번호"); 
    Public opt10075 As String() = {"계좌번호", "주문번호", "관리사번", "종목코드", "업무구분", "주문상태", "종목명", _
                                "주문수량", "주문가격", "미체결수량", "체결누계금액", "원주문번호", _
                                "주문구분", "매매구분", "시간", "체결번호", "체결가", _
                                "체결량", "현재가", "매도호가", "매수호가", "단위체결가", _
                                "단위체결량", "당일매매수수료", "당일매매세금", "개인투자자"}

    '[ opt20068 : 대차거래추이요청(종목별) ]'
    Public opt20068 As String() = {"일자", "대차거래체결주수", "대차거래상환주수", "잔고주수", "잔고금액"}
 
    Private DicKiwoomDefine As New Dictionary(Of KiwoomDefine, String)

    Public Enum KiwoomDefine
        Opt10001
        Opt10002
        Opt10003
        Opt10004
        Opt10005
        opt10006
        opt10007
        opt10008
        opt10009
        opt10010
        opt10011
        opt10012
        opt10013
        opt10014
        opt10015
        opt10059
        opt10060
        opt10061
        opt10064
        opt10078
        Opt10080
        Opt10081
        Opt10082
        Opt10038
        Opw00018
        opt10085
        opt10087
        opt10075
        opt20068
    End Enum

    Private Sub SetDicKiwoomDefine()
        DicKiwoomDefine.Add(KiwoomDefine.Opt10001, "opt10001[주식기본정보요청]")
        DicKiwoomDefine.Add(KiwoomDefine.Opt10002, "opt10002[주식거래원요청]")
        DicKiwoomDefine.Add(KiwoomDefine.Opt10003, "opt10003[체결정보요청]")
        DicKiwoomDefine.Add(KiwoomDefine.Opt10004, "opt10004[주식호가요청]")
        DicKiwoomDefine.Add(KiwoomDefine.Opt10005, "opt10005[주식일주월시분요청]")
        DicKiwoomDefine.Add(KiwoomDefine.opt10006, "opt10006[주식시세요청]")
        DicKiwoomDefine.Add(KiwoomDefine.opt10007, "opt10007[시세표성정보요청]")
        DicKiwoomDefine.Add(KiwoomDefine.opt10008, "opt10008[주식외국인요청]")
        DicKiwoomDefine.Add(KiwoomDefine.opt10009, "opt10009[주식기관요청]")
        DicKiwoomDefine.Add(KiwoomDefine.opt10010, "opt10010[업종프로그램요청]")
        DicKiwoomDefine.Add(KiwoomDefine.opt10011, "opt10011[투자자정보요청]")
        DicKiwoomDefine.Add(KiwoomDefine.opt10012, "opt10012[주문체결요청]")
        DicKiwoomDefine.Add(KiwoomDefine.opt10013, "opt10013[신용매매동향요청]")
        DicKiwoomDefine.Add(KiwoomDefine.opt10014, "opt10014[공매도추이요청]")
        DicKiwoomDefine.Add(KiwoomDefine.opt10015, "opt10015[일별거래상세요청]")
        DicKiwoomDefine.Add(KiwoomDefine.Opt10038, "opt10038[종목별증권사순위요청]")
        DicKiwoomDefine.Add(KiwoomDefine.opt10059, "opt10059[종목별투자자기관별요청]")
        DicKiwoomDefine.Add(KiwoomDefine.opt10060, "opt10060[종목별투자자기관별차트요청 ]")
        DicKiwoomDefine.Add(KiwoomDefine.opt10061, "opt10061[종목별투자자기관별합계요청]")
        DicKiwoomDefine.Add(KiwoomDefine.opt10064, "opt10064[장중투자자별매매차트요청]")
        DicKiwoomDefine.Add(KiwoomDefine.opt10075, "opt10075[실시간미체결요청]")
        DicKiwoomDefine.Add(KiwoomDefine.Opt10078, "opt10078[증권사별종목매매동향요청 ]")
        DicKiwoomDefine.Add(KiwoomDefine.Opt10080, "opt10080[주식분봉차트조회요청]")
        DicKiwoomDefine.Add(KiwoomDefine.Opt10081, "opt10081[주식일봉차트조회요청]")
        DicKiwoomDefine.Add(KiwoomDefine.Opt10082, "opt10082[주식주봉차트조회요청]")
        DicKiwoomDefine.Add(KiwoomDefine.opt10085, "opt10085[계좌수익률요청]")
        DicKiwoomDefine.Add(KiwoomDefine.opt10087, "opt10087[시간외단일가요청]")
        DicKiwoomDefine.Add(KiwoomDefine.opt20068, "opt20068[대차거래추이요청(종목별)]")
        DicKiwoomDefine.Add(KiwoomDefine.Opw00018, "Opw00018[계좌평가잔고내역요청]")
    End Sub

    Public Function KiwoomDefineDic(ByVal kiwoomDefine As KiwoomDefine, ByVal tableName As String, ByVal AxKH As AxKHOpenAPILib.AxKHOpenAPI, ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent) As DataSet
        Dim dt As New DataTable(tableName)
        Dim dr As DataRow, returnDs As New DataSet
        Dim nCnt As Integer = 0

        With dt.Columns
            Select Case DicKiwoomDefine(kiwoomDefine)
                Case DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.Opt10001)
                    For i As Integer = 0 To UBound(Opt10001)
                        .Add(Opt10001(i))
                    Next
                Case DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.Opt10002)
                    For i As Integer = 0 To UBound(Opt10002)
                        .Add(Opt10002(i))
                    Next
                Case DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.Opt10003)
                    For i As Integer = 0 To UBound(opt10003)
                        .Add(opt10003(i))
                    Next
                Case DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.Opt10004)
                    For i As Integer = 0 To UBound(Opt10004)
                        .Add(Opt10004(i))
                    Next
                Case DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.Opt10005)
                    For i As Integer = 0 To UBound(Opt10005)
                        .Add(Opt10005(i))
                    Next
                Case DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.opt10006)
                    For i As Integer = 0 To UBound(opt10006)
                        .Add(opt10006(i))
                    Next
                Case DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.opt10007)
                    For i As Integer = 0 To UBound(opt10007)
                        .Add(opt10007(i))
                    Next
                Case DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.opt10008)
                    For i As Integer = 0 To UBound(opt10008)
                        .Add(opt10008(i))
                    Next
                Case DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.opt10009)
                    For i As Integer = 0 To UBound(opt10009)
                        .Add(opt10009(i))
                    Next
                Case DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.opt10010)
                    For i As Integer = 0 To UBound(opt10010)
                        .Add(opt10010(i))
                    Next

                Case DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.opt10011)
                    For i As Integer = 0 To UBound(opt10011)
                        .Add(opt10011(i))
                    Next

                Case DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.opt10012)
                    For i As Integer = 0 To UBound(opt10012)
                        .Add(opt10012(i))
                    Next


                Case DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.opt10013)
                    For i As Integer = 0 To UBound(opt10013)
                        .Add(opt10013(i))
                    Next

                Case DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.opt10014)
                    For i As Integer = 0 To UBound(opt10014)
                        .Add(opt10014(i))
                    Next

                Case DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.opt10015)
                    For i As Integer = 0 To UBound(opt10015)
                        .Add(opt10015(i))
                    Next

                Case DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.opt10059)
                    For i As Integer = 0 To UBound(opt10059)
                        .Add(opt10059(i), System.Type.GetType(opt10059DataType(i)))
                    Next
                Case DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.opt10060)
                    For i As Integer = 0 To UBound(opt10060)
                        .Add(opt10060(i), System.Type.GetType(opt10060DataType(i)))
                    Next
                Case DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.opt10061)
                    For i As Integer = 0 To UBound(opt10061)
                        .Add(opt10061(i))
                    Next
                Case DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.opt10064)
                    For i As Integer = 0 To UBound(opt10064)
                        .Add(opt10064(i), System.Type.GetType(opt10064DataType(i)))
                    Next
                Case DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.opt10078)
                    For i As Integer = 0 To UBound(Opt10078)
                        .Add(Opt10078(i))
                    Next
                Case DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.Opt10080)
                    For i As Integer = 0 To UBound(Opt10080)
                        .Add(Opt10080(i))
                    Next
                Case DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.Opt10081)
                    For i As Integer = 0 To UBound(Opt10081)
                        .Add(Opt10081(i), System.Type.GetType(Opt10081DataType(i)))
                    Next
                Case DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.Opt10082)
                    For i As Integer = 0 To UBound(Opt10082)
                        .Add(Opt10082(i), System.Type.GetType(Opt10082DataType(i)))
                    Next
                Case DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.Opt10038)
                    For i As Integer = 0 To UBound(Opt10038)
                        .Add(Opt10038(i))
                    Next
                Case DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.Opw00018)
                    For i As Integer = 0 To UBound(opw00018)
                        .Add(opw00018(i))
                    Next
                Case DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.opt10075)
                    For i As Integer = 0 To UBound(opt10075)
                        .Add(opt10075(i))
                    Next
                Case DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.opt10085)
                    For i As Integer = 0 To UBound(opt10085)
                        .Add(opt10085(i))
                    Next
                Case DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.opt10087)
                    For i As Integer = 0 To UBound(opt10087)
                        .Add(opt10087(i), System.Type.GetType(Opt10087DataType(i)))
                    Next
                Case DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.opt20068)
                    For i As Integer = 0 To UBound(opt20068)
                        .Add(opt20068(i))
                    Next

            End Select
        End With

        nCnt = AxKH.GetRepeatCnt(e.sTrCode, e.sRQName)

        If kiwoomDefine = clsKiwoomDefine.KiwoomDefine.Opt10001 AndAlso nCnt = 0 Then '멀티데이터에서 싱글데이터로 변경됨
            nCnt = 1
        End If

        For i As Integer = 0 To (nCnt - 1)
            dr = dt.Rows.Add()

            For intColumnName As Integer = 0 To dt.Columns.Count - 1
                If DicKiwoomDefine(kiwoomDefine) = DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.Opt10003) OrElse DicKiwoomDefine(kiwoomDefine) = DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.opt10006) Then
                    If intColumnName = 0 Then
                        dr(dt.Columns.Item(intColumnName).ColumnName) = Right(e.sRQName.Trim(), 6)
                    Else
                        dr(dt.Columns.Item(intColumnName).ColumnName) = AxKH.GetCommData(e.sTrCode, e.sRQName, i, dt.Columns.Item(intColumnName).ColumnName).Trim()
                    End If
                Else
                    dr(dt.Columns.Item(intColumnName).ColumnName) = AxKH.GetCommData(e.sTrCode, e.sRQName, i, dt.Columns.Item(intColumnName).ColumnName).Trim()
                End If
            Next

            If DicKiwoomDefine(kiwoomDefine) = DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.Opt10081) Or DicKiwoomDefine(kiwoomDefine) = DicKiwoomDefine(clsKiwoomDefine.KiwoomDefine.Opt10080) Then
                If i > 0 Then
                    dt.Rows(i - 1)("전일종가") = dt.Rows(i)("현재가")
                End If
            End If
        Next

        returnDs.Tables.Add(dt)

        Return returnDs

    End Function

  

End Class