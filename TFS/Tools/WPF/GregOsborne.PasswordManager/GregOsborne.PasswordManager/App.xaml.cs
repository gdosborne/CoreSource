// <copyright file="App.xaml.cs" company="">
// Copyright (c) 2019 All rights reserved
// </copyright>
// <author>IDOTCENTRAL\gosborn</author>
// <date>12/16/2019</date>

namespace GregOsborne.PasswordManager {
    using System;
    using System.IO;
    using System.Windows;
    using GregOsborne.Application;
    using GregOsborne.Application.Primitives;
    using GregOsborne.Application.Theme;
    using GregOsborne.Dialogs;
    using GregOsborne.PasswordManager.Data;

    public partial class App : System.Windows.Application {
        public SecurityFile SecurityFile { get; private set; } = default;

        public string Name => "Password Manager";

        public string PasswordManagerFileName { get; private set; } = default;

        public string DataDirectory { get; private set; } = null;

        public Session Session { get; private set; } = default;

        public ThemeManager ThemeManager { get; private set; } = default;

        protected override void OnExit(ExitEventArgs e) => Session.Logger.LogMessage("Application ending", GregOsborne.Application.Logging.ApplicationLogger.EntryTypes.Information);

        protected override void OnStartup(StartupEventArgs e) {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Session = new Session(Name, GregOsborne.Application.Logging.ApplicationLogger.StorageTypes.WindowsLog, GregOsborne.Application.Logging.ApplicationLogger.StorageOptions.None);
            Session.Logger.LogMessage("Application starting", GregOsborne.Application.Logging.ApplicationLogger.EntryTypes.Information);
            DataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Name);
            var themesFileName = Path.Combine(DataDirectory, "Themes.xml");
            if (!File.Exists(themesFileName)) {
                var baseThemeFileName = Path.Combine(Path.GetDirectoryName(GetType().Assembly.Location), "Themes.xml");
                if (File.Exists(baseThemeFileName)) {
                    File.Copy(baseThemeFileName, themesFileName, true);
                }
            }
            ThemeManager = new ThemeManager(themesFileName);

            var areCredentialsSetup = Session.ApplicationSettings.GetValue(Name, "AreCredentialsSetup", false);
            PasswordManagerFileName = Session.ApplicationSettings.GetValue(Name, "PasswordManagerFileName", string.Empty);
            var dir = default(string);
            var baseFileName = default(string);
            if (string.IsNullOrEmpty(PasswordManagerFileName)) {
                dir = DataDirectory;
                baseFileName = "PasswordManager.bin";
            } else {
                dir = Path.GetDirectoryName(PasswordManagerFileName);
                baseFileName = Path.GetFileName(PasswordManagerFileName);
            }
            var tempFileName = Path.Combine(dir, baseFileName);
            areCredentialsSetup = areCredentialsSetup && File.Exists(tempFileName);

            var content = "Please provide your username and password for Password Manager. If you forget this information, " +
                    "you will not be able to see your login credentials for your applications.";
            if (!areCredentialsSetup) {
                content += "\n\nIt looks like you haven't set up your username and password yet. Please provide your email address " +
                    "for your username, and a strong password. Remember that you are using this password to secure other passwords.";
            }

            var cd = new CredentialDialog {
                Content = content,
                MainInstruction = "Password Manager",
                ShowSaveCheckBox = true,
                ShowUIForSavedCredentials = true,
                UseApplicationInstanceCredentialCache = true,
                WindowTitle = "Log in to Pasword Manager",
                Target = Name
            };
            var cdResult = cd.ShowDialog();
            if (!cdResult) {
                Shutdown();
                return;
            }
            if (!areCredentialsSetup) {
                var td = new TaskDialog {
                    AllowDialogCancellation = true,
                    ButtonStyle = TaskDialogButtonStyle.Standard,
                    CenterParent = false,
                    Content = "The username/password combination you have supplied will be used to securely store the credentials " +
                        "stored in Password Manager.\n\nAre you sure you want to use these credentials?",
                    MainInstruction = "Primary Password",
                    MainIcon = TaskDialogIcon.Information,
                    WindowTitle = "Setup credentials"
                };
                td.Buttons.Add(new TaskDialogButton(ButtonType.Yes));
                td.Buttons.Add(new TaskDialogButton(ButtonType.No));
                var tdResult = td.ShowDialog();
                if (tdResult.ButtonType == ButtonType.No) {
                    Shutdown();
                    return;
                }
            }
            var encryptionSalt = System.Text.Encoding.ASCII.GetBytes((cd.UserName.ToLower() + cd.Password).GetHashCode().ToString());
            Session.ApplicationSettings.AddOrUpdateSetting(Name, "AreCredentialsSetup", true);

            SecurityFile = SecurityFile.Open(tempFileName, encryptionSalt);

            if (cd.IsSaveChecked) {
                cd.ConfirmCredentials(true);
            }
            PasswordManagerFileName = tempFileName;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) => HandleException(e.ExceptionObject.As<Exception>());

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e) => HandleException(e.Exception);

        private void HandleException(Exception ex) {

        }
    }
}
