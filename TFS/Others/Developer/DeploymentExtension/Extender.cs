namespace SNC.OptiRamp.Application.Developer.Extensions.DeploymentExtension {

	using SNC.OptiRamp.Application.DeveloperEntities.Configuration;
	using SNC.OptiRamp.Application.DeveloperEntities.IO;
	using SNC.OptiRamp.Application.DeveloperEntities.Management;
	using GregOsborne.Application.Primitives;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.Composition;
	using System.Linq;
	using System.Windows.Controls.Ribbon;
    using GregOsborne.MVVMFramework;

    [Export(typeof(IExtender))]
	public class Extender : IExtender {
		public void UpdateInterface() {
			DeployCommand.RaiseCanExecuteChanged();
		}
		#region Public Constructors
		public Extender() {
			Name = "Deployment Extension";
			OptionsCategory = new Category {
				Title = "Deployment Extension"
			};

			ExportedCommands = new List<DelegateCommand> {
				DeployCommand
			};
		}
		#endregion Public Constructors

		#region Public Methods
		public void AddRibbonItems(System.Windows.Controls.Ribbon.Ribbon ribbon) {
			foreach (var tabItem in ribbon.Items) {
				if (tabItem is RibbonTab) {
					if (tabItem.As<RibbonTab>().Header is string) {
						if (((string)tabItem.As<RibbonTab>().Header).Equals("Tools")) {
							var rg = new RibbonGroup {
								Name = "DeployGroup",
								Header = "Deploy"
							};
							rg.Items.Add(RibbonHelper.GetButton(this.GetType(), DeployCommand, "Upload", "Uploads the project to the web development server.", "deployProject.png"));
							tabItem.As<RibbonTab>().Items.Add(rg);
							break;
						}
					}
				}
			}
		}
		#endregion Public Methods

		#region Private Methods
		private void Deploy(object state) {
			//do deployment
		}
		private bool ValidateDeployState(object state) {
			return ProjectFile != null;
		}
		#endregion Private Methods

		#region Public Events
		public event ShowUserControlHandler ShowUserControl;
		public event ProjectChangedHandler ProjectChanged;
		#endregion Public Events

		#region Private Fields
		private DelegateCommand _DeployCommand = null;
		private IList<DelegateCommand> _ExportedCommands = null;
		private string _Name = string.Empty;
		private Category _OptionsCategory = null;
		private ProjectFile _Project = null;
		private RibbonTab designerTab = null;
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
			set {
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
		public DelegateCommand DeployCommand {
			get {
                if (_DeployCommand == null) { }
					//_DeployCommand = new DelegateCommand(Deploy, ValidateDeployState) {
					//	Name = "DeployProject",
					//	Description = "Deploys the project to the remote server"
					//};
				return _DeployCommand;
			}
		}
		#endregion Private Properties
	}
}
