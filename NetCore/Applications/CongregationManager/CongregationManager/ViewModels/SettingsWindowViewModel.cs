﻿using Common.Application.Media;
using Common.Application.Primitives;
using Common.MVVMFramework;
using CongregationManager.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace CongregationManager.ViewModels {
    public class SettingsWindowViewModel : ViewModelBase {
        public SettingsWindowViewModel() {
            Title = "Settings [design]";
        }

        public override void Initialize() {
            base.Initialize();

            Title = "Settings";

            
            Fonts = new ObservableCollection<FontFamily>(Common.Application.Media.Extensions.GetAllFontFamiles().OrderBy(x => x.Source));
            SelectedFontFamily = Fonts.FirstOrDefault(x => x.Source == 
                App.ApplicationSession.ApplicationSettings.GetValue("Application", "FontFamilyName", "Calibri"));
            FontSizes = new ObservableCollection<double>();
            for (double i = 10; i < 41; i++) {
                FontSizes.Add(i);
            }
            SelectedFontSize = App.ApplicationSession.ApplicationSettings.GetValue("Application", "FontSize", 16.0);

            IsAppDefaultMode = true;
        }

        public enum Actions {
            CloseWindow,
            CreateTheme
        }

        #region CloseWindowCommand
        private DelegateCommand _CloseWindowCommand = default;
        /// <summary>Gets the CloseWindow command.</summary>
        /// <value>The CloseWindow command.</value>
        public DelegateCommand CloseWindowCommand => _CloseWindowCommand ??= new DelegateCommand(CloseWindow, ValidateCloseWindowState);
        private bool ValidateCloseWindowState(object state) => true;
        private void CloseWindow(object state) {
            ExecuteAction(nameof(Actions.CloseWindow));
        }
        #endregion

        #region Fonts Property
        private ObservableCollection<FontFamily> _Fonts = default;
        /// <summary>Gets/sets the Fonts.</summary>
        /// <value>The Fonts.</value>
        public ObservableCollection<FontFamily> Fonts {
            get => _Fonts;
            set {
                _Fonts = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SelectedFontFamily Property
        private FontFamily _SelectedFontName = default;
        /// <summary>Gets/sets the SelectedFontFamily.</summary>
        /// <value>The SelectedFontFamily.</value>
        public FontFamily SelectedFontFamily {
            get => _SelectedFontName;
            set {
                _SelectedFontName = value;
                App.ApplicationSession.ApplicationSettings.AddOrUpdateSetting("Application", "FontFamilyName", SelectedFontFamily.Source);
                App.Current.Resources["StandardFont"] = SelectedFontFamily;
                OnPropertyChanged();
            }
        }
        #endregion

        #region FontSizes Property
        private ObservableCollection<double> _FontSizes = default;
        /// <summary>Gets/sets the FontSizes.</summary>
        /// <value>The FontSizes.</value>
        public ObservableCollection<double> FontSizes {
            get => _FontSizes;
            set {
                _FontSizes = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SelectedFontSize Property
        private double _SelectedFontSize = default;
        /// <summary>Gets/sets the SelectedFontSize.</summary>
        /// <value>The SelectedFontSize.</value>
        public double SelectedFontSize {
            get => _SelectedFontSize;
            set {
                _SelectedFontSize = value;
                App.ApplicationSession.ApplicationSettings.AddOrUpdateSetting("Application", "FontSize", SelectedFontSize);
                App.Current.Resources["StandardFontSize"] = SelectedFontSize;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsLightMode Property
        private bool _IsLightMode = default;
        /// <summary>Gets/sets the IsLightMode.</summary>
        /// <value>The IsLightMode.</value>
        public bool IsLightMode {
            get => _IsLightMode;
            set {
                _IsLightMode = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsDarkMode Property
        private bool _IsDarkMode = default;
        /// <summary>Gets/sets the IsDarkMode.</summary>
        /// <value>The IsDarkMode.</value>
        public bool IsDarkMode {
            get => _IsDarkMode;
            set {
                _IsDarkMode = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsCustomMode Property
        private bool _IsCustomMode = default;
        /// <summary>Gets/sets the IsCustomMode.</summary>
        /// <value>The IsCustomMode.</value>
        public bool IsCustomMode {
            get => _IsCustomMode;
            set {
                _IsCustomMode = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsAppDefaultMode Property
        private bool _IsAppDefaultMode = default;
        /// <summary>Gets/sets the IsAppDefaultMode.</summary>
        /// <value>The IsAppDefaultMode.</value>
        public bool IsAppDefaultMode {
            get => _IsAppDefaultMode;
            set {
                _IsAppDefaultMode = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CreateThemeCommand
        private DelegateCommand _CreateThemeCommand = default;
        /// <summary>Gets the CreateTheme command.</summary>
        /// <value>The CreateTheme command.</value>
        public DelegateCommand CreateThemeCommand => _CreateThemeCommand ??= new DelegateCommand(CreateTheme, ValidateCreateThemeState);
        private bool ValidateCreateThemeState(object state) => true;
        private void CreateTheme(object state) {
            ExecuteAction(nameof(Actions.CreateTheme));
        }
        #endregion

    }
}
