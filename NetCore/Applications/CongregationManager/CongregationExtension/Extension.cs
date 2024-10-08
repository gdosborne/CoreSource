using Common;
using Common.Linq;
using Common.Logging;
using Common.Primitives;
using Common.Windows;
using Common.MVVMFramework;
using CongregationExtension.ViewModels;
using CongregationManager.Data;
using CongregationManager.Extensibility;
using Ookii.Dialogs.Wpf;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using static Common.Dialogs.Helpers;

namespace CongregationExtension {
    public class Extension : ExtensionBase {
        public Extension()
            : base("Congregations", '', "TabIconX") { }

        public override event RetrieveResourcesHandler RetrieveResources;

        private ExtensionControlViewModel extControlView = default;

        public override void Initialize(string dataDirectory, string tempDirectory, AppSettings appSettings, ApplicationLogger logger, DataManager dataManager) {

            App.logger = logger;
            App.AppSettings = appSettings;
            App.DataManager = dataManager;
            App.LogMessage($"Initializing the {Name} extension", ApplicationLogger.EntryTypes.Information);
            App.DataManager.ChangeNotification += DataManager_ChangeNotification;

            //appSettings.SetupColors(Resources.GetBrushNames(), Resources);
            var fontFamily = new FontFamily(appSettings.GetValue("Application", "FontFamilyName", "Calibri"));
            Resources["StandardFont"] = fontFamily;
            App.Current.Resources["StandardFontSize"] = appSettings.GetValue("Application", "FontSize", 16.0);

            var control = new ExtensionControl();
            extControlView = control.View;
            extControlView.Congregations.AddRange(dataManager.Congregations.OrderBy(x => x.Name));
            dataManager.Congregations.CollectionChanged += Congregations_CollectionChanged;

            DataDirectory = dataDirectory;
            TempDirectory = tempDirectory;
            Settings = appSettings;
            Logger = logger;
            DataManager = dataManager;

            LoadInterfaceItems();

            control.RemoveChild(control.MainGrid);
            Panel = new ExtensionPanel(Name, Glyph, control.MainGrid);

            var e = new RetrieveResourcesEventArgs();
            RetrieveResources?.Invoke(this, e);
            if (e.Dictionary != null) {
                control.Resources = e.Dictionary;
            }
            IsEnabled = Settings.GetValue($"{Name} Extension", "IsEnabled", true);
        }

        private void DataManager_ChangeNotification(object sender, ChangeNotificationEventArgs e) {
            switch (e.ModType) {
                case ModificationTypes.Added: {
                        if (e.Item.Is<Congregation>()) {
                            sender.As<DataManager>().Congregations.Add(e.Item.As<Congregation>());
                            e.Item.As<Congregation>().PropertyChanged += Extension_PropertyChanged;
                            e.Item.As<Congregation>().EditThisItem += Extension_EditThisItem;
                            extControlView.Refresh();
                        }
                        break;
                    }
                case ModificationTypes.Modified: {
                        break;
                    }
                case ModificationTypes.Deleted: {
                        break;
                    }
                default:
                    break;
            }
        }

        private void Extension_EditThisItem(object? sender, System.EventArgs e) {
            extControlView.Item_EditThisItem(sender, e); ;
        }

        private void Extension_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
            extControlView.Item_PropertyChanged(sender, e);
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

            var fontFamResName = "PeopleFontFamily";

            AddMenuItem("Add Congregation", topMenuItem, "people", AddCongregationCommand, fontFamResName);
            AddMenuItem("Delete Congregation", topMenuItem, "trash-can-wf", DeleteCongregationCommand, fontFamResName);

            AddMenuSeparator(topMenuItem);

            AddMenuItem("Add Member", topMenuItem, "business-man-01-wf", AddMemberCommand, fontFamResName);
            AddMenuItem("Add Group", topMenuItem, "user-group", AddGroupCommand, fontFamResName);

            AddMenuSeparator(topMenuItem);
            AddMenuItem("Recycle Bin", topMenuItem, "recycle-bin", RecycleBinCommand, fontFamResName);

            // ------------------ Toolbar ------------------
            Logger.LogMessage(new StringBuilder("  Adding toolbar items"), ApplicationLogger.EntryTypes.Information);
            AddToolbarSeparator();

            AddToolbarLabel("Congregation");
            AddToolbarButton("Add Congregation", "people", AddCongregationCommand, fontFamResName);
            AddToolbarButton("Delete Congregation", "trash-can-wf", DeleteCongregationCommand, fontFamResName);

            AddToolbarSeparator();

            AddToolbarButton("Add Member", "business-man-01-wf", AddMemberCommand, fontFamResName);
            AddToolbarButton("Add Group", "user-group", AddGroupCommand, fontFamResName);

        }

        public override void Destroy() {
            App.LogMessage($"Destroying the {Name} extension", ApplicationLogger.EntryTypes.Information);
            if (AddedControls != null) {
                if (AddedControls.ContainsKey(this)) {
                    foreach (var item in AddedControls[this]) {
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
                    AddedControls[this].Clear();
                }
            }
        }

        public override void ToggleLoadedControls(bool value) {
            if (AddedControls != null && AddedControls.Any()) {
                foreach (var item in AddedControls) {
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
            //var data = new object();
            //SaveExtensionData?.Invoke(this, new SaveExtensionDataEventArgs(data));
        }

        #region AddGroupCommand
        private DelegateCommand _AddGroupCommand = default;
        /// <summary>Gets the AddGroup command.</summary>
        /// <value>The AddGroup command.</value>
        public DelegateCommand AddGroupCommand => _AddGroupCommand ?? (_AddGroupCommand = new DelegateCommand(AddGroup, ValidateAddGroupState));
        private bool ValidateAddGroupState(object state) => true;
        private void AddGroup(object state) {
            App.AddEditGroup(extControlView.SelectedCongregation, default);
        }
        #endregion

        #region AddCongregationCommand
        private DelegateCommand _AddCongregationCommand = default;
        /// <summary>Gets the AddCongregation command.</summary>
        /// <value>The AddCongregation command.</value>
        public DelegateCommand AddCongregationCommand => _AddCongregationCommand ?? (_AddCongregationCommand = new DelegateCommand(AddCongregation, ValidateAddCongregationState));
        private bool ValidateAddCongregationState(object state) => true;
        private void AddCongregation(object state) {
            App.AddCongregation();
        }
        #endregion

        #region RecycleBinCommand
        private DelegateCommand _RecycleBinCommand = default;
        /// <summary>Gets the RecycleBin command.</summary>
        /// <value>The RecycleBin command.</value>
        public DelegateCommand RecycleBinCommand => _RecycleBinCommand ?? (_RecycleBinCommand = new DelegateCommand(RecycleBin, ValidateRecycleBinState));
        private bool ValidateRecycleBinState(object state) => true;
        private void RecycleBin(object state) {
            App.ShowRecycleBin();
        }
        #endregion

        #region DeleteCongregationCommand
        private DelegateCommand _DeleteCongregationCommand = default;
        /// <summary>Gets the DeleteCongregation command.</summary>
        /// <value>The DeleteCongregation command.</value>
        public DelegateCommand DeleteCongregationCommand => _DeleteCongregationCommand ?? (_DeleteCongregationCommand = new DelegateCommand(DeleteCongregation, ValidateDeleteCongregationState));
        private bool ValidateDeleteCongregationState(object state) => true;
        private void DeleteCongregation(object state) {
            if (extControlView.SelectedCongregation.IsLocal) {
                var title = "Local congregation";
                var msg = "You cannot delete this congregation because it is your local congregation.";
                ShowOKDialog(title, msg, TaskDialogIcon.Information);
                return;
            }
            if (!extControlView.Congregations.Any(x => x.IsLocal)) {
                var title = "Local congregation";
                var msg = "Before you can remove a congregation, you must select your local congregation. " +
                    "This is to ensure your data is intact before deleting.";
                ShowOKDialog(title, msg, TaskDialogIcon.Information);
                return;
            }
            var result = App.DeleteCongregation(extControlView.SelectedCongregation);
            if (result) {
                extControlView.Congregations.Remove(extControlView.SelectedCongregation);
                extControlView.SelectedCongregation = extControlView.Congregations.First(x => x.IsLocal);
            }
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
