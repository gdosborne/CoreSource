
namespace Imaginator
{
    using MVVMFramework;
    using System.Windows;
    using System.Windows.Input;
    using Views;

    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            View.PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs e) =>
            {
                switch (e.PropertyName)
                {
                    case "DialogResult":
                        DialogResult = View.DialogResult;
                        break;
                }
            };
        }
        private void Border_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Close_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        public SettingsWindowView View { get { return LayoutRoot.GetView<SettingsWindowView>(); } }

        private void ThisWindow_Loaded(object sender, RoutedEventArgs e)
        {
            View.InitView();
        }
    }
}
