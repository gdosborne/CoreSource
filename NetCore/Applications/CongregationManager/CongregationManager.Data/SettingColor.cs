using Common.Media;
using Common.MVVMFramework;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace CongregationManager.Data {
    public class SettingColor : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #region Name Property
        private string _Name = default;
        /// <summary>Gets/sets the Name.</summary>
        /// <value>The Name.</value>
        public string Name {
            get => _Name;
            set {
                _Name = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Key Property
        private string _Key = default;
        /// <summary>Gets/sets the Key.</summary>
        /// <value>The Key.</value>
        public string Key {
            get => _Key;
            set {
                _Key = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ColorValue Property
        private bool settingColor = false;
        private Color _ColorValue = default;
        /// <summary>Gets/sets the ColorValue.</summary>
        /// <value>The ColorValue.</value>
        public Color ColorValue {
            get => _ColorValue;
            set {
                _ColorValue = value;
                if (!settingStringValue) {
                    settingColor = true;
                    ColorString = ColorValue.ToHexValue();
                    settingColor = false;
                }
                OnPropertyChanged();
            }
        }
        #endregion

        #region ColorString Property
        private string _ColorString = default;
        private bool settingStringValue = false;
        /// <summary>Gets/sets the ColorString.</summary>
        /// <value>The ColorString.</value>
        public string ColorString {
            get => _ColorString;
            set {
                _ColorString = value;
                if (!settingColor) {
                    settingStringValue = true;
                    ColorValue = ColorString.ToColor();
                    settingStringValue = false;
                }
                OnPropertyChanged();
            }
        }
        #endregion

        public override string ToString() =>
            $"{Name} ({ColorString})";

        #region SelectColorCommand
        private DelegateCommand _SelectColorCommand = default;
        /// <summary>Gets the SelectColor command.</summary>
        /// <value>The SelectColor command.</value>
        public DelegateCommand SelectColorCommand => _SelectColorCommand ??= new DelegateCommand(SelectColor, ValidateSelectColorState);
        private bool ValidateSelectColorState(object state) => true;
        private void SelectColor(object state) {
            ColorClicked?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        public event EventHandler ColorClicked;

        public SolidColorBrush ToBrush() =>
            new SolidColorBrush(ColorValue);
    }
}
