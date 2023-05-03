namespace GregOsborne.Application {
    using GregOsborne.Application.Media;
    using GregOsborne.Application.Primitives;
    using GregOsborne.Application.Windows;
    using System;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using System.Xml.Linq;

    public static class Settings {
        public static string SettingsFileName { get; set; }
        private static XDocument _xDoc = default;

        public static void SaveWindowBounds(string applicationName, Window window, bool positionOnly = false) {
            if (string.IsNullOrEmpty(applicationName))
                throw new ArgumentNullException(nameof(applicationName));
            if (window == null)
                throw new ArgumentNullException(nameof(window));
            SetSetting(applicationName, window.GetType().Name, "Left", window.RestoreBounds.Left);
            SetSetting(applicationName, window.GetType().Name, "Top", window.RestoreBounds.Top);
            if (!positionOnly) {
                SetSetting(applicationName, window.GetType().Name, "Width", window.RestoreBounds.Width);
                SetSetting(applicationName, window.GetType().Name, "Height", window.RestoreBounds.Height);
            }
        }

        public static void ApplyWindowBounds(string applicationName, Window window, Rect defaultValue, bool positionOnly = false) {
            if (string.IsNullOrEmpty(applicationName))
                throw new ArgumentNullException(nameof(applicationName));
            if (window == null)
                throw new ArgumentNullException(nameof(window));
            if (defaultValue == null)
                throw new ArgumentNullException(nameof(defaultValue));
            window.Left = GetSetting(applicationName, window.GetType().Name, "Left", defaultValue.Left);
            if (window.Left > Screen.FullWidth)
                window.Left = 0;
            window.Top = GetSetting(applicationName, window.GetType().Name, "Top", defaultValue.Top);
            if (window.Top > Screen.MaxHeight)
                window.Top = 0;
            if (!positionOnly) {
                window.Width = GetSetting(applicationName, window.GetType().Name, "Width", defaultValue.Width);
                window.Height = GetSetting(applicationName, window.GetType().Name, "Height", defaultValue.Height);
            }
        }

        public static T GetSetting<T>(string applicationName, string sectionName, string settingName, T defaultValue) {
            if (string.IsNullOrEmpty(SettingsFileName))
                throw new ArgumentNullException(nameof(SettingsFileName));
            var fileName = SettingsFileName;
            var canOpenFile = false;
            if (File.Exists(fileName)) {
                while (!canOpenFile) {
                    try {
                        if (_xDoc == null)
                            _xDoc = XDocument.Load(fileName);
                        canOpenFile = true;
                    }
                    catch (System.Exception) {
                        canOpenFile = false;
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }
            else {
                _xDoc = new XDocument(new XElement("settings"));
                _xDoc.Save(fileName);
            }
            var root = _xDoc.Root;
            if (root == null)
                return defaultValue;
            var sectionXElement = root.Elements().FirstOrDefault(x => {
                var attribute = x.Attribute("name");
                return attribute != null && (x.Name.LocalName.Equals("section") && attribute.Value.Equals(sectionName));
            });
            if (sectionXElement == null)
                return defaultValue;
            var settingXElement = sectionXElement.Elements().FirstOrDefault(x => {
                var attribute = x.Attribute("name");
                return attribute != null && (x.Name.LocalName.Equals("value") && attribute.Value.Equals(settingName));
            });
			if (settingXElement == null) {
				settingXElement = new XElement("setting",
					new XAttribute("name", settingName),
					new XAttribute("value", defaultValue));
			}
            var xAttribute = settingXElement.Attribute("value");
            if (xAttribute == null)
                return defaultValue;
            var value = xAttribute.Value;
            //special cases for numbers
            if (value.Contains("Infinity"))
                return defaultValue;
            if (typeof(T).HasMethod("TryParse")) {
                var temp = default(T);
                var meth = typeof(T).GetMethods().First(x => x.Name == "TryParse");
                var args = new object[] { value, temp };
                var result = (bool)meth.Invoke(null, args);
                return result ? (T)args[1] : default;
            }
            else if (typeof(T) == typeof(Color)) {
                return (T)(object)value.ToColor();
            }
            if (typeof(T).IsEnum && Enum.IsDefined(typeof(T), value))
                return (T)Enum.Parse(typeof(T), value, true);
            if (typeof(T) == typeof(string))
                return (T)(object)value;
            return defaultValue;
        }

        public static void SetSetting<T>(string applicationName, string sectionName, string settingName, T value) {
            try {
                //this will throw an exception if not a number of Infinity, but it's just a small check so it's ok
                if (double.IsInfinity((double)(object)value) || double.IsNegativeInfinity((double)(object)value))
                    return;
            }
            catch { }
            if (string.IsNullOrEmpty(SettingsFileName))
                throw new ArgumentNullException(nameof(SettingsFileName));

            var fileName = SettingsFileName;
            var canOpenFile = false;
            if (File.Exists(fileName)) {
                while (!canOpenFile) {
                    try {
                        if (_xDoc == null)
                            _xDoc = XDocument.Load(fileName);
                        canOpenFile = true;
                    }
                    catch (System.Exception) {
                        canOpenFile = false;
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }
            else {
                _xDoc = new XDocument(new XElement("settings"));
                _xDoc.Save(fileName);
            }
            var root = _xDoc.Root;
            if (root != null) {
                var sectionXElement = root.Elements().FirstOrDefault(x => {
                    var xAttribute = x.Attribute("name");
                    return xAttribute != null && (x.Name.LocalName.Equals("section") && xAttribute.Value.Equals(sectionName));
                });
                if (sectionXElement == null) {
                    sectionXElement = new XElement("section", new XAttribute("name", sectionName));
                    root.Add(sectionXElement);
                }
                var settingXElement = sectionXElement.Elements().FirstOrDefault(x => {
                    var xAttribute = x.Attribute("name");
                    return xAttribute != null && (x.Name.LocalName.Equals("value") && xAttribute.Value.Equals(settingName));
                });
                if (settingXElement == null) {
                    settingXElement = new XElement("value", new XAttribute("name", settingName), new XAttribute("value", value == null ? string.Empty : value.ToString()));
                    sectionXElement.Add(settingXElement);
                }
                else {
                    var xAttribute = settingXElement.Attribute("value");
                    if (xAttribute != null)
                        xAttribute.Value = value.ToString();
                }
            }
            var saveTryCount = 0;
            while (saveTryCount < 10) {
                try {
                    _xDoc.Save(fileName);
                    break;
                }
                catch {
                    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
                    saveTryCount++;
                }
            }
        }
    }
}
