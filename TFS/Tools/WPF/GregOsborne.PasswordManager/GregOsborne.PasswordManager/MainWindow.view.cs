// <copyright file="MainWindow.view.cs" company="">
// Copyright (c) 2019 All rights reserved
// </copyright>
// <author>IDOTCENTRAL\gosborn</author>
// <date>12/16/2019</date>

namespace GregOsborne.PasswordManager {
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;

    using GregOsborne.Application;
    using GregOsborne.Application.Media;
    using GregOsborne.Application.Primitives;
    using GregOsborne.Application.Theme;
    using GregOsborne.MVVMFramework;
    using GregOsborne.PasswordManager.Data;

    public partial class MainWindowView : ViewModelBase, IThemedView {
        public const string AddNewSecurityGroupText = "AddNewSecurityGroup";
        public const string AddNewSecurityItemText = "AddNewSecurityItem";
        public const string ApplyThemeText = "ApplyTheme";
        public const string CloseApplicationText = "CloseApplication";
        public const string CloseOptionsText = "CloseOptions";
        public const string FocusToNameText = "FocusToName";
        public const string ImportFileText = "ImportFile";
        public const string MaximizeRestoreApplicationText = "MaximizeRestoreApplication";
        public const string MinimizeApplicationText = "MinimizeApplication";
        public const string OpenOptionsText = "OpenOptions";
        public const string ShowMessageText = "ShowMessage";
        internal const double sliderWidthModifier = 33.33;
        private const string applicationName = "Password Manager";
        private const double buttonWidthModifier = 8.3;
        private const double menuWidthModifier = 10;
        private SolidColorBrush activeCaptionBrush = default;
        private Color activeCaptionColor = default;
        private SolidColorBrush activeCaptionTextBrush = default;
        private Color activeCaptionTextColor = default;
        private readonly string addNewText = "Add new...";
        private string appName = default;
        private Settings appSettings = default;
        private SolidColorBrush borderBrush = default;
        private Color borderColor = default;
        private SolidColorBrush controlBorderBrush = default;
        private Color controlBorderColor = default;
        private readonly string defaultText = "Default";
        private double fontSize = default;
        private ObservableCollection<double> fontSizes = default;
        private SolidColorBrush inactiveBrush = default;
        private bool isAllowEditingEnabled = default;
        private bool isAllowEnabled = default;
        private bool isInitializing = true;
        private bool isSettingColor = false;
        private bool? isWindowBoundsSaved = default;
        private Thickness menuMargin = default;
        private double optionsWidth = default;
        private SolidColorBrush savedWindowBrush = default;
        private ObservableCollection<SecurityItemBase> securityItems = default;
        private SecurityItem selectedSecurityItem = default;
        private double settingsWidth = default;
        private double standardButtonWidth = default;
        private ApplicationTheme theme = default;
        private ObservableCollection<ApplicationTheme> themes = default;
        private readonly string visualThemeNameText = $"{ThemeNames.Visual}.ThemeName";
        private SolidColorBrush windowBrush = default;
        private Color windowColor = default;
        private SolidColorBrush windowTextBrush = default;
        private Color windowTextColor = default;
        private string windowTitle = default;
        public MainWindowView() {
            WindowTitle = applicationName;
            ActiveCaptionColor = SystemColors.ActiveCaptionColor;
            ActiveCaptionTextColor = SystemColors.ActiveCaptionTextColor;
            WindowColor = SystemColors.WindowColor;
            WindowTextColor = SystemColors.WindowTextColor;
            BorderColor = SystemColors.ActiveBorderColor;
            IsAllowEnabled = true;
            FontSize = 12.0;
            OptionsWidth = 0.0;
            FontSizes = new ObservableCollection<double>();
            for (double i = 8; i < 25; i++) {
                FontSizes.Add(i);
            }
            Themes = new ObservableCollection<ApplicationTheme>();
            SecurityItems = new ObservableCollection<SecurityItemBase>();
        }

        public event ExecuteUiActionHandler ExecuteUiAction;
        public SolidColorBrush ActiveCaptionBrush {
            get => this.activeCaptionBrush;
            set {
                this.activeCaptionBrush = value;
                this.isSettingColor = true;
                ActiveCaptionColor = ActiveCaptionBrush.Color;
                this.isSettingColor = false;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public Color ActiveCaptionColor {
            get => this.activeCaptionColor;
            set {
                this.activeCaptionColor = value;
                if (!this.isSettingColor) {
                    ActiveCaptionBrush = new SolidColorBrush(ActiveCaptionColor);
                    if (IsAllowEditingEnabled) {
                        Theme.ActiveCaptionBrush.Value = ActiveCaptionBrush;
                        Theme.HasChanges = true;
                    }
                }
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public SolidColorBrush ActiveCaptionTextBrush {
            get => this.activeCaptionTextBrush;
            set {
                this.activeCaptionTextBrush = value;
                this.isSettingColor = true;
                ActiveCaptionTextColor = ActiveCaptionTextBrush.Color;
                this.isSettingColor = false;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public Color ActiveCaptionTextColor {
            get => this.activeCaptionTextColor;
            set {
                this.activeCaptionTextColor = value;
                if (!this.isSettingColor) {
                    ActiveCaptionTextBrush = new SolidColorBrush(ActiveCaptionTextColor);
                    if (IsAllowEditingEnabled) {
                        Theme.ActiveCaptionTextBrush.Value = ActiveCaptionTextBrush;
                        Theme.HasChanges = true;
                    }
                }
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public SolidColorBrush BorderBrush {
            get => this.borderBrush;
            set {
                this.borderBrush = value;
                this.isSettingColor = true;
                BorderColor = BorderBrush.Color;
                this.isSettingColor = false;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public Color BorderColor {
            get => this.borderColor;
            set {
                this.borderColor = value;
                if (!this.isSettingColor) {
                    BorderBrush = new SolidColorBrush(BorderColor);
                    if (IsAllowEditingEnabled) {
                        Theme.BorderBrush.Value = BorderBrush;
                        Theme.HasChanges = true;
                    }
                }
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public SolidColorBrush ControlBorderBrush {
            get => this.controlBorderBrush;
            set {
                this.controlBorderBrush = value;
                this.isSettingColor = true;
                ControlBorderColor = ControlBorderBrush.Color;
                this.isSettingColor = false;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public Color ControlBorderColor {
            get => this.controlBorderColor;
            set {
                this.controlBorderColor = value;
                if (!this.isSettingColor) {
                    ControlBorderBrush = new SolidColorBrush(ControlBorderColor);
                    if (IsAllowEditingEnabled) {
                        Theme.ControlBorderBrush.Value = ControlBorderBrush;
                        Theme.HasChanges = true;
                    }
                }
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public double FontSize {
            get => this.fontSize;
            set {
                this.fontSize = value;
                if (IsAllowEditingEnabled) {
                    Theme.FontSize.Value = value;
                    Theme.HasChanges = true;
                }
                ItemTitleFontSize = FontSize * 1.5;
                SettingsWidth = sliderWidthModifier * FontSize;
                if (OptionsWidth > 0.0) {
                    OptionsWidth = SettingsWidth;
                }
                MenuMargin = new Thickness(0, 0, menuWidthModifier * FontSize, 5);
                StandardButtonWidth = buttonWidthModifier * FontSize;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public ObservableCollection<double> FontSizes {
            get => this.fontSizes;
            set {
                this.fontSizes = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public SolidColorBrush InactiveBrush {
            get => this.inactiveBrush;
            set {
                this.inactiveBrush = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public bool IsAllowEditingEnabled {
            get => this.isAllowEditingEnabled;
            set {
                this.isAllowEditingEnabled = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public bool IsAllowEnabled {
            get => this.isAllowEnabled;
            set {
                this.isAllowEnabled = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public bool? IsWindowBoundsSaved {
            get => this.isWindowBoundsSaved;
            set {
                this.isWindowBoundsSaved = value;
                if (!this.isInitializing) {
                    this.appSettings.AddOrUpdateSetting(this.appName, $"{ThemeNames.Visual}.{ThemeNames.SaveMainWindowBounds}", IsWindowBoundsSaved);
                }
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public Thickness MenuMargin {
            get => this.menuMargin;
            set {
                this.menuMargin = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public double OptionsWidth {
            get => this.optionsWidth;
            set {
                this.optionsWidth = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public SolidColorBrush SavedWindowBrush {
            get => this.savedWindowBrush;
            set {
                this.savedWindowBrush = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public ObservableCollection<SecurityItemBase> SecurityItems {
            get => this.securityItems;
            set {
                this.securityItems = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public SecurityItem SelectedSecurityItem {
            get => this.selectedSecurityItem;
            set {
                this.selectedSecurityItem = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public double SettingsWidth {
            get => this.settingsWidth;
            set {
                this.settingsWidth = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public double StandardButtonWidth {
            get => this.standardButtonWidth;
            set {
                this.standardButtonWidth = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public ApplicationTheme Theme {
            get => this.theme;
            set {
                this.theme = value;
                if (Theme != null) {
                    if (Theme.Name.Equals(this.addNewText)) {
                        IsAllowEditingEnabled = true;
                        IsAllowEnabled = true;
                        ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs(FocusToNameText));
                    } else if (Theme.Name.Equals("Default")) {
                        IsAllowEnabled = false;
                    } else {
                        IsAllowEnabled = true;
                        var settingsThemeName = this.appSettings.GetValue(this.appName, this.visualThemeNameText, this.defaultText);
                        if (!Theme.Name.Equals(settingsThemeName, System.StringComparison.OrdinalIgnoreCase)) {
                            this.appSettings.AddOrUpdateSetting(this.appName, this.visualThemeNameText, Theme.Name);
                        }
                    }
                    ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs(ApplyThemeText));
                    Theme.HasChanges = false;
                }
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public ObservableCollection<ApplicationTheme> Themes {
            get => this.themes;
            set {
                this.themes = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public SolidColorBrush WindowBrush {
            get => this.windowBrush;
            set {
                this.windowBrush = value;
                this.isSettingColor = true;
                WindowColor = WindowBrush.Color;
                this.isSettingColor = false;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public Color WindowColor {
            get => this.windowColor;
            set {
                this.windowColor = value;
                if (!this.isSettingColor) {
                    WindowBrush = new SolidColorBrush(WindowColor);
                    if (IsAllowEditingEnabled) {
                        Theme.WindowBrush.Value = WindowBrush;
                        Theme.HasChanges = true;
                    }
                }
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public SolidColorBrush WindowTextBrush {
            get => this.windowTextBrush;
            set {
                this.windowTextBrush = value;
                this.isSettingColor = true;
                WindowTextColor = WindowTextBrush.Color;
                this.isSettingColor = false;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public Color WindowTextColor {
            get => this.windowTextColor;
            set {
                this.windowTextColor = value;
                if (!this.isSettingColor) {
                    WindowTextBrush = new SolidColorBrush(WindowTextColor);
                    if (IsAllowEditingEnabled) {
                        Theme.WindowTextBrush.Value = WindowTextBrush;
                        Theme.HasChanges = true;
                    }
                }
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public string WindowTitle {
            get => this.windowTitle;
            set {
                this.windowTitle = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public void ApplyVisualElement<T>(VisualElement<T> element) {
            var viewProperty = GetType().GetProperty(element.Name);
            viewProperty.SetValue(this, element.Value);
        }

        public override void Initialize() {
            var thisApp = Application.Current.As<App>();
            this.appSettings = thisApp.Session.ApplicationSettings;
            this.appName = thisApp.Name;

            IsWindowBoundsSaved = this.appSettings.GetValue(this.appName, $"{ThemeNames.Visual}.{ThemeNames.SaveMainWindowBounds}", false);
            Themes = new ObservableCollection<ApplicationTheme>();

            if (thisApp.ThemeManager != null && thisApp.ThemeManager.Themes.Any()) {
                thisApp.ThemeManager.Themes.ForEach(x => Themes.Add(x));
                Themes.Add(new ApplicationTheme(thisApp.ThemeManager.ThemesFileName) {
                    Name = addNewText
                });
                var themeName = this.appSettings.GetValue(this.appName, this.visualThemeNameText, "default");
                Theme = Themes.FirstOrDefault(x => x.Name.Equals(themeName, System.StringComparison.InvariantCultureIgnoreCase));
            }

            if (Theme == null) {
                var defaultTheme = Themes.FirstOrDefault(x => x.Name == "Default");
                if (defaultTheme == null) {
                    FontSize = ApplicationTheme.DefaultValues.FontSize;
                    ActiveCaptionBrush = ApplicationTheme.DefaultValues.ActiveCaption;
                    ActiveCaptionTextBrush = ApplicationTheme.DefaultValues.ActiveCaptionText;
                    WindowBrush = ApplicationTheme.DefaultValues.Window;
                    WindowTextBrush = ApplicationTheme.DefaultValues.WindowText;
                    BorderBrush = ApplicationTheme.DefaultValues.Border;

                    SavedWindowBrush = new SolidColorBrush(WindowColor);
                    InactiveBrush = new SolidColorBrush("#FFDADADA".ToColor());
                } else {
                    Theme = defaultTheme;
                }
            }
            Theme.HasChanges = false;
            Populate(null);
            this.isInitializing = false;
        }


        private double itemTitleFontSize = default;
        public double ItemTitleFontSize {
            get => itemTitleFontSize;
            set {
                itemTitleFontSize = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        private void Populate(SecurityItemBase item) {
            SecurityItems = new ObservableCollection<SecurityItemBase>();
            if (item == null) {
                Application.Current.As<App>().SecurityFile.Groups.ToList().ForEach(x => SecurityItems.Add(x));
            } else {
                item.ControlBorderBrush = ControlBorderBrush;
                item.FontSize = FontSize;
                item.WindowBrush = WindowBrush;
                item.WindowTextBrush = WindowTextBrush;
                item.ItemTitleFontSize = ItemTitleFontSize;
                if (item.Is<SecurityItem>()) {
                    SelectedSecurityItem = item.As<SecurityItem>();
                } else {
                    SelectedSecurityItem = null;
                    var group = item.As<SecurityGroup>();
                    group.Groups.ToList().ForEach(x => {
                        x.ControlBorderBrush = ControlBorderBrush;
                        x.WindowBrush = WindowBrush;
                        x.WindowTextBrush = WindowTextBrush;
                        x.FontSize = FontSize;
                        x.ItemTitleFontSize = ItemTitleFontSize;
                        SecurityItems.Add(x);
                    });
                    group.Items.ToList().ForEach(x => {
                        x.ControlBorderBrush = ControlBorderBrush;
                        x.WindowBrush = WindowBrush;
                        x.WindowTextBrush = WindowTextBrush;
                        x.FontSize = FontSize;
                        x.ItemTitleFontSize = ItemTitleFontSize;
                        SecurityItems.Add(x);
                    });
                }
            }
        }
    }
}
