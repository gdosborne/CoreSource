using System;
using System.Collections.ObjectModel;
using System.Linq;
using GregOsborne.MVVMFramework;
using Life.Savings.Data;
using Life.Savings.Data.Model;

namespace Life.Savings
{
    public class SpouseDataWindowView : ViewModelBase
    {
        private IRepository _repository;
        public IRepository Repository {
            get => _repository;
            set {
                _repository = value;
                if (value != null)
                {
                    Genders = new ObservableCollection<Gender>(Repository.Genders);
                    States = new ObservableCollection<State>(Repository.States);
                    var appData = Repository.IsLS3DataSelected ? Repository.Ls3Data : Repository.Ls2Data;
                    SubstandardRates = new ObservableCollection<SimpleValue>(appData.LsSubStandardRatings);
                    ClientWpds = new ObservableCollection<SimpleValue>(appData.LsClientWPDs);
                    ClientGpos = new ObservableCollection<SimpleValue>(appData.LsClientGPOs);
                    ClientColas = new ObservableCollection<SimpleValue>(appData.LsClientCOLAs);
                    ClientYearToAgeOptions = new ObservableCollection<SimpleValue>(appData.LsClientRiderOptions);
                    ClientBenfitOptions = new ObservableCollection<SimpleValue>(appData.LsClientOptions);
                    ClientPlanTypes = new ObservableCollection<SimpleValue>(appData.LsClientPlans);

                    if (App.Illustration.SpouseAsClientData != null)
                    {
                        SpouseGender = App.Illustration.SpouseAsClientData.Gender ?? Repository.Genders.FirstOrDefault(x => x.Abbreviation.Equals('U'));
                        SpouseState = App.Illustration.SpouseAsClientData.State ?? App.DefaultState;
                        IsSpousePrincipalInsured = App.Illustration.IsSpousePrincipalInsured;

                        SpouseSubStandardRate = App.Illustration.SpouseAsClientData.SubstandardRate ?? SubstandardRates.FirstOrDefault(x => x.Id == 0);
                        SpouseWpd = App.Illustration.SpouseAsClientData.Wpd ?? ClientWpds.FirstOrDefault(x => x.Id == 0);
                        SpouseGpo = App.Illustration.SpouseAsClientData.Gpo ?? ClientGpos.FirstOrDefault(x => x.Id == 0);
                        SpouseCola = App.Illustration.SpouseAsClientData.Cola ?? ClientColas.FirstOrDefault(x => x.Id == 0);
                        SpouseYearToAgeOption = App.Illustration.SpouseAsClientData.YearToAgeOption ?? ClientYearToAgeOptions.FirstOrDefault(x => x.Id == 0);
                        SpouseBenefitOption = App.Illustration.SpouseAsClientData.BenefitOption ?? ClientBenfitOptions.FirstOrDefault(x => x.Id == 0);
                        SpousePlanType = App.Illustration.SpouseAsClientData.PlanType ?? ClientPlanTypes.FirstOrDefault(x => x.Id == 0);
                    }
                    IsSpouseGuidelineChecked = true;
                }

                InvokePropertyChanged(nameof(Repository));
            }
        }

        public event EventHandler ShowPremiumCalc;
        private DelegateCommand _premiumCommand = null;
        public DelegateCommand PremiumCommand => _premiumCommand ?? (_premiumCommand = new DelegateCommand(Premium, ValidatePremiumState));
        private void Premium(object state)
        {
            ShowPremiumCalc?.Invoke(this, EventArgs.Empty);
        }
        private bool ValidatePremiumState(object state)
        {
            return true;
        }

        private ObservableCollection<Gender> _genders;
        public ObservableCollection<Gender> Genders {
            get => _genders;
            set {
                _genders = value;
                InvokePropertyChanged(nameof(Genders));
            }
        }

        private ObservableCollection<State> _states;
        public ObservableCollection<State> States {
            get => _states;
            set {
                _states = value;
                InvokePropertyChanged(nameof(States));
            }
        }

        public SimpleValue SpouseYearToAgeOption {
            get => App.Illustration.SpouseAsClientData?.YearToAgeOption;
            set {
                App.Illustration.SpouseAsClientData.YearToAgeOption = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(SpouseYearToAgeOption));
            }
        }

        public State SpouseState {
            get => App.Illustration.SpouseAsClientData?.State;
            set {
                if (value == null)
                    value = App.DefaultState;
                App.Illustration.SpouseAsClientData.State = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(SpouseState));
            }
        }

        public Gender SpouseGender {
            get => App.Illustration.SpouseAsClientData?.Gender;
            set {
                if (value == null)
                    value = Genders.FirstOrDefault(x => x.Id == Genders.Min(y => y.Id));
                App.Illustration.SpouseAsClientData.Gender = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(SpouseGender));
            }
        }

        public string SpouseFirstName {
            get => App.Illustration.SpouseAsClientData?.FirstName;
            set {
                App.Illustration.SpouseAsClientData.FirstName = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(SpouseFirstName));
            }
        }

        public string SpouseLastName {
            get => App.Illustration.SpouseAsClientData?.LastName;
            set {
                App.Illustration.SpouseAsClientData.LastName = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(SpouseLastName));
            }
        }

        public string SpouseMiddleInitial {
            get => App.Illustration.SpouseAsClientData?.MiddleInitial;
            set {
                App.Illustration.SpouseAsClientData.MiddleInitial = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(SpouseMiddleInitial));
            }
        }

        public int SpouseAge {
            get {
                if (App.Illustration.SpouseAsClientData != null)
                    return App.Illustration.SpouseAsClientData.Age;
                return 0;
            }
            set {
                if (App.Illustration.SpouseAsClientData != null)
                    App.Illustration.SpouseAsClientData.Age = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(SpouseAge));
            }
        }

        public int SpouseTermYears {
            get => App.Illustration.SpouseAsClientData?.AgeOrYear ?? 0;
            set {
                App.Illustration.SpouseAsClientData.AgeOrYear = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(SpouseTermYears));
            }
        }

        public double SpouseInterestRate {
            get => App.Illustration.SpouseAsClientData?.InterestRate ?? 0;
            set {
                App.Illustration.SpouseAsClientData.InterestRate = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(SpouseInterestRate));
            }
        }

        public double SpouseInitialDeathBenefit {
            get => App.Illustration.SpouseAsClientData?.InitialDeathBenefit ?? 0;
            set {
                App.Illustration.SpouseAsClientData.InitialDeathBenefit = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(SpouseInitialDeathBenefit));
            }
        }

        public bool IsSpouseGuidelineChecked {
            get => App.Illustration.IsSpouseGuidelineChecked;
            set {
                App.Illustration.IsSpouseGuidelineChecked = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(IsSpouseGuidelineChecked));
            }
        }

        public bool IsSpouseTargetChecked {
            get => App.Illustration.IsSpouseTargetChecked;
            set {
                App.Illustration.IsSpouseTargetChecked = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(IsSpouseTargetChecked));
            }
        }

        public bool IsSpouseMinimumChecked {
            get => App.Illustration.IsSpouseMinimumChecked;
            set {
                App.Illustration.IsSpouseMinimumChecked = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(IsSpouseMinimumChecked));
            }
        }

        public double SpouseDeathBenefitAmount {
            get => App.Illustration.SpouseAsClientData?.FaceAmount ?? 0;
            set {
                App.Illustration.SpouseAsClientData.FaceAmount = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(SpouseDeathBenefitAmount));
            }
        }
        private DateTime? _SpouseBirthDate;
        public DateTime? SpouseBirthDate {
            get => _SpouseBirthDate;
            set {
                _SpouseBirthDate = value;

                if (value.HasValue)
                {
                    int age = DateTime.Now.Year - value.Value.Year;
                    if (DateTime.Now.Month < value.Value.Month || (DateTime.Now.Month == value.Value.Month && DateTime.Now.Day < value.Value.Day))
                        age--;
                    SpouseAge = age;
                }

                UpdateInterface();
                InvokePropertyChanged(nameof(SpouseBirthDate));
            }
        }
        private DelegateCommand _SpouseOKCommand;
        public DelegateCommand SpouseOKCommand => _SpouseOKCommand ?? (_SpouseOKCommand = new DelegateCommand(SpouseOK, ValidateSpouseOKState));
        private void SpouseOK(object state)
        {
            DialogResult = true;
        }
        private bool ValidateSpouseOKState(object state)
        {
            return !string.IsNullOrEmpty(SpouseFirstName)
                   && !string.IsNullOrEmpty(SpouseLastName)
                   && SpouseAge > 0
                   && SpouseGender != null
                   && SpouseState != null
                   && SpouseInitialDeathBenefit > 0
                   && SpouseInterestRate > 0;
        }

        private bool? _DialogResult;
        public bool? DialogResult {
            get => _DialogResult;
            set {
                _DialogResult = value;
                InvokePropertyChanged(nameof(DialogResult));
            }
        }
        private DelegateCommand _SpouseCancelCommand;
        public DelegateCommand SpouseCancelCommand => _SpouseCancelCommand ?? (_SpouseCancelCommand = new DelegateCommand(SpouseCancel, ValidateSpouseCancelState));
        private void SpouseCancel(object state)
        {
            SpouseFirstName = string.Empty;
            SpouseMiddleInitial = String.Empty;
            SpouseLastName = string.Empty;
            SpouseAge = 0;
            SpouseBirthDate = null;
            SpouseGender = null;
            SpouseState = null;
            SpousePlanType = null;
            SpouseInitialDeathBenefit = 0;
            SpouseInterestRate = 0;
            SpouseSubStandardRate = null;
            SpouseGpo = null;
            SpouseWpd = null;
            SpouseCola = null;
            SpouseBenefitOption = null;
            SpouseDeathBenefitAmount = 0;
            SpouseYearToAgeOption = null;
            IsSpouseGuidelineChecked = false;
            DialogResult = false;
        }
        private bool ValidateSpouseCancelState(object state)
        {
            return true;
        }

        private ObservableCollection<SimpleValue> _SubstandardRates;
        public ObservableCollection<SimpleValue> SubstandardRates {
            get => _SubstandardRates;
            set {
                _SubstandardRates = value;
                InvokePropertyChanged(nameof(SubstandardRates));
            }
        }

        public SimpleValue SpouseSubStandardRate {
            get => App.Illustration.SpouseAsClientData?.SubstandardRate;
            set {
                App.Illustration.SpouseAsClientData.SubstandardRate = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(SpouseSubStandardRate));
            }
        }

        private ObservableCollection<SimpleValue> _ClientWpds;
        public ObservableCollection<SimpleValue> ClientWpds {
            get => _ClientWpds;
            set {
                _ClientWpds = value;
                InvokePropertyChanged(nameof(ClientWpds));
            }
        }

        public SimpleValue SpouseWpd {
            get => App.Illustration.SpouseAsClientData.Wpd;
            set {
                App.Illustration.SpouseAsClientData.Wpd = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(SpouseWpd));
            }
        }

        public bool IsSpousePrincipalInsured {
            get => App.Illustration.IsSpousePrincipalInsured;
            set {
                App.Illustration.IsSpousePrincipalInsured = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(IsSpousePrincipalInsured));
            }
        }
        private ObservableCollection<SimpleValue> _ClientGpos;
        public ObservableCollection<SimpleValue> ClientGpos {
            get => _ClientGpos;
            set {
                _ClientGpos = value;
                InvokePropertyChanged(nameof(ClientGpos));
            }
        }

        public SimpleValue SpouseGpo {
            get => App.Illustration.SpouseAsClientData?.Gpo;
            set {
                App.Illustration.SpouseAsClientData.Gpo = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(SpouseGpo));
            }
        }

        private ObservableCollection<SimpleValue> _ClientColas;
        public ObservableCollection<SimpleValue> ClientColas {
            get => _ClientColas;
            set {
                _ClientColas = value;
                InvokePropertyChanged(nameof(ClientColas));
            }
        }

        public SimpleValue SpouseCola {
            get => App.Illustration.SpouseAsClientData?.Cola;
            set {
                App.Illustration.SpouseAsClientData.Cola = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(SpouseCola));
            }
        }

        private ObservableCollection<SimpleValue> _ClientYearToAgeOptions;
        public ObservableCollection<SimpleValue> ClientYearToAgeOptions {
            get => _ClientYearToAgeOptions;
            set {
                _ClientYearToAgeOptions = value;
                InvokePropertyChanged(nameof(ClientYearToAgeOptions));
            }
        }

        private ObservableCollection<SimpleValue> _ClientBenfitOptions;
        public ObservableCollection<SimpleValue> ClientBenfitOptions {
            get => _ClientBenfitOptions;
            set {
                _ClientBenfitOptions = value;
                InvokePropertyChanged(nameof(ClientBenfitOptions));
            }
        }

        public SimpleValue SpouseBenefitOption {
            get => App.Illustration.SpouseAsClientData?.BenefitOption;
            set {
                App.Illustration.SpouseAsClientData.BenefitOption = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(SpouseBenefitOption));
            }
        }

        private ObservableCollection<SimpleValue> _ClientPlanTypes;
        public ObservableCollection<SimpleValue> ClientPlanTypes {
            get => _ClientPlanTypes;
            set {
                _ClientPlanTypes = value;
                InvokePropertyChanged(nameof(ClientPlanTypes));
            }
        }

        public SimpleValue SpousePlanType {
            get => App.Illustration.SpouseAsClientData?.PlanType;
            set {
                if (value == null)
                    value = ClientPlanTypes.FirstOrDefault(x => x.Id == 0);
                App.Illustration.SpouseAsClientData.PlanType = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(SpousePlanType));
            }
        }
    }
}
