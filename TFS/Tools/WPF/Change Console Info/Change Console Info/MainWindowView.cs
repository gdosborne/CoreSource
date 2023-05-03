using Greg.Osborne.ChangeConsoleInfo.Data;
using GregOsborne.Application.Primitives;
using GregOsborne.Dialogs;
using GregOsborne.MVVMFramework;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;

namespace Greg.Osborne.ChangeConsoleInfo
{
    public class MainWindowView : ViewModelBase
    {
        private const string _defaultHeader = "Settings - select application to start";
        private ObservableCollection<AppInfo> _apps;
        private bool _areSettingsEnabled;
        private Dictionary<string, string> _currentFontNames;
        private ObservableCollection<FontItem> _fontFamilies;
        private ObservableCollection<int> _fontSizes;
        private string _groupBoxHeader;
        private DelegateCommand _SaveCommand;
        private AppInfo _selectedApp;
        private FontItem _selectedFontFamily;
        private int _selectedFontSize;

        public override void Initialize() {
            var regKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default);
            var subKey = regKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Console\TrueTypeFont");
            var valueNames = subKey.GetValueNames().Where(x => x.StartsWith("0"));
            foreach (var item in valueNames) {
                _currentFontNames.Add(item, (string)subKey.GetValue(item));
            }

            RefreshApps();
            RefreshFontFamilies();
            GroupBoxHeader = _defaultHeader;
            AreSettingsEnabled = false;
            FontSizes = new ObservableCollection<int>();
            for (int i = 5; i < 80; i++) {
                FontSizes.Add(i);
            }
        }

        public MainWindowView()
        {
            _currentFontNames = new Dictionary<string, string>();
        }

        public event ExecuteUiActionHandler ExecuteUIAction;

        public ObservableCollection<AppInfo> Apps {
            get => _apps;
            set {
                _apps = value;
                InvokePropertyChanged(nameof(Apps));
            }
        }

        public bool AreSettingsEnabled {
            get => _areSettingsEnabled;
            set {
                _areSettingsEnabled = value;
                InvokePropertyChanged(nameof(AreSettingsEnabled));
            }
        }

        public ObservableCollection<FontItem> FontFamilies {
            get => _fontFamilies;
            set {
                _fontFamilies = value;
                InvokePropertyChanged(nameof(FontFamilies));
            }
        }

        public ObservableCollection<int> FontSizes {
            get => _fontSizes;
            set {
                _fontSizes = value;
                InvokePropertyChanged(nameof(FontSizes));
            }
        }

        public string GroupBoxHeader {
            get => _groupBoxHeader;
            set {
                _groupBoxHeader = value;
                InvokePropertyChanged(nameof(GroupBoxHeader));
            }
        }

        public DelegateCommand SaveCommand => _SaveCommand ?? (_SaveCommand = new DelegateCommand(Save, ValidateSaveState));

        public AppInfo SelectedApp {
            get => _selectedApp;
            set {
                _selectedApp = value;
                AreSettingsEnabled = SelectedApp != null;
                if (!AreSettingsEnabled)
                {
                    GroupBoxHeader = _defaultHeader;
                    SelectedFontFamily = null;
                }
                else
                {
                    SelectedApp.PrepareForInitialization();
                    GroupBoxHeader = $"Settings [{SelectedApp.Name}]";
                    var faceName = SelectedApp.FaceName;
                    if (string.IsNullOrEmpty(faceName))
                        faceName = Apps.FirstOrDefault(x => x.Id == 0).FaceName;
                    SelectedFontFamily = FontFamilies.FirstOrDefault(x => x.FontFamily.Name.Equals(faceName));
                    if (SelectedApp.FontHeight <= FontSizes.Max())
                        SelectedFontSize = SelectedApp.FontHeight;
                    else
                        SelectedFontSize = 12;
                    SelectedApp.CompleteInitilization();
                }
                UpdateInterface();
                InvokePropertyChanged(nameof(SelectedApp));
            }
        }

        public FontItem SelectedFontFamily {
            get => _selectedFontFamily;
            set {
                _selectedFontFamily = value;
                if (SelectedFontFamily != null)
                    SelectedApp.FaceName = SelectedFontFamily.FontFamily.Name;
                UpdateInterface();
                InvokePropertyChanged(nameof(SelectedFontFamily));
            }
        }

        public int SelectedFontSize {
            get => _selectedFontSize;
            set {
                _selectedFontSize = value;
                SelectedApp.FontHeight = SelectedFontSize;
                UpdateInterface();
                InvokePropertyChanged(nameof(SelectedFontSize));
            }
        }

        private void AppInfo_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            UpdateInterface();
        }

        private AppInfo GetAppInfoFromKey(RegistryKey key, int index)
        {
            var parts = key.Name.Split('\\');
            var name = parts[parts.Length - 1];
            if (name.Equals("Console", StringComparison.OrdinalIgnoreCase) && key.GetSubKeyNames().Length > 0)
                name = "Default";
            var faceName = key.GetValue("FaceName").ToString();
            int fontSize = 0;
            if (key.GetValueNames().Contains("FontSize"))
            {
                fontSize = (int)key.GetValue("FontSize");
                var hexFontSize = fontSize.ToString("X").PadLeft(8, '0');
                var height = int.Parse(hexFontSize.Substring(0, 4), System.Globalization.NumberStyles.HexNumber);
                var width = int.Parse(hexFontSize.Substring(4, 4), System.Globalization.NumberStyles.HexNumber);
            }
            var result = new AppInfo
            {
                Id = index,
                Name = name,
                Key = key,
                FaceName = faceName,
                FontSize = fontSize
            };
            result.PropertyChanged += AppInfo_PropertyChanged;
            result.CompleteInitilization();
            return result;
        }

        private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("IsSelected"))
            {
                var regKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default);
                var subKey = regKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Console\TrueTypeFont", true);
                if (sender.As<FontItem>().IsSelected)
                {
                    var orderedItems = _currentFontNames.Keys.OrderBy(x => x.Length);
                    for (int i = 1; i < int.MaxValue; i++)
                    {
                        if (!_currentFontNames.Keys.Contains(new string('0', i)))
                        {
                            var missingKey = new string('0', i);
                            subKey.SetValue(missingKey, sender.As<FontItem>().FontFamily.Name);
                            sender.As<FontItem>().KeyName = missingKey;
                            break;
                        }
                    }
                }
                else
                {
                    var keyName = sender.As<FontItem>().KeyName;
                    if (subKey.GetValueNames().Contains(keyName))
                    {
                        subKey.DeleteValue(keyName);
                        sender.As<FontItem>().KeyName = null;
                    }
                }
            }
        }

        private void RefreshApps()
        {
            var current = SelectedApp;
            Apps = new ObservableCollection<AppInfo>();
            var regKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default);
            var regKeyConsole = regKey.OpenSubKey("Console", true);
            if (regKeyConsole == null)
                return;
            Apps.Add(GetAppInfoFromKey(regKeyConsole, 0));

            var subKeys = regKeyConsole.GetSubKeyNames();
            var index = 0;
            foreach (var name in subKeys)
            {
                index++;
                Apps.Add(GetAppInfoFromKey(regKeyConsole.OpenSubKey(name, true), index));
            }
        }

        private void RefreshFontFamilies()
        {
            FontFamilies = new ObservableCollection<FontItem>();
            foreach (var ff in System.Drawing.FontFamily.Families)
            {
                //if (ff.IsStyleAvailable(System.Drawing.FontStyle.Regular))
                //{
                //    var diff = default(float);
                //    using (var f = new System.Drawing.Font(ff, 16))
                //    {
                //        diff = TextRenderer.MeasureText("WWW", f).Width - TextRenderer.MeasureText("...", f).Width;
                //    }
                //    if (System.Math.Abs(diff) < float.Epsilon * 2)
                //    {
                //    }
                //}
                var item = new FontItem { FontFamily = ff, IsSelected = _currentFontNames.Values.Contains(ff.Name) };
                if (item.IsSelected)
                    item.KeyName = _currentFontNames.FirstOrDefault(x => x.Value == ff.Name).Key;
                item.PropertyChanged += Item_PropertyChanged;
                FontFamilies.Add(item);
            }
        }

        private void Save(object state)
        {
            var e = new ExecuteUiActionEventArgs("Save Settings");
            e.Parameters.Add("WindowTitle", "Save settings"); ;
            e.Parameters.Add("Message", $"Save the settings for the {SelectedApp.Name} console?");
            e.Parameters.Add("Icon", TaskDialogIcon.Warning);
            e.Parameters.Add("Button1Text", "Yes");
            e.Parameters.Add("Button1NoteText", $"Go ahead and make changes to the selected console");
            e.Parameters.Add("Button2Text", "No");
            e.Parameters.Add("Button2NoteText", $"Cancel saving and return to the application");
            e.Parameters.Add("Cancel", false);
            ExecuteUIAction?.Invoke(this, e);
            if ((bool)e.Parameters["Cancel"])
                return;
            SelectedApp.UpdateRegistry();
        }

        private bool ValidateSaveState(object state) => SelectedApp != null && SelectedApp.HasChanges;
    }
}