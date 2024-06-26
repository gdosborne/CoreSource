using System;
using System.ComponentModel;
using System.Windows;
using GregOsborne.Application.Windows;
using GregOsborne.MVVMFramework;

namespace GregOsborne.Dialog {
    internal partial class MessageDialog : Window {
        public MessageDialog() {
            InitializeComponent();
        }

        public MessageDialogView View => LayoutRoot.GetView<MessageDialogView>();

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);
            if (!AllowClose)
                this.HideControlBox();
            this.HideMinimizeAndMaximizeButtons();
        }

        private void MessageDialogView_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == "ButtonValue") DialogResult = true;
        }

        public bool AllowClose {
            get => (bool) GetValue(AllowCloseProperty);
            set => SetValue(AllowCloseProperty, value);
        }

        public static readonly DependencyProperty AllowCloseProperty = DependencyProperty.Register("AllowClose", typeof(bool), typeof(MessageDialog), new PropertyMetadata(true, OnAllowCloseChanged));

        private static void OnAllowCloseChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
            var src = (MessageDialog) source;
            if (src == null)
                return;
            var value = (bool) e.NewValue;
        }
    }
}