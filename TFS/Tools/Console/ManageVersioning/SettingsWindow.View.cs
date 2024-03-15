using ManageVersioning.SharedViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ManageVersioning {
    public partial class SettingsWindowView : SharedView {
        public SettingsWindowView() {
            Title = "Settings [designer]";
            ToggleBackgroundBrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            ToggleForegroundBrush = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            ToggleOffBackgroundBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            ToggleOffForegroundBrush = new SolidColorBrush(Color.FromArgb(255, 128, 128, 128));
            ToggleSize = 35;
            

            UpdateToggleStyle();
        }

        public void Initialize(Window window) {
            base.Initialize();
            this.window = window;

            Title = "Settings";
            UpdateToggleStyle();
        }

        #region DialogResult Property
        private bool? _DialogResult = default;
        public bool? DialogResult {
            get => _DialogResult;
            set {
                _DialogResult = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region AreWindowPositionsSaved Property
        private bool _AreWindowPositionsSaved = default;
        public bool AreWindowPositionsSaved {
            get => _AreWindowPositionsSaved;
            set {
                _AreWindowPositionsSaved = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsConsoleEditable Property
        private bool _IsConsoleEditable = default;
        public bool IsConsoleEditable {
            get => _IsConsoleEditable;
            set {
                _IsConsoleEditable = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsConsoleBackgroundBrushUsed Property
        private bool _IsConsoleBackgroundBrushUsed = default;
        public bool IsConsoleBackgroundBrushUsed {
            get => _IsConsoleBackgroundBrushUsed;
            set {
                _IsConsoleBackgroundBrushUsed = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ConsoleBrushFilePath Property
        private string _ConsoleBrushFilePath = default;
        public string ConsoleBrushFilePath {
            get => _ConsoleBrushFilePath;
            set {
                _ConsoleBrushFilePath = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ConsoleImageForegroundColor Property
        private System.Windows.Media.Brush _ConsoleImageForegroundColor = default;
        public System.Windows.Media.Brush ConsoleImageForegroundColor {
            get => _ConsoleImageForegroundColor;
            set {
                _ConsoleImageForegroundColor = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ConsoleImageOpacity Property
        private int _ConsoleImageOpacity = default;
        public int ConsoleImageOpacity {
            get => _ConsoleImageOpacity;
            set {
                _ConsoleImageOpacity = value;
                OnPropertyChanged();
            }
        }
        #endregion

    }
}
