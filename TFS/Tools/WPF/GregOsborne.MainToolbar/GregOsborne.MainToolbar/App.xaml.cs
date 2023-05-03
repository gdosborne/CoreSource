// <copyright file="App.xaml.cs" company="">
// Copyright (c) 2020 All rights reserved
// </copyright>
// <author>IDOTCENTRAL\gosborn</author>
// <date>3/13/2020</date>

namespace GregOsborne.MainToolbar {
    using System;
    using System.IO;
    using System.Windows;

    using GregOsborne.Application;
    using GregOsborne.Application.Exception;
    using GregOsborne.Application.Logging;
    using GregOsborne.Application.Primitives;
    using GregOsborne.Application.Security;
    using Toolbar.Controller;

    public partial class App : System.Windows.Application {
        public Session ApplicationSession { get; private set; } = default;

        public bool IsRunAsAdministrator { get; } = UserAccess.IsRunAsAdmin();

        public bool IsUserInAdministratorGroup { get; } = UserAccess.IsUserInAdminGroup();

        public PlatformID OsPlatform { get; } = Environment.OSVersion.Platform;

        public string OSServicePack { get; } = Environment.OSVersion.ServicePack;

        public Version OSVersion { get; } = Environment.OSVersion.Version;

        public Version RunTimeVersion { get; } = Environment.Version;

        public string ExtensionDirectory { get; private set; } = default;

        public Manager ControllerManager { get; private set; } = null;

        protected override void OnExit(ExitEventArgs e) => ApplicationSession.Logger.LogMessage("Application Ending", ApplicationLogger.EntryTypes.Information);

        protected override void OnStartup(StartupEventArgs e) {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            ApplicationSession = new Session("MainToolbar", ApplicationLogger.StorageTypes.FlatFile, ApplicationLogger.StorageOptions.None);
            ApplicationSession.Logger.LogMessage("Application Starting", ApplicationLogger.EntryTypes.Information);
            ExtensionDirectory = ApplicationSession.ApplicationSettings.GetValue("Personal.Settings", "Extension.Directory", Path.Combine(Path.GetDirectoryName(GetType().Assembly.Location), "Extensions"));
            try {
                if (!Directory.Exists(ExtensionDirectory)) {
                    Directory.CreateDirectory(ExtensionDirectory);
                }
                ControllerManager = new Manager(ExtensionDirectory, IsUserInAdministratorGroup || IsRunAsAdministrator, ".extension");
                ControllerManager.LogMessage += (sender, ee) => {
                    ApplicationSession.Logger.LogMessage(ee.Message, ee.EntryType);
                    if (ee.IsCritical) {
                        Shutdown(-100);
                    }
                };                
                ControllerManager.Refresh(ApplicationSession);
            } catch (Exception ex) {
                HandleException(ex);
            }
        }

        private void HandleException(Exception ex) => HandleException(ex, true);

        private void HandleException(Exception ex, bool isShutdownRequired) {
            var data = ex.ToStringRecurse();
            ApplicationSession.Logger.LogMessage(data, ApplicationLogger.EntryTypes.Error);
            if (isShutdownRequired) {
                Shutdown(ex.HResult);
            }
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) => HandleException(e.ExceptionObject.As<Exception>());

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e) => HandleException(e.Exception);
    }
}
