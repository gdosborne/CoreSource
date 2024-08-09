using Common.Primitives;
using Common.Windows.Controls;
using Congregation.Scheduler.Views;

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Congregation.Scheduler {
    internal partial class MainWindow : Window {
        public MainWindow () {
            InitializeComponent();
            View.Initialize();

            SourceInitialized += (s, e) => {
                Left = App.Session.ApplicationSettings.GetValue(nameof(MainWindow), nameof(Left), Left);
                Top = App.Session.ApplicationSettings.GetValue(nameof(MainWindow), nameof(Top), Top);
                Width = App.Session.ApplicationSettings.GetValue(nameof(MainWindow), nameof(Width), Width);
                Height = App.Session.ApplicationSettings.GetValue(nameof(MainWindow), nameof(Height), Height);
                WindowState = App.Session.ApplicationSettings.GetValue(nameof(MainWindow), nameof(WindowState), WindowState.Normal);

                VerticalToolbar.RemoveOverflow();
            };

            Closing += (s, e) => {
                App.Session.ApplicationSettings.AddOrUpdateSetting(nameof(MainWindow), nameof(Left), RestoreBounds.Left);
                App.Session.ApplicationSettings.AddOrUpdateSetting(nameof(MainWindow), nameof(Top), RestoreBounds.Top);
                App.Session.ApplicationSettings.AddOrUpdateSetting(nameof(MainWindow), nameof(Width), RestoreBounds.Width);
                App.Session.ApplicationSettings.AddOrUpdateSetting(nameof(MainWindow), nameof(Height), RestoreBounds.Height);
                App.Session.ApplicationSettings.AddOrUpdateSetting(nameof(MainWindow), nameof(WindowState), WindowState);
            };
        }

        internal MainWindowViewModel View => DataContext.As<MainWindowViewModel>();
    }
}