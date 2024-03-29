namespace SNC.OptiRamp.Application.Developer {
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
	using GregOsborne.Application.Windows;
	using MVVMFramework;
	using SNC.OptiRamp.Application.Developer.Views;
	using SNC.OptiRamp.Application.DeveloperEntities.IO;

	internal partial class OpenRemoteWindow : Window {
		public OpenRemoteWindow() {
			InitializeComponent();
			View.Initialize(this);
		}

		public OpenRemoteWindowView View {
			get {
				return LayoutRoot.GetView<OpenRemoteWindowView>();
			}
		}

		#region Address
		public string Address {
			get {
				return (string)GetValue(AddressProperty);
			}
			set {
				SetValue(AddressProperty, value);
			}
		}

		public static readonly DependencyProperty AddressProperty = DependencyProperty.Register("Address", typeof(string), typeof(OpenRemoteWindow), new PropertyMetadata(string.Empty, onAddressChanged));
		private static void onAddressChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (OpenRemoteWindow)source;
			if (src == null)
				return;
			var value = (string)e.NewValue;
			src.View.Address = value;
			src.MyFileManager.Refresh();
		}
		#endregion

		protected override void OnSourceInitialized(EventArgs e) {
			base.OnSourceInitialized(e);
			this.HideMinimizeAndMaximizeButtons();
		}

		private void FileManager_DialogCancelled(object sender, EventArgs e) {
			View.DialogResult = false;
		}

		//private void FileManager_DialogCompleted(object sender, ProjectFileManager.FileSelectedEventArgs e) {
		//	View.FileName = e.Address + "/" + e.FileName;
		//	View.Address = e.Address;
		//	View.Stream = e.FileStream;
		//	DialogResult = true;
		//}

		private void OpenRemoteWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			switch (e.PropertyName) {
				case "DialogResult":
					DialogResult = View.DialogResult;
					break;
			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			View.Persist(this);
		}
	}
}
