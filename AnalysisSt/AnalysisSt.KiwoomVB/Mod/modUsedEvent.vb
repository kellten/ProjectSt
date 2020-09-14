Module modUsedEvent

    Public Structure UsedOpt
        Public OPT10059QTY As Boolean
        Public OPT10059PRICE As Boolean
        Public OPT10081 As Boolean
    End Structure

    Public Structure UsedEvent
        Public AxKH_OnEventConnect As Boolean
        Public AxKH_OnReceiveMsg As Boolean
        Public AxKH_OnReceiveChejanData As Boolean
        Public AxKH_OnReceiveConditionVer As Boolean
        Public AxKH_OnReceiveTrCondition As Boolean
        Public AxKH_OnReceiveRealData As Boolean
        Public AxKH_OnReceiveTrData As Boolean
    End Structure

    Public Enum OptIndex
        Opt10059Qty
        Opt10059Price
        Opt10081
    End Enum

    Public Enum EventIndex
        AxKHOnEventConnect
        AxKHOnReceiveMsg
        AxKHOnReceiveChejanData
        AxKHOnReceiveConditionVer
        AxKHOnReceiveTrCondition
        AxKHOnReceiveRealData
        AxKHOnReceiveTrData
    End Enum

    Private _usedOpt As UsedOpt
    Private _usedEvent As UsedEvent

    Public Sub InitUsedOpt()
        _usedOpt.OPT10059QTY = False
        _usedOpt.OPT10059PRICE = False
        _usedOpt.OPT10081 = False

        _usedEvent.AxKH_OnEventConnect = False
        _usedEvent.AxKH_OnReceiveMsg = False
        _usedEvent.AxKH_OnReceiveChejanData = False
        _usedEvent.AxKH_OnReceiveConditionVer = False
        _usedEvent.AxKH_OnReceiveTrCondition = False
        _usedEvent.AxKH_OnReceiveRealData = False
        _usedEvent.AxKH_OnReceiveTrData = False

    End Sub

#Region " OPT "
    ''' <summary>
    ''' OPT 사용 여부를 체크한다.
    ''' </summary>
    ''' <param name="optId"></param>
    ''' <returns>TRUE - 사용 중 FALSE - 미사용 중</returns>
    ''' <remarks></remarks>
    Public Function ReturnUsedOpt(ByVal optId As OptIndex) As Boolean
        Select Case optId
            Case OptIndex.Opt10059Qty
                Return _usedOpt.OPT10059QTY
            Case OptIndex.Opt10059Price
                Return _usedOpt.OPT10059PRICE
            Case OptIndex.Opt10081
                Return _usedOpt.OPT10081
        End Select

        Return False
    End Function
    ''' <summary>
    ''' OPT 사용 여부를 변경한다.
    ''' </summary>
    ''' <param name="optId"></param>
    ''' <returns>>TRUE - 사용 FALSE - 미사용</returns>
    ''' <remarks></remarks>
    Public Function SetUsedOpt(ByVal optId As OptIndex, ByVal used As Boolean) As Boolean
        Select Case optId
            Case OptIndex.Opt10059Qty
                _usedOpt.OPT10059QTY = used
            Case OptIndex.Opt10059Price
                _usedOpt.OPT10059PRICE = used
            Case OptIndex.Opt10081
                _usedOpt.OPT10081 = used
        End Select

        Return True
    End Function
#End Region

#Region " "
    ''' <summary>
    ''' EVENT 사용 여부를 체크한다.
    ''' </summary>
    ''' <param name="eventId"></param>
    ''' <returns>TRUE - 사용 중 FALSE - 미사용 중</returns>
    ''' <remarks></remarks>
    Public Function ReturnUsedEvent(ByVal eventId As EventIndex) As Boolean
        Select Case eventId
            Case EventIndex.AxKHOnEventConnect
                Return _usedEvent.AxKH_OnEventConnect
            Case EventIndex.AxKHOnReceiveMsg
                Return _usedEvent.AxKH_OnReceiveMsg
            Case EventIndex.AxKHOnReceiveChejanData
                Return _usedEvent.AxKH_OnReceiveChejanData
            Case EventIndex.AxKHOnReceiveConditionVer
                Return _usedEvent.AxKH_OnReceiveConditionVer
            Case EventIndex.AxKHOnReceiveTrCondition
                Return _usedEvent.AxKH_OnReceiveTrCondition
            Case EventIndex.AxKHOnReceiveRealData
                Return _usedEvent.AxKH_OnReceiveRealData
            Case EventIndex.AxKHOnReceiveTrData
                Return _usedEvent.AxKH_OnReceiveTrData
        End Select

        Return False
    End Function
    ''' <summary>
    ''' EVENT 사용 여부를 변경한다.
    ''' </summary>
    ''' <param name="eventId"></param>
    ''' <returns>>TRUE - 사용 FALSE - 미사용</returns>
    ''' <remarks></remarks>
    Public Function SetUsedEvent(ByVal eventId As EventIndex, ByVal used As Boolean) As Boolean

        Select Case eventId
            Case EventIndex.AxKHOnEventConnect
                _usedEvent.AxKH_OnEventConnect = used
            Case EventIndex.AxKHOnReceiveMsg
                _usedEvent.AxKH_OnReceiveMsg = used
            Case EventIndex.AxKHOnReceiveChejanData
                _usedEvent.AxKH_OnReceiveChejanData = used
            Case EventIndex.AxKHOnReceiveConditionVer
                _usedEvent.AxKH_OnReceiveConditionVer = used
            Case EventIndex.AxKHOnReceiveTrCondition
                _usedEvent.AxKH_OnReceiveTrCondition = used
            Case EventIndex.AxKHOnReceiveRealData
                _usedEvent.AxKH_OnReceiveRealData = used
            Case EventIndex.AxKHOnReceiveTrData
                _usedEvent.AxKH_OnReceiveTrData = used
        End Select

        Return True
    End Function
#End Region
    
End Module
