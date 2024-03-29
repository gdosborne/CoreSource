// ***********************************************************************
// Assembly         : User Manager
// Author           : Greg
// Created          : 06-17-2015
//
// Last Modified By : Greg
// Last Modified On : 06-18-2015
// ***********************************************************************
// <copyright file="EditUserWindow.xaml.cs" company="Statistics & Controls, Inc.">
//     Copyright ©  2015 Statistics & Controls, Inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using MVVMFramework;


namespace User_Manager
{
	public partial class EditUserWindow : Window
	{
		#region Public Constructors

		public EditUserWindow()
		{
			InitializeComponent();
		}

		#endregion

		#region Public Properties
		public EditUserWindowView View
		{
			get
			{
				if (DesignerProperties.GetIsInDesignMode(this))
					return default(EditUserWindowView);
				return LayoutRoot.DataContext as EditUserWindowView;
			}
		}
		#endregion

		#region Private Methods

		private void EditUserWindowView_ExecuteCommand(object sender, ExecuteUIActionEventArgs e)
		{
			switch (e.CommandToExecute)
			{
				case "CloseWindow":
					DialogResult = (bool)e.Parameters["result"];
					break;
			}
		}

		private void EditUserWindowView_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
		}

		private void TextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			(sender as TextBox).SelectAll();
		}

		#endregion
	}
}
