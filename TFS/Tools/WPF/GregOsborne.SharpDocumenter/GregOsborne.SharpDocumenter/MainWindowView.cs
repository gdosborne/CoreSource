namespace GregOsborne.SharpDocumenter
{
    using GregOsborne.MVVMFramework;
    using System.Windows;

    public class MainWindowView : ViewModelBase
    {
        private bool _initializing = true;
        public MainWindowView()
        {
            WindowState = WindowState.Normal;
            _initializing = false;
        }
        private DelegateCommand _closeCommand;
        public DelegateCommand CloseCommand => _closeCommand ?? (_closeCommand = new DelegateCommand(Close, ValidateCloseState));
        private void Close(object state)
        {
            App.Current.Shutdown();
        }
        private bool ValidateCloseState(object state) => true;
        private DelegateCommand _minimizeCommand;
        public DelegateCommand MinimizeCommand => _minimizeCommand ?? (_minimizeCommand = new DelegateCommand(Minimize, ValidateMinimizeState));
        private void Minimize(object state)
        {
            WindowState = WindowState.Minimized;
        }
        private bool ValidateMinimizeState(object state) => true;
        private DelegateCommand _maximizeRestoreCommand;
        public DelegateCommand MaximizeRestoreCommand => _maximizeRestoreCommand ?? (_maximizeRestoreCommand = new DelegateCommand(MaximizeRestore, ValidateMaximizeRestoreState));
        private void MaximizeRestore(object state)
        {
            if (_initializing)
                return;
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }
        private bool ValidateMaximizeRestoreState(object state) => true;

        private WindowState _windowState;
        public WindowState WindowState {
            get => _windowState;
            set {
                _windowState = value;
                InvokePropertyChanged(nameof(WindowState));
            }
        }
    }
}
