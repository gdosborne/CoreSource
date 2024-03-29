using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using GregOsborne.Application.Primitives;
using GregOsborne.MVVMFramework;

namespace GregOsborne.Dialog {
    public class FontDialogBoxView : INotifyPropertyChanged {
        private DelegateCommand _CancelCommand;
        private DelegateCommand _OKCommand;

        public FontDialogBoxView() {
            FontSizes = new ObservableCollection<double>();
            FontStyles = new ObservableCollection<FontStyle>();
            FontWeights = new ObservableCollection<FontWeight>();
            Fonts = new ObservableCollection<FontFamily>();
        }
        public DelegateCommand OkCommand => _OKCommand ?? (_OKCommand = new DelegateCommand(Ok, ValidateOkState));

        public DelegateCommand CancelCommand => _CancelCommand ?? (_CancelCommand = new DelegateCommand(Cancel, ValidateCancelState));
        public event PropertyChangedEventHandler PropertyChanged;
        private void Ok(object state) {
            DialogResult = true;
        }
        private bool ValidateOkState(object state) {
            return SelectedFontFamily != null && SelectedFontSize > 0;
        }
        private void Cancel(object state) {
            DialogResult = false;
        }
        private static bool ValidateCancelState(object state) {
            return true;
        }
        public void Initialize(Window window) {
        }
        public void InitView() {
            foreach (var f in System.Windows.Media.Fonts.SystemFontFamilies.OrderBy(x => x.Source))
                Fonts.Add(f);
            for (double x = 6; x < 73; x++)
                FontSizes.Add(x);
            FontStyles.Add(System.Windows.FontStyles.Normal);
            FontStyles.Add(System.Windows.FontStyles.Italic);
            FontStyles.Add(System.Windows.FontStyles.Oblique);
            FontWeights.Add(System.Windows.FontWeights.Black);
            FontWeights.Add(System.Windows.FontWeights.Bold);
            FontWeights.Add(System.Windows.FontWeights.DemiBold);
            FontWeights.Add(System.Windows.FontWeights.ExtraBlack);
            FontWeights.Add(System.Windows.FontWeights.ExtraBold);
            FontWeights.Add(System.Windows.FontWeights.ExtraLight);
            FontWeights.Add(System.Windows.FontWeights.Heavy);
            FontWeights.Add(System.Windows.FontWeights.Light);
            FontWeights.Add(System.Windows.FontWeights.Medium);
            FontWeights.Add(System.Windows.FontWeights.Normal);
            FontWeights.Add(System.Windows.FontWeights.Regular);
            FontWeights.Add(System.Windows.FontWeights.SemiBold);
            FontWeights.Add(System.Windows.FontWeights.Thin);
            FontWeights.Add(System.Windows.FontWeights.UltraBlack);
            FontWeights.Add(System.Windows.FontWeights.UltraBold);
            FontWeights.Add(System.Windows.FontWeights.UltraLight);
        }
        public void Persist(Window window) {
        }
        public void UpdateInterface() {
            OkCommand.RaiseCanExecuteChanged();
        }
        private bool? _dialogResult;
        private ObservableCollection<FontFamily> _fonts;
        private ObservableCollection<double> _fontSizes;
        private ObservableCollection<FontStyle> _fontStyles;
        private ObservableCollection<FontWeight> _fontWeights;
        private FontFamily _selectedFontFamily;
        private double _selectedFontSize;
        private FontStyle _selectedFontStyle;
        private FontWeight _selectedFontWeight;
        private Visibility _sizeVisibility;
        private Visibility _styleVisibility;
        private Visibility _weightVisibility;

        public bool? DialogResult {
            get => _dialogResult;
            set {
                _dialogResult = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        public ObservableCollection<FontFamily> Fonts {
            get => _fonts;
            set {
                _fonts = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        public ObservableCollection<double> FontSizes {
            get => _fontSizes;
            set {
                _fontSizes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        public ObservableCollection<FontStyle> FontStyles {
            get => _fontStyles;
            set {
                _fontStyles = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        public ObservableCollection<FontWeight> FontWeights {
            get => _fontWeights;
            set {
                _fontWeights = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        public FontFamily SelectedFontFamily {
            get => _selectedFontFamily;
            set {
                _selectedFontFamily = value;
                UpdateInterface();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        public double SelectedFontSize {
            get => _selectedFontSize;
            set {
                _selectedFontSize = value;
                UpdateInterface();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        public FontStyle SelectedFontStyle {
            get => _selectedFontStyle;
            set {
                _selectedFontStyle = value;
                UpdateInterface();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        public FontWeight SelectedFontWeight {
            get => _selectedFontWeight;
            set {
                _selectedFontWeight = value;
                UpdateInterface();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        public Visibility SizeVisibility {
            get => _sizeVisibility;
            set {
                _sizeVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        public Visibility StyleVisibility {
            get => _styleVisibility;
            set {
                _styleVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        public Visibility WeightVisibility {
            get => _weightVisibility;
            set {
                _weightVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
    }
}