using ApplicationFramework.Security;
using Common.Applicationn;
using Common.Applicationn.Primitives;
using Common.Applicationn.Security;
using CongregationManager.Data;
using CongregationManager.Extensibility;
using CredentialManagement;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security;
using System.Windows;
using static Common.Applicationn.Security.Extensions;
using Credential = CredentialManagement.Credential;

namespace CongregationManager {
    public partial class App : System.Windows.Application {
        public static string ApplicationName => "Congregation Manager";
        public static string ApplicationFolder => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName);
        public static string ExtensionsFolder => Path.Combine(ApplicationFolder, "Extensions");
        public static string TempFolder => Path.Combine(ApplicationFolder, "Temp");
        public static string DataFolder => Path.Combine(ApplicationFolder, "Data");
        public static Session ApplicationSession { get; private set; }
        
        public static DataManager DataManager { get; private set; }

        public App() {
            SetupFolders();
            ApplicationSession = new Session(ApplicationFolder, ApplicationName,
                Common.Applicationn.Logging.ApplicationLogger.StorageTypes.Xml,
                Common.Applicationn.Logging.ApplicationLogger.StorageOptions.CreateFolderForEachDay);
        }

        public static bool IsYesInDialogSelected(string main, string content, string title, TaskDialogIcon icon) {
            var td = new TaskDialog {
                Width = 250,
                MainIcon = icon,
                MainInstruction = main,
                Content = content,
                AllowDialogCancellation = true,
                ButtonStyle = TaskDialogButtonStyle.Standard,
                WindowTitle = title
            };
            td.Buttons.Add(new TaskDialogButton(ButtonType.Yes));
            td.Buttons.Add(new TaskDialogButton(ButtonType.No));
            var result = td.ShowDialog();
            return result.ButtonType == ButtonType.Yes;
        }

        private bool IsContinueAfterInvalid() {
            var main = "Invalid username or password";
            var content = $"You have entered an invalid username or password for " +
                $"{ApplicationName}.\n\nWould you like to try again?";
            var title = "Invalid credentials";
            return IsYesInDialogSelected(main, content, title, TaskDialogIcon.Warning);
        }

        private bool IsLoginAccepted(string main, string content, string title,
                ref ApplicationCredentials privateCreds, ref ApplicationCredentials currentCreds) {

            //privatecreds => the existing credentials pulled from the Credential Manager.
            //                the CredentialDialog values must match these
            //                privatecreds.AreNewCredentials will be true if there were
            //                no credentials previously saved
            //currentCreds => will be populated with the login credentials entered in 
            //                CredentialDialog

            var isNewUser = privateCreds.AreNewCredentials;
            var cd = new CredentialDialog {
                Target = ApplicationName,
                MainInstruction = main,
                Content = content,
                WindowTitle = title,
                ShowUIForSavedCredentials = true
            };
            if (isNewUser) {
                cd.Credentials.UserName = privateCreds.UserName;
                cd.Credentials.Password = privateCreds.Password;
                cd.Content += $" You will be prompted to save your credentials for future " +
                    $"validation.";
                cd.IsSaveChecked = true;
            }
            var result = default(bool?);
            while (!result.HasValue) {
                result = cd.ShowDialog();
                if (!result.Value)
                    break;
                result = string.IsNullOrEmpty(cd.UserName) || string.IsNullOrEmpty(cd.Password)
                    ? null : result;
            }
            if (result.Value) {
                currentCreds = new ApplicationCredentials(cd.Credentials.UserName, cd.Credentials.SecurePassword, false);
                if (isNewUser) {
                    main = "Create username and password";
                    content = "It seems like this is the first time you have " +
                        $"logged in to {ApplicationName}. The username and password you have " +
                        $"just entered will be saved for future use.\n\nWould you like to save " +
                        $"this information?";
                    title = "Save username and password";
                    if (!IsYesInDialogSelected(main, content, title, TaskDialogIcon.Warning))
                        Environment.Exit(-1);

                    //save into CredentialManager
                    using (var credItem = new Credential()) {
                        credItem.Password = cd.Password;
                        credItem.Username = cd.UserName;
                        credItem.Target = ApplicationName;
                        credItem.Type = CredentialType.Generic;
                        credItem.PersistanceType = PersistanceType.LocalComputer;
                        credItem.Save();
                    }
                }
            }
            return result.Value;
        }

        private ApplicationCredentials GetCurrentCredentials() {
            var password = default(SecureString);
            var username = default(string);

            using var credItem = new Credential();
            credItem.Target = ApplicationName;
            credItem.Load();
            var isNewLogin = true;
            if (credItem.HasValidCredentials()) {
                password = new NetworkCredential(string.Empty, credItem.Password).SecurePassword;
                username = credItem.Username;
                isNewLogin = false;
            }
            return new ApplicationCredentials(username, password, isNewLogin);
        }

        private DataManager Login() {
            var privateCreds = GetCurrentCredentials();

            var main = $"Login to {ApplicationName}";
            var content = $"Use this to provide the user credentials for " +
                $"{ApplicationName},";
            var title = "Login";
            var cd = default(CredentialDialog);
            var newCreds = default(ApplicationCredentials);
            if (IsLoginAccepted(main, content, title, ref privateCreds, ref newCreds)) {
                var isValid = privateCreds.Equals(newCreds);

                while (!isValid && !privateCreds.AreNewCredentials) {
                    main = $"Login to {ApplicationName}";
                    content = $"Use this to provide the user credentials for " +
                        $"{ApplicationName},";
                    title = "Login"; 
                    var result = IsYesInDialogSelected(main, content, title, TaskDialogIcon.Warning);
                    if (!result)
                        Environment.Exit(-1);

                    if (!IsLoginAccepted(main, content, title, ref privateCreds, ref newCreds))
                        Environment.Exit(-1);
                    isValid = privateCreds.Equals(newCreds);
                }
            }
            else
                Environment.Exit(-1);
            return new DataManager(DataFolder, newCreds.Password);
        }

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            ApplicationData.Extensions = new List<ExtensionBase>();
            LoadExtensions();
            DataManager = Login();
        }

        protected override void OnExit(ExitEventArgs e) {
            base.OnExit(e);
            if (DataManager != null)
                DataManager.Dispose();
        }

        private void SetupFolders() {
            if (!Directory.Exists(ApplicationFolder)) {
                Directory.CreateDirectory(ApplicationFolder);
            }
            if (!Directory.Exists(ExtensionsFolder)) {
                Directory.CreateDirectory(ExtensionsFolder);
            }
            if (!Directory.Exists(TempFolder)) {
                Directory.CreateDirectory(TempFolder);
            }
            if (!Directory.Exists(DataFolder)) {
                Directory.CreateDirectory(DataFolder);
            }
        }

        public static void AddExtensions(string[] filenames) {
            foreach (var extFile in filenames) {
                var assy = Assembly.Load(File.ReadAllBytes(extFile));
                if (assy != null) {
                    var types = assy.GetTypes().Where(x => x.BaseType == typeof(ExtensionBase));
                    foreach (var type in types) {
                        var instance = Activator.CreateInstance(type);
                        if (instance != null) {
                            ApplicationData.Extensions.Add(instance.As<ExtensionBase>());
                            instance.As<ExtensionBase>().Filename = extFile;
                        }
                    }
                }
            }
        }

        public static Dictionary<string, AppDomain> Domains = default;

        private void LoadExtensions() {
            //ApplicationData.Extensions.Add(new CongregationExtension.Extension());
            Domains = new Dictionary<string, AppDomain>();
            var extDir = new DirectoryInfo(ExtensionsFolder);
            var extensions = extDir.GetFiles("*.dll");
            var fileNames = extensions.Select(x => x.FullName).ToArray();
            AddExtensions(fileNames);
        }
    }
}
