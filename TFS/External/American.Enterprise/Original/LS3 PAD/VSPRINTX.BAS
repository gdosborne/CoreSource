
Option Explicit

    Dim i As Integer
    Dim j As Integer
    Dim k As Integer
    Dim s As Integer
    
    Dim IllusTotalPages As Integer
    Dim IllusCurrPage As Integer
    Dim IllusBeginAge As Integer
    Dim IllusCurrAge As Integer
    Dim ClientBeginAge As Integer   'for in-force illustrations
    Dim US_Only As Integer          'for state versions
    Dim SaveCurrentY As Long
    Dim OptionalY As Long
    Dim BumpRMargin As Long
    
    Dim IllusText As String
    Dim TableName As String * 5
    Dim NL As String
    
    Dim IllusDB As Database
    Dim IllusDS As DynaSet
    Dim IllusTD As TableDefs
    Dim IllusFld As Fields

Sub Check_State_Variations ()

    Select Case (LSClient(s).State)
       Case "MI"
          US_Only = False
          IllusTotalPages = IllusTotalPages + 1
       Case Else
          US_Only = True
    End Select

End Sub

Sub Cost_Index_Table ()
    
    Dim IllusHeader As String
    Dim IllusFormat As String
  
    IllusHeader = " |Guaranteed" & NL & "Values| |Nonguaranteed" & NL & "Values;"
    IllusFormat = "^2800|^1850|^700|^1850;"
    IllusText = ""
    frmVSPrint.vsPrint.FontBold = False
    frmVSPrint.vsPrint.TextAlign = 2
    frmVSPrint.vsPrint.FontSize = 10
    frmVSPrint.vsPrint.CurrentY = 11400
    frmVSPrint.vsPrint.CurrentX = 3670
    frmVSPrint.vsPrint.MarginLeft = 3670
    frmVSPrint.vsPrint.MarginRight = 720
    frmVSPrint.vsPrint.TableBorder = 0
    frmVSPrint.vsPrint.Table = IllusFormat & IllusHeader
    frmVSPrint.vsPrint.CurrentY = 11960
    IllusHeader = " |10 Year|20 Year| |10 Year|20 Year;"
    IllusFormat = "^2800|>925|>925|^700|>925|>925;"
    frmVSPrint.vsPrint.Table = IllusFormat & IllusHeader
    IllusHeader = ""
    IllusFormat = "<2800|>925|>925|^700|>925|>925;"
    IllusText = IllusText & "Surrender Cost Index:|"
    For j = 0 To 1
      For k = 0 To 1
        If LS_Index_Numerator_SC(j, k) = 0 Or LS_Index_Denominator(j, k) = 0 Then
           IllusText = IllusText & "N/A"
        Else
           IllusText = IllusText & Format$(LS_Index_Numerator_SC(j, k) / (LS_Index_Denominator(j, k) / 1000), "##0.00")
        End If
        If j = 0 And k = 1 Then
           IllusText = IllusText & "| "
        End If
        If j = 1 And k = 1 Then
           IllusText = IllusText & ";"
        Else
           IllusText = IllusText & "|"
        End If
      Next k
    Next j
    IllusText = IllusText & "Net Payment Index:|"
    For j = 0 To 1
      For k = 0 To 1
        If IllusLapse(j, k) = True Or LS_Index_Numerator_NP(j, k) = 0 Or LS_Index_Denominator(j, k) = 0 Then
           IllusText = IllusText & "N/A"
        Else
           IllusText = IllusText & Format$(LS_Index_Numerator_NP(j, k) / (LS_Index_Denominator(j, k) / 1000), "##0.00")
        End If
        If j = 0 And k = 1 Then
           IllusText = IllusText & "| "
        End If
        If j = 1 And k = 1 Then
           IllusText = IllusText & ";"
        Else
           IllusText = IllusText & "|"
        End If
      Next k
    Next j
    frmVSPrint.vsPrint.Table = IllusFormat & IllusText

End Sub

Sub Illustration_Table ()
    
    Dim IllusHeader As String
    Dim IllusFormat As String
    Dim Limit_Age As Integer
  
    If LSClient(s).IllusType = 195 Then
       Limit_Age = 95
    Else
       Limit_Age = LSClient(s).IllusType - 300
    End If
    
    If LSLedger(95).DeathBenefit(0) > 0 Then
       IllusHeader = " |Guaranteed Values|Nonguaranteed Assumptions (Current);"
    Else
       IllusHeader = " |Guaranteed Values **|Nonguaranteed Assumptions (Current);"
    End If
    IllusFormat = "^1770|^2865|^2865;"
    frmVSPrint.vsPrint.FontBold = False
    frmVSPrint.vsPrint.TextAlign = 2
    frmVSPrint.vsPrint.FontSize = 10
    frmVSPrint.vsPrint.LineSpacing = 97  'saves approximately 1 linespace per page
    frmVSPrint.vsPrint.CurrentY = 4144
    frmVSPrint.vsPrint.MarginLeft = 3320
    frmVSPrint.vsPrint.MarginRight = 480
    frmVSPrint.vsPrint.TableBorder = 6
    frmVSPrint.vsPrint.Table = IllusFormat & IllusHeader
    IllusHeader = "Age|End of Year|Premium Outlay|Cash Surrender Value|Accum- ulation Value|Death Benefit|Cash Surrender Value|Accum- ulation Value|Death Benefit;"
    IllusFormat = "^435|^475|^860|^955|^955|^955|^955|^955|^955;"
    frmVSPrint.vsPrint.Table = IllusFormat & IllusHeader
    IllusHeader = ""
    IllusText = ""
    IllusFormat = ">435|>475|>860|>955|>955|>955|>955|>955|>955;"
    frmVSPrint.vsPrint.FontSize = 9
    k = 1
    IllusCurrAge = IllusBeginAge
    Do While (k < 36)
       IllusText = IllusText & Format$(IllusCurrAge, "##") & "|" & Format$(IllusCurrAge - ClientBeginAge, "##") & "|"
       If LSLedger(IllusCurrAge).AnnualOutlay > 0 Then
          IllusText = IllusText & Format$(LSLedger(IllusCurrAge).AnnualOutlay, "#,###,###") & "|"
       Else
          IllusText = IllusText & "*|"
       End If
       For j = 0 To 1
         If LSLedger(IllusCurrAge).SurrenderValue(j) > 0 Then
            IllusText = IllusText & Format$(LSLedger(IllusCurrAge).SurrenderValue(j), "##,###,###") & "|"
         ElseIf LSLedger(IllusCurrAge).CashValue(j) > 0 Then
            IllusText = IllusText & "0|"
         Else
            IllusText = IllusText & "0|"     'instead of double-asterisks
         End If
         If LSLedger(IllusCurrAge).CashValue(j) > 0 Then
            IllusText = IllusText & Format$(LSLedger(IllusCurrAge).CashValue(j), "##,###,###") & "|"
         Else
            IllusText = IllusText & "0|"     'instead of double-asterisks
         End If
         If LSLedger(IllusCurrAge).DeathBenefit(j) > 0 Then
            IllusText = IllusText & Format$(LSLedger(IllusCurrAge).DeathBenefit(j), "##,###,###")
         Else
            IllusText = IllusText & "0"      'instead of double-asterisks
         End If
         If j = 0 Then
            IllusText = IllusText & "|"
         Else
            IllusText = IllusText & ";"
         End If
       Next j
       If k Mod 5 = 0 And k < 35 And IllusCurrAge <> 95 Then
          IllusText = IllusText & " | | | | | | | | ;"
       End If
       If LSClient(s).IllusType = 195 Or IllusCurrAge < ClientBeginAge + 25 Then
       
          'standard "print-each-year" illustration
          
          IllusCurrAge = IllusCurrAge + 1
          k = k + 1
          If IllusCurrAge > 95 Then k = 36
          If LSClient(s).IllusType = 195 Then       'logic for printing all years
             If k = 26 Then
                If IllusCurrAge > 86 And IllusCurrAge < 91 Then k = 36
             End If
             If k = 31 Then
                If IllusCurrAge > 86 And IllusCurrAge < 92 Then k = 36
             End If
          End If
       Else                                    'print 25 years, then every 5 years to limit
          k = k + 1
          If k <> 26 Then IllusText = IllusText & " | | | | | | | | ;"
          IllusCurrAge = IllusCurrAge + 1
          If IllusCurrAge > LSClient(s).IllusType - 300 Then k = 36
          If ClientBeginAge < 55 Then
             If k > 27 Then k = 36
          End If
          Do While (IllusCurrAge Mod 5 <> 0 And IllusCurrAge < LSClient(s).IllusType - 300)
             IllusCurrAge = IllusCurrAge + 1
          Loop
       End If
    Loop
    frmVSPrint.vsPrint.Table = IllusFormat & IllusText
      
    If IllusCurrAge >= Limit_Age Then
       frmVSPrint.vsPrint.TextAlign = 0
       frmVSPrint.vsPrint.FontSize = 10
       frmVSPrint.vsPrint.MarginLeft = 3744
       frmVSPrint.vsPrint.MarginRight = 720
       If LSLedger(95).AnnualOutlay = 0 Then
          IllusText = "*    Based on the current interest rate, premium outlays may be suspended.  "
          IllusText = IllusText & "However, these outlays may need to resume depending on the interest actually paid."
          frmVSPrint.vsPrint.Paragraph = IllusText
       End If
       IllusText = "**  Based on your planned premium outlay of {PlannedAnnPrem} for {PayYears}{AltPayYears} under "
       IllusText = IllusText & "guaranteed interest and charges, the policy would{GuarEndAge}."
       Call Insert_Variables
       frmVSPrint.vsPrint.Paragraph = IllusText
       IllusBeginAge = ClientBeginAge + 1     'reset to initial value
    Else
       IllusBeginAge = IllusCurrAge                 'reset for next pass
    End If
      
End Sub

Sub Insert_Variables ()

    Dim VariableLeft As Integer
    Dim VariableRight As Integer
    Dim Position As Integer
    Dim VariableName As String
    Dim TempText As String
    
    VariableLeft = 0
    VariableRight = 0
    Position = 1
    Do While (Position <= Len(IllusText))
       VariableLeft = InStr(Position, IllusText, "{", 1)
       If VariableLeft > 0 Then
          VariableRight = InStr(VariableLeft, IllusText, "}", 1)
          TempText = TempText & Mid$(IllusText, Position, VariableLeft - Position)
          VariableName = Mid$(IllusText, VariableLeft + 1, VariableRight - VariableLeft - 1)
          Select Case (VariableName)
             Case ("AddtlInsds")
                If LSClient(s).InsuredRider(2) = False Then
                   TempText = TempText & "."
                Else
                   k = 2
                   Do While (k < 10)
                      If LSClient(s).InsuredRider(k) = True Then
                         If k = 9 Then
                            TempText = TempText & " and"
                         ElseIf LSClient(s).InsuredRider(k + 1) = False Then
                            TempText = TempText & " and"
                         Else
                            TempText = TempText & ","
                         End If
                         TempText = TempText & " $" & Format$(LSInsuredRider(k).FaceAmt, "#,###,###")
                         TempText = TempText & " to age " & Format$(LSInsuredRider(k).IssueAge + LSInsuredRider(k).EndYear, "##")
                         If LSInsuredRider(k).SexCode = "M" Then
                            TempText = TempText & " on a male "
                         Else
                            TempText = TempText & " on a female "
                         End If
                         TempText = TempText & "age " & Format$(LSInsuredRider(k).IssueAge, "#0")
                         If LSInsuredRider(k).TobaccoUse = 0 Then
                            TempText = TempText & " No Tobacco Use"
                         Else
                            TempText = TempText & " Tobacco User"
                         End If
                         If LSInsuredRider(k).SubStd > 0 Then
                            TempText = TempText & " with a " & Format$(LSInsuredRider(k).SubStd, "#0") & " Table rating"
                         Else
                            TempText = TempText & " standard rating"
                         End If
                      End If
                      k = k + 1
                   Loop
                   TempText = TempText & "."
                End If
             Case ("AgentAddr")
                If Trim(AgtInfo.Address1) <> "" Then
                   TempText = TempText & NL & Trim$(AgtInfo.Address1)
                   If Trim$(AgtInfo.Address2) <> "" Then
                      TempText = TempText & NL & Trim$(AgtInfo.Address2)
                   End If
                   TempText = TempText & NL & Trim$(AgtInfo.City) & " " & AgtInfo.State
                   If AgtInfo.ZipCode > 99999 Then
                      TempText = TempText & "  " & Format$(AgtInfo.ZipCode, "#####-####")
                   Else
                      TempText = TempText & "  " & Format$(AgtInfo.ZipCode, "#####")
                   End If
                Else
                   TempText = TempText & NL & "601 6th Avenue" & NL & "Des Moines, IA  50334"
                End If
             Case ("AgentName")
                TempText = TempText & Trim$(LSClient(s).ContactName)
             Case ("AgentPhone")
                TempText = TempText & NL & Trim$(LSClient(s).ContactPhone)
             Case ("AltIntRate")
               If LSFuture(1).Int_Age = 0 Then
                  TempText = TempText & " "
               Else
                  j = 1
                  Do While (j <= 5)
                     If LSFuture(j).Int_Age > 0 Then
                        TempText = TempText & " to age " & Format$(LSFuture(j).Int_Age, "#0")
                        If j < 5 Then
                           If LSFuture(j + 1).Int_Age > 0 Then
                              TempText = TempText & ", " & Format$(LSFuture(j).Int_Amount, "#0.00") & "%"
                           Else
                              TempText = TempText & " and " & Format$(LSFuture(j).Int_Amount, "#0.00") & "% thereafter "
                           End If
                        Else
                           TempText = TempText & " and " & Format$(LSFuture(j).Int_Amount, "#0.00") & "% thereafter "
                        End If
                     End If
                     j = j + 1
                  Loop
               End If
             Case ("AltPayYears")
                j = 1
                Do While (j <= 5)
                   If LSFuture(j).Prem_Age > 0 Then
                      Select Case (LSClient(s).Mode)
                         Case "M"
                            TempText = TempText & " $" & Format$(LSFuture(j).Prem_Amount * 12, "#,###,##0.00")
                         Case "Q"
                            TempText = TempText & " $" & Format$(LSFuture(j).Prem_Amount * 4, "#,###,##0.00")
                         Case "S"
                            TempText = TempText & " $" & Format$(LSFuture(j).Prem_Amount * 2, "#,###,##0.00")
                         Case Else
                            TempText = TempText & " $" & Format$(LSFuture(j).Prem_Amount, "#,###,##0.00")
                      End Select
                      TempText = TempText & " for the next "
                      If j < 5 Then
                         If LSFuture(j + 1).Prem_Age > 0 Then
                            TempText = TempText & Format$(LSFuture(j + 1).Prem_Age - LSFuture(j).Prem_Age, "#0") & " year"
                            If LSFuture(j + 1).Prem_Age - LSFuture(j).Prem_Age > 1 Then
                               TempText = TempText & "s"
                            End If
                            If j >= 1 And j <= 3 Then
                               If LSFuture(j + 2).Prem_Age > 0 Then
                                  TempText = TempText & ","
                               Else
                                  TempText = TempText & " and"
                               End If
                            End If
                            If j = 4 Then TempText = TempText & " and"
                         Else
                            TempText = TempText & Format$(LSClient(s).YearsToPay - (LSFuture(j).Prem_Age - LSClient(s).IssueAge), "#0") & " year"
                            If LSClient(s).YearsToPay - (LSFuture(j).Prem_Age - LSClient(s).IssueAge) > 1 Then
                               TempText = TempText & "s,"
                            End If
                         End If
                      Else
                         TempText = TempText & Format$(LSClient(s).YearsToPay - (LSFuture(j).Prem_Age - LSClient(s).IssueAge), "#0") & " year"
                         If LSClient(s).YearsToPay - (LSFuture(j).Prem_Age - LSClient(s).IssueAge) > 1 Then
                            TempText = TempText & "s,"
                         Else
                            TempText = TempText & ","
                         End If
                      End If
                   End If
                   j = j + 1
               Loop
             Case ("Bullet")
                TempText = TempText & "l"
             Case ("CertifIssueAge")
                TempText = TempText & Format$(LSClient(s).IssueAge, "#0")
             Case ("ChangesCertif")
                k = 1
                Do While (k <= 5)
                   If LSFuture(k).DB_Age > 0 Then
                      TempText = TempText & "A new Death Benefit Amount of $" & Format$(LSFuture(k).DB_Amount, "##,###,##0.00")
                      TempText = TempText & " is assumed starting at age " & Format$(LSFuture(k).DB_Age, "#0") & NL
                   End If
                   k = k + 1
                Loop
                k = 1
                Do While (k <= 5)
                   If LSFuture(k).Prem_Age > 0 Then
                      TempText = TempText & "A new Modal Premium Amount of $" & Format$(LSFuture(k).Prem_Amount, "##,###,##0.00")
                      TempText = TempText & " is assumed starting at age " & Format$(LSFuture(k).Prem_Age, "#0") & NL
                   End If
                   k = k + 1
                Loop
                k = 1
                Do While (k <= 5)
                   If LSFuture(k).Int_Age > 0 Then
                      TempText = TempText & "A new Projected Interest Rate of " & Format$(LSFuture(k).Int_Amount, "#0.000") & "%"
                      TempText = TempText & " is assumed starting at age " & Format$(LSFuture(k).Int_Age, "#0") & NL
                   End If
                   k = k + 1
                Loop
                k = 1
                Do While (k <= 5)
                   If LSFutureWD(k).Loan_Age > 0 Then
                      TempText = TempText & "A Policy Loan Amount of $" & Format$(LSFutureWD(k).Loan_Amount, "#,###,##0.00")
                      TempText = TempText & " is assumed starting at age " & Format$(LSFutureWD(k).Loan_Age, "#0")
                      TempText = TempText & " and ending at age " & Format$(LSFutureWD(k).Loan_Age + LSFutureWD(k).Loan_Years - 1, "#0") & NL
                   End If
                   k = k + 1
                Loop
                k = 1
                Do While (k <= 5)
                   If LSFutureWD(k).LoanPay_Age > 0 Then
                      TempText = TempText & "A Policy Loan Repayment of $" & Format$(LSFutureWD(k).LoanPay_Amount, "#,###,##0.00")
                      TempText = TempText & " is assumed starting at age " & Format$(LSFutureWD(k).LoanPay_Age, "#0")
                      TempText = TempText & " and ending at age " & Format$(LSFutureWD(k).LoanPay_Age + LSFutureWD(k).LoanPay_Years - 1, "#0") & NL
                   End If
                   k = k + 1
                Loop
                k = 1
                Do While (k <= 5)
                   If LSFutureWD(k).WD_Age > 0 Then
                      TempText = TempText & "A Partial Surrender of $" & Format$(LSFutureWD(k).WD_Amount, "#,###,##0.00")
                      TempText = TempText & " is assumed starting at age " & Format$(LSFutureWD(k).WD_Age, "#0")
                      TempText = TempText & " and ending at age " & Format$(LSFutureWD(k).WD_Age + LSFutureWD(k).WD_Years - 1, "#0") & NL
                   End If
                   k = k + 1
                Loop
             Case ("ChildAmount")
                TempText = TempText & "$" & Format$(LSChildRider.FaceAmt, "##,###")
             Case ("ChildCertif")
                TempText = TempText & NL & "Child's Term Life Insurance"
                If LSClient(s).State = "SD" Then TempText = TempText & " (form A-3387)"
                TempText = TempText & ":  $" & Format$(LSChildRider.FaceAmt, "##,###") & ".   (Age of youngest child:  " & Format$(LSChildRider.AgeYoungest, "#0") & ")"
             Case ("ChooseLowRate")
               If LSFuture(1).Int_Age > 0 Then
                  TempText = TempText & ", except for the lower interest rate(s) you have chosen"
               End If
             Case ("ClientAddr")
                TempText = TempText & ""
             Case ("ClientBirth")
                TempText = TempText & Format$(LSClient(s).DateOfBirth, "Short Date")
             Case ("ClientIssueAge")
                TempText = TempText & "   Issue Age " & Format$(LSClient(s).IssueAge, "#0")
             Case ("ClientName")
                TempText = TempText & Trim$(LSClient(s).ClientName)
             Case ("ClientSex")
                If LSClient(s).SexCode = "M" Then
                   TempText = TempText & "Male"
                Else
                   TempText = TempText & "Female"
                End If
             Case ("COLAEnd")
                TempText = TempText & Format$(65, "##")
             Case ("CostPct")
                TempText = TempText & Format$(LSClient(s).COLA, "##") & "%"
             Case ("CTPercent")
                If LSClient(s).State = "CT" Then
                   TempText = TempText & Format$(25, "##")
                Else
                   TempText = TempText & Format$(50, "##")
                End If
             Case ("CurrEndAge")
                If LSLedger(95).CashValue(1) > 0 Then
                   TempText = TempText & " remain in force to age 95"
                Else
                   For j = 95 To LSClient(s).IssueAge + 10 Step -1
                     If j = LSClient(s).IssueAge + 10 Or LSLedger(j).CashValue(1) > 0 Then
                        TempText = TempText & " cease at age " & Format$(j, "##")
                        j = 0     'forces end of loop
                     End If
                   Next j
                End If
             Case ("CurrentDate")
                TempText = TempText & Format$(Date, "dddd, mmmm d, yyyy")
             Case ("CurrIntRate")
                TempText = TempText & Format$(LSClient(s).CurrIntRate, "#0.00")
             Case ("CurrPage")
                TempText = TempText & Format$(IllusCurrPage, "##")
             Case ("Curr1stMort")
                TempText = TempText & Format$(LSMort(1, LSClient(s).IssueAge).Prin_Base + LSMort(1, LSClient(s).IssueAge).Prin_Sub, "###.000000")
             Case ("Curr1stSurr")
                TempText = TempText & Format$(LSMort(1, LSClient(s).IssueAge).Surr_Charge, "######.00")
             Case ("ExpPct")
                If LSClient(s).FaceAmt < 100000 Then
                   TempText = TempText & "7"
                Else
                   TempText = TempText & "0"
                End If
             Case ("GPOAges")
                Select Case LSClient(s).IssueAge
                   Case Is < 25
                      TempText = TempText & "s 25, 28, 31, 34, 37 and 40"
                   Case 25 To 27
                      TempText = TempText & "s 28, 31, 34, 37 and 40"
                   Case 28 To 30
                      TempText = TempText & "s 31, 34, 37 and 40"
                   Case 31 To 33
                      TempText = TempText & "s 34, 37 and 40"
                   Case 34 To 36
                      TempText = TempText & "s 37 and 40"
                   Case Else
                      TempText = TempText & " 40"
                End Select
             Case ("GPOAmount")
                TempText = TempText & "$" & Format$(LSClient(s).GPO, "##,###")
             Case ("GuarEndAge")
                If LSLedger(95).CashValue(0) > 0 Then
                   TempText = TempText & " remain in force to age 95"
                Else
                   For j = 95 To LSClient(s).IssueAge + 10 Step -1
                     If j = LSClient(s).IssueAge + 10 Or LSLedger(j).CashValue(0) > 0 Then
                        TempText = TempText & " cease at age " & Format$(j, "##")
                        j = 0     'forces end of loop
                     End If
                   Next j
                End If
             Case ("GuarEndDB")
                If LSLedger(95).CashValue(0) > 0 Then
                   TempText = TempText & "$" & Format$(LSLedger(95).DeathBenefit(0), "##,###,###")
                Else
                   For j = 95 To LSClient(s).IssueAge + 10 Step -1
                     If j = LSClient(s).IssueAge + 10 Or LSLedger(j).CashValue(0) > 0 Then
                        If LSLedger(j).DeathBenefit(0) > 0 Then
                           TempText = TempText & "$" & Format$(LSLedger(j).DeathBenefit(0), "##,###,###")
                        Else
                           TempText = TempText & "$" & Format$(LSClient(s).FaceAmt, "##,###,###")
                        End If
                        j = 0     'forces end of loop
                     End If
                   Next j
                End If
             Case ("GuideAnnPrem")
                TempText = TempText & "$" & Format$(CalcPrem.GuideAnnual, "#,###,###")
             Case ("GuideAnnual")
                If CalcPrem.GuideSingleAddInsd > 0 Then
                   BumpRMargin = -2200
                Else
                   BumpRMargin = 0
                End If
                TempText = TempText & "$" & Format$(CalcPrem.GuideAnnual, "#,###,###")
             Case ("GuideSingle")
                If CalcPrem.GuideSingleAddInsd > 0 Then
                   BumpRMargin = -2200
                Else
                   BumpRMargin = 0
                End If
                TempText = TempText & "$" & Format$(CalcPrem.GuideSingle, "#,###,###")
             Case ("GuideSingleRiderL")
                If CalcPrem.GuideSingleAddInsd > 0 Then
                   TempText = TempText & "Guideline Single Premium with Additional Insured's Rider: "
                End If
             Case ("GuideSingleRiderR")
                If CalcPrem.GuideSingleAddInsd > 0 Then
                   TempText = TempText & "$" & Format$(CalcPrem.GuideSingleAddInsd, "#,###,###")
                End If
             Case ("InsdsCertif")
                k = 1
                Do While (k < 10)
                   If LSClient(s).InsuredRider(k) = True Then
                      If k > 1 Then TempText = TempText & NL
                      TempText = TempText & "Additional Insured Term Life Insurance"
                      If LSClient(s).State = "SD" Then TempText = TempText & " (form A-3386)"
                      TempText = TempText & ":  $" & Format$(LSInsuredRider(k).FaceAmt, "#,###,###")
                      If LSInsuredRider(k).SexCode = "M" Then
                         TempText = TempText & " on a Male age "
                      Else
                         TempText = TempText & " on a Female age "
                      End If
                      TempText = TempText & Format$(LSInsuredRider(k).IssueAge, "#0")
                      If LSInsuredRider(k).TobaccoUse = 0 Then
                         TempText = TempText & " No Tobacco Use"
                      Else
                         TempText = TempText & " Tobacco User"
                      End If
                      If LSInsuredRider(k).SubStd > 0 Then
                         TempText = TempText & " (" & Format$(LSInsuredRider(k).SubStd, "#0") & " Table rating "
                      Else
                         TempText = TempText & " (Standard rating "
                      End If
                      TempText = TempText & " to age " & Format$(LSInsuredRider(k).IssueAge + LSInsuredRider(k).EndYear, "##") & ")"
                   End If
                   k = k + 1
                Loop
             Case ("Insd1")
                If LSInsuredRider(1).SexCode = "M" Then
                   TempText = TempText & " a male "
                Else
                   TempText = TempText & " a female "
                End If
                TempText = TempText & "age " & Format$(LSInsuredRider(1).IssueAge, "#0")
                If LSInsuredRider(1).TobaccoUse = 0 Then
                   TempText = TempText & " No Tobacco Use"
                Else
                   TempText = TempText & " Tobacco User"
                End If
                If LSInsuredRider(1).SubStd > 0 Then
                   TempText = TempText & " with a " & Format$(LSInsuredRider(1).SubStd, "#0") & " Table rating (" & Format$(LSInsuredRider(1).SubStd * 25, "#00") & "%)"
                Else
                   TempText = TempText & " standard rating"
                End If
             Case ("Insd1Amount")
                TempText = TempText & "$" & Format$(LSInsuredRider(1).FaceAmt, "#,###,###")
             Case ("Insd1EndAge")
                TempText = TempText & Format$(LSInsuredRider(1).IssueAge + LSInsuredRider(1).EndYear, "##")
             Case ("IssueAgePlus10")
                TempText = TempText & Format$(LSClient(s).IssueAge + 10, "##")
             Case ("IssueDB")
                TempText = TempText & "$" & Format$(LSClient(s).FaceAmt, "#,###,###")
             Case ("LoanText")
                j = 1
                Do While (j <= 5)
                   If LSFutureWD(j).Loan_Age > 0 Then
                      TempText = TempText & "  A policy loan of $" & Format$(LSFutureWD(j).Loan_Amount, "#,###,##0.00")
                      TempText = TempText & " is illustrated to be taken starting at age " & Format$(LSFutureWD(j).Loan_Age, "#0")
                      TempText = TempText & " ending at age " & Format$(LSFutureWD(j).Loan_Age + LSFutureWD(j).Loan_Years - 1, "#0") & "."
                   End If
                   j = j + 1
                Loop
                j = 1
                Do While (j <= 5)
                   If LSFutureWD(j).LoanPay_Age > 0 Then
                      TempText = TempText & "  A loan repayment of $" & Format$(LSFutureWD(j).LoanPay_Amount, "#,###,##0.00")
                      TempText = TempText & " is illustrated to be made starting at age " & Format$(LSFutureWD(j).LoanPay_Age, "#0")
                      TempText = TempText & " ending at age " & Format$(LSFutureWD(j).LoanPay_Age + LSFutureWD(j).LoanPay_Years - 1, "#0") & "."
                   End If
                   j = j + 1
               Loop
             Case ("LumpSum")
                If LSClient(s).LumpSum > 0 Then
                   TempText = TempText & "Lump Sum Payment of $" & Format$(LSClient(s).LumpSum, "#,###,##0.00")
                End If
             Case ("LumpSumCertif")
                TempText = TempText & "$" & Format$(LSClient(s).LumpSum, "#,###,##0.00")
             Case ("MaxExpPct")
                If LSClient(s).FaceAmt < 100000 Then
                   TempText = TempText & "10"
                Else
                   TempText = TempText & "5"
                End If
             Case ("Max1stMort")
                TempText = TempText & Format$(LSMort(0, LSClient(s).IssueAge).Prin_Base + LSMort(0, LSClient(s).IssueAge).Prin_Sub, "###.000000")
             Case ("Max1stSurr")
                TempText = TempText & Format$(LSMort(0, LSClient(s).IssueAge).Surr_Charge, "######.00")
             Case ("MidEndAge")
                If LSLedger(95).CashValue(0) > 0 Then
                   TempText = TempText & " remain in force to age 95"
                Else
                   For j = 95 To LSClient(s).IssueAge + 10 Step -1
                     If j = LSClient(s).IssueAge + 10 Or LSLedger(j).CashValue(2) > 0 Then
                        TempText = TempText & " cease at age " & Format$(j, "##")
                        j = 0     'forces end of loop
                     End If
                   Next j
                End If
             Case ("MidIntRate")
                TempText = TempText & Format$((LS_IRate(2) * 100), "#0.000")
             Case ("MinAnnPrem")
                TempText = TempText & "$" & Format$(CalcPrem.TotAnnMin, "###,##0")
             Case ("MIIntText")
                If LSClient(s).State = "MI" Then
                   TempText = TempText & "This policy's interest rate is not interest indexed.  "
                End If
             Case ("MITaxID")
                TempText = TempText & "Social Security Number: " & LSClient(s).TaxID
             Case ("Mode")
                Select Case LSClient(s).Mode
                   Case ("A")
                      TempText = TempText & "Annually"
                   Case ("S")
                      TempText = TempText & "Semiannually"
                   Case ("Q")
                      TempText = TempText & "Quarterly"
                   Case Else
                      TempText = TempText & "APP"
                End Select
             Case ("ModeCertif")
                Select Case LSClient(s).Mode
                   Case ("A")
                      TempText = TempText & "Annual"
                   Case ("S")
                      TempText = TempText & "Semiannual"
                   Case ("Q")
                      TempText = TempText & "Quarterly"
                   Case Else
                      TempText = TempText & "Monthly APP"
                End Select
             Case ("OmitMI")
                If LSClient(s).State = "MI" Then
                   TempText = "NOPRINT"
                Else
                   TempText = TempText & ""
                End If
             Case ("OtherInsdCost")
                If LSClient(s).InsuredRider(1) = True Then
                   TempText = TempText & "and the death benefit for any additional insureds "
                End If
             Case ("PAMedCond")
                If LSClient(s).State = "PA" Then
                   TempText = TempText & "medical condition"
                Else
                   TempText = TempText & "illness"
                End If
             Case ("PayYears")
               If LSFuture(1).Prem_Age > 0 Then
                  TempText = TempText & Format$(LSFuture(1).Prem_Age - LSClient(s).IssueAge, "#0") & " year"
                  If LSFuture(1).Prem_Age - LSClient(s).IssueAge > 1 Then
                     TempText = TempText & "s"
                  End If
                  If LSFuture(2).Prem_Age > 0 Then
                     TempText = TempText & ","
                  Else
                     TempText = TempText & " and"
                  End If
               Else
                  TempText = TempText & Format$(LSClient(s).YearsToPay, "#0") & " years,"
               End If
             Case ("PlannedAnnPrem")
                Select Case (LSClient(s).Mode)
                   Case "M"
                      TempText = TempText & "$" & Format$(LSClient(s).PlannedPrem * 12, "#,###,##0.00")
                   Case "Q"
                      TempText = TempText & "$" & Format$(LSClient(s).PlannedPrem * 4, "#,###,##0.00")
                   Case "S"
                      TempText = TempText & "$" & Format$(LSClient(s).PlannedPrem * 2, "#,###,##0.00")
                   Case Else
                      TempText = TempText & "$" & Format$(LSClient(s).PlannedPrem, "#,###,##0.00")
                End Select
             Case ("PlannedModalPrem")
                TempText = TempText & "$" & Format$(LSClient(s).PlannedPrem, "#,###,##0.00")
             Case ("Rating")
                If LSClient(s).SubStd > 0 Then
                   TempText = TempText & NL & "Extra mortality rating of " & Format$(LSClient(s).SubStd, "##") & " table"
                   If LSClient(s).SubStd > 1 Then
                      TempText = TempText & "s"
                   End If
                   TempText = TempText & " (" & Format$(LSClient(s).SubStd * 25, "###") & "%)"
                End If
             Case ("Standard")
                If LSClient(s).SubStd = 0 Then
                   TempText = TempText & "Standard" & NL
                Else
                   TempText = TempText & ""
                End If
             Case ("StandardText")
                If LSClient(s).SubStd = 0 Then
                   TempText = TempText & "Standard,"
                Else
                   TempText = TempText & ""
                End If
             Case ("StandCertif")
                If LSClient(s).SubStd = 0 Then
                   TempText = TempText & "Standard"
                Else
                   TempText = TempText & ""
                End If
             Case ("StateWritten")
                TempText = TempText & LSClient(s).State
             Case ("SubStandText")
                If LSClient(s).SubStd = 0 Then
                   TempText = TempText & ""
                Else
                   TempText = TempText & " with a " & Format$(LSClient(s).SubStd, "##") & " table, " & Format$(LSClient(s).SubStd * 25, "###") & "% rating"
                End If
             Case ("SubStandCertif")
                If LSClient(s).SubStd = 0 Then
                   TempText = TempText & ""
                Else
                   TempText = TempText & "Table Rating of " & Format$(LSClient(s).SubStd * 25, "###") & "%"
                End If
             Case ("SurrText")
                j = 1
                Do While (j <= 5)
                   If LSFutureWD(j).WD_Age > 0 Then
                      TempText = TempText & "  A partial surrender of $" & Format$(LSFutureWD(j).WD_Amount, "#,###,##0.00")
                      TempText = TempText & " is illustrated to be taken starting at age " & Format$(LSFutureWD(j).WD_Age, "#0")
                      TempText = TempText & " ending at age " & Format$(LSFutureWD(j).WD_Age + LSFutureWD(j).WD_Years - 1, "#0") & "."
                   End If
                   j = j + 1
                Loop
             Case ("SurrYears")
                TempText = TempText & "14"      'always 14 years for everyone: M/F,SM/NS
             Case ("TargetPrem")
                TempText = TempText & "$" & Format$(CalcPrem.TotTarg, "###,##0")
             Case ("TextLumpSum")
                If LSClient(s).LumpSum > 0 Then
                   TempText = TempText & NL & "Initial Additional Payment of $" & Format$(LSClient(s).LumpSum, "#,###,##0.00")
                End If
             Case ("Tobacco")
                If LSClient(s).TobaccoUse = 0 Then
                   TempText = TempText & "No Tobacco Use"
                Else
                   TempText = TempText & "Tobacco User"
                End If
             Case ("TotalPage")
                TempText = TempText & Format$(IllusTotalPages, "##")
             Case ("TradeMark")
                TempText = TempText & Chr$(174)
             Case Else
               TempText = TempText & "** missing! **"
          End Select
       Else
          If Position > 1 Then
             TempText = TempText & Mid$(IllusText, Position, Len(IllusText) - Position + 1)
          End If
          VariableRight = 0
       End If
       If VariableRight > 0 Then
          Position = VariableRight + 1
       Else
          Position = Len(IllusText) + 1
       End If
    Loop
    
    If Len(TempText) > 0 Then
       If Left$(TempText, 7) = "NOPRINT" Then
          IllusText = ""
       Else
          IllusText = TempText
       End If
    End If
    
End Sub

Sub Optional_Paragraphs ()
    
    Select Case (IllusDS!OptionalID)
       Case 1                                      'Optional Head or Paragraph for WPD
          If LSClient(s).WPD = False Then Exit Sub
       Case 2                                      'Optional Head or Paragraph for GPO
          If LSClient(s).GPO = 0 Then Exit Sub
       Case 3                                      'Optional Head or Paragraph for COLA
          If LSClient(s).COLA = 0 Then Exit Sub
       Case 4                                      'Optional Head or Paragraph for Child Rider
          If LSClient(s).ChildRider = False Then Exit Sub
       Case 5                                      'Optional Head or Paragraph for Insd Rider
          If LSClient(s).InsuredRider(1) = False Then Exit Sub
       Case 6                                      'Optional paragraphs for California only
          If LSClient(s).State = "CA" Then Exit Sub
       Case 10                                     'Optional Paragraph for Premium Suspense
          If LSLedger(95).AnnualOutlay > 0 Then Exit Sub
       Case 12                                     'Optional paragraphs for Illinois only
          frmVSPrint.vsPrint.CurrentY = IllusDS!CurrentY
          If (LSClient(s).State = "IL" And IllusDS!State <> "IL") Or (LSClient(s).State <> "IL" And IllusDS!State <> "US") Then Exit Sub
       Case 37                                    'Optional paragraphs for Pennsylvania only
          If (LSClient(s).State = "PA" And IllusDS!State <> "PA") Or (LSClient(s).State <> "PA" And IllusDS!State <> "US") Then Exit Sub
          If (LSClient(s).State = "CA" And IllusDS!State = "US") Then Exit Sub     'California does not get PA replacement paragraphs
       Case 120 To 124                             'Optional Paragraphs for surrender/loan/repay explanation                                      'Optional Paragraph for Premium Suspense
          If SurrExist = True And LoanExist = False And IllusDS!OptionalID <> 121 Then Exit Sub
          If SurrExist = False And LoanExist = True And RepayExist = False And IllusDS!OptionalID <> 122 Then Exit Sub
          If SurrExist = False And LoanExist = True And RepayExist = True And (IllusDS!OptionalID <> 122 And IllusDS!OptionalID <> 124) Then Exit Sub
          If SurrExist = True And LoanExist = True And RepayExist = False And IllusDS!OptionalID <> 123 Then Exit Sub
          If SurrExist = True And LoanExist = True And RepayExist = True And (IllusDS!OptionalID <> 123 And IllusDS!OptionalID <> 124) Then Exit Sub
          If SurrExist = False And LoanExist = False And IllusDS!OptionalID <> 120 Then Exit Sub
       Case Else
    End Select
    Call Insert_Variables
    frmVSPrint.vsPrint.FontBold = IllusDS!FontBold
    frmVSPrint.vsPrint.TextAlign = IllusDS!Align
    frmVSPrint.vsPrint.FontSize = IllusDS!FontSize
    frmVSPrint.vsPrint.CalcParagraph = IllusText
    If IllusDS!FontBold = True Then
       frmVSPrint.vsPrint.CurrentY = frmVSPrint.vsPrint.CurrentY + 180
    End If
    If IllusDS!CurrentY = 2 Then
       frmVSPrint.vsPrint.CurrentY = frmVSPrint.vsPrint.CurrentY + (frmVSPrint.vsPrint.FontSize * 22.5)
    End If
    frmVSPrint.vsPrint.CurrentX = IllusDS!LMargin
    frmVSPrint.vsPrint.MarginLeft = IllusDS!LMargin
    frmVSPrint.vsPrint.MarginRight = IllusDS!RMargin
    OptionalY = frmVSPrint.vsPrint.CurrentY
    SaveCurrentY = frmVSPrint.vsPrint.CurrentY
    frmVSPrint.vsPrint.Paragraph = IllusText
    If IllusDS!FontBold = True Then
       frmVSPrint.vsPrint.CurrentY = OptionalY
    End If

End Sub

Sub Print_Certification (StandAlone As Integer)
    
    Dim IllusHeader As String
    Dim IllusFormat As String
    Dim TableY As Long
    Dim TableX As Long
    Dim Save_X As Long
    Dim Save_Y As Long

    frmVSPrint.vsPrint.PageBorder = 0
    frmVSPrint.vsPrint.FontName = LS3FontDefault
    frmVSPrint.vsPrint.MarginTop = 0
    frmVSPrint.vsPrint.MarginBottom = 0
   
    NL = Chr$(13) & Chr$(10)
    s = SpouseClient
    If StandAlone = True Then
       IllusText = "Universal Life Insurance" & NL & "Computer Screen Illustration Certification Form"
       frmVSPrint.vsPrint.FontBold = True
       frmVSPrint.vsPrint.TextAlign = 1
       frmVSPrint.vsPrint.FontSize = 12
       frmVSPrint.vsPrint.CurrentY = 560
       frmVSPrint.vsPrint.MarginLeft = 720
       frmVSPrint.vsPrint.MarginRight = 720
       frmVSPrint.vsPrint.Paragraph = IllusText
       IllusText = NL & "I certify that I displayed a computer screen illustration for {ClientName}"
       IllusText = IllusText & " that complies with state requirements and for which no hard copy was furnished."
       IllusText = IllusText & " The illustration was based on the following personal and policy information:" & NL & NL
       frmVSPrint.vsPrint.FontBold = False
       frmVSPrint.vsPrint.TextAlign = 0
       frmVSPrint.vsPrint.FontSize = 10
       Call Insert_Variables
       frmVSPrint.vsPrint.Paragraph = IllusText
    Else
       IllusText = "American Republic Insurance Company" & NL & "National Headquarters, Des Moines, Iowa  50334"
       If LSClient(s).State = "SD" Then
          IllusText = IllusText & NL & "Life Savings (form A-3381)"
       Else
          IllusText = IllusText & NL
       End If
       IllusText = IllusText & NL & "Universal Life Insurance Illustration Information Form"
       frmVSPrint.vsPrint.FontBold = True
       frmVSPrint.vsPrint.TextAlign = 1
       frmVSPrint.vsPrint.FontSize = 12
       frmVSPrint.vsPrint.CurrentY = 560
       frmVSPrint.vsPrint.MarginLeft = 720
       frmVSPrint.vsPrint.MarginRight = 720
       frmVSPrint.vsPrint.Paragraph = IllusText
       IllusText = "HOME OFFICE USE ONLY -- Submit with application and signed illustration"
       frmVSPrint.vsPrint.Paragraph = IllusText
       IllusText = NL & "The following information summarizes the personal information and policy illustration for the proposed insured "
       frmVSPrint.vsPrint.FontBold = False
       frmVSPrint.vsPrint.TextAlign = 0
       frmVSPrint.vsPrint.FontSize = 10
       frmVSPrint.vsPrint.Text = IllusText
       IllusText = "{ClientName}."
       frmVSPrint.vsPrint.FontBold = True
       Call Insert_Variables
       frmVSPrint.vsPrint.Text = IllusText
       IllusText = "  The illustration was prepared by "
       frmVSPrint.vsPrint.FontBold = False
       frmVSPrint.vsPrint.Text = IllusText
       IllusText = "{AgentName}." & NL & NL
       frmVSPrint.vsPrint.FontBold = True
       Call Insert_Variables
       frmVSPrint.vsPrint.Text = IllusText
    End If
    
    CertifTable = 1
    Save_X = frmVSPrint.vsPrint.CurrentX
    Save_Y = frmVSPrint.vsPrint.CurrentY
    frmVSPrint.vsPrint.FontBold = False
    If LSClient(s).State = "SD" Then
       frmVSPrint.vsPrint.LineSpacing = 120
    Else
       frmVSPrint.vsPrint.LineSpacing = 150
    End If
    frmVSPrint.vsPrint.TextAlign = 0
    IllusFormat = "<400|<2468|>2100;"
    IllusText = ""
    frmVSPrint.vsPrint.MarginLeft = 720
    frmVSPrint.vsPrint.MarginRight = 7128
    frmVSPrint.vsPrint.TableBorder = 0
    IllusText = "1.|Gender:|{ClientSex};"
    IllusText = IllusText & "2.|Age At Issue:|{CertifIssueAge};"
    IllusText = IllusText & "3.|Tobacco Status:|{Tobacco};"
    IllusText = IllusText & "4.|Rating Class:|{StandCertif}{SubStandCertif};"
    IllusText = IllusText & "5.|Initial Death Benefit:|{IssueDB};"
    If LSClient(s).State = "SD" Then
       IllusText = IllusText & "6.|Initial Premium:|{PlannedModalPrem};"
    End If
    Call Insert_Variables
    frmVSPrint.vsPrint.Table = IllusFormat & IllusText
    
    frmVSPrint.vsPrint.CurrentX = Save_X
    frmVSPrint.vsPrint.CurrentY = Save_Y
    frmVSPrint.vsPrint.MarginLeft = 6192
    frmVSPrint.vsPrint.MarginRight = 1080
    If LSClient(s).State = "SD" Then
       frmVSPrint.vsPrint.LineSpacing = 100
    Else
       frmVSPrint.vsPrint.LineSpacing = 120
    End If
    frmVSPrint.vsPrint.TextAlign = 1
    If LSClient(s).State = "SD" Then
       IllusText = ""
    Else
       IllusText = "6.|Initial Premium:|{PlannedModalPrem};"
    End If
    IllusText = IllusText & "7.|Planned Annual Premium:|{PlannedAnnPrem};"
    IllusText = IllusText & "8.|Lump Sum Premium:|{LumpSumCertif};"
    IllusText = IllusText & "9.|Premium Mode:|{ModeCertif};"
    If LSClient(s).State = "SD" Then
       IllusText = IllusText & "10.|Interest Rates:| ;"
       IllusText = IllusText & "   |Current Nonguaranteed:|{CurrIntRate}%;"
       IllusText = IllusText & "   |Guaranteed:|" & Format$(LS3_GUAR_INT_RATE * 100, "#0.00") & "%;"
    Else
       IllusText = IllusText & "10.|Interest Rate:|{CurrIntRate}%;"
    End If
    IllusText = IllusText & "11.|State Written:|{StateWritten};"
    Call Insert_Variables
    frmVSPrint.vsPrint.Table = IllusFormat & IllusText
    
    frmVSPrint.vsPrint.MarginLeft = 720
    frmVSPrint.vsPrint.MarginRight = 720
    frmVSPrint.vsPrint.LineSpacing = 100
    frmVSPrint.vsPrint.FontBold = False
    frmVSPrint.vsPrint.TextAlign = 0
    IllusText = NL & "This policy also includes the following riders:" & NL
    If LSClient(s).State = "PA" Then
       IllusText = IllusText & NL & "Premature Death Accelerated Benefit"
    Else
       IllusText = IllusText & NL & "Accelerated Benefit"
    End If
    If LSClient(s).State = "SD" Then IllusText = IllusText & " (form A-3389)"
    If LSClient(s).ChildRider = True Or LSClient(s).GPO <> 0 Or LSClient(s).WPD = True Or LSClient(s).InsuredRider(1) = True Then
       If LSClient(s).InsuredRider(1) = True Then
          IllusText = IllusText & NL & "{InsdsCertif}"
       End If
       If LSClient(s).ChildRider = True Then
          IllusText = IllusText & "{ChildCertif}"
       End If
       If LSClient(s).WPD = True Then
          IllusText = IllusText & NL & "Disability Waiver of Cost of Insurance "
          If LSClient(s).State = "SD" Then
             IllusText = IllusText & " (form A-3392)"
          Else
             IllusText = IllusText & " Rider"
          End If
       End If
       If LSClient(s).GPO > 0 Then
          IllusText = IllusText & NL & "Guaranteed Added Death Benefit"
          If LSClient(s).State = "SD" Then
             IllusText = IllusText & " (form A-3388):"
          Else
             IllusText = IllusText & " Rider:"
          End If
          IllusText = IllusText & "  {GPOAmount}"
       End If
       Call Insert_Variables
    End If
    frmVSPrint.vsPrint.Paragraph = IllusText

    IllusText = NL & "Future Planned Changes:" & NL
    If LSClient(s).FutureChanges = False And LSClient(s).FutureWD = False Then
       IllusText = IllusText & NL & "(None)"
    Else
       IllusText = IllusText & NL & "{ChangesCertif}"
       Call Insert_Variables
    End If
    frmVSPrint.vsPrint.Paragraph = IllusText

    If StandAlone = True And frmVSPrint.vsPrint.CurrentY > 7920 Then GoSub Certif_Page_Break
    If StandAlone = False And frmVSPrint.vsPrint.CurrentY > 9620 Then GoSub Certif_Page_Break

    IllusText = NL & "Policy Summary" & NL
    frmVSPrint.vsPrint.TextAlign = 1
    frmVSPrint.vsPrint.FontBold = True
    frmVSPrint.vsPrint.Paragraph = IllusText

    CertifTable = 2
    frmVSPrint.vsPrint.TextAlign = 0
    frmVSPrint.vsPrint.LineSpacing = 150
    frmVSPrint.vsPrint.FontBold = False
    IllusHeader = " |Guaranteed Values|Nonguaranteed Assumptions (Current);"
    IllusFormat = "^2160|^4100|^4100;"
    IllusText = ""
    frmVSPrint.vsPrint.MarginLeft = 720
    frmVSPrint.vsPrint.MarginRight = 720
    frmVSPrint.vsPrint.TableBorder = 7
    frmVSPrint.vsPrint.Table = IllusFormat & IllusHeader
    IllusHeader = "Policy Year or" & NL & "Attained Age|Cash Surrender Value|Net Death Benefit|Cash Surrender Value|Net Death Benefit;"
    IllusFormat = "^2160|^2050|^2050|^2050|^2050;"
    frmVSPrint.vsPrint.Table = IllusFormat & IllusHeader
    IllusHeader = ""
    IllusFormat = "^2160|>2050|>2050|>2050|>2050;"
    
    j = 5
    Do While (j <= 20)
       k = ClientBeginAge + j
       If k <= 95 Then
          IllusText = IllusText & "End of Year " & Format$(j, "##") & "|"
          IllusText = IllusText & Format$(LSLedger(k).SurrenderValue(0), "##,###,##0") & "|" & Format$(LSLedger(k).DeathBenefit(0), "##,###,##0") & "|"
          IllusText = IllusText & Format$(LSLedger(k).SurrenderValue(1), "##,###,##0") & "|" & Format$(LSLedger(k).DeathBenefit(1), "##,###,##0") & ";"
       Else
          IllusText = IllusText & "End of Year " & Format$(j, "##") & "|"
          IllusText = IllusText & " N/A| N/A|"
          IllusText = IllusText & " N/A| N/A|;"
       End If
       j = j * 2
    Loop
    If ClientBeginAge <= 65 Then
       IllusText = IllusText & "At Age 65" & "|"
       IllusText = IllusText & Format$(LSLedger(65).SurrenderValue(0), "##,###,##0") & "|" & Format$(LSLedger(65).DeathBenefit(0), "##,###,##0") & "|"
       IllusText = IllusText & Format$(LSLedger(65).SurrenderValue(1), "##,###,##0") & "|" & Format$(LSLedger(65).DeathBenefit(1), "##,###,##0") & ";"
    Else
       IllusText = IllusText & "At Age 65" & "|"
       IllusText = IllusText & " N/A| N/A|"
       IllusText = IllusText & " N/A| N/A|;"
    End If
    If ClientBeginAge <= 70 Then
       IllusText = IllusText & "At Age 70" & "|"
       IllusText = IllusText & Format$(LSLedger(70).SurrenderValue(0), "##,###,##0") & "|" & Format$(LSLedger(70).DeathBenefit(0), "##,###,##0") & "|"
       IllusText = IllusText & Format$(LSLedger(70).SurrenderValue(1), "##,###,##0") & "|" & Format$(LSLedger(70).DeathBenefit(1), "##,###,##0") & ";"
    Else
       IllusText = IllusText & "At Age 70" & "|"
       IllusText = IllusText & " N/A| N/A|"
       IllusText = IllusText & " N/A| N/A|;"
    End If
    IllusText = IllusText & "At Age 95" & "|"
    IllusText = IllusText & Format$(LSLedger(95).SurrenderValue(0), "##,###,##0") & "|" & Format$(LSLedger(95).DeathBenefit(0), "##,###,##0") & "|"
    IllusText = IllusText & Format$(LSLedger(95).SurrenderValue(1), "##,###,##0") & "|" & Format$(LSLedger(95).DeathBenefit(1), "##,###,##0") & ";"
 
    frmVSPrint.vsPrint.Table = IllusFormat & IllusText
    frmVSPrint.vsPrint.LineSpacing = 100

    If StandAlone Then
       frmVSPrint.vsPrint.TextAlign = 0
       IllusText = NL & NL & "Date: "
       frmVSPrint.vsPrint.Text = IllusText
       frmVSPrint.vsPrint.X1 = frmVSPrint.vsPrint.CurrentX
       frmVSPrint.vsPrint.Y1 = frmVSPrint.vsPrint.CurrentY + 200
       frmVSPrint.vsPrint.X2 = frmVSPrint.vsPrint.X1 + 2160
       frmVSPrint.vsPrint.Y2 = frmVSPrint.vsPrint.Y1
       frmVSPrint.vsPrint.Draw = 1
       frmVSPrint.vsPrint.CurrentX = frmVSPrint.vsPrint.X2 + 1240
       IllusText = "Agent's Signature:  "
       frmVSPrint.vsPrint.Text = IllusText
       frmVSPrint.vsPrint.X1 = frmVSPrint.vsPrint.CurrentX
       frmVSPrint.vsPrint.Y1 = frmVSPrint.vsPrint.CurrentY + 200
       frmVSPrint.vsPrint.X2 = frmVSPrint.vsPrint.X1 + 4910
       frmVSPrint.vsPrint.Y2 = frmVSPrint.vsPrint.Y1
       frmVSPrint.vsPrint.Draw = 1
    
       IllusText = NL & NL & NL & "I acknowledge that I viewed a computer screen illustration based on the information as stated above.  No hard copy of the illustration "
       IllusText = IllusText & "was furnished.  I understand that an illustration based on the policy as issued will be provided to me no later than at the time the policy is delivered."
       IllusText = IllusText & NL & NL & NL & "Date: "
       frmVSPrint.vsPrint.Text = IllusText
       frmVSPrint.vsPrint.X1 = frmVSPrint.vsPrint.CurrentX
       frmVSPrint.vsPrint.Y1 = frmVSPrint.vsPrint.CurrentY + 200
       frmVSPrint.vsPrint.X2 = frmVSPrint.vsPrint.X1 + 2160
       frmVSPrint.vsPrint.Y2 = frmVSPrint.vsPrint.Y1
       frmVSPrint.vsPrint.Draw = 1
       frmVSPrint.vsPrint.CurrentX = frmVSPrint.vsPrint.X2 + 1240
       IllusText = "Applicant's Signature:  "
       frmVSPrint.vsPrint.Text = IllusText
       frmVSPrint.vsPrint.X1 = frmVSPrint.vsPrint.CurrentX
       frmVSPrint.vsPrint.Y1 = frmVSPrint.vsPrint.CurrentY + 200
       frmVSPrint.vsPrint.X2 = frmVSPrint.vsPrint.X1 + 4610
       frmVSPrint.vsPrint.Y2 = frmVSPrint.vsPrint.Y1
       frmVSPrint.vsPrint.Draw = 1
    End If

    frmVSPrint.vsPrint.TextAlign = 0
    frmVSPrint.vsPrint.FontSize = 8
    frmVSPrint.vsPrint.CurrentX = 720
    frmVSPrint.vsPrint.MarginLeft = 720
    frmVSPrint.vsPrint.MarginRight = 8300
    IllusText = "Z-1224"
    frmVSPrint.vsPrint.CurrentY = 14330
    frmVSPrint.vsPrint.Paragraph = IllusText
    
    Exit Sub

Certif_Page_Break:
    
    frmVSPrint.vsPrint.Action = 4    'New Page
    IllusText = "Universal Life Insurance" & NL & "Computer Screen Illustration Certification Form (Continued)"
    frmVSPrint.vsPrint.FontBold = True
    frmVSPrint.vsPrint.TextAlign = 1
    frmVSPrint.vsPrint.FontSize = 12
    frmVSPrint.vsPrint.CurrentY = 560
    frmVSPrint.vsPrint.MarginLeft = 720
    frmVSPrint.vsPrint.MarginRight = 720
    frmVSPrint.vsPrint.Paragraph = IllusText
    frmVSPrint.vsPrint.CurrentY = frmVSPrint.vsPrint.CurrentY + 560
    Return

End Sub

Sub Print_Footer ()
    
    IllusText = "Presented by: {AgentName}" & NL & "{CurrentDate}"
    Call Insert_Variables
    frmVSPrint.vsPrint.FontBold = False
    frmVSPrint.vsPrint.TextAlign = 0
    frmVSPrint.vsPrint.FontSize = 9
    frmVSPrint.vsPrint.CurrentY = 14272
    frmVSPrint.vsPrint.MarginLeft = 720
    frmVSPrint.vsPrint.MarginRight = 720
    frmVSPrint.vsPrint.Paragraph = IllusText

    IllusText = "This is page {CurrPage} of {TotalPage} and is not valid" & NL & "unless all pages are included."
    Call Insert_Variables
    frmVSPrint.vsPrint.TextAlign = 2
    frmVSPrint.vsPrint.CurrentY = 14272
    frmVSPrint.vsPrint.MarginLeft = 720
    frmVSPrint.vsPrint.MarginRight = 720
    frmVSPrint.vsPrint.Paragraph = IllusText

End Sub

Sub Print_Form_Number ()
       
    frmVSPrint.vsPrint.FontBold = False
    frmVSPrint.vsPrint.TextAlign = 0
    frmVSPrint.vsPrint.FontSize = 8
    frmVSPrint.vsPrint.CurrentX = 720
    frmVSPrint.vsPrint.MarginLeft = 720
    frmVSPrint.vsPrint.MarginRight = 8300
    If IllusCurrPage = IllusTotalPages Then
       IllusText = "Form A-3381, A-3389"
       If LSClient(s).WPD = True Then IllusText = IllusText & ", A-3392"
       If LSClient(s).GPO > 0 Then IllusText = IllusText & ", A-3388"
       If LSClient(s).COLA > 0 Then IllusText = IllusText & ", A-3391"
       If LSClient(s).ChildRider = True Then IllusText = IllusText & ", A-3387"
       If LSClient(s).InsuredRider(1) = True Then IllusText = IllusText & ", A-3386"
       frmVSPrint.vsPrint.Measure = IllusText
       If frmVSPrint.vsPrint.TextWid > 2400 Then
          frmVSPrint.vsPrint.CurrentY = 13922 - frmVSPrint.vsPrint.TextHei
       Else
          frmVSPrint.vsPrint.CurrentY = 14162 - frmVSPrint.vsPrint.TextHei
       End If
    Else
       IllusText = "Form A-3381"
       If IllusCurrPage = 0 Then
          frmVSPrint.vsPrint.CurrentY = 14750
       Else
          frmVSPrint.vsPrint.CurrentY = 13980
       End If
    End If
    frmVSPrint.vsPrint.Paragraph = IllusText

End Sub

Sub Print_Header ()
    
    IllusText = "AMERICAN REPUBLIC INSURANCE COMPANY"
    frmVSPrint.vsPrint.FontBold = False
    frmVSPrint.vsPrint.TextAlign = 1
    frmVSPrint.vsPrint.FontSize = 14
    frmVSPrint.vsPrint.CurrentY = 560
    frmVSPrint.vsPrint.MarginLeft = 720
    frmVSPrint.vsPrint.MarginRight = 720
    frmVSPrint.vsPrint.Paragraph = IllusText
    IllusText = "LifeSavings{TradeMark} Universal Life Insurance"
    Call Insert_Variables
    frmVSPrint.vsPrint.FontBold = False
    frmVSPrint.vsPrint.TextAlign = 1
    frmVSPrint.vsPrint.FontSize = 12
    frmVSPrint.vsPrint.CurrentY = 880
    frmVSPrint.vsPrint.MarginLeft = 720
    frmVSPrint.vsPrint.MarginRight = 720
    frmVSPrint.vsPrint.Paragraph = IllusText
    IllusText = "A Flexible Premium Adjustable Life Insurance Policy"
    frmVSPrint.vsPrint.FontBold = False
    frmVSPrint.vsPrint.TextAlign = 1
    frmVSPrint.vsPrint.FontSize = 12
    frmVSPrint.vsPrint.CurrentY = 1160
    frmVSPrint.vsPrint.MarginLeft = 720
    frmVSPrint.vsPrint.MarginRight = 720
    frmVSPrint.vsPrint.Paragraph = IllusText

End Sub

Sub Print_Illustration ()
    
    frmVSPrint.vsPrint.PageBorder = 0
    frmVSPrint.vsPrint.FontName = LS3FontDefault
    frmVSPrint.vsPrint.FontBold = False
    frmVSPrint.vsPrint.FontSize = 10
    frmVSPrint.vsPrint.MarginTop = 0
    frmVSPrint.vsPrint.MarginBottom = 0
    frmVSPrint.vsPrint.LineSpacing = 100
    frmVSPrint.vsPrint.PhysicalPage = True

    NL = Chr$(13) & Chr$(10)
    s = SpouseClient
    CertifTable = 0       'Set to 0 for illustration print

    If Val(frmLSPREM.txtBeginYear.Text) <> 0 Then
       ClientBeginAge = Val(frmLSPREM.txtBeginYear.Text)
    Else
       ClientBeginAge = LSClient(s).IssueAge
    End If

    If ClientBeginAge > 64 Then
       IllusTotalPages = 5
    ElseIf ClientBeginAge > 29 Then
       IllusTotalPages = 6
    Else
       IllusTotalPages = 7
    End If
    If LSClient(s).IllusType <> 195 Then   'adjust total pages for print option
       If ClientBeginAge < 55 Then
          IllusTotalPages = 6
       Else
          IllusTotalPages = 5
       End If
    End If
              
    Call Check_State_Variations   'Sets US_Only, updates IllusTotalPages for some states

    Set IllusDB = OpenDatabase("ls3\ls3illus.MDB", True, True)
    'Set IllusMask = Workspaces(0).OpenDatabase(CurDir & "\ls3\ls3illus.MDB", True, True)
    Set IllusTD = IllusDB.TableDefs
    
    ' "IllusCurrPage" is counter for current print page.  "i" is counter
    ' for logical pages on the text database.  These two variables are
    ' NOT NECESSARILY THE SAME!  Page 3 on the database may be repeated
    ' several times in the course of printing the illustration.
    
    IllusCurrPage = 0
    i = 0
    IllusBeginAge = ClientBeginAge + 1

    Do While (IllusCurrPage <= IllusTotalPages)
        TableName = "LS3P" & Mid$(Str$(i), 2)
        If i > 5 Then
           MsgText = "Illustration display error; ledger not complete.  Please RETURN to Client display and re-illustrate using 'short' form"
           MsgBox MsgText, , "LifeSavings"
           Exit Sub
        End If
        Set IllusFld = IllusTD(TableName).Fields
        Set IllusDS = IllusDB.CreateDynaset(TableName)
        'Set IllusTD = IllusMask.OpenRecordset(TableName, dbOpenTable)
        frmVSPrint.vsPrint.PenWidth = 2
        If IllusCurrPage > 0 Then
           'frmVSPrint.vsPrint.NewPage
           frmVSPrint.vsPrint.Action = 4
           frmVSPrint.vsPrint.X1 = 720
           frmVSPrint.vsPrint.Y1 = 2376
           frmVSPrint.vsPrint.X2 = 11520
           frmVSPrint.vsPrint.Y2 = 2376
           frmVSPrint.vsPrint.Draw = 1
           frmVSPrint.vsPrint.X1 = 3590
           frmVSPrint.vsPrint.Y1 = 2448
           frmVSPrint.vsPrint.X2 = 3590
           frmVSPrint.vsPrint.Y2 = 14056
           frmVSPrint.vsPrint.Draw = 1
           frmVSPrint.vsPrint.X1 = 720
           frmVSPrint.vsPrint.Y1 = 14200
           frmVSPrint.vsPrint.X2 = 11520
           frmVSPrint.vsPrint.Y2 = 14200
           frmVSPrint.vsPrint.Draw = 1
           'frmVSPrint.vsPrint.DrawLine 720, 2376, 11088, 2376
           'frmVSPrint.vsPrint.DrawLine 3528, 2448, 3528, 14056
           'frmVSPrint.vsPrint.DrawLine 720, 14200, 11088, 14200
           Call Print_Header
           Select Case i
              Case 3
                 Call Illustration_Table
              Case 4
                 Call Cost_Index_Table
              Case 5
                 Call Summary_Table
              Case Else
           End Select
        End If
        Do Until IllusDS.EOF
           If IllusDS!State = "US" Or IllusDS!State = LSClient(s).State Then
              IllusText = IllusDS!Text
              If i = 3 And IllusDS!Paragraph >= 5 And IllusDS!Paragraph <= 18 Then
                 frmVSPrint.vsPrint.LineSpacing = 89   'saves approximately 7 linespaces per page
              Else
                 frmVSPrint.vsPrint.LineSpacing = 100
              End If
              If IllusDS!OptionalID = 0 Then
                 If IllusDS!FontName = "Default" Then
                    frmVSPrint.vsPrint.FontName = LS3FontDefault
                 Else
                    frmVSPrint.vsPrint.FontName = IllusDS!FontName
                 End If
                 frmVSPrint.vsPrint.FontBold = IllusDS!FontBold
                 frmVSPrint.vsPrint.TextAlign = IllusDS!Align
                 frmVSPrint.vsPrint.FontSize = IllusDS!FontSize
                 Select Case (IllusDS!CurrentY)
                    Case 0                     'Leave CurrentY alone
                    Case 1                     'Use CurrentY from last paragraph (aligns columns)
                       frmVSPrint.vsPrint.CurrentY = SaveCurrentY
                    Case 2                     'Use CurrentY + 1 linespace
                       frmVSPrint.vsPrint.CurrentY = frmVSPrint.vsPrint.CurrentY + (frmVSPrint.vsPrint.FontSize * 22.5)
                    Case Is > 1000             'Use CurrentY as specifically set in table
                       frmVSPrint.vsPrint.CurrentY = IllusDS!CurrentY
                    Case Else                  'Add specific amount (3 to 999 twips) to CurrentY
                       frmVSPrint.vsPrint.CurrentY = frmVSPrint.vsPrint.CurrentY + IllusDS!CurrentY
                 End Select
                 frmVSPrint.vsPrint.CurrentX = IllusDS!LMargin
                 frmVSPrint.vsPrint.MarginLeft = IllusDS!LMargin
                 frmVSPrint.vsPrint.MarginRight = IllusDS!RMargin
                 Call Insert_Variables
                 frmVSPrint.vsPrint.MarginRight = frmVSPrint.vsPrint.MarginRight + BumpRMargin
                 SaveCurrentY = frmVSPrint.vsPrint.CurrentY
                 frmVSPrint.vsPrint.Paragraph = IllusText
              Else
                 Call Optional_Paragraphs
              End If
           End If
           BumpRMargin = 0                    'Reset to 0 for each new table variable
           IllusDS.MoveNext
        Loop
        Call Print_Form_Number
        If IllusCurrPage > 0 Then Call Print_Footer
        IllusDS.Close
        IllusCurrPage = IllusCurrPage + 1
        If i = 2 And US_Only = False Then Call State_Variations
        If IllusBeginAge = ClientBeginAge + 1 Then i = i + 1
    Loop
    
    IllusDB.Close
    frmVSPrint.vsPrint.Action = 4    'New Page, still have to print certification
    
End Sub

Sub State_Variations ()
    
     TableName = "LS3ST"
     Set IllusDS = IllusDB.CreateDynaset(TableName)
     Set IllusFld = IllusTD(TableName).Fields
     frmVSPrint.vsPrint.PenWidth = 2
     'frmVSPrint.vsPrint.NewPage
     frmVSPrint.vsPrint.Action = 4
     frmVSPrint.vsPrint.X1 = 720
     frmVSPrint.vsPrint.Y1 = 2376
     frmVSPrint.vsPrint.X2 = 11088
     frmVSPrint.vsPrint.Y2 = 2376
     frmVSPrint.vsPrint.Draw = 1
     frmVSPrint.vsPrint.X1 = 3528
     frmVSPrint.vsPrint.Y1 = 2448
     frmVSPrint.vsPrint.X2 = 3528
     frmVSPrint.vsPrint.Y2 = 14056
     frmVSPrint.vsPrint.Draw = 1
     frmVSPrint.vsPrint.X1 = 720
     frmVSPrint.vsPrint.Y1 = 14200
     frmVSPrint.vsPrint.X2 = 11088
     frmVSPrint.vsPrint.Y2 = 14200
     frmVSPrint.vsPrint.Draw = 1
     Call Print_Header
        
     Do Until IllusDS.EOF
        If LSClient(s).State = IllusDS!State Or IllusDS!State = "US" Then
           IllusText = IllusDS!Text
           If IllusDS!OptionalID = 0 Then
              If IllusDS!FontName = "Default" Then
                 frmVSPrint.vsPrint.FontName = LS3FontDefault
              Else
                 frmVSPrint.vsPrint.FontName = IllusDS!FontName
              End If
              frmVSPrint.vsPrint.FontBold = IllusDS!FontBold
              frmVSPrint.vsPrint.TextAlign = IllusDS!Align
              frmVSPrint.vsPrint.FontSize = IllusDS!FontSize
              Select Case (IllusDS!CurrentY)
                 Case 0                     'Leave CurrentY alone
                 Case 1                     'Use CurrentY from last paragraph (aligns columns)
                    frmVSPrint.vsPrint.CurrentY = SaveCurrentY
                 Case 2                     'Use CurrentY + 1 linespace
                    frmVSPrint.vsPrint.CurrentY = frmVSPrint.vsPrint.CurrentY + (frmVSPrint.vsPrint.FontSize * 22.5)
                 Case Is > 1000             'Use CurrentY as specifically set in table
                    frmVSPrint.vsPrint.CurrentY = IllusDS!CurrentY
                 Case Else                  'Add specific amount (3 to 999 twips) to CurrentY
                    frmVSPrint.vsPrint.CurrentY = frmVSPrint.vsPrint.CurrentY + IllusDS!CurrentY
              End Select
              frmVSPrint.vsPrint.CurrentX = IllusDS!LMargin
              frmVSPrint.vsPrint.MarginLeft = IllusDS!LMargin
              frmVSPrint.vsPrint.MarginRight = IllusDS!RMargin
              Call Insert_Variables
              frmVSPrint.vsPrint.MarginRight = frmVSPrint.vsPrint.MarginRight + BumpRMargin
              SaveCurrentY = frmVSPrint.vsPrint.CurrentY
              frmVSPrint.vsPrint.Paragraph = IllusText
           Else
              Call Optional_Paragraphs
           End If
        End If
        BumpRMargin = 0                    'Reset to 0 for each new table variable
        IllusDS.MoveNext
     Loop
     Call Print_Form_Number
     Call Print_Footer
     IllusDS.Close
     IllusCurrPage = IllusCurrPage + 1

End Sub

Sub Summary_Table ()

    Dim IllusHeader As String
    Dim IllusFormat As String
    Dim TableY As Long
    Dim TableX As Long
  
    If LSClient(s).State <> "MI" Then
       IllusHeader = " |Guaranteed Values|Nonguaranteed Assumptions;"
       IllusFormat = "^2475|^1675|^+3350;"
    Else
       IllusHeader = " |Guaranteed Values|Current Assumptions;"
       IllusFormat = "^2475|^2512|^2513;"
    End If
    IllusText = ""
    frmVSPrint.vsPrint.FontBold = False
    frmVSPrint.vsPrint.TextAlign = 2
    frmVSPrint.vsPrint.FontSize = 12
    frmVSPrint.vsPrint.CurrentY = 4320
    frmVSPrint.vsPrint.CurrentX = 3320
    frmVSPrint.vsPrint.MarginLeft = 3320
    frmVSPrint.vsPrint.MarginRight = 480
    frmVSPrint.vsPrint.TableBorder = 6
    frmVSPrint.vsPrint.CalcTable = IllusFormat & IllusHeader
    TableY = frmVSPrint.vsPrint.Y2
    TableX = frmVSPrint.vsPrint.X2
    If LSClient(s).State <> "MI" Then
       IllusHeader = IllusHeader & " | | ; | | ;"
    Else
       IllusHeader = IllusHeader & " | ; | ;"
    End If

    frmVSPrint.vsPrint.Table = IllusFormat & IllusHeader
    If LSClient(s).State <> "MI" Then
       IllusHeader = " |Midpoint Assumption|Current Assumption;"
       IllusFormat = "^4150|^1675|^1675;"
       frmVSPrint.vsPrint.TableBorder = 5
       frmVSPrint.vsPrint.CurrentY = TableY
       frmVSPrint.vsPrint.CurrentX = TableX
       frmVSPrint.vsPrint.Table = IllusFormat & IllusHeader
       frmVSPrint.vsPrint.X1 = TableX - 3350
       frmVSPrint.vsPrint.Y1 = TableY
       frmVSPrint.vsPrint.X2 = TableX
       frmVSPrint.vsPrint.Y2 = TableY
       frmVSPrint.vsPrint.Draw = 1
       'frmVSPrint.vsPrint.DrawLine 7750, 4880, 11110, 4880
    End If

    frmVSPrint.vsPrint.TableBorder = 7
    IllusHeader = ""
    If LSClient(s).State <> "MI" Then
       IllusFormat = ">2475|>_1675|>_1675|>_1675;"
    Else
       IllusFormat = ">2475|>_2513|>_2512;"
    End If
    frmVSPrint.vsPrint.FontSize = 10
    
    j = 5
    Do While (j <= 20)
       If (j = 5 And ClientBeginAge >= 61 And ClientBeginAge <= 65) Or (j = 10 And ClientBeginAge >= 56 And ClientBeginAge <= 60) Or (j = 20 And ClientBeginAge >= 46 And ClientBeginAge <= 55) Then
          IllusText = IllusText & "Summary at Age 65" & NL & "Cash Surrender Value" & NL & "Net Death Benefit" & "|"
          IllusText = IllusText & Format$(LSLedger(65).SurrenderValue(0), "##,###,##0") & NL & Format$(LSLedger(65).DeathBenefit(0), "##,###,##0") & "|"
          If LSClient(s).State <> "MI" Then
             IllusText = IllusText & Format$(LSLedger(65).SurrenderValue(2), "##,###,##0") & NL & Format$(LSLedger(65).DeathBenefit(2), "##,###,##0") & "|"
          End If
          IllusText = IllusText & Format$(LSLedger(65).SurrenderValue(1), "##,###,##0") & NL & Format$(LSLedger(65).DeathBenefit(1), "##,###,##0") & ";"
       End If
       If (j = 5 And ClientBeginAge >= 66 And ClientBeginAge <= 70) Or (j = 10 And ClientBeginAge >= 61 And ClientBeginAge <= 65) Or (j = 20 And ClientBeginAge >= 51 And ClientBeginAge <= 60) Then
          IllusText = IllusText & "Summary at Age 70" & NL & "Cash Surrender Value" & NL & "Net Death Benefit" & "|"
          IllusText = IllusText & Format$(LSLedger(70).SurrenderValue(0), "##,###,##0") & NL & Format$(LSLedger(70).DeathBenefit(0), "##,###,##0") & "|"
          If LSClient(s).State <> "MI" Then
             IllusText = IllusText & Format$(LSLedger(70).SurrenderValue(2), "##,###,##0") & NL & Format$(LSLedger(70).DeathBenefit(2), "##,###,##0") & "|"
          End If
          IllusText = IllusText & Format$(LSLedger(70).SurrenderValue(1), "##,###,##0") & NL & Format$(LSLedger(70).DeathBenefit(1), "##,###,##0") & ";"
       End If
       k = ClientBeginAge + j
       If k <= 95 Then
          IllusText = IllusText & "Summary at End of Year " & Format$(j, "##") & NL & "Cash Surrender Value" & NL & "Net Death Benefit" & "|"
          IllusText = IllusText & Format$(LSLedger(k).SurrenderValue(0), "##,###,##0") & NL & Format$(LSLedger(k).DeathBenefit(0), "##,###,##0") & "|"
          If LSClient(s).State <> "MI" Then
             IllusText = IllusText & Format$(LSLedger(k).SurrenderValue(2), "##,###,##0") & NL & Format$(LSLedger(k).DeathBenefit(2), "##,###,##0") & "|"
          End If
          IllusText = IllusText & Format$(LSLedger(k).SurrenderValue(1), "##,###,##0") & NL & Format$(LSLedger(k).DeathBenefit(1), "##,###,##0") & ";"
       End If
       j = j * 2
    Loop
    If ClientBeginAge + 20 <= 65 Then
       IllusText = IllusText & "Summary at Age 65" & NL & "Cash Surrender Value" & NL & "Net Death Benefit" & "|"
       IllusText = IllusText & Format$(LSLedger(65).SurrenderValue(0), "##,###,##0") & NL & Format$(LSLedger(65).DeathBenefit(0), "##,###,##0") & "|"
       If LSClient(s).State <> "MI" Then
          IllusText = IllusText & Format$(LSLedger(65).SurrenderValue(2), "##,###,##0") & NL & Format$(LSLedger(65).DeathBenefit(2), "##,###,##0") & "|"
       End If
       IllusText = IllusText & Format$(LSLedger(65).SurrenderValue(1), "##,###,##0") & NL & Format$(LSLedger(65).DeathBenefit(1), "##,###,##0") & ";"
    End If
    If ClientBeginAge + 20 <= 70 Then
       IllusText = IllusText & "Summary at Age 70" & NL & "Cash Surrender Value" & NL & "Net Death Benefit" & "|"
       IllusText = IllusText & Format$(LSLedger(70).SurrenderValue(0), "##,###,##0") & NL & Format$(LSLedger(70).DeathBenefit(0), "##,###,##0") & "|"
       If LSClient(s).State <> "MI" Then
          IllusText = IllusText & Format$(LSLedger(70).SurrenderValue(2), "##,###,##0") & NL & Format$(LSLedger(70).DeathBenefit(2), "##,###,##0") & "|"
       End If
       IllusText = IllusText & Format$(LSLedger(70).SurrenderValue(1), "##,###,##0") & NL & Format$(LSLedger(70).DeathBenefit(1), "##,###,##0") & ";"
    End If
    IllusText = IllusText & "Summary at Age 95" & NL & "Cash Surrender Value" & NL & "Net Death Benefit" & "|"
    IllusText = IllusText & Format$(LSLedger(95).SurrenderValue(0), "##,###,##0") & NL & Format$(LSLedger(95).DeathBenefit(0), "##,###,##0") & "|"
    If LSClient(s).State <> "MI" Then
       IllusText = IllusText & Format$(LSLedger(95).SurrenderValue(2), "##,###,##0") & NL & Format$(LSLedger(95).DeathBenefit(2), "##,###,##0") & "|"
    End If
    IllusText = IllusText & Format$(LSLedger(95).SurrenderValue(1), "##,###,##0") & NL & Format$(LSLedger(95).DeathBenefit(1), "##,###,##0") & ";"
    IllusText = IllusText & " |"
    If LSLedger(95).CashValue(0) > 0 Then
       IllusText = IllusText & " Coverage remains in force to age 95" & "|"
    Else
       For j = 95 To ClientBeginAge Step -1
         If LSLedger(j).CashValue(0) > 0 Then
            IllusText = IllusText & " Coverage ceases at age " & Format$(j, "##") & "|"
            j = 0     'forces end of loop
         End If
       Next j
    End If
    If LSClient(s).State <> "MI" Then
       If LSLedger(95).CashValue(2) > 0 Then
          IllusText = IllusText & " Coverage remains in force to age 95" & "|"
       Else
          For j = 95 To ClientBeginAge Step -1
            If LSLedger(j).CashValue(2) > 0 Then
               IllusText = IllusText & " Coverage ceases at age " & Format$(j, "##") & "|"
               j = 0     'forces end of loop
            End If
          Next j
       End If
    End If
    If LSLedger(95).CashValue(1) > 0 Then
       IllusText = IllusText & " Coverage remains in force to age 95" & ";"
    Else
       For j = 95 To ClientBeginAge Step -1
         If LSLedger(j).CashValue(1) > 0 Then
            IllusText = IllusText & " Coverage ceases at age " & Format$(j, "##") & ";"
            j = 0     'forces end of loop
         End If
       Next j
    End If
 
    frmVSPrint.vsPrint.Table = IllusFormat & IllusText

End Sub

