using GregOsborne.Application.Primitives;
using GregOsborne.MVVMFramework;
using Life.Savings.Data;
using Life.Savings.Data.Model;
using Life.Savings.Events;
using Life.Savings.Extensions;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using System.Collections.Generic;

namespace Life.Savings {
    public sealed class MainWindowView : ViewModelBase {
        private Visibility _ViewSpouseVisibility;
        private DelegateCommand _clientListCommand = null;
        private DelegateCommand _premiumCommand = null;
        private DelegateCommand _AdditionalInsuredCommand;
        private DelegateCommand _FutureYearsCommand;
        private ObservableCollection<SimpleValue> _SubstandardRates;
        private ObservableCollection<SimpleValue> _ClientWpds;
        private ObservableCollection<SimpleValue> _ClientGpos;
        private ObservableCollection<SimpleValue> _ClientColas;
        private ObservableCollection<SimpleValue> _ClientYearToAgeOptions;
        private ObservableCollection<SimpleValue> _ClientBenfitOptions;
        private ObservableCollection<SimpleValue> _ClientPlanTypes;
        private DelegateCommand _ProductsCommand;
        private DelegateCommand _clearWindowCommand;
        private DelegateCommand _closeCommand;
        private DelegateCommand _illustrateCommand;
        private DelegateCommand _logFolderCommand;
        private DelegateCommand _refreshStaticDataCommand;
        private DelegateCommand _settingsCommand;
        private ObservableCollection<Gender> _genders;
        private DateTime? _clientBirthDate;
        private bool _isSecondIllustrationChecked;
        private IRepository _repository;
        private ObservableCollection<State> _states;
        private DispatcherTimer dt = null;
        private DelegateCommand _RetainSpouseDataCommand;
        private Visibility _ChangedVisibility;
        private Visibility _PrimaryDataVisibility;
        private DelegateCommand _ViewSpouseDataCommand;

        public MainWindowView() {
            var datasetName = App.Repository.IsLS3DataSelected ? "LS3" : "LS2";
            WindowTitle = $"Life Savings II Principal Insured Illustration Parameters ({datasetName})";
            App.Illustration = GetNewIllustrationInfo();

            PreparedBy = GregOsborne.Application.Settings.GetSetting(App.ApplicationName, "Preparer", "Name", Environment.UserName);
            ContactTelephoneNumber = GregOsborne.Application.Settings.GetSetting(App.ApplicationName, "Preparer", "PhoneNumber", string.Empty).FormatAsPhoneNumber();
            IsClientGuidelineChecked = true;
            ViewSpouseVisibility = Visibility.Collapsed;
            PrimaryDataVisibility = Visibility.Visible;
            ChangedVisibility = Visibility.Collapsed;
            IsClientGuidelineChecked = true;

            //data binding causes the changed indicator flag to be set to true when initializing default values
            // need to reset the changed indicator flag after data binding has completed
            dt = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(250) };
            dt.Tick += Dt_Tick;
            dt.Start();
        }

        private void Dt_Tick(object sender, EventArgs e) {
            sender.As<DispatcherTimer>().Stop();
            App.Illustration.IsChanged = false;
            UpdateInterface();
        }

        public event ShowClientListHandler ShowClientList;
        public event EventHandler ShowPremiumCalc;
        public event EventHandler ShowAdditionalInsured;
        public event EventHandler ShowFutureYears;
        public event EventHandler ShowIllustration;
        public event EventHandler ShowProducts;
        public event EventHandler ShowCannotIllustrate;
        public event EventHandler ShowSpouseData;
        public event AskToClearHandler AskToClear;
        public event WarnToRefreshHandler WarnToRefresh;
        public event ShowSettingsHandler ShowSettings;

        public DelegateCommand ClientListCommand => _clientListCommand ?? (_clientListCommand = new DelegateCommand(ClientList, ValidateClientListState));
        public DelegateCommand PremiumCommand => _premiumCommand ?? (_premiumCommand = new DelegateCommand(Premium, ValidatePremiumState));
        public DelegateCommand AdditionalInsuredCommand => _AdditionalInsuredCommand ?? (_AdditionalInsuredCommand = new DelegateCommand(AdditionalInsured, ValidateAdditionalInsuredState));
        public DelegateCommand FutureYearsCommand => _FutureYearsCommand ?? (_FutureYearsCommand = new DelegateCommand(FutureYears, ValidateFutureYearsState));
        public DelegateCommand ProductsCommand => _ProductsCommand ?? (_ProductsCommand = new DelegateCommand(Products, ValidateProductsState));
        public DelegateCommand ViewSpouseDataCommand => _ViewSpouseDataCommand ?? (_ViewSpouseDataCommand = new DelegateCommand(ViewSpouseData, ValidateViewSpouseDataState));
        public DelegateCommand CloseCommand => _closeCommand ?? (_closeCommand = new DelegateCommand(Close, ValidateCloseState));
        public DelegateCommand SettingsCommand => _settingsCommand ?? (_settingsCommand = new DelegateCommand(Settings, ValidateSettingsState));
        public DelegateCommand IllustrateCommand => _illustrateCommand ?? (_illustrateCommand = new DelegateCommand(Illustrate, ValidateIllustrateState));
        public DelegateCommand LogFolderCommand => _logFolderCommand ?? (_logFolderCommand = new DelegateCommand(LogFolder, ValidateLogFolderState));
        public DelegateCommand RefreshStaticDataCommand => _refreshStaticDataCommand ?? (_refreshStaticDataCommand = new DelegateCommand(RefreshStaticData, ValidateRefreshStaticDataState));
        public DelegateCommand ClearWindowCommand => _clearWindowCommand ?? (_clearWindowCommand = new DelegateCommand(ClearWindow, ValidateClearWindowState));
        public DelegateCommand RetainSpouseDataCommand => _RetainSpouseDataCommand ?? (_RetainSpouseDataCommand = new DelegateCommand(RetainSpouseData, ValidateRetainSpouseDataCommandState));

        private void RetainSpouseData(object state) {
            //execute the command
        }

        private static bool ValidateRetainSpouseDataCommandState(object state) {
            return true;
        }

        public void SaveToClientList(Client attemptedClient) {
            Repository.AddClient(attemptedClient);
        }

        public void Reload() {
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

            ClientGender = Repository.Genders.FirstOrDefault(x => x.Abbreviation.Equals('U'));
            ClientState = App.DefaultState;

            ClientSubStandardRate = SubstandardRates.FirstOrDefault(x => x.Id == 0);
            ClientWpd = ClientWpds.FirstOrDefault(x => x.Id == 0);
            ClientGpo = ClientGpos.FirstOrDefault(x => x.Id == 0);
            ClientCola = ClientColas.FirstOrDefault(x => x.Id == 0);
            ClientYearToAgeOption = ClientYearToAgeOptions.FirstOrDefault(x => x.Id == 0);
            ClientBenefitOption = ClientBenfitOptions.FirstOrDefault(x => x.Id == 0);
            ClientPlanType = ClientPlanTypes.FirstOrDefault(x => x.Id == 0);

            App.Illustration.IsChanged = false;
            UpdateInterface();

        }

        public void PopulateWithClient(Client client) {
            if (client == null)
                return;
            EditingId = client.Id;
            ClientFirstName = client.ClientData.FirstName;
            ClientLastName = client.ClientData.LastName;
            if (!string.IsNullOrEmpty(client.ClientData.MiddleInitial))
                ClientMiddleInitial = client.ClientData.MiddleInitial.Substring(0);
            ClientAge = client.ClientData.Age;
            ClientBirthDate = client.ClientData.BirthDate.HasValue
                && client.ClientData.BirthDate.Value > DateTime.MinValue
                && client.ClientData.BirthDate.Value < DateTime.Now ? client.ClientData.BirthDate : null;
            ClientGender = client.ClientData.Gender;
            ClientState = client.ClientData.State;
            ClientPlanType = client.ClientData.PlanType;
            ClientInitialDeathBenefit = client.ClientData.InitialDeathBenefit ?? 0;
            ClientInterestRate = client.ClientData.InterestRate ?? 0;
            ClientSubStandardRate = client.ClientData.SubstandardRate;
            ClientGpo = client.ClientData.Gpo;
            ClientWpd = client.ClientData.Wpd;
            ClientCola = client.ClientData.Cola;
            ClientBenefitOption = client.ClientData.BenefitOption;
            ClientDeathBenefitAmount = client.ClientData.FaceAmount ?? 0;
            ClientTermYears = client.ClientData.AgeOrYear ?? 0;
            ClientYearToAgeOption = client.ClientData.YearToAgeOption;
            App.Illustration.ClientData.IsFutureWD = client.ClientData.IsFutureWD;

            if (client.SpouseData != null)
            {
                IsSecondIllustrationChecked = true;
                App.Illustration.SpouseAsClientData = new IndividualData {
                    Age = client.SpouseData.Age,
                    Gender = client.SpouseData.Gender,
                    BirthDate = client.SpouseData.BirthDate.HasValue && client.SpouseData.BirthDate.Value > DateTime.MinValue && client.SpouseData.BirthDate.Value < DateTime.Now
                        ? client.SpouseData.BirthDate : null,
                    Gpo = client.SpouseData.Gpo,
                    Cola = client.SpouseData.Cola,
                    Wpd = client.SpouseData.Wpd,
                    FaceAmount = client.SpouseData.FaceAmount ?? 0,
                    InterestRate = client.SpouseData.InterestRate ?? 0,
                    InitialDeathBenefit = client.SpouseData.InitialDeathBenefit ?? 0,
                    BenefitOption = client.SpouseData.BenefitOption,
                    State = client.SpouseData.State,
                    SubstandardRate = client.SpouseData.SubstandardRate,
                    YearToAgeOption = client.SpouseData.YearToAgeOption,
                    PlanType = client.SpouseData.PlanType,
                    AgeOrYear = client.SpouseData.AgeOrYear ?? 0,
                    FirstName = client.SpouseData.FirstName,
                    LastName = client.SpouseData.LastName,
                    MiddleInitial = client.SpouseData.MiddleInitial
                };
            }

            App.Illustration.PlannedModalPremium = client.PremiumPlannedModalPremium ?? 0;
            App.Illustration.InitialLumpSumAmount = client.PremiumInitialLumpSumAmount ?? 0;
            App.Illustration.IsChildRiderSelected = client.IsChildRiderSelected;
            App.Illustration.ChildRiderYoungestAge = client.ChildRiderYoungestAge ?? 0;
            App.Illustration.ChildRiderDeathBenefitAmount = client.ChildRiderDeathBenefitAmount ?? 0;
            App.Illustration.IsSpousePrincipalInsured = client.IsSpousePrincipalInsured;

            App.Illustration.Riders = new List<IIndividualData>(client.Riders);

            var index = 0;
            client.FutureSpecificDeathBenefits.ToList().ForEach(x => {
                App.Illustration.FutureSpecificDeathBenefits[index] = x;
                index++;
            });
            index = 0;
            client.FutureModalPremiums.ToList().ForEach(x => {
                App.Illustration.FutureModalPremiums[index] = x;
                index++;
            });
            index = 0;
            client.FutureCurrentInterestRates.ToList().ForEach(x => {
                App.Illustration.FutureCurrentInterestRates[index] = x;
                index++;
            });
            index = 0;
            client.FutureDeathBenefitOptions.ToList().ForEach(x => {
                App.Illustration.FutureDeathBenefitOptions[index] = x;
                index++;
            });
            index = 0;
            client.FutureWithdrawls.ToList().ForEach(x => {
                App.Illustration.FutureWithdrawls[index] = x;
                index++;
            });
            index = 0;
            client.FutureAnnualPolicyLoans.ToList().ForEach(x => {
                App.Illustration.FutureAnnualPolicyLoans[index] = x;
                index++;
            });
            index = 0;
            client.FutureAnnualLoanRepayments.ToList().ForEach(x => {
                App.Illustration.FutureAnnualLoanRepayments[index] = x;
                index++;
            });
            App.Illustration.Reset();

            UpdateInterface();
        }

        public IRepository Repository {
            get => _repository;
            set {
                _repository = value;
                if (value != null)
                    Reload();

                InvokePropertyChanged(nameof(Repository));
            }
        }

        public override void UpdateInterface() {
            if (!SettingProperty)
                base.UpdateInterface();
            ChangedVisibility = App.Illustration != null && App.Illustration.IsChanged ? Visibility.Visible : Visibility.Collapsed;
        }

        private string _WindowTitle;
        public string WindowTitle {
            get => _WindowTitle;
            set {
                _WindowTitle = value;
                InvokePropertyChanged(nameof(WindowTitle));
            }
        }

        public ObservableCollection<Gender> Genders {
            get => _genders;
            set {
                _genders = value;
                InvokePropertyChanged(nameof(Genders));
            }
        }

        public ObservableCollection<State> States {
            get => _states;
            set {
                _states = value;
                InvokePropertyChanged(nameof(States));
            }
        }

        public State ClientState {
            get => App.Illustration.ClientData.State;
            set {
                if (value == null)
                    value = App.DefaultState;
                App.Illustration.ClientData.State = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(ClientState));
            }
        }

        public Gender ClientGender {
            get => App.Illustration.ClientData.Gender;
            set {
                if (value == null)
                    value = Genders.FirstOrDefault(x => x.Id == Genders.Min(y => y.Id));
                App.Illustration.ClientData.Gender = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(ClientGender));
            }
        }

        public int EditingId {
            get => App.EditingClientId;
            set {
                App.EditingClientId = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(EditingId));
            }
        }

        public string ClientFirstName {
            get => App.Illustration.ClientData.FirstName;
            set {
                App.Illustration.ClientData.FirstName = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(ClientFirstName));
            }
        }

        public string ClientLastName {
            get => App.Illustration.ClientData.LastName;
            set {
                App.Illustration.ClientData.LastName = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(ClientLastName));
            }
        }

        public string ClientMiddleInitial {
            get => App.Illustration.ClientData.MiddleInitial;
            set {
                App.Illustration.ClientData.MiddleInitial = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(ClientMiddleInitial));
            }
        }

        public string PreparedBy {
            get => App.Illustration.PreparedBy;
            set {
                App.Illustration.PreparedBy = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(PreparedBy));
            }
        }

        public string ContactTelephoneNumber {
            get => App.Illustration.ContactTelephoneNumber;
            set {
                App.Illustration.ContactTelephoneNumber = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(ContactTelephoneNumber));
            }
        }

        public int ClientAge {
            get => App.Illustration.ClientData.Age;
            set {
                App.Illustration.ClientData.Age = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(ClientAge));
            }
        }

        public int? ClientTermYears {
            get => App.Illustration.ClientData.AgeOrYear;
            set {
                App.Illustration.ClientData.AgeOrYear = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(ClientTermYears));
            }
        }

        public double? ClientInterestRate {
            get => App.Illustration.ClientData.InterestRate;
            set {
                App.Illustration.ClientData.InterestRate = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(ClientInterestRate));
            }
        }

        public double? ClientInitialDeathBenefit {
            get => App.Illustration.ClientData.InitialDeathBenefit;
            set {
                App.Illustration.ClientData.InitialDeathBenefit = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(ClientInitialDeathBenefit));
            }
        }

        public bool IsSecondIllustrationChecked {
            get => _isSecondIllustrationChecked;
            set {
                _isSecondIllustrationChecked = value;
                if (!IsSecondIllustrationChecked)
                    App.Illustration.SpouseAsClientData = new IndividualData();
                ViewSpouseVisibility = value ? Visibility.Visible : Visibility.Collapsed;
                UpdateInterface();
                InvokePropertyChanged(nameof(IsSecondIllustrationChecked));
            }
        }

        public Visibility ChangedVisibility {
            get => _ChangedVisibility;
            set {
                _ChangedVisibility = value;
                InvokePropertyChanged(nameof(ChangedVisibility));
            }
        }

        public Visibility PrimaryDataVisibility {
            get => _PrimaryDataVisibility;
            set {
                _PrimaryDataVisibility = value;
                InvokePropertyChanged(nameof(PrimaryDataVisibility));
            }
        }

        public bool IsClientGuidelineChecked {
            get => App.Illustration.IsClientGuidelineChecked;
            set {
                App.Illustration.IsClientGuidelineChecked = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(IsClientGuidelineChecked));
            }
        }

        public bool IsClientTargetChecked {
            get => App.Illustration.IsClientTargetChecked;
            set {
                App.Illustration.IsClientTargetChecked = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(IsClientTargetChecked));
            }
        }

        public bool IsClientMinimumChecked {
            get => App.Illustration.IsClientMinimumChecked;
            set {
                App.Illustration.IsClientMinimumChecked = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(IsClientMinimumChecked));
            }
        }

        public double? ClientDeathBenefitAmount {
            get => App.Illustration.ClientData.FaceAmount;
            set {
                App.Illustration.ClientData.FaceAmount = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(ClientDeathBenefitAmount));
            }
        }

        public DateTime? ClientBirthDate {
            get => _clientBirthDate;
            set {
                _clientBirthDate = value;
                if (value.HasValue)
                {
                    int age = DateTime.Now.Year - value.Value.Year;
                    if (DateTime.Now.Month < value.Value.Month || (DateTime.Now.Month == value.Value.Month && DateTime.Now.Day < value.Value.Day))
                        age--;
                    ClientAge = age;
                }
                UpdateInterface();
                InvokePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
            }
        }

        private bool CheckForChanges() {
            return !string.IsNullOrEmpty(ClientFirstName)
                   || !string.IsNullOrEmpty(ClientMiddleInitial)
                   || !string.IsNullOrEmpty(ClientLastName)
                   || ClientAge > 0
                   || ClientBirthDate.HasValue
                   || ClientGender != null
                   || ClientState != null;
        }

        private void RefreshStaticData(object state) {
            var e = new WarningToRefreshEventArgs();
            WarnToRefresh?.Invoke(this, e);
            if (!e.Answer.HasValue || !e.Answer.Value) return;
            Repository.RefreshStaticData();
            ClearWindow(null);
        }

        private static bool ValidateRefreshStaticDataState(object state) {
            return true;
        }

        private IllustrationInfo GetNewIllustrationInfo() {
            return new IllustrationInfo {
                PremiumMode = App.CurrentDataSet.LsPremiumModes.FirstOrDefault(x => x.DisplayedValue.Equals("Annual", StringComparison.OrdinalIgnoreCase)),
                PremiumYearsToPay = App.CurrentDataSet.LsYearToPay.FirstOrDefault(x => x.Id == 0),
                ClientData = new IndividualData {
                    BenefitOption = App.CurrentDataSet.LsClientOptions.FirstOrDefault(x => x.DisplayedValue.Equals("Level Death Benefit", StringComparison.OrdinalIgnoreCase)),
                    Cola = App.CurrentDataSet.LsClientCOLAs.FirstOrDefault(x => x.DisplayedValue.Equals("None", StringComparison.OrdinalIgnoreCase)),
                    Gender = App.Repository.Genders.FirstOrDefault(x => x.Name.Equals("Male", StringComparison.OrdinalIgnoreCase)),
                    PlanType = App.CurrentDataSet.LsClientPlans.FirstOrDefault(x => x.DisplayedValue.Equals("Non-Smoker", StringComparison.OrdinalIgnoreCase)),
                    RemoveYearToAge = null,
                    SubstandardRate = App.CurrentDataSet.LsSubStandardRatings.FirstOrDefault(x => x.DisplayedValue.Equals("None", StringComparison.OrdinalIgnoreCase)),
                    Wpd = App.CurrentDataSet.LsClientWPDs.FirstOrDefault(x => x.DisplayedValue.Equals("No", StringComparison.OrdinalIgnoreCase)),
                    YearToAgeOption = App.CurrentDataSet.LsYearToPay.FirstOrDefault(x => x.DisplayedValue.Equals("To Age 95", StringComparison.OrdinalIgnoreCase)),
                },
                SpouseAsClientData = new IndividualData {
                    IsSelected = false
                }
            };
        }

        private void ClearWindow(object state) {
            var e = new AskToClearEventArgs();
            AskToClear?.Invoke(this, e);
            if (!e.Answer.HasValue || !e.Answer.Value) return;
            EditingId = 0;
            App.Illustration = GetNewIllustrationInfo();
            var tmpClient = new Client(App.Repository.Genders, App.CurrentDataSet.LsClientPlans, App.CurrentDataSet.LsClientRiderOptions, App.CurrentDataSet.LsSubStandardRatings, App.CurrentDataSet.LsClientCOLAs) {
                Id = 0,
                ClientData = new IndividualData()
            };
            PopulateWithClient(tmpClient);
            App.Illustration.Reset();
            UpdateInterface();
        }

        private bool ValidateClearWindowState(object state) {
            return CheckForChanges();
        }

        private void Close(object state) {
            Environment.Exit(0);
        }

        private static bool ValidateCloseState(object state) {
            return true;
        }

        private void Settings(object state) {
            ShowSettings?.Invoke(this, new ShowSettingsEventArgs());
        }

        private static bool ValidateSettingsState(object state) {
            return true;
        }

        private static void LogFolder(object state) {
            var p = new Process();
            var si = new ProcessStartInfo {
                FileName = "explorer.exe",
                Arguments = App.LogFolder
            };
            p.StartInfo = si;
            p.Start();
        }

        private static bool ValidateLogFolderState(object state) {
            return true;
        }

        private void ViewSpouseData(object state) {
            ShowSpouseData?.Invoke(this, EventArgs.Empty);
        }

        private bool ValidateViewSpouseDataState(object state) {
            return true;
        }

        public Visibility ViewSpouseVisibility {
            get => _ViewSpouseVisibility;
            set {
                _ViewSpouseVisibility = value;
                InvokePropertyChanged(nameof(ViewSpouseVisibility));
            }
        }

        private void Illustrate(object state) {
            if (ClientAge == 0 || ClientGender == null || ClientInitialDeathBenefit == 0 || ClientInterestRate == 0)
            {
                ShowCannotIllustrate?.Invoke(this, EventArgs.Empty);
                return;
            }
            ShowIllustration?.Invoke(this, EventArgs.Empty);
        }

        private bool ValidateIllustrateState(object state) {
            return !string.IsNullOrEmpty(ClientFirstName)
                   && !string.IsNullOrEmpty(ClientLastName)
                   && ClientAge > 0
                   && ClientGender != null
                   && ClientState != null
                   && ClientInitialDeathBenefit > 0
                   && ClientInterestRate > 0;
        }

        private void ClientList(object state) {
            ShowClientList?.Invoke(this, new ShowClientListEventArgs(true));
        }

        private bool ValidateClientListState(object state) {
            return true;
        }

        private void Premium(object state) {
            ShowPremiumCalc?.Invoke(this, EventArgs.Empty);
        }

        private bool ValidatePremiumState(object state) {
            return true;
        }

        private void AdditionalInsured(object state) {
            ShowAdditionalInsured?.Invoke(this, EventArgs.Empty);
        }

        private static bool ValidateAdditionalInsuredState(object state) {
            return true;
        }

        private void FutureYears(object state) {
            ShowFutureYears?.Invoke(this, EventArgs.Empty);
        }

        private static bool ValidateFutureYearsState(object state) {
            return true;
        }

        public ObservableCollection<SimpleValue> SubstandardRates {
            get => _SubstandardRates;
            set {
                _SubstandardRates = value;
                InvokePropertyChanged(nameof(SubstandardRates));
            }
        }

        public SimpleValue ClientSubStandardRate {
            get => App.Illustration.ClientData.SubstandardRate;
            set {
                App.Illustration.ClientData.SubstandardRate = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(ClientSubStandardRate));
            }
        }

        public ObservableCollection<SimpleValue> ClientWpds {
            get => _ClientWpds;
            set {
                _ClientWpds = value;
                InvokePropertyChanged(nameof(ClientWpds));
            }
        }

        public SimpleValue ClientWpd {
            get => App.Illustration.ClientData.Wpd;
            set {
                App.Illustration.ClientData.Wpd = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(ClientWpd));
            }
        }

        public ObservableCollection<SimpleValue> ClientGpos {
            get => _ClientGpos;
            set {
                _ClientGpos = value;
                InvokePropertyChanged(nameof(ClientGpos));
            }
        }

        public SimpleValue ClientGpo {
            get => App.Illustration.ClientData.Gpo;
            set {
                App.Illustration.ClientData.Gpo = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(ClientGpo));
            }
        }

        public ObservableCollection<SimpleValue> ClientColas {
            get => _ClientColas;
            set {
                _ClientColas = value;
                InvokePropertyChanged(nameof(ClientColas));
            }
        }

        public SimpleValue ClientCola {
            get => App.Illustration.ClientData.Cola;
            set {
                App.Illustration.ClientData.Cola = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(ClientCola));
            }
        }

        public ObservableCollection<SimpleValue> ClientYearToAgeOptions {
            get => _ClientYearToAgeOptions;
            set {
                _ClientYearToAgeOptions = value;
                InvokePropertyChanged(nameof(ClientYearToAgeOptions));
            }
        }

        public SimpleValue ClientYearToAgeOption {
            get => App.Illustration.ClientData.YearToAgeOption;
            set {
                App.Illustration.ClientData.YearToAgeOption = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(ClientYearToAgeOption));
            }
        }

        public ObservableCollection<SimpleValue> ClientBenfitOptions {
            get => _ClientBenfitOptions;
            set {
                _ClientBenfitOptions = value;
                InvokePropertyChanged(nameof(ClientBenfitOptions));
            }
        }

        public SimpleValue ClientBenefitOption {
            get => App.Illustration.ClientData.BenefitOption;
            set {
                App.Illustration.ClientData.BenefitOption = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(ClientBenefitOption));
            }
        }

        public ObservableCollection<SimpleValue> ClientPlanTypes {
            get => _ClientPlanTypes;
            set {
                _ClientPlanTypes = value;
                InvokePropertyChanged(nameof(ClientPlanTypes));
            }
        }

        public SimpleValue ClientPlanType {
            get => App.Illustration.ClientData.PlanType;
            set {
                if (value == null)
                    value = ClientPlanTypes.FirstOrDefault(x => x.Id == 1);
                App.Illustration.ClientData.PlanType = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(ClientPlanType));
            }
        }

        private void Products(object state) {
            ShowProducts?.Invoke(this, EventArgs.Empty);
        }

        private static bool ValidateProductsState(object state) {
            return true;
        }
    }
}
