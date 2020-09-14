﻿Public Class clsKiwoomRealDefine

    Sub New()

    End Sub


    Public OptKWFid As String() = {"종목코드", "종목명", "현재가", "기준가", "전일대비", "전일대비기호", "등락율", "거래량", _
                                "거래대금", "체결량", "체결강도", "전일거래량대비", _
                                "매도호가", "매수호가", "매도1차호가", "매도2차호가", "매도3차호가", "매도4차호가", "매도5차호가", _
                                "매수1차호가", "매수2차호가", "매수3차호가", "매수4차호가", "매수5차호가", _
                                "상한가", "하한가", "시가", "고가", "저가", "종가", "체결시간", "예상체결가", "예상체결량", _
                                "자본금", "액면가", "시가총액", "주식수", "호가시간", "일자", _
                                "우선매도잔량", "우선매수잔량", "우선매도건수", "우선매수건수", _
                                "총매도잔량", "총매수잔량", "총매도건수", "총매수건수", "패리티", "기어링", "손익분기", "자본지지", _
                                "ELW행사가", "전환비율", "ELW만기일", "미결제약정", "미결제전일대비", "이론가", "내재변동성", _
                                "델타", "감마", "쎄타", "베가", "로"}

    Public StockSise As String(,) = {{"10", "현재가"}, {"11", "전일대비"}, {"12", "등락율"}, {"27", "(최우선)매도호가"}, _
                                        {"28", "(최우선)매수호가"}, {"13", "누적거래량"}, {"14", "누적거래대금"}, {"16", "시가"}, _
                                        {"17", "고가"}, {"18", "저가"}, {"25", "전일대비기호"}, {"26", "전일거래량대비(계약,주)"}, _
                                        {"29", "거래대금증감"}, {"30", "전일거래량대비(비율)"}, {"31", "거래회전율"}, {"32", "거래비용"}, _
                                        {"311", "시가총액"}, {"567", "상한가발생시간"}, {"568", "하한가발생시간"}}

    Public StockTrade As String(,) = {{"20", "체결시간"}, {"10", "현재가"}, {"11", "전일대비"}, {"12", "등락율"}, {"27", "(최우선)매도호가"}, _
                                        {"28", "(최우선)매수호가"}, {"15", "거래량"}, {"13", "누적거래량"}, {"14", "누적거래대금"}, {"16", "시가"}, _
                                        {"17", "고가"}, {"18", "저가"}, {"25", "전일대비기호"}, {"26", "전일거래량대비(계약,주)"}, _
                                        {"29", "거래대금증감"}, {"30", "전일거래량대비(비율)"}, {"31", "거래회전율"}, {"32", "거래비용"}, _
                                        {"228", "체결강도"}, {"311", "시가총액"}, {"290", "장구분"}, {"691", "KO접근도"}, {"567", "상한가발생시간"}, {"568", "하한가발생시간"}}

    Public StockHogaJanQty As String(,) = {{"21", "호가시간"}, _
                                            {"41", "매도호가1"}, {"61", "매도호가수량1"}, {"81", "매도호가직전대비1"}, _
                                            {"51", "매수호가1"}, {"71", "매수호가수량1"}, {"91", "매수호가직전대비1"}, _
                                            {"42", "매도호가2"}, {"62", "매도호가수량2"}, {"82", "매도호가직전대비2"}, _
                                            {"52", "매수호가2"}, {"72", "매수호가수량2"}, {"92", "매수호가직전대비2"}, _
                                            {"43", "매도호가3"}, {"63", "매도호가수량3"}, {"83", "매도호가직전대비3"}, _
                                            {"53", "매수호가3"}, {"73", "매수호가수량3"}, {"93", "매수호가직전대비3"}, _
                                            {"44", "매도호가4"}, {"64", "매도호가수량4"}, {"84", "매도호가직전대비4"}, _
                                            {"54", "매수호가4"}, {"74", "매수호가수량4"}, {"94", "매수호가직전대비4"}, _
                                            {"45", "매도호가5"}, {"65", "매도호가수량5"}, {"85", "매도호가직전대비5"}, _
                                            {"55", "매수호가5"}, {"75", "매수호가수량5"}, {"95", "매수호가직전대비5"}, _
                                            {"46", "매도호가6"}, {"66", "매도호가수량6"}, {"86", "매도호가직전대비6"}, _
                                            {"56", "매수호가6"}, {"76", "매수호가수량6"}, {"96", "매수호가직전대비6"}, _
                                            {"47", "매도호가7"}, {"67", "매도호가수량7"}, {"87", "매도호가직전대비7"}, _
                                            {"57", "매수호가7"}, {"77", "매수호가수량7"}, {"97", "매수호가직전대비7"}, _
                                            {"48", "매도호가8"}, {"68", "매도호가수량8"}, {"88", "매도호가직전대비8"}, _
                                            {"58", "매수호가8"}, {"78", "매수호가수량8"}, {"98", "매수호가직전대비8"}, _
                                            {"49", "매도호가9"}, {"69", "매도호가수량9"}, {"89", "매도호가직전대비9"}, _
                                            {"59", "매수호가9"}, {"79", "매수호가수량9"}, {"99", "매수호가직전대비9"}, _
                                            {"50", "매도호가10"}, {"70", "매도호가수량10"}, {"90", "매도호가직전대비10"}, _
                                            {"60", "매수호가10"}, {"80", "매수호가수량10"}, {"100", "매수호가직전대비10"}, _
                                            {"23", "예상체결가"}, {"24", "예상체결수량"}, {"128", "순매수잔량"}, _
                                            {"129", "매수비율"}, {"138", "순매도잔량"}, {"139", "매도비율"}, _
                                            {"200", "예상체결가전일종가대비"}, {"201", "예상체결가전일종가대비등락율"}, {"238", "예상체결가전일종가대비기호"}, _
                                            {"291", "예상체결가2"}, {"292", "예상체결량"}, {"293", "예상체결가전일대비기호"}, {"294", "예상체결가전일대비등락률"}, _
                                            {"621", "LP매도호가수량1"}, {"631", "LP매수호가수량1"}, {"622", "LP매도호가수량2"}, {"632", "LP매수호가수량2"}, _
                                            {"623", "LP매도호가수량3"}, {"633", "LP매수호가수량3"}, {"624", "LP매도호가수량4"}, {"634", "LP매수호가수량4"}, _
                                            {"625", "LP매도호가수량5"}, {"635", "LP매수호가수량5"}, {"626", "LP매도호가수량6"}, {"636", "LP매수호가수량6"}, _
                                            {"627", "LP매도호가수량7"}, {"637", "LP매수호가수량7"}, {"628", "LP매도호가수량8"}, {"638", "LP매수호가수량8"}, _
                                            {"629", "LP매도호가수량9"}, {"639", "LP매수호가수량9"}, {"630", "LP매도호가수량10"}, {"640", "LP매수호가수량10"}, _
                                            {"13", "누적거래량"}, {"299", "전일거래량대비예상체결률"}, {"215", "장운영구분"}, {"216", "투자자별ticker"}}

    Public StockTradePort As String(,) = {{"9001", "종목코드,업종코드"}, {"9026", "회원사코드(거래원)"}, {"302", "종목명"}, {"334", "거래원명"}, {"20", "체결시간"}, _
                                          {"203", "매도증감"}, {"207", "매수증감"}, {"210", "순매수수량"}, {"211", "순매수수량증감"}, {"260", "매매구분Text"}, _
                                          {"337", "거래소구분"}, {"10", "현재가"}, {"11", "전일대비"}, {"12", "등락율"}, {"25", "전일대비기호"}}

    Public TodayStockTradeAt As String(,) = {{"141", "매도거래원1"}, {"161", "매도거래원수량1"}, {"166", "매도거래원별증감1"}, {"146", "매도거래원코드1"}, {"271", "매도거래원색깔1"}, _
                                            {"151", "매수거래원1"}, {"171", "매수거래원수량1"}, {"176", "매수거래원별증감1"}, {"156", "매수거래원코드1"}, {"281", "매수거래원색깔1"}, _
                                            {"142", "매도거래원2"}, {"162", "매도거래원수량2"}, {"167", "매도거래원별증감2"}, {"147", "매도거래원코드2"}, {"272", "매도거래원색깔2"}, _
                                            {"152", "매수거래원2"}, {"172", "매수거래원수량2"}, {"177", "매수거래원별증감2"}, {"157", "매수거래원코드2"}, {"282", "매수거래원색깔2"}, _
                                            {"143", "매도거래원3"}, {"163", "매도거래원수량3"}, {"168", "매도거래원별증감3"}, {"148", "매도거래원코드3"}, {"273", "매도거래원색깔3"}, _
                                            {"153", "매수거래원3"}, {"173", "매수거래원수량3"}, {"178", "매수거래원별증감3"}, {"158", "매수거래원코드3"}, {"283", "매수거래원색깔3"}, _
                                            {"144", "매도거래원4"}, {"164", "매도거래원수량4"}, {"169", "매도거래원별증감4"}, {"149", "매도거래원코드4"}, {"274", "매도거래원색깔4"}, _
                                            {"154", "매수거래원4"}, {"174", "매수거래원수량4"}, {"179", "매수거래원별증감4"}, {"159", "매수거래원코드4"}, {"284", "매수거래원색깔4"}, _
                                            {"145", "매도거래원5"}, {"165", "매도거래원수량5"}, {"170", "매도거래원별증감5"}, {"150", "매도거래원코드5"}, {"275", "매도거래원색깔5"}, _
                                            {"155", "매수거래원5"}, {"175", "매수거래원수량5"}, {"180", "매수거래원별증감5"}, {"160", "매수거래원코드5"}, {"285", "매수거래원색깔5"}, _
                                            {"261", "외국계매도추정합"}, {"262", "외국계매도추정합변동"}, {"263", "외국계매수추정합"}, {"264", "외국계매수추정합변동"}}

#Region " Fid "
    Public Fid As String(,) = {{"10", "현재가"}, {"11", "전일대비"}, {"12", "등락율"}, {"13", "누적거래량"}, {"14", "누적거래대금"}, {"15", "거래량"}, _
                             {"16", "시가"}, {"17", "고가"}, {"18", "저가"}, {"20", "체결시간"}, {"21", "호가시간"}, {"23", "예상체결가"}, _
                             {"24", "예상체결수량"}, {"25", "전일대비기호"}, {"26", "전일거래량대비(계약,주)"}, {"27", "(최우선)매도호가"}, _
                             {"28", "(최우선)매수호가"}, {"29", "거래대금증감"}, {"30", "전일거래량대비(비율)"}, {"31", "거래회전율"}, _
                             {"32", "거래비용"}, {"41", "매도호가1"}, {"42", "매도호가2"}, {"43", "매도호가3"}, {"44", "매도호가4"}, {"45", "매도호가5"}, _
                             {"46", "매도호가6"}, {"47", "매도호가7"}, {"48", "매도호가8"}, {"49", "매도호가9"}, {"50", "매도호가10"}, _
                             {"51", "매수호가1"}, {"52", "매수호가2"}, {"53", "매수호가3"}, {"54", "매수호가4"}, {"55", "매수호가5"}, {"56", "매수호가6"}, _
                             {"57", "매수호가7"}, {"58", "매수호가8"}, {"59", "매수호가9"}, {"60", "매수호가10"}, _
                             {"61", "매도호가수량1"}, {"62", "매도호가수량2"}, {"63", "매도호가수량3"}, {"64", "매도호가수량4"}, {"65", "매도호가수량5"}, _
                             {"66", "매도호가수량6"}, {"67", "매도호가수량7"}, {"68", "매도호가수량8"}, {"69", "매도호가수량9"}, {"70", "매도호가수량10"}, _
                             {"71", "매수호가수량1"}, {"72", "매수호가수량2"}, {"73", "매수호가수량3"}, {"74", "매수호가수량4"}, {"75", "매수호가수량5"}, {"76", "매수호가수량6"}, _
                             {"77", "매수호가수량7"}, {"78", "매수호가수량8"}, {"79", "매수호가수량9"}, {"80", "매수호가수량10"}, {"81", "매도호가직전대비1"}, _
                             {"82", "매도호가직전대비2"}, {"83", "매도호가직전대비3"}, {"84", "매도호가직전대비4"}, {"85", "매도호가직전대비5"}, {"86", "매도호가직전대비6"}, _
                             {"87", "매도호가직전대비7"}, {"88", "매도호가직전대비8"}, {"89", "매도호가직전대비9"}, {"90", "매도호가직전대비10"}, {"91", "매수호가직전대비1"}, _
                             {"92", "매수호가직전대비2"}, {"93", "매수호가직전대비3"}, {"94", "매수호가직전대비4"}, {"95", "매수호가직전대비5"}, {"96", "매수호가직전대비6"}, {"97", "매수호가직전대비7"}, _
                             {"98", "매수호가직전대비8"}, {"99", "매수호가직전대비9"}, {"100", " 매수호가직전대비10"}, {"121", " 매도호가총잔량"}, {"122", " 매도호가총잔량직전대비"}, _
                             {"125", " 매수호가총잔량"}, {"126", " 매수호가총잔량직전대비"}, {"128", " 순매수잔량"}, {"129", " 매수비율"}, {"131", " 시간외매도호가총잔량"}, _
                             {"132", " 시간외매도호가총잔량직전대비"}, {"135", " 시간외매수호가총잔량"}, {"136", " 시간외매수호가총잔량직전대비"}, {"138", " 순매도잔량"}, _
                             {"139", " 매도비율"}, {"141", " 매도거래원1"}, {"142", " 매도거래원2"}, {"143", " 매도거래원3"}, {"144", " 매도거래원4"}, {"145", " 매도거래원5"}, _
                             {"146", " 매도거래원코드1"}, {"147", " 매도거래원코드2"}, {"148", " 매도거래원코드3"}, {"149", " 매도거래원코드4"}, {"150", " 매도거래원코드5"}, _
                             {"151", " 매수거래원1"}, {"152", " 매수거래원2"}, {"153", " 매수거래원3"}, {"154", " 매수거래원4"}, {"155", " 매수거래원5"}, _
                             {"156", " 매수거래원코드1"}, {"157", " 매수거래원코드2"}, {"158", " 매수거래원코드3"}, {"159", " 매수거래원코드4"}, {"160", " 매수거래원코드5"}, _
                             {"161", " 매도거래원수량1"}, {"162", " 매도거래원수량2"}, {"163", " 매도거래원수량3"}, {"164", " 매도거래원수량4"}, {"165", " 매도거래원수량5"}, _
                             {"166", " 매도거래원별증감1"}, {"167", " 매도거래원별증감2"}, {"168", " 매도거래원별증감3"}, {"169", " 매도거래원별증감4"}, {"170", " 매도거래원별증감5"}, _
                             {"171", " 매수거래원수량1"}, {"172", " 매수거래원수량2"}, {"173", " 매수거래원수량3"}, {"174", " 매수거래원수량4"}, {"175", " 매수거래원수량5"}, _
                             {"176", " 매수거래원별증감1"}, {"177", " 매수거래원별증감2"}, {"178", " 매수거래원별증감3"}, {"179", " 매수거래원별증감4"}, {"180", " 매수거래원별증감5"}, _
                             {"200", " 예상체결가전일종가대비"}, {"201", " 예상체결가전일종가대비등락율"}, {"202", " 매도수량"}, {"204", " 매도금액"}, {"206", " 매수수량"}, {"208", " 매수금액"}, _
                             {"210", " 순매수수량"}, {"212", " 순매수금액"}, {"213", " 순매수금액증감"}, {"214", " 장시작예상잔여시간"}, {"215", " 장운영구분"}, _
                             {"216", " 투자자별ticker"}, {"228", " 체결강도"}, {"238", " 예상체결가전일종가대비기호"}, {"261", " 외국계매도추정합"}, {"262", " 외국계매도추정합변동"}, _
                             {"263", " 외국계매수추정합"}, {"264", " 외국계매수추정합변동"}, {"267", " 외국계순매수추정합"}, {"268", " 외국계순매수변동"}, {"271", " 매도거래원색깔1"}, _
                             {"272", " 매도거래원색깔2"}, {"273", " 매도거래원색깔3"}, {"274", " 매도거래원색깔4"}, {"275", " 매도거래원색깔5"}, {"281", " 매수거래원색깔1"}, {"282", " 매수거래원색깔2"}, _
                             {"283", " 매수거래원색깔3"}, {"284", " 매수거래원색깔4"}, {"285", " 매수거래원색깔5"}, {"290", " 장구분"}, {"291", " 예상체결가"}, {"292", " 예상체결량"}, _
                             {"293", " 예상체결가전일대비기호"}, {"294", " 예상체결가전일대비"}, {"295", " 예상체결가전일대비등락율"}, _
                             {"299", " 전일거래량대비예상체결률"}, {"302", " 종목명"}, {"311", " 시가총액(억)"}, {"337", " 거래소구분"}, {"567", " 상한가발생시간"}, {"568", " 하한가발생시간"}, _
                             {"621", " LP매도호가수량1"}, {"622", " LP매도호가수량2"}, {"623", " LP매도호가수량3"}, {"624", " LP매도호가수량4"}, {"625", " LP매도호가수량5"}, _
                             {"626", " LP매도호가수량6"}, {"627", " LP매도호가수량7"}, {"628", " LP매도호가수량8"}, {"629", " LP매도호가수량9"}, {"630", " LP매도호가수량10"}, _
                             {"631", " LP매수호가수량1"}, {"632", " LP매수호가수량2"}, {"633", " LP매수호가수량3"}, {"634", " LP매수호가수량4"}, {"635", " LP매수호가수량5"}, {"636", " LP매수호가수량6"}, _
                             {"637", " LP매수호가수량7"}, {"638", " LP매수호가수량8"}, {"639", " LP매수호가수량9"}, {"640", " LP매수호가수량10"}, {"691", " KO접근도"}, {"9001", "  종목코드,업종코드"}}

#End Region
 
    Public Function RealKiwoomDefineDic(ByVal AxKH As AxKHOpenAPILib.AxKHOpenAPI, ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEvent) As DataSet

        Dim returnDs As New DataSet
        Dim nCnt As Integer = 0

        Select Case e.sRealType
            Case "주식시세"
                returnDs = GetStockSise(AxKH, e)
            Case "주식체결"
                returnDs = GetStockTrade(AxKH, e)
            Case "주식우선호가"

            Case "주식호가잔량"
                returnDs = GetStockHogaJanQty(AxKH, e)
            Case "주식시간외호가"

            Case "주식당일거래원"
                returnDs = GetTodayStockTradeAt(AxKH, e)
            Case "주식거래원"
                returnDs = GetStockTradePort(AxKH, e)

        End Select

        Return returnDs

    End Function

    Private Function GetStockSise(ByVal AxKH As AxKHOpenAPILib.AxKHOpenAPI, ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEvent) As DataSet
        Dim dt As New DataTable("Default")
        Dim dt2New As New DataTable("StockSise")
        Dim dr As DataRow, returnDs As New DataSet
        Dim dr2th As DataRow

        With dt.Columns
            For i As Integer = 0 To UBound(StockSise)
                .Add(StockSise(i, 0))
            Next

            dr = dt.Rows.Add()

            For intColumnName As Integer = 0 To .Count - 1
                dr(dt.Columns.Item(intColumnName).ColumnName) = AxKH.GetCommRealData(e.sRealType, CInt(dt.Columns.Item(intColumnName).ColumnName))
            Next

        End With

        With dt2New.Columns
            .Add("sRealType")
            .Add("STOCK_CODE")

            For i As Integer = 0 To UBound(StockSise)
                .Add(StockSise(i, 1))
            Next

            dr2th = dt2New.Rows.Add

            For i As Integer = 0 To UBound(StockSise)
                If Trim(dt.Columns.Item(i).ColumnName) = StockTrade(i, 0) Then
                    dr2th(StockTrade(i, 1)) = Trim(dt.Rows(0).Item(dt.Columns.Item(i)).ToString)
                End If
            Next

            dr2th("sRealType") = e.sRealType

            dr2th("STOCK_CODE") = e.sRealKey

        End With

        returnDs.Tables.Add(dt2New)

        Return returnDs

    End Function

    Private Function GetStockTrade(ByVal AxKH As AxKHOpenAPILib.AxKHOpenAPI, ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEvent) As DataSet
        Dim dt As New DataTable("Default")
        Dim dt2New As New DataTable("StockTrade")
        Dim dr As DataRow, returnDs As New DataSet
        Dim dr2th As DataRow

        With dt.Columns
            For i As Integer = 0 To UBound(StockTrade)
                .Add(StockTrade(i, 0))
            Next

            dr = dt.Rows.Add()

            For intColumnName As Integer = 0 To .Count - 1
                dr(dt.Columns.Item(intColumnName).ColumnName) = AxKH.GetCommRealData(e.sRealType, CInt(dt.Columns.Item(intColumnName).ColumnName))
            Next

        End With

        With dt2New.Columns
            .Add("sRealType")
            .Add("STOCK_CODE")

            For i As Integer = 0 To UBound(StockTrade)
                .Add(StockTrade(i, 1))
            Next

            dr2th = dt2New.Rows.Add

            For i As Integer = 0 To UBound(StockTrade)
                If Trim(dt.Columns.Item(i).ColumnName) = StockTrade(i, 0) Then
                    dr2th(StockTrade(i, 1)) = Trim(dt.Rows(0).Item(dt.Columns.Item(i)).ToString)
                End If
            Next

            dr2th("sRealType") = e.sRealType
            dr2th("STOCK_CODE") = e.sRealKey

        End With

        returnDs.Tables.Add(dt2New)

        Return returnDs

    End Function

    Private Function GetTodayStockTradeAt(ByVal AxKH As AxKHOpenAPILib.AxKHOpenAPI, ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEvent) As DataSet
        Dim dt As New DataTable("Default")
        Dim dt2New As New DataTable("TodayStockTradeAt ")
        Dim dr As DataRow, returnDs As New DataSet
        Dim dr2th As DataRow
        Try


            With dt.Columns
                For i As Integer = 0 To UBound(TodayStockTradeAt)
                    .Add(TodayStockTradeAt(i, 0))
                Next

                dr = dt.Rows.Add()

                For intColumnName As Integer = 0 To .Count - 1
                    dr(dt.Columns.Item(intColumnName).ColumnName) = AxKH.GetCommRealData(e.sRealType, CInt(dt.Columns.Item(intColumnName).ColumnName))
                Next

            End With

            With dt2New.Columns

                .Add("sRealType")
                .Add("STOCK_CODE")

                For i As Integer = 0 To UBound(TodayStockTradeAt)
                    .Add(TodayStockTradeAt(i, 1))
                Next

                dr2th = dt2New.Rows.Add

                For i As Integer = 0 To UBound(TodayStockTradeAt)
                    If Trim(dt.Columns.Item(i).ColumnName) = TodayStockTradeAt(i, 0) Then
                        dr2th(TodayStockTradeAt(i, 1)) = Trim(dt.Rows(0).Item(dt.Columns.Item(i)).ToString)
                    End If
                Next

                dr2th("sRealType") = e.sRealType
                dr2th("STOCK_CODE") = e.sRealKey

            End With

            returnDs.Tables.Add(dt2New)

            Return returnDs

        Catch ex As Exception
            MsgBox(ex.ToString())
            Return Nothing
        End Try

    End Function

    Private Function GetStockHogaJanQty(ByVal AxKH As AxKHOpenAPILib.AxKHOpenAPI, ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEvent) As DataSet
        Dim dt As New DataTable("Default")
        Dim dt2New As New DataTable("StockHogaJanQty ")
        Dim dr As DataRow, returnDs As New DataSet
        Dim dr2th As DataRow
        Try


            With dt.Columns
                For i As Integer = 0 To UBound(StockHogaJanQty)
                    .Add(StockHogaJanQty(i, 0))
                Next

                dr = dt.Rows.Add()

                For intColumnName As Integer = 0 To .Count - 1
                    dr(dt.Columns.Item(intColumnName).ColumnName) = AxKH.GetCommRealData(e.sRealType, CInt(dt.Columns.Item(intColumnName).ColumnName))
                Next

            End With

            With dt2New.Columns

                .Add("sRealType")
                .Add("STOCK_CODE")

                For i As Integer = 0 To UBound(StockHogaJanQty)
                    .Add(StockHogaJanQty(i, 1))
                Next

                dr2th = dt2New.Rows.Add

                For i As Integer = 0 To UBound(StockHogaJanQty)
                    If Trim(dt.Columns.Item(i).ColumnName) = StockHogaJanQty(i, 0) Then
                        dr2th(StockHogaJanQty(i, 1)) = Trim(dt.Rows(0).Item(dt.Columns.Item(i)).ToString)
                    End If
                Next

                dr2th("sRealType") = e.sRealType
                dr2th("STOCK_CODE") = e.sRealKey

            End With

            returnDs.Tables.Add(dt2New)

            Return returnDs

        Catch ex As Exception
            MsgBox(ex.ToString())
            Return Nothing
        End Try

    End Function

    Private Function GetStockTradePort(ByVal AxKH As AxKHOpenAPILib.AxKHOpenAPI, ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEvent) As DataSet
        Dim dt As New DataTable("Default")
        Dim dt2New As New DataTable("StockTradePort")
        Dim dr As DataRow, returnDs As New DataSet
        Dim dr2th As DataRow

        With dt.Columns
            For i As Integer = 0 To UBound(StockTradePort)
                .Add(StockTradePort(i, 0))
            Next

            dr = dt.Rows.Add()

            For intColumnName As Integer = 0 To .Count - 1
                dr(dt.Columns.Item(intColumnName).ColumnName) = AxKH.GetCommRealData(e.sRealType, CInt(dt.Columns.Item(intColumnName).ColumnName))
            Next

        End With

        With dt2New.Columns
            .Add("sRealType")
            .Add("STOCK_CODE")

            For i As Integer = 0 To UBound(StockTradePort)
                .Add(StockTradePort(i, 1))
            Next

            dr2th = dt2New.Rows.Add

            For i As Integer = 0 To UBound(StockTradePort)
                If Trim(dt.Columns.Item(i).ColumnName) = StockTrade(i, 0) Then
                    dr2th(StockTradePort(i, 1)) = Trim(dt.Rows(0).Item(dt.Columns.Item(i)).ToString)
                End If
            Next

            dr2th("sRealType") = e.sRealType

            dr2th("STOCK_CODE") = e.sRealKey

        End With

        returnDs.Tables.Add(dt2New)

        Return returnDs

    End Function


End Class
