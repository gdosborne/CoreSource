
using Microsoft.VisualBasic;
using System;
using GregOsborne.Application.Primitives;
using Life.Savings.Data.Model;
using System.Linq;
using Life.Savings.Data;

public static class Module1 {
    public const int MAXAGE = 95;

    public class Life_HW_Values {
        public int WeightMin;
        public int WeightMax;
        public int WeightMin0;
        public int WeightMax0;
        public int WeightMin2;
        public int WeightMax2;
        public int WeightMin3;
        public int WeightMax3;
        public int WeightMin4;
        public int WeightMax4;
        public int WeightMin5;
        public int WeightMax5;
        public int WeightMin6;
        public int WeightMax6;
        public int WeightMin8;
        public int WeightMax8;
        public int WeightMin10;
        public int WeightMax10;
        public int WeightMin12;
        public int WeightMax12;
    }

    static Life_HW_Values LHtWt;
    public class Life_LSRATEPR_Values {
        public float[] MaleNS = new float[5];
        public float[] MaleSM = new float[5];
        public float[] FemaleNS = new float[5];
        public float[] FemaleSM = new float[5];
    }

    static Life_LSRATEPR_Values PrinRate;
    public class Life_LSRATESI_Values {
        public float[] MaleNS = new float[2];
        public float[] MaleSM = new float[2];
        public float[] FemaleNS = new float[2];
        public float[] FemaleSM = new float[2];
    }

    static Life_LSRATESI_Values SIRate;
    public class Life_LSRATEGP_Values {
        public float GPO;
    }

    static Life_LSRATEGP_Values GPORate;
    public class Life_LSRATEWP_Values {
        public float MaleWPD;
        public float FemaleWPD;
    }

    static Life_LSRATEWP_Values WPDRate;
    public class Life_LSRATESB_Values {
        public float SmokerSub;
        public float NonSmokerSub;
    }

    static Life_LSRATESB_Values SubRate;
    public class Life_LSSURR_Values {
        public float[] MaleSurr = new float[17];
        public float[] FemaleSurr = new float[17];
    }

    static Life_LSSURR_Values SurrRate;
    public class Life_LSMINP_Values {
        public float[] MaleNSMinp = new float[4];
        public float[] MaleSMMinp = new float[4];
        public float[] FemaleNSMinp = new float[4];
        public float[] FemaleSMMinp = new float[4];
    }

    static Life_LSMINP_Values MinpRate;
    public class Life_LSTARG_Values {
        public float MaleTarg;
        public float FemaleTarg;
    }

    static Life_LSTARG_Values TargRate;
    public class Life_LSCSO80RATE_Values {
        public float MaleNS;
        public float MaleSM;
        public float FemaleNS;
        public float FemaleSM;
    }

    static Life_LSCSO80RATE_Values CSO80Rate;
    static double SUMAAA;
    static double SUMAA;
    static double SUMA;

    static double SUMMAX;
    static string NL;
    static long today;
    static int s;
    static int i;
    static int year;
    static int k;
    static int filnum;
    static int recordNo;

    static string MsgText;

    public static void Add_COLA(string Who, int Which) {
        int TempAge = 0;
        float OneYearCOLA = 0;

        OneYearCOLA = 0;
        switch (Who)
        {
            case "P":
                //Principal COLA
                if (Module2.LS_COLA_Client < 200000 && Module2.LS_COLA_Client < Module2.LSClient[Module2.SpouseClient].FaceAmt * 2 && Module2.AnnYr < 64)
                {
                    OneYearCOLA = (float)(Module2.LS_DB[Module2.GuarCurr] + Module2.LS_COLA_Client) * (Module2.LSClient[Module2.SpouseClient].COLA / 100);
                    if (OneYearCOLA > (Module2.LS_DB[Module2.GuarCurr] + (double)Module2.LS_COLA_Client * 0.2))
                        OneYearCOLA = (float)(Module2.LS_DB[Module2.GuarCurr] + (double)Module2.LS_COLA_Client * 0.2);
                    if (OneYearCOLA > 25000)
                        OneYearCOLA = 25000;
                    Module2.LS_COLA_Client = Module2.LS_COLA_Client + OneYearCOLA;
                }
                if (Module2.LS_COLA_Client > 200000)
                    Module2.LS_COLA_Client = 200000;
                if (Module2.LS_COLA_Client > (Module2.LSClient[Module2.SpouseClient].FaceAmt * 2))
                    Module2.LS_COLA_Client = Module2.LSClient[Module2.SpouseClient].FaceAmt * 2;
                break;
            case "S":
                //Spouse COLA
                TempAge = Module2.LSClient[Module2.SpouseClient].IssueAge - Module2.AnnYr + Module2.LSSpouseRider.IssueAge;
                if (Module2.LS_COLA_Spouse < 200000 && Module2.LS_COLA_Spouse < Module2.LSSpouseRider.FaceAmt * 2 && TempAge < 64)
                {
                    OneYearCOLA = (Module2.LSSpouseRider.FaceAmt + Module2.LS_COLA_Spouse) * (Module2.LSSpouseRider.COLA / 100);
                    if (OneYearCOLA > Convert.ToSingle(Module2.LSSpouseRider.FaceAmt) * 0.2f)
                        OneYearCOLA = Convert.ToSingle(Module2.LSSpouseRider.FaceAmt) * 0.2f;
                    if (OneYearCOLA > 25000)
                        OneYearCOLA = 25000;
                    Module2.LS_COLA_Spouse = Module2.LS_COLA_Spouse + OneYearCOLA;
                }
                if (Module2.LS_COLA_Spouse > 200000)
                    Module2.LS_COLA_Spouse = 200000;
                if (Module2.LS_COLA_Spouse > Module2.LSSpouseRider.FaceAmt * 2)
                    Module2.LS_COLA_Spouse = Module2.LSSpouseRider.FaceAmt * 2;
                break;
            case "I":
                //Additional Insured COLA
                TempAge = Module2.LSClient[Module2.SpouseClient].IssueAge - Module2.AnnYr + Module2.LSInsuredRider[Which].IssueAge;
                if (Module2.LS_COLA_Insured[Which] < 200000 && Module2.LS_COLA_Insured[Which] < Module2.LSInsuredRider[Which].FaceAmt * 2 && TempAge < 64)
                {
                    OneYearCOLA = (Module2.LSInsuredRider[Which].FaceAmt + Module2.LS_COLA_Insured[Which]) * (Module2.LSInsuredRider[Which].COLA / 100);
                    if (OneYearCOLA > Convert.ToSingle(Module2.LSInsuredRider[Which].FaceAmt) * 0.2f)
                        OneYearCOLA = Convert.ToSingle(Module2.LSSpouseRider.FaceAmt) * 0.2f;
                    if (OneYearCOLA > 25000)
                        OneYearCOLA = 25000;
                    Module2.LS_COLA_Spouse = Module2.LS_COLA_Insured[Which] + OneYearCOLA;
                    Module2.LS_COLA_Insured[Which] = Module2.LS_COLA_Insured[Which] + OneYearCOLA;
                }
                if (Module2.LS_COLA_Insured[Which] > 200000)
                    Module2.LS_COLA_Insured[Which] = 200000;
                if (Module2.LS_COLA_Insured[Which] > Module2.LSInsuredRider[Which].FaceAmt * 2)
                    Module2.LS_COLA_Insured[Which] = Module2.LSInsuredRider[Which].FaceAmt * 2;
                break;
            default:
                break;
        }

    }

    public static void Add_Modal_Premium() {
        s = Module2.SpouseClient;
        Module2.LS_Charges = Module2.LSClient[s].FaceAmt < 100000 ? 3.5 : 3.0;

        if (Module2.AnnYr < Module2.LSClient[s].IssueAge + Module2.LSClient[s].YearsToPay)
        {
            if ((Module2.AnnMo == 1 && Module2.LSClient[s].Mode.Equals("Annual", StringComparison.OrdinalIgnoreCase)) 
                || ((Module2.AnnMo == 1 || Module2.AnnMo == 7) && Module2.LSClient[s].Mode.Equals("SemiAnnual", StringComparison.OrdinalIgnoreCase)) 
                || ((Module2.AnnMo == 1 || Module2.AnnMo == 4 || Module2.AnnMo == 7 || Module2.AnnMo == 10) && Module2.LSClient[s].Mode.Equals("Quarterly", StringComparison.OrdinalIgnoreCase)) 
                || Module2.LSClient[s].Mode.Equals("M",StringComparison.OrdinalIgnoreCase))
            {
                Module2.LS_Outlay = Module2.LS_Outlay + Module2.LS_Modal_Prem;
                Module2.LS_Monthly_Prem = Module2.LS_Modal_Prem;
                Module2.LS_CV[Module2.GuarCurr] = Module2.LS_CV[Module2.GuarCurr] + Module2.LS_Modal_Prem;
            }
            else
                Module2.LS_Monthly_Prem = 0;
            if ((Module2.AnnMo == 1 && Module2.LSClient[s].Mode.Equals("Annual", StringComparison.OrdinalIgnoreCase))
                 || ((Module2.AnnMo == 1 || Module2.AnnMo == 7) && Module2.LSClient[s].Mode.Equals("SemiAnnual", StringComparison.OrdinalIgnoreCase))
                 || ((Module2.AnnMo == 1 || Module2.AnnMo == 4 || Module2.AnnMo == 7 || Module2.AnnMo == 10) && Module2.LSClient[s].Mode.Equals("Quarterly", StringComparison.OrdinalIgnoreCase))
                 || Module2.LSClient[s].Mode.Equals("M", StringComparison.OrdinalIgnoreCase))
                Module2.LS_Charges += Module2.LS_Modal_Prem * (Module2.AnnYr - Module2.LSClient[s].IssueAge < 10 ? 0.075 : 0.025);
        }
        else
            Module2.LS_Monthly_Prem = 0;

        Module2.LS_CV[Module2.GuarCurr] = Module2.LS_CV[Module2.GuarCurr] - Module2.LS_Charges;
    }

    public static void Calculate_Full_LS_Ledger(IllustrationInfo info, IAppData data) {
        int Begin_Age = 0;
        int Search = 0;

        s = Module2.SpouseClient;
        for (var year = 0; year < 9; year++)
        {
            var rider = info.Riders.FirstOrDefault(x => x.IndividualType == IndividualData.KnownIndices.Rider && x.IsSelected && x.Index == year);
            if (rider == null)
                break;
            Module2.LSClient[s].InsuredRider[year] = rider.Age;
            Module2.LSInsuredRider[year] = new Module2.LSInsuredRiderParm {
                FaceAmt = rider.FaceAmount.HasValue ? Convert.ToInt64(rider.FaceAmount.Value) : 0,
                IssueAge = rider.Age,
                SexCode = rider.Gender != null ? rider.Gender.Abbreviation.ToString() : "M",
                StartYear = rider.AgeOrYear ?? 0,
                EndYear = rider.EndYear ?? 0
            };
        }

        Module2.LSClient[s].SpouseRider = info.Riders.Any(x => x.IndividualType == IndividualData.KnownIndices.Spouse && x.IsSelected) ? 1 : 0;
        Module2.LSClient[s].ChildRider = info.IsChildRiderSelected ? 1 : 0;

        Module2.LSLedger[Module2.LSClient[s].IssueAge + 1].SurrenderValue[0] = 0;
        Module2.LSLedger[Module2.LSClient[s].IssueAge + 1].SurrenderValue[1] = 0;
        Module2.LSLedger[Module2.LSClient[s].IssueAge + 1].CashValue[0] = 0;
        Module2.LSLedger[Module2.LSClient[s].IssueAge + 1].CashValue[1] = 0;
        Module2.LSLedger[Module2.LSClient[s].IssueAge + 1].DeathBenefit[0] = Module2.LSClient[s].FaceAmt;
        Module2.LSLedger[Module2.LSClient[s].IssueAge + 1].DeathBenefit[1] = Module2.LSClient[s].FaceAmt;
        Module2.LSLedger[Module2.LSClient[s].IssueAge + 1].WithdrawAmount = 0;
        Module2.LSLedger[Module2.LSClient[s].IssueAge + 1].LoanAmount = 0;
        Module2.LSLedger[Module2.LSClient[s].IssueAge + 1].LoanRepayAmount = 0;
        Module2.LSLedger[Module2.LSClient[s].IssueAge + 1].LoanBalance = 0;

        Module2.GuarCurr = 0;

        Module2.LS_CV[Module2.GuarCurr] = Module2.LSClient[s].LumpSum == 0 ? 0 : Module2.LSClient[s].LumpSum * 0.925;

        Module2.LS_Outlay = Module2.LSClient[s].LumpSum;
        Module2.LS_CSV[Module2.GuarCurr] = 0;
        Module2.LS_DB[Module2.GuarCurr] = Module2.LSClient[s].FaceAmt;
        Module2.LS_DB_Minimum[Module2.GuarCurr] = Module2.LSClient[s].FaceAmt;
        Module2.LS_IRate[Module2.GuarCurr] = 0.04f;
        Module2.LS_Modal_Prem = Module2.LSClient[s].PlannedPrem;
        Module2.LS_DBOption = Module2.LSClient[s].OptionX;
        Module2.LS_CV_Opt2_Start[Module2.GuarCurr] = 0;
        Module2.LS_Index_Numerator_NP[Module2.GuarCurr, 0] = 0;
        Module2.LS_Index_Numerator_NP[Module2.GuarCurr, 1] = 0;
        Module2.LS_Index_Numerator_SC[Module2.GuarCurr, 0] = 0;
        Module2.LS_Index_Numerator_SC[Module2.GuarCurr, 1] = 0;
        Module2.LS_Index_Denominator[0] = 0;
        Module2.LS_Index_Denominator[1] = 0;
        Module2.LS_COLA_Client = 0;
        Module2.LS_COLA_Spouse = 0;
        for (i = 0; i < 9; i++)
        {
            Module2.LS_COLA_Insured[i] = 0;
        }
        Module2.LS_LoanBalance = 0;

        if (info.IllustrateBeginAtAge != 0)
        {
            Begin_Age = info.IllustrateBeginAtAge;
            Module2.LS_CV[0] = info.IllustrateInitialCashValue;
            Search = 1;
        }
        else
        {
            Begin_Age = Module2.LSClient[s].IssueAge;
            Search = 0;
        }

        Process_Ledger_Years(info, data, Begin_Age, Module1.MAXAGE, Search);

        Module2.GuarCurr = 1;
        Module2.LS_Outlay = Module2.LSClient[s].LumpSum;
        Module2.LS_CSV[Module2.GuarCurr] = 0;
        Module2.LS_DB[Module2.GuarCurr] = Module2.LSClient[s].FaceAmt;
        Module2.LS_DB_Minimum[Module2.GuarCurr] = Module2.LSClient[s].FaceAmt;
        Module2.LS_IRate[Module2.GuarCurr] = Module2.LSClient[s].CurrRate / 100;
        Module2.LS_Modal_Prem = Module2.LSClient[s].PlannedPrem;
        Module2.LS_DBOption = Module2.LSClient[s].OptionX;
        Module2.LS_CV_Opt2_Start[Module2.GuarCurr] = 0;

        Module2.LS_CV[Module2.GuarCurr] = Module2.LSClient[s].LumpSum == 0 ? 0 : Module2.LSClient[s].LumpSum * 0.925;
        Module2.LS_Index_Numerator_NP[Module2.GuarCurr, 0] = 0;
        Module2.LS_Index_Numerator_NP[Module2.GuarCurr, 1] = 0;
        Module2.LS_Index_Numerator_SC[Module2.GuarCurr, 0] = 0;
        Module2.LS_Index_Numerator_SC[Module2.GuarCurr, 1] = 0;
        Module2.LS_COLA_Client = 0;
        Module2.LS_COLA_Spouse = 0;
        for (i = 0; i < 9; i++)
        {
            Module2.LS_COLA_Insured[i] = 0;
        }

        if (info.IllustrateBeginAtAge != 0)
        {
            Begin_Age = info.IllustrateBeginAtAge;
            Module2.LS_CV[1] = info.IllustrateInitialCashValue;
            Search = 1;
        }
        else
        {
            Begin_Age = Module2.LSClient[s].IssueAge;
            Search = 0;
        }

        if (Module2.LSClient[s].PlannedPrem + Module2.LSClient[s].LumpSum < Module2.CalcPrem.TotMin - 1)
        {
            info.InForceYears = "be below Minimum";
            Module2.LS_Outlay = 0;
            Module2.LS_Modal_Prem = 0;
        }

        Process_Ledger_Years(info, data, Begin_Age, MAXAGE, Search);

        if (Module2.LSLedger[MAXAGE - 1].CashValue[1] > 0)
            info.InForceYears = "ENDOW at age 95";
        else
        {
            for (i = MAXAGE; i >= Module2.LSClient[Module2.SpouseClient].IssueAge; i += -1)
            {
                if (Module2.LSLedger[i].CashValue[1] > 0)
                {
                    info.InForceYears = " LAPSE at age " + Strings.Format(i + 1, "##");
                    break;
                }
            }
        }
    }

    public static void Calculate_Initial_Premiums(IllustrationInfo info, IAppData data) {
        Module2.Need_Guideline = 0;
        info.MinimumModalPremium = 0;
        info.TargetPremium = 0;
        info.MinGuidanceAnnualPremium = 0;
        info.MinGuidanceSinglePremiumPrincipal = 0;
        info.MinGuidanceSinglePremiumAdditional = 0;
        info.InForceYears = string.Empty;
        Get_Mortality_Rates(info, data);
        Do_Minimum_Premiums(info, data);
        Do_Target_Premiums(info, data);
        Do_Guideline_Single(info, data);
        Do_Guideline_Annual(info, data);

        if (Module2.Need_Target_CV != 0 && info.SpecialOptionsCashValue != 0)
        {
            Premium_To_Obtain_CV(info, data);
            Module2.Need_Target_CV = 0;
        }
        Module2.Need_Ledger = 0;
        Calculate_Full_LS_Ledger(info, data);
    }

    public static void Check_Future_Changes() {
        for (i = 0; i <= 4; i++)
        {
            if (Module2.LSFuture[i].DB_Age != 0)
            {
                if (Module2.LSFuture[i].DB_Age == Module2.AnnYr + 1)
                {
                    Module2.LS_DB[Module2.GuarCurr] = Module2.LSFuture[i].DB_Amount;
                    Module2.LS_DB_Minimum[Module2.GuarCurr] = Module2.LSFuture[i].DB_Amount;
                }
            }

            if (Module2.LSFuture[i].Prem_Age != 0 && Module2.LSFuture[i].Prem_Age == Module2.AnnYr + 1)
                Module2.LS_Modal_Prem = Module2.LSFuture[i].Prem_Amount;

            if (!(Module2.LSFuture[i].Int_Age == 0 || Module2.GuarCurr == 0) && Module2.LSFuture[i].Int_Age == Module2.AnnYr + 1)
                Module2.LS_IRate[Module2.GuarCurr] = Module2.LSFuture[i].Int_Amount / 100;

            if (Module2.LSFuture[i].Opt_Age != 0 && Module2.LSFuture[i].Opt_Age == Module2.AnnYr + 1)
            {
                if (Module2.LSFuture[i].Opt_Class == 1 && Module2.LS_DBOption == 2)
                {
                    Module2.LS_DB[Module2.GuarCurr] = Module2.LS_DB[Module2.GuarCurr] + Module2.LS_CV[Module2.GuarCurr] - Module2.LS_CV_Opt2_Start[Module2.GuarCurr];
                    Module2.LS_DB_Minimum[Module2.GuarCurr] = Module2.LS_DB_Minimum[Module2.GuarCurr] + Module2.LS_CV[Module2.GuarCurr] - Module2.LS_CV_Opt2_Start[Module2.GuarCurr];
                    Module2.LS_CV_Opt2_Start[Module2.GuarCurr] = 0;
                }
                if (Module2.LSFuture[i].Opt_Class == 2 && Module2.LS_DBOption == 1)
                    Module2.LS_CV_Opt2_Start[Module2.GuarCurr] = Module2.LS_CV[Module2.GuarCurr];
                Module2.LS_DBOption = Module2.LSFuture[i].Opt_Type;
            }
        }
    }

    public static void Check_Future_WD() {
        int WD_Change = 0;
        int Loan_Change = 0;
        int LoanPay_Change = 0;
        int Changes = 0;

        Module2.LS_Withdraw = 0;
        Module2.LS_LoanAmt = 0;
        Module2.LS_LoanPay = 0;

        for (Changes = 0; Changes <= 4; Changes++)
        {
            WD_Change = 0;
            Loan_Change = 0;
            LoanPay_Change = 0;

            if (Module2.LSFutureWD[Changes].WD_Age == 0)
                WD_Change = 0;
            else
            {
                if (Module2.LSFutureWD[Changes].WD_Age <= Module2.AnnYr && (Module2.LSFutureWD[Changes].WD_Age + Module2.LSFutureWD[Changes].WD_Years - 1) >= Module2.AnnYr)
                {
                    Module2.LS_Withdraw = Module2.LS_Withdraw + Module2.LSFutureWD[Changes].WD_Amount;
                    WD_Change = 1;
                }
            }

            if (Module2.LSFutureWD[Changes].Loan_Age == 0)
                Loan_Change = 0;
            else
            {
                if (Module2.LSFutureWD[Changes].Loan_Age <= Module2.AnnYr && (Module2.LSFutureWD[Changes].Loan_Age + Module2.LSFutureWD[Changes].Loan_Years - 1) >= Module2.AnnYr)
                {
                    Module2.LS_LoanAmt = Module2.LS_LoanAmt + Module2.LSFutureWD[Changes].Loan_Amount;
                    Loan_Change = 1;
                }
            }

            if (Module2.LSFutureWD[Changes].LoanPay_Age == 0)
                LoanPay_Change = 0;
            else
            {
                if (Module2.LSFutureWD[Changes].LoanPay_Age <= Module2.AnnYr && (Module2.LSFutureWD[Changes].LoanPay_Age + Module2.LSFutureWD[Changes].LoanPay_Years - 1) >= Module2.AnnYr)
                {
                    Module2.LS_LoanPay = Module2.LS_LoanPay + Module2.LSFutureWD[Changes].LoanPay_Amount;
                    LoanPay_Change = 1;
                }
            }

            if (WD_Change != 0 && Loan_Change != 0 && LoanPay_Change != 0)
                return;
        }

        Module2.LSLedger[Module2.AnnYr].WithdrawAmount = (float)Module2.LS_Withdraw;
        Module2.LSLedger[Module2.AnnYr].LoanAmount = (float)Module2.LS_LoanAmt;
        Module2.LSLedger[Module2.AnnYr].LoanRepayAmount = (float)Module2.LS_LoanPay;
        Module2.LSLedger[Module2.AnnYr].LoanBalance = Module2.LSLedger[Module2.AnnYr - 1].LoanBalance + Convert.ToSingle(Module2.LS_LoanAmt) - Convert.ToSingle(Module2.LS_LoanPay);
        Module2.LS_LoanBalance = Module2.LSLedger[Module2.AnnYr].LoanBalance;
        if (Module2.LSLedger[Module2.AnnYr].LoanBalance < 0)
        {
            Module2.LS_LoanPay = Module2.LS_LoanPay + Module2.LSLedger[Module2.AnnYr].LoanBalance;
            Module2.LSLedger[Module2.AnnYr].LoanRepayAmount = (float)Module2.LS_LoanPay;
            Module2.LSLedger[Module2.AnnYr].LoanBalance = 0;
            Module2.LS_LoanBalance = 0;
        }
        Module2.LS_CV[Module2.GuarCurr] = Module2.LS_CV[Module2.GuarCurr] - Module2.LS_Withdraw - Module2.LS_LoanAmt + Module2.LS_LoanPay;
        Module2.LS_CSV[Module2.GuarCurr] = Module2.LS_CSV[Module2.GuarCurr] - Module2.LS_Withdraw - Module2.LS_LoanAmt + Module2.LS_LoanPay;
        Module2.LS_DB[Module2.GuarCurr] = Module2.LS_DB[Module2.GuarCurr] - Module2.LS_Withdraw - Module2.LS_LoanAmt + Module2.LS_LoanPay;
        Module2.LS_DB_Minimum[Module2.GuarCurr] = Module2.LS_DB_Minimum[Module2.GuarCurr] - Module2.LS_Withdraw - Module2.LS_LoanAmt + Module2.LS_LoanPay;
        if (Module2.LS_Withdraw > 0)
        {
            Module2.LS_CV[Module2.GuarCurr] = Module2.LS_CV[Module2.GuarCurr] - 25;
            Module2.LS_CSV[Module2.GuarCurr] = Module2.LS_CSV[Module2.GuarCurr] - 25;
            Module2.LS_DB[Module2.GuarCurr] = Module2.LS_DB[Module2.GuarCurr] - 25;
            Module2.LS_DB_Minimum[Module2.GuarCurr] = Module2.LS_DB_Minimum[Module2.GuarCurr] - 25;
        }
    }

    public static void Child_Rider_Mortality() {
        if ((Module2.AnnYr - Module2.LSClient[Module2.SpouseClient].IssueAge) + Module2.LSChildRider.AgeYoungest < 23)
            Module2.LS_Insureds_Mortcost = Module2.GuarCurr == 1 || (Module2.AnnYr - Module2.LSClient[Module2.SpouseClient].IssueAge) == 0
                ? Module2.LS_Insureds_Mortcost + (Module2.LSChildRider.FaceAmt / 1000f * 0.4f)
                : Module2.LS_Insureds_Mortcost + (Module2.LSChildRider.FaceAmt / 1000f * 0.5f);
    }

    public static void ClearClient() {
        Module2.ClientInfo = new Module2.ClientData();
        Module2.LSClient[Module2.SpouseClient] = new Module2.LSClientParameters();
    }

    public static void Do_Anniversary(IllustrationInfo info, IAppData data) {
        string sex = null;
        int age = 0;
        int duration = 0;
        float returnRate = 0;
        double SurrCharge = 0;

        s = Module2.SpouseClient;
        sex = GetSexCode(Module2.LSClient[s], Module2.SpouseClient == 0 ? info.ClientData : info.SpouseAsClientData);

        age = Module2.LSClient[s].IssueAge;
        duration = Module2.AnnYr - Module2.LSClient[s].IssueAge + 1;
        if (Verify_LSSURR(data, Module2.LS_RateFiles[2], "P", sex, age, duration, ref returnRate))
        {
            SurrCharge = returnRate * (Module2.LSClient[s].FaceAmt / 1000);
            Module2.LS_CSV[Module2.GuarCurr] = Module2.LS_CV[Module2.GuarCurr] - SurrCharge;
            if (Module2.LS_CSV[Module2.GuarCurr] < 0)
                Module2.LS_CSV[Module2.GuarCurr] = 0;
        }
        else
            Module2.LS_CSV[Module2.GuarCurr] = Module2.LS_CV[Module2.GuarCurr];

        if (Module2.LS_CSV[Module2.GuarCurr] < 0)
            Module2.LS_CSV[Module2.GuarCurr] = 0;
    }

    public static void Do_Guideline_Annual(IllustrationInfo info, IAppData data) {
        //Calculate Guideline Annual Premium, using base
        //policy, option 1 death benefit, and the current
        //interest rate (first year)/4% (2nd year and beyond)

        //By definition, the guideline premium is the maximum
        //premium that will not allow the cash value to equal
        //or exceed the initial death benefit (or the corridor
        //of coverage death benefit)

        //Finding this premium is a matter of guesswork and inter-
        //polation.  Start with a reasonable first guess that is
        //guaranteed to be "high": Guideline single premium / 10.
        //Save the resultant cash value at age 95.  Now set up a
        //reasonable guess that is guaranteed to be "low".  ($10)
        //Save the resultant cash value.

        //From this point on, we have a "high" and "low" guess and
        //their resulting cash values. For every new guess, determine
        //the "position" of the target cash value between the
        //resultant high and low values.  This "position" (expressed
        //as a percent) will be used to determine the next guess:
        //That same percent times the difference in "high" and "low"
        //guesses, added to the low guess.

        //The process is repeated until the guesses are less than
        //1.5 cents apart.  The low guess then becomes the Guideline
        //modal premium.

        float HighGuess = 0;
        float LowGuess = 0;
        double HighValue = 0;
        double LowValue = 0;
        double Percentage = 0;
        float CurrPremium = 0;

        double ScreenSum = 0;
        string ScreenValue = null;

        Module2.GuarCurr = 1;
        HighGuess = Module2.CalcPrem.GuideSingle / 10;
        CurrPremium = HighGuess;
        LowGuess = 10;
        Calculate_Back(data, ref CurrPremium);
        HighValue = Module2.LS_CV[Module2.GuarCurr];
        CurrPremium = LowGuess;
        Calculate_Back(data, ref CurrPremium);
        LowValue = Module2.LS_CV[Module2.GuarCurr];

        while (((HighGuess - LowGuess) > 0.015f))
        {
            if (System.Math.Abs(Module2.LS_CV[Module2.GuarCurr]) < 0.5)
            {
                if (Module2.LS_CV[Module2.GuarCurr] < 0)
                {
                    HighGuess = CurrPremium - 0.01f;
                    LowGuess = CurrPremium + 0.01f;
                }
                else
                {
                    HighGuess = CurrPremium;
                    LowGuess = CurrPremium;
                }
            }
            else
            {
                Module2.LS_CV[Module2.GuarCurr] = HighValue - LowValue;
                Percentage = HighValue / Module2.LS_CV[Module2.GuarCurr];
                CurrPremium = (HighGuess - LowGuess) * Convert.ToSingle(Percentage);
                CurrPremium = HighGuess - CurrPremium;
                Calculate_Back(data, ref CurrPremium);

                if (Module2.LS_CV[Module2.GuarCurr] < 0)
                {
                    HighValue = Module2.LS_CV[Module2.GuarCurr];
                    HighGuess = CurrPremium;
                }
                else
                {
                    LowValue = Module2.LS_CV[Module2.GuarCurr];
                    LowGuess = CurrPremium;
                }
            }
        }

        Module2.CalcPrem.GuideAnnual = Conversion.Int(LowGuess);
        ScreenSum = Module2.CalcPrem.GuideAnnual;

        Module2.Format_Amount(ScreenValue, 12, ScreenSum, " ");
        info.MinGuidanceAnnualPremium = ScreenSum;
    }

    private static void Calculate_Back(IAppData data, ref float CurrPremium) {
        Module2.LS_COLA_Client = 0;
        Module2.LS_CV[Module2.GuarCurr] = Module2.LSClient[Module2.SpouseClient].FaceAmt * 0.98;
        Module2.LS_DB[Module2.GuarCurr] = Module2.LSClient[Module2.SpouseClient].FaceAmt;
        Module2.LS_IRate[Module2.GuarCurr] = Convert.ToSingle(System.Math.Pow(1.04d, (1d / 12d)));
        float WaiverPct = 0;
        Module2.LS_LoanBalance = 0;
        float corrRate = 0.0f;
        float TempPremium = 0.0f;
        bool hasLSCorr = false;
        Module2.LS_CV_Opt2_Start[1] = 0;
        for (Module2.AnnYr = MAXAGE - 1; Module2.AnnYr >= Module2.LSClient[Module2.SpouseClient].IssueAge + 1; Module2.AnnYr += -1)
        {
            hasLSCorr = Verify_LSCORR(data, Module2.LS_RateFiles[9], Module2.AnnYr, ref corrRate);
            for (Module2.AnnMo = 1; Module2.AnnMo <= 12; Module2.AnnMo++)
            {
                Module2.LS_Charges = Module2.LSClient[s].FaceAmt < 100000 ? 3.5 : 3.0;
                if (hasLSCorr && Module2.LS_DB[Module2.GuarCurr] < Module2.LS_CV[Module2.GuarCurr] * corrRate)
                    Module2.LS_DB[Module2.GuarCurr] = Module2.LS_CV[Module2.GuarCurr] * corrRate;
                Principal_Mortality();

                TempPremium = Module2.AnnMo == 12
                    ? CurrPremium * (Module2.AnnYr > Module2.LSClient[Module2.SpouseClient].IssueAge + 9 ? 0.975f : 0.925f)
                    : 0;
                Reverse_CashValue(Module2.LSClient[Module2.SpouseClient].SubStd > 0 ? 0 : 1, WaiverPct, TempPremium, 0);
            }
        }
        Module2.LS_IRate[Module2.GuarCurr] = 1 + (Module2.LSClient[Module2.SpouseClient].CurrRate / 100);
        Module2.LS_IRate[Module2.GuarCurr] = Convert.ToSingle(System.Math.Pow(Module2.LS_IRate[Module2.GuarCurr], (1d / 12d)));
        Module2.AnnYr = Module2.LSClient[Module2.SpouseClient].IssueAge;
        hasLSCorr = Verify_LSCORR(data, Module2.LS_RateFiles[9], Module2.AnnYr, ref corrRate);
        for (Module2.AnnMo = 1; Module2.AnnMo <= 12; Module2.AnnMo++)
        {
            Module2.LS_Charges = Module2.LSClient[s].FaceAmt < 100000 ? 3.5 : 3.0;
            if (hasLSCorr && Module2.LS_DB[Module2.GuarCurr] < Module2.LS_CV[Module2.GuarCurr] * corrRate)
                Module2.LS_DB[Module2.GuarCurr] = Module2.LS_CV[Module2.GuarCurr] * corrRate;
            Principal_Mortality();

            TempPremium = Module2.AnnMo == 12 ? CurrPremium * 0.925f : 0;
            Reverse_CashValue(Module2.LSClient[s].SubStd > 0 ? 0 : 1, WaiverPct, TempPremium, 0);
        }
    }

    public static void Do_Guideline_Single(IllustrationInfo info, IAppData data) {
        //Calculate Guideline Single Premium, using base
        //policy, option 1 death benefit, and the current
        //interest rate (first year)/6% (2nd year and beyond)

        //By definition, the guideline premium is the maximum
        //premium that will not allow the cash value to equal
        //or exceed the initial death benefit (or the corridor
        //of coverage death benefit)

        float CorrRate = 0;
        float WaiverPct = 0;
        float CurrPremium = 0;
        bool hasLsCorr = false;
        int AddInsds = 0;
        //for 8/2/95 update

        double ScreenSum = 0;
        string ScreenValue = null;

        Module2.GuarCurr = 1;
        Module2.LS_COLA_Client = 0;
        Module2.LS_CV[Module2.GuarCurr] = Module2.LSClient[Module2.SpouseClient].FaceAmt * 0.98;
        Module2.LS_CSV[Module2.GuarCurr] = Module2.LSClient[Module2.SpouseClient].FaceAmt;
        //for additional insureds
        Module2.LS_DB[Module2.GuarCurr] = Module2.LSClient[Module2.SpouseClient].FaceAmt;
        Module2.LS_IRate[Module2.GuarCurr] = Convert.ToSingle(System.Math.Pow(1.06, (1d / 12d)));
        WaiverPct = 0;
        CurrPremium = 0;
        Module2.LS_LoanBalance = 0;
        Module2.LS_CV_Opt2_Start[1] = 0;
        for (Module2.AnnYr = MAXAGE - 1; Module2.AnnYr >= Module2.LSClient[Module2.SpouseClient].IssueAge + 1; Module2.AnnYr += -1)
        {
            hasLsCorr = Verify_LSCORR(data, Module2.LS_RateFiles[9], Module2.AnnYr, ref CorrRate);
            for (Module2.AnnMo = 1; Module2.AnnMo <= 12; Module2.AnnMo++)
            {
                Module2.LS_Charges = Module2.LSClient[s].FaceAmt < 100000 ? 3.5 : 3.0;
                if (hasLsCorr && Module2.LS_DB[Module2.GuarCurr] < Module2.LS_CV[Module2.GuarCurr] * CorrRate)
                    Module2.LS_DB[Module2.GuarCurr] = Module2.LS_CV[Module2.GuarCurr] * CorrRate;
                Principal_Mortality();

                ///*  added spouse and additional insured mortality  */
                ///*  at request of Special Sales/Actuarial (8/2/95) */

                Module2.LS_Insureds_Mortcost = 0;
                AddInsds = 0;
                if (Module2.LSClient[s].SpouseRider != 0)
                {
                    Spouse_Rider_Mortality(data, 1);
                    AddInsds = 1;
                }
                for (k = 0; k < 9; k++)
                {
                    if (Module2.LSClient[s].InsuredRider[k] != 0)
                    {
                        Insured_Rider_Mortality(k, true);
                        AddInsds = 1;
                    }
                }
                if (Module2.LSClient[s].AddFace > 0)
                {
                    Principal_Rider_Mortality(1);
                    AddInsds = 1;
                }

                ///*  Module2.LS_Insureds_Mortcost now set for call to Reverse_CashValue
                Reverse_CashValue(Module2.LSClient[Module2.SpouseClient].SubStd > 0 ? 0 : 1, WaiverPct, CurrPremium, AddInsds);
            }
        }

        Module2.LS_IRate[Module2.GuarCurr] = 1 + (Module2.LSClient[Module2.SpouseClient].CurrRate / 100);
        Module2.LS_IRate[Module2.GuarCurr] = Convert.ToSingle(System.Math.Pow(Module2.LS_IRate[Module2.GuarCurr], (1d / 12d)));
        i = Module2.LSClient[Module2.SpouseClient].IssueAge;
        hasLsCorr = Verify_LSCORR(data, Module2.LS_RateFiles[9], i, ref CorrRate);
        for (year = 1; year <= 12; year++)
        {
            Module2.LS_Charges = Module2.LSClient[s].FaceAmt < 100000 ? 3.5 : 3.0;
            if (hasLsCorr && Module2.LS_DB[Module2.GuarCurr] < Module2.LS_CV[Module2.GuarCurr] * CorrRate)
                Module2.LS_DB[Module2.GuarCurr] = Module2.LS_CV[Module2.GuarCurr] * CorrRate;
            Principal_Mortality();

            AddInsds = 0;
            Module2.LS_Insureds_Mortcost = 0;
            if (Module2.LSClient[s].SpouseRider != 0)
            {
                Spouse_Rider_Mortality(data, 1);
                AddInsds = 1;
            }
            for (k = 0; k < 9; k++)
            {
                if (Module2.LSClient[s].InsuredRider[k] != 0)
                {
                    Insured_Rider_Mortality(k, true);
                    AddInsds = 1;
                }
            }

            Reverse_CashValue(Module2.LSClient[Module2.SpouseClient].SubStd > 0 ? 0 : 1, WaiverPct, CurrPremium, AddInsds);
        }

        Module2.CalcPrem.GuideSingle = Convert.ToSingle(Module2.LS_CV[Module2.GuarCurr] / 0.00925);
        Module2.CalcPrem.GuideSingle = Conversion.Int((Module2.CalcPrem.GuideSingle + 5) / 100);
        ScreenSum = Module2.CalcPrem.GuideSingle;
        Module2.Format_Amount(ScreenValue, 12, ScreenSum, " ");
        info.MinGuidanceSinglePremiumPrincipal = ScreenSum;

        if (AddInsds != 0)
        {
            Module2.CalcPrem.GuideSingleAddInsd = Convert.ToSingle(Module2.LS_CSV[Module2.GuarCurr] / 0.00925);
            Module2.CalcPrem.GuideSingleAddInsd = Conversion.Int((Module2.CalcPrem.GuideSingleAddInsd + 5) / 100);
            ScreenSum = Module2.CalcPrem.GuideSingleAddInsd;
            Module2.Format_Amount(ScreenValue, 12, ScreenSum, " ");
            info.MinGuidanceSinglePremiumAdditional = ScreenSum;
        }
        else
        {
            Module2.CalcPrem.GuideSingleAddInsd = 0;
            info.MinGuidanceSinglePremiumAdditional = 0;
        }

    }

    public static void Do_Minimum_Premiums(IllustrationInfo info, IAppData data) {
        string sex = null;
        string smoke = null;
        int age = 0;
        int Band = 0;
        float returnRate = 0;
        float Three_Year_Mort = 0;
        bool hasLSMinp = false;
        double MinPrem = 0;
        double Mort_For_WPD = 0;
        double Average_WPD = 0;

        double ScreenSum = 0;
        string ScreenValue = null;

        MinPrem = 0;
        Mort_For_WPD = 0;
        Module2.CalcPrem.PrinMin = 0;
        s = Module2.SpouseClient;
        sex = GetSexCode(Module2.LSClient[s], Module2.SpouseClient == 0 ? info.ClientData : info.SpouseAsClientData);
        smoke = Module2.LSClient[s].PlanCode == 1 ? "N" : "S";
        age = Module2.LSClient[s].IssueAge;
        if (Module2.LSClient[s].FaceAmt < 50000)
            Band = 1;
        else if (Module2.LSClient[s].FaceAmt.IsBetween(50000, 99999, true))
            Band = 2;
        else if (Module2.LSClient[s].FaceAmt.IsBetween(100000, 249999, true))
            Band = 3;
        else
            Band = 4;

        hasLSMinp = Verify_LSMINP(data, Module2.LS_RateFiles[0], sex, smoke, age, Band - 1, ref returnRate);
        if (hasLSMinp)
            MinPrem = (returnRate * (Module2.LSClient[s].FaceAmt / 1000)) + Module2.LSClient[s].FaceAmt < 100000 ? 45.41 : 38.92;

        if (Module2.LSClient[s].SubStd > 0)
        {
            Three_Year_Mort = 0;
            for (i = 0; i <= 2; i++)
            {
                Module2.LS_MortRate = Module2.LSMort[1, Module2.LSClient[s].IssueAge + i].Prin_Sub[Module2.LS_Prin_Band];
                Module2.LS_MortRate = Module2.LS_MortRate * Module2.LSClient[s].FaceAmt / 1000;
                Three_Year_Mort = Three_Year_Mort + (Module2.LS_MortRate * 12);
            }
            MinPrem = MinPrem + ((Three_Year_Mort / 3) * 1.081081) + 0.005;
            Mort_For_WPD = Mort_For_WPD + Three_Year_Mort;
        }
        if (Module2.LSClient[s].GPO != 0)
        {
            Three_Year_Mort = 0;
            for (i = 0; i <= 2; i++)
            {
                Module2.LS_MortRate = Module2.LSClient[s].GPO / 1000 * Module2.LSMort[1, Module2.LSClient[s].IssueAge + i].Prin_GPO;
                Three_Year_Mort = Three_Year_Mort + (Module2.LS_MortRate * 12);
            }
            MinPrem = MinPrem + ((Three_Year_Mort / 3) * 1.081081) + 0.005;
            Mort_For_WPD = Mort_For_WPD + Three_Year_Mort;
        }
        if (Module2.LSClient[s].AddFace != 0)
        {
            Three_Year_Mort = 0;
            for (i = 0; i <= 2; i++)
            {
                Module2.LS_MortRate = Module2.LSMort[1, Module2.LSClient[s].IssueAge + i].Insured_Base[0];
                if (Module2.LSClient[s].SubStd > 0)
                    Module2.LS_MortRate = Module2.LS_MortRate + Module2.LSMort[1, Module2.LSClient[s].IssueAge + i].Insured_Sub[0];
                Module2.LS_MortRate = Module2.LSClient[s].AddFace / 1000 * Module2.LS_MortRate;
                Three_Year_Mort = Three_Year_Mort + (Module2.LS_MortRate * 12);
            }
            MinPrem = MinPrem + ((Three_Year_Mort / 3) * 1.081081) + 0.005;
            Mort_For_WPD = Mort_For_WPD + Three_Year_Mort;
        }

        Modalize_Minimum(MinPrem);
        Module2.CalcPrem.PrinMin = Convert.ToSingle(MinPrem);

        //Minimum for Spouse Rider

        if (Module2.LSClient[s].SpouseRider != 0)
        {
            MinPrem = 0;
            sex = GetSexCode(Module2.LSClient[s], s == 0 ? info.ClientData : info.SpouseAsClientData);
            age = Module2.LSSpouseRider.IssueAge;

            Three_Year_Mort = 0;
            for (i = 0; i <= 2; i++)
            {
                Module2.LS_MortRate = Module2.LSMort[1, Module2.LSSpouseRider.IssueAge + i].Spouse_Base;
                if (Module2.LSSpouseRider.SubStd > 0)
                    Module2.LS_MortRate = Module2.LS_MortRate + Module2.LSMort[1, Module2.LSSpouseRider.IssueAge + i].Spouse_Sub;
                Module2.LS_MortRate = Module2.LS_MortRate * (Module2.LSSpouseRider.FaceAmt / 1000);
                Three_Year_Mort = Three_Year_Mort + (Module2.LS_MortRate * 12);
            }
            MinPrem = MinPrem + ((Three_Year_Mort / 3) * 1.081081) + 0.005;
            Modalize_Minimum(MinPrem);
            Mort_For_WPD = Mort_For_WPD + Three_Year_Mort;
            Module2.CalcPrem.SpouseMin = Convert.ToSingle(MinPrem);
        }
        else
            Module2.CalcPrem.SpouseMin = 0;

        //Minimum for Insured Rider (other than Principal)

        for (i = 0; i < 9; i++)
        {
            if (Module2.LSClient[s].InsuredRider[i] != 0 && Module2.LSInsuredRider[i].StartYear < 4)
            {
                MinPrem = 0;
                sex = Module2.LSClient[s].State == "MT" ? "M" : Module2.LSInsuredRider[i].SexCode;
                age = Module2.LSSpouseRider.IssueAge;
                returnRate = 0;

                Three_Year_Mort = 0;
                for (year = 0; year <= 2; year++)
                {
                    if (Module2.LSInsuredRider[i].StartYear - 1 <= year)
                    {
                        Module2.LS_MortRate = Module2.LSMort[1, Module2.LSInsuredRider[i].IssueAge + year].Insured_Base[i];
                        if (Module2.LSInsuredRider[i].SubStd > 0)
                            Module2.LS_MortRate = Module2.LS_MortRate + Module2.LSMort[1, Module2.LSInsuredRider[i].IssueAge + year].Insured_Sub[i];
                        Module2.LS_MortRate = Module2.LS_MortRate * (Module2.LSInsuredRider[i].FaceAmt / 1000);
                        Three_Year_Mort = Three_Year_Mort + (Module2.LS_MortRate * 12);
                    }
                }
                MinPrem = MinPrem + ((Three_Year_Mort / 3) * 1.081081) + 0.005;
                Modalize_Minimum(MinPrem);
                Mort_For_WPD = Mort_For_WPD + Three_Year_Mort;
                Module2.CalcPrem.InsdMin[i] = Convert.ToSingle(MinPrem);
            }
            else
                Module2.CalcPrem.InsdMin[i] = 0;
        }

        //Minimum for Child Rider
        if (Module2.LSClient[s].ChildRider != 0)
        {
            MinPrem = 0;
            Three_Year_Mort = 0;
            for (i = 0; i <= 2; i++)
            {
                if (Module2.LSChildRider.AgeYoungest + i < 24)
                {
                    Module2.LS_MortRate = 0.4f * (Module2.LSChildRider.FaceAmt / 1000);
                    Three_Year_Mort = Three_Year_Mort + (Module2.LS_MortRate * 12);
                }
            }
            MinPrem = MinPrem + ((Three_Year_Mort / 3) * 1.081081) + 0.005;
            Modalize_Minimum(MinPrem);
            Mort_For_WPD = Mort_For_WPD + Three_Year_Mort;
            Module2.CalcPrem.ChildMin = Convert.ToSingle(MinPrem);
        }
        else
            Module2.CalcPrem.ChildMin = 0;

        if (Module2.LSClient[s].WPD != 0)
        {
            MinPrem = 0;
            Three_Year_Mort = 0;
            Average_WPD = 0;
            for (i = 0; i <= 2; i++)
            {
                Average_WPD = Average_WPD + Module2.LSMort[1, Module2.LSClient[s].IssueAge + i].Prin_WPD;
                if (Module2.LSClient[s].FaceAmt < 100000)
                {
                    Three_Year_Mort = Convert.ToSingle(Three_Year_Mort + (((Module2.LSMort[1, Module2.LSClient[s].IssueAge + i].Prin_Base[Module2.LS_Prin_Band] * (Module2.LSClient[s].FaceAmt / 1000)) * 12) * 0.996737));
                    Three_Year_Mort = Three_Year_Mort + 42;
                }
                else
                {
                    Three_Year_Mort = Convert.ToSingle(Three_Year_Mort + (((Module2.LSMort[1, Module2.LSClient[s].IssueAge + i].Prin_Base[Module2.LS_Prin_Band] * (Module2.LSClient[s].FaceAmt / 1000)) * 12) * 0.996737));
                    Three_Year_Mort = Three_Year_Mort + 36;
                }
            }
            Average_WPD = Average_WPD / 3;
            MinPrem = MinPrem + ((Three_Year_Mort / 3) * 1.081081) + 0.005;
            MinPrem = MinPrem + ((Mort_For_WPD / 3) * 1.081081) + 0.005;
            MinPrem = MinPrem * Average_WPD;
            Modalize_Minimum(MinPrem);
            Module2.CalcPrem.PrinMin = Module2.CalcPrem.PrinMin + Convert.ToSingle(MinPrem);
        }

        Module2.CalcPrem.TotMin = Module2.CalcPrem.PrinMin + Module2.CalcPrem.SpouseMin + Module2.CalcPrem.ChildMin;
        for (i = 1; i <= 9; i++)
        {
            Module2.CalcPrem.TotMin = Module2.CalcPrem.TotMin + Module2.CalcPrem.InsdMin[i];
        }
        ScreenSum = Module2.CalcPrem.TotMin;
        Module2.Format_Amount(ScreenValue, 12, ScreenSum, " ");
        info.MinimumModalPremium = ScreenSum;
        return;
    }

    private static void Modalize_Minimum(double MinPrem) {
        switch (Module2.LSClient[s].Mode)
        {
            case "A":
                break;
            case "S":
                MinPrem = MinPrem / 2;
                break;
            case "Q":
                MinPrem = MinPrem / 4;
                break;
            default:
                MinPrem = MinPrem / 12;
                break;
        }
    }

    public static void Do_Month_Interest(ref double IntRate) {
        //Returns the rate for one month of interest in IntRate
        IntRate = 1 + Module2.LS_IRate[Module2.GuarCurr];
        IntRate = System.Math.Pow(IntRate, (1d / 12d));
    }

    private static string GetSexCode(Module2.LSClientParameters clientParams, IndividualData clientOrSpouse) {
        return clientParams.State == "MT" ? "M" : clientOrSpouse.Gender.Abbreviation.ToString();
    }

    public static void Do_Target_Premiums(IllustrationInfo info, IAppData data) {
        string sex = null;
        int age = 0;
        float returnRate = 0;
        double TargPrem = 0;
        double Mort_For_WPD = 0;

        double ScreenSum = 0;
        string ScreenValue = null;

        s = Module2.SpouseClient;
        TargPrem = 0;
        Mort_For_WPD = 0;
        Module2.CalcPrem.PrinTarg = 0;
        sex = GetSexCode(Module2.LSClient[s], Module2.SpouseClient == 0 ? info.ClientData : info.SpouseAsClientData);
        age = Module2.LSClient[s].IssueAge;
        returnRate = 0;
        if (Verify_LSTARG(data, Module2.LS_RateFiles[1], sex, age, ref returnRate))
            TargPrem = returnRate * (Module2.LSClient[s].FaceAmt / 1000);
        if (Module2.LSClient[s].SubStd > 0)
        {
            Module2.LS_MortRate = Module2.LSMort[1, Module2.LSClient[s].IssueAge].Prin_Sub[Module2.LS_Prin_Band];
            Module2.LS_MortRate = Module2.LS_MortRate * Module2.LSClient[s].FaceAmt / 1000;
            TargPrem = TargPrem + (Module2.LS_MortRate * 12);
            Mort_For_WPD = Mort_For_WPD + (Module2.LS_MortRate * 12);
        }
        if (Module2.LSClient[s].GPO != 0)
        {
            Module2.LS_MortRate = Module2.LSClient[s].GPO / 1000 * Module2.LSMort[1, Module2.LSClient[s].IssueAge].Prin_GPO;
            TargPrem = TargPrem + (Module2.LS_MortRate * 12);
            Mort_For_WPD = Mort_For_WPD + (Module2.LS_MortRate * 12);
        }
        if (Module2.LSClient[s].AddFace != 0 && Module2.LSClient[s].AddFaceStart == 1)
        {
            Module2.LS_MortRate = Module2.LSClient[s].AddFace / 1000 * Module2.LSMort[1, Module2.LSClient[s].IssueAge].Insured_Base[0];
            TargPrem = TargPrem + (Module2.LS_MortRate * 12);
            Mort_For_WPD = Mort_For_WPD + (Module2.LS_MortRate * 12);
        }

        TargPrem = Modalize_Target(TargPrem);
        Module2.CalcPrem.PrinTarg = Convert.ToSingle(TargPrem);

        //Target for Spouse Rider

        if (Module2.LSClient[s].SpouseRider != 0)
        {
            TargPrem = 0;
            sex = GetSexCode(Module2.LSClient[s], s == 0 ? info.ClientData : info.SpouseAsClientData);
            age = Module2.LSSpouseRider.IssueAge;
            returnRate = 0;

            Module2.LS_MortRate = Module2.LSMort[1, Module2.LSSpouseRider.IssueAge].Spouse_Base;
            if (Module2.LSSpouseRider.SubStd > 0)
            {
                Module2.LS_MortRate = Module2.LS_MortRate + Module2.LSMort[1, Module2.LSSpouseRider.IssueAge].Spouse_Sub;
            }
            Module2.LS_MortRate = (Module2.LS_MortRate * Module2.LSSpouseRider.FaceAmt / 1000) * 12;
            TargPrem = TargPrem + Module2.LS_MortRate;
            Mort_For_WPD = Mort_For_WPD + TargPrem;
            TargPrem = Modalize_Target(TargPrem);

            Module2.CalcPrem.SpouseTarg = Convert.ToSingle(TargPrem);
        }
        else
            Module2.CalcPrem.SpouseTarg = 0;

        //Target for Insured Rider (other than additional principal)

        for (i = 0; i < 9; i++)
        {
            if (Module2.LSClient[s].InsuredRider[i] != 0 && Module2.LSInsuredRider[i].StartYear != 0)
            {
                TargPrem = 0;
                sex = Module2.LSClient[s].State == "MT" ? "M" : Module2.LSClient[s].SexCode;
                age = Module2.LSSpouseRider.IssueAge;
                returnRate = 0;

                Module2.LS_MortRate = Module2.LSMort[1, Module2.LSInsuredRider[i].IssueAge].Insured_Base[i];
                if (Module2.LSInsuredRider[i].SubStd > 0)
                {
                    Module2.LS_MortRate = Module2.LS_MortRate + Module2.LSMort[1, Module2.LSInsuredRider[i].IssueAge].Insured_Sub[i];
                }
                Module2.LS_MortRate = (Module2.LS_MortRate * Module2.LSInsuredRider[i].FaceAmt / 1000) * 12;
                TargPrem = TargPrem + Module2.LS_MortRate;
                Mort_For_WPD = Mort_For_WPD + TargPrem;
                TargPrem = Modalize_Target(TargPrem);

                Module2.CalcPrem.InsdTarg[i] = Convert.ToSingle(TargPrem);
            }
            else
                Module2.CalcPrem.InsdTarg[i] = 0;
        }

        //Target for Child

        if (Module2.LSClient[s].ChildRider != 0)
        {
            TargPrem = (0.4 * Module2.LSChildRider.FaceAmt / 1000) * 12;
            Mort_For_WPD = Mort_For_WPD + TargPrem;
            TargPrem = Modalize_Target(TargPrem);
            Module2.CalcPrem.ChildTarg = Convert.ToSingle(TargPrem);
        }
        else
            Module2.CalcPrem.ChildTarg = 0;

        if (Module2.LSClient[s].WPD != 0)
        {
            Mort_For_WPD = Mort_For_WPD + ((Module2.LSMort[1, Module2.LSClient[s].IssueAge].Prin_Base[Module2.LS_Prin_Band] * (Module2.LSClient[s].FaceAmt / 1000)) * 12);
            Module2.LS_MortRate = Convert.ToSingle((((Module2.LSClient[s].FaceAmt < 100000 ? 42 : 36) + Mort_For_WPD) * Module2.LSMort[1, Module2.LSClient[s].IssueAge].Prin_WPD));
            Module2.CalcPrem.PrinTarg = Module2.CalcPrem.PrinTarg + Module2.LS_MortRate;
        }

        Module2.CalcPrem.TotTarg = Module2.CalcPrem.PrinTarg + Module2.CalcPrem.SpouseTarg + Module2.CalcPrem.ChildTarg;
        for (i = 1; i <= 9; i++)
        {
            Module2.CalcPrem.TotTarg = Module2.CalcPrem.TotTarg + Module2.CalcPrem.InsdTarg[i];
        }
        ScreenSum = Module2.CalcPrem.TotTarg;
        Module2.Format_Amount(ScreenValue, 12, ScreenSum, " ");
        info.TargetPremium = ScreenSum;
    }

    private static double Modalize_Target(double value) {
        switch (Module2.LSClient[s].Mode)
        {
            case "A":
                break;
            case "S":
                value = value / 2;
                break;
            case "Q":
                value = value / 4;
                break;
            default:
                value = value / 12;
                break;
        }
        return value;
    }

    public static void Format_Ledger_Amount(ref string amtStr, int amtLen, double amtVal, string Comma, int Nsign) {
        amtStr = amtVal.ToString(Comma == "Y" ? "###,###,##0" : "#######0");
        if (amtStr.Length < amtLen)
            amtStr = amtStr.PadLeft(amtLen);
    }

    public static void Get_Mortality_Rates(IllustrationInfo info, IAppData data) {
        //This routine will get all possible mortality rates
        //(guaranteed and current) for each illustration. For
        //each ledger print, the mortality rates will rely on
        //the values in the global array "LSMort".

        string sex = null;
        string smoke = null;
        int band = 0;
        float returnRate = 0;
        float Tables = 0;
        float BandFace = 0;
        int i;
        int j;

        s = Module2.SpouseClient;
        sex = GetSexCode(Module2.LSClient[s], s == 0 ? info.ClientData : info.SpouseAsClientData);
        smoke = Module2.LSClient[s].PlanCode == 1 ? "N" : "S";

        //Guaranteed or current
        for (i = 0; i <= 1; i++)
        {
            //All possible is
            for (j = 0; j < MAXAGE; j++)
            {
                if (i == 0 && j > Module2.LSClient[s].IssueAge)
                    band = 5;
                else
                {
                    BandFace = Module2.LSClient[s].FaceAmt;
                    if (Module2.LSClient[s].AddFace != 0 && Module2.LSClient[s].AddFaceStart >= j && Module2.LSClient[s].AddFaceEnd <= j)
                        BandFace += Module2.LSClient[s].AddFace;
                    if (BandFace.IsBetween(50000, 99999, true))
                        band = 2;
                    else if (BandFace.IsBetween(100000, 249999, true))
                        band = 3;
                    else if (BandFace >= 250000)
                        band = 4;
                    else
                        band = 1;
                }
                if (band == 5)
                {
                    if (Verify_LSRATEPR(data, Module2.LS_RateFiles[3], "P", sex, smoke, j, band, ref returnRate))
                    {
                        Module2.LSMort[i, j].Prin_Base[1] = returnRate;
                        Module2.LSMort[i, j].Prin_Base[2] = returnRate;
                        Module2.LSMort[i, j].Prin_Base[3] = returnRate;
                        Module2.LSMort[i, j].Prin_Base[4] = returnRate;
                    }
                    else
                    {
                        MsgText = "Mortality rate not found for Client (sex = " + sex + ", age = " + Conversion.Str(i) + ", smoker = " + smoke + " face amount band = " + Conversion.Str(band) + ".  Illustration NOT VALID!!";
                        Interaction.MsgBox(MsgText);
                        return;
                    }
                }
                else
                {
                    Module2.LS_Prin_Band = band;
                    if (Verify_LSRATEPR(data, Module2.LS_RateFiles[3], "P", sex, smoke, i, band, ref returnRate))
                        Module2.LSMort[i, j].Prin_Base[band - 1] = returnRate;
                    for (int b = 1; b <= 4; b++)
                    {
                        if (Verify_LSRATEPR(data, Module2.LS_RateFiles[3], "P", sex, smoke, i, b, ref returnRate))
                            Module2.LSMort[i, j].Prin_Base[b - 1] = returnRate;
                        else
                        {
                            MsgText = "Mortality rate not found for Client (sex = " + sex + ", age = " + i.ToString() + ", smoker = " + smoke + " face amount band = " + b.ToString() + ".  Illustration NOT VALID!!";
                            Interaction.MsgBox(MsgText);
                            return;
                        }
                    }
                }
                if (Module2.LSClient[s].SubStd > 0)
                {
                    Tables = Module2.LSClient[s].SubStd / 4;
                    if (Verify_LSRATESB(data, Module2.LS_RateFiles[5], smoke, j, ref returnRate))
                    {
                        for (band = 0; band <= 3; band++)
                            Module2.LSMort[i, i].Prin_Sub[band] = Module2.LSMort[i, j].Prin_Base[band] * Tables * returnRate;
                    }
                    Module2.LSMort[i, j].Prin_CSO80 = 0;
                }
                else
                    Module2.LSMort[i, j].Prin_CSO80 = Verify_CSO80(data, Module2.LS_RateFiles[11], sex, smoke, j, ref returnRate) ? returnRate : 0;

                //Guaranteed Purchase
                if (Module2.LSClient[s].GPO != 0 && i < 41 && Verify_LSRATEGP(data, Module2.LS_RateFiles[6], j, ref returnRate))
                    Module2.LSMort[i, j].Prin_GPO = returnRate;

                //Waiver of Premium
                if (Module2.LSClient[s].WPD != 0 && i < 60 && Verify_LSRATEWP(data, Module2.LS_RateFiles[7], sex, j, ref returnRate))
                    Module2.LSMort[i, j].Prin_WPD = returnRate;

                //Term Rider on client
                if (Module2.LSClient[s].AddFace != 0)
                {
                    if (Module2.LSClient[s].IssueAge + Module2.LSClient[s].AddFaceStart - 1 <= j && Module2.LSClient[s].IssueAge + Module2.LSClient[s].AddFaceEnd >= j)
                    {
                        if (Verify_LSRATESI(data, Module2.LS_RateFiles[4], "I", sex, smoke, j, i + 1, ref returnRate))
                            Module2.LSMort[i, j].Insured_Base[0] = returnRate;
                        else
                        {
                            MsgText = "Mortality rate not found for Client Term Rider (age = " + Conversion.Str(j) + ", smoker = " + smoke + ".  Illustration NOT VALID!!";
                            Interaction.MsgBox(MsgText);
                            return;
                        }
                        if (Module2.LSClient[s].SubStd > 0)
                        {
                            Tables = Module2.LSClient[s].SubStd / 4;
                            if (Verify_LSRATESB(data, Module2.LS_RateFiles[5], smoke, j, ref returnRate))
                                Module2.LSMort[i, j].Insured_Sub[0] = Module2.LSMort[i, j].Insured_Base[0] * Tables * returnRate;
                            Module2.LSMort[i, j].Insured_CSO80[0] = 0;
                        }
                        else
                            Module2.LSMort[i, j].Insured_CSO80[0] = Verify_CSO80(data, Module2.LS_RateFiles[11], sex, smoke, j, ref returnRate) ? returnRate : 0;
                    }
                }
            }
        }

        //Spouse rider rates
        if (Module2.LSClient[s].SpouseRider != 0)
        {
            sex = Module2.LSClient[s].SexCode == "M" ? "F" : "M";
            smoke = Module2.LSSpouseRider.PlanCode == 1 ? "N" : "S";

            //Guaranteed or current
            for (i = 0; i <= 1; i++)
            {
                //All possible is
                for (j = 0; j <= MAXAGE; j++)
                {
                    if (Verify_LSRATESI(data, Module2.LS_RateFiles[4], "S", sex, smoke, i, i + 1, ref returnRate))
                        Module2.LSMort[i, i].Spouse_Base = returnRate;
                    else
                    {
                        MsgText = "Mortality rate not found for Spouse (age = " + Conversion.Str(i) + ", smoker = " + smoke + ".  Illustration NOT VALID!!";
                        Interaction.MsgBox(MsgText);
                        return;
                    }
                    if (Module2.LSSpouseRider.SubStd > 0)
                    {
                        Tables = Module2.LSSpouseRider.SubStd / 4;
                        if (Verify_LSRATESB(data, Module2.LS_RateFiles[5], smoke, i, ref returnRate))
                            Module2.LSMort[i, i].Spouse_Sub = Module2.LSMort[i, i].Spouse_Base * Tables * returnRate;
                        Module2.LSMort[i, i].Spouse_CSO80 = 0;
                    }
                    else
                        Module2.LSMort[i, i].Spouse_CSO80 = Verify_CSO80(data, Module2.LS_RateFiles[11], sex, smoke, i, ref returnRate) ? returnRate : 0;
                }
            }
        }

        for (k = 0; k < 9; k++)
        {
            //Addtl insd rider rates
            if (Module2.LSClient[s].InsuredRider[k] != 0)
            {
                sex = Module2.LSInsuredRider[k].SexCode;
                smoke = Module2.LSInsuredRider[k].PlanCode == 1 ? "N" : "S";

                //Guaranteed or current
                for (i = 0; i <= 1; i++)
                {
                    //All possible is
                    for (j = 0; j <= MAXAGE; j++)
                    {
                        if (Verify_LSRATESI(data, Module2.LS_RateFiles[4], "I", sex, smoke, i, i + 1, ref returnRate))
                            Module2.LSMort[i, j].Insured_Base[k] = returnRate;
                        else
                        {
                            MsgText = "Mortality rate not found for Insured (age = " + Conversion.Str(j) + ", smoker = " + smoke + ".  Illustration NOT VALID!!";
                            Interaction.MsgBox(MsgText);
                            return;
                        }
                        if (Module2.LSInsuredRider[k].SubStd > 0)
                        {
                            Tables = Module2.LSInsuredRider[k].SubStd / 4;
                            if (Verify_LSRATESB(data, Module2.LS_RateFiles[5], smoke, i, ref returnRate))
                                Module2.LSMort[i, i].Insured_Sub[k] = Module2.LSMort[i, i].Insured_Base[k] * Tables * returnRate;
                            Module2.LSMort[i, i].Insured_CSO80[k] = 0;
                        }
                        else
                            Module2.LSMort[i, i].Insured_CSO80[k] = Verify_CSO80(data, Module2.LS_RateFiles[11], sex, smoke, i, ref returnRate) ? returnRate : 0;
                    }
                }
            }
        }
    }

    public static void Insured_Rider_Mortality(int which, bool guideSingle) {
        int Insured_Age = 0;
        int Ledger_Year = 0;
        float[] Temp_Mort = new float[2];

        Insured_Age = Module2.AnnYr - Module2.LSClient[Module2.SpouseClient].IssueAge + Module2.LSInsuredRider[which].IssueAge;
        Ledger_Year = Insured_Age - Module2.LSInsuredRider[which].IssueAge + 1;
        if (Ledger_Year >= Module2.LSInsuredRider[which].StartYear && Ledger_Year <= Module2.LSInsuredRider[which].EndYear)
        {
            if (guideSingle)
            {
                Temp_Mort[0] = Module2.LSMort[0, Insured_Age].Insured_CSO80[which] < Module2.LSMort[0, Insured_Age].Insured_Base[which]
                    ? Module2.LSMort[0, Insured_Age].Insured_CSO80[which]
                    : Module2.LSMort[0, Insured_Age].Insured_Base[which];
                Temp_Mort[1] = Module2.LSMort[1, Insured_Age].Insured_CSO80[which] < Module2.LSMort[1, Insured_Age].Insured_Base[which]
                    ? Module2.LSMort[1, Insured_Age].Insured_CSO80[which]
                    : Module2.LSMort[1, Insured_Age].Insured_Base[which];
            }
            else
            {
                Temp_Mort[0] = Module2.LSMort[0, Insured_Age].Insured_Base[which];
                Temp_Mort[1] = Module2.LSMort[1, Insured_Age].Insured_Base[which];
            }

            if (Module2.LSInsuredRider[which].StartYear == Ledger_Year)
            {
                Module2.LS_Insureds_Mortcost = Module2.LS_Insureds_Mortcost + ((Module2.LSInsuredRider[which].FaceAmt * Temp_Mort[1]) / 1000);
                if (Module2.LSInsuredRider[which].SubStd > 0)
                    Module2.LS_Insureds_Mortcost = Module2.LS_Insureds_Mortcost + ((Module2.LSInsuredRider[which].FaceAmt * Module2.LSMort[1, Insured_Age].Insured_Sub[which]) / 1000);
            }
            else
            {
                Module2.LS_Insureds_Mortcost = Module2.LS_Insureds_Mortcost + ((Module2.LSInsuredRider[which].FaceAmt * Temp_Mort[Module2.GuarCurr]) / 1000);
                if (Module2.LSInsuredRider[which].SubStd > 0)
                    Module2.LS_Insureds_Mortcost = Module2.LS_Insureds_Mortcost + ((Module2.LSInsuredRider[which].FaceAmt * Module2.LSMort[Module2.GuarCurr, Insured_Age].Insured_Sub[which]) / 1000);
            }
        }

    }

    public static void LifeSave_Close_Text(ref string illpage, int section, float iRate, IllustrationInfo info, string companyName) {
        string OnePage = null;
        bool haveRiders = false;

        s = Module2.SpouseClient;
        NL = Environment.NewLine;
        //carriage return, line feed
        OnePage = "";

        if (Module2.LIFELINE > 28)
        {
            for (k = Module2.LIFELINE; k <= 59; k++)
            {
                OnePage = OnePage + NL;
            }
            OnePage = OnePage + NL + Strings.Space(15) + "THIS ILLUSTRATION IS NOT VALID WITHOUT ALL PAGES" + NL;
            illpage = illpage + OnePage;
            illpage = illpage + "------------------------------------------------------------------- End Page " + Strings.Format(Module2.LIFEPAGE, "#");
            Module2.PrintIllusPage[Module2.LIFEPAGE - 1] = Module2.PrintIllusPage[Module2.LIFEPAGE - 1] + OnePage;
            OnePage = "";
            Module2.LIFEPAGE = Module2.LIFEPAGE + 1;
            LifeSave_Illus_Head(ref OnePage, 0, companyName);
            illpage = illpage + OnePage;
            OnePage = "";
        }

        OnePage = OnePage + "Guideline Annual Premium: $" + Strings.Format(Module2.CalcPrem.GuideAnnual, "#,###,##0.00");
        if (Module2.CalcPrem.GuideSingleAddInsd > 0)
            OnePage = OnePage + "     Guideline Single Premium: $" + Strings.Format(Module2.CalcPrem.GuideSingleAddInsd, "##,###,##0.00") + NL;
        else
            OnePage = OnePage + "     Guideline Single Premium: $" + Strings.Format(Module2.CalcPrem.GuideSingle, "##,###,##0.00") + NL;
        Module2.LIFELINE = Module2.LIFELINE + 1;

        if (info.IllustrateInitialCashValue > 0)
        {
            OnePage = OnePage + NL + "This illustration begins at age ";
            OnePage = OnePage + info.IllustrateBeginAtAge.ToString("##");
            OnePage = OnePage + " with an accumulated cash value of $";
            OnePage = OnePage + info.IllustrateInitialCashValue.ToString("######0.00") + "." + NL;
            Module2.LIFELINE = Module2.LIFELINE + 2;
        }

        if (Module2.LSClient[s].SubStd > 0)
        {
            OnePage = OnePage + NL + "This illustration includes a monthly deduction for extra risk premium on the" + NL;
            OnePage = OnePage + "principal insured of " + Strings.Format(Module2.LSClient[s].SubStd / 0.04, "###") + "%";
            OnePage = OnePage + " for " + Strings.Format(Module2.LSClient[s].YearsToPay, "##") + " years." + NL;
            Module2.LIFELINE = Module2.LIFELINE + 3;
        }

        if (Module2.LSClient[s].FutureChanges != 0)
        {
            if (Module2.LSFuture[1].Int_Age > 0)
            {
                OnePage = OnePage + NL + "The projected values are based on the following interest rates:";
                OnePage = OnePage + NL + Strings.Space(21) + Strings.Format(Module2.LSClient[s].CurrRate, "##.000") + "% through age ";
                Module2.LIFELINE = Module2.LIFELINE + 2;
                for (i = 2; i <= 5; i++)
                {
                    OnePage = OnePage + Strings.Format(Module2.LSFuture[i - 1].Int_Age - 1, "#0") + NL;
                    OnePage = OnePage + Strings.Space(21) + Strings.Format(Module2.LSFuture[i - 1].Int_Amount, "##.000") + "% through age ";
                    if (i == 5 || Module2.LSFuture[i].Int_Age == 0)
                        OnePage = OnePage + Strings.Format(95, "#0") + NL;
                    if (Module2.LSFuture[i].Int_Age == 0)
                        i = 5;
                    Module2.LIFELINE = Module2.LIFELINE + 2;
                }
            }
            if (Module2.LSFuture[1].Opt_Age > 0)
            {
                OnePage = OnePage + NL + "Change in Death Benefit Option to Option " + Module2.LSFuture[1].Opt_Type;
                OnePage = OnePage + " at age " + Strings.Format(Module2.LSFuture[1].Opt_Age, "#0") + NL;
                Module2.LIFELINE = Module2.LIFELINE + 1;
                for (i = 2; i <= 5; i++)
                {
                    if (Module2.LSFuture[i].Opt_Age > 0)
                    {
                        OnePage = OnePage + NL + "Change in Death Benefit Option to Option " + Module2.LSFuture[i].Opt_Type;
                        OnePage = OnePage + " at age " + Strings.Format(Module2.LSFuture[i].Opt_Age, "#0") + NL;
                        Module2.LIFELINE = Module2.LIFELINE + 1;
                    }
                    if (Module2.LSFuture[i].Opt_Age == 0)
                        i = 5;
                }
            }
        }

        OnePage = OnePage + NL + "This illustration includes the following riders:";
        OnePage = OnePage + NL;
        Module2.LIFELINE = Module2.LIFELINE + 2;
        haveRiders = false;
        if (Module2.LSClient[s].GPO != 0)
        {
            haveRiders = true;
            OnePage = OnePage + NL + "  Guaranteed Added Death Benefit of $" + Strings.Format(Module2.LSClient[s].GPO, "#####");
            OnePage = OnePage + " to age 41";
            Module2.LIFELINE = Module2.LIFELINE + 1;
        }
        if (Module2.LSClient[s].WPD != 0)
        {
            haveRiders = true;
            OnePage = OnePage + NL + "  Disability Waiver of Cost to age 60";
            Module2.LIFELINE = Module2.LIFELINE + 1;
        }
        if (Module2.LSClient[s].COLA != 0)
        {
            haveRiders = true;
            OnePage = OnePage + NL + "  Cost of Living Increase Illustrated for Principal Insured to age 65 at " + Strings.Format(Module2.LSClient[s].COLA, "##") + "%";
            Module2.LIFELINE = Module2.LIFELINE + 1;
        }
        if (Module2.LSClient[s].AddFace != 0)
        {
            haveRiders = true;
            OnePage = OnePage + NL + "  Additional Death Benefit for Principal Insured of $" + Strings.Format(Module2.LSClient[s].AddFace, "#######");
            OnePage = OnePage + " (Ages " + Strings.Format(Module2.LSClient[s].AddFaceStart + Module2.LSClient[s].IssueAge - 1, "##");
            OnePage = OnePage + " to " + Strings.Format(Module2.LSClient[s].AddFaceEnd + Module2.LSClient[s].IssueAge, "##") + ")";
            Module2.LIFELINE = Module2.LIFELINE + 1;
        }
        if (Module2.LSClient[s].SpouseRider != 0)
        {
            haveRiders = true;
            OnePage = OnePage + NL + "  Spouse Term Life $" + Strings.Format(Module2.LSSpouseRider.FaceAmt, "#####");
            OnePage = OnePage + " to age " + Strings.Format(Module2.LSSpouseRider.IssueAge + Module2.LSSpouseRider.RemoveYear, "##");
            if (Module2.LSClient[s].SexCode == "M")
                OnePage = OnePage + " (Female age " + Strings.Format(Module2.LSSpouseRider.IssueAge, "##");
            else
                OnePage = OnePage + " (Male age " + Strings.Format(Module2.LSSpouseRider.IssueAge, "##");
            if (Module2.LSSpouseRider.PlanCode == 1)
                OnePage = OnePage + " NonSmoker)";
            else
                OnePage = OnePage + " Smoker)";
            if (Module2.LSSpouseRider.SubStd != 0)
                OnePage = OnePage + "(Substandard " + Strings.Format(Module2.LSSpouseRider.SubStd / 0.04, "###") + "%)";
            Module2.LIFELINE = Module2.LIFELINE + 1;
        }
        if (Module2.LSClient[s].ChildRider != 0)
        {
            haveRiders = true;
            OnePage = OnePage + NL + "  Children's Term Life $" + Strings.Format(Module2.LSChildRider.FaceAmt, "#####");
            OnePage = OnePage + " for " + Strings.Format(23 - Module2.LSChildRider.AgeYoungest, "##") + " years";
            Module2.LIFELINE = Module2.LIFELINE + 1;
        }
        for (i = 0; i < 9; i++)
        {
            if (Module2.LSClient[s].InsuredRider[i] != 0)
            {
                haveRiders = true;
                OnePage = OnePage + NL + "  Additional Insured Term Life $" + Strings.Format(Module2.LSInsuredRider[i].FaceAmt, "#####");
                OnePage = OnePage + " in years " + Strings.Format(Module2.LSInsuredRider[i].StartYear, "##") + " to " + Strings.Format(Module2.LSInsuredRider[i].EndYear, "##") + NL;
                if (Module2.LSInsuredRider[i].SexCode == "M")
                    OnePage = OnePage + "    (Male age " + Strings.Format(Module2.LSInsuredRider[i].IssueAge, "##");
                else
                    OnePage = OnePage + "    (Female age " + Strings.Format(Module2.LSInsuredRider[i].IssueAge, "##");
                if (Module2.LSInsuredRider[i].PlanCode == 1)
                    OnePage = OnePage + " NonSmoker)";
                else
                    OnePage = OnePage + " Smoker)";
                if (Module2.LSInsuredRider[i].SubStd != 0)
                    OnePage = OnePage + "(Substandard " + Strings.Format(Module2.LSInsuredRider[i].SubStd / 0.04, "###") + "%)";
                Module2.LIFELINE = Module2.LIFELINE + 2;
            }
        }
        if (!haveRiders)
        {
            OnePage = OnePage + NL + "   * * * N O N E * * * ";
            Module2.LIFELINE = Module2.LIFELINE + 1;
        }
        illpage = illpage + OnePage;
        Module2.PrintIllusPage[Module2.LIFEPAGE - 1] = Module2.PrintIllusPage[Module2.LIFEPAGE - 1] + OnePage;
        OnePage = "";

        Print_Cost_Indices(OnePage);
        Module2.LIFELINE = Module2.LIFELINE + 10;
        illpage = illpage + OnePage;
        Module2.PrintIllusPage[Module2.LIFEPAGE - 1] = Module2.PrintIllusPage[Module2.LIFEPAGE - 1] + OnePage;
        OnePage = "";

        if (Module2.LIFELINE > 42)
        {
            for (k = Module2.LIFELINE; k <= 59; k++)
            {
                OnePage = OnePage + NL;
            }
            OnePage = OnePage + NL + Strings.Space(15) + "THIS ILLUSTRATION IS NOT VALID WITHOUT ALL PAGES" + NL;
            illpage = illpage + OnePage;
            illpage = illpage + "------------------------------------------------------------------- End Page " + Strings.Format(Module2.LIFEPAGE, "#");
            Module2.PrintIllusPage[Module2.LIFEPAGE - 1] = Module2.PrintIllusPage[Module2.LIFEPAGE - 1] + OnePage;
            OnePage = "";
            Module2.LIFEPAGE = Module2.LIFEPAGE + 1;
            LifeSave_Illus_Head(ref OnePage, 0, companyName);
            illpage = illpage + OnePage;
            OnePage = "";
        }

        illpage = illpage + OnePage;
        Module2.PrintIllusPage[Module2.LIFEPAGE - 1] = Module2.PrintIllusPage[Module2.LIFEPAGE - 1] + OnePage;
        OnePage = "";
        OnePage = NL + "Control Code:" + NL;
        OnePage = OnePage + "T" + Strings.Format(Module2.CalcPrem.TotTarg, "####0.00");
        if (Module2.LSClient[Module2.SpouseClient].PlannedPrem + Module2.LSClient[Module2.SpouseClient].LumpSum > Module2.CalcPrem.TotTarg)
            OnePage = OnePage + "E" + Strings.Format(Module2.LSClient[Module2.SpouseClient].PlannedPrem + Module2.LSClient[Module2.SpouseClient].LumpSum - Module2.CalcPrem.TotTarg, "####0.00");
        else
            OnePage = OnePage + "E0.00";
        OnePage = OnePage + "M" + Strings.Format(Module2.CalcPrem.TotMin, "####0.00");
        OnePage = OnePage + Module2.LSClient[Module2.SpouseClient].Mode;

        for (k = Module2.LIFELINE; k <= 59; k++)
        {
            OnePage = OnePage + NL;
        }
        OnePage = OnePage + NL + Strings.Space(29) + "END OF ILLUSTRATION" + NL;

        illpage = illpage + OnePage;
        illpage = illpage + "------------------------------------------------------------------- End Page " + Strings.Format(Module2.LIFEPAGE, "#");
        Module2.PrintIllusPage[Module2.LIFEPAGE - 1] = Module2.PrintIllusPage[Module2.LIFEPAGE - 1] + OnePage;
    }

    public static void LifeSave_Illus_Head(ref string illpage, int colHead, string companyName) {
        string OneLine = null;

        NL = Environment.NewLine;
        //carriage return, line feed

        Module2.PrintIllusPage[Module2.LIFEPAGE - 1] = "";
        s = Module2.SpouseClient;
        illpage = illpage + NL;
        OneLine = Strings.Format(DateTime.Now, "dddd");
        //OneLine = OneLine && Space$(16 - Len(OneLine)) && "*** SAMPLE ***   LIFE SAVINGS   *** SAMPLE ***" && Space$(10) && "Page " && Format$(LIFEPAGE, "#") && NL
        OneLine = OneLine + Strings.Space(33 - Strings.Len(OneLine)) + "LIFE SAVINGS" + Strings.Space(27) + "Page " + Strings.Format(Module2.LIFEPAGE, "#") + NL;
        illpage = illpage + OneLine;
        OneLine = Strings.Format(DateTime.Now, "mm/dd/yyyy") + Strings.Space(12) + companyName + NL;
        illpage = illpage + OneLine;
        OneLine = Strings.Format(DateTime.Now, "hh:mm AM/PM") + Strings.Space(11) + "(A-2670) Life Insurance Policy Illustration" + NL;
        illpage = illpage + OneLine;
        OneLine = Strings.Space(33) + "Prepared For:" + NL;
        illpage = illpage + OneLine + NL + NL;
        OneLine = Strings.Space(39 - Strings.Len(Strings.Trim(Module2.LSClient[s].ClientName)) / 2) + Strings.Trim(Module2.LSClient[s].ClientName) + NL + NL;
        OneLine = OneLine + "Illustrating:" + Strings.Space(40) + "Provided by:" + NL;
        illpage = illpage + OneLine;
        OneLine = Strings.Space(1) + "Option ";
        if (Module2.LSClient[s].OptionX == 1)
            OneLine = OneLine + "1 (Level)     ";
        else
            OneLine = OneLine + "2 (Increasing)";
        if (Module2.LSClient[s].SexCode == "M")
            OneLine = OneLine + "      Male  ";
        else
            OneLine = OneLine + "      Female";
        OneLine = OneLine + Strings.Space(8) + "Age" + Conversion.Str(Module2.LSClient[s].IssueAge) + Strings.Space(5);
        OneLine = OneLine + Strings.Trim(Module2.LSClient[s].ContactName) + NL;
        if (Module2.LSClient[s].PlanCode == 1)
            OneLine = OneLine + " Non-Smoker ";
        else
            OneLine = OneLine + " Smoker     ";
        OneLine = OneLine + Strings.Space(41) + Strings.Trim(Module2.LSClient[s].ContactPhone) + NL + NL;
        OneLine = OneLine + "Initial Death Benefit:" + Strings.Space(31) + "Premium Mode:" + NL;
        illpage = illpage + OneLine;
        OneLine = Strings.Space(1) + Strings.Format(Module2.LSClient[s].FaceAmt, "$#,#00,000");
        OneLine = OneLine + Strings.Space(53 - Strings.Len(OneLine));
        switch ((Module2.LSClient[s].Mode))
        {
            case "A":
                OneLine = OneLine + "Annual     " + NL;
                break;
            case "Q":
                OneLine = OneLine + "Quarterly  " + NL;
                break;
            case "S":
                OneLine = OneLine + "Semi-Annual" + NL;
                break;
            default:
                OneLine = OneLine + "Monthly APP" + NL;
                break;
        }

        illpage = illpage + OneLine + NL + NL;
        if (colHead != 0)
        {
            OneLine = Strings.Space(24) + "Guaranteed Values" + Strings.Space(16) + "Projected Values" + NL;
            OneLine = OneLine + Strings.Space(24) + "-----------------" + Strings.Space(16) + "----------------" + NL;
            OneLine = OneLine + Strings.Space(4) + "End              Cash" + Strings.Space(28) + "Cash" + NL;
            OneLine = OneLine + Strings.Space(4) + " of  Annual   Surrender    Cash     Death     Surrender    Cash     Death" + NL;
            OneLine = OneLine + "Age Year Outlay     Value      Value   Benefit       Value     Value   Benefit" + NL;
            OneLine = OneLine + "--- ---- ------  -----------------------------   -----------------------------" + NL;
            illpage = illpage + OneLine;
            Module2.LIFELINE = 23;
        }
        else
            Module2.LIFELINE = 18;
        Module2.PrintIllusPage[Module2.LIFEPAGE - 1] = illpage;
    }

    public static void LifeSave_Ledger(ref string Illpage, IllustrationInfo info, string companyName) {
        int StartAge = 0;
        int IllAge = 0;
        int ShowIll = 0;
        string comma = null;
        int AmtLen = 0;
        string AmtStr = null;
        double AmtVal = 0;
        string OnePage = null;
        string Age_EndYear = null;
        string Age_EndYear_Plus1 = null;

        NL = Environment.NewLine;
        //carriage return, line feed
        OnePage = "";
        s = Module2.SpouseClient;
        if (info.IllustrateBeginAtAge == 0)
            StartAge = Module2.LSClient[s].IssueAge;
        else
            StartAge = info.IllustrateBeginAtAge;
        if (Module2.LSClient[s].IllusType.IsBetween(100, 195, true))
        {
            ShowIll = 1;
            IllAge = Module2.LSClient[s].IllusType - 100;
        }
        else if (Module2.LSClient[s].IllusType.IsBetween(200, 295, true))
        {
            ShowIll = 2;
            IllAge = Module2.LSClient[s].IllusType - 200;
        }
        else if (Module2.LSClient[s].IllusType == 299)
        {
            ShowIll = 4;
            IllAge = 95;
        }
        else if (Module2.LSClient[s].IllusType > 299)
        {
            ShowIll = 3;
            IllAge = Module2.LSClient[s].IllusType - 300;
        }
        i = 1;
        //Years Illustrated
        comma = Module2.LSLedger[StartAge].DeathBenefit[1] > 9999999 || Module2.LSLedger[IllAge - 1].CashValue[1] > 9999999 ? "Y" : "N";

        year = 0;
        for (year = StartAge + 1; year < IllAge; year++)
        {
            if (Module2.LIFELINE > 57)
                Page_Break(ref Illpage, OnePage, IllAge - 1, companyName);
            //if (Module2.LSLedger[year].CashValue[1] == 0 && year > StartAge + 3)
            //{
            //    QuitLedger(ref Illpage, OnePage);
            //    break;
            //}

            if (Check_Year(ShowIll, i))
            {
                if (year < 10)
                {
                    Age_EndYear = Strings.Space(1) + Strings.Format(year, "#") + Strings.Space(3);
                    Age_EndYear_Plus1 = year < 9
                        ? Strings.Space(1) + Strings.Format(year + 1, "#") + Strings.Space(3)
                        : Strings.Format(year + 1, "##") + Strings.Space(3);
                }
                else
                {
                    Age_EndYear = Strings.Format(year, "##") + Strings.Space(3);
                    Age_EndYear_Plus1 = Strings.Format(year + 1, "##") + Strings.Space(3);
                }
                if (year - StartAge < 10)
                {
                    Age_EndYear += Strings.Space(1) + Strings.Format(year - StartAge, "#");
                    Age_EndYear_Plus1 = year - StartAge < 9
                        ? Age_EndYear_Plus1 + Strings.Space(1) + Strings.Format(year + 1 - StartAge, "#")
                        : Age_EndYear_Plus1 + Strings.Format(year + 1 - StartAge, "##");
                }
                else
                {
                    Age_EndYear += Strings.Format(year - StartAge, "##");
                    Age_EndYear_Plus1 += Strings.Format(year + 1 - StartAge, "##");
                }
                OnePage = OnePage + Age_EndYear;
                AmtLen = 8;
                AmtVal = Module2.LSLedger[year].AnnualOutlay;
                Format_Ledger_Amount(ref AmtStr, AmtLen, AmtVal, "N", 0);
                OnePage = OnePage + AmtStr + Strings.Space(1);
                for (k = 0; k <= 1; k++)
                {
                    AmtLen = 10;
                    AmtVal = Module2.LSLedger[year].SurrenderValue[k];
                    Format_Ledger_Amount(ref AmtStr, AmtLen, AmtVal, comma, 0);
                    OnePage = OnePage + AmtStr;

                    AmtVal = Module2.LSLedger[year].CashValue[k];
                    Format_Ledger_Amount(ref AmtStr, AmtLen, AmtVal, comma, 0);
                    OnePage = OnePage + AmtStr;

                    AmtVal = Module2.LSLedger[year].DeathBenefit[k];
                    Format_Ledger_Amount(ref AmtStr, AmtLen, AmtVal, comma, 0);
                    OnePage = OnePage + AmtStr + Strings.Space(2);
                }
                OnePage = OnePage + NL;
                Module2.LIFELINE = Module2.LIFELINE + 1;

                if (i > 1 && i % 5 == 0)
                {
                    OnePage = OnePage + NL;
                    Module2.LIFELINE = Module2.LIFELINE + 1;
                }

                if (Module2.LIFELINE > 57)
                    Page_Break(ref Illpage, OnePage, IllAge - 1, companyName);

                if (Module2.LSLedger[year].WithdrawAmount > 0)
                {
                    OnePage = OnePage + Age_EndYear_Plus1;
                    AmtLen = 7;
                    AmtVal = Module2.LSLedger[year].WithdrawAmount;
                    Format_Ledger_Amount(ref AmtStr, AmtLen, AmtVal, "N", 1);
                    OnePage = OnePage + AmtStr + "  Withdrawal" + NL;
                    Module2.LIFELINE = Module2.LIFELINE + 1;
                }
                if (Module2.LSLedger[year].LoanAmount > 0 || Module2.LSLedger[year].LoanRepayAmount > 0)
                {
                    OnePage = OnePage + Age_EndYear_Plus1;
                    AmtLen = 7;
                    if (Module2.LSLedger[year].LoanAmount > Module2.LSLedger[year].LoanRepayAmount)
                    {
                        AmtVal = Module2.LSLedger[year].LoanAmount - Module2.LSLedger[year].LoanRepayAmount;
                        Format_Ledger_Amount(ref AmtStr, AmtLen, AmtVal, "N", 1);
                        OnePage = OnePage + AmtStr + "  Loan";
                    }
                    else
                    {
                        AmtVal = Module2.LSLedger[year].LoanRepayAmount - Module2.LSLedger[year].LoanAmount;
                        Format_Ledger_Amount(ref AmtStr, AmtLen, AmtVal, "N", 0);
                        OnePage = OnePage + AmtStr + "  Repayment of Loan";
                    }
                    OnePage = OnePage + NL;
                    Module2.LIFELINE = Module2.LIFELINE + 1;
                }

                i = i + 1;
            }
        }
    }
    private static void QuitLedger(ref string Illpage, string OnePage) {
        if (Module2.LIFELINE > 23)
        {
            Illpage = Illpage + OnePage;
            Module2.PrintIllusPage[Module2.LIFEPAGE - 1] = Module2.PrintIllusPage[Module2.LIFEPAGE - 1] + OnePage;
        }
    }

    private static bool Check_Year(int ShowIll, int itemIndex) {
        var ShowYear = true;
        if (ShowIll == 1 || ShowIll == 2)
            return ShowYear;
        if (ShowIll == 3 && (itemIndex < 11 || (year > 11 && year % 5 == 0)))
            return ShowYear;
        if (ShowIll == 4 && itemIndex < 26)
            return ShowYear;
        if (ShowIll == 4 && (year == Module2.LSClient[s].HighAges[0] || year == Module2.LSClient[s].HighAges[1] || year == Module2.LSClient[s].HighAges[2] || year == Module2.LSClient[s].HighAges[3] || year == Module2.LSClient[s].HighAges[4]))
            return ShowYear;
        ShowYear = false;
        return ShowYear;
    }

    private static void Page_Break(ref string Illpage, string OnePage, int IllAge, string companyName) {
        for (k = Module2.LIFELINE; k <= 59; k++)
        {
            OnePage = OnePage + NL;
        }
        OnePage = OnePage + NL + Strings.Space(15) + "THIS ILLUSTRATION IS NOT VALID WITHOUT ALL PAGES" + NL;
        Illpage = Illpage + OnePage;
        Illpage = Illpage + "------------------------------------------------------------------- End Page " + Strings.Format(Module2.LIFEPAGE, "#");
        Module2.PrintIllusPage[Module2.LIFEPAGE - 1] = Module2.PrintIllusPage[Module2.LIFEPAGE - 1] + OnePage;
        Module2.LIFEPAGE = Module2.LIFEPAGE + 1;
        OnePage = "";
        if (year < IllAge)
        {
            LifeSave_Illus_Head(ref OnePage, 1, companyName);
        }
        else
        {
            LifeSave_Illus_Head(ref OnePage, 0, companyName);
        }
        Illpage = Illpage + OnePage;
        OnePage = "";
    }


    public static void Load_Agent() {
        int FileNum = 0;
        long TempAgent = 0;
        Module2.AgtData RandomAgent = default(Module2.AgtData);

        FileNum = FileSystem.FreeFile();
        TempAgent = Module2.AgtInfo.Record;

        //    ' Open "PROFILE.DAT" For Random As FileNum Len = Len(RandomAgent)
        //' Get FileNum, TempAgent, RandomAgent
        //AgtInfo.FullName = RandomAgent.FullName
        //    AgtInfo.OfficePhone = RandomAgent.OfficePhone
        //    Close FileNum

    }


    public static void Load_Client() {
        int FileNum = 0;
        int TempNumber = 0;
        string DataSetNumber = null;

        FileNum = FileSystem.FreeFile();
        DataSetNumber = Conversion.Str(Module2.AgtInfo.Record);
        Module2.ClientFile = "clnt" + Strings.Trim(Strings.Right(DataSetNumber, 3)) + ".dat";
        Module2.InsuredFile = "insu" + Strings.Trim(Strings.Right(DataSetNumber, 3)) + ".dat";

        if (Module2.ClientInfo.ClientNumber > 0)
        {
            TempNumber = Module2.ClientInfo.ClientNumber;
            //     ' Open ClientFile For Random As FileNum Len = Len(Module2.ClientInfo)
            //' Get FileNum, TempNumber, Module2.ClientInfo
            //Close FileNum
        }

    }

    /*
        public static void Load_Future_Changes() {
            Module2.LSClient[Module2.SpouseClient].FutureChanges = 0;
            Module2.LSClient[Module2.SpouseClient].FutureWD = 0;

            for (i = 0; i <= 4; i++)
            {
                if (Module2.LSFuture[i].DB_Age > 0)
                {
                    //frmLSCHANGE.txtChangeFaceAge(i - 1).Text = Strings.Format(Module2.LSFuture[i].DB_Age, "##");
                    //frmLSCHANGE.txtChangeFace(i - 1).Text = Strings.Format(Module2.LSFuture[i].DB_Amount, "####00000");
                    //frmLSCHANGE.chkChangeFace.Checked = 1;
                }
                if (Module2.LSFuture[i].Prem_Age > 0)
                {
                    //frmLSCHANGE.txtChangePremAge(i - 1).Text = Strings.Format(Module2.LSFuture[i].Prem_Age, "##");
                    //frmLSCHANGE.txtChangePrem(i - 1).Text = Strings.Format(Module2.LSFuture[i].Prem_Amount, "#####0.00");
                    //frmLSCHANGE.chkChangePrem.Checked = 1;
                }
                if (Module2.LSFuture[i].Int_Age > 0)
                {
                    //frmLSCHANGE.txtChangeCurrIntAge(i - 1).Text = Strings.Format(Module2.LSFuture[i].Int_Age, "##");
                    //frmLSCHANGE.txtChangeCurrInt(i - 1).Text = Strings.Format(Module2.LSFuture[i].Int_Amount, "00.000");
                    //frmLSCHANGE.chkChangeCurrInt.Checked = 1;
                }
                if (Module2.LSFuture[i].Opt_Age > 0)
                {
                    //frmLSCHANGE.txtChangeDBOptAge(i - 1).Text = Strings.Format(Module2.LSFuture[i].Opt_Age, "##");
                    //frmLSCHANGE.txtChangeDBOpt(i - 1).Text = Strings.Format(Module2.LSFuture[i].Opt_Type, "#");
                    //frmLSCHANGE.chkChangeDBOpt.Checked = 1;
                }
                if (Module2.LSFutureWD[i].WD_Age > 0)
                {
                    //frmLSCHANGE.txtWithdrawStart(i - 1).Text = Strings.Format(Module2.LSFutureWD[i].WD_Age, "##");
                    //frmLSCHANGE.txtWithdraw(i - 1).Text = Strings.Format(Module2.LSFutureWD[i].WD_Amount, "###000.##");
                    //frmLSCHANGE.txtWithdrawEnd(i - 1).Text = Strings.Format(Module2.LSFutureWD[i].WD_Age + Module2.LSFutureWD[i).WD_Years - 1, "##");
                    //frmLSCHANGE.chkWithdraw.Checked = 1;
                }
                if (Module2.LSFutureWD[i].Loan_Age > 0)
                {
                    //frmLSCHANGE.txtLoanStart(i - 1).Text = Strings.Format(Module2.LSFutureWD[i].Loan_Age, "##");
                    //frmLSCHANGE.txtLoan(i - 1).Text = Strings.Format(Module2.LSFutureWD[i].Loan_Amount, "###000.##");
                    //frmLSCHANGE.txtLoanEnd(i - 1).Text = Strings.Format(Module2.LSFutureWD[i].Loan_Age + Module2.LSFutureWD[i).Loan_Years - 1, "##");
                    //frmLSCHANGE.chkLoan.Checked = 1;
                }
                if (Module2.LSFutureWD[i].LoanPay_Age > 0)
                {
                    //frmLSCHANGE.txtLoanRepayStart(i - 1).Text = Strings.Format(Module2.LSFutureWD[i].LoanPay_Age, "##");
                    //frmLSCHANGE.txtLoanRepay(i - 1).Text = Strings.Format(Module2.LSFutureWD[i].LoanPay_Amount, "###000.##");
                    //frmLSCHANGE.txtLoanRepayEnd(i - 1).Text = Strings.Format(Module2.LSFutureWD[i].LoanPay_Age + Module2.LSFutureWD[i).LoanPay_Years - 1, "##");
                    //frmLSCHANGE.chkLoanRepay.Checked = 1;
                }
            }

        }
        */
    /*
    public static void Load_Insureds() {
        s = Module2.SpouseClient;

        //if (Module2.LSClient[s].ChildRider == true)
        //{
        //    frmLSINSD.txtChildAge.Text = Strings.Format(Module2.LSChildRider.AgeYoungest, "##");
        //    switch ((Module2.LSChildRider.FaceAmt))
        //    {
        //        case 2500:
        //            frmLSINSD.cboChildFace.SelectedIndex = 3;
        //            break;
        //        case 5000:
        //            frmLSINSD.cboChildFace.SelectedIndex = 2;
        //            break;
        //        case 7500:
        //            frmLSINSD.cboChildFace.SelectedIndex = 1;
        //            break;
        //        default:
        //            frmLSINSD.cboChildFace.SelectedIndex = 0;
        //            break;
        //    }
        //    frmLSINSD.chkChild.Checked = 1;
        //}
        //else
        //{
        //    frmLSINSD.chkChild.Checked = 0;
        //}

        //if (Module2.LSClient[s].SpouseRider == true)
        //{
        //    frmLSINSD.txtSpouseAge.Text = Strings.Format(Module2.LSSpouseRider.IssueAge, "##");
        //    switch ((Module2.LSSpouseRider.FaceAmt))
        //    {
        //        case 25000:
        //            frmLSINSD.cboSpouseFace.SelectedIndex = 1;
        //            break;
        //        default:
        //            frmLSINSD.cboSpouseFace.SelectedIndex = 0;
        //            break;
        //    }
        //    switch ((Module2.LSSpouseRider.PlanCode))
        //    {
        //        case 2:
        //            frmLSINSD.cboSpousePlan.SelectedIndex = 1;
        //            break;
        //        default:
        //            frmLSINSD.cboSpousePlan.SelectedIndex = 0;
        //            break;
        //    }
        //    switch (Module2.LSSpouseRider.SubStd)
        //    {
        //        case 0:
        //        case 1:
        //        case 2:
        //        case 3:
        //        case 4:
        //        case 5:
        //        case 6:
        //        case 7:
        //        case 8:
        //        case 9:
        //        case 10:
        //            frmLSINSD.cboSpouseSubStd.SelectedIndex = Module2.LSSpouseRider.SubStd;
        //            break;
        //        default:
        //            frmLSINSD.cboSpouseSubStd.SelectedIndex = 0;
        //            break;
        //    }
        //    frmLSINSD.cboSpouseCOLA.SelectedIndex = Module2.LSClient[Module2.SpouseClient).COLA;
        //    frmLSINSD.cboSpouseYearsAge.SelectedIndex = 0;
        //    frmLSINSD.txtSpouseYears.Text = Strings.Format(Module2.LSSpouseRider.RemoveYear, "##");
        //    frmLSINSD.chkSpouse.Checked = 1;
        //}
        //else
        //{
        //    frmLSINSD.chkSpouse.Checked = 0;
        //}

        for (i = 1; i <= 9; i++)
        {
            if (Module2.LSClient[s].InsuredRider[i] != 0)
            {
                //frmLSINSD.txtAddInsdAge(i - 1).Text = Strings.Format(Module2.LSInsuredRider[i).IssueAge, "##");
                //frmLSINSD.txtAddInsdSex(i - 1).Text = Module2.LSInsuredRider[i).SexCode;
                //frmLSINSD.txtAddInsdFace(i - 1).Text = Strings.Format(Module2.LSInsuredRider[i).FaceAmt, "#######");
                //switch ((Module2.LSInsuredRider[i).PlanCode))
                //{
                //    case 2:
                //        frmLSINSD.cboAddInsdPlan(i - 1).SelectedIndex = 1;
                //        break;
                //    default:
                //        frmLSINSD.cboAddInsdPlan(i - 1).SelectedIndex = 0;
                //        break;
                //}
                //switch (Module2.LSInsuredRider[i).SubStd)
                //{
                //    case 0:
                //    case 1:
                //    case 2:
                //    case 3:
                //    case 4:
                //    case 5:
                //    case 6:
                //    case 7:
                //    case 8:
                //    case 9:
                //    case 10:
                //        frmLSINSD.cboAddInsdSubStd(i - 1).SelectedIndex = Module2.LSInsuredRider[i).SubStd;
                //        break;
                //    default:
                //        frmLSINSD.cboAddInsdSubStd(i - 1).SelectedIndex = 0;
                //        break;
                //}
                //frmLSINSD.cboAddInsdCOLA(i - 1).SelectedIndex = Module2.LSClient[Module2.SpouseClient).COLA;
                //frmLSINSD.cboAddInsdStartYearsAge(i - 1).SelectedIndex = 0;
                //frmLSINSD.txtAddInsdYearStart(i - 1).Text = Strings.Format(Module2.LSInsuredRider[i).StartYear, "##");
                //frmLSINSD.cboAddInsdEndYearsAge(i - 1).SelectedIndex = 0;
                //frmLSINSD.txtAddInsdYearEnd(i - 1).Text = Strings.Format(Module2.LSInsuredRider[i).EndYear, "##");
                //frmLSINSD.chkAddInsd(i - 1).Checked = 1;
            }
            else
            {
                //frmLSINSD.chkAddInsd(i - 1).Checked = 0;
            }
        }

    }
    */
    /*
    public static void Load_LS_From_Client() {
        int RetCd = 0;
        long SerialBDt = 0;
        string TempDate = null;
        int CalcYear = 0;
        long YMDBdate = 0;

        //if (AgtInfo.Record != 0)
        //{
        //    if (CallFromMain != "NO")
        //        Load_Agent();
        //    frmLIFESV.txtPreparedBy.Text = AgtInfo.FullName;
        //    if (Conversion.Val(AgtInfo.OfficePhone) > 0)
        //    {
        //        frmLIFESV.txtPhoneContact.Text = AgtInfo.OfficePhone + " Ext(_____)";
        //    }
        //    else
        //    {
        //        frmLIFESV.txtPhoneContact.Text = "(___) ___-____ Ext(_____)";
        //    }
        //    Load_Client();
        //}
        //else
        //{
        //    frmLIFESV.txtPreparedBy.Text = "";
        //    frmLIFESV.txtPhoneContact.Text = "(___) ___-____ Ext(_____)";
        //}

        if (Module2.ClientInfo.ClientNumber > 0)
        {
            //frmLIFESV.txtClientFirstName.Text = Module2.ClientInfo.FirstName;
            //frmLIFESV.txtClientMidInit.Text = Module2.ClientInfo.Initial;
            //frmLIFESV.txtClientLastName.Text = Module2.ClientInfo.LastName;
            //frmLIFESV.txtClientState.Text = Module2.ClientInfo.State;
            //frmLIFESV.txtClientSex.Text = Module2.ClientInfo.Gender;
            RetCd = Module2.insured_file(1, Module2.ClientInfo.ClientNumber, 0);
            if (RetCd != 0)
            {
                //switch ((Module2.InsuredInfo.insTobacco))
                //{
                //    case "Y":
                //    case "C":
                //    case "P":
                //    case "S":
                //        frmLIFESV.cboClientPlan.SelectedIndex = 1;
                //        break;
                //    default:
                //        frmLIFESV.cboClientPlan.SelectedIndex = 0;
                //        break;
                //}
            }
            else
            {
                //frmLIFESV.cboClientPlan.SelectedIndex = 0;
            }
            today = DateTime.Now.Ticks;
            if (Module2.ClientInfo.BirthDate != 0)
            {
                today = DateTime.Now.Ticks;
                SerialBDt = Module2.ClientInfo.BirthDate;
                Module2.Year_Calc(SerialBDt, today, CalcYear, YMDBdate);
                //frmLIFESV.txtClientAge.Text = Strings.Trim(Conversion.Str(CalcYear));
                TempDate = new DateTime(SerialBDt).ToShortDateString();
                //TempDate = Strings.Format(Month(new DateTime(SerialBDt)), "00") + "/" + Strings.Format(new DateTime(SerialBDt).Day, "00") + "/" + Strings.Format(Strings.Right(Conversion.Str(new DateTime(SerialBDt).Year), 4), "0000");
                //frmLIFESV.txtClientBirth.Text = TempDate;
            }
            else
            {
                //frmLIFESV.txtClientAge.Text = "  ";
                //frmLIFESV.txtClientBirth.Text = "__/__/____";
            }
            //frmLIFESV.txtClientFace.Text = "_______";
        }

        Module2.msgfile = "\\life\\LIFEHLP.MSG";
        Module2.ClientChanged = 0;

    }
    */
    /*
    public static void Load_Spouse_As_Client() {
        string Spouse_FirstName = null;
        string Spouse_MidInit = null;
        string Spouse_LastName = null;
        int RetCd = 0;
        long SerialBDt = 0;
        string TempDate = null;
        int CalcYear = 0;
        long YMDBdate = 0;

        if (Strings.Left(Module2.LSClient[1].ClientName, 1) != " ")
        {
            Parse_Spouse_Name(Spouse_FirstName, Spouse_MidInit, Spouse_LastName);
            //frmLSSPOUSE.txtSpouseFirstName.Text = Spouse_FirstName;
            //frmLSSPOUSE.txtSpouseMidInit.Text = Spouse_MidInit;
            //frmLSSPOUSE.txtSpouseLastName.Text = Spouse_LastName;
            //frmLSSPOUSE.txtSpouseAge.Text = Strings.Trim(Conversion.Str(Module2.LSClient[1).IssueAge));
            //frmLSSPOUSE.txtSpouseSex.Text = Module2.LSClient[1).SexCode;
            //frmLSSPOUSE.txtSpouseFace.Text = Strings.Format(Module2.LSClient[1).FaceAmt, "0000000");
            if (Module2.LSClient[1].PlanCode == 1)
            {
                //frmLSSPOUSE.cboSpousePlan.SelectedIndex = 0;
            }
            else
            {
                //frmLSSPOUSE.cboSpousePlan.SelectedIndex = 1;
            }
            //frmLSSPOUSE.txtSpouseState.Text = Module2.LSClient[1).State;
            if (Module2.LSClient[1].OptionX == 1)
            {
                //frmLSSPOUSE.cboSpouseOption.SelectedIndex = 0;
            }
            else
            {
                //frmLSSPOUSE.cboSpouseOption.SelectedIndex = 1;
            }
            if (Module2.LSClient[1].SubStd == 0)
            {
                //frmLSSPOUSE.cboSpouseSubStd.SelectedIndex = 0;
            }
            else
            {
                //frmLSSPOUSE.cboSpouseSubStd.SelectedIndex = Module2.LSClient[1).SubStd;
            }
            if (Module2.LSClient[1].WPD != 0)
            {
                //frmLSSPOUSE.cboSpouseWPD.SelectedIndex = 1;
            }
            else
            {
                //frmLSSPOUSE.cboSpouseWPD.SelectedIndex = 0;
            }
            if (Module2.LSClient[1].GPO == 0)
            {
                //frmLSSPOUSE.cboSpouseGPO.SelectedIndex = 0;
            }
            else
            {
                //frmLSSPOUSE.cboSpouseGPO.SelectedIndex = (Module2.LSClient[1).GPO / 1000) - 4;
            }
            if (Module2.LSClient[1].COLA == 0)
            {
                //frmLSSPOUSE.cboSpouseCOLA.SelectedIndex = 0;
            }
            else
            {
                //frmLSSPOUSE.cboSpouseCOLA.SelectedIndex = Module2.LSClient[1).COLA - 1;
            }
            if (Module2.LSClient[1].AddFace > 0)
            {
                //frmLSSPOUSE.txtSpouseTermRider.Text = Conversion.Str(Module2.LSClient[1).AddFace);
                //frmLSSPOUSE.txtSpouseRiderYearEnd.Text = Conversion.Str(Module2.LSClient[1).AddFaceEnd);
                //frmLSSPOUSE.cboSpouseRiderYrAgeEnd.SelectedIndex = 0;
            }
            if (Module2.LSClient[1].CurrRate > 0)
            {
                //frmLSSPOUSE.txtSpouseCurrentInt.Text = Strings.Format(Module2.LSClient[1).CurrRate, "00.000");
            }
        }
        else
        {
            if (Module2.ClientInfo.ClientNumber > 0)
            {
                for (k = 0; k <= 11; k++)
                {
                    RetCd = Module2.insured_file(1, Module2.ClientInfo.ClientNumber, k);
                    if (RetCd != 0 && Module2.InsuredInfo.InsRelation == "S")
                    {
                        Module2.LSClient[1].ClientName = Module2.LSClient[0].ClientName;
                        Parse_Spouse_Name(Spouse_FirstName, Spouse_MidInit, Spouse_LastName);
                        //frmLSSPOUSE.txtSpouseFirstName.Text = Module2.InsuredInfo.InsFName;
                        //frmLSSPOUSE.txtSpouseLastName.Text = Spouse_LastName;
                        //switch ((Module2.InsuredInfo.insTobacco))
                        //{
                        //    case "Y":
                        //    case "C":
                        //    case "P":
                        //    case "S":
                        //        frmLSSPOUSE.cboSpousePlan.SelectedIndex = 1;
                        //        break;
                        //    default:
                        //        frmLSSPOUSE.cboSpousePlan.SelectedIndex = 0;
                        //        break;
                        //}
                        today = DateTime.Now.Ticks;
                        if (Module2.InsuredInfo.InsBirthDate != 0)
                        {
                            today = DateTime.Now.Ticks;
                            SerialBDt = Module2.InsuredInfo.InsBirthDate;
                            Module2.Year_Calc(SerialBDt, today, CalcYear, YMDBdate);
                            //frmLSSPOUSE.txtSpouseAge.Text = Strings.Trim(Conversion.Str(CalcYear));
                            TempDate = new DateTime(SerialBDt).ToShortDateString();
                            //TempDate = Strings.Format(Month(new System.DateTime(SerialBDt)), "00") + "/" + Strings.Format(new DateTime(SerialBDt).Day, "00") + "/" + Strings.Format(Strings.Right(Conversion.Str(new DateTime(SerialBDt).Year), 4), "0000");
                            //frmLSSPOUSE.txtSpouseBirth.Text = TempDate;
                        }
                        else
                        {
                            //frmLSSPOUSE.txtSpouseAge.Text = "  ";
                            //frmLSSPOUSE.txtSpouseBirth.Text = "__/__/____";
                        }
                        //frmLSSPOUSE.txtSpouseSex.Text = Module2.InsuredInfo.InsGender;
                        k = 11;
                    }
                }
            }
        }
        return;
    }
    */
    /*
    private static void Parse_Spouse_Name(string Spouse_FirstName, string Spouse_MidInit, string Spouse_LastName) {
        year = 1;
        for (i = 1; i <= Strings.Len(Module2.LSClient[1].ClientName); i++)
        {
            if (Strings.Mid(Module2.LSClient[1].ClientName, i, 1) != " ")
            {
                if (year == 1)
                    Spouse_FirstName = Spouse_FirstName + Strings.Mid(Module2.LSClient[1].ClientName, i, 1);
                if (year == 2)
                    Spouse_MidInit = Strings.Mid(Module2.LSClient[1].ClientName, i, 1);
                if (year == 3)
                    Spouse_LastName = Spouse_LastName + Strings.Mid(Module2.LSClient[1].ClientName, i, 1);
            }
            else
            {
                year = year + 1;
            }
        }

        return;

    }
    */
    /*
    public static void Open_LS_RateFiles() {
        string file = null;
        string filepath = null;

        filepath = "C:\\aric\\life\\";
        //filepath = "X:\VB30\LS2 PAD\"
        filnum = FileSystem.FreeFile();
        file = filepath + "lsminp.rdm";
        //' Open file For Random As #filnum Len = Len(MinpRate)
        Module2.LS_RateFiles[0] = filnum;
        //0 is Minimum Premium

        filnum = FileSystem.FreeFile();
        file = filepath + "lstarg.rdm";
        //' Open file For Random As #filnum Len = Len(TargRate)
        Module2.LS_RateFiles[1] = filnum;
        //1 is Target Premium

        filnum = FileSystem.FreeFile();
        file = filepath + "lssurr.rdm";
        // Open file For Random As #filnum Len = Len(SurrRate)
        Module2.LS_RateFiles[2] = filnum;
        //2 is Surrender Charges

        filnum = FileSystem.FreeFile();
        file = filepath + "lsratepr.rdm";
        // Open file For Random As #filnum Len = Len(PrinRate)
        Module2.LS_RateFiles[3] = filnum;
        //3 is Principal Insured

        filnum = FileSystem.FreeFile();
        file = filepath + "lsratesi.rdm";
        // Open file For Random As #filnum Len = Len(SIRate)
        Module2.LS_RateFiles[4] = filnum;
        //4 is Spouse/Additional Insureds

        filnum = FileSystem.FreeFile();
        file = filepath + "lsratesb.rdm";
        // Open file For Random As #filnum Len = Len(SubRate)
        Module2.LS_RateFiles[5] = filnum;
        //5 is Substandard rates

        filnum = FileSystem.FreeFile();
        file = filepath + "lsrategp.rdm";
        // Open file For Random As #filnum Len = Len(GPORate)
        Module2.LS_RateFiles[6] = filnum;
        //6 is GPO rates

        filnum = FileSystem.FreeFile();
        file = filepath + "lsratewp.rdm";
        // Open file For Random As #filnum Len = Len(WPDRate)
        Module2.LS_RateFiles[7] = filnum;
        //7 is Waiver of Premium

        filnum = FileSystem.FreeFile();
        file = filepath + "htwtlife.rdm";
        // Open file For Random As #filnum Len = Len(LHtWt)
        Module2.LS_RateFiles[8] = filnum;
        //8 is Height/Weight tables

        filnum = FileSystem.FreeFile();
        file = filepath + "lscorr.rdm";
        // Open file For Random As #filnum Len = 4
        Module2.LS_RateFiles[9] = filnum;
        //9 is Corridor of Coverage table

        filnum = FileSystem.FreeFile();
        file = filepath + "lsspouse.rdm";
        // Open file For Random As #filnum Len = 4
        Module2.LS_RateFiles[10] = filnum;
        //10 is Spouse Death Benefit Adjustment table

        filnum = FileSystem.FreeFile();
        file = filepath + "lscso80.rdm";
        // Open file For Random As #filnum Len = Len(CSO80Rate)
        Module2.LS_RateFiles[11] = filnum;
        //11 is CSO80 values for Guideline

    }
    */
    /*
    public static void Parse_Command_Argument(string CommandStr, long CommandAgt, int CommandClient) {
        int RightClient = 0;
        int LeftClient = 0;
        int RightAgent = 0;
        int LeftAgent = 0;
        string DefaultAgent = null;
        string RetStr = null;

        RightClient = 0;
        LeftClient = 0;
        RightAgent = 0;
        LeftAgent = 1;

        //RetStr = GetAnyIniStr("Americare", "LS2Face", "AMRICARE.INI");
        if (!string.IsNullOrEmpty(Strings.Trim(RetStr)))
        {
            if (Conversion.Val(RetStr) == 99)
            {
                Module2.defFaceLimit = 1;
            }
            else
            {
                Module2.defFaceLimit = 0;
            }
        }
        else
        {
            Module2.defFaceLimit = 0;
        }

        if (Strings.Len(CommandStr) == 0)
        {
            //DefaultAgent = GetAnyIniStr("Americare", "Agent", "AMRICARE.INI");
            if (!string.IsNullOrEmpty(DefaultAgent))
            {
                CommandAgt = long.Parse(DefaultAgent);
            }
            else
            {
                CommandAgt = 0;
            }
            CommandClient = 0;
            return;
        }

        for (k = Strings.Len(CommandStr); k >= 1; k += -1)
        {
            switch (Strings.Mid(CommandStr, k, 1))
            {
                case " ":
                    if (RightClient != 0)
                    {
                        if (LeftClient == 0)
                        {
                            LeftClient = k + 1;
                        }
                        else if (RightAgent != 0 && LeftAgent == 0)
                        {
                            LeftAgent = k + 1;
                        }
                    }
                    break;
                default:
                    if (RightClient == 0)
                    {
                        RightClient = k + 1;
                    }
                    else if (RightAgent == 0 && LeftClient != 0)
                    {
                        RightAgent = k + 1;
                    }
                    break;
            }
        }

        CommandAgt = long.Parse(Strings.Mid(CommandStr, LeftAgent, RightAgent));
        CommandClient = int.Parse(Strings.Mid(CommandStr, LeftClient, RightClient));

    }
    */

    public static void Premium_To_Obtain_CV(IllustrationInfo info, IAppData data) {
        //Calculate Modal Premium to Obtain Target Cash Value,
        //using all current parameter values to reach the Target
        //in the given number of years.

        //Finding this premium is a matter of guesswork and inter-
        //polation.  Start with Minimum Premium as the "lower
        //bound" and then use Guideline Premium as the "upper
        //bound".  If the Target Cash Value is outside these
        //"bounds", discontinue the search and return the nearest
        //"bound" premium.

        //If the Target Cash Value falls between the "bounds",
        //the limits are established. For every new guess, determine
        //the a value halfway between the high value premium and the
        //low value premium.  Use this value as the next guess.  New
        //result is calculated and compared to Target Cash Value. If
        //the result is low, you have a new Low Premium; otherwise, you
        //have a new High Premium.

        //The process is repeated until the low and high premium are 1
        //cent apart.  The high guess then becomes the Modal Premium
        //for this Search.

        float HighGuess = 0;
        float LowGuess = 0;
        float NewGuess = 0;
        double HighValue = 0;
        double LowValue = 0;
        double EndValue = 0;
        int EndYear = 0;

        s = Module2.SpouseClient;

        Module2.LSLedger[Module2.LSClient[s].IssueAge + 1].SurrenderValue[1] = 0;
        Module2.LSLedger[Module2.LSClient[s].IssueAge + 1].CashValue[1] = 0;
        Module2.LSLedger[Module2.LSClient[s].IssueAge + 1].DeathBenefit[1] = Module2.LSClient[s].FaceAmt;

        EndYear = info.SpecialOptionsAge;
        EndValue = info.SpecialOptionsCashValue;
        Module2.GuarCurr = 1;

        Module2.LS_Outlay = 0;
        Module2.LS_CV[Module2.GuarCurr] = 0;
        Module2.LS_CSV[Module2.GuarCurr] = 0;
        Module2.LS_DB[Module2.GuarCurr] = Module2.LSClient[s].FaceAmt;
        Module2.LS_DB_Minimum[Module2.GuarCurr] = Module2.LSClient[s].FaceAmt;
        Module2.LS_IRate[Module2.GuarCurr] = Module2.LSClient[s].CurrRate / 100;
        Module2.LS_Modal_Prem = Module2.CalcPrem.TotMin;
        Module2.LS_DBOption = Module2.LSClient[s].OptionX;
        Module2.LS_COLA_Client = 0;
        Module2.LS_COLA_Spouse = 0;
        for (i = 1; i <= 9; i++)
        {
            Module2.LS_COLA_Insured[i] = 0;
        }

        Process_Ledger_Years(info, data, Module2.LSClient[s].IssueAge, EndYear, 2);

        LowValue = Module2.LSLedger[EndYear].CashValue[1];
        if (EndValue < LowValue)
        {
            info.SpecialOptionsModalPremium = "< MINIMUM";
            return;
        }

        Module2.LS_Outlay = 0;
        Module2.LS_CV[Module2.GuarCurr] = 0;
        Module2.LS_CSV[Module2.GuarCurr] = 0;
        Module2.LS_DB[Module2.GuarCurr] = Module2.LSClient[s].FaceAmt;
        Module2.LS_DB_Minimum[Module2.GuarCurr] = Module2.LSClient[s].FaceAmt;
        Module2.LS_IRate[Module2.GuarCurr] = Module2.LSClient[s].CurrRate / 100;
        switch ((Module2.LSClient[s].Mode))
        {
            case "M":
                Module2.LS_Modal_Prem = Module2.CalcPrem.GuideAnnual / 11.688f;
                break;
            case "Q":
                Module2.LS_Modal_Prem = Module2.CalcPrem.GuideAnnual / 3.916f;
                break;
            case "S":
                Module2.LS_Modal_Prem = Module2.CalcPrem.GuideAnnual / 1.972f;
                break;
            default:
                Module2.LS_Modal_Prem = Module2.CalcPrem.GuideAnnual;
                break;
        }
        Module2.LS_DBOption = Module2.LSClient[s].OptionX;
        Module2.LS_COLA_Client = 0;
        Module2.LS_COLA_Spouse = 0;
        for (i = 1; i <= 9; i++)
        {
            Module2.LS_COLA_Insured[i] = 0;
        }

        Process_Ledger_Years(info, data, Module2.LSClient[s].IssueAge, EndYear, 2);

        HighValue = Module2.LSLedger[EndYear].CashValue[1];

        if (EndValue > HighValue)
        {
            info.SpecialOptionsModalPremium = "> GUIDELINE";
            return;
        }

        HighGuess = Module2.LS_Modal_Prem;
        LowGuess = Module2.CalcPrem.TotMin;
        NewGuess = HighGuess - ((HighGuess - LowGuess) / 2);
        Module2.LS_Modal_Prem = NewGuess;

        while ((HighGuess - LowGuess > 0.01))
        {
            Module2.LS_Outlay = 0;
            Module2.LS_CV[Module2.GuarCurr] = 0;
            Module2.LS_CSV[Module2.GuarCurr] = 0;
            Module2.LS_DB[Module2.GuarCurr] = Module2.LSClient[s].FaceAmt;
            Module2.LS_DB_Minimum[Module2.GuarCurr] = Module2.LSClient[s].FaceAmt;
            Module2.LS_IRate[Module2.GuarCurr] = Module2.LSClient[s].CurrRate / 100;
            Module2.LS_DBOption = Module2.LSClient[s].OptionX;
            Module2.LS_COLA_Client = 0;
            Module2.LS_COLA_Spouse = 0;
            for (i = 1; i <= 9; i++)
            {
                Module2.LS_COLA_Insured[i] = 0;
            }

            Process_Ledger_Years(info, data, Module2.LSClient[s].IssueAge, EndYear, 1);

            //Within a buck; close enough
            if (System.Math.Abs(EndValue - Module2.LSLedger[EndYear].CashValue[1]) < 1)
            {
                LowGuess = Module2.LS_Modal_Prem;
                HighGuess = Module2.LS_Modal_Prem;
                LowValue = Module2.LSLedger[EndYear].CashValue[1];
                HighValue = Module2.LSLedger[EndYear].CashValue[1];
            }
            else
            {
                if (EndValue >= Module2.LSLedger[EndYear].CashValue[1])
                {
                    LowValue = Module2.LSLedger[EndYear].CashValue[1];
                    LowGuess = Module2.LS_Modal_Prem;
                }
                else
                {
                    HighValue = Module2.LSLedger[EndYear].CashValue[1];
                    HighGuess = Module2.LS_Modal_Prem;
                }
                NewGuess = HighGuess - ((HighGuess - LowGuess) / 2);
                Module2.LS_Modal_Prem = NewGuess;
            }
        }

        info.SpecialOptionsModalPremium = Strings.Format(HighGuess, "     0.00");

    }


    public static void Principal_Mortality() {
        float BandFace = Convert.ToSingle(Module2.LS_DB[Module2.GuarCurr] + Module2.LS_COLA_Client + Module2.LS_LoanBalance);
        if (BandFace < 50000)
            Module2.LS_Prin_Band = 1;
        else if (BandFace.IsBetween(50000, 99999, true))
            Module2.LS_Prin_Band = 2;
        else if (BandFace.IsBetween(100000, 249999, true))
            Module2.LS_Prin_Band = 3;
        else
            Module2.LS_Prin_Band = 4;

        Module2.LS_MortRate = Module2.LSMort[Module2.GuarCurr, Module2.AnnYr].Prin_Base[Module2.LS_Prin_Band - 1];

        if (Module2.LSClient[Module2.SpouseClient].SubStd > 0)
        {
            Module2.LS_MortRate = Module2.LS_MortRate + Module2.LSMort[Module2.GuarCurr, Module2.AnnYr].Prin_Sub[Module2.LS_Prin_Band - 1];
            if (Module2.AnnYr == 65)
            {
                //Module2.LS_MortRate = Module2.LS_MortRate;
            }
        }

    }

    public static void Principal_Premium() {
        double Temp_DB = 0;

        Temp_DB = Module2.LS_DB[Module2.GuarCurr] + Module2.LS_COLA_Client;
        if (Module2.LS_DBOption == 1)
            Module2.LS_Charges = (((Temp_DB * 0.996737) - Module2.LS_CV[Module2.GuarCurr]) * Module2.LS_MortRate) / 1000;
        else
        {
            if (Module2.LS_CV_Opt2_Start[Module2.GuarCurr] == 0)
                Module2.LS_Charges = ((Temp_DB * 0.996737) * Module2.LS_MortRate) / 1000;
            else
            {
                if (Module2.LS_CV[Module2.GuarCurr] > Module2.LS_CV_Opt2_Start[Module2.GuarCurr])
                    Temp_DB = Temp_DB + Module2.LS_CV[Module2.GuarCurr] - Module2.LS_CV_Opt2_Start[Module2.GuarCurr];
                Module2.LS_Charges = (((Temp_DB * 0.996737) - Module2.LS_CV[Module2.GuarCurr]) * Module2.LS_MortRate) / 1000;
            }
        }
    }

    public static void Principal_Rider_GPO() {
        s = Module2.SpouseClient;
        if (Module2.AnnYr >= 41)
            return;
        Module2.LS_Insureds_Mortcost = Module2.LS_Insureds_Mortcost + (Module2.LSClient[s].GPO / 1000 * Module2.LSMort[Module2.GuarCurr, Module2.AnnYr].Prin_GPO);
    }

    public static void Principal_Rider_Mortality(int GuideSingle) {
        int TempYr = 0;
        float[] Temp_Mort = new float[2];

        s = Module2.SpouseClient;
        TempYr = Module2.AnnYr - Module2.LSClient[s].IssueAge;
        //LS_Rider_Client = 0

        if (GuideSingle != 0)
        {
            Temp_Mort[0] = Module2.LSMort[0, Module2.AnnYr].Insured_CSO80[0] < Module2.LSMort[0, Module2.AnnYr].Insured_Base[0]
                ? Module2.LSMort[0, Module2.AnnYr].Insured_CSO80[0]
                : Module2.LSMort[0, Module2.AnnYr].Insured_Base[0];
            Temp_Mort[1] = Module2.LSMort[1, Module2.AnnYr].Insured_CSO80[0] < Module2.LSMort[1, Module2.AnnYr].Insured_Base[0]
                ? Module2.LSMort[1, Module2.AnnYr].Insured_CSO80[0]
                : Module2.LSMort[1, Module2.AnnYr].Insured_Base[0];
        }
        else
        {
            Temp_Mort[0] = Module2.LSMort[0, Module2.AnnYr].Insured_Base[0];
            Temp_Mort[1] = Module2.LSMort[1, Module2.AnnYr].Insured_Base[0];
        }

        if (Module2.LSClient[s].AddFace != 0 && Module2.LSClient[s].AddFaceStart <= TempYr + 1 && Module2.LSClient[s].AddFaceEnd >= TempYr)
        {
            if (TempYr == 0)
            {
                Module2.LS_Insureds_Mortcost = Module2.LS_Insureds_Mortcost + (Module2.LSClient[s].AddFace / 1000 * Temp_Mort[1]);
                if (Module2.LSClient[s].SubStd > 0)
                    Module2.LS_Insureds_Mortcost = Module2.LS_Insureds_Mortcost + (Module2.LSClient[s].AddFace / 1000 * Temp_Mort[1]);
            }
            else
            {
                Module2.LS_Insureds_Mortcost = Module2.LS_Insureds_Mortcost + (Module2.LSClient[s].AddFace / 1000 * Temp_Mort[Module2.GuarCurr]);
                if (Module2.LSClient[s].SubStd > 0)
                    Module2.LS_Insureds_Mortcost = Module2.LS_Insureds_Mortcost + (Module2.LSClient[s].AddFace / 1000 * Temp_Mort[Module2.GuarCurr]);
            }
        }
    }

    public static void Principal_Rider_WPD() {
        s = Module2.SpouseClient;

        if (Module2.AnnYr < 60)
        {
            Module2.LS_Insureds_Mortcost += ((Convert.ToSingle(Module2.LS_Charges) + Module2.LS_Insureds_Mortcost) * Module2.LSMort[Module2.GuarCurr, Module2.AnnYr].Prin_WPD);
            Module2.LS_Insureds_Mortcost += ((Module2.LS_DB[Module2.GuarCurr] + Module2.LS_COLA_Client < 100000 ? 3.5f : 3f) * Module2.LSMort[Module2.GuarCurr, Module2.AnnYr].Prin_WPD);
        }
    }

    public static void Print_Cost_Indices(string OnePage) {
        string Temp_Index = null;

        OnePage = OnePage + NL + NL + "5% Interest Adjusted Indices" + Strings.Space(11);
        OnePage = OnePage + "Guaranteed" + Strings.Space(12);
        OnePage = OnePage + "Projected" + Strings.Space(11) + NL;
        OnePage = OnePage + Strings.Space(41) + "Values";
        OnePage = OnePage + Strings.Space(16) + "Values" + NL;
        OnePage = OnePage + Strings.Space(36) + "----------------";
        OnePage = OnePage + Strings.Space(6) + "----------------" + NL;
        OnePage = OnePage + Strings.Space(36) + "10 Year  20 Year";
        OnePage = OnePage + Strings.Space(6) + "10 Year  20 Year" + NL;
        OnePage = OnePage + Strings.Space(36) + "----------------";
        OnePage = OnePage + Strings.Space(6) + "----------------" + NL;

        OnePage = OnePage + "Surrender Cost Index:" + Strings.Space(15);
        if (Module2.LS_Index_Numerator_SC[0, 0] == 0 || Module2.LS_Index_Denominator[0] == 0)
        {
            OnePage = OnePage + Strings.Space(3) + "N/A" + Strings.Space(4);
        }
        else
        {
            Temp_Index = Strings.Format(Module2.LS_Index_Numerator_SC[0, 0] / (Module2.LS_Index_Denominator[0] / 1000), "##0.00");
            OnePage = OnePage + Strings.Space(6 - Strings.Len(Temp_Index)) + Temp_Index + Strings.Space(4);
        }
        if (Module2.LS_Index_Numerator_SC[0, 1] == 0 || Module2.LS_Index_Denominator[1] == 0)
        {
            OnePage = OnePage + Strings.Space(3) + "N/A" + Strings.Space(6);
        }
        else
        {
            Temp_Index = Strings.Format(Module2.LS_Index_Numerator_SC[0, 1] / (Module2.LS_Index_Denominator[1] / 1000), "##0.00");
            OnePage = OnePage + Strings.Space(6 - Strings.Len(Temp_Index)) + Temp_Index + Strings.Space(6);
        }
        if (Module2.LS_Index_Numerator_SC[1, 0] == 0 || Module2.LS_Index_Denominator[0] == 0)
        {
            OnePage = OnePage + Strings.Space(3) + "N/A" + Strings.Space(4);
        }
        else
        {
            Temp_Index = Strings.Format(Module2.LS_Index_Numerator_SC[1, 0] / (Module2.LS_Index_Denominator[0] / 1000), "##0.00");
            OnePage = OnePage + Strings.Space(6 - Strings.Len(Temp_Index)) + Temp_Index + Strings.Space(4);
        }
        if (Module2.LS_Index_Numerator_SC[1, 1] == 0 || Module2.LS_Index_Denominator[1] == 0)
        {
            OnePage = OnePage + Strings.Space(3) + "N/A" + NL;
        }
        else
        {
            Temp_Index = Strings.Format(Module2.LS_Index_Numerator_SC[1, 1] / (Module2.LS_Index_Denominator[1] / 1000), "##0.00");
            OnePage = OnePage + Strings.Space(6 - Strings.Len(Temp_Index)) + Temp_Index + NL;
        }

        OnePage = OnePage + "Net Payment Index:" + Strings.Space(18);
        if (Module2.LS_Index_Numerator_NP[0, 0] == 0 || Module2.LS_Index_Denominator[0] == 0)
        {
            OnePage = OnePage + Strings.Space(3) + "N/A" + Strings.Space(4);
        }
        else
        {
            Temp_Index = Strings.Format(Module2.LS_Index_Numerator_NP[0, 0] / (Module2.LS_Index_Denominator[0] / 1000), "##0.00");
            OnePage = OnePage + Strings.Space(6 - Strings.Len(Temp_Index)) + Temp_Index + Strings.Space(4);
        }
        if (Module2.LS_Index_Numerator_NP[0, 1] == 0 || Module2.LS_Index_Denominator[1] == 0)
        {
            OnePage = OnePage + Strings.Space(3) + "N/A" + Strings.Space(6);
        }
        else
        {
            Temp_Index = Strings.Format(Module2.LS_Index_Numerator_NP[0, 1] / (Module2.LS_Index_Denominator[1] / 1000), "##0.00");
            OnePage = OnePage + Strings.Space(6 - Strings.Len(Temp_Index)) + Temp_Index + Strings.Space(6);
        }
        if (Module2.LS_Index_Numerator_NP[1, 0] == 0 || Module2.LS_Index_Denominator[0] == 0)
        {
            OnePage = OnePage + Strings.Space(3) + "N/A" + Strings.Space(4);
        }
        else
        {
            Temp_Index = Strings.Format(Module2.LS_Index_Numerator_NP[1, 0] / (Module2.LS_Index_Denominator[0] / 1000), "##0.00");
            OnePage = OnePage + Strings.Space(6 - Strings.Len(Temp_Index)) + Temp_Index + Strings.Space(4);
        }
        if (Module2.LS_Index_Numerator_NP[1, 1] == 0 || Module2.LS_Index_Denominator[1] == 0)
        {
            OnePage = OnePage + Strings.Space(3) + "N/A" + NL + NL;
        }
        else
        {
            Temp_Index = Strings.Format(Module2.LS_Index_Numerator_NP[1, 1] / (Module2.LS_Index_Denominator[1] / 1000), "##0.00");
            OnePage = OnePage + Strings.Space(6 - Strings.Len(Temp_Index)) + Temp_Index + NL + NL;
        }

    }


    public static void Process_Ledger_Years(IllustrationInfo info, IAppData data, int StartAge, int StopAge, int Search) {
        double MonthRate = 0;
        float CorrRate = 0;

        for (Module2.AnnYr = StartAge; Module2.AnnYr < StopAge; Module2.AnnYr++)
        {
            if (!Verify_LSCORR(data, Module2.LS_RateFiles[9], Module2.AnnYr, ref CorrRate))
                CorrRate = 0;
            if (Module2.LSClient[s].FutureWD != 0)
                Check_Future_WD();
            else
            {
                Module2.LSLedger[Module2.AnnYr].WithdrawAmount = 0;
                Module2.LSLedger[Module2.AnnYr].LoanAmount = 0;
                Module2.LSLedger[Module2.AnnYr].LoanRepayAmount = 0;
            }
            for (Module2.AnnMo = 1; Module2.AnnMo <= 12; Module2.AnnMo++)
            {
                Add_Modal_Premium();
                Module2.LS_Insureds_Mortcost = 0;
                if (Module2.LSClient[s].SpouseRider != 0)
                    Spouse_Rider_Mortality(data, 0);
                if (Module2.LSClient[s].ChildRider != 0)
                    Child_Rider_Mortality();

                for (year = 0; year < 9; year++)
                {
                    if (Module2.LSClient[s].InsuredRider[year] != 0)
                        Insured_Rider_Mortality(year, false);
                }
                Principal_Rider_Mortality(0);
                if (Module2.LSClient[s].GPO != 0)
                    Principal_Rider_GPO();
                Principal_Mortality();
                Principal_Premium();
                if (Module2.LSClient[s].WPD == 1)
                    Principal_Rider_WPD();
                Module2.LS_CV[Module2.GuarCurr] = Module2.LS_CV[Module2.GuarCurr] - Module2.LS_Charges - Module2.LS_Insureds_Mortcost;
                Do_Month_Interest(ref MonthRate);
                Module2.LS_CV[Module2.GuarCurr] = Module2.LS_CV[Module2.GuarCurr] * MonthRate;
                if (CorrRate > 0 && Module2.LS_DBOption == 1)
                {
                    if ((Module2.LS_DB_Minimum[Module2.GuarCurr] + Module2.LS_COLA_Client) < (Module2.LS_CV[Module2.GuarCurr] - Module2.LS_CV_Opt2_Start[Module2.GuarCurr]) * CorrRate)
                        Module2.LS_DB[Module2.GuarCurr] = Module2.LS_DB_Minimum[Module2.GuarCurr] + (((Module2.LS_CV[Module2.GuarCurr] - Module2.LS_CV_Opt2_Start[Module2.GuarCurr]) * CorrRate) - (Module2.LS_DB_Minimum[Module2.GuarCurr] + Module2.LS_COLA_Client));
                    else
                        Module2.LS_DB[Module2.GuarCurr] = Module2.LS_DB_Minimum[Module2.GuarCurr];
                }
                if (Module2.AnnYr - Module2.LSClient[s].IssueAge < 21 && Search == 0)
                    Sum_Cost_Indices();
            }

            Do_Anniversary(info, data);

            if (Module2.LSLedger != null)
            {
                if (Module2.LSLedger[Module2.AnnYr] == null)
                    throw new ApplicationException($"Ledger value for year {Module2.AnnYr} is missing.");
            }
            Module2.LSLedger[Module2.AnnYr].AnnualOutlay = Convert.ToSingle(Module2.LS_Outlay);
            Module2.LS_Outlay = 0;
            Module2.LSLedger[Module2.AnnYr].SurrenderValue[Module2.GuarCurr] = Convert.ToSingle(Module2.LS_CSV[Module2.GuarCurr]);
            if (Module2.LS_CV[Module2.GuarCurr] < 0)
                Module2.LSLedger[Module2.AnnYr].CashValue[Module2.GuarCurr] = 0;
            else
                Module2.LSLedger[Module2.AnnYr].CashValue[Module2.GuarCurr] = Convert.ToSingle(Module2.LS_CV[Module2.GuarCurr]);
            if (Module2.LS_DBOption == 1)
                Module2.LSLedger[Module2.AnnYr].DeathBenefit[Module2.GuarCurr] = Convert.ToSingle(Module2.LS_DB[Module2.GuarCurr] + Module2.LS_COLA_Client);
            else
            {
                if (Module2.LS_CV_Opt2_Start[Module2.GuarCurr] > Module2.LS_CV[Module2.GuarCurr])
                    Module2.LSLedger[Module2.AnnYr].DeathBenefit[Module2.GuarCurr] = Convert.ToSingle(Module2.LS_DB[Module2.GuarCurr] + Module2.LS_COLA_Client);
                else
                    Module2.LSLedger[Module2.AnnYr].DeathBenefit[Module2.GuarCurr] = Convert.ToSingle(Module2.LS_DB[Module2.GuarCurr] + Module2.LS_COLA_Client + Module2.LS_CV[Module2.GuarCurr] - Module2.LS_CV_Opt2_Start[Module2.GuarCurr]);
            }

            if ((Module2.LSClient[s].IssueAge % 2 != 0 && Module2.AnnYr % 2 == 0) || (Module2.LSClient[s].IssueAge % 2 == 0 && Module2.AnnYr % 2 != 0))
            {
                if (Module2.LSClient[s].COLA != 0)
                    Add_COLA("P", 0);

                if (Module2.LSClient[s].SpouseRider == 1)
                {
                    if (Module2.LSSpouseRider.COLA != 0)
                        Add_COLA("S", 0);
                }

                for (i = 0; i < 9; i++)
                {
                    if (Module2.LSClient[s].InsuredRider[i] == 1 && Module2.LSInsuredRider[i].COLA != 0)
                        Add_COLA("I", i);
                }
            }

            if (Module2.LS_CV[Module2.GuarCurr] <= 0 && Module2.AnnYr > 3)
            {
                for (i = Module2.AnnYr; i < MAXAGE; i++)
                {
                    Module2.LSLedger[i].SurrenderValue[Module2.GuarCurr] = 0;
                    Module2.LSLedger[i].CashValue[Module2.GuarCurr] = 0;
                    Module2.LSLedger[i].DeathBenefit[Module2.GuarCurr] = 0;
                }
                break;
                ////Module2.AnnYr = 95;
                //forces end of loop
            }
            if (Module2.LSClient[s].FutureChanges == 1)
                Check_Future_Changes();
        }
    }

    public static int ReadyToIllustrate() {
        int functionReturnValue = 0;

        functionReturnValue = 1;
        s = Module2.SpouseClient;
        if (Module2.LSClient[s].IssueAge > 75)
        {
            functionReturnValue = 0;
            return functionReturnValue;
        }
        if (Module2.LSClient[s].SexCode != "M" && Module2.LSClient[s].SexCode != "F")
        {
            functionReturnValue = 0;
            return functionReturnValue;
        }
        if (Module2.LSClient[s].FaceAmt < 15000 && Module2.defFaceLimit == 0)
        {
            functionReturnValue = 0;
            return functionReturnValue;
        }
        if ((Module2.LSClient[s].CurrRate * 100f) < 4.0)
        {
            functionReturnValue = 0;
            return functionReturnValue;
        }
        return functionReturnValue;

    }



    public static void Reverse_CashValue(int GuideLineStd, float WaiverPct, float Premium, int AddInsds) {
        double Numerator = 0;
        double Denominator = 0;
        float QFactor = 0;
        double Temp_DB = 0;

        Temp_DB = Module2.LS_DB[Module2.GuarCurr] + Module2.LS_COLA_Client;
        QFactor = Module2.LS_MortRate / 1000;
        if (GuideLineStd == 1)
        {
            if (Module2.LS_MortRate > Module2.LSMort[Module2.GuarCurr, Module2.AnnYr].Prin_CSO80 && Module2.LSMort[Module2.GuarCurr, Module2.AnnYr].Prin_CSO80 > 0)
            {
                QFactor = Module2.LSMort[Module2.GuarCurr, Module2.AnnYr].Prin_CSO80 / 1000;
            }
        }
        Numerator = Module2.LS_Charges * QFactor;
        Numerator = Numerator + (Temp_DB * 0.996737 * QFactor);
        Numerator = Numerator + Module2.LS_Charges;
        Numerator = Numerator * (1 + WaiverPct);
        Numerator = (Numerator * Module2.LS_IRate[Module2.GuarCurr]) + Module2.LS_CV[Module2.GuarCurr];
        Denominator = 1 + QFactor + (QFactor * WaiverPct);
        Denominator = Denominator * Module2.LS_IRate[Module2.GuarCurr];
        Module2.LS_CV[Module2.GuarCurr] = (Numerator / Denominator) - Premium + 0.005;
        Module2.LS_CV[Module2.GuarCurr] = Module2.LS_CV[Module2.GuarCurr] * 100;
        Module2.LS_CV[Module2.GuarCurr] = (Conversion.Int(Module2.LS_CV[Module2.GuarCurr])) / 100;

        ///* if AddInsds is true, calculate the Guideline Single
        ///* Premium inclusive of additional insured mortality
        ///* charges in the Module2.LS_CSV[Module2.GuarCurr] field.

        if (AddInsds == 1)
        {
            Numerator = (Module2.LS_Charges + Module2.LS_Insureds_Mortcost) * QFactor;
            Numerator = Numerator + (Temp_DB * 0.996737 * QFactor);
            Numerator = Numerator + Module2.LS_Charges + Module2.LS_Insureds_Mortcost;
            Numerator = Numerator * (1 + WaiverPct);
            Numerator = (Numerator * Module2.LS_IRate[Module2.GuarCurr]) + Module2.LS_CSV[Module2.GuarCurr];
            //Denominator shouldn't change
            Module2.LS_CSV[Module2.GuarCurr] = (Numerator / Denominator) - Premium + 0.005;
            Module2.LS_CSV[Module2.GuarCurr] = Module2.LS_CSV[Module2.GuarCurr] * 100;
            Module2.LS_CSV[Module2.GuarCurr] = (Conversion.Int(Module2.LS_CSV[Module2.GuarCurr])) / 100;
        }

    }


    public static void Set_Client_Illus_Parameters(IllustrationInfo info) {
        Module2.LSClient[0].ClientName = info.ClientData.FirstName + " " + (string.IsNullOrEmpty(info.ClientData.MiddleInitial) ? string.Empty : info.ClientData.MiddleInitial + " ") + info.ClientData.LastName;
        Module2.LSClient[0].IssueAge = info.ClientData.Age;
        if (Module2.LSClient[0].IssueAge + Module2.LSClient[0].YearsToPay > 95)
        {
            Module2.LSClient[0].YearsToPay = 95 - Module2.LSClient[0].IssueAge;
        }
        Module2.LSClient[0].SexCode = info.ClientData.Gender.Abbreviation.ToString();
        Module2.LSClient[0].FaceAmt = Convert.ToInt64(info.ClientData.InitialDeathBenefit);
        switch (info.ClientData.PlanType.Id)
        {
            case 1:
                Module2.LSClient[0].PlanCode = 2;
                break;
            default:
                Module2.LSClient[0].PlanCode = 1;
                break;
        }
        Module2.LSClient[0].State = info.ClientData.State.Abbreviation;
        switch (info.ClientData.BenefitOption.Id)
        {
            case 1:
                Module2.LSClient[0].OptionX = 2;
                break;
            default:
                Module2.LSClient[0].OptionX = 1;
                break;
        }
        switch (info.ClientData.SubstandardRate.Id)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
                Module2.LSClient[0].SubStd = info.ClientData.SubstandardRate.Id;
                break;
            default:
                Module2.LSClient[0].SubStd = 0;
                break;
        }
        switch (info.ClientData.Wpd.Id)
        {
            case 1:
                Module2.LSClient[0].WPD = 1;
                break;
            default:
                Module2.LSClient[0].WPD = 0;
                break;
        }
        switch (info.ClientData.Gpo.Id)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
                Module2.LSClient[0].GPO = 4000 + (1000 * info.ClientData.Gpo.Id);
                break;
            default:
                Module2.LSClient[0].GPO = 0;
                break;
        }
        switch (info.ClientData.Cola.Id)
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
                Module2.LSClient[0].COLA = info.ClientData.Cola.Id;
                break;
            default:
                Module2.LSClient[0].COLA = 0;
                break;
        }
        Module2.LSClient[0].AddFace = Convert.ToInt64(info.ClientData.InitialDeathBenefit);
        Module2.LSClient[0].AddFaceStart = 1;
        Module2.LSClient[0].AddFaceEnd = info.ClientData.YearToAgeOption == null || info.ClientData.YearToAgeOption.Id != 1
            ? info.ClientData.AgeOrYear == null ? 95 : info.ClientData.AgeOrYear.Value
            : info.ClientData.AgeOrYear.Value - info.ClientData.Age;
        Module2.LSClient[0].CurrRate = Convert.ToSingle(info.ClientData.InterestRate.Value);
        Module2.LSClient[0].IllusClass = 1;
        Module2.LSClient[0].PrintPages = 3;
        Module2.LSClient[0].ContactName = info.PreparedBy;
        Module2.LSClient[0].ContactPhone = info.ContactTelephoneNumber;

    }


    public static void Set_Future_Changes(IllustrationInfo info) {
        Module2.LSClient[Module2.SpouseClient].FutureChanges = 0;
        Module2.LSClient[Module2.SpouseClient].FutureWD = 0;

        for (i = 0; i <= 4; i++)
        {
            if (info.FutureSpecificDeathBenefits[i].Age > 0)
            {
                Module2.LSFuture[i].DB_Age = info.FutureSpecificDeathBenefits[i].Age.Value;
                Module2.LSFuture[i].DB_Amount = Convert.ToSingle(info.FutureSpecificDeathBenefits[i].Value.Value);
                Module2.LSClient[Module2.SpouseClient].FutureChanges = 1;
            }
            else
            {
                Module2.LSFuture[i].DB_Age = 0;
                Module2.LSFuture[i].DB_Amount = 0;
            }
            if (info.FutureModalPremiums[i].Age > 0)
            {
                Module2.LSFuture[i].Prem_Age = info.FutureModalPremiums[i].Age.Value;
                Module2.LSFuture[i].Prem_Amount = Convert.ToSingle(info.FutureModalPremiums[i].Value.Value);
                Module2.LSClient[Module2.SpouseClient].FutureChanges = 1;
            }
            else
            {
                Module2.LSFuture[i].Prem_Age = 0;
                Module2.LSFuture[i].Prem_Amount = 0;
            }
            if (info.FutureCurrentInterestRates[i].Age > 0)
            {
                Module2.LSFuture[i].Int_Age = info.FutureCurrentInterestRates[i].Age.Value;
                Module2.LSFuture[i].Int_Amount = Convert.ToSingle(info.FutureCurrentInterestRates[i].Value.Value);
                Module2.LSClient[Module2.SpouseClient].FutureChanges = 1;
            }
            else
            {
                Module2.LSFuture[i].Int_Age = 0;
                Module2.LSFuture[i].Int_Amount = 0;
            }
            if (info.FutureDeathBenefitOptions[i].Age > 0)
            {
                Module2.LSFuture[i].Opt_Age = info.FutureDeathBenefitOptions[i].Age.Value;
                Module2.LSFuture[i].Opt_Class = Convert.ToInt32(info.FutureDeathBenefitOptions[i].Value.Value);
                Module2.LSClient[Module2.SpouseClient].FutureChanges = 1;
            }
            else
            {
                Module2.LSFuture[i].Opt_Age = 0;
                Module2.LSFuture[i].Opt_Class = 0;
            }
            if (info.FutureWithdrawls[i].Age > 0)
            {
                Module2.LSFutureWD[i].WD_Age = info.FutureWithdrawls[i].Age.Value;
                Module2.LSFutureWD[i].WD_Amount = Convert.ToSingle(info.FutureWithdrawls[i].Value.Value);
                if (info.FutureWithdrawls[i].EndAge == 0)
                {
                    Module2.LSFutureWD[i].WD_Years = 95 - info.FutureWithdrawls[i].Age.Value + 1;
                }
                else
                {
                    Module2.LSFutureWD[i].WD_Years = info.FutureWithdrawls[i].EndAge.Value - info.FutureWithdrawls[i].Age.Value + 1;
                }
                Module2.LSClient[Module2.SpouseClient].FutureWD = 1;
            }
            else
            {
                Module2.LSFutureWD[i].WD_Age = 0;
                Module2.LSFutureWD[i].WD_Amount = 0;
                Module2.LSFutureWD[i].WD_Years = 0;
            }
            if (info.FutureAnnualPolicyLoans[i].Age > 0)
            {
                Module2.LSFutureWD[i].Loan_Age = info.FutureAnnualPolicyLoans[i].Age.Value;
                Module2.LSFutureWD[i].Loan_Amount = Convert.ToSingle(info.FutureAnnualPolicyLoans[i].Value.Value);
                if (info.FutureAnnualPolicyLoans[i].EndAge == 0)
                {
                    Module2.LSFutureWD[i].Loan_Years = 95 - info.FutureAnnualPolicyLoans[i].Age.Value + 1;
                }
                else
                {
                    Module2.LSFutureWD[i].Loan_Years = info.FutureAnnualPolicyLoans[i].EndAge.Value - info.FutureAnnualPolicyLoans[i].Age.Value + 1;
                }
                Module2.LSFutureWD[i].Loan_Interest = 1;
                Module2.LSClient[Module2.SpouseClient].FutureWD = 1;
            }
            else
            {
                Module2.LSFutureWD[i].Loan_Age = 0;
                Module2.LSFutureWD[i].Loan_Amount = 0;
                Module2.LSFutureWD[i].Loan_Years = 0;
                Module2.LSFutureWD[i].Loan_Interest = 0;
            }
            if (info.FutureAnnualLoanRepayments[i].Age > 0)
            {
                Module2.LSFutureWD[i].LoanPay_Age = info.FutureAnnualLoanRepayments[i].Age.Value;
                Module2.LSFutureWD[i].LoanPay_Amount = Convert.ToSingle(info.FutureAnnualLoanRepayments[i].Value.Value);
                if (info.FutureAnnualLoanRepayments[i].EndAge == 0)
                {
                    Module2.LSFutureWD[i].LoanPay_Years = 95 - info.FutureAnnualLoanRepayments[i].Age.Value + 1;
                }
                else
                {
                    Module2.LSFutureWD[i].LoanPay_Years = info.FutureAnnualLoanRepayments[i].EndAge.Value - info.FutureAnnualLoanRepayments[i].Age.Value + 1;
                }
                Module2.LSClient[Module2.SpouseClient].FutureWD = 1;
            }
            else
            {
                Module2.LSFutureWD[i].LoanPay_Age = 0;
                Module2.LSFutureWD[i].LoanPay_Amount = 0;
                Module2.LSFutureWD[i].LoanPay_Years = 0;
            }
        }

    }


    public static void Set_Insd_Illus_Parameters(IllustrationInfo info) {
        s = Module2.SpouseClient;

        if (info.IsChildRiderSelected)
        {
            if (info.ChildRiderYoungestAge == 0)
            {
                Module2.LSClient[s].ChildRider = 0;
            }
            else
            {
                Module2.LSClient[s].ChildRider = 1;
            }
            Module2.LSChildRider.AgeYoungest = info.ChildRiderYoungestAge;
            Module2.LSChildRider.FaceAmt = Convert.ToInt32(info.ChildRiderDeathBenefitAmount);
        }
        else
        {
            Module2.LSClient[s].ChildRider = 0;
        }

        var insured = info.Riders.FirstOrDefault(x => x.IndividualType == IndividualData.KnownIndices.Spouse);
        if (insured != null)
        {
            if (insured.Age == 0)
            {
                Module2.LSClient[s].SpouseRider = 0;
            }
            else
            {
                Module2.LSClient[s].SpouseRider = 1;
            }
            Module2.LSSpouseRider.IssueAge = insured.Age;
            Module2.LSSpouseRider.FaceAmt = Convert.ToInt64(insured.FaceAmount);
            Module2.LSSpouseRider.PlanCode = insured.PlanType == null ? 0 : insured.PlanType.Id;
            Module2.LSSpouseRider.SubStd = insured.SubstandardRate == null ? 0 : insured.SubstandardRate.Id;
            Module2.LSSpouseRider.COLA = insured.Cola == null ? 0 : insured.Cola.Id;
            Module2.LSSpouseRider.RemoveYear = insured.YearToAgeOption.Id == 1 ?
                !insured.EndYear.HasValue ? 0 : insured.EndYear.Value - insured.Age
                : insured.EndYear ?? 0;
        }
        else
        {
            Module2.LSClient[s].SpouseRider = 0;
        }

        var childRiders = info.Riders.Where(x => x.IndividualType == IndividualData.KnownIndices.Child).ToList();
        for (i = 0; i < 9; i++)
        {
            var rider = childRiders[i];
            if (rider.IsSelected)
            {
                Module2.LSClient[s].InsuredRider[i + 1] = 1;
                Module2.LSInsuredRider[i + 1].IssueAge = rider.Age;
                switch (rider.Gender.Abbreviation.ToString())
                {
                    case "M":
                        Module2.LSInsuredRider[i].SexCode = "M";
                        break;
                    case "F":
                        Module2.LSInsuredRider[i].SexCode = "F";
                        break;
                    default:
                        Module2.LSInsuredRider[i].SexCode = " ";
                        Module2.LSClient[s].InsuredRider[i] = 0;
                        break;
                }
                Module2.LSInsuredRider[i + 1].FaceAmt = Convert.ToInt64(rider.FaceAmount);
                Module2.LSInsuredRider[i].PlanCode = rider.PlanType == null ? 0 : rider.PlanType.Id;
                Module2.LSInsuredRider[i].SubStd = rider.HasSubstandardRate ? rider.SubstandardRate.Id : 0;
                Module2.LSInsuredRider[i].COLA = rider.Cola == null ? 0 : rider.Cola.Id;
                Module2.LSInsuredRider[i].StartYear = rider.YearToAgeOption.Id == 1 ? rider.AgeOrYear.Value - rider.Age : rider.AgeOrYear.Value;
                Module2.LSInsuredRider[i].EndYear = rider.RemoveYearToAge.Id == 1 ? rider.EndYear.Value - rider.Age : rider.EndYear.Value;
            }
            else
            {
                Module2.LSClient[s].InsuredRider[i + 1] = 0;
            }
        }

    }

    public static void Set_Monthly_Charge(int typeill) {
        if (Module2.LSClient[Module2.SpouseClient].FaceAmt <= 199999)
        {
            if (typeill == 1)
            {
                Module2.POLFEE = 100;
            }
            else
            {
                Module2.POLFEE = 85;
            }
        }
        else if (Module2.LSClient[Module2.SpouseClient].FaceAmt.IsBetween(200000, 999999, true))
        {
            Module2.POLFEE = 60;
        }
        else
        {
            Module2.POLFEE = 20;
        }
    }


    public static void Set_Premium_Illus_Parameters(IllustrationInfo info) {
        s = Module2.SpouseClient;
        Module2.LSClient[s].CurrRate = Convert.ToSingle(info.PremiumInterestRate);
        if (Module2.LSClient[s].CurrRate > 0)
            info.ClientData.InterestRate = Module2.LSClient[s].CurrRate;
        Module2.LSClient[s].Mode = info.PremiumMode.DisplayedValue;
        Module2.LSClient[s].PlannedPrem = Convert.ToSingle(info.PlannedModalPremium);
        if (info.PremiumYearsToPay.DisplayedValue.Equals("20 Years", StringComparison.OrdinalIgnoreCase))
            Module2.LSClient[s].YearsToPay = 20;
        else if (info.PremiumYearsToPay.DisplayedValue.Equals("To Age 65", StringComparison.OrdinalIgnoreCase))
            Module2.LSClient[s].YearsToPay = 65 - info.ClientData.Age;
        else if (info.PremiumYearsToPay.DisplayedValue.Equals("To Age 95", StringComparison.OrdinalIgnoreCase))
            Module2.LSClient[s].YearsToPay = 95 - info.ClientData.Age;
        Module2.LSClient[s].LumpSum = Convert.ToSingle(info.InitialLumpSumAmount);
    }


    public static void Set_Spouse_Illus_Parameters(IllustrationInfo info) {
        Module2.LSClient[1].ClientName = info.SpouseAsClientData.FirstName + " " + (string.IsNullOrEmpty(info.SpouseAsClientData.MiddleInitial) ? string.Empty : info.SpouseAsClientData.MiddleInitial + " ") + info.SpouseAsClientData.LastName;
        Module2.LSClient[1].IssueAge = info.SpouseAsClientData.Age;
        if (Module2.LSClient[1].IssueAge + Module2.LSClient[1].YearsToPay > 95)
        {
            Module2.LSClient[1].YearsToPay = 95 - Module2.LSClient[1].IssueAge;
        }
        Module2.LSClient[1].SexCode = info.SpouseAsClientData.Gender.Abbreviation.ToString();
        Module2.LSClient[1].FaceAmt = Convert.ToInt64(info.SpouseAsClientData.InitialDeathBenefit);
        Module2.LSClient[1].PlanCode = info.SpouseAsClientData.PlanType.Id;
        Module2.LSClient[1].State = info.SpouseAsClientData.State.Abbreviation;
        Module2.LSClient[1].OptionX = info.SpouseAsClientData.BenefitOption.Id;
        Module2.LSClient[1].SubStd = info.SpouseAsClientData.HasSubstandardRate ? info.SpouseAsClientData.SubstandardRate.Id : 0;
        Module2.LSClient[1].WPD = info.SpouseAsClientData.HasWpd ? info.SpouseAsClientData.Wpd.Id : 0;
        Module2.LSClient[1].GPO = info.SpouseAsClientData.HasGpo ? 4000 + (1000 * info.SpouseAsClientData.Gpo.Id) : 0;
        Module2.LSClient[1].COLA = info.SpouseAsClientData.Cola == null ? 0 : info.SpouseAsClientData.Cola.Id;
        Module2.LSClient[1].AddFace = Convert.ToInt64(info.SpouseAsClientData.InitialDeathBenefit);
        Module2.LSClient[1].AddFaceStart = 1;
        //Always 1st year if exist
        switch (info.SpouseAsClientData.YearToAgeOption.Id)
        {
            case 1:
                Module2.LSClient[1].AddFaceEnd = info.SpouseAsClientData.EndYear.Value - info.SpouseAsClientData.Age;
                break;
            default:
                Module2.LSClient[1].AddFaceEnd = info.SpouseAsClientData.EndYear.Value;
                break;
        }
        Module2.LSClient[1].CurrRate = Convert.ToSingle(info.SpouseAsClientData.InterestRate);
        Module2.LSClient[1].SpouseRider = 0;
        Module2.LSClient[1].ChildRider = 0;
        for (year = 1; year <= 9; year++)
        {
            Module2.LSClient[1].InsuredRider[year] = 0;
        }
        Module2.LSClient[1].IllusClass = 1;
        Module2.LSClient[1].PrintPages = 3;
        Module2.LSClient[1].ContactName = info.PreparedBy;
        Module2.LSClient[1].ContactPhone = info.ContactTelephoneNumber;

    }


    public static void Spouse_Rider_Mortality(IAppData data, int GuideSingle) {
        float Spouse_Adj_Face = 0;
        float returnRate = 0;
        int Spouse_Age = 0;
        float[] Temp_Mort = new float[2];

        Spouse_Adj_Face = Module2.LSSpouseRider.FaceAmt;
        Spouse_Age = (Module2.AnnYr - Module2.LSClient[Module2.SpouseClient].IssueAge) + Module2.LSSpouseRider.IssueAge;
        if (Spouse_Age > MAXAGE || Spouse_Age >= (Module2.LSSpouseRider.IssueAge + Module2.LSSpouseRider.RemoveYear))
            return;
        if (Spouse_Age > 64 && Verify_LSSPOUSE(data, Module2.LS_RateFiles[10], Spouse_Age, ref returnRate))
            Spouse_Adj_Face = Spouse_Adj_Face * returnRate;

        if (GuideSingle != 0)
        {
            Temp_Mort[0] = Module2.LSMort[0, Module2.AnnYr].Spouse_CSO80 < Module2.LSMort[0, Module2.AnnYr].Spouse_Base
                ? Module2.LSMort[0, Spouse_Age].Spouse_CSO80
                : Module2.LSMort[0, Spouse_Age].Spouse_Base;
            Temp_Mort[1] = Module2.LSMort[1, Spouse_Age].Spouse_CSO80 < Module2.LSMort[1, Spouse_Age].Spouse_Base
                ? Module2.LSMort[1, Spouse_Age].Spouse_CSO80
                : Module2.LSMort[1, Spouse_Age].Spouse_Base;
        }
        else
        {
            Temp_Mort[0] = Module2.LSMort[0, Spouse_Age].Spouse_Base;
            Temp_Mort[1] = Module2.LSMort[1, Spouse_Age].Spouse_Base;
        }

        if (Spouse_Age == Module2.LSSpouseRider.IssueAge)
        {
            Module2.LS_Insureds_Mortcost = Module2.LS_Insureds_Mortcost + ((Spouse_Adj_Face * Temp_Mort[1]) / 1000);
            if (Module2.LSSpouseRider.SubStd > 0)
                Module2.LS_Insureds_Mortcost = Module2.LS_Insureds_Mortcost + ((Spouse_Adj_Face * Module2.LSMort[1, Spouse_Age].Spouse_Sub) / 1000);
        }
        else
        {
            Module2.LS_Insureds_Mortcost = Module2.LS_Insureds_Mortcost + ((Spouse_Adj_Face * Temp_Mort[Module2.GuarCurr]) / 1000);
            if (Module2.LSSpouseRider.SubStd > 0)
                Module2.LS_Insureds_Mortcost = Module2.LS_Insureds_Mortcost + ((Spouse_Adj_Face * Module2.LSMort[Module2.GuarCurr, Spouse_Age].Spouse_Sub) / 1000);
        }
    }


    public static void SpouseSave(IllustrationInfo info) {
        int RetCode = 0;
        string IBirth = null;

        if (Module2.SpouseChanged != 0 && !info.SpouseAsClientData.FirstName.StartsWith(" ") && !string.IsNullOrEmpty(info.SpouseAsClientData.FirstName))
        {
            Module2.ClientInfo.FirstName = info.SpouseAsClientData.FirstName;
            Module2.ClientInfo.Initial = info.SpouseAsClientData.MiddleInitial.ToString();
            Module2.ClientInfo.LastName = info.SpouseAsClientData.LastName;
            Module2.ClientInfo.Gender = info.SpouseAsClientData.Gender.Abbreviation.ToString();
            Module2.ClientInfo.State = info.SpouseAsClientData.State.Abbreviation;
            IBirth = info.SpouseAsClientData.BirthDate.HasValue ? info.SpouseAsClientData.BirthDate.Value.ToShortDateString() : string.Empty;
            Module2.set_insd_birthdt(IBirth);
            Module2.ClientInfo.BirthDate = Module2.InsuredInfo.InsBirthDate;
            Module2.ClientInfo.ClientNumber = 0;
            RetCode = Module2.client_file(5, Module2.ClientInfo.ClientNumber);
            if (RetCode == 0)
            {
                Interaction.MsgBox(" Save for Spouse information failed; Spouse information has not been saved ", 0);
                return;
            }
            Module2.InsuredInfo.InsFName = info.SpouseAsClientData.FirstName + " " + info.SpouseAsClientData.LastName;
            IBirth = info.SpouseAsClientData.BirthDate.HasValue ? info.SpouseAsClientData.BirthDate.Value.ToShortDateString() : string.Empty;
            Module2.set_insd_birthdt(IBirth);
            Module2.InsuredInfo.InsGender = info.SpouseAsClientData.Gender.Abbreviation.ToString();
            Module2.InsuredInfo.InsRelation = "P";
            if (info.SpouseAsClientData.PlanType == null)
            {
                Module2.InsuredInfo.insTobacco = "N";
            }
            else
            {
                Module2.InsuredInfo.insTobacco = "Y";
            }
            RetCode = Module2.insured_file(5, Module2.ClientInfo.ClientNumber, 0);
            Module2.SpouseChanged = 0;
        }

    }


    public static void Sum_Cost_Indices() {
        float Power = 0;
        double CostFactor = 0;
        float Temp_DB = 0;

        Temp_DB = Convert.ToSingle(Module2.LS_DB[Module2.GuarCurr] + Module2.LS_COLA_Client);
        Power = Module2.AnnYr - Module2.LSClient[Module2.SpouseClient].IssueAge + 1;
        if (Power <= 11)
        {
            if (Power != 11)
            {
                Module2.LS_Index_OneYear_Cost[Module2.GuarCurr, 0] = Module2.LS_Index_OneYear_Cost[Module2.GuarCurr, 0] - (Module2.LS_Insureds_Mortcost * 1.081081f);
                Module2.LS_Index_OneYear_Cost[Module2.GuarCurr, 0] = Module2.LS_Index_OneYear_Cost[Module2.GuarCurr, 0] + Module2.LS_Monthly_Prem;
                if (Module2.AnnMo == 12)
                {
                    CostFactor = System.Math.Pow(1.05, (11d - Power));
                    Module2.LS_Index_Numerator_NP[Module2.GuarCurr, 0] = Module2.LS_Index_Numerator_NP[Module2.GuarCurr, 0] + Module2.LS_Index_OneYear_Cost[Module2.GuarCurr, 0] * Convert.ToSingle(CostFactor);
                    Module2.LS_Index_OneYear_Cost[Module2.GuarCurr, 0] = 0;
                    if (Module2.GuarCurr == 0)
                    {
                        Module2.LS_Index_Denominator[0] = Module2.LS_Index_Denominator[0] + Temp_DB * Convert.ToSingle(CostFactor);
                    }
                }
            }
            else
            {
                Module2.LS_Index_Numerator_SC[Module2.GuarCurr, 0] = Module2.LS_Index_Numerator_NP[Module2.GuarCurr, 0] - Convert.ToSingle(Module2.LS_CSV[Module2.GuarCurr]);
            }
        }
        if (Power != 21)
        {
            if (Power <= 10)
            {
                Module2.LS_Index_OneYear_Cost[Module2.GuarCurr, 1] = Module2.LS_Index_OneYear_Cost[Module2.GuarCurr, 1] - (Module2.LS_Insureds_Mortcost * 1.081081f);
            }
            else
            {
                Module2.LS_Index_OneYear_Cost[Module2.GuarCurr, 1] = Module2.LS_Index_OneYear_Cost[Module2.GuarCurr, 1] - (Module2.LS_Insureds_Mortcost * 1.025641f);
            }
            Module2.LS_Index_OneYear_Cost[Module2.GuarCurr, 1] = Module2.LS_Index_OneYear_Cost[Module2.GuarCurr, 1] + Module2.LS_Monthly_Prem;
            if (Module2.AnnMo == 12)
            {
                CostFactor = System.Math.Pow(1.05d, (21d - Convert.ToDouble(Power)));
                Module2.LS_Index_Numerator_NP[Module2.GuarCurr, 1] = Module2.LS_Index_Numerator_NP[Module2.GuarCurr, 1] + Module2.LS_Index_OneYear_Cost[Module2.GuarCurr, 1] * Convert.ToSingle(CostFactor);
                Module2.LS_Index_OneYear_Cost[Module2.GuarCurr, 1] = 0;
                if (Module2.GuarCurr == 0)
                {
                    Module2.LS_Index_Denominator[1] = Module2.LS_Index_Denominator[1] + Temp_DB * Convert.ToSingle(CostFactor);
                }
            }
        }
        else
        {
            Module2.LS_Index_Numerator_SC[Module2.GuarCurr, 1] = Module2.LS_Index_Numerator_NP[Module2.GuarCurr, 1] - Convert.ToSingle(Module2.LS_CSV[Module2.GuarCurr]);
        }

    }

    public static bool Verify_CSO80(IAppData data, int inFile, string sex, string smoke, int age, ref float returnRate) {
        returnRate = 0;

        sex = sex.ToUpper();
        smoke = smoke.ToUpper();

        if (!"MF".Contains(sex) || !"SN".Contains(smoke) || !age.IsBetween(15, MAXAGE, true))
            return false;

        recordNo = age + 1;

        dynamic value = DataAccess.GetRateCS80(recordNo, data);
        if (value == null)
            return false;

        returnRate = sex == "M"
            ? smoke == "N" ? value.MaleNonSmoker : value.MaleNonSmoker
            : smoke == "N" ? value.MaleNonSmoker : value.MaleNonSmoker;

        return true;
    }

    public static bool Verify_LSCORR(IAppData data, int inFile, int age, ref float returnRate) {
        int RecordNo = 0;
        returnRate = 0;
        RecordNo = age > 95 ? 56 : age < 40 ? 1 : age - 39;

        dynamic value = DataAccess.GetRateLsCorr(RecordNo, data);
        if (value == null)
            return false;

        returnRate = value.Value;
        return true;

    }

    public static bool Verify_LSMINP(IAppData data, int inFile, string sex, string smoke, int age, int band, ref float returnRate) {
        returnRate = 0;
        sex = sex.ToUpper();
        smoke = smoke.ToUpper();

        if (!"MF".Contains(sex) || !"SN".Contains(smoke) || age > 70 || !band.IsBetween(1, 4, true))
            return false;

        recordNo = age + 1;
        dynamic value = DataAccess.GetRateMinp(recordNo, data);
        if (value == null)
            return false;

        returnRate = sex == "M"
            ? smoke == "N" ? value.MaleNonSmoker[band] : value.FemaleSmoker[band]
            : smoke == "N" ? value.FemaleNonSmoker[band] : value.FemaleSmoker[band];
        return true;
    }

    public static bool Verify_LSRATEGP(IAppData data, int inFile, int age, ref float returnRate) {
        returnRate = 0;
        if (age > 40)
            return false;

        recordNo = age + 1;

        dynamic value = DataAccess.GetRateGp(recordNo, data);
        if (value == null)
            return false;

        returnRate = value.Value;
        return true;
    }

    public static bool Verify_LSRATEPR(IAppData data, int inFile, string who, string sex, string smoke, int age, int band, ref float returnRate) {
        returnRate = 0;
        who = Strings.UCase(who);
        sex = sex.ToUpper();
        smoke = smoke.ToUpper();

        if ((who != "P") || !"MF".Contains(sex) || !"SN".Contains(smoke) || age > MAXAGE || !band.IsBetween(1, 5, true))
            return false;

        recordNo = age + 1;
        var value = DataAccess.GetRatePR(recordNo, data);
        if (value == null)
            return false;

        returnRate = sex == "M"
            ? smoke == "N" ? value.MaleNonSmoker[band - 1] : value.MaleSmoker[band - 1]
            : smoke == "N" ? value.FemaleNonSmoker[band - 1] : value.FemaleSmoker[band - 1];

        return true;
    }

    public static bool Verify_LSRATESB(IAppData data, int inFile, string smoke, int age, ref float returnRate) {
        returnRate = 0;
        int RecordNo = 0;
        smoke = smoke.ToUpper();

        if (!"SN".Contains(smoke) || age > MAXAGE)
            return false;

        RecordNo = age + 1;
        dynamic value = DataAccess.GetRateSB(RecordNo, data);
        if (value == null)
            return false;

        returnRate = smoke == "N" ? value.NonSmokerSubstandard : value.SmokerSubstandard;

        return false;
    }

    public static bool Verify_LSRATESI(IAppData data, int inFile, string who, string sex, string smoke, int age, int band, ref float returnRate) {
        returnRate = 0;

        who = who.ToUpper(); ;
        sex = sex.ToUpper();
        smoke = smoke.ToUpper();

        if (!"SI".Contains(who) || !"MF".Contains(sex) || !"SN".Contains(smoke) || age > MAXAGE || !band.IsBetween(1, 2, true))
            return false;

        recordNo = age + 1;
        var value = DataAccess.GetRateSI(recordNo, data);
        if (value == null)
            return false;


        returnRate = sex == "M"
            ? smoke == "N" ? value.MaleNonSmoker[band - 1] : value.MaleSmoker[band - 1]
            : smoke == "N" ? value.FemaleNonSmoker[band - 1] : value.FemaleSmoker[band - 1];

        return true;
    }

    public static bool Verify_LSRATEWP(IAppData data, int inFile, string sex, int age, ref float returnRate) {
        returnRate = 0;
        sex = sex.ToUpper();

        if (!"MF".Contains(sex) || !age.IsBetween(15, 59, true))
            return false;

        recordNo = age + 1;

        dynamic value = DataAccess.GetRateWP(recordNo, data);
        if (value == null)
            return false;

        returnRate = sex == "M" ? value.MaleWPD : value.FemaleWPD;

        return true;
    }

    public static bool Verify_LSSPOUSE(IAppData data, int inFile, int age, ref float returnRate) {
        returnRate = 0;

        if (age < 65)
            return false;

        if (age > 95)
            recordNo = 31;
        else
            recordNo = age - 64;

        dynamic value = DataAccess.GetRateLsSpouse(recordNo, data);
        if (value == null)
            return false;

        returnRate = value.Value;
        return true;
    }

    public static bool Verify_LSSURR(IAppData data, int inFile, string who, string sex, int age, int duration, ref float returnRate) {
        returnRate = 0;

        who = who.ToUpper();
        sex = sex.ToUpper();

        if ((who != "P") || !"MF".Contains(sex) || age > 70 || duration > 16)
            return false;

        recordNo = age + 1;
        var value = DataAccess.GetRateLsSurr(recordNo, data);
        if (value == null)
            return false;

        returnRate = sex == "M" ? value.MaleSurr[duration] : value.FemaleSurr[duration];

        return false;
    }

    public static bool Verify_LSTARG(IAppData data, int inFile, string sex, int age, ref float returnRate) {
        returnRate = 0;
        sex = sex.ToUpper();

        if (!"MF".Contains(sex) || age > 70)
            return false;

        recordNo = age + 1;
        dynamic value = DataAccess.GetRateTarg(recordNo, data);
        if (value == null)
            return false;

        returnRate = sex == "M" ? value.MaleTarget : value.FemaleTarget;

        return false;
    }
}