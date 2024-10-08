namespace OSInstallerBuilder.Controls
{
	using GregOsborne.MVVMFramework;
	using GregOsborne.Application.Media;
	using OSInstallerExtensibility.Interfaces;
	using System;
	using System.ComponentModel;
	using System.Windows;
	using System.Windows.Media;

	public class InstallerGlobalSettingsView : ViewModelBase, INotifyPropertyChanged
	{
		#region Public Constructors
		public InstallerGlobalSettingsView()
		{
			ControlEnabled = false;
		}
		#endregion Public Constructors

		#region Public Methods
		public override void UpdateInterface()
		{
			TextBrush = SystemColors.ControlTextBrush;
		}
		#endregion Public Methods

		#region Public Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private bool _ControlEnabled;
		private bool _IsInitializing;
		private IInstallerManager _Manager;
		private Brush _TextBrush;
		private string _VariableTrigger = null;
		#endregion Private Fields

		#region Public Properties
		private bool _AllowSilentInstall;
		public bool AllowSilentInstall
		{
			get { return _AllowSilentInstall; }
			set
			{
				_AllowSilentInstall = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("AllowSilentInstall"));
			}
		}
		public bool ControlEnabled
		{
			get
			{
				return _ControlEnabled;
			}
			set
			{
				_ControlEnabled = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ControlEnabled"));
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
				VariableTrigger = _Manager.VariableTrigger.ToString();
				AllowSilentInstall = _Manager.AllowSilentInstall;
				PreviousVariableTrigger = _Manager.VariableTrigger.ToString();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Manager"));
				IsInitializing = false;
			}
		}
		public string PreviousVariableTrigger { get; set; }
		public Brush TextBrush
		{
			get
			{
				return _TextBrush;
			}
			set
			{
				_TextBrush = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("TextBrush"));
			}
		}
		public string VariableTrigger
		{
			get
			{
				return _VariableTrigger;
			}
			set
			{
				_VariableTrigger = value;
				if (Manager != null)
					Manager.VariableTrigger = value.ToCharArray()[0];
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("VariableTrigger"));
			}
		}
		#endregion Public Properties
	}
}
