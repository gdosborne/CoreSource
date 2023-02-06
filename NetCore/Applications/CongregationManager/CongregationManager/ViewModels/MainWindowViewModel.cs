using Common.MVVMFramework;
using CongregationManager.Extensibility;
using System.Collections.ObjectModel;

namespace CongregationManager.ViewModels {
    public class MainWindowViewModel : ViewModelBase {
        public MainWindowViewModel() {
            Title = $"{App.ApplicationName} [design]";
        }

        public override void Initialize() {
            base.Initialize();

            Title = App.ApplicationName;
            Panels = new ObservableCollection<IExtensionPanel>();
            Panels.CollectionChanged += Panels_CollectionChanged;
        }

        private void Panels_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            ExecuteAction(nameof(Actions.PanelAdded));
        }

        public enum Actions {
            CloseWindow,
            Minimize,
            Maximize,
            ManageExtensions,
            PanelAdded,
            ViewLogs
        }

        #region ViewLogsCommand
        private DelegateCommand _ViewLogsCommand = default;
        /// <summary>Gets the ViewLogs command.</summary>
        /// <value>The ViewLogs command.</value>
        public DelegateCommand ViewLogsCommand => _ViewLogsCommand ?? (_ViewLogsCommand = new DelegateCommand(ViewLogs, ValidateViewLogsState));
        private bool ValidateViewLogsState(object state) => true;
        private void ViewLogs(object state) {
            ExecuteAction(nameof(Actions.ViewLogs));
        }
        #endregion

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

        #region MinimizeCommand
        private DelegateCommand _MinimizeCommand = default;
        /// <summary>Gets the Minimize command.</summary>
        /// <value>The Minimize command.</value>
        public DelegateCommand MinimizeCommand => _MinimizeCommand ?? (_MinimizeCommand = new DelegateCommand(Minimize, ValidateMinimizeState));
        private bool ValidateMinimizeState(object state) => true;
        private void Minimize(object state) {
            ExecuteAction(nameof(Actions.Minimize));
        }
        #endregion

        #region MaximizeCommand
        private DelegateCommand _MaximizeCommand = default;
        /// <summary>Gets the Maximize command.</summary>
        /// <value>The Maximize command.</value>
        public DelegateCommand MaximizeCommand => _MaximizeCommand ?? (_MaximizeCommand = new DelegateCommand(Maximize, ValidateMaximizeState));
        private bool ValidateMaximizeState(object state) => true;
        private void Maximize(object state) {
            ExecuteAction(nameof(Actions.Maximize));
        }
        #endregion

        #region ExtensionManagerCommand
        private DelegateCommand _ExtensionManagerCommand = default;
        /// <summary>Gets the ExtensionManager command.</summary>
        /// <value>The ExtensionManager command.</value>
        public DelegateCommand ExtensionManagerCommand => _ExtensionManagerCommand ?? (_ExtensionManagerCommand = new DelegateCommand(ExtensionManager, ValidateExtensionManagerState));
        private bool ValidateExtensionManagerState(object state) => true;
        private void ExtensionManager(object state) {
            ExecuteAction(nameof(Actions.ManageExtensions));
        }
        #endregion

        #region Panels Property
        private ObservableCollection<IExtensionPanel> _Panels = default;
        /// <summary>Gets/sets the Panels.</summary>
        /// <value>The Panels.</value>
        public ObservableCollection<IExtensionPanel> Panels {
            get => _Panels;
            set {
                _Panels = value;
                OnPropertyChanged();                
            }
        }
        #endregion

    }
}
