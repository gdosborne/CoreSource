
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;

public static class Module2 {
    private static string low_temp;
    private static string hi_temp;
    private static long low_ymd;
    private static long hi_ymd;
    private static int calcyr;
    private static int Cage;
    private static long temp_dt;
    private static long curr_dt;
    private static long ent_date;
    private static string amtstr;
    private static string oneline;
    private static string Xmm;
    private static string Xdd;

    private static string Xyy;

    public const int KEY_BACK = 0x8;
    public const int KEY_TAB = 0x9;
    public const int KEY_RETURN = 0xd;
    public const int KEY_ESCAPE = 0x1b;
    public const int KEY_PRIOR = 0x21;
    public const int KEY_NEXT = 0x22;
    public const int KEY_END = 0x23;
    public const int KEY_HOME = 0x24;
    public const int KEY_LEFT = 0x25;
    public const int KEY_UP = 0x26;
    public const int KEY_RIGHT = 0x27;
    public const int KEY_DOWN = 0x28;
    public const int KEY_INSERT = 0x2d;
    public const int KEY_F1 = 0x70;
    public const int KEY_F2 = 0x71;
    public const int KEY_F3 = 0x72;
    public const int KEY_F4 = 0x73;
    public const int KEY_F5 = 0x74;
    public const int KEY_F6 = 0x75;
    public const int KEY_F7 = 0x76;
    public const int KEY_F8 = 0x77;
    public const int KEY_F9 = 0x78;
    public const int KEY_F10 = 0x79;
    public const int KEY_F11 = 0x7a;
    public const int KEY_F12 = 0x7b;
    // Function Parameters
    // MsgBox parameters
    // OK button only
    public const int MB_OK = 0;
    // OK and Cancel buttons
    public const int MB_OKCANCEL = 1;
    // Abort, Retry, and Ignore buttons
    public const int MB_ABORTRETRYIGNORE = 2;
    // Yes, No, and Cancel buttons
    public const int MB_YESNOCANCEL = 3;
    // Yes and No buttons
    public const int MB_YESNO = 4;
    // Retry and Cancel buttons
    public const int MB_RETRYCANCEL = 5;

    // Critical message
    public const int MB_ICONSTOP = 16;
    // Warning query
    public const int MB_ICONQUESTION = 32;
    // Warning message
    public const int MB_ICONEXCLAMATION = 48;
    // Information message
    public const int MB_ICONINFORMATION = 64;
    [DllImport("User", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]

    // Functions and Constants that enable interfacing with AmriCare

    public static extern int GetWindow(int hWnd, int wCmd);
    [DllImport("User", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern int GetWindowText(int hWnd, string lpString, int nMaxCount);
    [DllImport("User", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern int GetWindowTextLength(int hWnd);
    [DllImport("User", EntryPoint = "SetFocus", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern int SetFocusAPI(int hWnd);
    [DllImport("User", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern int ShowWindow(int hWnd, int nCmdShow);
    [DllImport("Kernel", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]

    public static extern int GetPrivateProfileInt(string lpApplicationName, string lpKeyName, int nDefault, string lpFileName);
    [DllImport("Kernel", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern int GetPrivateProfileString(string lpApplicationName, string lpKeyName, string lpDefault, string lpReturnedString, int nSize, string lpFileName);
    [DllImport("Kernel", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern int GetProfileString(string lpAppName, string lpKeyName, string lpDefault, string lpReturnedString, int nSize);
    [DllImport("Kernel", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern int WritePrivateProfileString(string lpApplicationName, string lpKeyName, string lpString, string lpFileName);

    public const int GW_CHILD = 5;
    public const int GW_HWNDFIRST = 0;
    public const int GW_HWNDLAST = 1;
    public const int GW_HWNDNEXT = 2;
    public const int GW_HWNDPREV = 3;
    public const int GW_HWNOWNER = 4;
    public const int SW_SHOWNORMAL = 1;

    public const int SW_SHOWMAXIMIZED = 3;
    // Window API const int ants
    public const int HELP_QUIT = 2;
    public const int HELP_INDEX = 3;
    public const int HELP_HELPONHELP = 4;

    public const int HELP_PARTIALKEY = 0x105;
    [DllImport("User", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    public static extern int WinHelp(int hWnd, string lpHelpFile, int wCommand, object dwData);

    //Common Dialog Control
    //Action Property
    public const int DLG_FONT = 4;

    public const int DLG_PRINT = 5;
    //Fonts Dialog Flags
    public const long CF_SCREENFONTS = 0x1L;
    public const long CF_PRINTERFONTS = 0x2L;
    public const long CF_BOTH = 0x3L;
    public const long CF_SHOWHELP = 0x4L;
    public const long CF_APPLY = 0x200L;
    public const long CF_LIMITSIZE = 0x2000L;
    public const long CF_FIXEDPITCHONLY = 0x4000L;
    public const int CF_TTONLY = 0x40000;
    public const int CF_FORCEFONTEXIST = 0x10000;

    public const int CF_NOSIZESEL = 0x200000;
    //Printer Dialog Flags
    public const long PD_PRINTSETUP = 0x40L;
    public const long PD_NOWARNING = 0x80L;

    public const long PD_SHOWHELP = 0x800L;
    // User Public Defines
    public const double RoundingFactor = 0.005;
    public static string defPrintFont;
    public static float defFontSiz;
    public static int defFontBold;

    public static int defFontItal;
    public class ClientData {
        public int ClientNumber;
        public string FirstName;
        public string Initial;
        public string LastName;
        public string Address1;
        public string Address2;
        public string City;
        public string State;
        public int StateNum;
        public long Zipcode;
        public string Phone1;
        public string phone2;
        public string Gender;
        public long BirthDate;
        public string Note;
    }

    public class AgtData {
        public string FullName;
        public string Address1;
        public string Address2;
        public string City;
        public string State;
        public long Zipcode;
        public string HomePhone;
        public string OfficePhone;
        public int Office;
        public int AgtState;
        public long Number;
        public long Record;
        public string Sales;
    }

    public class AgentExt {
        public long Record;
        public string FullName;
        public string OfficePhone;
        public string Sales;
    }

    public class InsuredData {
        public int InsClientNumber;
        public int InsNumber;
        public string InsFName;
        public string InsGender;
        public long InsBirthDate;
        public string InsRelation;
        public string insTobacco;
        public int insRating;
        public int InsHeight;
        public int InsWeight;
    }

    public static int Extended;
    public static string ClientFile;
    public static string InsuredFile;
    public static string formcode;
    public static AgentExt AgtInfo;
    public static ClientData ClientInfo;
    public static int ClientChanged;
    public static long Record_Number;
    public static int NoAgent;
    public static int NewAgent;
    public static int client_data;
    public static int No_Clients;
    public static string msgfile;
    public static InsuredData InsuredInfo;
    public static int insured_data;
    public static int insd_key;
    public static string BDSTR;
    public static int ReportClass;
    public static int[] InsuredArray;
    public static string desc;
    public static string file_prefix;
    public static string NL;
    public static int PrinterSw;
    public static double[] tot_prem;
    public static string[] strArray;
    public static int ClientOnly;
    public static object savPrintFont;
    public static float savFontSize;
    public static float savFontBold;
    public static float savFontItalic;

    public static int defSpacing;
    //declares for Life Illustrations in general
    public static int LifeIllusType;
    public static string[] PrintIllusPage = new string[6];
    public static string CommandString;

    public static string CallFromMain;
    //declares for Tailored Term forms and modules

    public class TTIllusParm {
        public string ClientName;
        public int IssueAge;
        public string SexCode;
        public long FaceAmt;
        public string Smoker;
        public string UnisexRate;
        public string Rate;
        public string State;
        public int IllusClass;
        public int PrintPages;
        public string ContactName;
        public string ContactPhone;
    }


    public static TTIllusParm TTInfo;
    public static double[,] NPINDEX = new double[3, 3];
    public static double[,] TTRATE = new double[3, 2];
    public static double[,] SPRATE = new double[4, 3];
    public static double[] GUARMAX = new double[15];
    public static double[,] TTPREM = new double[3, 2];
    public static double[,] SPPREM = new double[4, 3];

    public static double[] GUARPREM = new double[15];
    public static double POLFEE;
    public static int LIFEPAGE;
    public static int LIFELINE;
    public static int ONERATE;
    public static int TTermPage;
    public static int AgeChanged;
    public static int BirthChanged;
    public static int SpouseChanged;
    public static string SaveMsgFile;
    public static int ClientSave_ReturnCode;

    public static int SpouseIllus_ReturnCode;
    //declares for Life Savings forms and modules


    public const double LS_LoanRate = 0.06;
    public class LSClientParameters {
        public string ClientName;
        public int IssueAge;
        public string SexCode;
        public long FaceAmt;
        public int PlanCode;
        public string State;
        public int OnOnOnOnOnOn;
        public int SubStd;
        public int WPD;
        public long GPO;
        public int COLA;
        public long AddFace;
        public int AddFaceStart;
        public int AddFaceEnd;
        public float CurrRate;
        public string Mode;
        public float PlannedPrem;
        public int YearsToPay;
        public float LumpSum;
        public int SpouseRider;
        public int ChildRider;
        public int[] InsuredRider;
        public int FutureChanges;
        public int FutureWD;
        public int IllusClass;
        public int[] HighAges;
        public int PrintPages;
        public string ContactName;
        public string ContactPhone;
        public int OptionX;
        public int IllusType;
    }


    public static LSClientParameters[] LSClient = new LSClientParameters[2];
    public class LSSpouseRiderParm {
        public int IssueAge;
        public long FaceAmt;
        public int PlanCode;
        public int SubStd;
        public int COLA;
        public int RemoveYear;
    }


    public static LSSpouseRiderParm LSSpouseRider;
    public class LSChildRiderParm {
        public int AgeYoungest;
        public int FaceAmt;
    }


    public static LSChildRiderParm LSChildRider;
    public class LSInsuredRiderParm {
        public int IssueAge;
        public string SexCode;
        public long FaceAmt;
        public int PlanCode;
        public int SubStd;
        public int COLA;
        public int StartYear;
        public int EndYear;
    }


    public static LSInsuredRiderParm[] LSInsuredRider = new LSInsuredRiderParm[9];
    public class LSCalculatedPremiums {
        public float PrinMin;
        public float SpouseMin;
        public float ChildMin;
        public float[] InsdMin;
        public float PrinTarg;
        public float SpouseTarg;
        public float ChildTarg;
        public float[] InsdTarg;
        public float TotMin;
        public float TotTarg;
        public float GuideAnnual;
        public float GuideSingle;
        public float GuideSingleAddInsd;
    }


    public static LSCalculatedPremiums CalcPrem;
    public class LSLedgerValues {
        public float AnnualOutlay;
        public float[] SurrenderValue;
        public float[] CashValue;
        public float[] DeathBenefit;
        public float WithdrawAmount;
        public float LoanAmount;
        public float LoanRepayAmount;

        public float LoanBalance;
        //Public Shared Widening Operator CType(v As LSLedgerValues) As LSLedgerValues
        //    Throw New NotImplementedException()
        //End Operator
    }


    public static LSLedgerValues[] LSLedger = new LSLedgerValues[100];
    public class LSFutureChanges {
        public int DB_Age;
        public float DB_Amount;
        public int Prem_Age;
        public float Prem_Amount;
        public int Int_Age;
        public float Int_Amount;
        public int Opt_Age;
        public int Opt_Class;
        public int Opt_Type;
    }


    public static LSFutureChanges[] LSFuture = new LSFutureChanges[5];
    public class LSFutureWithdrawals {
        public int WD_Age;
        public float WD_Amount;
        public int WD_Years;
        public int Loan_Age;
        public float Loan_Amount;
        public int Loan_Years;
        public int Loan_Interest;
        public int LoanPay_Age;
        public float LoanPay_Amount;
        public int LoanPay_Years;
    }


    public static LSFutureWithdrawals[] LSFutureWD = new LSFutureWithdrawals[5];
    public class LSMortalityRates {
        //1 to 4 bands for Prin
        public float[] Prin_Base;
        //1 to 4 bands for Prin
        public float[] Prin_Sub;
        public float Prin_GPO;
        public float Prin_WPD;
        public float Prin_CSO80;
        public float Spouse_Base;
        public float Spouse_Sub;
        public float Spouse_CSO80;
        //0 for Prin term rider
        public float[] Insured_Base;
        //0 for Prin term rider
        public float[] Insured_Sub;
        //0 for Prin term rider
        public float[] Insured_CSO80;
    }


    public static LSMortalityRates[,] LSMort = new LSMortalityRates[2, Module1.MAXAGE];
    public static int Need_Ledger;
    public static int Need_Guideline;
    public static int Need_Target_CV;
    //Client or Spouse illustration
    public static int SpouseClient;
    public static int[] LS_RateFiles = new int[12];
    //Ledger Year
    public static int AnnYr;
    //Ledger Month
    public static int AnnMo;
    //Guaranteed or Current
    public static int GuarCurr;

    //Current Principal DB band
    public static int LS_Prin_Band;
    //Death Benefit Option
    public static int LS_DBOption;
    //Cash Surr Value (Guar or Curr)
    public static double[] LS_CSV = new double[2];
    //Cash Value (Guar or Curr)
    public static double[] LS_CV = new double[2];
    //Client Death Benefit (Guar or Curr)
    public static double[] LS_DB = new double[2];
    //Client minimum death benefit (Guar or Curr)
    public static double[] LS_DB_Minimum = new double[2];
    //Client cash valu up to the time Option 2 started
    public static double[] LS_CV_Opt2_Start = new double[2];
    //Loan Balance (Guar or Curr)
    public static double[] LS_LoanBal = new double[2];
    //Premium or Loan Pay, per year
    public static double LS_Outlay;
    //Policy Fee, Premium Charge (per year)
    public static double LS_Charges;
    //Cash Surrender (per year)
    public static double LS_Withdraw;
    //Cash Value Loan (per year)
    public static double LS_LoanAmt;
    //Loan repayment (per year)
    public static double LS_LoanPay;
    //Current outstanding loan balance
    public static double LS_LoanBalance;
    //Interest Rate (Guar or Curr)
    public static float[] LS_IRate = new float[2];
    //Modal Premium (per year)
    public static float LS_Modal_Prem;
    //Premium paid monthly (value or zero)
    public static float LS_Monthly_Prem;
    //Current mortality rate
    public static float LS_MortRate;
    //Cost of Insurance for Insureds (per year)
    public static float LS_Insureds_Mortcost;
    //one year cost for indices (10 or 20 years, guar or curr)
    public static float[,] LS_Index_OneYear_Cost = new float[2, 2];
    //Surrender Cost numerator
    public static float[,] LS_Index_Numerator_SC = new float[2, 2];
    //Net Payment numerator
    public static float[,] LS_Index_Numerator_NP = new float[2, 2];
    //Denominator for both SC and NP
    public static float[] LS_Index_Denominator = new float[2];
    //COLA increase for client (Cumulative)
    public static float LS_COLA_Client;
    //COLA increase for spouse rider (Cumulative)
    public static float LS_COLA_Spouse;
    //COLA increase for insured rider (Cumulative)
    public static float[] LS_COLA_Insured = new float[10];
    //Public LS_Rider_Client As Single    'Rider Death Benefit for Client, by year
    //CSC override for prin face amt
    public static int defFaceLimit;

    public static int State_Verify(string state_code) {
        int functionReturnValue = 0;
        //On Local Error GoTo StateErr
        string filepath = null;

        filepath = "C:\\aric\\life\\";
        //filepath = "X:\VB30\LS2 PAD\"
        dynamic found = false;
        functionReturnValue = 1;
        //FileNum% = FreeFile()
        if (state_code == "  ")
            return functionReturnValue;
        // Open filepath & "stateabr.txt" For Input As FileNum%
        //Do While Not EOF(FileNum%)
        //    tempabr$ = Input$(4, FileNum%)
        //    stateabr$ = Left$(tempabr$, 2)
        //    StateNum% = Val(Mid$(tempabr$, 3, 2))
        //    If stateabr$ = state_code$ Then
        //        State_Verify = StateNum%
        //        Close FileNum%
        //  Exit Function
        //    End If
        //Loop
        //Close FileNum%
        functionReturnValue = 1;
        dynamic msg = "Invalid State Abbrevation" + Strings.Chr(13) + "F1 for Help";
        Interaction.MsgBox(msg, 0);
        return functionReturnValue;
        //StateErr:
        //        Select Case Err()
        //            Case 6, 62
        //                State_Verify = True
        //                msg$ = "Invalid State Abbrevation" + Chr(13) + "F1 for Help"
        //                MsgBox msg$, 64
        //            Exit Function
        //            Case 53, 75, 76
        //                State_Verify = True
        //                MsgText = "State Code File not found.  Make sure that STATEABR.TEXT exists in the LIFE directory, and try again."
        //                MsgBox(MsgText), 64, "Americare Tailored Term"
        //            Exit Function
        //            Case Else
        //                State_Verify = True
        //                msg$ = Err() & " ERROR$" & " Global"
        //                MsgBox msg$, 0
        //            Exit Function
        //        End Select
    }


    public static void Verify_Life_Birth(int RetCode, string tempbirth) {
        RetCode = 1;
        dynamic mm = Strings.Left(tempbirth, 2);
        dynamic dd = Strings.Mid(tempbirth, 4, 2);
        dynamic yy = Strings.Right(tempbirth, 4);
        if (Conversion.Val(mm) < 1 | Conversion.Val(mm) > 12 | Conversion.Val(dd) < 1 | Conversion.Val(dd) > 31 | Conversion.Val(yy) > DateTime.Now.Year)
        {
            RetCode = 0;
            return;
        }

        switch (Conversion.Val(mm))
        {
            case 2:
                if (Conversion.Val(yy) % 4 != 0 | (Conversion.Val(yy) % 100 == 0 & Conversion.Val(yy) % 400 != 0))
                {
                    if (Conversion.Val(dd) > 28)
                    {
                        RetCode = 0;
                        return;
                    }
                }
                else
                {
                    if (Conversion.Val(dd) > 29)
                    {
                        RetCode = 0;
                        return;
                    }
                }
                break;
            case 4:
            case 6:
            case 9:
            case 11:
                if (Conversion.Val(dd) > 30)
                {
                    RetCode = 0;
                    return;
                }
                break;
            default:
                break;
        }

    }

    public static int client_file(int action, int recid) {
        int functionReturnValue = 0;

        //On Local Error GoTo Err_retrieve

        //filnum% = FreeFile()
        //FIL$ = Dir$(ClientFile$)
        //  If FIL$ = "" Then
        //     msg$ = "File Not Found - 'ClientFile$'"
        //     MsgBox msg$
        //     client_file = False
        //     Exit Function
        //  End If
        switch (action)
        {
            case 1:
                // retrieve data
                // Open ClientFile$ For Random As #filnum% Len = Len(ClientInfo)
                // Get #filnum%, recid%, ClientInfo
                functionReturnValue = 1;
                //Close #filnum%
                return functionReturnValue;
            case 5:
                //Save or add data
                // Open ClientFile$ For Random As #filnum% Len = Len(ClientInfo)
                //If recid% = 0 Then
                //    recid% = LOF(filnum%) / Len(ClientInfo) + 1
                //    ClientInfo.ClientNumber = recid%
                //    ClientInfo.City = Space$(20)
                //End If
                //Put #filnum%, recid%, ClientInfo
                functionReturnValue = 1;
                //Close #filnum%
                return functionReturnValue;
        }
        return functionReturnValue;
        //Err_retrieve:
        //        Select Case Err()
        //            Case 62
        //                msg$ = "Invalid Record Number on CLIENT file"
        //                MsgBox msg$, 0
        //        Case 63
        //                Resume Next
        //            Case Else
        //                msg$ = "Error Occured in retrieving client data  " + Str$(Err)
        //                MsgBox msg$, 0
        //    End Select
        //        client_file = False
        //        Exit Function
    }


    public static void Format_Amount(string amtstr, int amtlen, double amtval, string dollar) {
        amtstr = string.Format("{0:C}", amtval);
        //amtstr = Strings.Space(amtlen + 1);
        //string tempstr = Strings.Format(amtval, "##,###,##0.00");
        //Strings.Mid(amtstr, amtlen - Strings.Len(tempstr) + 2, Strings.Len(tempstr)) = tempstr;
        //if (dollar == "$")
        //{
        //    Strings.Mid(amtstr, amtlen - Strings.Len(tempstr) + 1, 1) = "$";
        //}

    }

    public static string GetAnyIniStr(string section, string key, string file) {
        string functionReturnValue = null;

        string RetVal = null;
        string AppName = null;
        int Worked = 0;

        RetVal = new string(Strings.Chr(0), 255);
        Worked = GetPrivateProfileString(section, key, "", RetVal, Strings.Len(RetVal), file);
        if (Worked == 0)
        {
            functionReturnValue = "";
        }
        else
        {
            functionReturnValue = Strings.Left(RetVal, Worked);
        }
        return functionReturnValue;

    }


    public static void Illus_Close_Text(string Illpage, int section, string phone, int typeill) {
        NL = Environment.NewLine;// Strings.Chr(13) + Strings.Chr(10);
        //carriage return, line feed

        //filnum% = FreeFile()

        switch (typeill)
        {
            case 1:
                break;
            // Open "tt\tt10clos.txt" For Input As #filnum%
            case 2:
                break;
            // Open "tt\tt15clos.txt" For Input As #filnum%
            default:
                break;
        }
        Insert_Phone:

        //Find the specified section

        // Do Until EOF(filnum%)
        //     Line Input #filnum%, oneline$
        //If Left$(oneline$, 1) = ">" Then
        //         If Val(Mid$(oneline$, 2)) = section% Then
        //             Exit Do
        //         End If
        //     End If
        // Loop

        //    If typeill = 1 Then
        //        Do While Not EOF(filnum%)
        //            Line Input #filnum%, oneline$
        //      If Left$(oneline$, 1) = ">" Then Exit Do
        //            If Mid$(oneline$, 44, 3) = "XXX" Then Insert_Phone
        //            Illpage$ = Illpage$ & oneline$ & NL$
        //        Loop
        //    Else
        //        Do While Not EOF(filnum%)
        //            Line Input #filnum%, oneline$
        //      If Left$(oneline$, 1) = ">" Then Exit Do
        //            If Mid$(oneline$, 44, 3) = "XXX" Then Insert_Phone
        //            Illpage$ = Illpage$ & oneline$ & NL$
        //        Loop
        //    End If
        //    Close #filnum%
        //Exit Sub


        //Mid$(oneline$, 44, 3) = Mid$(phone$, 1, 3)
        //Mid$(oneline$, 48, 3) = Mid$(phone$, 4, 3)
        //Mid$(oneline$, 52, 4) = Mid$(phone$, 7, 4)
        //If Val(Mid$(phone$, 11, 5)) <> 0 Then
        //    oneline$ = oneline$ & Space$(11)
        //    Mid$(oneline$, 56, 13) = ", extension "
        //    i% = 1
        //    Do While (i% < 6 And Mid$(phone$, 10 + i%, 1) <> "_")
        //        oneline$ = oneline$ & Mid$(phone$, 10 + i%, 1)
        //        i% = i% + 1
        //    Loop
        //    oneline$ = oneline$ & "." & NL$
        //End If
        return;

    }


    public static void Illus_Head(string Illpage, int typeill) {
        NL = Environment.NewLine;// Strings.Chr(13) + Strings.Chr(10);
        //carriage return, line feed
        oneline = NL + Strings.Space(20) + "A TAILORED TERM LIFE INSURANCE PROPOSAL" + NL;
        Illpage = Illpage + oneline;
        if (typeill == 2)
            Illpage = Illpage + NL;
        oneline = Strings.Space((76 - Strings.Len(Strings.RTrim(TTInfo.ClientName))) / 2) + "for " + Strings.RTrim(TTInfo.ClientName) + NL;
        Illpage = Illpage + oneline;
        if (typeill == 2)
            Illpage = Illpage + NL;
        oneline = Strings.Space(16) + "STATEMENT OF POLICY COST AND BENEFIT INFORMATION" + NL;
        Illpage = Illpage + oneline;
        if (typeill == 2)
            Illpage = Illpage + NL;
        oneline = "Prepared " + Strings.Format(DateTime.Now, "MMMM") + " " + Strings.Format(DateTime.Now, "DD") + ", " + Strings.Format(DateTime.Now, "YYYY") + " by " + Strings.RTrim(TTInfo.ContactName) + NL;
        oneline = Strings.Space((80 - Strings.Len(Strings.RTrim(oneline))) / 2) + oneline;
        Illpage = Illpage + oneline;
        oneline = Strings.Space(11) + "American Republic Insurance Company, Des Moines, IA  50334" + NL;
        Illpage = Illpage + oneline;
        dynamic DESCRPT = NL + "     ";
        if (TTInfo.UnisexRate == "Y")
        {
            DESCRPT = DESCRPT + "Person";
        }
        else
        {
            if (TTInfo.SexCode == "M")
            {
                DESCRPT = DESCRPT + "Male";
            }
            else
            {
                DESCRPT = DESCRPT + "Female";
            }
        }
        DESCRPT = DESCRPT + " age" + Conversion.Str(TTInfo.IssueAge);
        if (TTInfo.Smoker == "N")
        {
            DESCRPT = DESCRPT + " NonSmoker";
        }
        else
        {
            DESCRPT = DESCRPT + " Smoker ";
        }
        oneline = DESCRPT + Strings.Space(48 - Strings.Len(Strings.RTrim(DESCRPT))) + "Face Amount: ";
        dynamic tempamt = TTInfo.FaceAmt;
        Format_Amount(amtstr, 13, tempamt, "$");
        oneline = oneline + Strings.Space(3) + amtstr + NL;
        Illpage = Illpage + oneline;

    }

    public static int insured_file(int action, int recid, int insno) {
        int functionReturnValue = 0;
        Err_insureds:
        return functionReturnValue;
        return functionReturnValue;

        //On Local Error GoTo Err_insureds
        //filnum% = FreeFile()
        //FIL$ = Dir$(InsuredFile$)
        //Select Case action%
        //    Case 1                  ' retrieve data
        //        If FIL$ = "" Then
        //            '            msg$ = "File Not Found - " + InsuredFile$
        //            '            MSGBOX msg$
        //            insured_file = False
        //            Exit Function
        //        End If
        //        insd_key% = ((recid% - 1) * 12) + insno% + 1
        //        ' Open InsuredFile$ For Random As #filnum% Len = Len(InsuredInfo)
        //        ' Get #filnum%, insd_key%, InsuredInfo
        //        Select Case InsuredInfo.InsRelation
        //            Case "P", "S", "C", "O"
        //                If InsuredInfo.InsGender = "M" Or InsuredInfo.InsGender = "F" Then
        //                    insured_file = True
        //                    If InsuredInfo.InsBirthDate < -4000 Or InsuredInfo.InsBirthDate > 45000 Then
        //                        msg$ = "Invalid birthdate for insured #" + Format$(insno% + 1, "##") + "(" + Trim$(InsuredInfo.InsFName) + ").  Birthdate set to 01/01/1900."
        //                        MsgBox msg$
        //                                InsuredInfo.InsBirthDate = 1
        //                    End If
        //                    Select Case UCase$(InsuredInfo.insTobacco)
        //                        Case " "
        //                            InsuredInfo.insTobacco = "N"
        //                        Case Else
        //                            InsuredInfo.insTobacco = UCase$(InsuredInfo.insTobacco)
        //                    End Select
        //                Else
        //                    insured_file = False
        //                End If
        //            Case Else
        //                insured_file = False
        //        End Select
        //        Close #filnum%
        //        Exit Function
        //    Case 5                   'Save or add data
        //        insd_key% = ((recid% - 1) * 12) + insno% + 1
        //        ' Open InsuredFile$ For Random As #filnum% Len = Len(InsuredInfo)
        //        '     IF recid% = 0 THEN
        //        '         recid% = LOF(FilNum%) / LEN(InsuredInfo) + 1
        //        InsuredInfo.InsClientNumber = recid%
        //        InsuredInfo.InsNumber = insno%
        //        '     END IF
        //        Put #filnum%, insd_key%, InsuredInfo
        //        insured_file = True
        //        Close #filnum%
        //        Exit Function
        //End Select

        //Select Case Err()
        //    Case 62
        //        msg$ = Str$(Err) & "Invalid Record Number on INSURED file"
        //        MsgBox msg$, 0
        //        Case 63            'file does not yet exist
        //    Case Else
        //        msg$ = "Insured_file"
        //        msg$ = Str$(Err) & msg$ & """" & Error$(Err) & """" & "4"
        //        MsgBox msg$  ' Display message.
        //        Close #filnum%
        //End Select
        //insured_file = False
        //Close #filnum%
    }


    public static void Ledger_Allrates(string Illpage, int typeill) {
        NL = Environment.NewLine;// Strings.Chr(13) + Strings.Chr(10);
        //carriage return, line feed
        oneline = "";

    }


    public static void Ledger_Onerate(string Illpage, int typeill) {
        NL = Environment.NewLine;// Strings.Chr(13) + Strings.Chr(10);
        //carriage return, line feed
        oneline = "";
    }

    public static int PrintALine(int start, string onepage, string oneline) {
        int functionReturnValue = 0;

        NL = Environment.NewLine;// Strings.Chr(13) + Strings.Chr(10);
        functionReturnValue = 1;
        dynamic k = Strings.InStr(start, onepage, NL);
        if (k != 0)
        {
            oneline = Strings.Mid(onepage, start, k - start);
        }
        else
        {
            functionReturnValue = 0;
            oneline = "";
        }
        return functionReturnValue;

    }


    public static void set_insd_birthdt(string IBIRTH) {
        if (Strings.Mid(IBIRTH, 1, 2) != "  ")
        {
            Xmm = Strings.Mid(IBIRTH, 1, 2);
            Xdd = Strings.Mid(IBIRTH, 4, 2);
            Xyy = Strings.Mid(IBIRTH, 7, 4);
        }
        else
        {
            InsuredInfo.InsBirthDate = 0;
            return;
        }

        ent_date = new DateTime(int.Parse(Xyy), int.Parse(Xmm), int.Parse(Xdd)).Ticks;
        //ent_date = DateSerial(Conversion.Val(Xyy), Conversion.Val(Xmm), Conversion.Val(Xdd)).Ticks;
        if (ent_date != InsuredInfo.InsBirthDate)
        {
            if (ent_date < 65380)
            {
                curr_dt = DateTime.Now.Ticks;
                Year_Calc(ent_date, curr_dt, Cage, temp_dt);
                InsuredInfo.InsBirthDate = ent_date;
            }
            else
            {
                InsuredInfo.InsBirthDate = 0;
            }
        }
        else
        {
            // InsuredInfo.InsBirthdate = 0
        }

    }

    public static int WriteAnyIniStr(string section, string key, string strvalue, string file) {

        dynamic i = WritePrivateProfileString(section, key, strvalue, file);
        return i;

    }

    public static void Year_Calc(long lowdt, long highdt, int calcyr, long low_ymd) {
        if (lowdt != 0 & highdt != 0)
        {
            low_temp = new DateTime(lowdt).ToString("yyyymmdd");
            hi_temp = new DateTime(highdt).ToString("yyyymmdd");
            low_ymd = DateTime.Parse(low_temp).Ticks;
            hi_ymd = DateTime.Parse(hi_temp).Ticks;
            calcyr = Convert.ToInt32((hi_ymd - low_ymd) / 10000);
        }
    }

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
