namespace Greg.Osborne.Installer {
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Windows;
	using System.Xml.Linq;
	using Greg.Osborne.Installer.Support;
	using GregOsborne.Application.Xml.Linq;

	public partial class App : Application {

		private const int missingInstallationFile = -1;
		private const int silentModeCompleteed = -2;

		protected override void OnStartup(StartupEventArgs e) {

			var insFileName = Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), "Installation.controller.xml");
			if (!File.Exists(insFileName)) {
				//no installer in exe directory - try command line

				Environment.Exit(missingInstallationFile);
			}
			var doc = XDocument.Load(insFileName);
			var root = doc.Root;
			var settingsRoot = root.Element("settings");
			var sideItemsRoot = root.Element("side_items");
			var instItemsRoot = root.Element("installation_items");

			var installationHeaderText = default(string);
			var productsTabText = default(string);
			var optionsTabText = default(string);
			var windowSizeRatio = 0.75;
			var isSilentInstallation = default(bool);
			var welcomeParaText = default(string);
			var sideItems = new List<SideItem>();
			var installItems = new List<InstallItem>();
			var themeResourceDictionary = default(string);

			if (settingsRoot != null) {
				installationHeaderText = settingsRoot.Element("installation_header_text").GetElementValue("Osborne Installer");
				productsTabText = settingsRoot.Element("products_tab_text").GetElementValue("Products Installer");
				optionsTabText = settingsRoot.Element("options_tab_text").GetElementValue("Options");
				windowSizeRatio = settingsRoot.Element("window_size_ratio").GetElementValue(0.75);
				isSilentInstallation = settingsRoot.Element("silent_installation").GetElementValue(false);
				welcomeParaText = settingsRoot.Element("welcome_paragraph_text").GetElementValue("Missing text");
				themeResourceDictionary = settingsRoot.Element("theme_resource_dictionary").GetElementValue(string.Empty);
			}
			if (sideItemsRoot != null) {
				sideItemsRoot.Elements().ToList().ForEach(x => {
					sideItems.Add(SideItem.FromXElement(x));
				});
			}
			if(instItemsRoot != null) {
				instItemsRoot.Elements().ToList().ForEach(x => {
					installItems.Add(InstallItem.FromXElement(x));
				});
			}

			if (isSilentInstallation) {
				Environment.Exit(silentModeCompleteed);
			}

			if (!string.IsNullOrEmpty(themeResourceDictionary)) {
				var dir = Path.GetDirectoryName(themeResourceDictionary);
				if (string.IsNullOrEmpty(dir)) {
					themeResourceDictionary = Path.Combine(Environment.CurrentDirectory, themeResourceDictionary);
				}
				if (File.Exists(themeResourceDictionary)) {
					var dic = new ResourceDictionary {
						Source = new Uri(themeResourceDictionary, UriKind.Absolute)
					};
					Application.Current.Resources.MergedDictionaries.Add(dic);
				}
			}

			var win = new MainWindow();
			win.View.InstallerName = installationHeaderText;
			win.View.ProductsTabText = productsTabText;
			win.View.OptionsTabText = optionsTabText;
			win.View.WindowSizeRatio = windowSizeRatio;
			win.View.InstructionPanelWelcomeParagraph = welcomeParaText;

			sideItems.ForEach(x => win.View.SideItems.Add(x));
			installItems.ForEach(x => win.View.AddInstallationItem(x));
			Application.Current.MainWindow = win;
			win.Show();
		}
	}
}