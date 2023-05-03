using System.Windows;
using System.Windows.Controls;
using GregOsborne.Application.Primitives;
using Ookii.Dialogs.Wpf;
using GregOsborne.Dialog;

namespace Life.Savings {
    public partial class SettingsWindow : Window {
        public SettingsWindow() {
            InitializeComponent();
        }

        private void View_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName.Equals("DialogResult"))
                DialogResult = View.DialogResult;
        }

        public SettingsWindowView View => DataContext.As<SettingsWindowView>();

        private void SettingsWindowView_ShowBrowseFolder(object sender, Events.ShowBrowseFolderEventArgs e) {
            var folderDialog = new VistaFolderBrowserDialog {
                Description = e.Prompt,
                UseDescriptionForTitle = true,
                RootFolder = System.Environment.SpecialFolder.Desktop,
                SelectedPath = e.InitialFolderPath,
                ShowNewFolderButton = true
            };
            var result = folderDialog.ShowDialog(this);
            e.IsCancel = (!result.HasValue || !result.Value);
            e.SelectedFolderPath = e.IsCancel ? e.InitialFolderPath : folderDialog.SelectedPath;
        }

        private void RepoLocationTextBox_GotFocus(object sender, RoutedEventArgs e) {
            sender.As<TextBox>().SelectAll();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            RepoLocationTextBox.Focus();
        }
    }
}