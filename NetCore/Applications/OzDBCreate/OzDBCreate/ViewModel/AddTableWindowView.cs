using Common.MVVMFramework;
using CredentialManagement;
using OzDB.Management;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzDBCreate.ViewModel {
    public class AddTableWindowView : ViewModelBase {
        public AddTableWindowView() {
            Title = "Add Table [designer]";
        }

        public override void Initialize() {
            base.Initialize();

            Title = "Add Table";
        }

        #region DialogResult Property
        private bool _DialogResult = default;
        public bool DialogResult {
            get => _DialogResult;
            set {
                _DialogResult = value;
                InvokePropertyChanged();
            }
        }
        #endregion


        #region Name Property
        private string _Name = default;
        public string Name {
            get => _Name;
            set {
                _Name = value;
                InvokePropertyChanged();
            }
        }
        #endregion

        #region Description Property
        private string _Description = default;
        public string Description {
            get => _Description;
            set {
                _Description = value;
                InvokePropertyChanged();
            }
        }
        #endregion

        #region Fields Property
        private ObservableCollection<OzDBDataField> _Fields = default;
        public ObservableCollection<OzDBDataField> Fields {
            get => _Fields;
            set {
                _Fields = value;
                InvokePropertyChanged();
            }
        }
        #endregion


        #region IsHidden Property
        private bool _IsHidden = default;
        public bool IsHidden {
            get => _IsHidden;
            set {
                _IsHidden = value;
                InvokePropertyChanged();
            }
        }
        #endregion

        #region OK Command
        private DelegateCommand _OKCommand = default;
        public DelegateCommand OKCommand => _OKCommand ??= new DelegateCommand(OK, ValidateOKState);
        private bool ValidateOKState(object state) => true;
        private void OK(object state) {
            DialogResult = true;
        }
        #endregion

        #region Cancel Command
        private DelegateCommand _CancelCommand = default;
        public DelegateCommand CancelCommand => _CancelCommand ??= new DelegateCommand(Cancel, ValidateCancelState);
        private bool ValidateCancelState(object state) => true;
        private void Cancel(object state) {
            DialogResult = false;
        }
        #endregion
    }
}
