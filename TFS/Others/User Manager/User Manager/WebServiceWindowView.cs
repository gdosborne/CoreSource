// ***********************************************************************
// Assembly         : User Manager
// Author           : Greg
// Created          : 07-13-2015
//
// Last Modified By : Greg
// Last Modified On : 07-15-2015
// ***********************************************************************
// <copyright file="WebServiceWindowView.cs" company="Statistics & Controls, Inc.">
//     Copyright ©  2015 Statistics & Controls, Inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using MVVMFramework;
using Ookii.Dialogs.Wpf;
using User_Manager.Classes;

namespace User_Manager
{
	public class WebServiceWindowView : ViewModelBase, INotifyPropertyChanged
	{
		#region Private Fields
		private RelayCommand _CancelCommand;
		private string _DetailsText;
		private Visibility _DetailsVisibility;
		private string _DetailTitle;
		private bool _IsValidWebService;
		private List<string> _MethodNames;
		private RelayCommand _OKCommand;
		private RelayCommand _TestServiceCommand;
		private string _WebServiceUrl;
		#endregion

		#region Public Constructors

		public WebServiceWindowView()
		{
			WebServiceUrl = ApplicationSettings.GetValue<string>("Application", "LastWebServiceUrl", string.Empty);
			DetailsVisibility = Visibility.Collapsed;
			DetailTitle = "See details";
			ProgressBarVisibility = Visibility.Collapsed;
		}

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
		public string DetailsText
		{
			get { return _DetailsText; }
			set
			{
				_DetailsText = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("DetailsText"));
			}
		}
		public Visibility DetailsVisibility
		{
			get { return _DetailsVisibility; }
			set
			{
				_DetailsVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("DetailsVisibility"));
			}
		}
		public string DetailTitle
		{
			get { return _DetailTitle; }
			set
			{
				_DetailTitle = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("DetailTitle"));
			}
		}
		public bool IsValidWebService
		{
			get { return _IsValidWebService; }
			set
			{
				_IsValidWebService = value;
				UpdateUI();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("IsValidWebService"));
			}
		}
		public List<string> MethodNames
		{
			get { return _MethodNames; }
			set
			{
				_MethodNames = value;
				UpdateUI();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("MethodNames"));
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

		public RelayCommand TestServiceCommand
		{
			get
			{
				if (_TestServiceCommand == null)
					_TestServiceCommand = new RelayCommand(TestService) { IsEnabled = false };
				return _TestServiceCommand;
			}
		}

		public string WebServiceUrl
		{
			get { return _WebServiceUrl; }
			set
			{
				_WebServiceUrl = value;
				UpdateUI();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("WebServiceUrl"));
			}
		}

		private Visibility _ProgressBarVisibility;
		public Visibility ProgressBarVisibility
		{
			get { return _ProgressBarVisibility; }
			set
			{
				_ProgressBarVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ProgressBarVisibility"));
			}
		}
		#endregion

		#region Public Methods

		public void CheckUrl()
		{
			var t = Task.Factory.StartNew(Test);
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

		private void TestService()
		{
			ProgressBarVisibility = Visibility.Visible;
			CheckUrl();
		}

		private void Test()
		{
			try
			{
				EndpointAddress endpointAdress = new EndpointAddress(WebServiceUrl);
				BasicHttpBinding binding1 = new BasicHttpBinding();
				var client = new SecurityService.SecurityServiceClient(ApplicationSettings.ServiceName, endpointAdress);
				client.GetSecurityData();
				IsValidWebService = true;
				DetailsText = string.Format("Security data from the RemoteSecurityService located at \"{0}\" was accessed successfully.", WebServiceUrl);
			}
			catch (Exception ex)
			{
				DetailsText = ex.Message;
				var td = new TaskDialog
				{
					AllowDialogCancellation = true,
					ButtonStyle = TaskDialogButtonStyle.Standard,
					CenterParent = true,
					MainIcon = TaskDialogIcon.Error,
					MainInstruction = ex.Message,
					ExpandedInformation = ex.StackTrace,
					MinimizeBox = false,
					WindowTitle = "Application exception"
				};
				var okButton = new TaskDialogButton(ButtonType.Ok);
				td.Buttons.Add(okButton);
				var result = td.ShowDialog(App.Current == null ? null : App.Current.MainWindow);
				IsValidWebService = false;
			}
		}

		private void UpdateUI()
		{
			if (ExecuteCommand != null)
				ExecuteCommand(this, new ExecuteUIActionEventArgs("UpdateUI", null));
		}

		#endregion
	}
}
