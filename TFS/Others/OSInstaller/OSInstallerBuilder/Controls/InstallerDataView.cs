namespace OSInstallerBuilder.Controls
{
	using GregOsborne.MVVMFramework;
	using OSInstallerExtensibility.Interfaces;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Linq;
	using System.Windows;
	using System.Windows.Media;
	using GregOsborne.Application.Media;

	public class InstallerDataView : ViewModelBase, INotifyPropertyChanged
	{
		public InstallerDataView()
		{
		}
		
		#region Public Methods
		public override void UpdateInterface()
		{
			
		}
		#endregion Public Methods

		#region Private Methods
		private void x_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs("IndividualItem"));
		}
		#endregion Private Methods

		#region Public Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private ObservableCollection<IInstallerData> _Data;
		private bool _IsInitializing;
		private IInstallerManager _Manager;
		private IList<string> _RequiredNames;
		private string _VariableTrigger;
		#endregion Private Fields

		#region Public Properties
		public ObservableCollection<IInstallerData> Data
		{
			get
			{
				return _Data;
			}
			set
			{
				_Data = value;
				_Data.ToList().ForEach(x => x.PropertyChanged += x_PropertyChanged);
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Data"));
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
				Data = new ObservableCollection<IInstallerData>(_Manager.Datum.Where(x => !x.IsStepData));
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Manager"));
				IsInitializing = false;
			}
		}
		public IList<string> RequiredNames
		{
			get
			{
				return _RequiredNames;
			}
			set
			{
				_RequiredNames = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("RequiredNames"));
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
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("VariableTrigger"));
			}
		}
		#endregion Public Properties
	}
}
