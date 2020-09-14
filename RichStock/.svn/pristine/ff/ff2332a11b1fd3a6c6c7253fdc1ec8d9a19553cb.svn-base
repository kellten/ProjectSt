Public Class frmTest

    Dim _clsCommConnect As PaikRichStock.Common.KiwomConnectionInfo()

    Private Sub frmTest_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click
        'If bLoginStatus = True Then
        '    AxKHOpenAPI2.CommTerminate()
        '    lblLoginStatus.Text = "로그아웃"
        '    lblStatus.Text = "OFF"

        '    bLoginStatus = False
        'Else
        '    lblLoginStatus.Text = "로그인"
        '    AxKHOpenAPI2.CommConnect()
        'End If




    End Sub

    Private Sub AxKHOpenAPI1_OnEventConnect(ByVal sender As Object, ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent) Handles AxKHOpenAPI2.OnEventConnect

        If e.nErrCode = 0 Then
            lblStatus.Text = "ON"
            lblLoginStatus.Text = "로그아웃"
            bLoginStatus = True
        Else
            lblLoginStatus.Text = "로그인 실패!!!"
        End If

    End Sub

    Private Sub btnStockInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStockInfo.Click
        Dim 종목코드, 종목명 As String

        ' 종목명 업데이트
        종목코드 = txtStock.Text

        ' 종목코드 길이 확인
        If 종목코드.Length <> 6 Then
            MsgBox("종목코드 6자를 입력 해 주세요~!")
            Exit Sub
        End If

        종목명 = AxKHOpenAPI2.GetMasterCodeName(종목코드)
        ListBoxResult.Items.Add(종목명)

        ' 조회 함수 값 입력
        AxKHOpenAPI2.SetInputValue("종목코드", 종목코드)

        ' 조회값 서버 전송
        AxKHOpenAPI2.CommRqData("주식기본정보", "OPT10001", 0, "3001")
    End Sub

#Region ""

    Private Sub AxKHOpenAPI2_OnReceiveTrData(ByVal sender As Object, ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent) Handles AxKHOpenAPI2.OnReceiveTrData


        If e.sRQName = "주식주문" Then    ' 주식주문 설정

            Exit Sub
        ElseIf e.sRQName = "주식기본정보" Then
            ListBoxResult.Items.Add("주식기본정보")
            Dim nCnt As Integer
            Dim strItemValue As String

            nCnt = AxKHOpenAPI2.GetRepeatCnt(e.sTrCode, e.sRQName)

            For i As Integer = 0 To (nCnt - 1)
                strItemValue = AxKHOpenAPI2.GetCommData(e.sTrCode, e.sRQName, i, "종목명")
                strItemValue.Trim()
                ListBoxResult.Items.Add("종목명 : " & strItemValue)

                strItemValue = AxKHOpenAPI2.GetCommData(e.sTrCode, e.sRQName, i, "시가총액")
                strItemValue.Trim()
                ListBoxResult.Items.Add("시가총액 : " & strItemValue)

                strItemValue = AxKHOpenAPI2.GetCommData(e.sTrCode, e.sRQName, i, "시가")
                strItemValue.Trim()
                ListBoxResult.Items.Add("시가 : " & strItemValue)

                strItemValue = AxKHOpenAPI2.GetCommData(e.sTrCode, e.sRQName, i, "고가")
                strItemValue.Trim()
                ListBoxResult.Items.Add("고가 : " & strItemValue)

                strItemValue = AxKHOpenAPI2.GetCommData(e.sTrCode, e.sRQName, i, "저가")
                strItemValue.Trim()
                ListBoxResult.Items.Add("저가 : " & strItemValue)

                strItemValue = AxKHOpenAPI2.GetCommData(e.sTrCode, e.sRQName, i, "기준가")
                strItemValue.Trim()
                ListBoxResult.Items.Add("기준가 : " & strItemValue)

                strItemValue = AxKHOpenAPI2.GetCommData(e.sTrCode, e.sRQName, i, "현재가")
                strItemValue.Trim()
                ListBoxResult.Items.Add("현재가 : " & strItemValue)

                strItemValue = AxKHOpenAPI2.GetCommData(e.sTrCode, e.sRQName, i, "대비기호")
                strItemValue.Trim()
                ListBoxResult.Items.Add("대비기호 : " & strItemValue)

                strItemValue = AxKHOpenAPI2.GetCommData(e.sTrCode, e.sRQName, i, "전일대비")
                strItemValue.Trim()
                ListBoxResult.Items.Add("전일대비 : " & strItemValue)

                strItemValue = AxKHOpenAPI2.GetCommData(e.sTrCode, e.sRQName, i, "등락율")
                strItemValue.Trim()
                ListBoxResult.Items.Add("등락율 : " & strItemValue)

                strItemValue = AxKHOpenAPI2.GetCommData(e.sTrCode, e.sRQName, i, "거래량")
                strItemValue.Trim()
                ListBoxResult.Items.Add("거래량 : " & strItemValue)
            Next

            ListBoxResult.Items.Add("===============================")

        ElseIf e.sRQName = "주식일봉차트조회" Then
            ListBoxResult.Items.Add("주식일봉차트조회")
            Dim nCnt As Integer
            Dim strItemValue As String

            nCnt = AxKHOpenAPI2.GetRepeatCnt(e.sTrCode, e.sRQName)

            For i As Integer = 0 To (nCnt - 1)

                ListBoxResult.Items.Add("-------------------------------")
                ListBoxResult.Items.Add("[index] =>" & i)

                'Label_일봉수.Text = CType(i, String)

                strItemValue = AxKHOpenAPI2.GetCommData(e.sTrCode, e.sRQName, i, "현재가")
                strItemValue.Trim()
                ListBoxResult.Items.Add("현재가 : " & strItemValue)

                strItemValue = AxKHOpenAPI2.GetCommData(e.sTrCode, e.sRQName, i, "거래량")
                strItemValue.Trim()
                ListBoxResult.Items.Add("거래량 : " & strItemValue)

                strItemValue = AxKHOpenAPI2.GetCommData(e.sTrCode, e.sRQName, i, "거래대금")
                strItemValue.Trim()
                ListBoxResult.Items.Add("거래대금 : " & strItemValue)

                strItemValue = AxKHOpenAPI2.GetCommData(e.sTrCode, e.sRQName, i, "일자")
                strItemValue.Trim()
                ListBoxResult.Items.Add("일자 : " & strItemValue)

                strItemValue = AxKHOpenAPI2.GetCommData(e.sTrCode, e.sRQName, i, "시가")
                strItemValue.Trim()
                ListBoxResult.Items.Add("시가 : " & strItemValue)

                strItemValue = AxKHOpenAPI2.GetCommData(e.sTrCode, e.sRQName, i, "고가")
                strItemValue.Trim()
                ListBoxResult.Items.Add("고가 : " & strItemValue)

                strItemValue = AxKHOpenAPI2.GetCommData(e.sTrCode, e.sRQName, i, "저가")
                strItemValue.Trim()
                ListBoxResult.Items.Add("저가 : " & strItemValue)

                strItemValue = AxKHOpenAPI2.GetCommData(e.sTrCode, e.sRQName, i, "수정주가구분")
                strItemValue.Trim()
                ListBoxResult.Items.Add("수정주가구분 : " & strItemValue)

                strItemValue = AxKHOpenAPI2.GetCommData(e.sTrCode, e.sRQName, i, "수정비율")
                strItemValue.Trim()
                ListBoxResult.Items.Add("수정비율 : " & strItemValue)

                strItemValue = AxKHOpenAPI2.GetCommData(e.sTrCode, e.sRQName, i, "대업종구분")
                strItemValue.Trim()
                ListBoxResult.Items.Add("대업종구분 : " & strItemValue)

                strItemValue = AxKHOpenAPI2.GetCommData(e.sTrCode, e.sRQName, i, "소업종구분")
                strItemValue.Trim()
                ListBoxResult.Items.Add("소업종구분 : " & strItemValue)

                strItemValue = AxKHOpenAPI2.GetCommData(e.sTrCode, e.sRQName, i, "종목정보")
                strItemValue.Trim()
                ListBoxResult.Items.Add("종목정보 : " & strItemValue)

                strItemValue = AxKHOpenAPI2.GetCommData(e.sTrCode, e.sRQName, i, "수정주가이벤트")
                strItemValue.Trim()
                ListBoxResult.Items.Add("수정주가이벤트 : " & strItemValue)

                strItemValue = AxKHOpenAPI2.GetCommData(e.sTrCode, e.sRQName, i, "전일종가")
                strItemValue.Trim()
                ListBoxResult.Items.Add("전일종가 : " & strItemValue)

                strItemValue = AxKHOpenAPI2.GetCommData(e.sTrCode, e.sRQName, i, "소업종구분")
                strItemValue.Trim()
                ListBoxResult.Items.Add("소업종구분 : " & strItemValue)
            Next

            ListBoxResult.Items.Add("===============================")
        End If
    End Sub
#End Region


    Private Sub btnGetStockPoint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetStockPoint.Click
        'AxKHOpenAPI2.CommKwRqData("0", 1, 5, 0, "", "1")
        AxKHOpenAPI2.GetCodeListByMarket("0")
    End Sub

End Class