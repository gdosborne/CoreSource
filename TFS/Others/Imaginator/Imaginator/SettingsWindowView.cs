
namespace Imaginator.Views
{
    using MVVMFramework;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows;

    public class SettingsWindowView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event ExecuteUIActionHandler ExecuteUIAction;
        public void UpdateInterface()
        {
        }
        public void InitView()
        {
            DoubleClickActions = new ObservableCollection<string>
            {
                "Open default application",
                "Convert to all sizes"
            };
            SelectedDoubleClickAction = App.GetSetting<string>("App.Selected.Double.Click.Action", "Convert to all sizes");
        }
        public void Initialize(Window window)
        {
        }
        public void Persist(Window window)
        {
        }
        private ObservableCollection<string> _DoubleClickActions;
        public ObservableCollection<string> DoubleClickActions {
            get { return _DoubleClickActions; }
            set {
                _DoubleClickActions = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("DoubleClickActions"));
            }
        }
        private string _SelectedDoubleClickAction;
        public string SelectedDoubleClickAction {
            get { return _SelectedDoubleClickAction; }
            set {
                _SelectedDoubleClickAction = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedDoubleClickAction"));
            }
        }
        private DelegateCommand _OKCommand = null;
        public DelegateCommand OKCommand {
            get {
                if (_OKCommand == null)
                    _OKCommand = new DelegateCommand(OK, ValidateOKState);
                return _OKCommand as DelegateCommand;
            }
        }
        private void OK(object state)
        {
            App.SetSetting<string>("App.Selected.Double.Click.Action", SelectedDoubleClickAction == null ? "Convert to all sizes" : SelectedDoubleClickAction);
            DialogResult = true;
        }
        private bool ValidateOKState(object state)
        {
            return true;
        }
        private DelegateCommand _CancelCommand = null;
        public DelegateCommand CancelCommand {
            get {
                if (_CancelCommand == null)
                    _CancelCommand = new DelegateCommand(Cancel, ValidateCancelState);
                return _CancelCommand as DelegateCommand;
            }
        }
        private void Cancel(object state)
        {
            DialogResult = false;
        }
        private bool ValidateCancelState(object state)
        {
            return true;
        }
        private bool? _DialogResult;
        public bool? DialogResult {
            get { return _DialogResult; }
            set {
                _DialogResult = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("DialogResult"));
            }
        }
    }
}
