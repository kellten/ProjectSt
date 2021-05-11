Public Class RichQuery
    Private _sql As SDataAccess.Sql
    Private _arrParam As ArrayParam
    Private _tableName As String
    Private _ds As DataSet

#Region "Constuctor"
    Sub New()
        InitClassVar(ClsServerInfo.VADISSEVER, "RICHDB", "DEFAULT")
    End Sub

    Sub New(ByVal tableName As String)
        InitClassVar(ClsServerInfo.VADISSEVER, "RICHDB", tableName)
    End Sub

    Sub New(ByVal serverName As String, ByVal databaseName As String)
        InitClassVar(serverName, databaseName, "DEFAULT")
    End Sub

    Sub New(ByVal serverName As String, ByVal databaseName As String, ByVal tableName As String)
        InitClassVar(serverName, databaseName, tableName)
    End Sub

    Sub New(ByVal tableName As String, ByVal cmdTimeOut As Integer)
        InitClassVar(ClsServerInfo.VADISSEVER, "RICHDB", tableName, cmdTimeOut)
    End Sub

    Sub New(ByVal persistSecurity As Boolean)
        If persistSecurity = True Then
            'InitClassVarpersistSecurity("EDPB2F011\VADIS", "RICHDB", "DEFAULT", "Integrated Security=SSPI;initial catalog= RICHDB;persist security info=false;" & "EDPB2F011\vadis ;Application Name=AnaylsisSt")
            InitClassVarpersistSecurity(serverName:=ClsServerInfo.VADISSEVER, databaseName:="KIWOOMDB", tableName:="DEFAULT", connectionString:="Server=211.210.61.123, 8081;Database=KIWOOMDB;User Id=ywUser01;Password=hi@84966305;")
        End If

    End Sub

    Private Sub InitClassVar(ByVal serverName As String, ByVal databaseName As String,
            ByVal tableName As String)
        _sql = New SDataAccess.Sql(serverName, databaseName)
        _tableName = tableName
        _arrParam = New ArrayParam()
        _ds = New DataSet()
    End Sub

    Private Sub InitClassVar(ByVal serverName As String, ByVal databaseName As String,
            ByVal tableName As String, ByVal cmdTimeOut As Integer)
        _sql = New SDataAccess.Sql(serverName, databaseName, cmdTimeOut)
        _tableName = tableName
        _arrParam = New ArrayParam()
        _ds = New DataSet()
    End Sub

    Private Sub InitClassVarpersistSecurity(ByVal serverName As String, ByVal databaseName As String,
                                            ByVal tableName As String, ByVal connectionString As String)
        _sql = New SDataAccess.Sql(connectionString)
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

        Return _sql.ExecuteDataSet(queryName, queryType, filledDataTableName, dsCopy, parameters)
    End Function

    Public Sub ExecuteNonQuery2Tier(ByVal queryName As String, ByVal queryType As CommandType, _
        ByVal parameters As ArrayParam)

        _sql.ExecuteNonQuery(queryName, queryType, parameters)

    End Sub

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

#Region "p_ScodeQuery "
    Public Function p_ScodeQuery(ByVal query As String, ByVal stockCode As String, ByVal ybYongCode As String, _
                                 ByVal bln3tier As Boolean, _
                        Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        DataSetBinding(ds, tableName)

        With _arrParam
            .Clear()
            .Add("@QUERY", query)
            .Add("@STOCK_CODE", stockCode)
            .Add("@YBJONG_CODE", ybYongCode)
        End With

        If bln3tier = True Then
            Return ExecuteDataSet("p_ScodeQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        Else
            Return ExecuteDataSet2Tier("p_ScodeQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        End If

    End Function
#End Region

#Region "p_Sca01Query "
    Public Function p_Sca01Query(ByVal query As String, ByVal stockCode As String, ByVal bigFlow As Integer, _
                                 ByVal midFlow As Integer, ByVal startDate As String, ByVal endDate As String, _
                                 ByVal bln3tier As Boolean, _
                        Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        DataSetBinding(ds, tableName)

        With _arrParam
            .Clear()
            .Add("@QUERY", query)
            .Add("@STOCK_CODE", stockCode)
            .Add("@BIG_FLOW", bigFlow)
            .Add("@MID_FLOW", midFlow)
            .Add("@START_DATE", startDate)
            .Add("@END_DATE", endDate)
        End With

        If bln3tier = True Then
            Return ExecuteDataSet("p_Sca01Query", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        Else
            Return ExecuteDataSet2Tier("p_Sca01Query", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        End If

    End Function
#End Region

#Region "p_FCodeQuery "
    Public Function p_FCodeQuery(ByVal query As String, ByVal sGroupCode As String, ByVal sGroupName As String, _
                                 ByVal stockCode As String, _
                                 ByVal bln3tier As Boolean, _
                        Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        DataSetBinding(ds, tableName)

        With _arrParam
            .Clear()
            .Add("@QUERY", query)
            .Add("@SGROUP_CODE", sGroupCode)
            .Add("@SGROUP_NAME", sGroupName)
            .Add("@STOCK_CODE", stockCode)
        End With

        If bln3tier = True Then
            Return ExecuteDataSet("p_FCodeQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        Else
            Return ExecuteDataSet2Tier("p_FCodeQuery", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        End If

    End Function
#End Region

#Region "p_Fsa01Query "
    Public Function p_Fsa01Query(ByVal query As String, ByVal sGroupCode As String, ByVal stockCode As String, _
                                 ByVal seqNo As String, _
                                 ByVal bln3tier As Boolean, _
                        Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        DataSetBinding(ds, tableName)

        With _arrParam
            .Clear()
            .Add("@QUERY", query)
            .Add("@SGROUP_CODE", sGroupCode)
            .Add("@STOCK_CODE", stockCode)
            .Add("@SEQ_NO", seqNo)
        End With

        If bln3tier = True Then
            Return ExecuteDataSet("p_Fsa01Query", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        Else
            Return ExecuteDataSet2Tier("p_Fsa01Query", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        End If

    End Function
#End Region

#Region "p_Set01Query "
    Public Function p_Set01Query(ByVal query As String, ByVal stockCode As String, ByVal chartGb As String, _
                                 ByVal bln3tier As Boolean, _
                        Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        DataSetBinding(ds, tableName)

        With _arrParam
            .Clear()
            .Add("@QUERY", query)
            .Add("@STOCK_CODE", stockCode)
            .Add("@CHART_GB", chartGb)
        End With

        If bln3tier = True Then
            Return ExecuteDataSet("p_Set01Query", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        Else
            Return ExecuteDataSet2Tier("p_Set01Query", CommandType.StoredProcedure, _tableName, _ds, _arrParam)
        End If

    End Function
#End Region

End Class
