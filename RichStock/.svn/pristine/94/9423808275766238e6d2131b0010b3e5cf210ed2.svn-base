Public Class clsGabage
    ''' <summary>
    ''' 실시간 시세해지 예시
    ''' </summary>
    ''' <param name="allRemove"></param>
    ''' <param name="ScreenNo"></param>
    ''' <param name="stockCode"></param>
    ''' <returns></returns>
    ''' <remarks>
    '''  [SetRealRemove() 함수]
    '''[실시간 시세해지 예시]
    '''OpenAPI.SetRealRemove("0150", "039490");  // "0150"화면에서 "039490"종목해지
    '''OpenAPI.SetRealRemove("ALL", "ALL");  // 모든 화면에서 실시간 해지
    '''OpenAPI.SetRealRemove("0150", "ALL");  // 모든 화면에서 실시간 해지
    '''OpenAPI.SetRealRemove("ALL", "039490");  // 모든 화면에서 실시간 해지
    ''' </remarks>
    'Public Function SetRealRemove(ByVal allRemove As Boolean, ByVal ScreenNo As String, ByVal stockCode As String) As Boolean

    '    Dim dr As DataRow

    '    Try
    '        If allRemove = True Then
    '            If stockCode = "" Then
    '                AxKH.SetRealRemove("ALL", "ALL")
    '                _DtScreenNoManage.Clear()
    '            Else
    '                AxKH.SetRealRemove("ALL", stockCode)

    '                For row As Integer = _DtScreenNoManage.Rows.Count - 1 To 0 Step -1
    '                    dr = _DtScreenNoManage.Rows(row)

    '                    If dr("STOCK_CODE").ToString.Trim = stockCode Then
    '                        _DtScreenNoManage.Rows.RemoveAt(row)
    '                    End If

    '                Next

    '            End If

    '        Else
    '            If stockCode = "" Then
    '                AxKH.SetRealRemove(ScreenNo, "ALL")


    '                For row As Integer = _DtScreenNoManage.Rows.Count - 1 To 0 Step -1
    '                    dr = _DtScreenNoManage.Rows(row)

    '                    If dr("SCREEN_NO").ToString.Trim = ScreenNo Then
    '                        _DtScreenNoManage.Rows.RemoveAt(row)
    '                    End If

    '                Next

    '            Else

    '                AxKH.SetRealRemove(ScreenNo, stockCode)

    '                For row As Integer = _DtScreenNoManage.Rows.Count - 1 To 0 Step -1

    '                    dr = _DtScreenNoManage.Rows(row)
    '                    ' 다수종목에 있는 화면번호는 삭제하지 않는다..다수종목은 화면번호로 삭제....
    '                    If dr("SCREEN_NO").ToString.Trim = ScreenNo And dr("STOCK_CODE").ToString.Trim = stockCode Then
    '                        _DtScreenNoManage.Rows.RemoveAt(row)
    '                    End If

    '                Next

    '            End If

    '        End If

    '        Return True
    '    Catch ex As Exception
    '        MsgBox(ex.ToString, MsgBoxStyle.Exclamation)
    '    End Try
    '    Return False
    'End Function
End Class
