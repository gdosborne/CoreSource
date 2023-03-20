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
    }
}
