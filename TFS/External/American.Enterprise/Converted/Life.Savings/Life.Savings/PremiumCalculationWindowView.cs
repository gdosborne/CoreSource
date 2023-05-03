using GregOsborne.Application.Primitives;
using GregOsborne.Dialog;
using GregOsborne.MVVMFramework;
using Life.Savings.Data.Model;
using Life.Savings.Data.Model.Application;
using Life.Savings.Events;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Life.Savings.Data;
using System.Windows;
using System.Windows.Input;
using GregOsborne.Application.Logging;
using System;

namespace Life.Savings {
    public class PremiumCalculationWindowView : ViewModelBase {
        public PremiumCalculationWindowView() {
            WindowTitle = "Premium Calculation for Client";
            ReturnToText = "Return to Client";
            IsForSpouse = false;
            PremModes = new ObservableCollection<SimpleValue>(App.Repository.IsLS3DataSelected ? App.Repository.Ls3Data.LsPremiumModes : App.Repository.Ls2Data.LsPremiumModes);
            YearsToPay = new ObservableCollection<SimpleValue>(App.Repository.IsLS3DataSelected ? App.Repository.Ls3Data.LsYearToPay : App.Repository.Ls2Data.LsYearToPay);
            PremiumMode = App.Illustration.PremiumMode;
            PremiumYearsToPay = App.Illustration.PremiumYearsToPay;
            if (PremiumYearsToPay.Id == 0)
                App.CurrentDataSet.LsYearToPay[0].UpdateYearsToPayDisplayValue(Module1.MAXAGE - App.Illustration.ClientData.Age);
            PremiumInterestRate = App.Illustration.PremiumInterestRate == 0 && App.Illustration.ClientData.InterestRate.HasValue ? App.Illustration.ClientData.InterestRate.Value : App.Illustration.PremiumInterestRate;
            PlannedModalPremium = App.Illustration.PlannedModalPremium;
            InitialLumpSumAmount = App.Illustration.InitialLumpSumAmount;
            IllustrateBeginAtAge = App.Illustration.ClientData.Age;
            IllustrateInitialCashValue = App.Illustration.IllustrateInitialCashValue > 0
                ? App.Illustration.IllustrateInitialCashValue
                : App.Illustration.ClientData.LumpSum.HasValue
                    ? App.Illustration.ClientData.LumpSum.Value
                    : App.Illustration.ClientData.FaceAmount.HasValue
                        ? App.Illustration.ClientData.FaceAmount.Value
                        : 0;
            Insured = new ObservableCollection<IndividualData>();
            IIndividualData insuredRider = new IndividualData {
                IndividualType = IndividualData.KnownIndices.Primary,
                Name = "Principal",
                Visibility = Visibility.Visible,
                IsSelected = true,
                IsEnabled = false
            };
            Insured.Add(insuredRider.As<IndividualData>());
            IIndividualData childRider = null;
            if (App.Illustration.IsChildRiderSelected)
            {
                childRider = new IndividualData {
                    IndividualType = IndividualData.KnownIndices.Child,
                    Name = "Child Rider",
                    Visibility = Visibility.Visible,
                    FaceAmount = App.Illustration.ChildRiderDeathBenefitAmount,
                    Age = App.Illustration.ChildRiderYoungestAge,
                    IsSelected = true,
                    IsEnabled = true
                };
            }
            if (App.Illustration.Riders.Any())
            {
                var spouseRider = App.Illustration.Riders.FirstOrDefault(x => x.IndividualType == IndividualData.KnownIndices.Spouse);
                if (spouseRider != null && spouseRider.IsSelected)
                {
                    insuredRider = new IndividualData {
                        IndividualType = IndividualData.KnownIndices.Spouse,
                        Name = spouseRider.Name,
                        Visibility = Visibility.Visible,
                        IsSelected = true,
                        IsEnabled = true
                    };
                    Insured.Add(insuredRider.As<IndividualData>());
                }
                if (childRider != null)
                    Insured.Add(childRider.As<IndividualData>());
                var riders = App.Illustration.Riders.Where(x => x.IndividualType == IndividualData.KnownIndices.Rider).ToList();
                foreach (var rider in riders)
                {
                    insuredRider = new IndividualData {
                        IndividualType = 0,
                        Name = rider.Name,
                        Visibility = rider.IsSelected ? Visibility.Visible : Visibility.Collapsed,
                        IsSelected = true,
                        IsEnabled = false
                    };
                    Insured.Add(insuredRider.As<IndividualData>());
                }
            }
        }

        private ObservableCollection<IndividualData> _Insured;
        public ObservableCollection<IndividualData> Insured {
            get => _Insured;
            set {
                _Insured = value;
                InvokePropertyChanged(nameof(Insured));
            }
        }

        private bool? _dialogResult;
        public bool? DialogResult {
            get => _dialogResult;
            set {
                _dialogResult = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(DialogResult));
            }
        }
        private DelegateCommand _returnToClientCommand = null;
        public DelegateCommand ReturnToClientCommand => _returnToClientCommand ?? (_returnToClientCommand = new DelegateCommand(ReturnToClient, ValidateReturnToClientState));
        private void ReturnToClient(object state) {
            DialogResult = false;
        }
        private bool ValidateReturnToClientState(object state) {
            return true;
        }

        private bool _IsInsuredRidersNext;
        public bool IsInsuredRidersNext {
            get => _IsInsuredRidersNext;
            set {
                _IsInsuredRidersNext = value;
                InvokePropertyChanged(nameof(IsInsuredRidersNext));
            }
        }
        private DelegateCommand _InsuredRidersCommand;
        public DelegateCommand InsuredRidersCommand => _InsuredRidersCommand ?? (_InsuredRidersCommand = new DelegateCommand(InsuredRiders, ValidateInsuredRidersState));
        private void InsuredRiders(object state) {
            IsInsuredRidersNext = true;
            DialogResult = true;
        }
        private static bool ValidateInsuredRidersState(object state) {
            return true;
        }
        private bool _IsShowFutureChangesNext;
        public bool IsShowFutureChangesNext {
            get => _IsShowFutureChangesNext;
            set {
                _IsShowFutureChangesNext = value;
                InvokePropertyChanged(nameof(IsShowFutureChangesNext));
            }
        }
        private DelegateCommand _FutureChangesCommand;
        public DelegateCommand FutureChangesCommand => _FutureChangesCommand ?? (_FutureChangesCommand = new DelegateCommand(FutureChanges, ValidateFutureChangesState));
        private void FutureChanges(object state) {
            IsShowFutureChangesNext = true;
            DialogResult = true;
        }
        private static bool ValidateFutureChangesState(object state) {
            return true;
        }

        private ObservableCollection<SimpleValue> _PremModes;
        public ObservableCollection<SimpleValue> PremModes {
            get => _PremModes;
            set {
                _PremModes = value;
                InvokePropertyChanged(nameof(PremModes));
            }
        }

        private ObservableCollection<SimpleValue> _YearsToPay;
        public ObservableCollection<SimpleValue> YearsToPay {
            get => _YearsToPay;
            set {
                _YearsToPay = value;
                InvokePropertyChanged(nameof(YearsToPay));
            }
        }

        public SimpleValue PremiumMode {
            get => App.Illustration.PremiumMode;
            set {
                App.Illustration.PremiumMode = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(PremiumMode));
            }
        }

        public SimpleValue PremiumYearsToPay {
            get => App.Illustration.PremiumYearsToPay;
            set {
                App.Illustration.PremiumYearsToPay = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(PremiumYearsToPay));
            }
        }

        private SimpleValue _YearsToPayMode;
        public SimpleValue YearsToPayMode {
            get => _YearsToPayMode;
            set {
                _YearsToPayMode = value;
                InvokePropertyChanged(nameof(YearsToPayMode));
            }
        }

        public double PlannedModalPremium {
            get => App.Illustration.PlannedModalPremium;
            set {
                App.Illustration.PlannedModalPremium = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(PlannedModalPremium));
            }
        }

        public double InitialLumpSumAmount {
            get => App.Illustration.InitialLumpSumAmount;
            set {
                App.Illustration.InitialLumpSumAmount = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(InitialLumpSumAmount));
            }
        }

        public double PremiumInterestRate {
            get => App.Illustration.PremiumInterestRate;
            set {
                App.Illustration.PremiumInterestRate = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(PremiumInterestRate));
            }
        }

        public double MinimumModalPremium {
            get => App.Illustration.MinimumModalPremium;
            set {
                App.Illustration.MinimumModalPremium = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(MinimumModalPremium));
            }
        }

        public double TargetPremium {
            get => App.Illustration.TargetPremium;
            set {
                App.Illustration.TargetPremium = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(TargetPremium));
            }
        }

        public double MinGuidanceAnnualPremium {
            get => App.Illustration.MinGuidanceAnnualPremium;
            set {
                App.Illustration.MinGuidanceAnnualPremium = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(MinGuidanceAnnualPremium));
            }
        }

        public double MinGuidanceSinglePremiumPrincipal {
            get => App.Illustration.MinGuidanceSinglePremiumPrincipal;
            set {
                App.Illustration.MinGuidanceSinglePremiumPrincipal = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(MinGuidanceSinglePremiumPrincipal));
            }
        }

        public double MinGuidanceSinglePremiumAdditional {
            get => App.Illustration.MinGuidanceSinglePremiumAdditional;
            set {
                App.Illustration.MinGuidanceSinglePremiumAdditional = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(MinGuidanceSinglePremiumAdditional));
            }
        }

        public double SpecialOptionsCashValue {
            get => App.Illustration.SpecialOptionsCashValue;
            set {
                App.Illustration.SpecialOptionsCashValue = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(SpecialOptionsCashValue));
            }
        }

        public int SpecialOptionsAge {
            get => App.Illustration.SpecialOptionsAge;
            set {
                App.Illustration.SpecialOptionsAge = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(SpecialOptionsAge));
            }
        }

        public string SpecialOptionsModalPremium {
            get => App.Illustration.SpecialOptionsModalPremium;
            set {
                App.Illustration.SpecialOptionsModalPremium = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(SpecialOptionsModalPremium));
            }
        }

        public int IllustrateBeginAtAge {
            get => App.Illustration.IllustrateBeginAtAge;
            set {
                App.Illustration.IllustrateBeginAtAge = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(IllustrateBeginAtAge));
            }
        }

        public double IllustrateInitialCashValue {
            get => App.Illustration.IllustrateInitialCashValue;
            set {
                App.Illustration.IllustrateInitialCashValue = value;
                if (IllustrateInitialCashValue > 999999)
                {
                    ShowMessage?.Invoke(this, new ShowMessageEventArgs("Attained cash value must be less then $1,000,000.", "Incorrect value", 400, 200, ImagesTypes.Information));
                    IllustrateInitialCashValue = 999999;
                }
                UpdateInterface();
                InvokePropertyChanged(nameof(IllustrateInitialCashValue));
            }
        }

        private string _WindowTitle;
        public string WindowTitle {
            get => _WindowTitle;
            set {
                _WindowTitle = value;
                InvokePropertyChanged(nameof(WindowTitle));
            }
        }
        private bool _IsForSpouse;
        public bool IsForSpouse {
            get => _IsForSpouse;
            set {
                _IsForSpouse = value;
                if (IsForSpouse)
                {
                    WindowTitle = "Premium Calculation for Spouse";
                    ReturnToText = "Return to Spouse";
                }
                InvokePropertyChanged(nameof(IsForSpouse));
            }
        }

        private string _ReturnToText;
        public string ReturnToText {
            get => _ReturnToText;
            set {
                _ReturnToText = value;
                InvokePropertyChanged(nameof(ReturnToText));
            }
        }
        public event SetCursorHandler SetCursor;
        public event ShowMessageHandler ShowMessage;
        private DelegateCommand _CalculateCommand;
        public DelegateCommand CalculateCommand => _CalculateCommand ?? (_CalculateCommand = new DelegateCommand(Calculate, ValidateCalculateState));
        private void Calculate(object state) {
            SetCursor?.Invoke(this, new SetCursorEventArgs(Cursors.Wait));

            ICalculations calc = new Calculations();
            var startAge = App.Illustration.IsSpousePrincipalInsured ? App.Illustration.SpouseAsClientData.Age : App.Illustration.ClientData.Age;
            var calcPremiums = calc.BeginCalculations(App.Illustration, App.Repository, App.CurrentDataSet, startAge, out var errorMessage);
            if (ShowIfError(errorMessage))
                return;

            SpecialOptionsModalPremium = calcPremiums.SolvePremium;
            MinimumModalPremium = calcPremiums.TotMin;
            TargetPremium = calcPremiums.PrinTarg;
            MinGuidanceSinglePremiumPrincipal = calcPremiums.GuideSingle;
            MinGuidanceSinglePremiumAdditional = calcPremiums.GuideSingleAddInsd;
            MinGuidanceAnnualPremium = calcPremiums.GuideAnnual;
            InForceYears = calcPremiums.InForceYears;

            SetCursor?.Invoke(this, new SetCursorEventArgs(Cursors.Arrow));
        }
        private bool ShowIfError(string errorMessage) {
            if (string.IsNullOrEmpty(errorMessage))
                return false;
            Logger.LogMessage(errorMessage, true);
            SetCursor?.Invoke(this, new SetCursorEventArgs(Cursors.Arrow));
            ShowMessage?.Invoke(this, new ShowMessageEventArgs(errorMessage, "Error", 400, 200, ImagesTypes.Error));
            return true;
        }
        private static bool ValidateCalculateState(object state) {
            return true;
        }

        public string InForceYears {
            get => App.Illustration.InForceYears;
            set {
                App.Illustration.InForceYears = value;
                InvokePropertyChanged(nameof(InForceYears));
            }
        }
        public event EventHandler ShowIllustration;
        private DelegateCommand _IllustrateCommand;
        public DelegateCommand IllustrateCommand => _IllustrateCommand ?? (_IllustrateCommand = new DelegateCommand(Illustrate, ValidateIllustrateState));
        private void Illustrate(object state) {
            ShowIllustration?.Invoke(this, EventArgs.Empty);
        }
        private static bool ValidateIllustrateState(object state) {
            return true;
        }

    }
}
