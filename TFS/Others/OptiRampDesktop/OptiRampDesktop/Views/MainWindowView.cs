using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using MyApplication.Primitives;
using Microsoft.Win32;
using MVVMFramework;
using OptiRampControls.Classes;
using OptiRampDesktop.Helpers;

namespace OptiRampDesktop.Views
{
	public class MainWindowView : INotifyPropertyChanged
	{
		#region Private Fields
		private RegistryKey _ApplicationKey;

		private ICommand _DeleteElementCommand = null;

		private ICommand _PropertiesCommand = null;

		private RegistryKey _ThisWindowKey;
		#endregion

		#region Public Constructors

		public MainWindowView()
		{
			ApplicationKey = MyApplication.Registry.Extensions.GetApplicationKey("OptiRampDesktop");
			ThisWindowKey = ApplicationKey.CreateSubKey("MainWindow");
		}

		#endregion

		#region Public Events
		public event DeleteHandler DeleteItem;
		public event OpenPropertiesHandler OpenProperties;
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		#region Public Properties
		public RegistryKey ApplicationKey
		{
			get { return _ApplicationKey; }
			set
			{
				_ApplicationKey = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ApplicationKey"));
			}
		}
		public DelegateCommand DeleteElementCommand
		{
			get
			{
				if (_DeleteElementCommand == null)
					_DeleteElementCommand = new DelegateCommand(DeleteElement, ValidateDeleteElementState);
				return _DeleteElementCommand as DelegateCommand;
			}
		}
		public DelegateCommand PropertiesCommand
		{
			get
			{
				if (_PropertiesCommand == null)
					_PropertiesCommand = new DelegateCommand(Properties, ValidatePropertiesCommandState);
				return _PropertiesCommand as DelegateCommand;
			}
		}
		public RegistryKey ThisWindowKey
		{
			get { return _ThisWindowKey; }
			set
			{
				_ThisWindowKey = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ThisWindowKey"));
			}
		}
		#endregion

		#region Private Methods

		private void DeleteElement(object state)
		{
			var orObject = state.As<OptiRampElement>();
			if (DeleteItem != null)
				DeleteItem(this, new ElementEventArgs(orObject));
		}

		private void Properties(object state)
		{
			var orObject = state.As<OptiRampElement>();
			if (OpenProperties != null)
				OpenProperties(this, new ElementEventArgs(orObject));
		}

		private bool ValidateDeleteElementState(object state)
		{
			return true;
		}

		private bool ValidatePropertiesCommandState(object state)
		{
			return true;
		}

		#endregion
	}
}