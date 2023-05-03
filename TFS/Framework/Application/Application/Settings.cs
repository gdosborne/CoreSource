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
			var left = window.RestoreBounds.Left;
			var top = window.RestoreBounds.Top;
			var width = window.RestoreBounds.Width;
			var height = window.RestoreBounds.Height;

			SetSetting(applicationName, window.GetType().Name, "Left", left);
            SetSetting(applicationName, window.GetType().Name, "Top", top);
            if (!positionOnly) {
                SetSetting(applicationName, window.GetType().Name, "Width", width);
                SetSetting(applicationName, window.GetType().Name, "Height", height);
            }
        }

        public static void ApplyWindowBounds(string applicationName, Window window, Rect defaultValue, bool positionOnly = false) {
            if (string.IsNullOrEmpty(applicationName))
                throw new ArgumentNullException(nameof(applicationName));
            if (window == null)
                throw new ArgumentNullException(nameof(window));
            if (defaultValue == null)
                throw new ArgumentNullException(nameof(defaultValue));
			var left = GetSetting(applicationName, window.GetType().Name, "Left", defaultValue.Left);
			if (left > Screen.FullWidth)
				left = 0;
			var top = GetSetting(applicationName, window.GetType().Name, "Top", defaultValue.Top);
			if (top > Screen.MaxHeight)
				top = 0;
			var width = GetSetting(applicationName, window.GetType().Name, "Width", defaultValue.Width);
			var height = GetSetting(applicationName, window.GetType().Name, "Height", defaultValue.Height);
			window.Left = left;
			window.Top = top;
			if (!positionOnly) {
				window.Width = width;
				window.Height = height;
			}
        }

        public static T GetSetting<T>(string applicationName, string sectionName, string settingName, T defaultValue) {
			if (string.IsNullOrEmpty(SettingsFileName))
				return defaultValue;
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
            if (settingXElement == null)
                return defaultValue;
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
