namespace MyMinistry.Views
{
	using MyMinistry.Utilities;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class ContactsPageView : BindableBase
	{
		#region Private Fields

		private DelegateCommand _AddItemCommand = null;
		private string _AddTooltip;
		private ObservableCollection<MyMinistryContact> _Contacts;

		private DelegateCommand _DeleteItemCommand = null;

		private string _DeleteTooltip;
		private MyMinistryContact _SelectedContact;

		#endregion Private Fields

		#region Public Properties

		public DelegateCommand AddItemCommand {
			get {
				if (_AddItemCommand == null)
					_AddItemCommand = new DelegateCommand(AddItem, ValidateAddItemState);
				return _AddItemCommand as DelegateCommand;
			}
		}

		public string AddTooltip {
			get { return _AddTooltip; }
			set {
				_AddTooltip = value;
				OnPropertyChanged("AddTooltip");
			}
		}

		public ObservableCollection<MyMinistryContact> Contacts {
			get { return _Contacts; }
			set {
				_Contacts = value;
				UpdateInterface();
				OnPropertyChanged("Contacts");
			}
		}

		public DelegateCommand DeleteItemCommand {
			get {
				if (_DeleteItemCommand == null)
					_DeleteItemCommand = new DelegateCommand(DeleteItem, ValidateDeleteItemState);
				return _DeleteItemCommand as DelegateCommand;
			}
		}

		public string DeleteTooltip {
			get { return _DeleteTooltip; }
			set {
				_DeleteTooltip = value;
				OnPropertyChanged("DeleteTooltip");
			}
		}

		public bool IsCompactButtons {
			get { return CommonData.IsCompactButtons; }
			set {
				CommonData.IsCompactButtons = value;
				UpdateInterface();
				AddTooltip = value ? "Add contact" : null;
				DeleteTooltip = value ? "Delete contact" : null;
				OnPropertyChanged("IsCompactButtons");
			}
		}

		public MyMinistryContact SelectedContact {
			get { return _SelectedContact; }
			set {
				_SelectedContact = value;
				OnPropertyChanged("SelectedContact");
			}
		}

		#endregion Public Properties

		#region Public Methods

		public void Init()
		{
			Contacts = CommonData.Data.Contacts;
			CommonData.DataChanged += CommonData_DataChanged;
		}

		public void UpdateInterface()
		{
		}

		#endregion Public Methods

		#region Private Methods

		private void AddItem(object state)
		{
			OnExecuteUIAction("AddContact", null);
		}

		private void CommonData_DataChanged(EventArgs e)
		{
			OnPropertyChanged("IsCompactButtons");
		}

		private void DeleteItem(object state)
		{
		}

		private bool ValidateAddItemState(object state)
		{
			return true;
		}

		private bool ValidateDeleteItemState(object state)
		{
			return SelectedContact != null;
		}

		#endregion Private Methods
	}
}
