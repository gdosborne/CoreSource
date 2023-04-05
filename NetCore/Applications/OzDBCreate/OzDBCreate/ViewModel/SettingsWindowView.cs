using ApplicationFramework.Settings;
using Common.Application.Primitives;
using Common.MVVMFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzDBCreate.ViewModel {
    public class SettingsWindowView : ViewModelBase {
        public SettingsWindowView() {
            Title = "Application Settings [designer]";
        }

        public override void Initialize() {
            base.Initialize();

            Title = "Application Settings";
            SettingSections = new ObservableCollection<SettingSection>();
            SetupSettings();
        }

        private readonly string general = "General";
        private readonly string appLevel = "Application";
        private readonly string saveWindowPosition = "Save window positions";
        private readonly string dbLocation = "Database location";
        
        private void SetupSettings() {
            var section = new SettingSection(general);
            var valueSavePos = App.AppSession.ApplicationSettings.GetValue(appLevel, $"{section.Name}.{saveWindowPosition}", true);
            var valueDBFolder = App.AppSession.ApplicationSettings.GetValue(appLevel, $"{section.Name}.{dbLocation}", 
                App.DatabaseLocation);
            section.AddItem(saveWindowPosition, typeof(bool), valueSavePos);
            section.AddItem(dbLocation, typeof(DirectoryInfo), valueDBFolder);
            section.PropertyChanged += Section_PropertyChanged;
            SettingSections.Add(section);


        }

        private void Section_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
            var setting = sender.As<SettingItem>();
            var section = setting.Section;
            App.AppSession.ApplicationSettings.AddOrUpdateSetting(appLevel, $"{section.Name}.{setting.Name}", setting.Value);
        }

        #region SelectedSection Property
        private SettingSection _SelectedSection = default;
        public SettingSection SelectedSection {
            get => _SelectedSection;
            set {
                _SelectedSection = value;
                InvokePropertyChanged();
            }
        }
        #endregion

        #region DialogResult Property
        private bool _DialogResult = default;
        public bool DialogResult {
            get => _DialogResult;
            set {
                _DialogResult = value;
                InvokePropertyChanged();
            }
        }
        #endregion

        #region SettingSections Property
        private ObservableCollection<SettingSection> _SettingSections = default;
        public ObservableCollection<SettingSection> SettingSections {
            get => _SettingSections;
            set {
                _SettingSections = value;
                InvokePropertyChanged();
            }
        }
        #endregion

        #region OK Command
        private DelegateCommand _OKCommand = default;
        public DelegateCommand OKCommand => _OKCommand ??= new DelegateCommand(OK, ValidateOKState);
        private bool ValidateOKState(object state) => true;
        private void OK(object state) {
            DialogResult = true;
        }
        #endregion

        #region Cancel Command
        private DelegateCommand _CancelCommand = default;
        public DelegateCommand CancelCommand => _CancelCommand ??= new DelegateCommand(Cancel, ValidateCancelState);
        private bool ValidateCancelState(object state) => true;
        private void Cancel(object state) {
            DialogResult = false;
        }
        #endregion
    }
}
