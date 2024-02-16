using GregOsborne.Application;
using GregOsborne.Application.Logging;
using System.IO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace UpdateVersioning {
    public partial class App : System.Windows.Application {
        public static string ApplicationName { get; } = "Update Versioning";
        public static string ApplicationDirectory { get; private set; }
        public static Session Session { get; private set; }
        public App() {
            InitializeComponent();
            ApplicationDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName);
            if(!Directory.Exists(ApplicationDirectory)) Directory.CreateDirectory(ApplicationDirectory);
            Session = new Session(ApplicationDirectory, ApplicationName, ApplicationLogger.StorageTypes.FlatFile, 
                ApplicationLogger.StorageOptions.CreateFolderForEachDay);
        }
    }
}
