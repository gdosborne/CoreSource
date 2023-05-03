
Global Const KEY_BACK = &H8
Global Const KEY_TAB = &H9
Global Const KEY_RETURN = &HD
Global Const KEY_ESCAPE = &H1B
Global Const KEY_PRIOR = &H21
Global Const KEY_NEXT = &H22
Global Const KEY_END = &H23
Global Const KEY_HOME = &H24
Global Const KEY_LEFT = &H25
Global Const KEY_UP = &H26
Global Const KEY_RIGHT = &H27
Global Const KEY_DOWN = &H28
Global Const KEY_INSERT = &H2D
Global Const KEY_F1 = &H70
Global Const KEY_F2 = &H71
Global Const KEY_F3 = &H72
Global Const KEY_F4 = &H73
Global Const KEY_F5 = &H74
Global Const KEY_F6 = &H75
Global Const KEY_F7 = &H76
Global Const KEY_F8 = &H77
Global Const KEY_F9 = &H78
Global Const KEY_F10 = &H79
Global Const KEY_F11 = &H7A
Global Const KEY_F12 = &H7B
' Function Parameters
' MsgBox parameters
Global Const MB_OK = 0                 ' OK button only
Global Const MB_OKCANCEL = 1           ' OK and Cancel buttons
Global Const MB_ABORTRETRYIGNORE = 2   ' Abort, Retry, and Ignore buttons
Global Const MB_YESNOCANCEL = 3        ' Yes, No, and Cancel buttons
Global Const MB_YESNO = 4              ' Yes and No buttons
Global Const MB_RETRYCANCEL = 5        ' Retry and Cancel buttons

Global Const MB_ICONSTOP = 16          ' Critical message
Global Const MB_ICONQUESTION = 32      ' Warning query
Global Const MB_ICONEXCLAMATION = 48   ' Warning message
Global Const MB_ICONINFORMATION = 64   ' Information message

' Functions and Constants that enable interfacing with Amricare.ini

Declare Function GetWindow Lib "User" (ByVal hWnd%, ByVal wCmd%) As Integer
Declare Function GetWindowText Lib "User" (ByVal hWnd%, ByVal lpString$, ByVal nMaxCount%) As Integer
Declare Function GetWindowTextLength Lib "User" (ByVal hWnd%) As Integer
Declare Function SetFocusAPI Lib "User" Alias "SetFocus" (ByVal hWnd As Integer) As Integer
Declare Function ShowWindow Lib "User" (ByVal hWnd As Integer, ByVal nCmdShow As Integer) As Integer

Declare Function GetPrivateProfileInt Lib "Kernel" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal nDefault As Integer, ByVal lpFileName As String) As Integer
Declare Function GetPrivateProfileString Lib "Kernel" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
Declare Function GetProfileString Lib "Kernel" (ByVal lpAppName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer) As Integer
Declare Function WritePrivateProfileString Lib "Kernel" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer

Global Const GW_CHILD = 5
Global Const GW_HWNDFIRST = 0
Global Const GW_HWNDLAST = 1
Global Const GW_HWNDNEXT = 2
Global Const GW_HWNDPREV = 3
Global Const GW_HWNOWNER = 4
Global Const SW_SHOWNORMAL = 1
Global Const SW_SHOWMAXIMIZED = 3

' Window API Constants
Global Const HELP_QUIT = 2
Global Const HELP_INDEX = 3
Global Const HELP_HELPONHELP = 4
Global Const HELP_PARTIALKEY = &H105

Declare Function WinHelp Lib "User" (ByVal hWnd As Integer, ByVal lpHelpFile As String, ByVal wCommand As Integer, ByVal dwData As Any) As Integer

'Common Dialog Control
'Action Property
Global Const DLG_FONT = 4
Global Const DLG_PRINT = 5

'Fonts Dialog Flags
Global Const CF_SCREENFONTS = &H1&
Global Const CF_PRINTERFONTS = &H2&
Global Const CF_BOTH = &H3&
Global Const CF_SHOWHELP = &H4&
Global Const CF_APPLY = &H200&
Global Const CF_LIMITSIZE = &H2000&
Global Const CF_FIXEDPITCHONLY = &H4000&
Global Const CF_TTONLY = &H40000
Global Const CF_FORCEFONTEXIST = &H10000
Global Const CF_NOSIZESEL = &H200000

'Printer Dialog Flags
Global Const PD_PRINTSETUP = &H40&
Global Const PD_NOWARNING = &H80&
Global Const PD_SHOWHELP = &H800&

' User Global Defines
Global defFaceLimit As Integer
Global defPrintCertif As Integer
Global defZoomSize As Integer
Global defShowMono As Integer
            
Type ClientData
    ClientNumber As Integer
    FirstName As String * 20
    Initial As String * 1
    LastName As String * 20
    Address1 As String * 40
    Address2 As String * 40
    City As String * 20
    State As String * 2
    StateNum As Integer
    ZipCode As Long
    Phone1 As String * 14
    phone2 As String * 14
    Gender As String * 1
    BirthDate As Long
    Note As String * 60
End Type

Type AgtData
    FullName As String * 30
    Address1 As String * 30
    Address2 As String * 30
    City As String * 25
    State As String * 2
    ZipCode As Long
    HomePhone As String * 14
    OfficePhone As String * 25
    Office As Integer
    AgtState As Integer
    Number As Long
    Record As Long
    Sales As String * 1
End Type

Type AgentExt
    Record As Long
    FullName As String * 30
    Address1 As String * 30
    Address2 As String * 30
    City As String * 25
    State As String * 2
    ZipCode As Long
    OfficePhone As String * 14
    Sales As String * 1
End Type

Type InsuredData
    InsClientNumber As Integer
    InsNumber As Integer
    InsFName As String * 40
    insGender As String * 1
    InsBirthdate As Long
    InsRelation As String * 1
    InsTobacco As String * 1
    InsRating As Integer
    InsHeight As Integer
    InsWeight As Integer
End Type

Global ClientFile As String * 16
Global InsuredFile As String * 16
Global AgtInfo As AgentExt
Global ClientInfo As ClientData
Global ClientChanged As Integer
Global Record_Number As Long
Global NoAgent As Integer
Global NewAgent As Integer
Global Client_Data As Integer
Global No_Clients As Integer
Global MsgFile As String * 20
Global InsuredInfo As InsuredData
Global insd_key As Integer
Global ClientOnly As Integer

'declares for Life Illustrations in general
Global CommandString As String
Global CallFromMain As String
Global SaveMsgFile As String
Global MsgText As String
Global LS3FontDefault As String

Global AgeChanged As Integer
Global BirthChanged As Integer
Global SpouseChanged As Integer
Global ClientSave_ReturnCode As Integer
Global SpouseIllus_ReturnCode As Integer
Global FileNum As Integer
Global RetCode As Integer
Global CertifTable As Integer

'declares for LifeSavings forms and modules

Global Const LS3_MONTHLY_EXPENSE = 5
Global Const LS3_LOANINT = .05
Global Const LS3_GUAR_INT_RATE = .03

Type LSClientParameters
    ClientName As String * 40
    IssueAge As Integer
    DateOfBirth As Long
    SexCode As String * 1
    FaceAmt As Long
    TobaccoUse As Integer
    State As String * 2
    SubStd As Integer
    FlatRate As Currency
    YearsToPayFlat As Integer
    WPD As Integer
    GPO As Long
    COLA As Integer
    CurrIntRate As Single
    Mode As String * 1
    PlannedPrem As Single
    YearsToPay As Integer
    LumpSum As Single
    ChildRider As Integer
    InsuredRider(1 To 9) As Integer
    FutureChanges As Integer
    FutureWD As Integer
    IllusType As Integer
    HighAges(4) As Integer
    PrintPages As Integer
    ContactName As String * 40
    ContactPhone As String * 15
    TaxID As String * 11
End Type

Global LSClient(1) As LSClientParameters

Type LSChildRiderParm
    AgeYoungest As Integer
    FaceAmt As Integer
End Type

Global LSChildRider As LSChildRiderParm

Type LSInsuredRiderParm
    IssueAge As Integer
    SexCode As String * 1
    FaceAmt As Long
    TobaccoUse As Integer
    SubStd As Integer
    COLA As Integer
    StartYear As Integer
    EndYear As Integer
End Type

Global LSInsuredRider(1 To 9) As LSInsuredRiderParm

Type LSCalculatedPremiums
    PrinMin As Single
    ChildMin As Single
    InsdMin(1 To 9) As Single
    PrinTarg As Single
    ChildTarg As Single
    InsdTarg(1 To 9) As Single
    TotMin As Single
    TotAnnMin As Single
    TotTarg As Single
    GuideAnnual As Single
    GuideSingle As Single
    GuideSingleAddInsd As Single
End Type

Global CalcPrem As LSCalculatedPremiums

Type LSLedgerValues
    AnnualOutlay As Single
    SurrenderValue(2) As Single
    CashValue(2) As Single
    DeathBenefit(2) As Single
    WithdrawAmount As Single
    LoanAmount As Single
    LoanRepayAmount As Single
    LoanBalance(2) As Single
End Type

Global LSLedger(99) As LSLedgerValues

Type LSFutureChanges
    DB_Age As Integer
    DB_Amount As Single
    Prem_Age As Integer
    Prem_Amount As Single
    Int_Age As Integer
    Int_Amount As Single
End Type

Global LSFuture(1 To 5) As LSFutureChanges

Type LSFutureWithdrawals
    WD_Age As Integer
    WD_Amount As Single
    WD_Years As Integer
    Loan_Age As Integer
    Loan_Amount As Single
    Loan_Years As Integer
    Loan_Interest As Integer
    LoanPay_Age As Integer
    LoanPay_Amount As Single
    LoanPay_Years As Integer
End Type

Global LSFutureWD(1 To 5) As LSFutureWithdrawals

Type LSMortalityRates
    Prin_Base As Double             '1 band for Prin
    Prin_Sub As Double              '1 band for Prin
    Prin_GPO As Single
    Prin_WPD As Single
    Prin_CSO80 As Double
    Surr_Charge As Double
    Insured_Base(0 To 9) As Double  '0 for Prin term rider
    Insured_Sub(0 To 9) As Double   '0 for Prin term rider
    Insured_CSO80(0 To 9) As Double '0 for Prin term rider
End Type

Global LSMort(2, 99) As LSMortalityRates

Global Need_Ledger As Integer
Global Need_Guideline As Integer
Global Need_Target_CV As Integer
Global SpouseClient As Integer     'Client or Spouse illustration
Global LS_RateFiles(11) As Integer
Global AnnYr As Integer            'Ledger Year
Global AnnMo As Integer            'Ledger Month
Global GuarCurr As Integer         'Guaranteed or Current

Global LS_Prin_Band As Integer     'Current Principal DB band
Global LS_CSV(2) As Double         'Cash Surr Value (Guar, Curr, Mid)
Global LS_CV(2) As Double          'Cash Value (Guar, Curr, Mid)
Global LS_DB(2) As Double          'Client Death Benefit (Guar, Curr, Mid)
Global LS_DB_Minimum(2) As Double  'Client minimum death benefit (Guar, Curr, Mid)
Global LS_LoanBalance(2) As Double 'Current outstanding loan balance (Guar, Curr, Mid)
Global LS_Outlay As Double         'Premium or Loan Pay, per year
Global LS_Charges As Double        'Policy Fee, Premium Charge (per year)
Global LS_Withdraw As Double       'Cash Surrender (per year)
Global LS_LoanAmt As Double        'Cash Value Loan (per year)
Global LS_LoanPay As Double        'Loan repayment (per year)
Global LS_OneMonthInt(2) As Double 'Interest for one month (Guar, Curr, Mid)
Global LS_OneMonthLnInt As Double  'Interest for one month of loan _
Global LS_IRate(2) As Single       'Interest Rate (Guar, Curr, Mid)
Global LS_Modal_Prem As Single     'Modal Premium (per year)
Global LS_Monthly_Prem As Single   'Premium paid monthly (value or zero)
Global LS_MortRate As Single       'Current mortality rate
Global LS_Insureds_Mortcost As Single   'Cost of Insurance for Insureds (per year)
Global LS_Index_OneYear_Cost(1, 1) As Single  'one year cost for indices (10 or 20 years, guar or curr)
Global LS_Index_Numerator_SC(1, 1) As Single  'Surrender Cost numerator
Global LS_Index_Numerator_NP(1, 1) As Single  'Net Payment numerator
Global LS_Index_Denominator(1, 1) As Single      'Denominator for both SC and NP
Global LS_COLA_Client As Single           'COLA increase for client (Cumulative)
Global LS_COLA_Spouse As Single           'COLA increase for spouse rider (Cumulative)
Global LS_COLA_Insured(1 To 9) As Single  'COLA increase for insured rider (Cumulative)
Global LoanExist As Integer               'True if loan exists
Global SurrExist As Integer               'True if surrender exists
Global RepayExist As Integer              'True if repayment exists
Global IllusLapse(1, 1) As Integer        'for N/A on cost indices

Function Client_File (Action%, RecID%) As Integer

On Local Error GoTo Err_retrieve

    FileNum = FreeFile
    FIL$ = Dir$(ClientFile$)
  '  If FIL$ = "" Then
  '     MsgText = "File Not Found - 'ClientFile$'"
  '     MsgBox MsgText
  '     client_file = False
  '     Exit Function
  '  End If
    Select Case Action%
      Case 1                  ' retrieve data
        Open ClientFile$ For Random As #FileNum Len = Len(ClientInfo)
        Get #FileNum, RecID%, ClientInfo
        Client_File = True
        Close #FileNum
        Exit Function
      Case 3                   ' Mark client for removal
        Open ClientFile$ For Random As #FileNum Len = Len(ClientInfo)
        Get #FileNum, RecID%, ClientInfo
        ClientInfo.LastName = "*" & ClientInfo.LastName
        Put #FileNum, RecID%, ClientInfo
        Client_File = True
        Close #FileNum
        Exit Function
      Case 4                   ' Undelete all "deleted" clients
        Open ClientFile$ For Random As #FileNum Len = Len(ClientInfo)
        RecID% = LOF(FileNum) / Len(ClientInfo) + 1
        For i = 1 To RecID%
          Get #FileNum, i, ClientInfo
          If Left$(ClientInfo.LastName, 1) = "*" Then
             ClientInfo.LastName = Mid$(ClientInfo.LastName, 2, Len(ClientInfo.LastName) - 1)
             Put #FileNum, i, ClientInfo
          End If
        Next i
        Client_File = True
        Close #FileNum
        Exit Function
      Case 5                   'Save or add data
        Open ClientFile$ For Random As #FileNum Len = Len(ClientInfo)
        If RecID% = 0 Then
            RecID% = LOF(FileNum) / Len(ClientInfo) + 1
            ClientInfo.ClientNumber = RecID%
            ClientInfo.City = Space$(20)
        End If
        Put #FileNum, RecID%, ClientInfo
        Client_File = True
        Close #FileNum
        Exit Function
    End Select
Err_retrieve:
    Select Case Err
        Case 62
            MsgText = "Invalid Record Number on CLIENT file"
            MsgBox MsgText, 0, "LifeSavings"
        Case 63
            Resume Next
        Case Else
            MsgText = "Error Occured in retrieving client data  " + Str$(Err)
            MsgBox MsgText, 0, "LifeSavings"
    End Select
    Client_File = False
    Exit Function
End Function

Sub Fetch_Life_Birth (RetCode As Integer, tempbirth As String)

    RetCode = True
    mm$ = Left$(tempbirth$, 2)
    dd$ = Mid$(tempbirth$, 4, 2)
    yy$ = Right(tempbirth$, 4)
    If Val(mm$) < 1 Or Val(mm$) > 12 Or Val(dd$) < 1 Or Val(dd$) > 31 Or Val(yy$) > Year(Now) Or Val(yy$) < 1900 Then
       RetCode = False
       Exit Sub
    End If

    Select Case Val(mm$)
       Case 2
          If Val(yy$) Mod 4 <> 0 Or (Val(yy$) Mod 100 = 0 And Val(yy$) Mod 400 <> 0) Then
             If Val(dd$) > 28 Then
                RetCode = False
                Exit Sub
             End If
          Else
             If Val(dd$) > 29 Then
                RetCode = False
                Exit Sub
             End If
          End If
       Case 4, 6, 9, 11
          If Val(dd$) > 30 Then
             RetCode = False
             Exit Sub
          End If
       Case Else
    End Select

End Sub

Sub Format_Amount (amtstr As String, amtlen As Integer, amtval As Currency, dollar As String)

    amtstr$ = Space$(amtlen% + 1)
    TempStr$ = Format$(amtval, "##,###,##0.00")
    Mid$(amtstr$, amtlen% - Len(TempStr$) + 2, Len(TempStr$)) = TempStr$
    If dollar$ = "$" Then
       Mid$(amtstr$, amtlen% - Len(TempStr$) + 1, 1) = "$"
    End If

End Sub

Function GetAnyIniStr (section As String, key As String, file As String) As String
    
    Dim RetVal As String
    Dim AppName As String
    Dim Worked As Integer

    RetVal = String$(255, 0)
    Worked = GetPrivateProfileString(section, key, "", RetVal, Len(RetVal), file)
    If Worked = 0 Then
       GetAnyIniStr = ""
    Else
       GetAnyIniStr = Left$(RetVal, Worked)
    End If

End Function

Function insured_file (Action%, RecID%, InsNo%) As Integer

On Local Error GoTo Err_insureds
        FileNum = FreeFile
        FIL$ = Dir$(InsuredFile$)
        Select Case Action%
            Case 1                  ' retrieve data
                If FIL$ = "" Then
  '            MsgText = "File Not Found - " + InsuredFile$
  '            MSGBOX MsgText
                        insured_file = False
                        Exit Function
                End If
                insd_key% = ((RecID% - 1) * 12) + InsNo% + 1
                Open InsuredFile$ For Random As #FileNum Len = Len(InsuredInfo)
                Get #FileNum, insd_key%, InsuredInfo
                Select Case InsuredInfo.InsRelation
                        Case "P", "S", "C", "O"
                              If InsuredInfo.insGender = "M" Or InsuredInfo.insGender = "F" Then
                                insured_file = True
                                If InsuredInfo.InsBirthdate < -4000 Or InsuredInfo.InsBirthdate > 45000 Then
                                        MsgText = "Invalid birthdate for insured #" + Format$(InsNo% + 1, "##") + "(" + Trim$(InsuredInfo.InsFName) + ").  Birthdate set to 01/01/1900."
                                        MsgBox MsgText, , "LifeSavings"
                                        InsuredInfo.InsBirthdate = 1
                                End If
                                Select Case UCase$(InsuredInfo.InsTobacco)
                                    Case " "
                                        InsuredInfo.InsTobacco = "N"
                                    Case Else
                                        InsuredInfo.InsTobacco = UCase$(InsuredInfo.InsTobacco)
                                End Select
                              Else
                                insured_file = False
                              End If
                        Case Else
                              insured_file = False
                End Select
                Close #FileNum
                Exit Function
            Case 5                   'Save or add data
                insd_key% = ((RecID% - 1) * 12) + InsNo% + 1
                Open InsuredFile$ For Random As #FileNum Len = Len(InsuredInfo)
        '     IF recid% = 0 THEN
        '         recid% = LOF(FileNum) / LEN(InsuredInfo) + 1
                        InsuredInfo.InsClientNumber = RecID%
                        InsuredInfo.InsNumber = InsNo%
        '     END IF
                Put #FileNum, insd_key%, InsuredInfo
                insured_file = True
                Close #FileNum
                Exit Function
        End Select

Err_insureds:
        Select Case Err
                Case 62
                    MsgText = Str$(Err) & "Invalid Record Number on INSURED file"
                    MsgBox MsgText, 0, "LifeSavings"
                Case 63            'file does not yet exist
                Case Else
                    MsgText = "Insured_file"
                    MsgText = Str$(Err) & MsgText & """" & Error$(Err) & """" & "4"
                    MsgBox MsgText, , "LifeSavings"   ' Display message.
                    Close #FileNum
        End Select
        insured_file = False
        Close #FileNum
        Exit Function
End Function

Function PrintALine (start As Integer, onepage As String, oneline As String) As Integer

     NL$ = Chr$(13) & Chr$(10)
     PrintALine = True
     k% = InStr(start, onepage, NL$)
     If k% <> 0 Then
        oneline = Mid$(onepage, start, k% - start)
     Else
        PrintALine = False
        oneline = ""
     End If

End Function

Sub Set_Insd_Birthdt (Insured_Birthdate As String)

            
    If Mid$(Insured_Birthdate$, 1, 2) <> "  " Then
       Xmm$ = Mid$(Insured_Birthdate$, 1, 2)
       Xdd$ = Mid$(Insured_Birthdate$, 4, 2)
       Xyy$ = Mid$(Insured_Birthdate$, 7, 4)
    Else
       InsuredInfo.InsBirthdate = 0
       Exit Sub
    End If

    ent_date& = DateSerial(Val(Xyy$), Val(Xmm$), Val(Xdd$))
    If ent_date& <> InsuredInfo.InsBirthdate Then
       If ent_date& < 65380 Then
          curr_dt& = Now
          Call Year_Calc(ent_date&, curr_dt&, cage%, temp_dt&)
          InsuredInfo.InsBirthdate = ent_date&
       Else
          InsuredInfo.InsBirthdate = 0
       End If
    Else
     ' InsuredInfo.InsBirthdate = 0
    End If

End Sub

Function State_Verify (state_code As String) As Integer
On Local Error GoTo StateErr
    found% = False
    State_Verify = True
    FileNum = FreeFile
    If state_code$ = "  " Then Exit Function
    Open "ls3\STATEABR.TXT" For Input As FileNum
    Do While Not EOF(FileNum)
        tempabr$ = Input$(4, FileNum)
        stateabr$ = Left$(tempabr$, 2)
        StateNum% = Val(Mid$(tempabr$, 3, 2))
        If stateabr$ = state_code$ Then
          State_Verify = StateNum%
          Close FileNum
          Exit Function
        End If
    Loop
    Close FileNum
    State_Verify = True
    MsgText = "Invalid State Abbrevation" + Chr$(13) + "Use 2 character postal code"
    MsgBox MsgText, 0, "LifeSavings"
StateErr:
    Select Case Err
        Case 6, 62
            State_Verify = True
            MsgText = "Invalid State Abbrevation" + Chr$(13) + "Use 2 character postal code"
            MsgBox MsgText, 64, "LifeSavings"
            Exit Function
        Case 53, 75, 76
            State_Verify = True
            MsgText = "State Code File not found.  Make sure that STATEABR.TXT exists in the LS3 subdirectory, and try again."
            MsgBox MsgText, 64, "LifeSavings"
            Exit Function
        Case Else
            State_Verify = True
            MsgText = Err & " ERROR$" & " Global"
            MsgBox MsgText, 0, "LifeSavings"
            Exit Function
    End Select
End Function

Function WriteAnyIniStr (section As String, key As String, strvalue As String, file As String) As Integer
                            
    RetCode = WritePrivateProfileString(section, key, strvalue, file)
    WriteAnyIniStr = RetCode

End Function

Sub Year_Calc (lowdt&, highdt&, calcyr%, low_ymd&)
    If lowdt& <> 0 And highdt& <> 0 Then
       low_temp$ = Format$(lowdt&, "yyyymmdd")
       hi_temp$ = Format$(highdt&, "yyyymmdd")
       low_ymd& = Val(low_temp$)
       hi_ymd& = Val(hi_temp$)
       calcyr% = Int((hi_ymd& - low_ymd&) / 10000)
    End If
End Sub

