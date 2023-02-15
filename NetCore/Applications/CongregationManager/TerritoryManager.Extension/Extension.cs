using Common.Applicationn;
using Common.Applicationn.Logging;
using Common.Applicationn.Primitives;
using Common.Applicationn.Windows;
using CongregationManager.Data;
using CongregationManager.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TerritoryManager.Extension.ViewModels;

namespace TerritoryManager.Extension {
    public class Extension : ExtensionBase {
        public Extension()
            : base("Territories", '', "TabIcon") { }
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

        public override event RetrieveResourcesHandler RetrieveResources;

        private ExtensionControlViewModel extControlView = default;

        public override void Initialize(string dataDirectory, string tempDirectory, Settings appSettings, ApplicationLogger logger, DataManager dataManager) {
            App.logger = logger;
            App.AppSettings = appSettings;
            App.DataManager = dataManager;
            App.LogMessage($"Initializing the {Name} extension", ApplicationLogger.EntryTypes.Information);
            App.DataManager.ChangeNotification += DataManager_ChangeNotification;

            var control = new ExtensionControl();
            extControlView = control.View;
            //extControlView.Congregations.AddRange(dataManager.Congregations.OrderBy(x => x.Name));
            //dataManager.Congregations.CollectionChanged += Congregations_CollectionChanged;

            DataDirectory = dataDirectory;
            TempDirectory = tempDirectory;
            Settings = appSettings;
            Logger = logger;
            DataManager = dataManager;

            //LoadInterfaceItems();

            control.RemoveChild(control.MainGrid);
            Panel = new ExtensionPanel(Name, Glyph, control.MainGrid);

            var e = new RetrieveResourcesEventArgs();
            RetrieveResources?.Invoke(this, e);
            if (e.Dictionary != null) {
                control.MainGrid.Resources = e.Dictionary;
                control.Resources = e.Dictionary;
            }
            IsEnabled = Settings.GetValue($"{Name} Extension", "IsEnabled", true);
        }

        private void DataManager_ChangeNotification(object sender, ChangeNotificationEventArgs e) {
           
        }

        public override void Save() {
            //throw new NotImplementedException();
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
    }
}
