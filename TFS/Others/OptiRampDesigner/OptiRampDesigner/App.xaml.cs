namespace OptiRampDesigner
{
    using GregOsborne.Application.Primitives;
    using OptiRampDesignerModel;
    using OptiRampDesignerModel.Concrete;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Xml.Linq;

    public partial class App : Application
    {
        #region Public Fields

        public static string ApplicationConfigFolder = null;

        public static string ApplicationDataFolder = null;

        public static string ApplicationLogFolder = null;

        public static Dictionary<string, IConvertible> ApplicationSettings = null;

        public static string ApplicationTemporaryFolder = null;

        #endregion Public Fields

        #region Private Fields

        private static string configurationFileName = null;

        private static string modulesFileName = null;

        #endregion Private Fields

        #region Public Constructors

        public App()
        {
            this.Startup += App_Startup;
        }

        #endregion Public Constructors

        #region Public Properties

        public static IOptionSet ApplicationOptions { get; private set; }

        public static ILog Log { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public static bool AddModule(string fileName)
        {
            var result = false;
            var isExisting = false;
            XDocument doc = null;
            if (File.Exists(modulesFileName))
            {
                doc = XDocument.Load(modulesFileName);
                doc.Root.Elements().ToList().ForEach(x =>
                {
                    var path = x.Value;
                    if (path.Equals(fileName, StringComparison.OrdinalIgnoreCase))
                    {
                        isExisting = true;
                        return;
                    }
                });
            }
            else
                doc = new XDocument(new XElement("modules"));
            if (!isExisting)
            {
                Log.WriteMessage("Adding module: {0}", fileName);
                doc.Root.Add(new XElement("module", fileName));
                doc.Save(modulesFileName);
                result = true;
            }
            return result;
        }

        public static IEnumerable<IModule> GetModules()
        {
            var result = new List<IModule>();
            if (!File.Exists(modulesFileName))
                return result;
            var doc = XDocument.Load(modulesFileName);
            doc.Root.Elements().ToList().ForEach(x =>
            {
                var path = x.Value;
                if (!File.Exists(path))
                    return;
                var assy = Assembly.LoadFile(path);
                assy.GetTypes().ToList().ForEach(t =>
                {
                    if (!t.GetInterfaces().Contains(typeof(IModule)))
                        return;
                    var obj = (IModule)Activator.CreateInstance(t);
                    Log.WriteMessage("Found module \"{0}\" in {1}", obj.Name, path);
                    obj.ApplicationDirectory = ApplicationDataFolder;
                    obj.Log = Log;
                    result.Add(obj);
                });
            });
            return result;
        }

        public static T GetSetting<T>(string name, T defaultValue) where T : IConvertible
        {
            if (ApplicationSettings.ContainsKey(name))
                return (T)ApplicationSettings[name];
            return defaultValue;
        }

        public static Dictionary<string, IConvertible> LoadConfigurationXml(string fileName)
        {
            Dictionary<string, IConvertible> result = new Dictionary<string, IConvertible>();
            if (!File.Exists(fileName))
                return result;
            var doc = XDocument.Load(fileName);
            doc.Root.Elements().ToList().ForEach(x =>
            {
                var key = x.Attribute("key").Value;
                var value = x.Attribute("value").Value;
                var type = x.Attribute("type").Value;
                var realType = Type.GetType(type);
                var realValue = realType.IsEnum ? value.ToWindowState() : (IConvertible)Convert.ChangeType(value, realType);
                result.Add(key, realValue);
            });
            return result;
        }

        public static bool RemoveModule(string fileName)
        {
            var result = false;
            XDocument doc = null;
            if (!File.Exists(modulesFileName))
                return true;
            doc = XDocument.Load(modulesFileName);
            doc.Root.Elements().ToList().ForEach(x =>
            {
                if (result)
                    return;
                var path = x.Value;
                if (path.Equals(fileName, StringComparison.OrdinalIgnoreCase))
                {
                    Log.WriteMessage("Removing module: {0}", fileName);
                    x.Remove();
                    result = true;
                    return;
                }
            });
            doc.Save(modulesFileName);
            return result;
        }

        public static void SetSetting<T>(string name, T value) where T : IConvertible
        {
            if (ApplicationSettings.ContainsKey(name))
                ApplicationSettings[name] = value;
            else
                ApplicationSettings.Add(name, value);
            SaveConfigurationXml(configurationFileName, ApplicationSettings);
        }

        #endregion Public Methods

        #region Private Methods

        private static void SaveConfigurationXml(string fileName, Dictionary<string, IConvertible> settings)
        {
            var doc = new XDocument(new XElement("settings"));
            settings.ToList().ForEach(x =>
            {
                doc.Root.Add(new XElement("setting",
                    new XAttribute("key", x.Key),
                    new XAttribute("value", Convert.ChangeType(x.Value, typeof(string))),
                    new XAttribute("type", x.Value.GetType().AssemblyQualifiedName)));
            });
            doc.Save(fileName);
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            ApplicationDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OptiRamp© Desktop");
            if (!Directory.Exists(ApplicationDataFolder))
                Directory.CreateDirectory(ApplicationDataFolder);
            ApplicationTemporaryFolder = Path.Combine(ApplicationDataFolder, "temp");
            ApplicationConfigFolder = Path.Combine(ApplicationDataFolder, "config");
            ApplicationLogFolder = Path.Combine(ApplicationDataFolder, "logs");

            if (!Directory.Exists(ApplicationTemporaryFolder))
                Directory.CreateDirectory(ApplicationTemporaryFolder);
            if (!Directory.Exists(ApplicationConfigFolder))
                Directory.CreateDirectory(ApplicationConfigFolder);
            if (!Directory.Exists(ApplicationLogFolder))
                Directory.CreateDirectory(ApplicationLogFolder);

            var logFileName = System.IO.Path.Combine(ApplicationLogFolder, string.Format("OptiRampDesigner_{0}.log", DateTime.Now.ToString("yyyy-MM-dd")));
            Log = new Log(logFileName);
            Log.WriteMessage("Starting application");
            configurationFileName = Path.Combine(ApplicationConfigFolder, "settings.xml");
            Log.WriteMessage("Application settings: {0}", configurationFileName);
            modulesFileName = Path.Combine(ApplicationConfigFolder, "modules.xml");
            Log.WriteMessage("Modules: {0}", modulesFileName);
            ApplicationSettings = LoadConfigurationXml(configurationFileName);
            ApplicationOptions = new OptionSet("OptiRamp Designer");
            var genCat = new OptionCategory("General");
            ApplicationOptions.Categories.Add(genCat);
        }

        #endregion Private Methods
    }
}