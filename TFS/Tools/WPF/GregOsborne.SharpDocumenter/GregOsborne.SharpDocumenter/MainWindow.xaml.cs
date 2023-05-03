namespace GregOsborne.SharpDocumenter
{
    using GregOsborne.Application.Primitives;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MouseOverBrush = Application.Current.Resources["ControlButtonBackgroundMouseOver"].As<Brush>();
            MouseDownBrush = Application.Current.Resources["ControlButtonBackgroundMouseDown"].As<Brush>();
            MouseCurrentBrush = Application.Current.Resources["ControlButtonBackground"].As<Brush>();
            DataContext.As<MainWindowView>().PropertyChanged += MainWindow_PropertyChanged;
        }

        private void MainWindow_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var view = sender.As<MainWindowView>();
            if(e.PropertyName == "WindowState")
            {
                WindowState = view.WindowState;
            }
        }

        private Brush MouseOverBrush = null;
        private Brush MouseDownBrush = null;
        private Brush MouseCurrentBrush = null;
        private void CaptionPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void borderMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var bdr = sender.As<Border>();
            bdr.Background = MouseDownBrush;
            if (bdr.Name == "borderMinimize" || bdr.Name == "borderClose")
                bdr.BorderThickness = new Thickness(1.5, 0.5, 0.5, 0.5);
            else if (bdr.Name == "borderMaximize")
                bdr.BorderThickness = new Thickness(0.5, 0.5, 0.5, 1.5);
        }

        private void borderMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var bdr = sender.As<Border>();
            bdr.Background = MouseOverBrush;
            if (bdr.Name == "borderMinimize" || bdr.Name == "borderClose")
                bdr.BorderThickness = new Thickness(1, 0, 1, 1);
            else if (bdr.Name == "borderMaximize")
                bdr.BorderThickness = new Thickness(0, 0, 0, 1);
        }

        private void borderMouseEnter(object sender, MouseEventArgs e)
        {
            sender.As<Border>().Background = MouseOverBrush;
        }

        private void borderMouseLeave(object sender, MouseEventArgs e)
        {
            sender.As<Border>().Background = MouseCurrentBrush;
        }
    }
}
