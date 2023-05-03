using GregOsborne.Application.Primitives;
using Life.Savings.Data.Model.Application;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Life.Savings.Data.Model
{
    public class IllustrationInfo
    {
        public IllustrationInfo()
        {
            ClientData = new IndividualData();
            ClientData.PropertyChanged += LocalPropertyChanged;
            Riders = new ObservableCollection<IIndividualData>();
            Riders.As<ObservableCollection<IIndividualData>>().CollectionChanged += LocalCollectionChanged;

            FutureSpecificDeathBenefits = new ObservableCollection<FutureAgeValue>
            {
                new FutureAgeValue(),
                new FutureAgeValue(),
                new FutureAgeValue(),
                new FutureAgeValue(),
                new FutureAgeValue()
            };
            FutureModalPremiums = new ObservableCollection<FutureAgeValue>
            {
                new FutureAgeValue(),
                new FutureAgeValue(),
                new FutureAgeValue(),
                new FutureAgeValue(),
                new FutureAgeValue()
            };
            FutureCurrentInterestRates = new ObservableCollection<FutureAgeValue>
            {
                new FutureAgeValue(),
                new FutureAgeValue(),
                new FutureAgeValue(),
                new FutureAgeValue(),
                new FutureAgeValue()
            };
            FutureDeathBenefitOptions = new ObservableCollection<FutureAgeValue>
            {
                new FutureAgeValue(),
                new FutureAgeValue(),
                new FutureAgeValue(),
                new FutureAgeValue(),
                new FutureAgeValue()
            };
            FutureWithdrawls = new ObservableCollection<FutureAgeValueWithEndAge>
            {
                new FutureAgeValueWithEndAge(),
                new FutureAgeValueWithEndAge(),
                new FutureAgeValueWithEndAge(),
                new FutureAgeValueWithEndAge(),
                new FutureAgeValueWithEndAge()
            };
            FutureAnnualPolicyLoans = new ObservableCollection<FutureAgeValueWithEndAge>
            {
                new FutureAgeValueWithEndAge(),
                new FutureAgeValueWithEndAge(),
                new FutureAgeValueWithEndAge(),
                new FutureAgeValueWithEndAge(),
                new FutureAgeValueWithEndAge()
            };
            FutureAnnualLoanRepayments = new ObservableCollection<FutureAgeValueWithEndAge>
            {
                new FutureAgeValueWithEndAge(),
                new FutureAgeValueWithEndAge(),
                new FutureAgeValueWithEndAge(),
                new FutureAgeValueWithEndAge(),
                new FutureAgeValueWithEndAge()
            };
            HighlightedAges = new List<int> { 0, 0, 0, 0, 0 };
            FutureSpecificDeathBenefits.ToList().ForEach(x => x.PropertyChanged += LocalPropertyChanged);
            FutureModalPremiums.ToList().ForEach(x => x.PropertyChanged += LocalPropertyChanged);
            FutureCurrentInterestRates.ToList().ForEach(x => x.PropertyChanged += LocalPropertyChanged);
            FutureDeathBenefitOptions.ToList().ForEach(x => x.PropertyChanged += LocalPropertyChanged);
            FutureWithdrawls.ToList().ForEach(x => x.PropertyChanged += LocalPropertyChanged);
            FutureAnnualPolicyLoans.ToList().ForEach(x => x.PropertyChanged += LocalPropertyChanged);
            FutureAnnualLoanRepayments.ToList().ForEach(x => x.PropertyChanged += LocalPropertyChanged);
            IsChanged = false;
        }
        public void Reset()
        {
            IsChanged = false;
        }
        private void LocalCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            IsChanged = true;
        }

        private void LocalPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IsChanged = true;
        }


        private IList<int> _HighlightedAges;
        public IList<int> HighlightedAges {
            get => _HighlightedAges;
            set {
                _HighlightedAges = value;
                IsChanged = true;
            }
        }

        public IndividualData ClientData { get; set; }
        public IndividualData SpouseAsClientData { get; set; }
        public IList<IIndividualData> Riders { get; set; }


        private bool _IsSpousePrincipalInsured;
        public bool IsSpousePrincipalInsured {
            get => _IsSpousePrincipalInsured;
            set {
                _IsSpousePrincipalInsured = value;
                IsChanged = true;
            }
        }

        private bool _IsClientGuidelineChecked;
        public bool IsClientGuidelineChecked {
            get => _IsClientGuidelineChecked;
            set {
                _IsClientGuidelineChecked = value;
                IsChanged = true;
            }
        }

        private bool _IsClientTargetChecked;
        public bool IsClientTargetChecked {
            get => _IsClientTargetChecked;
            set {
                _IsClientTargetChecked = value;
                IsChanged = true;
            }
        }

        private bool _IsClientMinimumChecked;
        public bool IsClientMinimumChecked {
            get => _IsClientMinimumChecked;
            set {
                _IsClientMinimumChecked = value;
                IsChanged = true;
            }
        }

        public bool HasSpouseData { get { return SpouseAsClientData != null; } }

        private bool _IsSpouseGuidelineChecked;
        public bool IsSpouseGuidelineChecked {
            get => _IsSpouseGuidelineChecked;
            set {
                _IsSpouseGuidelineChecked = value;
                IsChanged = true;
            }
        }

        private bool _IsSpouseTargetChecked;
        public bool IsSpouseTargetChecked {
            get => _IsSpouseTargetChecked;
            set {
                _IsSpouseTargetChecked = value;
                IsChanged = true;
            }
        }

        private bool _IsSpouseMinimumChecked;
        public bool IsSpouseMinimumChecked {
            get => _IsSpouseMinimumChecked;
            set {
                _IsSpouseMinimumChecked = value;
                IsChanged = true;
            }
        }

        private bool _IsChildRiderSelected;
        public bool IsChildRiderSelected {
            get => _IsChildRiderSelected;
            set {
                _IsChildRiderSelected = value;
                IsChanged = true;
            }
        }

        private int _ChildRiderYoungestAge;
        public int ChildRiderYoungestAge {
            get => _ChildRiderYoungestAge;
            set {
                _ChildRiderYoungestAge = value;
                IsChanged = true;
            }
        }

        private double _ChildRiderDeathBenefitAmount;
        public double ChildRiderDeathBenefitAmount {
            get => _ChildRiderDeathBenefitAmount;
            set {
                _ChildRiderDeathBenefitAmount = value;
                IsChanged = true;
            }
        }

        private SimpleValue _PremiumMode;
        public SimpleValue PremiumMode {
            get => _PremiumMode;
            set {
                _PremiumMode = value;
                IsChanged = true;
            }
        }

        private SimpleValue _PremiumYearsToPay;
        public SimpleValue PremiumYearsToPay {
            get => _PremiumYearsToPay;
            set {
                _PremiumYearsToPay = value;
                IsChanged = true;
            }
        }

        private double _PremiumInterestRate;
        public double PremiumInterestRate {
            get => _PremiumInterestRate;
            set {
                _PremiumInterestRate = value;
                IsChanged = true;
            }
        }

        private double _PlannedModalPremium;
        public double PlannedModalPremium {
            get => _PlannedModalPremium;
            set {
                _PlannedModalPremium = value;
                IsChanged = true;
            }
        }

        private double _InitialLumpSumAmount;
        public double InitialLumpSumAmount {
            get => _InitialLumpSumAmount;
            set {
                _InitialLumpSumAmount = value;
                IsChanged = true;
            }
        }

        private double _MinimumModalPremium;
        public double MinimumModalPremium {
            get => _MinimumModalPremium;
            set {
                _MinimumModalPremium = value;
                IsChanged = true;
            }
        }

        private double _TargetPremium;
        public double TargetPremium {
            get => _TargetPremium;
            set {
                _TargetPremium = value;
                IsChanged = true;
            }
        }

        private double _MinGuidanceAnnualPremium;
        public double MinGuidanceAnnualPremium {
            get => _MinGuidanceAnnualPremium;
            set {
                _MinGuidanceAnnualPremium = value;
                IsChanged = true;
            }
        }

        private double _MinGuidanceSinglePremiumPrincipal;
        public double MinGuidanceSinglePremiumPrincipal {
            get => _MinGuidanceSinglePremiumPrincipal;
            set {
                _MinGuidanceSinglePremiumPrincipal = value;
                IsChanged = true;
            }
        }

        private double _MinGuidanceSinglePremiumAdditional;
        public double MinGuidanceSinglePremiumAdditional {
            get => _MinGuidanceSinglePremiumAdditional;
            set {
                _MinGuidanceSinglePremiumAdditional = value;
                IsChanged = true;
            }
        }

        private double _SpecialOptionsCashValue;
        public double SpecialOptionsCashValue {
            get => _SpecialOptionsCashValue;
            set {
                _SpecialOptionsCashValue = value;
                IsChanged = true;
            }
        }

        private string _SpecialOptionsModalPremium;
        public string SpecialOptionsModalPremium {
            get => _SpecialOptionsModalPremium;
            set {
                _SpecialOptionsModalPremium = value;
                IsChanged = true;
            }
        }

        private double _IllustrateInitialCashValue;
        public double IllustrateInitialCashValue {
            get => _IllustrateInitialCashValue;
            set {
                _IllustrateInitialCashValue = value;
                IsChanged = true;
            }
        }

        private int _SpecialOptionsAge;
        public int SpecialOptionsAge {
            get => _SpecialOptionsAge;
            set {
                _SpecialOptionsAge = value;
                IsChanged = true;
            }
        }

        private int _IllustrateBeginAtAge;
        public int IllustrateBeginAtAge {
            get => _IllustrateBeginAtAge;
            set {
                _IllustrateBeginAtAge = value;
                IsChanged = true;
            }
        }

        private string _InForceYears;
        public string InForceYears {
            get => _InForceYears;
            set {
                _InForceYears = value;
                IsChanged = true;
            }
        }

        public IList<FutureAgeValue> FutureSpecificDeathBenefits { get; set; }
        public IList<FutureAgeValue> FutureModalPremiums { get; set; }
        public IList<FutureAgeValue> FutureCurrentInterestRates { get; set; }
        public IList<FutureAgeValue> FutureDeathBenefitOptions { get; set; }
        public IList<FutureAgeValueWithEndAge> FutureWithdrawls { get; set; }
        public IList<FutureAgeValueWithEndAge> FutureAnnualPolicyLoans { get; set; }
        public IList<FutureAgeValueWithEndAge> FutureAnnualLoanRepayments { get; set; }

        private string _PreparedBy;
        public string PreparedBy {
            get => _PreparedBy;
            set {
                _PreparedBy = value;
                IsChanged = true;
            }
        }

        private string _ContactTelephoneNumber;
        public string ContactTelephoneNumber {
            get => _ContactTelephoneNumber;
            set {
                _ContactTelephoneNumber = value;
                IsChanged = true;
            }
        }

        public bool IsChanged { get; set; }
    }
}
