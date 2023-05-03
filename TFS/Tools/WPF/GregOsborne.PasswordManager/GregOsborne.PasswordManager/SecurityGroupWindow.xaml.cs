using System.Windows;

namespace GregOsborne.PasswordManager {
    public partial class SecurityGroupWindow : Window {
        public SecurityGroupWindow() {
            this.InitializeComponent();
        }

        public SecurityGroupWindowView View => (SecurityGroupWindowView)this.DataContext;

        private void TheWindow_Loaded(object sender, RoutedEventArgs e) {
            this.View.Initialize();

            this.View.PropertyChanged += View_PropertyChanged;
            this.View.ExecuteUiAction += this.View_ExecuteUiAction;
        }

        private void View_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName == "DialogResult")
                this.DialogResult = View.DialogResult;
        }

        private void View_ExecuteUiAction(object sender, MVVMFramework.ExecuteUiActionEventArgs e) {
            switch (e.CommandToExecute) {
                case "CloseApplication": {
                    this.Close();
                    break;
                }
            }
        }

        private void TheWindow_SizeChanged(object sender, SizeChangedEventArgs e) {

        }

        private void TheWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) {

        }
    }
}
