// ***********************************************************************
// Assembly         : User Manager
// Author           : Greg
// Created          : 07-13-2015
//
// Last Modified By : Greg
// Last Modified On : 07-15-2015
// ***********************************************************************
// <copyright file="WebServiceWindow.xaml.cs" company="Statistics & Controls, Inc.">
//     Copyright ©  2015 Statistics & Controls, Inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using MVVMFramework;


namespace User_Manager
{
	public partial class WebServiceWindow : Window
	{
		#region Private Fields
		private DispatcherTimer _TestTimer = null;
		#endregion

		#region Public Constructors

		public WebServiceWindow()
		{
			InitializeComponent();
		}

		#endregion

		#region Public Properties
		public WebServiceWindowView View
		{
			get
			{
				if (DesignerProperties.GetIsInDesignMode(this))
					return default(WebServiceWindowView);
				return LayoutRoot.DataContext as WebServiceWindowView;
			}
		}
		#endregion

		#region Private Methods

		private void _TestTimer_Tick(object sender, EventArgs e)
		{
			_TestTimer.Stop();

			if (!string.IsNullOrEmpty(View.WebServiceUrl))
			{
				View.ProgressBarVisibility = Visibility.Visible;
				View.CheckUrl();
			}
		}

		private void Expander_ExpandedCollapsed(object sender, RoutedEventArgs e)
		{
			View.DetailsVisibility = (sender as Expander).IsExpanded ? Visibility.Visible : Visibility.Collapsed;
			View.DetailTitle = (sender as Expander).IsExpanded ? "Hide details" : "See details";
		}

		private void WebServiceWindowView_ExecuteCommand(object sender, ExecuteUIActionEventArgs e)
		{
			switch (e.CommandToExecute)
			{
				case "CloseWindow":
					DialogResult = (bool?)e.Parameters["result"];
					break;
				case "UpdateUI":
					if (Dispatcher.CheckAccess())
					{
						View.TestServiceCommand.IsEnabled = !string.IsNullOrEmpty(View.WebServiceUrl);
						View.OKCommand.IsEnabled = View.IsValidWebService;
						View.ProgressBarVisibility = Visibility.Collapsed;
					}
					else
						Dispatcher.BeginInvoke(new ExecuteUIActionHandler(WebServiceWindowView_ExecuteCommand), new object[] { sender, e });
					break;
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			_TestTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(500) };
			_TestTimer.Tick += _TestTimer_Tick;
			_TestTimer.Start();
		}

		#endregion
	}
}
