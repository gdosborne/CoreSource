using Common.MVVMFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongregationManager.ViewModels {
    public class ThemeNameWindowViewModel : ViewModelBase {
        public ThemeNameWindowViewModel() {
            Title = "Theme Name [designer]";
        }

        public override void Initialize() {
            base.Initialize();

            Title = "Theme Name";
        }

        #region ThemeName Property
        private string _ThemeName = default;
        /// <summary>Gets/sets the ThemeName.</summary>
        /// <value>The ThemeName.</value>
        public string ThemeName {
            get => _ThemeName;
            set {
                _ThemeName = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region DialogResult Property
        private bool _DialogResult = default;
        /// <summary>Gets/sets the DialogResult.</summary>
        /// <value>The DialogResult.</value>
        public bool DialogResult {
            get => _DialogResult;
            set {
                _DialogResult = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public enum Actions {
            OK,
            Cancel
        }

        #region OKCommand
        private DelegateCommand _OKCommand = default;
        /// <summary>Gets the OK command.</summary>
        /// <value>The OK command.</value>
        public DelegateCommand OKCommand => _OKCommand ??= new DelegateCommand(OK, ValidateOKState);
        private bool ValidateOKState(object state) => !string.IsNullOrEmpty(ThemeName);
        private void OK(object state) {
            ExecuteAction(nameof(Actions.OK));
        }
        #endregion

        #region CancelCommand
        private DelegateCommand _CancelCommand = default;
        /// <summary>Gets the Cancel command.</summary>
        /// <value>The Cancel command.</value>
        public DelegateCommand CancelCommand => _CancelCommand ??= new DelegateCommand(Cancel, ValidateCancelState);
        private bool ValidateCancelState(object state) => true;
        private void Cancel(object state) {
            ExecuteAction(nameof(Actions.Cancel));
        }
        #endregion

    }
}
