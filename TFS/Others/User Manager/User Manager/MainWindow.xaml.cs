// ***********************************************************************
// Assembly         : User Manager
// Author           : Greg
// Created          : 06-15-2015
//
// Last Modified By : Greg
// Last Modified On : 06-26-2015
// ***********************************************************************
// <copyright file="MainWindow.xaml.cs" company="Statistics and Controls, Inc.">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Linq;
using System.Printing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Ookii.Dialogs.Wpf;
using SNC.Authorization.Entities;
using User_Manager.Classes;
using User_Manager.Classes.Documents;
using Application.Primitives;
using MVVMFramework;

namespace User_Manager
{
	public partial class MainWindow : Window
	{
		[DllImport("kernel32.dll")]
		static extern IntPtr GlobalLock(IntPtr hMem);
		[DllImport("kernel32.dll")]
		static extern bool GlobalUnlock(IntPtr hMem);
		[DllImport("kernel32.dll")]
		static extern bool GlobalFree(IntPtr hMem);
		[DllImport("winspool.Drv", EntryPoint = "DocumentPropertiesW", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
		static extern int DocumentProperties(IntPtr hwnd, IntPtr hPrinter, [MarshalAs(UnmanagedType.LPWStr)] string pDeviceName, IntPtr pDevModeOutput, IntPtr pDevModeInput, int fMode);
		private const Int32 DM_OUT_BUFFER = 14;


		public void OpenPrinterPropertiesDialog(PrinterSettings printerSettings, System.IntPtr pHandle)
		{

			IntPtr hDevMode = printerSettings.GetHdevmode();
			IntPtr pDevMode = GlobalLock(hDevMode);
			Int32 fMode = 0;
			int sizeNeeded = DocumentProperties(pHandle, IntPtr.Zero, printerSettings.PrinterName, IntPtr.Zero, pDevMode, fMode);
			IntPtr devModeData = Marshal.AllocHGlobal(sizeNeeded);

			fMode = DM_OUT_BUFFER;

			DocumentProperties(pHandle, IntPtr.Zero, printerSettings.PrinterName, devModeData, pDevMode, fMode);
			GlobalUnlock(hDevMode);
			printerSettings.SetHdevmode(devModeData);
			printerSettings.DefaultPageSettings.SetHdevmode(devModeData);

			ApplicationSettings.SetValue<bool>("Printing", "Collate", printerSettings.Collate);
			ApplicationSettings.SetValue<short>("Printing", "Copies", printerSettings.Copies);
			ApplicationSettings.SetValue<bool>("Printing", "Page Color", printerSettings.DefaultPageSettings.Color);
			ApplicationSettings.SetValue<bool>("Printing", "Page Landscape", printerSettings.DefaultPageSettings.Landscape);
			ApplicationSettings.SetValue<int>("Printing", "Page Margin Left", printerSettings.DefaultPageSettings.Margins.Left);
			ApplicationSettings.SetValue<int>("Printing", "Page Margin Top", printerSettings.DefaultPageSettings.Margins.Top);
			ApplicationSettings.SetValue<int>("Printing", "Page Margin Right", printerSettings.DefaultPageSettings.Margins.Right);
			ApplicationSettings.SetValue<int>("Printing", "Page Margin Bottom", printerSettings.DefaultPageSettings.Margins.Bottom);
			ApplicationSettings.SetValue<string>("Printing", "Page PaperSource Name", printerSettings.DefaultPageSettings.PaperSource.SourceName);
			ApplicationSettings.SetValue<PrinterResolutionKind>("Printing", "Page PrinterResolution Kind", printerSettings.DefaultPageSettings.PrinterResolution.Kind);
			ApplicationSettings.SetValue<Duplex>("Printing", "Duplex", printerSettings.Duplex);
			ApplicationSettings.SetValue<int>("Printing", "FromPage", printerSettings.FromPage);
			ApplicationSettings.SetValue<int>("Printing", "MaximumPage", printerSettings.MaximumPage);
			ApplicationSettings.SetValue<int>("Printing", "MinimumPage", printerSettings.MinimumPage);
			ApplicationSettings.SetValue<string>("Printing", "PrinterName", printerSettings.PrinterName);
			ApplicationSettings.SetValue<bool>("Printing", "PrintToFile", printerSettings.PrintToFile);
			ApplicationSettings.SetValue<int>("Printing", "ToPage", printerSettings.ToPage);

			GlobalFree(hDevMode);
			Marshal.FreeHGlobal(devModeData);
		}

		#region Private Methods

		private void Window_Closing(object sender, CancelEventArgs e)
		{
			ApplicationSettings.SetValue<string>("Application", "LastDirectory", View.LastDirectory);
			ApplicationSettings.SetValue<string>("Application", "LastWebConfigFile", View.LastWebConfigFile);
			ApplicationSettings.SetValue<int>("Application", "LastFilterIndex", View.LastFilterIndex);
			if (!double.IsInfinity(RestoreBounds.Left))
				ApplicationSettings.SetValue<double>("MainWindow", "Left", RestoreBounds.Left);
			if (!double.IsInfinity(RestoreBounds.Top))
				ApplicationSettings.SetValue<double>("MainWindow", "Top", RestoreBounds.Top);
			if (!double.IsInfinity(RestoreBounds.Width))
				ApplicationSettings.SetValue<double>("MainWindow", "Width", RestoreBounds.Width);
			if (!double.IsInfinity(RestoreBounds.Height))
				ApplicationSettings.SetValue<double>("MainWindow", "Height", RestoreBounds.Height);
			ApplicationSettings.SetValue<WindowState>("MainWindow", "WindowState", WindowState);

			if (!View.CloseView().HasValue)
				e.Cancel = true;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			View.LastDirectory = ApplicationSettings.GetValue<string>("Application", "LastDirectory", string.Empty);
			View.LastWebConfigFile = ApplicationSettings.GetValue<string>("Application", "LastWebConfigFile", string.Empty);
			View.LastFilterIndex = ApplicationSettings.GetValue<int>("Application", "LastFilterIndex", 0);
			Left = ApplicationSettings.GetValue<double>("MainWindow", "Left", 0);
			Top = ApplicationSettings.GetValue<double>("MainWindow", "Top", 0);
			Width = ApplicationSettings.GetValue<double>("MainWindow", "Width", 600);
			Height = ApplicationSettings.GetValue<double>("MainWindow", "Height", 400);
			WindowState = ApplicationSettings.GetValue<WindowState>("MainWindow", "WindowState", WindowState);

			bool loginSuccess = false;
			string content = null;
			try
			{
				while (!loginSuccess)
				{
					var cd = new CredentialDialog
					{
						Target = "SNC User Manager",
						MainInstruction = "OptiRamp® User Manager Login",
						WindowTitle = "OptiRamp® Login",
						Content = content,
						ShowSaveCheckBox = true,
						UseApplicationInstanceCredentialCache = false,
						ShowUIForSavedCredentials = true
					};
					var result = cd.ShowDialog(this);
					if (!result)
						App.Current.Shutdown();
					loginSuccess = View.Authenticate(cd.Credentials);
					if(loginSuccess)
						App.WriteEventMessage(string.Format("User {0} login successful", cd.Credentials.UserName));
					if (loginSuccess && cd.IsSaveChecked)
						cd.ConfirmCredentials(true);
					else
					{
						App.WriteEventMessage(string.Format("Invalid login for {0}", cd.Credentials.UserName));
						content = "The supplied credentials are invalid or you are not System Administrator";
					}
				}
			}
			catch (Exception ex)
			{
				App.HandleException(ex, true);
			}
		}

		#endregion

		#region Public Constructors

		public MainWindow()
		{
			InitializeComponent();
		}

		#endregion

		#region Public Properties
		public MainWindowView View
		{
			get
			{
				if (DesignerProperties.GetIsInDesignMode(this))
					return default(MainWindowView);
				return LayoutRoot.DataContext as MainWindowView;
			}
		}
		#endregion

		private void Border_GotFocus(object sender, RoutedEventArgs e)
		{
			(sender as Image).Cursor = Cursors.Hand;
		}

		private void DataGrid_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			View.EditUserCommand.Execute(null);
		}

		private PrinterSettings GetPrinterSettings()
		{
			var result = new PrinterSettings();
			result.PrinterName = ApplicationSettings.GetValue<string>("Printing", "PrinterName", result.PrinterName);
			result.Collate = ApplicationSettings.GetValue<bool>("Printing", "Collate", result.Collate);
			result.Copies = ApplicationSettings.GetValue<short>("Printing", "Copies", result.Copies);
			result.DefaultPageSettings.Color = ApplicationSettings.GetValue<bool>("Printing", "Page Color", result.DefaultPageSettings.Color);
			result.DefaultPageSettings.Landscape = ApplicationSettings.GetValue<bool>("Printing", "Page Landscape", result.DefaultPageSettings.Landscape);
			result.DefaultPageSettings.Margins.Left = ApplicationSettings.GetValue<int>("Printing", "Page Margin Left", result.DefaultPageSettings.Margins.Left);
			result.DefaultPageSettings.Margins.Top = ApplicationSettings.GetValue<int>("Printing", "Page Margin Top", result.DefaultPageSettings.Margins.Top);
			result.DefaultPageSettings.Margins.Right = ApplicationSettings.GetValue<int>("Printing", "Page Margin Right", result.DefaultPageSettings.Margins.Right);
			result.DefaultPageSettings.Margins.Bottom = ApplicationSettings.GetValue<int>("Printing", "Page Margin Bottom", result.DefaultPageSettings.Margins.Bottom);
			result.DefaultPageSettings.PaperSource.SourceName = ApplicationSettings.GetValue<string>("Printing", "Page PaperSource Name", result.DefaultPageSettings.PaperSource.SourceName);
			result.DefaultPageSettings.PrinterResolution.Kind = ApplicationSettings.GetValue<PrinterResolutionKind>("Printing", "Page PrinterResolution Kind", result.DefaultPageSettings.PrinterResolution.Kind);
			result.Duplex = ApplicationSettings.GetValue<Duplex>("Printing", "Duplex", result.Duplex);
			result.FromPage = ApplicationSettings.GetValue<int>("Printing", "FromPage", result.FromPage);
			result.MaximumPage = ApplicationSettings.GetValue<int>("Printing", "MaximumPage", result.MaximumPage);
			result.MinimumPage = ApplicationSettings.GetValue<int>("Printing", "MinimumPage", result.MinimumPage);
			result.PrintToFile = ApplicationSettings.GetValue<bool>("Printing", "PrintToFile", result.PrintToFile);
			result.ToPage = ApplicationSettings.GetValue<int>("Printing", "ToPage", result.ToPage);
			return result;
		}

		private void MainWindowView_ExecuteCommand(object sender, ExecuteUIActionEventArgs e)
		{
			bool? result = null;
			switch (e.CommandToExecute)
			{
				case "OpenWebService":
					var wsWin = new WebServiceWindow
					{
						Owner = this,
						WindowStartupLocation = WindowStartupLocation.CenterOwner
					};
					var result1 = wsWin.ShowDialog();
					if (!result1.GetValueOrDefault())
						return;
					View.WSUrl = wsWin.View.WebServiceUrl;
					View.OpenWebServiceSecurityData();
					break;
				case "ShowPrinterSettingsDialog":
					OpenPrinterPropertiesDialog(GetPrinterSettings(), (new System.Windows.Interop.WindowInteropHelper(this)).Handle);
					break;
				case "ShowPrintDialog":
					var paginator = new UserPrintPaginator<PermissionItem>(View.Manager);
					var isLandscape = paginator.PageSize.Width > paginator.PageSize.Height;
					PrintDialog pd = new PrintDialog { UserPageRangeEnabled = false };
					pd.PrintTicket.PageOrientation = isLandscape ? PageOrientation.Landscape : PageOrientation.Portrait;
					pd.PrintQueue = new PrintQueue(new PrintServer(), ApplicationSettings.GetValue<string>("Printing", "PrinterName", string.Empty));
					if (!pd.ShowDialog().GetValueOrDefault())
						return;
					pd.PrintDocument(paginator, "User List");
					break;
				case "ShowOptionsWindow":
					var optWin = new OptionsWindow
					{
						Owner = this,
						WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner
					};
					optWin.View.Manager = View.Manager;
					optWin.View.AreDefaultValues = View.Manager == null;
					optWin.View.FileName = View.FileName;
					result = optWin.ShowDialog();
					View.RecentMenuItems = ApplicationSettings.GetRecentItems();
					if (!result.GetValueOrDefault())
						return;
					if (View.Manager == null)
					{
						//sets new defaults - all new files created from this point forward
						// will use these values until changed
						ApplicationSettings.SetValue<string>("CustomNames", "Root", optWin.View.RootName);
						ApplicationSettings.SetValue<string>("CustomNames", "Group", optWin.View.GroupName);
						ApplicationSettings.SetValue<string>("CustomNames", "Item", optWin.View.ItemName);
						ApplicationSettings.SetValue<string>("CustomNames", "ItemName", optWin.View.ItemNameAttributeName);
						ApplicationSettings.SetValue<string>("CustomNames", "ItemType", optWin.View.ItemTypeAttributeName);
						ApplicationSettings.SetValue<string>("CustomNames", "ItemFirstName", optWin.View.ItemFirstNameAttributeName);
						ApplicationSettings.SetValue<string>("CustomNames", "ItemLastName", optWin.View.ItemLastNameAttributeName);
						ApplicationSettings.SetValue<string>("CustomNames", "Password", optWin.View.ItemPasswordAttributeName);
						ApplicationSettings.SetValue<string>("CustomNames", "UserPermissions", optWin.View.UsersPermissionsName);
						ApplicationSettings.SetValue<string>("CustomNames", "UserPermission", optWin.View.UsersPermissionName);
						ApplicationSettings.SetValue<string>("CustomNames", "PermissionValue", optWin.View.UserPermissionValueName);
						ApplicationSettings.SetValue<string>("CustomNames", "RolePermissions", optWin.View.RolesPermissionsName);
						ApplicationSettings.SetValue<string>("CustomNames", "RolePermission", optWin.View.RolesPermissionName);
						ApplicationSettings.SetValue<string>("CustomNames", "PermissionValue", optWin.View.RolePermissionValueName);
						ApplicationSettings.SetValue<string>("CustomNames", "References", optWin.View.ReferencesName);
						ApplicationSettings.SetValue<string>("CustomNames", "Reference", optWin.View.ReferenceName);
						ApplicationSettings.SetValue<string>("CustomNames", "ReferenceName", optWin.View.ReferenceNameAttributeName);
						ApplicationSettings.SetValue<string>("CustomNames", "ReferenceType", optWin.View.ReferenceTypeAttributeName);
						ApplicationSettings.SetValue<string>("CustomNames", "ReferenceSubType", optWin.View.ReferenceSubTypeAttributeName);
					}
					View.HasChanges = !optWin.View.AreDefaultValues;
					break;
				case "ShowAddUserWindow":
					var addWin = new AddUserWindow
					{
						Owner = this,
						WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner
					};
					addWin.View.Items = View.PermissionItems.ToList();
					result = addWin.ShowDialog();
					if (!result.GetValueOrDefault())
						return;
					PermissionItem item = null;
					if (addWin.View.Type == "User")
					{
						App.WriteEventMessage(string.Format("Adding user \"{0}\"", addWin.View.Name));
						item = new User(addWin.View.Name, addWin.View.FirstName, addWin.View.LastName) { Password = addWin.View.Password };
					}
					else if (addWin.View.Type == "Role")
					{
						App.WriteEventMessage(string.Format("Adding role \"{0}\"", addWin.View.Name));
						item = new Role(addWin.View.Name);
						addWin.View.Items
							.Where(x => x.IsSelected).ToList()
							.ForEach(x => (item as Role).References.Add(new Reference(x.Name) { SubType = x.Type }));
					}
					item.Permissions.Add(SNC.Authorization.Authorizations.None);
					View.AddPermission(item);
					break;
				case "ShowEditWindow":
					if (View.SelectedItem.IsReadOnly)
						return;
					var editWin = new EditUserWindow
					{
						Owner = this,
						WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner,
					};
					var hasChanges = View.HasChanges;
					var data = new List<PermissionItemSelector>();
					var itemName = View.SelectedItem.Name;
					if (View.SelectedItem.Type.Equals("User"))
					{
						View.Manager.Permissions.Where(x => x is Role).Cast<Role>().ToList().ForEach(p =>
						{
							data.Add(new PermissionItemSelector(p) { IsSelected = false });
						});
						data.ForEach(p =>
						{
							if ((p.Item as Role).References.Any(x => x.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase)))
							{
								p.IsSelected = true;
							}
						});
						editWin.View.Roles = data;
					}
					else if (View.SelectedItem.Type.Equals("Role"))
					{
						View.Manager.Permissions.Where(x => x.Name != itemName).ToList().ForEach(p =>
						{
							data.Add(new PermissionItemSelector(p) { IsSelected = false });
						});
						var users = data.Where(x => x.Type.Equals("User")).ToList();
						var roles = data.Where(x => x.Type.Equals("Role")).ToList();
						var validRoles = new List<PermissionItemSelector>();

						roles.ForEach(r => RemoveUsedRoles(r, View.SelectedItem.Name, ref validRoles));

						data = users;
						data.AddRange(validRoles);

						var refs = (View.SelectedItem.Item as Role).References;
						refs.ForEach(r =>
						{
							data.ForEach(p =>
							{
								if (r.Name.Equals(p.Name, StringComparison.OrdinalIgnoreCase))
								{
									p.IsSelected = true;
								}
							});
						});
						editWin.View.Items = data;
					}
					editWin.View.Permissions = View.Permissions;
					editWin.View.Applications = View.Applications;
					editWin.View.SpecialFlags = View.SpecialFlags;
					editWin.View.Item = View.SelectedItem;
					View.HasChanges = hasChanges;
					result = editWin.ShowDialog();
					if (!result.GetValueOrDefault())
						return;
					App.WriteEventMessage(string.Format("Updating {1} \"{0}\"", View.SelectedItem.Name, View.SelectedItem.Type));
					View.SelectedItem.Name = editWin.View.Name;
					View.SelectedItem.Item.Name = editWin.View.Name;
					if (View.SelectedItem.Type.Equals("User"))
					{
						editWin.View.Roles.ForEach(r =>
						{
							(r.Item as Role).References.Clear();
							if (r.IsSelected)
								(r.Item as Role).References.Add(new Reference(View.SelectedItem.Name) { SubType = "User" });
						});
					}
					else if (View.SelectedItem.Type.Equals("Role"))
					{
						(View.SelectedItem.Item as Role).References.Clear();
						editWin.View.Items.ForEach(u =>
						{
							if (u.IsSelected)
								(View.SelectedItem.Item as Role).References.Add(new Reference(u.Name) { SubType = u.Type });
						});
					}
					View.HasChanges = true;
					break;
			}
		}

		private void MainWindowView_GetItemsToDelete(object sender, Classes.Events.GetItemsToDeleteEventArgs e)
		{
			PermissionsDataGrid.SelectedItems.Cast<PermissionItemSelector>().ToList().ForEach(x =>
			{
				if ((x.Item.Permissions.FirstOrDefault() & SNC.Authorization.Authorizations.Spec_Persistent) != SNC.Authorization.Authorizations.Spec_Persistent)
					e.Items.Add(x);
			});
		}

		private void MainWindowView_GetSelectedElements(object sender, Classes.Events.ClipboardElementsEventArgs e)
		{
			PermissionsDataGrid.SelectedItems.Cast<PermissionItemSelector>().ToList().ForEach(x =>
			{
				if ((x.Item.Permissions.FirstOrDefault() & SNC.Authorization.Authorizations.Spec_Persistent) != SNC.Authorization.Authorizations.Spec_Persistent)
					e.Parent.Add(x.Item.ToXElement(View.Manager));
			});
		}

		private void MainWindowView_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
		}

		private void MenuItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			View.ProcessClick();
		}

		private void PermissionsDataGrid_ContextMenuOpening(object sender, ContextMenuEventArgs e)
		{
			View.ProcessClick();
		}

		private void RemoveUsedRoles(PermissionItemSelector role, string currentName, ref List<PermissionItemSelector> roles)
		{
			if (!(role.Item as Role).References.Any(x => x.Name.Equals(View.SelectedItem.Name, StringComparison.OrdinalIgnoreCase)))
				roles.Add(role);
			else
			{
				if ((role.Item as Role).References == null || !(role.Item as Role).References.Any() || role.Name.Equals(currentName, StringComparison.OrdinalIgnoreCase))
					return;
				var roleNames = (role.Item as Role).References.Where(x => x.SubType.Equals("Role")).Select(x => x.Name).ToList();
				if (roleNames.Contains(currentName))
					roleNames.Remove(currentName);
				var subRoles = View.PermissionItems.Where(x => roleNames.Contains(x.Name)).ToList();
				foreach (var r in subRoles)
				{
					RemoveUsedRoles(r, currentName, ref roles);
				}
			}
		}

		private void CheckBox_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			sender.As<CheckBox>().IsChecked = !sender.As<CheckBox>().IsChecked.GetValueOrDefault();
		}
	}
}
