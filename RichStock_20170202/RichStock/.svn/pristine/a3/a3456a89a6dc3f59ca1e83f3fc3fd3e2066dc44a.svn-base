Public Class clsOrderDefine

    Public TradeGb(,) As String = {{"00", "지정가"}, {"03", "시장가"}, {"05", "조건부지정가"}, {"06", "최유리지정가"}, {"07", "최우선지정가"}, _
                                    {"10", "지정가IOC"}, {"13", "시장가IOC"}, {"16", "최유리IOC"}, {"20", "지정가FOK"}, {"23", "시장가FOK"}, _
                                    {"26", "최유리FOK"}, {"61", "장전시간외종가"}, {"62", "시간외단일가매매"}, {"81", "장후시간외종가"}}

    Public ChejanFidList(,) As String = {{"9201", "계좌번호"}, {"9203", "주문번호"}, {"9001", "종목코드"}, {"913", "주문상태"}, {"302", "종목명"}, {"900", "주문수량"}, _
                                        {"901", "주문가격"}, {"902", "미체결수량"}, {"903", "체결누계금액"}, {"904", "원주문번호"}, {"905", "주문구분"}, {"906", "매매구분"}, _
                                        {"907", "매도수구분"}, {"908", "주문/체결시간"}, {"909", "체결번호"}, {"910", "체결가"}, {"911", "체결량"}, {"10", "현재가"}, _
                                        {"27", "(최우선)매도호가"}, {"28", "(최우선)매수호가"}, {"914", "단위체결가"}, {"915", "단위체결량"}, {"919", "거부사유"}, _
                                        {"920", "화면번호"}, {"917", "신용구분"}, {"916", "대출일"}, {"930", "보유수량"}, {"931", "매입단가"}, {"932", "총매입가"}, _
                                        {"933", "주문가능수량"}, {"945", "당일순매수수량"}, {"946", "매도/매수구분"}, {"950", "당일총매도손일"}, {"951", "예수금"}, _
                                        {"307", "기준가"}, {"8019", "손익율"}, {"957", "신용금액"}, {"958", "신용이자"}, {"918", "만기일"}, {"990", "당일실현손익(유가)"}, _
                                        {"991", "당일실현손익률(유가)"}, {"992", "당일실현손익(신용)"}, {"993", "당일실현손익률(신용)"}, {"397", "파생상품거래단위"}, _
                                        {"305", "상한가"}, {"306", "하한가"}}



    Public Function GetTradeGb() As DataSet
        Dim dt As New DataTable("TradeGb")
        Dim dr As DataRow, returnDs As New DataSet

        With dt.Columns
            .Add("TRADE_GB")
            .Add("TRADE_NAME")

            For i As Integer = 0 To UBound(TradeGb)
                dr = dt.Rows.Add()

                dr("TRADE_GB") = TradeGb(i, 0)
                dr("TRADE_NAME") = TradeGb(i, 1)
            Next

        End With

        returnDs.Tables.Add(dt)

        Return returnDs

    End Function

    Public Function GetChejanFidList(ByVal AxKH As AxKHOpenAPILib.AxKHOpenAPI, ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveChejanDataEvent) As DataSet
        Dim dt As New DataTable("ChejanFidList")
        Dim dr As DataRow, returnDs As New DataSet

        With dt.Columns

            For i As Integer = 0 To UBound(ChejanFidList)
                .Add(ChejanFidList(i, 1))
            Next

            dr = dt.Rows.Add

            For i As Integer = 0 To UBound(ChejanFidList)
                dr(ChejanFidList(i, 1)) = AxKH.GetChejanData(CInt(ChejanFidList(i, 0)))
            Next

        End With

        returnDs.Tables.Add(dt)

        Return returnDs

    End Function

End Class
