Public Class clsKiwoomBaseInfo

#Region " 거래소 종목 가져온다 "
    Public Enum MaketGb
        All
        Kospi
        Kosdak
    End Enum

    Public Function GetStockListByMarKetAll(ByVal AxKH As AxKHOpenAPILib.AxKHOpenAPI) As DataSet
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

    Public Function GetStockListByMarKetKospi(ByVal AxKH As AxKHOpenAPILib.AxKHOpenAPI) As DataSet
        Dim str1 As String = AxKH.GetCodeListByMarket("0")
        Dim arrStockCode1 As String() = Split(str1, ";")
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

        ds.Tables.Add(dt)

        Return ds

    End Function

    Public Function GetStockListByMarKetKosDak(ByVal AxKH As AxKHOpenAPILib.AxKHOpenAPI) As DataSet
        Dim str1 As String = AxKH.GetCodeListByMarket("10")
        Dim arrStockCode1 As String() = Split(str1, ";")
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

        ds.Tables.Add(dt)

        Return ds

    End Function
#End Region

End Class
