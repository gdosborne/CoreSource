using System.Windows;
using Toolbar.Controller;

namespace ApplicationSettings.Addon {
    public partial class App : ToolbarApp {
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            if (IsShutdownRequired) {
            }
        }
    }
}
