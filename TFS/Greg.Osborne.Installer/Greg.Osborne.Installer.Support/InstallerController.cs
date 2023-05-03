namespace Greg.Osborne.Installer.Support {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Xml.Linq;
	using Greg.Osborne.Installer.Support.Events;
	using Microsoft.VisualBasic.FileIO;
	using SysIO = System.IO;

	public sealed class InstallerController {
		public event AskForFilenameHandler AskForFilename;
		public event FilenameChangedHandler FilenameHasChanged;
		public static event EventHandler FileLoaded;

		public static InstallerController FromDocument(XDocument doc) {
			var result = default(InstallerController);
			try {
				var root = doc.Root;
				result = new InstallerController();
				root.Elements().ToList().ForEach(x => {
					if (x.Name == "settings") {
						x.Elements().ToList().ForEach(y => {
							var item = result.Settings.FirstOrDefault(z => z.Name == y.Attribute("name").Value);
							if (item == null) {
								var setting = SettingItem.FromXElement(y);
								setting.PropertyChanged += result.Setting_PropertyChanged;
								result.Settings.Add(setting);
							} else {
								item.Value = Convert.ChangeType(y.Value, item.ValueType);
							}
						});
					} else if (x.Name == "actions") {

					} else if (x.Name == "side_items") {
						x.Elements().ToList().ForEach(y => {
							var item = SideItem.FromXElement(y);
							result.SideItems.Add(item);
						});
					} else if (x.Name == "intallation_items") {
						x.Elements().ToList().ForEach(y => {
							result.InstallItems.Add(InstallItem.FromXElement(y));
						});
					}
				});
				FileLoaded?.Invoke(result, EventArgs.Empty);
			}
			catch (Exception ex) { throw ex; }
			return result;
		}

		public static InstallerController FromFilename(string filename) {
			if (!SysIO.File.Exists(filename)) {
				//throw new SysIO.FileNotFoundException($"Cannot find controller file {filename}");
				return null;
			}

			try {
				var doc = XDocument.Load(filename);
				var result = FromDocument(doc);				
				result.Filename = filename;
				return result;
			}
			catch (Exception ex) { throw ex; }
		}

		public static InstallerController CreateNew() => new InstallerController();

		public bool IsTempFile {
			get {
				if (string.IsNullOrEmpty(this.Filename)) {
					return false;
				}

				var fi = new SysIO.FileInfo(this.Filename);
				if (!fi.Exists) {
					return false;
				}

				var ext = fi.Extension;
				var result = ext.Equals(".tmp", StringComparison.OrdinalIgnoreCase);
				return result;
			}
		}

		private SettingItem GetSetting(string name, string xmlName, Type type, object value) => new SettingItem {
			Name = name,
			XmlName = xmlName,
			ValueType = type,
			Value = value
		};

		private InstallerController() {
			//these are default values for settings - they will be overwritten if we are opening a file
			this.Settings = new List<SettingItem> {
				this.GetSetting("Installation Header Text",  "installation_header_text", typeof(string), "None"),
				this.GetSetting("Products Tab Text", "products_tab_text", typeof(string), "Products"),
				this.GetSetting("Options Tab Text", "options_tab_text", typeof(string), "Options"),
				this.GetSetting("Window to Screen Size Ratio", "window_size_ratio", typeof(double), 0.75),
				this.GetSetting("Silent Installation", "silent_installation", typeof(bool), false),
				this.GetSetting("Welcome Paragraph Text", "welcome_paragraph_text", typeof(string), "Undefined"),
				this.GetSetting("Theme Resource Dictionary", "theme_resource_dictionary", typeof(string), "default")
			};
			this.Settings.ForEach(x => x.PropertyChanged += this.Setting_PropertyChanged);
			this.SideItems = new List<SideItem>();
			this.InstallItems = new List<InstallItem>();
		}

		private void Setting_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) => this.HasChanges = true;

		public void Save() {
			if (string.IsNullOrEmpty(this.Filename)) {
				var e = new AskForFilenameEventArgs();
				AskForFilename?.Invoke(this, e);
				if (string.IsNullOrEmpty(e.Filename)) {
					this.Save(e.Filename);
					return;
				}
				throw new ApplicationException("Filename was not specified for save operation");
			}
			var doc = new XDocument(
				new XElement("installation"));

			var settingsElement = new XElement("settings");
			this.Settings.ForEach(x => settingsElement.Add(x.ToXElement()));
			doc.Root.Add(settingsElement);

			var actionsElement = new XElement("actions");

			doc.Root.Add(actionsElement);

			var sideItemsElement = new XElement("side_items");
			this.SideItems.ForEach(x => sideItemsElement.Add(x.ToXElement()));
			doc.Root.Add(sideItemsElement);

			var installItemsElement = new XElement("installation_items");
			this.InstallItems.ForEach(x => installItemsElement.Add(x.ToXElement()));
			doc.Root.Add(installItemsElement);

			doc.Save(this.Filename);
			this.HasChanges = false;
		}

		public void Save(string filename) {
			if (SysIO.File.Exists(filename)) {
				var backup = $"{filename}.backup";
				if (SysIO.File.Exists(backup)) {
					FileSystem.DeleteFile(backup, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
					//SysIO.File.Delete(backup);
				}

				SysIO.File.Move(filename, backup);
			}
			this.Filename = filename;
			this.Save();
		}

		public bool HasChanges { get; set; }

		private string filename = default;
		public string Filename {
			get => this.filename;
			set {
				var oldName = this.Filename;
				this.filename = value;
				this.FilenameHasChanged?.Invoke(this, new FilenameChangedEventArgs(oldName));
			}
		}

		public List<SettingItem> Settings { get; private set; }

		public List<SideItem> SideItems { get; private set; }

		public List<InstallItem> InstallItems { get; private set; }
	}

}
