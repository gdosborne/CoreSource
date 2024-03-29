using System.Windows;
using System.Windows.Media;

namespace GregOsborne.Dialog {
    public sealed class FontDialog {
        public FontDialog() {
            StyleVisibility = Visibility.Visible;
            SizeVisibility = Visibility.Visible;
            WeightVisibility = Visibility.Visible;
        }
        public FontStyle CurrentFontStyle { get; set; }
        public FontWeight CurrentFontWeight { get; set; }
        public double CurrentFontSize { get; set; }
        public FontFamily CurrentFontFamily { get; set; }
        public Visibility StyleVisibility { get; set; }
        public Visibility WeightVisibility { get; set; }
        public Visibility SizeVisibility { get; set; }
        public string Title { get; set; }

        public void Show() {
            var dlg = new FontDialogBox {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Title = Title,
                StyleVisibility = StyleVisibility,
                WeightVisibility = WeightVisibility,
                SizeVisibility = SizeVisibility,
                CurrentFontFamily = CurrentFontFamily,
                CurrentFontSize = CurrentFontSize,
                CurrentFontWeight = CurrentFontWeight,
                CurrentFontStyle = CurrentFontStyle
            };
            dlg.Show();
        }

        public bool? ShowDialog(Window owner) {
            var dlg = new FontDialogBox {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = owner,
                Title = Title,
                StyleVisibility = StyleVisibility,
                WeightVisibility = WeightVisibility,
                SizeVisibility = SizeVisibility,
                CurrentFontFamily = CurrentFontFamily,
                CurrentFontSize = CurrentFontSize,
                CurrentFontWeight = CurrentFontWeight,
                CurrentFontStyle = CurrentFontStyle
            };
            var result = dlg.ShowDialog();
            CurrentFontFamily = dlg.CurrentFontFamily;
            CurrentFontSize = dlg.CurrentFontSize;
            CurrentFontWeight = dlg.CurrentFontWeight;
            CurrentFontStyle = dlg.CurrentFontStyle;
            return result;
        }
    }
}