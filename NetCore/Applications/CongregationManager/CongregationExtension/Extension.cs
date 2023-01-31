using Common.Applicationn;
using Common.Applicationn.Logging;
using Common.Applicationn.Primitives;
using Common.MVVMFramework;
using CongregationManager;
using CongregationManager.Data;
using CongregationManager.Extensibility;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CongregationExtension {
    public class Extension : ExtensionBase {
        public Extension()
            : base("Congregations", "") { }

        public override event SaveExtensionDataHandler SaveExtensionData;
        public override event AddControlItemHandler AddControlItem;
        public override event RemoveControlItemHandler RemoveControlItem;
        public override event RetrieveResourcesHandler RetrieveResources;

        public override void Initialize(string dataDirectory,
            string tempDirectory, Settings appSettings, ApplicationLogger logger,
            DataManager dataManager) {

            var control = new ExtensionControl();
            control.View.Congregations = dataManager.Congregations;

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

        private List<object> addedControls { get; set; }

        private void LoadInterfaceItems() {
            addedControls = new List<object>();

            var e = new AddControlItemEventArgs(AddControlItemEventArgs.ControlTypes.TopLevelMenuItem,
                "_Congregations", null, null, null);
            AddControlItem?.Invoke(this, e);
            if (e.ManagableItem != null)
                addedControls?.Add(e.ManagableItem);

            var topMenuItem = e.ManagableItem.As<MenuItem>();
            e = new AddControlItemEventArgs(AddControlItemEventArgs.ControlTypes.MenuItem,
                "Add", AddCongregationCommand, topMenuItem, "");
            AddControlItem?.Invoke(this, e);
            if (e.ManagableItem != null)
                addedControls?.Add(e.ManagableItem);

            e = new AddControlItemEventArgs(AddControlItemEventArgs.ControlTypes.MenuItem,
                "Delete", DeleteCongregationCommand, topMenuItem, "");
            AddControlItem?.Invoke(this, e);
            if (e.ManagableItem != null)
                addedControls?.Add(e.ManagableItem);

            e = new AddControlItemEventArgs(AddControlItemEventArgs.ControlTypes.ToolbarSeparator);
            AddControlItem?.Invoke(this, e);
            if (e.ManagableItem != null)
                addedControls?.Add(e.ManagableItem);

            e = new AddControlItemEventArgs(AddControlItemEventArgs.ControlTypes.ToolbarLabel,
              "Congregation", null, null, null);
            AddControlItem?.Invoke(this, e);
            if (e.ManagableItem != null)
                addedControls?.Add(e.ManagableItem);

            e = new AddControlItemEventArgs(AddControlItemEventArgs.ControlTypes.ToolbarButton,
               "Add Congregation", AddCongregationCommand, null, "");
            AddControlItem?.Invoke(this, e);
            if (e.ManagableItem != null)
                addedControls?.Add(e.ManagableItem);

            e = new AddControlItemEventArgs(AddControlItemEventArgs.ControlTypes.ToolbarButton,
               "Delete Congregation", DeleteCongregationCommand, null, "");
            AddControlItem?.Invoke(this, e);
            if (e.ManagableItem != null)
                addedControls?.Add(e.ManagableItem);

        }

        public override void Destroy() {
            if (addedControls != null && addedControls.Any()) {
                RemoveControlItem?.Invoke(this, new RemoveControlItemEventArgs(addedControls.ToArray()));
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
            SaveExtensionData?.Invoke(this, new SaveExtensionDataEventArgs(data));
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
            congWindow.View.AppSettings = Settings;
            congWindow.View.DataManager = DataManager;
            congWindow.Show();
        }
        private CongregationWindow congWindow = default;
        #endregion

        #region DeleteCongregationCommand
        private DelegateCommand _DeleteCongregationCommand = default;
        /// <summary>Gets the DeleteCongregation command.</summary>
        /// <value>The DeleteCongregation command.</value>
        public DelegateCommand DeleteCongregationCommand => _DeleteCongregationCommand ?? (_DeleteCongregationCommand = new DelegateCommand(DeleteCongregation, ValidateDeleteCongregationState));
        private bool ValidateDeleteCongregationState(object state) => true;
        private void DeleteCongregation(object state) {

        }
        #endregion

    }
}
