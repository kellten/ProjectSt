Public Module modDataTable
    Public _dtScreenNo As New DataTable
    Public _dtTradingScreenNo As New DataTable
    Public _dtOptKWFidScreenNo As New DataTable

    Public opt10059_Cal As String() = {"개인투자자", "외국인투자자", "기관계", "금융투자", "보험", "투신", "기타금융", "은행", _
                                   "연기금등", "사모펀드", "국가", "기타법인", "내외국인"}

    Public opt10059_GiganCal As String() = {"금융투자", "보험", "투신", "기타금융", "은행", "연기금등", "사모펀드", "국가"}

    Public opt10059_YouTongCal As String() = {"개인투자자", "외국인투자자", "기관계", "기타법인", "내외국인"}

End Module

