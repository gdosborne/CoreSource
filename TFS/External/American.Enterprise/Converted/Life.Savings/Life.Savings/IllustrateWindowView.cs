using FlowDocumentConverter;
using GregOsborne.MVVMFramework;
using Life.Savings.Data.Model.Application;
using Life.Savings.Events;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using static Life.Savings.Events.AddGuaranteeProjectedValueRowEventArgs;
using GregOsborne.Application.Logging;
using GregOsborne.Dialog;
using System.Windows.Input;
using System.Linq;
using Life.Savings.Data.Model;
using System.Collections.Generic;
using GregOsborne.Application;

namespace Life.Savings {
    public class IllustrateWindowView : ViewModelBase {
        public event AddGuaranteeProjectedValueRowHandler AddGuaranteeProjectedValueRow;
        public IllustrateWindowView() {
            TypeOfIllustrationVisibility = Visibility.Visible;
            IsAllYearsChecked = true;
            DayValue = DateTime.Now.ToString("dddd");
            DateValue = DateTime.Now.ToString("MM/dd/yyyy");
            TimeValue = DateTime.Now.ToString("hh:mm tt");
            InsuranceCompanyName = App.InsuranceCompanyName;
            PreparedFor = GetClientFullName();
            PreparedBy = App.Illustration.PreparedBy;
            BenefitOption = App.Illustration.ClientData.BenefitOption.DisplayedValue;
            Gender = App.Illustration.ClientData.Gender.Name;
            Age = App.Illustration.ClientData.Age.ToString();
            PhoneNumber = App.Illustration.ContactTelephoneNumber;
            InitialDeathBenefit = App.Illustration.ClientData.InitialDeathBenefit.Value.ToString("$#,0");
            PremiumMode = App.Illustration.PremiumMode.DisplayedValue;

            var startAge = App.Illustration.IllustrateBeginAtAge == 0 ? App.Illustration.ClientData.Age : App.Illustration.IllustrateBeginAtAge;

            Module2.LSClient[Module2.SpouseClient].IllusType = 195;

            //GuaranteedProjectedValues = App.Illustration.
            UpdateInterface();
        }
        private string GetClientFullName() {
            if (!string.IsNullOrEmpty(App.Illustration.ClientData.FirstName) && !string.IsNullOrEmpty(App.Illustration.ClientData.LastName) && !string.IsNullOrEmpty(App.Illustration.ClientData.MiddleInitial))
                return App.Illustration.ClientData.FirstName + " " + App.Illustration.ClientData.MiddleInitial + " " + App.Illustration.ClientData.LastName;
            else if (!string.IsNullOrEmpty(App.Illustration.ClientData.FirstName) && !string.IsNullOrEmpty(App.Illustration.ClientData.MiddleInitial))
                return App.Illustration.ClientData.FirstName + " " + App.Illustration.ClientData.LastName;
            else
                return null;
        }

        private string _InitialDeathBenefit;
        public string InitialDeathBenefit {
            get => _InitialDeathBenefit;
            set {
                _InitialDeathBenefit = value;
                InvokePropertyChanged(nameof(InitialDeathBenefit));
            }
        }

        private string _PremiumMode;
        public string PremiumMode {
            get => _PremiumMode;
            set {
                _PremiumMode = value;
                InvokePropertyChanged(nameof(PremiumMode));
            }
        }

        private string _PhoneNumber;
        public string PhoneNumber {
            get => _PhoneNumber;
            set {
                _PhoneNumber = value;
                InvokePropertyChanged(nameof(PhoneNumber));
            }
        }
        private string _Age;
        public string Age {
            get => _Age;
            set {
                _Age = value;
                InvokePropertyChanged(nameof(Age));
            }
        }
        private string _Gender;
        public string Gender {
            get => _Gender;
            set {
                _Gender = value;
                InvokePropertyChanged(nameof(Gender));
            }
        }
        private string _BenefitOption;
        public string BenefitOption {
            get => _BenefitOption;
            set {
                _BenefitOption = value;
                InvokePropertyChanged(nameof(BenefitOption));
            }
        }
        private string _PreparedBy;
        public string PreparedBy {
            get => _PreparedBy;
            set {
                _PreparedBy = value;
                InvokePropertyChanged(nameof(PreparedBy));
            }
        }
        private string _PreparedFor;
        public string PreparedFor {
            get => _PreparedFor;
            set {
                _PreparedFor = value;
                InvokePropertyChanged(nameof(PreparedFor));
            }
        }
        private string _TimeValue;
        public string TimeValue {
            get => _TimeValue;
            set {
                _TimeValue = value;
                InvokePropertyChanged(nameof(TimeValue));
            }
        }
        private string _InsuranceCompanyName;
        public string InsuranceCompanyName {
            get => _InsuranceCompanyName;
            set {
                _InsuranceCompanyName = value;
                InvokePropertyChanged(nameof(InsuranceCompanyName));
            }
        }
        private string _DayValue;
        public string DayValue {
            get => _DayValue;
            set {
                _DayValue = value;
                InvokePropertyChanged(nameof(DayValue));
            }
        }

        private string _DateValue;
        public string DateValue {
            get => _DateValue;
            set {
                _DateValue = value;
                InvokePropertyChanged(nameof(DateValue));
            }
        }

        private double _PixelsPerDip;
        public double PixelsPerDip {
            get => _PixelsPerDip;
            set {
                _PixelsPerDip = value;
                InvokePropertyChanged(nameof(PixelsPerDip));
            }
        }

        private Visibility _TypeOfIllustrationVisibility;
        public Visibility TypeOfIllustrationVisibility {
            get => _TypeOfIllustrationVisibility;
            set {
                _TypeOfIllustrationVisibility = value;
                InvokePropertyChanged(nameof(TypeOfIllustrationVisibility));
            }
        }

        private bool _IsAllYearsChecked;
        public bool IsAllYearsChecked {
            get => _IsAllYearsChecked;
            set {
                _IsAllYearsChecked = value;
                if (Module2.LSClient[Module2.SpouseClient] == null)
                    Module2.LSClient[Module2.SpouseClient] = new Module2.LSClientParameters();
                if (IsAllYearsChecked)
                    Module2.LSClient[Module2.SpouseClient].IllusType = 195;
                InvokePropertyChanged(nameof(IsAllYearsChecked));
            }
        }

        private bool _IsFirst25Checked;
        public bool IsFirst25Checked {
            get => _IsFirst25Checked;
            set {
                _IsFirst25Checked = value;
                if (IsFirst25Checked)
                {
                    var age = Module2.SpouseClient == 0 ? App.Illustration.ClientData.Age : App.Illustration.SpouseAsClientData.Age;
                    Module2.LSClient[Module2.SpouseClient].IllusType = 200 + age + 25;
                }
                InvokePropertyChanged(nameof(IsFirst25Checked));
            }
        }

        private bool _IsFirst10Checked;
        public bool IsFirst10Checked {
            get => _IsFirst10Checked;
            set {
                _IsFirst10Checked = value;
                if (IsFirst10Checked)
                    Module2.LSClient[Module2.SpouseClient].IllusType = 395;
                InvokePropertyChanged(nameof(IsFirst10Checked));
            }
        }

        private bool? _DialogResult;
        public bool? DialogResult {
            get => _DialogResult;
            set {
                _DialogResult = value;
                InvokePropertyChanged(nameof(DialogResult));
            }
        }

        private FlowDocument _Document;
        public FlowDocument Document {
            get => _Document;
            set {
                _Document = value;
                InvokePropertyChanged(nameof(Document));
            }
        }
        public event GetIllustrationSaveParametersHandler GetIllustrationSaveParameters;
        private DelegateCommand _SaveIllustrationCommand;
        public DelegateCommand SaveIllustrationCommand => _SaveIllustrationCommand ?? (_SaveIllustrationCommand = new DelegateCommand(SaveIllustration, ValidateSaveIllustrationState));
        private void SaveIllustration(object state) {
            var e = new GetIllustrationSaveParametersEventArgs();
            GetIllustrationSaveParameters?.Invoke(this, e);
            if (e.IsCancel)
                return;

            var data = e.FileType == SaveFileTypes.Xps
                ? XpsConverter.ConverterDoc(e.Document)
                : PdfConverter.ConvertDoc(e.Document);

            if (File.Exists(e.FileName))
                File.Delete(e.FileName);
            using (var fs = new FileStream(e.FileName, FileMode.CreateNew, FileAccess.Write, FileShare.None))
            using (var sw = new BinaryWriter(fs))
            {
                sw.Write(data);
            }
        }

        private ObservableCollection<LedgerValue> _LedgerItems;
        public ObservableCollection<LedgerValue> LedgerItems {
            get => _LedgerItems;
            set {
                _LedgerItems = value;
                InvokePropertyChanged(nameof(LedgerItems));
            }
        }
        private static bool ValidateSaveIllustrationState(object state) {
            return !string.IsNullOrEmpty(App.IllustrationSaveLocation) && Directory.Exists(App.IllustrationSaveLocation);
        }
        public event EventHandler PrintFlowDocument;
        private DelegateCommand _PrintIllustrationCommand;
        public DelegateCommand PrintIllustrationCommand => _PrintIllustrationCommand ?? (_PrintIllustrationCommand = new DelegateCommand(PrintIllustration, ValidatePrintIllustrationState));
        private void PrintIllustration(object state) {
            PrintFlowDocument?.Invoke(this, EventArgs.Empty);
        }
        private static bool ValidatePrintIllustrationState(object state) {
            return true;
        }
        private DelegateCommand _ExitIllustrationCommand;
        public DelegateCommand ExitIllustrationCommand => _ExitIllustrationCommand ?? (_ExitIllustrationCommand = new DelegateCommand(ExitIllustration, ValidateExitIllustrationState));
        private void ExitIllustration(object state) {
            DialogResult = true;
        }
        private static bool ValidateExitIllustrationState(object state) {
            return true;
        }
        public event SetCursorHandler SetCursor;
        public event ShowMessageHandler ShowMessage;
        private bool ShowIfError(string errorMessage) {
            if (string.IsNullOrEmpty(errorMessage))
                return false;
            Logger.LogMessage(errorMessage, true);
            SetCursor?.Invoke(this, new SetCursorEventArgs(Cursors.Arrow));
            ShowMessage?.Invoke(this, new ShowMessageEventArgs(errorMessage, "Error", 400, 200, ImagesTypes.Error));
            return true;
        }
        private double GetLoanBalance(IllustrationInfo info) {
            var result = 0.0;
            foreach (var item in info.FutureAnnualPolicyLoans)
            {
                if (!item.Age.HasValue || !item.EndAge.HasValue || info.ClientData.Age < item.Age || info.ClientData.Age > item.EndAge)
                    continue;
                result += item.Value.Value;
            }
            if (result == 0)
                return result;
            foreach (var item in info.FutureAnnualLoanRepayments)
            {
                if (info.ClientData.Age < item.Age || info.ClientData.Age > item.EndAge)
                    continue;
                result -= item.Value.Value;
            }
            return result;
        }
        private void BeginIllustration() {
            SetCursor?.Invoke(this, new SetCursorEventArgs(Cursors.Wait));

            Module2.LSClient[0].InsuredRider = new int[9];
            Module2.LSClient[1] = new Module2.LSClientParameters {
                InsuredRider = new int[9]
            };
            Module2.LSClient[0].FaceAmt = Convert.ToInt64(App.Illustration.ClientData.InitialDeathBenefit);
            Module2.LSClient[1].FaceAmt = App.Illustration.SpouseAsClientData == null ? 0 : Convert.ToInt64(App.Illustration.SpouseAsClientData.InitialDeathBenefit);

            Module2.LS_IRate[0] = Convert.ToSingle(App.Illustration.ClientData.InterestRate * 100);
            Module2.LS_IRate[1] = App.Illustration.SpouseAsClientData == null ? 0 : Convert.ToSingle(App.Illustration.SpouseAsClientData.InterestRate * 100);

            Module2.LSClient[0].CurrRate = Convert.ToSingle(App.Illustration.ClientData.InterestRate);
            Module2.LSClient[1].CurrRate = App.Illustration.SpouseAsClientData == null ? 0 : Convert.ToSingle(App.Illustration.SpouseAsClientData.InterestRate);
            Module2.LSClient[0].HighAges = new int[5];
            for (int i = 0; i < App.Illustration.HighlightedAges.Count; i++)
            {
                Module2.LSClient[0].HighAges[i] = App.Illustration.HighlightedAges[i];
            }

            Module2.LS_COLA_Client = App.Illustration.ClientData.Cola != null ? Convert.ToSingle(App.Illustration.ClientData.Cola.Value) : 0f;
            Module2.LS_LoanBalance = GetLoanBalance(App.Illustration);

            Module2.LSFutureWD = new Module2.LSFutureWithdrawals[5];
            for (int i = 1; i <= 4; i++)
            {
                var loan = App.Illustration.FutureAnnualPolicyLoans[i - 1];
                var payment = App.Illustration.FutureAnnualLoanRepayments[i - 1];
                var withdrawl = App.Illustration.FutureWithdrawls[i - 1];
                Module2.LSFutureWD[i] = new Module2.LSFutureWithdrawals {
                    Loan_Age = loan.Age.HasValue ? loan.Age.Value : 0,
                    Loan_Amount = loan.Value.HasValue ? Convert.ToSingle(loan.Value.Value) : 0f,
                    Loan_Interest = 0,
                    Loan_Years = loan.EndAge.HasValue && loan.Age.HasValue ? loan.EndAge.Value - loan.Age.Value : 0,
                    LoanPay_Age = payment.Age.HasValue ? payment.Age.Value : 0,
                    LoanPay_Amount = payment.Value.HasValue ? Convert.ToSingle(payment.Value.Value) : 0f,
                    LoanPay_Years = payment.EndAge.HasValue && payment.Age.HasValue ? payment.EndAge.Value - payment.Age.Value : 0,
                    WD_Age = withdrawl.Age.HasValue ? withdrawl.Age.Value : 0,
                    WD_Amount = withdrawl.Value.HasValue ? Convert.ToSingle(withdrawl.Value.Value) : 0f,
                    WD_Years = withdrawl.EndAge.HasValue && withdrawl.Age.HasValue ? withdrawl.EndAge.Value - withdrawl.Age.Value : 0
                };
            }

            for (var i = 0; i <= 1; i++)
            {
                for (var j = 0; j < Module1.MAXAGE; j++)
                {
                    Module2.LSMort[i, j] = new Module2.LSMortalityRates {
                        Insured_Base = new float[5],
                        Insured_CSO80 = new float[5],
                        Insured_Sub = new float[5],
                        Prin_Base = new float[5],
                        Prin_Sub = new float[5]
                    };
                }
            }
            Module2.LSLedger = new Module2.LSLedgerValues[Module1.MAXAGE];
            for (int i = 0; i < Module1.MAXAGE; i++)
            {
                Module2.LSLedger[i] = new Module2.LSLedgerValues {
                    SurrenderValue = new float[2],
                    CashValue = new float[2],
                    DeathBenefit = new float[2]
                };
            }

            Module2.CalcPrem = new Module2.LSCalculatedPremiums {
                InsdMin = new float[10],
                InsdTarg = new float[10]
            };

            var spouseRider = App.Illustration.Riders.FirstOrDefault(x => x.IndividualType == IndividualData.KnownIndices.Spouse && x.IsSelected);
            Module2.LSSpouseRider = new Module2.LSSpouseRiderParm {
                FaceAmt = (spouseRider != null && spouseRider.FaceAmount.HasValue) ? Convert.ToInt64(spouseRider.FaceAmount.Value) : 0,
                IssueAge = spouseRider != null ? spouseRider.Age : 0,
                RemoveYear = (spouseRider != null && spouseRider.AgeOrYear.HasValue) ? spouseRider.AgeOrYear.Value : 0
            };
            Module2.LSChildRider = new Module2.LSChildRiderParm {
                AgeYoungest = App.Illustration.ChildRiderYoungestAge,
                FaceAmt = Convert.ToInt32(App.Illustration.ChildRiderDeathBenefitAmount)
            };

            for (int i = 0; i <= 4; i++)
            {
                Module2.LSFuture[i] = new Module2.LSFutureChanges();
                Module2.LSFutureWD[i] = new Module2.LSFutureWithdrawals();
            }

            Module1.Set_Future_Changes(App.Illustration);

            //try
            //{
            Module2.LifeIllusType = 1;
            Module1.Set_Client_Illus_Parameters(App.Illustration);
            var ready = Module1.ReadyToIllustrate();
            if (ready == 0)
            {
                ShowMessage?.Invoke(this, new ShowMessageEventArgs("Cannot illustrate until valid Age, Gender, Death Benefit and Interest Rate are input", "Error", 400, 200, ImagesTypes.Error));
                return;
            }
            Module1.Set_Premium_Illus_Parameters(App.Illustration);
            Module1.Calculate_Initial_Premiums(App.Illustration, App.CurrentDataSet);
            if (App.Illustration.IsClientGuidelineChecked)
                App.Illustration.PlannedModalPremium = Module2.CalcPrem.GuideAnnual;
            else if (App.Illustration.IsClientTargetChecked)
                App.Illustration.PlannedModalPremium = Module2.CalcPrem.TotTarg;
            else
                App.Illustration.PlannedModalPremium = Module2.CalcPrem.TotMin;
            Module1.Set_Premium_Illus_Parameters(App.Illustration);
            Module1.Calculate_Full_LS_Ledger(App.Illustration, App.CurrentDataSet);

            Module2.PrintIllusPage[0] = "P1";
            Module2.PrintIllusPage[1] = "P2";
            Module2.PrintIllusPage[2] = "P3";
            Module2.PrintIllusPage[3] = "P4";
            Module2.PrintIllusPage[4] = "P5";
            Module2.PrintIllusPage[5] = "P6";

            var pageText = string.Empty;

            Pages = new List<string>();

            Module2.PrintIllusPage[0] = string.Empty;
            Module2.LIFEPAGE = 1;
            var companyName = Settings.GetSetting(App.ApplicationName, "Application", "InsuranceCompanyName", "American Enterprise Insurance Company");
            Module1.LifeSave_Illus_Head(ref pageText, 1, companyName);
            Module1.LifeSave_Ledger(ref pageText, App.Illustration, companyName);
            Pages.Add(pageText);
            pageText = string.Empty;
            Module1.LifeSave_Close_Text(ref pageText, 1, Module2.LSClient[Module2.SpouseClient].CurrRate, App.Illustration, companyName);
            Pages.Add(pageText);
            pageText = string.Empty;

            CompleteIllustration?.Invoke(this, EventArgs.Empty);

            //}
            //catch (Exception ex)
            //{
            //    ShowMessage?.Invoke(this, new ShowMessageEventArgs(ex.Message, "Error", 400, 200, ImagesTypes.Error));
            //    throw;
            //}

            SetCursor?.Invoke(this, new SetCursorEventArgs(Cursors.Arrow));
        }

        public event EventHandler CompleteIllustration;

        private List<string> _Pages;
        public List<string> Pages {
            get => _Pages;
            set {
                _Pages = value;
                InvokePropertyChanged(nameof(Pages));
            }
        }

        private DelegateCommand _TypeOfIllustrationOKCommand;
        public DelegateCommand TypeOfIllustrationOKCommand => _TypeOfIllustrationOKCommand ?? (_TypeOfIllustrationOKCommand = new DelegateCommand(TypeOfIllustrationOK, ValidateTypeOfIllustrationOKState));
        private void TypeOfIllustrationOK(object state) {
            TypeOfIllustrationVisibility = Visibility.Collapsed;
            BeginIllustration();
        }
        private static bool ValidateTypeOfIllustrationOKState(object state) {
            return true;
        }
        private DelegateCommand _TypeOfIllustrationCancelCommand;
        public DelegateCommand TypeOfIllustrationCancelCommand => _TypeOfIllustrationCancelCommand ?? (_TypeOfIllustrationCancelCommand = new DelegateCommand(TypeOfIllustrationCancel, ValidateTypeOfIllustrationCancelState));
        private void TypeOfIllustrationCancel(object state) {
            DialogResult = false;
        }
        private static bool ValidateTypeOfIllustrationCancelState(object state) {
            return true;
        }

        public int First25_1 {
            get => App.Illustration.HighlightedAges[0];
            set {
                App.Illustration.HighlightedAges[0] = value;
                InvokePropertyChanged(nameof(First25_1));
            }
        }

        public int First25_2 {
            get => App.Illustration.HighlightedAges[1];
            set {
                App.Illustration.HighlightedAges[1] = value;
                InvokePropertyChanged(nameof(First25_2));
            }
        }

        public int First25_3 {
            get => App.Illustration.HighlightedAges[2];
            set {
                App.Illustration.HighlightedAges[2] = value;
                InvokePropertyChanged(nameof(First25_3));
            }
        }

        public int First25_4 {
            get => App.Illustration.HighlightedAges[3];
            set {
                App.Illustration.HighlightedAges[3] = value;
                InvokePropertyChanged(nameof(First25_4));
            }
        }

        public int First25_5 {
            get => App.Illustration.HighlightedAges[4];
            set {
                App.Illustration.HighlightedAges[4] = value;
                InvokePropertyChanged(nameof(First25_5));
            }
        }

        private int _ShowToAge;
        public int ShowToAge {
            get => _ShowToAge;
            set {
                _ShowToAge = value;
                InvokePropertyChanged(nameof(ShowToAge));
            }
        }
    }
}
