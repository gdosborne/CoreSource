namespace OzChat.Views
{
	using MVVMFramework;
	using GregOsborne.Application.Media;
	using GregOsborne.Application.Primitives;
	using OzChat.Classes;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Linq;
	using System.Text;
	using System.Threading;
	using System.Threading.Tasks;
	using System.Windows;
	using System.Windows.Media;

	internal class MainWindowView : ViewModelBase, INotifyPropertyChanged
	{
		#region Public Constructors
		public MainWindowView()
		{
			//Myself = new Person { Name = "Rex Gray", UserName = "rgray" };
			//Myself = new Person { Name = "Greg Osborne", UserName = "gosborne" };
			ConversationVisibility = Visibility.Collapsed;
			Persons = new ObservableCollection<Person>();
			SendText = string.Empty;
			ConversationItems = new ObservableCollection<ConversationItem>();
		}
		#endregion Public Constructors

		#region Public Methods
		public void InitView()
		{
			try
			{
				using (var client = new ChatService.ChatServiceClient("ChatService", App.Uri))
				{
					client.RegisterUser(Myself.Name, Myself.UserName);
					var users = client.GetAllUsers();
					foreach (var user in users)
					{
						if (user.Key != Myself.UserName)
						{
							Persons.Add(new Person
							{
								Name = user.Value,
								UserName = user.Key
							});
						}
					}
				}
				qm = new QueueMonitor(Myself.UserName);
				qm.ConversationItemReceived += qm_ConversationItemReceived;
				qm.UpdateUsers += qm_UpdateUsers;
				var ct = new CancellationTokenSource();
				monitorTask = Task.Factory.StartNew(() => qm.Start());
				monitorTask.Repeat(ct.Token, TimeSpan.FromMilliseconds(100));
			}
			catch (Exception ex)
			{
				throw;
			}
			UpdateInterface();
		}

		void qm_UpdateUsers(object sender, EventArgs e)
		{
			string selectedUserName = null;
			if (SelectedPerson != null)
				selectedUserName = SelectedPerson.UserName;
			using (var client = new ChatService.ChatServiceClient("ChatService", App.Uri))
			{
				var users = client.GetAllUsers();
				foreach (var user in users)
				{
					if (user.Key != Myself.UserName && !Persons.Any(x => x.UserName == user.Key))
					{
						Persons.Add(new Person
						{
							Name = user.Value,
							UserName = user.Key
						});
					}
				}
			}
			if (!string.IsNullOrEmpty(selectedUserName))
				SelectedPerson = Persons.FirstOrDefault(x => x.UserName == selectedUserName);
		}
		public override void UpdateInterface()
		{
			SendTextCommand.RaiseCanExecuteChanged();
			CopyItemCommand.RaiseCanExecuteChanged();
			CopyConversationCommand.RaiseCanExecuteChanged();
		}
		#endregion Public Methods

		#region Private Methods
		private void CopyConversation(object state)
		{
			var sb = new StringBuilder();
			foreach (var item in ConversationItems.Where(x => x.ToPerson.UserName != Myself.UserName))
			{
				sb.AppendLine(item.Text);
			}
			Clipboard.SetText(sb.ToString());
		}
		private void CopyItem(object state)
		{
			Clipboard.SetText(SelectedConversationItem.Text);
		}
		private void PushItem(ConversationItem item)
		{
			var newItem = new ChatService.ConversationItem
			{
				FromPerson = item.FromPerson.UserName,
				Text = item.Text,
				ToPerson = item.ToPerson.UserName,
				TextBrush = item.TextBrush.As<SolidColorBrush>().Color.ToHexValue(),
				TimeSent = item.TimeSent
			};
			using (var client = new ChatService.ChatServiceClient("ChatService", App.Uri))
			{
				client.PostConversationItem(newItem);
			}
		}
		private void qm_ConversationItemReceived(object sender, ConversationItemReceivedEventArgs e)
		{
			if (ConversationItemReceived != null)
				ConversationItemReceived(this, e);
		}
		private void SendTextAction(object state)
		{
			var item = new ConversationItem
			{
				Text = SendText,
				FromPerson = Myself,
				ToPerson = SelectedPerson,
				TextBrush = new SolidColorBrush(myColor),
				TimeSent = DateTime.Now
			};
			PushItem(item);
			ConversationItems.Add(item);
			SendText = string.Empty;
			if (TextSent != null)
				TextSent(this, EventArgs.Empty);
		}
		private bool ValidateCopyConversationState(object state)
		{
			return SelectedPerson != null; ;
		}
		private bool ValidateCopyItemState(object state)
		{
			return SelectedConversationItem != null;
		}
		private bool ValidateSendTextState(object state)
		{
			return !string.IsNullOrEmpty(SendText);
		}
		#endregion Private Methods

		#region Public Events
		public event ConversationItemReceivedHandler ConversationItemReceived;
		public event PropertyChangedEventHandler PropertyChanged;
		public event EventHandler TextSent;
		#endregion Public Events

		#region Private Fields
		private readonly Color myColor = Colors.Orange;
		private ObservableCollection<ConversationItem> _ConversationItems;
		private Visibility _ConversationVisibility;
		private DelegateCommand _CopyConversationCommand = null;
		private DelegateCommand _CopyItemCommand = null;
		private bool _IsQueueMissing;
		private Person _Myself;
		private ObservableCollection<Person> _Persons;
		private ConversationItem _SelectedConversationItem;
		private Person _SelectedPerson;
		private string _SendText;
		private DelegateCommand _SendTextCommand = null;
		private DelegateCommand _StartConversationCommand = null;
		private Task monitorTask = null;
		private QueueMonitor qm = null;
		#endregion Private Fields

		#region Public Properties
		public ObservableCollection<ConversationItem> ConversationItems
		{
			get
			{
				return _ConversationItems;
			}
			set
			{
				_ConversationItems = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ConversationItems"));
			}
		}
		public Visibility ConversationVisibility
		{
			get
			{
				return _ConversationVisibility;
			}
			set
			{
				_ConversationVisibility = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ConversationVisibility"));
			}
		}
		public DelegateCommand CopyConversationCommand
		{
			get
			{
				if (_CopyConversationCommand == null)
					_CopyConversationCommand = new DelegateCommand(CopyConversation, ValidateCopyConversationState);
				return _CopyConversationCommand as DelegateCommand;
			}
		}
		public DelegateCommand CopyItemCommand
		{
			get
			{
				if (_CopyItemCommand == null)
					_CopyItemCommand = new DelegateCommand(CopyItem, ValidateCopyItemState);
				return _CopyItemCommand as DelegateCommand;
			}
		}
		public bool IsQueueMissing
		{
			get
			{
				return _IsQueueMissing;
			}
			set
			{
				_IsQueueMissing = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("IsQueueMissing"));
			}
		}
		public Person Myself
		{
			get
			{
				return _Myself;
			}
			set
			{
				_Myself = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Myself"));
			}
		}
		public ObservableCollection<Person> Persons
		{
			get
			{
				return _Persons;
			}
			set
			{
				_Persons = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Persons"));
			}
		}
		public ConversationItem SelectedConversationItem
		{
			get
			{
				return _SelectedConversationItem;
			}
			set
			{
				_SelectedConversationItem = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SelectedConversationItem"));
			}
		}
		public Person SelectedPerson
		{
			get
			{
				return _SelectedPerson;
			}
			set
			{
				_SelectedPerson = value;
				ConversationItems = new ObservableCollection<ConversationItem>();
				ConversationVisibility = Visibility.Visible;
				if (TextSent != null)
					TextSent(this, EventArgs.Empty);
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SelectedPerson"));
			}
		}
		public string SendText
		{
			get
			{
				return _SendText;
			}
			set
			{
				_SendText = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SendText"));
			}
		}
		public DelegateCommand SendTextCommand
		{
			get
			{
				if (_SendTextCommand == null)
					_SendTextCommand = new DelegateCommand(SendTextAction, ValidateSendTextState);
				return _SendTextCommand as DelegateCommand;
			}
		}
		#endregion Public Properties
	}
}
