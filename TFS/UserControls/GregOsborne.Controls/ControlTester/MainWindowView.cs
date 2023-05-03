
using System;
using System.ComponentModel.Design;
using GregOsborne.MVVMFramework;
using System.Reflection;
using System.Windows;

namespace ControlTester {
    public class MainWindowView : ViewModelBase {
        private DelegateCommand _TitlebarCommand = null;
        public DelegateCommand TitlebarCommand => _TitlebarCommand ?? (_TitlebarCommand = new DelegateCommand(Titlebar, ValidateTitlebarState));
        private void Titlebar(object state) {
            switch (state) {
                case "Close":
                    Environment.Exit(0);
                    break;
                case "Minimize":
                    WindowState = WindowState == WindowState.Minimized ? WindowState.Normal : WindowState.Minimized;
                    break;
                case "Maximize":
                    WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
                    break;
            }
        }
        private bool ValidateTitlebarState(object state) {
            return true;
        }
        private WindowState _WindowState;
        public WindowState WindowState {
            get => _WindowState;
            set {
                _WindowState = value;
                InvokePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
            }
        }
    }
}
