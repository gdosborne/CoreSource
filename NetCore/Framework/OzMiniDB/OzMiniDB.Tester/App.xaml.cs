using OzFramework;
using OzFramework.Primitives;

using System.Windows;

namespace OzMiniDB.Builder {
    public partial class App : System.Windows.Application {
        public static class Constants {
            internal static string SaveWinSizeAndLoc => "Save Window Size and Location";
            internal static string Application => "Application";
            internal static string General => "General";
            internal static string Database => "Database";
            internal static string UserInterface => "User Interface";
            internal static string LastGrpSelected => "Last Group Selected";
            internal static string Left => "Left";
            internal static string Top => "Top";
            internal static string Width => "Width";
            internal static string Height => "Height";
            internal static string HasSettings => "HasSettings";
            internal static string WindowState => "WindowState";
            internal static string Fname => "filename";
            internal static string FileName => "Filename";
            internal static string Cancel => "cancel";
            internal static string LastFldr => "Last Folder";
            internal static string SaveDbFile => "Save Database File";
            internal static string ReplDbFile => "Replace Database File";
            internal static string DbExtension => "*.database";
            internal static string DbExtName => "Database Files";
            internal static string AllFilesExtension => "*.*";
            internal static string AllFileExtName => "All Files";
            internal static string OpnDB => "Open database...";
            internal static string SaveDB => "Save database...";
            internal static string Value => "Value";
            internal static string Name => "Name";
            internal static string NewField => "_NewField_";
            internal static string NewDatabase => "New Database";

            internal static string DefaultDBName => "My Personal.database";
            internal static string StdTextBoxStyleName => "StandardTextBox";
            internal static string StdCheckBoxStyleName => "StandardCheckBox";
            internal static string StdRadioButton => "StandardRadioButton";
            internal static string StdComboBox => "StandardComboBox";
            internal static string StdHeaderStyleName => "SubHeaderTextBlock";
            internal static string IconFontFamilyName => "IconFont";
            internal static string IconFontSizeName => "IconFontSize";
            internal static string GenTopLevelDBEClass => "Generate Top-Level DBEngine Class";
            internal static string ImplementPropertyChanged => "Data classes implement INotifyPropertyChanged";
            internal static string ListType => "List Type";
            internal static string PartialMethods => "Partial Methods (check all that apply)";
            internal static string ImplementPartialMethods => "Data classes implement partial Methods";

            internal static string GenTopLevDBDClassTip => "If checked, a top-level DBEngine class will be generated to access your data";
            internal static string ImplementPropertyChangedTip => "If checked, all data classes will implement interface INotifyPropertyChanged";
            internal static string ImplementPartialMethodsTip => "If checked, all data classes will implement the selected partial Methods";
            internal static string ListTypeTip => "The type of list to generate on DBEngine (if selected)";
            internal static string PartialMethodTip => "Select all methods to be generated";
            internal static string PathToDBDefFileTip => "Path to the database definition file";
            internal static string TheDBNameTip => "This will be the database name";
        }

        public static Session Session { get; set; }
        public static string ApplicationName => "OZ Database";
        public static string ApplicationDirectory =>
            SysIO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName);
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            Session = new Session(ApplicationDirectory, ApplicationName, OzFramework.Logging.ApplicationLogger.StorageTypes.FlatFile,
                OzFramework.Logging.ApplicationLogger.StorageOptions.CreateFolderForEachDay);
        }

        public static T FindResource<T>(ResourceDictionary resourceDic, string name, T defaultValue = default) {
            try {
                T result = defaultValue;
                var item = resourceDic[name];
                if (!item.IsNull() && item is T tValue) {
                    result = tValue;
                    return result;
                }
                resourceDic.MergedDictionaries.ToList().ForEach(res => {
                    if (!result.IsNull())
                        return;
                    result = FindResource<T>(res, name, defaultValue);
                });
                return result;
            }
            catch { return defaultValue; }
        }
    }
}
