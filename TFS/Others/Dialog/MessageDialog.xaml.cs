namespace GregOsborne.Dialog {
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
	using MVVMFramework;
	using GregOsborne.Application.Windows;

	internal partial class MessageDialog : Window {
		public MessageDialog() {
			InitializeComponent();
		}
		public MessageDialogView View {
			get {
				return LayoutRoot.GetView<MessageDialogView>();
			}
		}
		protected override void OnSourceInitialized(EventArgs e) {
			base.OnSourceInitialized(e);
			if (!AllowClose)
				this.HideControlBox();
			this.HideMinimizeAndMaximizeButtons();
		}

		#region AllowClose
		public bool AllowClose {
			get {
				return (bool)GetValue(AllowCloseProperty);
			}
			set {
				SetValue(AllowCloseProperty, value);
			}
		}

		public static readonly DependencyProperty AllowCloseProperty = DependencyProperty.Register("AllowClose", typeof(bool), typeof(MessageDialog), new PropertyMetadata(true, onAllowCloseChanged));
		private static void onAllowCloseChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (MessageDialog)source;
			if (src == null)
				return;
			var value = (bool)e.NewValue;
		}
		#endregion

		private void MessageDialogView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			switch (e.PropertyName) {
				case "ButtonValue":
					DialogResult = true;
					break;
			}
		}
	}
}
