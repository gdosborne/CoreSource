namespace Imaginator
{
    using Imaginator.Views;
    using MVVMFramework;
    using System.Windows;
    using System.Windows.Input;

    public partial class SizesWindow : Window
    {
        public SizesWindow()
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

        public SizesWindowView View { get { return LayoutRoot.GetView<SizesWindowView>(); } }
    }
}
