using GregOsborne.Application.Windows;
using System.Windows;

namespace ManageVersioning {
    public partial class SettingsWindow : Window {
        public SettingsWindow() {
            InitializeComponent();
            View.Initialize();
            View.PropertyChanged += (s, e) => {
                if (e.PropertyName.EqualsIgnoreCase("DialogResult")) {
                    DialogResult = View.DialogResult;
                }
            };
            Closing += (s, e) => {
                this.SavePosition(App.Session.ApplicationSettings);
            };
        }

        public SettingsWindowView View => DataContext.As<SettingsWindowView>();

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);
            this.HideMinimizeAndMaximizeButtons();
            if (App.Settings.AreWindowPositionsSaved) {
                this.SetPosition(App.Session.ApplicationSettings);
            }
        }
    }
}
