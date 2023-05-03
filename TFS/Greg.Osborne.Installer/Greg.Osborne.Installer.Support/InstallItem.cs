namespace Greg.Osborne.Installer.Support {
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Drawing;
	using System.IO;
	using System.Windows.Media;
	using System.Xml.Linq;
	using GregOsborne.Application.Media;
	using GregOsborne.Application.Primitives;

	public class InstallItem : INotifyPropertyChanged {
		public static InstallItem FromValues(string title, IconColors iconColor, Version version, string description, Guid id, string iconPath = null, Icon customIcon = null) {
			var result = new InstallItem {
				Title = title,
				IconColor = iconColor,
				Version = version,
				Description = description,
				CustomIcon = customIcon,
				IconPath = iconPath,
				Id = id
			};
			return result;
		}

		public static InstallItem FromXElement(XElement element) {
			var id = Guid.Parse(element.Attribute("id").Value);
			var title = element.Element("title").Value;
			var description = element.Element("description").Value;
			var iconColor = (IconColors)Enum.Parse(typeof(IconColors), element.Element("icon").Value, true);
			var version = Version.Parse(element.Element("version").Value);
			var icn = default(Icon);
			var iconPath = default(string);
			if (iconColor == IconColors.Custom) {
				iconPath = element.Element("iconpath").Value;
				if (File.Exists(iconPath)) {
					icn = typeof(InstallItem).Assembly.GetIconByFileName(iconPath, new System.Drawing.Size(128, 128));
				}
			}
			return FromValues(title, iconColor, version, description, id, iconPath, icn);
		}

		private string iconPath = default;
		public string IconPath {
			get => this.iconPath;
			set {
				this.iconPath = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}
		public enum IconColors {
			Custom,
			Blue,
			Brown,
			DarkGreen,
			Gray,
			Green,
			LightGreen,
			Orange,
			Red,
			Teal
		}

		public enum InstallStatuses {
			Install,
			Skip
		}

		private InstallItem() {
			this.InstallationStatuses = new List<InstallStatuses>();
			this.InstallationStatuses.AddRange((InstallStatuses[])Enum.GetValues(typeof(InstallStatuses)));
		}
		public event EventHandler StatusChanged;

		public override string ToString() => this.Title;
		public string Title { get; private set; } = default;
		public IconColors IconColor { get; private set; } = default;
		public Version Version { get; private set; } = default;
		public Icon CustomIcon { get; private set; } = default;
		public string Description { get; private set; } = default;
		public List<InstallStatuses> InstallationStatuses { get; private set; } = default;
		public Guid Id { get; private set; } = default;

		private InstallStatuses status = default;
		public InstallStatuses Status {
			get => this.status;
			set {
				this.status = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public XElement ToXElement() {
			var result = new XElement("item",
				new XAttribute("id", this.Id),
				new XElement("title", this.Title),
				new XElement("description",
					new XCData(this.Description)),
				new XElement("icon", this.IconColor),
				new XElement("version", this.Version));
			if (this.IconColor == IconColors.Custom) {
				result.Add(new XElement("iconpath", this.IconPath));
			}
			return result;
		}

		private ImageSource iconSource = default;
		public ImageSource IconSource {
			get {
				if (this.iconSource == null) {
					var size = new System.Windows.Size(128, 128);
					switch (this.IconColor) {
						case IconColors.Brown: {
							this.iconSource = this.GetType().Assembly.GetIconFromEmbeddedResource("Setup_Icon_Brown", size).ToImageSource();
						}
						break;
						case IconColors.DarkGreen: {
							this.iconSource = this.GetType().Assembly.GetIconFromEmbeddedResource("Setup_Icon_Dark_Green", size).ToImageSource();
						}
						break;
						case IconColors.Gray: {
							this.iconSource = this.GetType().Assembly.GetIconFromEmbeddedResource("Setup_Icon_Gray", size).ToImageSource();
						}
						break;
						case IconColors.Green: {
							this.iconSource = this.GetType().Assembly.GetIconFromEmbeddedResource("Setup_Icon_Green", size).ToImageSource();
						}
						break;
						case IconColors.LightGreen: {
							this.iconSource = this.GetType().Assembly.GetIconFromEmbeddedResource("Setup_Icon_Light_Green", size).ToImageSource();
						}
						break;
						case IconColors.Orange: {
							this.iconSource = this.GetType().Assembly.GetIconFromEmbeddedResource("Setup_Icon_Orange", size).ToImageSource();
						}
						break;
						case IconColors.Red: {
							this.iconSource = this.GetType().Assembly.GetIconFromEmbeddedResource("Setup_Icon_Red", size).ToImageSource();
						}
						break;
						case IconColors.Teal: {
							this.iconSource = this.GetType().Assembly.GetIconFromEmbeddedResource("Setup_Icon_Teal", size).ToImageSource();
						}
						break;
						case IconColors.Custom: {
							if (this.CustomIcon == null) {
								this.IconColor = IconColors.Blue;
								return this.IconSource;
							}
							this.iconSource = this.CustomIcon.ToImageSource();
						}
						break;
						default: {
							// blue
							this.iconSource = this.GetType().Assembly.GetIconFromEmbeddedResource("Setup_Icon_Blue", size).ToImageSource();
						}
						break;
					}
				}
				return this.iconSource;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void InvokePropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
