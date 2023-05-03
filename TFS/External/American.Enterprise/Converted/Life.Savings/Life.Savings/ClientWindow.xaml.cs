using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GregOsborne.Application.Primitives;
using GregOsborne.Application.Windows;
using GregOsborne.Dialog;

namespace Life.Savings
{
    public partial class ClientWindow : Window
    {
        public ClientWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            this.HideMinimizeAndMaximizeButtons();
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataContext.As<ClientWindowView>().SelectClientCommand.Execute(null);
        }

        private void ClientWindowView_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("DialogResult"))
                DialogResult = sender.As<ClientWindowView>().DialogResult;
        }

        private void ListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                if (DataContext.As<ClientWindowView>().SelectedClient != null)
                    DataContext.As<ClientWindowView>().SelectClientCommand.Execute(null);
            }
        }

        private void ListBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (DataContext.As<ClientWindowView>().SelectedClient == null)
                DataContext.As<ClientWindowView>().SelectedClient = DataContext.As<ClientWindowView>().Clients.FirstOrDefault();
        }

        private void ClientWindowView_DeleteTheClient(object sender, Events.DeleteClientEventArgs e)
        {
            var td = new TaskDialog
            {
                Image = ImagesTypes.Warning,
                MessageText = "You are about to delete the selected client. If you continue you will not be able to access the client again.\n\nDo you want to delete the client?",
                Title = "Static data refresh",
                Width = 400,
                Height = 200,
            };
            td.AddButtons(ButtonTypes.Yes, ButtonTypes.No);
            var result = td.ShowDialog(this);
            e.IsContinueDelete = result == (int)ButtonTypes.Yes;
        }
    }
}