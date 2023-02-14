using Common.Applicationn.Primitives;
using Common.MVVMFramework;
using CongregationManager.Data;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CongregationExtension.ViewModels {
    public class RecycleBinWindowViewModel : LocalBase {
        public RecycleBinWindowViewModel() {
            Title = "Recycle Bin [design]";
            RecycleGroups = new ObservableCollection<RecycleGroup>();
        }

        public override void Initialize() {
            base.Initialize();

            Title = "Recycle Bin";
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

        public void AddGroup(RecycleGroup group) {
            group.PropertyChanged += Group_PropertyChanged;
            RecycleGroups.Add(group);
        }

        private void Group_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName == "SelectedItem") {
                var group = sender.As<RecycleGroup>();
                SelectedItem = group.SelectedItem;
            }
        }

        private ObservableCollection<RecycleGroup> _RecycleGroups = default;
        public ObservableCollection<RecycleGroup> RecycleGroups {
            get => _RecycleGroups;
            set {
                _RecycleGroups = value;
                OnPropertyChanged();
            }
        }

        #region SelectedItem Property
        private RecycleItem _SelectedItem = default;
        /// <summary>Gets/sets the SelectedItem.</summary>
        /// <value>The SelectedItem.</value>
        public RecycleItem SelectedItem {
            get => _SelectedItem;
            set {
                _SelectedItem = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region RecycleItemCommand
        private DelegateCommand _RecycleItemCommand = default;
        /// <summary>Gets the RecycleItem command.</summary>
        /// <value>The RecycleItem command.</value>
        public DelegateCommand RecycleItemCommand => _RecycleItemCommand ?? (_RecycleItemCommand = new DelegateCommand(RecycleTheItem, ValidateRecycleItemState));
        private bool ValidateRecycleItemState(object state) => SelectedItem != null;
        private void RecycleTheItem(object state) {
            var group = RecycleGroups.FirstOrDefault(x => x.Items.Contains(SelectedItem));

            var restoreFileName = Path.Combine(App.DataManager.DataFolder, group.Name);
            var existingMessage = string.Empty;
            var width = 250;
            if (File.Exists(restoreFileName)) {
                existingMessage = $"This name is currently in use for a congregation. If you continue, all data " +
                    $"in the existing congregation will be replaced.";
                width = 275;
            }

            var msg = $"This will restore the recycle bin item for {group.Name}, recyled on {SelectedItem.RecycleDateTime}." +
                $"{existingMessage}\n\nAre you sure you want to do this?";
            var title = $"Restore recycle bin item";
            if (App.IsYesInDialogSelected(title, msg, title, Ookii.Dialogs.Wpf.TaskDialogIcon.Shield, width)) {
                var file = new FileInfo(SelectedItem.RecycleFileName);
                if (file.Exists) {
                    var dir = file.Directory;
                    
                    file.MoveTo(restoreFileName, true);
                    group.Items.Remove(SelectedItem);
                    if (group.Items.Count == 0) {
                        RecycleGroups.Remove(group);
                        dir.Delete();
                    }
                    SelectedItem = null;
                    App.logger.LogMessage(new StringBuilder($"Restored recycle bin object {group.Name}"), Common.Applicationn.Logging.ApplicationLogger.EntryTypes.Information);
                }
            }
        }
        #endregion

        #region ClearItemCommand
        private DelegateCommand _ClearItemCommand = default;
        /// <summary>Gets the ClearItem command.</summary>
        /// <value>The ClearItem command.</value>
        public DelegateCommand ClearItemCommand => _ClearItemCommand ?? (_ClearItemCommand = new DelegateCommand(ClearItem, ValidateClearItemState));
        private bool ValidateClearItemState(object state) => SelectedItem != null;
        private void ClearItem(object state) {
            var group = RecycleGroups.FirstOrDefault(x => x.Items.Contains(SelectedItem));
            var msg = $"This will delete the recycle bin item for {group.Name}, recyled on {SelectedItem.RecycleDateTime}." +
                $"\n\nAre you sure you want to do this?";
            var title = $"Delete recycle bin item";
            if (App.IsYesInDialogSelected(title, msg, title, Ookii.Dialogs.Wpf.TaskDialogIcon.Shield)) {
                var file = new FileInfo(SelectedItem.RecycleFileName);
                if (file.Exists) {
                    file.Delete();
                    var dir = file.Directory;
                    group.Items.Remove(SelectedItem);
                    if (group.Items.Count == 0) {
                        RecycleGroups.Remove(group);
                        dir.Delete();
                    }
                    App.logger.LogMessage(new StringBuilder($"Deleted recycle bin object {group.Name}"), Common.Applicationn.Logging.ApplicationLogger.EntryTypes.Information);
                    SelectedItem = null;
                }
            }
        }
        #endregion


    }
}
