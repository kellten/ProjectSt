' 키움 Open API 샘플제작
' (주)에스비씨엔
' site  : http://www.ZooATS.com
' email : support@zooats.com

Public Class Order

    Private Sub Order_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Dim i As Integer

        '=================================================
        '거래구분목록 지정
        For i = 0 To 12
            ComboBox_거래구분.Items.Add(거래구분목록(i, 1))
        Next

        ComboBox_거래구분.SelectedIndex = 0


        '=================================================
        '주문유형
        For i = 0 To 5
            ComboBox_매매구분.Items.Add(주문유형목록(i, 1))
        Next

        ComboBox_매매구분.SelectedIndex = 0

    End Sub

    Private Sub Button_주문_Click(sender As System.Object, e As System.EventArgs) Handles Button_주문.Click
        '=================================================
        ' 계좌번호 입력 여부 확인
        If 계좌번호.Length <> 10 Then
            Main.ListBoxStatus.Items.Add("[주문에러] 계좌번호가 잘 못 입력되었습니다.")
            MsgBox("계좌번호 10자를 입력 해 주세요~!")
            Exit Sub
        End If

        '=================================================
        ' 종목코드 입력 여부 확인
        If TextBox_종목코드.TextLength <> 6 Then
            Main.ListBoxStatus.Items.Add("[주문에러] 종목코드가 잘 못 입력되었습니다.")
            MsgBox("종목코드 6자를 입력 해 주세요~!")
            Exit Sub
        End If

        '=================================================
        ' 주문수량 입력 여부 확인
        Dim n주문수량 As Integer

        If TextBox_주문수량.Text <> "" Then
            n주문수량 = CType(TextBox_주문수량.Text, Integer)
        Else
            Main.ListBoxStatus.Items.Add("[주문에러] 주문수량을 입력하지 않으셨습니다.")
            MsgBox("주문수량을 입력해주세요")
        End If

        If n주문수량 < 1 Then
            Main.ListBoxStatus.Items.Add("[주문에러] 주문수량이 1보다 작습니다.")
            MsgBox("주문수량을 0보다 큰 수로 입력 해 주세요~!")
            Exit Sub
        End If

        '=================================================
        ' 거래구분 취득
        ' 0:지정가, 3:시장가, 5:조건부지정가, 6:최유리지정가, 7:최우선지정가,
        ' 10:지정가IOC, 13:시장가IOC, 16:최유리IOC, 20:지정가FOK, 23:시장가FOK,
        ' 26:최유리FOK, 61:장개시전시간외, 62:시간외단일가매매, 81:시간외종가
        Dim 거래구분 As String
        거래구분 = 거래구분목록(ComboBox_거래구분.SelectedIndex, 0)

        '=================================================
        ' 주문가격 입력 여부
        Dim n주문가격 As Integer

        If TextBox_주문가격.Text <> "" Then
            n주문가격 = CType(TextBox_주문가격.Text, Integer)
        End If

        If (거래구분 <> "3" Or 거래구분 <> "13" Or 거래구분 <> "23") And n주문가격 < 1 Then
            Main.ListBoxStatus.Items.Add("[주문에러] 주문가격이 1보다 작습니다.")
            MsgBox("주문가격을 0보다 큰 수로 입력 해 주세요~!")
            Exit Sub
        End If

        '=================================================
        ' 매매구분 취득(1:신규매수, 2:신규매도 3:매수취소, 4:매도취소, 5:매수정정, 6:매도정정)
        Dim n매매구분 As Integer
        n매매구분 = CType(주문유형목록(ComboBox_매매구분.SelectedIndex, 0), Integer)

        '=================================================
        ' 원주문번호 입력 여부
        If n매매구분 > 2 And TextBox_원주문번호.TextLength < 1 Then
            Main.ListBoxStatus.Items.Add("[주문에러] 원주문번호를 입력해 주세요.")
            MsgBox("원주문번호를 입력 해 주세요~!")
            Exit Sub
        End If

        '=================================================
        ' 주식주문
        Dim lRet, strRQName, strScrNo
        strRQName = "주식주문"
        strScrNo = "3003"

        lRet = Main.AxKHOpenAPI1.SendOrder(strRQName, strScrNo, 계좌번호, n매매구분, TextBox_종목코드.Text, n주문수량, n주문가격, 거래구분, TextBox_원주문번호.Text)

        If lRet = 0 Then
            Main.ListBoxResult.Items.Add("주문이 전송 되었습니다.")
        Else
            Main.ListBoxResult.Items.Add("주문 전송이 실패 했습니다.  에러] " & lRet)
        End If

    End Sub

    Private Sub Button_계좌저장_Click(sender As System.Object, e As System.EventArgs) Handles Button_계좌저장.Click

        If TextBox_계좌번호.TextLength <> 10 Then
            Main.ListBoxStatus.Items.Add("[에러] 계좌번호가 잘 못 입력되었습니다.")
            MsgBox("계좌번호 10자를 입력 해 주세요~!")
            Exit Sub
        End If

        계좌번호 = TextBox_계좌번호.Text
        Main.ListBoxStatus.Items.Add("계좌번호 (" & 계좌번호 & ")가 저장되었습니다")

    End Sub

    Private Sub Sleep(p1 As Integer)
        Throw New NotImplementedException
    End Sub

End Class