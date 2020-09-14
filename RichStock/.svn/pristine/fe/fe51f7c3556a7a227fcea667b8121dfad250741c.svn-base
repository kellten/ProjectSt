Imports AxKHOpenAPILib

Public Class KiwomConnectionInfo
    Private _AxKH As New AxKHOpenAPI
    Private _loginStatus As LoginStatus = LoginStatus.DisConnect

    Private Enum LoginStatus
        Connect
        DisConnect
    End Enum

    Sub New(ByVal AxKH As AxKHOpenAPI)
        _AxKH = AxKH
    End Sub

    Public Sub Connection()
        If _loginStatus = LoginStatus.DisConnect Then
            _AxKH.CommConnect()
        Else
            _AxKH.CommTerminate()
        End If
    End Sub

    Private Sub _AxKH_OnEventConnect(ByVal sender As Object, ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent)

        'If e.nErrCode = 0 Then
        '    ListBoxStatus.Items.Add("로그인 성공!!!")
        '    Label_로그인상태.Text = "ON"
        '    Btn_Login.Text = "로그아웃"
        '    bLoginStatus = True
        'Else
        '    ListBoxStatus.Items.Add("로그인 실패!!!")
        'End If

    End Sub

End Class
