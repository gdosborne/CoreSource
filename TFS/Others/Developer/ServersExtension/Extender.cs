namespace SNC.OptiRamp.Application.Developer.Extensions.ServersExtension {

	using MVVMFramework;
	using SNC.OptiRamp.Application.DeveloperEntities.Configuration;
	using SNC.OptiRamp.Application.DeveloperEntities.IO;
	using SNC.OptiRamp.Application.DeveloperEntities.Management;
	using GregOsborne.Application.Primitives;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.Composition;
	using System.Linq;
	using System.Windows.Controls.Ribbon;

	[Export(typeof(IExtender))]
	public class Extender : IExtender {
		public void UpdateInterface() {
			ShowServersCommand.RaiseCanExecuteChanged();
		}

		#region Public Constructors
		public Extender() {
			Name = "Computers Extension";
			_Control = new ExtensionControl();
			OptionsCategory = new Category {
				Title = "Computers Extension"
			};
			ExportedCommands = new List<DelegateCommand>();
		}
		#endregion Public Constructors

		#region Public Methods
		public void AddRibbonItems(System.Windows.Controls.Ribbon.Ribbon ribbon) {
			foreach (var tabItem in ribbon.Items) {
				if (tabItem is RibbonTab) {
					if (tabItem.As<RibbonTab>().Header is string) {
						if (((string)tabItem.As<RibbonTab>().Header).Equals("Tools")) {
							var rg = new RibbonGroup {
								Name = "ComputersGroup",
								Header = "Computers"
							};
							rg.Items.Add(RibbonHelper.GetButton(this.GetType(), ShowServersCommand, "Manager", "Manages computers in the project file.", "server.png"));
							tabItem.As<RibbonTab>().Items.Add(rg);
							break;
						}
					}
				}
			}
		}
		#endregion Public Methods

		#region Private Methods
		private void ShowServers(object state) {
			if (_Control != null && ShowUserControl != null)
				ShowUserControl(this, new ShowUserControlEventArgs(_Control));
		}
		private bool ValidateShowServersState(object state) {
			return ProjectFile != null;
		}
		#endregion Private Methods

		#region Public Events
		public event ShowUserControlHandler ShowUserControl;
		public event ProjectChangedHandler ProjectChanged;
		#endregion Public Events

		#region Private Fields
		private ExtensionControl _Control = null;
		private IList<DelegateCommand> _ExportedCommands = null;
		private string _Name = string.Empty;
		private Category _OptionsCategory = null;
		private ProjectFile _Project = null;
		private DelegateCommand _ShowServersCommand = null;
		#endregion Private Fields

		#region Public Properties
		public IList<DelegateCommand> ExportedCommands {
			get {
				return _ExportedCommands;
			}
			private set {
				_ExportedCommands = value;
			}
		}
		public string Name {
			get {
				return _Name;
			}
			private set {
				_Name = value;
			}
		}
		public Category OptionsCategory {
			get {
				return _OptionsCategory;
			}
			private set {
				_OptionsCategory = value;
			}
		}

		public ProjectFile ProjectFile {
			get {
				return _Project;
			}
			set {
				_Project = value;
			}
		}
		#endregion Public Properties

		#region Private Properties
		private DelegateCommand ShowServersCommand {
			get {
				if (_ShowServersCommand == null)
					_ShowServersCommand = new DelegateCommand(ShowServers, ValidateShowServersState);
				return _ShowServersCommand as DelegateCommand;
			}
		}
		#endregion Private Properties
	}
}
