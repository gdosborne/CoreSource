using GregOsborne.Application;
using GregOsborne.Application.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SetPath
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Left = Settings.GetSetting(App.ApplicationName, "Settings", "Left", Left);
            Top = Settings.GetSetting(App.ApplicationName, "Settings", "Top", Top);
            Width = Settings.GetSetting(App.ApplicationName, "Settings", "Width", Width);
            Height = Settings.GetSetting(App.ApplicationName, "Settings", "Height", Height);
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            sender.As<TextBox>().SelectAll();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DataContext.As<MainWindowView>().PathIsChanged)
            {
                var result = MessageBox.Show($"Path data has changed. Are you sure you want to exit?", "Exit Set Path?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
            Settings.SetSetting(App.ApplicationName, "Settings", "Left", RestoreBounds.Left);
            Settings.SetSetting(App.ApplicationName, "Settings", "Top", RestoreBounds.Top);
            Settings.SetSetting(App.ApplicationName, "Settings", "Width", RestoreBounds.Width);
            Settings.SetSetting(App.ApplicationName, "Settings", "Height", RestoreBounds.Height);
        }

        private void BorderLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var position = (int)sender.As<Border>().Tag;
            DataContext.As<MainWindowView>().SetSelectedItem(position);
        }
    }
}