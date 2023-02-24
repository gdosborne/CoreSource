using ApplicationFramework.Security;
using Common.Application;
using Common.Application.Media;
using Common.Application.Primitives;
using Common.Application.Security;
using Common.Application.Windows;
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
using System.Text;
using System.Windows;
using System.Windows.Media;
using static ApplicationFramework.Dialogs.Helpers;
using static Common.Application.Logging.ApplicationLogger;
using static Common.Application.Security.Extensions;
using Credential = CredentialManagement.Credential;

namespace CongregationManager {
    public partial class App : System.Windows.Application {
        public static string ApplicationName => "Congregation Manager";

        private static string _ApplicationFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName);

        public static string ApplicationFolder {
            get => _ApplicationFolder;
            private set => _ApplicationFolder = value;
        }

        public static string ExtensionsFolder => Path.Combine(ApplicationFolder, "Extensions");

        public static string TempFolder => Path.Combine(ApplicationFolder, "Temp");

        public static string DataFolder => Path.Combine(ApplicationFolder, "Data");

        public static string RecycleBinFolder => Path.Combine(DataFolder, "Recycle Bin");

        public static string ThemeFolder => Path.Combine(ApplicationFolder, "Themes");

        public static List<ApplicationTheme> AppThemes { get; private set; }

        public static Session ApplicationSession { get; private set; }

        public static Dictionary<string, string> FileFilters { get; private set; } =
            new Dictionary<string, string> {
                { "Congregation Manager Extension (*.dll)","*.dll" }
            };
        public static DataManager DataManager { get; private set; }

        public static ApplicationTheme CurrentTheme { get; private set; }

        public App() {
            SetupFolders();
            ApplicationSession = new Session(ApplicationFolder, ApplicationName,
                StorageTypes.Xml, StorageOptions.CreateFolderForEachDay);
            AppThemes = GetThemes();
        }

        private List<ApplicationTheme> GetThemes() {
            var result = new List<ApplicationTheme>();
            var themeFiles = new DirectoryInfo(ThemeFolder).GetFiles("*.apptheme");
            if (!themeFiles.Any()) {
                MakeDefaultThemeFile();
            }

            themeFiles.ToList().ForEach(file => {
                result.Add(ApplicationTheme.FromFile(file.FullName));
            });
            return result;
        }

        public static void ApplyTheme(string name) {
            CurrentTheme = AppThemes.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            CurrentTheme?.Apply(App.Current.Resources);
        }

        private void MakeDefaultThemeFile() {
            var result = new ApplicationTheme {
                Name = "Default",
                FullPath = Path.Combine(ThemeFolder, "Default.apptheme")
            };
            result.Values.Add("ControlBackground", SystemColors.ControlColor.ToHexValue());
            result.Values.Add("ControlForeground", SystemColors.ControlTextColor.ToHexValue());
            result.Values.Add("ControlMouseOverBackground", SystemColors.ControlDarkColor.ToHexValue());
            result.Values.Add("ControlMouseOverForeground", SystemColors.ControlLightColor.ToHexValue());
            result.Values.Add("WindowBorderBrush", SystemColors.ActiveBorderColor.ToHexValue());
            result.Values.Add("WindowBackground", SystemColors.WindowColor.ToHexValue());
            result.Values.Add("LightBorderBrush", SystemColors.ControlLightColor.ToHexValue());
            result.Values.Add("WindowTextForeground", SystemColors.WindowTextColor.ToHexValue());
            result.Values.Add("HighlightForeground", SystemColors.HighlightTextColor.ToHexValue());
            result.Values.Add("CaptionBackground", SystemColors.ActiveCaptionColor.ToHexValue());
            result.Values.Add("CaptionForeground", SystemColors.ActiveCaptionTextColor.ToHexValue());
            result.Values.Add("ToggleSwitchBackground", SystemColors.ControlColor.ToHexValue());
            result.Values.Add("GoForeground", SystemColors.InfoColor.ToHexValue());
            result.Values.Add("TabTextForeground", SystemColors.WindowTextColor.ToHexValue());
            result.Values.Add("ButtonForeground", SystemColors.ControlTextColor.ToHexValue());
            result.Values.Add("ButtonBackground", SystemColors.ControlColor.ToHexValue());
            result.Values.Add("MenuBackground", SystemColors.MenuColor.ToHexValue());
            result.Values.Add("MenuForeground", SystemColors.MenuTextColor.ToHexValue());
            result.Values.Add("MenuItemBackground", SystemColors.MenuColor.ToHexValue());
            result.Values.Add("MenuItemForeground", SystemColors.MenuTextColor.ToHexValue());
            result.Values.Add("ErrorBackground", SystemColors.HighlightColor.ToHexValue());
            result.Values.Add("ErrorForeground", SystemColors.HighlightTextColor.ToHexValue());
            result.Save();
        }

        public static void LogMessage(StringBuilder message, EntryTypes type) =>
            ApplicationSession.Logger.LogMessage(message, type);

        public static void LogMessage(string message, EntryTypes type) =>
            LogMessage(new StringBuilder(message), type);

        public static string[] SelectFileDialog(Window win, string title, string defaultExtension,
            Dictionary<string, string> filters, string lastDirectory, bool returnMultiple = false) {
            var ofd = new VistaOpenFileDialog {
                Title = title,
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = defaultExtension,
                Filter = string.Join('|', filters.Select(x => x.Key + "|" + x.Value)),
                InitialDirectory = lastDirectory,
                Multiselect = returnMultiple,
                RestoreDirectory = true
            };
            var result = ofd.ShowDialog(win);
            if (!result.HasValue || !result.Value)
                return new List<string>().ToArray();
            return ofd.FileNames;
        }

        private bool IsContinueAfterInvalid() {
            var main = "Invalid username or password";
            var content = $"You have entered an invalid username or password for " +
                $"{ApplicationName}.\n\nWould you like to try again?";
            var title = "Invalid credentials";
            return ShowYesNoDialog(main, content, TaskDialogIcon.Warning);
        }

        private bool IsLoginAccepted(string title, string content,
            ref ApplicationCredentials privateCreds, ref ApplicationCredentials currentCreds) {

            //privatecreds => the existing credentials pulled from the Credential Manager.
            //                the CredentialDialog values must match these
            //                privatecreds.AreNewCredentials will be true if there were
            //                no credentials previously saved
            //currentCreds => will be populated with the login credentials entered in 
            //                CredentialDialog

            LogMessage("Starting login", EntryTypes.Information);
            var isNewUser = privateCreds.AreNewCredentials;
            var cd = new CredentialDialog {
                Target = ApplicationName,
                MainInstruction = title,
                Content = content,
                WindowTitle = title
            };
#if DEBUG            
            cd.ShowUIForSavedCredentials = false;
            cd.ShowSaveCheckBox = true;
#else
            cd.ShowUIForSavedCredentials = true;
            cd.ShowSaveCheckBox = false;
#endif
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
#if DEBUG
                if (cd.IsSaveChecked)
                    cd.ConfirmCredentials(true);
                result = true;
#else
                result = string.IsNullOrEmpty(cd.UserName) || string.IsNullOrEmpty(cd.Password)
                    ? null : result;
#endif
            }
            if (result.Value) {
                currentCreds = new ApplicationCredentials(cd.Credentials.UserName, cd.Credentials.SecurePassword, false);
                if (isNewUser) {
                    title = "Create username and password";
                    content = "It seems like this is the first time you have " +
                        $"logged in to {ApplicationName}. The username and password you have " +
                        $"just entered will be saved for future use.\n\nWould you like to save " +
                        $"this information?";
                    title = "Save username and password";
                    if (!ShowYesNoDialog(title, content, TaskDialogIcon.Warning))
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

            var title = $"Login to {ApplicationName}";
            var content = $"Use this to provide the user credentials for " +
                $"{ApplicationName},";
            var cd = default(CredentialDialog);
            var newCreds = default(ApplicationCredentials);
            if (IsLoginAccepted(title, content, ref privateCreds, ref newCreds)) {
                var isValid = privateCreds.Equals(newCreds);
#if DEBUG
                isValid = true;
#endif

                while (!isValid && !privateCreds.AreNewCredentials) {
                    title = $"Login to {ApplicationName}";
                    content = $"Use this to provide the user credentials for " +
                        $"{ApplicationName},";
                    title = "Login";
                    var result = ShowYesNoDialog(title, content, TaskDialogIcon.Warning);
                    if (!result)
                        Environment.Exit(-1);

                    if (!IsLoginAccepted(title, content, ref privateCreds, ref newCreds))
                        Environment.Exit(-1);
                    isValid = privateCreds.Equals(newCreds);
                }
            }
            else
                Environment.Exit(-1);
            newCreds.SecurePassword.IsReadOnly();
            var iconFontFamily = Resources["GlyphFontFamily"].As<FontFamily>();
            var genderFontFamily = Resources["GenderFontFamily"].As<FontFamily>();
            return new DataManager(DataFolder, ExtensionsFolder, RecycleBinFolder,
                newCreds.SecurePassword, Resources);
        }

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            ApplicationData.Extensions = new List<ExtensionBase>();
            LogMessage("Application Starting", EntryTypes.Information);
            DataManager = Login();
            DataManager.FileChangedDetected += DataManager_FileChangedDetected;
        }

        private void DataManager_FileChangedDetected(object sender, FileChangeDetectedEventArgs e) {
            var win = MainWindow.As<MainWindow>();
            if (e.FileType == FileTypes.Extension) {
                LogMessage($"Extension ({Path.GetFileName(e.Filename)}) found", EntryTypes.Information);
                switch (e.ChangeType) {
                    case ChangeTypes.Add: {
                            LogMessage($"  Adding extension", EntryTypes.Information);
                            var assy = Assembly.Load(File.ReadAllBytes(e.Filename));
                            if (assy != null) {
                                var types = assy.GetTypes().Where(x => x.BaseType == typeof(ExtensionBase));
                                foreach (var type in types) {
                                    var instance = Activator.CreateInstance(type);
                                    if (instance != null) {
                                        var ext = instance.As<ExtensionBase>();
                                        if (ApplicationData.Extensions.Any(x => x.Name == ext.Name)) {
                                            continue;
                                        }

                                        ApplicationData.Extensions.Add(ext);
                                        ext.Filename = e.Filename;
                                        win.InitializeExtension(ext);
                                    }
                                }
                            }

                            break;
                        }
                    case ChangeTypes.Remove: {
                            LogMessage($"  Removing extension", EntryTypes.Information);
                            var ext = ApplicationData.Extensions.FirstOrDefault(x => x.Filename == e.Filename);
                            var name = ext.Name;

                            var ctrl = ext.Panel.Control;
                            if (ctrl != null && ctrl.Parent != null) {
                                ctrl.Parent.RemoveChild(ctrl);
                            }

                            ext.Destroy();

                            win.MainTabControl.Items.Remove(ext.TabItem);
                            ext.Panel.Control = null;
                            ApplicationData.Extensions.Remove(ext);

                            if (ext.Panel != null && ext.Panel.Control != null && ext.Panel.Control.Parent != null) {
                                ext.Panel.Control.Parent.RemoveChild(ext.Panel.Control);
                            }

                            break;
                        }
                    case ChangeTypes.Change:
                        break;
                    default:
                        break;
                }
            }
        }

        protected override void OnExit(ExitEventArgs e) {
            base.OnExit(e);
            LogMessage("Cleaning up temp directory", EntryTypes.Information);
            if (Directory.Exists(TempFolder)) {
                var files = Directory.GetFiles(TempFolder, "*.*");
                foreach (var file in files) {
                    File.Delete(file);
                }
            }
            DataManager?.Dispose();
            LogMessage("Application Ending", EntryTypes.Information);
        }

        private static void SetupFolders() {
#if DEBUG
            ApplicationFolder += " (Debug)";
#endif
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
            if (!Directory.Exists(RecycleBinFolder)) {
                Directory.CreateDirectory(RecycleBinFolder);
            }
            if (!Directory.Exists(ThemeFolder)) {
                Directory.CreateDirectory(ThemeFolder);
            }
        }
    }
}
