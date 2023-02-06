using Common.MVVMFramework;
using CongregationManager.Extensibility;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace CongregationExtension {
    public class ExtensionPanel : IExtensionPanel, INotifyPropertyChanged {
        public ExtensionPanel(string title, char glyph, UserControl control) {
            Title = title;
            Glyph = glyph;
            Control = control;
        }

        #region Glyph Property
        private char _Glyph = default;
        /// <summary>Gets/sets the Glyph.</summary>
        /// <value>The Glyph.</value>
        public char Glyph {
            get => _Glyph;
            set {
                _Glyph = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Title Property
        private string _Title = default;
        /// <summary>Gets/sets the Extension Title.</summary>
        /// <value>The ExtensionName.</value>
        public string Title {
            get => _Title;
            set {
                _Title = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Control Property
        private UserControl _Control = default;
        /// <summary>Gets/sets the Control.</summary>
        /// <value>The Control.</value>
        public UserControl Control {
            get => _Control;
            set {
                _Control = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SaveCommand
        private ICommand _SaveCommand = default;
        public ICommand SaveCommand => _SaveCommand ?? (_SaveCommand = new DelegateCommand(Save, ValidateSaveState));
        private bool ValidateSaveState(object state) => true;
        private void Save(object state) {

        }
        #endregion

        #region RevertCommand
        private ICommand _RevertCommand = default;
        public ICommand RevertCommand => _RevertCommand ?? (_RevertCommand = new DelegateCommand(Revert, ValidateRevertState));
        private bool ValidateRevertState(object state) => true;
        private void Revert(object state) {

        }
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = default) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
