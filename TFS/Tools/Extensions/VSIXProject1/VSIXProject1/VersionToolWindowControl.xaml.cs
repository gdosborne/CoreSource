namespace VSIXProject1
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Controls;

    public partial class VersionToolWindowControl : UserControl
    {
        public VersionToolWindowControl()
        {
            InitializeComponent();

            View.PropertyChanged += View_PropertyChanged;
        }

        private void View_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
        }

        public VersionToolWindowControlView View => (DataContext as VersionToolWindowControlView);
    }
}