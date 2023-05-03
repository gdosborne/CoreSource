using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using GregOsborne.MVVMFramework;
using Life.Savings.Data.Model.Application;

namespace Life.Savings
{
    public class FutureYearsWindowView : ViewModelBase
    {
        public FutureYearsWindowView()
        {
            SpecifiedDeathBenefits = new ObservableCollection<FutureAgeValue>(App.Illustration.FutureSpecificDeathBenefits);
            ModalPremiums = new ObservableCollection<FutureAgeValue>(App.Illustration.FutureModalPremiums);
            CurrentInterestRates = new ObservableCollection<FutureAgeValue>(App.Illustration.FutureCurrentInterestRates);
            DeathBenefitOptions = new ObservableCollection<FutureAgeValue>(App.Illustration.FutureDeathBenefitOptions);
            Withdrawls = new ObservableCollection<FutureAgeValueWithEndAge>(App.Illustration.FutureWithdrawls);
            AnnualPolicyLoans = new ObservableCollection<FutureAgeValueWithEndAge>(App.Illustration.FutureAnnualPolicyLoans);
            AnnualLoanRepayments = new ObservableCollection<FutureAgeValueWithEndAge>(App.Illustration.FutureAnnualLoanRepayments);

            IsSpecialDeathBenefitChecked = SpecifiedDeathBenefits.Any(x => x.Age > 0 && x.Value > 0);
            IsModalPremiumAmountChecked = ModalPremiums.Any(x => x.Age > 0 && x.Value > 0);
            IsCurrentInterestRateChecked = CurrentInterestRates.Any(x => x.Age > 0 && x.Value > 0);
            IsDeathBenefitOptionOptionChecked = DeathBenefitOptions.Any(x => x.Age > 0 && x.Value > 0);
            IsWithdrawlsChecked = Withdrawls.Any(x => x.Age > 0 && x.Value > 0 && x.EndAge > x.Age);
            IsAnnualPolicyLoanChecked = AnnualPolicyLoans.Any(x => x.Age > 0 && x.Value > 0 && x.EndAge > x.Age);
            IsAnnualLoanRepaymentsChecked = AnnualLoanRepayments.Any(x => x.Age > 0 && x.Value > 0 && x.EndAge > x.Age);

            SpecifiedDeathBenefits.ToList().ForEach(x => x.PropertyChanged += X_PropertyChanged);
            ModalPremiums.ToList().ForEach(x => x.PropertyChanged += X_PropertyChanged);
            CurrentInterestRates.ToList().ForEach(x => x.PropertyChanged += X_PropertyChanged);
            DeathBenefitOptions.ToList().ForEach(x => x.PropertyChanged += X_PropertyChanged);
            Withdrawls.ToList().ForEach(x => x.PropertyChanged += X_PropertyChanged);
            AnnualPolicyLoans.ToList().ForEach(x => x.PropertyChanged += X_PropertyChanged);
            AnnualLoanRepayments.ToList().ForEach(x => x.PropertyChanged += X_PropertyChanged);
        }

        private void X_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) => InvokePropertyChanged(e.PropertyName);

        private bool _IsForSpouse;
        public bool IsForSpouse {
            get => _IsForSpouse;
            set {
                _IsForSpouse = value;
                InvokePropertyChanged(nameof(IsForSpouse));
            }
        }

        private bool? _dialogResult;
        public bool? DialogResult {
            get => _dialogResult;
            set {
                _dialogResult = value;
                UpdateInterface();
                InvokePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
            }
        }
        private DelegateCommand _returnToClientCommand = null;
        public DelegateCommand ReturnToClientCommand => _returnToClientCommand ?? (_returnToClientCommand = new DelegateCommand(ReturnToClient, ValidateReturnToClientState));
        private void ReturnToClient(object state)
        {
            UpdateValues();
            DialogResult = false;
        }
        private void UpdateValues()
        {
            var index = 0;
            SpecifiedDeathBenefits.Where(x => x.Age > 0 && x.Value > 0).ToList().ForEach(x => {
                App.Illustration.FutureSpecificDeathBenefits[index] = x;
                index++;
            });
            index = 0;
            ModalPremiums.Where(x => x.Age > 0 && x.Value > 0).ToList().ForEach(x => {
                App.Illustration.FutureModalPremiums[index] = x;
                index++;
            });
            index = 0;
            CurrentInterestRates.Where(x => x.Age > 0 && x.Value > 0).ToList().ForEach(x => {
                App.Illustration.FutureCurrentInterestRates[index] = x;
                index++;
            });
            index = 0;
            DeathBenefitOptions.Where(x => x.Age > 0 && x.Value > 0).ToList().ForEach(x => {
                App.Illustration.FutureDeathBenefitOptions[index] = x;
                index++;
            });
            index = 0;
            Withdrawls.Where(x => x.Age > 0 && x.Value > 0 && x.EndAge > x.Age).ToList().ForEach(x => {
                App.Illustration.FutureWithdrawls[index] = x;
                index++;
            });
            index = 0;
            AnnualPolicyLoans.Where(x => x.Age > 0 && x.Value > 0 && x.EndAge > x.Age).ToList().ForEach(x => {
                App.Illustration.FutureAnnualPolicyLoans[index] = x;
                index++;
            });
            index = 0;
            AnnualLoanRepayments.Where(x => x.Age > 0 && x.Value > 0 && x.EndAge > x.Age).ToList().ForEach(x => {
                App.Illustration.FutureAnnualLoanRepayments[index] = x;
                index++;
            });
            SpecifiedDeathBenefits.ToList().ForEach(x => x.PropertyChanged -= X_PropertyChanged);
            ModalPremiums.ToList().ForEach(x => x.PropertyChanged -= X_PropertyChanged);
            CurrentInterestRates.ToList().ForEach(x => x.PropertyChanged -= X_PropertyChanged);
            DeathBenefitOptions.ToList().ForEach(x => x.PropertyChanged -= X_PropertyChanged);
            Withdrawls.ToList().ForEach(x => x.PropertyChanged -= X_PropertyChanged);
            AnnualPolicyLoans.ToList().ForEach(x => x.PropertyChanged -= X_PropertyChanged);
            AnnualLoanRepayments.ToList().ForEach(x => x.PropertyChanged -= X_PropertyChanged);
        }
        private bool ValidateReturnToClientState(object state)
        {
            return true;
        }
        private DelegateCommand _InsuredRidersCommand;
        public DelegateCommand InsuredRidersCommand => _InsuredRidersCommand ?? (_InsuredRidersCommand = new DelegateCommand(InsuredRiders, ValidateInsuredRidersState));
        private void InsuredRiders(object state)
        {
            UpdateValues();
            IsInsuredRidersNext = true;
            DialogResult = true;
        }
        private static bool ValidateInsuredRidersState(object state)
        {
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
        private bool _IsShowPrmiumCalcNext;
        public bool IsShowPrmiumCalcNext {
            get => _IsShowPrmiumCalcNext;
            set {
                _IsShowPrmiumCalcNext = value;
                InvokePropertyChanged(nameof(IsShowPrmiumCalcNext));
            }
        }

        private DelegateCommand _PremiumCalcCommand;
        public DelegateCommand PremiumCalcCommand => _PremiumCalcCommand ?? (_PremiumCalcCommand = new DelegateCommand(PremiumCalc, ValidatePremiumCalcState));
        private void PremiumCalc(object state)
        {
            UpdateValues();
            IsShowPrmiumCalcNext = true;
            DialogResult = true;
        }
        private static bool ValidatePremiumCalcState(object state)
        {
            return true;
        }

        private bool _IsSpecialDeathBenefitChecked;
        public bool IsSpecialDeathBenefitChecked {
            get => _IsSpecialDeathBenefitChecked;
            set {
                _IsSpecialDeathBenefitChecked = value;
                if (!IsSpecialDeathBenefitChecked)
                {
                    SpecifiedDeathBenefits.ToList().ForEach(x => {
                        x.Age = 0;
                        x.Value = null;
                    });
                }
                SpecifiedDeathBenefitVisibility = value ? Visibility.Visible : Visibility.Collapsed;
                InvokePropertyChanged(nameof(IsSpecialDeathBenefitChecked));
            }
        }

        private bool _IsModalPremiumAmountChecked;
        public bool IsModalPremiumAmountChecked {
            get => _IsModalPremiumAmountChecked;
            set {
                _IsModalPremiumAmountChecked = value;
                if (!IsModalPremiumAmountChecked)
                {
                    ModalPremiums.ToList().ForEach(x => {
                        x.Age = 0;
                        x.Value = null;
                    });
                }
                ModalPremiumAmountVisibility = value ? Visibility.Visible : Visibility.Collapsed;
                InvokePropertyChanged(nameof(IsModalPremiumAmountChecked));
            }
        }

        private bool _IsCurrentInterestRateChecked;
        public bool IsCurrentInterestRateChecked {
            get => _IsCurrentInterestRateChecked;
            set {
                _IsCurrentInterestRateChecked = value;
                if (!IsCurrentInterestRateChecked)
                {
                    CurrentInterestRates.ToList().ForEach(x => {
                        x.Age = 0;
                        x.Value = null;
                    });
                }
                CurrentInterestRateVisibility = value ? Visibility.Visible : Visibility.Collapsed;
                InvokePropertyChanged(nameof(IsCurrentInterestRateChecked));
            }
        }

        private bool _IsDeathBenefitOptionOptionChecked;
        public bool IsDeathBenefitOptionOptionChecked {
            get => _IsDeathBenefitOptionOptionChecked;
            set {
                _IsDeathBenefitOptionOptionChecked = value;
                if (!IsDeathBenefitOptionOptionChecked)
                {
                    DeathBenefitOptions.ToList().ForEach(x => {
                        x.Age = 0;
                        x.Value = null;
                    });
                }
                DeathBenefitOptionVisibility = value ? Visibility.Visible : Visibility.Collapsed;
                InvokePropertyChanged(nameof(IsDeathBenefitOptionOptionChecked));
            }
        }

        private bool _IsWithdrawlsChecked;
        public bool IsWithdrawlsChecked {
            get => _IsWithdrawlsChecked;
            set {
                _IsWithdrawlsChecked = value;
                if (!IsWithdrawlsChecked)
                {
                    Withdrawls.ToList().ForEach(x => {
                        x.Age = 0;
                        x.Value = null;
                    });
                }
                WithdrawlsVisibility = value ? Visibility.Visible : Visibility.Collapsed;
                InvokePropertyChanged(nameof(IsWithdrawlsChecked));
            }
        }

        private bool _IsAnnualPolicyLoanChecked;
        public bool IsAnnualPolicyLoanChecked {
            get => _IsAnnualPolicyLoanChecked;
            set {
                _IsAnnualPolicyLoanChecked = value;
                if (!IsAnnualPolicyLoanChecked)
                {
                    AnnualPolicyLoans.ToList().ForEach(x => {
                        x.Age = 0;
                        x.Value = null;
                    });
                }
                AnnualPolicyLoanVisibility = value ? Visibility.Visible : Visibility.Collapsed;
                InvokePropertyChanged(nameof(IsAnnualPolicyLoanChecked));
            }
        }

        private bool _IsAnnualLoanRepaymentsChecked;
        public bool IsAnnualLoanRepaymentsChecked {
            get => _IsAnnualLoanRepaymentsChecked;
            set {
                _IsAnnualLoanRepaymentsChecked = value;
                if (!IsAnnualLoanRepaymentsChecked)
                {
                    AnnualLoanRepayments.ToList().ForEach(x => {
                        x.Age = 0;
                        x.Value = null;
                    });
                }
                AnnualLoanRepaymentsVisibility = value ? Visibility.Visible : Visibility.Collapsed;
                InvokePropertyChanged(nameof(IsAnnualLoanRepaymentsChecked));
            }
        }

        private Visibility _SpecifiedDeathBenefitVisibility;
        public Visibility SpecifiedDeathBenefitVisibility {
            get => _SpecifiedDeathBenefitVisibility;
            set {
                _SpecifiedDeathBenefitVisibility = value;
                InvokePropertyChanged(nameof(SpecifiedDeathBenefitVisibility));
            }
        }

        private Visibility _ModalPremiumAmountVisibility;
        public Visibility ModalPremiumAmountVisibility {
            get => _ModalPremiumAmountVisibility;
            set {
                _ModalPremiumAmountVisibility = value;
                InvokePropertyChanged(nameof(ModalPremiumAmountVisibility));
            }
        }

        private Visibility _CurrentInterestRateVisibility;
        public Visibility CurrentInterestRateVisibility {
            get => _CurrentInterestRateVisibility;
            set {
                _CurrentInterestRateVisibility = value;
                InvokePropertyChanged(nameof(CurrentInterestRateVisibility));
            }
        }

        private Visibility _DeathBenefitOptionVisibility;
        public Visibility DeathBenefitOptionVisibility {
            get => _DeathBenefitOptionVisibility;
            set {
                _DeathBenefitOptionVisibility = value;
                InvokePropertyChanged(nameof(DeathBenefitOptionVisibility));
            }
        }

        private Visibility _WithdrawlsVisibility;
        public Visibility WithdrawlsVisibility {
            get => _WithdrawlsVisibility;
            set {
                _WithdrawlsVisibility = value;
                InvokePropertyChanged(nameof(WithdrawlsVisibility));
            }
        }

        private Visibility _AnnualPolicyLoanVisibility;
        public Visibility AnnualPolicyLoanVisibility {
            get => _AnnualPolicyLoanVisibility;
            set {
                _AnnualPolicyLoanVisibility = value;
                InvokePropertyChanged(nameof(AnnualPolicyLoanVisibility));
            }
        }

        private Visibility _AnnualLoanRepaymentsVisibility;
        public Visibility AnnualLoanRepaymentsVisibility {
            get => _AnnualLoanRepaymentsVisibility;
            set {
                _AnnualLoanRepaymentsVisibility = value;
                InvokePropertyChanged(nameof(AnnualLoanRepaymentsVisibility));
            }
        }

        public ObservableCollection<FutureAgeValue> SpecifiedDeathBenefits {
            get => (ObservableCollection<FutureAgeValue>)App.Illustration.FutureSpecificDeathBenefits;
            set {
                App.Illustration.FutureSpecificDeathBenefits = value;
                InvokePropertyChanged(nameof(SpecifiedDeathBenefits));
            }
        }

        public ObservableCollection<FutureAgeValue> ModalPremiums {
            get => (ObservableCollection<FutureAgeValue>)App.Illustration.FutureModalPremiums;
            set {
                App.Illustration.FutureModalPremiums = value;
                InvokePropertyChanged(nameof(ModalPremiums));
            }
        }

        public ObservableCollection<FutureAgeValue> CurrentInterestRates {
            get => (ObservableCollection<FutureAgeValue>)App.Illustration.FutureCurrentInterestRates;
            set {
                App.Illustration.FutureCurrentInterestRates = value;
                InvokePropertyChanged(nameof(CurrentInterestRates));
            }
        }

        public ObservableCollection<FutureAgeValue> DeathBenefitOptions {
            get => (ObservableCollection<FutureAgeValue>)App.Illustration.FutureDeathBenefitOptions;
            set {
                App.Illustration.FutureDeathBenefitOptions = value;
                InvokePropertyChanged(nameof(DeathBenefitOptions));
            }
        }

        public ObservableCollection<FutureAgeValueWithEndAge> Withdrawls {
            get => (ObservableCollection<FutureAgeValueWithEndAge>)App.Illustration.FutureWithdrawls;
            set {
                App.Illustration.FutureWithdrawls = value;
                InvokePropertyChanged(nameof(Withdrawls));
            }
        }

        public ObservableCollection<FutureAgeValueWithEndAge> AnnualPolicyLoans {
            get => (ObservableCollection<FutureAgeValueWithEndAge>)App.Illustration.FutureAnnualPolicyLoans;
            set {
                App.Illustration.FutureAnnualPolicyLoans = value;
                InvokePropertyChanged(nameof(AnnualPolicyLoans));
            }
        }

        public ObservableCollection<FutureAgeValueWithEndAge> AnnualLoanRepayments {
            get => (ObservableCollection<FutureAgeValueWithEndAge>)App.Illustration.FutureAnnualLoanRepayments;
            set {
                App.Illustration.FutureAnnualLoanRepayments = value;
                InvokePropertyChanged(nameof(AnnualLoanRepayments));
            }
        }
    }
}
