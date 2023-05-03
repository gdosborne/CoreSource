using GregOsborne.Application;
using GregOsborne.Application.Primitives;
using GregOsborne.RegistryHack.Data;
using System.Windows;
using System.Windows.Controls;

namespace RegistryHack
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Left = Settings.GetSetting(App.ApplicationName, Name, "Left", Left);
            Top = Settings.GetSetting(App.ApplicationName, Name, "Top", Top);
            Width = Settings.GetSetting(App.ApplicationName, Name, "Width", Width);
            Height = Settings.GetSetting(App.ApplicationName, Name, "Height", Height);
            WindowState = Settings.GetSetting(App.ApplicationName, Name, "Height", WindowState.Normal);
            var splitterPosition = Settings.GetSetting(App.ApplicationName, Name, "VerticalSplitterPosition", 150.0);
            splitterPosition = splitterPosition < 150 ? 150 : splitterPosition;
            ControllerGrid.ColumnDefinitions[0].Width = new GridLength(splitterPosition);
            var firstColumnWidth = Settings.GetSetting(App.ApplicationName, Name, "FirstColumnWidth", 0.0);
            if (firstColumnWidth == 0)
                theDataGrid.Columns[0].Width = new DataGridLength(0, DataGridLengthUnitType.Auto);
            else
                theDataGrid.Columns[0].Width = new DataGridLength(firstColumnWidth);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Settings.SetSetting(App.ApplicationName, Name, "WindowState", WindowState);
            Settings.SetSetting(App.ApplicationName, Name, "Left", RestoreBounds.Left);
            Settings.SetSetting(App.ApplicationName, Name, "Top", RestoreBounds.Top);
            Settings.SetSetting(App.ApplicationName, Name, "Width", RestoreBounds.Width);
            Settings.SetSetting(App.ApplicationName, Name, "Height", RestoreBounds.Height);
            var splitterPosition = ControllerGrid.ColumnDefinitions[0].Width.Value;
            Settings.SetSetting(App.ApplicationName, Name, "VerticalSplitterPosition", splitterPosition);
            var firstColumnWidth = theDataGrid.Columns[0].Width.Value;
            Settings.SetSetting(App.ApplicationName, Name, "FirstColumnWidth", firstColumnWidth);
        }
    }
}