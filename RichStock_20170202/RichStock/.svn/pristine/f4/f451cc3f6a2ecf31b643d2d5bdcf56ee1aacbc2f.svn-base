Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace KiCodeGroup

    Class KOAErrorCode
        Public Const OP_ERR_NONE As Integer = 0                 '"정상처리"
        Public Const OP_ERR_LOGIN As Integer = -100             '"사용자정보교환에 실패하였습니다. 잠시후 다시 시작하여 주십시오."
        Public Const OP_ERR_CONNECT As Integer = -101           '"서버 접속 실패"
        Public Const OP_ERR_VERSION As Integer = -102           '"버전처리가 실패하였습니다.
        Public Const OP_ERR_SISE_OVERFLOW As Integer = -200     '”시세조회 과부하”
        Public Const OP_ERR_RQ_STRUCT_FAIL As Integer = -201    '”REQUEST_INPUT_st Failed”
        Public Const OP_ERR_RQ_STRING_FAIL As Integer = -202    '”요청 전문 작성 실패”
        Public Const OP_ERR_ORD_WRONG_INPUT As Integer = -300   '”주문 입력값 오류”
        Public Const OP_ERR_ORD_WRONG_ACCNO As Integer = -301   '”계좌비밀번호를 입력하십시오.”
        Public Const OP_ERR_OTHER_ACC_USE As Integer = -302     '”타인계좌는 사용할 수 없습니다.
        Public Const OP_ERR_MIS_2BILL_EXC As Integer = -303     '”주문가격이 20억원을 초과합니다.”
        Public Const OP_ERR_MIS_5BILL_EXC As Integer = -304     '”주문가격은 50억원을 초과할 수 없습니다.”
        Public Const OP_ERR_MIS_1PER_EXC As Integer = -305      '”주문수량이 총발행주수의 1%를 초과합니다.”
        Public Const OP_ERR_MID_3PER_EXC As Integer = -306      '”주문수량은 총발행주수의 3%를 초과할 수 없습니다.”
    End Class

    Class KOAError

        Public Shared _errorMessage As String

        Public Shared ReadOnly Property GetErrorMassage() As String
            Get
                Return _errorMessage
            End Get
        End Property

        Public Shared Function IsError(ByVal nErrorCode As Integer) As Boolean
            Select Case nErrorCode
                Case KOAErrorCode.OP_ERR_NONE
                    _errorMessage = "[" + nErrorCode.ToString() + "] :" + "정상처리"
                    Return True

                Case KOAErrorCode.OP_ERR_LOGIN
                    _errorMessage = "[" + nErrorCode.ToString() + "] :" + "사용자정보교환에 실패하였습니다. 잠시 후 다시 시작하여 주십시오."

                Case KOAErrorCode.OP_ERR_CONNECT
                    _errorMessage = "[" + nErrorCode.ToString() + "] :" + "서버 접속 실패"

                Case KOAErrorCode.OP_ERR_VERSION
                    _errorMessage = "[" + nErrorCode.ToString() + "] :" + "버전처리가 실패하였습니다"

                Case KOAErrorCode.OP_ERR_SISE_OVERFLOW
                    _errorMessage = "[" + nErrorCode.ToString() + "] :" + "시세조회 과부하 초당 5회건수 초과"

                Case KOAErrorCode.OP_ERR_RQ_STRUCT_FAIL
                    _errorMessage = "[" + nErrorCode.ToString() + "] :" + "REQUEST_INPUT_st Failed"

                Case KOAErrorCode.OP_ERR_RQ_STRING_FAIL
                    _errorMessage = "[" + nErrorCode.ToString() + "] :" + "요청 전문 작성 실패"

                Case KOAErrorCode.OP_ERR_ORD_WRONG_INPUT
                    _errorMessage = "[" + nErrorCode.ToString() + "] :" + "주문 입력값 오류"

                Case KOAErrorCode.OP_ERR_ORD_WRONG_ACCNO
                    _errorMessage = "[" + nErrorCode.ToString() + "] :" + "계좌비밀번호를 입력하십시오."

                Case KOAErrorCode.OP_ERR_OTHER_ACC_USE
                    _errorMessage = "[" + nErrorCode.ToString() + "] :" + "타인계좌는 사용할 수 없습니다. 계좌번호 10자리 확인"

                Case KOAErrorCode.OP_ERR_MIS_2BILL_EXC
                    _errorMessage = "[" + nErrorCode.ToString() + "] :" + "주문가격이 20억원을 초과합니다."

                Case KOAErrorCode.OP_ERR_MIS_5BILL_EXC
                    _errorMessage = "[" + nErrorCode.ToString() + "] :" + "주문가격은 50억원을 초과할 수 없습니다."

                Case KOAErrorCode.OP_ERR_MIS_1PER_EXC
                    _errorMessage = "[" + nErrorCode.ToString() + "] :" + "주문수량이 총발행주수의 1%를 초과합니다."

                Case KOAErrorCode.OP_ERR_MID_3PER_EXC
                    _errorMessage = "[" + nErrorCode.ToString() + "] :" + "주문수량은 총발행주수의 3%를 초과할 수 없습니다"

                Case Else
                    _errorMessage = "[" + nErrorCode.ToString() + "] :" + "알려지지 않은 오류입니다."
            End Select
        End Function

    End Class

End Namespace

