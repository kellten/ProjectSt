﻿
Public Class DataAccess

    Private _arrParam As New ArrayParam
    Private _mySqlDbConn As New mySqlDbConn


#Region "p_Psi01Query"
    '--------------------------------------------------
    '관심그룹을 가져온다.
    '--------------------------------------------------
    Public Function p_Psi01Query(ByVal query As String, ByVal stockId As String, ByVal interId As Integer, ByVal interName As String, _
            Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet
        Dim returnDs As DataSet
        Try

            If mySqlDbConn._sqlCon.State = ConnectionState.Closed Then
                mySqlDbConn.Open()
            End If

            With _arrParam
                .Clear()
                .Add("_QUERY", query)
                .Add("_STOCK_ID", stockId)
                .Add("_INTER_ID", interId)
                .Add("_INTER_NAME", interName)
            End With

            returnDs = mySqlDbConn.GetDataTableSp("p_Psi01Query", _arrParam)

            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If

            Return returnDs

        Catch ex As Exception
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If
            Return Nothing
        End Try
    End Function

    Public Function Open() As Boolean
        Try
            If mySqlDbConn._sqlCon.State = ConnectionState.Closed Then
                mySqlDbConn.Open()
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function DisConnect() As Boolean
        Try
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

#Region "p_Psi01Add"
    '--------------------------------------------------
    '관심그룹을 저장한다.
    '--------------------------------------------------
    Public Function p_Psi01Add(ByVal query As String, ByVal stockId As String, ByVal interId As Integer, ByVal interName As String, _
            Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As Boolean

        Try
            If mySqlDbConn._sqlCon.State = ConnectionState.Closed Then
                mySqlDbConn.Open()
            End If

            With _arrParam
                .Clear()
                .Add("_STOCK_ID", stockId)
                .Add("_INTER_ID", interId)
                .Add("_INTER_NAME", interName)
            End With

            mySqlDbConn.ExecuteNonQuery("p_Psi01Add", _arrParam)

            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If

            Return True

        Catch ex As Exception
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If
            Return False
        End Try

    End Function
#End Region

#Region "p_Psi02Query"
    '--------------------------------------------------
    '관심그룹의 종목을 가져온다.
    '--------------------------------------------------
    Public Function p_Psi02Query(ByVal query As String, ByVal stockId As String, ByVal interId As Integer, ByVal stockCode As String, _
                                 ByVal stockPoint As String, ByVal remark As String, _
            Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        Dim returnDs As DataSet
        Dim cmd As String = ""

        Try

            If mySqlDbConn._sqlCon.State = ConnectionState.Closed Then
                mySqlDbConn.Open()
            End If

            With _arrParam
                .Clear()
                .Add("_QUERY", query)
                .Add("_STOCK_ID", stockId)
                .Add("_INTER_ID", interId)
                .Add("_STOCK_CODE", stockCode)
                .Add("_STOCK_POINT", stockPoint)
                .Add("_REMARK", remark)
            End With

            returnDs = mySqlDbConn.GetDataTableSp("p_Psi02Query", _arrParam)

            ' returnDs = mySqlDbConn.GetDataTableCommndText(cmd)

            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If

            Return returnDs

        Catch ex As Exception
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If
            Return Nothing
        End Try


    End Function
#End Region

#Region "p_Psi02Add"
    '--------------------------------------------------
    '관심그룹의 종목을 저장한다.
    '--------------------------------------------------
    Public Function p_Psi02Add(ByVal query As String, ByVal stockId As String, ByVal interId As Integer, ByVal stockCode As String, _
                                 ByVal stockPoint As String, ByVal remark As String, ByVal monitorYn As String, _
            Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As Boolean

        Dim cmd As String = ""

        Try
            If mySqlDbConn._sqlCon.State = ConnectionState.Closed Then
                mySqlDbConn.Open()
            End If

            With _arrParam
                .Clear()
                .Add("_QUERY", query)
                .Add("_STOCK_ID", stockId)
                .Add("_INTER_ID", interId)
                .Add("_STOCK_CODE", stockCode)
                .Add("_STOCK_POINT", stockPoint)
                .Add("_REMARK", remark)
                .Add("_MONITOR_YN", monitorYn)
            End With

            mySqlDbConn.ExecuteNonQuery("p_Psi02Add", _arrParam)

            ' returnDs = mySqlDbConn.GetDataTableCommndText(cmd)

            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If

            Return True

        Catch ex As Exception
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If
            Return Nothing
        End Try


    End Function
#End Region

#Region "p_psi02Del"
    '--------------------------------------------------
    '관심그룹의 종목을 저장한다.
    '--------------------------------------------------
    Public Function p_psi02Del(ByVal query As String, ByVal stockId As String, ByVal interId As Integer, ByVal stockCode As String, _
            Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As Boolean

        Dim cmd As String = ""

        Try

            If mySqlDbConn._sqlCon.State = ConnectionState.Closed Then
                mySqlDbConn.Open()
            End If

            With _arrParam
                .Clear()
                .Add("_STOCK_ID", stockId)
                .Add("_INTER_ID", interId)
                .Add("_STOCK_CODE", stockCode)
            End With

            mySqlDbConn.ExecuteNonQuery("p_psi02Del", _arrParam)

            ' returnDs = mySqlDbConn.GetDataTableCommndText(cmd)

            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If

            Return True

        Catch ex As Exception
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If
            Return False
        End Try


    End Function
#End Region

#Region "p_D1a01Add"
    '--------------------------------------------------
    '공시내역을 저장한다.
    '--------------------------------------------------
    Public Function p_D1a01Add(ByVal query As String, ByVal deungDate As String, ByVal stockCode As String, _
                            ByVal deungPrice As String, ByVal creator As String, ByVal title As String, ByVal link As String, _
            Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As Boolean

        Dim cmd As String = ""

        Try

            If mySqlDbConn._sqlCon.State = ConnectionState.Closed Then
                mySqlDbConn.Open()
            End If

            With _arrParam
                .Clear()
                .Add("_QUERY", query)
                .Add("_DEUNG_DATE", deungDate)
                .Add("_STOCK_CODE", stockCode)
                .Add("_DEUNG_PRICE", deungPrice)
                .Add("_CREATOR", creator)
                .Add("_TITLE", title)
                .Add("_LINK", link)
            End With

            mySqlDbConn.ExecuteNonQuery("p_D1a01Add", _arrParam)

            ' returnDs = mySqlDbConn.GetDataTableCommndText(cmd)

            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If

            Return True

        Catch ex As Exception
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If
            Return Nothing
        End Try


    End Function
#End Region

#Region "p_stock_day_data_Add"
    '--------------------------------------------------
    '일봉차트데이터저장
    '--------------------------------------------------
    Public Function p_stock_day_data_Add(ByVal query As String, _
                                            ByVal _STOCK_CODE As String, _
                                            ByVal _END_PRICE As String, _
                                            ByVal _VOLUME As String, _
                                            ByVal _TRADING_VALUE As String, _
                                            ByVal _STOCK_DATE As String, _
                                            ByVal _S_PRICE As String, _
                                            ByVal _H_PRICE As String, _
                                            ByVal _L_PRICE As String, _
                                            ByVal _MOD_GUBUN As String, _
                                            ByVal _MOD_RATE As String, _
                                            ByVal _CATE_GUBUN1 As String, _
                                            ByVal _CATE_GUBUN2 As String, _
                                            ByVal _STOCK_INFO As String, _
                                            ByVal _MOD_EVENT As String, _
                                            ByVal _PRE_E_PRICE As String, _
                                            ByVal _LOW_MA As String, _
                                            ByVal _LOWEST_PERIOD As String, _
                                            ByVal _ENDLOW_PERIOD As String, _
                                            ByVal _LOWEST_MA As String, _
                                            ByVal _LOWEND_MA As String, _
                                            ByVal _H_LOWEND_MA As String, _
                                            ByVal isConinueConnect As Boolean, _
            Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As Boolean


        If mySqlDbConn._sqlCon.State = ConnectionState.Closed Then
            mySqlDbConn.Open()
        End If

        With _arrParam
            .Clear()
            .Add("_QUERY", "A")
            .Add("_STOCK_CODE", _STOCK_CODE)
            .Add("_END_PRICE", _END_PRICE)
            .Add("_VOLUME", _VOLUME)
            .Add("_TRADING_VALUE", _TRADING_VALUE)
            .Add("_STOCK_DATE", _STOCK_DATE)
            .Add("_S_PRICE", _S_PRICE)
            .Add("_H_PRICE", _H_PRICE)
            .Add("_L_PRICE", _L_PRICE)
            .Add("_MOD_GUBUN", _MOD_GUBUN)
            .Add("_MOD_RATE", Val(_MOD_RATE))
            .Add("_CATE_GUBUN1", _CATE_GUBUN1)
            .Add("_CATE_GUBUN2", _CATE_GUBUN2)
            .Add("_STOCK_INFO", _STOCK_INFO)
            .Add("_MOD_EVENT", _MOD_EVENT)
            .Add("_PRE_E_PRICE", Val(_PRE_E_PRICE))
            .Add("_LOW_MA", Val(_LOW_MA))
            .Add("_LOWEST_PERIOD", Val(_LOWEST_PERIOD))
            .Add("_ENDLOW_PERIOD", Val(_ENDLOW_PERIOD))
            .Add("_LOWEST_MA", Val(_LOWEST_MA))
            .Add("_LOWEND_MA", Val(IIf(_LOWEND_MA Is Nothing, 0, _LOWEND_MA)))
            .Add("_H_LOWEND_MA", Val(IIf(_H_LOWEND_MA Is Nothing, 0, _H_LOWEND_MA)))
        End With

        Try
            mySqlDbConn.ExecuteNonQuery("p_stock_day_data_Add", _arrParam)
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                If isConinueConnect <> True Then
                    mySqlDbConn.Close()
                End If
            End If
            Return True
        Catch ex As Exception
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If
            Return False
        End Try

        Return False
    End Function
#End Region

#Region "p_stock_week_data_Add"
    '--------------------------------------------------
    '주봉차트데이터저장
    '--------------------------------------------------
    Public Function p_stock_week_data_Add(ByVal query As String, _
                                            ByVal _STOCK_CODE As String, _
                                            ByVal _END_PRICE As String, _
                                            ByVal _VOLUME As String, _
                                            ByVal _TRADING_VALUE As String, _
                                            ByVal _STOCK_DATE As String, _
                                            ByVal _S_PRICE As String, _
                                            ByVal _H_PRICE As String, _
                                            ByVal _L_PRICE As String, _
                                            ByVal _MOD_GUBUN As String, _
                                            ByVal _MOD_RATE As String, _
                                            ByVal _CATE_GUBUN1 As String, _
                                            ByVal _CATE_GUBUN2 As String, _
                                            ByVal _STOCK_INFO As String, _
                                            ByVal _MOD_EVENT As String, _
                                            ByVal _PRE_E_PRICE As String, _
                                            ByVal isConinueConnect As Boolean, _
            Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As Boolean


        If mySqlDbConn._sqlCon.State = ConnectionState.Closed Then
            mySqlDbConn.Open()
        End If

        With _arrParam
            .Clear()
            .Add("_QUERY", query)
            .Add("_STOCK_CODE", _STOCK_CODE)
            .Add("_END_PRICE", _END_PRICE)
            .Add("_VOLUME", _VOLUME)
            .Add("_TRADING_VALUE", _TRADING_VALUE)
            .Add("_STOCK_DATE", _STOCK_DATE)
            .Add("_S_PRICE", _S_PRICE)
            .Add("_H_PRICE", _H_PRICE)
            .Add("_L_PRICE", _L_PRICE)
            .Add("_MOD_GUBUN", _MOD_GUBUN)
            .Add("_MOD_RATE", Val(_MOD_RATE))
            .Add("_CATE_GUBUN1", _CATE_GUBUN1)
            .Add("_CATE_GUBUN2", _CATE_GUBUN2)
            .Add("_STOCK_INFO", _STOCK_INFO)
            .Add("_MOD_EVENT", _MOD_EVENT)
            .Add("_PRE_E_PRICE", Val(_PRE_E_PRICE))
        End With

        Try
            mySqlDbConn.ExecuteNonQuery("p_stock_week_data_Add", _arrParam)
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                If isConinueConnect <> True Then
                    mySqlDbConn.Close()
                End If
            End If
            Return True
        Catch ex As Exception
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If
            Return False
        End Try

        Return False
    End Function
#End Region

#Region "p_company_info_Add"
    '--------------------------------------------------
    '회사 기본 데이터 저장
    '--------------------------------------------------
    Public Function p_company_info_Add(ByVal query As String, _
                                            ByVal _STOCK_CODE As String, _
                                            ByVal _JUJU As String, _
                                            ByVal _CLASS_GB As String, _
                                            ByVal _CEO As String, _
                                            ByVal _PRE_CNAME As String, _
                                            ByVal _CREATE_DATE As String, _
                                            ByVal _SANGJANG_DATE As String, _
                                            ByVal _WORKER_CNT As String, _
                                            ByVal _GROUP_NAME As String, _
                                            ByVal _HOMEPAGE As String, _
                                            ByVal _ADDRESS As String, _
                                            ByVal _COMPANY_TEL As String, _
                                            ByVal _JUDAM_TEL As String, _
                                            ByVal _GAMSA_TEXT As String, _
                                            ByVal _MAIN_BANK As String, _
                                            ByVal _MAIN_PRODUCT As String, _
                                            ByVal _ETC As String, _
                                            ByVal _REMARK As String, _
                                            ByVal isConinueConnect As Boolean,
                                            Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As Boolean
        If mySqlDbConn._sqlCon.State = ConnectionState.Closed Then
            mySqlDbConn.Open()
        End If

        With _arrParam
            .Clear()
            .Add("_QUERY", query)
            .Add("_STOCK_CODE", _STOCK_CODE)
            .Add("_JUJU", _JUJU)
            .Add("_CLASS_GB", _CLASS_GB)
            .Add("_CEO", _CEO)
            .Add("_PRE_CNAME", _PRE_CNAME)
            .Add("_CREATE_DATE", _CREATE_DATE)
            .Add("_SANGJANG_DATE", _SANGJANG_DATE)
            .Add("_WORKER_CNT", _WORKER_CNT)
            .Add("_GROUP_NAME", _GROUP_NAME.Replace("'", "''"))
            .Add("_HOMEPAGE", _HOMEPAGE.Replace("'", "''"))
            .Add("_ADDRESS", _ADDRESS)
            .Add("_COMPANY_TEL", _COMPANY_TEL)
            .Add("_JUDAM_TEL", _JUDAM_TEL)
            .Add("_GAMSA_TEXT", _GAMSA_TEXT.Replace("'", "''"))
            .Add("_MAIN_BANK", _MAIN_BANK.Replace("'", "''"))
            .Add("_MAIN_PRODUCT", _MAIN_PRODUCT.Replace("'", "''"))
            .Add("_ETC", _ETC)
            .Add("_REMARK", _REMARK.Replace("'", "''"))
        End With

        Try
            mySqlDbConn.ExecuteNonQuery("p_company_info_Add", _arrParam)

            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                If isConinueConnect <> True Then
                    mySqlDbConn.Close()
                End If
            End If
            Return True
        Catch ex As Exception
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If
            Return False
        End Try

        Return False
    End Function
#End Region

#Region "p_company_info_query"
    '--------------------------------------------------
    '회사정보를 가져온다
    '--------------------------------------------------
    Public Function p_company_info_query(ByVal query As String, ByVal stockCode As String, ByVal isConinueConnect As Boolean, _
            Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        Dim returnDs As DataSet
        Dim cmd As String = ""

        Try
            If mySqlDbConn._sqlCon.State = ConnectionState.Closed Then
                mySqlDbConn.Open()
            End If


            With _arrParam
                .Clear()
                .Add("_QUERY", query)
                .Add("_STOCK_CODE", stockCode)
            End With

            returnDs = mySqlDbConn.GetDataTableSp("p_company_info_query", _arrParam)

            ' returnDs = mySqlDbConn.GetDataTableCommndText(cmd)

            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                If isConinueConnect <> True Then
                    mySqlDbConn.Close()
                End If
            End If

            Return returnDs
        Catch ex As Exception
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If
            Return Nothing
        End Try
    End Function
#End Region

#Region "p_stock_day_data_query"
    '--------------------------------------------------
    '일봉데이터 정보를 가져온다
    '--------------------------------------------------
    Public Function p_stock_day_data_query(ByVal query As String, ByVal stockCode As String, ByVal stockDate As String, ByVal isConinueConnect As Boolean, _
            Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        Dim returnDs As DataSet
        Dim cmd As String = ""

        If mySqlDbConn._sqlCon.State = ConnectionState.Closed Then
            mySqlDbConn.Open()
        End If


        With _arrParam
            .Clear()
            .Add("_QUERY", query)
            .Add("_STOCK_CODE", stockCode)
            .Add("_STOCK_DATE", stockDate)
        End With

        returnDs = mySqlDbConn.GetDataTableSp("p_stock_day_data_query", _arrParam)

        Try
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                If isConinueConnect <> True Then
                    mySqlDbConn.Close()
                End If
            End If
            Return returnDs
        Catch ex As Exception
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If
            Return returnDs
        End Try
    End Function
#End Region

#Region "p_stock_week_data_query"
    '--------------------------------------------------
    '주봉데이터 정보를 가져온다
    '--------------------------------------------------
    Public Function p_stock_week_data_query(ByVal query As String, ByVal stockCode As String, ByVal stockDate As String, ByVal isConinueConnect As Boolean, _
            Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        Dim returnDs As DataSet
        Dim cmd As String = ""

        If mySqlDbConn._sqlCon.State = ConnectionState.Closed Then
            mySqlDbConn.Open()
        End If


        With _arrParam
            .Clear()
            .Add("_QUERY", query)
            .Add("_STOCK_CODE", stockCode)
            .Add("_STOCK_DATE", stockDate)
        End With

        returnDs = mySqlDbConn.GetDataTableSp("p_stock_week_data_query", _arrParam)

        Try
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                If isConinueConnect <> True Then
                    mySqlDbConn.Close()
                End If
            End If
            Return returnDs
        Catch ex As Exception
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If
            Return returnDs
        End Try
    End Function
#End Region

#Region "p_stock_day_data_query_MALowEnd"
    '--------------------------------------------------
    '일봉데이터 정보를 가져온다
    '--------------------------------------------------
    Public Function p_stock_day_data_query_MALowEnd(ByVal query As String, ByVal stockCode As String, ByVal stockDate As String, _
            Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        Dim returnDs As DataSet
        Dim cmd As String = ""

        Try
            If mySqlDbConn._sqlCon.State = ConnectionState.Closed Then
                mySqlDbConn.Open()
            End If


            With _arrParam
                .Clear()
                .Add("_QUERY", query)
                .Add("_STOCK_CODE", stockCode)
                .Add("_STOCK_DATE", stockDate)
            End With

            returnDs = mySqlDbConn.GetDataTableSp("p_stock_day_data_query_MALowEnd", _arrParam)

            ' returnDs = mySqlDbConn.GetDataTableCommndText(cmd)

            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If

            Return returnDs

        Catch ex As Exception
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If
            Return Nothing
        End Try
    End Function
#End Region

#Region "p_stock_day_data_query_Line"
    '--------------------------------------------------
    '이평선의 가격을 구한다.
    '--------------------------------------------------
    Public Function p_stock_day_data_query_Line(ByVal stockCode As String, ByVal line As String, _
            Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        Dim returnDs As DataSet
        Dim cmd As String = ""

        Try
            If mySqlDbConn._sqlCon.State = ConnectionState.Closed Then
                mySqlDbConn.Open()
            End If


            With _arrParam
                .Clear()
                .Add("_STOCK_CODE", stockCode)
                .Add("_LINE", line)
            End With

            returnDs = mySqlDbConn.GetDataTableSp("p_stock_day_data_query_Line", _arrParam)

            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If

            Return returnDs

        Catch ex As Exception
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If
            Return Nothing
        End Try
    End Function
#End Region

#Region "p_Stock_Master_Add"
    '--------------------------------------------------
    'STOCK 마스터를 저장
    '--------------------------------------------------
    Public Function p_Stock_Master_Add(ByVal query As String, ByVal stockCode As String, ByVal stockName As String, ByVal gubun As String, ByVal isConinueConnect As Boolean, _
            Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As Boolean
        Dim cmd As String = ""

        If mySqlDbConn._sqlCon.State = ConnectionState.Closed Then
            mySqlDbConn.Open()
        End If


        With _arrParam
            .Clear()
            .Add("_QUERY", query)
            .Add("_STOCK_CODE", stockCode)
            .Add("_STOCK_NAME", stockName)
            .Add("_GUBUN", gubun)
        End With

        Try
            mySqlDbConn.ExecuteNonQuery("p_Stock_Master_Add", _arrParam)

            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                If isConinueConnect <> True Then
                    mySqlDbConn.Close()
                End If
            End If
            Return True
        Catch ex As Exception
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If
            Return False
        End Try
    End Function
#End Region

#Region "p_company_ceo_Add"
    '--------------------------------------------------
    'STOCK 마스터를 저장
    '--------------------------------------------------
    Public Function p_company_ceo_Add(ByVal query As String,
                                       ByVal STOCK_CODE As String, _
                                        ByVal PUB_DATE As String, _
                                        ByVal NAME As String, _
                                        ByVal SEX As String, _
                                        ByVal BIRTH_DAY As String, _
                                        ByVal GRADE As String, _
                                        ByVal IS_MANAGER As String, _
                                        ByVal IS_IN_COMPANY As String, _
                                        ByVal DAMDANG_WORK As String, _
                                        ByVal CAREER As String, _
                                        ByVal POWER_STOCK As String, _
                                        ByVal NPOWER_STOCK As String, _
                                        ByVal WORK_PERIOD As String, _
                                        ByVal END_WORKDATE As String, _
                                        ByVal isConinueConnect As Boolean, _
            Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As Boolean
        Dim cmd As String = ""

        If mySqlDbConn._sqlCon.State = ConnectionState.Closed Then
            mySqlDbConn.Open()
        End If


        With _arrParam
            .Clear()
            .Add("_QUERY", query)
            .Add("_STOCK_CODE", STOCK_CODE)
            .Add("_PUB_DATE", PUB_DATE)
            .Add("_NAME", NAME)
            .Add("_SEX", SEX)
            .Add("_BIRTH_DAY", BIRTH_DAY)
            .Add("_GRADE", GRADE)
            .Add("_IS_MANAGER", IS_MANAGER)
            .Add("_IS_IN_COMPANY", IS_IN_COMPANY)
            .Add("_DAMDANG_WORK", DAMDANG_WORK)
            .Add("_CAREER", CAREER)
            .Add("_POWER_STOCK", POWER_STOCK)
            .Add("_NPOWER_STOCK", NPOWER_STOCK)
            .Add("_WORK_PERIOD", WORK_PERIOD)
            .Add("_END_WORKDATE", END_WORKDATE)
        End With

        Try
            mySqlDbConn.ExecuteNonQuery("p_company_ceo_Add", _arrParam)

            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                If isConinueConnect <> True Then
                    mySqlDbConn.Close()
                End If
            End If
            Return True
        Catch ex As Exception
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If
            Return False
        End Try
    End Function
#End Region

#Region "p_stock_finance_Add"
    '--------------------------------------------------
    '일봉차트데이터저장
    '--------------------------------------------------
    Public Function p_stock_finance_Add(ByVal query As String, _
                                            _STOCK_CODE As String, _
                                            _STOCK_NAME As String, _
                                            _REULT_MONTH As String, _
                                            _BASE_P As String, _
                                            _CAPITAL_P As String, _
                                            _STOCK_QTY As String, _
                                            _CREDIT_RATE As String, _
                                            _YEAR_HIGH_P As String, _
                                            _YEAR_LOW_P As String, _
                                            _STOCK_TOTAL_P As String, _
                                            _STOCK_TOTAL_P_RATE As String, _
                                            _F_REDUCE_RATE As String, _
                                            _RENTAL_P As String, _
                                            _PER As String, _
                                            _EPS As String, _
                                            _ROE As String, _
                                            _PBR As String, _
                                            _EV As String, _
                                            _BPS As String, _
                                            _SALES_P As String, _
                                            _O_PROFIT As String, _
                                            _P_PROFIT As String, _
                                            _HIGH_250 As String, _
                                            _LOW_250 As String, _
                                            _S_PRICE As String, _
                                            _H_PRICE As String, _
                                            _L_PROCE As String, _
                                            _UP_PRICE As String, _
                                            _DOWN_PRICE As String, _
                                            _STD_PRICE As String, _
                                            _HIGH_250_DAY As String, _
                                            _HIGH_250_RATE As String, _
                                            _LOW_250_DAY As String, _
                                            _LOW_250_RATE As String, _
                                            _END_PRICE As String, _
                                            _SYMBOL As String, _
                                            _PRE_CONPARE As String, _
                                            _END_PRICE_RATE As String, _
                                            _VOLUME As String, _
                                            _VOLUME_RATE As String, _
                                            _PRICE_UNIT As String, _
                                            ByVal isConinueConnect As Boolean, _
            Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As Boolean


        If mySqlDbConn._sqlCon.State = ConnectionState.Closed Then
            mySqlDbConn.Open()
        End If

        With _arrParam
            .Clear()
            .Add("_QUERY", query)
            .Add("_STOCK_CODE", _STOCK_CODE)
            .Add("_STOCK_NAME", _STOCK_NAME)
            .Add("_REULT_MONTH", _REULT_MONTH)
            .Add("_BASE_P", _BASE_P)
            .Add("_CAPITAL_P", _CAPITAL_P)
            .Add("_STOCK_QTY", _STOCK_QTY)
            .Add("_CREDIT_RATE", _CREDIT_RATE)
            .Add("_YEAR_HIGH_P", _YEAR_HIGH_P)
            .Add("_YEAR_LOW_P", _YEAR_LOW_P)
            .Add("_STOCK_TOTAL_P", _STOCK_TOTAL_P)
            .Add("_STOCK_TOTAL_P_RATE", _STOCK_TOTAL_P_RATE)
            .Add("_F_REDUCE_RATE", _F_REDUCE_RATE)
            .Add("_RENTAL_P", _RENTAL_P)
            .Add("_PER", _PER)
            .Add("_EPS", _EPS)
            .Add("_ROE", _ROE)
            .Add("_PBR", _PBR)
            .Add("_EV", _EV)
            .Add("_BPS", _BPS)
            .Add("_SALES_P", _SALES_P)
            .Add("_O_PROFIT", _O_PROFIT)
            .Add("_P_PROFIT", _P_PROFIT)
            .Add("_HIGH_250", _HIGH_250)
            .Add("_LOW_250", _LOW_250)
            .Add("_S_PRICE", _S_PRICE)
            .Add("_H_PRICE", _H_PRICE)
            .Add("_L_PROCE", _L_PROCE)
            .Add("_UP_PRICE", _UP_PRICE)
            .Add("_DOWN_PRICE", _DOWN_PRICE)
            .Add("_STD_PRICE", _STD_PRICE)
            .Add("_HIGH_250_DAY", _HIGH_250_DAY)
            .Add("_HIGH_250_RATE", _HIGH_250_RATE)
            .Add("_LOW_250_DAY", _LOW_250_DAY)
            .Add("_LOW_250_RATE", _LOW_250_RATE)
            .Add("_END_PRICE", _END_PRICE)
            .Add("_SYMBOL", _SYMBOL)
            .Add("_PRE_CONPARE", _PRE_CONPARE)
            .Add("_END_PRICE_RATE", _END_PRICE_RATE)
            .Add("_VOLUME", _VOLUME)
            .Add("_VOLUME_RATE", _VOLUME_RATE)
            .Add("_PRICE_UNIT", _PRICE_UNIT)
        End With

        Try
            'mySqlDbConn.GetDataTableSp("p_stock_finance_Add", _arrParam)
            mySqlDbConn.ExecuteNonQuery("p_stock_finance_Add", _arrParam)
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                If isConinueConnect <> True Then
                    mySqlDbConn.Close()
                End If
            End If
            Return True
        Catch ex As Exception
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If
            Return False
        End Try

        Return False
    End Function
#End Region

#Region "p_stock_batch_mibi"
    '--------------------------------------------------
    '일봉데이터 미비내역을 가져온다
    '--------------------------------------------------
    Public Function p_stock_batch_mibi(Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        Dim returnDs As DataSet
        Dim cmd As String = ""

        Try
            If mySqlDbConn._sqlCon.State = ConnectionState.Closed Then
                mySqlDbConn.Open()
            End If


            With _arrParam
                .Clear()
            End With

            returnDs = mySqlDbConn.GetDataTableSp("p_stock_batch_mibi", _arrParam)

            ' returnDs = mySqlDbConn.GetDataTableCommndText(cmd)

            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If

            Return returnDs
        Catch ex As Exception
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If
            Return Nothing
        End Try
    End Function
#End Region

#Region "p_stock_day_data_query_Line2"
    '--------------------------------------------------
    '일봉데이터 미비내역을 가져온다
    '--------------------------------------------------
    Public Function p_stock_day_data_query_Line2(ByVal query As String, ByVal stockCode As String, ByVal isConinueConnect As Boolean, Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        Dim returnDs As DataSet
        Dim cmd As String = ""

        If mySqlDbConn._sqlCon.State = ConnectionState.Closed Then
            mySqlDbConn.Open()
        End If


        With _arrParam
            .Clear()
            .Add("_QUERY", query)
            .Add("_STOCK_CODE", stockCode)
        End With

        returnDs = mySqlDbConn.GetDataTableSp("p_stock_day_data_query_Line2", _arrParam)
        Try
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                If isConinueConnect <> True Then
                    mySqlDbConn.Close()
                End If
            End If
            Return returnDs
        Catch ex As Exception
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If
            Return returnDs
        End Try
    End Function
#End Region

#Region "p_stock_day_data_query_BB"
    '--------------------------------------------------
    '전일 볼밴 라인을 가지고 온다.
    '--------------------------------------------------
    Public Function p_stock_day_data_query_BB(ByVal stockCode As String, ByVal isConinueConnect As Boolean, Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        Dim returnDs As DataSet
        Dim cmd As String = ""

        If mySqlDbConn._sqlCon.State = ConnectionState.Closed Then
            mySqlDbConn.Open()
        End If


        With _arrParam
            .Clear()
            .Add("_STOCK_CODE", stockCode)
        End With

        returnDs = mySqlDbConn.GetDataTableSp("p_stock_day_data_query_BB", _arrParam)
        Try
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                If isConinueConnect <> True Then
                    mySqlDbConn.Close()
                End If
            End If
            Return returnDs
        Catch ex As Exception
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If
            Return returnDs
        End Try

    End Function
#End Region

#Region "p_stock_tick_Add"
    '--------------------------------------------------
    '일봉차트데이터저장
    '--------------------------------------------------
    Public Function p_stock_tick_Add(ByVal query As String, _
                                    ByVal _STOCK_ID As String, _
                                    ByVal _STOCK_CODE As String, _
                                    ByVal _STOCK_DATE As String, _
                                    ByVal _S_TIME As String, _
                                    ByVal _E_TIME As String, _
                                    ByVal _C_PRICE As String, _
                                    ByVal _S_PRICE As String, _
                                    ByVal _H_PRICE As String, _
                                    ByVal _L_PRICE As String, _
                                    ByVal _RATE As String, _
                                    ByVal _POWER_RATE As String, _
                                    ByVal _BUY_VOLUME As String, _
                                    ByVal _SELL_VOLUME As String, _
                                    ByVal _BUY_TRADING_P As String, _
                                    ByVal _SELL_TRADING_P As String, _
                                    ByVal _LINE5 As String, _
                                    ByVal _LINE10 As String, _
                                    ByVal _LINE20 As String, _
                                    ByVal _LINE40 As String, _
                                    ByVal _LINE60 As String, _
                                    ByVal isConinueConnect As Boolean, _
            Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As Boolean


        If mySqlDbConn._sqlCon.State = ConnectionState.Closed Then
            mySqlDbConn.Open()
        End If

        With _arrParam
            .Clear()
            .Add("_QUERY", query)
            .Add("_STOCK_ID", _STOCK_ID)
            .Add("_STOCK_CODE", _STOCK_CODE)
            .Add("_STOCK_DATE", _STOCK_DATE)
            .Add("_S_TIME", _S_TIME)
            .Add("_E_TIME", _E_TIME)
            .Add("_C_PRICE", _C_PRICE)
            .Add("_S_PRICE", _S_PRICE)
            .Add("_H_PRICE", _H_PRICE)
            .Add("_L_PRICE", _L_PRICE)
            .Add("_RATE", _RATE)
            .Add("_POWER_RATE", _POWER_RATE)
            .Add("_BUY_VOLUME", _BUY_VOLUME)
            .Add("_SELL_VOLUME", _SELL_VOLUME)
            .Add("_BUY_TRADING_P", _BUY_TRADING_P)
            .Add("_SELL_TRADING_P", _SELL_TRADING_P)
            .Add("_LINE5", Val(_LINE5))
            .Add("_LINE10", Val(_LINE10))
            .Add("_LINE20", Val(_LINE20))
            .Add("_LINE40", Val(_LINE40))
            .Add("_LINE60", Val(_LINE60))
        End With

        Try
            mySqlDbConn.ExecuteNonQuery("p_stock_tick_Add", _arrParam)
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                If isConinueConnect <> True Then
                    mySqlDbConn.Close()
                End If
            End If
            Return True
        Catch ex As Exception
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If
            Return False
        End Try

        Return False
    End Function
#End Region

#Region "p_stock_tick_query"
    '--------------------------------------------------
    '일봉데이터 미비내역을 가져온다
    '--------------------------------------------------
    Public Function p_stock_tick_query(ByVal query As String, _
                                       ByVal _STOCK_ID As String, _
                                       ByVal _STOCK_DATE As String, _
                                       ByVal _STOCK_CODE As String, _
                                       ByVal _S_TIME As String, _
                                       Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        Dim returnDs As DataSet
        Dim cmd As String = ""

        Try
            If mySqlDbConn._sqlCon.State = ConnectionState.Closed Then
                mySqlDbConn.Open()
            End If


            With _arrParam
                .Clear()
                .Add("_QUERY", query)
                .Add("_STOCK_ID", _STOCK_ID)
                .Add("_STOCK_DATE", _STOCK_DATE)
                .Add("_STOCK_CODE", _STOCK_CODE)
                .Add("_S_TIME", _S_TIME)
            End With

            returnDs = mySqlDbConn.GetDataTableSp("p_stock_tick_query", _arrParam)

            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If

            Return returnDs
        Catch ex As Exception
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If
            Return Nothing
        End Try
    End Function
#End Region

#Region "p_stock_finance_query"
    '--------------------------------------------------
    '일봉데이터 미비내역을 가져온다
    '--------------------------------------------------
    Public Function p_stock_finance_query(ByVal query As String, ByVal stockCode As String, ByVal isConinueConnect As Boolean, Optional ByVal ds As DataSet = Nothing, Optional ByVal tableName As String = Nothing) As DataSet

        Dim returnDs As DataSet
        Dim cmd As String = ""

        If mySqlDbConn._sqlCon.State = ConnectionState.Closed Then
            mySqlDbConn.Open()
        End If


        With _arrParam
            .Clear()
            .Add("_QUERY", query)
            .Add("_STOCK_CODE", stockCode)
        End With

        returnDs = mySqlDbConn.GetDataTableSp("p_stock_finance_query", _arrParam)
        Try
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                If isConinueConnect <> True Then
                    mySqlDbConn.Close()
                End If
            End If
            Return returnDs
        Catch ex As Exception
            If mySqlDbConn._sqlCon.State = ConnectionState.Open Then
                mySqlDbConn.Close()
            End If
            Return returnDs
        End Try
    End Function
#End Region

End Class
