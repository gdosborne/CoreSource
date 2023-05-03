// ***********************************************************************
// Assembly         : User Manager
// Author           : Greg
// Created          : 06-17-2015
//
// Last Modified By : Greg
// Last Modified On : 06-17-2015
// ***********************************************************************
// <copyright file="AddUserWindow.xaml.cs" company="Statistics and Controls, Inc.">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using MVVMFramework;

namespace User_Manager
{
	public partial class AddUserWindow : Window
	{
		#region Public Constructors

		public AddUserWindow()
		{
			InitializeComponent();
		}

		#endregion

		#region Public Properties
		public AddUserWindowView View
		{
			get
			{
				if (DesignerProperties.GetIsInDesignMode(this))
					return default(AddUserWindowView);
				return LayoutRoot.DataContext as AddUserWindowView;
			}
		}
		#endregion

		#region Private Methods

		private void AddUserWindowView_ExecuteCommand(object sender, ExecuteUIActionEventArgs e)
		{
			switch (e.CommandToExecute)
			{
				case "CenterToParent":
					break;
				case "CloseWindow":
					DialogResult = (bool)e.Parameters["result"];
					break;
			}
		}

		private void EditUserWindowView_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
		}

		#endregion
	}
}
