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

   Type Life_LSRATEPR_Values
      MaleNS(1 To 5) As Single
      MaleSM(1 To 5) As Single
      FemaleNS(1 To 5) As Single
      FemaleSM(1 To 5) As Single
   End Type

   Dim PrinRate As Life_LSRATEPR_Values

   Type Life_LSRATESI_Values
      MaleNS(1 To 2) As Single
      MaleSM(1 To 2) As Single
      FemaleNS(1 To 2) As Single
      FemaleSM(1 To 2) As Single
   End Type

   Dim SIRate As Life_LSRATESI_Values

   Type Life_LSRATEGP_Values
      GPO As Single
   End Type

   Dim GPORate As Life_LSRATEGP_Values

   Type Life_LSRATEWP_Values
      MaleWPD As Single
      FemaleWPD As Single
   End Type

   Dim WPDRate As Life_LSRATEWP_Values

   Type Life_LSRATESB_Values
      SmokerSub As Single
      NonSmokerSub As Single
   End Type

   Dim SubRate As Life_LSRATESB_Values

   Type Life_LSSURR_Values
      MaleSurr(0 To 16) As Single
      FemaleSurr(0 To 16) As Single
   End Type

   Dim SurrRate As Life_LSSURR_Values

   Type Life_LSMINP_Values
      MaleNSMinp(1 To 4) As Single
      MaleSMMinp(1 To 4) As Single
      FemaleNSMinp(1 To 4) As Single
      FemaleSMMinp(1 To 4) As Single
   End Type

   Dim MinpRate As Life_LSMINP_Values

   Type Life_LSTARG_Values
      MaleTarg As Single
      FemaleTarg As Single
   End Type

   Dim TargRate As Life_LSTARG_Values
   
   Type Life_LSCSO80RATE_Values
      MaleNS As Single
      MaleSM As Single
      FemaleNS As Single
      FemaleSM As Single
   End Type

   Dim CSO80Rate As Life_LSCSO80RATE_Values

   Dim SUMAAA As Currency
   Dim SUMAA As Currency
   Dim SUMA As Currency
   Dim SUMMAX As Currency

   Dim NL As String
   Dim today As Long
   Dim s As Integer
   Dim i As Integer
   Dim j As Integer
   Dim k As Integer
   Dim filnum As Integer
   Dim RecordNo As Integer
   Dim MsgText As String

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
       Case "S"     'Spouse COLA
	  TempAge = LSClient(SpouseClient).IssueAge - AnnYr + LSSpouseRider.IssueAge
	  If LS_COLA_Spouse < 200000 And LS_COLA_Spouse < LSSpouseRider.FaceAmt * 2 And TempAge < 64 Then
	     OneYearCOLA = (LSSpouseRider.FaceAmt + LS_COLA_Spouse) * (LSSpouseRider.COLA / 100)
	     If OneYearCOLA > LSSpouseRider.FaceAmt * .2 Then OneYearCOLA = LSSpouseRider.FaceAmt * .2
	     If OneYearCOLA > 25000 Then OneYearCOLA = 25000
	     LS_COLA_Spouse = LS_COLA_Spouse + OneYearCOLA
	  End If
	  If LS_COLA_Spouse > 200000 Then LS_COLA_Spouse = 200000
	  If LS_COLA_Spouse > LSSpouseRider.FaceAmt * 2 Then LS_COLA_Spouse = LSSpouseRider.FaceAmt * 2
       Case "I"     'Additional Insured COLA
	  TempAge = LSClient(SpouseClient).IssueAge - AnnYr + LSInsuredRider(Which).IssueAge
	  If LS_COLA_Insured(Which) < 200000 And LS_COLA_Insured(Which) < LSInsuredRider(Which).FaceAmt * 2 And TempAge < 64 Then
	     OneYearCOLA = (LSInsuredRider(Which).FaceAmt + LS_COLA_Insured(Which)) * (LSInsuredRider(Which).COLA / 100)
	     If OneYearCOLA > LSInsuredRider(Which).FaceAmt * .2 Then OneYearCOLA = LSSpouseRider.FaceAmt * .2
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
    
    If LSClient(s).FaceAmt < 100000 Then
       LS_Charges = 3.5
    Else
       LS_Charges = 3#
    End If

    If AnnYr < LSClient(s).IssueAge + LSClient(s).YearsToPay Then
       If (AnnMo = 1 And LSClient(s).Mode = "A") Or ((AnnMo = 1 Or AnnMo = 7) And LSClient(s).Mode = "S") Or ((AnnMo = 1 Or AnnMo = 4 Or AnnMo = 7 Or AnnMo = 10) And LSClient(s).Mode = "Q") Or LSClient(s).Mode = "M" Then
	  LS_Outlay = LS_Outlay + LS_Modal_Prem
	  LS_Monthly_Prem = LS_Modal_Prem
	  LS_CV(GuarCurr) = LS_CV(GuarCurr) + LS_Modal_Prem
       Else
	  LS_Monthly_Prem = 0
       End If
       If (AnnMo = 1 And LSClient(s).Mode = "A") Or ((AnnMo = 1 Or AnnMo = 7) And LSClient(s).Mode = "S") Or ((AnnMo = 1 Or AnnMo = 4 Or AnnMo = 7 Or AnnMo = 10) And LSClient(s).Mode = "Q") Or LSClient(s).Mode = "M" Then
	  If AnnYr - LSClient(s).IssueAge < 10 Then
	     LS_Charges = LS_Charges + (LS_Modal_Prem * .075)
	  Else
	     LS_Charges = LS_Charges + (LS_Modal_Prem * .025)
	  End If
       End If
    Else
       LS_Monthly_Prem = 0
    End If
    
    LS_CV(GuarCurr) = LS_CV(GuarCurr) - LS_Charges

End Sub

Sub Calculate_Full_LS_Ledger ()

    Dim Begin_Age As Integer
    Dim Search As Integer

    s = SpouseClient
    
    LSLedger(LSClient(s).IssueAge + 1).SurrenderValue(0) = 0
    LSLedger(LSClient(s).IssueAge + 1).SurrenderValue(1) = 0
    LSLedger(LSClient(s).IssueAge + 1).CashValue(0) = 0
    LSLedger(LSClient(s).IssueAge + 1).CashValue(1) = 0
    LSLedger(LSClient(s).IssueAge + 1).DeathBenefit(0) = LSClient(s).FaceAmt
    LSLedger(LSClient(s).IssueAge + 1).DeathBenefit(1) = LSClient(s).FaceAmt
    LSLedger(LSClient(s).IssueAge + 1).WithdrawAmount = 0
    LSLedger(LSClient(s).IssueAge + 1).LoanAmount = 0
    LSLedger(LSClient(s).IssueAge + 1).LoanRepayAmount = 0
    LSLedger(LSClient(s).IssueAge + 1).LoanBalance = 0
    
    GuarCurr = 0

    If LSClient(s).LumpSum = 0 Then
       LS_CV(GuarCurr) = 0
    Else
       LS_CV(GuarCurr) = LSClient(s).LumpSum * .925
    End If

    LS_Outlay = LSClient(s).LumpSum
    LS_CSV(GuarCurr) = 0
    LS_DB(GuarCurr) = LSClient(s).FaceAmt
    LS_DB_Minimum(GuarCurr) = LSClient(s).FaceAmt
    LS_Irate(GuarCurr) = .04
    LS_Modal_Prem = LSClient(s).PlannedPrem
    LS_DBOption = LSClient(s).Option
    LS_CV_Opt2_Start(GuarCurr) = 0
    LS_Index_Numerator_NP(GuarCurr, 0) = 0
    LS_Index_Numerator_NP(GuarCurr, 1) = 0
    LS_Index_Numerator_SC(GuarCurr, 0) = 0
    LS_Index_Numerator_SC(GuarCurr, 1) = 0
    LS_Index_Denominator(0) = 0
    LS_Index_Denominator(1) = 0
    LS_COLA_Client = 0
    LS_COLA_Spouse = 0
    For i = 1 To 9
      LS_COLA_Insured(i) = 0
    Next i
    LS_LoanBalance = 0

    If Val(frmLSPREM.txtBeginYear.Text) <> 0 Then
       Begin_Age = Val(frmLSPREM.txtBeginYear.Text)
       LS_CV(0) = Val(frmLSPREM.txtBeginCashValue.Text)
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
    LS_Irate(GuarCurr) = LSClient(s).CurrRate / 100
    LS_Modal_Prem = LSClient(s).PlannedPrem
    LS_DBOption = LSClient(s).Option
    LS_CV_Opt2_Start(GuarCurr) = 0
    
    If LSClient(s).LumpSum = 0 Then
       LS_CV(GuarCurr) = 0
    Else
       LS_CV(GuarCurr) = LSClient(s).LumpSum * .925
    End If
    LS_Index_Numerator_NP(GuarCurr, 0) = 0
    LS_Index_Numerator_NP(GuarCurr, 1) = 0
    LS_Index_Numerator_SC(GuarCurr, 0) = 0
    LS_Index_Numerator_SC(GuarCurr, 1) = 0
    LS_COLA_Client = 0
    LS_COLA_Spouse = 0
    For i = 1 To 9
      LS_COLA_Insured(i) = 0
    Next i
    
    If Val(frmLSPREM.txtBeginYear.Text) <> 0 Then
       Begin_Age = Val(frmLSPREM.txtBeginYear.Text)
       LS_CV(1) = Val(frmLSPREM.txtBeginCashValue.Text)
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
    Else
       For i = 95 To LSClient(SpouseClient).IssueAge Step -1
	 If LSLedger(i).CashValue(1) > 0 Then
	    frmLSPREM.txtInForceYears.Text = " LAPSE at age " & Format$(i + 1, "##")
	    i = 0     'forces end of loop
	 End If
       Next i
    End If
    
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
    Dim Opt_Change As Integer
    
    For i = 1 To 5
      DB_Change = False
      Prem_Change = False
      Int_Change = False
      Opt_Change = False
      
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
	    LS_Irate(GuarCurr) = LSFuture(i).Int_Amount / 100
	    Int_Change = True
	 End If
      End If

      If LSFuture(i).Opt_Age = 0 Then
	 Opt_Change = False
      Else
	 If LSFuture(i).Opt_Age = AnnYr + 1 Then
	    If LSFuture(i).Opt_Type = 1 And LS_DBOption = 2 Then
	       LS_DB(GuarCurr) = LS_DB(GuarCurr) + LS_CV(GuarCurr) - LS_CV_Opt2_Start(GuarCurr)
	       LS_DB_Minimum(GuarCurr) = LS_DB_Minimum(GuarCurr) + LS_CV(GuarCurr) - LS_CV_Opt2_Start(GuarCurr)
	       LS_CV_Opt2_Start(GuarCurr) = 0
	    End If
	    If LSFuture(i).Opt_Type = 2 And LS_DBOption = 1 Then
	       LS_CV_Opt2_Start(GuarCurr) = LS_CV(GuarCurr)
	    End If
	    LS_DBOption = LSFuture(i).Opt_Type
	    Opt_Change = True
	 End If
      End If

      If DB_Change And Prem_Change And Int_Change And Opt_Change Then Exit Sub
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
    LSLedger(AnnYr).LoanBalance = LSLedger(AnnYr - 1).LoanBalance + LS_LoanAmt - LS_LoanPay
    LS_LoanBalance = LSLedger(AnnYr).LoanBalance
    If LSLedger(AnnYr).LoanBalance < 0 Then
       LS_LoanPay = LS_LoanPay + LSLedger(AnnYr).LoanBalance
       LSLedger(AnnYr).LoanRepayAmount = LS_LoanPay
       LSLedger(AnnYr).LoanBalance = 0
       LS_LoanBalance = 0
    End If
    LS_CV(GuarCurr) = LS_CV(GuarCurr) - LS_Withdraw - LS_LoanAmt + LS_LoanPay
    LS_CSV(GuarCurr) = LS_CSV(GuarCurr) - LS_Withdraw - LS_LoanAmt + LS_LoanPay
    LS_DB(GuarCurr) = LS_DB(GuarCurr) - LS_Withdraw - LS_LoanAmt + LS_LoanPay
    LS_DB_Minimum(GuarCurr) = LS_DB_Minimum(GuarCurr) - LS_Withdraw - LS_LoanAmt + LS_LoanPay
    If LS_Withdraw > 0 Then
       LS_CV(GuarCurr) = LS_CV(GuarCurr) - 25
       LS_CSV(GuarCurr) = LS_CSV(GuarCurr) - 25
       LS_DB(GuarCurr) = LS_DB(GuarCurr) - 25
       LS_DB_Minimum(GuarCurr) = LS_DB_Minimum(GuarCurr) - 25
    End If

End Sub

Sub Child_Rider_Mortality ()

    If (AnnYr - LSClient(SpouseClient).IssueAge) + LSChildRider.AgeYoungest < 23 Then
       If GuarCurr = 1 Or (AnnYr - LSClient(SpouseClient).IssueAge) = 0 Then
	  LS_Insureds_MortCost = LS_Insureds_MortCost + (LSChildRider.FaceAmt / 1000 * .4)
       Else
	  LS_Insureds_MortCost = LS_Insureds_MortCost + (LSChildRider.FaceAmt / 1000 * .5)
       End If
    End If

End Sub

Sub ClearClient ()
    
    Dim BlankClient As ClientData
    Dim BlankClientParms As LSClientParameters

    LSet ClientInfo = BlankClient
    LSet LSClient(SpouseClient) = BlankClientParms

End Sub

Sub ClientSave ()

    Dim RetCode As Integer
    Dim IBirth As String

    If ClientChanged = True And Left(frmLIFESV.txtClientFirstName.Text, 1) <> " " And frmLIFESV.txtClientFirstName.Text <> "" Then
       frmSVCLIENT.Show 1
       Select Case ClientSave_ReturnCode
	  Case 0   'Yes, save Client
	     ClientInfo.FirstName = frmLIFESV.txtClientFirstName.Text
	     ClientInfo.Initial = frmLIFESV.txtClientMidInit.Text
	     ClientInfo.LastName = frmLIFESV.txtClientLastName.Text
	     ClientInfo.Gender = frmLIFESV.txtClientSex.Text
	     ClientInfo.State = frmLIFESV.txtClientState.Text
	     IBirth = frmLIFESV.txtClientBirth.Text
	     set_insd_birthdt (IBirth)
	     ClientInfo.BirthDate = InsuredInfo.InsBirthDate
	     RetCode = client_file(5, ClientInfo.ClientNumber)
	     If RetCode = False Then
		MsgBox " Save for Client information failed; Client information has not been saved ", 0
		Exit Sub
	     End If
	     InsuredInfo.InsFName = Trim$(frmLIFESV.txtClientFirstName.Text) & " " & Trim$(frmLIFESV.txtClientLastName.Text)
	     IBirth = frmLIFESV.txtClientBirth.Text
	     set_insd_birthdt (IBirth)
	     InsuredInfo.InsGender = frmLIFESV.txtClientSex.Text
	     InsuredInfo.InsRelation = "P"
	     If frmLIFESV.cboClientPlan.ListIndex = 0 Then
		InsuredInfo.insTobacco = "N"
	     Else
		InsuredInfo.insTobacco = "Y"
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
		InsuredInfo.insTobacco = "N"
	     Else
		InsuredInfo.insTobacco = "Y"
	     End If
	     RetCode = client_file(5, ClientInfo.ClientNumber)
	     If RetCode = False Then
		MsgBox " Save for Client information failed; Client information has not been saved ", 0
		Exit Sub
	     End If
	     InsuredInfo.InsFName = Trim$(frmLIFESV.txtClientFirstName.Text) & " " & Trim$(frmLIFESV.txtClientLastName.Text)
	     InsuredInfo.InsGender = frmLIFESV.txtClientSex.Text
	     InsuredInfo.InsRelation = "P"
	     RetCode = insured_file(5, ClientInfo.ClientNumber, 0)
	     ClientChanged = False
	  Case 3   'Cancel, return to calling program
	  Case Else
       End Select
    End If

End Sub

Sub Close_LS_RateFiles ()
    
    For filnum = 0 To 11
      Close #filnum
    Next filnum

End Sub

Sub Do_Anniversary ()

    Dim Sex As String
    Dim Age As Integer
    Dim Dur As Integer
    Dim ReturnRate As Single
    Dim SurrCharge As Double

    s = SpouseClient
    
    If LSClient(s).State = "MT" Then
       Sex = "M"
    Else
       Sex = LSClient(s).SexCode
    End If
    Age = LSClient(s).IssueAge
    Dur = AnnYr - LSClient(s).IssueAge + 1
    ReturnRate = 0
    If Verify_LSSURR(LS_RateFiles(2), "P", Sex, Age, Dur, ReturnRate) = True Then
       SurrCharge = ReturnRate
       SurrCharge = SurrCharge * (LSClient(s).FaceAmt / 1000)
       LS_CSV(GuarCurr) = LS_CV(GuarCurr) - SurrCharge
       If LS_CSV(GuarCurr) < 0 Then LS_CSV(GuarCurr) = 0
    Else
       LS_CSV(GuarCurr) = LS_CV(GuarCurr)
    End If
   
    If LS_CSV(GuarCurr) < 0 Then LS_CSV(GuarCurr) = 0

End Sub

Sub Do_Guideline_Annual ()

    'Calculate Guideline Annual Premium, using base
    'policy, option 1 death benefit, and the current
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
    
    GuarCurr = 1
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

    CalcPrem.GuideAnnual = Int(LowGuess)
    ScreenSum = CalcPrem.GuideAnnual

    Call Format_Amount(ScreenValue, 12, ScreenSum, " ")
    frmLSPREM.txtGuidePrem.Text = ScreenValue
    Exit Sub

Calculate_Back:
       
    LS_COLA_Client = 0
    LS_CV(GuarCurr) = LSClient(SpouseClient).FaceAmt * .98
    LS_DB(GuarCurr) = LSClient(SpouseClient).FaceAmt
    LS_Irate(GuarCurr) = 1.04 ^ (1 / 12)
    WaiverPct = 0
    LS_LoanBalance = 0
    LS_CV_Opt2_Start(1) = 0
    For AnnYr = 94 To LSClient(SpouseClient).IssueAge + 1 Step -1
      ReturnCode = Verify_LSCORR(LS_RateFiles(9), AnnYr, CorrRate)
      For AnnMo = 1 To 12
	If LSClient(s).FaceAmt < 100000 Then
	   LS_Charges = 3.5
	Else
	   LS_Charges = 3
	End If
	If ReturnCode = True Then
	   If LS_DB(GuarCurr) < LS_CV(GuarCurr) * CorrRate Then
	      LS_DB(GuarCurr) = LS_CV(GuarCurr) * CorrRate
	   End If
	End If
	Call Principal_Mortality
	If AnnMo = 12 Then
	   If AnnYr > LSClient(SpouseClient).IssueAge + 9 Then
	      TempPremium = CurrPremium * .975
	   Else
	      TempPremium = CurrPremium * .925
	   End If
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
    LS_Irate(GuarCurr) = 1 + (LSClient(SpouseClient).CurrRate / 100)
    LS_Irate(GuarCurr) = LS_Irate(GuarCurr) ^ (1 / 12)
    AnnYr = LSClient(SpouseClient).IssueAge
    ReturnCode = Verify_LSCORR(LS_RateFiles(9), AnnYr, CorrRate)
    For AnnMo = 1 To 12
      If LSClient(s).FaceAmt < 100000 Then
	 LS_Charges = 3.5
      Else
	 LS_Charges = 3
      End If
      If ReturnCode = True Then
	 If LS_DB(GuarCurr) < LS_CV(GuarCurr) * CorrRate Then
	    LS_DB(GuarCurr) = LS_CV(GuarCurr) * CorrRate
	 End If
      End If
      Call Principal_Mortality
      If AnnMo = 12 Then
	 TempPremium = CurrPremium * .925
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
    'policy, option 1 death benefit, and the current
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

    GuarCurr = 1
    LS_COLA_Client = 0
    LS_CV(GuarCurr) = LSClient(SpouseClient).FaceAmt * .98
    LS_CSV(GuarCurr) = LSClient(SpouseClient).FaceAmt 'for additional insureds
    LS_DB(GuarCurr) = LSClient(SpouseClient).FaceAmt
    LS_Irate(GuarCurr) = 1.06 ^ (1 / 12)
    WaiverPct = 0
    CurrPremium = 0
    LS_LoanBalance = 0
    LS_CV_Opt2_Start(1) = 0
    For AnnYr = 94 To LSClient(SpouseClient).IssueAge + 1 Step -1
      ReturnCode = Verify_LSCORR(LS_RateFiles(9), AnnYr, CorrRate)
      For AnnMo = 1 To 12
	If LSClient(s).FaceAmt < 100000 Then
	   LS_Charges = 3.5
	Else
	   LS_Charges = 3
	End If
	If ReturnCode = True Then
	   If LS_DB(GuarCurr) < LS_CV(GuarCurr) * CorrRate Then
	      LS_DB(GuarCurr) = LS_CV(GuarCurr) * CorrRate
	   End If
	End If
	Call Principal_Mortality

   '/*  added spouse and additional insured mortality  */
   '/*  at request of Special Sales/Actuarial (8/2/95) */
	
	LS_Insureds_MortCost = 0
	AddInsds = False
	If LSClient(s).SpouseRider Then
	   Call Spouse_Rider_Mortality(True)
	   AddInsds = True
	End If
	For k = 1 To 9
	  If LSClient(s).InsuredRider(k) Then
	     Call Insured_Rider_Mortality(k, True)
	     AddInsds = True
	  End If
	Next k
	If LSClient(s).AddFace > 0 Then
	   Call Principal_Rider_Mortality(True)
	   AddInsds = True
	End If
	
   '/*  LS_Insureds_MortCost now set for call to Reverse_CashValue

	If LSClient(SpouseClient).SubStd > 0 Then
	   Call Reverse_CashValue(False, WaiverPct, CurrPremium, AddInsds)
	Else
	   Call Reverse_CashValue(True, WaiverPct, CurrPremium, AddInsds)
	End If
      Next AnnMo
    Next AnnYr
    
    LS_Irate(GuarCurr) = 1 + (LSClient(SpouseClient).CurrRate / 100)
    LS_Irate(GuarCurr) = LS_Irate(GuarCurr) ^ (1 / 12)
    i = LSClient(SpouseClient).IssueAge
    ReturnCode = Verify_LSCORR(LS_RateFiles(9), i, CorrRate)
    For j = 1 To 12
      If LSClient(s).FaceAmt < 100000 Then
	 LS_Charges = 3.5
      Else
	 LS_Charges = 3
      End If
      If ReturnCode = True Then
	 If LS_DB(GuarCurr) < LS_CV(GuarCurr) * CorrRate Then
	    LS_DB(GuarCurr) = LS_CV(GuarCurr) * CorrRate
	 End If
      End If
      Call Principal_Mortality
	
      AddInsds = False
      LS_Insureds_MortCost = 0
      If LSClient(s).SpouseRider Then
	 Call Spouse_Rider_Mortality(True)
	 AddInsds = True
      End If
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
   
    CalcPrem.GuideSingle = LS_CV(GuarCurr) / .00925
    CalcPrem.GuideSingle = Int((CalcPrem.GuideSingle + 5) / 100)
    ScreenSum = CalcPrem.GuideSingle
    Call Format_Amount(ScreenValue, 12, ScreenSum, " ")
    frmLSPREM.txtMaxSinglePrem(0).Text = ScreenValue

    If AddInsds = True Then
       CalcPrem.GuideSingleAddInsd = LS_CSV(GuarCurr) / .00925
       CalcPrem.GuideSingleAddInsd = Int((CalcPrem.GuideSingleAddInsd + 5) / 100)
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
    Dim ReturnRate As Single
    Dim Three_Year_Mort As Single
    Dim RetCd As Integer
    Dim MinPrem As Double
    Dim Mort_For_WPD As Double
    Dim Average_WPD As Double

    Dim ScreenSum As Currency
    Dim ScreenValue As String

    MinPrem = 0
    Mort_For_WPD = 0
    CalcPrem.PrinMin = 0
    s = SpouseClient
    If LSClient(s).State = "MT" Then
       Sex = "M"
    Else
       Sex = LSClient(s).SexCode
    End If
    If LSClient(s).PlanCode = 1 Then
       Smoke = "N"
    Else
       Smoke = "S"
    End If
    Age = LSClient(s).IssueAge
    Select Case LSClient(s).FaceAmt
       Case Is < 50000
	  Band = 1
       Case 50000 To 99999
	  Band = 2
       Case 100000 To 249999
	  Band = 3
       Case Else
	  Band = 4
    End Select
    ReturnRate = 0
    RetCd = Verify_LSMINP(LS_RateFiles(0), Sex, Smoke, Age, Band, ReturnRate)
    If RetCd = True Then
       MinPrem = ReturnRate * (LSClient(s).FaceAmt / 1000)
       If LSClient(s).FaceAmt < 100000 Then
	  MinPrem = MinPrem + 45.41
       Else
	  MinPrem = MinPrem + 38.92
       End If
    End If

    If LSClient(s).SubStd > 0 Then
       Three_Year_Mort = 0
       For i = 0 To 2
	 LS_MortRate = LSMort(1, LSClient(s).IssueAge + i).Prin_Sub(LS_Prin_Band)
	 LS_MortRate = LS_MortRate * LSClient(s).FaceAmt / 1000
	 Three_Year_Mort = Three_Year_Mort + (LS_MortRate * 12)
       Next i
       MinPrem = MinPrem + ((Three_Year_Mort / 3) * 1.081081) + .005
       Mort_For_WPD = Mort_For_WPD + Three_Year_Mort
    End If
    If LSClient(s).GPO <> 0 Then
       Three_Year_Mort = 0
       For i = 0 To 2
	 LS_MortRate = LSClient(s).GPO / 1000 * LSMort(1, LSClient(s).IssueAge + i).Prin_GPO
	 Three_Year_Mort = Three_Year_Mort + (LS_MortRate * 12)
       Next i
       MinPrem = MinPrem + ((Three_Year_Mort / 3) * 1.081081) + .005
       Mort_For_WPD = Mort_For_WPD + Three_Year_Mort
    End If
    If LSClient(s).AddFace <> 0 Then
       Three_Year_Mort = 0
       For i = 0 To 2
	 LS_MortRate = LSMort(1, LSClient(s).IssueAge + i).Insured_Base(0)
	 If LSClient(s).SubStd > 0 Then
	    LS_MortRate = LS_MortRate + LSMort(1, LSClient(s).IssueAge + i).Insured_Sub(0)
	 End If
	 LS_MortRate = LSClient(s).AddFace / 1000 * LS_MortRate
	 Three_Year_Mort = Three_Year_Mort + (LS_MortRate * 12)
       Next i
       MinPrem = MinPrem + ((Three_Year_Mort / 3) * 1.081081) + .005
       Mort_For_WPD = Mort_For_WPD + Three_Year_Mort
    End If

    GoSub Modalize_Minimum
    CalcPrem.PrinMin = MinPrem

    'Minimum for Spouse Rider
    
    If LSClient(s).SpouseRider = True Then
       MinPrem = 0
       If LSClient(s).State = "MT" Then
	  Sex = "M"
       Else
	  If LSClient(s).SexCode = "M" Then
	     Sex = "F"
	  Else
	     Sex = "M"
	  End If
       End If
       Age = LSSpouseRider.IssueAge
       ReturnRate = 0

       Three_Year_Mort = 0
       For i = 0 To 2
	 LS_MortRate = LSMort(1, LSSpouseRider.IssueAge + i).Spouse_Base
	 If LSSpouseRider.SubStd > 0 Then
	    LS_MortRate = LS_MortRate + LSMort(1, LSSpouseRider.IssueAge + i).Spouse_Sub
	 End If
	 LS_MortRate = LS_MortRate * (LSSpouseRider.FaceAmt / 1000)
	 Three_Year_Mort = Three_Year_Mort + (LS_MortRate * 12)
       Next i
       MinPrem = MinPrem + ((Three_Year_Mort / 3) * 1.081081) + .005
       GoSub Modalize_Minimum
       Mort_For_WPD = Mort_For_WPD + Three_Year_Mort
       CalcPrem.SpouseMin = MinPrem
    Else
       CalcPrem.SpouseMin = 0
    End If
			     
    'Minimum for Insured Rider (other than Principal)
    
    For i = 1 To 9
      If LSClient(s).InsuredRider(i) And LSInsuredRider(i).StartYear < 4 Then
	 MinPrem = 0
	 If LSClient(s).State = "MT" Then
	    Sex = "M"
	 Else
	    Sex = LSInsuredRider(i).SexCode
	 End If
	 Age = LSSpouseRider.IssueAge
	 ReturnRate = 0

	 Three_Year_Mort = 0
	 For j = 0 To 2
	   If LSInsuredRider(i).StartYear - 1 <= j Then
	      LS_MortRate = LSMort(1, LSInsuredRider(i).IssueAge + j).Insured_Base(i)
	      If LSInsuredRider(i).SubStd > 0 Then
		 LS_MortRate = LS_MortRate + LSMort(1, LSInsuredRider(i).IssueAge + j).Insured_Sub(i)
	      End If
	      LS_MortRate = LS_MortRate * (LSInsuredRider(i).FaceAmt / 1000)
	      Three_Year_Mort = Three_Year_Mort + (LS_MortRate * 12)
	   End If
	 Next j
	 MinPrem = MinPrem + ((Three_Year_Mort / 3) * 1.081081) + .005
	 GoSub Modalize_Minimum
	 Mort_For_WPD = Mort_For_WPD + Three_Year_Mort
	 CalcPrem.InsdMin(i) = MinPrem
      Else
	 CalcPrem.InsdMin(i) = 0
      End If
    Next i
					      
    'Minimum for Child Rider
    If LSClient(s).ChildRider = True Then
       MinPrem = 0
       Three_Year_Mort = 0
       For i = 0 To 2
	 If LSChildRider.AgeYoungest + i < 24 Then
	    LS_MortRate = .4 * (LSChildRider.FaceAmt / 1000)
	    Three_Year_Mort = Three_Year_Mort + (LS_MortRate * 12)
	 End If
       Next i
       MinPrem = MinPrem + ((Three_Year_Mort / 3) * 1.081081) + .005
       GoSub Modalize_Minimum
       Mort_For_WPD = Mort_For_WPD + Three_Year_Mort
       CalcPrem.ChildMin = MinPrem
    Else
       CalcPrem.ChildMin = 0
    End If

    If LSClient(s).WPD = True Then
       MinPrem = 0
       Three_Year_Mort = 0
       Average_WPD = 0
       For i = 0 To 2
	 Average_WPD = Average_WPD + LSMort(1, LSClient(s).IssueAge + i).Prin_WPD
	 If LSClient(s).FaceAmt < 100000 Then
	    Three_Year_Mort = Three_Year_Mort + (((LSMort(1, LSClient(s).IssueAge + i).Prin_Base(LS_Prin_Band) * (LSClient(s).FaceAmt / 1000)) * 12) * .996737)
	    Three_Year_Mort = Three_Year_Mort + 42
	 Else
	    Three_Year_Mort = Three_Year_Mort + (((LSMort(1, LSClient(s).IssueAge + i).Prin_Base(LS_Prin_Band) * (LSClient(s).FaceAmt / 1000)) * 12) * .996737)
	    Three_Year_Mort = Three_Year_Mort + 36
	 End If
       Next i
       Average_WPD = Average_WPD / 3
       MinPrem = MinPrem + ((Three_Year_Mort / 3) * 1.081081) + .005
       MinPrem = MinPrem + ((Mort_For_WPD / 3) * 1.081081) + .005
       MinPrem = MinPrem * Average_WPD
       GoSub Modalize_Minimum
       CalcPrem.PrinMin = CalcPrem.PrinMin + MinPrem
    End If

    CalcPrem.TotMin = CalcPrem.PrinMin + CalcPrem.SpouseMin + CalcPrem.ChildMin
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

Sub Do_Month_Interest (IntRate As Double)

   'Returns the rate for one month of interest in IntRate

    IntRate = 1 + LS_Irate(GuarCurr)
    IntRate = IntRate ^ (1 / 12)

End Sub

Sub Do_Target_Premiums ()
    
    Dim Sex As String
    Dim Age As Integer
    Dim ReturnRate As Single
    Dim RetCd As Integer
    Dim TargPrem As Double
    Dim Mort_For_WPD As Double
    
    Dim ScreenSum As Currency
    Dim ScreenValue As String

    s = SpouseClient
    TargPrem = 0
    Mort_For_WPD = 0
    CalcPrem.PrinTarg = 0
    If LSClient(s).State = "MT" Then
       Sex = "M"
    Else
       Sex = LSClient(s).SexCode
    End If
    Age = LSClient(s).IssueAge
    ReturnRate = 0
    RetCd = Verify_LSTARG(LS_RateFiles(1), Sex, Age, ReturnRate)
    If RetCd = True Then
       TargPrem = ReturnRate * (LSClient(s).FaceAmt / 1000)
    End If
    If LSClient(s).SubStd > 0 Then
       LS_MortRate = LSMort(1, LSClient(s).IssueAge).Prin_Sub(LS_Prin_Band)
       LS_MortRate = LS_MortRate * LSClient(s).FaceAmt / 1000
       TargPrem = TargPrem + (LS_MortRate * 12)
       Mort_For_WPD = Mort_For_WPD + (LS_MortRate * 12)
    End If
    If LSClient(s).GPO <> 0 Then
       LS_MortRate = LSClient(s).GPO / 1000 * LSMort(1, LSClient(s).IssueAge).Prin_GPO
       TargPrem = TargPrem + (LS_MortRate * 12)
       Mort_For_WPD = Mort_For_WPD + (LS_MortRate * 12)
    End If
    If LSClient(s).AddFace <> 0 And LSClient(s).AddFaceStart = 1 Then
       LS_MortRate = LSClient(s).AddFace / 1000 * LSMort(1, LSClient(s).IssueAge).Insured_Base(0)
       TargPrem = TargPrem + (LS_MortRate * 12)
       Mort_For_WPD = Mort_For_WPD + (LS_MortRate * 12)
    End If

    'GoSub Modalize_Target
    CalcPrem.PrinTarg = TargPrem

    'Target for Spouse Rider

    If LSClient(s).SpouseRider = True Then
       TargPrem = 0
       If LSClient(s).State = "MT" Then
	  Sex = "M"
       Else
	  If LSClient(s).SexCode = "M" Then
	     Sex = "F"
	  Else
	     Sex = "M"
	  End If
       End If
       Age = LSSpouseRider.IssueAge
       ReturnRate = 0

       LS_MortRate = LSMort(1, LSSpouseRider.IssueAge).Spouse_Base
       If LSSpouseRider.SubStd > 0 Then
	  LS_MortRate = LS_MortRate + LSMort(1, LSSpouseRider.IssueAge).Spouse_Sub
       End If
       LS_MortRate = (LS_MortRate * LSSpouseRider.FaceAmt / 1000) * 12
       TargPrem = TargPrem + LS_MortRate
       Mort_For_WPD = Mort_For_WPD + TargPrem
       'GoSub Modalize_Target

       CalcPrem.SpouseTarg = TargPrem
    Else
       CalcPrem.SpouseTarg = 0
    End If

    'Target for Insured Rider (other than additional principal)

    For i = 1 To 9
      If LSClient(s).InsuredRider(i) And LSInsuredRider(i).StartYear = 1 Then
	 TargPrem = 0
	 If LSClient(s).State = "MT" Then
	    Sex = "M"
	 Else
	    Sex = LSClient(s).SexCode
	 End If
	 Age = LSSpouseRider.IssueAge
	 ReturnRate = 0

	 LS_MortRate = LSMort(1, LSInsuredRider(i).IssueAge).Insured_Base(i)
	 If LSInsuredRider(i).SubStd > 0 Then
	    LS_MortRate = LS_MortRate + LSMort(1, LSInsuredRider(i).IssueAge).Insured_Sub(i)
	 End If
	 LS_MortRate = (LS_MortRate * LSInsuredRider(i).FaceAmt / 1000) * 12
	 TargPrem = TargPrem + LS_MortRate
	 Mort_For_WPD = Mort_For_WPD + TargPrem
	 'GoSub Modalize_Target

	 CalcPrem.InsdTarg(i) = TargPrem
      Else
	 CalcPrem.InsdTarg(i) = 0
      End If
    Next i

    'Target for Child

    If LSClient(s).ChildRider = True Then
       TargPrem = (.4 * LSChildRider.FaceAmt / 1000) * 12
       Mort_For_WPD = Mort_For_WPD + TargPrem
       'GoSub Modalize_Target
       CalcPrem.ChildTarg = TargPrem
    Else
       CalcPrem.ChildTarg = 0
    End If
    
    If LSClient(s).WPD = True Then
       Mort_For_WPD = Mort_For_WPD + ((LSMort(1, LSClient(s).IssueAge).Prin_Base(LS_Prin_Band) * (LSClient(s).FaceAmt / 1000)) * 12)
       If LSClient(s).FaceAmt < 100000 Then
	  LS_MortRate = ((42 + Mort_For_WPD) * LSMort(1, LSClient(s).IssueAge).Prin_WPD)
       Else
	  LS_MortRate = ((36 + Mort_For_WPD) * LSMort(1, LSClient(s).IssueAge).Prin_WPD)
       End If
       CalcPrem.PrinTarg = CalcPrem.PrinTarg + LS_MortRate
    End If

    CalcPrem.TotTarg = CalcPrem.PrinTarg + CalcPrem.SpouseTarg + CalcPrem.ChildTarg
    For i = 1 To 9
	CalcPrem.TotTarg = CalcPrem.TotTarg + CalcPrem.InsdTarg(i)
    Next i
    ScreenSum = CalcPrem.TotTarg
    Call Format_Amount(ScreenValue, 12, ScreenSum, " ")
    frmLSPREM.txtTargPrem.Text = ScreenValue
    Exit Sub

Modalize_Target:
    
    Select Case LSClient(s).Mode
       Case "A"
       Case "S"
	  TargPrem = TargPrem / 2
       Case "Q"
	  TargPrem = TargPrem / 4
       Case Else
	  TargPrem = TargPrem / 12
    End Select
    
    Return
    
End Sub

Sub Format_Ledger_Amount (AmtStr As String, AmtLen As Integer, AmtVal As Currency, Comma As String, Nsign As Integer)

    Dim TempStr As String
    Dim Ctr As Integer

    AmtStr = Space$(AmtLen% + 1)
    If Comma = "Y" Then
       TempStr = Format$(AmtVal, "###,###,##0")
    Else
       TempStr = Format$(AmtVal, "#######0")
    End If
    If Len(TempStr) > AmtLen + 1 Then
       Mid$(AmtStr, Len(TempStr$) - AmtLen, AmtLen) = TempStr$
    Else
       Mid$(AmtStr, AmtLen - Len(TempStr$) + 2, Len(TempStr$)) = TempStr$
    End If
    If Nsign Then
       For Ctr = 1 To AmtLen
	 If Mid$(AmtStr, Ctr, 1) <> " " Then
	    Mid$(AmtStr, Ctr - 1, 1) = "-"
	    Ctr = AmtLen
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
    Dim ReturnRate As Single
    Dim Tables As Single
    Dim BandFace As Single

    s = SpouseClient
    If LSClient(s).State = "MT" Then
       Sex = "M"
    Else
       Sex = LSClient(s).SexCode
    End If
    If LSClient(s).PlanCode = 1 Then
       Smoke = "N"
    Else
       Smoke = "S"
    End If

    For i = 0 To 1   'Guaranteed or current
      For j = 0 To 94   'All possible years
	If i = 0 And j > LSClient(s).IssueAge Then
	   Band = 5
	Else
	   BandFace = LSClient(s).FaceAmt
	   If LSClient(s).AddFace <> 0 Then
	      If LSClient(s).AddFaceStart >= j And LSClient(s).AddFaceEnd <= j Then
		 BandFace = BandFace + LSClient(s).AddFace
	      End If
	   End If
	   Select Case BandFace
	      Case 50000 To 99999
		 Band = 2
	      Case 100000 To 249999
		 Band = 3
	      Case Is >= 250000
		 Band = 4
	      Case Else
		 Band = 1
	   End Select
	End If
	If Band = 5 Then
	   If Verify_LSRATEPR(LS_RateFiles(3), "P", Sex, Smoke, j, Band, ReturnRate) = True Then
	      LSMort(i, j).Prin_Base(1) = ReturnRate
	      LSMort(i, j).Prin_Base(2) = ReturnRate
	      LSMort(i, j).Prin_Base(3) = ReturnRate
	      LSMort(i, j).Prin_Base(4) = ReturnRate
	   Else
	      MsgText = "Mortality rate not found for Client (sex = " & Sex & ", age = " & Str$(i) & ", smoker = " & Smoke & " face amount band = " & Str$(Band) & ".  Illustration NOT VALID!!"
	      MsgBox MsgText
	      Exit Sub
	   End If
	Else
	   LS_Prin_Band = Band
	   For Band = 1 To 4
	      If Verify_LSRATEPR(LS_RateFiles(3), "P", Sex, Smoke, j, Band, ReturnRate) = True Then
		 LSMort(i, j).Prin_Base(Band) = ReturnRate
	      Else
		 MsgText = "Mortality rate not found for Client (sex = " & Sex & ", age = " & Str$(i) & ", smoker = " & Smoke & " face amount band = " & Str$(Band) & ".  Illustration NOT VALID!!"
		 MsgBox MsgText
		 Exit Sub
	      End If
	   Next Band
	End If
	If LSClient(s).SubStd > 0 Then
	   Tables = LSClient(s).SubStd / 4
	   If Verify_LSRATESB(LS_RateFiles(5), Smoke, j, ReturnRate) = True Then
	      For Band = 1 To 4
		 LSMort(i, j).Prin_Sub(Band) = LSMort(i, j).Prin_Base(Band) * Tables * ReturnRate
	      Next Band
	   End If
	   LSMort(i, j).Prin_CSO80 = 0
	Else
	   If Verify_CSO80(LS_RateFiles(11), Sex, Smoke, j, ReturnRate) = True Then
	      LSMort(i, j).Prin_CSO80 = ReturnRate
	   Else
	      LSMort(i, j).Prin_CSO80 = 0
	   End If
	End If
	If LSClient(s).GPO <> 0 Then  'Guaranteed Purchase
	   If j < 41 Then
	      If Verify_LSRATEGP(LS_RateFiles(6), j, ReturnRate) = True Then
		 LSMort(i, j).Prin_GPO = ReturnRate
	      End If
	   End If
	End If
	If LSClient(s).WPD = True Then  'Waiver of Premium
	   If j < 60 Then
	      If Verify_LSRATEWP(LS_RateFiles(7), Sex, j, ReturnRate) = True Then
		 LSMort(i, j).Prin_WPD = ReturnRate
	      End If
	   End If
	End If
	If LSClient(s).AddFace <> 0 Then    'Term Rider on client
	   If LSClient(s).IssueAge + LSClient(s).AddFaceStart - 1 <= j And LSClient(s).IssueAge + LSClient(s).AddFaceEnd >= j Then
	      If Verify_LSRATESI(LS_RateFiles(4), "I", Sex, Smoke, j, i + 1, ReturnRate) = True Then
		 LSMort(i, j).Insured_Base(0) = ReturnRate
	      Else
		 MsgText = "Mortality rate not found for Client Term Rider (age = " & Str$(i) & ", smoker = " & Smoke & ".  Illustration NOT VALID!!"
		 MsgBox MsgText
		 Exit Sub
	      End If
	      If LSClient(s).SubStd > 0 Then
		 Tables = LSClient(s).SubStd / 4
		 If Verify_LSRATESB(LS_RateFiles(5), Smoke, j, ReturnRate) = True Then
		    LSMort(i, j).Insured_Sub(0) = LSMort(i, j).Insured_Base(0) * Tables * ReturnRate
		 End If
		 LSMort(i, j).Insured_CSO80(0) = 0
	      Else
		 If Verify_CSO80(LS_RateFiles(11), Sex, Smoke, j, ReturnRate) = True Then
		    LSMort(i, j).Insured_CSO80(0) = ReturnRate
		 Else
		    LSMort(i, j).Insured_CSO80(0) = 0
		 End If
	      End If
	   End If
	End If
      Next j
    Next i

    If LSClient(s).SpouseRider Then    'Spouse rider rates
       If LSSpouseRider.PlanCode = 1 Then
	  Smoke = "N"
       Else
	  Smoke = "S"
       End If
       If LSClient(s).SexCode = "M" Then
	  Sex = "F"
       Else
	  Sex = "M"
       End If
       
       For i = 0 To 1   'Guaranteed or current
	 For j = 0 To 94   'All possible years
	   If Verify_LSRATESI(LS_RateFiles(4), "S", Sex, Smoke, j, i + 1, ReturnRate) = True Then
	      LSMort(i, j).Spouse_Base = ReturnRate
	   Else
	      MsgText = "Mortality rate not found for Spouse (age = " & Str$(i) & ", smoker = " & Smoke & ".  Illustration NOT VALID!!"
	      MsgBox MsgText
	      Exit Sub
	   End If
	   If LSSpouseRider.SubStd > 0 Then
	      Tables = LSSpouseRider.SubStd / 4
	      If Verify_LSRATESB(LS_RateFiles(5), Smoke, j, ReturnRate) = True Then
		 LSMort(i, j).Spouse_Sub = LSMort(i, j).Spouse_Base * Tables * ReturnRate
	      End If
	      LSMort(i, j).Spouse_CSO80 = 0
	   Else
	      If Verify_CSO80(LS_RateFiles(11), Sex, Smoke, j, ReturnRate) = True Then
		 LSMort(i, j).Spouse_CSO80 = ReturnRate
	      Else
		 LSMort(i, j).Spouse_CSO80 = 0
	      End If
	   End If
	 Next j
       Next i
    End If

    For k = 1 To 9
      If LSClient(s).InsuredRider(k) Then    'Addtl insd rider rates
	 If LSInsuredRider(k).PlanCode = 1 Then
	    Smoke = "N"
	 Else
	    Smoke = "S"
	 End If
	 Sex = LSInsuredRider(k).SexCode
	 For i = 0 To 1   'Guaranteed or current
	   For j = 0 To 94   'All possible years
	     If Verify_LSRATESI(LS_RateFiles(4), "I", Sex, Smoke, j, i + 1, ReturnRate) = True Then
		LSMort(i, j).Insured_Base(k) = ReturnRate
	     Else
		MsgText = "Mortality rate not found for Insured (age = " & Str$(i) & ", smoker = " & Smoke & ".  Illustration NOT VALID!!"
		MsgBox MsgText
		Exit Sub
	     End If
	     If LSInsuredRider(k).SubStd > 0 Then
		Tables = LSInsuredRider(k).SubStd / 4
		If Verify_LSRATESB(LS_RateFiles(5), Smoke, j, ReturnRate) = True Then
		   LSMort(i, j).Insured_Sub(k) = LSMort(i, j).Insured_Base(k) * Tables * ReturnRate
		End If
		LSMort(i, j).Insured_CSO80(k) = 0
	     Else
		If Verify_CSO80(LS_RateFiles(11), Sex, Smoke, j, ReturnRate) = True Then
		   LSMort(i, j).Insured_CSO80(k) = ReturnRate
		Else
		   LSMort(i, j).Insured_CSO80(k) = 0
		End If
	     End If
	   Next j
	 Next i
      End If
    Next k

End Sub

Sub Insured_Rider_Mortality (Which As Integer, GuideSingle As Integer)
    
    Dim Insured_Age As Integer
    Dim Ledger_Year As Integer
    ReDim Temp_Mort(1) As Single

    Insured_Age = AnnYr - LSClient(SpouseClient).IssueAge + LSInsuredRider(Which).IssueAge
    Ledger_Year = Insured_Age - LSInsuredRider(Which).IssueAge + 1
    If Ledger_Year >= LSInsuredRider(Which).StartYear And Ledger_Year <= LSInsuredRider(Which).EndYear Then
       If GuideSingle Then
	  If LSMort(0, Insured_Age).Insured_CSO80(Which) < LSMort(0, Insured_Age).Insured_Base(Which) Then
	     Temp_Mort(0) = LSMort(0, Insured_Age).Insured_CSO80(Which)
	  Else
	     Temp_Mort(0) = LSMort(0, Insured_Age).Insured_Base(Which)
	  End If
	  If LSMort(1, Insured_Age).Insured_CSO80(Which) < LSMort(1, Insured_Age).Insured_Base(Which) Then
	     Temp_Mort(1) = LSMort(1, Insured_Age).Insured_CSO80(Which)
	  Else
	     Temp_Mort(1) = LSMort(1, Insured_Age).Insured_Base(Which)
	  End If
       Else
	  Temp_Mort(0) = LSMort(0, Insured_Age).Insured_Base(Which)
	  Temp_Mort(1) = LSMort(1, Insured_Age).Insured_Base(Which)
       End If

       If LSInsuredRider(Which).StartYear = Ledger_Year Then
	  LS_Insureds_MortCost = LS_Insureds_MortCost + ((LSInsuredRider(Which).FaceAmt * Temp_Mort(1)) / 1000)
	  If LSInsuredRider(Which).SubStd > 0 Then
	     LS_Insureds_MortCost = LS_Insureds_MortCost + ((LSInsuredRider(Which).FaceAmt * LSMort(1, Insured_Age).Insured_Sub(Which)) / 1000)
	  End If
       Else
	  LS_Insureds_MortCost = LS_Insureds_MortCost + ((LSInsuredRider(Which).FaceAmt * Temp_Mort(GuarCurr)) / 1000)
	  If LSInsuredRider(Which).SubStd > 0 Then
	     LS_Insureds_MortCost = LS_Insureds_MortCost + ((LSInsuredRider(Which).FaceAmt * LSMort(GuarCurr, Insured_Age).Insured_Sub(Which)) / 1000)
	  End If
       End If
    End If

End Sub

Sub LifeSave_Close_Text (Illpage As String, section As Integer, Irate As Single)

    Dim OneLine As String
    Dim OnePage As String
    Dim HaveRiders As Integer

    s = SpouseClient
    NL = Chr$(13) & Chr$(10)  'carriage return, line feed
    OnePage = ""
    
    If LIFELINE > 28 Then
       For k = LIFELINE To 59
	 OnePage = OnePage & NL
       Next k
       OnePage = OnePage & NL & Space$(15) & "THIS ILLUSTRATION IS NOT VALID WITHOUT ALL PAGES" & NL
       Illpage = Illpage & OnePage
       Illpage = Illpage & "------------------------------------------------------------------- End Page " & Format(LIFEPAGE, "#")
       PrintIllusPage(LIFEPAGE - 1) = PrintIllusPage(LIFEPAGE - 1) & OnePage
       OnePage = ""
       LIFEPAGE = LIFEPAGE + 1
       Call LifeSave_Illus_Head(OnePage, False)
       Illpage = Illpage & OnePage
       OnePage = ""
    End If

    OnePage = OnePage & "Guideline Annual Premium: $" & Format$(CalcPrem.GuideAnnual, "#,###,##0.00")
    If CalcPrem.GuideSingleAddInsd > 0 Then
       OnePage = OnePage & "     Guideline Single Premium: $" & Format$(CalcPrem.GuideSingleAddInsd, "##,###,##0.00") & NL
    Else
       OnePage = OnePage & "     Guideline Single Premium: $" & Format$(CalcPrem.GuideSingle, "##,###,##0.00") & NL
    End If
    LIFELINE = LIFELINE + 1

    If Val(frmLSPREM.txtBeginCashValue.Text) > 0 Then
       OnePage = OnePage & NL & "This illustration begins at age "
       OnePage = OnePage & Format$(Val(frmLSPREM.txtBeginYear.Text), "##")
       OnePage = OnePage & " with an accumulated cash value of $"
       OnePage = OnePage & Format$(Val(frmLSPREM.txtBeginCashValue.Text), "######0.00") & "." & NL
       LIFELINE = LIFELINE + 2
    End If

    If LSClient(s).SubStd > 0 Then
       OnePage = OnePage & NL & "This illustration includes a monthly deduction for extra risk premium on the" & NL
       OnePage = OnePage & "principal insured of " & Format$(LSClient(s).SubStd / .04, "###") & "%"
       OnePage = OnePage & " for " & Format$(LSClient(s).YearsToPay, "##") & " years." & NL
       LIFELINE = LIFELINE + 3
    End If
    
    If LSClient(s).FutureChanges = True Then
       If LSFuture(1).Int_Age > 0 Then
	  OnePage = OnePage & NL & "The projected values are based on the following interest rates:"
	  OnePage = OnePage & NL & Space$(21) & Format$(LSClient(s).CurrRate, "##.000") & "% through age "
	  LIFELINE = LIFELINE + 2
	  For i = 2 To 5
	    OnePage = OnePage & Format$(LSFuture(i - 1).Int_Age - 1, "#0") & NL
	    OnePage = OnePage & Space$(21) & Format$(LSFuture(i - 1).Int_Amount, "##.000") & "% through age "
	    If i = 5 Or LSFuture(i).Int_Age = 0 Then OnePage = OnePage & Format$(95, "#0") & NL
	    If LSFuture(i).Int_Age = 0 Then
	       i = 5   'forces end of loop
	    End If
	    LIFELINE = LIFELINE + 2
	  Next i
       End If
       If LSFuture(1).Opt_Age > 0 Then
	  OnePage = OnePage & NL & "Change in Death Benefit Option to Option " & LSFuture(1).Opt_Type
	  OnePage = OnePage & " at age " & Format$(LSFuture(1).Opt_Age, "#0") & NL
	  LIFELINE = LIFELINE + 1
	  For i = 2 To 5
	    If LSFuture(i).Opt_Age > 0 Then
	       OnePage = OnePage & NL & "Change in Death Benefit Option to Option " & LSFuture(i).Opt_Type
	       OnePage = OnePage & " at age " & Format$(LSFuture(i).Opt_Age, "#0") & NL
	       LIFELINE = LIFELINE + 1
	    End If
	    If LSFuture(i).Opt_Age = 0 Then
	       i = 5   'forces end of loop
	    End If
	  Next i
       End If
    End If

    OnePage = OnePage & NL & "This illustration includes the following riders:"
    OnePage = OnePage & NL
    LIFELINE = LIFELINE + 2
    HaveRiders = False
    If LSClient(s).GPO <> 0 Then
       HaveRiders = True
       OnePage = OnePage & NL & "  Guaranteed Added Death Benefit of $" & Format$(LSClient(s).GPO, "#####")
       OnePage = OnePage & " to age 41"
       LIFELINE = LIFELINE + 1
    End If
    If LSClient(s).WPD = True Then
       HaveRiders = True
       OnePage = OnePage & NL & "  Disability Waiver of Cost to age 60"
       LIFELINE = LIFELINE + 1
    End If
    If LSClient(s).COLA <> 0 Then
       HaveRiders = True
       OnePage = OnePage & NL & "  Cost of Living Increase Illustrated for Principal Insured to age 65 at " & Format$(LSClient(s).COLA, "##") & "%"
       LIFELINE = LIFELINE + 1
    End If
    If LSClient(s).AddFace <> 0 Then
       HaveRiders = True
       OnePage = OnePage & NL & "  Additional Death Benefit for Principal Insured of $" & Format$(LSClient(s).AddFace, "#######")
       OnePage = OnePage & " (Ages " & Format$(LSClient(s).AddFaceStart + LSClient(s).IssueAge - 1, "##")
       OnePage = OnePage & " to " & Format$(LSClient(s).AddFaceEnd + LSClient(s).IssueAge, "##") & ")"
       LIFELINE = LIFELINE + 1
    End If
    If LSClient(s).SpouseRider = True Then
       HaveRiders = True
       OnePage = OnePage & NL & "  Spouse Term Life $" & Format$(LSSpouseRider.FaceAmt, "#####")
       OnePage = OnePage & " to age " & Format$(LSSpouseRider.IssueAge + LSSpouseRider.RemoveYear, "##")
       If LSClient(s).SexCode = "M" Then
	  OnePage = OnePage & " (Female age " & Format$(LSSpouseRider.IssueAge, "##")
       Else
	  OnePage = OnePage & " (Male age " & Format$(LSSpouseRider.IssueAge, "##")
       End If
       If LSSpouseRider.PlanCode = 1 Then
	  OnePage = OnePage & " NonSmoker)"
       Else
	  OnePage = OnePage & " Smoker)"
       End If
       If LSSpouseRider.SubStd <> 0 Then
	  OnePage = OnePage & "(Substandard " & Format$(LSSpouseRider.SubStd / .04, "###") & "%)"
       End If
       LIFELINE = LIFELINE + 1
    End If
    If LSClient(s).ChildRider = True Then
       HaveRiders = True
       OnePage = OnePage & NL & "  Children's Term Life $" & Format$(LSChildRider.FaceAmt, "#####")
       OnePage = OnePage & " for " & Format$(23 - LSChildRider.AgeYoungest, "##") & " years"
       LIFELINE = LIFELINE + 1
    End If
    For i = 1 To 9
       If LSClient(s).InsuredRider(i) = True Then
	  HaveRiders = True
	  OnePage = OnePage & NL & "  Additional Insured Term Life $" & Format$(LSInsuredRider(i).FaceAmt, "#####")
	  OnePage = OnePage & " in years " & Format$(LSInsuredRider(i).StartYear, "##") & " to " & Format$(LSInsuredRider(i).EndYear, "##") & NL
	  If LSInsuredRider(i).SexCode = "M" Then
	     OnePage = OnePage & "    (Male age " & Format$(LSInsuredRider(i).IssueAge, "##")
	  Else
	     OnePage = OnePage & "    (Female age " & Format$(LSInsuredRider(i).IssueAge, "##")
	  End If
	  If LSInsuredRider(i).PlanCode = 1 Then
	     OnePage = OnePage & " NonSmoker)"
	  Else
	     OnePage = OnePage & " Smoker)"
	  End If
	  If LSInsuredRider(i).SubStd <> 0 Then
	     OnePage = OnePage & "(Substandard " & Format$(LSInsuredRider(i).SubStd / .04, "###") & "%)"
	  End If
	  LIFELINE = LIFELINE + 2
       End If
    Next i
    If HaveRiders = False Then
       OnePage = OnePage & NL & "   * * * N O N E * * * "
       LIFELINE = LIFELINE + 1
    End If
    Illpage = Illpage & OnePage
    PrintIllusPage(LIFEPAGE - 1) = PrintIllusPage(LIFEPAGE - 1) & OnePage
    OnePage = ""
    
    Call Print_Cost_Indices(OnePage)
    LIFELINE = LIFELINE + 10
    Illpage = Illpage & OnePage
    PrintIllusPage(LIFEPAGE - 1) = PrintIllusPage(LIFEPAGE - 1) & OnePage
    OnePage = ""
    
    If LIFELINE > 42 Then
       For k = LIFELINE To 59
	 OnePage = OnePage & NL
       Next k
       OnePage = OnePage & NL & Space$(15) & "THIS ILLUSTRATION IS NOT VALID WITHOUT ALL PAGES" & NL
       Illpage = Illpage & OnePage
       Illpage = Illpage & "------------------------------------------------------------------- End Page " & Format(LIFEPAGE, "#")
       PrintIllusPage(LIFEPAGE - 1) = PrintIllusPage(LIFEPAGE - 1) & OnePage
       OnePage = ""
       LIFEPAGE = LIFEPAGE + 1
       Call LifeSave_Illus_Head(OnePage, False)
       Illpage = Illpage & OnePage
       OnePage = ""
    End If
    
    filnum = FreeFile
    Open "life\lsclos.txt" For Input As #filnum
    
    'Find the specified section

    Do Until EOF(filnum)
       Line Input #filnum, OneLine
       If Left$(OneLine, 1) = ">" Then
	  If Val(Mid$(OneLine, 2)) = section Then
	     Exit Do
	  End If
       End If
    Loop
   
    Do While Not EOF(filnum)
       Line Input #filnum, OneLine
       If Left$(OneLine, 1) = ">" Then Exit Do
       If Mid$(OneLine, 58, 4) = "XXXX" Then GoSub Insert_Rate
       OnePage = OnePage & OneLine & NL
       LIFELINE = LIFELINE + 1
    Loop
    Close #filnum
    
    Illpage = Illpage & OnePage
    PrintIllusPage(LIFEPAGE - 1) = PrintIllusPage(LIFEPAGE - 1) & OnePage
    OnePage = ""
    OnePage = NL & "Control Code:" & NL
    OnePage = OnePage & "T" & Format$(CalcPrem.TotTarg, "####0.00")
    If LSClient(SpouseClient).PlannedPrem + LSClient(SpouseClient).LumpSum > CalcPrem.TotTarg Then
       OnePage = OnePage & "E" & Format$(LSClient(SpouseClient).PlannedPrem + LSClient(SpouseClient).LumpSum - CalcPrem.TotTarg, "####0.00")
    Else
       OnePage = OnePage & "E0.00"
    End If
    OnePage = OnePage & "M" & Format$(CalcPrem.TotMin, "####0.00")
    OnePage = OnePage & LSClient(SpouseClient).Mode
    
    For k = LIFELINE To 59
      OnePage = OnePage & NL
    Next k
    OnePage = OnePage & NL & Space$(29) & "END OF ILLUSTRATION" & NL
    
    Illpage = Illpage & OnePage
    Illpage = Illpage & "------------------------------------------------------------------- End Page " & Format(LIFEPAGE, "#")
    PrintIllusPage(LIFEPAGE - 1) = PrintIllusPage(LIFEPAGE - 1) & OnePage

    Exit Sub

Insert_Rate:

    If Irate < 10 Then
       Mid$(OneLine$, 58, 4) = Format$(Irate, "0.00")
    Else
       Mid$(OneLine$, 58, 21) = Format$(Irate, "00.00") & Mid$(OnePage$, 62, 18)
    End If
    Return

End Sub

Sub LifeSave_Illus_Head (Illpage As String, ColHead As Integer)

     Dim OneLine As String
     
     NL = Chr$(13) & Chr$(10) 'carriage return, line feed
     
     PrintIllusPage(LIFEPAGE - 1) = ""
     s = SpouseClient
     Illpage = Illpage & NL
     OneLine = Format$(Now, "dddd")
     'OneLine = OneLine & Space$(16 - Len(OneLine)) & "*** SAMPLE ***   LIFE SAVINGS   *** SAMPLE ***" & Space$(10) & "Page " & Format$(LIFEPAGE, "#") & NL
     OneLine = OneLine & Space$(33 - Len(OneLine)) & "LIFE SAVINGS" & Space$(27) & "Page " & Format$(LIFEPAGE, "#") & NL
     Illpage = Illpage & OneLine
     OneLine = Format$(Now, "mm/dd/yyyy") & Space$(12) & "American Republic Insurance Company" & NL
     Illpage = Illpage & OneLine
     OneLine = Format$(Now, "hh:mm AM/PM") & Space$(11) & "(A-2670) Life Insurance Policy Illustration" & NL
     Illpage = Illpage & OneLine
     OneLine = Space$(33) & "Prepared For:" & NL
     Illpage = Illpage & OneLine & NL & NL
     OneLine = Space$(39 - Len(Trim$(LSClient(s).ClientName)) / 2) & Trim$(LSClient(s).ClientName) & NL & NL
     OneLine = OneLine & "Illustrating:" & Space$(40) & "Provided by:" & NL
     Illpage = Illpage & OneLine
     OneLine = Space$(1) & "Option "
     If LSClient(s).Option = 1 Then
	OneLine = OneLine & "1 (Level)     "
     Else
	OneLine = OneLine & "2 (Increasing)"
     End If
     If LSClient(s).SexCode = "M" Then
	OneLine = OneLine & "      Male  "
     Else
	OneLine = OneLine & "      Female"
     End If
     OneLine = OneLine & Space$(8) & "Age" & Str$(LSClient(s).IssueAge) & Space$(5)
     OneLine = OneLine & Trim$(LSClient(s).ContactName) & NL
     If LSClient(s).PlanCode = 1 Then
	OneLine = OneLine & " Non-Smoker "
     Else
	OneLine = OneLine & " Smoker     "
     End If
     OneLine = OneLine & Space$(41) & Trim$(LSClient(s).ContactPhone) & NL & NL
     OneLine = OneLine & "Initial Death Benefit:" & Space$(31) & "Premium Mode:" & NL
     Illpage = Illpage & OneLine
     OneLine = Space$(1) & Format$(LSClient(s).FaceAmt, "$#,#00,000")
     OneLine = OneLine & Space$(53 - Len(OneLine))
     Select Case (LSClient(s).Mode)
	Case "A"
	   OneLine = OneLine & "Annual     " & NL
	Case "Q"
	   OneLine = OneLine & "Quarterly  " & NL
	Case "S"
	   OneLine = OneLine & "Semi-Annual" & NL
	Case Else
	   OneLine = OneLine & "Monthly APP" & NL
     End Select
     
     Illpage = Illpage & OneLine & NL & NL
     If ColHead = True Then
	OneLine = Space$(24) & "Guaranteed Values" & Space$(16) & "Projected Values" & NL
	OneLine = OneLine & Space$(24) & "-----------------" & Space$(16) & "----------------" & NL
	OneLine = OneLine & Space$(4) & "End              Cash" & Space$(28) & "Cash" & NL
	OneLine = OneLine & Space$(4) & " of  Annual   Surrender    Cash     Death     Surrender    Cash     Death" & NL
	OneLine = OneLine & "Age Year Outlay     Value      Value   Benefit       Value     Value   Benefit" & NL
	OneLine = OneLine & "--- ---- ------  -----------------------------   -----------------------------" & NL
	Illpage = Illpage & OneLine
	LIFELINE = 23
     Else
	LIFELINE = 18
     End If
     PrintIllusPage(LIFEPAGE - 1) = Illpage

End Sub

Sub LifeSave_Ledger (Illpage As String)

     Dim StartAge As Integer
     Dim IllAge As Integer
     Dim ShowIll As Integer
     Dim ShowYear As Integer
     Dim Comma As String
     Dim AmtLen As Integer
     Dim AmtStr As String
     Dim AmtVal As Currency
     Dim OnePage As String
     Dim Age_EndYear As String
     Dim Age_EndYear_Plus1 As String

     NL = Chr$(13) & Chr$(10)  'carriage return, line feed
     OnePage = ""
     s = SpouseClient
     If Val(frmLSPREM.txtBeginYear.Text) = 0 Then
	StartAge = LSClient(s).IssueAge
     Else
	StartAge = Val(frmLSPREM.txtBeginYear.Text)
     End If
     Select Case LSClient(s).IllusType
	Case 100 To 195
	   ShowIll = 1
	   IllAge = LSClient(s).IllusType - 100
	Case 200 To 295
	   ShowIll = 2
	   IllAge = LSClient(s).IllusType - 200
	Case 299
	   ShowIll = 4
	   IllAge = 95
	Case Is > 300
	   ShowIll = 3
	   IllAge = LSClient(s).IllusType - 300
	Case Else
     End Select

     i = 1      'Years Illustrated
     If LSLedger(StartAge).DeathBenefit(1) > 9999999 Or LSLedger(IllAge).CashValue(1) > 9999999 Then
	Comma = "N"
     Else
	Comma = "Y"
     End If

     For j = StartAge + 1 To IllAge
       If LIFELINE > 57 Then GoSub Page_Break
       If LSLedger(j).CashValue(1) = 0 And j > StartAge + 3 Then GoTo QuitLedger
       GoSub Check_Year
       If ShowYear = True Then
	  If j < 10 Then
	     Age_EndYear = Space$(1) & Format$(j, "#") & Space$(3)
	     If j < 9 Then
		Age_EndYear_Plus1 = Space$(1) & Format$(j + 1, "#") & Space$(3)
	     Else
		Age_EndYear_Plus1 = Format$(j + 1, "##") & Space$(3)
	     End If
	  Else
	     Age_EndYear = Format$(j, "##") & Space$(3)
	     Age_EndYear_Plus1 = Format$(j + 1, "##") & Space$(3)
	  End If
	  If j - StartAge < 10 Then
	     Age_EndYear = Age_EndYear & Space$(1) & Format$(j - StartAge, "#")
	     If j - StartAge < 9 Then
		Age_EndYear_Plus1 = Age_EndYear_Plus1 & Space$(1) & Format$(j + 1 - StartAge, "#")
	     Else
		Age_EndYear_Plus1 = Age_EndYear_Plus1 & Format$(j + 1 - StartAge, "##")
	     End If
	  Else
	     Age_EndYear = Age_EndYear & Format$(j - StartAge, "##")
	     Age_EndYear_Plus1 = Age_EndYear_Plus1 & Format$(j + 1 - StartAge, "##")
	  End If
	  OnePage = OnePage & Age_EndYear
	  AmtLen = 7
	  AmtVal = LSLedger(j).AnnualOutlay
	  Call Format_Ledger_Amount(AmtStr, AmtLen, AmtVal, "N", False)
	  OnePage = OnePage & AmtStr & Space$(1)
	  For k = 0 To 1
	    AmtLen = 9
	    AmtVal = LSLedger(j).SurrenderValue(k)
	    Call Format_Ledger_Amount(AmtStr, AmtLen, AmtVal, Comma, False)
	    OnePage = OnePage & AmtStr
	       
	    AmtVal = LSLedger(j).CashValue(k)
	    Call Format_Ledger_Amount(AmtStr, AmtLen, AmtVal, Comma, False)
	    OnePage = OnePage & AmtStr
	    
	    AmtVal = LSLedger(j).DeathBenefit(k)
	    Call Format_Ledger_Amount(AmtStr, AmtLen, AmtVal, Comma, False)
	    OnePage = OnePage & AmtStr & Space$(2)
	  Next k
	  OnePage = OnePage & NL
	  LIFELINE = LIFELINE + 1
	  
	  If i > 1 And i Mod 5 = 0 Then
	     OnePage = OnePage & NL
	     LIFELINE = LIFELINE + 1
	  End If

	  If LIFELINE > 57 Then GoSub Page_Break

	  If LSLedger(j).WithdrawAmount > 0 Then
	     OnePage = OnePage & Age_EndYear_Plus1
	     AmtLen = 7
	     AmtVal = LSLedger(j).WithdrawAmount
	     Call Format_Ledger_Amount(AmtStr, AmtLen, AmtVal, "N", True)
	     OnePage = OnePage & AmtStr & "  Withdrawal" & NL
	     LIFELINE = LIFELINE + 1
	  End If
	  If LSLedger(j).LoanAmount > 0 Or LSLedger(j).LoanRepayAmount > 0 Then
	     OnePage = OnePage & Age_EndYear_Plus1
	     AmtLen = 7
	     If LSLedger(j).LoanAmount > LSLedger(j).LoanRepayAmount Then
		AmtVal = LSLedger(j).LoanAmount - LSLedger(j).LoanRepayAmount
		Call Format_Ledger_Amount(AmtStr, AmtLen, AmtVal, "N", True)
		OnePage = OnePage & AmtStr & "  Loan"
	     Else
		AmtVal = LSLedger(j).LoanRepayAmount - LSLedger(j).LoanAmount
		Call Format_Ledger_Amount(AmtStr, AmtLen, AmtVal, "N", False)
		OnePage = OnePage & AmtStr & "  Repayment of Loan"
	     End If
	     'AmtLen = 6
	     'AmtVal = LSLedger(j).LoanAmount
	     'Call Format_Ledger_Amount(AmtStr, AmtLen, AmtVal, "N", True)
	     'OnePage = OnePage & "$" & AmtStr & " Loan Repay: "
	     'AmtLen = 6
	     'AmtVal = LSLedger(j).LoanRepayAmount
	     'Call Format_Ledger_Amount(AmtStr, AmtLen, AmtVal, "N", False)
	     'OnePage = OnePage & "$" & AmtStr & " Loan Balance: "
	     'AmtLen = 9
	     'AmtVal = LSLedger(j).LoanBalance
	     'Call Format_Ledger_Amount(AmtStr, AmtLen, AmtVal, "N", False)
	     'OnePage = OnePage & "$" & AmtStr & NL
	     OnePage = OnePage & NL
	     LIFELINE = LIFELINE + 1
	  End If

	  i = i + 1
       End If
     Next j
       
QuitLedger:
     If LIFELINE > 23 Then
	Illpage = Illpage & OnePage
	PrintIllusPage(LIFEPAGE - 1) = PrintIllusPage(LIFEPAGE - 1) & OnePage
     End If
     Exit Sub

Check_Year:

     ShowYear = True
     If ShowIll = 1 Or ShowIll = 2 Then Return
     If ShowIll = 3 And (i < 11 Or (j > 11 And j Mod 5 = 0)) Then Return
     If ShowIll = 4 And i < 26 Then Return
     If ShowIll = 4 And (j = LSClient(s).HighAges(0) Or j = LSClient(s).HighAges(1) Or j = LSClient(s).HighAges(2) Or j = LSClient(s).HighAges(3) Or j = LSClient(s).HighAges(4)) Then Return
     ShowYear = False
     Return

Page_Break:

     For k = LIFELINE To 59
       OnePage = OnePage & NL
     Next k
     OnePage = OnePage & NL & Space$(15) & "THIS ILLUSTRATION IS NOT VALID WITHOUT ALL PAGES" & NL
     Illpage = Illpage & OnePage
     Illpage = Illpage & "------------------------------------------------------------------- End Page " & Format(LIFEPAGE, "#")
     PrintIllusPage(LIFEPAGE - 1) = PrintIllusPage(LIFEPAGE - 1) & OnePage
     LIFEPAGE = LIFEPAGE + 1
     OnePage = ""
     If j < IllAge Then
	Call LifeSave_Illus_Head(OnePage, True)
     Else
	Call LifeSave_Illus_Head(OnePage, False)
     End If
     Illpage = Illpage & OnePage
     OnePage = ""

     Return

End Sub

Sub Load_Agent ()

    Dim FileNum As Integer
    Dim TempAgent As Integer
    Dim RandomAgent As AgtData

    FileNum = FreeFile
    TempAgent = AgtInfo.Record

    Open "PROFILE.DAT" For Random As FileNum Len = Len(RandomAgent)
    Get FileNum, TempAgent, RandomAgent
    AgtInfo.FullName = RandomAgent.FullName
    AgtInfo.OfficePhone = RandomAgent.OfficePhone
    Close FileNum

End Sub

Sub Load_Client ()
	
    Dim FileNum As Integer
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
      If LSFuture(i).Opt_Age > 0 Then
	 frmLSCHANGE.txtChangeDBOptAge(i - 1).Text = Format$(LSFuture(i).Opt_Age, "##")
	 frmLSCHANGE.txtChangeDBOpt(i - 1).Text = Format$(LSFuture(i).Opt_Type, "#")
	 frmLSCHANGE.chkChangeDBOpt.Value = 1
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

    If LSClient(s).SpouseRider = True Then
       frmLSINSD.txtSpouseAge.Text = Format$(LSSpouseRider.IssueAge, "##")
       Select Case (LSSpouseRider.FaceAmt)
	  Case 25000
	     frmLSINSD.cboSpouseFace.ListIndex = 1
	  Case Else
	     frmLSINSD.cboSpouseFace.ListIndex = 0
       End Select
       Select Case (LSSpouseRider.PlanCode)
	  Case 2
	     frmLSINSD.cboSpousePlan.ListIndex = 1
	  Case Else
	     frmLSINSD.cboSpousePlan.ListIndex = 0
       End Select
       Select Case LSSpouseRider.SubStd
	  Case 1 To 20
	     frmLSINSD.cboSpouseSubStd.ListIndex = LSSpouseRider.SubStd
	  Case Else
	     frmLSINSD.cboSpouseSubStd.ListIndex = 0
       End Select
       frmLSINSD.cboSpouseCOLA.ListIndex = LSClient(SpouseClient).COLA
       frmLSINSD.cboSpouseYearsAge.ListIndex = 0
       frmLSINSD.txtSpouseYears.Text = Format$(LSSpouseRider.RemoveYear, "##")
       frmLSINSD.chkSpouse.Value = 1
    Else
       frmLSINSD.chkSpouse.Value = 0
    End If

    For i = 1 To 9
      If LSClient(s).InsuredRider(i) = True Then
	 frmLSINSD.txtAddInsdAge(i - 1).Text = Format$(LSInsuredRider(i).IssueAge, "##")
	 frmLSINSD.txtAddInsdSex(i - 1).Text = LSInsuredRider(i).SexCode
	 frmLSINSD.txtAddInsdFace(i - 1).Text = Format$(LSInsuredRider(i).FaceAmt, "#######")
	 Select Case (LSInsuredRider(i).PlanCode)
	    Case 2
	       frmLSINSD.cboAddInsdPlan(i - 1).ListIndex = 1
	    Case Else
	       frmLSINSD.cboAddInsdPlan(i - 1).ListIndex = 0
	 End Select
	 Select Case LSInsuredRider(i).SubStd
	    Case 1 To 20
	       frmLSINSD.cboAddInsdSubStd(i - 1).ListIndex = LSInsuredRider(i).SubStd
	    Case Else
	       frmLSINSD.cboAddInsdSubStd(i - 1).ListIndex = 0
	 End Select
	 frmLSINSD.cboAddInsdCOLA(i - 1).ListIndex = LSClient(SpouseClient).COLA
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

Sub Load_LS_From_Client ()
 
    Dim RetCd As Integer
    Dim SerialBDt As Long
    Dim TempDate As String
    Dim CalcYear As Integer
    Dim YMDBdate As Long

    If AgtInfo.Record <> 0 Then
       If CallFromMain <> "NO" Then Call Load_Agent
       frmLIFESV.txtPreparedBy.Text = AgtInfo.FullName
       If Val(AgtInfo.OfficePhone) > 0 Then
	  frmLIFESV.txtPhoneContact.Text = AgtInfo.OfficePhone & " Ext(_____)"
       Else
	  frmLIFESV.txtPhoneContact.Text = "(___) ___-____ Ext(_____)"
       End If
       Call Load_Client
    Else
       frmLIFESV.txtPreparedBy.Text = ""
       frmLIFESV.txtPhoneContact.Text = "(___) ___-____ Ext(_____)"
    End If

    If ClientInfo.ClientNumber > 0 Then
       frmLIFESV.txtClientFirstName.Text = ClientInfo.FirstName
       frmLIFESV.txtClientMidInit.Text = ClientInfo.Initial
       frmLIFESV.txtClientLastName.Text = ClientInfo.LastName
       frmLIFESV.txtClientState.Text = ClientInfo.State
       frmLIFESV.txtClientSex.Text = ClientInfo.Gender
       RetCd = insured_file(1, ClientInfo.ClientNumber, 0)
       If RetCd Then
	  Select Case (InsuredInfo.insTobacco)
	     Case "Y", "C", "P", "S"
		frmLIFESV.cboClientPlan.ListIndex = 1
	     Case Else
		frmLIFESV.cboClientPlan.ListIndex = 0
	  End Select
       Else
	  frmLIFESV.cboClientPlan.ListIndex = 0
       End If
       today = Now
       If ClientInfo.BirthDate <> 0 Then
	  today = Fix(Now)
	  SerialBDt = ClientInfo.BirthDate
	  Call Year_Calc(SerialBDt, today, CalcYear, YMDBdate)
	  frmLIFESV.txtClientAge.Text = Trim$(Str$(CalcYear))
	  TempDate = Format$(Month(SerialBDt), "00") + "/" + Format$(Day(SerialBDt), "00") + "/" + Format$(Right$(Str$(Year(SerialBDt)), 4), "0000")
	  frmLIFESV.txtClientBirth.Text = TempDate
       Else
	  frmLIFESV.txtClientAge.Text = "  "
	  frmLIFESV.txtClientBirth.Text = "__/__/____"
       End If
       frmLIFESV.txtClientFace.Text = "_______"
    End If

    MsgFile = "life\LIFEHLP.MSG"
    ClientChanged = False

End Sub

Sub Load_Spouse_As_Client ()
    
    Dim Spouse_FirstName As String
    Dim Spouse_MidInit As String
    Dim Spouse_LastName As String
    Dim RetCd As Integer
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
       If LSClient(1).PlanCode = 1 Then
	  frmLSSPOUSE.cboSpousePlan.ListIndex = 0
       Else
	  frmLSSPOUSE.cboSpousePlan.ListIndex = 1
       End If
       frmLSSPOUSE.txtSpouseState.Text = LSClient(1).State
       If LSClient(1).Option = 1 Then
	  frmLSSPOUSE.cboSpouseOption.ListIndex = 0
       Else
	  frmLSSPOUSE.cboSpouseOption.ListIndex = 1
       End If
       If LSClient(1).SubStd = 0 Then
	  frmLSSPOUSE.cboSpouseSubStd.ListIndex = 0
       Else
	  frmLSSPOUSE.cboSpouseSubStd.ListIndex = LSClient(1).SubStd
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
	  frmLSSPOUSE.cboSpouseCOLA.ListIndex = 0
       Else
	  frmLSSPOUSE.cboSpouseCOLA.ListIndex = LSClient(1).COLA - 1
       End If
       If LSClient(1).AddFace > 0 Then
	  frmLSSPOUSE.txtSpouseTermRider.Text = Str$(LSClient(1).AddFace)
	  frmLSSPOUSE.txtSpouseRiderYearEnd.Text = Str$(LSClient(1).AddFaceEnd)
	  frmLSSPOUSE.cboSpouseRiderYrAgeEnd.ListIndex = 0
       End If
       If LSClient(1).CurrRate > 0 Then frmLSSPOUSE.txtSpouseCurrentInt.Text = Format$(LSClient(1).CurrRate, "00.000")
    Else
       If ClientInfo.ClientNumber > 0 Then
	  For k = 0 To 11
	    RetCd = insured_file(1, ClientInfo.ClientNumber, k)
	    If RetCd And InsuredInfo.InsRelation = "S" Then
	       LSClient(1).ClientName = LSClient(0).ClientName
	       GoSub Parse_Spouse_Name
	       frmLSSPOUSE.txtSpouseFirstName.Text = InsuredInfo.InsFName
	       frmLSSPOUSE.txtSpouseLastName.Text = Spouse_LastName
	       Select Case (InsuredInfo.insTobacco)
		  Case "Y", "C", "P", "S"
		     frmLSSPOUSE.cboSpousePlan.ListIndex = 1
		  Case Else
		     frmLSSPOUSE.cboSpousePlan.ListIndex = 0
	       End Select
	       today = Now
	       If InsuredInfo.InsBirthDate <> 0 Then
		  today = Fix(Now)
		  SerialBDt = InsuredInfo.InsBirthDate
		  Call Year_Calc(SerialBDt, today, CalcYear, YMDBdate)
		  frmLSSPOUSE.txtSpouseAge.Text = Trim$(Str$(CalcYear))
		  TempDate = Format$(Month(SerialBDt), "00") + "/" + Format$(Day(SerialBDt), "00") + "/" + Format$(Right$(Str$(Year(SerialBDt)), 4), "0000")
		  frmLSSPOUSE.txtSpouseBirth.Text = TempDate
	       Else
		  frmLSSPOUSE.txtSpouseAge.Text = "  "
		  frmLSSPOUSE.txtSpouseBirth.Text = "__/__/____"
	       End If
	       frmLSSPOUSE.txtSpouseSex.Text = InsuredInfo.InsGender
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

    filnum = FreeFile
    file = "life\lsminp.rdm"
    Open file For Random As #filnum Len = Len(MinpRate)
    LS_RateFiles(0) = filnum     '0 is Minimum Premium

    filnum = FreeFile
    file = "life\lstarg.rdm"
    Open file For Random As #filnum Len = Len(TargRate)
    LS_RateFiles(1) = filnum     '1 is Target Premium

    filnum = FreeFile
    file = "life\lssurr.rdm"
    Open file For Random As #filnum Len = Len(SurrRate)
    LS_RateFiles(2) = filnum     '2 is Surrender Charges

    filnum = FreeFile
    file = "life\lsratepr.rdm"
    Open file For Random As #filnum Len = Len(PrinRate)
    LS_RateFiles(3) = filnum     '3 is Principal Insured

    filnum = FreeFile
    file = "life\lsratesi.rdm"
    Open file For Random As #filnum Len = Len(SIRate)
    LS_RateFiles(4) = filnum     '4 is Spouse/Additional Insureds

    filnum = FreeFile
    file = "life\lsratesb.rdm"
    Open file For Random As #filnum Len = Len(SubRate)
    LS_RateFiles(5) = filnum     '5 is Substandard rates

    filnum = FreeFile
    file = "life\lsrategp.rdm"
    Open file For Random As #filnum Len = Len(GPORate)
    LS_RateFiles(6) = filnum     '6 is GPO rates

    filnum = FreeFile
    file = "life\lsratewp.rdm"
    Open file For Random As #filnum Len = Len(WPDRate)
    LS_RateFiles(7) = filnum     '7 is Waiver of Premium

    filnum = FreeFile
    file = "life\htwtlife.rdm"
    Open file For Random As #filnum Len = Len(LHtWt)
    LS_RateFiles(8) = filnum     '8 is Height/Weight tables

    filnum = FreeFile
    file = "life\lscorr.rdm"
    Open file For Random As #filnum Len = 4
    LS_RateFiles(9) = filnum     '9 is Corridor of Coverage table

    filnum = FreeFile
    file = "life\lsspouse.rdm"
    Open file For Random As #filnum Len = 4
    LS_RateFiles(10) = filnum     '10 is Spouse Death Benefit Adjustment table

    filnum = FreeFile
    file = "life\lscso80.rdm"
    Open file For Random As #filnum Len = Len(CSO80Rate)
    LS_RateFiles(11) = filnum     '11 is CSO80 values for Guideline

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
    Dim Percentage As Double
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
    LS_Irate(GuarCurr) = LSClient(s).CurrRate / 100
    LS_Modal_Prem = CalcPrem.TotMin
    LS_DBOption = LSClient(s).Option
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
    LS_Irate(GuarCurr) = LSClient(s).CurrRate / 100
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
    LS_DBOption = LSClient(s).Option
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
    Percentage = (HighValue - EndValue) / (HighValue - LowValue)
    LS_Modal_Prem = HighGuess - ((HighGuess - LowGuess) * Percentage)
    
    While (HighGuess - LowGuess > .015)
       LS_Outlay = 0
       LS_CV(GuarCurr) = 0
       LS_CSV(GuarCurr) = 0
       LS_DB(GuarCurr) = LSClient(s).FaceAmt
       LS_Irate(GuarCurr) = LSClient(s).CurrRate / 100
       LS_DBOption = LSClient(s).Option
       LS_COLA_Client = 0
       LS_COLA_Spouse = 0
       For i = 1 To 9
	 LS_COLA_Insured(i) = 0
       Next i
    
       Call Process_Ledger_Years(LSClient(s).IssueAge, EndYear, True)

       If Abs(EndValue - LSLedger(EndYear).CashValue(1)) < 1 Then
	  LowGuess = LS_Modal_Prem
	  HighGuess = LS_Modal_Prem
	  LowValue = LSLedger(EndYear).CashValue(1)
	  HighValue = LSLedger(EndYear).CashValue(1)
       Else
	  If EndValue >= LSLedger(EndYear).CashValue(1) Then
	     LowValue = LSLedger(EndYear).CashValue(1)
	     LowGuess = LS_Modal_Prem
	     Percentage = (HighValue - EndValue) / (HighValue - LowValue)
	     LS_Modal_Prem = LowGuess + ((HighGuess - LowGuess) * Percentage)
	  Else
	     HighValue = LSLedger(EndYear).CashValue(1)
	     HighGuess = LS_Modal_Prem
	     Percentage = (HighValue - EndValue) / (HighValue - LowValue)
	     LS_Modal_Prem = HighGuess - ((HighGuess - LowGuess) * Percentage)
	  End If
       End If
    Wend

    frmLSPREM.txtSolvePrem.Text = Format$(LS_Modal_Prem, "     0.00")

End Sub

Sub Principal_Mortality ()
    
    Dim BandFace As Single

    BandFace = LS_DB(GuarCurr) + LS_COLA_Client + LS_LoanBalance

    Select Case BandFace
       Case 50000 To 99999
	  LS_Prin_Band = 2
       Case 100000 To 249999
	  LS_Prin_Band = 3
       Case Is >= 250000
	  LS_Prin_Band = 4
       Case Else
	  LS_Prin_Band = 1
    End Select
    
    LS_MortRate = LSMort(GuarCurr, AnnYr).Prin_Base(LS_Prin_Band)
    
    If LSClient(SpouseClient).SubStd > 0 Then
       LS_MortRate = LS_MortRate + LSMort(GuarCurr, AnnYr).Prin_Sub(LS_Prin_Band)
       If AnnYr = 65 Then
	  LS_MortRate = LS_MortRate
       End If
    End If

End Sub

Sub Principal_Premium ()
    
    Dim Temp_DB As Double

    Temp_DB = LS_DB(GuarCurr) + LS_COLA_Client
    If LS_DBOption = 1 Then
       LS_Charges = (((Temp_DB * .996737) - LS_CV(GuarCurr)) * LS_MortRate) / 1000
    Else
       If LS_CV_Opt2_Start(GuarCurr) = 0 Then
	  LS_Charges = ((Temp_DB * .996737) * LS_MortRate) / 1000
       Else
	  If LS_CV(GuarCurr) > LS_CV_Opt2_Start(GuarCurr) Then
	     Temp_DB = Temp_DB + LS_CV(GuarCurr) - LS_CV_Opt2_Start(GuarCurr)
	  End If
	  LS_Charges = (((Temp_DB * .996737) - LS_CV(GuarCurr)) * LS_MortRate) / 1000
       End If
    End If
 
End Sub

Sub Principal_Rider_GPO ()
    
    s = SpouseClient

    If AnnYr < 41 Then
       LS_Insureds_MortCost = LS_Insureds_MortCost + (LSClient(s).GPO / 1000 * LSMort(GuarCurr, AnnYr).Prin_GPO)
    End If

End Sub

Sub Principal_Rider_Mortality (GuideSingle As Integer)
	   
    Dim TempYr As Integer
    ReDim Temp_Mort(1) As Single

    s = SpouseClient
    TempYr = AnnYr - LSClient(s).IssueAge
    LS_Rider_Client = 0

    If GuideSingle Then
       If LSMort(0, AnnYr).Insured_CSO80(0) < LSMort(0, AnnYr).Insured_Base(0) Then
	  Temp_Mort(0) = LSMort(0, AnnYr).Insured_CSO80(0)
       Else
	  Temp_Mort(0) = LSMort(0, AnnYr).Insured_Base(0)
       End If
       If LSMort(1, AnnYr).Insured_CSO80(0) < LSMort(1, AnnYr).Insured_Base(0) Then
	  Temp_Mort(1) = LSMort(1, AnnYr).Insured_CSO80(0)
       Else
	  Temp_Mort(1) = LSMort(1, AnnYr).Insured_Base(0)
       End If
    Else
       Temp_Mort(0) = LSMort(0, AnnYr).Insured_Base(0)
       Temp_Mort(1) = LSMort(1, AnnYr).Insured_Base(0)
    End If

    If LSClient(s).AddFace <> 0 Then
       If LSClient(s).AddFaceStart <= TempYr + 1 And LSClient(s).AddFaceEnd >= TempYr Then
	  If TempYr = 0 Then
	     LS_Insureds_MortCost = LS_Insureds_MortCost + (LSClient(s).AddFace / 1000 * Temp_Mort(1))
	     If LSClient(s).SubStd > 0 Then
		LS_Insureds_MortCost = LS_Insureds_MortCost + (LSClient(s).AddFace / 1000 * Temp_Mort(1))
	     End If
	  Else
	     LS_Insureds_MortCost = LS_Insureds_MortCost + (LSClient(s).AddFace / 1000 * Temp_Mort(GuarCurr))
	     If LSClient(s).SubStd > 0 Then
		LS_Insureds_MortCost = LS_Insureds_MortCost + (LSClient(s).AddFace / 1000 * Temp_Mort(GuarCurr))
	     End If
	  End If
	  LS_Rider_Client = LSClient(s).AddFace
       End If
    End If

End Sub

Sub Principal_Rider_WPD ()
    
    s = SpouseClient

    If AnnYr < 60 Then
       LS_Insureds_MortCost = LS_Insureds_MortCost + ((LS_Charges + LS_Insureds_MortCost) * LSMort(GuarCurr, AnnYr).Prin_WPD)
       If LS_DB(GuarCurr) + LS_COLA_Client < 100000 Then
	  LS_Insureds_MortCost = LS_Insureds_MortCost + (3.5 * LSMort(GuarCurr, AnnYr).Prin_WPD)
       Else
	  LS_Insureds_MortCost = LS_Insureds_MortCost + (3 * LSMort(GuarCurr, AnnYr).Prin_WPD)
       End If
    End If

End Sub

Sub Print_Cost_Indices (OnePage As String)

    Dim Temp_Index As String

    OnePage = OnePage & NL & NL & "5% Interest Adjusted Indices" & Space$(11)
    OnePage = OnePage & "Guaranteed" & Space$(12)
    OnePage = OnePage & "Projected" & Space$(11) & NL
    OnePage = OnePage & Space$(41) & "Values"
    OnePage = OnePage & Space$(16) & "Values" & NL
    OnePage = OnePage & Space$(36) & "----------------"
    OnePage = OnePage & Space$(6) & "----------------" & NL
    OnePage = OnePage & Space$(36) & "10 Year  20 Year"
    OnePage = OnePage & Space$(6) & "10 Year  20 Year" & NL
    OnePage = OnePage & Space$(36) & "----------------"
    OnePage = OnePage & Space$(6) & "----------------" & NL
    
    OnePage = OnePage & "Surrender Cost Index:" & Space$(15)
    If LS_Index_Numerator_SC(0, 0) = 0 Or LS_Index_Denominator(0) = 0 Then
       OnePage = OnePage & Space$(3) & "N/A" & Space$(4)
    Else
       Temp_Index = Format$(LS_Index_Numerator_SC(0, 0) / (LS_Index_Denominator(0) / 1000), "##0.00")
       OnePage = OnePage & Space$(6 - Len(Temp_Index)) & Temp_Index & Space$(4)
    End If
    If LS_Index_Numerator_SC(0, 1) = 0 Or LS_Index_Denominator(1) = 0 Then
       OnePage = OnePage & Space$(3) & "N/A" & Space$(6)
    Else
       Temp_Index = Format$(LS_Index_Numerator_SC(0, 1) / (LS_Index_Denominator(1) / 1000), "##0.00")
       OnePage = OnePage & Space$(6 - Len(Temp_Index)) & Temp_Index & Space$(6)
    End If
    If LS_Index_Numerator_SC(1, 0) = 0 Or LS_Index_Denominator(0) = 0 Then
       OnePage = OnePage & Space$(3) & "N/A" & Space$(4)
    Else
       Temp_Index = Format$(LS_Index_Numerator_SC(1, 0) / (LS_Index_Denominator(0) / 1000), "##0.00")
       OnePage = OnePage & Space$(6 - Len(Temp_Index)) & Temp_Index & Space$(4)
    End If
    If LS_Index_Numerator_SC(1, 1) = 0 Or LS_Index_Denominator(1) = 0 Then
       OnePage = OnePage & Space$(3) & "N/A" & NL
    Else
       Temp_Index = Format$(LS_Index_Numerator_SC(1, 1) / (LS_Index_Denominator(1) / 1000), "##0.00")
       OnePage = OnePage & Space$(6 - Len(Temp_Index)) & Temp_Index & NL
    End If
    
    OnePage = OnePage & "Net Payment Index:" & Space$(18)
    If LS_Index_Numerator_NP(0, 0) = 0 Or LS_Index_Denominator(0) = 0 Then
       OnePage = OnePage & Space$(3) & "N/A" & Space$(4)
    Else
       Temp_Index = Format$(LS_Index_Numerator_NP(0, 0) / (LS_Index_Denominator(0) / 1000), "##0.00")
       OnePage = OnePage & Space$(6 - Len(Temp_Index)) & Temp_Index & Space$(4)
    End If
    If LS_Index_Numerator_NP(0, 1) = 0 Or LS_Index_Denominator(1) = 0 Then
       OnePage = OnePage & Space$(3) & "N/A" & Space$(6)
    Else
       Temp_Index = Format$(LS_Index_Numerator_NP(0, 1) / (LS_Index_Denominator(1) / 1000), "##0.00")
       OnePage = OnePage & Space$(6 - Len(Temp_Index)) & Temp_Index & Space$(6)
    End If
    If LS_Index_Numerator_NP(1, 0) = 0 Or LS_Index_Denominator(0) = 0 Then
       OnePage = OnePage & Space$(3) & "N/A" & Space$(4)
    Else
       Temp_Index = Format$(LS_Index_Numerator_NP(1, 0) / (LS_Index_Denominator(0) / 1000), "##0.00")
       OnePage = OnePage & Space$(6 - Len(Temp_Index)) & Temp_Index & Space$(4)
    End If
    If LS_Index_Numerator_NP(1, 1) = 0 Or LS_Index_Denominator(1) = 0 Then
       OnePage = OnePage & Space$(3) & "N/A" & NL & NL
    Else
       Temp_Index = Format$(LS_Index_Numerator_NP(1, 1) / (LS_Index_Denominator(1) / 1000), "##0.00")
       OnePage = OnePage & Space$(6 - Len(Temp_Index)) & Temp_Index & NL & NL
    End If

End Sub

Sub Process_Ledger_Years (StartAge As Integer, StopAge As Integer, Search As Integer)
    
    Dim MonthRate As Double
    Dim CorrRate As Single

    For AnnYr = StartAge To StopAge
      If Verify_LSCORR(LS_RateFiles(9), AnnYr, CorrRate) <> True Then
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
	LS_Insureds_MortCost = 0
	If LSClient(s).SpouseRider Then Call Spouse_Rider_Mortality(False)
	If LSClient(s).ChildRider Then Call Child_Rider_Mortality
	For j = 1 To 9
	  If LSClient(s).InsuredRider(j) Then Call Insured_Rider_Mortality(j, False)
	Next j
	Call Principal_Rider_Mortality(False)
	If LSClient(s).GPO <> 0 Then Call Principal_Rider_GPO
	Call Principal_Mortality
	Call Principal_Premium
	If LSClient(s).WPD = True Then Call Principal_Rider_WPD
	LS_CV(GuarCurr) = LS_CV(GuarCurr) - LS_Charges - LS_Insureds_MortCost
	Call Do_Month_Interest(MonthRate)
	LS_CV(GuarCurr) = LS_CV(GuarCurr) * MonthRate
	If CorrRate > 0 And LS_DBOption = 1 Then
	   If (LS_DB_Minimum(GuarCurr) + LS_COLA_Client) < (LS_CV(GuarCurr) - LS_CV_Opt2_Start(GuarCurr)) * CorrRate Then
	      LS_DB(GuarCurr) = LS_DB_Minimum(GuarCurr) + (((LS_CV(GuarCurr) - LS_CV_Opt2_Start(GuarCurr)) * CorrRate) - (LS_DB_Minimum(GuarCurr) + LS_COLA_Client))
	   Else
	      LS_DB(GuarCurr) = LS_DB_Minimum(GuarCurr)
	   End If
	End If
	If AnnYr - LSClient(s).IssueAge < 21 And Search = False Then Call Sum_Cost_Indices
      Next AnnMo
      
      Call Do_Anniversary
      LSLedger(AnnYr + 1).AnnualOutlay = LS_Outlay
      LS_Outlay = 0
      LSLedger(AnnYr + 1).SurrenderValue(GuarCurr) = LS_CSV(GuarCurr)
      If LS_CV(GuarCurr) < 0 Then
	 LSLedger(AnnYr + 1).CashValue(GuarCurr) = 0
      Else
	 LSLedger(AnnYr + 1).CashValue(GuarCurr) = LS_CV(GuarCurr)
      End If
      If LS_DBOption = 1 Then
	 LSLedger(AnnYr + 1).DeathBenefit(GuarCurr) = LS_DB(GuarCurr) + LS_COLA_Client
      Else
	 If LS_CV_Opt2_Start(GuarCurr) > LS_CV(GuarCurr) Then
	    LSLedger(AnnYr + 1).DeathBenefit(GuarCurr) = LS_DB(GuarCurr) + LS_COLA_Client
	 Else
	    LSLedger(AnnYr + 1).DeathBenefit(GuarCurr) = LS_DB(GuarCurr) + LS_COLA_Client + LS_CV(GuarCurr) - LS_CV_Opt2_Start(GuarCurr)
	 End If
      End If
      
      If (LSClient(s).IssueAge Mod 2 <> 0 & AnnYr Mod 2 = 0) Or (LSClient(s).IssueAge Mod 2 = 0 & AnnYr Mod 2 <> 0) Then
	 If LSClient(s).COLA <> 0 Then
	    Call Add_COLA("P", 0)
	 End If
     
	 If LSClient(s).SpouseRider = True Then
	    If LSSpouseRider.COLA <> 0 Then
	       Call Add_COLA("S", 0)
	    End If
	 End If
    
	 For i = 1 To 9
	   If LSClient(s).InsuredRider(i) = True Then
	      If LSInsuredRider(i).COLA <> 0 Then
		 Call Add_COLA("I", i)
	      End If
	   End If
	 Next i
      End If

      If LS_CV(GuarCurr) <= 0 And AnnYr > 3 Then
	 For i = AnnYr + 1 To 95
	   LSLedger(i).SurrenderValue(GuarCurr) = 0
	   LSLedger(i).CashValue(GuarCurr) = 0
	   LSLedger(i).DeathBenefit(GuarCurr) = 0
	 Next i
	 AnnYr = 95           'forces end of loop
      End If
      If LSClient(s).FutureChanges = True Then Call Check_Future_Changes
    Next AnnYr

End Sub

Function ReadyToIllustrate () As Integer

    ReadyToIllustrate = True
    s = SpouseClient
    If LSClient(s).IssueAge > 75 Then
       ReadyToIllustrate = False
       Exit Function
    End If
    If LSClient(s).SexCode <> "M" And LSClient(s).SexCode <> "F" Then
       ReadyToIllustrate = False
       Exit Function
    End If
    If LSClient(s).FaceAmt < 15000 Then
       ReadyToIllustrate = False
       Exit Function
    End If
    If LSClient(s).CurrRate < 4# Then
       ReadyToIllustrate = False
       Exit Function
    End If

End Function

Sub Reverse_CashValue (GuideLineStd As Integer, WaiverPct As Single, Premium As Single, AddInsds As Integer)

    Dim Numerator As Double
    Dim Denominator As Double
    Dim QFactor As Single
    Dim Temp_DB As Double

    Temp_DB = LS_DB(GuarCurr) + LS_COLA_Client
    QFactor = LS_MortRate / 1000
    If GuideLineStd = True Then
       If LS_MortRate > LSMort(GuarCurr, AnnYr).Prin_CSO80 And LSMort(GuarCurr, AnnYr).Prin_CSO80 > 0 Then
	  QFactor = LSMort(GuarCurr, AnnYr).Prin_CSO80 / 1000
       End If
    End If
    Numerator = LS_Charges * QFactor
    Numerator = Numerator + (Temp_DB * .996737 * QFactor)
    Numerator = Numerator + LS_Charges
    Numerator = Numerator * (1 + WaiverPct)
    Numerator = (Numerator * LS_Irate(GuarCurr)) + LS_CV(GuarCurr)
    Denominator = 1 + QFactor + (QFactor * WaiverPct)
    Denominator = Denominator * LS_Irate(GuarCurr)
    LS_CV(GuarCurr) = (Numerator / Denominator) - Premium + .005
    LS_CV(GuarCurr) = LS_CV(GuarCurr) * 100
    LS_CV(GuarCurr) = (Int(LS_CV(GuarCurr))) / 100

'/* if AddInsds is true, calculate the Guideline Single
'/* Premium inclusive of additional insured mortality
'/* charges in the LS_CSV(GuarCurr) field.

    If AddInsds = True Then
       Numerator = (LS_Charges + LS_Insureds_MortCost) * QFactor
       Numerator = Numerator + (Temp_DB * .996737 * QFactor)
       Numerator = Numerator + LS_Charges + LS_Insureds_MortCost
       Numerator = Numerator * (1 + WaiverPct)
       Numerator = (Numerator * LS_Irate(GuarCurr)) + LS_CSV(GuarCurr)
      'Denominator shouldn't change
       LS_CSV(GuarCurr) = (Numerator / Denominator) - Premium + .005
       LS_CSV(GuarCurr) = LS_CSV(GuarCurr) * 100
       LS_CSV(GuarCurr) = (Int(LS_CSV(GuarCurr))) / 100
    End If

End Sub

Sub Set_Client_Illus_Parameters ()
     
    LSClient(0).ClientName = Trim$(frmLIFESV.txtClientFirstName.Text) & " " & frmLIFESV.txtClientMidInit.Text & " " & Trim$(frmLIFESV.txtClientLastName.Text)
    LSClient(0).IssueAge = Val(frmLIFESV.txtClientAge.Text)
    If LSClient(0).IssueAge + LSClient(0).YearsToPay > 95 Then
       LSClient(0).YearsToPay = 95 - LSClient(0).IssueAge
    End If
    LSClient(0).SexCode = frmLIFESV.txtClientSex.Text
    LSClient(0).FaceAmt = Val(frmLIFESV.txtClientFace.Text)
    Select Case frmLIFESV.cboClientPlan.ListIndex
       Case 1
	  LSClient(0).PlanCode = 2
       Case Else
	  LSClient(0).PlanCode = 1
    End Select
    LSClient(0).State = frmLIFESV.txtClientState.Text
    Select Case frmLIFESV.cboClientOption.ListIndex
       Case 1
	  LSClient(0).Option = 2
       Case Else
	  LSClient(0).Option = 1
    End Select
    Select Case frmLIFESV.cboClientSubStd.ListIndex
       Case 1 To 20
	  LSClient(0).SubStd = frmLIFESV.cboClientSubStd.ListIndex
       Case Else
	  LSClient(0).SubStd = 0
    End Select
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
    Select Case frmLIFESV.cboClientCOLA.ListIndex
       Case 1 To 10
	  LSClient(0).COLA = frmLIFESV.cboClientCOLA.ListIndex
       Case Else
	  LSClient(0).COLA = 0
    End Select
    LSClient(0).AddFace = Val(frmLIFESV.txtClientTermRider.Text)
    LSClient(0).AddFaceStart = 1  'Always 1st year if exist
    Select Case frmLIFESV.cboClientRiderYrAgeEnd.ListIndex
       Case 1
	  LSClient(0).AddFaceEnd = Val(frmLIFESV.txtClientRiderYearEnd.Text) - Val(frmLIFESV.txtClientAge.Text)
       Case Else
	  LSClient(0).AddFaceEnd = Val(frmLIFESV.txtClientRiderYearEnd.Text)
    End Select
    LSClient(0).CurrRate = Val(frmLIFESV.txtClientCurrentInt.Text)
    LSClient(0).IllusType = 1
    LSClient(0).PrintPages = 3
    LSClient(0).ContactName = Trim$(frmLIFESV.txtPreparedBy.Text)
    LSClient(0).ContactPhone = frmLIFESV.txtPhoneContact

End Sub

Sub Set_Future_Changes ()

    LSClient(SpouseClient).FutureChanges = False
    LSClient(SpouseClient).FutureWD = False

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
      If Val(frmLSCHANGE.txtChangeDBOptAge(i - 1).Text) > 0 Then
	 LSFuture(i).Opt_Age = Val(frmLSCHANGE.txtChangeDBOptAge(i - 1).Text)
	 LSFuture(i).Opt_Type = Val(frmLSCHANGE.txtChangeDBOpt(i - 1).Text)
	 LSClient(SpouseClient).FutureChanges = True
      Else
	 LSFuture(i).Opt_Age = 0
	 LSFuture(i).Opt_Type = 0
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

    If frmLSINSD.chkSpouse.Value = 1 Then
       If frmLSINSD.txtSpouseAge.Text = "" Then
	  LSClient(s).SpouseRider = False
       Else
	  LSClient(s).SpouseRider = True
       End If
       LSSpouseRider.IssueAge = Val(frmLSINSD.txtSpouseAge.Text)
       Select Case (frmLSINSD.cboSpouseFace.ListIndex)
	  Case 1
	     LSSpouseRider.FaceAmt = 25000
	  Case Else
	     LSSpouseRider.FaceAmt = 50000
       End Select
       Select Case (frmLSINSD.cboSpousePlan.ListIndex)
	  Case 1
	     LSSpouseRider.PlanCode = 2
	  Case Else
	     LSSpouseRider.PlanCode = 1
       End Select
       Select Case frmLSINSD.cboSpouseSubStd.ListIndex
	  Case 1 To 20
	     LSSpouseRider.SubStd = frmLSINSD.cboSpouseSubStd.ListIndex
	  Case Else
	     LSSpouseRider.SubStd = 0
       End Select
       Select Case frmLSINSD.cboSpouseCOLA.ListIndex
	  Case 1 To 10
	     LSSpouseRider.COLA = frmLSINSD.cboSpouseCOLA.ListIndex
	  Case Else
	     LSSpouseRider.COLA = 0
       End Select
       If frmLSINSD.cboSpouseYearsAge.ListIndex = 1 Then
	  LSSpouseRider.RemoveYear = Val(frmLSINSD.txtSpouseYears.Text) - LSSpouseRider.IssueAge
       Else
	  LSSpouseRider.RemoveYear = Val(frmLSINSD.txtSpouseYears.Text)
       End If
    Else
       LSClient(s).SpouseRider = False
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
	    Case 1
	       LSInsuredRider(i).PlanCode = 2
	    Case Else
	       LSInsuredRider(i).PlanCode = 1
	 End Select
	 Select Case frmLSINSD.cboAddInsdSubStd(i - 1).ListIndex
	    Case 1 To 20
	       LSInsuredRider(i).SubStd = frmLSINSD.cboAddInsdSubStd(i - 1).ListIndex
	    Case Else
	       LSInsuredRider(i).SubStd = 0
	 End Select
	 Select Case frmLSINSD.cboAddInsdCOLA(i - 1).ListIndex
	    Case 1 To 10
	       LSInsuredRider(i).COLA = frmLSINSD.cboAddInsdCOLA(i - 1).ListIndex
	    Case Else
	       LSInsuredRider(i).COLA = 0
	 End Select
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

Sub Set_Monthly_Charge (typeill As Integer)

     Select Case (LSClient(SpouseClient).FaceAmt)
	Case Is < 200000
	   If typeill% = 1 Then
	      POLFEE = 100
	   Else
	      POLFEE = 85
	   End If
	Case 200000 To 999999
	   POLFEE = 60
	Case Else
	   POLFEE = 20
     End Select

End Sub

Sub Set_Premium_Illus_Parameters ()

    s = SpouseClient
    LSClient(s).CurrRate = Val(frmLSPREM.txtPremCurrentInt.Text)
    If LSClient(s).CurrRate > 0 Then frmLIFESV.txtClientCurrentInt.Text = Format$(LSClient(s%).CurrRate, "00.000")
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

    LSClient(1).ClientName = Trim$(frmLSSPOUSE.txtSpouseFirstName.Text) & " " & frmLSSPOUSE.txtSpouseMidInit.Text & " " & Trim$(frmLSSPOUSE.txtSpouseLastName.Text)
    LSClient(1).IssueAge = Val(frmLSSPOUSE.txtSpouseAge.Text)
    If LSClient(1).IssueAge + LSClient(1).YearsToPay > 95 Then
       LSClient(1).YearsToPay = 95 - LSClient(1).IssueAge
    End If
    LSClient(1).SexCode = frmLSSPOUSE.txtSpouseSex.Text
    LSClient(1).FaceAmt = Val(frmLSSPOUSE.txtSpouseFace.Text)
    Select Case frmLSSPOUSE.cboSpousePlan.ListIndex
       Case 1
	  LSClient(1).PlanCode = 2
       Case Else
	  LSClient(1).PlanCode = 1
    End Select
    LSClient(1).State = frmLSSPOUSE.txtSpouseState.Text
    Select Case frmLSSPOUSE.cboSpouseOption.ListIndex
       Case 1
	  LSClient(1).Option = 2
       Case Else
	  LSClient(1).Option = 1
    End Select
    Select Case frmLSSPOUSE.cboSpouseSubStd.ListIndex
       Case 1 To 20
	  LSClient(1).SubStd = frmLSSPOUSE.cboSpouseSubStd.ListIndex
       Case Else
	  LSClient(1).SubStd = 0
    End Select
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
    Select Case frmLSSPOUSE.cboSpouseCOLA.ListIndex
       Case 1 To 10
	  LSClient(1).COLA = frmLSSPOUSE.cboSpouseCOLA.ListIndex
       Case Else
	  LSClient(1).COLA = 0
    End Select
    LSClient(1).AddFace = Val(frmLSSPOUSE.txtSpouseTermRider.Text)
    LSClient(1).AddFaceStart = 1 'Always 1st year if exist
    Select Case frmLSSPOUSE.cboSpouseRiderYrAgeEnd.ListIndex
       Case 1
	  LSClient(1).AddFaceEnd = Val(frmLSSPOUSE.txtSpouseRiderYearEnd.Text) - Val(frmLSSPOUSE.txtSpouseAge.Text)
       Case Else
	  LSClient(1).AddFaceEnd = Val(frmLSSPOUSE.txtSpouseRiderYearEnd.Text)
    End Select
    LSClient(1).CurrRate = Val(frmLSSPOUSE.txtSpouseCurrentInt)
    LSClient(1).SpouseRider = False
    LSClient(1).ChildRider = False
    For j = 1 To 9
      LSClient(1).InsuredRider(j) = False
    Next j
    LSClient(1).IllusType = 1
    LSClient(1).PrintPages = 3
    LSClient(1).ContactName = Trim$(frmLIFESV.txtPreparedBy.Text)
    LSClient(1).ContactPhone = frmLIFESV.txtPhoneContact

End Sub

Sub Spouse_Rider_Mortality (GuideSingle As Integer)
    
    Dim Spouse_Adj_Face As Single
    Dim ReturnRate As Single
    Dim Spouse_Age As Integer
    ReDim Temp_Mort(1) As Single

    Spouse_Adj_Face = LSSpouseRider.FaceAmt
    Spouse_Age = (AnnYr - LSClient(SpouseClient).IssueAge) + LSSpouseRider.IssueAge
    If Spouse_Age > 94 Or Spouse_Age >= (LSSpouseRider.IssueAge + LSSpouseRider.RemoveYear) Then Exit Sub
    If Spouse_Age > 64 Then
       If Verify_LSSPOUSE(LS_RateFiles(10), Spouse_Age, ReturnRate) = True Then
	  Spouse_Adj_Face = Spouse_Adj_Face * ReturnRate
       End If
    End If

    If GuideSingle Then
       If LSMort(0, AnnYr).Spouse_CSO80 < LSMort(0, AnnYr).Spouse_Base Then
	  Temp_Mort(0) = LSMort(0, Spouse_Age).Spouse_CSO80
       Else
	  Temp_Mort(0) = LSMort(0, Spouse_Age).Spouse_Base
       End If
       If LSMort(1, Spouse_Age).Spouse_CSO80 < LSMort(1, Spouse_Age).Spouse_Base Then
	  Temp_Mort(1) = LSMort(1, Spouse_Age).Spouse_CSO80
       Else
	  Temp_Mort(1) = LSMort(1, Spouse_Age).Spouse_Base
       End If
    Else
       Temp_Mort(0) = LSMort(0, Spouse_Age).Spouse_Base
       Temp_Mort(1) = LSMort(1, Spouse_Age).Spouse_Base
    End If

    If Spouse_Age = LSSpouseRider.IssueAge Then
       LS_Insureds_MortCost = LS_Insureds_MortCost + ((Spouse_Adj_Face * Temp_Mort(1)) / 1000)
       If LSSpouseRider.SubStd > 0 Then
	  LS_Insureds_MortCost = LS_Insureds_MortCost + ((Spouse_Adj_Face * LSMort(1, Spouse_Age).Spouse_Sub) / 1000)
       End If
    Else
       LS_Insureds_MortCost = LS_Insureds_MortCost + ((Spouse_Adj_Face * Temp_Mort(GuarCurr)) / 1000)
       If LSSpouseRider.SubStd > 0 Then
	  LS_Insureds_MortCost = LS_Insureds_MortCost + ((Spouse_Adj_Face * LSMort(GuarCurr, Spouse_Age).Spouse_Sub) / 1000)
       End If
    End If

End Sub

Sub SpouseSave ()

    Dim RetCode As Integer
    Dim IBirth As String

    If SpouseChanged = True And Left(frmLSSPOUSE.txtSpouseFirstName.Text, 1) <> " " And frmLSSPOUSE.txtSpouseFirstName.Text <> "" Then
       ClientInfo.FirstName = frmLSSPOUSE.txtSpouseFirstName.Text
       ClientInfo.Initial = frmLSSPOUSE.txtSpouseMidInit.Text
       ClientInfo.LastName = frmLSSPOUSE.txtSpouseLastName.Text
       ClientInfo.Gender = frmLSSPOUSE.txtSpouseSex.Text
       ClientInfo.State = frmLSSPOUSE.txtSpouseState.Text
       IBirth = frmLSSPOUSE.txtSpouseBirth.Text
       set_insd_birthdt (IBirth)
       ClientInfo.BirthDate = InsuredInfo.InsBirthDate
       ClientInfo.ClientNumber = 0
       RetCode = client_file(5, ClientInfo.ClientNumber)
       If RetCode = False Then
	  MsgBox " Save for Spouse information failed; Spouse information has not been saved ", 0
	  Exit Sub
       End If
       InsuredInfo.InsFName = Trim$(frmLSSPOUSE.txtSpouseFirstName.Text) & " " & Trim$(frmLSSPOUSE.txtSpouseLastName.Text)
       IBirth = frmLSSPOUSE.txtSpouseBirth.Text
       set_insd_birthdt (IBirth)
       InsuredInfo.InsGender = frmLSSPOUSE.txtSpouseSex.Text
       InsuredInfo.InsRelation = "P"
       If frmLSSPOUSE.cboSpousePlan.ListIndex = 0 Then
	  InsuredInfo.insTobacco = "N"
       Else
	  InsuredInfo.insTobacco = "Y"
       End If
       RetCode = insured_file(5, ClientInfo.ClientNumber, 0)
       SpouseChanged = False
    End If

End Sub

Sub Sum_Cost_Indices ()

    Dim Power As Single
    Dim CostFactor As Double
    Dim Temp_DB As Double

    Temp_DB = LS_DB(GuarCurr) + LS_COLA_Client
    Power = AnnYr - LSClient(SpouseClient).IssueAge + 1
    If Power <= 11 Then
       If Power <> 11 Then
	  LS_Index_OneYear_Cost(GuarCurr, 0) = LS_Index_OneYear_Cost(GuarCurr, 0) - (LS_Insureds_MortCost * 1.081081)
	  LS_Index_OneYear_Cost(GuarCurr, 0) = LS_Index_OneYear_Cost(GuarCurr, 0) + LS_Monthly_Prem
	  If AnnMo = 12 Then
	     CostFactor = 1.05 ^ (11 - Power)
	     LS_Index_Numerator_NP(GuarCurr, 0) = LS_Index_Numerator_NP(GuarCurr, 0) + LS_Index_OneYear_Cost(GuarCurr, 0) * CostFactor
	     LS_Index_OneYear_Cost(GuarCurr, 0) = 0
	     If GuarCurr = 0 Then
		LS_Index_Denominator(0) = LS_Index_Denominator(0) + Temp_DB * CostFactor
	     End If
	  End If
       Else
	  LS_Index_Numerator_SC(GuarCurr, 0) = LS_Index_Numerator_NP(GuarCurr, 0) - LS_CSV(GuarCurr)
       End If
    End If
    If Power <> 21 Then
       If Power <= 10 Then
	  LS_Index_OneYear_Cost(GuarCurr, 1) = LS_Index_OneYear_Cost(GuarCurr, 1) - (LS_Insureds_MortCost * 1.081081)
       Else
	  LS_Index_OneYear_Cost(GuarCurr, 1) = LS_Index_OneYear_Cost(GuarCurr, 1) - (LS_Insureds_MortCost * 1.025641)
       End If
       LS_Index_OneYear_Cost(GuarCurr, 1) = LS_Index_OneYear_Cost(GuarCurr, 1) + LS_Monthly_Prem
       If AnnMo = 12 Then
	  CostFactor = 1.05 ^ (21 - Power)
	  LS_Index_Numerator_NP(GuarCurr, 1) = LS_Index_Numerator_NP(GuarCurr, 1) + LS_Index_OneYear_Cost(GuarCurr, 1) * CostFactor
	  LS_Index_OneYear_Cost(GuarCurr, 1) = 0
	  If GuarCurr = 0 Then
	     LS_Index_Denominator(1) = LS_Index_Denominator(1) + Temp_DB * CostFactor
	  End If
       End If
    Else
       LS_Index_Numerator_SC(GuarCurr, 1) = LS_Index_Numerator_NP(GuarCurr, 1) - LS_CSV(GuarCurr)
    End If

End Sub

Function Verify_CSO80 (InFile As Integer, Sex As String, Smoke As String, Age As Integer, ReturnRate As Single) As Integer
    
    Verify_CSO80 = True
    On Local Error GoTo Err_CSO80

    Sex = UCase(Sex)
    Smoke = UCase(Smoke)

    If (Sex <> "M" And Sex <> "F") Or (Smoke <> "S" And Smoke <> "N") Then
       Verify_CSO80 = False
       Exit Function
    End If

    If Age < 15 Or Age > 94 Then
       Verify_CSO80 = False
       Exit Function
    End If
   
    RecordNo = Age + 1

    filnum = InFile
    Get #filnum, RecordNo, CSO80Rate

    Verify_CSO80 = True
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
    MsgBox MsgText  ' Display message.
    Verify_CSO80 = False
    Exit Function

End Function

Function Verify_Life_Weight (InFile As Integer, Inches As Integer, Pounds As Integer, Age As Integer) As Integer

    On Local Error GoTo Err_LHtWt
    
    If Age < 18 Then
       If Age < 15 Then
	  Verify_Life_Weight = False
	  Exit Function
       Else
	  Inches = Inches - 1
       End If
    End If

    If Inches < 56 Or Inches > 80 Then
       Verify_Life_Weight = False
       Exit Function
    End If

    RecordNo = Inches - 55

    filnum = InFile
    Get #filnum, RecordNo, LHtWt

    Select Case Pounds
       Case Is < LHtWt.WeightMin
	  Verify_Life_Weight = False
       Case Is > LHtWt.WeightMax
	  Verify_Life_Weight = False
       Case LHtWt.WeightMin0 To LHtWt.WeightMax0
	  Verify_Life_Weight = True
       Case LHtWt.WeightMin2 To LHtWt.WeightMax2
	  Verify_Life_Weight = 2
       Case LHtWt.WeightMin3 To LHtWt.WeightMax3
	  Verify_Life_Weight = 3
       Case LHtWt.WeightMin4 To LHtWt.WeightMax4
	  Verify_Life_Weight = 4
       Case LHtWt.WeightMin5 To LHtWt.WeightMax5
	  Verify_Life_Weight = 5
       Case LHtWt.WeightMin6 To LHtWt.WeightMax6
	  Verify_Life_Weight = 6
       Case LHtWt.WeightMin8 To LHtWt.WeightMax8
	  Verify_Life_Weight = 8
       Case LHtWt.WeightMin10 To LHtWt.WeightMax10
	  Verify_Life_Weight = 10
       Case LHtWt.WeightMin12 To LHtWt.WeightMax12
	  Verify_Life_Weight = 12
    End Select

    Exit Function

Err_LHtWt:
    
    MsgText = "Height/Weight File"
    MsgText = Str$(Err) & MsgText
    MsgBox MsgText  ' Display message.
    Exit Function

End Function

Function Verify_LSCORR (InFile As Integer, Age As Integer, ReturnRate As Single) As Integer
    
    Dim RecordNo As Integer
    
    Verify_LSCORR = True
    On Local Error GoTo Err_LSCORR

    Select Case Age
       Case Is > 95
	  RecordNo = 56
       Case Is < 40
	  RecordNo = 1
       Case Else
	  RecordNo = Age - 39
    End Select

    filnum = InFile
    Get #filnum, RecordNo, ReturnRate

    Exit Function

Err_LSCORR:
    
    MsgText = "Corridor File"
    MsgText = Str$(Err) & MsgText
    MsgBox MsgText  ' Display message.
    Verify_LSCORR = False
    Exit Function

End Function

Function Verify_LSMINP (InFile As Integer, Sex As String, Smoke As String, Age As Integer, Band As Integer, ReturnRate As Single) As Integer

    Verify_LSMINP = True
    On Local Error GoTo Err_LSMINP

    Sex = UCase(Sex)
    Smoke = UCase(Smoke)

    If (Sex <> "M" And Sex <> "F") Or (Smoke <> "S" And Smoke <> "N") Then
       Verify_LSMINP = False
       Exit Function
    End If

    If Age > 70 Or Band < 1 Or Band > 4 Then
       Verify_LSMINP = False
       Exit Function
    End If
   
    RecordNo = Age + 1

    filnum = InFile
    Get #filnum, RecordNo, MinpRate

    Select Case Band
       Case Is < 1
	  Verify_LSMINP = False
       Case Is > 4
	  Verify_LSMINP = False
       Case Else
	  Verify_LSMINP = True
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
    End Select

    Exit Function

Err_LSMINP:
    
    MsgText = "Minimum Premium File"
    MsgText = Str$(Err) & MsgText
    MsgBox MsgText  ' Display message.
    Exit Function

End Function

Function Verify_LSRATEGP (InFile As Integer, Age As Integer, ReturnRate As Single) As Integer

    Verify_LSRATEGP = True
    On Local Error GoTo Err_LSRATEGP

    If Age > 40 Then
       Verify_LSRATEGP = False
       Exit Function
    End If
   
    RecordNo = Age + 1

    filnum = InFile
    Get #filnum, RecordNo, GPORate

    ReturnRate = GPORate.GPO

    Exit Function

Err_LSRATEGP:
    
    MsgText = "GPO File"
    MsgText = Str$(Err) & MsgText
    MsgBox MsgText  ' Display message.
    Exit Function

End Function

Function Verify_LSRATEPR (InFile As Integer, Who As String, Sex As String, Smoke As String, Age As Integer, Band As Integer, ReturnRate As Single) As Integer

    On Local Error GoTo Err_LSRATEPR

    Who = UCase(Who)
    Sex = UCase(Sex)
    Smoke = UCase(Smoke)

    If (Who <> "P") Or (Sex <> "M" And Sex <> "F") Or (Smoke <> "S" And Smoke <> "N") Then
       Verify_LSRATEPR = False
       Exit Function
    End If

    If Age > 94 Or Band < 1 Or Band > 5 Then
       Verify_LSRATEPR = False
       Exit Function
    End If
   
    RecordNo = Age + 1

    filnum = InFile
    Get #filnum, RecordNo, PrinRate

    Select Case Band
       Case Is < 1
	  Verify_LSRATEPR = False
	  Exit Function
       Case Is > 5
	  Verify_LSRATEPR = False
	  Exit Function
       Case Else
	  Verify_LSRATEPR = True
	  If Sex = "M" Then
	     If Smoke = "N" Then
		ReturnRate = PrinRate.MaleNS(Band)
	     Else
		ReturnRate = PrinRate.MaleSM(Band)
	     End If
	  Else
	     If Smoke = "N" Then
		ReturnRate = PrinRate.FemaleNS(Band)
	     Else
		ReturnRate = PrinRate.FemaleSM(Band)
	     End If
	  End If
    End Select
    
    Verify_LSRATEPR = True
    Exit Function

Err_LSRATEPR:
    
    MsgText = "Principal Rate File"
    MsgText = Str$(Err) & MsgText
    MsgBox MsgText  ' Display message.
    Exit Function

End Function

Function Verify_LSRATESB (InFile As Integer, Smoke As String, Age As Integer, ReturnRate As Single) As Integer
    
    Dim RecordNo As Integer

    Verify_LSRATESB = True
    On Local Error GoTo Err_LSRATESB

    Smoke = UCase(Smoke)

    If (Smoke <> "S" And Smoke <> "N") Then
       Verify_LSRATESB = False
       Exit Function
    End If

    If Age > 94 Then
       Verify_LSRATESB = False
       Exit Function
    End If
   
    RecordNo = Age + 1
		   
    filnum = InFile
    Get #filnum, RecordNo, SubRate

    Verify_LSRATESB = True
    If Smoke = "N" Then
       ReturnRate = SubRate.NonSmokerSub
    Else
       ReturnRate = SubRate.SmokerSub
    End If

    Exit Function

Err_LSRATESB:
    
    MsgText = "Substandard File"
    MsgText = Str$(Err) & MsgText
    MsgBox MsgText  ' Display message.
    Exit Function

End Function

Function Verify_LSRATESI (InFile As Integer, Who As String, Sex As String, Smoke As String, Age As Integer, Band As Integer, ReturnRate As Single) As Integer
    
    Verify_LSRATESI = True
    On Local Error GoTo Err_LSRATESI

    Who = UCase(Who)
    Sex = UCase(Sex)
    Smoke = UCase(Smoke)

    If (Who <> "S" And Who <> "I") Or (Sex <> "M" And Sex <> "F") Or (Smoke <> "S" And Smoke <> "N") Then
       Verify_LSRATESI = False
       Exit Function
    End If

    If Age > 94 Or Band < 1 Or Band > 2 Then
       Verify_LSRATESI = False
       Exit Function
    End If
   
    RecordNo = Age + 1

    filnum = InFile
    Get #filnum, RecordNo, SIRate

    Select Case Band
       Case Is < 1
	  Verify_LSRATESI = False
       Case Is > 2
	  Verify_LSRATESI = False
       Case Else
	  Verify_LSRATESI = True
	  If Sex = "M" Then
	     If Smoke = "N" Then
		ReturnRate = SIRate.MaleNS(Band)
	     Else
		ReturnRate = SIRate.MaleSM(Band)
	     End If
	  Else
	     If Smoke = "N" Then
		ReturnRate = SIRate.FemaleNS(Band)
	     Else
		ReturnRate = SIRate.FemaleSM(Band)
	     End If
	  End If
    End Select

    Exit Function

Err_LSRATESI:
    
    MsgText = "Spouse/Insured Rate File"
    MsgText = Str$(Err) & MsgText
    MsgBox MsgText  ' Display message.
    Exit Function
    
End Function

Function Verify_LSRATEWP (InFile As Integer, Sex As String, Age As Integer, ReturnRate As Single) As Integer

    Verify_LSRATEWP = True
    On Local Error GoTo Err_LSRATEWP

    Sex = UCase(Sex)

    If Sex <> "M" And Sex <> "F" Then
       Verify_LSRATEWP = False
       Exit Function
    End If

    If Age < 15 Or Age > 59 Then
       Verify_LSRATEWP = False
       Exit Function
    End If
   
    RecordNo = Age + 1

    filnum = InFile
    Get #filnum, RecordNo, WPDRate

    If Sex = "M" Then
       ReturnRate = WPDRate.MaleWPD
    Else
       ReturnRate = WPDRate.FemaleWPD
    End If

    Exit Function

Err_LSRATEWP:
    
    MsgText = "WPD File"
    MsgText = Str$(Err) & MsgText
    MsgBox MsgText  ' Display message.
    Exit Function

End Function

Function Verify_LSSPOUSE (InFile As Integer, Age As Integer, ReturnRate As Single) As Integer
    
    Dim RecordNo As Integer
    
    Verify_LSSPOUSE = True
    On Local Error GoTo Err_LSSPOUSE

    Select Case Age
       Case Is > 95
	  RecordNo = 31
       Case Is < 65
	  ReturnRate = 1
	  Exit Function
       Case Else
	  RecordNo = Age - 64
    End Select

    filnum = InFile
    Get #filnum, RecordNo, ReturnRate

    Exit Function

Err_LSSPOUSE:
    
    MsgText = "Spouse Adjust Death Benefit File"
    MsgText = Str$(Err) & MsgText
    MsgBox MsgText  ' Display message.
    Verify_LSSPOUSE = False
    Exit Function

End Function

Function Verify_LSSURR (InFile As Integer, Who As String, Sex As String, Age As Integer, Duration As Integer, ReturnRate As Single) As Integer
    
    Verify_LSSURR = True
    On Local Error GoTo Err_LSSURR

    Who = UCase(Who)
    Sex = UCase(Sex)

    If (Who <> "P") Or (Sex <> "M" And Sex <> "F") Then
       Verify_LSSURR = False
       Exit Function
    End If

    If Age > 70 Or Duration > 16 Then
       Verify_LSSURR = False
       Exit Function
    End If
   
    RecordNo = Age + 1

    filnum = InFile
    Get #filnum, RecordNo, SurrRate

    If Sex = "M" Then
       ReturnRate = SurrRate.MaleSurr(Duration)
    Else
       ReturnRate = SurrRate.FemaleSurr(Duration)
    End If

    Exit Function

Err_LSSURR:
    
    MsgText = "Surrender File"
    MsgText = Str$(Err) & MsgText
    MsgBox MsgText  ' Display message.
    Exit Function

End Function

Function Verify_LSTARG (InFile As Integer, Sex As String, Age As Integer, ReturnRate As Single) As Integer

    Verify_LSTARG = True
    On Local Error GoTo Err_LSTARG

    Sex = UCase(Sex)

    If (Sex <> "M" And Sex <> "F") Then
       Verify_LSTARG = False
       Exit Function
    End If

    If Age > 70 Then
       Verify_LSTARG = False
       Exit Function
    End If
   
    RecordNo = Age + 1

    filnum = InFile
    Get #filnum, RecordNo, TargRate

    If Sex = "M" Then
       ReturnRate = TargRate.MaleTarg
    Else
       ReturnRate = TargRate.FemaleTarg
    End If

    Exit Function

Err_LSTARG:
    
    MsgText = "Target Premium File"
    MsgText = Str$(Err) & MsgText
    MsgBox MsgText  ' Display message.
    Exit Function

End Function

