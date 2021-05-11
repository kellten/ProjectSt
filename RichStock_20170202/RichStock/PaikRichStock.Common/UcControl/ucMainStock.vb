﻿Public Class ucMainStock

    Private _loginStatus As LoginStatus = LoginStatus.DisConnect
    Public _allStockDataset As DataSet
    Public _KospiStockDataset As DataSet
    Public _KosDakStockDataset As DataSet
    Public _AccNo As DataSet
    Public _ChejanFidList As DataSet
    Public _TradeGb As DataSet
    Public _OrderResult As New DataSet

    Private _clsKiwoomDefine As New clsKiwoomDefine
    Private _clsRealKiwoomDefine As New clsKiwoomRealDefine
    Private _tech_TypicalPrice As New Tech_TypicalPrice
    Private _clsKiwoomBaseInfo As New clsKiwoomBaseInfo
    Private _clsConditionSearch As New clsConditionSearch
    Private _clsScreenNo As New clsScreenNoManage
    Private _clsOrderDefine As New clsOrderDefine
    Private _oCon As New Object

    Public _dtScreenNo As New DataTable
    Public _dtTradingScreenNo As New DataTable
    Public _dtOptKWFidScreenNo As New DataTable

#Region " Enum "
    Public Enum EventTypeIndex
        OnEventConnect              ' 통신 연결 상태 변경시 이벤트
        OnReceiveConditionVer       ' 로컬에 사용자조건식 저장 성공여부 응답 이벤트
        OnReceiveRealCondition      ' 실시간 시세 이벤트
        OnReceiveTrCondition        ' 조건검색 조회응답 이벤트
        OnReceiveTrData             ' Tran 수신시 이벤트 
        OnReceiveRealData           ' 실시간 시세 이벤트
        OnReceiveMsg                ' 수신 메시지 이벤트
        OnReceiveChejanData         ' 주문 접수/확인 수신시 이벤트
    End Enum

    Public Enum SearchName
        PreriodLowestSearch
        PreriodLowestEndSearch
    End Enum

    Public Enum RaiseIndex
        RaiseConnection
        RaiseDs_GetConditionList
        RaiseDs_SetConditionList
        RaiseDs_BaseInfo
        RaiseDs_DayBaseInfo
        RaiseDs_TradePortInfo
        RaiseDs_StockByTradePortNumer
        RaiseDs_OnReceiveRealData
        RaiseDs_OnReceiveChejanData         ' 주문 접수/확인 수신시 반환 Dataset
        RaiseDs_Opw00018                    ' 계좌잔고요청내역
        RaiseDs_opt10085                    ' 계좌수익률요청
        RaiseDs_opt10003                    ' 체결정보요청
        RaiseDs_opt10006                    ' 주식시세요청
        RaiseDs_OnMsg                       ' Massage
    End Enum

    Private Enum spConDetailListIndex
        StockName
        NowPrice
        UpDownRate
        PrevRateSymbol
        StartPrice
        HighPrice
        LowPrice
        StockCode
        ScreenNo
    End Enum
#End Region

#Region " RaiseUserEvent "
    Private Sub RaiseUserEvent(ByVal raiseType As RaiseIndex, ByVal status As String, ByVal ds As DataSet, ByVal dr As DataRow)
        Select Case raiseType
            Case RaiseIndex.RaiseConnection
                RaiseEvent OnConnection(status)
            Case RaiseIndex.RaiseDs_GetConditionList
                RaiseEvent OnDsGetConditionList(ds)
            Case RaiseIndex.RaiseDs_SetConditionList
                RaiseEvent OnDsSetConditionList(ds)
            Case RaiseIndex.RaiseDs_BaseInfo
                RaiseEvent OnDsBaseInfo(ds)
            Case RaiseIndex.RaiseDs_DayBaseInfo
                RaiseEvent OnDayDsBaseInfo(ds)
            Case RaiseIndex.RaiseDs_TradePortInfo
                RaiseEvent OnDsTradePortInfo(dr)
            Case RaiseIndex.RaiseDs_StockByTradePortNumer
                RaiseEvent OnDsStockByTradePortNumer(ds)
            Case RaiseIndex.RaiseDs_OnReceiveRealData
                RaiseEvent OnDsReceiveRealData(ds)
            Case RaiseIndex.RaiseDs_OnReceiveChejanData
                SetOrderResultDataSet(ds)
                RaiseEvent OnDsReceiveChejanData(ds)
            Case RaiseIndex.RaiseDs_Opw00018
                RaiseEvent OnDsOpw00018(ds)
            Case RaiseIndex.RaiseDs_opt10085
                RaiseEvent OnDsopt10085(ds)
            Case RaiseIndex.RaiseDs_opt10003
                RaiseEvent OnDsopt10003(ds)
            Case RaiseIndex.RaiseDs_opt10006
                RaiseEvent OnDsopt10006(ds)
            Case RaiseIndex.RaiseDs_OnMsg
                RaiseEvent OnMsg(status)
        End Select
    End Sub
#End Region

    ' 1. Tran 수신시 이벤트 - OnReceiveTrData
    ' 2. 실시간 시세 이벤트 - OnReceiveRealData
    ' 3. 수신 메시지 이벤트 - OnReceiveMsg 
    ' 4. 주문 접수/확인 수신시 이벤트 - OnReceiveChejanData
    ' 5. 통신 연결 상태 변경시 이벤트 - OnEventConnect
    ' 6. 조건검색 실시간 편입,이탈종목 이벤트 - OnReceiveRealCondition
    ' 7. 조건검색 조회응답 이벤트 - OnReceiveTrCondition
    ' 8. 로컬에 사용자조건식 저장 성공여부 응답 이벤트 - OnReceiveConditionVer

#Region " 사용자 Event명 "
    ''' <summary>
    ''' 접속 정보
    ''' </summary>
    ''' <param name="status"></param>
    ''' <remarks></remarks>
    Public Event OnConnection(ByVal status As String)
    ''' <summary>
    ''' 종목 기본 정도에 대한 Event
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Public Event OnDsBaseInfo(ByVal ds As DataSet)
    ''' <summary>
    ''' 일봉 차트 정보에 대한 Event
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Public Event OnDayDsBaseInfo(ByVal ds As DataSet)

    Public Event OnDsGetConditionList(ByVal ds As DataSet)

    Public Event OnDsSetConditionList(ByVal ds As DataSet)

    Public Event OnDsTradePortInfo(ByVal dr As DataRow)

    Public Event OnDsStockByTradePortNumer(ByVal ds As DataSet)

    Public Event OnDsReceiveRealData(ByVal ds As DataSet)

    Public Event OnDsReceiveChejanData(ByVal ds As DataSet)

    Public Event OnDsOpw00018(ByVal ds As DataSet)

    Public Event OnDsopt10085(ByVal ds As DataSet)

    Public Event OnDsopt10003(ByVal ds As DataSet)

    Public Event OnDsopt10006(ByVal ds As DataSet)

    Public Event OnMsg(ByVal msg As String)

#End Region

#Region " Structure "
    Public EVENT_STATUS As EventStatus

    Public Structure EventStatus
        Public STATUS_OnEventConnect As Boolean
        Public STATUS_OnReceiveConditionVer As Boolean
        Public STATUS_OnReceiveTrCondition As Boolean
        Public STATUS_OnReceiveTrData As Boolean

        Public STATUS_OnReceiveRealCondition As Boolean
        Public STATUS_OnReceiveRealData As Boolean
        Public STATUS_OnReceiveMsg As Boolean

        Public Sub InitEventStatus()
            STATUS_OnEventConnect = True
            STATUS_OnReceiveConditionVer = True
            STATUS_OnReceiveTrCondition = True
            STATUS_OnReceiveTrData = True
            STATUS_OnReceiveRealCondition = True
            STATUS_OnReceiveRealData = True
            STATUS_OnReceiveMsg = True
        End Sub
    End Structure
#End Region

#Region " Event 상태 Property & Event "
    Private _OnEventConnect As Boolean
    Private _OnReceiveConditionVer As Boolean
    Private _OnReceiveTrCondition As Boolean
    Private _OnReceiveTrData As Boolean

#Region " EventOnOff "
    Private Sub EventOnOff(ByVal EventType As EventTypeIndex, ByVal eventOffType As Boolean)

        Select Case EventType
            Case EventTypeIndex.OnEventConnect          ' 통신 연결 상태 변경시 이벤트
                If eventOffType = EventOn Then
                    AddHandler AxKH.OnEventConnect, AddressOf AxKH_OnEventConnect
                    EVENT_STATUS.STATUS_OnEventConnect = EventOn
                Else
                    RemoveHandler AxKH.OnEventConnect, AddressOf AxKH_OnEventConnect
                    RemoveHandler AxKH.OnEventConnect, AddressOf AxKH_OnEventConnect
                    EVENT_STATUS.STATUS_OnEventConnect = EventOff
                End If
            Case EventTypeIndex.OnReceiveConditionVer   ' 로컬에 사용자조건식 저장 성공여부 응답 이벤트
                If eventOffType = EventOn Then
                    AddHandler AxKH.OnReceiveConditionVer, AddressOf AxKH_OnReceiveConditionVer
                    EVENT_STATUS.STATUS_OnReceiveConditionVer = EventOn
                Else
                    RemoveHandler AxKH.OnReceiveConditionVer, AddressOf AxKH_OnReceiveConditionVer
                    RemoveHandler AxKH.OnReceiveConditionVer, AddressOf AxKH_OnReceiveConditionVer
                    EVENT_STATUS.STATUS_ONRECEIVECONDITIONVER = EventOff
                End If
            Case EventTypeIndex.OnReceiveTrCondition    ' 조건검색 조회응답 이벤트
                If eventOffType = EventOn Then
                    AddHandler AxKH.OnReceiveTrCondition, AddressOf AxKH_OnReceiveTrCondition
                    EVENT_STATUS.STATUS_OnReceiveTrCondition = EventOn
                Else
                    RemoveHandler AxKH.OnReceiveTrCondition, AddressOf AxKH_OnReceiveTrCondition
                    RemoveHandler AxKH.OnReceiveTrCondition, AddressOf AxKH_OnReceiveTrCondition
                    EVENT_STATUS.STATUS_OnReceiveTrCondition = EventOff
                End If
            Case EventTypeIndex.OnReceiveTrData          ' Tran 수신시 이벤트 
                If eventOffType = EventOn Then
                    AddHandler AxKH.OnReceiveTrData, AddressOf AxKH_OnReceiveTrData
                    EVENT_STATUS.STATUS_OnReceiveTrData = EventOn
                Else
                    RemoveHandler AxKH.OnReceiveTrData, AddressOf AxKH_OnReceiveTrData
                    RemoveHandler AxKH.OnReceiveTrData, AddressOf AxKH_OnReceiveTrData
                    EVENT_STATUS.STATUS_OnReceiveTrData = EventOff
                End If
            Case EventTypeIndex.OnReceiveRealCondition   ' 조건검색 실시간 편입,이탈종목 이벤트
                If eventOffType = EventOn Then
                    AddHandler AxKH.OnReceiveRealCondition, AddressOf AxKH_OnReceiveRealCondition
                    EVENT_STATUS.STATUS_OnReceiveRealCondition = EventOn
                Else
                    RemoveHandler AxKH.OnReceiveRealCondition, AddressOf AxKH_OnReceiveRealCondition
                    RemoveHandler AxKH.OnReceiveRealCondition, AddressOf AxKH_OnReceiveRealCondition
                    EVENT_STATUS.STATUS_OnReceiveRealCondition = EventOff
                End If
            Case EventTypeIndex.OnReceiveRealData           ' 실시간 시세 이벤트
                If eventOffType = EventOn Then
                    AddHandler AxKH.OnReceiveRealData, AddressOf AxKH_OnReceiveRealData
                    EVENT_STATUS.STATUS_OnReceiveRealData = EventOn
                Else
                    RemoveHandler AxKH.OnReceiveRealData, AddressOf AxKH_OnReceiveRealData
                    RemoveHandler AxKH.OnReceiveRealData, AddressOf AxKH_OnReceiveRealData
                    EVENT_STATUS.STATUS_OnReceiveRealData = EventOff
                End If
            Case EventTypeIndex.OnReceiveMsg          ' 수신 메시지 이벤트
                If eventOffType = EventOn Then
                    AddHandler AxKH.OnReceiveMsg, AddressOf AxKH_OnReceiveMsg
                    EVENT_STATUS.STATUS_OnReceiveMsg = EventOn
                Else
                    RemoveHandler AxKH.OnReceiveMsg, AddressOf AxKH_OnReceiveMsg
                    RemoveHandler AxKH.OnReceiveMsg, AddressOf AxKH_OnReceiveMsg
                    EVENT_STATUS.STATUS_OnReceiveMsg = EventOff
                End If
        End Select

    End Sub
#End Region

#Region " 이벤트 OnOf Propery "

#Region " 로컬에 사용자조건식 저장 성공여부 응답 "
    Public Property OnReceiveConditionVer() As Boolean
        Get
            Return _OnReceiveConditionVer
        End Get
        Set(ByVal value As Boolean)
            _OnReceiveConditionVer = value
            EventOnOff(EventTypeIndex.OnReceiveConditionVer, value)
        End Set
    End Property
#End Region

#Region " 조건검색 조회응답 "
    Public Property OnReceiveTrCondition() As Boolean
        Get
            Return _OnReceiveTrCondition
        End Get
        Set(ByVal value As Boolean)
            _OnReceiveTrCondition = value
            EventOnOff(EventTypeIndex.OnReceiveTrCondition, value)
        End Set
    End Property
#End Region

#Region " Tran 수신 "
    Public Property OnReceiveTrData() As Boolean
        Get
            Return _OnReceiveTrData
        End Get
        Set(ByVal value As Boolean)
            _OnReceiveTrData = value
            EventOnOff(EventTypeIndex.OnReceiveTrData, value)
        End Set
    End Property
#End Region

#End Region

#End Region

#Region " 접속 "

#Region " Connection                            | Login "
    ''' <summary>
    ''' C:\Kiwoom\KiwoomFlash2\khministarter.exe 를 통해 접속한다.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Connection()
        If _loginStatus = LoginStatus.DisConnect Then
            AxKH.CommConnect()
        Else
            AxKH.CommTerminate()
        End If
    End Sub
#End Region


    Public Property OnEventConnect() As Boolean
        Get
            Return _OnEventConnect
        End Get
        Set(ByVal value As Boolean)
            _OnEventConnect = value
            EventOnOff(EventTypeIndex.OnEventConnect, value)
        End Set
    End Property

    ''' <summary>
    ''' 통신 연결 상태 변경시 이벤트
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AxKH_OnEventConnect(ByVal sender As Object, ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent) Handles AxKH.OnEventConnect
        If e.nErrCode = 0 Then
            ConnectionReturnValue(LoginSucessStatus.Success)
            _loginStatus = LoginStatus_Connect
            _TradeGb = _clsOrderDefine.GetTradeGb
        Else
            ConnectionReturnValue(LoginSucessStatus.Fail)
            _loginStatus = LoginStatus_DisConnect
        End If
    End Sub

    Public Sub ConnectionReturnValue(ByVal status As Integer)
        Select Case status
            Case LoginSucessStatus_Success
                lblLoginStatus.Text = "로그인 성공"
                AxKH.SetRealRemove("ALL", "ALL")
                _allStockDataset = _clsKiwoomBaseInfo.GetStockListByMarKetAll(AxKH)
                _KospiStockDataset = _clsKiwoomBaseInfo.GetStockListByMarKetKospi(AxKH)
                _KosDakStockDataset = _clsKiwoomBaseInfo.GetStockListByMarKetKosDak(AxKH)

                SetDtScreenNoDt()
                SetTradingScreenNoDt()
                SetOptKWFidScreenNoDt()

            Case LoginSucessStatus_Fail
                lblLoginStatus.Text = "로그인 실패"
                _allStockDataset.Reset()
                _KospiStockDataset.Reset()
                _KosDakStockDataset.Reset()
        End Select

        RaiseUserEvent(RaiseIndex.RaiseConnection, lblLoginStatus.Text, Nothing, Nothing)

    End Sub
#End Region

#Region " 응답 이벤트 "
    ''' 로컬에 사용자조건식 저장 성공여부 응답 이벤트
    Private Sub AxKH_OnReceiveConditionVer(ByVal sender As Object, ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveConditionVerEvent) Handles AxKH.OnReceiveConditionVer
        If e.lRet <> 1 Then Exit Sub

        Dim strList As String = AxKH.GetConditionNameList()
        RaiseUserEvent(RaiseIndex.RaiseDs_GetConditionList, "", _clsConditionSearch.SetUserConditionList(strList), Nothing)
    End Sub

    ''' 조건검색 조회응답 이벤트
    Private Sub AxKH_OnReceiveTrCondition(ByVal sender As Object, ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrConditionEvent) Handles AxKH.OnReceiveTrCondition
        If Trim(e.strCodeList) <> "" Then
            RaiseUserEvent(RaiseIndex.RaiseDs_SetConditionList, "", _clsConditionSearch.SetUserConditionStockList(e.strCodeList, AxKH), Nothing)
        End If
    End Sub
    ''' Tran 수신시 이벤트(OnReceiveTrData)
    Private Sub AxKH_OnReceiveTrData(ByVal sender As Object, ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent) Handles AxKH.OnReceiveTrData
        Select Case e.sRQName
            Case "주식기본정보"
                SetStockBaseInfo(e)
                DisconnectRealData(e.sScrNo)
            Case "주식일봉차트조회"
                SetDayStockBaseInfo(e)
                DisconnectRealData(e.sScrNo)
            Case "주식거래원요청"
                SetTradePortInfo(e)
                DisconnectRealData(e.sScrNo)
            Case "종목별증권사순위요청"
                SetStockByTradePortNumer(e)
                DisconnectRealData(e.sScrNo)
            Case "계좌평가잔고내역요청"
                Setopw00018(e)
                DisconnectRealData(e.sScrNo)
            Case "계좌수익률요청"
                Setopt10085(e)
                DisconnectRealData(e.sScrNo)
        End Select

        If InStr(e.sRQName, "체결정보요청") > 0 Then
            Setopt10003(e)
            DisconnectRealData(e.sScrNo)
        ElseIf InStr(e.sRQName, "주식시세요청") > 0 Then
            Setopt10006(e)
            DisconnectRealData(e.sScrNo)
        End If
    End Sub
    ' 수신 메시지 이벤트
    Private Sub AxKH_OnReceiveMsg(ByVal sender As Object, ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveMsgEvent) Handles AxKH.OnReceiveMsg
        RaiseUserEvent(RaiseIndex.RaiseDs_OnMsg, e.sMsg, Nothing, Nothing)
    End Sub

    ' 조건검색 실시간 편입,이탈종목 이벤트
    Private Sub AxKH_OnReceiveRealCondition(ByVal sender As Object, ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealConditionEvent) Handles AxKH.OnReceiveRealCondition

    End Sub
#End Region

#Region " 응답 이벤트 발생 시 사용자 이벤트 발생"
    ''' <summary>
    ''' 주식기본정보
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SetStockBaseInfo(ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)
        RaiseUserEvent(RaiseIndex.RaiseDs_BaseInfo, "", _clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.Opt10001, "StockBaseInfo", AxKH, e), Nothing)
    End Sub

    ''' <summary>
    ''' 주식일봉차트조회
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SetDayStockBaseInfo(ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)

        RaiseUserEvent(RaiseIndex.RaiseDs_DayBaseInfo, "", _tech_TypicalPrice.Fn_PeriodLowestMA("20", _clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.Opt10081, "DayStockBaseInfo", AxKH, e), "DayStockBaseInfo", e.sTrCode, ""), Nothing)

    End Sub
    ''' <summary>
    ''' 기준일 거래원 가져온다.
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SetTradePortInfo(ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)
        Dim dr As DataRow = _clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.Opt10081, "TradePortInfo", AxKH, e).Tables(0).Rows(0)
        RaiseUserEvent(RaiseIndex.RaiseDs_TradePortInfo, "", Nothing, dr)
    End Sub

    Private Sub SetStockByTradePortNumer(ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)
        RaiseUserEvent(RaiseIndex.RaiseDs_StockByTradePortNumer, "", _clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.Opt10038, "StockByTradePortNumer", AxKH, e), Nothing)
    End Sub

    Private Sub Setopw00018(ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)
        RaiseUserEvent(RaiseIndex.RaiseDs_Opw00018, "", _clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.Opw00018, "Opw00018", AxKH, e), Nothing)
    End Sub

    Private Sub Setopt10085(ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)
        RaiseUserEvent(RaiseIndex.RaiseDs_opt10085, "", _clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt10085, "opt10085", AxKH, e), Nothing)
    End Sub

    Private Sub Setopt10003(ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)
        RaiseUserEvent(RaiseIndex.RaiseDs_opt10003, "", _clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt10003, "opt10003", AxKH, e), Nothing)
    End Sub

    Private Sub Setopt10006(ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)
        RaiseUserEvent(RaiseIndex.RaiseDs_opt10006, "", _clsKiwoomDefine.KiwoomDefineDic(clsKiwoomDefine.KiwoomDefine.opt10006, "opt10006", AxKH, e), Nothing)
    End Sub
#End Region

#Region " 실시간 응답 이벤트 "
    Private Sub AxKH_OnReceiveRealData(ByVal sender As Object, ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEvent) Handles AxKH.OnReceiveRealData
        If Trim(e.sRealData) <> "" Then
            RaiseUserEvent(RaiseIndex.RaiseDs_OnReceiveRealData, "", _clsRealKiwoomDefine.RealKiwoomDefineDic(AxKH, e), Nothing)
        End If
    End Sub
#End Region

#Region " Koa Studio TxData 함수 "

#Region " Fn_InScreenNo "
    Private Function Fn_InScreenNo(ByVal screenNo As String, ByVal optCode As String, ByVal optName As String) As Boolean
        Select Case InScreenNo(screenNo, optCode, optName)
            Case ReturnScreenNo.Exists
                AxKH.SetRealRemove(screenNo, "ALL")
                Return True
            Case ReturnScreenNo.Fail
                MsgBox(" 화면번호 입력 실패했습니다.", MsgBoxStyle.Information)
                Return False
            Case ReturnScreenNo.Success
                Return True
        End Select
    End Function

    Private Function Fn_InOptKWFidScreenNo(ByVal screenNo As String, ByVal stockCode As String) As Boolean
        Select Case InOptKWFidScreenNo(screenNo, stockCode)
            Case ReturnScreenNo.Exists
                ' AxKH.SetRealRemove(screenNo, "ALL")
                Return True
            Case ReturnScreenNo.Fail
                MsgBox(" 화면번호 입력 실패했습니다.", MsgBoxStyle.Information)
                Return False
            Case ReturnScreenNo.Success
                Return True
        End Select
    End Function
#End Region

#Region " Tran 수신시 이벤트(OnReceiveTrData) 사용 함수들 "

#Region " GetStockBaseInfo                      | 종목 기본 내역을 가져온다. "
    ' Tran 수신시 이벤트(OnReceiveTrData)
    Public Sub GetStockBaseInfo(ByVal stockCode As String, ByVal screenNum As String)
        If Fn_InScreenNo(screenNum, "opt10001", "주식기본정보") = True Then

            If EVENT_STATUS.STATUS_OnReceiveTrData = EventOff Then
                EventOnOff(EventTypeIndex.OnReceiveTrData, EventOn)
            End If

            'If EVENT_STATUS.STATUS_OnReceiveRealData = EventOn Then
            '    EventOnOff(EventTypeIndex.OnReceiveRealData, EventOff)
            'End If

            AxKH.SetInputValue("종목코드", stockCode)
            AxKH.CommRqData("주식기본정보", "opt10001", "0", screenNum)

        End If
    End Sub
    ' Tran 수신시 이벤트(OnReceiveTrData)
    Public Sub GetStockBaseInfoOnSetControl(ByVal stockCode As String, ByVal screenNum As String, ByVal oCon As Object)
        If Fn_InScreenNo(screenNum, "opt10001", "주식기본정보") = True Then

            If EVENT_STATUS.STATUS_OnReceiveTrData = EventOff Then
                EventOnOff(EventTypeIndex.OnReceiveTrData, EventOn)
            End If

            'If EVENT_STATUS.STATUS_OnReceiveRealData = EventOn Then
            '    EventOnOff(EventTypeIndex.OnReceiveRealData, EventOff)
            'End If

            _oCon = oCon
            AxKH.SetInputValue("종목코드", stockCode)
            AxKH.CommRqData("주식기본정보(컨트롤)", "opt10001", "0", screenNum)


        End If
    End Sub
#End Region

#Region " GetDayStockBaseInfo                   | 기준일자별 내역을 가져온다. "
    ' Tran 수신시 이벤트(OnReceiveTrData)
    Public Sub GetDayStockBaseInfo(ByVal stockCode As String, ByVal screenNum As String)
        If Fn_InScreenNo(screenNum, "OPT10081", "주식일봉차트조회") = True Then

            If EVENT_STATUS.STATUS_OnReceiveTrData = EventOff Then
                EventOnOff(EventTypeIndex.OnReceiveTrData, EventOn)
            End If

            'If EVENT_STATUS.STATUS_OnReceiveRealData = EventOn Then
            '    EventOnOff(EventTypeIndex.OnReceiveRealData, EventOff)
            'End If

            AxKH.SetInputValue("종목코드", stockCode)
            AxKH.SetInputValue("기준일자", DateTime.Now.ToString("yyyyMMdd"))
            ' 0 or 1, 수신데이터 1:유상증자, 2:무상증자, 4:배당락, 8:액면분할, 16:액면병합, 32:기업합병, 64:감자, 256:권리락
            AxKH.SetInputValue("수정주가구분", 1)
            AxKH.CommRqData("주식일봉차트조회", "OPT10081", "0", screenNum)

        End If
    End Sub
#End Region

#Region " GetDayStockBaseInfo                   | 기준일자별 내역을 가져온다. "
    ' Tran 수신시 이벤트(OnReceiveTrData)
    Public Sub GetDayStockBaseInfo(ByVal stockCode As String, ByVal screenNum As String, ByVal isModPrice As Integer)
        If Fn_InScreenNo(screenNum, "OPT10081", "주식일봉차트조회") = True Then

            If EVENT_STATUS.STATUS_OnReceiveTrData = EventOff Then
                EventOnOff(EventTypeIndex.OnReceiveTrData, EventOn)
            End If

            'If EVENT_STATUS.STATUS_OnReceiveRealData = EventOn Then
            '    EventOnOff(EventTypeIndex.OnReceiveRealData, EventOff)
            'End If

            AxKH.SetInputValue("종목코드", stockCode)
            AxKH.SetInputValue("기준일자", CDateTime.FormatDate(Now.Date))
            AxKH.SetInputValue("수정주가구분", isModPrice)
            AxKH.CommRqData("주식일봉차트조회", "OPT10081", "0", screenNum)

        End If
    End Sub
#End Region

#Region " GetDayStockTradePort                  | 거래원 요청"
    ' Tran 수신시 이벤트(OnReceiveTrData)
    Public Sub GetDayStockTradePort(ByVal stockCode As String, ByVal screenNum As String, ByVal stdDate As String)
        If Fn_InScreenNo(screenNum, "OPT10002", "주식거래원요청") = True Then

            If EVENT_STATUS.STATUS_OnReceiveTrData = EventOff Then
                EventOnOff(EventTypeIndex.OnReceiveTrData, EventOn)
            End If

            'If EVENT_STATUS.STATUS_OnReceiveRealData = EventOn Then
            '    EventOnOff(EventTypeIndex.OnReceiveRealData, EventOff)
            'End If

            AxKH.SetInputValue("종목코드", stockCode)
            AxKH.SetInputValue("기준일자", stdDate)
            AxKH.SetInputValue("수정주가구분", 0)
            AxKH.CommRqData("주식거래원요청", "OPT10002", "0", screenNum)
        End If
    End Sub
#End Region

#Region " GetTopTradePort                       | 종목별 순위 요청 "
    ' Tran 수신시 이벤트(OnReceiveTrData)
    Public Sub GetTopTradePort(ByVal stockCode As String, ByVal screenNum As String, ByVal fromDate As String, ByVal toDate As String)
        If Fn_InScreenNo(screenNum, "OPT10038", "종목별증권사순위요청") = True Then

            If EVENT_STATUS.STATUS_OnReceiveTrData = EventOff Then
                EventOnOff(EventTypeIndex.OnReceiveTrData, EventOn)
            End If

            'If EVENT_STATUS.STATUS_OnReceiveRealData = EventOn Then
            '    EventOnOff(EventTypeIndex.OnReceiveRealData, EventOff)
            'End If

            AxKH.SetInputValue("종목코드", stockCode)
            AxKH.SetInputValue("시작일자", fromDate)
            AxKH.SetInputValue("종료일자", toDate)
            AxKH.SetInputValue("조회구분", 0)
            AxKH.CommRqData("종목별증권사순위요청", "OPT10038", "0", screenNum)
        End If
    End Sub
#End Region

#Region " GetOpt10003                  | 체결정보요청. "
    ' Tran 수신시 이벤트(OnReceiveTrData)
    Public Sub GetOpt10003(ByVal stockCode As String, ByVal screenNum As String)
        If Fn_InScreenNo(screenNum, "OPT10003", "체결정보요청|" & stockCode) = True Then

            If EVENT_STATUS.STATUS_OnReceiveTrData = EventOff Then
                EventOnOff(EventTypeIndex.OnReceiveTrData, EventOn)
            End If

            AxKH.SetInputValue("종목코드", stockCode)
            AxKH.CommRqData("체결정보요청|" & stockCode, "OPT10003", "0", screenNum)
        End If
    End Sub
#End Region


#End Region

#Region " 조건검색 이벤트 사용 함수들 "

#Region " GetUserConditionLoad                  | 조건검색을 가져온다. "
    ' 조건검색 조회응답 이벤트(OnReceiveTrCondition)
    Public Sub GetUserConditionLoad()
        AxKH.GetConditionLoad()
    End Sub
#End Region

#Region " GetUserConditionStockLoad             | 조건검색에 따른 세부종목을 가져온다. "
    ' 로컬에 사용자조건식 저장 성공여부 응답 이벤트(OnReceiveConditionVer)
    ''' <summary>
    ''' 조건 검색 수신
    ''' </summary>
    ''' <param name="screenNo"></param>
    ''' <param name="conditionName"></param>
    ''' <param name="conditionNo"></param>
    ''' <param name="searchType"> 0 - 조건검색 1 - 실시간 </param>
    ''' <remarks></remarks>
    Public Sub GetUserConditionStockLoad(ByVal screenNo As String, ByVal conditionName As String, ByVal conditionNo As String, ByVal searchType As Integer)
        AxKH.SendCondition(screenNo, conditionName, conditionNo, searchType)
    End Sub
#End Region

#End Region

#Region " GetOptKWFid                           | 관심종목 RealData 요청 "
    Public Sub GetOptKWFid(ByVal stockCode As String, ByVal nCount As Integer, ByVal screenNum As String)
        'If Fn_InOptKWFidScreenNo(screenNum, stockCode) = True Then
            AxKH.CommKwRqData(stockCode, 0, nCount, 0, "관심종목정보요청", screenNum)
        'End If
    End Sub
#End Region

#Region " 실시간 시세를 가져온다. "

    '[SetRealReg() 함수]

    'SetRealReg(
    'BSTR strScreenNo,   // 화면번호
    'BSTR strCodeList,   // 종목코드 리스트
    'BSTR strFidList,  // 실시간 FID리스트
    'BSTR strOptType   // 실시간 등록 타입, 0또는 1
    ')

    '실시간 시세를 받으려는 종목코드와 FID 리스트를 이용해서 실시간 시세를 등록하는 함수입니다.
    '한번에 등록가능한 종목과 FID갯수는 100종목, 100개 입니다.
    '실시간 등록타입을 0으로 설정하면 등록한 종목들은 실시간 해지되고 등록한 종목만 실시간 시세가 등록됩니다.
    '실시간 등록타입을 1로 설정하면 먼저 등록한 종목들과 함께 실시간 시세가 등록됩니다

    '------------------------------------------------------------------------------------------------------------------------------------

    '[실시간 시세등록 예시]
    'OpenAPI.SetRealReg(_T("0150"), _T("039490"), _T("9001;302;10;11;25;12;13"), "0");  // 039490종목만 실시간 등록
    'OpenAPI.SetRealReg(_T("0150"), _T("000660"), _T("9001;302;10;11;25;12;13"), "1");  // 000660 종목을 실시간 추가등록


    Public Sub SetRealReg(ByVal screenNo As String, ByVal codeList As String, ByVal fidList As String, ByVal realType As String)
        AxKH.SetRealReg(screenNo, codeList, fidList, realType)
    End Sub

    Public Sub SetRealRemove(ByVal screenNo As String, ByVal codeList As String)
        AxKH.SetRealRemove(screenNo, codeList)
    End Sub
#End Region

#Region " GetAccount                            | 계좌정보를 가져온다 "
    Public Sub GetAccount()
        Dim ds As New DataSet, dr As DataRow
        Dim dt As New DataTable("ACCNO")

        With dt.Columns
            .Add("ACCNO")
        End With


        Dim arrAccNo As String() = Split(AxKH.GetLoginInfo("ACCNO"), ";")

        For i As Integer = 0 To UBound(arrAccNo) - 1
            dr = dt.Rows.Add()

            dr("ACCNO") = arrAccNo(i)

        Next

        ds.Tables.Add(dt)

        _AccNo = ds

    End Sub
#End Region

#End Region

#Region " Koa Studio Order 함수 & 계좌 관련 "
    ' 주문과 관련한 이벤트 함수는 OnReceiveMsg(), OnReceiveTRData(), OnReceiveChejan()
    '  주문 ---> 접수 ---> 체결1 ---> 잔고1  ---> 체결2  ---> 잔고2... ---> 체결n  ---> 잔고n
    ' 모의투자에서는 지정가 주문과 시장가 주문만 가능합니다.
    Public Enum OrderType
        신규매수 = 1
        신규매도 = 2
        매수취소 = 3
        매도취소 = 4
        매수정정 = 5
        매도정정 = 6
    End Enum

    ' 주문 접수/확인 수신시 이벤트(OnReceiveChejanData)
    Private Sub AxKH_OnReceiveChejanData(ByVal sender As Object, ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveChejanDataEvent) Handles AxKH.OnReceiveChejanData
        RaiseUserEvent(RaiseIndex.RaiseDs_OnReceiveChejanData, "", _clsOrderDefine.GetChejanFidList(AxKH, e), Nothing)
    End Sub

    ' OnReceiveChejanData
    Public Sub SendOrder(ByVal sRQName As String, ByVal screenNo As String, ByVal accNo As String, ByVal eOrderType As OrderType, _
                        ByVal stockCode As String, ByVal qty As Integer, ByVal price As Integer, ByVal tradeGb As String, ByVal orgOrderNo As String)
        If InTradingScreenNo(screenNo, sRQName, stockCode) = ReturnScreenNo.Success Then
            AxKH.SendOrder(sRQName, screenNo, accNo, eOrderType, stockCode, Math.Abs(qty), Math.Abs(price), tradeGb, orgOrderNo)
        End If

    End Sub

    Public Sub Getopw00018(ByVal accNo As String, ByVal ps As String, ByVal psgb As String, ByVal searchGb As Integer, ByVal screenNo As String)

        If Fn_InScreenNo(screenNo, "opw00018", "계좌평가잔고내역요청") = True Then
            AxKH.SetInputValue("계좌번호", accNo)
            AxKH.SetInputValue("비밀번호", "")
            AxKH.SetInputValue("비밀번호입력매체구분", "00")
            AxKH.SetInputValue("조회구분", "1")
            AxKH.CommRqData("계좌평가잔고내역요청", "opw00018", "0", screenNo)
        End If

    End Sub

    Public Sub Getopt10085(ByVal accNo As String, ByVal screenNo As String)
        If Fn_InScreenNo(screenNo, "opt10085", "계좌수익률요청 ") = True Then
            AxKH.SetInputValue("계좌번호", accNo)
            AxKH.CommRqData("계좌수익률요청", "opt10085", "0", screenNo)
        End If
    End Sub

    Public Sub GetOpt10006(ByVal stockCode As String, ByVal screenNo As String)
        If Fn_InScreenNo(screenNo, "OPT10006", "주식시세요청|" & stockCode) = True Then
            AxKH.SetInputValue("종목코드", stockCode)
            AxKH.CommRqData("주식시세요청|" & stockCode, "OPT10006", "0", screenNo)
        End If
    End Sub

    Private Sub SetOrderResultDataSet(ByVal ds As DataSet)
        If _OrderResult Is Nothing = True Then
            Dim dt As DataTable = ds.Tables(0)

            _OrderResult.Tables.Add(dt.Copy)
        ElseIf _OrderResult.Tables.Count = 0 Then
            Dim dt As DataTable = ds.Tables(0)

            _OrderResult.Tables.Add(dt.Copy)
        Else

            Dim dt As DataTable = ds.Tables(0)
            Dim drValue As DataRow

            For Each dr As DataRow In ds.Tables(0).Rows
                drValue = _OrderResult.Tables(0).NewRow()
                For i As Integer = 0 To dr.ItemArray.Length - 1
                    drValue.Item(i) = dr.Item(i)
                Next

                _OrderResult.Tables(0).Rows.Add(drValue)
            Next



        End If
    End Sub
#End Region

#Region " DisconnectRealData                    | RealData 종료 "
    Public Sub DisconnectRealData(ByVal ScreenNo As String)
        'Dim blnTrue As Boolean
        'For Each dr In _dtOptKWFidScreenNo.Select("SCREEN_NO = '" & ScreenNo & "'")
        '    'If dr("SCREEN_NO").ToString.Trim = ScreenNo Then
        '    _dtOptKWFidScreenNo.Rows.Remove(dr)
        '    blnTrue = True
        '    'End If
        'Next

        'If blnTrue = True Then
        AxKH.DisconnectRealData(ScreenNo)
        'Application.DoEvents()
        'System.Threading.Thread.Sleep(100)
        'End If
    End Sub
#End Region

#Region " 화면번호관리 "

    ' 관심종목 : 1101 ~ 1199(StockId1),  1201 ~ 1299(StockId2), 1301 ~ 1399(StockId3), 1401 ~ 1499(StockId4), 1501 ~ 1599(StockId5)
    ' 개별관심종목 : 1101 ~ 1199(StockId1),  1201 ~ 1299(StockId2), 1301 ~ 1399(StockId3), 1401 ~ 1499(StockId4), 1501 ~ 1599(StockId5)
    ' 주문 : 8000~8999
    ' 실시간 종목 데이터 Tx수신 : 7001~7999

#Region " 관심종목(1101~1599) "
    Private Sub SetDtScreenNoDt()
        With _dtScreenNo.Columns
            .Add("SCREEN_NO")
            .Add("OPT_CODE")
            .Add("OPT_NAME")
        End With
    End Sub

    Public Enum ReturnScreenNo
        Success
        Fail
        Exists
    End Enum

#Region " ScreenNo Return "
    Public Enum StockIdIndex
        Stock_Id1
        Stock_Id2
        Stock_Id3
        Stock_Id4
        Stock_Id5
    End Enum

    ''' <summary>
    ''' 관심종목의 화면번호를 만든다.
    ''' </summary>
    ''' <param name="stockId"></param>
    ''' <param name="favId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReceiveFavScreenNo(ByVal stockId As StockIdIndex, ByVal favId As String) As String
        If Len(favId) = 3 Then Return ""

        Dim screenNo As String = ""
        Dim screenNo2th As String = ""

        If Len(favId) = 2 Then
            screenNo2th = favId
        Else
            screenNo2th = "0" & favId
        End If

        Select Case stockId
            Case StockIdIndex.Stock_Id1
                screenNo = "11" & screenNo2th
            Case StockIdIndex.Stock_Id2
                screenNo = "12" & screenNo2th
            Case StockIdIndex.Stock_Id3
                screenNo = "13" & screenNo2th
            Case StockIdIndex.Stock_Id4
                screenNo = "14" & screenNo2th
            Case StockIdIndex.Stock_Id5
                screenNo = "15" & screenNo2th
        End Select

        Return screenNo

    End Function

    Public Function ConditionSearchScreenNo()
        Return "2100"
    End Function
#End Region

#End Region

#Region " 개별종목 "

#Region " InScreenNo "
    Public Function InScreenNo(ByVal screenNo As String, ByVal optCode As String, ByVal optName As String) As ReturnScreenNo

        Try

            Dim blnChk As Boolean = False

            With _dtScreenNo
                Dim dr As DataRow
                Dim drReader As DataRow

                If _dtScreenNo.Rows.Count < 1 Then
                    dr = _dtScreenNo.Rows.Add

                    dr("SCREEN_NO") = screenNo
                    dr("OPT_CODE") = optCode
                    dr("OPT_NAME") = optName

                    Return ReturnScreenNo.Success
                End If

                For Each drReader In _dtScreenNo.Rows
                    If Trim(drReader("SCREEN_NO").ToString()) = screenNo Then
                        blnChk = True
                    End If
                Next

                If blnChk = True Then

                    Return ReturnScreenNo.Exists

                Else
                    dr = _dtScreenNo.Rows.Add

                    dr("SCREEN_NO") = screenNo
                    dr("OPT_CODE") = optCode
                    dr("OPT_NAME") = optName

                    Return ReturnScreenNo.Success

                End If

            End With
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Exclamation)
            Return ReturnScreenNo.Fail
        End Try

    End Function
#End Region

#End Region

#Region " 매수, 매도 화면번호 관리 (8001~8999)"

    Private Sub SetTradingScreenNoDt()
        With _dtTradingScreenNo.Columns
            .Add("SCREEN_NO")
            .Add("TRADING_NAME")
            .Add("STOCK_CODE")
        End With
    End Sub

#Region " InTradingScreenNo "
    Public Function InTradingScreenNo(ByVal screenNo As String, ByVal tradingName As String, ByVal stockCode As String) As ReturnScreenNo

        Try

            Dim blnChk As Boolean = False

            With _dtTradingScreenNo
                Dim dr As DataRow
                Dim drReader As DataRow

                If _dtTradingScreenNo.Rows.Count < 1 Then
                    dr = _dtTradingScreenNo.Rows.Add

                    dr("SCREEN_NO") = screenNo
                    dr("TRADING_NAME") = tradingName
                    dr("STOCK_CODE") = stockCode

                    Return ReturnScreenNo.Success
                End If

                For Each drReader In _dtTradingScreenNo.Rows
                    If Trim(drReader("SCREEN_NO").ToString()) = screenNo Then
                        blnChk = True
                    End If
                Next

                If blnChk = True Then

                    Return ReturnScreenNo.Exists

                Else
                    dr = _dtTradingScreenNo.Rows.Add

                    dr("SCREEN_NO") = screenNo
                    dr("TRADING_NAME") = tradingName
                    dr("STOCK_CODE") = stockCode

                    Return ReturnScreenNo.Success

                End If

            End With
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Exclamation)
            Return ReturnScreenNo.Fail
        End Try

    End Function
#End Region

#Region " GetTradingScreenNo "
    Public Function GetTradingScreenNo(ByVal tradingName As String, ByVal stockCode As String) As String
        Dim screeNo As String = ""
        Dim blnExists As Boolean = False

        If _dtTradingScreenNo.Rows.Count = 0 Then

            Return "8001"

        Else
            Dim dv As DataView

            dv = New DataView(_dtTradingScreenNo)

            For i As Integer = 8001 To 8999

                blnExists = False

                dv.RowFilter = String.Format("SCREEN_NO = '{0}'", i.ToString)

                For Each drRowView As DataRowView In dv
                    blnExists = True
                Next

                If blnExists = False Then
                    Return i.ToString()
                End If

            Next

        End If

        Return "fail"

    End Function

    Public Function DeleteTradingScreenNo(ByVal screenNo As String) As Boolean

        Try
            Dim row As Integer = 0

            For Each drReader As DataRow In _dtTradingScreenNo.Rows
                If Trim(drReader("SCREEN_NO").ToString()) = screenNo Then
                    _dtTradingScreenNo.Rows(row).Delete()
                    Return True
                End If

                row = row + 1
            Next

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Exclamation)
        End Try

        Return False

    End Function

#End Region

#End Region

#Region " 실시간 종목 데이터 Tx수신 (7001~7999) "
    Private Sub SetOptKWFidScreenNoDt()
        With _dtOptKWFidScreenNo.Columns
            .Add("SCREEN_NO")
            .Add("STOCK_CODE")
        End With
    End Sub

#Region " InOptKWFidScreenNo "
    Public Function InOptKWFidScreenNo(ByVal screenNo As String, ByVal stockCode As String) As ReturnScreenNo

        Try

            Dim blnChk As Boolean = False
            With _dtOptKWFidScreenNo
                If screenNo = "" Then
                    Dim dr As DataRow
                    Dim drReader As DataRow

                    If _dtOptKWFidScreenNo.Rows.Count < 1 Then
                        Return ReturnScreenNo.Success
                    Else
                        Dim arrStockCode As String()

                        For Each drReader In _dtOptKWFidScreenNo.Rows
                            If Len(Trim(drReader("STOCK_CODE"))) > 8 Then

                                arrStockCode = Split(Trim(drReader("STOCK_CODE")), ";")

                                For i As Integer = 0 To UBound(arrStockCode)
                                    If arrStockCode(i) = stockCode Then
                                        blnChk = True
                                        Exit For
                                    End If
                                Next

                                ReDim arrStockCode(0)

                            Else
                                If Trim(drReader("STOCK_CODE").ToString()) = stockCode Then
                                    blnChk = True
                                    Exit For
                                End If
                            End If
                        Next

                        If blnChk = True Then

                            Return ReturnScreenNo.Exists

                        Else
                            For i As Integer = 7001 To 7999
                                If _dtOptKWFidScreenNo.Select("SCREEN_NO = '" & i & "'").Length > 0 Then
                                    Continue For
                                Else
                                    screenNo = i
                                    Exit For
                                End If
                            Next

                            dr = _dtOptKWFidScreenNo.Rows.Add

                            dr("SCREEN_NO") = screenNo
                            dr("STOCK_CODE") = stockCode

                            Return ReturnScreenNo.Success

                        End If
                    End If
                Else
                    Dim dr As DataRow
                    Dim drReader As DataRow

                    If _dtOptKWFidScreenNo.Rows.Count < 1 Then
                        dr = _dtOptKWFidScreenNo.Rows.Add

                        dr("SCREEN_NO") = screenNo
                        dr("STOCK_CODE") = stockCode

                        Return ReturnScreenNo.Success
                    End If

                    For Each drReader In _dtOptKWFidScreenNo.Rows
                        If Trim(drReader("SCREEN_NO").ToString()) = screenNo Then
                            blnChk = True
                        End If
                    Next

                    If blnChk = True Then

                        Return ReturnScreenNo.Exists

                    Else
                        dr = _dtOptKWFidScreenNo.Rows.Add

                        dr("SCREEN_NO") = screenNo
                        dr("STOCK_CODE") = stockCode

                        Return ReturnScreenNo.Success

                    End If


                End If
            End With

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Exclamation)
            Return ReturnScreenNo.Fail
        End Try

    End Function
#End Region

#Region " GetOptKWFidScreenNo "
    Public _scrNum As Integer = 7000

    Public Function GetScrNum() As Integer
        If _scrNum > 7999 Then
            _scrNum = 7000
        Else
            _scrNum = _scrNum + 1
        End If
        Return _scrNum
    End Function
    Public Function GetOptKWFidScreenNo(ByVal stockCode As String) As String
        Dim screeNo As String = ""
        Dim dr As DataRow
        Dim blnExists As Boolean = False

        If _dtOptKWFidScreenNo.Rows.Count = 0 Then
            dr = _dtOptKWFidScreenNo.Rows.Add

            dr("SCREEN_NO") = GetScrNum()
            dr("STOCK_CODE") = stockCode

            Return dr("SCREEN_NO")

        Else
            Dim dv As DataView

            dv = New DataView(_dtOptKWFidScreenNo)
            dv.RowFilter = String.Format("STOCK_CODE = '{0}'", stockCode)
            If dv.Count > 0 Then Return dv(0)("SCREEN_NO").ToString.Trim


            Return GetScrNum()
            'For i As Integer = 7001 To 7999
            '    dv.RowFilter = String.Format("SCREEN_NO = '{0}'", i.ToString)
            '    If dv.Count > 0 Then Continue For
            '    Return i.ToString()
            'Next
        End If

        Return "fail"

    End Function

    Public Function DeleteOptKWFidScreenNo(ByVal screenNo As String) As Boolean

        Try

            For Each drReader As DataRow In _dtOptKWFidScreenNo.Rows
                If Trim(drReader("SCREEN_NO").ToString()) = screenNo Then
                    _dtOptKWFidScreenNo.Rows.Remove(drReader)
                    Return True
                End If
            Next

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Exclamation)
        End Try

        Return False

    End Function

#End Region

#End Region

#End Region

End Class