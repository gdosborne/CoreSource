using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFControls {
    public partial class VerticalSeparator : UserControl {
        public VerticalSeparator() {
            InitializeComponent();
            SizeChanged += (s, e) => {
                DrawIt();
            };
        }

        #region Foreground Dependency Property
        public static new readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(VerticalSeparator), new PropertyMetadata(Brushes.Black, new PropertyChangedCallback(OnForegroundChanged)));
        public new Brush Foreground {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }
        private static void OnForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (VerticalSeparator)d;
            var val = (Brush)e.NewValue;
            obj.DrawIt();
        }
        #endregion

        #region Background Dependency Property
        public static new readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(VerticalSeparator), new PropertyMetadata(default(Brush), new PropertyChangedCallback(OnBackgroundChanged)));
        public new Brush Background {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }
        private static void OnBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (VerticalSeparator)d;
            var val = (Brush)e.NewValue;
            obj.Background = val;
            obj.DrawIt();
        }
        #endregion

        #region Thickness Dependency Property
        public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register("Thickness", typeof(double), typeof(VerticalSeparator), new PropertyMetadata(1.0, new PropertyChangedCallback(OnThicknessChanged)));
        public double Thickness {
            get { return (double)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }
        private static void OnThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (VerticalSeparator)d;
            var val = (double)e.NewValue;
            obj.DrawIt();
        }
        #endregion

        #region Padding Dependency Property
        public static new readonly DependencyProperty PaddingProperty = DependencyProperty.Register("Padding", typeof(Thickness), typeof(VerticalSeparator), new PropertyMetadata(new Thickness(0), new PropertyChangedCallback(OnPaddingChanged)));
        public new Thickness Padding {
            get { return (Thickness)GetValue(PaddingProperty); }
            set { SetValue(PaddingProperty, value); }
        }
        private static void OnPaddingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var obj = (VerticalSeparator)d;
            var val = (Thickness)e.NewValue;
            obj.DrawIt();
        }
        #endregion

        private void DrawIt() {
            this.Width = this.Padding.Left + this.Padding.Right + this.Thickness;
            var lineHeight = this.ActualHeight - this.Padding.Bottom - this.Padding.Top;
            var lineX = this.Padding.Left;
            theLine.X1 = lineX;
            theLine.X2 = lineX;
            theLine.Y1 = 0;
            theLine.Y2 = lineHeight;
            theLine.Stroke = this.Foreground;
            theLine.StrokeThickness = Thickness;
        }

    }
}
