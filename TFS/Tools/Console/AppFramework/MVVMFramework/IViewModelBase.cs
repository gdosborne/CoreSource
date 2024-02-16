namespace GregOsborne.MVVMFramework {
	using System.ComponentModel;

	public interface IViewModelBase {
		event PropertyChangedEventHandler PropertyChanged;
		event ExecuteUiActionHandler ExecuteUiAction;
		void Initialize();
	}
}