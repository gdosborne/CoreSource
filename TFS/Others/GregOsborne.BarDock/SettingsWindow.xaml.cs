using GregOsborne.Application.Primitives;
using GregOsborne.Application.Windows;
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
using System.Windows.Shapes;

namespace GregOsborne.BarDock
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            DataContext.As<SettingsView>().WindowVisibility = Visibility.Collapsed;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            this.HideMinimizeAndMaximizeButtons();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.SetSetting("MainWindow", "Left", RestoreBounds.Left);
            App.SetSetting("MainWindow", "Top", RestoreBounds.Top);
            App.SetSetting("MainWindow", "Width", RestoreBounds.Width);
            App.SetSetting("MainWindow", "Height", RestoreBounds.Height);
            e.Cancel = true;
            Visibility = Visibility.Collapsed;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Left = App.GetSetting("MainWindow", "Left", RestoreBounds.Left);
            Top = App.GetSetting("MainWindow", "Top", RestoreBounds.Top);
            Width = App.GetSetting("MainWindow", "Width", RestoreBounds.Width);
            Height = App.GetSetting("MainWindow", "Height", RestoreBounds.Height);
        }
    }
}
