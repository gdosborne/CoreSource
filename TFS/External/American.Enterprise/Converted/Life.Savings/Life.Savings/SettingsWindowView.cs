using GregOsborne.Application;
using GregOsborne.MVVMFramework;
using Life.Savings.Data;
using Life.Savings.Data.Model;
using Life.Savings.Events;
using Life.Savings.Extensions;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.IO;

namespace Life.Savings
{
    public class SettingsWindowView : ViewModelBase
    {
        public SettingsWindowView()
        {
            RepositoryLocation = new DirectoryInfo(App.Repository.Location).Parent.FullName;
            IsDoNotAskAboutRefreshChecked = !Settings.GetSetting(App.ApplicationName, "Application", "DoNotAskAboutRefresh", false);
            IsDoNotAskAboutClearChecked = !Settings.GetSetting(App.ApplicationName, "Application", "DoNotAskAboutClear", false);
        }
        private DelegateCommand _closeCommand = null;
        public DelegateCommand CloseCommand => _closeCommand ?? (_closeCommand = new DelegateCommand(Close, ValidateCloseState));
        private void Close(object state)
        {
            DialogResult = false;
        }
        private static bool ValidateCloseState(object state)
        {
            return true;
        }
        private bool? _dialogResult;
        public bool? DialogResult {
            get => _dialogResult;
            set {
                _dialogResult = value;
                InvokePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
            }
        }
        private IRepository _repository;
        public IRepository Repository {
            get => _repository;
            set {
                _repository = value;
                RefreshData();
                InvokePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
            }
        }

        private bool _IsEncryptionModeChanged;
        public bool IsEncryptionModeChanged {
            get => _IsEncryptionModeChanged;
            set {
                _IsEncryptionModeChanged = value;
                App.Repository.EncryptClients = !App.Repository.EncryptClients;
                App.Repository.SaveClients();
                InvokePropertyChanged(nameof(IsEncryptionModeChanged));
            }
        }
        public bool IsClientDataEncrypted {
            get => Settings.GetSetting(App.ApplicationName, "Application", "IsClientDataEncrypted", false);
            set {
                IsEncryptionModeChanged = true;
                Settings.SetSetting(App.ApplicationName, "Application", "IsClientDataEncrypted", value);
                InvokePropertyChanged(nameof(IsClientDataEncrypted));
            }
        }
        private void RefreshData()
        {
            if (Repository == null)
                return;

            //used to output values to the clipboard for verification
            //var sb = new StringBuilder();
            //App.CurrentDataSet.LsRateWpItems.ToList().ForEach(x => sb.AppendLine($"{x.MaleWPD},{x.FemaleWPD}"));
            //Clipboard.SetText(sb.ToString());

            LsMinpItems = new ObservableCollection<Data.Model.LsMinp>(App.CurrentDataSet.LsMinpItems);
            LsTargItems = new ObservableCollection<Data.Model.LsTarg>(App.CurrentDataSet.LsTargItems);
            LsSurrItems = new ObservableCollection<Data.Model.LsSurr>(App.CurrentDataSet.LsSurrItems);
            LsRatePrItems = new ObservableCollection<Data.Model.LsRatePr>(App.CurrentDataSet.LsRatePrItems);
            LsRateSiItems = new ObservableCollection<Data.Model.LsRateSi>(App.CurrentDataSet.LsRateSiItems);
            LsRateSbItems = new ObservableCollection<Data.Model.LsRateSb>(App.CurrentDataSet.LsRateSbItems);
            LsRateGpItems = new ObservableCollection<Data.Model.LsRateGp>(App.CurrentDataSet.LsRateGpItems);
            LsRateWpItems = new ObservableCollection<Data.Model.LsRateWp>(App.CurrentDataSet.LsRateWpItems);
            WeightMinMaxValues = new ObservableCollection<Data.Model.WeightMinMax>(App.CurrentDataSet.WeightMinMax);
            LsCorrItems = new ObservableCollection<Data.Model.LsCorr>(App.CurrentDataSet.LsCorrItems);
            LsSpouseItems = new ObservableCollection<Data.Model.LsSpouse>(App.CurrentDataSet.LsSpouseItems);
            LsCso80Items = new ObservableCollection<Data.Model.LsCso80>(App.CurrentDataSet.LsCso80Items);

            States = new ObservableCollection<State>(Repository.States);

            SelectedState = App.DefaultState;

        }

        private string _XpsFileSaveLocation;
        public string XpsFileSaveLocation {
            get => Settings.GetSetting(App.ApplicationName, "Application", "IllustrationSaveLocation", string.Empty);
            set {
                _XpsFileSaveLocation = value;
                UpdateInterface();
                App.IllustrationSaveLocation = value;
                Settings.SetSetting(App.ApplicationName, "Application", "IllustrationSaveLocation", value);
                InvokePropertyChanged(nameof(XpsFileSaveLocation));
            }
        }
        private string _repositoryLocation;
        public string RepositoryLocation {
            get => _repositoryLocation;
            set {
                _repositoryLocation = value;
                var di = new DirectoryInfo(value);
                if (di.Name.Equals("ls2data", StringComparison.OrdinalIgnoreCase) || di.Name.Equals("ls3data", StringComparison.OrdinalIgnoreCase))
                    Settings.SetSetting(App.ApplicationName, "Application", "RepositoryPath", di.Parent.FullName);
                else
                {
                    var repoLocation = Path.Combine(value, App.CurrentDataSet == App.Repository.Ls2Data ? "LS2Data" : "LS3Data");
                    App.Repository.Location = repoLocation;
                    Settings.SetSetting(App.ApplicationName, "Application", "RepositoryPath", di.FullName);
                }
                UpdateInterface();
                InvokePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
            }
        }

        public string PreparersName {
            get => Settings.GetSetting(App.ApplicationName, "Preparer", "Name", Environment.UserName);
            set {
                Settings.SetSetting(App.ApplicationName, "Preparer", "Name", value);
                InvokePropertyChanged(nameof(PreparersName));
            }
        }

        public string PreparersPhoneNumber {
            get => Settings.GetSetting(App.ApplicationName, "Preparer", "PhoneNumber", string.Empty).FormatAsPhoneNumber();
            set {
                Settings.SetSetting(App.ApplicationName, "Preparer", "PhoneNumber", value == null ? string.Empty : value);
                InvokePropertyChanged(nameof(PreparersPhoneNumber));
            }
        }
        public string InsuranceCompanyName {
            get => Settings.GetSetting(App.ApplicationName, "Application", "InsuranceCompanyName", "American Enterprise Insurance Company");
            set {
                App.InsuranceCompanyName = value;
                Settings.SetSetting(App.ApplicationName, "Application", "InsuranceCompanyName", value);
                UpdateInterface();
                InvokePropertyChanged(nameof(InsuranceCompanyName));
            }
        }
        private DelegateCommand _SelectXpsFolderCommand;
        public DelegateCommand SelectXpsFolderCommand => _SelectXpsFolderCommand ?? (_SelectXpsFolderCommand = new DelegateCommand(SelectXpsFolder, ValidateSelectXpsFolderState));
        private void SelectXpsFolder(object state)
        {
            var e = new ShowBrowseFolderEventArgs(XpsFileSaveLocation, "Select where you would like your illustration files to be located...");
            ShowBrowseFolder?.Invoke(this, e);
            if (e.IsCancel)
                return;
            XpsFileSaveLocation = e.SelectedFolderPath;
        }
        private static bool ValidateSelectXpsFolderState(object state)
        {
            return true;
        }

        private DelegateCommand _selectRepoFolderCommand = null;
        public DelegateCommand SelectRepoFolderCommand => _selectRepoFolderCommand ?? (_selectRepoFolderCommand = new DelegateCommand(SelectRepoFolder, ValidateSelectRepoFolderState));
        private void SelectRepoFolder(object state)
        {
            var e = new ShowBrowseFolderEventArgs(RepositoryLocation, "Select the folder containing your data...");
            ShowBrowseFolder?.Invoke(this, e);
            if (e.IsCancel)
                return;
            RepositoryLocation = e.SelectedFolderPath;
        }
        public event ShowBrowseFolderHandler ShowBrowseFolder;
        private static bool ValidateSelectRepoFolderState(object state)
        {
            return true;
        }
        private bool _isDoNotAskAboutRefreshChecked;
        public bool IsDoNotAskAboutRefreshChecked {
            get => _isDoNotAskAboutRefreshChecked;
            set {
                _isDoNotAskAboutRefreshChecked = value;
                Settings.SetSetting(App.ApplicationName, "Application", "DoNotAskAboutRefresh", !value);
                InvokePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
            }
        }
        private bool _isDoNotAskAboutClearChecked;
        public bool IsDoNotAskAboutClearChecked {
            get => _isDoNotAskAboutClearChecked;
            set {
                _isDoNotAskAboutClearChecked = value;
                Settings.SetSetting(App.ApplicationName, "Application", "DoNotAskAboutClear", !value);
                InvokePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
            }
        }

        private ObservableCollection<Data.Model.LsMinp> _LsMinpItems;
        public ObservableCollection<Data.Model.LsMinp> LsMinpItems {
            get => _LsMinpItems;
            set {
                _LsMinpItems = value;
                InvokePropertyChanged(nameof(LsMinpItems));
            }
        }

        private ObservableCollection<Data.Model.LsTarg> _LsTargItems;
        public ObservableCollection<Data.Model.LsTarg> LsTargItems {
            get => _LsTargItems;
            set {
                _LsTargItems = value;
                InvokePropertyChanged(nameof(LsTargItems));
            }
        }

        private ObservableCollection<Data.Model.LsSurr> _LsSurrItems;
        public ObservableCollection<Data.Model.LsSurr> LsSurrItems {
            get => _LsSurrItems;
            set {
                _LsSurrItems = value;
                InvokePropertyChanged(nameof(LsSurrItems));
            }
        }

        private ObservableCollection<Data.Model.LsRatePr> _LsRatePrItems;
        public ObservableCollection<Data.Model.LsRatePr> LsRatePrItems {
            get => _LsRatePrItems;
            set {
                _LsRatePrItems = value;
                InvokePropertyChanged(nameof(LsRatePrItems));
            }
        }

        private ObservableCollection<Data.Model.LsRateSi> _LsRateSiItems;
        public ObservableCollection<Data.Model.LsRateSi> LsRateSiItems {
            get => _LsRateSiItems;
            set {
                _LsRateSiItems = value;
                InvokePropertyChanged(nameof(LsRateSiItems));
            }
        }

        private ObservableCollection<Data.Model.LsRateGp> _LsRateGpItems;
        public ObservableCollection<Data.Model.LsRateGp> LsRateGpItems {
            get => _LsRateGpItems;
            set {
                _LsRateGpItems = value;
                InvokePropertyChanged(nameof(LsRateGpItems));
            }
        }

        private ObservableCollection<Data.Model.LsRateSb> _LsRateSbItems;
        public ObservableCollection<Data.Model.LsRateSb> LsRateSbItems {
            get => _LsRateSbItems;
            set {
                _LsRateSbItems = value;
                InvokePropertyChanged(nameof(LsRateSbItems));
            }
        }

        private ObservableCollection<Data.Model.LsCorr> _LsCorrItems;
        public ObservableCollection<Data.Model.LsCorr> LsCorrItems {
            get => _LsCorrItems;
            set {
                _LsCorrItems = value;
                InvokePropertyChanged(nameof(LsCorrItems));
            }
        }

        private ObservableCollection<Data.Model.LsSpouse> _LsSpouseItems;
        public ObservableCollection<Data.Model.LsSpouse> LsSpouseItems {
            get => _LsSpouseItems;
            set {
                _LsSpouseItems = value;
                InvokePropertyChanged(nameof(LsSpouseItems));
            }
        }

        private ObservableCollection<Data.Model.LsCso80> _LsCso80Items;
        public ObservableCollection<Data.Model.LsCso80> LsCso80Items {
            get => _LsCso80Items;
            set {
                _LsCso80Items = value;
                InvokePropertyChanged(nameof(LsCso80Items));
            }
        }

        private ObservableCollection<Data.Model.WeightMinMax> _WeightValues;
        public ObservableCollection<Data.Model.WeightMinMax> WeightMinMaxValues {
            get => _WeightValues;
            set {
                _WeightValues = value;
                InvokePropertyChanged(nameof(WeightMinMaxValues));
            }
        }

        private ObservableCollection<Data.Model.LsRateWp> _LsRateWpItems;
        public ObservableCollection<Data.Model.LsRateWp> LsRateWpItems {
            get => _LsRateWpItems;
            set {
                _LsRateWpItems = value;
                InvokePropertyChanged(nameof(LsRateWpItems));
            }
        }

        private ObservableCollection<State> _States;
        public ObservableCollection<State> States {
            get => _States;
            set {
                _States = value;
                InvokePropertyChanged(nameof(States));
            }
        }

        private State _SelectedState;
        public State SelectedState {
            get => _SelectedState;
            set {
                _SelectedState = value;
                if (value != null)
                    App.DefaultState = value;
                InvokePropertyChanged(nameof(SelectedState));
            }
        }
    }
}
