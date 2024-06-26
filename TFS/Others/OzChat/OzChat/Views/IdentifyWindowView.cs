namespace OzChat.Views
{
	using MVVMFramework;
	using OzChat.Classes;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;

	using System.Linq;

	internal class IdentifyWindowView : ViewModelBase, INotifyPropertyChanged
	{
		#region Public Methods
		public void InitView()
		{
			Colors = new ObservableCollection<RowColor>(RowColor.GetColors());
		}
		public override void UpdateInterface()
		{
			OKCommand.RaiseCanExecuteChanged();
		}
		#endregion Public Methods

		#region Private Methods
		private void Cancel(object state)
		{
			DialogResult = false;
		}
		private void OK(object state)
		{
			ThisPerson = new Person
			{
				Name = FullName,
				UserName = UserName,
				Color = SelectedColor
			};
			DialogResult = true;
		}
		private bool ValidateCancelState(object state)
		{
			return true;
		}
		private bool ValidateOKState(object state)
		{
			return !String.IsNullOrEmpty(FullName) && !string.IsNullOrEmpty(UserName) && SelectedColor != null;
		}
		#endregion Private Methods

		#region Public Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private DelegateCommand _CancelCommand = null;
		private ObservableCollection<RowColor> _Colors;
		private bool? _DialogResult;
		private string _FullName;
		private DelegateCommand _OKCommand = null;
		private RowColor _SelectedColor;
		private Person _ThisPerson;
		private string _UserName;
		#endregion Private Fields

		#region Public Properties
		public DelegateCommand CancelCommand
		{
			get
			{
				if (_CancelCommand == null)
					_CancelCommand = new DelegateCommand(Cancel, ValidateCancelState);
				return _CancelCommand as DelegateCommand;
			}
		}
		public ObservableCollection<RowColor> Colors
		{
			get
			{
				return _Colors;
			}
			set
			{
				_Colors = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Colors"));
			}
		}
		public bool? DialogResult
		{
			get
			{
				return _DialogResult;
			}
			set
			{
				_DialogResult = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("DialogResult"));
			}
		}
		public string FullName
		{
			get
			{
				return _FullName;
			}
			set
			{
				_FullName = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FullName"));
			}
		}
		public DelegateCommand OKCommand
		{
			get
			{
				if (_OKCommand == null)
					_OKCommand = new DelegateCommand(OK, ValidateOKState);
				return _OKCommand as DelegateCommand;
			}
		}
		public RowColor SelectedColor
		{
			get
			{
				return _SelectedColor;
			}
			set
			{
				_SelectedColor = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SelectedColor"));
			}
		}
		public Person ThisPerson
		{
			get
			{
				return _ThisPerson;
			}
			set
			{
				_ThisPerson = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ThisPerson"));
			}
		}
		public string UserName
		{
			get
			{
				return _UserName;
			}
			set
			{
				_UserName = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("UserName"));
			}
		}
		#endregion Public Properties
	}
}
