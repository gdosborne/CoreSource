using OzFramework.Primitives;
using OzFramework.Windows;

using System.Windows;
using System.Windows.Controls;

namespace OzMiniDB.Builder {
    public partial class NewTableWindow : Window {
        public NewTableWindow() {
            InitializeComponent();
            View.Initialize();
            View.PropertyChanged += (s, e) => {
                if (e.PropertyName.Equals(nameof(DialogResult)))
                    DialogResult = View.DialogResult;
            };
        }

        public NewTableWindowView View => this.DataContext.As<NewTableWindowView>();

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);
            this.HideMinimizeAndMaximizeButtons();
        }

        private void TextBoxGotFocus(object sender, RoutedEventArgs e) {
            sender.As<TextBox>().SelectAll();
        }
    }
}
