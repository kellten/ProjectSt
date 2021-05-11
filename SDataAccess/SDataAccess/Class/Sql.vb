Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationManager

Public Class Sql

    Private _connectionString As String = ""
    Private _commandTimeOut As Integer

    Protected _arrDirectValue As New ArrayParam

#Region "Constructor"
    '특정한 연결문자가 없이 연결할 경우 Web.Config에 기록된 ConnectionString을 이용한다.
    Public Sub New()
        Dim configureConnString As String = AppSettings.Get("ConnectionString")

        If configureConnString.Length = 0 Then
            Throw New Exception("연결문자열이 지정되어 있지 않습니다!!!")
        Else
            MyClass._connectionString = configureConnString
        End If
    End Sub

    'DataBase연결 문자열을 이용하여 DataBase연결
    Public Sub New(ByVal connectionString As String)
        MyClass._connectionString = connectionString
    End Sub

    'Server이름과 해당 Server의 Database이름을 이용하여 연결
    Public Sub New(ByVal serverName As String, ByVal databaseName As String)
        If serverName.Trim = "" OrElse databaseName.Trim = "" Then
            Throw New Exception("서버명 또는 데이터베이스명이 지정되지 않았습니다.")
            MyClass._connectionString = ""
        End If

        'WebApplicationServer의 machine.Config 안에 Key = 서버명, value = uid, pwd를 지정한다. 
        ' Ex)   <appSettings> <add key="EMRSERVER" value="uid=XXX; pwd=XXXXXX"/> </appSettings>
        'MyClass._connectionString = "data source=" + serverName + ";initial catalog=" + databaseName + ";persist security info=False;"
        'MyClass._connectionString += AppSettings.Get(serverName)

        If serverName = "EDPB2F011\VADIS" Then
            MyClass._connectionString = "initial catalog=" + databaseName + ";" + " persist security info=True; Integrated Security=SSPI;"
            MyClass._connectionString += AppSettings.Get(serverName)
            MyClass._connectionString += "uid=EDPB2F011\vadis; pwd=hi@84966305"
        ElseIf serverName = "211.210.61.123, 8081" Then
            MyClass._connectionString = "data source=" + serverName + "; initial catalog=" + databaseName + ";" + " persist security info=True; Integrated Security=false;"
            'MyClass._connectionString += AppSettings.Get(serverName)
            MyClass._connectionString += "uid=ywUser01; pwd=hi@84966305"
        End If
    End Sub

    'Server이름과 해당 Server의 Database이름을 이용하여 연결
    Public Sub New(ByVal serverName As String, ByVal databaseName As String, ByVal user As String, ByVal pwd As String)
        If serverName.Trim = "" OrElse databaseName.Trim = "" Then
            Throw New Exception("서버명 또는 데이터베이스명이 지정되지 않았습니다.")
            MyClass._connectionString = ""
        End If

        'WebApplicationServer의 machine.Config 안에 Key = 서버명, value = uid, pwd를 지정한다. 
        ' Ex)   <appSettings> <add key="EMRSERVER" value="uid=XXX; pwd=XXXXXX"/> </appSettings>
        'MyClass._connectionString = "data source=" + serverName + ";initial catalog=" + databaseName + ";persist security info=False;"
        'MyClass._connectionString += "uid=" & user & "; pwd=" & pwd & ""

        MyClass._connectionString = "data source=" + serverName + ";initial catalog=" + databaseName + ";persist security info=False;"
        MyClass._connectionString += "uid=" & user & "; pwd=" & pwd & ""
    End Sub

    'Server이름과 해당 Server의 Database이름을 이용하여 연결
    Public Sub New(ByVal serverName As String, ByVal databaseName As String, ByVal cmdTimeOut As Integer)
        MyClass.New(serverName, databaseName)

        If cmdTimeOut = -1 Then
            MyClass._connectionString = "data source=" + serverName + ";initial catalog=" + databaseName + ";persist security info=False;"
            'MyClass._connectionString += "uid=EDPB2F011\vadis; pwd=hi@84966305"
            MyClass._connectionString += "uid=YwUser01; pwd=hi@84966305"
            'MyClass._connectionString += "uid=vadis; pwd=hi@84966305"
            _commandTimeOut = 3600
        Else
            If cmdTimeOut > 0 Then
                _commandTimeOut = cmdTimeOut
            Else
                _commandTimeOut = 30
            End If
        End If
    End Sub
#End Region

    'Stored Procedure실행 후 Return값이 있거나
    '파리미터중에 OUTPUT Option이 있을 경우 그 값들을 배열에 저장한다.
    Public ReadOnly Property DirectReturnValue() As ArrayParam
        Get
            Return _arrDirectValue
        End Get
    End Property


#Region "ExecuteDataSet"
    '------------------------------------------------------------
    ' queryString, queryType, SqlParameter()
    '------------------------------------------------------------
    Public Overridable Function ExecuteDataSet(ByVal queryString As String, ByVal queryType As CommandType, _
            ByVal ParamArray parameters As SqlParameter()) As DataSet

        Return MyClass.ExecuteDataSet(queryString, queryType, "Default", parameters)
    End Function

    '------------------------------------------------------------
    ' queryString, queryType, filledDataTableName, SqlParameter()
    '------------------------------------------------------------
    Public Overridable Function ExecuteDataSet(ByVal queryString As String, ByVal queryType As CommandType, _
            ByVal filledDataTableName As String, ByVal ParamArray parameters As SqlParameter()) As DataSet
        Dim dtSet As New DataSet()

        Return MyClass.ExecuteDataSet(queryString, queryType, filledDataTableName, dtSet, parameters)

    End Function

    '---------------------------------------------------------------------
    ' queryString, queryType, filledDataTableName, paramType(@, value)형식
    '---------------------------------------------------------------------
    Public Overridable Function ExecuteDataSet(ByVal queryString As String, ByVal queryType As CommandType, _
            ByVal filledDataTableName As String, ByVal parameters As ArrayParam) As DataSet
        Dim dtSet As New DataSet()

        Return MyClass.ExecuteDataSet(queryString, queryType, filledDataTableName, dtSet, parameters)

    End Function

    '-------------------------------------------------------------------------------------
    ' queryString, queryType, filledDataTableName, filledDataSet, paramType(@, value)형식
    ' 넘어온 DataSet에 DataSet을 추가할 경우
    '-------------------------------------------------------------------------------------
    Public Overridable Function ExecuteDataSet(ByVal queryString As String, ByVal queryType As CommandType, _
            ByVal filledDataTableName As String, ByVal filledDataSet As DataSet, _
            ByVal parameters As ArrayParam) As DataSet

        Dim sqlParams(parameters.Count - 1) As SqlParameter
        Dim ix As Integer
        For ix = 0 To parameters.Count - 1
            If parameters.Item(ix).Type <> SqlDbType.VarChar Then
                ''sqlParams(ix) = _
                ''        New SqlParameter(parameters.Item(ix).Name, parameters.Item(ix).Type).Value = _
                ''                parameters.Item(ix).Value
                sqlParams(ix) = _
                        New SqlParameter(parameters.Item(ix).Name, parameters.Item(ix).Type)
                sqlParams(ix).Value = parameters.Item(ix).Value
            Else
                sqlParams(ix) = _
                        New SqlParameter(parameters.Item(ix).Name, SqlDbType.VarChar)
                sqlParams(ix).Value = parameters.Item(ix).Value

            End If
        Next

        Return MyClass.ExecuteDataSet(queryString, queryType, filledDataTableName, filledDataSet, sqlParams)
    End Function

    '----------------------------------------------------------------------------
    ' queryString, queryType, filledDataTableName, filledDataSet, SqlParameter()
    '----------------------------------------------------------------------------
    Public Overridable Function ExecuteDataSet(ByVal queryString As String, ByVal queryType As CommandType, _
                ByVal filledDataTableName As String, ByVal filledDataSet As DataSet, _
                ByVal ParamArray parameters As SqlParameter()) As DataSet
        Dim sqlAdapter As New SqlDataAdapter(queryString, _connectionString)

        Try
            sqlAdapter.SelectCommand.CommandType = queryType
            If _commandTimeOut = -1 Then
                sqlAdapter.SelectCommand.CommandTimeout = 1000000
            ElseIf _commandTimeOut > 0 Then
                ' sqlAdapter.SelectCommand.CommandTimeout = 30
                sqlAdapter.SelectCommand.CommandTimeout = _commandTimeOut
            End If

            If Not (parameters Is Nothing) Then
                FillParameters(sqlAdapter.SelectCommand, parameters)
            End If

            sqlAdapter.Fill(filledDataSet, filledDataTableName)

            Return filledDataSet
        Catch ex As Exception
            Throw ex
        Finally
            sqlAdapter.SelectCommand.Connection.Close()
            sqlAdapter.Dispose()
        End Try

    End Function

#End Region

#Region "ExecuteReader"
    Public Function ExecuteReader(ByVal queryString As String, ByVal queryType As CommandType, _
                    ByVal ParamArray parameters As SqlParameter()) As SqlDataReader
        Return ExecuteReader(queryString, queryType, CommandBehavior.CloseConnection, parameters)
    End Function

    Public Function ExecuteReader(ByVal queryString As String, ByVal queryType As CommandType, _
                    ByVal commandBehavior As CommandBehavior, _
                    ByVal ParamArray parameters As SqlParameter()) As SqlDataReader
        Dim myComm As New SqlCommand(queryString)
        myComm.Connection = GetConnection()
        myComm.CommandType = queryType

        If Not (parameters Is Nothing) Then
            FillParameters(myComm, parameters)
        End If

        myComm.Connection.Open()
        Return myComm.ExecuteReader(commandBehavior)
    End Function

    Public Function ExecuteReader(ByVal queryString As String, ByVal queryType As CommandType, _
            ByVal commandBehavior As CommandBehavior, ByVal parameters As ArrayParam) As SqlDataReader

        Dim sqlParams(parameters.Count - 1) As SqlParameter
        Dim ix As Integer
        For ix = 0 To parameters.Count - 1
            sqlParams(ix) = _
                    New SqlClient.SqlParameter(parameters.Item(ix).Name, parameters.Item(ix).Value)
        Next

        Return MyClass.ExecuteReader(queryString, queryType, commandBehavior, sqlParams)
    End Function


#End Region

#Region "ExecuteNonQuery"
    Public Function ExecuteNonQuery(ByVal cmdString As String, ByVal cmdType As CommandType, _
                    ByVal ParamArray parameters As SqlParameter()) As Integer

        Dim myComm As New SqlCommand(cmdString)
        Dim retAffectedRow As Integer
        Dim ix As Integer

        Try
            myComm.Connection = GetConnection()
            If _commandTimeOut = -1 Then
                myComm.CommandTimeout = 3600
            ElseIf _commandTimeOut > 0 Then
                myComm.CommandTimeout = _commandTimeOut
            End If
            myComm.CommandType = cmdType

            If Not (parameters Is Nothing) AndAlso parameters.Length > 0 Then
                FillParameters(myComm, parameters)
            End If

            myComm.Connection.Open()

            retAffectedRow = myComm.ExecuteNonQuery
        Catch ex As Exception
            Throw New System.Exception(ex.ToString)
        Finally
            _arrDirectValue.Clear()

            If Not (parameters Is Nothing) AndAlso parameters.Length > 0 Then
                For ix = 0 To parameters.Length - 1
                    Select Case parameters(ix).Direction
                        Case ParameterDirection.ReturnValue, ParameterDirection.Output, ParameterDirection.InputOutput
                            _arrDirectValue.Add(parameters(ix).ParameterName, parameters(ix).Value)
                    End Select
                Next
                If parameters(0).Direction = ParameterDirection.ReturnValue Then
                    retAffectedRow = parameters(0).Value()
                End If
            End If

            myComm.Connection.Close()
            myComm.Dispose()
        End Try

        Return retAffectedRow
    End Function

    Public Overridable Function ExecuteNonQuery(ByVal cmdString As String, ByVal cmdType As CommandType, _
                ByVal parameters As ArrayParam) As Integer
        Dim sqlParams(parameters.Count - 1) As SqlParameter
        Dim ix As Integer
        For ix = 0 To parameters.Count - 1
            If parameters.Item(ix).Type <> SqlDbType.VarChar Then
                sqlParams(ix) = _
                        New SqlParameter(parameters.Item(ix).Name, parameters.Item(ix).Type)
                sqlParams(ix).Value = parameters.Item(ix).Value
            Else
                sqlParams(ix) = _
                        New SqlParameter(parameters.Item(ix).Name, parameters.Item(ix).Value)
            End If
            If parameters.Item(ix).Direction = ParameterDirection.Output Then
                If sqlParams(ix).Size <= 0 Then
                    sqlParams(ix).Size = parameters.Item(ix).Size
                End If
            End If
            sqlParams(ix).Direction = parameters.Item(ix).Direction
        Next

        Return MyClass.ExecuteNonQuery(cmdString, cmdType, sqlParams)
    End Function

#End Region

#Region "ExecuteScalar"
    Public Function ExecuteScalar(ByVal queryString As String, ByVal queryType As CommandType, _
                    ByVal ParamArray parameters As SqlParameter()) As Object

        Dim myComm As New SqlCommand(queryString)
        myComm.Connection = GetConnection()
        myComm.CommandType = queryType

        If Not (parameters Is Nothing) Then
            FillParameters(myComm, parameters)
        End If

        myComm.Connection.Open()
        Dim retVal As Object

        retVal = myComm.ExecuteScalar
        myComm.Connection.Close()

        Return retVal

    End Function
#End Region

    Protected Function GetConnection() As SqlConnection
        Return (New SqlConnection(MyClass._connectionString))
    End Function

    Protected Sub FillParameters(ByRef FilledCommand As SqlCommand, _
        ByVal ParamArray parameters As SqlParameter())
        Dim param As SqlParameter

        For Each param In parameters
            FilledCommand.Parameters.Add(param)
        Next
    End Sub
End Class
