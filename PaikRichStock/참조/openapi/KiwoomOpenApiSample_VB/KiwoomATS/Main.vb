' 키움 Open API 샘플제작
' (주)에스비씨엔
' site  : http://www.ZooATS.com
' email : support@zooats.com

Public Class Main

    Private Sub Button_로그인_Click(sender As System.Object, e As System.EventArgs) Handles Btn_Login.Click

        If bLoginStatus = True Then
            AxKHOpenAPI1.CommTerminate()
            Btn_Login.Text = "로그인"
            Label_로그인상태.Text = "OFF"
            ListBoxStatus.Items.Add("===============================")
            ListBoxStatus.Items.Add("로그아웃!!!")
            bLoginStatus = False
        Else
            ListBoxStatus.Items.Add("===============================")
            ListBoxStatus.Items.Add("로그인창 열기")
            AxKHOpenAPI1.CommConnect()
        End If

    End Sub

    Private Sub AxKHOpenAPI1_OnEventConnect(sender As Object, e As AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent) Handles AxKHOpenAPI1.OnEventConnect

        If e.nErrCode = 0 Then
            ListBoxStatus.Items.Add("로그인 성공!!!")
            Label_로그인상태.Text = "ON"
            Btn_Login.Text = "로그아웃"
            bLoginStatus = True
        Else
            ListBoxStatus.Items.Add("로그인 실패!!!")
        End If

    End Sub

    Private Sub Button_주식기본정보_Click(sender As System.Object, e As System.EventArgs) Handles Button_주식기본정보.Click
        Dim 종목코드, 종목명 As String

        ' 종목명 업데이트
        종목코드 = TextBox_종목코드.Text

        ' 종목코드 길이 확인
        If 종목코드.Length <> 6 Then
            MsgBox("종목코드 6자를 입력 해 주세요~!")
            Exit Sub
        End If

        종목명 = AxKHOpenAPI1.GetMasterCodeName(종목코드)
        ListBoxResult.Items.Add(종목명)

        ' 조회 함수 값 입력
        AxKHOpenAPI1.SetInputValue("종목코드", 종목코드)

        ' 조회값 서버 전송
        AxKHOpenAPI1.CommRqData("주식기본정보", "OPT10001", 0, "3001")

    End Sub

    Private Sub AxKHOpenAPI1_OnReceiveMsg(sender As Object, e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveMsgEvent) Handles AxKHOpenAPI1.OnReceiveMsg

    End Sub

    Private Sub AxKHOpenAPI1_OnReceiveTrData(sender As Object, e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent) Handles AxKHOpenAPI1.OnReceiveTrData

        ListBoxStatus.Items.Add("===============================")
        ListBoxStatus.Items.Add("RQName = " & e.sRQName)
        ListBoxStatus.Items.Add("===============================")

        If e.sRQName = "주식주문" Then    ' 주식주문 설정
            Order.TextBox_원주문번호.Text = Trim(AxKHOpenAPI1.GetCommData(e.sTrCode, "", 0, ""))
            Exit Sub
        ElseIf e.sRQName = "주식기본정보" Then
            ListBoxResult.Items.Add("주식기본정보")
            Dim nCnt As Integer
            Dim strItemValue As String

            nCnt = AxKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName)

            For i As Integer = 0 To (nCnt - 1)
                strItemValue = AxKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목명")
                strItemValue.Trim()
                ListBoxResult.Items.Add("종목명 : " & strItemValue)

                strItemValue = AxKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "시가총액")
                strItemValue.Trim()
                ListBoxResult.Items.Add("시가총액 : " & strItemValue)

                strItemValue = AxKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "시가")
                strItemValue.Trim()
                ListBoxResult.Items.Add("시가 : " & strItemValue)

                strItemValue = AxKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "고가")
                strItemValue.Trim()
                ListBoxResult.Items.Add("고가 : " & strItemValue)

                strItemValue = AxKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "저가")
                strItemValue.Trim()
                ListBoxResult.Items.Add("저가 : " & strItemValue)

                strItemValue = AxKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "기준가")
                strItemValue.Trim()
                ListBoxResult.Items.Add("기준가 : " & strItemValue)

                strItemValue = AxKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "현재가")
                strItemValue.Trim()
                ListBoxResult.Items.Add("현재가 : " & strItemValue)

                strItemValue = AxKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "대비기호")
                strItemValue.Trim()
                ListBoxResult.Items.Add("대비기호 : " & strItemValue)

                strItemValue = AxKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "전일대비")
                strItemValue.Trim()
                ListBoxResult.Items.Add("전일대비 : " & strItemValue)

                strItemValue = AxKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "등락율")
                strItemValue.Trim()
                ListBoxResult.Items.Add("등락율 : " & strItemValue)

                strItemValue = AxKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "거래량")
                strItemValue.Trim()
                ListBoxResult.Items.Add("거래량 : " & strItemValue)
            Next

            ListBoxResult.Items.Add("===============================")

        ElseIf e.sRQName = "주식일봉차트조회" Then
            ListBoxResult.Items.Add("주식일봉차트조회")
            Dim nCnt As Integer
            Dim strItemValue As String

            nCnt = AxKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName)

            For i As Integer = 0 To (nCnt - 1)

                ListBoxResult.Items.Add("-------------------------------")
                ListBoxResult.Items.Add("[index] =>" & i)

                Label_일봉수.Text = CType(i, String)

                strItemValue = AxKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "현재가")
                strItemValue.Trim()
                ListBoxResult.Items.Add("현재가 : " & strItemValue)

                strItemValue = AxKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "거래량")
                strItemValue.Trim()
                ListBoxResult.Items.Add("거래량 : " & strItemValue)

                strItemValue = AxKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "거래대금")
                strItemValue.Trim()
                ListBoxResult.Items.Add("거래대금 : " & strItemValue)

                strItemValue = AxKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "일자")
                strItemValue.Trim()
                ListBoxResult.Items.Add("일자 : " & strItemValue)

                strItemValue = AxKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "시가")
                strItemValue.Trim()
                ListBoxResult.Items.Add("시가 : " & strItemValue)

                strItemValue = AxKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "고가")
                strItemValue.Trim()
                ListBoxResult.Items.Add("고가 : " & strItemValue)

                strItemValue = AxKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "저가")
                strItemValue.Trim()
                ListBoxResult.Items.Add("저가 : " & strItemValue)

                strItemValue = AxKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "수정주가구분")
                strItemValue.Trim()
                ListBoxResult.Items.Add("수정주가구분 : " & strItemValue)

                strItemValue = AxKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "수정비율")
                strItemValue.Trim()
                ListBoxResult.Items.Add("수정비율 : " & strItemValue)

                strItemValue = AxKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "대업종구분")
                strItemValue.Trim()
                ListBoxResult.Items.Add("대업종구분 : " & strItemValue)

                strItemValue = AxKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "소업종구분")
                strItemValue.Trim()
                ListBoxResult.Items.Add("소업종구분 : " & strItemValue)

                strItemValue = AxKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목정보")
                strItemValue.Trim()
                ListBoxResult.Items.Add("종목정보 : " & strItemValue)

                strItemValue = AxKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "수정주가이벤트")
                strItemValue.Trim()
                ListBoxResult.Items.Add("수정주가이벤트 : " & strItemValue)

                strItemValue = AxKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "전일종가")
                strItemValue.Trim()
                ListBoxResult.Items.Add("전일종가 : " & strItemValue)

                strItemValue = AxKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "소업종구분")
                strItemValue.Trim()
                ListBoxResult.Items.Add("소업종구분 : " & strItemValue)
            Next

            ListBoxResult.Items.Add("===============================")
        End If
    End Sub

    Private Sub Button_주식일봉차트_Click(sender As System.Object, e As System.EventArgs) Handles Button_주식일봉차트.Click
        Dim 종목코드, 기준일자, 수정주가 As String

        ' 종목코드 업데이트
        종목코드 = TextBox_종목코드.Text

        ' 종목코드 길이 확인
        If 종목코드.Length <> 6 Then
            MsgBox("종목코드 6자를 입력 해 주세요~!")
            Exit Sub
        End If

        ' 기준일자 업데이트
        기준일자 = TextBox_기준일.Text

        ' 기준일자 길이 확인
        If 기준일자.Length <> 8 Then
            MsgBox("기준일자 8자를 입력 해 주세요~!")
            Exit Sub
        End If

        '수정주가 업데이트
        수정주가 = "0"

        ' 조회 함수 값 입력

        ListBoxResult.Items.Add("종목코드 - " & 종목코드)
        ListBoxResult.Items.Add("기준일자 - " & 기준일자)
        ListBoxResult.Items.Add("수정주가 - " & 수정주가)

        AxKHOpenAPI1.SetInputValue("종목코드", 종목코드)
        AxKHOpenAPI1.SetInputValue("기준일자", 기준일자)
        AxKHOpenAPI1.SetInputValue("수정주가구분", 수정주가)

        ' 조회값 서버 전송
        AxKHOpenAPI1.CommRqData("주식일봉차트조회", "OPT10081", "0", "3002")
    End Sub

    Private Sub ButtonOrder_Click(sender As System.Object, e As System.EventArgs) Handles ButtonOrder.Click
        Order.Show()
    End Sub

    Private Sub Main_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        '=================================================
        '거래구분목록 지정

        거래구분목록(0, 0) = "00"
        거래구분목록(0, 1) = "지정가"

        거래구분목록(1, 0) = "03"
        거래구분목록(1, 1) = "시장가"

        거래구분목록(2, 0) = "05"
        거래구분목록(2, 1) = "조건부지정가"

        거래구분목록(3, 0) = "06"
        거래구분목록(3, 1) = "최유리지정가"

        거래구분목록(4, 0) = "07"
        거래구분목록(4, 1) = "최우선지정가"

        거래구분목록(5, 0) = "10"
        거래구분목록(5, 1) = "지정가IOC"

        거래구분목록(6, 0) = "11"
        거래구분목록(6, 1) = "시장가IOC"

        거래구분목록(7, 0) = "16"
        거래구분목록(7, 1) = "최유리IOC"

        거래구분목록(8, 0) = "20"
        거래구분목록(8, 1) = "지정가FOK"

        거래구분목록(9, 0) = "23"
        거래구분목록(9, 1) = "시장가FOK"

        거래구분목록(10, 0) = "26"
        거래구분목록(10, 1) = "최유리FOK"

        거래구분목록(11, 0) = "61"
        거래구분목록(11, 1) = "시간외단일가매매"

        거래구분목록(12, 0) = "81"
        거래구분목록(12, 1) = "시간외종가"

        '=================================================
        '주문유형
        주문유형목록(0, 0) = "1"
        주문유형목록(0, 1) = "신규매수"

        주문유형목록(1, 0) = "2"
        주문유형목록(1, 1) = "신규매도"

        주문유형목록(2, 0) = "3"
        주문유형목록(2, 1) = "매수취소"

        주문유형목록(3, 0) = "4"
        주문유형목록(3, 1) = "매도취소"

        주문유형목록(4, 0) = "5"
        주문유형목록(4, 1) = "매수정정"

        주문유형목록(5, 0) = "6"
        주문유형목록(5, 1) = "매도정정"

    End Sub
End Class
