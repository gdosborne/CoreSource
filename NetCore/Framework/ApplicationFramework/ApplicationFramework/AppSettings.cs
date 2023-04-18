using Common.OzApplication.Media;
using Common.OzApplication.Primitives;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace Common.OzApplication {
    /// <summary>
    /// The settings.
    /// </summary>
    public class AppSettings : XmlInfrastructureFile {
        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public AppSettings(string name) {
            ApplicationName = name;
            AllKeys = new List<string>();
        }

        /// <summary>
        /// Gets the application name.
        /// </summary>
        public string ApplicationName { get; private set; } = null;
        public event SettingsActionHandler SettingsAction;

        private XDocument xDocument = null;
        internal string xDocumentFileName = default;
        private ApplicationSettingsBase applicationSettingsType = default;

        /// <summary>
        /// Gets the all keys.
        /// </summary>
        public List<string> AllKeys { get; private set; }

        /// <summary>
        /// Gets or sets the application settings type.
        /// </summary>
        public ApplicationSettingsBase ApplicationSettingsType {
            get => applicationSettingsType;
            set {
                if (value == null) {
                    return;
                }

                applicationSettingsType = value;
                var sectionName = default(string);
                var settingName = default(string);
                foreach (SettingsProperty prop in value.Properties) {
                    sectionName = "Application";
                    if (prop.Name.Contains("_")) {
                        sectionName = prop.Name.Split('_')[0];
                        settingName = prop.Name.Split('_')[1];
                    }
                    else {
                        settingName = prop.Name;
                    }

                    AddOrUpdateSetting(sectionName, settingName, prop.DefaultValue);
                }
            }
        }

        /// <summary>
        /// Creates the from other settings file.
        /// </summary>
        /// <param name="applicationName">The application name.</param>
        /// <param name="fileName">The file name.</param>
        /// <returns>A Settings.</returns>
        public static AppSettings CreateFromOtherSettingsFile(string applicationName, string fileName) => Create(applicationName, fileName);

        /// <summary>
        /// Creates the from application settings file.
        /// </summary>
        /// <param name="applicationName">The application name.</param>
        /// <param name="applicationDirectory">The application directory.</param>
        /// <returns>A Settings.</returns>
        public static AppSettings CreateFromApplicationSettingsFile(string applicationName, string applicationDirectory) => Create(applicationName, GetFileName(applicationDirectory, FileTypes.Settings));

        /// <summary>
        /// Creates the.
        /// </summary>
        /// <param name="applicationName">The application name.</param>
        /// <param name="fileName">The file name.</param>
        /// <returns>A Settings.</returns>
        private static AppSettings Create(string applicationName, string fileName) {
            var dir = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(dir)) {
                Directory.CreateDirectory(dir);
            }

            if (!File.Exists(fileName)) {
                new XDocument(new XElement("settings")).Save(fileName);
            }

            var result = new AppSettings(applicationName) {
                ActualFileName = fileName,
            };

            result.xDocumentFileName = fileName;
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
                            var objResult = default(object);
                            var s = new AppSettings(applicationName);
                            var mi = s.GetType().GetMethods().FirstOrDefault(m => m.Name == "GetLocalValue");
                            if (mi != null) {
                                mi = mi.MakeGenericMethod(t);
                                var args = new object[] {
                                    y, 0
                                };
                                objResult = mi.Invoke(s, args);
                            }
                            else {
                                objResult = value;
                            }
                            if (objResult != null) {
                                theValue = Enum.Parse(t, objResult.ToString(), true);
                            }
                        }
                        else if (t == typeof(string)) {
                            theValue = value;
                        }
                        else {
                            var mi = t.GetMethods().FirstOrDefault(m => m.Name == "TryParse");
                            var args = new object[] {
                            value, null
                        };
                            var objResult = (bool)mi.Invoke(null, args);
                            if (objResult) {
                                theValue = args[1];
                            }
                        }
                        result.AddOrUpdateSetting($"{applicationName}|{section}|{key}", theValue.GetType(), theValue);
                    }
                    catch (System.Exception) {

                    }
                });
            });
            return result;
        }

        /// <summary>
        /// Gets the value as string.
        /// </summary>
        /// <param name="sectionName">The section name.</param>
        /// <param name="keyName">The key name.</param>
        /// <returns>A string.</returns>
        public string GetValueAsString(string sectionName, string keyName) {
            var key = GetKey($"{ApplicationName}|{sectionName}|{keyName}", out var isNewKey);
            var value = GetLocalValue<string>(key, null);
            return value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="sectionName">The section name.</param>
        /// <param name="keyName">The key name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>A T1.</returns>
        public T1 GetValue<T1>(string sectionName, string keyName, T1 defaultValue = default) {
            var val = default(T1);
            if (KeyExists(sectionName, keyName)) {
                val = GetValue($"{ApplicationName}|{sectionName}|{keyName}", defaultValue);
                if (val.Is<string>() && ((string)(object)val) == "@null") {
                    return defaultValue;
                }
            }
            else
                val = defaultValue;

            return val;
        }

        /// <summary>
        /// Removes the setting.
        /// </summary>
        /// <param name="sectionName">The section name.</param>
        /// <param name="keyName">The key name.</param>
        public void RemoveSetting(string sectionName, string keyName) {
            var name = $"{ApplicationName}|{sectionName}|{keyName}";
            var key = GetKey(name, out var isNewKey);
            if (key == null) {
                return;
            }

            var xDoc = key.Document;
            key.Remove();
            var parts = name.Split('|');
            Save();
            AllKeys.Remove($"{sectionName}.{keyName}");
            SettingsAction?.Invoke(this, new SettingsActionEventArgs(isNewKey ? Actions.Add : Actions.Update, parts[0], parts[1], parts[2]));
        }

        /// <summary>
        /// Keys the exists.
        /// </summary>
        /// <param name="sectionName">The section name.</param>
        /// <param name="keyName">The key name.</param>
        /// <returns>A bool.</returns>
        public bool KeyExists(string sectionName, string keyName) =>
            GetKey($"{ApplicationName}|{sectionName}|{keyName}", out var isNewKey, true) != null && !isNewKey;

        /// <summary>
        /// Adds the or update setting.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>A Settings.</returns>
        public AppSettings AddOrUpdateSetting(string section, string name, object value) {
            if (value == null)
                AddOrUpdateSetting($"{ApplicationName}|{section}|{name}", "[string]", "[null]");
            else
                AddOrUpdateSetting($"{ApplicationName}|{section}|{name}", value.GetType(), value);
            return this;
        }

        /// <summary>
        /// Saves the.
        /// </summary>
        private void Save() {
            var result = false;
            var root = xDocument.Root;
            var sections = root.Elements();
            var toRemove = new List<XElement>();
            sections.ToList().ForEach(section => {
                var keys = section.Elements();
                keys.ToList().ForEach(key => {
                    if (string.IsNullOrEmpty(key.Attribute("value").Value)) {
                        toRemove.Add(key);
                    }
                });
            });
            if (toRemove.Any()) {
                toRemove.ForEach(x => x.Remove());
            }
            xDocument.Save(xDocumentFileName);
        }

        /// <summary>
        /// Adds the or update setting.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        private void AddOrUpdateSetting(string name, Type type, object value) {
            var key = GetKey(name, out var isNewKey);
            if (value == null || key == null) {
                return;
            }

            key.Attribute("type").Value = type.FullName;
            var parts = name.Split('|');
            key.Attribute("value").Value = value.ToString();
            Save();
            SettingsAction?.Invoke(this, new SettingsActionEventArgs(isNewKey ? Actions.Add : Actions.Update, parts[0], parts[1], parts[2], value));
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="isNewKey">If true, is new key.</param>
        /// <param name="isValidation">If true, is validation.</param>
        /// <returns>A XElement.</returns>
        private XElement GetKey(string name, out bool isNewKey, bool isValidation = false) {
            var parts = name.Split('|');
            var appName = parts[0];
            var sectionName = parts[1];
            var keyName = parts[2];
            isNewKey = false;
            //var xDoc = default(XDocument);
            if (string.IsNullOrEmpty(xDocumentFileName)) {
                try {
                    var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName, "Settings");
                    if (!Directory.Exists(dir)) {
                        Directory.CreateDirectory(dir);
                    }
                    ActualFileName = $"{dir}\\{name.Split('|')[0]}.xml";
                }
                catch (System.Exception ex) {
                    ex.Data.Add("FileExists", SettingsFileExists);
					throw;
				}
                xDocumentFileName = ActualFileName;
            }
            if (xDocument == default) {
                xDocument = SettingsFileExists ? XDocument.Load(xDocumentFileName) : new XDocument(new XElement("settings"));
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
                if (xKey == null && !isValidation) {
                    isNewKey = true;
                    xKey = new XElement("setting",
                            new XAttribute("name", keyName),
                            new XAttribute("type", string.Empty),
                            new XAttribute("value", string.Empty));
                    xSection.Add(xKey);
                    AllKeys.Add($"{sectionName}.{keyName}");
                }
            }
            return xKey;
        }

        /// <summary>
        /// Gets the local value.
        /// </summary>
        /// <param name="xKey">The x key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>A T1.</returns>
        internal T1 GetLocalValue<T1>(XElement xKey, T1 defaultValue) {
            var valueAttr = xKey.Attribute("value");
            var typeAttr = xKey.Attribute("type");
            var nameAttr = xKey.Attribute("name");
            var theValue = (object)defaultValue;
            var theName = nameAttr.Value;
            var theType = typeof(T1);
            var isUsingDefault = string.IsNullOrEmpty(valueAttr.Value) && !string.IsNullOrEmpty(defaultValue as string);

            if (typeAttr.Value == "[string]" && valueAttr.Value == "[null]") {
                return defaultValue;
            }

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
                }
                else {
                    switch (theType.Name.ToLowerInvariant()) {
                        case "timespan":
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
                        case "point":
                            theType = typeof(System.Windows.Point);
                            theValue = System.Windows.Point.Parse((string)theValue);
                            break;
                        case "size":
                            theType = typeof(System.Windows.Size);
                            theValue = System.Windows.Size.Parse((string)theValue);
                            break;
                        case "directoryinfo":
                            theType = typeof(DirectoryInfo);
                            theValue = new DirectoryInfo((string)theValue);
                            break;
                        default:
                            theValue = Convert.ChangeType(theValue, theType);
                            break;
                    }
                    if (isUsingDefault) {
                        valueAttr.Value = theValue.ToString();
                        typeAttr.Value = theType.FullName;
                    }
                }
            }
            else {
                xKey.Add(new XAttribute("value", theValue));
            }

            if (theValue == null || theValue.Equals("@null")) {
                return defaultValue;
            }
            return (T1)theValue;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>A T1.</returns>
        private T1 GetValue<T1>(string name, T1 defaultValue) {
            var key = GetKey(name, out var isNewKey);
            if (key == null) {
                return defaultValue;
            }

            return GetLocalValue(key, defaultValue);
        }

        /// <summary>
        /// Saves the window properties.
        /// </summary>
        /// <param name="window">The window.</param>
        public void SaveWindowProperties(Window window) {
            var section = window.GetType().Name;
            AddOrUpdateSetting(section, nameof(window.Left), window.RestoreBounds.Left);
            AddOrUpdateSetting(section, nameof(window.Top), window.RestoreBounds.Top);
            AddOrUpdateSetting(section, nameof(window.Width), window.RestoreBounds.Width);
            AddOrUpdateSetting(section, nameof(window.Height), window.RestoreBounds.Height);
            AddOrUpdateSetting(section, nameof(window.WindowState), window.WindowState);
        }

        /// <summary>
        /// Applies the window properties.
        /// </summary>
        /// <param name="window">The window.</param>
        public void ApplyWindowProperties(Window window) {
            var section = window.GetType().Name;
            var left = GetValue(section, nameof(window.Left), window.Left);
            var top = GetValue(section, nameof(window.Top), window.Top);
            var width = GetValue(section, nameof(window.Width), window.Width);
            var height = GetValue(section, nameof(window.Height), window.Height);

            window.Left = double.IsInfinity(left) ? 100.0 : left;
            window.Top = double.IsInfinity(top) ? 100.0 : top;
            window.Width = double.IsInfinity(width) ? 800 : width; ;
            window.Height = double.IsInfinity(height) ? 600 : height;

            window.WindowState = GetValue(section, nameof(window.WindowState), WindowState.Normal);

        }
    }
}
