Public Module ModStatus

#Region " Const "
    Public Const LoginStatus_Connect As Integer = 0
    Public Const LoginStatus_DisConnect As Integer = 1
    Public Const LoginSucessStatus_Success As Integer = 0
    Public Const LoginSucessStatus_Fail As Integer = 1
    Public Const EventOn As Boolean = True
    Public Const EventOff As Boolean = False
#End Region

#Region " Enum "
    ''' <summary>
    ''' 로그인 상태 값 - Connect : 0, DisConnect : 1
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum LoginStatus
        Connect
        DisConnect
    End Enum
    ''' <summary>
    ''' 로그인 이벤트 값 - Success : 0, Fail : 1
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum LoginSucessStatus
        Success
        Fail
    End Enum
#End Region
End Module
