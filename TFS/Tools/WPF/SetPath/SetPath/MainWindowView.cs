using GregOsborne.Application;
using GregOsborne.Application.Primitives;
using GregOsborne.MVVMFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SetPath
{
    public delegate void PerformActionHandler(object sender, PerformActionEventArgs e);
    public class PerformActionEventArgs : EventArgs
    {
        public enum Actions { Delete, MoveUp, MoveDown }
        public PerformActionEventArgs(Actions action)
        {
            Action = action;
        }
        public Actions Action { get; private set; }
    }
    public class PathItem : INotifyPropertyChanged
    {
        public event PerformActionHandler PerformAction;

        private int _Position;
        public int Position {
            get { return _Position; }
            set {
                _Position = value;
                InvokePropertyChanged(nameof(Position));
            }
        }

        private string _Value;
        public string Value {
            get { return _Value; }
            set {
                _Value = value;
                InvokePropertyChanged(nameof(Value));
            }
        }
        public override string ToString() => $"{Position},{Value}";
        private DelegateCommand _DeleteCommand;
        public DelegateCommand DeleteCommand => _DeleteCommand ?? (_DeleteCommand = new DelegateCommand(Delete, ValidateDeleteState));
        private void Delete(object state)
        {
            PerformAction?.Invoke(this, new PerformActionEventArgs(PerformActionEventArgs.Actions.Delete));
        }
        private bool ValidateDeleteState(object state) => true;
        private DelegateCommand _MoveUpCommand;
        public DelegateCommand MoveUpCommand => _MoveUpCommand ?? (_MoveUpCommand = new DelegateCommand(MoveUp, ValidateMoveUpState));
        private void MoveUp(object state)
        {
            PerformAction?.Invoke(this, new PerformActionEventArgs(PerformActionEventArgs.Actions.MoveUp));
        }
        private bool ValidateMoveUpState(object state) => true;
        private DelegateCommand _MoveDownCommand;
        public DelegateCommand MoveDownCommand => _MoveDownCommand ?? (_MoveDownCommand = new DelegateCommand(MoveDown, ValidateMoveDownState));
        private void MoveDown(object state)
        {
            PerformAction?.Invoke(this, new PerformActionEventArgs(PerformActionEventArgs.Actions.MoveDown));
        }
        private bool ValidateMoveDownState(object state) => true;
        private bool _IsSelected;
        public bool IsSelected {
            get { return _IsSelected; }
            set {
                _IsSelected = value;
                InvokePropertyChanged(nameof(IsSelected));
            }
        }

        private bool _IsDeleteEnabled;
        public bool IsDeleteEnabled {
            get { return _IsDeleteEnabled; }
            set {
                _IsDeleteEnabled = value;
                InvokePropertyChanged(nameof(IsDeleteEnabled));
            }
        }

        private bool _IsMoveUpEnabled;
        public bool IsMoveUpEnabled {
            get { return _IsMoveUpEnabled; }
            set {
                _IsMoveUpEnabled = value;
                InvokePropertyChanged(nameof(IsMoveUpEnabled));
            }
        }

        private bool _IsMoveDownEnabled;
        public bool IsMoveDownEnabled {
            get { return _IsMoveDownEnabled; }
            set {
                _IsMoveDownEnabled = value;
                InvokePropertyChanged(nameof(IsMoveDownEnabled));
            }
        }
        private void InvokePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    public class MainWindowView : ViewModelBase
    {
        public MainWindowView()
        {
            if (Settings.GetSetting(App.ApplicationName, "Settings", "PathTarget", System.EnvironmentVariableTarget.Machine) == EnvironmentVariableTarget.Machine)
                MachineIsChecked = true;
            else if (Settings.GetSetting(App.ApplicationName, "Settings", "PathTarget", System.EnvironmentVariableTarget.Machine) == EnvironmentVariableTarget.User)
                UserIsChecked = true;
            else
                ProcessIsChecked = true;
        }
        public void RefreshItems(EnvironmentVariableTarget target)
        {
            Settings.SetSetting(App.ApplicationName, "Settings", "PathTarget", target);
            RefreshItems();
        }
        private void DisableAll()
        {
            if (_SettingItem)
                return;
            PathItems.ToList().ForEach(x =>
            {
                x.IsSelected = false;
                x.IsDeleteEnabled = false;
                x.IsMoveDownEnabled = false;
                x.IsMoveUpEnabled = false;
            });
        }
        public void RefreshItems()
        {
            if (PathItems != null)
            {
                PathItems.ToList().ForEach(x =>
                {
                    x.PropertyChanged -= Item_PropertyChanged;
                    x.PerformAction -= Item_PerformAction;
                });
                PathItems.Clear();
            }
            else
                PathItems = new ObservableCollection<PathItem>();

            var pathValue = Environment.GetEnvironmentVariable("Path", Settings.GetSetting(App.ApplicationName, "Settings", "PathTarget", System.EnvironmentVariableTarget.Machine));
            if (string.IsNullOrEmpty(pathValue))
                pathValue = string.Empty;
            var items = pathValue.Split(';');
            var index = 0;
            items.ToList().ForEach(x =>
            {
                var item = new PathItem { Position = index, Value = x };
                item.PerformAction += Item_PerformAction;
                item.PropertyChanged += Item_PropertyChanged;
                PathItems.Add(item);
                index++;
            });
            DisableAll();
            PathIsChanged = false;
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Value") || e.PropertyName.Equals("Position"))
                PathIsChanged = true;
        }

        private void Item_PerformAction(object sender, PerformActionEventArgs e)
        {
            var item = sender.As<PathItem>();
            if (e.Action == PerformActionEventArgs.Actions.Delete)
            {
                var result = MessageBox.Show($"Delete item #{item.Position} of the path?", "Delete path item", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                    return;
                PathIsChanged = true;
                PathItems.Remove(item);
            }
            else if (e.Action == PerformActionEventArgs.Actions.MoveUp)
            {
                var minItemPosition = PathItems.Min(x => x.Position);
                if (SelectedPathItem.Position <= minItemPosition)
                    return;
                var otherItem = PathItems.FirstOrDefault(x => x.Position == item.Position - 1);
                SwapItems(ref item, ref otherItem);
                SetSelectedItem(item.Position - 1);
            }
            else if (e.Action == PerformActionEventArgs.Actions.MoveDown)
            {
                var maxItemPosition = PathItems.Max(x => x.Position);
                if (SelectedPathItem.Position >= maxItemPosition)
                    return;
                var otherItem = PathItems.FirstOrDefault(x => x.Position == item.Position + 1);
                SwapItems(ref item, ref otherItem);
                SetSelectedItem(item.Position + 1);
            }
        }
        private void SwapItems(ref PathItem firstItem, ref PathItem secondItem)
        {
            var tempValue = firstItem.Value;
            firstItem.Value = secondItem.Value;
            secondItem.Value = tempValue;
        }

        private ObservableCollection<PathItem> _PathItems;
        public ObservableCollection<PathItem> PathItems {
            get { return _PathItems; }
            set {
                _PathItems = value;
                InvokePropertyChanged(nameof(PathItems));
            }
        }
        private void SwitchTarget(EnvironmentVariableTarget? oldTarget, EnvironmentVariableTarget target)
        {
            if (_ResettingTaget)
                return;
            if (PathIsChanged)
            {
                var result = MessageBox.Show($"Path data has changed. Are you sure you want to switch target?", "Switch Target?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    _ResettingTaget = true;
                    if (oldTarget.HasValue)
                    {
                        switch (oldTarget.Value)
                        {
                            case EnvironmentVariableTarget.Process:
                                ProcessIsChecked = true;
                                break;
                            case EnvironmentVariableTarget.User:
                                UserIsChecked = true;
                                break;
                            case EnvironmentVariableTarget.Machine:
                                MachineIsChecked = true;
                                break;
                            default:
                                break;
                        }
                    }
                    _ResettingTaget = false;
                    return;
                }
            }
            _CurrentTarget = target;
            RefreshItems(target);
        }
        private bool _ResettingTaget = false;
        private EnvironmentVariableTarget? _CurrentTarget;
        private bool _MachineIsChecked;
        public bool MachineIsChecked {
            get { return _MachineIsChecked; }
            set {
                _MachineIsChecked = value;
                if (MachineIsChecked)
                    SwitchTarget(_CurrentTarget, EnvironmentVariableTarget.Machine);
                InvokePropertyChanged(nameof(MachineIsChecked));
            }
        }

        private bool _UserIsChecked;
        public bool UserIsChecked {
            get { return _UserIsChecked; }
            set {
                _UserIsChecked = value;
                if (UserIsChecked)
                    SwitchTarget(_CurrentTarget, EnvironmentVariableTarget.User);
                InvokePropertyChanged(nameof(UserIsChecked));
            }
        }

        private bool _ProcessIsChecked;
        public bool ProcessIsChecked {
            get { return _ProcessIsChecked; }
            set {
                _ProcessIsChecked = value;
                if (ProcessIsChecked)
                    SwitchTarget(_CurrentTarget, EnvironmentVariableTarget.Process);
                InvokePropertyChanged(nameof(ProcessIsChecked));
            }
        }

        private PathItem _SelectedPathItem;
        public PathItem SelectedPathItem {
            get { return _SelectedPathItem; }
            set {
                _SelectedPathItem = value;
                InvokePropertyChanged(nameof(SelectedPathItem));
            }
        }

        private bool _PathIsChanged;
        public bool PathIsChanged {
            get { return _PathIsChanged; }
            set {
                _PathIsChanged = value;
                UpdateInterface();
                InvokePropertyChanged(nameof(PathIsChanged));
            }
        }
        private DelegateCommand _SaveCommand;
        public DelegateCommand SaveCommand => _SaveCommand ?? (_SaveCommand = new DelegateCommand(Save, ValidateSaveState));
        private void Save(object state)
        {
            var result = MessageBox.Show($"Save the {_CurrentTarget.Value} path variable?", "Save Path?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                return;
            var pathValue = string.Join(";", PathItems.Select(x => x.Value));
            Environment.SetEnvironmentVariable("Path", pathValue, _CurrentTarget.Value);
            PathIsChanged = false;
        }
        private bool ValidateSaveState(object state) => PathIsChanged;
        private DelegateCommand _CloseCommand;
        public DelegateCommand CloseCommand => _CloseCommand ?? (_CloseCommand = new DelegateCommand(Close, ValidateCloseState));
        private void Close(object state)
        {
            if (PathIsChanged)
            {
                var result = MessageBox.Show($"Path data has changed. Are you sure you want to exit?", "Exit Set Path?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                    return;
            }
            Environment.Exit(0);
        }
        private bool _SettingItem = false;
        public void SetSelectedItem(int position)
        {
            DisableAll();
            var minItem = PathItems.Min(x => x.Position);
            if (position < minItem)
                return;
            var maxItem = PathItems.Max(x => x.Position);
            _SettingItem = true;
            SelectedPathItem = PathItems.FirstOrDefault(x => x.Position == position);
            SelectedPathItem.IsMoveUpEnabled = SelectedPathItem.Position > minItem;
            SelectedPathItem.IsMoveDownEnabled = SelectedPathItem.Position < maxItem;
            SelectedPathItem.IsDeleteEnabled = true;
            SelectedPathItem.IsSelected = true;
            UpdateInterface();
            _SettingItem = false;
        }
        private bool ValidateCloseState(object state) => true;


    }
}
