using GregOsborne.Application.Theme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Media;

namespace ManageVersioning.SharedViews {
    public class SharedView : ViewModelBase {
        #region TitlebarFontSize Property
        private double _TitlebarFontSize = default;
        public double TitlebarFontSize {
            get => _TitlebarFontSize;
            set {
                _TitlebarFontSize = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region FontSize Property
        private double _FontSize = default;
        public double FontSize {
            get => _FontSize;
            set {
                _FontSize = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ActiveCaptionBrush Property
        private SolidColorBrush _ActiveCaptionBrush = default;
        public SolidColorBrush ActiveCaptionBrush {
            get => _ActiveCaptionBrush;
            set {
                _ActiveCaptionBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ActiveCaptionTextBrush Property
        private SolidColorBrush _ActiveCaptionTextBrush = default;
        public SolidColorBrush ActiveCaptionTextBrush {
            get => _ActiveCaptionTextBrush;
            set {
                _ActiveCaptionTextBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region BorderBrush Property
        private SolidColorBrush _BorderBrush = default;
        public SolidColorBrush BorderBrush {
            get => _BorderBrush;
            set {
                _BorderBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ControlBorderBrush Property
        private SolidColorBrush _ControlBorderBrush = default;
        public SolidColorBrush ControlBorderBrush {
            get => _ControlBorderBrush;
            set {
                _ControlBorderBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region WindowBrush Property
        private SolidColorBrush _WindowBrush = default;
        public SolidColorBrush WindowBrush {
            get => _WindowBrush;
            set {
                _WindowBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region WindowTextBrush Property
        private SolidColorBrush _WindowTextBrush = default;
        public SolidColorBrush WindowTextBrush {
            get => _WindowTextBrush;
            set {
                _WindowTextBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CloseBackgroundBrush Property
        private SolidColorBrush _CloseBackgroundBrush = default;
        public SolidColorBrush CloseBackgroundBrush {
            get => _CloseBackgroundBrush;
            set {
                _CloseBackgroundBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CloseForegroundBrush Property
        private SolidColorBrush _CloseForegroundBrush = default;
        public SolidColorBrush CloseForegroundBrush {
            get => _CloseForegroundBrush;
            set {
                _CloseForegroundBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region HighlightBrush Property
        private SolidColorBrush _HighlightBrush = default;
        public SolidColorBrush HighlightBrush {
            get => _HighlightBrush;
            set {
                _HighlightBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region HighlightTextBrush Property
        private SolidColorBrush _HighlightTextBrush = default;
        public SolidColorBrush HighlightTextBrush {
            get => _HighlightTextBrush;
            set {
                _HighlightTextBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ConsoleForegroundBrush Property
        private SolidColorBrush _ConsoleForegroundBrush = default;
        public SolidColorBrush ConsoleForegroundBrush {
            get => _ConsoleForegroundBrush;
            set {
                _ConsoleForegroundBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ConsoleBackgroundBrush Property
        private SolidColorBrush _ConsoleBackgroundBrush = default;
        public SolidColorBrush ConsoleBackgroundBrush {
            get => _ConsoleBackgroundBrush;
            set {
                _ConsoleBackgroundBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region DataGridAlternatingRowBackgroundBrush Property
        private SolidColorBrush _DataGridAlternatingRowBackgroundBrush = default;
        public SolidColorBrush DataGridAlternatingRowBackgroundBrush {
            get => _DataGridAlternatingRowBackgroundBrush;
            set {
                _DataGridAlternatingRowBackgroundBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region DataGridHeaderBackgroundBrush Property
        private SolidColorBrush _DataGridHeaderBackgroundBrush = default;
        public SolidColorBrush DataGridHeaderBackgroundBrush {
            get => _DataGridHeaderBackgroundBrush;
            set {
                _DataGridHeaderBackgroundBrush = value;
                if (DGColumnHeaderStyle != null) {
                    DGColumnHeaderStyle.Setters.First(x => x.As<Setter>().Property == DataGridColumnHeader.BackgroundProperty).As<Setter>().Value = DataGridHeaderBackgroundBrush;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region DataGridHeaderForegroundBrush Property
        private SolidColorBrush _DataGridHeaderForegroundBrush = default;
        public SolidColorBrush DataGridHeaderForegroundBrush {
            get => _DataGridHeaderForegroundBrush;
            set {
                _DataGridHeaderForegroundBrush = value;
                if (DGColumnHeaderStyle != null) {
                    DGColumnHeaderStyle.Setters.First(x => x.As<Setter>().Property == DataGridColumnHeader.ForegroundProperty).As<Setter>().Value = DataGridHeaderForegroundBrush;
                }
                OnPropertyChanged();
            }
        }
        #endregion

        #region ToggleBackgroundBrush Property
        private SolidColorBrush _ToggleBackgroundBrush = default;
        public SolidColorBrush ToggleBackgroundBrush {
            get => _ToggleBackgroundBrush;
            set {
                _ToggleBackgroundBrush = value;
                if (ToggleStyle != null)
                    ToggleStyle.Setters.First(x => x.As<Setter>().Property == Controls.Toggle.ToggleBackgroundProperty).As<Setter>().Value = ToggleBackgroundBrush;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ToggleForegroundBrush Property
        private SolidColorBrush _ToggleForegroundBrush = default;
        public SolidColorBrush ToggleForegroundBrush {
            get => _ToggleForegroundBrush;
            set {
                _ToggleForegroundBrush = value;
                if (ToggleStyle != null)
                    ToggleStyle.Setters.First(x => x.As<Setter>().Property == Controls.Toggle.ToggleForegroundProperty).As<Setter>().Value = ToggleForegroundBrush;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ToggleOffBackgroundBrush Property
        private SolidColorBrush _ToggleOffBackgroundBrush = default;
        public SolidColorBrush ToggleOffBackgroundBrush {
            get => _ToggleOffBackgroundBrush;
            set {
                _ToggleOffBackgroundBrush = value;
                if (ToggleStyle != null)
                    ToggleStyle.Setters.First(x => x.As<Setter>().Property == Controls.Toggle.ToggleOffBackgroundProperty).As<Setter>().Value = ToggleOffBackgroundBrush;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ToggleOffForegroundBrush Property
        private SolidColorBrush _ToggleOffForegroundBrush = default;
        public SolidColorBrush ToggleOffForegroundBrush {
            get => _ToggleOffForegroundBrush;
            set {
                _ToggleOffForegroundBrush = value;
                if (ToggleStyle != null)
                    ToggleStyle.Setters.First(x => x.As<Setter>().Property == Controls.Toggle.ToggleOffForegroundProperty).As<Setter>().Value = ToggleOffForegroundBrush;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ToggleSize Property
        private double _ToggleSize = default;
        public double ToggleSize {
            get => _ToggleSize;
            set {
                _ToggleSize = value;
                if (ToggleStyle != null)
                    ToggleStyle.Setters.First(x => x.As<Setter>().Property == Controls.Toggle.ToggleSizeProperty).As<Setter>().Value = ToggleSize;
                OnPropertyChanged();
            }
        }
        #endregion

        #region DGColumnHeaderStyle Property
        private Style _DGColumnHeaderStyle = default;
        public Style DGColumnHeaderStyle {
            get => _DGColumnHeaderStyle;
            set {
                _DGColumnHeaderStyle = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ToggleStyle Property
        private Style _ToggleStyle = default;
        public Style ToggleStyle {
            get => _ToggleStyle;
            set {
                _ToggleStyle = value;
                OnPropertyChanged();
            }
        }
        #endregion

        protected Window window = default;
        protected void UpdateDGColumnHeaderStyle() {
            var result = new Style(typeof(DataGridColumnHeader));
            result.Setters.Add(new Setter(DataGridColumnHeader.BackgroundProperty, WindowTextBrush));
            result.Setters.Add(new Setter(DataGridColumnHeader.ForegroundProperty, WindowBrush));
            result.Setters.Add(new Setter(DataGridColumnHeader.PaddingProperty, new Thickness(5, 3, 5, 3)));
            result.Setters.Add(new Setter(DataGridColumnHeader.BorderThicknessProperty, new Thickness(0, 0, 1, 0)));
            result.Setters.Add(new Setter(DataGridColumnHeader.BorderBrushProperty, BorderBrush));
            result.Setters.Add(new Setter(DataGridColumnHeader.HeightProperty, 24.0));
            DGColumnHeaderStyle = result;
        }

        protected void UpdateToggleStyle() {
            var result = new Style(typeof(Controls.Toggle));
            result.Setters.Add(new Setter(Controls.Toggle.BackgroundProperty, ToggleBackgroundBrush));
            result.Setters.Add(new Setter(Controls.Toggle.ForegroundProperty, ToggleForegroundBrush));
            result.Setters.Add(new Setter(Controls.Toggle.ToggleOffBackgroundProperty, ToggleOffBackgroundBrush));
            result.Setters.Add(new Setter(Controls.Toggle.ToggleOffForegroundProperty, ToggleOffForegroundBrush));
            result.Setters.Add(new Setter(Controls.Toggle.ToggleSizeProperty, ToggleSize));
            result.Setters.Add(new Setter(Controls.Toggle.WidthProperty, 70.0));
            result.Setters.Add(new Setter(Controls.Toggle.HeightProperty, 25.0));
            result.Setters.Add(new Setter(Controls.Toggle.VerticalAlignmentProperty, VerticalAlignment.Center));
            result.Setters.Add(new Setter(Controls.Toggle.MarginProperty, new Thickness(0, 0, 5, 0)));
            DGColumnHeaderStyle = result;
        }

        protected void SetDefaults() {
            WindowTextBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            WindowBrush = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            ActiveCaptionBrush = new SolidColorBrush(Color.FromArgb(255, 0, 0, 220));
            ActiveCaptionTextBrush = new SolidColorBrush(Color.FromArgb(255, 200, 200, 200));
            ControlBorderBrush = new SolidColorBrush(Color.FromArgb(255, 125, 125, 125));
            CloseBackgroundBrush = new SolidColorBrush(Color.FromArgb(255, 220, 75, 75));
            ConsoleForegroundBrush = new SolidColorBrush(Color.FromArgb(255, 200, 200, 200));
            ConsoleBackgroundBrush = System.Windows.Media.Brushes.Black;
            CloseForegroundBrush = System.Windows.Media.Brushes.WhiteSmoke;
            DataGridAlternatingRowBackgroundBrush = WindowBrush;
            FontSize = 14.0;
            TitlebarFontSize = 14.0;
        }

    }
}
