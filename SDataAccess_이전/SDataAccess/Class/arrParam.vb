
#Region "StructParam"
Public Structure StructParam
    Private _paramName As String
    Private _paramValue As Object
    Private _paramType As SqlDbType
    Private _direction As ParameterDirection
    Private _size As Integer

    Public Property Name() As String
        Get
            Return _paramName
        End Get
        Set(ByVal Value As String)
            _paramName = Value
        End Set
    End Property
    Public Property Value() As Object
        Get
            Return _paramValue
        End Get
        Set(ByVal Value As Object)
            _paramValue = Value
        End Set
    End Property
    Public Property Type() As SqlDbType
        Get
            Return _paramType
        End Get
        Set(ByVal Value As SqlDbType)
            _paramType = Value
        End Set
    End Property

    Public Property Direction() As ParameterDirection
        Get
            Return _direction
        End Get
        Set(ByVal Value As ParameterDirection)
            _direction = Value
        End Set
    End Property

    Public Property Size() As Integer
        Get
            Return _size
        End Get
        Set(ByVal value As Integer)
            _size = value
        End Set
    End Property

End Structure
#End Region

#Region "ArrayParam"
Public Class ArrayParam

    Protected _List As New ArrayList

    Private _commandString As String
    Private _commandType As CommandType
    Private _commandTimeOut As Integer
    Private _serverName As String
    Private _databaseName As String

    '웹서비스 때문에 만든 의미없는 코드
    Public Sub Add(ByVal value As Object)
        Dim pSqlParam As New StructParam
        pSqlParam = value
        'pSqlParam.Name = value
        'pSqlParam.Value = value

        _List.Add(pSqlParam)
    End Sub

    'paramName : 파라미터 이름
    'value : 파라미터 값
    '(SQL 저장시 또는 파리미터 값을 가지는 개체간의 값 전달시)
    Public Sub Add(ByVal paramName As String, ByVal value As Object)
        Dim pSqlParam As New StructParam()
        pSqlParam.Name = paramName
        pSqlParam.Value = value
        pSqlParam.Type = SqlDbType.VarChar
        pSqlParam.Direction = ParameterDirection.Input
        _List.Add(pSqlParam)
    End Sub

    'SQL저장시 이용
    'dbType : Stored Procedure의 데이터 type
    Public Sub Add(ByVal paramName As String, ByVal value As Object, ByVal dbType As SqlDbType)
        Dim pSqlParam As New StructParam()
        pSqlParam.Name = paramName
        pSqlParam.Value = value
        pSqlParam.Type = dbType
        pSqlParam.Direction = ParameterDirection.Input
        _List.Add(pSqlParam)
    End Sub

    'SQL저장시 이용
    'dbType : Stored Procedure의 데이터 type
    Public Sub Add(ByVal paramName As String, ByVal value As Object, ByVal dbType As SqlDbType, ByVal direct As ParameterDirection)
        Dim pSqlParam As New StructParam()
        pSqlParam.Name = paramName
        pSqlParam.Value = value
        pSqlParam.Type = dbType
        pSqlParam.Direction = direct
        _List.Add(pSqlParam)
    End Sub

    'SQL저장시 이용
    'dbType : Stored Procedure의 데이터 type
    Public Sub Add(ByVal paramName As String, ByVal value As Object, ByVal size As Integer, ByVal dbType As SqlDbType, ByVal direct As ParameterDirection)
        Dim pSqlParam As New StructParam()
        pSqlParam.Name = paramName
        pSqlParam.Value = value
        pSqlParam.Type = dbType
        pSqlParam.Direction = direct
        pSqlParam.Size = size
        _List.Add(pSqlParam)
    End Sub

    Public Overloads Function Count() As Integer
        Return _List.Count
    End Function

    Public Sub Clear()
        _List.Clear()
    End Sub

    Public Function Clone() As Object
        Return _List.Clone
    End Function

    Public Sub Copy(ByVal aList As ArrayList)
        _List = aList
    End Sub

    Public Function Remove(ByVal index As Integer) As Boolean
        If index > _List.Count - 1 Or index < 0 Then
            Return False
        End If
        _List.RemoveAt(index)
        Return True
    End Function

    Public Function Insert(ByVal index As Integer, ByVal sParam As StructParam) As Boolean
        If index > _List.Count - 1 Or index < 0 Then
            Return False
        End If

        _List.Insert(index, sParam)
        Return True
    End Function

    Public Function Replace(ByVal index As Integer, ByVal itemText As Object) As Boolean
        If index > _List.Count - 1 Or index < 0 Then
            Return False
        End If

        Dim pSqlParam As New StructParam()
        pSqlParam.Name = Item(index).Name
        pSqlParam.Value = itemText
        pSqlParam.Type = Item(index).Type
        pSqlParam.Direction = Item(index).Direction

        Item(index) = pSqlParam

        Return True
    End Function

    '저장된 배열의 이름인덱스를 찾아서 값변경
    Public Function Replace(ByVal indexName As String, ByVal itemText As Object) As Boolean
        Dim ix As Integer
        For ix = 0 To _List.Count - 1
            If CType(_List.Item(ix), StructParam).Name.ToUpper = indexName.ToUpper Then
                Dim pSqlParam As New StructParam()
                pSqlParam.Name = Item(ix).Name
                pSqlParam.Value = itemText
                pSqlParam.Type = Item(ix).Type
                pSqlParam.Direction = Item(ix).Direction

                Item(ix) = pSqlParam
                Return True
            End If
        Next
        Return False
    End Function

    'Structure Index를 이용한 정보구하기
    Default Public Property Item(ByVal index As Integer) As StructParam
        Get
            Return CType(_List.Item(index), StructParam)
        End Get
        Set(ByVal Value As StructParam)
            _List.Item(index) = Value
        End Set
    End Property

    '필드이름을 이용해서 Structure정보 구하기
    Default Public Property Item(ByVal name As String) As StructParam
        Get
            Dim ix As Integer
            For ix = 0 To _List.Count - 1
                If CType(_List.Item(ix), StructParam).Name.ToUpper = name.ToUpper Then
                    Return CType(_List.Item(ix), StructParam)
                End If
            Next
            Return Nothing
        End Get
        Set(ByVal value As StructParam)
            Dim ix As Integer
            For ix = 0 To _List.Count - 1
                If CType(_List.Item(ix), StructParam).Name.ToUpper = name.ToUpper Then
                    _List.Item(ix) = value
                End If
            Next
        End Set
    End Property

    Public Property CommandType() As CommandType
        Get
            Return _commandType
        End Get
        Set(ByVal value As CommandType)
            _commandType = value
        End Set
    End Property

    Public Property CommandString() As String
        Get
            Return _commandString
        End Get
        Set(ByVal value As String)
            _commandString = value
        End Set
    End Property

    Public Property ServerName() As String
        Get
            Return _serverName
        End Get
        Set(ByVal value As String)
            _serverName = value
        End Set
    End Property

    Public Property DataBaseName() As String
        Get
            Return _databaseName
        End Get
        Set(ByVal value As String)
            _databaseName = value
        End Set
    End Property

    Public Property CommandTimeOut() As Integer
        Get
            Return _commandTimeOut
        End Get
        Set(ByVal value As Integer)
            _commandTimeOut = value
        End Set
    End Property

    '-------------------------------------------------------------------------------------------
    '리스트 배열의 값을 문자열로 변환한다.
    '사용 : 보고서 출력시 스크립트명령문장을 만들어야 하는데 ArrayParam은 String객체가 아니어서
    '       문자열의 + 연산이 불가능하다. 그래서 배열을 하나의 문자열로 변경한 후
    '       ConvertStringToArr을 이용해서 다시 배열로 재수집한다.
    '-------------------------------------------------------------------------------------------
    Public Function ConvertArrToString() As String

        Dim ix As Integer
        Dim aStr As String = ""
        Dim aName As String = ""
        Dim aValue As String = ""

        For ix = 0 To _List.Count - 1
            aName = CType(_List.Item(ix), StructParam).Name
            aValue = CType(_List.Item(ix), StructParam).Value
            If aStr <> "" Then
                aStr += "!#"
            End If
            aStr += aName + "!@" + aValue
        Next

        Return aStr
    End Function

    '-------------------------------------------------------------------------------------------
    '문자열을 배열의 요소로 추가한다.
    '사용 : 문자열을 분해해서 배열요소로 재수집한다.
    '-------------------------------------------------------------------------------------------
    Public Sub ConvertStringToArr(ByVal paramString As String)
        Dim ix1 As Integer
        Dim arrIndex1(), arrIndex2() As String

        If Trim(paramString) = "" Then Exit Sub

        arrIndex1 = Split(paramString, "!#")
        For ix1 = 0 To arrIndex1.Length - 1
            arrIndex2 = Split(arrIndex1(ix1), "!@")
            Dim pSqlParam As New StructParam()
            pSqlParam.Name = arrIndex2(0)
            pSqlParam.Value = arrIndex2(1)
            pSqlParam.Type = SqlDbType.VarChar
            _List.Add(pSqlParam)
        Next
    End Sub
End Class
#End Region

#Region "ArrayParams"
Public Class ArrayParams
    Private _list As ArrayList

    Public Sub New()
        _list = New ArrayList()
    End Sub

    'Public Sub Add(ByVal arrParam As Paik.DevTools.ArrayParam)
    '    Dim ap As New Paik.DevTools.ArrayParam

    '    ap.Copy(arrParam.Clone)

    '    _list.Add(ap)
    'End Sub

    Public Sub Add(ByVal aParam As ArrayParam)
        Dim param As New ArrayParam
        Dim sParam As StructParam

        param.CommandString = aParam.CommandString
        param.CommandType = aParam.CommandType
        param.ServerName = aParam.ServerName
        param.DataBaseName = aParam.DataBaseName
        param.CommandTimeOut = aParam.CommandTimeOut

        For ix As Integer = 0 To aParam.Count - 1
            sParam = New StructParam
            sParam.Direction = aParam(ix).Direction
            sParam.Name = aParam(ix).Name
            sParam.Size = aParam(ix).Size
            sParam.Type = aParam(ix).Type
            sParam.Value = aParam(ix).Value
            param.Add(sParam)
        Next

        _list.Add(param)
    End Sub

    Public Sub Clear()
        _list.Clear()
    End Sub

    Public Function Remove(ByVal index As Integer) As Boolean
        If index > Count - 1 Or index < 0 Then
            Return False
        End If

        _list.RemoveAt(index)
        Return True
    End Function

    Default Public Property Item(ByVal index As Integer) As ArrayParam
        Get
            Return _list(index)
        End Get
        Set(ByVal value As ArrayParam)
            _list(index) = value
        End Set
    End Property

    Public ReadOnly Property Count() As Integer
        Get
            Return _list.Count
        End Get
    End Property
End Class
#End Region
