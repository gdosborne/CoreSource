using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageVersioning {
    public partial class SettingsWindowView : ViewModelBase {
        public SettingsWindowView() {
            Title = "Settings [designer]";
        }

        public override void Initialize() {
            base.Initialize();
            Title = "Settings";
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

    }
}
