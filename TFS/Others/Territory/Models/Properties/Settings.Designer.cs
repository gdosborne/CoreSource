namespace Models.Properties {
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=gdos7825.db.11133221.hostedresource.com;Initial Catalog=gdos7825;Pers" +
            "ist Security Info=True;User ID=gdos7825;Password=Ozzy0118@")]
        public string gdos7825ConnectionString {
            get {
                return ((string)(this["gdos7825ConnectionString"]));
            }
        }
    }
}
