using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using GregOsborne.Application.Windows;
using GregOsborne.MVVMFramework;

namespace GregOsborne.Dialog {
    internal partial class SetAddressDialog : Window {
        public SetAddressDialog() {
            InitializeComponent();
        }
        public SetAddressDialogView View => LayoutRoot.GetView<SetAddressDialogView>();
        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);
            this.HideControlBox();
            this.HideMinimizeAndMaximizeButtons();
        }
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private void SetAddressDialogView_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName.Equals("DialogResult"))
                DialogResult = View.DialogResult;
        }
    }
}