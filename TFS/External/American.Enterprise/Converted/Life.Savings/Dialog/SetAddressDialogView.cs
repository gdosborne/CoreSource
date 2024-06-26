using System.ComponentModel;
using GregOsborne.MVVMFramework;

namespace GregOsborne.Dialog {
    public class SetAddressDialogView : INotifyPropertyChanged {
        private string _address;
        private DelegateCommand _cancelCommand;
        private bool? _dialogResult;
        private DelegateCommand _okCommand;

        public string Address {
            get => _address;
            set {
                _address = value;
                UpdateInterface();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Address"));
            }
        }

        public DelegateCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new DelegateCommand(Cancel, ValidateCancelState));

        public bool? DialogResult {
            get => _dialogResult;
            set {
                _dialogResult = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DialogResult"));
            }
        }

        public DelegateCommand OkCommand => _okCommand ?? (_okCommand = new DelegateCommand(Ok, ValidateOkState));
        public event PropertyChangedEventHandler PropertyChanged;

        public void UpdateInterface() {
            OkCommand.RaiseCanExecuteChanged();
        }

        private void Cancel(object state) {
            DialogResult = false;
        }

        private void Ok(object state) {
            DialogResult = true;
        }

        private static bool ValidateCancelState(object state) {
            return true;
        }

        private bool ValidateOkState(object state) {
            return !string.IsNullOrEmpty(Address);
        }
    }
}