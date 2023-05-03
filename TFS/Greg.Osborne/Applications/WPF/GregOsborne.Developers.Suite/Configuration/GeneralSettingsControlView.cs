using GregOsborne.Application.Primitives;
using GregOsborne.MVVMFramework;

namespace GregOsborne.Developers.Suite.Configuration {
	public class GeneralSettingsControlView : ViewModelBase {
		public override void Initialize() {
		}

		private bool showWatermarks = default;
		public bool ShowWaterMarks {
			get => this.showWatermarks;
			set {
				this.showWatermarks = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}
	}
}
