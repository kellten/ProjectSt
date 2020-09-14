Public Class clsConditionSearch

    Public Function SetUserConditionList(ByVal strConList As String) As DataSet
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

        Return ds

    End Function

    Public Function SetUserConditionStockList(ByVal strCodeList As String, ByVal AxKH As AxKHOpenAPILib.AxKHOpenAPI) As DataSet
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

        Return ds

    End Function

    Public Function SetUserConditionStockReal(ByVal e As AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealConditionEvent, ByVal AxKH As AxKHOpenAPILib.AxKHOpenAPI) As DataSet
        Dim ds As DataSet = New DataSet
        Dim dt As New DataTable("CondiStockReal")
        Dim dr As DataRow
        With dt.Columns
            .Add("STR_TYPE")
            .Add("STOCK_CODE")
            .Add("STOCK_NAME")
            .Add("STR_CONDITION_INDEX")
            .Add("STR_CONDITION_NAME")
        End With

        dr = dt.Rows.Add()
        dr("STR_TYPE") = e.strType
        dr("STOCK_CODE") = e.sTrCode
        dr("STOCK_NAME") = AxKH.GetMasterCodeName(e.sTrCode)
        dr("STR_CONDITION_INDEX") = e.strConditionIndex
        dr("STR_CONDITION_NAME") = e.strConditionName
        ds.Tables.Add(dt)

        Return ds
    End Function
End Class
