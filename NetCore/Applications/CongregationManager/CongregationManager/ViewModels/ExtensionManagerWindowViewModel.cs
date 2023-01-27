using Common.MVVMFramework;
using CongregationManager.Extensibility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongregationManager.ViewModels {
    public class ExtensionManagerWindowViewModel : ViewModelBase {
        public ExtensionManagerWindowViewModel() {
            Title = "Extension Manager [designer]";
        }

        public override void Initialize() {
            base.Initialize();

            Title = "Extension Manager";

            Extensions = new ObservableCollection<ExtensionBase>(ApplicationData.Extensions);
        }

        public enum Actions {
            CloseWindow,
            AddNewExtension,
            DeleteExtension
        }

        #region CloseWindowCommand
        private DelegateCommand _CloseWindowCommand = default;
        /// <summary>Gets the CloseWindow command.</summary>
        /// <value>The CloseWindow command.</value>
        public DelegateCommand CloseWindowCommand => _CloseWindowCommand ?? (_CloseWindowCommand = new DelegateCommand(CloseWindow, ValidateCloseWindowState));
        private bool ValidateCloseWindowState(object state) => true;
        private void CloseWindow(object state) {
            ExecuteAction(nameof(Actions.CloseWindow));
        }
        #endregion

        #region Extensions Property
        private ObservableCollection<ExtensionBase> _Extensions = default;
        /// <summary>Gets/sets the Extensions.</summary>
        /// <value>The Extensions.</value>
        public ObservableCollection<ExtensionBase> Extensions {
            get => _Extensions;
            set {
                _Extensions = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SelectedExtension Property
        private ExtensionBase _SelectedExtension = default;
        /// <summary>Gets/sets the SelectedExtension.</summary>
        /// <value>The SelectedExtension.</value>
        public ExtensionBase SelectedExtension {
            get => _SelectedExtension;
            set {
                _SelectedExtension = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region AddExtensionCommand
        private DelegateCommand _AddExtensionCommand = default;
        /// <summary>Gets the AddExtension command.</summary>
        /// <value>The AddExtension command.</value>
        public DelegateCommand AddExtensionCommand => _AddExtensionCommand ?? (_AddExtensionCommand = new DelegateCommand(AddExtension, ValidateAddExtensionState));
        private bool ValidateAddExtensionState(object state) => true;
        private void AddExtension(object state) {
            ExecuteAction(nameof(Actions.AddNewExtension));
        }
        #endregion

        #region DeleteExtensionCommand
        private DelegateCommand _DeleteExtensionCommand = default;
        /// <summary>Gets the DeleteExtension command.</summary>
        /// <value>The DeleteExtension command.</value>
        public DelegateCommand DeleteExtensionCommand => _DeleteExtensionCommand ?? (_DeleteExtensionCommand = new DelegateCommand(DeleteExtension, ValidateDeleteExtensionState));
        private bool ValidateDeleteExtensionState(object state) => SelectedExtension != null;
        private void DeleteExtension(object state) {
            ExecuteAction(nameof(Actions.DeleteExtension));
        }
        #endregion

    }
}
