using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using EnvDTE;
using EnvDTE80;

namespace GregOsborne.AppVersion
{
	// This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
	// a package.
	[PackageRegistration(UseManagedResourcesOnly = true)]
	// This attribute is used to register the information needed to show this package
	// in the Help/About dialog of Visual Studio.
	[InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
	// This attribute is needed to let the shell know that this package exposes some menus.
	[ProvideMenuResource("Menus.ctmenu", 1)]
	// This attribute registers a tool window exposed by this package.
	[ProvideToolWindow(typeof(VersionWindow))]
	[Guid(GuidList.guidAppVersionPkgString)]
	public sealed class AppVersionPackage : Package
	{
		public AppVersionPackage() {
			Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
		}

		// Overridden Package Implementation
		#region Package Members
		private Events solutionEvents;
		protected override void Initialize() {
			Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
			base.Initialize();

			var service1 = (DTE)Package.GetGlobalService(typeof(SDTE));
			var service = (AppVersionPackage)this.GetService(typeof(AppVersionPackage));
			//var svc = (AppVersionPackage)AppVersionPackage.GetGlobalService(typeof(AppVersionPackage));

			solutionEvents = service1.Events;
			//solutionEvents.BuildEvents.OnBuildBegin += BuildEvents_OnBuildBegin;
			solutionEvents.BuildEvents.OnBuildProjConfigBegin += BuildEvents_OnBuildProjConfigBegin;

			// Add our command handlers for menu (commands must exist in the .vsct file)
			OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
			if (null != mcs) {
				// Create the command for the menu item.
				CommandID menuCommandID = new CommandID(GuidList.guidAppVersionCmdSet, (int)PkgCmdIDList.cmdidProjectVersion);
				MenuCommand menuItem = new MenuCommand(MenuItemCallback, menuCommandID);
				mcs.AddCommand(menuItem);
			}
		}
		
		void BuildEvents_OnBuildProjConfigBegin(string Project, string ProjectConfig, string Platform, string SolutionConfig) {
			
		}

		#endregion

		private void MenuItemCallback(object sender, EventArgs e) {
			var win = new VersionWindow();
			win.ShowDialog();
		}

	}
}
