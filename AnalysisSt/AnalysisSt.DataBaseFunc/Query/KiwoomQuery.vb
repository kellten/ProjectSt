Public Class KiwoomQuery
    Private _sql As AnalysisSt.DataBaseFunc.Sql
    Private _arrParam As ArrayParam
    Private _tableName As String
    Private _ds As DataSet

#Region "Constuctor"
    Sub New()
        InitClassVar("EDPB2F011\VADIS", "KIWOOMDB", "DEFAULT")
    End Sub

    Sub New(ByVal tableName As String)
        InitClassVar("EDPB2F011\VADIS", "KIWOOMDB", tableName)
    End Sub

    Sub New(ByVal serverName As String, ByVal databaseName As String)
        InitClassVar(serverName, databaseName, "DEFAULT")
    End Sub

    Sub New(ByVal serverName As String, ByVal databaseName As String, ByVal tableName As String)
        InitClassVar(serverName, databaseName, tableName)
    End Sub

    Sub New(ByVal tableName As String, ByVal cmdTimeOut As Integer)
        InitClassVar("EDPB2F011\VADIS", "KIWOOMDB", tableName, cmdTimeOut)
    End Sub

    Sub New(ByVal persistSecurity As Boolean)
        If persistSecurity = True Then
            InitClassVarpersistSecurity("EDPB2F011\VADIS", "KIWOOMDB", "DEFAULT", "Integrated Security=SSPI;initial catalog= RICHDB;persist security info=false;" & "EDPB2F011\VADIS ;Application Name=AnaylsisSt")
        End If

    End Sub

    Private Sub InitClassVar(ByVal serverName As String, ByVal databaseName As String, _
            ByVal tableName As String)
        _sql = New AnalysisSt.DataBaseFunc.Sql(serverName, databaseName)
        _tableName = tableName
        _arrParam = New ArrayParam()
        _ds = New DataSet()
    End Sub

    Private Sub InitClassVar(ByVal serverName As String, ByVal databaseName As String, _
            ByVal tableName As String, ByVal cmdTimeOut As Integer)
        _sql = New AnalysisSt.DataBaseFunc.Sql(serverName, databaseName, cmdTimeOut)
        _tableName = tableName
        _arrParam = New ArrayParam()
        _ds = New DataSet()
    End Sub

    Private Sub InitClassVarpersistSecurity(ByVal serverName As String, ByVal databaseName As String, _
                                            ByVal tableName As String, ByVal connectionString As String)
        _sql = New AnalysisSt.DataBaseFunc.Sql(connectionString)
        _tableName = tableName
        _arrParam = New ArrayParam()
        _ds = New DataSet()
    End Sub

#End Region

    Public Sub DataSetBinding(ByVal ds As DataSet, ByVal tableName As String)
        If Not ds Is Nothing Then
            _ds = ds
            _tableName = tableName
        End If
        _arrParam.Clear()
    End Sub

    Public Overridable Function ExecuteDataSet(ByVal queryName As String, ByVal queryType As CommandType, _
            ByVal filledDataTableName As String, ByVal filledDataSet As DataSet, _
            ByVal parameters As ArrayParam) As DataSet

        Return _sql.ExecuteDataSet(queryName, queryType, filledDataTableName, filledDataSet, parameters)
    End Function

    Public Overridable Function ExecuteDataSet2Tier(ByVal queryName As String, ByVal queryType As CommandType, _
            ByVal filledDataTableName As String, ByVal filledDataSet As DataSet, _
            ByVal parameters As ArrayParam) As DataSet

        Dim dsCopy As New DataSet
        dsCopy = filledDataSet.Copy
        filledDataSet.Reset()

        Return _sql.ExecuteDataSet(queryName, queryType, filledDataTableName, filledDataSet, parameters)
    End Function

    Public Sub ExecuteNonQuery2Tier(ByVal queryName As String, ByVal queryType As CommandType, _
        ByVal parameters As ArrayParam)

        _sql.ExecuteNonQuery(queryName, queryType, parameters)

    End Sub

#Region "p_Opt10059PriceMinMaxQuery "
    Public Function p_Opt10059PriceMinMaxQuery(ByVal query As String, ByVal stockCode As String, ByVal stockDate As String, _
                                               ByVal bln3tier As Boolean, _
                                      Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        DataSetBinding(ds, tableName)

        With _arrParam
            .Clear()
            .Add("@QUERY", query)
            .Add("@STOCK_CODE", stockCode)
            .Add("@STOCK_DATE", stockDate)
        End With

        If bln3tier = True Then
            Return ExecuteDataSet("p_Opt10059PriceMinMaxQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        Else
            Return ExecuteDataSet2Tier("p_Opt10059PriceMinMaxQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        End If

    End Function
#End Region

#Region "p_Opt10059QtyMinMaxQuery "
    Public Function p_Opt10059QtyMinMaxQuery(ByVal query As String, ByVal stockCode As String, ByVal stockDate As String, _
                                               ByVal bln3tier As Boolean, _
                                      Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        DataSetBinding(ds, tableName)

        With _arrParam
            .Clear()
            .Add("@QUERY", query)
            .Add("@STOCK_CODE", stockCode)
            .Add("@STOCK_DATE", stockDate)
        End With

        If bln3tier = True Then
            Return ExecuteDataSet("p_Opt10059QtyMinMaxQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        Else
            Return ExecuteDataSet2Tier("p_Opt10059QtyMinMaxQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        End If

    End Function
#End Region

#Region "p_Opt10060PriceMinMaxQuery "
    Public Function p_Opt10060PriceMinMaxQuery(ByVal query As String, ByVal stockCode As String, ByVal maemeGb As String, _
                                               ByVal stockDate As String, ByVal bln3tier As Boolean, _
                                      Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        DataSetBinding(ds, tableName)

        With _arrParam
            .Clear()
            .Add("@QUERY", query)
            .Add("@STOCK_CODE", stockCode)
            .Add("@MAEME_GB", maemeGb)
            .Add("@STOCK_DATE", stockDate)
        End With

        If bln3tier = True Then
            Return ExecuteDataSet("p_Opt10060PriceMinMaxQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        Else
            Return ExecuteDataSet2Tier("p_Opt10060PriceMinMaxQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        End If

    End Function
#End Region

#Region "p_Opt10060QtyMinMaxQuery "
    Public Function p_Opt10060QtyMinMaxQuery(ByVal query As String, ByVal stockCode As String, ByVal maemeGb As String, _
                                             ByVal stockDate As String, ByVal bln3tier As Boolean, _
                                      Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        DataSetBinding(ds, tableName)

        With _arrParam
            .Clear()
            .Add("@QUERY", query)
            .Add("@STOCK_CODE", stockCode)
            .Add("@MAEME_GB", maemeGb)
            .Add("@STOCK_DATE", stockDate)
        End With

        If bln3tier = True Then
            Return ExecuteDataSet("p_Opt10060QtyMinMaxQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        Else
            Return ExecuteDataSet2Tier("p_Opt10060QtyMinMaxQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        End If

    End Function
#End Region

#Region "p_OPT10060PriceQuery "
    Public Function p_OPT10060PriceQuery(ByVal query As String, ByVal stockCode As String, ByVal fromdate As String, ByVal toDate As String, ByVal maemeGb As String, _
                                               ByVal bln3tier As Boolean, _
                                      Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        DataSetBinding(ds, tableName)

        With _arrParam
            .Clear()
            .Add("@QUERY", query)
            .Add("@STOCK_CODE", stockCode)
            .Add("@FROM_DATE", fromdate)
            .Add("@TO_DATE", toDate)
            .Add("@MAEME_GB", maemeGb)
        End With

        If bln3tier = True Then
            Return ExecuteDataSet("p_OPT10060PriceQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        Else
            Return ExecuteDataSet2Tier("p_OPT10060PriceQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        End If

    End Function
#End Region

#Region "p_Opt10081MinMaxQuery "
    Public Function p_Opt10081MinMaxQuery(ByVal query As String, ByVal stockCode As String, ByVal stockDate As String, _
                                               ByVal bln3tier As Boolean, _
                                      Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        DataSetBinding(ds, tableName)

        With _arrParam
            .Clear()
            .Add("@QUERY", query)
            .Add("@STOCK_CODE", stockCode)
            .Add("@STOCK_DATE", stockDate)
        End With

        If bln3tier = True Then
            Return ExecuteDataSet("p_Opt10081MinMaxQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        Else
            Return ExecuteDataSet2Tier("p_Opt10081MinMaxQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        End If

    End Function
#End Region

#Region "p_Opt10081MaxMinPriceDateQuery "
    Public Function p_Opt10081MaxMinPriceDateQuery(ByVal query As String, ByVal stockCode As String, ByVal fromDate As String, ByVal toDate As String, _
                                               ByVal bln3tier As Boolean, _
                                      Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        DataSetBinding(ds, tableName)

        With _arrParam
            .Clear()
            .Add("@QUERY", query)
            .Add("@STOCK_CODE", stockCode)
            .Add("@FROM_DATE", fromDate)
            .Add("@TO_DATE", toDate)
        End With

        If bln3tier = True Then
            Return ExecuteDataSet("p_Opt10081MaxMinPriceDateQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        Else
            Return ExecuteDataSet2Tier("p_Opt10081MaxMinPriceDateQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        End If

    End Function
#End Region

#Region "p_Opt10081Query "
    Public Function p_Opt10081Query(ByVal query As String, ByVal stockCode As String, ByVal fromDate As String, ByVal toDate As String, _
                                               ByVal bln3tier As Boolean, _
                                      Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        DataSetBinding(ds, tableName)

        With _arrParam
            .Clear()
            .Add("@QUERY", query)
            .Add("@STOCK_CODE", stockCode)
            .Add("@FROM_DATE", fromDate)
            .Add("@TO_DATE", toDate)
        End With

        If bln3tier = True Then
            Return ExecuteDataSet("p_Opt10081Query", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        Else
            Return ExecuteDataSet2Tier("p_Opt10081Query", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        End If

    End Function
#End Region

#Region "p_VolumeAnaly0Query "
    Public Function p_VolumeAnaly0Query(ByVal query As String, ByVal stockCode As String, ByVal fromDate As String, ByVal toDate As String, _
                                               ByVal bln3tier As Boolean, _
                                      Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        DataSetBinding(ds, tableName)

        With _arrParam
            .Clear()
            .Add("@QUERY", query)
            .Add("@STOCK_CODE", stockCode)
            .Add("@FROM_DATE", fromDate)
            .Add("@TO_DATE", toDate)
        End With

        If bln3tier = True Then
            Return ExecuteDataSet("p_VolumeAnaly0Query", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        Else
            Return ExecuteDataSet2Tier("p_VolumeAnaly0Query", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        End If

    End Function
#End Region

#Region "p_ktCodeQuery "
    Public Function p_ktCodeQuery(ByVal query As String, ByVal ktCode As String, ByVal ktName As String, _
                                               ByVal bln3tier As Boolean, _
                                      Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        DataSetBinding(ds, tableName)

        With _arrParam
            .Clear()
            .Add("@QUERY", query)
            .Add("@KT_CODE", ktCode)
            .Add("@KT_NAME", ktName)
        End With

        If bln3tier = True Then
            Return ExecuteDataSet("p_ktCodeQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        Else
            Return ExecuteDataSet2Tier("p_ktCodeQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        End If

    End Function
#End Region

#Region "p_TodayTradeInfoQueryQuery "
    Public Function p_TodayTradeInfoQueryQuery(ByVal query As String, ByVal sGroupCode As String, ByVal stockCode As String, _
                                               ByVal stockDate As String, _
                                               ByVal bln3tier As Boolean, _
                                      Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        DataSetBinding(ds, tableName)

        With _arrParam
            .Clear()
            .Add("@QUERY", query)
            .Add("@SGROUP_CODE", sGroupCode)
            .Add("@STOCK_CODE", stockCode)
            .Add("@STOCK_DATE", stockDate)
        End With

        If bln3tier = True Then
            Return ExecuteDataSet("p_TodayTradeInfoQueryQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        Else
            Return ExecuteDataSet2Tier("p_TodayTradeInfoQueryQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        End If

    End Function
#End Region

#Region "p_TodayTraderInfoExists "
    Public Function p_TodayTraderInfoExists(ByVal query As String, ByVal sGroupCode As String, ByVal stockCode As String, _
                                               ByVal stockDate As String, _
                                               ByVal bln3tier As Boolean, _
                                      Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        DataSetBinding(ds, tableName)

        With _arrParam
            .Clear()
            .Add("@QUERY", query)
            .Add("@SGROUP_CODE", sGroupCode)
            .Add("@STOCK_CODE", stockCode)
            .Add("@STOCK_DATE", stockDate)
        End With

        If bln3tier = True Then
            Return ExecuteDataSet("p_TodayTraderInfoExists", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        Else
            Return ExecuteDataSet2Tier("p_TodayTraderInfoExists", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        End If

    End Function
#End Region

#Region "p_Opt10059QtyNujukQuery "
    Public Function p_Opt10059QtyNujukQuery(ByVal query As String, ByVal stockCode As String, ByVal stockDate As String, _
                                               ByVal bln3tier As Boolean, _
                                      Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        DataSetBinding(ds, tableName)

        With _arrParam
            .Clear()
            .Add("@QUERY", query)
            .Add("@STOCK_CODE", stockCode)
            .Add("@STOCK_DATE", stockDate)
        End With

        If bln3tier = True Then
            Return ExecuteDataSet("p_Opt10059QtyNujukQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        Else
            Return ExecuteDataSet2Tier("p_Opt10059QtyNujukQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        End If

    End Function
#End Region

#Region "p_Opt10059PriceNujukQuery "
    Public Function p_Opt10059PriceNujukQuery(ByVal query As String, ByVal stockCode As String, ByVal stockDate As String, _
                                               ByVal bln3tier As Boolean, _
                                      Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        DataSetBinding(ds, tableName)

        With _arrParam
            .Clear()
            .Add("@QUERY", query)
            .Add("@STOCK_CODE", stockCode)
            .Add("@STOCK_DATE", stockDate)
        End With

        If bln3tier = True Then
            Return ExecuteDataSet("p_Opt10059PriceNujukQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        Else
            Return ExecuteDataSet2Tier("p_Opt10059PriceNujukQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        End If

    End Function
#End Region

#Region "p_Opt10059ScarePriceNujukQuery "
    Public Function p_Opt10059ScarePriceNujukQuery(ByVal query As String, ByVal stockCode As String, ByVal fromdate As String, ByVal toDate As String, _
                                               ByVal bln3tier As Boolean, _
                                      Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        DataSetBinding(ds, tableName)

        With _arrParam
            .Clear()
            .Add("@QUERY", query)
            .Add("@STOCK_CODE", stockCode)
            .Add("@FROM_DATE", fromdate)
            .Add("@TO_DATE", toDate)
        End With

        If bln3tier = True Then
            Return ExecuteDataSet("p_Opt10059ScarePriceNujukQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        Else
            Return ExecuteDataSet2Tier("p_Opt10059ScarePriceNujukQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        End If

    End Function
#End Region

#Region "p_Opt1005Scare9QtyNujukQuery "
    Public Function p_Opt1005Scare9QtyNujukQuery(ByVal query As String, ByVal stockCode As String, ByVal fromdate As String, ByVal toDate As String, _
                                               ByVal bln3tier As Boolean, _
                                      Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        DataSetBinding(ds, tableName)

        With _arrParam
            .Clear()
            .Add("@QUERY", query)
            .Add("@STOCK_CODE", stockCode)
            .Add("@FROM_DATE", fromdate)
            .Add("@TO_DATE", toDate)
        End With

        If bln3tier = True Then
            Return ExecuteDataSet("p_Opt1005Scare9QtyNujukQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        Else
            Return ExecuteDataSet2Tier("p_Opt1005Scare9QtyNujukQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        End If

    End Function
#End Region

#Region "p_NuOPT10059PriceQuery "
    Public Function p_NuOPT10059PriceQuery(ByVal query As String, ByVal stockCode As String, ByVal fromDate As String, ByVal toDate As String, _
                                               ByVal bln3tier As Boolean, _
                                      Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        DataSetBinding(ds, tableName)

        With _arrParam
            .Clear()
            .Add("@QUERY", query)
            .Add("@STOCK_CODE", stockCode)
            .Add("@FROM_DATE", fromDate)
            .Add("@TO_DATE", toDate)
        End With

        If bln3tier = True Then
            Return ExecuteDataSet("p_NuOPT10059PriceQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        Else
            Return ExecuteDataSet2Tier("p_NuOPT10059PriceQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        End If

    End Function
#End Region

#Region "p_NuOPT10059QtyQuery "
    Public Function p_NuOPT10059QtyQuery(ByVal query As String, ByVal stockCode As String, ByVal fromDate As String, ByVal toDate As String, _
                                               ByVal bln3tier As Boolean, _
                                      Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        DataSetBinding(ds, tableName)

        With _arrParam
            .Clear()
            .Add("@QUERY", query)
            .Add("@STOCK_CODE", stockCode)
            .Add("@FROM_DATE", fromDate)
            .Add("@TO_DATE", toDate)
        End With

        If bln3tier = True Then
            Return ExecuteDataSet("p_NuOPT10059QtyQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        Else
            Return ExecuteDataSet2Tier("p_NuOPT10059QtyQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        End If

    End Function
#End Region

#Region "p_Opt10060_QtyQuery "
    Public Function p_Opt10060_QtyQuery(ByVal query As String, ByVal stockCode As String, ByVal fromDate As String, ByVal toDate As String, _
                                               ByVal bln3tier As Boolean, _
                                      Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        DataSetBinding(ds, tableName)

        With _arrParam
            .Clear()
            .Add("@QUERY", query)
            .Add("@STOCK_CODE", stockCode)
            .Add("@FROM_DATE", fromDate)
            .Add("@TO_DATE", toDate)
        End With

        If bln3tier = True Then
            Return ExecuteDataSet("p_Opt10060_QtyQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        Else
            Return ExecuteDataSet2Tier("p_Opt10060_QtyQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        End If

    End Function
#End Region

#Region "p_Smm01Query "
    Public Function p_Smm01Query(ByVal query As String, ByVal stockCode As String, _
                                 ByVal bln3tier As Boolean, _
                        Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        DataSetBinding(ds, tableName)

        With _arrParam
            .Clear()
            .Add("@QUERY", query)
            .Add("@STOCK_CODE", stockCode)
        End With

        If bln3tier = True Then
            Return ExecuteDataSet("p_Smm01Query", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        Else
            Return ExecuteDataSet2Tier("p_Smm01Query", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        End If

    End Function
#End Region

#Region "p_Smm01UnPivotQuery "
    Public Function p_Smm01UnPivotQuery(ByVal query As String, ByVal stockCode As String, ByVal mimaGb As String, _
                                 ByVal bln3tier As Boolean, _
                        Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        DataSetBinding(ds, tableName)

        With _arrParam
            .Clear()
            .Add("@QUERY", query)
            .Add("@STOCK_CODE", stockCode)
            .Add("@MIMA_GB", mimaGb)
        End With

        If bln3tier = True Then
            Return ExecuteDataSet("p_Smm01UnPivotQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        Else
            Return ExecuteDataSet2Tier("p_Smm01UnPivotQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        End If

    End Function
#End Region

#Region "p_Smm02Query "
    Public Function p_Smm02Query(ByVal query As String, ByVal stockCode As String, _
                                 ByVal bln3tier As Boolean, _
                        Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        DataSetBinding(ds, tableName)

        With _arrParam
            .Clear()
            .Add("@QUERY", query)
            .Add("@STOCK_CODE", stockCode)
        End With

        If bln3tier = True Then
            Return ExecuteDataSet("p_Smm02Query", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        Else
            Return ExecuteDataSet2Tier("p_Smm02Query", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        End If

    End Function
#End Region

#Region "CommandTextQuery"
    '--------------------------------------------------------------------------
    'CommandText형태의 명령수행
    '입력 : cmdText(SQL문장), cmdType(CommandType)
    '--------------------------------------------------------------------------
    ''' <summary>
    ''' TextQuery
    ''' </summary>
    ''' <param name="cmdText">쿼리내용</param>
    ''' <param name="cmdType">쿼리타입</param>
    ''' <param name="bln3tier">3tier 구분 [TRUE - 3Tier] </param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="tableName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Function CommandTextQuery(ByVal cmdText As String, _
            ByVal cmdType As CommandType, ByVal bln3tier As Boolean, _
            Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        DataSetBinding(ds, tableName)

        If bln3tier = True Then
            Return ExecuteDataSet(cmdText, cmdType, _tableName, _ds, _arrParam)
        Else
            Return ExecuteDataSet2Tier(cmdText, cmdType, _tableName, _ds, _arrParam)
        End If

    End Function
#End Region

End Class
