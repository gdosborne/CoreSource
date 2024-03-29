// ***********************************************************************
// Assembly         : User Manager
// Author           : Greg
// Created          : 06-22-2015
//
// Last Modified By : Greg
// Last Modified On : 06-22-2015
// ***********************************************************************
// <copyright file="EditWebConfigWindowView.cs" company="Statistics and Controls, Inc.">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;
using MVVMFramework;


namespace User_Manager
{
	public class EditWebConfigWindowView : ViewModelBase, INotifyPropertyChanged
	{
		#region Private Fields
		private RelayCommand _CancelCommand;
		private string _CurrentValue;
		private string _FileName;
		private string _NewValue;
		private RelayCommand _OKCommand;
		private string _SelectedSetting;
		private List<string> _SettingNames;
		#endregion

		#region Public Events
		public event ExecuteUIActionHandler ExecuteCommand;
		public override event PropertyChangedEventHandler PropertyChanged;
		#endregion

		#region Public Properties
		public RelayCommand CancelCommand
		{
			get
			{
				if (_CancelCommand == null)
					_CancelCommand = new RelayCommand(Cancel) { IsEnabled = true };
				return _CancelCommand;
			}
		}
		public string CurrentValue
		{
			get { return _CurrentValue; }
			set
			{
				_CurrentValue = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("CurrentValue"));
			}
		}
		public string FileName
		{
			get { return _FileName; }
			set
			{
				_FileName = value;
				var doc = XDocument.Load(_FileName);
				var appSettingsElement = doc.Root.Element("appSettings");
				if (appSettingsElement != null)
				{
					var names = new List<string>();
					foreach (var item in appSettingsElement.Elements())
					{
						if (item.Attribute("key") != null)
							names.Add(item.Attribute("key").Value);
					}
					SettingNames = names.OrderBy(x => x).ToList();
					SelectedSetting = SettingNames.First();
				}

				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FileName"));
			}
		}
		public string NewValue
		{
			get { return _NewValue; }
			set
			{
				_NewValue = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("NewValue"));
			}
		}
		public RelayCommand OKCommand
		{
			get
			{
				if (_OKCommand == null)
					_OKCommand = new RelayCommand(OK) { IsEnabled = false };
				return _OKCommand;
			}
		}
		public string SelectedSetting
		{
			get { return _SelectedSetting; }
			set
			{
				_SelectedSetting = value;
				UpdateInterface();
				var doc = XDocument.Load(FileName);
				var appSettingsElement = doc.Root.Element("appSettings");
				if (appSettingsElement != null)
				{
					foreach (var item in appSettingsElement.Elements())
					{
						if (item.Attribute("key") != null && item.Attribute("key").Value == value)
						{
							CurrentValue = item.Attribute("value").Value;
							break;
						}
					}
				}
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SelectedSetting"));
			}
		}
		public List<string> SettingNames
		{
			get { return _SettingNames; }
			set
			{
				_SettingNames = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SettingNames"));
			}
		}
		#endregion

		#region Private Methods

		private void Cancel()
		{
			if (ExecuteCommand != null)
				ExecuteCommand(this, new ExecuteUIActionEventArgs("CloseWindow", new Dictionary<string, object> { { "result", false } }));
		}

		private void OK()
		{
			if (ExecuteCommand != null)
				ExecuteCommand(this, new ExecuteUIActionEventArgs("CloseWindow", new Dictionary<string, object> { { "result", true } }));
		}

		private void UpdateInterface()
		{
			OKCommand.IsEnabled = !string.IsNullOrEmpty(NewValue);
		}

		#endregion
	}
}
