using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using GregOsborne.Application.Primitives;
using GregOsborne.MVVMFramework;
using Life.Savings.Data.Model;
using Life.Savings.Data.Model.Application;
using Life.Savings.Events;

namespace Life.Savings {
    public class AdditionalInsuredWindowView : ViewModelBase {
        public event ShowMessageHandler ShowMessage;

        public DelegateCommand ReturnToClientCommand => _returnToClientCommand ?? (_returnToClientCommand = new DelegateCommand(ReturnToClient, ValidateReturnToClientState));
        public DelegateCommand PremiumCalcCommand => _PremiumCalcCommand ?? (_PremiumCalcCommand = new DelegateCommand(PremiumCalc, ValidatePremiumCalcState));
        public DelegateCommand FutureChangesCommand => _FutureChangesCommand ?? (_FutureChangesCommand = new DelegateCommand(FutureChanges, ValidateFutureChangesState));
        public DelegateCommand RefreshCommand => _RefreshCommand ?? (_RefreshCommand = new DelegateCommand(Refresh, ValidateRefreshState));

        private bool isSelectedBeingChanged = false;
        private ObservableCollection<Gender> _Genders;
        private ObservableCollection<IIndividualData> _AdditionalInsureds;
        private SimpleValue _AdditionalPlanType;
        private SimpleValue _SelectedSpouseColaPercent;
        private ObservableCollection<SimpleValue> _SpouseColaPercents;
        private ObservableCollection<SimpleValue> _SpouseTableRatings;
        private SimpleValue _SelectedSpouseTableRating;
        private ObservableCollection<SimpleValue> _YearsOrAges;
        private int _SpouseYears;
        private SimpleValue _SpouseSelectedYearsOrAge;
        private SimpleValue _SpouseSelectedPlanType;
        private ObservableCollection<SimpleValue> _PlanTypes;
        private int _SpouseAge;
        private bool _IsForSpouse;
        private bool? _dialogResult;
        private DelegateCommand _returnToClientCommand = null;
        private ObservableCollection<double> _ChildBenefitAmounts;
        private double _SelectedSpouseBenefitAmount;
        private ObservableCollection<double> _SpouseBenefitAmounts;
        private Visibility _InsuredChildRiderVisibility;
        private Visibility _InsuredSpouseRiderVisibility;
        private bool _IsInsuredSpouseRiderChecked;
        private bool _IsShowPrmiumCalcNext;
        private DelegateCommand _PremiumCalcCommand;
        private bool _IsShowFutureChangesNext;
        private DelegateCommand _FutureChangesCommand;
        private DelegateCommand _RefreshCommand;

        private InsuredIllustrationParameters GetInsuredIllustrationParameters() {
            var result = new InsuredIllustrationParameters();
            if (!IsForSpouse)
            {
                result.ChildRider = IsInsuredChildRiderChecked && AgeYoungestChild > 0;
                result.AgeYoungestChild = AgeYoungestChild;
                result.ChildBenefitAmount = SelectedChildBenefitAmount;
            }
            if (IsInsuredSpouseRiderChecked)
            {
                result.SpouseRider = IsInsuredSpouseRiderChecked;
                result.SpouseAge = SpouseAge;
                result.SpouseBenefitAmount = SelectedSpouseBenefitAmount;
                result.SpousePlanType = SpouseSelectedPlanType;
                result.SpouseSubstandardRating = SelectedSpouseTableRating;
                result.SpouseCola = SelectedSpouseColaPercent;
                result.SpouseYearToAge = SpouseSelectedYearsOrAge;
            }
            var index = 0;
            foreach (var item in AdditionalInsureds)
            {
                result.Riders[index].IsSelected = item.IsSelected;
                if (!result.Riders[index].IsSelected)
                    continue;
                result.Riders[index].Age = item.Age;
                result.Riders[index].Gender = item.Gender;
                result.Riders[index].DeathBenefitAmount = item.FaceAmount.Value;
                result.Riders[index].PlanType = item.PlanType;
                result.Riders[index].SubstandardRate = item.SubstandardRate;
                result.Riders[index].Cola = item.Cola;
                result.Riders[index].YearsToAges = item.YearToAgeOption;
                result.Riders[index].StartYearOrAge = item.AgeOrYear.Value - (result.Riders[index].YearsToAges.DisplayedValue == "Years" ? result.Riders[index].Age : 0);
                result.Riders[index].EndYearOrAge = item.EndYear.Value - (result.Riders[index].YearsToAges.DisplayedValue == "Years" ? result.Riders[index].Age : 0);
            }
            return result;
        }

        public InsuredIllustrationParameters InsuredIllustrationParameters { get; set; }

        private void ReturnToClient(object state) {
            InsuredIllustrationParameters = GetInsuredIllustrationParameters();
            DialogResult = false;
        }

        private bool ValidateReturnToClientState(object state) {
            return true;
        }

        private void PremiumCalc(object state) {
            InsuredIllustrationParameters = GetInsuredIllustrationParameters();
            IsShowPrmiumCalcNext = true;
            DialogResult = true;
        }

        private static bool ValidatePremiumCalcState(object state) {
            return true;
        }

        private void FutureChanges(object state) {
            InsuredIllustrationParameters = GetInsuredIllustrationParameters();
            IsShowFutureChangesNext = true;
            DialogResult = true;
        }

        private static bool ValidateFutureChangesState(object state) {
            return true;
        }

        private void Refresh(object state) {
            //execute the command
        }

        private static bool ValidateRefreshState(object state) {
            return true;
        }

        private void X_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (isSelectedBeingChanged)
                return;
            if (e.PropertyName.Equals("TermYears") || e.PropertyName.Equals("RemoveTermYears"))
            {
                var result = sender.As<IndividualData>().IsYearRangeValid(out string errorMessage);
                if (!result.HasValue)
                    return;
                if (!result.Value)
                {
                    ShowMessage?.Invoke(this, new ShowMessageEventArgs(errorMessage, "Error", 400, 200, GregOsborne.Dialog.ImagesTypes.Error));
                    return;
                }
            }
            if (e.PropertyName.Equals("IsSelected"))
            {
                isSelectedBeingChanged = true;
                var thisIndex = sender.As<IIndividualData>().Index - 1;
                if (sender.As<IIndividualData>().IsSelected)
                {
                    for (int i = 8; i >= 0; i--)
                    {
                        AdditionalInsureds[i].IsSelected = i > thisIndex ? false : true;
                        AdditionalInsureds[i].Visibility = i > thisIndex ? Visibility.Hidden : Visibility.Visible;
                    }
                    if (thisIndex < AdditionalInsureds.Count - 2)
                        AdditionalInsureds[thisIndex + 1].Visibility = Visibility.Visible;
                }
                else if (thisIndex < AdditionalInsureds.Count - 1)
                {
                    for (int i = thisIndex + 1; i < 9; i++)
                    {
                        AdditionalInsureds[i].Visibility = Visibility.Hidden;
                    }
                }
                isSelectedBeingChanged = false;
            }
        }

        public AdditionalInsuredWindowView() {
            InsuredChildRiderVisibility = Visibility.Hidden;
            InsuredSpouseRiderVisibility = Visibility.Hidden;

            ChildBenefitAmounts = new ObservableCollection<double> { 10000, 7500, 5000, 2500 };
            SpouseBenefitAmounts = new ObservableCollection<double> { 50000, 25000 };
            PlanTypes = new ObservableCollection<SimpleValue>(App.CurrentDataSet.LsClientPlans);
            YearsOrAges = new ObservableCollection<SimpleValue>(App.CurrentDataSet.LsClientRiderOptions);
            TableRatings = new ObservableCollection<SimpleValue>(App.CurrentDataSet.LsSubStandardRatings);
            ColaPercents = new ObservableCollection<SimpleValue>(App.CurrentDataSet.LsClientCOLAs);
            Genders = new ObservableCollection<Gender>(App.Repository.Genders);
            IsInsuredChildRiderChecked = App.Illustration.IsChildRiderSelected;
            AgeYoungestChild = App.Illustration.ChildRiderYoungestAge;
            SelectedChildBenefitAmount = App.Illustration.ChildRiderDeathBenefitAmount;

            App.Illustration.Riders.First(x => x.Index == App.Illustration.Riders.Where(y => y.IndividualType == IndividualData.KnownIndices.Rider).OrderBy(y => y.Index).First(y => !y.IsSelected).Index).Visibility = Visibility.Visible;
            AdditionalInsureds = new ObservableCollection<IIndividualData>(App.Illustration.Riders.Where(x => x.IndividualType == IndividualData.KnownIndices.Rider).OrderBy(x => x.Index));
            AdditionalInsureds.ToList().ForEach(x => x.PropertyChanged += X_PropertyChanged);

            var spouseItem = App.Illustration.Riders.FirstOrDefault(x => x.IndividualType == IndividualData.KnownIndices.Spouse);
            SpouseAge = spouseItem.Age;
            SpouseSelectedPlanType = spouseItem.PlanType;
            SelectedSpouseBenefitAmount = spouseItem.FaceAmount ?? SpouseBenefitAmounts.First();
            SpouseYears = spouseItem.AgeOrYear ?? 95;
            SpouseSelectedYearsOrAge = spouseItem.YearToAgeOption;
            SelectedSpouseTableRating = spouseItem.SubstandardRate;
            SelectedSpouseColaPercent = spouseItem.Cola;
            IsInsuredSpouseRiderChecked = spouseItem.IsSelected;
        }

        public ObservableCollection<Gender> Genders {
            get => _Genders;
            set {
                _Genders = value;
                InvokePropertyChanged(nameof(Genders));
            }
        }

        public ObservableCollection<IIndividualData> AdditionalInsureds {
            get => _AdditionalInsureds;
            set {
                _AdditionalInsureds = value;
                InvokePropertyChanged(nameof(AdditionalInsureds));
            }
        }

        public SimpleValue AdditionalPlanType {
            get => _AdditionalPlanType;
            set {
                _AdditionalPlanType = value;
                InvokePropertyChanged(nameof(AdditionalPlanType));
            }
        }

        public SimpleValue SelectedSpouseColaPercent {
            get => _SelectedSpouseColaPercent;
            set {
                _SelectedSpouseColaPercent = value;
                var insured = App.Illustration.Riders.FirstOrDefault(x => x.IndividualType == IndividualData.KnownIndices.Spouse);
                if (insured != null && insured.IsSelected)
                    insured.Cola = value;
                InvokePropertyChanged(nameof(SelectedSpouseColaPercent));
            }
        }

        public ObservableCollection<SimpleValue> ColaPercents {
            get => _SpouseColaPercents;
            set {
                _SpouseColaPercents = value;
                InvokePropertyChanged(nameof(ColaPercents));
            }
        }

        public ObservableCollection<SimpleValue> TableRatings {
            get => _SpouseTableRatings;
            set {
                _SpouseTableRatings = value;
                InvokePropertyChanged(nameof(TableRatings));
            }
        }

        public SimpleValue SelectedSpouseTableRating {
            get => _SelectedSpouseTableRating;
            set {
                _SelectedSpouseTableRating = value;
                var insured = App.Illustration.Riders.FirstOrDefault(x => x.IndividualType == IndividualData.KnownIndices.Spouse);
                if (insured != null && insured.IsSelected)
                    insured.SubstandardRate = value;
                InvokePropertyChanged(nameof(SelectedSpouseTableRating));
            }
        }

        public ObservableCollection<SimpleValue> YearsOrAges {
            get => _YearsOrAges;
            set {
                _YearsOrAges = value;
                InvokePropertyChanged(nameof(YearsOrAges));
            }
        }

        public int SpouseYears {
            get => _SpouseYears;
            set {
                _SpouseYears = value;
                var insured = App.Illustration.Riders.FirstOrDefault(x => x.IndividualType == IndividualData.KnownIndices.Spouse);
                if (insured != null && insured.IsSelected)
                    insured.AgeOrYear = value;
                InvokePropertyChanged(nameof(SpouseYears));
            }
        }

        public SimpleValue SpouseSelectedYearsOrAge {
            get => _SpouseSelectedYearsOrAge;
            set {
                _SpouseSelectedYearsOrAge = value;
                var insured = App.Illustration.Riders.FirstOrDefault(x => x.IndividualType == IndividualData.KnownIndices.Spouse);
                if (insured != null && insured.IsSelected)
                    insured.YearToAgeOption = value;
                InvokePropertyChanged(nameof(SpouseSelectedYearsOrAge));
            }
        }

        public SimpleValue SpouseSelectedPlanType {
            get => _SpouseSelectedPlanType;
            set {
                _SpouseSelectedPlanType = value;
                var insured = App.Illustration.Riders.FirstOrDefault(x => x.IndividualType == IndividualData.KnownIndices.Spouse);
                if (insured != null && insured.IsSelected)
                    insured.PlanType = value;
                InvokePropertyChanged(nameof(SpouseSelectedPlanType));
            }
        }

        public ObservableCollection<SimpleValue> PlanTypes {
            get => _PlanTypes;
            set {
                _PlanTypes = value;
                InvokePropertyChanged(nameof(PlanTypes));
            }
        }

        public int SpouseAge {
            get => _SpouseAge;
            set {
                _SpouseAge = value;
                var insured = App.Illustration.Riders.FirstOrDefault(x => x.IndividualType == IndividualData.KnownIndices.Spouse);
                if (insured != null && insured.IsSelected)
                    insured.Age = value;
                InvokePropertyChanged(nameof(SpouseAge));
            }
        }

        public bool IsForSpouse {
            get => _IsForSpouse;
            set {
                _IsForSpouse = value;
                InvokePropertyChanged(nameof(IsForSpouse));
            }
        }

        public bool? DialogResult {
            get => _dialogResult;
            set {
                _dialogResult = value;
                UpdateInterface();
                InvokePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
            }
        }

        public double SelectedChildBenefitAmount {
            get => App.Illustration.ChildRiderDeathBenefitAmount;
            set {
                App.Illustration.ChildRiderDeathBenefitAmount = value;
                InvokePropertyChanged(nameof(SelectedChildBenefitAmount));
            }
        }
        public ObservableCollection<double> ChildBenefitAmounts {
            get => _ChildBenefitAmounts;
            set {
                _ChildBenefitAmounts = value;
                InvokePropertyChanged(nameof(ChildBenefitAmounts));
            }
        }

        public double SelectedSpouseBenefitAmount {
            get => _SelectedSpouseBenefitAmount;
            set {
                _SelectedSpouseBenefitAmount = value;
                var insured = App.Illustration.Riders.FirstOrDefault(x => x.IndividualType == IndividualData.KnownIndices.Spouse);
                if (insured != null && insured.IsSelected)
                    insured.FaceAmount = value;
                InvokePropertyChanged(nameof(SelectedSpouseBenefitAmount));
            }
        }
        public ObservableCollection<double> SpouseBenefitAmounts {
            get => _SpouseBenefitAmounts;
            set {
                _SpouseBenefitAmounts = value;
                InvokePropertyChanged(nameof(SpouseBenefitAmounts));
            }
        }

        public Visibility InsuredChildRiderVisibility {
            get => _InsuredChildRiderVisibility;
            set {
                _InsuredChildRiderVisibility = value;
                InvokePropertyChanged(nameof(InsuredChildRiderVisibility));
            }
        }

        public int AgeYoungestChild {
            get => App.Illustration.ChildRiderYoungestAge;
            set {
                App.Illustration.ChildRiderYoungestAge = value;
                InvokePropertyChanged(nameof(AgeYoungestChild));
            }
        }

        public bool IsInsuredChildRiderChecked {
            get => App.Illustration.IsChildRiderSelected;
            set {
                App.Illustration.IsChildRiderSelected = value;
                InsuredChildRiderVisibility = value ? Visibility.Visible : Visibility.Collapsed;
                InvokePropertyChanged(nameof(IsInsuredChildRiderChecked));
            }
        }

        public Visibility InsuredSpouseRiderVisibility {
            get => _InsuredSpouseRiderVisibility;
            set {
                _InsuredSpouseRiderVisibility = value;
                InvokePropertyChanged(nameof(InsuredSpouseRiderVisibility));
            }
        }

        public bool IsInsuredSpouseRiderChecked {
            get => _IsInsuredSpouseRiderChecked;
            set {
                _IsInsuredSpouseRiderChecked = value;
                var insured = App.Illustration.Riders.FirstOrDefault(x => x.IndividualType == IndividualData.KnownIndices.Spouse);
                if (insured != null)
                    insured.IsSelected = value;
                InsuredSpouseRiderVisibility = value ? Visibility.Visible : Visibility.Hidden;
                InvokePropertyChanged(nameof(IsInsuredSpouseRiderChecked));
            }
        }

        public bool IsShowPrmiumCalcNext {
            get => _IsShowPrmiumCalcNext;
            set {
                _IsShowPrmiumCalcNext = value;
                InvokePropertyChanged(nameof(IsShowPrmiumCalcNext));
            }
        }

        public bool IsShowFutureChangesNext {
            get => _IsShowFutureChangesNext;
            set {
                _IsShowFutureChangesNext = value;
                InvokePropertyChanged(nameof(IsShowFutureChangesNext));
            }
        }
    }
}
