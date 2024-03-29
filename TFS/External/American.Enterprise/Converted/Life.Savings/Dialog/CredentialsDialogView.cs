using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Windows;
using GregOsborne.MVVMFramework;

namespace GregOsborne.Dialog {
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class CredentialsDialogView : INotifyPropertyChanged {
        private DelegateCommand _cancelCommand;
        private NetworkCredential _credentials;
        private bool? _dialogResult;
        private string _instructions;
        private bool _isSaveChecked;
        private DelegateCommand _okCommand;
        private string _password;
        private Visibility _saveVisibility;
        private string _userName;

        public CredentialsDialogView() {
            Instructions = "Application Name";
            SaveVisibility = Visibility.Visible;
            IsSaveChecked = false;
        }

        public bool? DialogResult {
            get => _dialogResult;
            set {
                _dialogResult = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DialogResult"));
            }
        }

        public DelegateCommand OkCommand => _okCommand ?? (_okCommand = new DelegateCommand(Ok, ValidateOkState));

        public DelegateCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new DelegateCommand(Cancel, ValidateCancelState));

        public NetworkCredential Credentials {
            get => _credentials;
            set {
                _credentials = value;
                if (value != null) {
                    Password = value.Password;
                    UserName = value.UserName;
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Credentials"));
            }
        }

        public string Instructions {
            get => _instructions;
            set {
                _instructions = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Instructions"));
            }
        }

        public Visibility SaveVisibility {
            get => _saveVisibility;
            set {
                _saveVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SaveVisibility"));
            }
        }

        public string Password {
            get => _password;
            set {
                _password = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Password"));
            }
        }

        public string UserName {
            get => _userName;
            set {
                _userName = value;
                UpdateInterface();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UserName"));
            }
        }

        public bool IsSaveChecked {
            get => _isSaveChecked;
            set {
                _isSaveChecked = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsSaveChecked"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void UpdateInterface() {
            OkCommand.RaiseCanExecuteChanged();
        }

        public void InitView() {
        }

        private void Ok(object state) {
            DialogResult = true;
        }

        private bool ValidateOkState(object state) {
            return !string.IsNullOrEmpty(UserName);
        }

        private void Cancel(object state) {
            DialogResult = false;
        }

        private static bool ValidateCancelState(object state) {
            return true;
        }
    }
}