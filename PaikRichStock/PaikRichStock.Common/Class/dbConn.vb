Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class dbConn
    Public Shared _sqlCon As New OleDb.OleDbConnection
    Protected _arrDirectValue As New ArrayParam
    Private _connectionString As String = ""
    Private _commandTimeOut As Integer

    Public Shared Function Open() As Boolean

        Try

            '//비밀번호가 없을때(참고)
            '"Provider=Microsoft.ACE.OLEDB.12.0;" & _
            '"Data Source=C:\myFolder\myAccess2007file.accdb;" & _
            '"Persist Security Info=False;

            '//비빌번호가 있을때(참고)
            '"Provider=Microsoft.ACE.OLEDB.12.0;" & _
            '"Data Source=C:\myFolder\myAccess2007file.accdb;" & _
            '"Jet OLEDB:Database Password=MyDbPassword;

            _sqlCon.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=c:\PaikStockDb\PaikStock.accdb;Persist Security Info=False;"

            _sqlCon.Open()

            Return True

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Exclamation)
        End Try

        Return False

    End Function


    Public Shared Function Close() As Boolean

        Try
            _sqlCon.Close()

            Return True
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Exclamation)
        End Try

        Return False

    End Function

#Region " DbExcuteQuery "
    Private Sub DbExcuteQuery(ByVal sql As String, ByVal arrParam As ArrayParam)

    End Sub
#End Region

#Region " ExecuteNonQuery "
    Public Shared Function ExecuteNonQuery(ByVal sp As String, ByVal arrParam As ArrayParam) As Integer
        Dim methodName As String = System.Reflection.MethodBase.GetCurrentMethod().Name
        Dim cmd As OleDb.OleDbCommand
        Dim oleAdapt As New OleDb.OleDbDataAdapter
        Dim retValue As Integer

        Try
            Common.dbConn.Open()

            cmd = New OleDb.OleDbCommand(sp, _sqlCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = sp

            For i As Integer = 0 To arrParam.Count - 1
                cmd.Parameters.Add(arrParam.Item(i).Name, arrParam.Item(i).Type, arrParam.Item(i).Size)
                cmd.Parameters(arrParam.Item(i).Name).Direction = arrParam.Item(i).Direction
                cmd.Parameters(arrParam.Item(i).Name).Value = arrParam.Item(i).Value
            Next

            retValue = cmd.ExecuteNonQuery()

            Common.dbConn.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

        If Common.dbConn._sqlCon.State = ConnectionState.Open Then
            Common.dbConn.Close()
        End If

    End Function

#End Region

#Region " GetDataTableSp "
    Public Shared Function GetDataTableSp(ByVal sql As String) As DataSet
        Dim ds As New DataSet
        Dim methodName As String = System.Reflection.MethodBase.GetCurrentMethod().Name
        Dim cmd As OleDb.OleDbCommand
        Dim oleAdapt As New OleDb.OleDbDataAdapter

        sql = "exec " & sql

        cmd = New OleDb.OleDbCommand(sql, _sqlCon)

        oleAdapt.SelectCommand = cmd

        oleAdapt.Fill(ds)

        Return ds

    End Function

    Public Shared Function GetDataTableSp(ByVal sql As String, ByVal arrParam As ArrayParam) As DataSet
        Dim ds As New DataSet
        Dim methodName As String = System.Reflection.MethodBase.GetCurrentMethod().Name
        Dim cmd As OleDb.OleDbCommand
        Dim oleAdapt As New OleDb.OleDbDataAdapter

        sql = sql

        cmd = New OleDb.OleDbCommand(sql, _sqlCon)

        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = sql

        For i As Integer = 0 To arrParam.Count - 1
            cmd.Parameters.Add(arrParam.Item(i).Name, arrParam.Item(i).Type, arrParam.Item(i).Size)
            cmd.Parameters(arrParam.Item(i).Name).Direction = arrParam.Item(i).Direction
            cmd.Parameters(arrParam.Item(i).Name).Value = arrParam.Item(i).Value
        Next

        oleAdapt.SelectCommand = cmd

        oleAdapt.Fill(ds)

        Return ds

    End Function
#End Region

#Region " GetDataTableCommndText "
    Public Shared Function GetDataTableCommndText(ByVal sql As String) As DataSet
        Dim ds As New DataSet
        Dim methodName As String = System.Reflection.MethodBase.GetCurrentMethod().Name
        Dim cmd As OleDb.OleDbCommand
        Dim oleAdapt As New OleDb.OleDbDataAdapter

        cmd = New OleDb.OleDbCommand(sql, _sqlCon)

        oleAdapt.SelectCommand = cmd

        oleAdapt.Fill(ds)

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
