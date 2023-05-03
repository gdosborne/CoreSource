
   Option Explicit
   
   Type Life_HW_Values
      WeightMin As Integer
      WeightMax As Integer
      WeightMin0 As Integer
      WeightMax0 As Integer
      WeightMin2 As Integer
      WeightMax2 As Integer
      WeightMin3 As Integer
      WeightMax3 As Integer
      WeightMin4 As Integer
      WeightMax4 As Integer
      WeightMin5 As Integer
      WeightMax5 As Integer
      WeightMin6 As Integer
      WeightMax6 As Integer
      WeightMin8 As Integer
      WeightMax8 As Integer
      WeightMin10 As Integer
      WeightMax10 As Integer
      WeightMin12 As Integer
      WeightMax12 As Integer
   End Type

   Dim LHtWt As Life_HW_Values

   Type Life_LS3COI_Values
      MaleNS(1 To 2) As Double
      MaleSM(1 To 2) As Double
      FemaleNS(1 To 2) As Double
      FemaleSM(1 To 2) As Double
   End Type

   Dim PrinRate As Life_LS3COI_Values

   Type Life_LSRATEGP_Values
      GPO As Single
   End Type

   Dim GPORate As Life_LSRATEGP_Values

   Type Life_LSRATEWP_Values
      MaleWPD(1) As Single     'Guar, Curr
      FemaleWPD(1) As Single   'Guar, Curr
   End Type

   Dim WPDRate As Life_LSRATEWP_Values

   Type Life_LSRATESB_Values
      SmokerSub As Single
      NonSmokerSub As Single
   End Type

   Dim SubRate As Life_LSRATESB_Values

   Type Life_LSSURR_Values
      MaleNSSurr(0 To 15) As Single
      MaleSMSurr(0 To 15) As Single
      FemaleNSSurr(0 To 15) As Single
      FemaleSMSurr(0 To 15) As Single
   End Type

   Dim SurrRate As Life_LSSURR_Values

   Type Life_LSMINP_Values
      MaleNSMinp(1 To 2) As Single
      MaleSMMinp(1 To 2) As Single
      FemaleNSMinp(1 To 2) As Single
      FemaleSMMinp(1 To 2) As Single
   End Type

   Dim MinpRate As Life_LSMINP_Values

   Type Life_LSTARG_Values
      MaleNSTarg(1 To 2) As Single
      MaleSMTarg(1 To 2) As Single
      FemaleNSTarg(1 To 2) As Single
      FemaleSMTarg(1 To 2) As Single
   End Type

   Dim TargRate As Life_LSTARG_Values

   Type Life_CSO80_QValues
      MaleNS As Double
      MaleSM As Double
      FemaleNS As Double
      FemaleSM As Double
   End Type

   Dim CSO80Rate As Life_CSO80_QValues
  
   Dim NL As String * 2
   Dim Today As Long
   Dim s As Integer
   Dim i As Integer
   Dim j As Integer
   Dim k As Integer
   Dim RecordNo As Integer

Sub Add_COLA (Who As String, Which As Integer)

    Dim TempAge As Integer
    Dim OneYearCOLA As Single

    OneYearCOLA = 0
    Select Case Who
       Case "P"     'Principal COLA
          If LS_COLA_Client < 200000 And LS_COLA_Client < LSClient(SpouseClient).FaceAmt * 2 And AnnYr < 64 Then
             OneYearCOLA = (LS_DB(GuarCurr) + LS_COLA_Client) * (LSClient(SpouseClient).COLA / 100)
             If OneYearCOLA > (LS_DB(GuarCurr) + LS_COLA_Client) * .2 Then OneYearCOLA = (LS_DB(GuarCurr) + LS_COLA_Client) * .2
             If OneYearCOLA > 25000 Then OneYearCOLA = 25000
             LS_COLA_Client = LS_COLA_Client + OneYearCOLA
          End If
          If LS_COLA_Client > 200000 Then LS_COLA_Client = 200000
          If LS_COLA_Client > (LSClient(SpouseClient).FaceAmt * 2) Then LS_COLA_Client = LSClient(SpouseClient).FaceAmt * 2
       Case "I"     'Additional Insured COLA
          TempAge = LSClient(SpouseClient).IssueAge - AnnYr + LSInsuredRider(Which).IssueAge
          If LS_COLA_Insured(Which) < 200000 And LS_COLA_Insured(Which) < LSInsuredRider(Which).FaceAmt * 2 And TempAge < 64 Then
             OneYearCOLA = (LSInsuredRider(Which).FaceAmt + LS_COLA_Insured(Which)) * (LSInsuredRider(Which).COLA / 100)
             If OneYearCOLA > LSInsuredRider(Which).FaceAmt * .2 Then OneYearCOLA = LSInsuredRider(Which).FaceAmt * .2
             If OneYearCOLA > 25000 Then OneYearCOLA = 25000
             LS_COLA_Spouse = LS_COLA_Insured(Which) + OneYearCOLA
             LS_COLA_Insured(Which) = LS_COLA_Insured(Which) + OneYearCOLA
          End If
          If LS_COLA_Insured(Which) > 200000 Then LS_COLA_Insured(Which) = 200000
          If LS_COLA_Insured(Which) > LSInsuredRider(Which).FaceAmt * 2 Then LS_COLA_Insured(Which) = LSInsuredRider(Which).FaceAmt * 2
       Case Else
    End Select

End Sub

Sub Add_Modal_Premium ()

    s = SpouseClient
    
    LS_Charges = LS3_MONTHLY_EXPENSE

    If AnnYr < LSClient(s).IssueAge + LSClient(s).YearsToPay Then
       If (AnnMo = 1 And LSClient(s).Mode = "A") Or ((AnnMo = 1 Or AnnMo = 7) And LSClient(s).Mode = "S") Or ((AnnMo = 1 Or AnnMo = 4 Or AnnMo = 7 Or AnnMo = 10) And LSClient(s).Mode = "Q") Or LSClient(s).Mode = "M" Then
          LS_Outlay = LS_Outlay + LS_Modal_Prem
          LS_Monthly_Prem = LS_Modal_Prem
          LS_CV(GuarCurr) = LS_CV(GuarCurr) + LS_Modal_Prem
       Else
          LS_Monthly_Prem = 0
       End If
       If (AnnMo = 1 And LSClient(s).Mode = "A") Or ((AnnMo = 1 Or AnnMo = 7) And LSClient(s).Mode = "S") Or ((AnnMo = 1 Or AnnMo = 4 Or AnnMo = 7 Or AnnMo = 10) And LSClient(s).Mode = "Q") Or LSClient(s).Mode = "M" Then
          Select Case (GuarCurr)
             Case (0)
                If LSClient(s).FaceAmt < 100000 Then
                   LS_Charges = LS_Charges + (LS_Modal_Prem * .1)
                Else
                   LS_Charges = LS_Charges + (LS_Modal_Prem * .05)
                End If
             Case (1)
                If LSClient(s).FaceAmt < 100000 Then
                   LS_Charges = LS_Charges + (LS_Modal_Prem * .07)
                End If
             Case (2)
                If LSClient(s).FaceAmt < 100000 Then
                   LS_Charges = LS_Charges + (LS_Modal_Prem * .085)
                Else
                   LS_Charges = LS_Charges + (LS_Modal_Prem * .025)
                End If
             Case Else
          End Select
       End If
    Else
       LS_Monthly_Prem = 0
    End If
    
    LS_CV(GuarCurr) = LS_CV(GuarCurr) - LS_Charges

End Sub

Sub Calculate_Full_LS_Ledger ()

    Dim Begin_Age As Integer
    Dim Search As Integer
    Dim ScreenSum As Currency
    Dim ScreenValue As String

    s = SpouseClient
    
    LSLedger(LSClient(s).IssueAge + 1).SurrenderValue(0) = 0
    LSLedger(LSClient(s).IssueAge + 1).SurrenderValue(1) = 0
    LSLedger(LSClient(s).IssueAge + 1).SurrenderValue(2) = 0
    LSLedger(LSClient(s).IssueAge + 1).CashValue(0) = 0
    LSLedger(LSClient(s).IssueAge + 1).CashValue(1) = 0
    LSLedger(LSClient(s).IssueAge + 1).CashValue(2) = 0
    LSLedger(LSClient(s).IssueAge + 1).DeathBenefit(0) = LSClient(s).FaceAmt
    LSLedger(LSClient(s).IssueAge + 1).DeathBenefit(1) = LSClient(s).FaceAmt
    LSLedger(LSClient(s).IssueAge + 1).DeathBenefit(2) = LSClient(s).FaceAmt
    LSLedger(LSClient(s).IssueAge + 1).WithdrawAmount = 0
    LSLedger(LSClient(s).IssueAge + 1).LoanAmount = 0
    LSLedger(LSClient(s).IssueAge + 1).LoanRepayAmount = 0
    LSLedger(LSClient(s).IssueAge + 1).LoanBalance(0) = 0
    LSLedger(LSClient(s).IssueAge + 1).LoanBalance(1) = 0
    LSLedger(LSClient(s).IssueAge + 1).LoanBalance(2) = 0
    
    GuarCurr = 0

    If LSClient(s).LumpSum = 0 Then
       LS_CV(GuarCurr) = 0
    Else
       If LSClient(s).FaceAmt < 100000 Then
          LS_CV(GuarCurr) = LSClient(s).LumpSum * .9  'Guaranteed charge low band (10%)
       Else
          LS_CV(GuarCurr) = LSClient(s).LumpSum * .95  'Guaranteed charge high band (5%)
       End If
    End If

    LS_Outlay = LSClient(s).LumpSum
    LS_CSV(GuarCurr) = 0
    LS_DB(GuarCurr) = LSClient(s).FaceAmt
    LS_DB_Minimum(GuarCurr) = LSClient(s).FaceAmt
    LS_IRate(GuarCurr) = LS3_GUAR_INT_RATE
    LS_Modal_Prem = LSClient(s).PlannedPrem
    LS_Index_Numerator_NP(GuarCurr, 0) = 0
    LS_Index_Numerator_NP(GuarCurr, 1) = 0
    LS_Index_Numerator_SC(GuarCurr, 0) = 0
    LS_Index_Numerator_SC(GuarCurr, 1) = 0
    LS_Index_Denominator(GuarCurr, 0) = 0
    LS_Index_Denominator(GuarCurr, 1) = 0
    LS_COLA_Client = 0
    LS_COLA_Spouse = 0
    For i = 1 To 9
      LS_COLA_Insured(i) = 0
    Next i
    LS_LoanBalance(GuarCurr) = 0

    If Val(frmLSPREM.txtBeginYear.Text) <> 0 Then
       Begin_Age = Val(frmLSPREM.txtBeginYear.Text)
       LS_CV(0) = Val(frmLSPREM.txtBeginCashValue.Text)
       LS_DB(0) = Val(frmLSPREM.txtCurrentDB.Text)
       LS_DB_Minimum(0) = Val(frmLSPREM.txtCurrentDB.Text)
       LS_LoanBalance(0) = Val(frmLSPREM.txtBeginLoan.Text)
       Search = True
    Else
       Begin_Age = LSClient(s).IssueAge
       Search = False
    End If

    Call Process_Ledger_Years(Begin_Age, 94, Search)

    GuarCurr = 1
    LS_Outlay = LSClient(s).LumpSum
    LS_CSV(GuarCurr) = 0
    LS_DB(GuarCurr) = LSClient(s).FaceAmt
    LS_DB_Minimum(GuarCurr) = LSClient(s).FaceAmt
    LS_IRate(GuarCurr) = LSClient(s).CurrIntRate / 100
    LS_Modal_Prem = LSClient(s).PlannedPrem
    
    If LSClient(s).LumpSum = 0 Then
       LS_CV(GuarCurr) = 0
    Else
       If LSClient(s).FaceAmt < 100000 Then
          LS_CV(GuarCurr) = LSClient(s).LumpSum * .93    'Current charge low band (7%)
       Else
          LS_CV(GuarCurr) = LSClient(s).LumpSum          'Current charge high band (0%)
       End If
    End If
    LS_Index_Numerator_NP(GuarCurr, 0) = 0
    LS_Index_Numerator_NP(GuarCurr, 1) = 0
    LS_Index_Numerator_SC(GuarCurr, 0) = 0
    LS_Index_Numerator_SC(GuarCurr, 1) = 0
    LS_Index_Denominator(GuarCurr, 0) = 0
    LS_Index_Denominator(GuarCurr, 1) = 0
    LS_COLA_Client = 0
    LS_COLA_Spouse = 0
    For i = 1 To 9
      LS_COLA_Insured(i) = 0
    Next i
    LS_LoanBalance(GuarCurr) = 0
    
    If Val(frmLSPREM.txtBeginYear.Text) <> 0 Then
       Begin_Age = Val(frmLSPREM.txtBeginYear.Text)
       LS_CV(1) = Val(frmLSPREM.txtBeginCashValue.Text)
       LS_DB(1) = Val(frmLSPREM.txtCurrentDB.Text)
       LS_DB_Minimum(1) = Val(frmLSPREM.txtCurrentDB.Text)
       LS_LoanBalance(1) = Val(frmLSPREM.txtBeginLoan.Text)
       Search = True
    Else
       Begin_Age = LSClient(s).IssueAge
       Search = False
    End If

    If LSClient(s).PlannedPrem + LSClient(s).LumpSum < CalcPrem.TotMin - 1 Then
       frmLSPREM.txtInForceYears.Text = "be below Minimum"
       LS_Outlay = 0
       LS_Modal_Prem = 0
    End If
    
    Call Process_Ledger_Years(Begin_Age, 94, Search)
    
    If LSLedger(95).CashValue(1) > 0 Then
       frmLSPREM.txtInForceYears.Text = "ENDOW at age 95"
       ScreenSum = LSLedger(95).CashValue(1)
       Call Format_Amount(ScreenValue, 12, ScreenSum, " ")
       frmLSPREM.txtEndowCurr.Text = ScreenValue
    Else
       For i = 95 To LSClient(s).IssueAge Step -1
         If i = LSClient(s).IssueAge Or LSLedger(i).CashValue(1) > 0 Then
            frmLSPREM.txtInForceYears.Text = "LAPSE at age " & Format$(i + 1, "##")
            i = 0     'forces end of loop
         End If
       Next i
       frmLSPREM.txtEndowCurr.Text = "  N/A"
    End If
    
    If LSLedger(95).CashValue(0) > 0 Then
       ScreenSum = LSLedger(95).CashValue(0)
       Call Format_Amount(ScreenValue, 12, ScreenSum, " ")
       frmLSPREM.txtEndowGuar.Text = ScreenValue
    Else
       frmLSPREM.txtEndowGuar.Text = "  N/A"
    End If
    
   'run ledger calculations for midpoint amounts
    
    GuarCurr = 2
    LS_Outlay = LSClient(s).LumpSum
    LS_CSV(GuarCurr) = 0
    LS_DB(GuarCurr) = LSClient(s).FaceAmt
    LS_DB_Minimum(GuarCurr) = LSClient(s).FaceAmt
    LS_IRate(GuarCurr) = ((LSClient(s).CurrIntRate / 100) + LS_IRate(0)) / 2
    LS_Modal_Prem = LSClient(s).PlannedPrem
    LS_LoanBalance(GuarCurr) = 0
    
    If LSClient(s).LumpSum = 0 Then
       LS_CV(GuarCurr) = 0
    Else
       If LSClient(s).FaceAmt < 100000 Then
          LS_CV(GuarCurr) = LSClient(s).LumpSum * .915   'midpoint charge low band
       Else
          LS_CV(GuarCurr) = LSClient(s).LumpSum * .975   'midpoint charge high band
       End If
    End If
    LS_COLA_Client = 0
    LS_COLA_Spouse = 0
    For i = 1 To 9
      LS_COLA_Insured(i) = 0
    Next i
    
    If Val(frmLSPREM.txtBeginYear.Text) <> 0 Then
       Begin_Age = Val(frmLSPREM.txtBeginYear.Text)
       LS_CV(GuarCurr) = Val(frmLSPREM.txtBeginCashValue.Text)
       LS_DB(GuarCurr) = Val(frmLSPREM.txtCurrentDB.Text)
       LS_DB_Minimum(GuarCurr) = Val(frmLSPREM.txtCurrentDB.Text)
       LS_LoanBalance(GuarCurr) = Val(frmLSPREM.txtBeginLoan.Text)
       Search = True
    Else
       Begin_Age = LSClient(s).IssueAge
       Search = False
    End If

    Call Process_Ledger_Years(Begin_Age, 94, Search)
    
End Sub

Sub Calculate_Initial_Premiums ()

    frmLSPREM.MousePointer = 11
    Need_Guideline = False
    frmLSPREM.txtMinPrem.Text = ""
    frmLSPREM.txtTargPrem.Text = ""
    frmLSPREM.txtGuidePrem.Text = ""
    frmLSPREM.txtMaxSinglePrem(0).Text = ""
    frmLSPREM.txtMaxSinglePrem(1).Text = ""
    frmLSPREM.txtInForceYears.Text = ""
    Call Get_Mortality_Rates
    Call Do_Minimum_Premiums
    Call Do_Target_Premiums
    Call Do_Guideline_Single
    Call Do_Guideline_Annual
    If Need_Target_CV = True And Val(frmLSPREM.txtSolveCashValue.Text) <> 0 Then
       Call Premium_To_Obtain_CV
       Need_Target_CV = False
    End If
    Need_Ledger = False
    Call Calculate_Full_LS_Ledger

    frmLSPREM.MousePointer = 0

End Sub

Sub Check_Future_Changes ()

    Dim DB_Change As Integer
    Dim Prem_Change As Integer
    Dim Int_Change As Integer
    
    For i = 1 To 5
      DB_Change = False
      Prem_Change = False
      Int_Change = False
      
      If LSFuture(i).DB_Age = 0 Then
         DB_Change = False
      Else
         If LSFuture(i).DB_Age = AnnYr + 1 Then
            LS_DB(GuarCurr) = LSFuture(i).DB_Amount
            LS_DB_Minimum(GuarCurr) = LSFuture(i).DB_Amount
            DB_Change = True
         End If
      End If

      If LSFuture(i).Prem_Age = 0 Then
         Prem_Change = False
      Else
         If LSFuture(i).Prem_Age = AnnYr + 1 Then
            LS_Modal_Prem = LSFuture(i).Prem_Amount
            Prem_Change = True
         End If
      End If

      If LSFuture(i).Int_Age = 0 Or GuarCurr = 0 Then
         Int_Change = False
      Else
         If LSFuture(i).Int_Age = AnnYr + 1 Then
            Select Case (GuarCurr)
               Case (1)
                  LS_IRate(GuarCurr) = LSFuture(i).Int_Amount / 100
               Case (2)
                  LS_IRate(GuarCurr) = ((LSFuture(i).Int_Amount / 100) + LS3_GUAR_INT_RATE) / 2
               Case Else
            End Select
            Call Do_Month_Interest
            Int_Change = True
         End If
      End If

      If DB_Change And Prem_Change And Int_Change Then Exit Sub
    Next i

End Sub

Sub Check_Future_WD ()

    Dim WD_Change As Integer
    Dim Loan_Change As Integer
    Dim LoanPay_Change As Integer
    Dim Changes As Integer

    LS_Withdraw = 0
    LS_LoanAmt = 0
    LS_LoanPay = 0

    For Changes = 1 To 5
      WD_Change = False
      Loan_Change = False
      LoanPay_Change = False
      
      If LSFutureWD(Changes).WD_Age = 0 Then
         WD_Change = False
      Else
         If LSFutureWD(Changes).WD_Age <= AnnYr And (LSFutureWD(Changes).WD_Age + LSFutureWD(Changes).WD_Years - 1) >= AnnYr Then
            LS_Withdraw = LS_Withdraw + LSFutureWD(Changes).WD_Amount
            WD_Change = True
         End If
      End If

      If LSFutureWD(Changes).Loan_Age = 0 Then
         Loan_Change = False
      Else
         If LSFutureWD(Changes).Loan_Age <= AnnYr And (LSFutureWD(Changes).Loan_Age + LSFutureWD(Changes).Loan_Years - 1) >= AnnYr Then
            LS_LoanAmt = LS_LoanAmt + LSFutureWD(Changes).Loan_Amount
            Loan_Change = True
         End If
      End If

      If LSFutureWD(Changes).LoanPay_Age = 0 Then
         LoanPay_Change = False
      Else
         If LSFutureWD(Changes).LoanPay_Age <= AnnYr And (LSFutureWD(Changes).LoanPay_Age + LSFutureWD(Changes).LoanPay_Years - 1) >= AnnYr Then
            LS_LoanPay = LS_LoanPay + LSFutureWD(Changes).LoanPay_Amount
            LoanPay_Change = True
         End If
      End If

      If WD_Change And Loan_Change And LoanPay_Change Then Exit Sub
    Next Changes

    LSLedger(AnnYr).WithdrawAmount = LS_Withdraw
    LSLedger(AnnYr).LoanAmount = LS_LoanAmt
    LSLedger(AnnYr).LoanRepayAmount = LS_LoanPay
    LS_LoanBalance(GuarCurr) = LS_LoanBalance(GuarCurr) + LS_LoanAmt - LS_LoanPay
    If LS_LoanBalance(GuarCurr) < 0 Then
       LS_LoanPay = LS_LoanPay + LS_LoanBalance(GuarCurr)
       LSLedger(AnnYr).LoanRepayAmount = LS_LoanPay
       LS_LoanBalance(GuarCurr) = 0
       LSLedger(AnnYr).LoanBalance(GuarCurr) = LS_LoanBalance(GuarCurr)
    End If
    LSLedger(AnnYr).LoanBalance(GuarCurr) = LS_LoanBalance(GuarCurr)
    LS_CV(GuarCurr) = LS_CV(GuarCurr) - LS_Withdraw - LS_LoanAmt + LS_LoanPay
    LS_DB(GuarCurr) = LS_DB(GuarCurr) - LS_Withdraw
    LS_DB_Minimum(GuarCurr) = LS_DB_Minimum(GuarCurr) - LS_Withdraw
    If LS_Withdraw > 0 Then
       LS_CV(GuarCurr) = LS_CV(GuarCurr) - 25
       LS_DB(GuarCurr) = LS_DB(GuarCurr) - 25
       LS_DB_Minimum(GuarCurr) = LS_DB_Minimum(GuarCurr) - 25
    End If

End Sub

Sub Child_Rider_Mortality ()

    If (AnnYr - LSClient(SpouseClient).IssueAge) + LSChildRider.AgeYoungest < 23 Then
       Select Case (GuarCurr)
          Case (0)
             LS_Insureds_Mortcost = LS_Insureds_Mortcost + (LSChildRider.FaceAmt / 1000 * .5)
          Case (1)
             LS_Insureds_Mortcost = LS_Insureds_Mortcost + (LSChildRider.FaceAmt / 1000 * .4)
          Case (2)
             LS_Insureds_Mortcost = LS_Insureds_Mortcost + (LSChildRider.FaceAmt / 1000 * .45)
          Case Else
       End Select
    End If

End Sub

Sub ClearClient ()
    
    Dim BlankClient As ClientData
    Dim BlankClientParms As LSClientParameters

    LSet ClientInfo = BlankClient
    LSet LSClient(SpouseClient) = BlankClientParms

End Sub

Sub ClientSave ()

    Dim Insured_Birthdate As String

    If ClientChanged = True And Left(frmLIFESV.txtClientFirstName.Text, 1) <> " " And frmLIFESV.txtClientFirstName.Text <> "" Then
       frmSVCLIENT.Show 1
       Select Case ClientSave_ReturnCode
          Case 0   'Yes, save Client
             ClientInfo.FirstName = frmLIFESV.txtClientFirstName.Text
             ClientInfo.Initial = frmLIFESV.txtClientMidInit.Text
             ClientInfo.LastName = frmLIFESV.txtClientLastName.Text
             ClientInfo.Gender = frmLIFESV.txtClientSex.Text
             ClientInfo.State = frmLIFESV.txtClientState.Text
             Insured_Birthdate = frmLIFESV.txtClientBirth.Text
             Set_Insd_Birthdt (Insured_Birthdate)
             ClientInfo.BirthDate = InsuredInfo.InsBirthdate
             RetCode = client_file(5, ClientInfo.ClientNumber)
             If RetCode = False Then
                MsgText = " Save for Client information failed; Client information has not been saved "
                MsgBox MsgText, , "LifeSavings"
                Exit Sub
             End If
             InsuredInfo.InsFName = Trim$(frmLIFESV.txtClientFirstName.Text) & " " & Trim$(frmLIFESV.txtClientLastName.Text)
             Insured_Birthdate = frmLIFESV.txtClientBirth.Text
             Set_Insd_Birthdt (Insured_Birthdate)
             InsuredInfo.insGender = frmLIFESV.txtClientSex.Text
             InsuredInfo.InsRelation = "P"
             If frmLIFESV.cboClientPlan.ListIndex = 0 Then
                InsuredInfo.InsTobacco = "N"
             Else
                InsuredInfo.InsTobacco = "Y"
             End If
             RetCode = insured_file(5, ClientInfo.ClientNumber, 0)
             ClientChanged = False
          Case 1   'No, do not save Client
             ClientChanged = False
          Case 2   'Save Client, but no update on Birthdate
             ClientInfo.FirstName = frmLIFESV.txtClientFirstName.Text
             ClientInfo.Initial = frmLIFESV.txtClientMidInit.Text
             ClientInfo.LastName = frmLIFESV.txtClientLastName.Text
             ClientInfo.Gender = frmLIFESV.txtClientSex.Text
             ClientInfo.State = frmLIFESV.txtClientState.Text
             If frmLIFESV.cboClientPlan.ListIndex = 0 Then
                InsuredInfo.InsTobacco = "N"
             Else
                InsuredInfo.InsTobacco = "Y"
             End If
             RetCode = client_file(5, ClientInfo.ClientNumber)
             If RetCode = False Then
                MsgText = " Save for Client information failed; Client information has not been saved "
                MsgBox MsgText, , "LifeSavings"
                Exit Sub
             End If
             InsuredInfo.InsFName = Trim$(frmLIFESV.txtClientFirstName.Text) & " " & Trim$(frmLIFESV.txtClientLastName.Text)
             InsuredInfo.insGender = frmLIFESV.txtClientSex.Text
             InsuredInfo.InsRelation = "P"
             RetCode = insured_file(5, ClientInfo.ClientNumber, 0)
             ClientChanged = False
          Case 3   'Cancel, return to calling program
          Case Else
       End Select
    End If

End Sub

Sub Close_LS_RateFiles ()
    
    For FileNum = 0 To 11
      Close #FileNum
    Next FileNum

End Sub

Sub Do_Anniversary ()

    Dim Sex As String * 1
    Dim Smoke As String * 1
    Dim Age As Integer
    Dim Dur As Integer
    Dim ReturnRate As Double

    s = SpouseClient
    
    If LSClient(s).State = "MT" Then
       Sex = "M"
    Else
       Sex = LSClient(s).SexCode
    End If
    If LSClient(s).TobaccoUse = 0 Then
       Smoke = "N"
    Else
       If LSClient(s).IssueAge < 15 Then
          Smoke = "N"
       Else
          Smoke = "S"
       End If
    End If
    Age = LSClient(s).IssueAge
    Dur = AnnYr - LSClient(s).IssueAge + 1
    ReturnRate = 0
    If Fetch_LSSURR(LS_RateFiles(2), Sex, Smoke, Age, Dur, ReturnRate) = True Then
       LSMort(GuarCurr, AnnYr).Surr_Charge = ReturnRate
       LSMort(GuarCurr, AnnYr).Surr_Charge = LSMort(GuarCurr, AnnYr).Surr_Charge * (LSClient(s).FaceAmt / 1000)
       LS_CSV(GuarCurr) = LS_CV(GuarCurr) - LSMort(GuarCurr, AnnYr).Surr_Charge
       If LS_CSV(GuarCurr) < 0 Then LS_CSV(GuarCurr) = 0
    Else
       LS_CSV(GuarCurr) = LS_CV(GuarCurr)
    End If
   
    If LS_CSV(GuarCurr) < 0 Then LS_CSV(GuarCurr) = 0

    'check for contract lapse in 9th or 19th year;  if so,
    'set flag so that "N/A" is printed for net payment index

    If AnnYr - LSClient(s).IssueAge = 9 And GuarCurr < 2 And LS_CV(GuarCurr) <= 0 Then IllusLapse(GuarCurr, 0) = True
    If AnnYr - LSClient(s).IssueAge = 19 And GuarCurr < 2 And LS_CV(GuarCurr) <= 0 Then IllusLapse(GuarCurr, 1) = True

End Sub

Sub Do_Guideline_Annual ()

    'Calculate Guideline Annual Premium, using base
    'policy, level death benefit, and the current
    'interest rate (first year)/4% (2nd year and beyond)

    'By definition, the guideline premium is the maximum
    'premium that will not allow the cash value to equal
    'or exceed the initial death benefit (or the corridor
    'of coverage death benefit)

    'Finding this premium is a matter of guesswork and inter-
    'polation.  Start with a reasonable first guess that is
    'guaranteed to be "high": Guideline single premium / 10.
    'Save the resultant cash value at age 95.  Now set up a
    'reasonable guess that is guaranteed to be "low".  ($10)
    'Save the resultant cash value.

    'From this point on, we have a "high" and "low" guess and
    'their resulting cash values. For every new guess, determine
    'the "position" of the target cash value between the
    'resultant high and low values.  This "position" (expressed
    'as a percent) will be used to determine the next guess:
    'That same percent times the difference in "high" and "low"
    'guesses, added to the low guess.

    'The process is repeated until the guesses are less than
    '1.5 cents apart.  The low guess then becomes the Guideline
    'modal premium.

    Dim HighGuess As Single
    Dim LowGuess As Single
    Dim HighValue As Double
    Dim LowValue As Double
    Dim Percentage As Double
    Dim CurrPremium As Single
    Dim TempPremium As Single
    
    Dim ScreenSum As Currency
    Dim ScreenValue As String

    Dim CorrRate As Single
    Dim WaiverPct As Single
    Dim ReturnCode As Integer
    
    If LSClient(SpouseClient).SubStd > 0 Then
       GuarCurr = 1
    Else
       GuarCurr = 0
    End If
    HighGuess = CalcPrem.GuideSingle / 10
    CurrPremium = HighGuess
    LowGuess = 10
    GoSub Calculate_Back
    HighValue = LS_CV(GuarCurr)
    CurrPremium = LowGuess
    GoSub Calculate_Back
    LowValue = LS_CV(GuarCurr)

    While (HighGuess - LowGuess > .015)
       If Abs(LS_CV(GuarCurr)) < .5 Then
          If LS_CV(GuarCurr) < 0 Then
             HighGuess = CurrPremium - .01
             LowGuess = CurrPremium - .01
          Else
             HighGuess = CurrPremium
             LowGuess = CurrPremium
          End If
       Else
          LS_CV(GuarCurr) = HighValue - LowValue
          Percentage = HighValue / LS_CV(GuarCurr)
          CurrPremium = (HighGuess - LowGuess) * Percentage
          CurrPremium = HighGuess - CurrPremium
          GoSub Calculate_Back
          If LS_CV(GuarCurr) < 0 Then
             HighValue = LS_CV(GuarCurr)
             HighGuess = CurrPremium
          Else
             LowValue = LS_CV(GuarCurr)
             LowGuess = CurrPremium
          End If
       End If
    Wend

    If LSClient(SpouseClient).FaceAmt < 100000 Then
       CalcPrem.GuideAnnual = Int(LowGuess)
    Else
       CalcPrem.GuideAnnual = LowGuess * .98
       CalcPrem.GuideAnnual = Int(CalcPrem.GuideAnnual)
    End If
    ScreenSum = CalcPrem.GuideAnnual

    Call Format_Amount(ScreenValue, 12, ScreenSum, " ")
    frmLSPREM.txtGuidePrem.Text = ScreenValue
    Exit Sub

Calculate_Back:
       
    LS_COLA_Client = 0
    LS_CV(GuarCurr) = LSClient(SpouseClient).FaceAmt
    LS_DB(GuarCurr) = LSClient(SpouseClient).FaceAmt
    LS_IRate(GuarCurr) = (1.04 ^ (1 / 12))       '4% for Guideline Annual
    WaiverPct = 0
    LS_LoanBalance(GuarCurr) = 0
    For AnnYr = 94 To LSClient(SpouseClient).IssueAge + 1 Step -1
      ReturnCode = Fetch_LSCORR(LS_RateFiles(9), AnnYr, CorrRate)
      For AnnMo = 1 To 12
        LS_Charges = LS3_MONTHLY_EXPENSE
        Call Principal_Mortality
        If AnnMo = 12 Then
           TempPremium = CurrPremium
        Else
           TempPremium = 0
        End If
        If LSClient(SpouseClient).SubStd > 0 Then
           Call Reverse_CashValue(False, WaiverPct, TempPremium, False)
        Else
           Call Reverse_CashValue(True, WaiverPct, TempPremium, False)
        End If
      Next AnnMo
    Next AnnYr
    LS_IRate(GuarCurr) = (1.04 ^ (1 / 12))       '4% for Guideline Annual
    AnnYr = LSClient(SpouseClient).IssueAge
    ReturnCode = Fetch_LSCORR(LS_RateFiles(9), AnnYr, CorrRate)
    For AnnMo = 1 To 12
      LS_Charges = LS3_MONTHLY_EXPENSE
      Call Principal_Mortality
      If AnnMo = 12 Then
         TempPremium = CurrPremium
      Else
         TempPremium = 0
      End If
      If LSClient(SpouseClient).SubStd > 0 Then
         Call Reverse_CashValue(False, WaiverPct, TempPremium, False)
      Else
         Call Reverse_CashValue(True, WaiverPct, TempPremium, False)
      End If
    Next AnnMo

    Return

End Sub

Sub Do_Guideline_Single ()

    'Calculate Guideline Single Premium, using base
    'policy, level death benefit, and the current
    'interest rate (first year)/6% (2nd year and beyond)

    'By definition, the guideline premium is the maximum
    'premium that will not allow the cash value to equal
    'or exceed the initial death benefit (or the corridor
    'of coverage death benefit)

    Dim CorrRate As Single
    Dim WaiverPct As Single
    Dim CurrPremium As Single
    Dim ReturnCode As Integer
    Dim AddInsds As Integer     'for 8/2/95 update

    Dim ScreenSum As Currency
    Dim ScreenValue As String

    If LSClient(SpouseClient).SubStd > 0 Then
       GuarCurr = 1
    Else
       GuarCurr = 0
    End If
    LS_COLA_Client = 0
    LS_CV(GuarCurr) = LSClient(SpouseClient).FaceAmt
    LS_CSV(GuarCurr) = LSClient(SpouseClient).FaceAmt         'for additional insureds
    LS_DB(GuarCurr) = LSClient(SpouseClient).FaceAmt
    LS_IRate(GuarCurr) = (1.06 ^ (1 / 12))    '1.06 for Guideline Single
    WaiverPct = 0
    CurrPremium = 0
    LS_LoanBalance(GuarCurr) = 0
    For AnnYr = 94 To LSClient(SpouseClient).IssueAge + 1 Step -1
      ReturnCode = Fetch_LSCORR(LS_RateFiles(9), AnnYr, CorrRate)
      For AnnMo = 1 To 12
        LS_Charges = LS3_MONTHLY_EXPENSE
        Call Principal_Mortality

   '/*  added spouse and additional insured mortality  */
   '/*  at request of Special Sales/Actuarial (8/2/95) */
        
        LS_Insureds_Mortcost = 0
        AddInsds = False
        For k = 1 To 9
          If LSClient(s).InsuredRider(k) Then
             Call Insured_Rider_Mortality(k, True)
             AddInsds = True
          End If
        Next k
        
   '/*  LS_Insureds_MortCost now set for call to Reverse_CashValue */

        If LSClient(SpouseClient).SubStd > 0 Then
           Call Reverse_CashValue(False, WaiverPct, CurrPremium, AddInsds)
        Else
           Call Reverse_CashValue(True, WaiverPct, CurrPremium, AddInsds)
        End If
      Next AnnMo
    Next AnnYr
    
    LS_IRate(GuarCurr) = (1.06 ^ (1 / 12))    '1.06 for Guideline Single
    i = LSClient(SpouseClient).IssueAge
    ReturnCode = Fetch_LSCORR(LS_RateFiles(9), i, CorrRate)
    For j = 1 To 12
      LS_Charges = LS3_MONTHLY_EXPENSE
      Call Principal_Mortality
        
      AddInsds = False
      LS_Insureds_Mortcost = 0
      For k = 1 To 9
        If LSClient(s).InsuredRider(k) Then
           Call Insured_Rider_Mortality(k, True)
           AddInsds = True
        End If
      Next k

      If LSClient(SpouseClient).SubStd > 0 Then
         Call Reverse_CashValue(False, WaiverPct, CurrPremium, AddInsds)
      Else
         Call Reverse_CashValue(True, WaiverPct, CurrPremium, AddInsds)
      End If
    Next j
   
    CalcPrem.GuideSingle = LS_CV(GuarCurr) / .01
    
    If LSClient(SpouseClient).FaceAmt < 100000 Then
       CalcPrem.GuideSingle = Int((CalcPrem.GuideSingle + 5) / 100)
    Else
       CalcPrem.GuideSingle = CalcPrem.GuideSingle * .98
       CalcPrem.GuideSingle = Int((CalcPrem.GuideSingle + 5) / 100)
    End If
    ScreenSum = CalcPrem.GuideSingle
    Call Format_Amount(ScreenValue, 12, ScreenSum, " ")
    frmLSPREM.txtMaxSinglePrem(0).Text = ScreenValue

    If AddInsds = True Then
       CalcPrem.GuideSingleAddInsd = LS_CSV(GuarCurr) / .01
       If LSClient(SpouseClient).FaceAmt < 100000 Then
          CalcPrem.GuideSingleAddInsd = Int((CalcPrem.GuideSingleAddInsd + 5) / 100)
       Else
          CalcPrem.GuideSingleAddInsd = CalcPrem.GuideSingleAddInsd * .98
          CalcPrem.GuideSingleAddInsd = Int((CalcPrem.GuideSingleAddInsd + 5) / 100)
       End If
       ScreenSum = CalcPrem.GuideSingleAddInsd
       Call Format_Amount(ScreenValue, 12, ScreenSum, " ")
       frmLSPREM.txtMaxSinglePrem(1).Text = ScreenValue
    Else
       CalcPrem.GuideSingleAddInsd = 0
       frmLSPREM.txtMaxSinglePrem(1).Text = "     N/A"
    End If

End Sub

Sub Do_Minimum_Premiums ()
    
    Dim Sex As String
    Dim Smoke As String
    Dim Age As Integer
    Dim Band As Integer
    Dim ReturnRate As Double
    Dim Ten_Year_Mort As Double
    Dim MinPrem As Double
    Dim Mort_For_WPD As Double
    Dim Average_WPD As Double

    Dim ScreenSum As Currency
    Dim ScreenValue As String

    MinPrem = 0
    Average_WPD = 0
    CalcPrem.PrinMin = 0
    CalcPrem.TotAnnMin = 0
    
    'Minimum will be equal to Target in following cases
    
    If LSClient(s).FaceAmt < 15000 Then Exit Sub
    If LSClient(s).IssueAge < 30 And LSClient(s).FaceAmt < 25000 Then Exit Sub
    If LSClient(s).IssueAge < 11 And LSClient(s).FaceAmt < 35000 Then Exit Sub

    s = SpouseClient
    If LSClient(s).State = "MT" Then
       Sex = "M"
    Else
       Sex = LSClient(s).SexCode
    End If
    If LSClient(s).TobaccoUse = 0 Then
       Smoke = "N"
    Else
       If LSClient(s).IssueAge < 15 Then
          Smoke = "N"
       Else
          Smoke = "S"
       End If
    End If
    Age = LSClient(s).IssueAge
    Select Case LSClient(s).FaceAmt
       Case Is < 100000
          Band = 1
       Case Else
          Band = 2
    End Select
    ReturnRate = 0
    RetCode = Fetch_LSMINP(LS_RateFiles(0), Sex, Smoke, Age, Band, ReturnRate)
    If RetCode = True Then
       MinPrem = ReturnRate * (LSClient(s).FaceAmt / 1000)
    End If

    If LSClient(s).SubStd > 0 Then
       Ten_Year_Mort = 0
       For i = 0 To 9
         LS_MortRate = LSMort(1, LSClient(s).IssueAge + i).Prin_Sub
         LS_MortRate = LS_MortRate * LSClient(s).FaceAmt / 1000
         Ten_Year_Mort = Ten_Year_Mort + (LS_MortRate * 12)
         Average_WPD = Average_WPD + (LS_MortRate * 12) * (LSMort(1, LSClient(s).IssueAge + i).Prin_WPD)
       Next i
       If LSClient(s).FaceAmt < 100000 Then
          MinPrem = MinPrem + ((Ten_Year_Mort / 10) * 1.0752688)
       Else
          MinPrem = MinPrem + (Ten_Year_Mort / 10)
       End If
    End If
    If LSClient(s).GPO <> 0 Then
       Ten_Year_Mort = 0
       For i = 0 To 9
         LS_MortRate = LSClient(s).GPO / 1000 * LSMort(1, LSClient(s).IssueAge + i).Prin_GPO
         Ten_Year_Mort = Ten_Year_Mort + (LS_MortRate * 12)
         Average_WPD = Average_WPD + (LS_MortRate * 12) * (LSMort(1, LSClient(s).IssueAge + i).Prin_WPD)
       Next i
       If LSClient(s).FaceAmt < 100000 Then
          MinPrem = MinPrem + ((Ten_Year_Mort / 10) * 1.0752688)
       Else
          MinPrem = MinPrem + (Ten_Year_Mort / 10)
       End If
    End If

    CalcPrem.TotAnnMin = MinPrem
    GoSub Modalize_Minimum
    CalcPrem.PrinMin = MinPrem
                             
    'Minimum for Insured Rider (other than Principal)
    
    For i = 1 To 9
      If LSClient(s).InsuredRider(i) And LSInsuredRider(i).StartYear < 11 Then
         MinPrem = 0
         If LSClient(s).State = "MT" Then
            Sex = "M"
         Else
            Sex = LSInsuredRider(i).SexCode
         End If
         Age = LSInsuredRider(i).IssueAge
         ReturnRate = 0

         Ten_Year_Mort = 0
         For j = 0 To 9
           If LSInsuredRider(i).StartYear - 1 <= j Then
              LS_MortRate = LSMort(1, LSInsuredRider(i).IssueAge + j).Insured_Base(i)
              If LSInsuredRider(i).SubStd > 0 Then
                 LS_MortRate = LS_MortRate + LSMort(1, LSInsuredRider(i).IssueAge + j).Insured_Sub(i)
              End If
              LS_MortRate = LS_MortRate * (LSInsuredRider(i).FaceAmt / 1000)
              Ten_Year_Mort = Ten_Year_Mort + (LS_MortRate * 12)
              Average_WPD = Average_WPD + (LS_MortRate * 12) * (LSMort(1, LSClient(s).IssueAge + j).Prin_WPD)
           End If
         Next j
         If LSClient(s).FaceAmt < 100000 Then
            MinPrem = MinPrem + ((Ten_Year_Mort / 10) * 1.0752688)
         Else
            MinPrem = MinPrem + (Ten_Year_Mort / 10)
         End If
         CalcPrem.TotAnnMin = CalcPrem.TotAnnMin + MinPrem
         GoSub Modalize_Minimum
         CalcPrem.InsdMin(i) = MinPrem
      Else
         CalcPrem.InsdMin(i) = 0
      End If
    Next i
                                              
    'Minimum for Child Rider
    
    If LSClient(s).ChildRider = True Then
       MinPrem = 0
       Ten_Year_Mort = 0
       For i = 0 To 9
         If LSChildRider.AgeYoungest + i < 24 Then
            LS_MortRate = .4 * (LSChildRider.FaceAmt / 1000)
            Ten_Year_Mort = Ten_Year_Mort + (LS_MortRate * 12)
            Average_WPD = Average_WPD + (LS_MortRate * 12) * (LSMort(1, LSClient(s).IssueAge + i).Prin_WPD)
         End If
       Next i
       If LSClient(s).FaceAmt < 100000 Then
          MinPrem = MinPrem + ((Ten_Year_Mort / 10) * 1.0752688)
       Else
          MinPrem = MinPrem + (Ten_Year_Mort / 10)
       End If
       CalcPrem.TotAnnMin = CalcPrem.TotAnnMin + MinPrem
       GoSub Modalize_Minimum
       CalcPrem.ChildMin = MinPrem
    Else
       CalcPrem.ChildMin = 0
    End If

    If LSClient(s).WPD = True Then
       MinPrem = 0
       Mort_For_WPD = 0
       For i = 0 To 9
         Mort_For_WPD = 12 * ((LSClient(s).FaceAmt / 1000) * LSMort(1, LSClient(s).IssueAge + i).Prin_Base)
         Mort_For_WPD = Mort_For_WPD + 60
         Average_WPD = Average_WPD + (Mort_For_WPD) * (LSMort(1, LSClient(s).IssueAge + i).Prin_WPD)
       Next i
       If LSClient(s).FaceAmt < 100000 Then
          MinPrem = (Average_WPD / 10) * 1.0752688
       Else
          MinPrem = Average_WPD / 10
       End If
       CalcPrem.TotAnnMin = CalcPrem.TotAnnMin + MinPrem
       GoSub Modalize_Minimum
       CalcPrem.PrinMin = CalcPrem.PrinMin + MinPrem
    End If

    CalcPrem.TotMin = CalcPrem.PrinMin + CalcPrem.ChildMin
    For i = 1 To 9
        CalcPrem.TotMin = CalcPrem.TotMin + CalcPrem.InsdMin(i)
    Next i
    ScreenSum = CalcPrem.TotMin
    Call Format_Amount(ScreenValue, 12, ScreenSum, " ")
    frmLSPREM.txtMinPrem.Text = ScreenValue
    Exit Sub

Modalize_Minimum:

    Select Case LSClient(s).Mode
       Case "A"
       Case "S"
          MinPrem = MinPrem / 2
       Case "Q"
          MinPrem = MinPrem / 4
       Case Else
          MinPrem = MinPrem / 12
    End Select

    Return

End Sub

Sub Do_Month_Interest ()

   'Calculates interest rates for one month: Guaranteed, Current, Midpoint and Loan

    For k = 0 To 2
      LS_OneMonthInt(k) = 1 + LS_IRate(k)
      LS_OneMonthInt(k) = LS_OneMonthInt(k) ^ (1 / 12)
    Next k
    
    ' One Month Loan Interest Earned is (5% - Guar Rate)
    ' (removed loan interest logic 06-97 -- loan interest is assumed to be paid

    'LS_OneMonthLnInt = (1 + LS3_LOANINT) ^ (1 / 12)
    'LS_OneMonthLnInt = 1 + (LS_OneMonthLnInt - LS_OneMonthInt(0))

End Sub

Sub Do_Target_Premiums ()
    
    Dim Sex As String * 1
    Dim Smoke As String * 1
    Dim Age As Integer
    Dim Band As Integer
    Dim ReturnRate As Double
    Dim TargPrem As Double
    Dim MinPrem As Double
    Dim Mort_For_WPD As Double
    Dim Average_WPD As Double
    Dim Ten_Year_Mort As Double
     
    Dim ScreenSum As Currency
    Dim ScreenValue As String

    s = SpouseClient
    TargPrem = 0
    Average_WPD = 0
    CalcPrem.PrinTarg = 0
    If LSClient(s).State = "MT" Then
       Sex = "M"
    Else
       Sex = LSClient(s).SexCode
    End If
    If LSClient(s).TobaccoUse = 0 Then
       Smoke = "N"
    Else
       If LSClient(s).IssueAge < 15 Then
          Smoke = "N"
       Else
          Smoke = "S"
       End If
    End If
    If LSClient(s).FaceAmt < 100000 Then
       Band = 1
    Else
       Band = 2
    End If
       
    Age = LSClient(s).IssueAge
    ReturnRate = 0
    RetCode = Fetch_LSTARG(LS_RateFiles(1), Sex, Smoke, Age, Band, ReturnRate)
    If RetCode = True Then
       TargPrem = ReturnRate * (LSClient(s).FaceAmt / 1000)
    End If
    
    'Special Target, Minimum for certain face amt/age combinations
    
    If (LSClient(s).FaceAmt < 15000) Or (LSClient(s).IssueAge < 30 And LSClient(s).FaceAmt < 25000) Or (LSClient(s).IssueAge < 11 And LSClient(s).FaceAmt < 35000) Then
       TargPrem = TargPrem + 64.52  '($5 * 12)/(1 - curr exp chg)
    End If

    If LSClient(s).SubStd > 0 Then
       Ten_Year_Mort = 0
       For i = 0 To 9
         LS_MortRate = LSMort(1, LSClient(s).IssueAge + i).Prin_Sub
         LS_MortRate = LS_MortRate * LSClient(s).FaceAmt / 1000
         Ten_Year_Mort = Ten_Year_Mort + (LS_MortRate * 12)
         Average_WPD = Average_WPD + (LS_MortRate * 12) * (LSMort(1, LSClient(s).IssueAge + i).Prin_WPD)
       Next i
       If LSClient(s).FaceAmt < 100000 Then
          TargPrem = TargPrem + ((Ten_Year_Mort / 10) * 1.0752688)
       Else
          TargPrem = TargPrem + (Ten_Year_Mort / 10)
       End If
    End If
    If LSClient(s).GPO <> 0 Then
       Ten_Year_Mort = 0
       For i = 0 To 9
         LS_MortRate = LSClient(s).GPO / 1000 * LSMort(1, LSClient(s).IssueAge + i).Prin_GPO
         Ten_Year_Mort = Ten_Year_Mort + (LS_MortRate * 12)
         Average_WPD = Average_WPD + (LS_MortRate * 12) * (LSMort(1, LSClient(s).IssueAge + i).Prin_WPD)
       Next i
       If LSClient(s).FaceAmt < 100000 Then
          TargPrem = TargPrem + ((Ten_Year_Mort / 10) * 1.0752688)
       Else
          TargPrem = TargPrem + (Ten_Year_Mort / 10)
       End If
    End If

    CalcPrem.PrinTarg = TargPrem

    'Target for Insured Rider (other than additional principal)

    For i = 1 To 9
      If LSClient(s).InsuredRider(i) And LSInsuredRider(i).StartYear = 1 Then
         TargPrem = 0
         If LSClient(s).State = "MT" Then
            Sex = "M"
         Else
            Sex = LSClient(s).SexCode
         End If
         Age = LSInsuredRider(i).IssueAge
         ReturnRate = 0

         Ten_Year_Mort = 0
         For j = 0 To 9
           If LSInsuredRider(i).StartYear - 1 <= j Then
              LS_MortRate = LSMort(1, LSInsuredRider(i).IssueAge + j).Insured_Base(i)
              If LSInsuredRider(i).SubStd > 0 Then
                 LS_MortRate = LS_MortRate + LSMort(1, LSInsuredRider(i).IssueAge + j).Insured_Sub(i)
              End If
              LS_MortRate = LS_MortRate * (LSInsuredRider(i).FaceAmt / 1000)
              Ten_Year_Mort = Ten_Year_Mort + (LS_MortRate * 12)
              Average_WPD = Average_WPD + (LS_MortRate * 12) * (LSMort(1, LSClient(s).IssueAge + j).Prin_WPD)
           End If
         Next j
         If LSClient(s).FaceAmt < 100000 Then
            TargPrem = TargPrem + ((Ten_Year_Mort / 10) * 1.0752688)
         Else
            TargPrem = TargPrem + (Ten_Year_Mort / 10)
         End If
         CalcPrem.InsdTarg(i) = TargPrem
      Else
         CalcPrem.InsdTarg(i) = 0
      End If
    Next i

    'Target for Child

    If LSClient(s).ChildRider = True Then
       TargPrem = 0
       Ten_Year_Mort = 0
       For i = 0 To 9
         If LSChildRider.AgeYoungest + i < 24 Then
            LS_MortRate = .4 * (LSChildRider.FaceAmt / 1000)
            Ten_Year_Mort = Ten_Year_Mort + (LS_MortRate * 12)
            Average_WPD = Average_WPD + (LS_MortRate * 12) * (LSMort(1, LSClient(s).IssueAge + i).Prin_WPD)
         End If
       Next i
       If LSClient(s).FaceAmt < 100000 Then
          TargPrem = TargPrem + ((Ten_Year_Mort / 10) * 1.0752688)
       Else
          TargPrem = TargPrem + (Ten_Year_Mort / 10)
       End If
       CalcPrem.ChildTarg = TargPrem
    Else
       CalcPrem.ChildTarg = 0
    End If
    
    If LSClient(s).WPD = True Then
       Mort_For_WPD = 0
       For i = 0 To 9
         Mort_For_WPD = 12 * ((LSClient(s).FaceAmt / 1000) * LSMort(1, LSClient(s).IssueAge + i).Prin_Base)
         Mort_For_WPD = Mort_For_WPD + 60
         Average_WPD = Average_WPD + (Mort_For_WPD) * (LSMort(1, LSClient(s).IssueAge + i).Prin_WPD)
       Next i
       If LSClient(s).FaceAmt < 100000 Then
          TargPrem = (Average_WPD / 10) * 1.0752688
       Else
          TargPrem = Average_WPD / 10
       End If
       CalcPrem.PrinTarg = CalcPrem.PrinTarg + TargPrem
    End If

    CalcPrem.TotTarg = CalcPrem.PrinTarg + CalcPrem.ChildTarg
    For i = 1 To 9
        CalcPrem.TotTarg = CalcPrem.TotTarg + CalcPrem.InsdTarg(i)
    Next i
    ScreenSum = CalcPrem.TotTarg
    Call Format_Amount(ScreenValue, 12, ScreenSum, " ")
    frmLSPREM.txtTargPrem.Text = ScreenValue
    
    'Minimum = Target in following cases
    
    If (LSClient(s).FaceAmt < 15000) Or (LSClient(s).IssueAge < 30 And LSClient(s).FaceAmt < 25000) Or (LSClient(s).IssueAge < 11 And LSClient(s).FaceAmt < 35000) Then
       Select Case LSClient(s).Mode
          Case "A"
             MinPrem = CalcPrem.TotTarg
          Case "S"
             MinPrem = CalcPrem.TotTarg / 2
          Case "Q"
             MinPrem = CalcPrem.TotTarg / 4
          Case Else
             MinPrem = CalcPrem.TotTarg / 12
       End Select
       ScreenSum = MinPrem
       Call Format_Amount(ScreenValue, 12, ScreenSum, " ")
       frmLSPREM.txtMinPrem.Text = ScreenValue
       CalcPrem.TotMin = MinPrem
       CalcPrem.TotAnnMin = CalcPrem.TotTarg
    End If
    
    Exit Sub
    
End Sub

Function Fetch_CSO80 (Infile As Integer, Sex As String, Smoke As String, Age As Integer, ReturnRate As Double) As Integer
    
    Fetch_CSO80 = True
    On Local Error GoTo Err_CSO80

    Sex = UCase(Sex)
    Smoke = UCase(Smoke)

    If (Sex <> "M" And Sex <> "F") Or (Smoke <> "S" And Smoke <> "N") Then
       Fetch_CSO80 = False
       Exit Function
    End If

    If Age < 15 Or Age > 94 Then
       Fetch_CSO80 = False
       Exit Function
    End If
   
    RecordNo = Age + 1

    FileNum = Infile
    Get #FileNum, RecordNo, CSO80Rate

    Fetch_CSO80 = True
    If Sex = "M" Then
       If Smoke = "N" Then
          ReturnRate = CSO80Rate.MaleNS
       Else
          ReturnRate = CSO80Rate.MaleSM
       End If
    Else
       If Smoke = "N" Then
          ReturnRate = CSO80Rate.FemaleNS
       Else
          ReturnRate = CSO80Rate.FemaleSM
       End If
    End If

    Exit Function

Err_CSO80:
    
    MsgText = Str$(Err) & "CSO80 File"
    MsgBox MsgText, , "LifeSavings"   ' Display message.
    Fetch_CSO80 = False
    Exit Function

End Function

Function Fetch_Life_Weight (Infile As Integer, Inches As Integer, Pounds As Integer, Age As Integer) As Integer

    On Local Error GoTo Err_LHtWt
    
    If Age < 18 Then
       If Age < 15 Then
          Fetch_Life_Weight = False
          Exit Function
       Else
          Inches = Inches - 1
       End If
    End If

    If Inches < 56 Or Inches > 80 Then
       Fetch_Life_Weight = False
       Exit Function
    End If

    RecordNo = Inches - 55

    FileNum = Infile
    Get #FileNum, RecordNo, LHtWt

    Select Case Pounds
       Case Is < LHtWt.WeightMin
          Fetch_Life_Weight = False
       Case Is > LHtWt.WeightMax
          Fetch_Life_Weight = False
       Case LHtWt.WeightMin0 To LHtWt.WeightMax0
          Fetch_Life_Weight = True
       Case LHtWt.WeightMin2 To LHtWt.WeightMax2
          Fetch_Life_Weight = 2
       Case LHtWt.WeightMin3 To LHtWt.WeightMax3
          Fetch_Life_Weight = 3
       Case LHtWt.WeightMin4 To LHtWt.WeightMax4
          Fetch_Life_Weight = 4
       Case LHtWt.WeightMin5 To LHtWt.WeightMax5
          Fetch_Life_Weight = 5
       Case LHtWt.WeightMin6 To LHtWt.WeightMax6
          Fetch_Life_Weight = 6
       Case LHtWt.WeightMin8 To LHtWt.WeightMax8
          Fetch_Life_Weight = 8
       Case LHtWt.WeightMin10 To LHtWt.WeightMax10
          Fetch_Life_Weight = 10
       Case LHtWt.WeightMin12 To LHtWt.WeightMax12
          Fetch_Life_Weight = 12
    End Select

    Exit Function

Err_LHtWt:
    
    MsgText = "Height/Weight File"
    MsgText = Str$(Err) & MsgText
    MsgBox MsgText, , "LifeSavings"   ' Display message.
    Exit Function

End Function

Function Fetch_LS3COI (Infile As Integer, Sex As String, Smoke As String, Age As Integer, GuarCurr As Integer, ReturnRate As Double) As Integer

    Fetch_LS3COI = True
    On Local Error GoTo Err_LS3COI
     
    Sex = UCase(Sex)
    Smoke = UCase(Smoke)

    If (Sex <> "M" And Sex <> "F") Or (Smoke <> "S" And Smoke <> "N") Then
       Fetch_LS3COI = False
       Exit Function
    End If

    If Age > 94 Or GuarCurr > 2 Then
       Fetch_LS3COI = False
       Exit Function
    End If
   
    RecordNo = Age + 1

    FileNum = Infile
    Get #FileNum, RecordNo, PrinRate
   
    Fetch_LS3COI = True
    If Sex = "M" Then
       If Smoke = "N" Then
          ReturnRate = PrinRate.MaleNS(GuarCurr + 1)
       Else
          ReturnRate = PrinRate.MaleSM(GuarCurr + 1)
       End If
    Else
       If Smoke = "N" Then
          ReturnRate = PrinRate.FemaleNS(GuarCurr + 1)
       Else
          ReturnRate = PrinRate.FemaleSM(GuarCurr + 1)
       End If
    End If
   
    Exit Function

Err_LS3COI:
    
    MsgText = "Cost of Insurance Rate File"
    MsgText = Str$(Err) & MsgText
    MsgBox MsgText, , "LifeSavings"   ' Display message.
    Exit Function

End Function

Function Fetch_LSCORR (Infile As Integer, Age As Integer, ReturnRate As Single) As Integer
    
    Dim RecordNo As Integer
    
    Fetch_LSCORR = True
    On Local Error GoTo Err_LSCORR

    Select Case Age
       Case Is > 95
          RecordNo = 56
       Case Is < 40
          RecordNo = 1
       Case Else
          RecordNo = Age - 39
    End Select

    FileNum = Infile
    Get #FileNum, RecordNo, ReturnRate

    Exit Function

Err_LSCORR:
    
    MsgText = "Corridor File"
    MsgText = Str$(Err) & MsgText
    MsgBox MsgText, , "LifeSavings"   ' Display message.
    Fetch_LSCORR = False
    Exit Function

End Function

Function Fetch_LSMINP (Infile As Integer, Sex As String, Smoke As String, Age As Integer, Band As Integer, ReturnRate As Double) As Integer

    Fetch_LSMINP = True
    On Local Error GoTo Err_LSMINP

    Sex = UCase(Sex)
    Smoke = UCase(Smoke)

    If (Sex <> "M" And Sex <> "F") Or (Smoke <> "S" And Smoke <> "N") Then
       Fetch_LSMINP = False
       Exit Function
    End If

    If Age > 80 Or Band > 2 Then
       Fetch_LSMINP = False
       Exit Function
    End If
   
    RecordNo = Age + 1

    FileNum = Infile
    Get #FileNum, RecordNo, MinpRate

    Fetch_LSMINP = True
    If Sex = "M" Then
       If Smoke = "N" Then
          ReturnRate = MinpRate.MaleNSMinp(Band)
       Else
          ReturnRate = MinpRate.MaleSMMinp(Band)
       End If
    Else
       If Smoke = "N" Then
          ReturnRate = MinpRate.FemaleNSMinp(Band)
       Else
          ReturnRate = MinpRate.FemaleSMMinp(Band)
       End If
    End If
    
    Exit Function

Err_LSMINP:
    
    MsgText = "Minimum Premium File"
    MsgText = Str$(Err) & MsgText
    MsgBox MsgText, , "LifeSavings"   ' Display message.
    Exit Function

End Function

Function Fetch_LSRATEGP (Infile As Integer, Age As Integer, ReturnRate As Double) As Integer

    Fetch_LSRATEGP = True
    On Local Error GoTo Err_LSRATEGP

    If Age > 40 Then
       Fetch_LSRATEGP = False
       Exit Function
    End If
   
    RecordNo = Age + 1

    FileNum = Infile
    Get #FileNum, RecordNo, GPORate

    ReturnRate = GPORate.GPO

    Exit Function

Err_LSRATEGP:
    
    MsgText = "GPO File"
    MsgText = Str$(Err) & MsgText
    MsgBox MsgText, , "LifeSavings"   ' Display message.
    Exit Function

End Function

Function Fetch_LSRATESB (Infile As Integer, Smoke As String, Age As Integer, ReturnRate As Double) As Integer
    
    Dim RecordNo As Integer

    Fetch_LSRATESB = True
    On Local Error GoTo Err_LSRATESB

    Smoke = UCase(Smoke)

    If (Smoke <> "S" And Smoke <> "N") Then
       Fetch_LSRATESB = False
       Exit Function
    End If

    If Age > 94 Then
       Fetch_LSRATESB = False
       Exit Function
    End If
   
    RecordNo = Age + 1
                   
    FileNum = Infile
    Get #FileNum, RecordNo, SubRate

    Fetch_LSRATESB = True
    If Smoke = "N" Then
       ReturnRate = SubRate.NonSmokerSub
    Else
       ReturnRate = SubRate.SmokerSub
    End If

    Exit Function

Err_LSRATESB:
    
    MsgText = "Substandard File"
    MsgText = Str$(Err) & MsgText
    MsgBox MsgText, , "LifeSavings"   ' Display message.
    Exit Function

End Function

Function Fetch_LSRATEWP (Infile As Integer, Sex As String, Age As Integer, GuarCurr As Integer, ReturnRate As Double) As Integer

    Fetch_LSRATEWP = True
    On Local Error GoTo Err_LSRATEWP

    Sex = UCase(Sex)

    If Sex <> "M" And Sex <> "F" Then
       Fetch_LSRATEWP = False
       Exit Function
    End If

    If Age < 15 Or Age > 59 Or GuarCurr > 1 Then
       Fetch_LSRATEWP = False
       Exit Function
    End If
   
    RecordNo = Age + 1

    FileNum = Infile
    Get #FileNum, RecordNo, WPDRate

    If Sex = "M" Then
       ReturnRate = WPDRate.MaleWPD(GuarCurr)
    Else
       ReturnRate = WPDRate.FemaleWPD(GuarCurr)
    End If

    Exit Function

Err_LSRATEWP:
    
    MsgText = "WPD File"
    MsgText = Str$(Err) & MsgText
    MsgBox MsgText, , "LifeSavings"   ' Display message.
    Exit Function

End Function

Function Fetch_LSSURR (Infile As Integer, Sex As String, Smoke As String, Age As Integer, Duration As Integer, ReturnRate As Double) As Integer
    
    Fetch_LSSURR = True
    On Local Error GoTo Err_LSSURR

    Sex = UCase(Sex)
    Smoke = UCase(Smoke)

    If (Sex <> "M" And Sex <> "F") Or (Smoke <> "N" And Smoke <> "S") Then
       Fetch_LSSURR = False
       Exit Function
    End If

    If Age > 80 Or Duration > 15 Then
       Fetch_LSSURR = False
       Exit Function
    End If
   
    RecordNo = Age + 1

    FileNum = Infile
    Get #FileNum, RecordNo, SurrRate

    If Sex = "M" Then
       If Smoke = "N" Then
          ReturnRate = SurrRate.MaleNSSurr(Duration)
       Else
          ReturnRate = SurrRate.MaleSMSurr(Duration)
       End If
    Else
       If Smoke = "N" Then
          ReturnRate = SurrRate.FemaleNSSurr(Duration)
       Else
          ReturnRate = SurrRate.FemaleSMSurr(Duration)
       End If
    End If

    Exit Function

Err_LSSURR:
    
    MsgText = "Surrender File"
    MsgText = Str$(Err) & MsgText
    MsgBox MsgText, , "LifeSavings"   ' Display message.
    Exit Function

End Function

Function Fetch_LSTARG (Infile As Integer, Sex As String, Smoke As String, Age As Integer, Band As Integer, ReturnRate As Double) As Integer

    Fetch_LSTARG = True
    On Local Error GoTo Err_LSTARG

    Sex = UCase(Sex)
    Smoke = UCase(Smoke)

    If (Sex <> "M" And Sex <> "F") Or (Smoke <> "S" And Smoke <> "N") Then
       Fetch_LSTARG = False
       Exit Function
    End If

    If Age > 80 Or Band > 2 Then
       Fetch_LSTARG = False
       Exit Function
    End If
   
    RecordNo = Age + 1

    FileNum = Infile
    Get #FileNum, RecordNo, TargRate

    If Sex = "M" Then
       If Smoke = "N" Then
          ReturnRate = TargRate.MaleNSTarg(Band)
       Else
          ReturnRate = TargRate.MaleSMTarg(Band)
       End If
    Else
       If Smoke = "N" Then
          ReturnRate = TargRate.FemaleNSTarg(Band)
       Else
          ReturnRate = TargRate.FemaleSMTarg(Band)
       End If
    End If

    Exit Function

Err_LSTARG:
    
    MsgText = "Target Premium File"
    MsgText = Str$(Err) & MsgText
    MsgBox MsgText, , "LifeSavings"   ' Display message.
    Exit Function

End Function

Sub Format_Ledger_Amount (amtstr As String, amtlen As Integer, amtval As Currency, Comma As String, Nsign As Integer)

    Dim TempStr As String
    Dim Ctr As Integer

    amtstr = Space$(amtlen% + 1)
    If Comma = "Y" Then
       TempStr = Format$(amtval, "###,###,##0")
    Else
       TempStr = Format$(amtval, "#######0")
    End If
    If Len(TempStr) > amtlen + 1 Then
       Mid$(amtstr, Len(TempStr$) - amtlen, amtlen) = TempStr$
    Else
       Mid$(amtstr, amtlen - Len(TempStr$) + 2, Len(TempStr$)) = TempStr$
    End If
    If Nsign Then
       For Ctr = 1 To amtlen
         If Mid$(amtstr, Ctr, 1) <> " " Then
            Mid$(amtstr, Ctr - 1, 1) = "-"
            Ctr = amtlen
         End If
       Next Ctr
    End If

End Sub

Sub Get_Mortality_Rates ()
    
    'This routine will get all possible mortality rates
    '(guaranteed and current) for each illustration. For
    'each ledger print, the mortality rates will rely on
    'the values in the global array "LSMort".

    Dim Sex As String
    Dim Smoke As String
    Dim Band As Integer
    Dim ReturnCode As Integer
    Dim ReturnRate As Double
    Dim Tables As Single

    s = SpouseClient
    If LSClient(s).State = "MT" Then
       Sex = "M"
    Else
       Sex = LSClient(s).SexCode
    End If
    If LSClient(s).TobaccoUse = 0 Then
       Smoke = "N"
    Else
       If LSClient(s).IssueAge < 15 Then
          Smoke = "N"
       Else
          Smoke = "S"
       End If
    End If

    For i = 0 To 1   'Guaranteed or current
      For j = 0 To 94   'All possible years
        If Fetch_LS3COI(LS_RateFiles(3), Sex, Smoke, j, i, ReturnRate) = True Then
           LSMort(i, j).Prin_Base = ReturnRate
        Else
           If i = 0 Then
              MsgText = "Mortality rate not found for Client (sex = " & Sex & ", age = " & Str$(j) & ", smoker = " & Smoke & ", guaranteed value.  Illustration NOT VALID!!"
           Else
              MsgText = "Mortality rate not found for Client (sex = " & Sex & ", age = " & Str$(j) & ", smoker = " & Smoke & ", current value.  Illustration NOT VALID!!"
           End If
           MsgBox MsgText, , "LifeSavings"
           Exit Sub
        End If

        If LSClient(s).SubStd > 0 Then
           Tables = LSClient(s).SubStd / 4
           If Fetch_LSRATESB(LS_RateFiles(5), Smoke, j, ReturnRate) = True Then
              LSMort(i, j).Prin_Sub = LSMort(i, j).Prin_Base * Tables * ReturnRate
           End If
           LSMort(i, j).Prin_CSO80 = 0
        Else
           If Fetch_CSO80(LS_RateFiles(11), Sex, Smoke, j, ReturnRate) = True Then
              LSMort(i, j).Prin_CSO80 = ReturnRate
           Else
              LSMort(i, j).Prin_CSO80 = 0
           End If
        End If
        If LSClient(s).GPO <> 0 Then  'Guaranteed Purchase
           If j < 41 Then
              If Fetch_LSRATEGP(LS_RateFiles(6), j, ReturnRate) = True Then
                 LSMort(i, j).Prin_GPO = ReturnRate
              End If
           End If
        End If
        If LSClient(s).WPD = True Then  'Waiver of Cost of Insurance
        If j < 60 Then
              If Fetch_LSRATEWP(LS_RateFiles(7), Sex, j, i, ReturnRate) = True Then
                 LSMort(i, j).Prin_WPD = ReturnRate
              End If
           End If
        End If
      Next j
    Next i

    For k = 1 To 9
      If LSClient(s).InsuredRider(k) Then    'Addtl insd rider rates
         If LSInsuredRider(k).TobaccoUse = 0 Then
            Smoke = "N"
         Else
            If LSInsuredRider(k).IssueAge < 15 Then
               Smoke = "N"
            Else
               Smoke = "S"
            End If
         End If
         Sex = LSInsuredRider(k).SexCode
         For i = 0 To 1   'Guaranteed or current
           For j = 0 To 94   'All possible years
             If Fetch_LS3COI(LS_RateFiles(3), Sex, Smoke, j, i, ReturnRate) = True Then
                LSMort(i, j).Insured_Base(k) = ReturnRate
             Else
                MsgText = "Mortality rate not found for Insured (age = " & Str$(i) & ", smoker = " & Smoke & ".  Illustration NOT VALID!!"
                MsgBox MsgText, , "LifeSavings"
                Exit Sub
             End If
             If LSInsuredRider(k).SubStd > 0 Then
                Tables = LSInsuredRider(k).SubStd / 4
                If Fetch_LSRATESB(LS_RateFiles(5), Smoke, j, ReturnRate) = True Then
                   LSMort(i, j).Insured_Sub(k) = LSMort(i, j).Insured_Base(k) * Tables * ReturnRate
                End If
                LSMort(i, j).Insured_CSO80(k) = 0
             Else
                If Fetch_CSO80(LS_RateFiles(11), Sex, Smoke, j, ReturnRate) = True Then
                   LSMort(i, j).Insured_CSO80(k) = ReturnRate
                Else
                   LSMort(i, j).Insured_CSO80(k) = 0
                End If
             End If
           Next j
         Next i
      End If
    Next k

    'midpoint mortality rates

    For j = 0 To 94
      LSMort(2, j).Prin_Base = (LSMort(0, j).Prin_Base + LSMort(1, j).Prin_Base) / 2
      LSMort(2, j).Prin_Sub = (LSMort(0, j).Prin_Sub + LSMort(1, j).Prin_Sub) / 2
      LSMort(2, j).Prin_CSO80 = (LSMort(0, j).Prin_CSO80 + LSMort(1, j).Prin_CSO80) / 2
      LSMort(2, j).Prin_GPO = (LSMort(0, j).Prin_GPO + LSMort(1, j).Prin_GPO) / 2
      LSMort(2, j).Prin_WPD = (LSMort(0, j).Prin_WPD + LSMort(1, j).Prin_WPD) / 2
      For k = 1 To 9
        LSMort(2, j).Insured_Base(k) = (LSMort(0, j).Insured_Base(k) + LSMort(1, j).Insured_Base(k)) / 2
        LSMort(2, j).Insured_Sub(k) = (LSMort(0, j).Insured_Sub(k) + LSMort(1, j).Insured_Sub(k)) / 2
        LSMort(2, j).Insured_CSO80(k) = (LSMort(0, j).Insured_CSO80(k) + LSMort(1, j).Insured_CSO80(k)) / 2
      Next k
    Next j


End Sub

Sub Insured_Rider_Mortality (Which As Integer, GuideSingle As Integer)
    
    Dim Insured_Age As Integer
    Dim Ledger_Year As Integer
    ReDim Temp_Mort(2) As Single

    Insured_Age = AnnYr - LSClient(SpouseClient).IssueAge + LSInsuredRider(Which).IssueAge
    Ledger_Year = Insured_Age - LSInsuredRider(Which).IssueAge + 1
    If Ledger_Year >= LSInsuredRider(Which).StartYear And Ledger_Year <= LSInsuredRider(Which).EndYear Then
       If GuideSingle Then
       '  If LSMort(0, Insured_Age).Insured_CSO80(Which) < LSMort(0, Insured_Age).Insured_Base(Which) Then
       '     Temp_Mort(0) = LSMort(0, Insured_Age).Insured_CSO80(Which)
       '  Else
       '     Temp_Mort(0) = LSMort(0, Insured_Age).Insured_Base(Which)
       '  End If
       '  If LSMort(1, Insured_Age).Insured_CSO80(Which) < LSMort(1, Insured_Age).Insured_Base(Which) Then
       '     Temp_Mort(1) = LSMort(1, Insured_Age).Insured_CSO80(Which)
       '  Else
       '     Temp_Mort(1) = LSMort(1, Insured_Age).Insured_Base(Which)
       '  End If
       '
       '  For Guideline Single Premium, use Current Rates
       '  for all calculations involving mortality
       '
          Temp_Mort(0) = LSMort(1, Insured_Age).Insured_Base(Which)
          Temp_Mort(1) = LSMort(1, Insured_Age).Insured_Base(Which)
          Temp_Mort(2) = LSMort(1, Insured_Age).Insured_Base(Which)
       Else
          Temp_Mort(0) = LSMort(0, Insured_Age).Insured_Base(Which)
          Temp_Mort(1) = LSMort(1, Insured_Age).Insured_Base(Which)
          Temp_Mort(2) = LSMort(2, Insured_Age).Insured_Base(Which)
       End If

       LS_Insureds_Mortcost = LS_Insureds_Mortcost + ((LSInsuredRider(Which).FaceAmt * Temp_Mort(GuarCurr)) / 1000)
       If LSInsuredRider(Which).SubStd > 0 Then
          If GuideSingle Then
             LS_Insureds_Mortcost = LS_Insureds_Mortcost + ((LSInsuredRider(Which).FaceAmt * LSMort(1, Insured_Age).Insured_Sub(Which)) / 1000)
          Else
             LS_Insureds_Mortcost = LS_Insureds_Mortcost + ((LSInsuredRider(Which).FaceAmt * LSMort(GuarCurr, Insured_Age).Insured_Sub(Which)) / 1000)
          End If
       End If
    End If

End Sub

Sub Load_Agent ()

    Dim TempAgent As Integer
    Dim RandomAgent As AgtData

    FileNum = FreeFile
    TempAgent = AgtInfo.Record

    Open "PROFILE.DAT" For Random As FileNum Len = Len(RandomAgent)
    Get FileNum, TempAgent, RandomAgent
    AgtInfo.FullName = RandomAgent.FullName
    AgtInfo.Address1 = RandomAgent.Address1
    AgtInfo.Address2 = RandomAgent.Address2
    AgtInfo.City = RandomAgent.City
    AgtInfo.State = RandomAgent.State
    AgtInfo.ZipCode = RandomAgent.ZipCode
    AgtInfo.OfficePhone = RandomAgent.OfficePhone
    Close FileNum

End Sub

Sub Load_Client ()
        
    Dim TempNumber As Integer
    Dim DataSetNumber As String

    FileNum = FreeFile
    DataSetNumber = Str$(AgtInfo.Record)
    ClientFile = "clnt" & Trim$(Right$(DataSetNumber, 3)) & ".dat"
    InsuredFile = "insu" & Trim$(Right$(DataSetNumber, 3)) & ".dat"
    
    If ClientInfo.ClientNumber > 0 Then
       TempNumber = ClientInfo.ClientNumber
       Open ClientFile For Random As FileNum Len = Len(ClientInfo)
       Get FileNum, TempNumber, ClientInfo
       Close FileNum
    End If

End Sub

Sub Load_Future_Changes ()
    
    LSClient(SpouseClient).FutureChanges = False
    LSClient(SpouseClient).FutureWD = False

    For i = 1 To 5
      If LSFuture(i).DB_Age > 0 Then
         frmLSCHANGE.txtChangeFaceAge(i - 1).Text = Format$(LSFuture(i).DB_Age, "##")
         frmLSCHANGE.txtChangeFace(i - 1).Text = Format$(LSFuture(i).DB_Amount, "####00000")
         frmLSCHANGE.chkChangeFace.Value = 1
      End If
      If LSFuture(i).Prem_Age > 0 Then
         frmLSCHANGE.txtChangePremAge(i - 1).Text = Format$(LSFuture(i).Prem_Age, "##")
         frmLSCHANGE.txtChangePrem(i - 1).Text = Format$(LSFuture(i).Prem_Amount, "#####0.00")
         frmLSCHANGE.chkChangePrem.Value = 1
      End If
      If LSFuture(i).Int_Age > 0 Then
         frmLSCHANGE.txtChangeCurrIntAge(i - 1).Text = Format$(LSFuture(i).Int_Age, "##")
         frmLSCHANGE.txtChangeCurrInt(i - 1).Text = Format$(LSFuture(i).Int_Amount, "00.000")
         frmLSCHANGE.chkChangeCurrInt.Value = 1
      End If
      If LSFutureWD(i).WD_Age > 0 Then
         frmLSCHANGE.txtWithdrawStart(i - 1).Text = Format$(LSFutureWD(i).WD_Age, "##")
         frmLSCHANGE.txtWithdraw(i - 1).Text = Format$(LSFutureWD(i).WD_Amount, "###000.##")
         frmLSCHANGE.txtWithdrawEnd(i - 1).Text = Format$(LSFutureWD(i).WD_Age + LSFutureWD(i).WD_Years - 1, "##")
         frmLSCHANGE.chkWithdraw.Value = 1
      End If
      If LSFutureWD(i).Loan_Age > 0 Then
         frmLSCHANGE.txtLoanStart(i - 1).Text = Format$(LSFutureWD(i).Loan_Age, "##")
         frmLSCHANGE.txtLoan(i - 1).Text = Format$(LSFutureWD(i).Loan_Amount, "###000.##")
         frmLSCHANGE.txtLoanEnd(i - 1).Text = Format$(LSFutureWD(i).Loan_Age + LSFutureWD(i).Loan_Years - 1, "##")
         frmLSCHANGE.chkLoan.Value = 1
      End If
      If LSFutureWD(i).LoanPay_Age > 0 Then
         frmLSCHANGE.txtLoanRepayStart(i - 1).Text = Format$(LSFutureWD(i).LoanPay_Age, "##")
         frmLSCHANGE.txtLoanRepay(i - 1).Text = Format$(LSFutureWD(i).LoanPay_Amount, "###000.##")
         frmLSCHANGE.txtLoanRepayEnd(i - 1).Text = Format$(LSFutureWD(i).LoanPay_Age + LSFutureWD(i).LoanPay_Years - 1, "##")
         frmLSCHANGE.chkLoanRepay.Value = 1
      End If
    Next i

End Sub

Sub Load_Insureds ()
    
    s = SpouseClient

    If LSClient(s).ChildRider = True Then
       frmLSINSD.txtChildAge.Text = Format$(LSChildRider.AgeYoungest, "##")
       Select Case (LSChildRider.FaceAmt)
          Case 2500
             frmLSINSD.cboChildFace.ListIndex = 3
          Case 5000
             frmLSINSD.cboChildFace.ListIndex = 2
          Case 7500
             frmLSINSD.cboChildFace.ListIndex = 1
          Case Else
             frmLSINSD.cboChildFace.ListIndex = 0
       End Select
       frmLSINSD.chkChild.Value = 1
    Else
       frmLSINSD.chkChild.Value = 0
    End If
    
    For i = 1 To 9
      If LSClient(s).InsuredRider(i) = True Then
         frmLSINSD.txtAddInsdAge(i - 1).Text = Format$(LSInsuredRider(i).IssueAge, "##")
         frmLSINSD.txtAddInsdSex(i - 1).Text = LSInsuredRider(i).SexCode
         frmLSINSD.txtAddInsdFace(i - 1).Text = Format$(LSInsuredRider(i).FaceAmt, "#######")
         Select Case (LSInsuredRider(i).TobaccoUse)
            Case 0
               frmLSINSD.cboAddInsdPlan(i - 1).ListIndex = 0
            Case Else
               frmLSINSD.cboAddInsdPlan(i - 1).ListIndex = 1
         End Select
         Select Case LSInsuredRider(i).SubStd
            Case 1 To 20
               frmLSINSD.cboAddInsdSubStd(i - 1).ListIndex = LSInsuredRider(i).SubStd
            Case Else
               frmLSINSD.cboAddInsdSubStd(i - 1).ListIndex = 0
         End Select
         'frmLSINSD.cboAddInsdCOLA(i - 1).ListIndex = LSClient(SpouseClient).COLA
         frmLSINSD.cboAddInsdStartYearsAge(i - 1).ListIndex = 0
         frmLSINSD.txtAddInsdYearStart(i - 1).Text = Format$(LSInsuredRider(i).StartYear, "##")
         frmLSINSD.cboAddInsdEndYearsAge(i - 1).ListIndex = 0
         frmLSINSD.txtAddInsdYearEnd(i - 1).Text = Format$(LSInsuredRider(i).EndYear, "##")
         frmLSINSD.chkAddInsd(i - 1).Value = 1
      Else
         frmLSINSD.chkAddInsd(i - 1).Value = 0
      End If
    Next i

End Sub

Sub Load_LifePro ()

    Dim TempAgent As Integer
    Dim RandomAgent As AgtData

    FileNum = FreeFile
    TempAgent = 1

    Open "LIFEPRO.DAT" For Random As FileNum Len = Len(RandomAgent)
    Get FileNum, TempAgent, RandomAgent
    AgtInfo.FullName = RandomAgent.FullName
    AgtInfo.Address1 = RandomAgent.Address1
    AgtInfo.Address2 = RandomAgent.Address2
    AgtInfo.City = RandomAgent.City
    AgtInfo.State = RandomAgent.State
    AgtInfo.ZipCode = RandomAgent.ZipCode
    AgtInfo.OfficePhone = RandomAgent.OfficePhone
    frmLIFESV.txtPreparedBy.Text = AgtInfo.FullName
    If Len(Trim$(AgtInfo.Address1)) > 0 Then
       frmLIFESV.txtPrepAddr(0).Text = Trim$(AgtInfo.Address1)
    End If
    If Len(Trim$(AgtInfo.Address2)) > 0 Then
       frmLIFESV.txtPrepAddr(1).Text = Trim$(AgtInfo.Address2)
    End If
    If Len(Trim$(AgtInfo.City)) > 0 Then
       frmLIFESV.txtPrepAddr(2).Text = Trim$(AgtInfo.City) & "  " & Trim$(AgtInfo.State)
       frmLIFESV.txtPrepAddr(3).Text = Format$(AgtInfo.ZipCode, "#####")
    End If
    If Val(Mid$(AgtInfo.OfficePhone, 2, 10)) > 0 Then
       frmLIFESV.txtPhoneContact.Text = AgtInfo.OfficePhone & " Ext(_____)"
    Else
       frmLIFESV.txtPhoneContact.Text = "(___) ___-____ Ext(_____)"
    End If
    Close FileNum

End Sub

Sub Load_LS_From_Client ()
 
    Dim SerialBDt As Long
    Dim TempDate As String
    Dim CalcYear As Integer
    Dim YMDBdate As Long

    NL = Chr$(13) & Chr$(10)

    If AgtInfo.Record <> 0 Then
       If CallFromMain <> "NO" Then Call Load_Agent
       frmLIFESV.txtPreparedBy.Text = AgtInfo.FullName
       If Len(Trim$(AgtInfo.Address1)) > 0 Then
          frmLIFESV.txtPrepAddr(0).Text = Trim$(AgtInfo.Address1)
       End If
       If Len(Trim$(AgtInfo.Address2)) > 0 Then
          frmLIFESV.txtPrepAddr(1).Text = Trim$(AgtInfo.Address2)
       End If
       If Len(Trim$(AgtInfo.City)) > 0 Then
          frmLIFESV.txtPrepAddr(2).Text = Trim$(AgtInfo.City) & "  " & Trim$(AgtInfo.State)
          frmLIFESV.txtPrepAddr(3).Text = Format$(AgtInfo.ZipCode, "#####")
       End If
       If Val(Mid$(AgtInfo.OfficePhone, 2, 10)) > 0 Then
          frmLIFESV.txtPhoneContact.Text = AgtInfo.OfficePhone & " Ext(_____)"
       Else
          frmLIFESV.txtPhoneContact.Text = "(___) ___-____ Ext(_____)"
       End If
    Else
       Call Load_LifePro
    End If
    Call Load_Client

    If ClientInfo.ClientNumber > 0 Then
       frmLIFESV.txtClientFirstName.Text = ClientInfo.FirstName
       frmLIFESV.txtClientMidInit.Text = ClientInfo.Initial
       frmLIFESV.txtClientLastName.Text = ClientInfo.LastName
       frmLIFESV.txtClientState.Text = ClientInfo.State
       frmLIFESV.txtClientSex.Text = ClientInfo.Gender
       RetCode = insured_file(1, ClientInfo.ClientNumber, 0)
       If RetCode Then
          Select Case (InsuredInfo.InsTobacco)
             Case "Y", "C", "P", "S"
                frmLIFESV.cboClientPlan.ListIndex = 1
             Case Else
                frmLIFESV.cboClientPlan.ListIndex = 0
          End Select
       Else
          frmLIFESV.cboClientPlan.ListIndex = 0
       End If
       Today = Now
       If ClientInfo.BirthDate <> 0 Then
          Today = Fix(Now)
          SerialBDt = ClientInfo.BirthDate
          Call Year_Calc(SerialBDt, Today, CalcYear, YMDBdate)
          frmLIFESV.txtClientAge.Text = Trim$(Str$(CalcYear))
          TempDate = Format$(Month(SerialBDt), "00") + "/" + Format$(Day(SerialBDt), "00") + "/" + Format$(Right$(Str$(Year(SerialBDt)), 4), "0000")
          frmLIFESV.txtClientBirth.Text = TempDate
       Else
          frmLIFESV.txtClientAge.Text = "  "
          frmLIFESV.txtClientBirth.Text = "__/__/____"
       End If
       frmLIFESV.txtClientFace.Text = "_______"
    End If

    MsgFile = "ls3\LIFEHLP.MSG"
    ClientChanged = False

End Sub

Sub Load_Spouse_As_Client ()
    
    Dim Spouse_FirstName As String
    Dim Spouse_MidInit As String
    Dim Spouse_LastName As String
    Dim SerialBDt As Long
    Dim TempDate As String
    Dim CalcYear As Integer
    Dim YMDBdate As Long

    If Left$(LSClient(1).ClientName, 1) <> " " Then
       GoSub Parse_Spouse_Name
       frmLSSPOUSE.txtSpouseFirstName.Text = Spouse_FirstName
       frmLSSPOUSE.txtSpouseMidInit.Text = Spouse_MidInit
       frmLSSPOUSE.txtSpouseLastName.Text = Spouse_LastName
       frmLSSPOUSE.txtSpouseAge.Text = Trim$(Str$(LSClient(1).IssueAge))
       frmLSSPOUSE.txtSpouseSex.Text = LSClient(1).SexCode
       frmLSSPOUSE.txtSpouseFace.Text = Format$(LSClient(1).FaceAmt, "0000000")
       If LSClient(1).TobaccoUse = 0 Then
          frmLSSPOUSE.cboSpousePlan.ListIndex = 0
       Else
          frmLSSPOUSE.cboSpousePlan.ListIndex = 1
       End If
       frmLSSPOUSE.txtSpouseState.Text = LSClient(1).State
       frmLSSPOUSE.txtSpouseTaxID.Text = LSClient(1).TaxID
       If LSClient(1).SubStd = 0 Then
          frmLSSPOUSE.cboSpouseSubStd.ListIndex = 0
       Else
          frmLSSPOUSE.cboSpouseSubStd.ListIndex = LSClient(1).SubStd
       End If
       If LSClient(1).FlatRate > 0 Then
          frmLSSPOUSE.txtSpouseFlatRate.Text = "     " & Format$(LSClient(1).FlatRate, "##.00")
          frmLSSPOUSE.txtSpouseRiderYearEnd.Text = Trim(Str$(LSClient(1).YearsToPayFlat))
          frmLSSPOUSE.cboSpouseRiderYrAgeEnd.ListIndex = 0
       End If
       If LSClient(1).WPD = True Then
          frmLSSPOUSE.cboSpouseWPD.ListIndex = 1
       Else
          frmLSSPOUSE.cboSpouseWPD.ListIndex = 0
       End If
       If LSClient(1).GPO = 0 Then
          frmLSSPOUSE.cboSpouseGPO.ListIndex = 0
       Else
          frmLSSPOUSE.cboSpouseGPO.ListIndex = (LSClient(1).GPO / 1000) - 4
       End If
       If LSClient(1).COLA = 0 Then
          'frmLSSPOUSE.cboSpouseCOLA.ListIndex = 0
       Else
          'frmLSSPOUSE.cboSpouseCOLA.ListIndex = LSClient(1).COLA - 1
       End If
       If LSClient(1).CurrIntRate > 0 Then frmLSSPOUSE.txtSpouseCurrentInt.Text = Format$(LSClient(1).CurrIntRate, "00.000")
    Else
       If ClientInfo.ClientNumber > 0 Then
          For k = 0 To 11
            RetCode = insured_file(1, ClientInfo.ClientNumber, k)
            If RetCode And InsuredInfo.InsRelation = "S" Then
               LSClient(1).ClientName = LSClient(0).ClientName
               GoSub Parse_Spouse_Name
               frmLSSPOUSE.txtSpouseFirstName.Text = InsuredInfo.InsFName
               frmLSSPOUSE.txtSpouseLastName.Text = Spouse_LastName
               Select Case (InsuredInfo.InsTobacco)
                  Case "Y", "C", "P", "S"
                     frmLSSPOUSE.cboSpousePlan.ListIndex = 1
                  Case Else
                     frmLSSPOUSE.cboSpousePlan.ListIndex = 0
               End Select
               Today = Now
               If InsuredInfo.InsBirthdate <> 0 Then
                  Today = Fix(Now)
                  SerialBDt = InsuredInfo.InsBirthdate
                  Call Year_Calc(SerialBDt, Today, CalcYear, YMDBdate)
                  frmLSSPOUSE.txtSpouseAge.Text = Trim$(Str$(CalcYear))
                  TempDate = Format$(Month(SerialBDt), "00") + "/" + Format$(Day(SerialBDt), "00") + "/" + Format$(Right$(Str$(Year(SerialBDt)), 4), "0000")
                  frmLSSPOUSE.txtSpouseBirth.Text = TempDate
               Else
                  frmLSSPOUSE.txtSpouseAge.Text = "  "
                  frmLSSPOUSE.txtSpouseBirth.Text = "__/__/____"
               End If
               frmLSSPOUSE.txtSpouseSex.Text = InsuredInfo.insGender
               k = 11
            End If
          Next k
       End If
    End If
    Exit Sub

Parse_Spouse_Name:

    j = 1
    For i = 1 To Len(LSClient(1).ClientName)
      If Mid$(LSClient(1).ClientName, i, 1) <> " " Then
         If j = 1 Then Spouse_FirstName = Spouse_FirstName & Mid$(LSClient(1).ClientName, i, 1)
         If j = 2 Then Spouse_MidInit = Mid$(LSClient(1).ClientName, i, 1)
         If j = 3 Then Spouse_LastName = Spouse_LastName & Mid$(LSClient(1).ClientName, i, 1)
      Else
         j = j + 1
      End If
    Next i
    
    Return

End Sub

Sub Open_LS_RateFiles ()

    Dim file As String

    FileNum = FreeFile
    file = "ls3\ls3minp.rdm"
    Open file For Random As #FileNum Len = Len(MinpRate)
    LS_RateFiles(0) = FileNum     '0 is Minimum Premium

    FileNum = FreeFile
    file = "ls3\ls3targ.rdm"
    Open file For Random As #FileNum Len = Len(TargRate)
    LS_RateFiles(1) = FileNum     '1 is Target Premium

    FileNum = FreeFile
    file = "ls3\ls3surr.rdm"
    Open file For Random As #FileNum Len = Len(SurrRate)
    LS_RateFiles(2) = FileNum     '2 is Surrender Charges

    FileNum = FreeFile
    file = "ls3\ls3coi.rdm"
    Open file For Random As #FileNum Len = Len(PrinRate)
    LS_RateFiles(3) = FileNum     '3 is Principal Insured

    'FileNum = FreeFile
    'file = "ls3\lsratesi.rdm"
    'Open file For Random As #FileNum Len = Len(SIRate)
    'LS_RateFiles(4) = FileNum     '4 is Spouse/Additional Insureds

    FileNum = FreeFile
    file = "ls3\ls3sub.rdm"
    Open file For Random As #FileNum Len = Len(SubRate)
    LS_RateFiles(5) = FileNum     '5 is Substandard rates

    FileNum = FreeFile
    file = "ls3\ls3gpo.rdm"
    Open file For Random As #FileNum Len = Len(GPORate)
    LS_RateFiles(6) = FileNum     '6 is GPO rates

    FileNum = FreeFile
    file = "ls3\ls3wpd.rdm"
    Open file For Random As #FileNum Len = Len(WPDRate)
    LS_RateFiles(7) = FileNum     '7 is Waiver of Cost of Insurance

    FileNum = FreeFile
    file = "ls3\htwtlife.rdm"
    Open file For Random As #FileNum Len = Len(LHtWt)
    LS_RateFiles(8) = FileNum     '8 is Height/Weight tables

    FileNum = FreeFile
    file = "ls3\lscorr.rdm"
    Open file For Random As #FileNum Len = 4
    LS_RateFiles(9) = FileNum     '9 is Corridor of Coverage table

    'FileNum = FreeFile
    'file = "ls3\lsspouse.rdm"
    'Open file For Random As #FileNum Len = 4
    'LS_RateFiles(10) = FileNum     '10 is Spouse Death Benefit Adjustment table

    FileNum = FreeFile
    file = "ls3\lscso80.rdm"
    Open file For Random As #FileNum Len = Len(CSO80Rate)
    LS_RateFiles(11) = FileNum     '11 is CSO80 values for Guideline

End Sub

Sub Parse_Command_Argument (CommandStr As String, CommandAgt As Long, CommandClient As Integer)

    Dim RightClient As Integer
    Dim LeftClient As Integer
    Dim RightAgent As Integer
    Dim LeftAgent As Integer
    Dim DefaultAgent As String

    RightClient = 0
    LeftClient = 0
    RightAgent = 0
    LeftAgent = 1

    If Len(CommandStr) = 0 Then
       DefaultAgent = GetAnyIniStr("Americare", "Agent", "AMRICARE.INI")
       If DefaultAgent <> "" Then
          CommandAgt = Val(DefaultAgent)
       Else
          CommandAgt = 0
       End If
       CommandClient = 0
       Exit Sub
    End If
    
    For k = Len(CommandStr) To 1 Step -1
      Select Case Mid$(CommandStr, k, 1)
        Case " "
          If RightClient <> 0 Then
             If LeftClient = 0 Then
                LeftClient = k + 1
             ElseIf RightAgent <> 0 And LeftAgent = 0 Then
                LeftAgent = k + 1
             End If
          End If
        Case Else
          If RightClient = 0 Then
             RightClient = k + 1
          ElseIf RightAgent = 0 And LeftClient <> 0 Then
             RightAgent = k + 1
          End If
      End Select
    Next k

    CommandAgt = Val(Mid$(CommandStr, LeftAgent, RightAgent))
    CommandClient = Val(Mid$(CommandStr, LeftClient, RightClient))
 
End Sub

Sub Premium_To_Obtain_CV ()

    'Calculate Modal Premium to Obtain Target Cash Value,
    'using all current parameter values to reach the Target
    'in the given number of years.

    'Finding this premium is a matter of guesswork and inter-
    'polation.  Start with Minimum Premium as the "lower
    'bound" and then use Guideline Premium as the "upper
    'bound".  If the Target Cash Value is outside these
    '"bounds", discontinue the search and return the nearest
    '"bound" premium.

    'If the Target Cash Value falls between the "bounds",
    'the limits are established. For every new guess, determine
    'the "position" of the target cash value between the
    'resultant high and low values.  This "position" (expressed
    'as a percent) will be used to determine the next guess:
    'That same percent times the difference in "high" and "low"
    'guesses, added to the low guess.

    'The process is repeated until the guesses are less than
    '1.5 cents apart.  The low guess then becomes the Modal
    'Premium for this Search.

    Dim HighGuess As Single
    Dim LowGuess As Single
    Dim HighValue As Double
    Dim LowValue As Double
    Dim EndValue As Double
    Dim EndYear As Integer
    
    s = SpouseClient

    LSLedger(LSClient(s).IssueAge + 1).SurrenderValue(1) = 0
    LSLedger(LSClient(s).IssueAge + 1).CashValue(1) = 0
    LSLedger(LSClient(s).IssueAge + 1).DeathBenefit(1) = LSClient(s).FaceAmt

    EndYear = Val(frmLSPREM.txtSolveAge.Text)
    EndValue = Val(frmLSPREM.txtSolveCashValue.Text)
    GuarCurr = 1

    LS_Outlay = 0
    LS_CV(GuarCurr) = 0
    LS_CSV(GuarCurr) = 0
    LS_DB(GuarCurr) = LSClient(s).FaceAmt
    LS_DB_Minimum(GuarCurr) = LSClient(s).FaceAmt
    LS_IRate(GuarCurr) = LSClient(s).CurrIntRate / 100
    LS_Modal_Prem = CalcPrem.TotMin
    LS_COLA_Client = 0
    LS_COLA_Spouse = 0
    For i = 1 To 9
      LS_COLA_Insured(i) = 0
    Next i
    
    Call Process_Ledger_Years(LSClient(s).IssueAge, EndYear, True)

    LowValue = LSLedger(EndYear).CashValue(1)
    If EndValue < LowValue Then
       frmLSPREM.txtSolvePrem.Text = "< MINIMUM"
       Exit Sub
    End If

    LS_Outlay = 0
    LS_CV(GuarCurr) = 0
    LS_CSV(GuarCurr) = 0
    LS_DB(GuarCurr) = LSClient(s).FaceAmt
    LS_DB_Minimum(GuarCurr) = LSClient(s).FaceAmt
    LS_IRate(GuarCurr) = LSClient(s).CurrIntRate / 100
    Select Case (LSClient(s).Mode)
       Case "M"
          LS_Modal_Prem = CalcPrem.GuideAnnual / 12
       Case "Q"
          LS_Modal_Prem = CalcPrem.GuideAnnual / 4
       Case "S"
          LS_Modal_Prem = CalcPrem.GuideAnnual / 2
       Case Else
          LS_Modal_Prem = CalcPrem.GuideAnnual
    End Select
    LS_COLA_Client = 0
    LS_COLA_Spouse = 0
    For i = 1 To 9
      LS_COLA_Insured(i) = 0
    Next i
    
    Call Process_Ledger_Years(LSClient(s).IssueAge, EndYear, True)

    HighValue = LSLedger(EndYear).CashValue(1)

    If EndValue > HighValue Then
       frmLSPREM.txtSolvePrem.Text = "> GUIDELINE"
       Exit Sub
    End If

    HighGuess = LS_Modal_Prem
    LowGuess = CalcPrem.TotMin
    LS_Modal_Prem = (HighGuess + LowGuess) / 2
    
    While (HighGuess - LowGuess > .015)
       LS_Outlay = 0
       LS_CV(GuarCurr) = 0
       LS_CSV(GuarCurr) = 0
       LS_DB(GuarCurr) = LSClient(s).FaceAmt
       LS_DB_Minimum(GuarCurr) = LSClient(s).FaceAmt
       LS_IRate(GuarCurr) = LSClient(s).CurrIntRate / 100
       LS_COLA_Client = 0
       LS_COLA_Spouse = 0
       For i = 1 To 9
         LS_COLA_Insured(i) = 0
       Next i
    
       Call Process_Ledger_Years(LSClient(s).IssueAge, EndYear, True)

       If EndValue > LSLedger(EndYear).CashValue(1) Then
          LowGuess = LS_Modal_Prem
       Else
          HighGuess = LS_Modal_Prem
       End If
       LS_Modal_Prem = (HighGuess + LowGuess) / 2
    Wend

    frmLSPREM.txtSolvePrem.Text = Format$(LS_Modal_Prem, "     0.00")

End Sub

Sub Principal_Mortality ()
    
    LS_MortRate = LSMort(GuarCurr, AnnYr).Prin_Base
    
    If LSClient(SpouseClient).SubStd > 0 Then
       LS_MortRate = LS_MortRate + LSMort(GuarCurr, AnnYr).Prin_Sub
    End If

End Sub

Sub Principal_Premium ()
    
    Dim Temp_DB As Double
    Dim Temp_CV As Double

    Temp_DB = LS_DB(GuarCurr) + LS_COLA_Client
    Temp_CV = LS_CV(GuarCurr) + LS_LoanBalance(GuarCurr)

    'Loan Balance is added back into CV for mortality purposes

    LS_Charges = (((Temp_DB * .99754) - Temp_CV) * LS_MortRate) / 1000
 
End Sub

Sub Principal_Rider_GPO ()
    
    s = SpouseClient

    If AnnYr < 41 Then
       LS_Insureds_Mortcost = LS_Insureds_Mortcost + (LSClient(s).GPO / 1000 * LSMort(GuarCurr, AnnYr).Prin_GPO)
    End If

End Sub

Sub Principal_Rider_Mortality (GuideSingle As Integer)
           
    Dim TempYr As Integer
    ReDim Temp_Mort(2) As Single

    s = SpouseClient
    TempYr = AnnYr - LSClient(s).IssueAge

    'If GuideSingle Then
    '   If LSMort(0, AnnYr).Insured_CSO80(0) < LSMort(0, AnnYr).Insured_Base(0) Then
    '      Temp_Mort(0) = LSMort(0, AnnYr).Insured_CSO80(0)
    '   Else
    '      Temp_Mort(0) = LSMort(0, AnnYr).Insured_Base(0)
    '   End If
    '   If LSMort(1, AnnYr).Insured_CSO80(0) < LSMort(1, AnnYr).Insured_Base(0) Then
    '      Temp_Mort(1) = LSMort(1, AnnYr).Insured_CSO80(0)
    '   Else
    '      Temp_Mort(1) = LSMort(1, AnnYr).Insured_Base(0)
    '   End If
    'Else
       Temp_Mort(0) = LSMort(0, AnnYr).Insured_Base(0)
       Temp_Mort(1) = LSMort(1, AnnYr).Insured_Base(0)
       Temp_Mort(2) = LSMort(2, AnnYr).Insured_Base(0)
    'End If


End Sub

Sub Principal_Rider_WPD ()
    
    Dim WPD_Contrib As Double

    If AnnYr < 60 Then
       WPD_Contrib = (LS_Charges + LS_Insureds_Mortcost) * LSMort(GuarCurr, AnnYr).Prin_WPD
       WPD_Contrib = WPD_Contrib + (LS3_MONTHLY_EXPENSE * LSMort(GuarCurr, AnnYr).Prin_WPD)
       LS_Charges = LS_Charges + WPD_Contrib
       LS_Insureds_Mortcost = LS_Insureds_Mortcost + WPD_Contrib
    End If

End Sub

Sub Process_Ledger_Years (StartAge As Integer, StopAge As Integer, Search As Integer)
    
    Dim MonthRate As Double
    Dim CorrRate As Single

    Call Do_Month_Interest   'Sets interest rates for 1 month
    
    If GuarCurr < 2 Then
       IllusLapse(GuarCurr, 0) = False
       IllusLapse(GuarCurr, 1) = False
    End If

    For AnnYr = StartAge To StopAge
      If Fetch_LSCORR(LS_RateFiles(9), AnnYr, CorrRate) <> True Then
         CorrRate = 0
      End If
      If LSClient(s).FutureWD = True Then
         Call Check_Future_WD
      Else
         LSLedger(AnnYr).WithdrawAmount = 0
         LSLedger(AnnYr).LoanAmount = 0
         LSLedger(AnnYr).LoanRepayAmount = 0
      End If
      For AnnMo = 1 To 12
         Call Add_Modal_Premium
         LS_Insureds_Mortcost = 0
         If LSClient(s).ChildRider Then Call Child_Rider_Mortality
         For j = 1 To 9
            If LSClient(s).InsuredRider(j) Then Call Insured_Rider_Mortality(j, False)
         Next j
         Call Principal_Rider_Mortality(False)
         If LSClient(s).GPO <> 0 Then Call Principal_Rider_GPO
         LS_CV(GuarCurr) = LS_CV(GuarCurr) - LS_Insureds_Mortcost
         If CorrRate > 0 Then
            If (LS_DB_Minimum(GuarCurr) + LS_COLA_Client) < ((LS_CV(GuarCurr) + LS_LoanBalance(GuarCurr)) * CorrRate) Then
               LS_DB(GuarCurr) = LS_DB_Minimum(GuarCurr) + ((LS_CV(GuarCurr) + LS_LoanBalance(GuarCurr)) * CorrRate) - (LS_DB_Minimum(GuarCurr) + LS_COLA_Client)
            Else
               LS_DB(GuarCurr) = LS_DB_Minimum(GuarCurr)
            End If
         End If
         Call Principal_Mortality
         Call Principal_Premium
         If LSClient(s).WPD = True Then Call Principal_Rider_WPD
         LS_CV(GuarCurr) = LS_CV(GuarCurr) - LS_Charges
         If LS_CV(GuarCurr) < 0 Then
            LS_CV(GuarCurr) = 0             'JWEB 01-1998
            LS_LoanBalance(GuarCurr) = 0    'JWEB 10-98
         Else
            LS_CV(GuarCurr) = LS_CV(GuarCurr) * LS_OneMonthInt(GuarCurr)  'Add Interest
            If LS_LoanBalance(GuarCurr) > 0 Then
               LS_CV(GuarCurr) = LS_CV(GuarCurr) + (LS_LoanBalance(GuarCurr) * (LS_OneMonthInt(0) - 1))
               'LS_LoanBalance(GuarCurr) = LS_LoanBalance(GuarCurr) * LS_OneMonthLnInt
            End If
         End If
         If AnnYr - LSClient(s).IssueAge < 21 And Search = False And GuarCurr < 2 Then Call Sum_Cost_Indices
      Next AnnMo
      
      Call Do_Anniversary
      If GuarCurr = 1 And LS_CV(GuarCurr) <= 0 And AnnYr - LSClient(s).IssueAge >= 10 Then
         LSLedger(AnnYr + 1).AnnualOutlay = 0
      Else
         LSLedger(AnnYr + 1).AnnualOutlay = LS_Outlay
      End If
      LS_Outlay = 0
      LSLedger(AnnYr + 1).SurrenderValue(GuarCurr) = LS_CSV(GuarCurr)
      If LS_CV(GuarCurr) < 0 Then
         LSLedger(AnnYr + 1).CashValue(GuarCurr) = 0
      Else
         LSLedger(AnnYr + 1).CashValue(GuarCurr) = LS_CV(GuarCurr)
      End If
      LSLedger(AnnYr + 1).DeathBenefit(GuarCurr) = LS_DB(GuarCurr) + LS_COLA_Client - LS_LoanBalance(GuarCurr)
      
      If (LSClient(s).IssueAge Mod 2 <> 0 & AnnYr Mod 2 = 0) Or (LSClient(s).IssueAge Mod 2 = 0 & AnnYr Mod 2 <> 0) Then
         If LSClient(s).COLA <> 0 Then
            Call Add_COLA("P", 0)
         End If
     
         For i = 1 To 9
           If LSClient(s).InsuredRider(i) = True Then
              If LSInsuredRider(i).COLA <> 0 Then
                 Call Add_COLA("I", i)
              End If
           End If
         Next i
      End If

      If LS_CV(GuarCurr) <= 0 And AnnYr - LSClient(s).IssueAge >= 10 Then
         For i = AnnYr + 1 To 95
           LSLedger(i).SurrenderValue(GuarCurr) = 0
           LSLedger(i).CashValue(GuarCurr) = 0
           LSLedger(i).DeathBenefit(GuarCurr) = 0
           If GuarCurr = 1 Then
              LSLedger(i).AnnualOutlay = 0   'Outlay to 0, too
           End If
         Next i
         AnnYr = 95           'forces end of loop
      End If
      If LSClient(s).FutureChanges = True Then Call Check_Future_Changes
    Next AnnYr

End Sub

Function ReadyToIllustrate () As Integer

    ReadyToIllustrate = 0   'All is well
    s = SpouseClient
    If LSClient(s).IssueAge > 80 Then
       ReadyToIllustrate = 1  'Issue age too high
       Exit Function
    End If
    If LSClient(s).SexCode <> "M" And LSClient(s).SexCode <> "F" Then
       ReadyToIllustrate = 2  'Gender not set
       Exit Function
    End If
    If LSClient(s).FaceAmt < 25001 Then
       If defFaceLimit = 0 Then
          If LSClient(s).FaceAmt < 15000 Then
             ReadyToIllustrate = 3  'Face less than minimum
             Exit Function
          End If
          If LSClient(s).State = "WV" Then
             ReadyToIllustrate = 4  'State minimum
             Exit Function
          End If
          If LSClient(s).State = "WA" And LSClient(s).FaceAmt < 25000 Then
             ReadyToIllustrate = 4   'State minimum
             Exit Function
          End If
       End If
    End If
    If LSClient(s).CurrIntRate < 3# Then
       ReadyToIllustrate = 5  'Interest rate invalid
       Exit Function
    End If

End Function

Sub Refresh_ClntList ()
    
    Dim NumberRecords As Integer
    Dim OneClientName As String

    FileNum = FreeFile
    Open ClientFile For Random As #FileNum Len = Len(ClientInfo)

    'Calculate Number of Records in File
    NumberRecords = LOF(FileNum) \ Len(ClientInfo)

    'Retrieve records and load into list box
    For i = 1 To NumberRecords
      Get #FileNum, i, ClientInfo
      If Left$(ClientInfo.LastName, 1) <> "*" Then
         OneClientName = RTrim$(ClientInfo.LastName) + "," + RTrim$(ClientInfo.FirstName) + " " + ClientInfo.Initial + ",#" + Trim$(Right$(Str$(ClientInfo.ClientNumber), 5))
         frmClntList.List1.AddItem OneClientName
      End If
    Next i
    Close #FileNum
    If frmClntList.List1.ListCount > 0 Then
       frmClntList.List1.Selected(0) = True
    Else
       MsgText = "No Clients on file"
       MsgBox MsgText, 64, "LifeSavings"
       No_Clients = True
       Exit Sub
    End If

End Sub

Sub Reverse_CashValue (GuideLineStd As Integer, WaiverPct As Single, Premium As Single, AddInsds As Integer)

    Dim Numerator As Double
    Dim Denominator As Double
    Dim QFactor As Single
    Dim Temp_DB As Double

    Temp_DB = LS_DB(GuarCurr) + LS_COLA_Client
    QFactor = LS_MortRate / 1000
    'If GuideLineStd = True Then
    '   If LS_MortRate > LSMort(GuarCurr, AnnYr).Prin_CSO80 And LSMort(GuarCurr, AnnYr).Prin_CSO80 > 0 Then
    '      QFactor = LSMort(GuarCurr, AnnYr).Prin_CSO80 / 1000
    '   End If
    'End If
    Numerator = LS_Charges * QFactor
    Numerator = Numerator + (Temp_DB * .99754 * QFactor)
    Numerator = Numerator + LS_Charges
    Numerator = Numerator * (1 + WaiverPct)
    Numerator = (Numerator * LS_IRate(GuarCurr)) + LS_CV(GuarCurr)
    Denominator = 1 + QFactor + (QFactor * WaiverPct)
    Denominator = Denominator * LS_IRate(GuarCurr)
    LS_CV(GuarCurr) = (Numerator / Denominator) - Premium + .005
    LS_CV(GuarCurr) = LS_CV(GuarCurr) * 100
    LS_CV(GuarCurr) = (Int(LS_CV(GuarCurr))) / 100

'/* if AddInsds is true, calculate the Guideline Single
'/* Premium inclusive of additional insured mortality
'/* charges in the LS_CSV(GuarCurr) field.

    If AddInsds = True Then
       Numerator = (LS_Charges + LS_Insureds_Mortcost) * QFactor
       Numerator = Numerator + (Temp_DB * .99754 * QFactor)
       Numerator = Numerator + LS_Charges + LS_Insureds_Mortcost
       Numerator = Numerator * (1 + WaiverPct)
       Numerator = (Numerator * LS_IRate(GuarCurr)) + LS_CSV(GuarCurr)
      'Denominator shouldn't change
       LS_CSV(GuarCurr) = (Numerator / Denominator) - Premium + .005
       LS_CSV(GuarCurr) = LS_CSV(GuarCurr) * 100
       LS_CSV(GuarCurr) = (Int(LS_CSV(GuarCurr))) / 100
    End If

End Sub

Sub Save_LifePro ()

    Dim TempAgent As Integer
    Dim RandomAgent As AgtData

    FileNum = FreeFile
    TempAgent = 1

    Open "LIFEPRO.DAT" For Random As FileNum Len = Len(RandomAgent)
    RandomAgent.FullName = AgtInfo.FullName
    RandomAgent.Address1 = AgtInfo.Address1
    RandomAgent.Address2 = AgtInfo.Address2
    RandomAgent.City = AgtInfo.City
    RandomAgent.State = AgtInfo.State
    RandomAgent.ZipCode = AgtInfo.ZipCode
    RandomAgent.OfficePhone = AgtInfo.OfficePhone
    Put FileNum, TempAgent, RandomAgent
    Close FileNum

End Sub

Sub Set_Client_Illus_Parameters ()
     
    Dim Insured_Birthdate As String
     
    LSClient(0).ClientName = Trim$(frmLIFESV.txtClientFirstName.Text) & " " & frmLIFESV.txtClientMidInit.Text & " " & Trim$(frmLIFESV.txtClientLastName.Text)
    LSClient(0).IssueAge = Val(frmLIFESV.txtClientAge.Text)
    If LSClient(0).IssueAge + LSClient(0).YearsToPay > 95 Then
       LSClient(0).YearsToPay = 95 - LSClient(0).IssueAge
    End If
    Insured_Birthdate = frmLIFESV.txtClientBirth.Text
    Set_Insd_Birthdt (Insured_Birthdate)
    LSClient(0).DateOfBirth = InsuredInfo.InsBirthdate
    LSClient(0).SexCode = frmLIFESV.txtClientSex.Text
    LSClient(0).FaceAmt = Val(frmLIFESV.txtClientFace.Text)
    Select Case frmLIFESV.cboClientPlan.ListIndex
       Case 0
          LSClient(0).TobaccoUse = 0
       Case Else
          LSClient(0).TobaccoUse = 1
    End Select
    LSClient(0).State = frmLIFESV.txtClientState.Text
    LSClient(0).TaxID = frmLIFESV.txtClientTaxID.Text
    Select Case frmLIFESV.cboClientSubStd.ListIndex
       Case 1 To 20
          LSClient(0).SubStd = frmLIFESV.cboClientSubStd.ListIndex
       Case Else
          LSClient(0).SubStd = 0
    End Select
    LSClient(0).FlatRate = Val(frmLIFESV.txtClientFlatRate.Text)
    If frmLIFESV.cboClientRiderYrAgeEnd.ListIndex = 0 Then
       LSClient(0).YearsToPayFlat = Val(frmLIFESV.txtClientRiderYearEnd.Text)
    Else
       LSClient(0).YearsToPayFlat = Val(frmLIFESV.txtClientRiderYearEnd.Text) - LSClient(1).IssueAge
    End If
    Select Case frmLIFESV.cboClientWPD.ListIndex
       Case 1
          LSClient(0).WPD = True
       Case Else
          LSClient(0).WPD = False
    End Select
    Select Case frmLIFESV.cboClientGPO.ListIndex
       Case 1 To 21
          LSClient(0).GPO = 4000 + (1000 * frmLIFESV.cboClientGPO.ListIndex)
       Case Else
          LSClient(0).GPO = 0
    End Select
    'Select Case frmLIFESV.cboClientCOLA.ListIndex
    '   Case 1 To 10
    '      LSClient(0).COLA = frmLIFESV.cboClientCOLA.ListIndex
    '   Case Else
    '      LSClient(0).COLA = 0
    'End Select
    LSClient(0).CurrIntRate = Val(frmLIFESV.txtClientCurrentInt.Text)
    LSClient(0).IllusType = 1
    LSClient(0).PrintPages = 3
    LSClient(0).ContactName = Trim$(frmLIFESV.txtPreparedBy.Text)
    LSClient(0).ContactPhone = frmLIFESV.txtPhoneContact

End Sub

Sub Set_Future_Changes ()

    LSClient(SpouseClient).FutureChanges = False
    LSClient(SpouseClient).FutureWD = False
    SurrExist = False
    LoanExist = False
    RepayExist = False
    
    For i = 1 To 5
      If Val(frmLSCHANGE.txtChangeFaceAge(i - 1).Text) > 0 Then
         LSFuture(i).DB_Age = Val(frmLSCHANGE.txtChangeFaceAge(i - 1).Text)
         LSFuture(i).DB_Amount = Val(frmLSCHANGE.txtChangeFace(i - 1).Text)
         LSClient(SpouseClient).FutureChanges = True
      Else
         LSFuture(i).DB_Age = 0
         LSFuture(i).DB_Amount = 0
      End If
      If Val(frmLSCHANGE.txtChangePremAge(i - 1).Text) > 0 Then
         LSFuture(i).Prem_Age = Val(frmLSCHANGE.txtChangePremAge(i - 1).Text)
         LSFuture(i).Prem_Amount = Val(frmLSCHANGE.txtChangePrem(i - 1).Text)
         LSClient(SpouseClient).FutureChanges = True
      Else
         LSFuture(i).Prem_Age = 0
         LSFuture(i).Prem_Amount = 0
      End If
      If Val(frmLSCHANGE.txtChangeCurrIntAge(i - 1).Text) > 0 Then
         LSFuture(i).Int_Age = Val(frmLSCHANGE.txtChangeCurrIntAge(i - 1).Text)
         LSFuture(i).Int_Amount = Val(frmLSCHANGE.txtChangeCurrInt(i - 1).Text)
         LSClient(SpouseClient).FutureChanges = True
      Else
         LSFuture(i).Int_Age = 0
         LSFuture(i).Int_Amount = 0
      End If
      If Val(frmLSCHANGE.txtWithdrawStart(i - 1).Text) > 0 Then
         LSFutureWD(i).WD_Age = Val(frmLSCHANGE.txtWithdrawStart(i - 1).Text)
         LSFutureWD(i).WD_Amount = Val(frmLSCHANGE.txtWithdraw(i - 1).Text)
         If Val(frmLSCHANGE.txtWithdrawEnd(i - 1).Text) = 0 Then
            LSFutureWD(i).WD_Years = 95 - Val(frmLSCHANGE.txtWithdrawStart(i - 1).Text) + 1
         Else
            LSFutureWD(i).WD_Years = Val(frmLSCHANGE.txtWithdrawEnd(i - 1).Text) - Val(frmLSCHANGE.txtWithdrawStart(i - 1).Text) + 1
         End If
         LSClient(SpouseClient).FutureWD = True
         SurrExist = True
      Else
         LSFutureWD(i).WD_Age = 0
         LSFutureWD(i).WD_Amount = 0
         LSFutureWD(i).WD_Years = 0
      End If
      If Val(frmLSCHANGE.txtLoanStart(i - 1).Text) > 0 Then
         LSFutureWD(i).Loan_Age = Val(frmLSCHANGE.txtLoanStart(i - 1).Text)
         LSFutureWD(i).Loan_Amount = Val(frmLSCHANGE.txtLoan(i - 1).Text)
         If Val(frmLSCHANGE.txtLoanEnd(i - 1).Text) = 0 Then
            LSFutureWD(i).Loan_Years = 95 - Val(frmLSCHANGE.txtLoanStart(i - 1).Text) + 1
         Else
            LSFutureWD(i).Loan_Years = Val(frmLSCHANGE.txtLoanEnd(i - 1).Text) - Val(frmLSCHANGE.txtLoanStart(i - 1).Text) + 1
         End If
         LSFutureWD(i).Loan_Interest = True
         LSClient(SpouseClient).FutureWD = True
         LoanExist = True
      Else
         LSFutureWD(i).Loan_Age = 0
         LSFutureWD(i).Loan_Amount = 0
         LSFutureWD(i).Loan_Years = 0
         LSFutureWD(i).Loan_Interest = False
      End If
      If Val(frmLSCHANGE.txtLoanRepayStart(i - 1).Text) > 0 Then
         LSFutureWD(i).LoanPay_Age = Val(frmLSCHANGE.txtLoanRepayStart(i - 1).Text)
         LSFutureWD(i).LoanPay_Amount = Val(frmLSCHANGE.txtLoanRepay(i - 1).Text)
         If Val(frmLSCHANGE.txtLoanRepayEnd(i - 1).Text) = 0 Then
            LSFutureWD(i).LoanPay_Years = 95 - Val(frmLSCHANGE.txtLoanRepayStart(i - 1).Text) + 1
         Else
            LSFutureWD(i).LoanPay_Years = Val(frmLSCHANGE.txtLoanRepayEnd(i - 1).Text) - Val(frmLSCHANGE.txtLoanRepayStart(i - 1).Text) + 1
         End If
         LSClient(SpouseClient).FutureWD = True
         RepayExist = True
      Else
         LSFutureWD(i).LoanPay_Age = 0
         LSFutureWD(i).LoanPay_Amount = 0
         LSFutureWD(i).LoanPay_Years = 0
      End If
    Next i

End Sub

Sub Set_Insd_Illus_Parameters ()

    s = SpouseClient

    If frmLSINSD.chkChild.Value = 1 Then
       If frmLSINSD.txtChildAge.Text = "" Then
          LSClient(s).ChildRider = False
       Else
          LSClient(s).ChildRider = True
       End If
       LSChildRider.AgeYoungest = Val(frmLSINSD.txtChildAge.Text)
       Select Case (frmLSINSD.cboChildFace.ListIndex)
          Case 3
             LSChildRider.FaceAmt = 2500
          Case 2
             LSChildRider.FaceAmt = 5000
          Case 1
             LSChildRider.FaceAmt = 7500
          Case Else
             LSChildRider.FaceAmt = 10000
       End Select
    Else
       LSClient(s).ChildRider = False
    End If

    
    For i = 1 To 9
      If frmLSINSD.chkAddInsd(i - 1).Value = 1 Then
         If frmLSINSD.txtAddInsdAge(i - 1).Text = "" Then
            LSClient(s).InsuredRider(i) = False
         Else
            LSClient(s).InsuredRider(i) = True
         End If
         LSInsuredRider(i).IssueAge = Val(frmLSINSD.txtAddInsdAge(i - 1).Text)
         Select Case (frmLSINSD.txtAddInsdSex(i - 1).Text)
            Case "M"
               LSInsuredRider(i).SexCode = "M"
            Case "F"
               LSInsuredRider(i).SexCode = "F"
            Case Else
               LSInsuredRider(i).SexCode = " "
               LSClient(s).InsuredRider(i) = False
         End Select
         LSInsuredRider(i).FaceAmt = Val(frmLSINSD.txtAddInsdFace(i - 1).Text)
         Select Case (frmLSINSD.cboAddInsdPlan(i - 1).ListIndex)
            Case 0
               LSInsuredRider(i).TobaccoUse = 0
            Case Else
               LSInsuredRider(i).TobaccoUse = 1
         End Select
         Select Case frmLSINSD.cboAddInsdSubStd(i - 1).ListIndex
            Case 1 To 20
               LSInsuredRider(i).SubStd = frmLSINSD.cboAddInsdSubStd(i - 1).ListIndex
            Case Else
               LSInsuredRider(i).SubStd = 0
         End Select
         'Select Case frmLSINSD.cboAddInsdCOLA(i - 1).ListIndex
         '   Case 1 To 10
         '      LSInsuredRider(i).COLA = frmLSINSD.cboAddInsdCOLA(i - 1).ListIndex
         '   Case Else
         '      LSInsuredRider(i).COLA = 0
         'End Select
         If frmLSINSD.cboAddInsdStartYearsAge(i - 1).ListIndex = 1 Then
            LSInsuredRider(i).StartYear = Val(frmLSINSD.txtAddInsdYearStart(i - 1).Text) - LSInsuredRider(i).IssueAge
         Else
            LSInsuredRider(i).StartYear = Val(frmLSINSD.txtAddInsdYearStart(i - 1).Text)
         End If
         If frmLSINSD.cboAddInsdEndYearsAge(i - 1).ListIndex = 1 Then
            LSInsuredRider(i).EndYear = Val(frmLSINSD.txtAddInsdYearEnd(i - 1).Text) - LSInsuredRider(i).IssueAge
         Else
            LSInsuredRider(i).EndYear = Val(frmLSINSD.txtAddInsdYearEnd(i - 1).Text)
         End If
      Else
         LSClient(s).InsuredRider(i) = False
      End If
    Next i

End Sub

Sub Set_Premium_Illus_Parameters ()

    s = SpouseClient
    LSClient(s).CurrIntRate = Val(frmLSPREM.txtPremCurrentInt.Text)
    If LSClient(s).CurrIntRate > 0 Then frmLIFESV.txtClientCurrentInt.Text = Format$(LSClient(s%).CurrIntRate, "00.000")
    Select Case frmLSPREM.cboPremiumMode.ListIndex
       Case 1
          LSClient(s).Mode = "Q"
       Case 2
          LSClient(s).Mode = "S"
       Case 3
          LSClient(s).Mode = "A"
       Case Else
          LSClient(s).Mode = "M"
    End Select
    LSClient(s).PlannedPrem = Val(frmLSPREM.txtModalPremium.Text)
    LSClient(s).YearsToPay = Val(frmLSPREM.cboYearsPay.Text)
    LSClient(s).LumpSum = Val(frmLSPREM.txtPremiumLump)
    
End Sub

Sub Set_Spouse_Illus_Parameters ()

    Dim Insured_Birthdate As String

    LSClient(1).ClientName = Trim$(frmLSSPOUSE.txtSpouseFirstName.Text) & " " & frmLSSPOUSE.txtSpouseMidInit.Text & " " & Trim$(frmLSSPOUSE.txtSpouseLastName.Text)
    LSClient(1).IssueAge = Val(frmLSSPOUSE.txtSpouseAge.Text)
    If LSClient(1).IssueAge + LSClient(1).YearsToPay > 95 Then
       LSClient(1).YearsToPay = 95 - LSClient(1).IssueAge
    End If
    Insured_Birthdate = frmLSSPOUSE.txtSpouseBirth.Text
    Set_Insd_Birthdt (Insured_Birthdate)
    LSClient(1).DateOfBirth = InsuredInfo.InsBirthdate
    LSClient(1).SexCode = frmLSSPOUSE.txtSpouseSex.Text
    LSClient(1).FaceAmt = Val(frmLSSPOUSE.txtSpouseFace.Text)
    Select Case frmLSSPOUSE.cboSpousePlan.ListIndex
       Case 0
          LSClient(1).TobaccoUse = 0
       Case Else
          LSClient(1).TobaccoUse = 1
    End Select
    LSClient(1).State = frmLSSPOUSE.txtSpouseState.Text
    LSClient(1).TaxID = frmLSSPOUSE.txtSpouseTaxID.Text
    Select Case frmLSSPOUSE.cboSpouseSubStd.ListIndex
       Case 1 To 20
          LSClient(1).SubStd = frmLSSPOUSE.cboSpouseSubStd.ListIndex
       Case Else
          LSClient(1).SubStd = 0
    End Select
    LSClient(1).FlatRate = Val(frmLSSPOUSE.txtSpouseFlatRate.Text)
    If frmLSSPOUSE.cboSpouseRiderYrAgeEnd.ListIndex = 0 Then
       LSClient(1).YearsToPayFlat = Val(frmLSSPOUSE.txtSpouseRiderYearEnd.Text)
    Else
       LSClient(1).YearsToPayFlat = Val(frmLSSPOUSE.txtSpouseRiderYearEnd.Text) - LSClient(1).IssueAge
    End If
    Select Case frmLSSPOUSE.cboSpouseWPD.ListIndex
       Case 1
          LSClient(1).WPD = True
       Case Else
          LSClient(1).WPD = False
    End Select
    Select Case frmLSSPOUSE.cboSpouseGPO.ListIndex
       Case 1 To 21
          LSClient(1).GPO = 4000 + (1000 * frmLSSPOUSE.cboSpouseGPO.ListIndex)
       Case Else
          LSClient(1).GPO = 0
    End Select
    'Select Case frmLSSPOUSE.cboSpouseCOLA.ListIndex
    '   Case 1 To 10
    '     LSClient(1).COLA = frmLSSPOUSE.cboSpouseCOLA.ListIndex
    '   Case Else
    '     LSClient(1).COLA = 0
    'End Select
    LSClient(1).CurrIntRate = Val(frmLSSPOUSE.txtSpouseCurrentInt)
    LSClient(1).ChildRider = False
    For j = 1 To 9
      LSClient(1).InsuredRider(j) = False
    Next j
    LSClient(1).IllusType = 1
    LSClient(1).PrintPages = 3
    LSClient(1).ContactName = Trim$(frmLIFESV.txtPreparedBy.Text)
    LSClient(1).ContactPhone = frmLIFESV.txtPhoneContact

End Sub

Sub SpouseSave ()

    Dim Insured_Birthdate As String

    If SpouseChanged = True And Left(frmLSSPOUSE.txtSpouseFirstName.Text, 1) <> " " And frmLSSPOUSE.txtSpouseFirstName.Text <> "" Then
       ClientInfo.FirstName = frmLSSPOUSE.txtSpouseFirstName.Text
       ClientInfo.Initial = frmLSSPOUSE.txtSpouseMidInit.Text
       ClientInfo.LastName = frmLSSPOUSE.txtSpouseLastName.Text
       ClientInfo.Gender = frmLSSPOUSE.txtSpouseSex.Text
       ClientInfo.State = frmLSSPOUSE.txtSpouseState.Text
       Insured_Birthdate = frmLSSPOUSE.txtSpouseBirth.Text
       Set_Insd_Birthdt (Insured_Birthdate)
       ClientInfo.BirthDate = InsuredInfo.InsBirthdate
       ClientInfo.ClientNumber = 0
       RetCode = client_file(5, ClientInfo.ClientNumber)
       If RetCode = False Then
          MsgText = " Save for Spouse information failed; Spouse information has not been saved "
          MsgBox MsgText, , "LifeSavings"
          Exit Sub
       End If
       InsuredInfo.InsFName = Trim$(frmLSSPOUSE.txtSpouseFirstName.Text) & " " & Trim$(frmLSSPOUSE.txtSpouseLastName.Text)
       Insured_Birthdate = frmLSSPOUSE.txtSpouseBirth.Text
       Set_Insd_Birthdt (Insured_Birthdate)
       InsuredInfo.insGender = frmLSSPOUSE.txtSpouseSex.Text
       InsuredInfo.InsRelation = "P"
       If frmLSSPOUSE.cboSpousePlan.ListIndex = 0 Then
          InsuredInfo.InsTobacco = "N"
       Else
          InsuredInfo.InsTobacco = "Y"
       End If
       RetCode = insured_file(5, ClientInfo.ClientNumber, 0)
       SpouseChanged = False
    End If

End Sub

Sub Sum_Cost_Indices ()

    Dim Power As Single
    Dim CostFactor As Double
    Dim Temp_DB As Double
    Dim GrossUp As Double
    Dim BaseIntRate As Double
    
    s = SpouseClient

    If Val(frmLSPREM.txtBeginYear.Text) <> 0 Then
       Exit Sub   'no cost indexes on in-force illustrations
    End If

    If LSClient(s).State = "AR" Then
       BaseIntRate = 1.04
    Else
       BaseIntRate = 1.05
    End If

    Temp_DB = LS_DB(GuarCurr) + LS_COLA_Client
    Power = AnnYr - LSClient(s).IssueAge + 1
    If Power <= 11 Then
       If Power <> 11 Then
          GoSub Get_Gross
          LS_Index_OneYear_Cost(GuarCurr, 0) = LS_Index_OneYear_Cost(GuarCurr, 0) - ((LS_Insureds_Mortcost) * GrossUp)
          LS_Index_OneYear_Cost(GuarCurr, 0) = LS_Index_OneYear_Cost(GuarCurr, 0) + LS_Monthly_Prem
          If AnnMo = 1 Then
            CostFactor = BaseIntRate ^ (11 - Power)
            LS_Index_Denominator(GuarCurr, 0) = LS_Index_Denominator(GuarCurr, 0) + Temp_DB * CostFactor
            If AnnYr = LSClient(s).IssueAge Then LS_Index_OneYear_Cost(GuarCurr, 0) = LS_Index_OneYear_Cost(GuarCurr, 0) + LSClient(s).LumpSum
          End If
          If AnnMo = 12 Then
             CostFactor = BaseIntRate ^ (11 - Power)
             LS_Index_Numerator_NP(GuarCurr, 0) = LS_Index_Numerator_NP(GuarCurr, 0) + LS_Index_OneYear_Cost(GuarCurr, 0) * CostFactor
             LS_Index_OneYear_Cost(GuarCurr, 0) = 0
          End If
       Else
          LS_Index_Numerator_SC(GuarCurr, 0) = LS_Index_Numerator_NP(GuarCurr, 0) - LS_CSV(GuarCurr)
       End If
    End If
    If Power <> 21 Then
       GoSub Get_Gross
       LS_Index_OneYear_Cost(GuarCurr, 1) = LS_Index_OneYear_Cost(GuarCurr, 1) - ((LS_Insureds_Mortcost) * GrossUp)
       LS_Index_OneYear_Cost(GuarCurr, 1) = LS_Index_OneYear_Cost(GuarCurr, 1) + LS_Monthly_Prem
       If AnnMo = 1 Then
          CostFactor = BaseIntRate ^ (21 - Power)
          LS_Index_Denominator(GuarCurr, 1) = LS_Index_Denominator(GuarCurr, 1) + Temp_DB * CostFactor
          If AnnYr = LSClient(s).IssueAge Then LS_Index_OneYear_Cost(GuarCurr, 1) = LS_Index_OneYear_Cost(GuarCurr, 1) + LSClient(s).LumpSum
       End If
       If AnnMo = 12 Then
          CostFactor = BaseIntRate ^ (21 - Power)
          LS_Index_Numerator_NP(GuarCurr, 1) = LS_Index_Numerator_NP(GuarCurr, 1) + LS_Index_OneYear_Cost(GuarCurr, 1) * CostFactor
          LS_Index_OneYear_Cost(GuarCurr, 1) = 0
       End If
    Else
       LS_Index_Numerator_SC(GuarCurr, 1) = LS_Index_Numerator_NP(GuarCurr, 1) - LS_CSV(GuarCurr)
    End If

    Exit Sub
    
Get_Gross:

    If GuarCurr = 0 Then                       'Gross up charges for mortality:
       If (LS_DB_Minimum(GuarCurr) + LS_COLA_Client) < 100000 Then
          GrossUp = 1 / .9                     '10% Guaranteed when < 100000
       Else
          GrossUp = 1 / .95                    '5% Guaranteed when >= 100000
       End If
    Else
       If (LS_DB_Minimum(GuarCurr) + LS_COLA_Client) < 100000 Then
          GrossUp = 1 / .93                    '7% Current when < 100000
       Else
          GrossUp = 1                          '0% Current when >= 100000
       End If
    End If

Return

End Sub

