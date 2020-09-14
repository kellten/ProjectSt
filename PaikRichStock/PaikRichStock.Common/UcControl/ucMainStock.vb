Public Class ucMainStock

    Private _loginStatus As LoginStatus = LoginStatus.DisConnect
    Public _allStockDataset As DataSet

#Region " Enum "
    Public Enum EventTypeIndex
        statusOnEventConnect
        statusOnReceiveConditionVer
        statusOnReceiveTrCondition
        statusOnReceiveTrData
    End Enum

    Public Enum SearchName
        PreriodLowestSearch
        PreriodLowestEndSearch
    End Enum
#End Region

#Region " Event Register "
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

#End Region

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

#Region " Structure "
    Public EVENT_STATUS As EventStatus

    Public Structure EventStatus
        Public STATUS_ONEVENTCONNECT As Boolean
        Public STATUS_ONRECEIVECONDITIONVER As Boolean
        Public STATUS_ONRECEIVETRCONDITION As Boolean
        Public STATUS_ONRECEIVETRDATA As Boolean

        Public Sub InitEventStatus()
            STATUS_ONEVENTCONNECT = True
            STATUS_ONRECEIVECONDITIONVER = True
            STATUS_ONRECEIVETRCONDITION = True
            STATUS_ONRECEIVETRDATA = True
        End Sub
    End Structure
#End Region

#Region " Event 상태 Property & Event "
    Private _statusOnEventConnect As Boolean
    Private _statusOnReceiveConditionVer As Boolean
    Private _statusOnReceiveTrCondition As Boolean
    Private _statusOnReceiveTrData As Boolean

#Region " 통신 연결 상태 "
    Public Property StatusOnEventConnect() As Boolean
        Get
            Return _statusOnEventConnect
        End Get
        Set(ByVal value As Boolean)
            _statusOnEventConnect = value
            EventOnOff(EventTypeIndex.statusOnEventConnect, value)
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
        Else
            ConnectionReturnValue(LoginSucessStatus.Fail)
            _loginStatus = LoginStatus_DisConnect
        End If
    End Sub

    Public Sub ConnectionReturnValue(ByVal status As Integer)
        Select Case status
            Case LoginSucessStatus_Success
                lblLoginStatus.Text = "로그인 성공"
                _allStockDataset = UserGetCodeListByMarket()
            Case LoginSucessStatus_Fail
                lblLoginStatus.Text = "로그인 실패"
                _allStockDataset.Reset()
        End Select

        RaiseConnection(lblLoginStatus.Text)

    End Sub

    Private Sub RaiseConnection(ByVal status As String)
        RaiseEvent OnConnection(status)
    End Sub
#End Region
   
#Region " 로컬에 사용자조건식 저장 성공여부 응답 "
    Public Property StatusOnReceiveConditionVer() As Boolean
        Get
            Return _statusOnReceiveConditionVer
        End Get
        Set(ByVal value As Boolean)
            _statusOnReceiveConditionVer = value
            EventOnOff(EventTypeIndex.statusOnReceiveConditionVer, value)
        End Set
    End Property
    ''' <summary>
    ''' 로컬에 사용자조건식 저장 성공여부 응답 이벤트
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AxKH_OnReceiveConditionVer(ByVal sender As Object, ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveConditionVerEvent) Handles AxKH.OnReceiveConditionVer
        If e.lRet <> 1 Then Exit Sub

        Dim strList As String = AxKH.GetConditionNameList()
        SetUserConditionList(strList)
    End Sub


    Public Sub SetUserConditionList(ByVal strConList As String)
        Dim ds As New DataSet, dr As DataRow
        Dim dt As New DataTable("CondiList")

        With dt.Columns
            .Add("CONDI_SEQ")
            .Add("CONDI_NAME")
        End With


        Dim arrConList As String() = Split(strConList, ";")
        Dim arrConListSplit As String()

        For i As Integer = 0 To UBound(arrConList) - 1
            dr = dt.Rows.Add()

            arrConListSplit = Split(arrConList(i), "^")

            dr("CONDI_SEQ") = arrConListSplit(0)
            dr("CONDI_NAME") = arrConListSplit(1)

        Next

        ds.Tables.Add(dt)

        RaiseDsGetConditionList(ds)

    End Sub

    Private Sub RaiseDsGetConditionList(ByVal ds As DataSet)
        RaiseEvent OnDsGetConditionList(ds)
    End Sub
#End Region
   
#Region " 조건검색 조회응답 "
    Public Sub GetUserConditionLoad()
        AxKH.GetConditionLoad()
    End Sub

    Public Property StatusOnReceiveTrCondition() As Boolean
        Get
            Return _statusOnReceiveTrCondition
        End Get
        Set(ByVal value As Boolean)
            _statusOnReceiveTrCondition = value
            EventOnOff(EventTypeIndex.statusOnReceiveTrCondition, value)
        End Set
    End Property

    ''' <summary>
    ''' 조건검색 조회응답 이벤트
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AxKH_OnReceiveTrCondition(ByVal sender As Object, ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrConditionEvent) Handles AxKH.OnReceiveTrCondition
        If Trim(e.strCodeList) <> "" Then
            SetUserConditionStockList(e.strCodeList)
        End If
    End Sub

    Public Sub SetUserConditionStockList(ByVal strCodeList As String)
        Dim dt As New DataTable("CondiStockList")
        Dim dr As DataRow, ds As New DataSet
        Dim arrStockCode As String() = Split(strCodeList, ";")

        With dt.Columns
            .Add("STOCK_CODE")
            .Add("STOCK_NAME")
        End With

        For i As Integer = 0 To UBound(arrStockCode)
            dr = dt.Rows.Add()
            dr("STOCK_CODE") = arrStockCode(i)
            dr("STOCK_NAME") = AxKH.GetMasterCodeName(arrStockCode(i))
        Next

        ds.Tables.Add(dt)

        RaiseDsSetConditionList(ds)

    End Sub

    Private Sub RaiseDsSetConditionList(ByVal ds As DataSet)
        RaiseEvent OnDsSetConditionList(ds)
    End Sub
#End Region

#Region " Tran 수신 "
    Public Property StatusOnReceiveTrData() As Boolean
        Get
            Return _statusOnReceiveTrData
        End Get
        Set(ByVal value As Boolean)
            _statusOnReceiveTrData = value
            EventOnOff(EventTypeIndex.statusOnReceiveTrData, value)
        End Set
    End Property
    ''' <summary>
    ''' Tran 수신시 이벤트
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AxKH_OnReceiveTrData(ByVal sender As Object, ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent) Handles AxKH.OnReceiveTrData
        Select Case e.sRQName
            Case "주식기본정보"
                SetStockBaseInfo(e)
            Case "주식일봉차트조회"
                SetDayStockBaseInfo(e)
        End Select
    End Sub

    Private Sub SetStockBaseInfo(ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)
        Dim dt As New DataTable("StockBaseInfo")
        Dim dr As DataRow, ds As New DataSet
        Dim nCnt As Integer

        With dt.Columns
            .Add("종목코드")
            .Add("시가총액")
            .Add("시가")
            .Add("고가")
            .Add("저가")
            .Add("기준가")
            .Add("현재가")
            .Add("대비기호")
            .Add("전일대비")
            .Add("등락율")
            .Add("거래량")
        End With

        nCnt = AxKH.GetRepeatCnt(e.sTrCode, e.sRQName)

        For i As Integer = 0 To (nCnt - 1)

            dr = dt.Rows.Add()

            dr("종목코드") = AxKH.GetCommData(e.sTrCode, e.sRQName, i, "종목코드")
            dr("시가총액") = AxKH.GetCommData(e.sTrCode, e.sRQName, i, "시가총액")
            dr("시가") = AxKH.GetCommData(e.sTrCode, e.sRQName, i, "시가")
            dr("고가") = AxKH.GetCommData(e.sTrCode, e.sRQName, i, "고가")
            dr("저가") = AxKH.GetCommData(e.sTrCode, e.sRQName, i, "저가")
            dr("기준가") = AxKH.GetCommData(e.sTrCode, e.sRQName, i, "기준가")
            dr("현재가") = AxKH.GetCommData(e.sTrCode, e.sRQName, i, "현재가")
            dr("대비기호") = AxKH.GetCommData(e.sTrCode, e.sRQName, i, "대비기호")
            dr("전일대비") = AxKH.GetCommData(e.sTrCode, e.sRQName, i, "전일대비")
            dr("등락율") = AxKH.GetCommData(e.sTrCode, e.sRQName, i, "등락율")
            dr("거래량") = AxKH.GetCommData(e.sTrCode, e.sRQName, i, "거래량")


        Next

        ds.Tables.Add(dt)

        RaiseDsBaseInfo(ds)

        ds.Reset()

    End Sub

    Private Sub RaiseDsBaseInfo(ByVal ds As DataSet)
        RaiseEvent OnDsBaseInfo(ds)
    End Sub

    Private Sub SetDayStockBaseInfo(ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent)
        Dim dt As New DataTable("DayStockBaseInfo")
        Dim dr As DataRow, ds As New DataSet
        Dim nCnt As Integer

        With dt.Columns
            .Add("종목코드")
            .Add("시가총액")
            .Add("시가")
            .Add("고가")
            .Add("종가")
            .Add("저가")
            .Add("기준가")
            .Add("현재가")
            .Add("대비기호")
            .Add("전일대비")
            .Add("등락율")
            .Add("거래량")
            .Add("일자")
            .Add("저가MA")
        End With

        nCnt = AxKH.GetRepeatCnt(e.sTrCode, e.sRQName)

        For i As Integer = 0 To (nCnt - 1)

            dr = dt.Rows.Add()

            dr("종목코드") = Trim(AxKH.GetCommData(e.sTrCode, e.sRQName, i, "종목코드"))
            dr("시가총액") = Trim(AxKH.GetCommData(e.sTrCode, e.sRQName, i, "시가총액"))
            dr("시가") = Trim(AxKH.GetCommData(e.sTrCode, e.sRQName, i, "시가"))
            dr("고가") = Trim(AxKH.GetCommData(e.sTrCode, e.sRQName, i, "고가"))
            dr("저가") = Trim(AxKH.GetCommData(e.sTrCode, e.sRQName, i, "저가"))
            dr("기준가") = Trim(AxKH.GetCommData(e.sTrCode, e.sRQName, i, "기준가"))
            dr("현재가") = Trim(AxKH.GetCommData(e.sTrCode, e.sRQName, i, "현재가"))
            dr("대비기호") = Trim(AxKH.GetCommData(e.sTrCode, e.sRQName, i, "대비기호"))
            dr("전일대비") = Trim(AxKH.GetCommData(e.sTrCode, e.sRQName, i, "전일대비"))
            dr("등락율") = Trim(AxKH.GetCommData(e.sTrCode, e.sRQName, i, "등락율"))
            dr("거래량") = Trim(AxKH.GetCommData(e.sTrCode, e.sRQName, i, "거래량"))
            dr("일자") = Trim(AxKH.GetCommData(e.sTrCode, e.sRQName, i, "일자"))

        Next

        ds.Tables.Add(dt)

        ds.Tables.Add(GetLowestMa(20, ds))

        'Dim obj_ParentClm As DataColumn, obj_ChildClm As DataColumn
        'Dim obj_DataRelation As DataRelation

        'obj_ParentClm = ds.Tables("DayStockBaseInfo").Columns("일자")
        'obj_ChildClm = ds.Tables("DayStockBaseInfoMa").Columns("일자")

        'obj_DataRelation = New DataRelation("DayStockBaseInfo_relation", obj_ParentClm, obj_ChildClm)

        'ds.Relations.Add(obj_DataRelation)

        RaiseDayDsBaseInfo(ds)

        ds.Reset()

    End Sub

    Private Sub RaiseDayDsBaseInfo(ByVal ds As DataSet)
        RaiseEvent OnDayDsBaseInfo(ds)
    End Sub

#End Region

    Private Sub EventOnOff(ByVal EventType As EventTypeIndex, ByVal eventOffType As Boolean)

        Select Case EventType
            Case EventTypeIndex.statusOnEventConnect
                If eventOffType = EventOn Then
                    AddHandler AxKH.OnEventConnect, AddressOf AxKH_OnEventConnect
                    EVENT_STATUS.STATUS_ONEVENTCONNECT = EventOn
                Else
                    RemoveHandler AxKH.OnEventConnect, AddressOf AxKH_OnEventConnect
                    RemoveHandler AxKH.OnEventConnect, AddressOf AxKH_OnEventConnect
                    EVENT_STATUS.STATUS_ONEVENTCONNECT = EventOff
                End If
            Case EventTypeIndex.statusOnReceiveConditionVer
                If eventOffType = EventOn Then
                    AddHandler AxKH.OnReceiveConditionVer, AddressOf AxKH_OnReceiveConditionVer
                    EVENT_STATUS.STATUS_ONRECEIVECONDITIONVER = EventOn
                Else
                    RemoveHandler AxKH.OnReceiveConditionVer, AddressOf AxKH_OnReceiveConditionVer
                    RemoveHandler AxKH.OnReceiveConditionVer, AddressOf AxKH_OnReceiveConditionVer
                    EVENT_STATUS.STATUS_ONRECEIVECONDITIONVER = EventOff
                End If
            Case EventTypeIndex.statusOnReceiveTrCondition
                If eventOffType = EventOn Then
                    AddHandler AxKH.OnReceiveTrCondition, AddressOf AxKH_OnReceiveTrCondition
                    EVENT_STATUS.STATUS_ONRECEIVETRCONDITION = EventOn
                Else
                    RemoveHandler AxKH.OnReceiveTrCondition, AddressOf AxKH_OnReceiveTrCondition
                    RemoveHandler AxKH.OnReceiveTrCondition, AddressOf AxKH_OnReceiveTrCondition
                    EVENT_STATUS.STATUS_ONRECEIVETRCONDITION = EventOff
                End If
            Case EventTypeIndex.statusOnReceiveTrData
                If eventOffType = EventOn Then
                    AddHandler AxKH.OnReceiveTrData, AddressOf AxKH_OnReceiveTrData
                    EVENT_STATUS.STATUS_ONRECEIVETRDATA = EventOn
                Else
                    RemoveHandler AxKH.OnReceiveTrData, AddressOf AxKH_OnReceiveTrData
                    RemoveHandler AxKH.OnReceiveTrData, AddressOf AxKH_OnReceiveTrData
                    EVENT_STATUS.STATUS_ONRECEIVETRDATA = EventOff
                End If
        End Select

    End Sub
#End Region

#Region " 함수 "
    '                                           |
#Region " UserGetCodeListByMarket               | Stock List 가져온다. "
    ''' <summary>
    ''' StockList들을 가져온다.
    ''' </summary>
    ''' <returns>List DataSet값</returns>
    ''' <remarks></remarks>
    Public Function UserGetCodeListByMarket() As DataSet
        Dim str1 As String = AxKH.GetCodeListByMarket("0")
        Dim str2 As String = AxKH.GetCodeListByMarket("10")
        Dim arrStockCode1 As String() = Split(str1, ";")
        Dim arrStockCode2 As String() = Split(str2, ";")
        Dim dt As New DataTable("StockList")
        Dim dr As DataRow, ds As New DataSet

        With dt.Columns
            .Add("STOCK_CODE")
            .Add("STOCK_NAME")
        End With

        For i As Integer = 0 To UBound(arrStockCode1)
            dr = dt.Rows.Add()
            dr("STOCK_CODE") = arrStockCode1(i)
            dr("STOCK_NAME") = AxKH.GetMasterCodeName(arrStockCode1(i))
        Next

        For i As Integer = 0 To UBound(arrStockCode2)
            dr = dt.Rows.Add()
            dr("STOCK_CODE") = arrStockCode2(i)
            dr("STOCK_NAME") = AxKH.GetMasterCodeName(arrStockCode2(i))
        Next

        ds.Tables.Add(dt)

        Return ds

    End Function
#End Region

#Region " GetStockBaseInfo                      | 종목 기본 내역을 가져온다. "
    Public Sub GetStockBaseInfo(ByVal stockCode As String, ByVal screenNum As String)
        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.CommRqData("주식기본정보", "opt10001", "0", screenNum)
    End Sub
#End Region

#Region " GetDayStockBaseInfo                   | 기준일자별 내역을 가져온다. "
    Public Sub GetDayStockBaseInfo(ByVal stockCode As String, ByVal screenNum As String)
        AxKH.SetInputValue("종목코드", stockCode)
        AxKH.SetInputValue("기준일자", CDateTime.FormatDate(Now.Date))
        AxKH.SetInputValue("수정주가구분", 0)
        AxKH.CommRqData("주식일봉차트조회", "opt10081", "0", screenNum)
    End Sub
#End Region

#Region " GetUserConditionStockLoad "
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

#Region " 사용자 조건 함수 "

#Region " GetLowestMa                           | 기간별 최저가 이동 평균값 가져오기 "
    Public Function GetLowestMa(ByVal period As Integer, ByVal ds As DataSet) As DataTable
        Dim dt As New DataTable("DayStockBaseInfoMa")
        Dim dr As DataRow
        Dim totalValue As Integer = 0
        Dim cnt As Integer = 0
        Dim lowestValue As Integer = 0
        Dim lowestEndValue As Integer = 0

        With dt.Columns
            .Add("일자")
            .Add("저가MA")
            .Add("기간최저가")
            .Add("기간종가최저가")
            .Add("최저가MA")
            .Add("최저가종가MA")
        End With

        For i As Integer = 0 To (ds.Tables("DayStockBaseInfo").Rows.Count - period - 1)

            dr = dt.Rows.Add()

            totalValue = 0
            lowestValue = 0

            For row As Integer = i To period - 1 + i
                totalValue = totalValue + ds.Tables("DayStockBaseInfo").Rows(row).Item("저가")
            Next

            For row As Integer = i To period - 1 + i
                If lowestValue = 0 Then
                    lowestValue = ds.Tables("DayStockBaseInfo").Rows(row).Item("저가")
                Else
                    If lowestValue >= ds.Tables("DayStockBaseInfo").Rows(row).Item("저가") Then
                        lowestValue = ds.Tables("DayStockBaseInfo").Rows(row).Item("저가")
                    End If
                End If
            Next

            For row As Integer = i To period - 1 + i
                If lowestEndValue = 0 Then
                    lowestEndValue = (ds.Tables("DayStockBaseInfo").Rows(row).Item("종가") + ds.Tables("DayStockBaseInfo").Rows(row).Item("저가")) / 2
                Else
                    If lowestEndValue >= (ds.Tables("DayStockBaseInfo").Rows(row).Item("종가") + ds.Tables("DayStockBaseInfo").Rows(row).Item("저가")) / 2 Then
                        lowestEndValue = (ds.Tables("DayStockBaseInfo").Rows(row).Item("종가") + ds.Tables("DayStockBaseInfo").Rows(row).Item("저가")) / 2
                    End If
                End If
            Next

            dr("기간최저가") = lowestValue
            dr("기간종가최저가") = lowestEndValue
            dr("저가MA") = totalValue / period
            dr("일자") = Trim(ds.Tables("DayStockBaseInfo").Rows(i).Item("일자"))

        Next

        For i As Integer = 0 To dt.Rows.Count - period - 1
            totalValue = 0

            For row As Integer = i To period - 1 + i
                totalValue = totalValue + dt.Rows(row).Item("기간최저가")
            Next

            dt.Rows(i).Item("최저가MA") = totalValue / period

        Next

        For i As Integer = 0 To dt.Rows.Count - period - 1
            totalValue = 0

            For row As Integer = i To period - 1 + i
                totalValue = totalValue + dt.Rows(row).Item("기간종가최저가")
            Next

            dt.Rows(i).Item("최저가종가MA") = totalValue / period

        Next

        Return dt

    End Function
#End Region

#End Region


End Class
