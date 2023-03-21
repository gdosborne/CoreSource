using Common.MVVMFramework;
using CredentialManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeCompositeIcon {
    internal partial class OptionsWindowView : ViewModelBase {
        public OptionsWindowView() {
            Title = "Options [designer]";
        }

        public override void Initialize() {
            base.Initialize();

            Title = "Options";
            IsUseLastPositionChecked = App.ThisApp.IsUseLastPositionChecked;
        }

        #region DialogResult Property
        private bool? _DialogResult = default;
        /// <summary>Gets/sets the DialogResult.</summary>
        /// <value>The DialogResult.</value>
        public bool? DialogResult {
            get => _DialogResult;
            set {
                _DialogResult = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsUseLastPositionChecked Property
        private bool _IsUseLastPositionChecked = default;
        /// <summary>Gets/sets the IsUseLastPositionChecked.</summary>
        /// <value>The IsUseLastPositionChecked.</value>
        public bool IsUseLastPositionChecked {
            get => _IsUseLastPositionChecked;
            set {
                _IsUseLastPositionChecked = value;
                App.ThisApp.IsUseLastPositionChecked = IsUseLastPositionChecked;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
