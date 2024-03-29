using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using GregOsborne.Application.Primitives;
using GregOsborne.Application.Windows;
using GregOsborne.MVVMFramework;

namespace GregOsborne.Dialog {
    internal partial class CredentialsDialog : Window {
        public CredentialsDialog() {
            InitializeComponent();
        }

        public CredentialsDialogView View => LayoutRoot.GetView<CredentialsDialogView>();

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);
            this.HideMinimizeAndMaximizeButtons();
            this.HideControlBox();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e) {
            View.Password = sender.As<PasswordBox>().Password;
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e) {
            sender.As<PasswordBox>().SelectAll();
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [SuppressMessage("ReSharper", "SwitchStatementMissingSomeCases")]
        private void CredentialsDialogView_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            switch (e.PropertyName) {
                case "Credentials":
                    if (View.Credentials != null)
                        MyPassword.Password = View.Credentials.Password;
                    break;
                case "DialogResult":
                    DialogResult = View.DialogResult;
                    break;
            }
        }

        private void MyUserName_GotFocus(object sender, RoutedEventArgs e) {
            sender.As<TextBox>().SelectAll();
        }
    }
}