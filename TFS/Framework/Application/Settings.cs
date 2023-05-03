namespace GregOsborne.Application {
	using System;
	using System.Collections.Generic;
	using System.Configuration;
	using System.IO;
	using System.Linq;
	using System.Windows;
	using System.Windows.Media;
	using System.Xml.Linq;
	using GregOsborne.Application.Media;
	using GregOsborne.Application.Primitives;

	public class Settings : XmlInfrastructureFile {
		public Settings(string name) {
			this.ApplicationName = name;
			this.AllKeys = new List<string>();
		}

		public string[] GetNames() => this.AllKeys.ToArray();

		public static void ApplyWindowBounds(string appName, Window window) {
			var s = new Settings(appName);
			window.Left = s.GetValue(window.GetType().Name, "Position.Left", !double.IsNaN(window.Left) || !double.IsInfinity(window.Left) ? 0 : window.Left);
			window.Top = s.GetValue(window.GetType().Name, "Position.Top", !double.IsNaN(window.Top) || !double.IsInfinity(window.Top) ? 0 : window.Top);
			window.Width = s.GetValue(window.GetType().Name, "Position.Width", !double.IsNaN(window.Width) || !double.IsInfinity(window.Width) ? 800.0 : window.Width);
			window.Height = s.GetValue(window.GetType().Name, "Position.Height", !double.IsNaN(window.Height) || !double.IsInfinity(window.Height) ? 600.0 : window.Height);
			window.WindowState = s.GetValue(window.GetType().Name, "WindowState", WindowState.Normal);
		}

		public static void SaveWindowBounds(string appName, Window window) {
			var s = new Settings(appName);
			s.AddOrUpdateSetting(window.GetType().Name, "Position.Left", window.RestoreBounds.Left);
			s.AddOrUpdateSetting(window.GetType().Name, "Position.Top", window.RestoreBounds.Top);
			s.AddOrUpdateSetting(window.GetType().Name, "Position.Width", window.RestoreBounds.Width);
			s.AddOrUpdateSetting(window.GetType().Name, "Position.Height", window.RestoreBounds.Height);
			s.AddOrUpdateSetting(window.GetType().Name, "WindowState", window.WindowState);
		}

		private static XDocument xDocument = default;
		private static string xDocumentFileName = default;
		private object locker = new object();
		private ApplicationSettingsBase applicationSettingsType = default;
		public ApplicationSettingsBase ApplicationSettingsType {
			get => this.applicationSettingsType;
			set {
				if (value == null) {
					return;
				}

				this.applicationSettingsType = value;
				var sectionName = default(string);
				var settingName = default(string);
				foreach (SettingsProperty prop in value.Properties) {
					sectionName = "Application";
					if (prop.Name.Contains("_")) {
						sectionName = prop.Name.Split('_')[0];
						settingName = prop.Name.Split('_')[1];
					} else {
						settingName = prop.Name;
					}

					this.AddOrUpdateSetting(sectionName, settingName, prop.DefaultValue);
				}
			}
		}

		private static Settings Create(string applicationName, string fileName) {
			var dir = Path.GetDirectoryName(fileName);
			if (!Directory.Exists(dir)) {
				Directory.CreateDirectory(dir);
			}

			if (!File.Exists(fileName)) {
				new XDocument(new XElement("settings")).Save(fileName);
			}

			var result = new Settings(applicationName) {
				ActualFileName = fileName
			};
			xDocumentFileName = fileName;
			var xDoc = XDocument.Load(result.ActualFileName);
			var xRoot = xDoc.Root;
			xRoot.Elements().ToList().ForEach(x => {
				var section = x.Attribute("name").Value;
				x.Elements().ToList().ForEach(y => {
					var key = y.Attribute("name").Value;
					var value = y.Attribute("value").Value;
					var type = y.Attribute("type").Value;
					if (string.IsNullOrEmpty(type)) {
						return;
					}

					result.AllKeys.Add($"{section}.{key}");

					var theValue = default(object);
					try {
						var t = Type.GetType(type.ToLowerInvariant(), true, true);
						if (t.IsEnum) {
							var s = new Settings(applicationName);
							var mi = s.GetType().GetMethods().FirstOrDefault(m => m.Name == "GetLocalValue");
							mi = mi.MakeGenericMethod(t);
							var args = new object[] {
							y, 0
						};
							var objResult = mi.Invoke(s, args);
							if (objResult != null) {
								theValue = Enum.Parse(t, objResult.ToString(), true);
							}
						} else if (t == typeof(string)) {
							theValue = value;
						} else {
							var mi = t.GetMethods().FirstOrDefault(m => m.Name == "TryParse");
							var args = new object[] {
							value, null
						};
							var objResult = (bool)mi.Invoke(null, args);
							if (objResult) {
								theValue = args[1];
							}
						}
						result.AddOrUpdateSetting($"{applicationName}|{section}|{key}", theValue.GetType(), theValue, false);
					}
					catch (System.Exception) {

					}
				});
			});
			return result;
		}

		public static Settings CreateFromOtherSettingsFile(string applicationName, string fileName) => Create(applicationName, fileName);

		public static Settings CreateFromApplicationSettingsFile(string applicationName, string applicationDirectory) => Create(applicationName, GetFileName(applicationDirectory, FileTypes.Settings));

		public string GetValueAsString(string sectionName, string keyName) {
			var key = this.GetKey($"{this.ApplicationName}|{sectionName}|{keyName}", out var isNewKey);
			var value = this.GetLocalValue<string>(key, null);
			return value;
		}

		public T1 GetValue<T1>(string sectionName, string keyName, T1 defaultValue = default) {
			var val = this.GetValue($"{this.ApplicationName}|{sectionName}|{keyName}", defaultValue);
			if (val.Is<string>() && ((string)(object)val) == "@null") {
				return defaultValue;
			}

			return val;
		}

		public void RemoveSetting(string sectionName, string keyName) {
			var name = $"{this.ApplicationName}|{sectionName}|{keyName}";
			var key = this.GetKey(name, out var isNewKey);
			if (key == null) {
				return;
			}

			var xDoc = key.Document;
			key.Remove();
			var parts = name.Split('|');
			xDoc.Save(xDocumentFileName);
			this.AllKeys.Remove($"{sectionName}.{keyName}");
			SettingsAction?.Invoke(this, new SettingsActionEventArgs(isNewKey ? Actions.Add : Actions.Update, parts[0], parts[1], parts[2]));
		}

		public bool KeyExists(string sectionName, string keyName) => this.GetKey($"{this.ApplicationName}|{sectionName}|{keyName}", out var isNewKey) != null && !isNewKey;

		public Settings AddOrUpdateSetting(string section, string name, object value) {
			this.AddOrUpdateSetting($"{this.ApplicationName}|{section}|{name}", value.GetType(), value, true);
			return this;
		}

		private void AddOrUpdateSetting(string name, Type type, object value, bool saveImmediate) {
			var key = this.GetKey(name, out var isNewKey);
			if (value == null || key == null) {
				return;
			}

			key.Attribute("type").Value = type.FullName;
			var parts = name.Split('|');
			key.Attribute("value").Value = value.ToString();
			var tryNumber = 0;
			if (saveImmediate) {
				while (tryNumber < 10) {
					try {
						key.Document.Save(xDocumentFileName);
						break;
					}
					catch {
						tryNumber++;
						System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
					}
				}
			}
			SettingsAction?.Invoke(this, new SettingsActionEventArgs(isNewKey ? Actions.Add : Actions.Update, parts[0], parts[1], parts[2], value));
		}

		private XElement GetKey(string name, out bool isNewKey) {
			var parts = name.Split('|');
			var appName = parts[0];
			var sectionName = parts[1];
			var keyName = parts[2];
			isNewKey = false;
			//var xDoc = default(XDocument);
			if (string.IsNullOrEmpty(xDocumentFileName)) {
				try {
					var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), this.ApplicationName, "Settings");
					if (!Directory.Exists(dir)) {
						Directory.CreateDirectory(dir);
					}
					this.ActualFileName = $"{dir}\\{name.Split('|')[0]}.xml";
				}
				catch (System.Exception ex) {
					ex.Data.Add("FileExists", this.SettingsFileExists);
					throw ex;
				}
				xDocumentFileName = this.ActualFileName;
			}
			if (xDocument == default) {
				xDocument = this.SettingsFileExists ? XDocument.Load(xDocumentFileName) : new XDocument(new XElement("settings"));
			}
			var xKey = default(XElement);
			if (xDocument != null) {
				var xSection = xDocument.Root.Elements().FirstOrDefault(x => x.Attribute("name").Value.Equals(sectionName));
				if (xSection == null) {
					xSection = new XElement("section", new XAttribute("name", sectionName));
					xDocument.Root.Add(xSection);
					isNewKey = true;
				}
				xKey = xSection.Elements().FirstOrDefault(x => x.Attribute("name").Value.Equals(keyName));
				if (xKey == null) {
					isNewKey = true;
					xKey = new XElement("setting",
							new XAttribute("name", keyName),
							new XAttribute("type", string.Empty),
							new XAttribute("value", string.Empty));
					xSection.Add(xKey);
					this.AllKeys.Add($"{sectionName}.{keyName}");
				}
				xDocument.Save(xDocumentFileName);
			}
			return xKey;
		}

		internal T1 GetLocalValue<T1>(XElement xKey, T1 defaultValue) {
			var valueAttr = xKey.Attribute("value");
			var typeAttr = xKey.Attribute("type");
			var nameAttr = xKey.Attribute("name");
			var theValue = (object)defaultValue;
			var theName = nameAttr.Value;
			var theType = typeof(T1);
			var isUsingDefault = string.IsNullOrEmpty(valueAttr.Value) && !string.IsNullOrEmpty(defaultValue as string);

			if (valueAttr != null) {
				if (!string.IsNullOrEmpty(valueAttr.Value)) {
					theValue = valueAttr.Value;
					isUsingDefault = false;
				}

				var hasValue = theValue != null || !string.IsNullOrEmpty(theValue as string);
				if (!hasValue) {
					theValue = defaultValue as string;
					if (string.IsNullOrEmpty(theValue as string)) {
						theValue = string.Empty;
					}

					isUsingDefault = true;
				}
				if (theType.IsEnum) {
					var test = theValue.ToString();
					if (test.Contains("|")) {
						test = test.Replace("|", ", ");
					}

					theValue = Enum.Parse(theType, test, true);
				} else {
					switch (theType.Name.ToLowerInvariant()) {
						case "system.timespan":
							theType = typeof(TimeSpan);
							theValue = TimeSpan.Parse(theValue.ToString());
							break;
						case "fontfamily":
							theType = typeof(FontFamily);
							theValue = new FontFamily(theValue.ToString());
							break;
						case "color":
							theType = typeof(Color);
							theValue = theValue.ToString().ToColor();
							break;
						//case "point":
						//    theType = typeof(Point);
						//    theValue = PointCollection.Parse((string)theValue);
						//    break;
						default:
							theValue = Convert.ChangeType(theValue, theType);
							break;
					}
					if (isUsingDefault) {
						valueAttr.Value = theValue.ToString();
						typeAttr.Value = theType.FullName;
					}
				}
			} else {
				xKey.Add(new XAttribute("value", theValue));
			}

			if (theValue == null || theValue.Equals("@null")) {
				return defaultValue;
			}

			xDocument.Save(xDocumentFileName);
			return (T1)theValue;
		}

		private T1 GetValue<T1>(string name, T1 defaultValue) {
			var key = this.GetKey(name, out var isNewKey);
			if (key == null) {
				return defaultValue;
			}

			return this.GetLocalValue(key, defaultValue);
		}

		public List<string> AllKeys {
			get; private set;
		}

		public event SettingsActionHandler SettingsAction;
	}
}
