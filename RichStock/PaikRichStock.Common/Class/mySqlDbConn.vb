Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class mySqlDbConn
    Public _sqlCon As SqlConnection
    Protected _arrDirectValue As New ArrayParam
    Private _connectionString As String = ""
    Private _commandTimeOut As Integer
    Public Shared QueryStr As String = ""

    Sub New()
        Open()
    End Sub

    Public Function Open() As Boolean
        Dim connStr As String = ""
        Try
            'If My.Computer.Name.IndexOf("EDPB2F") > -1 Then
            '    '_sqlCon.ConnectionString = "DRIVER=MySqlProv;SERVER=udors.cafe24.com;PORT=3306;DATABASE=udors;USER=udors;PASSWORD=rnjs3236@;allow user variables=true;"
            '    connStr = "SERVER=udors.cafe24.com;PORT=3306;DATABASE=udors;USER=udors;PASSWORD=rnjs3236@;allow user variables=true;"
            'Else
            '    '_sqlCon.ConnectionString = "DRIVER=MySqlProv;SERVER=sundown99.ipdisk.co.kr;PORT=1433;DATABASE=kapi;USER=kojedevil;PASSWORD=rnjs99@;allow user variables=true;"
            '    connStr = "SERVER=sundown99.ipdisk.co.kr;PORT=1433;DATABASE=kapi;USER=kojedevil;PASSWORD=rnjs99@;allow user variables=true;"
            'End If
            connStr = "SERVER=eunha821.cafe24.com;PORT=1433;DATABASE=udors;USER=richstock;PASSWORD=asdfqwer!2;allow user variables=true;"
            _sqlCon = New SqlConnection(connStr)
            _sqlCon.Open()
            Return True

        Catch ex As Exception
            Try
                _sqlCon.Open()
            Catch
                Return False
            End Try
        End Try

        Return False
    End Function

    Public Function Close() As Boolean

        Try
            _sqlCon.Close()

            Return True
        Catch ex As Exception
        End Try

        Return False

    End Function


    Public Function Dispose() As Boolean

        Try
            _sqlCon.Close()
            Me.Dispose()
        Catch ex As Exception
        End Try

        Return False

    End Function


#Region " ExecuteNonQuery "
    Public Function ExecuteNonQuery(ByVal sp As String, ByVal arrParam As ArrayParam, Optional ByVal cmdType As System.Data.CommandType = CommandType.StoredProcedure) As Integer
        Dim methodName As String = System.Reflection.MethodBase.GetCurrentMethod().Name
        Dim cmd As SqlCommand
        Dim odbcAdapt As New SqlDataAdapter
        Dim retValue As Integer
        Dim spParam As String = ""
        Try
            If _sqlCon Is Nothing Then
                Open()
            End If

            If _sqlCon.State = System.Data.ConnectionState.Closed Then
                _sqlCon.Open()
            End If

            cmd = New SqlCommand()
            cmd.Connection = _sqlCon
            If cmdType = CommandType.StoredProcedure Then
                'For i As Integer = 0 To arrParam.Count - 1
                '    spParam += "?"
                '    If i < arrParam.Count - 1 Then
                '        spParam += ","
                '    End If
                'Next

                cmd.CommandType = CommandType.StoredProcedure
                'cmd.CommandText = "CALL " + sp + "(" + spParam + ")"
                cmd.CommandText = sp
                For i As Integer = 0 To arrParam.Count - 1
                    'cmd.Parameters.Add(arrParam.Item(i).Name, arrParam.Item(i).Type, arrParam.Item(i).Size)
                    'cmd.Parameters(arrParam.Item(i).Name).Direction = arrParam.Item(i).Direction
                    'cmd.Parameters(arrParam.Item(i).Name).Value = arrParam.Item(i).Value
                    cmd.Parameters.AddWithValue(arrParam.Item(i).Name, arrParam.Item(i).Value)
                Next
            Else
                cmd.CommandType = CommandType.Text
                cmd.CommandText = sp
            End If

            retValue = cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            Close()
        End Try
    End Function

    'Public Function ExecuteNonQueryTransaction(ByVal arrParams As ArrayParams) As Integer
    '    Dim methodName As String = System.Reflection.MethodBase.GetCurrentMethod().Name
    '    Dim cmd As MySqlCommand
    '    Dim odbcAdapt As New MySqlDataAdapter
    '    Dim retValue As Integer
    '    Dim spParam As String = ""
    '    Dim query As String = ""
    '    Dim spName As String = ""
    '    Try
    '        If _sqlCon Is Nothing Then
    '            Open()
    '        End If

    '        cmd = New MySqlCommand
    '        cmd.Connection = _sqlCon
    '        cmd.CommandType = CommandType.StoredProcedure
    '        For i As Integer = 0 To arrParams.Count - 1
    '            spParam = ""
    '            spName = ""
    '            For j As Integer = 0 To arrParams(i).Count - 1
    '                spParam += "?"
    '                If j < arrParams(i).Count - 1 Then
    '                    spParam += ","
    '                End If
    '                spName = arrParams(i).CommandString
    '            Next
    '            query += String.Format("CALL {0} ({1});", spName, spParam)
    '        Next

    '        cmd.CommandText = query

    '        For i As Integer = 0 To arrParams.Count - 1
    '            For j As Integer = 0 To arrParams(i).Count - 1
    '                cmd.Parameters.AddWithValue(arrParams(i).Item(j).Name, arrParams(i).Item(j).Value)
    '            Next
    '        Next

    '        retValue = cmd.ExecuteNonQuery()
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '    Finally
    '        Close()
    '    End Try
    'End Function
#End Region

#Region " ExecuteNonMultiInsert "
    Public Function ExecuteNonMultiInsert(ByVal tableName As String, ByVal arrParams As ArrayParams) As Integer
        Dim methodName As String = System.Reflection.MethodBase.GetCurrentMethod().Name
        Dim cmd As SqlCommand
        Dim odbcAdapt As New SqlDataAdapter
        Dim retValue As Integer
        Dim spParam As String = ""
        Dim arrParam As ArrayParam
        Dim colStr As String = ""
        Dim valuesStr As String = ""
        Dim valuesStrMulti As String = ""
        Dim query As String = ""
        Dim strCol() As String
        Try
            If _sqlCon Is Nothing Then
                Open()
            End If

            If _sqlCon.State = ConnectionState.Closed Then
                _sqlCon.Open()
            End If

            For i As Integer = 0 To arrParams.Count - 1
                colStr = ""
                valuesStr = ""
                arrParam = arrParams(i)
                strCol = arrParam.ConvertArrToString().Split("#")

                For j As Integer = 0 To strCol.Length - 1
                    colStr += strCol(j).Substring(0, strCol(j).IndexOf("!"))
                    If j < strCol.Length - 1 Then
                        colStr += ","
                    End If

                    valuesStr += strCol(j).Substring(strCol(j).IndexOf("@"), strCol(j).Length - strCol(j).IndexOf("@"))
                    valuesStr = valuesStr.Replace("@", "'").Replace("!", "'")
                    If j = strCol.Length - 1 Then
                        valuesStr += "'"
                    End If

                    If j < strCol.Length - 1 Then
                        valuesStr += ","
                    End If
                Next
                valuesStr = "(" + valuesStr + ") ,"
                valuesStrMulti += valuesStr
            Next

            If colStr = "" OrElse valuesStr = "" Then Exit Function

            query = String.Format("INSERT INTO {0} ({1}) VALUES {2}", tableName, colStr, valuesStrMulti)
            query = Left(query, query.Length - 1) '마지막에 , 때문에 하나 제거 해준다.
            cmd = New SqlCommand(query, _sqlCon)
            cmd.CommandType = CommandType.Text

            retValue = cmd.ExecuteNonQuery()
        Catch ex As Exception
            'MsgBox(ex.ToString)
            Try
                cmd = New SqlCommand(query, _sqlCon)
                cmd.CommandType = CommandType.Text
                retValue = cmd.ExecuteNonQuery()
            Catch ex1 As Exception
                Try
                    cmd = New SqlCommand(query, _sqlCon)
                    cmd.CommandType = CommandType.Text
                    retValue = cmd.ExecuteNonQuery()
                Catch ex2 As Exception

                End Try
            End Try

        Finally
            Close()
        End Try
    End Function
#End Region

#Region " ExecuteNonMultiDelete "
    Public Function ExecuteNonMultiDelete(ByVal tableName As String, ByVal arrParams As ArrayParams) As Integer
        Dim methodName As String = System.Reflection.MethodBase.GetCurrentMethod().Name
        Dim cmd As SqlCommand
        Dim odbcAdapt As New SqlDataAdapter
        Dim retValue As Integer
        Dim arrParam As ArrayParam
        Dim query As String = ""
        Dim whereStr As String = ""
        Try
            If _sqlCon Is Nothing Then
                Open()
            End If

            If _sqlCon.State = ConnectionState.Closed Then
                _sqlCon.Open()
            End If

            For i As Integer = 0 To arrParams.Count - 1
                arrParam = arrParams(i)
                For j As Integer = 0 To arrParam.Count - 1
                    If j = 0 Then
                        whereStr += " ( "
                    End If
                    whereStr += String.Format("{0} = '{1}'", arrParam(j).Name, arrParam(j).Value)
                    If j < arrParam.Count - 1 Then
                        whereStr += " AND "
                    End If

                    If j = arrParam.Count - 1 Then
                        whereStr += " ) "
                    End If
                Next

                If i < arrParams.Count - 1 Then
                    whereStr += " OR "
                End If
            Next

            If whereStr = "" Then Exit Function

            query = String.Format("DELETE FROM {0} WHERE {1} ", tableName, whereStr)
            cmd = New SqlCommand(query, _sqlCon)
            cmd.CommandType = CommandType.Text

            retValue = cmd.ExecuteNonQuery()
        Catch ex As Exception
            'MsgBox(ex.ToString)
            Try
                cmd = New SqlCommand(query, _sqlCon)
                cmd.CommandType = CommandType.Text
                retValue = cmd.ExecuteNonQuery()
            Catch ex1 As Exception
                Try
                    cmd = New SqlCommand(query, _sqlCon)
                    cmd.CommandType = CommandType.Text
                    retValue = cmd.ExecuteNonQuery()
                Catch ex2 As Exception

                End Try
            End Try
        Finally
            Close()
        End Try
    End Function
#End Region

#Region " GetDataTableSp "
    'Public Shared Function GetDataTableSp(ByVal sql As String) As DataSet
    '    Dim ds As New DataSet
    '    Dim methodName As String = System.Reflection.MethodBase.GetCurrentMethod().Name
    '    Dim cmd As Odbc.OdbcCommand
    '    Dim odbcAdapt As New Odbc.OdbcDataAdapter

    '    sql = "CALL " & sql

    '    cmd = New Odbc.OdbcCommand(sql, _sqlCon)

    '    odbcAdapt.SelectCommand = cmd

    '    odbcAdapt.Fill(ds)

    '    Return ds

    'End Function

    Public Function GetDataTableSp(ByVal sp As String, ByVal arrParam As ArrayParam) As DataSet
        Dim ds As New DataSet
        Dim methodName As String = System.Reflection.MethodBase.GetCurrentMethod().Name
        Dim cmd As SqlCommand
        Dim odbcAdapt As New SqlDataAdapter
        Dim cmdText As String = ""
        Dim spParam As String = ""

        If _sqlCon Is Nothing Then
            Open()
        End If

        If _sqlCon.State = ConnectionState.Closed Then
            _sqlCon.Open()
        End If

        'For i As Integer = 0 To arrParam.Count - 1
        '    spParam += "?"
        '    If i < arrParam.Count - 1 Then
        '        spParam += ","
        '    End If
        'Next

        'If spParam = "" Then
        '    cmdText = "CALL " + sp
        'Else
        '    cmdText = "CALL " + sp + "(" + spParam + ")"
        'End If


        'cmd = New MySqlCommand(cmdText)
        cmd = New SqlCommand(sp)
        cmd.Connection = _sqlCon
        cmd.CommandType = CommandType.StoredProcedure

        For i As Integer = 0 To arrParam.Count - 1
            'cmd.Parameters.Add(arrParam.Item(i).Name, arrParam.Item(i).Type, arrParam.Item(i).Size)
            'cmd.Parameters(arrParam.Item(i).Name).Direction = arrParam.Item(i).Direction
            'cmd.Parameters(arrParam.Item(i).Name).Value = arrParam.Item(i).Value
            cmd.Parameters.AddWithValue(arrParam.Item(i).Name, arrParam.Item(i).Value)
        Next

        odbcAdapt = New SqlDataAdapter(cmd)
        odbcAdapt.Fill(ds)

        Close()

        Return ds
    End Function


    Public Function GetDataTableSpNotDisCon(ByVal sp As String, ByVal arrParam As ArrayParam) As DataSet
        Dim ds As New DataSet
        Dim methodName As String = System.Reflection.MethodBase.GetCurrentMethod().Name
        Dim cmd As SqlCommand
        Dim odbcAdapt As New SqlDataAdapter
        Dim cmdText As String = ""
        Dim spParam As String = ""

        'For i As Integer = 0 To arrParam.Count - 1
        '    spParam += "?"
        '    If i < arrParam.Count - 1 Then
        '        spParam += ","
        '    End If
        'Next

        'If spParam = "" Then
        '    cmdText = "CALL " + sp
        'Else
        '    cmdText = "CALL " + sp + "(" + spParam + ")"
        'End If

        'cmd = New MySqlCommand(cmdText)
        cmd = New SqlCommand(sp)
        cmd.Connection = _sqlCon
        cmd.CommandType = CommandType.StoredProcedure

        For i As Integer = 0 To arrParam.Count - 1
            'cmd.Parameters.Add(arrParam.Item(i).Name, arrParam.Item(i).Type, arrParam.Item(i).Size)
            'cmd.Parameters(arrParam.Item(i).Name).Direction = arrParam.Item(i).Direction
            'cmd.Parameters(arrParam.Item(i).Name).Value = arrParam.Item(i).Value
            cmd.Parameters.AddWithValue(arrParam.Item(i).Name, arrParam.Item(i).Value)
        Next

        odbcAdapt = New SqlDataAdapter(cmd)
        odbcAdapt.Fill(ds)

        Return ds
    End Function
#End Region

#Region " GetDataTableCommndText "
    Public Function GetDataTableCommndText(ByVal sql As String) As DataSet
        Dim ds As New DataSet
        Dim methodName As String = System.Reflection.MethodBase.GetCurrentMethod().Name
        Dim cmd As SqlCommand
        Dim odbcAdapt As New SqlDataAdapter

        cmd = New SqlCommand(sql, _sqlCon)

        odbcAdapt.SelectCommand = cmd

        odbcAdapt.Fill(ds)

        Return ds

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
