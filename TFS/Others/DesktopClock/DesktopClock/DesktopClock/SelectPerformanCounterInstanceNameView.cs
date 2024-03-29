namespace DesktopClock
{
	using MVVMFramework;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.IO;
	using System.Windows;
	using System.Windows.Controls;

	public class SelectPerformanCounterInstanceNameView : ViewModelBase, INotifyPropertyChanged
	{
		#region Private Methods
		private void Cancel(object state)
		{
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("CloseDialog", new Dictionary<string, object> { { "result", false } }));
		}
		private void OK(object state)
		{
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("CloseDialog", new Dictionary<string, object> { { "result", true } }));
		}
		private void rdo_Checked(object sender, RoutedEventArgs e)
		{
			SelectedInstanceName = (sender as RadioButton).Tag as string;
		}
		private bool ValidateCancelState(object state)
		{
			return true;
		}
		private bool ValidateOKState(object state)
		{
			return !string.IsNullOrEmpty(SelectedInstanceName);
		}
		#endregion Private Methods

		#region Public Events
		public event ExecuteUIActionHandler ExecuteUIAction;
		public override event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private DelegateCommand _CancelCommand = null;
		private List<string> _CounterNames;
		private DelegateCommand _OKCommand = null;
		private string _SelectedInstanceName;
		private Dictionary<DriveInfo, string> DriveCref = new Dictionary<DriveInfo, string>();
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
		public List<string> CounterNames
		{
			get
			{
				return _CounterNames;
			}
			set
			{
				_CounterNames = value;
				var drives = new List<DriveInfo>(DriveInfo.GetDrives());
				_CounterNames.ForEach(x =>
				{
					drives.ForEach(y =>
					{
						if (x.ToUpper().Contains(y.Name.Trim('\\').ToUpper()))
						{
							DriveCref.Add(y, x);
							var rdo = new RadioButton
							{
								GroupName = "Drive",
								Content = y.Name,
								Tag = x,
								Margin = new Thickness(5, 0, 5, 0),
								Width = 50
							};
							rdo.Checked += rdo_Checked;
							if (ExecuteUIAction != null)
								ExecuteUIAction(this, new ExecuteUIActionEventArgs("AddRadioButton", new Dictionary<string, object> { { "value", rdo } }));
						}
					});
				});
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
		public string SelectedInstanceName
		{
			get
			{
				return _SelectedInstanceName;
			}
			set
			{
				_SelectedInstanceName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SelectedInstanceName"));
			}
		}
		#endregion Public Properties
	}
}
