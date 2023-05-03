namespace GregOsborne.Developers.Tasks.Ext {
	using System.Windows;
	using GregOsborne.Application;
	using GregOsborne.Application.Primitives;
	using GregOsborne.MVVMFramework;

	public class DeveloperTasksView : ViewModelBase {
		public DeveloperTasksView() => this.MDL2AssetsCharacter = char.ConvertFromUtf32(60392);

		private string mdl2AssetsCharacter = default;
		public string MDL2AssetsCharacter {
			get => this.mdl2AssetsCharacter;
			set {
				this.mdl2AssetsCharacter = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private Visibility watermarkVisibility = default;
		public Visibility WatermarkVisibility {
			get => this.watermarkVisibility;
			set {
				this.watermarkVisibility = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}
	}
}
