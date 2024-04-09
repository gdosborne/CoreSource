using GregOsborne.Application.Primitives;
using System.Windows;

namespace BuildInno {
    public partial class SettingsWindow : Window {
        public SettingsWindow() {
            InitializeComponent();
            View.Initialize();

            TitlebarBorder.PreviewMouseDown += (s, e) => {
                this.DragMove();
            };

            View.PropertyChanged += (s, e) => {
                switch (e.PropertyName) {
                    case nameof(View.DialogResult):
                        this.DialogResult = View.DialogResult;
                        break;
                }
            };
        }

        public SettingsWindowView View => DataContext.As<SettingsWindowView>();
    }
}
