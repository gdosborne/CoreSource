namespace GregOsborne.Documater
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using GregOsborne.Application.Logging;
    using SysIO = System.IO;

    public partial class App : System.Windows.Application
    {
        public static string ApplicationName = "Document Automater";

        protected override void OnStartup(StartupEventArgs e)
        {
            Logger.LogDirectory = SysIO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName);
            Logger.LogMessage("Application Startup");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Logger.LogMessage("Application Shutdown");
        }
    }
}
