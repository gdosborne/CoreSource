// ***********************************************************************
// Assembly         : User Manager
// Author           : Greg
// Created          : 06-16-2015
//
// Last Modified By : Greg
// Last Modified On : 07-15-2015
// ***********************************************************************
// <copyright file="MainWindowView.cs" company="Statistics and Controls, Inc.">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using Application.Primitives;
using MVVMFramework;
using Ookii.Dialogs.Wpf;
using SNC.Authorization;
using SNC.Authorization.Entities;
using SNC.Authorization.Management;
using User_Manager.Classes;
using User_Manager.Classes.Events;
using User_Manager.Classes.Exceptions;

namespace User_Manager
{
	public class MainWindowView : ViewModelBase, INotifyPropertyChanged
	{
		#region Private Fields
		private RelayCommand _AddUserCommand;
		private List<AuthorizationSelector> _Applications = null;
		private RelayCommand _CheckForUpdateCommand;
		private bool _ClipboardContainsItems;
		private RelayCommand _CloseCommand;
		private RelayCommand _CopyCommand;
		private RelayCommand _CutCommand;
		private RelayCommand _DeleteUserCommand;
		private Visibility _EditorVisibility;
		private RelayCommand _EditUserCommand;
		private RelayCommand _ExitCommand;
		private string _FileName;
		private bool _HasChanges;
		private Visibility _HasChangeVisibility;
		private RelayCommand _HelpAboutCommand;
		private bool _IsWebFile;
		private string _LastDirectory;
		private int _LastFilterIndex;
		private string _LastWebConfigFile;
		private string _LocalUserSecurityFile = null;
		private ISNCPermissionManager _Manager;
		private Visibility _ManageToolbarVisibility;
		private RelayCommand _NewCommand;
		private Visibility _NoChangeVisibility;
		private RelayCommand _OpenCommand;
		private RelayCommand _OpenLogFolderCommand;
		private RelayCommand _OpenUserSecurityCommand;
		private RelayCommand _OpenWebConfigCommand;
		private RelayCommand _OpenWebServiceCommand;
		private RelayCommand _OptionsCommand;
		private RelayCommand _PasteCommand;
		private ObservableCollection<PermissionItemSelector> _PermissionItems = null;
		private List<AuthorizationSelector> _Permissions = null;
		private bool _PermissionsAreEnabled;
		private RelayCommand _PrintCommand;
		private RelayCommand _PrintSetupCommand;
		private RelayCommand _SaveAsCommand;
		private RelayCommand _SaveCommand;
		private PermissionItemSelector _SelectedItem;
		private bool _SelectedItemIsSystemAdmin;
		private List<AuthorizationSelector> _SpecialFlags = null;
		private Visibility _SpinnerVisibility;
		private string _UserName;
		private string _Version;
		private string _WSDataMethodName;
		private string _WSSaveMethodName;
		private string _WSUrl;
		#endregion

		#region Public Constructors

		public MainWindowView()
		{
			HasChangeVisibility = Visibility.Collapsed;
			NoChangeVisibility = Visibility.Visible;
			ManageToolbarVisibility = Visibility.Collapsed;
			EditorVisibility = Visibility.Collapsed;
			PermissionsAreEnabled = false;
			Version thisVersion = this.GetType().Assembly.GetName().Version;
			Version = thisVersion.ToString();
			SpinnerVisibility = Visibility.Collapsed;
			RecentMenuItems = ApplicationSettings.GetRecentItems();
		}
		private DelegateCommand _RecentItemCommand = null;
		public DelegateCommand RecentItemCommand
		{
			get
			{
				if (_RecentItemCommand == null)
					_RecentItemCommand = new DelegateCommand(RecentItem, ValidateRecentItemState);
				return _RecentItemCommand as DelegateCommand;
			}
		}
		private void RecentItem(object state)
		{
			var fileName = (string)state;
			if (Path.GetFileName(fileName).Equals("web.config", StringComparison.OrdinalIgnoreCase))
			{
				if (OpenWebConfigFile(fileName))
					return;
			}
			else if(fileName.StartsWith("http", StringComparison.OrdinalIgnoreCase))
				OpenWebService();
			else
				OpenFile(fileName);
		}
		private bool ValidateRecentItemState(object state)
		{
			return true;
		}

		#endregion

		#region Public Events
		public event ExecuteUIActionHandler ExecuteCommand;
		public event GetItemsToDeleteHandler GetItemsToDelete;
		public event ClipboardElementHandler GetSelectedElements;
		public override event PropertyChangedEventHandler PropertyChanged;
		#endregion

		#region Public Properties
		public RelayCommand AddUserCommand
		{
			get
			{
				if (_AddUserCommand == null)
					_AddUserCommand = new RelayCommand(AddUser) { IsEnabled = true };
				return _AddUserCommand;
			}
		}
		public List<AuthorizationSelector> Applications
		{
			get
			{
				if (_Applications == null)
				{
					var result = new List<AuthorizationSelector>();
					var values = AuthorizationDefaults.Applications;
					values.ForEach(x =>
					{
						var auth = new AuthorizationSelector { Authorization = x, IsSelected = false };
						result.Add(auth);
					});
					_Applications = result;
				}
				return _Applications;
			}
			set
			{
				_Applications = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Applications"));
			}
		}
		public RelayCommand CheckForUpdateCommand
		{
			get
			{
				if (_CheckForUpdateCommand == null)
					_CheckForUpdateCommand = new RelayCommand(CheckForUpdate) { IsEnabled = true };
				return _CheckForUpdateCommand;
			}
		}
		public bool ClipboardContainsItems
		{
			get { return _ClipboardContainsItems; }
			set
			{
				_ClipboardContainsItems = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ClipboardContainsItems"));
			}
		}
		public RelayCommand CloseCommand
		{
			get
			{
				if (_CloseCommand == null)
					_CloseCommand = new RelayCommand(Close) { IsEnabled = false };
				return _CloseCommand;
			}
		}
		public RelayCommand CopyCommand
		{
			get
			{
				if (_CopyCommand == null)
					_CopyCommand = new RelayCommand(Copy) { IsEnabled = false };
				return _CopyCommand;
			}
		}
		public RelayCommand CutCommand
		{
			get
			{
				if (_CutCommand == null)
					_CutCommand = new RelayCommand(Cut) { IsEnabled = false };
				return _CutCommand;
			}
		}
		public RelayCommand DeleteUserCommand
		{
			get
			{
				if (_DeleteUserCommand == null)
					_DeleteUserCommand = new RelayCommand(DeleteUser) { IsEnabled = false };
				return _DeleteUserCommand;
			}
		}
		public Visibility EditorVisibility
		{
			get { return _EditorVisibility; }
			set
			{
				_EditorVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("EditorVisibility"));
			}
		}
		public RelayCommand EditUserCommand
		{
			get
			{
				if (_EditUserCommand == null)
					_EditUserCommand = new RelayCommand(EditUser) { IsEnabled = false };
				return _EditUserCommand;
			}
		}
		public RelayCommand ExitCommand
		{
			get
			{
				if (_ExitCommand == null)
					_ExitCommand = new RelayCommand(Exit) { IsEnabled = true };
				return _ExitCommand;
			}
		}
		public string FileName
		{
			get { return _FileName; }
			set
			{
				_FileName = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FileName"));
			}
		}
		public bool HasChanges
		{
			get { return _HasChanges; }
			set
			{
				_HasChanges = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("HasChanges"));
			}
		}
		public Visibility HasChangeVisibility
		{
			get { return _HasChangeVisibility; }
			set
			{
				_HasChangeVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("HasChangeVisibility"));
			}
		}
		public RelayCommand HelpAboutCommand
		{
			get
			{
				if (_HelpAboutCommand == null)
					_HelpAboutCommand = new RelayCommand(HelpAbout) { IsEnabled = true };
				return _HelpAboutCommand;
			}
		}
		public bool IsWebFile
		{
			get { return _IsWebFile; }
			set
			{
				_IsWebFile = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("IsWebFile"));
			}
		}
		public string LastDirectory
		{
			get { return _LastDirectory; }
			set
			{
				_LastDirectory = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("LastDirectory"));
			}
		}
		public int LastFilterIndex
		{
			get { return _LastFilterIndex; }
			set
			{
				_LastFilterIndex = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("LastFilterIndex"));
			}
		}
		public string LastWebConfigFile
		{
			get { return _LastWebConfigFile; }
			set
			{
				_LastWebConfigFile = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("LastWebConfigFile"));
			}
		}
		public string LocalUserSecurityFile
		{
			get { return _LocalUserSecurityFile; }
			set
			{
				_LocalUserSecurityFile = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("LocalUserSecurityFile"));
			}
		}
		public ISNCPermissionManager Manager
		{
			get { return _Manager; }
			set
			{
				_Manager = value;
				if (_Manager == null)
					PermissionItems = null;
				else
				{
					var result = new List<PermissionItemSelector>();
					_Manager.Permissions.ToList().ForEach(x =>
					{
						var p = new PermissionItemSelector(x);
						p.PropertyChanged += p_PropertyChanged;
						result.Add(p);
					});
					PermissionItems = new ObservableCollection<PermissionItemSelector>(result);
					PermissionItems.CollectionChanged += PermissionItems_CollectionChanged;
				}
				HasChanges = false;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Manager"));
			}
		}
		public Visibility ManageToolbarVisibility
		{
			get { return _ManageToolbarVisibility; }
			set
			{
				_ManageToolbarVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ManageToolbarVisibility"));
			}
		}
		public RelayCommand NewCommand
		{
			get
			{
				if (_NewCommand == null)
					_NewCommand = new RelayCommand(New) { IsEnabled = true };
				return _NewCommand;
			}
		}
		public Visibility NoChangeVisibility
		{
			get { return _NoChangeVisibility; }
			set
			{
				_NoChangeVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("NoChangeVisibility"));
			}
		}
		public RelayCommand OpenCommand
		{
			get
			{
				if (_OpenCommand == null)
					_OpenCommand = new RelayCommand(Open) { IsEnabled = true };
				return _OpenCommand;
			}
		}
		public RelayCommand OpenLogFolderCommand
		{
			get
			{
				if (_OpenLogFolderCommand == null)
					_OpenLogFolderCommand = new RelayCommand(OpenLogFolder) { IsEnabled = true };
				return _OpenLogFolderCommand;
			}
		}
		public RelayCommand OpenUserSecurityCommand
		{
			get
			{
				if (_OpenUserSecurityCommand == null)
					_OpenUserSecurityCommand = new RelayCommand(OpenUserSecurity) { IsEnabled = true };
				return _OpenUserSecurityCommand;
			}
		}
		public RelayCommand OpenWebConfigCommand
		{
			get
			{
				if (_OpenWebConfigCommand == null)
					_OpenWebConfigCommand = new RelayCommand(OpenWebConfig) { IsEnabled = true };
				return _OpenWebConfigCommand;
			}
		}
		public RelayCommand OpenWebServiceCommand
		{
			get
			{
				if (_OpenWebServiceCommand == null)
					_OpenWebServiceCommand = new RelayCommand(OpenWebService) { IsEnabled = true };
				return _OpenWebServiceCommand;
			}
		}
		public RelayCommand OptionsCommand
		{
			get
			{
				if (_OptionsCommand == null)
					_OptionsCommand = new RelayCommand(Options) { IsEnabled = true };
				return _OptionsCommand;
			}
		}
		public RelayCommand PasteCommand
		{
			get
			{
				if (_PasteCommand == null)
					_PasteCommand = new RelayCommand(Paste) { IsEnabled = false };
				return _PasteCommand;
			}
		}
		public ObservableCollection<PermissionItemSelector> PermissionItems
		{
			get { return _PermissionItems; }
			set
			{
				_PermissionItems = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("PermissionItems"));
			}
		}
		public List<AuthorizationSelector> Permissions
		{
			get
			{
				if (_Permissions == null)
				{
					var result = new List<AuthorizationSelector>();
					var values = AuthorizationDefaults.Permissions;
					values.ForEach(x =>
					{
						var auth = new AuthorizationSelector { Authorization = x, IsSelected = false };
						result.Add(auth);
					});
					_Permissions = result;
				}
				return _Permissions;
			}
			set
			{
				_Permissions = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Permissions"));
			}
		}
		private IList<string> _RecentMenuItems;
		public IList<string> RecentMenuItems
		{
			get { return _RecentMenuItems; }
			set
			{
				_RecentMenuItems = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("RecentMenuItems"));
			}
		}
		public bool PermissionsAreEnabled
		{
			get { return _PermissionsAreEnabled; }
			set
			{
				_PermissionsAreEnabled = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("PermissionsAreEnabled"));
			}
		}
		public RelayCommand PrintCommand
		{
			get
			{
				if (_PrintCommand == null)
					_PrintCommand = new RelayCommand(Print) { IsEnabled = false };
				return _PrintCommand;
			}
		}
		public RelayCommand PrintSetupCommand
		{
			get
			{
				if (_PrintSetupCommand == null)
					_PrintSetupCommand = new RelayCommand(PrintSetup) { IsEnabled = true };
				return _PrintSetupCommand;
			}
		}
		public RelayCommand SaveAsCommand
		{
			get
			{
				if (_SaveAsCommand == null)
					_SaveAsCommand = new RelayCommand(SaveAs) { IsEnabled = false };
				return _SaveAsCommand;
			}
		}
		public RelayCommand SaveCommand
		{
			get
			{
				if (_SaveCommand == null)
					_SaveCommand = new RelayCommand(Save) { IsEnabled = false };
				return _SaveCommand;
			}
		}
		public PermissionItemSelector SelectedItem
		{
			get { return _SelectedItem; }
			set
			{
				_SelectedItem = value;
				SetPermissionsForItem();
				if (SelectedItem != null)
					SelectedItemIsSystemAdmin = (SelectedItem.Item.Permissions.FirstOrDefault() & Authorizations.Spec_SystemAdmin) == Authorizations.Spec_SystemAdmin;
				else
					SelectedItemIsSystemAdmin = false;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SelectedItem"));
			}
		}
		public bool SelectedItemIsSystemAdmin
		{
			get { return _SelectedItemIsSystemAdmin; }
			set
			{
				_SelectedItemIsSystemAdmin = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SelectedItemIsSystemAdmin"));
			}
		}
		public List<AuthorizationSelector> SpecialFlags
		{
			get
			{
				if (_SpecialFlags == null)
				{
					var result = new List<AuthorizationSelector>();
					var values = AuthorizationDefaults.SpecialFlags;
					values.ForEach(x =>
					{
						var auth = new AuthorizationSelector { Authorization = x, IsSelected = false };
						result.Add(auth);
					});
					_SpecialFlags = result;
				}
				return _SpecialFlags;
			}
			set
			{
				_SpecialFlags = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SpecialFlags"));
			}
		}
		public Visibility SpinnerVisibility
		{
			get { return _SpinnerVisibility; }
			set
			{
				_SpinnerVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SpinnerVisibility"));
			}
		}
		public string UserName
		{
			get { return _UserName; }
			set
			{
				_UserName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("UserName"));
			}
		}

		public string Version
		{
			get { return _Version; }
			set
			{
				_Version = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Version"));
			}
		}

		public string WSUrl
		{
			get { return _WSUrl; }
			set
			{
				_WSUrl = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("WSUrl"));
			}
		}
		#endregion

		#region Public Methods

		public void AddPermission(PermissionItem item)
		{
			Manager.Permissions.Add(item);
			PermissionItems.Add(new PermissionItemSelector(item));
			HasChanges = true;
		}

		public bool Authenticate(NetworkCredential credentials)
		{
			var appPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "OptiRamp® User Manager");
			if (!Directory.Exists(appPath))
				Directory.CreateDirectory(appPath);
			LocalUserSecurityFile = Path.Combine(appPath, "System.userdata");
			PermissionManager man = new Manager();
			Task t = null;
			if (!File.Exists(LocalUserSecurityFile))
			{
				man.Load(User_Manager.Classes.Manager.GetNewFileElement((Manager)man));
				t = man.SaveFileAsync(LocalUserSecurityFile, null);
			}
			else
			{
				t = man.LoadFileAsync(LocalUserSecurityFile);
			}
			man.PropertyChanged += Manager_PropertyChanged;
			t.Start();
			t.Wait();

			var result = man.Authenticate(credentials, Authorizations.Spec_SystemAdmin);
			if (result)
				UserName = credentials.UserName;
			return result;
		}

		public bool? CloseView()
		{
			if (HasChanges)
			{
				var result = MustSaveChanges();
				if (result.HasValue)
				{
					if (result.GetValueOrDefault())
					{
						Save();
						return true;
					}
					else
					{
						Manager = null;
						FileName = null;
						HasChanges = false;
						return false;
					}
				}
				else
					return null;
			}
			return true;
		}

		public void OpenWebServiceSecurityData()
		{
			if (HasChanges)
			{
				var saveResult = MustSaveChanges();
				if (!saveResult.HasValue)
					return;
				if (saveResult.GetValueOrDefault())
					Save();
			}
			App.WriteEventMessage(string.Format("Opening security data from {0}", WSUrl));
			RecentMenuItems = ApplicationSettings.AddRecentItem(WSUrl);
			EndpointAddress endpointAdress = new EndpointAddress(WSUrl);
			BasicHttpBinding binding1 = new BasicHttpBinding();
			var client = new SecurityService.SecurityServiceClient(ApplicationSettings.ServiceName, endpointAdress);
			var data = client.GetSecurityData();
			var man = new User_Manager.Classes.Manager();
			man.Parse(data);
			this.Manager = man;
			this.Manager.PropertyChanged += Manager_PropertyChanged;
			((PermissionManager)this.Manager).ItemAdded += Manager_ItemAdded;
			FileName = WSUrl;
			IsWebFile = true;
			HasChanges = false;
		}

		#endregion

		#region Internal Methods

		internal void ProcessClick()
		{
			ClipboardContainsItems = Clipboard.ContainsData("XElement");
			UpdateInterface();
		}

		#endregion

		#region Private Methods

		private void AddUser()
		{
			if (ExecuteCommand != null)
				ExecuteCommand(this, new ExecuteUIActionEventArgs("ShowAddUserWindow", null));
		}

		private void CheckForUpdate()
		{
			if (!App.AppRequiresUpdate)
			{
				var td = new TaskDialog
				{
					AllowDialogCancellation = true,
					ButtonStyle = TaskDialogButtonStyle.Standard,
					CenterParent = true,
					MainIcon = TaskDialogIcon.Shield,
					MainInstruction = "You have the current version of this application installed on your machine",
					ExpandedInformation = this.GetType().Assembly.GetName().Version.ToString(),
					MinimizeBox = false,
					WindowTitle = "Application is up-to-date"
				};
				var okButton = new TaskDialogButton(ButtonType.Ok);
				td.Buttons.Add(okButton);
				td.ShowDialog(App.Current.MainWindow);
			}
		}

		private void Close()
		{
			if (HasChanges)
			{
				var result = MustSaveChanges();
				if (!result.HasValue)
					return;
				if (result.GetValueOrDefault())
					Save();
			}
			Manager = null;
			FileName = null;
			HasChanges = false;
			Permissions = null;
			Applications = null;
			SpecialFlags = null;
			PermissionItems = null;
		}

		private void Copy()
		{
			var copyElement = new XElement("clipboard");
			if (GetSelectedElements != null)
				GetSelectedElements(this, new ClipboardElementsEventArgs(copyElement));
			Clipboard.SetData("XElement", copyElement.ToString());
		}

		private void Cut()
		{
			var copyElement = new XElement("clipboard");
			if (GetSelectedElements != null)
				GetSelectedElements(this, new ClipboardElementsEventArgs(copyElement));
			Clipboard.SetData("XElement", copyElement.ToString());
			DeleteUser();
		}

		private void DeleteUser()
		{
			var copyElement = new XElement("clipboard");
			if (GetSelectedElements != null)
				GetSelectedElements(this, new ClipboardElementsEventArgs(copyElement));
			var selectedCount = copyElement.Elements().Count();

			var td = new TaskDialog
			{
				AllowDialogCancellation = true,
				ButtonStyle = TaskDialogButtonStyle.Standard,
				CenterParent = true,
				MainIcon = TaskDialogIcon.Warning,
				MainInstruction = string.Format("Delete the {1}selected {0}?",
					selectedCount == 1 ? SelectedItem.Type.ToLower() : "items",
					selectedCount == 1 ? string.Empty : selectedCount.ToString() + " "),
				ExpandedInformation = string.Format("If the item{0} part of a role, they will be removed from the roles also.", selectedCount == 1 ? "is" : "s are"),
				MinimizeBox = false,
				WindowTitle = string.Format("Delete item{0}", selectedCount == 1 ? string.Empty : "s")
			};
			var yesButton = new TaskDialogButton(ButtonType.Yes);
			var noButton = new TaskDialogButton(ButtonType.No);
			td.Buttons.Add(yesButton);
			td.Buttons.Add(noButton);
			var result = td.ShowDialog(App.Current.MainWindow);
			if (result == noButton)
				return;

			App.WriteEventMessage(string.Format("Deleting {1} items", selectedCount));
			var e = new GetItemsToDeleteEventArgs();
			if (GetItemsToDelete != null)
				GetItemsToDelete(this, e);
			if (e.Items == null || !e.Items.Any())
				e.Items.Add(SelectedItem);

			foreach (var item in e.Items)
			{
				App.WriteEventMessage(string.Format("Deleting {0}", item.Name));
				var isDeleted = Manager.DeleteItem(item.Item);
				if (isDeleted)
				{
					PermissionItems.Remove(item);
					SelectedItem = null;
					HasChanges = true;
				}
			}
		}

		private void EditUser()
		{
			if (ExecuteCommand != null)
				ExecuteCommand(this, new ExecuteUIActionEventArgs("ShowEditWindow", null));
		}

		private void Exit()
		{
			if (HasChanges)
			{
				var result = MustSaveChanges();
				if (result.HasValue)
				{
					if (result.GetValueOrDefault())
						Save();
					else
					{
						Manager = null;
						FileName = null;
						HasChanges = false;
						PermissionsAreEnabled = false;
						PermissionItems = null;
					}
				}
				else
					return;
			}
			App.Current.Shutdown();
		}

		private void HelpAbout()
		{
			var aboutWin = new AboutWindow
			{
				Owner = App.Current.MainWindow,
				WindowStartupLocation = WindowStartupLocation.CenterOwner
			};
			aboutWin.ShowDialog();
		}

		private void Manager_ItemAdded(object sender, ItemAddedEventArgs e)
		{
			var p = new PermissionItemSelector(Manager.Permissions.FirstOrDefault(x => x.Name.Equals(e.Name, StringComparison.OrdinalIgnoreCase)));
			p.PropertyChanged += p_PropertyChanged;
			PermissionItems.Add(p);
		}

		private void Manager_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "LastException":
					var td = new TaskDialog
					{
						AllowDialogCancellation = true,
						ButtonStyle = TaskDialogButtonStyle.Standard,
						CenterParent = true,
						MainIcon = TaskDialogIcon.Error,
						MainInstruction = string.Format("An unhandled exception has occurred in the application. The exception message is \"{0}\"\r\rClose the application?", (sender as PermissionManager).LastException.Message),
						ExpandedInformation = string.Format("StackTrace\r{0}\r\r{1}", (sender as PermissionManager).LastException.StackTrace, "If you currently have a permission file open, it is suggested you close the application without saving the permissions file to avoid corruption."),
						MinimizeBox = false,
						WindowTitle = "Application exception"
					};
					var yesButton = new TaskDialogButton(ButtonType.Yes);
					var noButton = new TaskDialogButton(ButtonType.No);
					td.Buttons.Add(yesButton);
					td.Buttons.Add(noButton);
					var result = td.ShowDialog(App.Current.MainWindow);
					if (result == yesButton)
						App.Current.Shutdown();
					break;
			}
		}

		private bool? MustSaveChanges()
		{
			var td = new TaskDialog
			{
				AllowDialogCancellation = true,
				ButtonStyle = TaskDialogButtonStyle.Standard,
				CenterParent = true,
				MainIcon = TaskDialogIcon.Warning,
				MainInstruction = "The data in the permissions file has changed. Would you like to save the loaded permissions file?",
				ExpandedInformation = FileName,
				MinimizeBox = false,
				WindowTitle = "Permissions has changed"
			};
			var yesButton = new TaskDialogButton(ButtonType.Yes);
			var noButton = new TaskDialogButton(ButtonType.No);
			var cancelButton = new TaskDialogButton(ButtonType.Cancel);
			td.Buttons.Add(yesButton);
			td.Buttons.Add(noButton);
			td.Buttons.Add(cancelButton);
			var result = td.ShowDialog(App.Current.MainWindow);
			if (result == yesButton)
				return true;
			else if (result == noButton)
				return false;
			return null;
		}

		private void New()
		{
			if (HasChanges)
			{
				var saveResult = MustSaveChanges();
				if (!saveResult.HasValue)
					return;
				if (saveResult.GetValueOrDefault())
					Save();
			}
			Close();
			SaveAs();
		}

		private void Open()
		{
			if (HasChanges)
			{
				var saveResult = MustSaveChanges();
				if (!saveResult.HasValue)
					return;
				if (saveResult.GetValueOrDefault())
					Save();
			}
			var ofd = new VistaOpenFileDialog
			{
				AddExtension = true,
				CheckFileExists = true,
				CheckPathExists = true,
				DefaultExt = ".userdata",
				FileName = string.IsNullOrEmpty(FileName) ? string.Empty : FileName,
				Filter = "Users files|*.userdata|Xml files|*.xml",
				FilterIndex = LastFilterIndex,
				InitialDirectory = string.IsNullOrEmpty(LastDirectory) ? Environment.GetFolderPath(Environment.SpecialFolder.Desktop) : LastDirectory,
				ShowReadOnly = false,
				Multiselect = false,
				Title = "Select permissions file..."
			};
			var result = ofd.ShowDialog(App.Current.MainWindow);
			if (!result.GetValueOrDefault())
				return;
			OpenFile(ofd.FileName);
		}

		private void OpenFile(string fileName)
		{
			if (HasChanges)
			{
				var saveResult = MustSaveChanges();
				if (!saveResult.HasValue)
					return;
				if (saveResult.GetValueOrDefault())
					Save();
			}

			RecentMenuItems = ApplicationSettings.AddRecentItem(fileName);

			FileName = fileName;
			LastDirectory = Path.GetDirectoryName(FileName);
			LastFilterIndex = Path.GetExtension(FileName).Equals(".xml") ? 2 : 1;
			PermissionManager man = new User_Manager.Classes.Manager();
			try
			{
				var t = man.LoadFileAsync(FileName);
				t.Start();
				t.Wait();
			}
			catch (Exception ex)
			{
				var errorDisplayed = false;
				while (ex != null)
				{
					if (ex is PermissionRootElementMissingException)
					{
						var td = new TaskDialog
						{
							AllowDialogCancellation = true,
							ButtonStyle = TaskDialogButtonStyle.Standard,
							CenterParent = true,
							MainIcon = TaskDialogIcon.Warning,
							MainInstruction = ex.Message,
							ExpandedInformation = FileName,
							MinimizeBox = false,
							WindowTitle = "File error"
						};
						var okButton = new TaskDialogButton(ButtonType.Ok);
						td.Buttons.Add(okButton);
						td.ShowDialog(App.Current.MainWindow);
						errorDisplayed = true;
						break;
					}
					ex = ex.InnerException;
				}
				if (!errorDisplayed)
					throw;
				else
					return;
			}
			this.Manager = man;
			this.Manager.PropertyChanged += Manager_PropertyChanged;
			((PermissionManager)this.Manager).ItemAdded += Manager_ItemAdded;
			IsWebFile = false;
			HasChanges = false;
		}

		private void OpenLogFolder()
		{
			Process.Start("explorer.exe", string.Format("\"{0}\"", App.LogFolder));
		}

		private void OpenUserSecurity()
		{
			OpenFile(LocalUserSecurityFile);
		}

		private bool OpenWebConfigFile(string fileName)
		{
			LastWebConfigFile = fileName;
			var wcWin = new EditWebConfigWindow
			{
				Owner = App.Current.MainWindow,
				WindowStartupLocation = WindowStartupLocation.CenterOwner
			};
			wcWin.View.FileName = LastWebConfigFile;
			var winResult = wcWin.ShowDialog();
			if (!winResult.GetValueOrDefault())
				return true;
			RecentMenuItems = ApplicationSettings.AddRecentItem(LastWebConfigFile);
			App.WriteEventMessage(string.Format("Writing password to {0}, key {1}", LastWebConfigFile, wcWin.View.SelectedSetting));
			ISNCPermissionManager man = null;
			if (Manager != null)
				man = Manager;
			else
				man = new User_Manager.Classes.Manager();
			var newValue = man.HashString(wcWin.View.NewValue);
			var doc = XDocument.Load(LastWebConfigFile);
			var appSettingsElement = doc.Root.Element("appSettings");
			if (appSettingsElement != null)
			{
				foreach (var item in appSettingsElement.Elements())
				{
					if (item.Attribute("key") != null && item.Attribute("key").Value == wcWin.View.SelectedSetting)
					{
						item.Attribute("value").Value = newValue;
						break;
					}
				}
			}
			doc.Save(LastWebConfigFile);
			return false;
		}

		private void OpenWebConfig()
		{
			var ofd = new VistaOpenFileDialog
			{
				AddExtension = true,
				CheckFileExists = true,
				CheckPathExists = true,
				DefaultExt = ".config",
				FileName = "web.config",
				Filter = "Web.config files|*.config",
				FilterIndex = LastFilterIndex,
				InitialDirectory = string.IsNullOrEmpty(LastWebConfigFile) ? Environment.GetFolderPath(Environment.SpecialFolder.Desktop) : Path.GetDirectoryName(LastWebConfigFile),
				ShowReadOnly = false,
				Multiselect = false,
				Title = "Select web.config file..."
			};
			var result = ofd.ShowDialog(App.Current.MainWindow);
			if (!result.GetValueOrDefault())
				return;
			OpenWebConfigFile(ofd.FileName);
		}

		private void OpenWebService()
		{
			if (ExecuteCommand != null)
				ExecuteCommand(this, new ExecuteUIActionEventArgs("OpenWebService", null));
		}

		private void Options()
		{
			if (ExecuteCommand != null)
				ExecuteCommand(this, new ExecuteUIActionEventArgs("ShowOptionsWindow", null));
		}

		private void p_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			App.WriteEventMessage(string.Format("Permission changed for {0} ({1})", SelectedItem.Name, string.Join(",", sender.As<PermissionItemSelector>().Permissions.Select(x => x.Authorization))));
			HasChanges = true;
		}

		private void Paste()
		{
			var data = (string)Clipboard.GetData("XElement");
			var element = XElement.Parse(data);
			element.Elements().ToList().ForEach(x =>
			{
				PermissionItem item = null;
				if (x.Attribute(Manager.ItemTypeAttributeName).Value == "Role")
					item = Role.FromXElement(x, Manager);
				else if (x.Attribute(Manager.ItemTypeAttributeName).Value == "User")
					item = User.FromXElement(x, Manager);
				Manager.AddItem(item);
			});
		}

		private void PermissionItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			HasChanges = true;
		}

		private void Print()
		{
			if (ExecuteCommand != null)
				ExecuteCommand(this, new ExecuteUIActionEventArgs("ShowPrintDialog", null));
		}

		private void PrintSetup()
		{
			if (ExecuteCommand != null)
				ExecuteCommand(this, new ExecuteUIActionEventArgs("ShowPrinterSettingsDialog", null));
		}

		private void Save()
		{
			if (string.IsNullOrEmpty(FileName) && !IsWebFile)
			{
				SaveAs();
				return;
			}
			var t = Manager.SaveFileAsync(FileName, Manager.GetPermissionXElement().ToString(), IsWebFile);
			t.Start();
			t.Wait();
			HasChanges = false;
		}

		private void SaveAs()
		{
			var ofd = new VistaSaveFileDialog
			{
				AddExtension = true,
				CheckFileExists = false,
				CheckPathExists = true,
				DefaultExt = ".userdata",
				FileName = string.IsNullOrEmpty(FileName) ? string.Empty : FileName,
				Filter = "Users files|*.userdata|Xml files|*.xml",
				FilterIndex = LastFilterIndex,
				InitialDirectory = string.IsNullOrEmpty(LastDirectory) ? Environment.GetFolderPath(Environment.SpecialFolder.Desktop) : LastDirectory,
				OverwritePrompt = true,
				Title = "Select permissions file..."
			};
			var result = ofd.ShowDialog(App.Current.MainWindow);
			if (!result.GetValueOrDefault())
				return;
			FileName = ofd.FileName;
			LastDirectory = Path.GetDirectoryName(FileName);
			LastFilterIndex = Path.GetExtension(FileName).Equals(".xml") ? 2 : 1;
			ISNCPermissionManager man = null;
			if (this.Manager == null)
			{
				man = new Manager();
				man.Load(User_Manager.Classes.Manager.GetNewFileElement((Manager)man));
			}
			else
				man = this.Manager;
			var t = man.SaveFileAsync(FileName, man.OriginalXmlData);
			t.Start();
			t.Wait();
			Manager = man;
			Manager.PropertyChanged += Manager_PropertyChanged;
			((PermissionManager)this.Manager).ItemAdded += Manager_ItemAdded;
			HasChanges = false;
		}

		private void SetPermissionsForItem()
		{
			if (SelectedItem == null)
				return;
			Permissions = SelectedItem.Permissions;
			Applications = SelectedItem.Applications;
			SpecialFlags = SelectedItem.SpecialFlags;
		}

		private void UpdateInterface()
		{
			ManageToolbarVisibility = !string.IsNullOrEmpty(FileName) ? Visibility.Visible : Visibility.Collapsed;
			EditorVisibility = !string.IsNullOrEmpty(FileName) ? Visibility.Visible : Visibility.Collapsed;
			HasChangeVisibility = HasChanges ? Visibility.Visible : Visibility.Collapsed;
			NoChangeVisibility = HasChanges ? Visibility.Collapsed : Visibility.Visible;

			PermissionsAreEnabled = SelectedItem != null && !SelectedItem.IsReadOnly;

			SaveCommand.IsEnabled = HasChanges;
			CloseCommand.IsEnabled = !string.IsNullOrEmpty(FileName);
			SaveAsCommand.IsEnabled = HasChanges || !string.IsNullOrEmpty(FileName);
			PrintCommand.IsEnabled = !string.IsNullOrEmpty(FileName);
			EditUserCommand.IsEnabled = SelectedItem != null && !SelectedItemIsSystemAdmin;
			DeleteUserCommand.IsEnabled = SelectedItem != null && !SelectedItemIsSystemAdmin;
			CutCommand.IsEnabled = SelectedItem != null && !SelectedItemIsSystemAdmin;
			CopyCommand.IsEnabled = SelectedItem != null && !SelectedItemIsSystemAdmin;
			PasteCommand.IsEnabled = !string.IsNullOrEmpty(FileName) && ClipboardContainsItems;
			AddUserCommand.IsEnabled = !string.IsNullOrEmpty(FileName);
		}

		#endregion
	}
}
