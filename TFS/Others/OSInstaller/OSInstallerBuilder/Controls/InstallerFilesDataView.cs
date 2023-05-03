namespace OSInstallerBuilder.Controls
{
	using MVVMFramework;
	using MyApplication.Primitives;
	using OSInstallerExtensibility.Interfaces;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Linq;

	public class InstallerFilesDataView : ViewModelBase, INotifyPropertyChanged
	{
		#region Public Constructors
		public InstallerFilesDataView()
		{
			SelectedItems = new ObservableCollection<IInstallerItem>();
			SelectedItems.CollectionChanged += SelectedItems_CollectionChanged;
			UpdateInterface();
		}
		#endregion Public Constructors

		#region Public Methods
		public void AddItem(IInstallerItem item)
		{
			if (Manager.Items.Any(x => x.Path.Equals(item.Path, StringComparison.OrdinalIgnoreCase)))
				return;
			Manager.Items.Add(item);
			ItemsSource = new ObservableCollection<IInstallerItem>(Manager.Items.OrderBy(x => x.ItemType).ThenBy(x => x.Path));
		}
		public override void UpdateInterface()
		{
			DeleteItemCommand.RaiseCanExecuteChanged();
		}
		#endregion Public Methods

		#region Private Methods
		private void AddFolderItem(object state)
		{
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("AddFolderItems"));
		}
		private void AddItem(object state)
		{
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("AddFileItems"));
		}
		private void DeleteItem(object state)
		{
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("DeleteItems"));
		}
		private void SelectedItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			UpdateInterface();
		}
		private bool ValidateAddFolderItemState(object state)
		{
			return true;
		}
		private bool ValidateAddItemState(object state)
		{
			return true;
		}
		private bool ValidateDeleteItemState(object state)
		{
			return SelectedItems.Any();
		}
		#endregion Private Methods

		#region Public Events
		public event ExecuteUIActionHandler ExecuteUIAction;
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private DelegateCommand _AddFileItemCommand = null;
		private DelegateCommand _AddFolderItemCommand = null;
		private DelegateCommand _DeleteItemCommand = null;
		private bool _IsInitializing;
		private ObservableCollection<IInstallerItem> _ItemsSource;
		private IInstallerManager _Manager;
		private ObservableCollection<IInstallerItem> _SelectedItems;
		#endregion Private Fields

		#region Public Properties
		public DelegateCommand AddFileItemCommand
		{
			get
			{
				if (_AddFileItemCommand == null)
					_AddFileItemCommand = new DelegateCommand(AddItem, ValidateAddItemState);
				return _AddFileItemCommand.As<DelegateCommand>();
			}
		}
		public DelegateCommand AddFolderItemCommand
		{
			get
			{
				if (_AddFolderItemCommand == null)
					_AddFolderItemCommand = new DelegateCommand(AddFolderItem, ValidateAddFolderItemState);
				return _AddFolderItemCommand.As<DelegateCommand>();
			}
		}
		public DelegateCommand DeleteItemCommand
		{
			get
			{
				if (_DeleteItemCommand == null)
					_DeleteItemCommand = new DelegateCommand(DeleteItem, ValidateDeleteItemState);
				return _DeleteItemCommand.As<DelegateCommand>();
			}
		}
		public bool IsInitializing
		{
			get
			{
				return _IsInitializing;
			}
			set
			{
				_IsInitializing = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("IsInitializing"));
			}
		}
		public ObservableCollection<IInstallerItem> ItemsSource
		{
			get
			{
				return _ItemsSource;
			}
			set
			{
				_ItemsSource = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ItemsSource"));
			}
		}
		public IInstallerManager Manager
		{
			get
			{
				return _Manager;
			}
			set
			{
				IsInitializing = true;
				_Manager = value;
				if (_Manager == null)
					return;

				ItemsSource = new ObservableCollection<IInstallerItem>(_Manager.Items.OrderBy(x => x.ItemType).ThenBy(x => x.Path));

				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Manager"));
				IsInitializing = false;
			}
		}
		public ObservableCollection<IInstallerItem> SelectedItems
		{
			get
			{
				return _SelectedItems;
			}
			set
			{
				_SelectedItems = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SelectedItems"));
			}
		}
		#endregion Public Properties
	}
}
