
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

Function State_Verify (state_code As String) As Integer
On Local Error GoTo StateErr
    found% = False
    State_Verify = True
    FileNum% = FreeFile
    If state_code$ = "  " Then Exit Function
    Open "STATEABR.TXT" For Input As FileNum%
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
    yy$ = Right(tempbirth$, 2)
    If Val(mm$) < 1 Or Val(mm$) > 12 Or Val(dd$) < 1 Or Val(dd$) > 31 Or Val(yy$) > (1900 + Year(Now)) Then
       RetCode% = False
       Exit Sub
    End If

    Select Case Val(mm$)
       Case 2
          If (Val(yy$) = 0 And Year(Now) < 2000) Or Val(yy$) / 4 <> Int(Val(yy$) / 4) Then
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

Sub Format_Amount (amtstr As String, amtlen As Integer, amtval As Currency, dollar As String)

    amtstr$ = Space$(amtlen% + 1)
    tempstr$ = Format$(amtval, "##,###,##0.00")
    Mid$(amtstr$, amtlen% - Len(tempstr$) + 2, Len(tempstr$)) = tempstr$
    If dollar$ = "$" Then
       Mid$(amtstr$, amtlen% - Len(tempstr$) + 1, 1) = "$"
    End If

End Sub

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
              Xyy$ = Mid$(IBIRTH$, 7, 2)
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

Function WriteAnyIniStr (section As String, key As String, strvalue As String, file As String) As Integer
     
    i% = WritePrivateProfileString(section, key, strvalue, file)
    WriteAnyIniStr = i%

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

