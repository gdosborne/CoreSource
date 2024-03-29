namespace MyMinistry.Views
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Utilities;
	using Windows.Storage;

	public class ContactPageView : BindableBase
	{
		#region Private Fields
		private string _Address1;
		private string _Address2;
		private DelegateCommand _CancelCommand = null;
		private string _City;
		private MyMinistryContact _Contact;
		private string _FirstName;
		private string _HomePhone;
		private string _LastName;
		private string _MobilePhone;
		private string _Notes;
		private DelegateCommand _SaveCommand = null;
		private string _SpouseName;
		private StateItem _State;
		private string _Title;
		#endregion Private Fields
		private ObservableCollection<StateItem> _States;
		public ObservableCollection<StateItem> States
		{
			get { return _States; }
			set
			{
				_States = value;
				UpdateInterface();
				OnPropertyChanged("States");
			}
		}
		#region Public Constructors

		public ContactPageView()
		{
			Title = "Add Contact";
			States = new ObservableCollection<StateItem>(StateItem.GetStates().OrderBy(x => x.Abbreviation));
		}

		#endregion Public Constructors

		#region Public Events

		public event EventHandler Close;

		#endregion Public Events

		#region Public Properties

		public string Address1
		{
			get { return _Address1; }
			set
			{
				_Address1 = value;
				UpdateInterface();
				OnPropertyChanged("Address1");
			}
		}

		public string Address2
		{
			get { return _Address2; }
			set
			{
				_Address2 = value;
				UpdateInterface();
				OnPropertyChanged("Address2");
			}
		}

		public DelegateCommand CancelCommand
		{
			get
			{
				if (_CancelCommand == null)
					_CancelCommand = new DelegateCommand(Cancel, ValidateCancelState);
				return _CancelCommand as DelegateCommand;
			}
		}

		public string City
		{
			get { return _City; }
			set
			{
				_City = value;
				UpdateInterface();
				OnPropertyChanged("City");
			}
		}

		public MyMinistryContact Contact
		{
			get { return _Contact; }
			set
			{
				_Contact = value;
				if (value != null)
				{
					Title = "Edit Contact";
					FirstName = Contact.FirstName;
					LastName = Contact.LastName;
					Address1 = Contact.Address1;
					Address2 = Contact.Address2;
					SpouseName = Contact.SpouseName;
					City = Contact.City;
					State = States.First(x => x.Abbreviation.Equals(Contact.State.Abbreviation));
					HomePhone = Contact.TelephoneHome;
					MobilePhone = Contact.TelephoneMobile;
					Notes = Contact.Notes;
				}
				UpdateInterface();
				OnPropertyChanged("Contact");
			}
		}

		public string FirstName
		{
			get { return _FirstName; }
			set
			{
				_FirstName = value;
				UpdateInterface();
				OnPropertyChanged("FirstName");
			}
		}

		public string HomePhone
		{
			get { return _HomePhone; }
			set
			{
				_HomePhone = value;
				UpdateInterface();
				OnPropertyChanged("HomePhone");
			}
		}

		public string LastName
		{
			get { return _LastName; }
			set
			{
				_LastName = value;
				UpdateInterface();
				OnPropertyChanged("LastName");
			}
		}

		public string MobilePhone
		{
			get { return _MobilePhone; }
			set
			{
				_MobilePhone = value;
				UpdateInterface();
				OnPropertyChanged("MobilePhone");
			}
		}

		public string Notes
		{
			get { return _Notes; }
			set
			{
				_Notes = value;
				UpdateInterface();
				OnPropertyChanged("Notes");
			}
		}

		public DelegateCommand SaveCommand
		{
			get
			{
				if (_SaveCommand == null)
					_SaveCommand = new DelegateCommand(Save, ValidateSaveState);
				return _SaveCommand as DelegateCommand;
			}
		}

		public string SpouseName
		{
			get { return _SpouseName; }
			set
			{
				_SpouseName = value;
				UpdateInterface();
				OnPropertyChanged("SpouseName");
			}
		}

		public StateItem State
		{
			get { return _State; }
			set
			{
				_State = value;
				UpdateInterface();
				OnPropertyChanged("State");
			}
		}

		public string Title
		{
			get { return _Title; }
			set
			{
				_Title = value;
				UpdateInterface();
				OnPropertyChanged("Title");
			}
		}

		#endregion Public Properties

		#region Public Methods

		public void UpdateInterface()
		{
			SaveCommand.RaiseCanExecuteChanged();
		}

		#endregion Public Methods

		#region Private Methods

		private void Cancel(object state)
		{
			if (Close != null)
				Close(this, EventArgs.Empty);
		}

		private async void Save(object state)
		{
			if (Contact == null)
			{
				Contact = new MyMinistryContact
				{
					FirstName = FirstName,
					LastName = LastName,
					Address1 = Address1,
					Address2 = Address2,
					SpouseName = SpouseName,
					City = City,
					State = State,
					TelephoneHome = HomePhone,
					TelephoneMobile = MobilePhone,
					Notes = Notes
				};
				CommonData.Data.Contacts.Add(Contact);
			}
			else
			{
				Contact.FirstName = FirstName;
				Contact.LastName = LastName;
				Contact.Address1 = Address1;
				Contact.Address2 = Address2;
				Contact.SpouseName = SpouseName;
				Contact.City = City;
				Contact.State = State;
				Contact.TelephoneHome = HomePhone;
				Contact.TelephoneMobile = MobilePhone;
				Contact.Notes = Notes;
			}
			await SaveFile();
			MyMinistry.Utilities.CommonData.ContactToEdit = null;
			if (Close != null)
				Close(this, EventArgs.Empty);
		}

		private async Task SaveFile()
		{
			Enumerations.DataLocations loc = Enumerations.DataLocations.Local;
			if (ApplicationData.Current.RoamingSettings.Values.ContainsKey("DataLocation"))
				loc = (Enumerations.DataLocations)Enum.Parse(typeof(Enumerations.DataLocations), ApplicationData.Current.RoamingSettings.Values["DataLocation"].ToString(), true);
			if (loc == Enumerations.DataLocations.OneDrive)
			{
				await MyMinistryData.WriteInfo<MyMinistryData>(CommonData.Data, CommonData.FolderId, CommonData.DataFile.FileName, CommonData.Client);
			}
			else
			{
				await MyMinistryData.WriteInfo<MyMinistryData>(CommonData.Data, CommonData.FolderId, CommonData.LocalFile);
			}
		}

		private bool ValidateCancelState(object state)
		{
			return true;
		}

		private bool ValidateSaveState(object state)
		{
			return !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(City);
		}

		#endregion Private Methods
	}
}
