namespace GregOsborne.Item.Templates {
	using System;
	using GregOsborne.Application;
	using GregOsborne.MVVMFramework;

	public partial class ViewModelWindowView : ViewModelBase {
		public ViewModelWindowView() {
			//all setting of properties here are applied tp both design time and runtime
			this.Title = "View Model Window";
		}

		public new event ExecuteUiActionHandler ExecuteUiAction;
		public override void Initialize() {
			//all setting of properties here are applied only at runtime
		}

		//place all data values to bind to the window in this class

		private string title = default;
		public string Title {
			get => this.title;
			set {
				this.title = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}
	}
}