namespace GregOsborne.Developers.Suite {
	using System.Collections.ObjectModel;
	using System.Windows;
	using GregOsborne.Application.Primitives;
	using GregOsborne.MVVMFramework;
	using GregOsborne.Suite.Extender;

	public partial class ExtensionManagerWindowView : ViewModelBase {
		public ExtensionManagerWindowView() {
			this.Extensions = new ObservableCollection<IExtender>();
			App.Current.As<App>().ExtensionManager.BeginRead();
			while (App.Current.As<App>().ExtensionManager.HasNextExtension) {
				var extension = App.Current.As<App>().ExtensionManager.GetNextExtension();
				this.Extensions.Add(extension);
			}
			App.Current.As<App>().ExtensionManager.ExtensionAdded += this.ExtensionManager_ExtensionAdded;
			this.SecondLineVisibility = Visibility.Visible;
		}

		public override void Initialize() {
			this.SecondLineVisibility = Visibility.Collapsed;
		}

		private void ExtensionManager_ExtensionAdded(object sender, ExtensionAddedEventArgs e) => this.Extensions.Add(e.Extension);

		private IExtender selectedExtension = default;
		public IExtender SelectedExtension {
			get => this.selectedExtension;
			set {
				this.selectedExtension = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private bool? dialogResult = default;
		public bool? DialogResult {
			get => this.dialogResult;
			set {
				this.dialogResult = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private ObservableCollection<IExtender> extentions = default;
		public ObservableCollection<IExtender> Extensions {
			get => this.extentions;
			set {
				this.extentions = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private Visibility secondLineVisibility = default;
		public Visibility SecondLineVisibility {
			get => this.secondLineVisibility;
			set {
				this.secondLineVisibility = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}
	}
}
