// ***********************************************************************
// Assembly         : User Manager
// Author           : Greg
// Created          : 06-25-2015
//
// Last Modified By : Greg
// Last Modified On : 06-25-2015
// ***********************************************************************
// <copyright file="OptionsWindow.xaml.cs" company="Statistics & Controls, Inc.">
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
using Ookii.Dialogs.Wpf;
using User_Manager.Classes;

namespace User_Manager
{
	public partial class OptionsWindow : Window
	{
		#region Public Constructors

		public OptionsWindow()
		{
			InitializeComponent();
		}

		#endregion

		#region Public Properties
		public OptionsWindowView View
		{
			get
			{
				if (DesignerProperties.GetIsInDesignMode(this))
					return default(OptionsWindowView);
				return LayoutRoot.DataContext as OptionsWindowView;
			}
		}
		#endregion

		#region Private Methods

		private void OptionsWindowView_ExecuteCommand(object sender, ExecuteUIActionEventArgs e)
		{
			switch (e.CommandToExecute)
			{
				case "ResetToDefaultValues":
					var td = new TaskDialog
					{
						AllowDialogCancellation = true,
						ButtonStyle = TaskDialogButtonStyle.Standard,
						CenterParent = true,
						MainIcon = TaskDialogIcon.Warning,
						MainInstruction = string.Format("If you continue, you will reset the {0} to the default names?\r\rAre you sure you want to continue?", View.AreDefaultValues ? "default custom names" : "custom names for the currently loaded file"),
						MinimizeBox = false,
						WindowTitle = "Reset custom names"
					};
					var yesButton = new TaskDialogButton(ButtonType.Yes);
					var noButton = new TaskDialogButton(ButtonType.No);
					td.Buttons.Add(yesButton);
					td.Buttons.Add(noButton);
					var result1 = td.ShowDialog(this);
					if (result1 == yesButton)
					{
						var defNames = ApplicationSettings.GetDefaultCustomNames();
						View.RootName = defNames["Root"];
						View.GroupName = defNames["Group"];
						View.ItemName = defNames["Item"];
						View.ItemNameAttributeName = defNames["ItemName"];
						View.ItemTypeAttributeName = defNames["ItemType"];
						View.ItemFirstNameAttributeName = defNames["ItemFirstName"];
						View.ItemLastNameAttributeName = defNames["ItemLastName"];
						View.ItemPasswordAttributeName = defNames["ItemPassword"];
						View.UsersPermissionsName = defNames["UserPermissions"];
						View.UsersPermissionName = defNames["UserPermission"];
						View.UserPermissionValueName = defNames["PermissionValue"];
						View.RolesPermissionsName = defNames["RolePermissions"];
						View.RolesPermissionName = defNames["RolePermission"];
						View.RolePermissionValueName = defNames["PermissionValue"];
						View.ReferencesName = defNames["References"];
						View.ReferenceName = defNames["Reference"];
						View.ReferenceNameAttributeName = defNames["ReferenceName"];
						View.ReferenceTypeAttributeName = defNames["ReferenceType"];
						View.ReferenceSubTypeAttributeName = defNames["ReferenceSubType"];
					}
					break;
				case "ShowFolderBrowseDialog":
					var fbd = new VistaFolderBrowserDialog
					{
						UseDescriptionForTitle = true,
						ShowNewFolderButton = false,
						RootFolder = Environment.SpecialFolder.Desktop,
						Description = "Select folder where installation is located...",
						SelectedPath = View.InstallationFolder
					};
					var result = fbd.ShowDialog(this);
					if (!result.GetValueOrDefault())
						return;
					View.InstallationFolder = fbd.SelectedPath;
					break;
				case "CloseWindow":
					DialogResult = (bool)e.Parameters["result"];
					break;
			}
		}

		private void OptionsWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
		}

		private void TextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			((TextBox)sender).SelectAll();
		}

		#endregion
	}
}
