using Common.Applicationn;
using Common.Applicationn.Linq;
using Common.Applicationn.Logging;
using Common.Applicationn.Primitives;
using Common.MVVMFramework;
using CongregationExtension.ViewModels;
using CongregationManager;
using CongregationManager.Data;
using CongregationManager.Extensibility;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace CongregationExtension {
    public class Extension : ExtensionBase {
        public Extension()
            : base("Congregations", '') { }

        public override event RetrieveResourcesHandler RetrieveResources;

        private ExtensionControlViewModel extControlView = default;

        public override void Initialize(string dataDirectory,
            string tempDirectory, Settings appSettings, ApplicationLogger logger,
            DataManager dataManager) {

            App.logger = logger;
            App.AppSettings = appSettings;
            App.DataManager = dataManager;
            App.LogMessage($"Initializing the {Name} extension", ApplicationLogger.EntryTypes.Information);

            var control = new ExtensionControlView();
            extControlView = control.View;
            extControlView.Congregations.AddRange(dataManager.Congregations.OrderBy(x => x.Name));
            dataManager.Congregations.CollectionChanged += Congregations_CollectionChanged;

            DataDirectory = dataDirectory;
            TempDirectory = tempDirectory;
            Settings = appSettings;
            Logger = logger;
            DataManager = dataManager;

            LoadInterfaceItems();

            Panel = new ExtensionPanel(Name, Glyph, control);

            var e = new RetrieveResourcesEventArgs();
            RetrieveResources?.Invoke(this, e);
            if (e.Dictionary != null) {
                control.Resources = e.Dictionary;
            }
            IsEnabled = Settings.GetValue($"{Name} Extension", "IsEnabled", true);
        }

        private void Congregations_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            if (e.NewItems != null) {
                foreach (Congregation item in e.NewItems) {
                    if (!extControlView.Congregations.Any(x => x.ID == item.ID)) {
                        App.LogMessage($"Congregation {item.Name} added", ApplicationLogger.EntryTypes.Information);
                        extControlView.Congregations.Add(item);
                    }
                }
            }
            else if (e.OldItems != null) {

            }
        }

        private void LoadInterfaceItems() {

            //------------------ menu -------------------
            Logger.LogMessage(new StringBuilder("  Adding Congregations menu"), ApplicationLogger.EntryTypes.Information);
            var topMenuItem = AddTopLevelMenuItem("_Congregations");

            AddMenuItem("Add Congregation", topMenuItem, "people", AddCongregationCommand);
            AddMenuItem("Delete Congregation", topMenuItem, "trash-can-wf", DeleteCongregationCommand);

            AddMenuSeparator(topMenuItem);

            AddMenuItem("Add Member", topMenuItem, "business-man-01-wf", AddMemberCommand);
            AddMenuItem("Add Group", topMenuItem, "user-group", null);

            // ------------------ Toolbar ------------------
            Logger.LogMessage(new StringBuilder("  Adding toolbar items"), ApplicationLogger.EntryTypes.Information);
            AddToolbarSeparator();

            AddToolbarLabel("Congregation");
            AddToolbarButton("Add Congregation", "people", AddCongregationCommand);
            AddToolbarButton("Delete Congregation", "trash-can-wf", DeleteCongregationCommand);

            AddToolbarSeparator();

            AddToolbarButton("Add Member", "business-man-01-wf", AddMemberCommand);
            AddToolbarButton("Add Group", "user-group", null);

        }

        public override void Destroy() {
            App.LogMessage($"Destroying the {Name} extension", ApplicationLogger.EntryTypes.Information);
            if (addedControls != null) {
                if (addedControls.ContainsKey(this)) {
                    foreach (var item in addedControls[this]) {
                        if (item.Is<MenuItem>()) {
                            if (item.As<MenuItem>().Parent.Is<MenuItem>()) {
                                item.As<MenuItem>().Parent.As<MenuItem>().Items.Remove(item);
                            }
                            else if (item.As<MenuItem>().Parent.Is<Menu>()) {
                                Menu.Items.Remove(item);
                            }
                        }
                        else if (item.Is<Button>() || item.Is<TextBlock>()) {
                            Toolbar.Items.Remove(item);
                        }
                        else if (item.Is<Separator>()) {
                            if (Toolbar.Items.Contains(item)) {
                                Toolbar.Items.Remove(item);
                            }
                            else if (Menu.Items.Contains(item)) {
                                Menu.Items.Remove(item);
                            }
                            else if (item.As<Separator>().Parent.Is<MenuItem>()) {
                                item.As<Separator>().Parent.As<MenuItem>().Items.Remove(item);
                            }
                        }
                    }
                    addedControls[this].Clear();
                }
            }
        }

        public override void ToggleLoadedControls(bool value) {
            if (addedControls != null && addedControls.Any()) {
                foreach (var item in addedControls) {
                    if (item.GetType().GetProperty("IsEnabled") != null) {
                        item.GetType().GetProperty("IsEnabled")?.SetValue(item, value);
                    }
                }
            }
            if (Panel != null && Panel.Control != null) {
                Panel.Control.IsEnabled = value;
            }
        }

        public override void Save() {
            var data = new object();
            //SaveExtensionData?.Invoke(this, new SaveExtensionDataEventArgs(data));
        }

        #region AddCongregationCommand
        private DelegateCommand _AddCongregationCommand = default;
        /// <summary>Gets the AddCongregation command.</summary>
        /// <value>The AddCongregation command.</value>
        public DelegateCommand AddCongregationCommand => _AddCongregationCommand ?? (_AddCongregationCommand = new DelegateCommand(AddCongregation, ValidateAddCongregationState));
        private bool ValidateAddCongregationState(object state) => true;
        private void AddCongregation(object state) {
            if (congWindow != null) {
                congWindow = null;
            }
            congWindow = new CongregationWindow {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            congWindow.View.Congregation = new Congregation {
                IsNew = true,
                MeetingDay = System.DayOfWeek.Sunday,
                MeetingTime = new System.TimeSpan(10, 0, 0)
            };
            congWindow.ShowDialog();
        }
        #endregion

        private CongregationWindow congWindow = default;

        #region DeleteCongregationCommand
        private DelegateCommand _DeleteCongregationCommand = default;
        /// <summary>Gets the DeleteCongregation command.</summary>
        /// <value>The DeleteCongregation command.</value>
        public DelegateCommand DeleteCongregationCommand => _DeleteCongregationCommand ?? (_DeleteCongregationCommand = new DelegateCommand(DeleteCongregation, ValidateDeleteCongregationState));
        private bool ValidateDeleteCongregationState(object state) => true;
        private void DeleteCongregation(object state) {
            
        }
        #endregion

        #region AddMemberCommand
        private DelegateCommand _AddMemberCommand = default;
        /// <summary>Gets the AddMember command.</summary>
        /// <value>The AddMember command.</value>
        public DelegateCommand AddMemberCommand => _AddMemberCommand ?? (_AddMemberCommand = new DelegateCommand(AddMember, ValidateAddMemberState));
        private bool ValidateAddMemberState(object state) => true;
        private void AddMember(object state) {            
            App.AddNewMember(extControlView.SelectedCongregation);
        }
        #endregion
    }
}
