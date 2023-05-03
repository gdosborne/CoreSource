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

' Functions and Constants that enable interfacing with AmriCare

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
Global Const RoundingFactor = .005
Global defPrintFont As String
Global defFontSiz As Single
Global defFontBold As Integer
Global defFontItal As Integer
            
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
    Zipcode As Long
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
    Zipcode As Long
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
    OfficePhone As String * 14
    Sales As String * 1
End Type

Type InsuredData
    InsClientNumber As Integer
    InsNumber As Integer
    InsFName As String * 40
    InsGender As String * 1
    InsBirthDate As Long
    InsRelation As String * 1
    insTobacco As String * 1
    insRating As Integer
    InsHeight As Integer
    InsWeight As Integer
End Type

Global Extended As Integer
Global ClientFile As String * 16
Global InsuredFile As String * 16
Global formcode As String * 8
Global AgtInfo As AgentExt
Global ClientInfo As ClientData
Global ClientChanged As Integer
Global Record_Number As Long
Global NoAgent As Integer
Global NewAgent As Integer
Global client_data As Integer
Global No_Clients As Integer
Global msgfile As String * 20
Global InsuredInfo As InsuredData
Global insured_data As Integer
Global insd_key As Integer
Global BDSTR As String
Global ReportType As Integer
Global InsuredArray() As Integer
Global desc As String * 30
Global file_prefix As String * 7
Global NL As String * 2
Global PrinterSw As Integer
Global tot_prem()  As Currency
Global strArray() As String
Global ClientOnly As Integer
Global savPrintFont As Variant
Global savFontSize As Single
Global savFontBold As Single
Global savFontItalic As Single
Global defSpacing As Integer

'declares for Life Illustrations in general
Global LifeIllusType As Integer
Global PrintIllusPage(5) As String
Global CommandString As String
Global CallFromMain As String

'declares for Tailored Term forms and modules

Type TTIllusParm
    ClientName As String * 40
    IssueAge As Integer
    SexCode As String * 1
    FaceAmt As Long
    Smoker As String * 1
    UnisexRate As String * 1
    Rate As String * 3
    State As String * 2
    IllusType As Integer
    PrintPages As Integer
    ContactName As String * 40
    ContactPhone As String * 15
End Type

Global TTInfo As TTIllusParm

Global NPINDEX(2, 2) As Double
Global TTRATE(2, 1) As Currency
Global SPRATE(3, 2) As Currency
Global GUARMAX(14) As Currency
Global TTPREM(2, 1) As Currency
Global SPPREM(3, 2) As Currency
Global GUARPREM(14) As Currency

Global POLFEE As Currency
Global LIFEPAGE As Integer
Global LIFELINE As Integer
Global ONERATE As Integer
Global TTermPage As Integer
Global AgeChanged As Integer
Global BirthChanged As Integer
Global SpouseChanged As Integer
Global SaveMsgFile As String
Global ClientSave_ReturnCode As Integer
Global SpouseIllus_ReturnCode As Integer

'declares for Life Savings forms and modules

Global Const LS_LoanRate = .06
         
Type LSClientParameters
    ClientName As String * 40
    IssueAge As Integer
    SexCode As String * 1
    FaceAmt As Long
    PlanCode As Integer
    State As String * 2
    Option As Integer
    SubStd As Integer
    WPD As Integer
    GPO As Long
    COLA As Integer
    AddFace As Long
    AddFaceStart As Integer
    AddFaceEnd As Integer
    CurrRate As Single
    Mode As String * 1
    PlannedPrem As Single
    YearsToPay As Integer
    LumpSum As Single
    SpouseRider As Integer
    ChildRider As Integer
    InsuredRider(1 To 9) As Integer
    FutureChanges As Integer
    FutureWD As Integer
    IllusType As Integer
    HighAges(4) As Integer
    PrintPages As Integer
    ContactName As String * 40
    ContactPhone As String * 15
End Type

Global LSClient(1) As LSClientParameters

Type LSSpouseRiderParm
    IssueAge As Integer
    FaceAmt As Long
    PlanCode As Integer
    SubStd As Integer
    COLA As Integer
    RemoveYear As Integer
End Type

Global LSSpouseRider As LSSpouseRiderParm

Type LSChildRiderParm
    AgeYoungest As Integer
    FaceAmt As Integer
End Type

Global LSChildRider As LSChildRiderParm

Type LSInsuredRiderParm
    IssueAge As Integer
    SexCode As String * 1
    FaceAmt As Long
    PlanCode As Integer
    SubStd As Integer
    COLA As Integer
    StartYear As Integer
    EndYear As Integer
End Type

Global LSInsuredRider(1 To 9) As LSInsuredRiderParm

Type LSCalculatedPremiums
    PrinMin As Single
    SpouseMin As Single
    ChildMin As Single
    InsdMin(1 To 9) As Single
    PrinTarg As Single
    SpouseTarg As Single
    ChildTarg As Single
    InsdTarg(1 To 9) As Single
    TotMin As Single
    TotTarg As Single
    GuideAnnual As Single
    GuideSingle As Single
    GuideSingleAddInsd As Single
End Type

Global CalcPrem As LSCalculatedPremiums

Type LSLedgerValues
    AnnualOutlay As Single
    SurrenderValue(1) As Single
    CashValue(1) As Single
    DeathBenefit(1) As Single
    WithdrawAmount As Single
    LoanAmount As Single
    LoanRepayAmount As Single
    LoanBalance As Single
End Type

Global LSLedger(99) As LSLedgerValues

Type LSFutureChanges
    DB_Age As Integer
    DB_Amount As Single
    Prem_Age As Integer
    Prem_Amount As Single
    Int_Age As Integer
    Int_Amount As Single
    Opt_Age As Integer
    Opt_Type As Integer
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
    Prin_Base(1 To 4) As Single     '1 to 4 bands for Prin
    Prin_Sub(1 To 4) As Single      '1 to 4 bands for Prin
    Prin_GPO As Single
    Prin_WPD As Single
    Prin_CSO80 As Single
    Spouse_Base As Single
    Spouse_Sub As Single
    Spouse_CSO80 As Single
    Insured_Base(0 To 9) As Single  '0 for Prin term rider
    Insured_Sub(0 To 9) As Single   '0 for Prin term rider
    Insured_CSO80(0 To 9) As Single '0 for Prin term rider
End Type

Global LSMort(1, 99) As LSMortalityRates

Global Need_Ledger As Integer
Global Need_Guideline As Integer
Global Need_Target_CV As Integer
Global SpouseClient As Integer     'Client or Spouse illustration
Global LS_RateFiles(11) As Integer
Global AnnYr As Integer            'Ledger Year
Global AnnMo As Integer            'Ledger Month
Global GuarCurr As Integer         'Guaranteed or Current

Global LS_Prin_Band As Integer     'Current Principal DB band
Global LS_DBOption As Integer      'Death Benefit Option
Global LS_CSV(1) As Double         'Cash Surr Value (Guar or Curr)
Global LS_CV(1) As Double          'Cash Value (Guar or Curr)
Global LS_DB(1) As Double          'Client Death Benefit (Guar or Curr)
Global LS_DB_Minimum(1) As Double  'Client minimum death benefit (Guar or Curr)
Global LS_CV_Opt2_Start(1) As Double 'Client cash valu up to the time Option 2 started
Global LS_LoanBal(1) As Double     'Loan Balance (Guar or Curr)
Global LS_Outlay As Double         'Premium or Loan Pay, per year
Global LS_Charges As Double        'Policy Fee, Premium Charge (per year)
Global LS_Withdraw As Double       'Cash Surrender (per year)
Global LS_LoanAmt As Double        'Cash Value Loan (per year)
Global LS_LoanPay As Double        'Loan repayment (per year)
Global LS_LoanBalance As Double    'Current outstanding loan balance
Global LS_IRate(1) As Single       'Interest Rate (Guar or Curr)
Global LS_Modal_Prem As Single     'Modal Premium (per year)
Global LS_Monthly_Prem As Single   'Premium paid monthly (value or zero)
Global LS_MortRate As Single       'Current mortality rate
Global LS_Insureds_Mortcost As Single   'Cost of Insurance for Insureds (per year)
Global LS_Index_OneYear_Cost(1, 1) As Single  'one year cost for indices (10 or 20 years, guar or curr)
Global LS_Index_Numerator_SC(1, 1) As Single  'Surrender Cost numerator
Global LS_Index_Numerator_NP(1, 1) As Single  'Net Payment numerator
Global LS_Index_Denominator(1) As Single      'Denominator for both SC and NP
Global LS_COLA_Client As Single           'COLA increase for client (Cumulative)
Global LS_COLA_Spouse As Single           'COLA increase for spouse rider (Cumulative)
Global LS_COLA_Insured(1 To 9) As Single  'COLA increase for insured rider (Cumulative)
Global LS_Rider_Client As Single    'Rider Death Benefit for Client, by year

Function WriteAnyIniStr (section As String, key As String, strvalue As String, file As String) As Integer
     
    i% = WritePrivateProfileString(section, key, strvalue, file)
    WriteAnyIniStr = i%

End Function

Function client_file (action%, recid%) As Integer

On Local Error GoTo Err_retrieve

    filnum% = FreeFile
    FIL$ = Dir$(ClientFile$)
  '  If FIL$ = "" Then
  '     msg$ = "File Not Found - 'ClientFile$'"
  '     MsgBox msg$
  '     client_file = False
  '     Exit Function
  '  End If
    Select Case action%
      Case 1                  ' retrieve data
        Open ClientFile$ For Random As #filnum% Len = Len(ClientInfo)
        Get #filnum%, recid%, ClientInfo
        client_file = True
        Close #filnum%
        Exit Function
      Case 5                   'Save or add data
        Open ClientFile$ For Random As #filnum% Len = Len(ClientInfo)
        If recid% = 0 Then
            recid% = LOF(filnum%) / Len(ClientInfo) + 1
            ClientInfo.ClientNumber = recid%
            ClientInfo.City = Space$(20)
        End If
        Put #filnum%, recid%, ClientInfo
        client_file = True
        Close #filnum%
        Exit Function
    End Select
Err_retrieve:
    Select Case Err
        Case 62
            msg$ = "Invalid Record Number on CLIENT file"
            MsgBox msg$, 0
        Case 63
            Resume Next
        Case Else
            msg$ = "Error Occured in retrieving client data  " + Str$(Err)
            MsgBox msg$, 0
    End Select
    client_file = False
    Exit Function
End Function

Sub Format_Amount (amtstr As String, amtlen As Integer, amtval As Currency, dollar As String)

    amtstr$ = Space$(amtlen% + 1)
    tempstr$ = Format$(amtval, "##,###,##0.00")
    Mid$(amtstr$, amtlen% - Len(tempstr$) + 2, Len(tempstr$)) = tempstr$
    If dollar$ = "$" Then
       Mid$(amtstr$, amtlen% - Len(tempstr$) + 1, 1) = "$"
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

Sub Illus_Close_Text (Illpage As String, section As Integer, phone As String, typeill As Integer)

    NL$ = Chr$(13) & Chr$(10)  'carriage return, line feed
    
    filnum% = FreeFile
    Select Case typeill
      Case 1
        Open "tt\tt10clos.txt" For Input As #filnum%
      Case 2
        Open "tt\tt15clos.txt" For Input As #filnum%
      Case Else
    End Select

    'Find the specified section

    Do Until EOF(filnum%)
       Line Input #filnum%, oneline$
       If Left$(oneline$, 1) = ">" Then
          If Val(Mid$(oneline$, 2)) = section% Then
             Exit Do
          End If
       End If
    Loop
   
    If typeill = 1 Then
       Do While Not EOF(filnum%)
          Line Input #filnum%, oneline$
          If Left$(oneline$, 1) = ">" Then Exit Do
          If Mid$(oneline$, 44, 3) = "XXX" Then GoSub Insert_Phone
          Illpage$ = Illpage$ & oneline$ & NL$
       Loop
    Else
       Do While Not EOF(filnum%)
          Line Input #filnum%, oneline$
          If Left$(oneline$, 1) = ">" Then Exit Do
          If Mid$(oneline$, 44, 3) = "XXX" Then GoSub Insert_Phone
          Illpage$ = Illpage$ & oneline$ & NL$
       Loop
    End If
    Close #filnum%
    Exit Sub

Insert_Phone:

    Mid$(oneline$, 44, 3) = Mid$(phone$, 1, 3)
    Mid$(oneline$, 48, 3) = Mid$(phone$, 4, 3)
    Mid$(oneline$, 52, 4) = Mid$(phone$, 7, 4)
    If Val(Mid$(phone$, 11, 5)) <> 0 Then
       oneline$ = oneline$ & Space$(11)
       Mid$(oneline$, 56, 13) = ", extension "
       i% = 1
       Do While (i% < 6 And Mid$(phone$, 10 + i%, 1) <> "_")
          oneline$ = oneline$ & Mid$(phone$, 10 + i%, 1)
          i% = i% + 1
       Loop
       oneline$ = oneline$ & "." & NL$
    End If
    Return

End Sub

Sub Illus_Head (Illpage As String, typeill As Integer)

     NL$ = Chr$(13) & Chr$(10) 'carriage return, line feed
     oneline$ = NL$ & Space$(20) & "A TAILORED TERM LIFE INSURANCE PROPOSAL" & NL$
     Illpage$ = Illpage$ & oneline$
     If typeill = 2 Then Illpage$ = Illpage$ & NL$
     oneline$ = Space$((76 - Len(RTrim$(TTInfo.ClientName))) / 2) & "for " & RTrim$(TTInfo.ClientName) & NL$
     Illpage$ = Illpage$ & oneline$
     If typeill = 2 Then Illpage$ = Illpage$ & NL$
     oneline$ = Space$(16) & "STATEMENT OF POLICY COST AND BENEFIT INFORMATION" & NL$
     Illpage$ = Illpage$ & oneline$
     If typeill = 2 Then Illpage$ = Illpage$ & NL$
     oneline$ = "Prepared " & Format$(Now, "MMMM") & " " & Format$(Now, "DD") & ", " & Format$(Now, "YYYY") & " by " & RTrim$(TTInfo.ContactName) & NL$
     oneline$ = Space$((80 - Len(RTrim$(oneline$))) / 2) & oneline$
     Illpage$ = Illpage$ & oneline$
     oneline$ = Space$(11) & "American Republic Insurance Company, Des Moines, IA  50334" & NL$
     Illpage$ = Illpage$ & oneline$
     DESCRPT$ = NL$ & "     "
     If TTInfo.UnisexRate = "Y" Then
        DESCRPT$ = DESCRPT$ & "Person"
     Else
        If TTInfo.SexCode = "M" Then
           DESCRPT$ = DESCRPT$ & "Male"
        Else
           DESCRPT$ = DESCRPT$ & "Female"
        End If
     End If
     DESCRPT$ = DESCRPT$ & " age" & Str$(TTInfo.IssueAge)
     If TTInfo.Smoker = "N" Then
        DESCRPT$ = DESCRPT$ & " NonSmoker"
        Else DESCRPT$ = DESCRPT$ & " Smoker "
     End If
     oneline$ = DESCRPT$ & Space$(48 - Len(RTrim$(DESCRPT$))) & "Face Amount: "
     tempamt@ = TTInfo.FaceAmt
     Call Format_Amount(amtstr$, 13, tempamt@, "$")
     oneline$ = oneline$ & Space$(3) & amtstr$ & NL$
     Illpage$ = Illpage$ & oneline$

End Sub

Function insured_file (action%, recid%, insno%) As Integer

On Local Error GoTo Err_insureds
        filnum% = FreeFile
        FIL$ = Dir$(InsuredFile$)
        Select Case action%
            Case 1                  ' retrieve data
                If FIL$ = "" Then
  '            msg$ = "File Not Found - " + InsuredFile$
  '            MSGBOX msg$
                        insured_file = False
                        Exit Function
                End If
                insd_key% = ((recid% - 1) * 12) + insno% + 1
                Open InsuredFile$ For Random As #filnum% Len = Len(InsuredInfo)
                Get #filnum%, insd_key%, InsuredInfo
                Select Case InsuredInfo.InsRelation
                        Case "P", "S", "C", "O"
                              If InsuredInfo.InsGender = "M" Or InsuredInfo.InsGender = "F" Then
                                insured_file = True
                                If InsuredInfo.InsBirthDate < -4000 Or InsuredInfo.InsBirthDate > 45000 Then
                                        msg$ = "Invalid birthdate for insured #" + Format$(insno% + 1, "##") + "(" + Trim$(InsuredInfo.InsFName) + ").  Birthdate set to 01/01/1900."
                                        MsgBox msg$
                                        InsuredInfo.InsBirthDate = 1
                                End If
                                Select Case UCase$(InsuredInfo.insTobacco)
                                    Case " "
                                        InsuredInfo.insTobacco = "N"
                                    Case Else
                                        InsuredInfo.insTobacco = UCase$(InsuredInfo.insTobacco)
                                End Select
                              Else
                                insured_file = False
                              End If
                        Case Else
                              insured_file = False
                End Select
                Close #filnum%
                Exit Function
            Case 5                   'Save or add data
                insd_key% = ((recid% - 1) * 12) + insno% + 1
                Open InsuredFile$ For Random As #filnum% Len = Len(InsuredInfo)
        '     IF recid% = 0 THEN
        '         recid% = LOF(FilNum%) / LEN(InsuredInfo) + 1
                        InsuredInfo.InsClientNumber = recid%
                        InsuredInfo.InsNumber = insno%
        '     END IF
                Put #filnum%, insd_key%, InsuredInfo
                insured_file = True
                Close #filnum%
                Exit Function
        End Select

Err_insureds:
        Select Case Err
                Case 62
                    msg$ = Str$(Err) & "Invalid Record Number on INSURED file"
                    MsgBox msg$, 0
                Case 63            'file does not yet exist
                Case Else
                    msg$ = "Insured_file"
                    msg$ = Str$(Err) & msg$ & """" & Error$(Err) & """" & "4"
                    MsgBox msg$  ' Display message.
                    Close #filnum%
        End Select
        insured_file = False
        Close #filnum%
        Exit Function
End Function

Sub Ledger_Allrates (Illpage As String, typeill As Integer)

     NL$ = Chr$(13) & Chr$(10) 'carriage return, line feed
     oneline$ = ""

End Sub

Sub Ledger_Onerate (Illpage As String, typeill As Integer)

     NL$ = Chr$(13) & Chr$(10) 'carriage return, line feed
     oneline$ = ""
End Sub

Function PrintALine (start As Integer, onepage As String, oneline As String) As Integer

     NL$ = Chr$(13) & Chr$(10)
     PrintALine = True
     K% = InStr(start, onepage, NL$)
     If K% <> 0 Then
        oneline = Mid$(onepage, start, K% - start)
     Else
        PrintALine = False
        oneline = ""
     End If

End Function

Sub set_insd_birthdt (IBIRTH As String)

        If Mid$(IBIRTH$, 1, 2) <> "  " Then
              Xmm$ = Mid$(IBIRTH$, 1, 2)
              Xdd$ = Mid$(IBIRTH$, 4, 2)
              Xyy$ = Mid$(IBIRTH$, 7, 4)
        Else
              InsuredInfo.InsBirthDate = 0
              Exit Sub
        End If

        ent_date& = DateSerial(Val(Xyy$), Val(Xmm$), Val(Xdd$))
        If ent_date& <> InsuredInfo.InsBirthDate Then
                If ent_date& < 65380 Then
                        curr_dt& = Now
                        Call Year_Calc(ent_date&, curr_dt&, Cage%, temp_dt&)
                        InsuredInfo.InsBirthDate = ent_date&
                Else
                        InsuredInfo.InsBirthDate = 0
                End If
        Else
               ' InsuredInfo.InsBirthdate = 0
        End If

End Sub

Function State_Verify (state_code As String) As Integer
On Local Error GoTo StateErr
    found% = False
    State_Verify = True
    FileNum% = FreeFile
    If state_code$ = "  " Then Exit Function
    Open "life/stateabr.txt" For Input As FileNum%
    Do While Not EOF(FileNum%)
        tempabr$ = Input$(4, FileNum%)
        stateabr$ = Left$(tempabr$, 2)
        StateNum% = Val(Mid$(tempabr$, 3, 2))
        If stateabr$ = state_code$ Then
          State_Verify = StateNum%
          Close FileNum%
          Exit Function
        End If
    Loop
    Close FileNum%
    State_Verify = True
    msg$ = "Invalid State Abbrevation" + Chr$(13) + "F1 for Help"
    MsgBox msg$, 0
StateErr:
    Select Case Err
        Case 6, 62
            State_Verify = True
            msg$ = "Invalid State Abbrevation" + Chr$(13) + "F1 for Help"
            MsgBox msg$, 64
            Exit Function
        Case 53, 75, 76
            State_Verify = True
            MsgText = "State Code File not found.  Make sure that STATEABR.TEXT exists in the AMERTT directory, and try again."
            MsgBox MsgText, 64, "Americare Tailored Term"
            Exit Function
        Case Else
            State_Verify = True
            msg$ = Err & " ERROR$" & " Global"
            MsgBox msg$, 0
            Exit Function
    End Select
End Function

Sub Verify_Life_Birth (RetCode As Integer, tempbirth As String)

    RetCode% = True
    mm$ = Left$(tempbirth$, 2)
    dd$ = Mid$(tempbirth$, 4, 2)
    yy$ = Right(tempbirth$, 4)
    If Val(mm$) < 1 Or Val(mm$) > 12 Or Val(dd$) < 1 Or Val(dd$) > 31 Or Val(yy$) > Year(Now) Then
       RetCode% = False
       Exit Sub
    End If

    Select Case Val(mm$)
       Case 2
          If Val(yy$) Mod 4 <> 0 Or (Val(yy$) Mod 100 = 0 And Val(yy$) Mod 400 <> 0) Then
             If Val(dd$) > 28 Then
                RetCode% = False
                Exit Sub
             End If
          Else
             If Val(dd$) > 29 Then
                RetCode% = False
                Exit Sub
             End If
          End If
       Case 4, 6, 9, 11
          If Val(dd$) > 30 Then
             RetCode% = False
             Exit Sub
          End If
       Case Else
    End Select

End Sub

Sub Year_Calc (lowdt&, highdt&, calcyr%, low_ymd&)
    If lowdt& <> 0 And highdt& <> 0 Then
       low_temp$ = Format$(lowdt&, "yyyymmdd")
       hi_temp$ = Format$(highdt&, "yyyymmdd")
       low_ymd& = Val(low_temp$)
       hi_ymd& = Val(hi_temp$)
       calcyr% = Int((hi_ymd& - low_ymd&) / 10000)
    End If
End Sub

