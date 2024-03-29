namespace SNC.OptiRamp.Application.Developer.Extensions.FileManagerExtension {
	using System;
	using System.ComponentModel;
	using System.Windows;

	public class ExtensionControlView : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;
		public void UpdateInterface() {

		}
		public void InitView() {

		}
		public void Initialize(Window window) {

		}
		public void Persist(Window window) {

		}
		private bool? _DialogResult;
		public bool? DialogResult {
			get {
				return _DialogResult;
			}
			set {
				_DialogResult = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("DialogResult"));
			}
		}
	}
}
