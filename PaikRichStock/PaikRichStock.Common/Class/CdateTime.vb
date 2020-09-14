
Public Class CDateTime
    '---------------------------------------------------------------
    ' 목적: 날짜 간에 시간 간격을 계산하여 값을 반환합니다
    ' 입력: DateDiff24(Interval,FromDate,ToDate)
    '       Interval; 날짜,시간,분 간격
    '       FromDate; 시작날짜시간(200604201200) : 20060424일 1200시
    '       Todate;   종료날짜시간
    ' 반환: 계산 값
    '---------------------------------------------------------------
    Public Shared Function DateDiff24(ByVal interval As DateInterval, ByVal dateTime1 As String, _
            ByVal dateTime2 As String) As Integer
        Dim date1, date2 As String
        Dim time1, time2 As String

        '시간값을 추가해준다.(DateTime형으로 변환하기 위해)
        dateTime1 += "000000"
        dateTime2 += "000000"

        date1 = Left(dateTime1, 8)
        time1 = Mid(dateTime1, 9, 6)
        date2 = Left(dateTime2, 8)
        time2 = Mid(dateTime2, 9, 6)

        If Left(time1, 4) = "2400" Then
            date1 = CDateTime.FormatDate(CDateTime.ToDateType(date1).AddDays(1).ToShortDateString)
            time1 = "000000"
        End If
        If Left(time2, 4) = "2400" Then
            date2 = CDateTime.FormatDate(CDateTime.ToDateType(date2).AddDays(1).ToShortDateString)
            time2 = "000000"
        End If

        Return DateDiff(interval, ToDateType(date1, time1), ToDateType(date2, time2))
    End Function

    '------------------------------------------------------------
    '목적 : 문자열을 날짜형식으로 변환한다.
    '입력 : strDate(날짜)
    '       strHour(시간)
    '------------------------------------------------------------
    Public Shared Function ToDateType(ByVal strDate As String, Optional ByVal strHour As String = Nothing) As DateTime
        Try
            strDate = CDateTime.FormatDate(strDate)
            Dim myDate As DateTime
            If strHour Is Nothing Then
                myDate = New DateTime(CInt(Mid(strDate, 1, 4)), CInt(Mid(strDate, 5, 2)), CInt(Right(strDate, 2)))
            Else
                strHour = CDateTime.FormatTime(strHour)
                myDate = New DateTime(CInt(Mid(strDate, 1, 4)), CInt(Mid(strDate, 5, 2)), CInt(Right(strDate, 2)), _
                        CInt(Left(strHour, 2)), CInt(Mid(strHour, 3, 2)), CInt(Right(strHour, 2)))
            End If
            Return myDate
        Catch ex As Exception
            ''System.Windows.Forms.MessageBox.Show(ex.ToString, "Error")
        End Try

    End Function

    '------------------------------------------------------------
    ' 목적: 1) 날짜형식은 문자열,문자열은 날짜형식으로 변환시켜
    '          반환 한다.
    '       2) vntMark 생략이면 문자열,아니면 날짜형식으로 변환
    ' 입력: FormatDate(strDate,Optional vntMask)
    '       strDate;        문자열
    '       vntMask;        Format 문자
    ' 반환: 문자열
    '------------------------------------------------------------
    Public Shared Function FormatDate(ByVal strDate As String, Optional ByVal vntMask As String = "") As String
        Dim i As Short
        Dim Text As String = ""

        If strDate Is Nothing Then
            Return ""
        End If
        If strDate.Trim = "" Then
            Return strDate
        End If

        If vntMask = "" Then
            For i = 1 To Len(strDate)
                If Mid(strDate, i, 1) >= "0" And Mid(strDate, i, 1) <= "9" Then
                    Text = Text & Mid(strDate, i, 1)
                End If
            Next
        Else
            For i = 1 To Len(strDate)
                If Mid(strDate, i, 1) >= "0" And Mid(strDate, i, 1) <= "9" Then
                    Text = Text & Mid(strDate, i, 1)
                    If Len(Text) = 4 Or Len(Text) = 7 Then
                        If vntMask = "년" Then
                            If Len(Text) = 4 Then
                                Text = Text & "년"
                            Else
                                Text = Text & "월"
                            End If
                        Else
                            Text = Text & vntMask
                        End If
                    End If
                End If
            Next
            If vntMask = "년" Then
                Text = Replace(Text, "년", "년 ")
                Text = Replace(Text, "월", "월 ")
                If Len(strDate) = 8 Then
                    Text += "일"
                End If
            End If
        End If

        FormatDate = Text
    End Function

    '------------------------------------------------------------
    ' 목적: 1) 시간형식은 문자열,문자열은 시간형식으로 변환시켜
    '          반환 한다.
    '       2) vntMark 생략이면 문자열,아니면 시간형식으로 변환
    ' 입력: FormatDate(strTime,Optional vntMask)
    '       strTime;        문자열
    '       vntMask;        Foramt 문자
    ' 반환: 문자열
    '------------------------------------------------------------
    Public Shared Function FormatTime(ByVal strTime As String, Optional ByVal vntMask As Object = Nothing) As String
        Dim i As Integer, Text As String = ""

        If strTime Is Nothing Then
            Return ""
        End If

        If strTime.Trim = "" Then
            Return strTime
        End If

        If InStr(strTime, "오전") > 0 Then
            strTime = Trim(Replace(strTime, "오전", ""))
            strTime = Mid(strTime, 1, InStr(strTime, ":") - 1) + Mid(strTime, InStr(strTime, ":") + 1, strTime.Length - InStr(strTime, ":"))
        ElseIf InStr(strTime, "오후") > 0 Then
            strTime = Trim(Replace(strTime, "오후", ""))
            strTime = CStr(Val(Mid(strTime, 1, InStr(strTime, ":") - 1)) + 12) + Mid(strTime, InStr(strTime, ":") + 1, strTime.Length - InStr(strTime, ":"))
        End If

        If IsNothing(vntMask) Then
            If strTime.Length = 3 Then
                strTime = "0" & strTime
            End If
            For i = 1 To Len(strTime)
                If Mid(strTime, i, 1) >= "0" And Mid(strTime, i, 1) <= "9" Then
                    Text = Text & Mid(strTime, i, 1)
                End If
            Next
        Else
            For i = 1 To Len(strTime)
                If Mid(strTime, i, 1) >= "0" And Mid(strTime, i, 1) <= "9" Then
                    If Len(Text) = 2 Or Len(Text) = 5 Or Len(Text) = 8 Then
                        Text = Text & vntMask
                    End If
                    Text = Text & Mid(strTime, i, 1)
                End If
            Next
        End If

        FormatTime = Text
    End Function

    '----------------------------------------------------------------------------------------
    ' 목적: 특정 날짜의 요일을 구하여 요일명을 반환한다.
    ' 입력: WeekDayName(strDate,Optional ImeMode)
    '       strDate;        날짜형,문자열
    '       blnAbbreviate;  True(축약된 요일: 화요일 > 화),False(완전 요일 : 화요일 > 화요일)
    '       blnKorean : True(화요일), False(Tuesday)
    ' 반환: 정상이면 요일명,아니면 vbNullString
    '----------------------------------------------------------------------------------------
    Public Shared Function WeekDayNames(ByVal strDate As String, _
            Optional ByVal blnAbbreviate As Boolean = False, Optional ByVal blnKorean As Boolean = True) As String
        Dim tmpDate As String = ""
        Dim dayName As String = ""

        If Not IsDate(strDate) Then
            tmpDate = CDateTime.FormatDate(strDate, "/")
            If Not IsDate(tmpDate) Then Return ""
        Else
            tmpDate = strDate
        End If

        If blnKorean = True Then
            dayName = WeekdayName(Weekday(CDate(tmpDate)), blnAbbreviate)
        Else
            Select Case Weekday(CDate(tmpDate))
                Case FirstDayOfWeek.Sunday : dayName = "Sunday"
                Case FirstDayOfWeek.Monday : dayName = "Monday"
                Case FirstDayOfWeek.Tuesday : dayName = "Tuesday"
                Case FirstDayOfWeek.Wednesday : dayName = "Wednesday"
                Case FirstDayOfWeek.Thursday : dayName = "Thursday"
                Case FirstDayOfWeek.Friday : dayName = "Friday"
                Case FirstDayOfWeek.Saturday : dayName = "Saturday"
            End Select
            If blnAbbreviate = True Then
                dayName = Left(dayName, 3)
            End If
        End If

        Return dayName
    End Function

    '------------------------------------------------------------
    ' 목적: 식을 시간으로 변환할 수 있는지 나타내는 Boolean
    '       값을 반환합니다.
    ' 입력: IsTime(MyTime)
    '       MyTime;     시간,형식(HH:MM:SS or HH:MM)
    ' 반환: Boolean
    '------------------------------------------------------------
    ''' <summary>
    ''' 입력문자열이 시간형이 맞는지 체크한다.
    ''' </summary>
    ''' <param name="MyTime">hh:mm:ss or hh:mm</param>
    ''' <param name="blnHr24">True이면 24:00을 인정 False이면 23:59까지만 인정</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Shared Function IsTime(ByVal MyTime As String, Optional ByVal blnHr24 As Boolean = False) As Boolean
        If InStr(MyTime, ":") > 0 Then
            If Len(MyTime) = 5 Then
                MyTime = MyTime & ":00"
            End If

            If Left(MyTime, 5) < "00:00" Then
                Exit Function
            End If

            If blnHr24 Then
                If Left(MyTime, 5) > "24:00" Then
                    Exit Function
                End If
            Else
                If Left(MyTime, 5) > "23:59" Then
                    Exit Function
                End If
            End If

            If Mid(MyTime, 4, 2) < "00" Or Mid(MyTime, 4, 2) > "59" Then
                Exit Function
            End If

            If Mid(MyTime, 7) < "00" Or Mid(MyTime, 7) > "59" Then
                Exit Function
            End If

            IsTime = True
        Else
            If Len(MyTime) = 4 Then
                MyTime = MyTime & "00"
            End If

            If Left(MyTime, 4) < "0000" Then
                Exit Function
            End If

            If blnHr24 Then
                If Left(MyTime, 4) > "2400" Then
                    Exit Function
                End If
            Else
                If Left(MyTime, 4) > "2359" Then
                    Exit Function
                End If
            End If

            If Mid(MyTime, 3, 2) < "00" Or Mid(MyTime, 3, 2) > "59" Then
                Exit Function
            End If

            If Mid(MyTime, 5) < "00" Or Mid(MyTime, 5) > "59" Then
                Exit Function
            End If

            IsTime = True
        End If

    End Function

    '------------------------------------------------------------
    ' 목적: 식을 날짜(yyyymmdd)로 변환할 수 있는지 나타내는 Boolean
    '       값을 반환합니다.
    ' 입력: IsDateF(MyDate)
    '       MyDate;     날짜,형식(yyyy/mm/dd or yyyymmdd)
    ' 반환: Boolean
    '------------------------------------------------------------
    ''' <summary>
    ''' 입력문자열이 날짜형식으로 변환가능한지 체크한다.
    ''' </summary>
    ''' <param name="dateType">yyyy/MM/dd or yyyyMMdd</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Shared Function IsDateF(ByVal dateType As String) As Boolean
        Dim ix As Integer, convertDate As String = ""

        For ix = 1 To Len(dateType)
            If Mid(dateType, ix, 1) >= "0" And Mid(dateType, ix, 1) <= "9" Then
                convertDate = convertDate & Mid(dateType, ix, 1)
                If Len(convertDate) = 4 Or Len(convertDate) = 7 Then
                    convertDate = convertDate & "-"
                End If
            End If
        Next

        If Len(convertDate) <> 10 Then
            Exit Function
        End If

        If Not IsDate(convertDate) Then
            Exit Function
        End If

        Return True
    End Function

    '-------------------------------------------------------------------------------
    '두 일자사이를 월단위로 계산해서 해당월의 시작일자와 종료일자를 배열로 저장한다.
    ' 예) 200607, 200608  => (20060701, 20060731) (20060801, 20060831)
    '     20060710, 20060825 => (20060710, 20060731) (20060801, 20060825)
    '-------------------------------------------------------------------------------
    Public Shared Function GetMonthBetweenDate(ByVal fromDate As String, ByVal toDate As String) As Object
        Dim monthDiff As Integer, Ix As Integer
        Dim fDate, tDate, tmpFDate, tmpTDate As String
        Dim firstDate, lastDate As String
        Dim arrDate As Object

        tmpFDate = fromDate
        tmpTDate = toDate
        If Len(fromDate) = 6 Then
            tmpFDate = tmpFDate & "01"
        End If
        If Len(toDate) = 6 Then
            tmpTDate = tmpTDate & "01"
        End If

        monthDiff = DateDiff(DateInterval.Month, CDateTime.ToDateType(tmpFDate), _
                CDateTime.ToDateType(tmpTDate))

        ReDim arrDate(monthDiff, 1)

        If Len(fromDate) = 6 Then
            fromDate = fromDate & "01"
        End If
        fDate = fromDate
        tmpFDate = Mid(fDate, 1, 6) + "01"
        For Ix = 0 To monthDiff
            '작업월의 기간중 한달이상이 차이 날경우
            '질의한 달의 마지막 일을 구한다.
            firstDate = CDateTime.FormatDate(DateAdd(DateInterval.Month, 1, CDateTime.ToDateType(tmpFDate)).ToShortDateString)
            lastDate = CDateTime.FormatDate(DateAdd(DateInterval.Day, -1, _
                    CDateTime.ToDateType(firstDate)).ToShortDateString())
            tDate = lastDate

            '질의할 기간이 동일한 월일경우 마지막일은 입력한 마지막일자로 처리하며
            '단, 월로 입력이 들어온 경우 마지막일수를 구해서 질의의 마지막 일자를 구한다.
            If Ix = monthDiff Then
                arrDate(Ix, 0) = fDate
                If Len(toDate) = 6 Then
                    toDate = lastDate
                End If
                arrDate(Ix, 1) = toDate
            Else
                arrDate(Ix, 0) = fDate
                arrDate(Ix, 1) = tDate
            End If
            '시작일은 항상 다음달 1일로 한다.
            fDate = firstDate
            tmpFDate = fDate
        Next

        Return arrDate
    End Function

#Region "양력으로부터 음력날짜를 구한다.(ConvertLunarFromSolar)"
    '입력: 8자리 날짜문자열(yyyyMMdd)
    Public Shared Function ConvertLunarFromSolar(ByVal strDate As String) As String
        Return ConvertLunarCalendarFromSolarCalendar(CDateTime.ToDateType(CDateTime.FormatDate(strDate)))
    End Function

    '입력: 양력 날짜(Date)
    Public Shared Function ConvertLunarFromSolar(ByVal solarDate As DateTime) As String
        Return ConvertLunarCalendarFromSolarCalendar(solarDate)
    End Function

    Private Shared Function ConvertLunarCalendarFromSolarCalendar(ByVal solarDate As DateTime) As String
        Dim calendar As System.Globalization.KoreanLunisolarCalendar = New System.Globalization.KoreanLunisolarCalendar()

        Dim year As Integer = calendar.GetYear(solarDate)  ' 음력 년
        Dim month As Integer = calendar.GetMonth(solarDate) ' 음력 월
        Dim day As Integer = calendar.GetDayOfMonth(solarDate) ' 음력 일

        ' 윤달이 끼어 있으면..
        If (calendar.GetMonthsInYear(year) > 12) Then
            Dim leapMonth As Integer = calendar.GetLeapMonth(year)

            ' 윤달보다 크거나 같으면 달수가 1씩 더해지므로 이를 재조정 함.
            If (month >= leapMonth) Then
                month -= 1
            End If

        End If

        Return year.ToString + Format(month, "00") + Format(day, "00")
    End Function

#End Region

End Class
