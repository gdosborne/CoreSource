namespace GregOsborne.Suite.Extender.UserControls {
	using GregOsborne.Application.Primitives;
	using GregOsborne.MVVMFramework;

	public class LabeledToggleSwitchView : ViewModelBase {
		public LabeledToggleSwitchView() {
			this.LabelText = "Label goes here";
			this.IsChecked = true;
		}

		public override void Initialize() {
		}


		private string labelText = default;
		public string LabelText {
			get => this.labelText;
			set {
				this.labelText = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}


		private bool isChecked = default;
		public bool IsChecked {
			get => this.isChecked;
			set {
				this.isChecked = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}
	}
}
